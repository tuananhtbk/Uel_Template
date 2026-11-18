// Cửa sổ thử nghiệm chung: chọn component -> chỉnh thông số -> chọn cấu kiện -> dựng / dựng lại / xoá.
// Đối tượng tạo ra là đối tượng thường trong model (không phải component) -> dùng nút "Xoa ket qua" để dọn.
// Mỗi lần dựng, EXE kiểm tra các file _Dev\<bản>\*.Core.dll: file nào vừa build lại thì nạp bản mới, giữ nguyên thông số đang nhập.
// Nút "Build Core + dung lai" (18/11/2026): chạy build.bat core <Ten> <bộ> rồi nạp Core mới và dựng lại.
// Tab "Knobs": chỉnh các số trong khối // <KNOBS> của .cs, xem ngay trong Tekla, ghi lại vào .cs (xem KnobPanel.cs).
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using UEL.Host;
using Color = System.Drawing.Color;

namespace UEL.Tester
{
    internal class TesterForm : Form
    {
        private readonly ComboBox _combo = new ComboBox();
        private readonly PropertyGrid _grid = new PropertyGrid();
        private readonly TabControl _views = new TabControl();
        private readonly TabPage _dialogPage = new TabPage("Hop thoai");
        private readonly TabPage _gridPage = new TabPage("Bang thong so (tat ca, ke ca gia dinh)");
        private readonly TabPage _knobPage = new TabPage("Knobs (so hinh hoc trong code)");
        private readonly KnobPanel _knobs;
        private Button _buildBtn;
        private System.Diagnostics.Process _buildProc;   // build.bat core dang chay (null = khong)
        private InpView _inp;            // hộp thoại vẽ từ .inp của component đang chọn
        private string _inpKey;
        private string _inpStamp;        // đường dẫn + thời điểm file DLL chứa .inp (đổi thì vẽ lại)
        private readonly TextBox _log = new TextBox();
        private readonly Label _status = new Label();
        private readonly Label _coreInfo = new Label();

        private CoreHost _core;                 // Core của component đang chọn
        private readonly List<CoreHost> _cores = new List<CoreHost>();   // mọi Core đang dùng (mỗi component 1 file)
        private string _key;
        private object _params;
        private Model _model;
        private ModelObject _primary;
        private List<ModelObject> _secondaries = new List<ModelObject>();
        private ArrayList _last = new ArrayList();
        private readonly Action<string> _logHandler;

