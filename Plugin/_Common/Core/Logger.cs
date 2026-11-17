// Nhật ký dùng chung cho mọi component (plugin DLL, plugin DEV và EXE thử nghiệm).
// File: %TEMP%\UEL_PLUGINS.log
using System;
using System.IO;

namespace UEL.Core
{
    public static class Logger
    {
        /// EXE thử nghiệm đăng ký sự kiện này (qua reflection) để hiện log trên cửa sổ.
        public static event Action<string> Message;

        public static string FilePath
        {
            get { return Path.Combine(Path.GetTempPath(), "UEL_PLUGINS.log"); }
        }

        public static void Log(string message)
        {
            string line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + message;
            try { File.AppendAllText(FilePath, line + Environment.NewLine); }
            catch { }
            Action<string> h = Message;
            if (h != null)
            {
                try { h(line); } catch { }
            }
        }
    }
}
