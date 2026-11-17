// Hộp thoại giống bản DLL trong Tekla: vẽ lại từ chính phần .inp (UserInterface trong Plugin.cs) của component,
// đúng tab, nhãn, vị trí từng ô.
//
// Giống Tekla: ô ĐỂ TRỐNG = dùng giá trị mặc định (chữ xám trong ô là giá trị đó, lấy từ bảng thông số).
// Khi dựng, các ô đã nhập được gom thành bảng tên -> giá trị (int / double / string, giống trường [StructuresField])
// rồi đưa qua Core.Apply -> AfterApply, y như bản DLL làm khi Tekla chạy component.
//
// Chưa có: ảnh minh hoạ (chỉ vẽ khung), các nút chọn catalog profile / material / bolt của Tekla (gõ tay),
// lưu / nạp bộ thuộc tính.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UEL.Tester
{
    internal sealed class InpView : UserControl
    {
        private sealed class Field
        {
            public string Name;
            public string Type;
            public string Kind;            // "int" | "double" | "string"
            public TextBox Text;
            public ComboBox Combo;         // ô option
            public List<string> Options;
            public bool Known;
        }

        private const int RowH = 30;
        private const int RowLabelX = 10;
        private const int RowFieldX = 210;
        private const int RowFieldW = 160;

        private readonly List<Field> _fields = new List<Field>();
        private readonly ToolTip _tip = new ToolTip();
        private readonly Action<string> _log;
        private readonly List<string> _notes = new List<string>();

        public InpView(string inp, object prm, IDictionary<string, string> restore, Action<string> log)
        {
            _log = log;
            Dock = DockStyle.Fill;
            TabControl tabs = new TabControl();
            tabs.Dock = DockStyle.Fill;
            Controls.Add(tabs);

            List<InpNode> top = InpParser.Parse(inp);
            InpNode root = InpParser.Find(top, "joint", "detail");
            if (root == null)
            {
                Label l = new Label();
                l.Text = "Khong doc duoc hop thoai (.inp khong co joint/detail).";
                l.AutoSize = true;
                l.Location = new Point(10, 10);
                Controls.Add(l);
                l.BringToFront();
                return;
            }

            List<InpNode> pages = new List<InpNode>();
            foreach (InpNode n in root.Children) if (n.Name == "tab_page") pages.Add(n);
            if (pages.Count == 0)
            {
                InpNode p = new InpNode();
                p.Name = "tab_page";
                p.Args.Add("");
                p.Args.Add("Parameters");
                p.Children = root.Children;
                pages.Add(p);
            }
            else
            {
                // tab_page("", "Ten", thu_tu)
                List<InpNode> sorted = new List<InpNode>(pages);
                sorted.Sort(delegate(InpNode a, InpNode b)
                {
                    int c = a.IntArg(2, 0).CompareTo(b.IntArg(2, 0));
                    return c != 0 ? c : pages.IndexOf(a).CompareTo(pages.IndexOf(b));
                });
                pages = sorted;
            }

            foreach (InpNode p in pages)
            {
                string title = p.Arg(1).Trim();
                TabPage tp = new TabPage(title.Length > 0 ? title : "Tab " + (tabs.TabPages.Count + 1));
                tp.AutoScroll = true;
                tp.BackColor = SystemColors.Control;
                LayoutItems(p.Children, tp);
                tabs.TabPages.Add(tp);
            }

            RefreshDefaults(prm);
            if (restore != null) Restore(restore);
            foreach (string n in _notes) _log("Hop thoai: " + n);
        }

        // ---------------------------------------------------------------- vẽ

        private void LayoutItems(List<InpNode> items, Control host)
        {
            List<Control> pictures = new List<Control>();
            foreach (InpNode n in items)
            {
                switch (n.Name)
                {
                    case "attribute":
                        if (n.Arg(2) == "label") AddLabel(n, host);
                        else if (n.Arg(2) == "option") AddOption(n, host);
                        else _notes.Add("chua ve: " + n);
                        break;
                    case "parameter":
                        AddParameter(n, host);
                        break;
                    case "picture":
                        pictures.Add(AddPicture(n, host));
                        break;
                    case "helpurl":
                    case "macro":
                        break;
                    default:
                        _notes.Add("chua ve: " + n);
                        break;
                }
            }
            foreach (Control c in pictures) c.SendToBack();
        }

        private static int RowY(int row)
        {
            return 10 + (Math.Max(row, 1) - 1) * RowH;
        }

        private static void AddText(Control host, string text, int x, int y)
        {
            if (string.IsNullOrEmpty(text)) return;
            Label l = new Label();
            l.Text = text;
            l.AutoSize = true;
            l.Location = new Point(x, y + 3);
            host.Controls.Add(l);
        }

        /// attribute("", "Nhan", label, "%s", none, none, "0", "0", x, y)   hoặc   ..., hang)
        private static void AddLabel(InpNode n, Control host)
        {
            if (n.Args.Count >= 10) AddText(host, n.Arg(1), n.IntArg(8, 0), n.IntArg(9, 0));
            else AddText(host, n.Arg(1), RowLabelX, RowY(n.IntArg(8, 1)));
        }

        /// parameter("Nhan", "P1", distance, number, x, y, rong)   hoặc   ..., hang)
        private void AddParameter(InpNode n, Control host)
        {
            Field f = new Field();
            f.Name = n.Arg(1);
            f.Type = n.Arg(2);
            f.Kind = KindOf(f.Type, n.Arg(3));
            TextBox tb = new TextBox();
            f.Text = tb;
            if (n.Args.Count >= 7)
            {
                tb.Location = new Point(n.IntArg(4, 0), n.IntArg(5, 0));
                tb.Width = n.IntArg(6, RowFieldW);
                if (n.Arg(0).Length > 0) AddLabelLeftOf(host, n.Arg(0), tb.Location);
            }
            else
            {
                int y = RowY(n.IntArg(4, 1));
                AddText(host, n.Arg(0), RowLabelX, y);
                tb.Location = new Point(RowFieldX, y);
                tb.Width = RowFieldW;
            }
            tb.HandleCreated += delegate { SetCue(tb); };
            tb.TextChanged += delegate { tb.BackColor = f.Known ? SystemColors.Window : Color.LightYellow; };
            host.Controls.Add(tb);
            _fields.Add(f);
        }

        /// attribute("P13", "Nhan", option, "%s", none, none, "0.0", "0.0", x, y, rong) { value("A", 1) value("B", 0) }
        private void AddOption(InpNode n, Control host)
        {
            Field f = new Field();
            f.Name = n.Arg(0);
            f.Type = "option";
            f.Kind = "int";
            f.Options = new List<string>();
            foreach (InpNode v in n.Children) if (v.Name == "value") f.Options.Add(v.Arg(0));
            ComboBox cb = new ComboBox();
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            f.Combo = cb;
            if (n.Args.Count >= 11)
            {
                cb.Location = new Point(n.IntArg(8, 0), n.IntArg(9, 0));
                cb.Width = n.IntArg(10, RowFieldW);
                if (n.Arg(1).Length > 0) AddLabelLeftOf(host, n.Arg(1), cb.Location);
            }
            else
            {
                int y = RowY(n.IntArg(8, 1));
                AddText(host, n.Arg(1), RowLabelX, y);
                cb.Location = new Point(RowFieldX, y);
                cb.Width = RowFieldW;
            }
            host.Controls.Add(cb);
            _fields.Add(f);
        }

        private static void AddLabelLeftOf(Control host, string text, Point field)
        {
            Label l = new Label();
            l.Text = text;
            l.AutoSize = true;
            host.Controls.Add(l);
            l.Location = new Point(Math.Max(0, field.X - l.PreferredWidth - 6), field.Y + 3);
        }

        /// picture("ten", rong, cao, x, y): file ảnh không có -> chỉ vẽ khung + tên.
        private static Control AddPicture(InpNode n, Control host)
        {
            Label p = new Label();
            p.BorderStyle = BorderStyle.FixedSingle;
            p.ForeColor = Color.Gray;
            p.TextAlign = ContentAlignment.MiddleCenter;
            p.Text = "[anh " + n.Arg(0) + "]";
            p.Size = new Size(Math.Max(n.IntArg(1, 50), 10), Math.Max(n.IntArg(2, 20), 10));
            p.Location = new Point(n.IntArg(3, 0), n.IntArg(4, 0));
            host.Controls.Add(p);
            return p;
        }

        /// Kiểu giá trị giống trường [StructuresField] của bản DLL.
        private static string KindOf(string type, string format)
        {
            if (format == "text") return "string";
            switch (type)
            {
                case "integer":
                case "chamfer_type":
                case "weld_type":
                case "welding_site":
                case "bolt_type":
                    return "int";
                default:
                    return "double";
            }
        }

        // ---------------------------------------------------------------- chữ xám = giá trị mặc định

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        private static void SetCue(TextBox tb)
        {
            if (tb.IsHandleCreated) SendMessage(tb.Handle, EM_SETCUEBANNER, (IntPtr)1, tb.Tag as string ?? "");
        }

        /// Map tên biến (P1...) -> thuộc tính có [UelParam] trong lớp Params (so theo tên lớp attribute,
        /// vì mỗi lần nạp lại Core là một kiểu khác).
        private static Dictionary<string, PropertyInfo> ParamProps(object prm)
        {
            Dictionary<string, PropertyInfo> map = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            if (prm == null) return map;
            foreach (PropertyInfo pi in prm.GetType().GetProperties())
                foreach (object a in pi.GetCustomAttributes(true))
                {
                    if (a.GetType().Name != "UelParamAttribute") continue;
                    PropertyInfo np = a.GetType().GetProperty("Name");
                    string name = np == null ? null : np.GetValue(a, null) as string;
                    if (!string.IsNullOrEmpty(name)) map[name] = pi;
                }
            return map;
        }

        private static string Show(object v)
        {
            if (v == null) return "";
            if (v is bool) return (bool)v ? "1" : "0";
            if (v is double) return ((double)v).ToString("0.###", CultureInfo.InvariantCulture);
            return Convert.ToString(v, CultureInfo.InvariantCulture);
        }

        /// Cập nhật chữ xám (giá trị dùng khi ô để trống) theo bảng thông số hiện tại.
        public void RefreshDefaults(object prm)
        {
            Dictionary<string, PropertyInfo> map = ParamProps(prm);
            Dictionary<string, bool> used = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            foreach (Field f in _fields)
            {
                PropertyInfo pi;
                f.Known = map.TryGetValue(f.Name, out pi);
                used[f.Name] = true;
                object v = f.Known ? pi.GetValue(prm, null) : null;
                string tip = f.Name + "  (" + f.Type + ", " + f.Kind + ")";
                if (!f.Known) tip += "\nKHONG co trong Params.cs ([UelParam(\"" + f.Name + "\")]) -> gia tri bi bo qua";

                if (f.Combo != null)
                {
                    int sel = f.Combo.SelectedIndex;
                    f.Combo.Items.Clear();
                    f.Combo.Items.Add("(mac dinh) " + OptionText(f, v));
                    for (int i = 0; i < f.Options.Count; i++) f.Combo.Items.Add(i + ": " + f.Options[i]);
                    f.Combo.SelectedIndex = sel >= 0 && sel < f.Combo.Items.Count ? sel : 0;
                    f.Combo.BackColor = f.Known ? SystemColors.Window : Color.LightYellow;
                    _tip.SetToolTip(f.Combo, tip);
                }
                else
                {
                    f.Text.Tag = Show(v);
                    SetCue(f.Text);
                    f.Text.BackColor = f.Known ? SystemColors.Window : Color.LightYellow;
                    _tip.SetToolTip(f.Text, tip);
                }
            }
            foreach (KeyValuePair<string, PropertyInfo> kv in map)
                if (!used.ContainsKey(kv.Key) && !_notes.Contains("Params co " + kv.Key + " nhung hop thoai khong co"))
                    _notes.Add("Params co " + kv.Key + " nhung hop thoai khong co");
        }

        private static string OptionText(Field f, object v)
        {
            if (v == null) return "";
            int i;
            if (v is bool) i = (bool)v ? 1 : 0;
            else
            {
                double d;
                if (!double.TryParse(Show(v), NumberStyles.Float, CultureInfo.InvariantCulture, out d)) return Show(v);
                i = (int)Math.Round(d);
            }
            return i >= 0 && i < f.Options.Count ? i + ": " + f.Options[i] : i.ToString(CultureInfo.InvariantCulture);
        }

        // ---------------------------------------------------------------- giá trị

        /// Các ô đang nhập (để giữ lại khi vẽ lại hộp thoại). Option lưu dạng "#chi_so".
        public Dictionary<string, string> Snapshot()
        {
            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (Field f in _fields)
            {
                if (f.Combo != null) { if (f.Combo.SelectedIndex > 0) r[f.Name] = "#" + (f.Combo.SelectedIndex - 1); }
                else if (f.Text.Text.Trim().Length > 0) r[f.Name] = f.Text.Text;
            }
            return r;
        }

        private void Restore(IDictionary<string, string> values)
        {
            foreach (Field f in _fields)
            {
                string v;
                if (!values.TryGetValue(f.Name, out v)) continue;
                if (f.Combo != null)
                {
                    int i;
                    if (v.StartsWith("#") && int.TryParse(v.Substring(1), out i) && i + 1 < f.Combo.Items.Count) f.Combo.SelectedIndex = i + 1;
                }
                else f.Text.Text = v;
            }
        }

        public void ClearAll()
        {
            foreach (Field f in _fields)
            {
                if (f.Combo != null) { if (f.Combo.Items.Count > 0) f.Combo.SelectedIndex = 0; }
                else f.Text.Text = "";
            }
        }

        public int FilledCount
        {
            get { return Snapshot().Count; }
        }

        /// Kích thước cần để thấy hết mọi ô của tab lớn nhất (không cuộn), chưa tính tiêu đề tab.
        public Size ContentSize
        {
            get
            {
                int w = 0, h = 0;
                foreach (Control tabs in Controls)
                {
                    TabControl tc = tabs as TabControl;
                    if (tc == null) continue;
                    foreach (TabPage p in tc.TabPages)
                        foreach (Control c in p.Controls)
                        {
                            w = Math.Max(w, c.Right);
                            h = Math.Max(h, c.Bottom);
                        }
                }
                return new Size(w + 20, h + 20);
            }
        }

        /// Gom các ô đã nhập thành tên -> giá trị đúng kiểu. Ô sai định dạng: tô đỏ, ghi vào errors.
        public Hashtable Collect(List<string> errors)
        {
            Hashtable values = new Hashtable();
            foreach (Field f in _fields)
            {
                if (f.Combo != null)
                {
                    if (f.Combo.SelectedIndex > 0) values[f.Name] = f.Combo.SelectedIndex - 1;
                    continue;
                }
                string s = f.Text.Text.Trim();
                f.Text.BackColor = f.Known ? SystemColors.Window : Color.LightYellow;
                if (s.Length == 0) continue;
                if (f.Kind == "string")
                {
                    values[f.Name] = s;
                    continue;
                }
                double d;
                if (!double.TryParse(s.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out d))
                {
                    f.Text.BackColor = Color.MistyRose;
                    errors.Add(f.Name + " = \"" + s + "\" khong phai so");
                    continue;
                }
                if (f.Kind == "int") values[f.Name] = (int)Math.Round(d);
                else values[f.Name] = d;
            }
            return values;
        }
    }
}
