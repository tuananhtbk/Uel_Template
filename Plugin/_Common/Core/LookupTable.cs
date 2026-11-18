// Bảng tra dùng chung cho mọi component (tương đương fVF("file", khoá, cột) của .uel gốc).
//
// Định dạng file (giống các bảng .txt trong UEL Template):
//   - phân cách TAB, dòng đầu (không tính dòng trống / dòng "#") là tiêu đề;
//   - cột đầu là KHOÁ (thường là profile của Part 2). Các cột sau đặt tên theo một trong 3 cách (18/11/2026):
//       tên dễ đọc khai báo ở [UelParam("P1", Table = "Base Plate Thk")]  (không phân biệt hoa thường, bỏ dấu cách . _ - '),
//       tên biến P (P1, P20, P38_diameter...), hoặc tên kèm biến trong ngoặc "Base Plate Thk (P1)";
//   - cột REMARK / NOTE / GHI CHU bỏ qua; cột khác không nhận ra thì bỏ qua và ghi CANH BAO; ô trống = không đổi giá trị đó;
//   - dòng khoá "*" = dòng mặc định khi không có dòng nào khớp;
//   - encoding cp949 hoặc UTF-8 (tự nhận), xuống dòng CRLF hoặc LF.
//
// Tìm file (ApplyByKey, người dùng yêu cầu 18/11/2026): bảng tra lưu trong THƯ MỤC MODEL và luôn ưu tiên bản trong model.
//   1. Thư mục model đã có file cùng tên -> dùng file đó (kể cả khi ô chọn file ở chỗ khác).
//   2. Chưa có -> tìm file nguồn: đường dẫn đầy đủ thì dùng luôn; chỉ có tên (hoặc đường dẫn tương đối) thì tìm lần lượt
//      XS_PROJECT -> XS_FIRM -> thư mục chứa DLL plugin; tìm thấy thì CHÉP 1 bản vào thư mục model rồi dùng bản đó.
//   Không bao giờ ghi đè file đã có trong model. Ngoài việc chép đó, bảng chỉ được ĐỌC.
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

        /// Bảng tra lưu trong model: thư mục model có file cùng tên thì dùng file đó; chưa có thì tìm file nguồn (Resolve)
        /// và chép 1 bản vào thư mục model (không ghi đè). Không biết thư mục model thì như Resolve.
        public static string ResolveInModel(string fileOrPath, string modelPath, List<string> tried)
        {
            if (string.IsNullOrEmpty(fileOrPath) || fileOrPath.Trim().Length == 0) return null;
            string f = fileOrPath.Trim().Trim('"');
            if (string.IsNullOrEmpty(modelPath)) return Resolve(f, modelPath, tried);
            string inModel;
            try { inModel = System.IO.Path.Combine(modelPath, System.IO.Path.GetFileName(f)); }
            catch (ArgumentException) { return Resolve(f, modelPath, tried); }
            tried.Add(inModel);
            if (File.Exists(inModel))
            {
                if (System.IO.Path.IsPathRooted(f) && File.Exists(f) && !SamePath(f, inModel))
                    Logger.Log("Bang tra: model da co " + System.IO.Path.GetFileName(f) + " -> dung ban trong model (" + inModel +
                               "), KHONG dung " + f + ". Muon dung bang moi: sua file trong model, hoac xoa no de chep lai.");
                return inModel;
            }
            string src = Resolve(f, modelPath, tried);
            if (src == null) return null;
            try
            {
                File.Copy(src, inModel, false);
                Logger.Log("Bang tra: da chep " + src + " vao model -> " + inModel);
                return inModel;
            }
            catch (Exception ex)
            {
                Logger.Log("CANH BAO: khong chep duoc bang tra vao model (" + ex.Message + ") -> dung " + src);
                return src;
            }
        }

        private static bool SamePath(string a, string b)
        {
            try { return string.Equals(System.IO.Path.GetFullPath(a), System.IO.Path.GetFullPath(b), StringComparison.OrdinalIgnoreCase); }
            catch (Exception) { return false; }
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
        /// Trả về các biến đã gán (tên -> giá trị chuỗi).
        /// Người dùng 18/11/2026: không có bảng / không đọc được / không có dòng khoá / ô không đúng kiểu (vd. "M24" ở cột
        /// Bolt Size) -> Logger.Error và trả về null: component KHÔNG được dựng (không lùi về giá trị hộp thoại nữa).
        /// Ô trống vẫn là "giữ giá trị hộp thoại" (vd. Gusset Length trống = tự tính).
        public static Hashtable ApplyByKey(object prm, string fileOrPath, string defaultFile, string modelPath, string key,
                                           params string[] skip)
        {
            Hashtable applied = new Hashtable();
            string want = string.IsNullOrEmpty(fileOrPath) || fileOrPath.Trim().Length == 0 ? defaultFile : fileOrPath.Trim();
            List<string> tried = new List<string>();
            string path = ResolveInModel(want, modelPath, tried);
            if (path == null)
            {
                Logger.Error("Khong tim thay bang tra '" + want + "'. Da tim: " + string.Join(" | ", tried.ToArray()));
                return null;
            }
            LookupTable t;
            try { t = Load(path); }
            catch (Exception ex)
            {
                Logger.Error("Khong doc duoc bang tra " + path + ": " + ex.Message);
                return null;
            }
            string matched;
            string[] row = t.FindRow(key, out matched);
            if (row == null)
            {
                Logger.Error("Bang tra " + path + " khong co dong '" + key + "'. Them dong nay vao bang (hoac tat bang tra) roi Modify.");
                return null;
            }

            // Tiêu đề cột: tên biến P ("P1"), tên dễ đọc ([UelParam(Table = "Base Plate Thk")]),
            // hoặc tên kèm biến trong ngoặc ("Base Plate Thk (P1)" -> lấy P1).
            Dictionary<string, PropertyInfo> props = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, PropertyInfo> byTable = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (PropertyInfo pi in prm.GetType().GetProperties())
            {
                UelParamAttribute a = (UelParamAttribute)Attribute.GetCustomAttribute(pi, typeof(UelParamAttribute));
                if (a == null || !pi.CanWrite) continue;
                props[a.Name] = pi;
                if (!string.IsNullOrEmpty(a.Table)) byTable[NormalizeHeader(a.Table)] = pi;
            }
            List<string> skipList = new List<string>(skip ?? new string[0]);
            List<string> unknown = new List<string>();
            Hashtable values = new Hashtable();
            bool bad = false;
            for (int i = 1; i < t.Header.Length && i < row.Length; i++)
            {
                string col = t.Header[i];
                if (col.Length == 0 || row[i].Length == 0) continue;
                PropertyInfo pi;
                Match inParen = Regex.Match(col, @"[\(\[]\s*(P\d+\w*)\s*[\)\]]\s*$", RegexOptions.IgnoreCase);
                bool found = props.TryGetValue(col, out pi)
                             || (inParen.Success && props.TryGetValue(inParen.Groups[1].Value, out pi))
                             || byTable.TryGetValue(NormalizeHeader(col), out pi);
                if (!found)
                {
                    if (!IsRemarkColumn(col)) unknown.Add(col);
                    continue;
                }
                string pname = ((UelParamAttribute)Attribute.GetCustomAttribute(pi, typeof(UelParamAttribute))).Name;
                bool skipIt = false;
                foreach (string s in skipList) if (string.Equals(s, pname, StringComparison.OrdinalIgnoreCase)) skipIt = true;
                if (skipIt) continue;
                // ô phải đổi được sang kiểu của biến (số cho thickness / bolt size...), không thì dừng
                try { ParamMapper.ConvertTo(row[i], pi.PropertyType); }
                catch (Exception)
                {
                    Logger.Error("Bang tra " + path + ", dong '" + matched + "', cot '" + col + "': gia tri '" + row[i] +
                                 "' khong hop le (can " + (pi.PropertyType == typeof(string) ? "chu" : "so") + ").");
                    bad = true;
                    continue;
                }
                values[pname] = row[i];
            }
            if (bad) return null;
            ParamMapper.Apply(prm, values);
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry e in values) { applied[e.Key] = e.Value; sb.Append(e.Key).Append('=').Append(e.Value).Append(' '); }
            Logger.Log("Bang tra " + path + ": dong '" + matched + "' (khoa '" + key + "') -> " + (sb.Length > 0 ? sb.ToString().Trim() : "(khong co o nao)"));
            if (unknown.Count > 0)
                Logger.Log("CANH BAO: bang tra co cot khong nhan ra (sai ten?), bo qua: " + string.Join(", ", unknown.ToArray()) +
                           ". Ten cot hop le: " + string.Join(", ", ColumnNames(prm).ToArray()));
            return applied;
        }

        /// So khớp tên cột: không phân biệt hoa thường, bỏ dấu cách và . _ - ' ( ).
        public static string NormalizeHeader(string s)
        {
            return s == null ? "" : Regex.Replace(s, @"[\s\._\-'’\(\)]", "").ToUpperInvariant();
        }

        /// Cột ghi chú (bỏ qua, không cảnh báo): REMARK, NOTE, GHI CHU...
        private static bool IsRemarkColumn(string col)
        {
            string n = NormalizeHeader(col);
            return n.StartsWith("REMARK") || n.StartsWith("NOTE") || n.StartsWith("GHICHU") || n.StartsWith("COMMENT");
        }

        /// Tên cột dùng được cho bảng tra của component: "Ten de doc (P..)" hoặc "P..".
        public static List<string> ColumnNames(object prm)
        {
            List<string> r = new List<string>();
            foreach (PropertyInfo pi in prm.GetType().GetProperties())
            {
                UelParamAttribute a = (UelParamAttribute)Attribute.GetCustomAttribute(pi, typeof(UelParamAttribute));
                if (a == null || !pi.CanWrite) continue;
                r.Add(string.IsNullOrEmpty(a.Table) ? a.Name : a.Table + " (" + a.Name + ")");
            }
            return r;
        }
    }
}
