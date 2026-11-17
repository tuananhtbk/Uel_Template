// Vẽ hình minh hoạ cho hộp thoại RAKER+T_GIRDER và sinh phần .inp tương ứng (ô điền đặt đúng chỗ trên hình).
// Thư mục bắt đầu bằng "_" nên build.bat không đưa file này vào plugin.
//
// Chạy (ra bitmaps\*.bmp và _hinh\dialog.inp):
//   csc /out:VeHinh.exe /reference:System.Drawing.dll VeHinh.cs  &&  VeHinh.exe "<thư mục RAKER+T_GIRDER>"
// Sau đó chép nội dung dialog.inp vào hằng UserInterface trong Plugin.cs (nhân đôi dấu ", thay tên joint).
//
// Mỗi ô điền là 1 "slot": khung trắng vẽ sẵn trên hình + 1 dòng parameter(...) cùng toạ độ trong .inp.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

internal static class VeHinh
{
    private const int SlotW = 64, SlotH = 22;

    private static readonly Color Bg = Color.FromArgb(240, 240, 240);
    private static readonly Color Green = Color.FromArgb(40, 190, 70);
    private static readonly Color Blue = Color.FromArgb(40, 70, 205);
    private static readonly Color Steel = Color.FromArgb(150, 150, 155);
    private static readonly Color SteelDark = Color.FromArgb(105, 105, 110);
    private static readonly Color Web = Color.FromArgb(185, 185, 190);
    private static readonly Color Cyan = Color.FromArgb(0, 200, 220);
    private static readonly Color Bolt = Color.FromArgb(205, 140, 30);
    private static readonly Color Dim = Color.FromArgb(200, 30, 30);

    private sealed class Field
    {
        public string Name, Type, Format;
        public int X, Y, W;
    }

    private sealed class Page
    {
        public string Title, Bitmap;
        public int PicX = 10, PicY = 10, PicW, PicH;
        public readonly List<Field> Fields = new List<Field>();
        public readonly List<string> Extra = new List<string>();   // label / field ngoài hình (toạ độ tab)
    }

    private static Graphics _g;
    private static Page _page;
    private static readonly Font F = new Font("Arial", 8.5f);
    private static readonly Font FB = new Font("Arial", 8.5f, FontStyle.Bold);

    private static int Main(string[] args)
    {
        string dir = args.Length > 0 ? args[0] : ".";
        string bmpDir = Path.Combine(dir, "bitmaps");
        Directory.CreateDirectory(bmpDir);
        List<Page> pages = new List<Page>();
        pages.Add(BasePlate(bmpDir));
        pages.Add(Gusset(bmpDir));
        pages.Add(Flange(bmpDir));
        File.WriteAllText(Path.Combine(Path.Combine(dir, "_hinh"), "dialog.inp"), Inp(pages), new UTF8Encoding(false));
        Console.WriteLine("Xong: " + pages.Count + " hinh + _hinh\\dialog.inp");
        return 0;
    }

    // ------------------------------------------------------------------ tiện ích vẽ

