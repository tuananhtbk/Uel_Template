// Cầu nối tới Core (UEL.Core.Entry) qua reflection. Dùng chung cho:
//   - plugin phát hành : Core nằm ngay trong cùng DLL           -> CoreHost.FromAssembly(...)
//   - plugin DEV       : Core là file riêng (_Dev\<bản>\<Ten>.Core.dll), nạp bằng mảng byte -> CoreHost.LoadFile(...)
//   - EXE thử nghiệm   : như plugin DEV, nạp mọi file *.Core.dll
// Nạp bằng mảng byte thì file .Core.dll KHÔNG bị khoá: build lại lúc Tekla/EXE đang mở vẫn được,
// lần chạy sau tự nạp bản mới (so thời điểm sửa file).
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

namespace UEL.Host
{
    public sealed class CoreHost
    {
        private readonly Type _entry;
        private static readonly object Sync = new object();
        // Mỗi file Core (mỗi component) có bản nạp riêng. Khoá = đường dẫn (không phân biệt hoa thường).
        private static readonly Dictionary<string, CoreHost> Cache = new Dictionary<string, CoreHost>(StringComparer.OrdinalIgnoreCase);
        // Tên các assembly Core đã nạp: không bao giờ trả về qua AssemblyResolve (tránh lấy nhầm bản cũ).
        private static readonly Dictionary<string, bool> CoreNames = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        private static bool _resolverOn;

        private CoreHost(Assembly asm, string sourceFile, DateTime fileTime)
        {
            Assembly = asm;
            SourceFile = sourceFile;
            FileTime = fileTime;
            _entry = asm.GetType("UEL.Core.Entry", true);
        }

        public Assembly Assembly { get; private set; }
        public string SourceFile { get; private set; }
        public DateTime FileTime { get; private set; }

        /// Core nằm trong assembly đã nạp sẵn (plugin phát hành).
        public static CoreHost FromAssembly(Assembly asm)
        {
            return new CoreHost(asm, null, DateTime.MinValue);
        }

        /// Nạp Core từ file dưới dạng byte. Chỉ nạp lại khi file đã đổi.
        public static CoreHost LoadFile(string path)
        {
            lock (Sync)
            {
                EnsureResolver();
                if (!File.Exists(path)) throw new FileNotFoundException("Khong thay Core: " + path, path);
                DateTime t = File.GetLastWriteTimeUtc(path);
                CoreHost cached;
                if (Cache.TryGetValue(path, out cached) && cached.FileTime == t)
                    return cached;

                byte[] dll = ReadAllBytesRetry(path);
                string pdbPath = Path.ChangeExtension(path, ".pdb");
                byte[] pdb = File.Exists(pdbPath) ? ReadAllBytesRetry(pdbPath) : null;
                Assembly asm = pdb != null ? Assembly.Load(dll, pdb) : Assembly.Load(dll);
                CoreNames[asm.GetName().Name] = true;
                cached = new CoreHost(asm, path, t);
                Cache[path] = cached;
                HostLog.Write("Da nap Core: " + path + " (sua luc " + t.ToLocalTime().ToString("HH:mm:ss") + ")");
                return cached;
            }
        }

        // File có thể đang được csc ghi dở -> thử lại vài lần
        private static byte[] ReadAllBytesRetry(string path)
        {
            for (int i = 0; ; i++)
            {
                try { return File.ReadAllBytes(path); }
                catch (IOException)
                {
                    if (i >= 10) throw;
                    Thread.Sleep(200);
                }
            }
        }

        // Core nạp bằng byte không có thư mục gốc để dò DLL Tekla -> trả về đúng bản Tekla đã nạp trong tiến trình
        private static void EnsureResolver()
        {
            if (_resolverOn) return;
            _resolverOn = true;
            AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs args)
            {
                string name = new AssemblyName(args.Name).Name;
                lock (Sync)
                {
                    if (CoreNames.ContainsKey(name)) return null;
                }
                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (string.Equals(a.GetName().Name, name, StringComparison.OrdinalIgnoreCase))
                        return a;
                }
                return null;
            };
        }

        private object Call(string method, params object[] args)
        {
            try
            {
                return _entry.InvokeMember(method, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, args);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public string BuildStamp() { return (string)Call("BuildStamp"); }
        public string[] Keys() { return (string[])Call("Keys"); }
        /// [0] Title, [1] SourceUel, [2] PrimaryPrompt, [3..] SecondaryPrompts
        public string[] Describe(string key) { return (string[])Call("Describe", key); }
        public object NewParams(string key) { return Call("NewParams", key); }
        public void Apply(string key, object prm, IDictionary values) { Call("Apply", key, prm, values); }

        public bool Build(string key, object prm, object model, object primary, IList secondaries, IList created)
        {
            return (bool)Call("Build", key, prm, model, primary, secondaries, created);
        }

        /// Đăng ký nhận log của Core (EXE thử nghiệm dùng).
        public void Subscribe(Action<string> handler, bool add)
        {
            Type t = Assembly.GetType("UEL.Core.Logger");
            if (t == null) return;
            EventInfo ev = t.GetEvent("Message");
            if (ev == null) return;
            if (add) ev.AddEventHandler(null, handler);
            else ev.RemoveEventHandler(null, handler);
        }
    }

    /// Log phía plugin/EXE (khi chưa nạp được Core). Ghi chung file với Core.
    public static class HostLog
    {
        public static string FilePath
        {
            get { return Path.Combine(Path.GetTempPath(), "UEL_PLUGINS.log"); }
        }

        public static void Write(string message)
        {
            try { File.AppendAllText(FilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  [host] " + message + Environment.NewLine); }
            catch { }
        }
    }
}
