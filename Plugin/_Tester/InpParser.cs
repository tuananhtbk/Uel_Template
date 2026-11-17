// Đọc cú pháp hộp thoại .inp của Tekla (phần UserInterface trong Plugin.cs):
//   ten(arg, arg, ...) { ten(...) ... }
// Mỗi lệnh thành 1 InpNode: Name, Args (chuỗi, đã bỏ dấu "), Children (khối { }).
// Không hiểu nghĩa lệnh, chỉ tách cấu trúc; InpView quyết định vẽ gì.
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace UEL.Tester
{
    internal sealed class InpNode
    {
        public string Name;
        public readonly List<string> Args = new List<string>();
        public List<InpNode> Children = new List<InpNode>();

        public string Arg(int i)
        {
            return i < Args.Count ? Args[i] : "";
        }

        public int IntArg(int i, int fallback)
        {
            double d;
            if (double.TryParse(Arg(i), NumberStyles.Float, CultureInfo.InvariantCulture, out d)) return (int)d;
            return fallback;
        }

        public override string ToString()
        {
            return Name + "(" + string.Join(", ", Args.ToArray()) + ")";
        }
    }

    internal static class InpParser
    {
        public static List<InpNode> Parse(string s)
        {
            int i = 0;
            return ParseList(s ?? "", ref i, false);
        }

        /// Tìm lệnh đầu tiên có tên thuộc names (duyệt sâu).
        public static InpNode Find(List<InpNode> nodes, params string[] names)
        {
            foreach (InpNode n in nodes)
            {
                foreach (string k in names) if (n.Name == k) return n;
                InpNode r = Find(n.Children, names);
                if (r != null) return r;
            }
            return null;
        }

        private static List<InpNode> ParseList(string s, ref int i, bool inBlock)
        {
            List<InpNode> list = new List<InpNode>();
            while (true)
            {
                SkipWs(s, ref i);
                if (i >= s.Length) break;
                char c = s[i];
                if (c == '}')
                {
                    i++;
                    if (inBlock) break;
                    continue;
                }
                if (c == '{')
                {
                    // khối { } đứng riêng: gắn vào lệnh trước đó
                    i++;
                    List<InpNode> kids = ParseList(s, ref i, true);
                    if (list.Count > 0) list[list.Count - 1].Children.AddRange(kids);
                    continue;
                }
                if (char.IsLetter(c) || c == '_')
                {
                    int st = i;
                    while (i < s.Length && (char.IsLetterOrDigit(s[i]) || s[i] == '_')) i++;
                    InpNode n = new InpNode();
                    n.Name = s.Substring(st, i - st);
                    SkipWs(s, ref i);
                    if (i < s.Length && s[i] == '(')
                    {
                        i++;
                        ParseArgs(s, ref i, n.Args);
                    }
                    SkipWs(s, ref i);
                    if (i < s.Length && s[i] == '{')
                    {
                        i++;
                        n.Children = ParseList(s, ref i, true);
                    }
                    list.Add(n);
                    continue;
                }
                i++;   // ký tự lạ (; ...) -> bỏ qua
            }
            return list;
        }

        private static void ParseArgs(string s, ref int i, List<string> args)
        {
            StringBuilder cur = new StringBuilder();
            int depth = 0;
            while (i < s.Length)
            {
                char c = s[i];
                if (c == '"')
                {
                    int j = s.IndexOf('"', i + 1);
                    if (j < 0) j = s.Length;
                    cur.Append(s, i + 1, j - i - 1);
                    i = j + 1;
                    continue;
                }
                if (c == '(') depth++;
                if (c == ')')
                {
                    if (depth == 0)
                    {
                        i++;
                        args.Add(cur.ToString().Trim());
                        return;
                    }
                    depth--;
                }
                if (c == ',' && depth == 0)
                {
                    args.Add(cur.ToString().Trim());
                    cur.Length = 0;
                    i++;
                    continue;
                }
                cur.Append(c);
                i++;
            }
            args.Add(cur.ToString().Trim());
        }

        private static void SkipWs(string s, ref int i)
        {
            while (i < s.Length && char.IsWhiteSpace(s[i])) i++;
        }
    }
}
