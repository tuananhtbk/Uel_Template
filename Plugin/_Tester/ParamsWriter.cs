// Nút "Ghi bang thong so vao Params.cs" (người dùng 18/11/2026): ghi các giá trị đã đổi trong tab "Bang thong so"
// thành giá trị MẶC ĐỊNH trong hàm khởi tạo của lớp Params (vd. COL_TO_GIRDER\Params.cs), rồi build Core.
//
// Quy tắc:
//   - Chỉ ghi thuộc tính KHÁC mặc định hiện tại (so với NewParams của Core đang nạp).
//   - Bỏ qua ô "trong = ..." (DisplayName có "(trong"): trống/0 ở đó nghĩa là TÍNH theo công thức .uel / bảng tra,
//     ghi số vào sẽ tắt công thức.
//   - Chỉ thay khi phép gán cũ là hằng (số, true/false, "chuoi"); vế phải là biểu thức thì bỏ qua.
//   - Thay đúng vế phải của phép gán "Ten = ...;" đầu tiên trong thân hàm khởi tạo; cuối dòng thêm ghi chú
//     "EXE dd/MM: P4 65 -> 55" để còn biết giá trị gốc (Plugin\CLAUDE.md: mặc định lấy từ .uel gốc).
//   - Giữ nguyên encoding (BOM hay không) và kiểu xuống dòng.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace UEL.Tester
{
    internal sealed class ParamChange
    {
        public string Prop;
        public string Label;       // DisplayName (hoặc tên thuộc tính)
        public string Uel;         // "P4" lấy từ đầu DisplayName, có thể rỗng
        public string OldText;     // vế phải cũ trong .cs
        public string NewText;     // vế phải mới (literal C#)
        public string Skip;        // khác null = không ghi, lý do
        public int Line;           // dòng (1-based) trong file
    }

    internal static class ParamsWriter
    {
        private static readonly CultureInfo CI = CultureInfo.InvariantCulture;
        private static readonly Regex Literal = new Regex(@"^\s*(-?\d+(\.\d+)?|true|false|""([^""\\]|\\.)*"")\s*$");

        /// File .cs (trong compDir, bỏ Plugin*.cs) chứa hàm khởi tạo "public <cls>()". null nếu không thấy.
        public static string FindFile(string compDir, string cls)
        {
            Regex ctor = new Regex(@"\bpublic\s+" + Regex.Escape(cls) + @"\s*\(\s*\)");
            foreach (string f in KnobSource.CoreFiles(compDir))
                if (ctor.IsMatch(File.ReadAllText(f))) return f;
            return null;
        }

        /// So sánh giá trị đang có với mặc định, tìm phép gán trong hàm khởi tạo. Không ghi gì.
        public static List<ParamChange> Diff(object current, object defaults, string file)
        {
            List<ParamChange> list = new List<ParamChange>();
            string text = TextFile.Read(file).Text;
            int b0, b1;
            if (!CtorBody(text, current.GetType().Name, out b0, out b1)) return null;
            foreach (PropertyInfo p in current.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!p.CanRead || !p.CanWrite || p.GetIndexParameters().Length > 0) continue;
                object v = p.GetValue(current, null), d = p.GetValue(defaults, null);
                string nv = ToLiteral(v);
                if (nv == null || nv == ToLiteral(d)) continue;

                ParamChange c = new ParamChange();
                c.Prop = p.Name;
                c.NewText = nv;
                c.Label = p.Name;
                object[] dn = p.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                if (dn.Length > 0) c.Label = ((DisplayNameAttribute)dn[0]).DisplayName;
                Match mp = Regex.Match(c.Label, @"^(P\d+\w*)\s");
                c.Uel = mp.Success ? mp.Groups[1].Value : "";
                list.Add(c);

                if (c.Label.IndexOf("(trong", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    c.Skip = "o 'trong = tinh/bang tra' - ghi so se tat cong thuc";
                    continue;
                }
                Match m = FindAssign(text, b0, b1, p.Name);
                if (!m.Success) { c.Skip = "khong thay '" + p.Name + " = ...;' trong ham khoi tao"; continue; }
                c.OldText = m.Groups["val"].Value.Trim();
                c.Line = LineOf(text, m.Index);
                if (!Literal.IsMatch(c.OldText)) c.Skip = "ve phai la bieu thuc (" + c.OldText + "), khong phai hang";
            }
            return list;
        }

        /// Ghi các thay đổi không bị bỏ qua. Trả về số giá trị đã ghi.
        public static int Write(string file, string cls, List<ParamChange> changes, Action<string> log)
        {
            TextFile tf = TextFile.Read(file);
            string text = tf.Text;
            int b0, b1;
            if (!CtorBody(text, cls, out b0, out b1)) { log("Khong thay ham khoi tao " + cls + "() trong " + file); return 0; }

            // ghi chú cuối dòng, gom theo dòng
            Dictionary<int, List<string>> notes = new Dictionary<int, List<string>>();
            int n = 0;
            string day = DateTime.Now.ToString("dd/MM/yyyy", CI);
            foreach (ParamChange c in changes)
            {
                if (c.Skip != null) continue;
                Match m = FindAssign(text, b0, b1, c.Prop);
                if (!m.Success ||m.Groups["val"].Value.Trim() != c.OldText)
                {
                    log("Params: " + c.Prop + " da bi sua trong .cs tu luc so sanh -> bo qua");
                    continue;
                }
                Group g = m.Groups["val"];
                string lead = g.Value.Substring(0, g.Value.Length - g.Value.TrimStart().Length);
                string trail = g.Value.Substring(g.Value.TrimEnd().Length);
                string repl = lead + c.NewText + trail;
                text = text.Substring(0, g.Index) + repl + text.Substring(g.Index + g.Length);
                int delta = repl.Length - g.Length;
                // giữ cột comment: "...;<cách>// ..." -> bớt / thêm dấu cách theo độ dài mới (còn ít nhất 1)
                int semi = g.Index + repl.Length;                       // vị trí dấu ;
                int sp = semi + 1;
                while (sp < text.Length && text[sp] == ' ') sp++;
                if (delta != 0 && sp + 1 < text.Length && text[sp] == '/' && text[sp + 1] == '/')
                {
                    int want = Math.Max(1, (sp - semi - 1) - delta);
                    text = text.Substring(0, semi + 1) + new string(' ', want) + text.Substring(sp);
                    delta += want - (sp - semi - 1);
                }
                b1 += delta;
                int line = LineOf(text, g.Index);
                List<string> l;
                if (!notes.TryGetValue(line, out l)) notes[line] = l = new List<string>();
                l.Add((c.Uel.Length > 0 ? c.Uel : c.Prop) + " " + c.OldText + " -> " + c.NewText);
                log("Params: dong " + line + ": " + c.Prop + " = " + c.OldText + " -> " + c.NewText);
                n++;
            }
            if (n == 0) return 0;

            string[] lines = text.Split('\n');
            foreach (KeyValuePair<int, List<string>> kv in notes)
            {
                int i = kv.Key - 1;
                bool cr = lines[i].EndsWith("\r");
                string s = cr ? lines[i].Substring(0, lines[i].Length - 1) : lines[i];
                string note = "EXE " + day + ": " + string.Join(", ", kv.Value.ToArray());
                s = s.TrimEnd() + (CodeHasComment(s) ? " | " : "   // ") + note;
                lines[i] = s + (cr ? "\r" : "");
            }
            tf.Text = string.Join("\n", lines);
            tf.Write(file);
            return n;
        }

        // ------------------------------------------------------------------

        /// Phép gán "Prop = <val>;" đầu tiên trong [b0, b1) không nằm trong comment //.
        /// Không khớp "Prop == ", "XProp =", "Prop.Y =".
        private static Match FindAssign(string text, int b0, int b1, string prop)
        {
            Regex r = new Regex(@"(?<![\w.])" + Regex.Escape(prop) + @"\s*=(?!=)(?<val>(""([^""\\]|\\.)*""|[^;""\n])*);");
            Match m = r.Match(text, b0, b1 - b0);
            while (m.Success)
            {
                int ls = text.LastIndexOf('\n', Math.Max(0, m.Index - 1)) + 1;
                if (!CodeHasComment(text.Substring(ls, m.Index - ls) + "  ")) return m;
                m = m.NextMatch();
            }
            return m;
        }

        private static string ToLiteral(object v)
        {
            if (v == null) return null;
            if (v is bool) return (bool)v ? "true" : "false";
            if (v is int) return ((int)v).ToString(CI);
            if (v is double) return ((double)v).ToString("R", CI);
            string s = v as string;
            if (s != null) return "\"" + s.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
            return null;   // kiểu khác: không hỗ trợ
        }

        /// Thân hàm khởi tạo "public <cls>()": [b0, b1) giữa 2 ngoặc nhọn.
        private static bool CtorBody(string text, string cls, out int b0, out int b1)
        {
            b0 = b1 = -1;
            Match m = new Regex(@"\bpublic\s+" + Regex.Escape(cls) + @"\s*\(\s*\)\s*(:\s*\w+\s*\([^)]*\)\s*)?\{").Match(text);
            if (!m.Success) return false;
            b0 = m.Index + m.Length;
            int depth = 1;
            bool str = false, lineCmt = false, blockCmt = false;
            for (int i = b0; i < text.Length; i++)
            {
                char ch = text[i], nx = i + 1 < text.Length ? text[i + 1] : '\0';
                if (lineCmt) { if (ch == '\n') lineCmt = false; continue; }
                if (blockCmt) { if (ch == '*' && nx == '/') { blockCmt = false; i++; } continue; }
                if (str) { if (ch == '\\') i++; else if (ch == '"') str = false; continue; }
                if (ch == '/' && nx == '/') { lineCmt = true; i++; continue; }
                if (ch == '/' && nx == '*') { blockCmt = true; i++; continue; }
                if (ch == '"') { str = true; continue; }
                if (ch == '{') depth++;
                else if (ch == '}' && --depth == 0) { b1 = i; return true; }
            }
            return false;
        }

        private static bool CodeHasComment(string line)
        {
            bool str = false;
            for (int i = 0; i + 1 < line.Length; i++)
            {
                if (str) { if (line[i] == '\\') i++; else if (line[i] == '"') str = false; continue; }
                if (line[i] == '"') str = true;
                else if (line[i] == '/' && line[i + 1] == '/') return true;
            }
            return false;
        }

        private static int LineOf(string text, int index)
        {
            int n = 1;
            for (int i = 0; i < index && i < text.Length; i++) if (text[i] == '\n') n++;
            return n;
        }
    }

    /// File UTF-8, nhớ có BOM hay không để ghi lại y như cũ.
    internal sealed class TextFile
    {
        public string Text;
        private bool _bom;

        public static TextFile Read(string path)
        {
            byte[] b = File.ReadAllBytes(path);
            TextFile t = new TextFile();
            t._bom = b.Length >= 3 && b[0] == 0xEF && b[1] == 0xBB && b[2] == 0xBF;
            int skip = t._bom ? 3 : 0;
            t.Text = new UTF8Encoding(false).GetString(b, skip, b.Length - skip);
            return t;
        }

        public void Write(string path)
        {
            File.WriteAllText(path, Text, new UTF8Encoding(_bom));
        }
    }
}
