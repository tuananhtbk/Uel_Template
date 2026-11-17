// Bảng tra dùng chung cho mọi component (tương đương fVF("file", khoá, cột) của .uel gốc).
//
// Định dạng file (giống các bảng .txt trong UEL Template):
//   - phân cách TAB, dòng đầu (không tính dòng trống / dòng "#") là tiêu đề;
//   - cột đầu là KHOÁ (thường là profile của Part 2), các cột sau đặt tên theo biến P của component (P1, P20, P38_diameter...);
//   - cột không phải tên biến (REMARK...) thì bỏ qua; ô trống = không đổi giá trị đó;
//   - dòng khoá "*" = dòng mặc định khi không có dòng nào khớp;
//   - encoding cp949 hoặc UTF-8 (tự nhận), xuống dòng CRLF hoặc LF.
//
// Tìm file (ApplyByKey): đường dẫn đầy đủ thì dùng luôn; chỉ có tên (hoặc đường dẫn tương đối) thì tìm lần lượt
//   thư mục model -> XS_PROJECT -> XS_FIRM -> thư mục chứa DLL plugin.
// Bảng chỉ ĐỌC, không bao giờ ghi lại file.
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Tekla.Structures.Model;

namespace UEL.Core
{
    public sealed class LookupTable
    {
        public string Path { get; private set; }
        public string[] Header { get; private set; }
        public List<string[]> Rows { get; private set; }

        private LookupTable() { }

        // ------------------------------------------------------------------ đọc file

        public static LookupTable Load(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            string text = Decode(bytes);
            LookupTable t = new LookupTable();
            t.Path = path;
            t.Rows = new List<string[]>();
            foreach (string raw in text.Split('\n'))
            {
                string line = raw.TrimEnd('\r');
                if (line.Trim().Length == 0 || line.TrimStart().StartsWith("#")) continue;
                string[] cells = line.Split('\t');
                for (int i = 0; i < cells.Length; i++) cells[i] = cells[i].Trim().Trim('"');
                if (t.Header == null) t.Header = cells;
                else t.Rows.Add(cells);
            }
            if (t.Header == null) t.Header = new string[0];
            return t;
        }

        /// UTF-8 nếu có BOM hoặc giải mã UTF-8 chặt không lỗi; còn lại là cp949 (bảng gốc của thư viện UEL).
        private static string Decode(byte[] b)
        {
            if (b.Length >= 3 && b[0] == 0xEF && b[1] == 0xBB && b[2] == 0xBF) return Encoding.UTF8.GetString(b, 3, b.Length - 3);
            try
            {
                return new UTF8Encoding(false, true).GetString(b);
            }
            catch (DecoderFallbackException)
            {
                return Encoding.GetEncoding(949).GetString(b);
            }
        }

        /// Chuẩn hoá khoá: bỏ dấu cách, chữ hoa, "x"/"X"/"×" giữa 2 chữ số -> "*". Vd. "h350 x 357x19X19" -> "H350*357*19*19".
        public static string NormalizeKey(string s)
        {
            if (s == null) return "";
            string r = Regex.Replace(s.Trim(), @"(?<=\d)\s*[xX×]\s*(?=\d)", "*");
            return r.Replace(" ", "").ToUpperInvariant();
        }

        /// Dòng có khoá (cột đầu) khớp; không có thì dòng "*"; không có nữa thì null. matched = khoá của dòng tìm được.
        public string[] FindRow(string key, out string matched)
        {
            string k = NormalizeKey(key);
            string[] star = null;
            foreach (string[] r in Rows)
            {
                if (r.Length == 0) continue;
                string rk = NormalizeKey(r[0]);
                if (rk == k) { matched = r[0]; return r; }
                if (rk == "*" && star == null) star = r;
            }
            matched = star != null ? "*" : null;
            return star;
        }

        // ------------------------------------------------------------------ tìm file

        /// Trả về đường dẫn file tìm thấy, hoặc null. tried = các chỗ đã tìm (để ghi log).
        public static string Resolve(string fileOrPath, string modelPath, List<string> tried)
        {
            if (string.IsNullOrEmpty(fileOrPath)) return null;
            string f = fileOrPath.Trim().Trim('"');
            if (System.IO.Path.IsPathRooted(f))
            {
                tried.Add(f);
                return File.Exists(f) ? f : null;
            }
            List<string> dirs = new List<string>();
            if (!string.IsNullOrEmpty(modelPath)) dirs.Add(modelPath);
            foreach (string opt in new[] { "XS_PROJECT", "XS_FIRM" })
            {
                string v = "";
                try { Tekla.Structures.TeklaStructuresSettings.GetAdvancedOption(opt, ref v); } catch (Exception) { }
                if (string.IsNullOrEmpty(v)) continue;
                foreach (string d in v.Split(';')) if (d.Trim().Length > 0) dirs.Add(d.Trim());
            }
            dirs.AddRange(PluginDirs());
            foreach (string d in dirs)
            {
                string p;
                try { p = System.IO.Path.Combine(d, f); } catch (ArgumentException) { continue; }
                tried.Add(p);
                if (File.Exists(p)) return p;
            }
            return null;
        }

