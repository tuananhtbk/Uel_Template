// Cửa sổ thử nghiệm chung: chọn component -> chỉnh thông số -> chọn cấu kiện -> dựng / dựng lại / xoá.
// Đối tượng tạo ra là đối tượng thường trong model (không phải component) -> dùng nút "Xoa ket qua" để dọn.
// Mỗi lần dựng, EXE kiểm tra các file _Dev\<bản>\*.Core.dll: file nào vừa build lại thì nạp bản mới, giữ nguyên thông số đang nhập.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
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
            Size = new Size(860, 760);
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
            buttons.Height = 72;
            buttons.Padding = new Padding(4);
            AddButton(buttons, "1. Chon cau kien va dung", OnPickAndBuild, 190);
            AddButton(buttons, "2. Dung lai (giu cau kien da chon)", OnRebuild, 220);
            AddButton(buttons, "Xoa ket qua vua dung", OnDelete, 150);
            AddButton(buttons, "Tra ve mac dinh", OnReset, 120);
            AddButton(buttons, "Mo file log", OnOpenLog, 100);
            AddButton(buttons, "So sanh: component goc  vs  plugin", OnCompare, 240);

            _status.Dock = DockStyle.Top;
            _status.Height = 22;
            _status.TextAlign = ContentAlignment.MiddleLeft;
            _coreInfo.Dock = DockStyle.Top;
            _coreInfo.Height = 20;
            _coreInfo.TextAlign = ContentAlignment.MiddleLeft;
            _coreInfo.ForeColor = Color.DimGray;

            _grid.Dock = DockStyle.Fill;
            _grid.PropertySort = PropertySort.Categorized;

            _log.Dock = DockStyle.Bottom;
            _log.Height = 200;
            _log.Multiline = true;
            _log.ScrollBars = ScrollBars.Both;
            _log.WordWrap = false;
            _log.ReadOnly = true;
            _log.Font = new Font("Consolas", 9f);

            Controls.Add(_grid);
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

        private static void AddButton(Control parent, string text, EventHandler click, int width)
        {
            Button b = new Button();
            b.Text = text;
            b.Width = width;
            b.Height = 28;
            b.Click += click;
            parent.Controls.Add(b);
        }

        private void AppendLog(string line)
        {
            if (InvokeRequired) { BeginInvoke(new Action<string>(AppendLog), line); return; }
            _log.AppendText(line + Environment.NewLine);
        }

        // ---------------------------------------------------------------- Core

        /// Nạp (hoặc nạp lại nếu vừa build) mọi file *.Core.dll trong CoreDir. Giữ nguyên thông số đang nhập.
        private bool EnsureCore()
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
        }

        // ---------------------------------------------------------------- Tekla

        private bool Connect()
        {
            try
            {
                if (_model == null) _model = new Model();
                if (_model.GetConnectionStatus())
                {
                    _status.Text = "Da noi voi Tekla. Model: " + _model.GetInfo().ModelName +
                                   "   |  API: " + Program.ApiDir + " (" + Program.ApiSource + ")";
                    _status.ForeColor = Color.DarkGreen;
                    return true;
                }
            }
            catch (Exception ex)
            {
                AppendLog("Loi ket noi: " + ex.Message);
            }
            _status.Text = "CHUA noi duoc Tekla - hay mo Tekla va mot model truoc. API dang dung: " + Program.ApiDir + " (" + Program.ApiSource + ")";
            _status.ForeColor = Color.Red;
            return false;
        }

        private void OnPickAndBuild(object sender, EventArgs e)
        {
            if (!EnsureCore() || !Connect()) return;
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
            Build();
        }

        private void OnRebuild(object sender, EventArgs e)
        {
            if (!EnsureCore() || !Connect()) return;
            if (_primary == null)
            {
                AppendLog("Chua chon cau kien - bam nut 1 truoc.");
                return;
            }
            DeleteLast();
            // đọc lại vì fitting/cut vừa bị xoá
            _primary.Select();
            foreach (ModelObject o in _secondaries) o.Select();
            Build();
        }

        private void Build()
        {
            try
            {
                ArrayList created = new ArrayList();
                bool ok = _core.Build(_key, _params, _model, _primary, new ArrayList(_secondaries), created);
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