        public TesterForm()
        {
            Text = "UEL - thu nghiem component (Tekla " + Program.CoreTarget + ")";
            // 17/11/2026: người dùng muốn mặc định nhỏ bằng 1/4 (diện tích) cửa sổ cũ (1594x1032) -> ~800x520.
            // Hộp thoại lớn thì cuộn trong tab; kéo giãn cửa sổ nếu cần.
            Size = new Size(800, 520);
            MinimumSize = new Size(500, 360);
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            _logHandler = AppendLog;

            Panel top = new Panel();
            top.Dock = DockStyle.Top;
            top.Height = 30;
            Label lb = new Label();
            lb.Text = "Component:";
            lb.AutoSize = true;
            lb.Location = new Point(6, 8);
            _combo.DropDownStyle = ComboBoxStyle.DropDownList;
            _combo.Location = new Point(80, 4);
            _combo.Width = 420;
            _combo.SelectedIndexChanged += OnComponentChanged;
            top.Controls.Add(lb);
            top.Controls.Add(_combo);

            FlowLayoutPanel buttons = new FlowLayoutPanel();
            buttons.Dock = DockStyle.Top;
            buttons.AutoSize = true;                            // cửa sổ hẹp -> thêm hàng nút, không bị che
            buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttons.Padding = new Padding(4);
            AddButton(buttons, "1. Chon cau kien va dung", OnPickAndBuild, 190);
            AddButton(buttons, "2. Dung lai (giu cau kien da chon)", OnRebuild, 220);
            _buildBtn = AddButton(buttons, "Build Core + dung lai", OnBuildCore, 150);
            AddButton(buttons, "Ghi bang thong so vao Params.cs", OnWriteParams, 210);
            AddButton(buttons, "Xoa ket qua vua dung", OnDelete, 150);
            AddButton(buttons, "Tra ve mac dinh", OnReset, 120);
            AddButton(buttons, "Mo file log", OnOpenLog, 100);
            AddButton(buttons, "So sanh: component goc  vs  plugin", OnCompare, 240);
            AddButton(buttons, "Xuat JSON component", OnExport, 150);
            AddButton(buttons, "Mo thu muc JSON", OnOpenExport, 120);

            _status.Dock = DockStyle.Top;
            _status.Height = 22;
            _status.TextAlign = ContentAlignment.MiddleLeft;
            _coreInfo.Dock = DockStyle.Top;
            _coreInfo.Height = 20;
            _coreInfo.TextAlign = ContentAlignment.MiddleLeft;
            _coreInfo.ForeColor = Color.DimGray;

            _grid.Dock = DockStyle.Fill;
            _grid.PropertySort = PropertySort.Categorized;
            _grid.PropertyValueChanged += delegate { if (_inp != null) _inp.RefreshDefaults(_params); };
            _gridPage.Controls.Add(_grid);
            _views.Dock = DockStyle.Fill;
            _views.TabPages.Add(_dialogPage);
            _views.TabPages.Add(_gridPage);
            _knobs = new KnobPanel(AppendLog, delegate { OnRebuild(this, EventArgs.Empty); }, delegate { StartBuildCore(); });
            _knobPage.Controls.Add(_knobs);
            _views.TabPages.Add(_knobPage);
            _views.SelectedIndexChanged += delegate { if (_views.SelectedTab == _gridPage) _grid.Refresh(); };

            _log.Dock = DockStyle.Bottom;
            _log.Height = 90;
            _log.Multiline = true;
            _log.ScrollBars = ScrollBars.Both;
            _log.WordWrap = false;
            _log.ReadOnly = true;
            _log.Font = new Font("Consolas", 9f);

            Controls.Add(_views);
            Controls.Add(_log);
            Controls.Add(_coreInfo);
            Controls.Add(_status);
            Controls.Add(buttons);
            Controls.Add(top);

            Load += delegate
            {
                EnsureCore();
                Connect();
            };
            FormClosed += delegate { foreach (CoreHost c in _cores) c.Subscribe(_logHandler, false); };
        }