        /// Thư mục chứa DLL plugin của component (bản phát hành <Ten>.dll hoặc DEV <Ten>_DEV.dll) đang nạp trong tiến trình.
        /// Core bản DEV nạp dạng byte nên không có Location -> tìm theo tên assembly của vỏ plugin.
        private static List<string> PluginDirs()
        {
            List<string> r = new List<string>();
            string name = UEL.ComponentInfo.Name;
            foreach (System.Reflection.Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                string n = a.GetName().Name;
                if (!string.Equals(n, name, StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(n, name + "_DEV", StringComparison.OrdinalIgnoreCase)) continue;
                string loc = "";
                try { loc = a.Location; } catch (Exception) { }
                if (string.IsNullOrEmpty(loc)) continue;
                string d = System.IO.Path.GetDirectoryName(loc);
                if (!r.Contains(d)) r.Add(d);
            }
            return r;
        }

        // ------------------------------------------------------------------ gán vào Params

        /// Tìm bảng, tìm dòng theo khoá, gán các cột trùng tên biến [UelParam] vào prm (bỏ qua tên trong "skip").
        /// Trả về các biến đã gán (tên -> giá trị chuỗi), rỗng nếu không dùng được bảng. Mọi bước đều ghi log.
        public static Hashtable ApplyByKey(object prm, string fileOrPath, string defaultFile, string modelPath, string key,
                                           params string[] skip)
        {
            Hashtable applied = new Hashtable();
            string want = string.IsNullOrEmpty(fileOrPath) || fileOrPath.Trim().Length == 0 ? defaultFile : fileOrPath.Trim();
            List<string> tried = new List<string>();
            string path = Resolve(want, modelPath, tried);
            if (path == null)
            {
                Logger.Log("CANH BAO: khong tim thay bang tra '" + want + "' -> dung gia tri hop thoai. Da tim: " + string.Join(" | ", tried.ToArray()));
                return applied;
            }
            LookupTable t;
            try { t = Load(path); }
            catch (Exception ex)
            {
                Logger.Log("CANH BAO: khong doc duoc bang tra " + path + ": " + ex.Message + " -> dung gia tri hop thoai.");
                return applied;
            }
            string matched;
            string[] row = t.FindRow(key, out matched);
            if (row == null)
            {
                Logger.Log("CANH BAO: bang tra " + path + " khong co dong '" + key + "' (va khong co dong '*') -> dung gia tri hop thoai.");
                return applied;
            }

            Dictionary<string, PropertyInfo> props = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (PropertyInfo pi in prm.GetType().GetProperties())
            {
                UelParamAttribute a = (UelParamAttribute)Attribute.GetCustomAttribute(pi, typeof(UelParamAttribute));
                if (a != null && pi.CanWrite) props[a.Name] = pi;
            }
            List<string> skipList = new List<string>(skip ?? new string[0]);
            List<string> unknown = new List<string>();
            Hashtable values = new Hashtable();
            for (int i = 1; i < t.Header.Length && i < row.Length; i++)
            {
                string col = t.Header[i];
                if (col.Length == 0 || row[i].Length == 0) continue;
                PropertyInfo pi;
                if (!props.TryGetValue(col, out pi))
                {
                    if (Regex.IsMatch(col, @"^P\d+", RegexOptions.IgnoreCase)) unknown.Add(col);
                    continue;
                }
                bool skipIt = false;
                foreach (string s in skipList) if (string.Equals(s, col, StringComparison.OrdinalIgnoreCase)) skipIt = true;
                if (skipIt) continue;
                values[((UelParamAttribute)Attribute.GetCustomAttribute(pi, typeof(UelParamAttribute))).Name] = row[i];
            }
            ParamMapper.Apply(prm, values);
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry e in values) { applied[e.Key] = e.Value; sb.Append(e.Key).Append('=').Append(e.Value).Append(' '); }
            Logger.Log("Bang tra " + path + ": dong '" + matched + "' (khoa '" + key + "') -> " + (sb.Length > 0 ? sb.ToString().Trim() : "(khong co o nao)"));
            if (unknown.Count > 0)
                Logger.Log("CANH BAO: bang tra co cot khong phai bien cua component, bo qua: " + string.Join(", ", unknown.ToArray()));
            return applied;
        }
    }
}
