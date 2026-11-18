// Tab "Knobs": chỉnh các con số hình học trong code Core bằng thanh trượt, xem ngay trong Tekla, ưng thì ghi vào .cs.
//
// Khai báo trong .cs của component (Builder.cs, Component.cs..., KHÔNG phải Plugin*.cs) - chỉ những dòng nằm giữa 2 dấu:
//     // <KNOBS>
//     internal static double BevelExtra = 3.0;    // [0..20, 0.5] Khoi cat vat vuot mat canh (mm)
//     internal static int    MaxRows    = 4;      // [1..10] So hang toi da
//     // </KNOBS>
//   - Kiểu double hoặc int, là trường static THƯỜNG (không const, không readonly) -> EXE gán được bằng reflection.
//   - Comment: [min..max] hoặc [min..max, buoc] rồi mô tả. Không có [..] thì chỉ có ô số, không có thanh trượt.
//
// Cách chạy:
//   - Kéo / gõ số: EXE gán giá trị vào trường static của Core đang nạp rồi dựng lại (không biên dịch, gần như tức thì).
//   - "Ghi vao .cs": thay đúng con số trên dòng đó (giữ nguyên phần còn lại, encoding, xuống dòng), rồi build Core.
//   - EXE chỉ sửa trong khối <KNOBS>; phần code khác không bao giờ bị đụng.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UEL.Tester
{
    internal sealed class Knob
    {
        public string File;        // đường dẫn đầy đủ
        public int Line;           // số dòng (1-based) lúc quét, để hiện
        public string Class;       // tên lớp chứa trường
        public string Name;
        public bool IsInt;
        public double Source;      // giá trị đang ghi trong .cs
        public double Value;       // giá trị đang chỉnh
        public bool HasRange;
        public double Min, Max, Step;
        public string Note;

        public bool Dirty { get { return Math.Abs(Value - Source) > 1e-9; } }

        public string Format(double v)
        {
            if (IsInt) return ((long)Math.Round(v)).ToString(CultureInfo.InvariantCulture);
            string s = v.ToString("R", CultureInfo.InvariantCulture);
            if (s.IndexOf('.') < 0 && s.IndexOf('E') < 0) s += ".0";
            return s;
        }
    }

    internal static class KnobSource
    {
        private const string Open = "// <KNOBS>";
        private const string Close = "// </KNOBS>";
        private static readonly Regex Decl = new Regex(
            @"^(?<head>\s*(?:(?:public|internal|private|protected)\s+)?static\s+(?<type>double|int)\s+(?<name>\w+)\s*=\s*)(?<val>-?\d+(?:\.\d+)?)(?<tail>\s*;.*)$");
        private static readonly Regex Range = new Regex(
            @"^\s*\[\s*(?<min>-?\d+(?:\.\d+)?)\s*\.\.\s*(?<max>-?\d+(?:\.\d+)?)\s*(?:,\s*(?<step>\d+(?:\.\d+)?))?\s*\]\s*(?<note>.*)$");
        private static readonly Regex ClassDecl = new Regex(@"\bclass\s+(\w+)");
        private static readonly CultureInfo CI = CultureInfo.InvariantCulture;

        /// Các file .cs của Core (giống build.ps1: bỏ Plugin*.cs và thư mục con bắt đầu bằng "_").
        public static List<string> CoreFiles(string compDir)
        {
            List<string> r = new List<string>();
            if (!Directory.Exists(compDir)) return r;
            foreach (string f in Directory.GetFiles(compDir, "*.cs", SearchOption.AllDirectories))
            {
                if (f.Substring(compDir.Length).Contains("\\_")) continue;
                if (Path.GetFileName(f).StartsWith("Plugin", StringComparison.OrdinalIgnoreCase)) continue;
                r.Add(f);
            }
            r.Sort(StringComparer.OrdinalIgnoreCase);
            return r;
        }

        public static List<Knob> Scan(string compDir, Action<string> log)
        {
            List<Knob> list = new List<Knob>();
            foreach (string f in CoreFiles(compDir))
            {
                string[] lines = SplitLines(ReadText(f));
                bool inside = false;
                string cls = null;
                for (int i = 0; i < lines.Length; i++)
                {
                    string ln = lines[i].TrimEnd('\r');
                    Match mc = ClassDecl.Match(ln);
                    if (mc.Success && !ln.TrimStart().StartsWith("//")) cls = mc.Groups[1].Value;
                    string t = ln.Trim();
                    if (t.StartsWith(Open)) { inside = true; continue; }
                    if (t.StartsWith(Close)) { inside = false; continue; }
                    if (!inside || t.Length == 0 || t.StartsWith("//")) continue;
                    Match m = Decl.Match(ln);
                    if (!m.Success)
                    {
                        log("Knobs: bo qua dong " + (i + 1) + " cua " + Path.GetFileName(f) + " (chi nhan 'static double|int Ten = so;')");
                        continue;
                    }
                    Knob k = new Knob();
                    k.File = f;
                    k.Line = i + 1;
                    k.Class = cls;
                    k.Name = m.Groups["name"].Value;
                    k.IsInt = m.Groups["type"].Value == "int";
                    k.Source = k.Value = double.Parse(m.Groups["val"].Value, CI);
                    string tail = m.Groups["tail"].Value;
                    int c = tail.IndexOf("//", StringComparison.Ordinal);
                    string comment = c >= 0 ? tail.Substring(c + 2) : "";
                    Match mr = Range.Match(comment);
                    if (mr.Success)
                    {
                        k.Min = double.Parse(mr.Groups["min"].Value, CI);
                        k.Max = double.Parse(mr.Groups["max"].Value, CI);
                        k.Step = mr.Groups["step"].Success ? double.Parse(mr.Groups["step"].Value, CI) : (k.IsInt ? 1 : 0.5);
                        k.HasRange = k.Max > k.Min && k.Step > 0;
                        k.Note = mr.Groups["note"].Value.Trim();
                    }
                    else
                    {
                        k.Note = comment.Trim();
                        k.Step = k.IsInt ? 1 : 0.5;
                    }
                    if (cls == null) { log("Knobs: " + k.Name + " khong nam trong lop nao -> bo qua"); continue; }
                    list.Add(k);
                }
            }
            return list;
        }

        /// Gán giá trị đang chỉnh vào trường static của Core đã nạp. Trả về số trường gán được.
        public static int Apply(IList<Knob> knobs, Assembly asm, Action<string> log)
        {
            int n = 0;
            Type[] types;
            try { types = asm.GetTypes(); }
            catch (ReflectionTypeLoadException ex) { types = ex.Types; }
            foreach (Knob k in knobs)
            {
                FieldInfo fi = null;
                foreach (Type t in types)
                {
                    if (t == null || t.Name != k.Class) continue;
                    fi = t.GetField(k.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    if (fi != null) break;
                }
                if (fi == null || fi.IsLiteral || fi.IsInitOnly)
                {
                    log("Knobs: khong gan duoc " + k.Class + "." + k.Name + " (Core chua build lai, hoac la const/readonly)");
                    continue;
                }
                try
                {
                    if (fi.FieldType == typeof(int)) fi.SetValue(null, (int)Math.Round(k.Value));
                    else fi.SetValue(null, k.Value);
                    n++;
                }
                catch (Exception ex) { log("Knobs: loi gan " + k.Name + ": " + ex.Message); }
            }
            return n;
        }

        /// Ghi các knob đã đổi vào .cs. Tìm lại dòng theo TÊN trong khối <KNOBS> (file có thể đã bị sửa từ lúc quét).
        public static int Save(IList<Knob> knobs, Action<string> log)
        {
            int saved = 0;
            Dictionary<string, List<Knob>> byFile = new Dictionary<string, List<Knob>>(StringComparer.OrdinalIgnoreCase);
            foreach (Knob k in knobs)
            {
                if (!k.Dirty) continue;
                List<Knob> l;
                if (!byFile.TryGetValue(k.File, out l)) byFile[k.File] = l = new List<Knob>();
                l.Add(k);
            }
            foreach (KeyValuePair<string, List<Knob>> kv in byFile)
            {
                Encoding enc;
                string text = ReadText(kv.Key, out enc);
                string[] lines = SplitLines(text);
                bool inside = false;
                Dictionary<string, Knob> want = new Dictionary<string, Knob>();
                foreach (Knob k in kv.Value) want[k.Name] = k;
                for (int i = 0; i < lines.Length; i++)
                {
                    bool cr = lines[i].EndsWith("\r");
                    string ln = cr ? lines[i].Substring(0, lines[i].Length - 1) : lines[i];
                    string t = ln.Trim();
                    if (t.StartsWith(Open)) { inside = true; continue; }
                    if (t.StartsWith(Close)) { inside = false; continue; }
                    if (!inside) continue;
                    Match m = Decl.Match(ln);
                    Knob k;
                    if (!m.Success || !want.TryGetValue(m.Groups["name"].Value, out k)) continue;
                    lines[i] = m.Groups["head"].Value + k.Format(k.Value) + m.Groups["tail"].Value + (cr ? "\r" : "");
                    log("Knobs: " + Path.GetFileName(kv.Key) + " dong " + (i + 1) + ": " + k.Name + " = " + k.Format(k.Source) + " -> " + k.Format(k.Value));
                    k.Source = k.Value;
                    want.Remove(k.Name);
                    saved++;
                }
                foreach (string miss in want.Keys)
                    log("Knobs: KHONG thay " + miss + " trong khoi <KNOBS> cua " + Path.GetFileName(kv.Key) + " (file da bi sua?) -> chua ghi");
                File.WriteAllText(kv.Key, string.Join("\n", lines), enc);
            }
            return saved;
        }

        private static string[] SplitLines(string text) { return text.Split('\n'); }

        private static string ReadText(string path)
        {
            Encoding enc;
            return ReadText(path, out enc);
        }

        /// Đọc UTF-8, nhớ có BOM hay không để ghi lại y như cũ.
        private static string ReadText(string path, out Encoding enc)
        {
            byte[] b = File.ReadAllBytes(path);
            bool bom = b.Length >= 3 && b[0] == 0xEF && b[1] == 0xBB && b[2] == 0xBF;
            enc = new UTF8Encoding(bom);
            return new UTF8Encoding(false).GetString(b, bom ? 3 : 0, b.Length - (bom ? 3 : 0));
        }
    }

    /// Giao diện tab Knobs.
    internal sealed class KnobPanel : Panel
    {
        private static readonly CultureInfo CI = CultureInfo.InvariantCulture;
        private readonly Action<string> _log;
        private readonly Action _rebuild;          // dựng lại trong Tekla (giữ cấu kiện đã chọn)
        private readonly Action _saveAndBuild;     // build Core sau khi ghi .cs
        private readonly TableLayoutPanel _table = new TableLayoutPanel();
        private readonly CheckBox _auto = new CheckBox();
        private readonly Label _info = new Label();
        private readonly Timer _debounce = new Timer();
        private List<Knob> _knobs = new List<Knob>();
        private string _compDir;
        private bool _filling;

        public KnobPanel(Action<string> log, Action rebuild, Action saveAndBuild)
        {
            _log = log;
            _rebuild = rebuild;
            _saveAndBuild = saveAndBuild;
            Dock = DockStyle.Fill;

            FlowLayoutPanel bar = new FlowLayoutPanel();
            bar.Dock = DockStyle.Top;
            bar.AutoSize = true;
            bar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bar.Padding = new Padding(2);
            AddButton(bar, "Ghi vao .cs + build Core", 170, delegate { OnSave(); });
            AddButton(bar, "Bo thay doi (lay lai tu .cs)", 170, delegate { Rescan(true); });
            AddButton(bar, "Dung lai ngay", 100, delegate { _rebuild(); });
            _auto.Text = "Tu dung lai khi doi so";
            _auto.AutoSize = true;
            _auto.Checked = true;
            _auto.Margin = new Padding(8, 7, 3, 3);
            bar.Controls.Add(_auto);

            _info.Dock = DockStyle.Top;
            _info.Height = 34;
            _info.ForeColor = Color.DimGray;

            _table.Dock = DockStyle.Top;
            _table.AutoSize = true;
            _table.ColumnCount = 4;
            _table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90f));
            _table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            Panel scroll = new Panel();
            scroll.Dock = DockStyle.Fill;
            scroll.AutoScroll = true;
            scroll.Controls.Add(_table);

            Controls.Add(scroll);
            Controls.Add(_info);
            Controls.Add(bar);

            _debounce.Interval = 350;
            _debounce.Tick += delegate
            {
                _debounce.Stop();
                if (_auto.Checked) _rebuild();
            };
        }

        public IList<Knob> Knobs { get { return _knobs; } }

        public bool Dirty
        {
            get { foreach (Knob k in _knobs) if (k.Dirty) return true; return false; }
        }

        /// Đổi component (hoặc quét lại). force = bỏ các thay đổi chưa ghi.
        public void LoadComponent(string compDir)
        {
            _compDir = compDir;
            Rescan(true);
        }

        /// Quét lại .cs. Không force và đang có thay đổi chưa ghi thì giữ nguyên (không mất số đang chỉnh).
        public void Rescan(bool force)
        {
            if (!force && Dirty) return;
            string stamp = Stamp();
            if (!force && stamp == _stamp) return;          // .cs không đổi -> giữ nguyên giao diện (đang kéo thanh trượt)
            _stamp = stamp;
            _knobs = _compDir == null ? new List<Knob>() : KnobSource.Scan(_compDir, _log);
            Fill();
        }

        private string _stamp;

        private string Stamp()
        {
            if (_compDir == null) return "";
            StringBuilder sb = new StringBuilder(_compDir);
            foreach (string f in KnobSource.CoreFiles(_compDir)) sb.Append('|').Append(File.GetLastWriteTimeUtc(f).Ticks);
            return sb.ToString();
        }

        private static void AddButton(Control parent, string text, int width, EventHandler click)
        {
            Button b = new Button();
            b.Text = text;
            b.Width = width;
            b.Height = 26;
            b.Click += click;
            parent.Controls.Add(b);
        }

        private void Fill()
        {
            _filling = true;
            _table.SuspendLayout();
            _table.Controls.Clear();
            _table.RowStyles.Clear();
            _table.RowCount = 0;
            if (_knobs.Count == 0)
            {
                _info.Text = "Component nay chua co khoi // <KNOBS> ... // </KNOBS> trong .cs (Builder.cs...).\n" +
                             "Vd:  internal static double BevelExtra = 3.0;  // [0..20, 0.5] mo ta   (xem dau _Tester\\KnobPanel.cs)";
            }
            else
            {
                _info.Text = _knobs.Count + " knob. Keo/go so -> gan vao Core dang nap va dung lai (khong bien dich). " +
                             "O vang = khac gia tri trong .cs.\nBam 'Ghi vao .cs + build Core' de luu that (chi sua dung con so tren dong do).";
            }
            foreach (Knob k in _knobs) AddRow(k);
            _table.ResumeLayout();
            _filling = false;
        }

        private void AddRow(Knob k)
        {
            int row = _table.RowCount++;
            _table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            Label name = new Label();
            name.AutoSize = true;
            name.Text = k.Name + (string.IsNullOrEmpty(k.Note) ? "" : "\n" + k.Note);
            name.Margin = new Padding(3, 4, 6, 4);
            ToolTip tip = new ToolTip();
            tip.SetToolTip(name, k.Class + "." + k.Name + "  (" + Path.GetFileName(k.File) + " dong " + k.Line + ")");

            NumericUpDown num = new NumericUpDown();
            num.Width = 84;
            num.DecimalPlaces = k.IsInt ? 0 : Decimals(k.Step);
            num.Increment = (decimal)k.Step;
            num.Minimum = (decimal)Math.Min(k.HasRange ? k.Min : -1e6, k.Value);
            num.Maximum = (decimal)Math.Max(k.HasRange ? k.Max : 1e6, k.Value);
            num.Value = (decimal)k.Value;

            TrackBar tb = null;
            if (k.HasRange)
            {
                tb = new TrackBar();
                tb.Dock = DockStyle.Fill;
                tb.AutoSize = false;
                tb.Height = 30;
                tb.TickStyle = TickStyle.None;
                int ticks = (int)Math.Min(2000, Math.Round((k.Max - k.Min) / k.Step));
                tb.Minimum = 0;
                tb.Maximum = Math.Max(1, ticks);
                tb.Value = Clamp(TickOf(k, k.Value), tb.Maximum);
            }

            Label src = new Label();
            src.AutoSize = true;
            src.Margin = new Padding(3, 6, 3, 3);
            src.ForeColor = Color.DimGray;

            Action mark = delegate
            {
                num.BackColor = k.Dirty ? Color.LightYellow : SystemColors.Window;
                src.Text = k.Dirty ? "(.cs: " + k.Format(k.Source) + ")" : "";
            };
            mark();

            num.ValueChanged += delegate
            {
                if (_filling) return;
                k.Value = (double)num.Value;
                if (tb != null) { _filling = true; tb.Value = Clamp(TickOf(k, k.Value), tb.Maximum); _filling = false; }
                mark();
                Changed();
            };
            if (tb != null)
            {
                tb.ValueChanged += delegate
                {
                    if (_filling) return;
                    double v = k.Min + tb.Value * k.Step;
                    if (v > k.Max) v = k.Max;
                    if (k.IsInt) v = Math.Round(v);
                    k.Value = v;
                    _filling = true; num.Value = Math.Max(num.Minimum, Math.Min(num.Maximum, (decimal)v)); _filling = false;
                    mark();
                    Changed();
                };
            }

            _table.Controls.Add(name, 0, row);
            if (tb != null) _table.Controls.Add(tb, 1, row);
            _table.Controls.Add(num, 2, row);
            _table.Controls.Add(src, 3, row);
        }

        private void Changed()
        {
            _debounce.Stop();
            _debounce.Start();
        }

        private void OnSave()
        {
            if (!Dirty) { _log("Knobs: khong co gi thay doi."); return; }
            try
            {
                int n = KnobSource.Save(_knobs, _log);
                _log("Knobs: da ghi " + n + " gia tri vao .cs.");
                Fill();
                _saveAndBuild();
            }
            catch (Exception ex) { _log("Knobs: loi ghi .cs: " + ex.Message); }
        }

        private static int TickOf(Knob k, double v) { return (int)Math.Round((v - k.Min) / k.Step); }
        private static int Clamp(int v, int max) { return v < 0 ? 0 : v > max ? max : v; }

        private static int Decimals(double step)
        {
            string s = step.ToString("R", CI);
            int dot = s.IndexOf('.');
            return dot < 0 ? 1 : Math.Min(4, s.Length - dot - 1);
        }
    }
}
