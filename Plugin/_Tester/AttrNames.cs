// Tìm danh sách TÊN BIẾN (P1, P38_diameter, ...) của một component để đọc giá trị bằng GetAttribute.
// Tekla API không liệt kê được biến của component, nên lấy từ hộp thoại .inp theo thứ tự:
//   1. <thư mục model>\CustomComponentDialogFiles\<tên>.inp        (custom component đang có trong model)
//   2. DLL plugin UEL của bộ này (_Dev\<bộ>\<tên>.dll, <bộ>\<tên>.dll): hằng UserInterface
//   3. các file .uel trong thư mục UEL Template và <model>\UEL      (custom component, khớp tên joint/detail)
//   4. DLL plugin bất kỳ trong thư mục plugins của Tekla: [PluginUserInterface] + các [StructuresField]
//      (đọc metadata, KHÔNG chạy code của plugin)
//   5. dò P1...P300 (biến tính toán của custom component không có trong hộp thoại)
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace UEL.Tester
{
    internal sealed class AttrDef
    {
        public string Name;
        public string Kind;      // "int" | "double" | "string" | "" (chưa biết)
        public string InpType;   // distance, integer, material, option...
        public string Label;
        public string Source;
        public List<string> Options;
    }

    internal static class AttrNames
    {
        private static Dictionary<string, KeyValuePair<string, string>> _uelIndex;   // tên joint/detail -> (file, inp)

        public static Encoding Cp949
        {
            get
            {
                try { return Encoding.GetEncoding(949); }
                catch { return Encoding.Default; }
            }
        }

        /// Trả về (nguồn, nội dung .inp) hoặc null.
        public static KeyValuePair<string, string>? FindInp(string compName, string modelPath, string apiDir, Action<string> log)
        {
            if (string.IsNullOrEmpty(compName)) return null;

            // 1. model
            if (!string.IsNullOrEmpty(modelPath))
            {
                string f = Path.Combine(Path.Combine(modelPath, "CustomComponentDialogFiles"), compName + ".inp");
                if (File.Exists(f)) return new KeyValuePair<string, string>(f, Cp949.GetString(File.ReadAllBytes(f)));
            }

            // 2. plugin UEL của bộ này
            string pluginRoot = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'));
            foreach (string dll in new[]
            {
                Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Program.CoreTarget), compName + ".dll"),
                Path.Combine(Path.Combine(pluginRoot, Program.CoreTarget), compName + ".dll")
            })
            {
                if (!File.Exists(dll)) continue;
                try
                {
                    string ui = PluginMeta.ReadUserInterfaceConst(dll);
                    if (ui != null) return new KeyValuePair<string, string>(dll, ui);
                }
                catch (Exception ex) { log("Khong doc duoc " + dll + ": " + ex.Message); }
            }

            // 3. .uel
            if (_uelIndex == null) _uelIndex = IndexUel(new[] { Path.GetDirectoryName(pluginRoot), string.IsNullOrEmpty(modelPath) ? null : Path.Combine(modelPath, "UEL") }, log);
            KeyValuePair<string, string> hit;
            if (_uelIndex.TryGetValue(compName, out hit)) return hit;

            // 4. plugin DLL bất kỳ của Tekla
            KeyValuePair<string, string>? pl = PluginMeta.FindPluginUi(compName, apiDir, log);
            if (pl != null) return pl;
            return null;
        }

        /// File .uel có hộp thoại mang tên compName (tìm trong UEL Template và <model>\UEL), hoặc null.
        public static string FindUel(string compName, string modelPath, Action<string> log)
        {
            string pluginRoot = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'));
            if (_uelIndex == null) _uelIndex = IndexUel(new[] { Path.GetDirectoryName(pluginRoot), string.IsNullOrEmpty(modelPath) ? null : Path.Combine(modelPath, "UEL") }, log);
            KeyValuePair<string, string> hit;
            return _uelIndex.TryGetValue(compName, out hit) ? hit.Key : null;
        }

        /// Các bộ thuộc tính (preset) của component: file <model>\attributes\*.p_<tên> và các khối .p_<tên> trong file .uel.
        /// Trả về: nguồn -> nội dung (text).
        public static List<KeyValuePair<string, string>> Presets(string compName, string modelPath, string uelFile)
        {
            List<KeyValuePair<string, string>> r = new List<KeyValuePair<string, string>>();
            string ext = ".p_" + compName;
            if (!string.IsNullOrEmpty(modelPath))
            {
                string dir = Path.Combine(modelPath, "attributes");
                if (Directory.Exists(dir))
                    foreach (string f in Directory.GetFiles(dir))
                        if (f.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                            try { r.Add(new KeyValuePair<string, string>(f, Cp949.GetString(File.ReadAllBytes(f)))); } catch { }
            }
            if (!string.IsNullOrEmpty(uelFile))
                try
                {
                    foreach (KeyValuePair<string, string> s in SectionsOfUel(uelFile))
                        if (s.Key.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                            r.Add(new KeyValuePair<string, string>(uelFile + " | " + s.Key, s.Value));
                }
                catch { }
            return r;
        }

        /// Đọc .inp -> danh sách biến (tên, kiểu, nhãn).
        public static List<AttrDef> FromInp(string inp, string source)
        {
            List<AttrDef> r = new List<AttrDef>();
            List<InpNode> nodes = InpParser.Parse(inp);
            Collect(nodes, r, source);
            Dictionary<string, bool> seen = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            List<AttrDef> uniq = new List<AttrDef>();
            foreach (AttrDef d in r)
                if (!string.IsNullOrEmpty(d.Name) && !seen.ContainsKey(d.Name)) { seen[d.Name] = true; uniq.Add(d); }
            return uniq;
        }

        private static void Collect(List<InpNode> nodes, List<AttrDef> r, string source)
        {
            List<InpNode> labels = new List<InpNode>();
            foreach (InpNode n in nodes) if (n.Name == "attribute" && n.Arg(2) == "label") labels.Add(n);
            foreach (InpNode n in nodes)
            {
                if (n.Name == "parameter")
                {
                    AttrDef d = new AttrDef();
                    d.Name = n.Arg(1);
                    d.InpType = n.Arg(2);
                    d.Kind = KindOf(n.Arg(2), n.Arg(3));
                    d.Label = n.Arg(0).Length > 0 ? n.Arg(0) : NearLabel(labels, n.IntArg(4, -1), n.IntArg(5, -1), n.Args.Count >= 7);
                    d.Source = source;
                    r.Add(d);
                }
                else if (n.Name == "attribute" && n.Arg(2) == "option")
                {
                    AttrDef d = new AttrDef();
                    d.Name = n.Arg(0);
                    d.InpType = "option";
                    d.Kind = "int";
                    d.Label = n.Arg(1).Length > 0 ? n.Arg(1) : NearLabel(labels, n.IntArg(8, -1), n.IntArg(9, -1), n.Args.Count >= 11);
                    d.Options = new List<string>();
                    foreach (InpNode v in n.Children) if (v.Name == "value") d.Options.Add(v.Arg(0));
                    d.Source = source;
                    r.Add(d);
                }
                if (n.Children.Count > 0) Collect(n.Children, r, source);
            }
        }

        /// Nhãn đứng bên trái ô (cùng hàng, lệch y <= 15) khi hộp thoại dùng toạ độ tuyệt đối.
        private static string NearLabel(List<InpNode> labels, int x, int y, bool absolute)
        {
            if (!absolute || x < 0) return "";
            InpNode best = null;
            int bestX = int.MinValue;
            foreach (InpNode l in labels)
            {
                if (l.Args.Count < 10) continue;
                int lx = l.IntArg(8, 0), ly = l.IntArg(9, 0);
                if (Math.Abs(ly - y) <= 15 && lx < x && lx > bestX) { best = l; bestX = lx; }
            }
            return best == null ? "" : best.Arg(1);
        }

        public static string KindOf(string type, string format)
        {
            if (format == "text") return "string";
            switch (type)
            {
                case "integer":
                case "YesNo":
                case "option":
                case "chamfer_type":
                case "weld_type":
                case "welding_site":
                case "bolt_type":
                    return "int";
                default:
                    return "double";
            }
        }

        // ---------------------------------------------------------------- .uel

        private static readonly Regex SecRx = new Regex(@"<FileInfo><Name>(.*?)</Name><Size>(\d+)</Size><Compressed>(\d)</Compressed></FileInfo>", RegexOptions.Singleline);
        private static readonly Regex OwnerRx = new Regex(@"\b(joint|detail|part|seam)\s*\(\s*\d+\s*,\s*""([^""]*)""");

        private static Dictionary<string, KeyValuePair<string, string>> IndexUel(string[] dirs, Action<string> log)
        {
            Dictionary<string, KeyValuePair<string, string>> idx = new Dictionary<string, KeyValuePair<string, string>>(StringComparer.OrdinalIgnoreCase);
            foreach (string d in dirs)
            {
                if (string.IsNullOrEmpty(d) || !Directory.Exists(d)) continue;
                string[] files;
                try { files = Directory.GetFiles(d, "*.uel", SearchOption.AllDirectories); }
                catch { continue; }
                foreach (string f in files)
                {
                    if (f.IndexOf(@"\Plugin\", StringComparison.OrdinalIgnoreCase) >= 0) continue;
                    try
                    {
                        foreach (string inp in InpOfUel(f))
                        {
                            Match m = OwnerRx.Match(inp);
                            if (m.Success && !idx.ContainsKey(m.Groups[2].Value)) idx[m.Groups[2].Value] = new KeyValuePair<string, string>(f, inp);
                        }
                    }
                    catch (Exception ex) { log("Bo qua " + f + ": " + ex.Message); }
                }
            }
            return idx;
        }

        /// Các phần .inp bên trong 1 file .uel (gzip, chuỗi khối <FileInfo> + dữ liệu).
        public static List<string> InpOfUel(string file)
        {
            List<string> r = new List<string>();
            foreach (KeyValuePair<string, string> s in SectionsOfUel(file))
                if (s.Key.EndsWith(".inp", StringComparison.OrdinalIgnoreCase)) r.Add(s.Value);
            return r;
        }

        /// Mọi khối trong file .uel: tên khối -> nội dung (giải mã cp949). Bỏ qua khối nén.
        public static List<KeyValuePair<string, string>> SectionsOfUel(string file)
        {
            byte[] data;
            using (FileStream fs = File.OpenRead(file))
            using (GZipStream gz = new GZipStream(fs, CompressionMode.Decompress))
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buf = new byte[65536];
                int n;
                while ((n = gz.Read(buf, 0, buf.Length)) > 0) ms.Write(buf, 0, n);
                data = ms.ToArray();
            }
            string latin = Encoding.GetEncoding(28591).GetString(data);   // 1 byte = 1 ký tự -> vị trí khớp byte
            List<KeyValuePair<string, string>> r = new List<KeyValuePair<string, string>>();
            foreach (Match m in SecRx.Matches(latin))
            {
                int size = int.Parse(m.Groups[2].Value);
                int start = m.Index + m.Length;
                if (start + size > data.Length) size = data.Length - start;
                byte[] nameBytes = Encoding.GetEncoding(28591).GetBytes(m.Groups[1].Value);
                r.Add(new KeyValuePair<string, string>(Cp949.GetString(nameBytes), Cp949.GetString(data, start, size)));
            }
            return r;
        }
    }

    /// Đọc metadata của DLL plugin mà không chạy code của nó.
    internal static class PluginMeta
    {
        /// Hằng UserInterface (.inp) trong lớp plugin (plugin UEL của bộ này).
        public static string ReadUserInterfaceConst(string dll)
        {
            System.Reflection.Assembly a = System.Reflection.Assembly.Load(File.ReadAllBytes(dll));
            Type[] types;
            try { types = a.GetTypes(); }
            catch (ReflectionTypeLoadException ex) { types = ex.Types; }
            foreach (Type t in types)
            {
                if (t == null) continue;
                FieldInfo f = t.GetField("UserInterface", BindingFlags.Public | BindingFlags.Static);
                if (f != null && f.IsLiteral) return f.GetRawConstantValue() as string;
            }
            return null;
        }

        private static string _apiDir;

        private static System.Reflection.Assembly OnReflectionResolve(object sender, ResolveEventArgs e)
        {
            try { return System.Reflection.Assembly.ReflectionOnlyLoad(e.Name); }
            catch { }
            string simple = new AssemblyName(e.Name).Name;
            foreach (string d in new[] { _apiDir, _apiDir == null ? null : Path.GetDirectoryName(_apiDir.TrimEnd('\\')) })
            {
                if (d == null) continue;
                string p = Path.Combine(d, simple + ".dll");
                if (File.Exists(p)) return System.Reflection.Assembly.ReflectionOnlyLoadFrom(p);
            }
            return null;
        }

        /// Tìm DLL có [Plugin("compName")] trong thư mục plugins của Tekla; trả về .inp (nếu UI là .inp)
        /// hoặc 1 .inp tự dựng từ các trường [StructuresField] của lớp dữ liệu.
        public static KeyValuePair<string, string>? FindPluginUi(string compName, string apiDir, Action<string> log)
        {
            if (string.IsNullOrEmpty(apiDir) || !Directory.Exists(apiDir)) return null;
            string dir = apiDir.TrimEnd('\\').EndsWith("plugins", StringComparison.OrdinalIgnoreCase) ? apiDir : Path.Combine(apiDir, "plugins");
            if (!Directory.Exists(dir)) return null;
            byte[] needle = Encoding.UTF8.GetBytes(compName);
            _apiDir = apiDir;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += OnReflectionResolve;
            try
            {
                foreach (string f in Directory.GetFiles(dir, "*.dll", SearchOption.AllDirectories))
                {
                    FileInfo fi = new FileInfo(f);
                    if (fi.Length > 30 * 1024 * 1024 || fi.Name.StartsWith("Tekla.", StringComparison.OrdinalIgnoreCase)) continue;
                    byte[] bytes;
                    try { bytes = File.ReadAllBytes(f); } catch { continue; }
                    if (IndexOf(bytes, needle) < 0) continue;
                    try
                    {
                        string r = UiOfAssembly(System.Reflection.Assembly.ReflectionOnlyLoad(bytes), compName);
                        if (r != null) return new KeyValuePair<string, string>(f, r);
                    }
                    catch (Exception ex) { log("Bo qua " + fi.Name + ": " + ex.Message); }
                }
            }
            finally
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= OnReflectionResolve;
            }
            return null;
        }

        private static string UiOfAssembly(System.Reflection.Assembly a, string compName)
        {
            Type[] types;
            try { types = a.GetTypes(); }
            catch (ReflectionTypeLoadException ex) { types = ex.Types; }
            foreach (Type t in types)
            {
                if (t == null) continue;
                IList<CustomAttributeData> attrs;
                try { attrs = CustomAttributeData.GetCustomAttributes(t); } catch { continue; }
                string name = null, ui = null;
                foreach (CustomAttributeData cad in attrs)
                {
                    string an = cad.Constructor.DeclaringType.Name;
                    if (cad.ConstructorArguments.Count == 0) continue;
                    object v = cad.ConstructorArguments[0].Value;
                    if (an == "PluginAttribute") name = v as string;
                    if (an == "PluginUserInterfaceAttribute") ui = v as string;
                }
                if (name != compName) continue;
                if (ui != null && ui.Contains("parameter(")) return ui;

                // UI là form WinForms/WPF -> dựng .inp giả từ [StructuresField] của lớp dữ liệu (tham số hàm tạo)
                StringBuilder sb = new StringBuilder("page(\"TeklaStructures\",\"\")\n{\n joint(1, \"" + compName + "\")\n {\n");
                foreach (ConstructorInfo ci in t.GetConstructors())
                    foreach (ParameterInfo pi in ci.GetParameters())
                        foreach (FieldInfo fi in pi.ParameterType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                        {
                            IList<CustomAttributeData> fa;
                            try { fa = CustomAttributeData.GetCustomAttributes(fi); } catch { continue; }
                            foreach (CustomAttributeData cad in fa)
                            {
                                if (cad.Constructor.DeclaringType.Name != "StructuresFieldAttribute" || cad.ConstructorArguments.Count == 0) continue;
                                string fmt = fi.FieldType == typeof(string) ? "text" : "number";
                                string ty = fi.FieldType == typeof(int) ? "integer" : fi.FieldType == typeof(string) ? "string" : "distance";
                                sb.Append("  parameter(\"").Append(fi.Name).Append("\", \"").Append(cad.ConstructorArguments[0].Value)
                                  .Append("\", ").Append(ty).Append(", ").Append(fmt).Append(", 1)\n");
                            }
                        }
                sb.Append(" }\n}\n");
                return sb.ToString();
            }
            return null;
        }

        private static int IndexOf(byte[] hay, byte[] needle)
        {
            if (needle.Length == 0) return -1;
            for (int i = 0; i <= hay.Length - needle.Length; i++)
            {
                if (hay[i] != needle[0]) continue;
                int k = 1;
                while (k < needle.Length && hay[i + k] == needle[k]) k++;
                if (k == needle.Length) return i;
            }
            return -1;
        }
    }
}
