// Nhật ký dùng chung cho mọi component (plugin DLL, plugin DEV và EXE thử nghiệm).
// File: %TEMP%\UEL_PLUGINS.log
//
// Logger.Error (người dùng 18/11/2026): lỗi làm component KHÔNG được dựng (thiếu bảng tra, không có dòng girder size,
// thiếu / sai số liệu plate size, bolt size...). Ghi "LOI: ..." vào log và gom lại; Entry.Build dựng thất bại thì hiện
// MỘT hộp thông báo liệt kê các lỗi đó (ShowErrors). Component phải dừng (return false) TRƯỚC khi tạo đối tượng.
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace UEL.Core
{
    public static class Logger
    {
        /// EXE thử nghiệm đăng ký sự kiện này (qua reflection) để hiện log trên cửa sổ.
        public static event Action<string> Message;

        private static readonly List<string> Errors = new List<string>();
        private static string _lastShown;
        private static DateTime _lastShownAt = DateTime.MinValue;

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

        /// Lỗi chặn dựng component: ghi log "LOI: ..." và gom để hiện thông báo khi dựng thất bại.
        public static void Error(string message)
        {
            Log("LOI: " + message);
            lock (Errors) Errors.Add(message);
        }

        /// Lần dựng này đã có Logger.Error chưa.
        public static bool HasErrors
        {
            get { lock (Errors) return Errors.Count > 0; }
        }

        /// Đầu mỗi lần dựng (Entry.Build): xoá các lỗi của lần trước.
        internal static void BeginRun()
        {
            lock (Errors) Errors.Clear();
        }

        /// Dựng thất bại: hiện 1 hộp thông báo với các lỗi đã gom (nếu có). Cùng nội dung trong vòng 10 giây thì
        /// không hiện lại (Tekla có thể chạy lại component nhiều lần liên tiếp).
        internal static void ShowErrors(string component)
        {
            string body;
            lock (Errors)
            {
                if (Errors.Count == 0) return;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Component " + component + " KHONG duoc tao:");
                sb.AppendLine();
                foreach (string e in Errors) sb.AppendLine("- " + e);
                sb.AppendLine();
                sb.Append("Sua bang tra / thong so roi bam Modify. Chi tiet: " + FilePath);
                body = sb.ToString();
            }
            if (body == _lastShown && (DateTime.Now - _lastShownAt).TotalSeconds < 10) return;
            _lastShown = body;
            _lastShownAt = DateTime.Now;
            try
            {
                using (Form owner = new Form())
                {
                    owner.TopMost = true;               // không bị cửa sổ Tekla che
                    owner.ShowInTaskbar = false;
                    owner.StartPosition = FormStartPosition.CenterScreen;
                    owner.Size = new System.Drawing.Size(1, 1);
                    owner.Opacity = 0;
                    owner.Show();
                    MessageBox.Show(owner, body, "UEL - " + component, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Log("Khong hien duoc thong bao: " + ex.Message);
            }
        }
    }
}
