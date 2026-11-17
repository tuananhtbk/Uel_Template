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
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        [STAThread]
        private static void Main(string[] args)
        {
            ResolveApiDir(args);
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

        // Tách riêng để AssemblyResolve được đăng ký trước khi .NET nạp các DLL Tekla
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Start()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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