        /// _Dev\18.0 hoặc _Dev\2020+ (theo bản Tekla đang dùng), chứa các file <Ten>.Core.dll.
        private static string CoreDir
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Program.CoreTarget); }
        }

        private static Button AddButton(Control parent, string text, EventHandler click, int width)
        {
            Button b = new Button();
            b.Text = text;
            b.Width = width;
            b.Height = 28;
            b.Click += click;
            parent.Controls.Add(b);
            return b;
        }

        /// Plugin\ (thư mục cha của _Dev).
        private static string PluginRoot
        {
            get { return Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')); }
        }

        // ---------------------------------------------------------------- Ghi mặc định vào Params.cs

        /// Ghi các giá trị đã đổi ở tab "Bang thong so" thành mặc định trong Params.cs (xem ParamsWriter.cs), rồi build Core.
        private void OnWriteParams(object sender, EventArgs e)
        {
            if (!EnsureCore() || _key == null || _params == null) return;
            string cls = _params.GetType().Name;
            string file = ParamsWriter.FindFile(Path.Combine(PluginRoot, _key), cls);
            if (file == null) { AppendLog("Khong thay file .cs co ham khoi tao " + cls + "() trong Plugin\\" + _key); return; }

            List<ParamChange> changes;
            try { changes = ParamsWriter.Diff(_params, _core.NewParams(_key), file); }
            catch (Exception ex) { AppendLog("Loi doc " + file + ": " + ex.Message); return; }
            if (changes == null) { AppendLog("Khong doc duoc ham khoi tao " + cls + "() trong " + file); return; }
            if (changes.Count == 0)
            {
                AppendLog("Bang thong so giong mac dinh trong " + Path.GetFileName(file) + " -> khong co gi de ghi." +
                          " (Chi ghi tab 'Bang thong so'; o nhap trong tab 'Hop thoai' khong tinh.)");
                return;
            }

            StringBuilder sb = new StringBuilder();
            int ok = 0;
            foreach (ParamChange c in changes) if (c.Skip == null) ok++;
            sb.AppendLine("Ghi " + ok + " gia tri vao " + _key + "\\" + Path.GetFileName(file) + " (mac dinh):");
            sb.AppendLine();
            foreach (ParamChange c in changes)
                if (c.Skip == null) sb.AppendLine("  " + c.Label + ":   " + c.OldText + "  ->  " + c.NewText + "   (dong " + c.Line + ")");
            bool anySkip = false;
            foreach (ParamChange c in changes)
            {
                if (c.Skip == null) continue;
                if (!anySkip) { sb.AppendLine(); sb.AppendLine("KHONG ghi:"); anySkip = true; }
                sb.AppendLine("  " + c.Label + " = " + c.NewText + ": " + c.Skip);
            }
            if (ok == 0)
            {
                foreach (string l in sb.ToString().Split('\n')) if (l.Trim().Length > 0) AppendLog(l.TrimEnd('\r'));
                AppendLog("Khong co gia tri nao ghi duoc.");
                return;
            }
            sb.AppendLine();
            sb.AppendLine("CANH BAO: moi lien ket " + _key + " da co trong model ma de TRONG cac o nay se doi hinh hoc");
            sb.AppendLine("o lan Modify / dung lai tiep theo (ke ca model cu), khi Core / DLL moi duoc dung.");
            sb.AppendLine("Mac dinh cu (theo .uel goc) duoc ghi chu cuoi dong; nho ghi vao README muc 'Gia dinh'.");
            sb.AppendLine();
            sb.AppendLine("Sau khi ghi: build Core. Ban DLL cai trong Tekla chi doi khi build ban phat hanh / _DEV.");
            if (MessageBox.Show(this, sb.ToString(), "Ghi mac dinh vao Params.cs", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                AppendLog("Da huy ghi Params.cs.");
                return;
            }
            try
            {
                int n = ParamsWriter.Write(file, cls, changes, AppendLog);
                AppendLog("Params: da ghi " + n + " gia tri vao " + file);
                if (n > 0) StartBuildCore();
            }
            catch (Exception ex) { AppendLog("Loi ghi " + file + ": " + ex.Message); }
        }

        // ---------------------------------------------------------------- Build Core

        private void OnBuildCore(object sender, EventArgs e)
        {
            StartBuildCore();
        }

        /// Chạy  _Tools\build.ps1 core <Ten> <bộ>  (chỉ Core của component đang chọn, đúng bộ của Tekla đang nối).
        /// Xong mà thành công: nạp Core mới, cập nhật hộp thoại, dựng lại nếu đã chọn cấu kiện.
        private void StartBuildCore()
        {
            if (_key == null) { AppendLog("Chua chon component."); return; }
            if (_buildProc != null) { AppendLog("Dang build, doi xong da."); return; }
            string ps1 = Path.Combine(PluginRoot, @"_Tools\build.ps1");
            if (!File.Exists(ps1)) { AppendLog("Khong thay " + ps1); return; }
            string target = Program.CoreTarget == "2020+" ? "2020" : "18";
            string key = _key;

            System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo("powershell.exe",
                "-NoProfile -ExecutionPolicy Bypass -File \"" + ps1 + "\" core \"" + key + "\" " + target);
            si.UseShellExecute = false;
            si.CreateNoWindow = true;
            si.RedirectStandardOutput = true;
            si.RedirectStandardError = true;
            si.WorkingDirectory = PluginRoot;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = si;
            p.EnableRaisingEvents = true;
            p.OutputDataReceived += delegate(object s, System.Diagnostics.DataReceivedEventArgs a) { if (a.Data != null) AppendLog("  " + a.Data); };
            p.ErrorDataReceived += delegate(object s, System.Diagnostics.DataReceivedEventArgs a) { if (a.Data != null) AppendLog("  " + a.Data); };
            p.Exited += delegate
            {
                int code = p.ExitCode;
                BeginInvoke(new Action(delegate { AfterBuildCore(key, code); }));
            };
            AppendLog("---- build.bat core " + key + " " + target + " ----");
            _buildBtn.Enabled = false;
            _buildBtn.Text = "Dang build...";
            _buildProc = p;
            try
            {
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                _buildProc = null;
                AppendLog("Khong chay duoc build: " + ex.Message);
                _buildBtn.Enabled = true;
                _buildBtn.Text = "Build Core + dung lai";
            }
        }

        private void AfterBuildCore(string key, int code)
        {
            if (_buildProc != null) { try { _buildProc.WaitForExit(); _buildProc.Dispose(); } catch { } }
            _buildProc = null;
            _buildBtn.Enabled = true;
            _buildBtn.Text = "Build Core + dung lai";
            if (code != 0)
            {
                AppendLog("BUILD LOI (xem dong csc o tren) -> van dung Core cu.");
                return;
            }
            AppendLog("Build xong.");
            if (!EnsureCore()) return;
            if (_key != key) return;
            if (_primary != null && Connect()) OnRebuild(this, EventArgs.Empty);
            else AppendLog("Chua chon cau kien -> bam nut 1 de dung.");
        }

        private void AppendLog(string line)
        {
            if (InvokeRequired) { BeginInvoke(new Action<string>(AppendLog), line); return; }
            _log.AppendText(line + Environment.NewLine);
        }

        // ---------------------------------------------------------------- Core

        /// Nạp lại Core nếu vừa build, rồi vẽ lại hộp thoại nếu .inp vừa đổi (build.bat dev).
        private bool EnsureCore()
        {
            bool ok = EnsureCoreFiles();
            if (ok)
            {
                RefreshDialog();
                SyncKnobs();
            }
            return ok;
        }

        private string _knobKey;

        /// Tab Knobs theo component đang chọn. Cùng component thì quét lại .cs (giữ số đang chỉnh nếu chưa ghi).
        private void SyncKnobs()
        {
            if (_key == null) return;
            if (_knobKey != _key)
            {
                _knobKey = _key;
                _knobs.LoadComponent(Path.Combine(PluginRoot, _key));
            }
            else _knobs.Rescan(false);
            _knobPage.Text = "Knobs (" + _knobs.Knobs.Count + ")" + (_knobs.Dirty ? " *" : "");
        }

        /// Nạp (hoặc nạp lại nếu vừa build) mọi file *.Core.dll trong CoreDir. Giữ nguyên thông số đang nhập.
        private bool EnsureCoreFiles()
        {
            List<CoreHost> now = new List<CoreHost>();
            string[] files = Directory.Exists(CoreDir) ? Directory.GetFiles(CoreDir, "*.Core.dll") : new string[0];
            Array.Sort(files, StringComparer.OrdinalIgnoreCase);
            foreach (string f in files)
            {
                try { now.Add(CoreHost.LoadFile(f)); }
                catch (Exception ex) { AppendLog("Khong nap duoc " + Path.GetFileName(f) + ": " + ex.Message); }
            }
            if (now.Count == 0)
            {
                AppendLog("Chua co Core nao trong " + CoreDir + "  -> chay build.bat core");
                _coreInfo.Text = "Core: CHUA NAP (" + CoreDir + ")";
                return false;
            }

            bool changed = now.Count != _cores.Count;
            for (int i = 0; !changed && i < now.Count; i++) changed = !ReferenceEquals(now[i], _cores[i]);
            if (!changed) return _core != null;

            foreach (CoreHost c in _cores) c.Subscribe(_logHandler, false);
            if (_cores.Count > 0) AppendLog("---- Da nap lai Core moi ----");
            _cores.Clear();
            _cores.AddRange(now);
            foreach (CoreHost c in _cores) c.Subscribe(_logHandler, true);

            string keep = _key;
            CoreHost oldCore = _core;
            _combo.SelectedIndexChanged -= OnComponentChanged;
            _combo.Items.Clear();
            foreach (CoreHost c in _cores)
                foreach (string k in c.Keys()) _combo.Items.Add(new ComboItem(c, k, c.Describe(k)));
            int idx = 0;
            for (int i = 0; i < _combo.Items.Count; i++)
                if (((ComboItem)_combo.Items[i]).Key == keep) idx = i;
            if (_combo.Items.Count == 0)
            {
                _combo.SelectedIndexChanged += OnComponentChanged;
                return false;
            }
            _combo.SelectedIndex = idx;
            _combo.SelectedIndexChanged += OnComponentChanged;

            ComboItem it = (ComboItem)_combo.SelectedItem;
            _core = it.Core;
            _key = it.Key;
            _coreInfo.Text = "Core: " + CoreDir + "   (" + _cores.Count + " component)   build " + _core.BuildStamp();
            if (!ReferenceEquals(oldCore, _core) || _params == null)
            {
                object old = _params;
                _params = _core.NewParams(_key);
                if (old != null && keep == _key) CopyProperties(old, _params);
                _grid.SelectedObject = _params;
            }
            return true;
        }

        /// Chép giá trị theo tên thuộc tính (2 đối tượng thuộc 2 lần nạp Core khác nhau).
        private static void CopyProperties(object from, object to)
        {
            foreach (PropertyInfo t in to.GetType().GetProperties())
            {
                if (!t.CanWrite) continue;
                PropertyInfo f = from.GetType().GetProperty(t.Name);
                if (f == null || !f.CanRead || f.PropertyType.FullName != t.PropertyType.FullName) continue;
                try { t.SetValue(to, f.GetValue(from, null), null); } catch { }
            }
        }

        private void OnComponentChanged(object sender, EventArgs e)
        {
            ComboItem it = _combo.SelectedItem as ComboItem;
            if (it == null) return;
            _core = it.Core;
            _key = it.Key;
            _coreInfo.Text = "Core: " + CoreDir + "   (" + _cores.Count + " component)   build " + _core.BuildStamp();
            _params = _core.NewParams(_key);
            _grid.SelectedObject = _params;
            _primary = null;
            _secondaries.Clear();
            AppendLog("Component: " + it);
            RefreshDialog();
            SyncKnobs();
        }

        // ---------------------------------------------------------------- Hộp thoại (.inp)

        /// File chứa .inp của component: bản DEV cùng thư mục Core, nếu không có thì bản phát hành.
        private static string ShellDll(string key)
        {
            string dev = Path.Combine(CoreDir, key + "_DEV.dll");
            if (File.Exists(dev)) return dev;
            string pluginDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'));
            string rel = Path.Combine(Path.Combine(pluginDir, Program.CoreTarget), key + ".dll");
            return File.Exists(rel) ? rel : null;
        }

        /// Lấy hằng UserInterface (.inp) trong lớp plugin. Hằng nằm sẵn trong metadata, không chạy code plugin.
        private static string ReadUserInterface(string dll)
        {
            return PluginMeta.ReadUserInterfaceConst(dll);
        }

        /// Plugin\<Ten>\Plugin.cs (mã nguồn) của component, hoặc null.
        private static string PluginSource(string key)
        {
            string pluginRoot = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'));
            string f = Path.Combine(Path.Combine(pluginRoot, key), "Plugin.cs");
            return File.Exists(f) ? f : null;
        }

        /// Đọc hằng UserInterface = @"...";  thẳng từ Plugin.cs (không cần build _DEV.dll):
        ///   "" -> "   và   """ + PluginName + @"""  -> "<tên>_DEV"
        private static string InpFromSource(string file, string key)
        {
            string src = File.ReadAllText(file);
            int a = src.IndexOf("UserInterface = @\"", StringComparison.Ordinal);
            if (a < 0) return null;
            a += "UserInterface = @\"".Length;
            // kết thúc: dấu " đứng một mình (không phải "") theo sau là ;
            int i = a;
            StringBuilder sb = new StringBuilder();
            while (i < src.Length)
            {
                if (src[i] == '"')
                {
                    if (i + 1 < src.Length && src[i + 1] == '"') { sb.Append('"'); i += 2; continue; }
                    // hết 1 đoạn chuỗi @"...": nối tiếp kiểu  " + PluginName + @"  hoặc kết thúc ;
                    int semi = src.IndexOf(';', i);
                    int at = src.IndexOf("@\"", i, StringComparison.Ordinal);
                    if (at >= 0 && (semi < 0 || at < semi))
                    {
                        sb.Append(key + "_DEV");
                        i = at + 2;
                        continue;
                    }
                    break;
                }
                sb.Append(src[i]);
                i++;
            }
            return sb.ToString();
        }

        private static long LatestBitmapTicks(string key)
        {
            long t = 0;
            try
            {
                string pluginRoot = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'));
                string d = Path.Combine(Path.Combine(pluginRoot, key), "bitmaps");
                if (Directory.Exists(d)) foreach (string f in Directory.GetFiles(d)) t = Math.Max(t, File.GetLastWriteTimeUtc(f).Ticks);
            }
            catch { }
            return t;
        }

        private void RefreshDialog()
        {
            if (_key == null) return;
            // Ưu tiên hộp thoại trong mã nguồn Plugin.cs (thấy ngay khi sửa, không phải build _DEV.dll);
            // không có mã nguồn thì đọc từ DLL.
            string srcFile = PluginSource(_key);
            string dll = srcFile == null ? ShellDll(_key) : null;
            string from = srcFile ?? dll;
            string stamp = from == null ? null : from + "|" + File.GetLastWriteTimeUtc(from).Ticks + "|" + LatestBitmapTicks(_key);
            if (_inp != null && _inpKey == _key && _inpStamp == stamp)
            {
                _inp.RefreshDefaults(_params);
                return;
            }

            Dictionary<string, string> keep = _inp != null && _inpKey == _key ? _inp.Snapshot() : null;
            string inp = null;
            if (from != null)
            {
                try { inp = srcFile != null ? InpFromSource(srcFile, _key) : ReadUserInterface(dll); }
                catch (Exception ex) { AppendLog("Khong doc duoc hop thoai trong " + from + ": " + ex.Message); }
            }
            _dialogPage.Controls.Clear();
            if (_inp != null) _inp.Dispose();
            _inp = null;
            _inpKey = _key;
            _inpStamp = stamp;

            if (inp == null)
            {
                Label l = new Label();
                l.AutoSize = true;
                l.Location = new Point(10, 10);
                l.Text = "Chua co hop thoai: can " + _key + "_DEV.dll trong " + CoreDir + "  ->  chay build.bat dev " + _key +
                         "\nTam thoi dung tab 'Bang thong so'.";
                _dialogPage.Controls.Add(l);
                _dialogPage.Text = "Hop thoai (chua co)";
                return;
            }

            // ảnh hộp thoại: Plugin\<Ten>\bitmaps trước, rồi thư mục ảnh của Tekla (DAK_BMPPATH)
            InpView.BitmapDirs.Clear();
            string pluginRoot = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'));
            InpView.BitmapDirs.Add(Path.Combine(Path.Combine(pluginRoot, _key), "bitmaps"));
            try
            {
                string ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(Path.Combine(Program.ApiDir, "Tekla.Structures.Model.dll")).FileMajorPart + ".0";
                InpView.BitmapDirs.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Trimble\Tekla Structures\" + ver + @"\Bitmaps"));
                string apiParent = Path.GetDirectoryName(Program.ApiDir.TrimEnd('\\'));      // 18.0: ...\nt\bin
                InpView.BitmapDirs.Add(Path.Combine(Path.GetDirectoryName(apiParent), "bitmaps"));   // 18.0: ...\nt\bitmaps
                InpView.BitmapDirs.Add(Path.Combine(Program.ApiDir, @"Env\Bitmaps"));       // 2025: ...\bin\Env\Bitmaps
            }
            catch { }
            _inp = new InpView(inp, _params, keep, AppendLog);
            Label hint = new Label();
            hint.Dock = DockStyle.Bottom;
            hint.Height = 20;
            hint.ForeColor = Color.DimGray;
            hint.Text = "O de trong = gia tri mac dinh (chu xam, lay tu tab 'Bang thong so'), giong hop thoai Tekla. O vang: ten bien khong co trong Params.cs.";
            _dialogPage.Controls.Add(_inp);
            _dialogPage.Controls.Add(hint);
            _dialogPage.Text = "Hop thoai (giong Tekla, tu " + (srcFile != null ? _key + "\\Plugin.cs" : Path.GetFileName(dll)) + ")";
            if (keep != null) AppendLog("Da ve lai hop thoai (Plugin.cs vua build lai), giu cac o dang nhap.");
        }

        /// Thông số đem dựng = bảng thông số + các ô đã nhập trong hộp thoại, qua Core.Apply (giống bản DLL).
        private bool PrepareParams(out object prm)
        {
            prm = _core.NewParams(_key);
            CopyProperties(_params, prm);
            Hashtable values = new Hashtable();
            if (_inp != null)
            {
                List<string> errors = new List<string>();
                values = _inp.Collect(errors);
                if (errors.Count > 0)
                {
                    foreach (string e in errors) AppendLog("Hop thoai: " + e);
                    AppendLog("Sua cac o to do roi bam lai.");
                    return false;
                }
            }
            _core.Apply(_key, prm, values);
            if (_inp != null) AppendLog("Hop thoai: " + values.Count + " o da nhap, cac o con lai dung gia tri mac dinh.");
            return true;
        }

        // ---------------------------------------------------------------- Tekla

        private bool Connect()
        {
            try
            {
                if (_model == null) _model = new Model();
                if (_model.GetConnectionStatus())
                {
                    _status.Text = "Da noi voi Tekla. Model: " + _model.GetInfo().ModelName + "   |  " + Program.InstanceInfo +
                                   "   |  API: " + Program.ApiDir;
                    _status.ForeColor = Color.DarkGreen;
                    try { InpView.ModelDir = _model.GetInfo().ModelPath; } catch (Exception) { }   // nút "..." mở ở thư mục model
                    return true;
                }
            }
            catch (Exception ex)
            {
                AppendLog("Loi ket noi: " + ex.Message);
            }
            _status.Text = "CHUA noi duoc Tekla (" + Program.InstanceInfo + ", SESSIONNAME=" + Environment.GetEnvironmentVariable("SESSIONNAME") +
                           "). Mo Tekla + model roi DONG va MO LAI EXE. API: " + Program.ApiDir + " (" + Program.ApiSource + ")";
            _status.ForeColor = Color.Red;
            return false;
        }

        private void OnPickAndBuild(object sender, EventArgs e)
        {
            if (!EnsureCore() || !Connect()) return;
            object prm;
            if (!PrepareParams(out prm)) return;
            string[] d = _core.Describe(_key);
            List<ModelObject> secs = new List<ModelObject>();
            ModelObject prim;
            try
            {
                Picker picker = new Picker();
                prim = picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART, d[2]);
                for (int i = 3; i < d.Length; i++)
                    secs.Add(picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART, d[i]));
            }
            catch (Exception ex)
            {
                AppendLog("Huy chon: " + ex.Message);
                return;
            }
            if (prim == null || secs.Contains(null))
            {
                AppendLog("Chon chua du cau kien.");
                return;
            }
            _primary = prim;
            _secondaries = secs;
            Build(prm);
        }

        private void OnRebuild(object sender, EventArgs e)
        {
            if (!EnsureCore() || !Connect()) return;
            if (_primary == null)
            {
                AppendLog("Chua chon cau kien - bam nut 1 truoc.");
                return;
            }
            object prm;
            if (!PrepareParams(out prm)) return;
            DeleteLast();
            // đọc lại vì fitting/cut vừa bị xoá
            _primary.Select();
            foreach (ModelObject o in _secondaries) o.Select();
            Build(prm);
        }

        private void Build(object prm)
        {
            try
            {
                ArrayList created = new ArrayList();
                if (_knobs.Knobs.Count > 0)
                {
                    // gán số đang chỉnh ở tab Knobs vào Core đang nạp (không cần biên dịch)
                    int n = KnobSource.Apply(_knobs.Knobs, _core.Assembly, AppendLog);
                    int dirty = 0;
                    foreach (Knob k in _knobs.Knobs) if (k.Dirty) dirty++;
                    if (dirty > 0) AppendLog("Knobs: gan " + n + " gia tri, " + dirty + " khac .cs (chua ghi).");
                    _knobPage.Text = "Knobs (" + _knobs.Knobs.Count + ")" + (dirty > 0 ? " *" : "");
                }
                bool ok = _core.Build(_key, prm, _model, _primary, new ArrayList(_secondaries), created);
                _last = created;
                _model.CommitChanges();
                AppendLog(ok ? "Xong: tao " + _last.Count + " doi tuong." : "Dung KHONG thanh cong (xem log o tren). Da tao " + _last.Count + " doi tuong.");
            }
            catch (Exception ex)
            {
                AppendLog("Loi: " + ex);
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (!Connect()) return;
            DeleteLast();
        }

        private void DeleteLast()
        {
            int n = 0;
            for (int i = _last.Count - 1; i >= 0; i--)
            {
                ModelObject o = _last[i] as ModelObject;
                try { if (o != null && o.Delete()) n++; } catch { }
            }
            _last.Clear();
            _model.CommitChanges();
            AppendLog("Da xoa " + n + " doi tuong.");
        }

        private void OnReset(object sender, EventArgs e)
        {
            if (!EnsureCore() || _key == null) return;
            _params = _core.NewParams(_key);
            _grid.SelectedObject = _params;
            if (_inp != null)
            {
                _inp.ClearAll();
                _inp.RefreshDefaults(_params);
            }
        }

        // ---------------------------------------------------------------- So sánh

        /// Chọn 1 đối tượng của component gốc rồi 1 đối tượng của component plugin -> so từng part / bu lông / hàn / cắt.
        private void OnCompare(object sender, EventArgs e)
        {
            if (!Connect()) return;
            Picker picker = new Picker();
            BaseComponent a = PickComponent(picker, "So sanh: chon 1 part (hoac ky hieu) cua component GOC, vd. 0-RAKER+TG");
            if (a == null) return;
            BaseComponent b = PickComponent(picker, "So sanh: chon 1 part (hoac ky hieu) cua component PLUGIN, vd. ..._DEV");
            if (b == null) return;
            if (a.Identifier.ID == b.Identifier.ID)
            {
                AppendLog("So sanh: ca 2 lan deu chon cung mot component (" + a.Name + ").");
                return;
            }
            try
            {
                AppendLog("So sanh: A = " + a.Name + " (id " + a.Identifier.ID + "), B = " + b.Name + " (id " + b.Identifier.ID + ") ...");
                CmpResult r = ComponentCompare.Run(_model, a, b);
                AppendLog("So sanh: khop " + r.Same.Count + ", lech " + r.Diff.Count + ", B thieu " + r.OnlyA.Count + ", B thua " + r.OnlyB.Count);
                CompareForm f = new CompareForm(r);
                f.Owner = this;
                f.Show();
            }
            catch (Exception ex)
            {
                AppendLog("Loi so sanh: " + ex);
            }
        }

        // ---------------------------------------------------------------- Xuất JSON

        private static string ExportDir
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Export"); }
        }

        /// Xuất component ra JSON (chỉ đọc model). Lấy các component đang chọn trong Tekla (chọn component, hoặc
        /// chọn part do component tạo ra); không chọn gì thì hỏi chọn 1 cái.
        private void OnExport(object sender, EventArgs e)
        {
            if (!Connect()) return;
            List<BaseComponent> comps = new List<BaseComponent>();
            Dictionary<int, bool> ids = new Dictionary<int, bool>();
            try
            {
                ModelObjectEnumerator sel = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
                while (sel != null && sel.MoveNext())
                {
                    ModelObject o = sel.Current;
                    if (o == null) continue;
                    BaseComponent bc = o as BaseComponent;
                    if (bc == null) { try { bc = o.GetFatherComponent(); } catch { bc = null; } }
                    if (bc == null || ids.ContainsKey(bc.Identifier.ID)) continue;
                    ids[bc.Identifier.ID] = true;
                    bc.Select();
                    comps.Add(bc);
                }
            }
            catch (Exception ex) { AppendLog("Doc doi tuong dang chon loi: " + ex.Message); }

            if (comps.Count == 0)
            {
                BaseComponent one = PickComponent(new Picker(), "Xuat JSON: chon 1 part (hoac ky hieu) cua component can xuat");
                if (one == null) return;
                comps.Add(one);
            }

            AppendLog("Xuat JSON " + comps.Count + " component ...");
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (BaseComponent bc in comps)
                {
                    try { ComponentExport.Run(_model, bc, ExportDir, AppendLog); }
                    catch (Exception ex) { AppendLog("Loi xuat " + bc.Name + " (id " + bc.Identifier.ID + "): " + ex); }
                    Application.DoEvents();
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void OnOpenExport(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(ExportDir);
                System.Diagnostics.Process.Start("explorer.exe", "\"" + ExportDir + "\"");
            }
            catch (Exception ex) { AppendLog(ex.Message); }
        }

        private BaseComponent PickComponent(Picker picker, string prompt)
        {
            ModelObject o;
            try
            {
                o = picker.PickObject(Picker.PickObjectEnum.PICK_ONE_OBJECT, prompt);
            }
            catch (Exception ex)
            {
                AppendLog("Huy chon: " + ex.Message);
                return null;
            }
            if (o == null) return null;
            BaseComponent c = o as BaseComponent;
            if (c == null)
            {
                try { c = o.GetFatherComponent(); } catch { c = null; }
            }
            if (c == null)
            {
                AppendLog("Doi tuong vua chon (" + o.GetType().Name + ", id " + o.Identifier.ID +
                          ") khong thuoc component nao. Hay chon part do component tao ra (ban ma, ban de...), khong chon dam dau vao.");
                return null;
            }
            c.Select();
            return c;
        }

        private void OnOpenLog(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start("notepad.exe", HostLog.FilePath); }
            catch (Exception ex) { AppendLog(ex.Message); }
        }

        private sealed class ComboItem
        {
            public ComboItem(CoreHost core, string key, string[] describe)
            {
                Core = core;
                Key = key;
                _text = key + "  -  " + describe[0] + "   [" + describe[1] + "]";
            }
            public readonly CoreHost Core;
            public readonly string Key;
            private readonly string _text;
            public override string ToString() { return _text; }
        }
    }
}
