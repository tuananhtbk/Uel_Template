// Cửa sổ kết quả so sánh 2 component (xem ComponentCompare.cs). Kết quả cũng được lưu thành file .txt trong _Dev\SoSanh\.
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace UEL.Tester
{
    internal sealed class CompareForm : Form
    {
        private readonly CmpResult _r;
        private readonly string _file;
        private readonly Label _info = new Label();

        public CompareForm(CmpResult r)
        {
            _r = r;
            _file = Save(r);
            Text = "So sanh: " + r.NameA + "  (A)   vs   " + r.NameB + "  (B)";
            Size = new Size(980, 720);
            StartPosition = FormStartPosition.CenterScreen;

            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.Multiline = true;
            tb.ReadOnly = true;
            tb.ScrollBars = ScrollBars.Both;
            tb.WordWrap = false;
            tb.Font = new Font("Consolas", 9.5f);
            tb.Text = r.Report;

            FlowLayoutPanel bottom = new FlowLayoutPanel();
            bottom.Dock = DockStyle.Bottom;
            bottom.Height = 40;
            bottom.Padding = new Padding(4);
            AddButton(bottom, "Chon trong Tekla: doi tuong lech / thieu / thua", OnSelectDiff, 300);
            AddButton(bottom, "Mo file ket qua", OnOpenFile, 120);
            AddButton(bottom, "Dong", delegate { Close(); }, 80);
            _info.AutoSize = true;
            _info.Padding = new Padding(6, 8, 0, 0);
            _info.ForeColor = Color.DimGray;
            _info.Text = _file == null ? "(khong luu duoc file)" : _file;
            bottom.Controls.Add(_info);

            Controls.Add(tb);
            Controls.Add(bottom);
            Shown += delegate { tb.SelectionLength = 0; };
        }

        private static void AddButton(Control parent, string text, EventHandler click, int width)
        {
            Button b = new Button();
            b.Text = text;
            b.Width = width;
            b.Height = 28;
            b.Click += click;
            parent.Controls.Add(b);
        }

        private static string Save(CmpResult r)
        {
            try
            {
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SoSanh");
                Directory.CreateDirectory(dir);
                string name = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + Clean(r.NameA) + "_vs_" + Clean(r.NameB) + ".txt";
                string path = Path.Combine(dir, name);
                File.WriteAllText(path, r.Report, new UTF8Encoding(true));
                return path;
            }
            catch
            {
                return null;
            }
        }

        private static string Clean(string s)
        {
            if (string.IsNullOrEmpty(s)) return "_";
            foreach (char c in Path.GetInvalidFileNameChars()) s = s.Replace(c, '_');
            return s.Replace(' ', '_');
        }

        private void OnSelectDiff(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            foreach (CmpPair p in _r.Diff) { list.Add(p.A.Obj); list.Add(p.B.Obj); }
            foreach (CmpItem it in _r.OnlyA) list.Add(it.Obj);
            foreach (CmpItem it in _r.OnlyB) list.Add(it.Obj);
            if (list.Count == 0)
            {
                _info.Text = "Khong co doi tuong nao khac nhau.";
                return;
            }
            try
            {
                new ModelObjectSelector().Select(list);
                _info.Text = "Da chon " + list.Count + " doi tuong trong Tekla.";
            }
            catch (Exception ex)
            {
                _info.Text = "Khong chon duoc: " + ex.Message;
            }
        }

        private void OnOpenFile(object sender, EventArgs e)
        {
            if (_file == null) return;
            try { Process.Start("notepad.exe", "\"" + _file + "\""); }
            catch (Exception ex) { _info.Text = ex.Message; }
        }
    }
}