    private static Bitmap Begin(Page p, int w, int h)
    {
        p.PicW = w; p.PicH = h;
        Bitmap bmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);
        _g = Graphics.FromImage(bmp);
        _g.SmoothingMode = SmoothingMode.AntiAlias;
        _g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        _g.Clear(Bg);
        _page = p;
        return bmp;
    }

    private static void End(Bitmap bmp, string file)
    {
        _g.Dispose();
        bmp.Save(file, ImageFormat.Bmp);
        bmp.Dispose();
    }

    private static void Rect(Color fill, int x, int y, int w, int h)
    {
        using (SolidBrush b = new SolidBrush(fill)) _g.FillRectangle(b, x, y, w, h);
        _g.DrawRectangle(Pens.Black, x, y, w, h);
    }

    private static void Poly(Color fill, params int[] xy)
    {
        Point[] pts = new Point[xy.Length / 2];
        for (int i = 0; i < pts.Length; i++) pts[i] = new Point(xy[2 * i], xy[2 * i + 1]);
        using (SolidBrush b = new SolidBrush(fill)) _g.FillPolygon(b, pts);
        _g.DrawPolygon(Pens.Black, pts);
    }

    private static void Text(string s, int x, int y, bool bold = false, Color? c = null)
    {
        using (SolidBrush b = new SolidBrush(c ?? Color.Black)) _g.DrawString(s, bold ? FB : F, b, x, y);
    }

    private static void TextR(string s, int xRight, int y)
    {
        SizeF sz = _g.MeasureString(s, F);
        _g.DrawString(s, F, Brushes.Black, xRight - sz.Width, y);
    }

    private static void BoltAt(int x, int y)
    {
        using (SolidBrush b = new SolidBrush(Bolt)) _g.FillEllipse(b, x - 8, y - 8, 16, 16);
        _g.DrawEllipse(Pens.Black, x - 8, y - 8, 16, 16);
        _g.DrawLine(Pens.Black, x - 4, y, x + 4, y);
        _g.DrawLine(Pens.Black, x, y - 4, x, y + 4);
    }

    private static void Arrow(int x, int y, int dx, int dy, Pen pen)
    {
        double l = Math.Sqrt(dx * dx + dy * dy);
        if (l < 1) return;
        double ux = dx / l, uy = dy / l;
        PointF tip = new PointF(x, y);
        PointF a = new PointF((float)(x - ux * 8 - uy * 3.5), (float)(y - uy * 8 + ux * 3.5));
        PointF b = new PointF((float)(x - ux * 8 + uy * 3.5), (float)(y - uy * 8 - ux * 3.5));
        using (SolidBrush br = new SolidBrush(pen.Color)) _g.FillPolygon(br, new[] { tip, a, b });
    }

    /// Đường kích thước ngang tại cao độ y, từ x1 tới x2, kèm đường gióng (từ ext1/ext2 tới y).
    private static void DimH(int x1, int x2, int y, int ext1, int ext2)
    {
        using (Pen p = new Pen(Dim, 1f))
        {
            using (Pen d = new Pen(Dim, 1f) { DashStyle = DashStyle.Dash })
            {
                _g.DrawLine(d, x1, ext1, x1, y + 4 * Math.Sign(y - ext1));
                _g.DrawLine(d, x2, ext2, x2, y + 4 * Math.Sign(y - ext2));
            }
            _g.DrawLine(p, x1, y, x2, y);
            Arrow(x1, y, x1 - x2, 0, p);
            Arrow(x2, y, x2 - x1, 0, p);
        }
    }

    /// Đường kích thước đứng tại hoành độ x, từ y1 tới y2, kèm đường gióng.
    private static void DimV(int y1, int y2, int x, int ext1, int ext2)
    {
        using (Pen p = new Pen(Dim, 1f))
        {
            using (Pen d = new Pen(Dim, 1f) { DashStyle = DashStyle.Dash })
            {
                _g.DrawLine(d, ext1, y1, x + 4 * Math.Sign(x - ext1), y1);
                _g.DrawLine(d, ext2, y2, x + 4 * Math.Sign(x - ext2), y2);
            }
            _g.DrawLine(p, x, y1, x, y2);
            Arrow(x, y1, 0, y1 - y2, p);
            Arrow(x, y2, 0, y2 - y1, p);
        }
    }

    private static void Leader(int x1, int y1, int x2, int y2)
    {
        using (Pen p = new Pen(Dim, 1f)) { _g.DrawLine(p, x1, y1, x2, y2); Arrow(x2, y2, x2 - x1, y2 - y1, p); }
    }

    /// Ô điền trên hình: khung trắng + nhãn bên trái hoặc phía trên. Ghi lại để sinh .inp.
    private static void Slot(string name, string type, string fmt, int x, int y, string label, string where = "left", int w = SlotW)
    {
        _g.FillRectangle(Brushes.White, x, y, w, SlotH);
        _g.DrawRectangle(Pens.Gray, x, y, w, SlotH);
        if (label != null)
        {
            if (where == "left") TextR(label, x - 3, y + 4);
            else if (where == "right") Text(label, x + w + 3, y + 4);
            else Text(label, x, y - 15);
        }
        Field f = new Field();
        f.Name = name; f.Type = type; f.Format = fmt; f.X = x; f.Y = y; f.W = w;
        _page.Fields.Add(f);
    }

    // ------------------------------------------------------------------ hình 1: bản đế

    private static Page BasePlate(string dir)
    {
        Page p = new Page();
        p.Title = "Base Plate";
        p.Bitmap = "UEL_RAKER_TG_BP";
        Bitmap bmp = Begin(p, 600, 400);

        // dầm chính (primary) chạy ngang, cao từ 120 đến 290
        Rect(Steel, 15, 120, 530, 170);
        Rect(SteelDark, 15, 120, 530, 14);
        Rect(SteelDark, 15, 276, 530, 14);

        // bản đế
        Rect(Green, 160, 90, 250, 230);

        // dấu chân raker (điểm xa nhất) - tiết diện H
        Poly(Web, 214, 150, 356, 150, 356, 163, 290, 163, 290, 247, 356, 247, 356, 260, 214, 260, 214, 247, 280, 247, 280, 163, 214, 163);

        // Top / Bott: từ mép dầm chính
        DimV(90, 120, 432, 410, 545);
        DimV(290, 320, 432, 545, 410);
        Slot("P6", "distance", "number", 448, 92, "Top gap", "right");
        Slot("P8", "distance", "number", 448, 294, "Bott. gap", "right");

        // Left / Right: từ điểm xa nhất của raker
        DimH(160, 214, 70, 90, 150);
        DimH(356, 410, 70, 150, 90);
        Slot("P7", "distance", "number", 60, 40, "Left gap", "top");
        Leader(124, 52, 185, 66);
        Slot("P19", "distance", "number", 436, 40, "Right gap", "top");
        Leader(436, 52, 385, 66);

        End(bmp, Path.Combine(dir, p.Bitmap + ".bmp"));

        // thông số tấm (ngoài hình)
        int x = p.PicX + p.PicW + 30, y = p.PicY + 20;
        p.Extra.Add(Label("Base Plate Thk.", x, y)); p.Extra.Add(Param("P1", "distance", "number", x + 120, y, 120));
        p.Extra.Add(Label("Mat.", x, y + 35)); p.Extra.Add(Param("P2", "material", "text", x + 120, y + 35, 120));
        p.Extra.Add(Label("Class", x, y + 70)); p.Extra.Add(Param("P3", "integer", "number", x + 120, y + 70, 120));
        p.Extra.Add(Label("Mark", x, y + 105)); p.Extra.Add(Param("P4", "string", "text", x + 120, y + 105, 120));
        return p;
    }

    // ------------------------------------------------------------------ hình 2: bản mã + bu lông

    private static Page Gusset(string dir)
    {
        Page p = new Page();
        p.Title = "Gusset & Bolt";
        p.Bitmap = "UEL_RAKER_TG_GP";
        Bitmap bmp = Begin(p, 700, 440);

        // bản đế (đứng) + raker
        Rect(Green, 40, 25, 20, 380);
        Rect(Web, 112, 72, 578, 288);                              // bụng (lùi P46)
        using (SolidBrush b = new SolidBrush(Bg)) { _g.FillPie(b, 98, 58, 28, 28, 0, 90); _g.FillPie(b, 98, 346, 28, 28, 270, 90); }
        Rect(Steel, 70, 58, 620, 14);                              // cánh trên
        Rect(Steel, 70, 360, 620, 14);                             // cánh dưới

        // bản mã + bu lông 3 cột x 3 hàng
        Rect(Blue, 60, 110, 410, 210);
        int[] cx = { 232, 312, 392 }, cy = { 150, 215, 280 };
        foreach (int a in cx) foreach (int b in cy) BoltAt(a, b);

        // kích thước dọc trục raker (dưới cánh dưới)
        DimH(60, 112, 392, 405, 360);
        DimH(112, 232, 392, 360, 280);
        DimH(232, 312, 392, 280, 280);
        DimH(392, 470, 392, 280, 320);
        Slot("P46", "distance", "number", 50, 405, null);
        Text("Web gap", 64, 378);
        Slot("P29", "distance", "number", 140, 405, null);
        Text("Bolt to web", 136, 378);
        Slot("P32", "distance", "number", 240, 405, null);
        Text("Bolt dist. x", 238, 378);
        Slot("P44", "distance", "number", 400, 405, null);
        Text("eH", 425, 378);

        // kích thước theo phương vuông góc cạnh dài (bên phải bản mã)
        DimV(110, 150, 488, 470, 400);
        DimV(150, 215, 488, 400, 400);
        Slot("P45", "distance", "number", 500, 119, "eV", "right");
        Slot("P43", "distance", "number", 500, 172, "Bolt dist. y", "right");
        Slot("P34", "integer", "number", 500, 225, "Rows", "right");
        Slot("P31", "integer", "number", 500, 260, "Cols", "right");

        End(bmp, Path.Combine(dir, p.Bitmap + ".bmp"));

        int x = p.PicX + p.PicW + 30, y = p.PicY + 10;
        p.Extra.Add(Label("Conn. type", x, y)); p.Extra.Add(Param("P13", "integer", "number", x + 140, y, 110));
        y += 45;
        p.Extra.Add(Label("Gusset Thk.", x, y)); p.Extra.Add(Param("P20", "distance", "number", x + 140, y, 110)); y += 32;
        p.Extra.Add(Label("Matt", x, y)); p.Extra.Add(Param("P22", "material", "text", x + 140, y, 110)); y += 32;
        p.Extra.Add(Label("Mark", x, y)); p.Extra.Add(Param("P21", "string", "text", x + 140, y, 110)); y += 32;
        p.Extra.Add(Label("Class", x, y)); p.Extra.Add(Param("P23", "integer", "number", x + 140, y, 110)); y += 32;
        p.Extra.Add(Label("Guss. L or R", x, y)); p.Extra.Add(Param("P24", "YesNo", "number", x + 140, y + 16, 110)); y += 60;
        p.Extra.Add(Label("Bolt size", x, y)); p.Extra.Add(Param("P38_diameter", "bolt_size", "number", x + 140, y, 110)); y += 32;
        p.Extra.Add(Label("Bolt standard", x, y)); p.Extra.Add(Param("P38_screwdin", "bolt_standard", "text", x + 140, y, 110)); y += 32;
        p.Extra.Add(Label("Tolerance", x, y)); p.Extra.Add(Param("P40", "distance", "number", x + 140, y, 110)); y += 32;
        p.Extra.Add(Label("Slotted hole X", x, y)); p.Extra.Add(Param("P39", "distance", "number", x + 140, y, 110)); y += 32;
        p.Extra.Add(Label("Slotted hole Y", x, y)); p.Extra.Add(Param("P41", "distance", "number", x + 140, y, 110));
        return p;
    }

    // ------------------------------------------------------------------ hình 3: cánh, vát mép, FB

    private static Page Flange(string dir)
    {
        Page p = new Page();
        p.Title = "Flange & FB";
        p.Bitmap = "UEL_RAKER_TG_FB";
        Bitmap bmp = Begin(p, 600, 420);

        Rect(Green, 60, 25, 22, 380);                               // bản đế
        // bụng + scallop
        Rect(Web, 132, 100, 460, 220);
        using (SolidBrush b = new SolidBrush(Bg)) { _g.FillPie(b, 112, 80, 40, 40, 0, 90); _g.FillPie(b, 112, 300, 40, 40, 270, 90); }
        _g.DrawArc(Pens.Black, 112, 80, 40, 40, 0, 90);
        _g.DrawArc(Pens.Black, 112, 300, 40, 40, 270, 90);
        // cánh trên: vát mở lên trên, mặt chân 2 mm ở dưới
        Poly(Steel, 130, 80, 590, 80, 590, 100, 104, 100, 104, 97);
        // cánh dưới: vát mở lên trên (mặt trong), chân ở mặt ngoài
        Poly(Steel, 130, 320, 590, 320, 590, 340, 104, 340, 104, 337);
        // FB: dưới cánh trên, dưới cánh dưới
        Rect(Cyan, 82, 100, 62, 12);
        Rect(Cyan, 82, 340, 62, 12);

        // khe chân P10
        DimH(82, 104, 58, 25, 97);
        Slot("P10", "distance", "number", 130, 6, "Weld gap", "right");
        Leader(130, 17, 95, 55);
        // góc vát P12
        using (Pen d = new Pen(Dim, 1f) { DashStyle = DashStyle.Dash }) _g.DrawLine(d, 104, 97, 104, 45);
        using (Pen d = new Pen(Dim, 1f)) _g.DrawArc(d, 74, 67, 60, 60, 270, 51);
        Slot("P12", "angle", "number", 200, 40, "Weld angle", "right");
        Leader(200, 51, 128, 72);
        // scallop
        Slot("P51", "YesNo", "number", 230, 125, "Top scallop", "right");
        Leader(230, 136, 150, 112);
        Slot("P53", "YesNo", "number", 230, 270, "Bott. scallop", "right");
        Leader(230, 281, 150, 308);
        // FB
        Slot("P52", "YesNo", "number", 230, 372, "FB", "right");
        Leader(230, 380, 140, 350);
        Leader(230, 376, 140, 110);

        End(bmp, Path.Combine(dir, p.Bitmap + ".bmp"));

        int x = p.PicX + p.PicW + 30, y = p.PicY + 30;
        p.Extra.Add(Label("FB Mark", x, y)); p.Extra.Add(Param("P11", "string", "text", x + 90, y, 120)); y += 35;
        p.Extra.Add(Label("FB Mat.", x, y)); p.Extra.Add(Param("P5", "material", "text", x + 90, y, 120));
        return p;
    }

    // ------------------------------------------------------------------ .inp

    private static string Label(string text, int x, int y)
    {
        return "            attribute(\"\", \"" + text + "\", label, \"%s\", none, none, \"0\", \"0\", " + x + ", " + (y + 3) + ")";
    }

    private static string Param(string name, string type, string fmt, int x, int y, int w)
    {
        return "            parameter(\"\", \"" + name + "\", " + type + ", " + fmt + ", " + x + ", " + y + ", " + w + ")";
    }

    private static string Inp(List<Page> pages)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("page(\"TeklaStructures\", \"\")");
        sb.AppendLine("{");
        sb.AppendLine("    joint(1, \"RAKER+T_GIRDER\")");
        sb.AppendLine("    {");
        for (int i = 0; i < pages.Count; i++)
        {
            Page p = pages[i];
            sb.AppendLine("        tab_page(\"\", \"" + p.Title + "\", " + (i + 1) + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            picture(\"" + p.Bitmap + "\", " + p.PicW + ", " + p.PicH + ", " + p.PicX + ", " + p.PicY + ")");
            foreach (Field f in p.Fields) sb.AppendLine(Param(f.Name, f.Type, f.Format, p.PicX + f.X, p.PicY + f.Y, f.W));
            foreach (string e in p.Extra) sb.AppendLine(e);
            sb.AppendLine("        }");
        }
        sb.AppendLine("    }");
        sb.AppendLine("}");
        return sb.ToString();
    }
}
