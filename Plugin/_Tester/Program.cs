// EXE thử nghiệm dùng chung cho mọi component của bộ UEL, chạy được với Tekla 18.0 -> 2025.
// Nối vào Tekla đang mở; nạp các Core (_Dev\18.0\*.Core.dll hoặc _Dev\2020+\*.Core.dll, theo bản Tekla)
// dạng byte nên có thể build lại ("build.bat core") trong khi EXE và Tekla vẫn mở.
// Mỗi lần bấm Dựng, EXE tự dùng bản Core mới nhất.
//
// EXE phải dùng ĐÚNG bộ DLL API của bản Tekla đang mở. Thứ tự chọn thư mục DLL API:
//   1. tham số dòng lệnh:   UEL_TESTER.exe 2025   hoặc   UEL_TESTER.exe "C:\...\bin"
//   2. biến môi trường TEKLA_BIN
//   3. Tekla đang chạy (lấy theo đường dẫn TeklaStructures.exe)
//   4. mặc định Tekla 18.0
//
// NỐI VÀO TEKLA: API Tekla (2020...) nối qua pipe tên  Tekla.Structures.Model-<SESSIONNAME>:<phiên bản>.
//   - SESSIONNAME là biến môi trường của Windows ("Console"). Mở EXE từ vài nơi (terminal của app khác,
//     tác vụ nền...) thì biến này rỗng -> API tìm pipe "Tekla.Structures.Model-:2020.0.0.0" -> không nối được.
//   - Mở nhiều Tekla cùng bản: cửa sổ thứ 2 trở đi có pipe "...-Console-<pid>", API bình thường không nối tới được.
//   => EXE tự tìm các pipe Tekla đang mở; nhiều Tekla thì hỏi nối vào model nào; rồi đặt SESSIONNAME tương ứng
//      TRƯỚC khi nạp DLL Tekla. Muốn đổi sang Tekla khác thì đóng EXE mở lại.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UEL.Tester
{
    internal static class Program
    {
        private static string[] _probeDirs;

        /// Thư mục DLL API đang dùng và lý do chọn (hiện lên cửa sổ).
        internal static string ApiDir;
        internal static string ApiSource;
        /// Bộ build dùng cho bản Tekla này: "18.0" (Tekla 18.0, 2019) hoặc "2020+" (Tekla 2020 trở lên).
        internal static string CoreTarget;

        private static string TargetOf(string apiDir)
        {
            try
            {
                FileVersionInfo fv = FileVersionInfo.GetVersionInfo(Path.Combine(apiDir, "Tekla.Structures.Model.dll"));
                return fv.FileMajorPart >= 2020 ? "2020+" : "18.0";
            }
            catch
            {
                return "18.0";
            }
        }

        /// Tekla mà EXE nối vào (hiện trên cửa sổ).
        internal static string InstanceInfo = "";

        internal sealed class TeklaInstance
        {
            public string Session;      // phần giữa "Tekla.Structures.Model-" và ":" trong tên pipe
            public string Version;      // "2020.0"
            public int Pid;
            public string Title;
            public string ExeDir;

            public override string ToString()
            {
                return "Tekla " + Version + "   " + (string.IsNullOrEmpty(Title) ? "(khong ro model)" : Title) + "      [pid " + Pid + "]";
            }
        }

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ResolveApiDir(args);
            ChooseInstance();
            CoreTarget = TargetOf(ApiDir);
            _probeDirs = new[] { ApiDir, Path.GetDirectoryName(ApiDir.TrimEnd('\\')), Path.Combine(ApiDir, "plugins") };
            AppDomain.CurrentDomain.AssemblyResolve += ResolveTekla;
            Start();
        }

        private static void ResolveApiDir(string[] args)
        {
            if (args.Length > 0)
            {
                string a = args[0].Trim('"');
                string dir = Directory.Exists(a) ? a : ApiDirForVersion(a);
                if (dir != null) { ApiDir = dir; ApiSource = "tham so dong lenh"; return; }
            }
            string env = Environment.GetEnvironmentVariable("TEKLA_BIN");
            if (!string.IsNullOrEmpty(env) && Directory.Exists(env)) { ApiDir = env; ApiSource = "bien TEKLA_BIN"; return; }

            string running = ApiDirOfRunningTekla();
            if (running != null) { ApiDir = running; ApiSource = "Tekla dang mo"; return; }

            ApiDir = ApiDirForVersion("18.0") ?? @"C:\TeklaStructures\18.0\nt\bin\plugins";
            ApiSource = "mac dinh 18.0";
        }

        /// Giống tekla_paths.bat: 18.0 nằm ở C:\TeklaStructures, các bản khác ở Program Files.
        private static string ApiDirForVersion(string ver)
        {
            if (string.IsNullOrEmpty(ver)) return null;
            if (ver.IndexOf('.') < 0) ver += ".0";
            string[] roots =
            {
                Path.Combine(@"C:\TeklaStructures", ver),
                Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Tekla Structures"), ver)
            };
            foreach (string r in roots)
            {
                string a = Path.Combine(r, @"nt\bin\plugins");
                if (File.Exists(Path.Combine(a, "Tekla.Structures.Model.dll"))) return a;
                a = Path.Combine(r, "bin");
                if (File.Exists(Path.Combine(a, "Tekla.Structures.Model.dll"))) return a;
            }
            return null;
        }

        private static string ApiDirOfRunningTekla()
        {
            try
            {
                foreach (Process p in Process.GetProcessesByName("TeklaStructures"))
                {
                    string exe;
                    try { exe = p.MainModule.FileName; } catch { continue; }
                    string dir = Path.GetDirectoryName(exe);
                    foreach (string c in new[] { Path.Combine(dir, "plugins"), dir })
                        if (File.Exists(Path.Combine(c, "Tekla.Structures.Model.dll"))) return c;
                }
            }
            catch { }
            return null;
        }

        // ---------------------------------------------------------------- chọn Tekla đang mở

        private static string VersionOfFile(string file)
        {
            try
            {
                FileVersionInfo fv = FileVersionInfo.GetVersionInfo(file);
                return fv.FileMajorPart + "." + fv.FileMinorPart;
            }
            catch
            {
                return null;
            }
        }

        /// Các Tekla đang mở, đọc từ tên pipe "Tekla.Structures.Model-<phien>:<ban>".
        internal static List<TeklaInstance> FindInstances()
        {
            List<TeklaInstance> list = new List<TeklaInstance>();
            string[] pipes;
            try { pipes = Directory.GetFiles(@"\\.\pipe\"); }
            catch { pipes = new string[0]; }
            Regex rx = new Regex(@"^Tekla\.Structures\.Model-(.*):(\d+)\.(\d+)\.\d+\.\d+$");
            foreach (string p in pipes)
            {
                string name = p.StartsWith(@"\\.\pipe\") ? p.Substring(9) : p;
                Match m = rx.Match(name);
                if (!m.Success) continue;
                TeklaInstance t = new TeklaInstance();
                t.Session = m.Groups[1].Value;
                t.Version = m.Groups[2].Value + "." + m.Groups[3].Value;
                Match pm = Regex.Match(t.Session, @"-(\d+)$");
                if (pm.Success) t.Pid = int.Parse(pm.Groups[1].Value);
                list.Add(t);
            }

            List<Process> procs = new List<Process>(Process.GetProcessesByName("TeklaStructures"));
            // phiên không có hậu tố pid = tiến trình cùng bản chưa được phiên nào nhận
            foreach (TeklaInstance t in list)
            {
                if (t.Pid != 0) continue;
                foreach (Process pr in procs)
                {
                    bool taken = false;
                    foreach (TeklaInstance x in list) if (x.Pid == pr.Id) taken = true;
                    if (taken) continue;
                    string exe;
                    try { exe = pr.MainModule.FileName; } catch { continue; }
                    if (VersionOfFile(exe) == t.Version) { t.Pid = pr.Id; break; }
                }
            }
            foreach (TeklaInstance t in list)
                foreach (Process pr in procs)
                {
                    if (pr.Id != t.Pid) continue;
                    string title = pr.MainWindowTitle ?? "";
                    t.Title = title.StartsWith("Tekla Structures - ") ? title.Substring(19).Trim() : title;
                    try { t.ExeDir = Path.GetDirectoryName(pr.MainModule.FileName); } catch { }
                }
            list.Sort(delegate(TeklaInstance a, TeklaInstance b) { return a.Pid.CompareTo(b.Pid); });
            return list;
        }

        /// Chọn Tekla để nối (hỏi nếu có nhiều), đặt SESSIONNAME cho đúng pipe của Tekla đó.
        /// Nếu thư mục API được chỉ định (tham số / TEKLA_BIN) thì chỉ xét Tekla cùng bản.
        private static void ChooseInstance()
        {
            List<TeklaInstance> all = FindInstances();
            string want = ApiSource == "Tekla dang mo" || ApiSource == "mac dinh 18.0" ? null
                : VersionOfFile(Path.Combine(ApiDir, "Tekla.Structures.Model.dll"));
            List<TeklaInstance> list = new List<TeklaInstance>();
            foreach (TeklaInstance t in all) if (want == null || t.Version == want) list.Add(t);

            TeklaInstance pick = null;
            if (list.Count == 1) pick = list[0];
            else if (list.Count > 1) pick = AskInstance(list);

            if (pick != null)
            {
                Environment.SetEnvironmentVariable("SESSIONNAME", pick.Session);
                if (want == null && pick.ExeDir != null)
                    foreach (string c in new[] { Path.Combine(pick.ExeDir, "plugins"), pick.ExeDir })
                        if (File.Exists(Path.Combine(c, "Tekla.Structures.Model.dll"))) { ApiDir = c; ApiSource = "Tekla dang mo"; break; }
                InstanceInfo = pick.ToString();
                return;
            }
            // không thấy pipe nào (Tekla chưa mở, hoặc bản cũ đặt tên khác): ít nhất đừng để SESSIONNAME rỗng
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SESSIONNAME")))
                Environment.SetEnvironmentVariable("SESSIONNAME", "Console");
            InstanceInfo = all.Count == 0 ? "khong thay Tekla nao dang mo" : "khong co Tekla ban " + want + " dang mo";
        }

        private static TeklaInstance AskInstance(List<TeklaInstance> list)
        {
            Form f = new Form();
            f.Text = "UEL_TESTER - chon Tekla de noi";
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Size = new Size(760, 260);
            f.TopMost = true;
            f.FormBorderStyle = FormBorderStyle.FixedDialog;
            f.MinimizeBox = false;
            f.MaximizeBox = false;

            Label l = new Label();
            l.Dock = DockStyle.Top;
            l.Height = 30;
            l.Padding = new Padding(6, 8, 0, 0);
            l.Text = "Dang mo " + list.Count + " Tekla. Chon model de EXE noi vao (muon doi thi dong EXE mo lai):";

            ListBox lb = new ListBox();
            lb.Dock = DockStyle.Fill;
            lb.Font = new Font("Consolas", 10f);
            foreach (TeklaInstance t in list) lb.Items.Add(t);
            lb.SelectedIndex = 0;
            lb.DoubleClick += delegate { f.DialogResult = DialogResult.OK; };

            FlowLayoutPanel bottom = new FlowLayoutPanel();
            bottom.Dock = DockStyle.Bottom;
            bottom.Height = 40;
            bottom.FlowDirection = FlowDirection.RightToLeft;
            Button ok = new Button();
            ok.Text = "Noi";
            ok.DialogResult = DialogResult.OK;
            Button cancel = new Button();
            cancel.Text = "Bo qua";
            cancel.DialogResult = DialogResult.Cancel;
            bottom.Controls.Add(cancel);
            bottom.Controls.Add(ok);
            f.AcceptButton = ok;
            f.CancelButton = cancel;

            f.Controls.Add(lb);
            f.Controls.Add(l);
            f.Controls.Add(bottom);
            return f.ShowDialog() == DialogResult.OK ? lb.SelectedItem as TeklaInstance : null;
        }

        // Tách riêng để AssemblyResolve được đăng ký trước khi .NET nạp các DLL Tekla
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Start()
        {
            Application.Run(new TesterForm());
        }

        // EXE được build với DLL Tekla 18; khi chạy thì trả về DLL của bản Tekla đã chọn (mọi tên DLL,
        // vì Tekla bản mới cần thêm các DLL phụ đi kèm Tekla.Structures.Model.dll).
        private static Assembly ResolveTekla(object sender, ResolveEventArgs args)
        {
            string name = new AssemblyName(args.Name).Name;
            if (name.StartsWith("UEL.", StringComparison.OrdinalIgnoreCase)) return null;
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                if (string.Equals(a.GetName().Name, name, StringComparison.OrdinalIgnoreCase)) return a;
            foreach (string dir in _probeDirs)
            {
                if (string.IsNullOrEmpty(dir)) continue;
                string path = Path.Combine(dir, name + ".dll");
                if (File.Exists(path)) return Assembly.LoadFrom(path);
            }
            return null;
        }
    }
}
