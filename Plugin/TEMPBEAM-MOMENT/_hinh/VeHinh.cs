// Hình minh hoạ cho hộp thoại TEMPBEAM-MOMENT và phần .inp tương ứng (ô điền đặt đúng chỗ trên hình).
// Thư mục bắt đầu bằng "_" nên build.bat không đưa file này vào plugin.
//
// Chạy (ra bitmaps\UEL_TBM_1.bmp và _hinh\dialog.inp):
//   csc /out:VeHinh.exe /reference:System.Drawing.dll VeHinh.cs  &&  VeHinh.exe "<thư mục TEMPBEAM-MOMENT>"
// Sau đó chép dialog.inp vào hằng UserInterface trong Plugin.cs bằng script (nhân đôi dấu ", thay tên joint).
//
// Người dùng 18/11/2026: vẽ theo PHONG CÁCH HÌNH MẶC ĐỊNH CỦA TEKLA (ảnh mẫu component hệ thống): nền xám nhạt, dầm màu
// xanh thép, tấm màu cam vàng, viền xanh đậm mảnh, nét khuất đứt, bu lông = vòng tròn + dấu cộng, ô điền nằm trên hình.
// Hình vẽ ĐÚNG TỈ LỆ theo JSON của component gốc (Part 1 = H500*200*10*16, Part 2 = H400*200*8*13, tỉ lệ 0.8 px/mm):
// mặt đứng nhìn dọc trục Part 1 - Part 1 cắt ngang (chữ I) bên phải, Part 2 vào từ bên trái.
// Ảnh chụp component gốc giữ ở _hinh\anh_goc_mat_dung.png, _hinh\anh_goc_3d.png để đối chiếu.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

internal static class VeHinh
{
    // ---- màu kiểu Tekla
    private static readonly Color Bg = Color.FromArgb(243, 243, 243);
    private static readonly Color Edge = Color.FromArgb(28, 40, 70);
    private static readonly Color Beam = Color.FromArgb(108, 156, 206);
    private static readonly Color BeamDark = Color.FromArgb(84, 128, 178);
    private static readonly Color Plate = Color.FromArgb(255, 204, 112);
    private static readonly Color Plate2 = Color.FromArgb(245, 170, 80);
    private static readonly Color Dim = Color.FromArgb(30, 30, 30);
    private static readonly Color Txt = Color.FromArgb(20, 20, 20);

    // ---- kích thước mẫu (mm, JSON 18/11/2026)
    private const double H1 = 500, B1 = 200, TW1 = 10, TF1 = 16, Y1C = -50;     // Part 1, tâm thấp hơn trục Part 2 50
    private const double H2 = 400, TF2 = 13;                                   // Part 2
    private const double GAP = 5, T3 = 11, TABH = 275, B3 = 135, R = 35, FIL = 10, GPIN = 20, BKIN = 10;
    private const double P39 = 14, FBW = 25, FBT = 9, BOLTX = 65, PITCH = 195;

    // ---- toạ độ: x mm tính từ bụng Part 1 về phía Part 2 (sang trái), y mm lên trên từ trục Part 2
    private const double K = 0.8;
    private const int OX = 500, OY = 230;                  // bụng Part 1 / trục Part 2 trên ảnh
    private static float X(double xmm) { return (float)(OX - xmm * K); }
    private static float Y(double ymm) { return (float)(OY - ymm * K); }

    private sealed class Page
    {
        public string Title, Bitmap;
        public int PicX = 10, PicY = 10, PicW, PicH;
        public readonly List<string> Lines = new List<string>();
    }

    private static Graphics _g;
    private static Pen _pen;
    private static readonly Font F = new Font("Arial", 8f);

    private static int Main(string[] args)
    {
        string dir = args.Length > 0 ? args[0] : ".";
        string bmpDir = Path.Combine(dir, "bitmaps");
        Directory.CreateDirectory(bmpDir);
        List<Page> pages = new List<Page>();
        pages.Add(Page1(bmpDir));
        pages.Add(Advanced());
        File.WriteAllText(Path.Combine(Path.Combine(dir, "_hinh"), "dialog.inp"), Inp(pages), new UTF8Encoding(false));
        Console.WriteLine("Xong: bitmaps\\UEL_TBM_1.bmp + _hinh\\dialog.inp");
        return 0;
    }

    // ------------------------------------------------------------------ vẽ

    private static PointF P(double xmm, double ymm) { return new PointF(X(xmm), Y(ymm)); }

    private static void Fill(Color c, List<PointF> pts)
    {
        PointF[] a = pts.ToArray();
        using (SolidBrush b = new SolidBrush(c)) _g.FillPolygon(b, a);
        _g.DrawPolygon(_pen, a);
    }

    private static void Box(Color c, double x0, double y0, double x1, double y1)
    {
        List<PointF> pts = new List<PointF>();
        pts.Add(P(x0, y0)); pts.Add(P(x1, y0)); pts.Add(P(x1, y1)); pts.Add(P(x0, y1));
        Fill(c, pts);
    }

    /// Cung tâm (cx, cy) bán kính r, góc a0..a1 (độ, đo từ +x ngược chiều kim đồng hồ, toạ độ mm).
    private static void Arc(List<PointF> pts, double cx, double cy, double r, double a0, double a1)
    {
        for (int i = 0; i <= 12; i++)
        {
            double a = (a0 + (a1 - a0) * i / 12.0) * Math.PI / 180.0;
            pts.Add(P(cx + r * Math.Cos(a), cy + r * Math.Sin(a)));
        }
    }

    private static void Dashed(double x0, double y0, double x1, double y1)
    {
        using (Pen p = new Pen(Edge, 1f) { DashStyle = DashStyle.Dash }) _g.DrawLine(p, P(x0, y0), P(x1, y1));
    }

    private static void CenterLine(float x0, float y0, float x1, float y1)
    {
        using (Pen p = new Pen(Color.FromArgb(110, 110, 110), 1f) { DashStyle = DashStyle.DashDot }) _g.DrawLine(p, x0, y0, x1, y1);
    }

    private static void Bolt(double xmm, double ymm)
    {
        PointF c = P(xmm, ymm);
        // lỗ ô van dọc trục Part 2 (P15) + vòng bu lông + dấu cộng
        using (Pen p = new Pen(Edge, 1f)) _g.DrawArc(p, c.X - 9, c.Y - 6, 12, 12, 90, 180);
        using (Pen p = new Pen(Edge, 1f)) _g.DrawArc(p, c.X - 3, c.Y - 6, 12, 12, 270, 180);
        using (Pen p = new Pen(Edge, 1f)) { _g.DrawLine(p, c.X - 3, c.Y - 6, c.X + 3, c.Y - 6); _g.DrawLine(p, c.X - 3, c.Y + 6, c.X + 3, c.Y + 6); }
        using (SolidBrush b = new SolidBrush(Color.White)) _g.FillEllipse(b, c.X - 4.5f, c.Y - 4.5f, 9, 9);
        _g.DrawEllipse(_pen, c.X - 4.5f, c.Y - 4.5f, 9, 9);
        _g.DrawLine(_pen, c.X - 7, c.Y, c.X + 7, c.Y);
        _g.DrawLine(_pen, c.X, c.Y - 7, c.X, c.Y + 7);
    }

    private static void Arrow(float x, float y, float dx, float dy, Pen pen)
    {
        double l = Math.Sqrt(dx * dx + dy * dy);
        if (l < 1) return;
        double ux = dx / l, uy = dy / l;
        PointF tip = new PointF(x, y);
        PointF a = new PointF((float)(x - ux * 6 - uy * 2), (float)(y - uy * 6 + ux * 2));
        PointF b = new PointF((float)(x - ux * 6 + uy * 2), (float)(y - uy * 6 - ux * 2));
        using (SolidBrush br = new SolidBrush(pen.Color)) _g.FillPolygon(br, new[] { tip, a, b });
    }

    /// Kích thước ngang giữa x0..x1 (mm) tại cao độ y (px), đường gióng từ yA / yB (px).
    private static void DimH(double x0, double x1, float y, float yA, float yB)
    {
        using (Pen p = new Pen(Dim, 1f))
        {
            float a = X(x0), b = X(x1);
            _g.DrawLine(p, a, yA, a, y + 3 * Math.Sign(y - yA));
            _g.DrawLine(p, b, yB, b, y + 3 * Math.Sign(y - yB));
            _g.DrawLine(p, a, y, b, y);
            Arrow(a, y, a - b, 0, p);
            Arrow(b, y, b - a, 0, p);
        }
    }

    private static void DimV(double y0, double y1, float x, float xA, float xB)
    {
        using (Pen p = new Pen(Dim, 1f))
        {
            float a = Y(y0), b = Y(y1);
            _g.DrawLine(p, xA, a, x + 3 * Math.Sign(x - xA), a);
            _g.DrawLine(p, xB, b, x + 3 * Math.Sign(x - xB), b);
            _g.DrawLine(p, x, a, x, b);
            Arrow(x, a, 0, a - b, p);
            Arrow(x, b, 0, b - a, p);
        }
    }

    private static void Leader(float x0, float y0, float x1, float y1)
    {
        using (Pen p = new Pen(Dim, 1f))
        {
            _g.DrawLine(p, x0, y0, x1, y1);
            Arrow(x1, y1, x1 - x0, y1 - y0, p);
        }
    }

    private static void Text(string s, float x, float y)
    {
        using (SolidBrush b = new SolidBrush(Txt)) _g.DrawString(s, F, b, x, y);
    }

    private static void TextC(string s, float x, float y)
    {
        SizeF sz = _g.MeasureString(s, F);
        Text(s, x - sz.Width / 2, y);
    }

    // ------------------------------------------------------------------ tab 1

    private static Page Page1(string bmpDir)
    {
        Page p = new Page();
        p.Title = " Parameters 1 ";
        p.Bitmap = "UEL_TBM_1";
        p.PicW = 640; p.PicH = 480;
        Bitmap bmp = new Bitmap(p.PicW, p.PicH, PixelFormat.Format24bppRgb);
        _g = Graphics.FromImage(bmp);
        _g.SmoothingMode = SmoothingMode.AntiAlias;
        _g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        _g.Clear(Bg);
        _pen = new Pen(Edge, 1f);

        double tip = B1 / 2, tw = TW1 / 2;
        double yT1o = Y1C + H1 / 2, yT1 = yT1o - TF1, yB1o = Y1C - H1 / 2, yB1 = yB1o + TF1;
        double yT2 = H2 / 2, yiT = yT2 - TF2, yB2 = -H2 / 2, yiB = yB2 + TF2;
        double xe = tip + GAP, xLeft = 440;

        // ---- Part 2 (dầm tạm, mặt bên): bụng + 2 cánh, đầu có scallop, đầu trái là đường cắt gãy
        List<PointF> web = new List<PointF>();
        web.Add(P(xLeft, yiT)); web.Add(P(xe + R, yiT));
        Arc(web, xe, yiT, R, 0, -90);                     // scallop trên: (xe+R, yiT) -> (xe, yiT-R)
        web.Add(P(xe, yiB + R));
        Arc(web, xe, yiB, R, 90, 0);                      // scallop dưới: (xe, yiB+R) -> (xe+R, yiB)
        web.Add(P(xLeft, yiB));
        web.Add(P(xLeft + 6, -20)); web.Add(P(xLeft - 6, 20));          // đường cắt gãy
        Fill(Beam, web);
        // 2 cánh: vát CJP ở đầu (cánh trên vát mặt ngoài, cánh dưới vát mặt trong)
        List<PointF> ft = new List<PointF>();
        ft.Add(P(xLeft, yT2)); ft.Add(P(xe + (TF2 - 2), yT2)); ft.Add(P(xe, yiT + 2)); ft.Add(P(xe, yiT)); ft.Add(P(xLeft, yiT));
        Fill(BeamDark, ft);
        List<PointF> fb = new List<PointF>();
        fb.Add(P(xLeft, yiB)); fb.Add(P(xe + (TF2 - 2), yiB)); fb.Add(P(xe, yB2 + 2)); fb.Add(P(xe, yB2)); fb.Add(P(xLeft, yB2));
        Fill(BeamDark, fb);

        // ---- Part 1 (dầm chính, mặt cắt chữ I)
        Box(Beam, -tip, yT1o, tip, yT1);
        Box(Beam, -tip, yB1, tip, yB1o);
        Box(Beam, -tw, yT1, tw, yB1);

        // ---- BACK SF (phía bên kia bụng Part 1)
        List<PointF> bk = new List<PointF>();
        Arc(bk, -tw, yB1, R, 90, 180);                    // (-tw, yB1+R) -> (-tw-R, yB1)
        bk.Add(P(-(tip - BKIN), yB1)); bk.Add(P(-(tip - BKIN), yT1));
        Arc(bk, -tw, yT1, R, 180, 270);                   // (-tw-R, yT1) -> (-tw, yT1-R)
        Fill(Plate, bk);

        // ---- GP: phần trong Part 1 + tab sang Part 2
        double xg = tip - GPIN, xt = tip + B3, h = TABH / 2;
        List<PointF> gp = new List<PointF>();
        Arc(gp, tw, yB1, R, 90, 0);                       // (tw, yB1+R) -> (tw+R, yB1)
        gp.Add(P(xg, yB1));
        Arc(gp, xg + FIL, -h - FIL, FIL, 180, 90);        // bo chân tab dưới
        gp.Add(P(xt, -h)); gp.Add(P(xt, h));
        Arc(gp, xg + FIL, h + FIL, FIL, 270, 180);        // bo chân tab trên
        gp.Add(P(xg, yT1));
        Arc(gp, tw, yT1, R, 0, -90);                      // (tw+R, yT1) -> (tw, yT1-R)
        Fill(Plate, gp);
        // đầu Part 2 + scallop bị tab che: nét khuất
        Dashed(xe, -h, xe, h);

        // ---- SF dưới (nằm ngang, cao độ cánh dưới Part 2), FB lót 2 cánh
        Box(Plate2, tw, yB2 + P39, tip, yB2);
        Box(Plate2, tip, yiT, tip + FBW, yiT - FBT);
        Box(Plate2, tip, yB2, tip + FBW, yB2 - FBT);

        // ---- bu lông
        double xb = xe + BOLTX;
        Bolt(xb, PITCH / 2);
        Bolt(xb, -PITCH / 2);

        // ---- đường tâm
        CenterLine(X(0), Y(yT1o) - 18, X(0), Y(yB1o) + 12);
        CenterLine(X(xLeft) + 4, Y(0), X(xt) + 14, Y(0));

        // ---- nhãn
        Text("Part 2", X(xLeft) + 8, Y(yiT) + 6);
        Text("Part 1", X(-tip) + 4, Y(yT1o) - 30);
        Text("GP", X(xg) + 8, Y(0) - 30);
        Text("Back SF", X(-tw) + 4, Y(yT1) + 50);
        Text("SF", X(tip / 2) - 6, Y(yB2) + 3);
        Text("FB", X(tip + FBW) - 22, Y(yT2) - 16);

        // ---- kích thước (số lấy từ bảng tra / tự tính: chỉ ghi nhãn)
        float yDim = Y(yB2) + 40;
        DimH(tip, xt, yDim, Y(-h) + 4, Y(yB2) + 12);
        TextC("B3 (bang tra)", (X(tip) + X(xt)) / 2, yDim + 3);
        float xDim = X(xt) - 30;
        DimV(-h, h, xDim, X(xt) - 4, X(xt) - 4);
        Text("H1", xDim - 22, Y(0) - 18);
        Text("(bang", xDim - 34, Y(0) - 4);
        Text("tra)", xDim - 28, Y(0) + 8);
        float yGap = Y(yT2) - 12;
        DimH(tip, xe, yGap, Y(yT1o), Y(yT2));
        Text("5", X(xe) - 12, yGap - 15);

        // ---- mũi chỉ từ ô điền tới chi tiết
        double xb0 = xe + BOLTX;
        Leader(X(xb0) - 68, Y(PITCH / 2), X(xb0) - 10, Y(PITCH / 2));
        Leader(X(xb0) - 68, Y(-PITCH / 2), X(xb0) - 10, Y(-PITCH / 2));
        Leader(342, 52, X(xe + R * 0.7), Y(yiT - R * 0.7));
        Leader(300, 422, X(xe + R * 0.7), Y(yiB + R * 0.7));

        _pen.Dispose();
        _g.Dispose();
        bmp.Save(Path.Combine(bmpDir, p.Bitmap + ".bmp"), ImageFormat.Bmp);
        bmp.Dispose();

        // ---- ô điền trên hình (toạ độ px của ảnh)
        // bu lông: cỡ (P12) + lỗ ô van (P15), bên trái 2 con bu lông
        float bx = X(xb);
        Slot(p, "Bolt M", "P12", "integer", (int)bx - 110, (int)Y(PITCH / 2) - 10, 40);
        Slot(p, "Slot", "P15", "integer", (int)bx - 110, (int)Y(-PITCH / 2) - 10, 40);
        // scallop: cỡ (P41) trên, kiểu (P40) dưới, cạnh scallop đầu Part 2
        Slot(p, "Scallop R", "P41", "distance", 302, 42, 40);
        Slot(p, "Scallop", "P40", "chamfer_type", 250, 412, 50);
        // GP: phía tấm (P11) trên phần trong Part 1; Back SF (P31) trên tấm phía sau
        Slot(p, "", "P11", "YesNo", (int)X(xg) + 6, (int)Y(0) - 12, 52);
        Slot(p, "", "P31", "YesNo", (int)X(-tw) + 6, (int)Y(yT1) + 66, 52);
        // P43 (gia cường cánh dưới - chi tiết chưa rõ vị trí) đặt ở cột bên phải, xem Inp()
        return p;
    }

    /// Ô điền tại (x, y) px của ảnh, nhãn (nếu có) ngay bên trái.
    private static void Slot(Page pg, string label, string name, string type, int x, int y, int w)
    {
        if (label.Length > 0)
        {
            int lx = x - 6 - label.Length * 6;
            pg.Lines.Add(Label(label, pg.PicX + lx, pg.PicY + y));
        }
        pg.Lines.Add(Param(name, type, "number", pg.PicX + x, pg.PicY + y, w));
    }

    // ------------------------------------------------------------------ tab Advanced

    private static Page Advanced()
    {
        Page p = new Page();
        p.Title = " Advanced ";
        int x0 = 20, lw = 110, cw = 200, gap = 20, y = 15;
        int[] cx = { x0 + lw, x0 + lw + cw + gap, x0 + lw + 2 * (cw + gap) };
        string[] head = { "GP", "SF (Back SF / SF duoi)", "FB" };
        for (int i = 0; i < 3; i++) p.Lines.Add(Label(head[i], cx[i], y));
        y += 26;
        string[,] rows = {
            { "Name",   "P66", "P69", "P72" },
            { "Class",  "P67", "P70", "P73" },
            { "Finish", "P68", "P71", "P74" },
        };
        for (int r = 0; r < rows.GetLength(0); r++)
        {
            p.Lines.Add(Label(rows[r, 0], x0, y));
            for (int i = 0; i < 3; i++) p.Lines.Add(Param(rows[r, i + 1], "string", "text", cx[i], y, cw));
            y += 30;
        }
        p.Lines.Add(Label("SF class trong = 99 (Back SF) / 83 (SF duoi)", cx[1], y)); y += 36;
        p.Lines.Add(Label("FB profile", x0, y)); p.Lines.Add(Param("P75", "profile", "text", cx[0], y, cw)); y += 30;
        p.Lines.Add(Label("FB material", x0, y)); p.Lines.Add(Param("P76", "material", "text", cx[0], y, cw)); y += 30;
        p.Lines.Add(Label("Bolt standard", x0, y)); p.Lines.Add(Param("P77", "bolt_standard", "text", cx[0], y, cw));
        return p;
    }

    // ------------------------------------------------------------------ .inp

    private static string Label(string text, int x, int y)
    {
        return "            attribute(\"\", \"" + text + "\", label, \"%s\", none, none, \"0\", \"0\", " + x + ", " + (y + 3) + ")";
    }

    private static string Param(string name, string type, string fmt, int x, int y, int w)
    {
        string f = type == "string" || type == "profile" || type == "material" || type == "bolt_standard" || type == "file_in" ? "text" : fmt;
        return "            parameter(\"\", \"" + name + "\", " + type + ", " + f + ", " + x + ", " + y + ", " + w + ")";
    }

    private static string Inp(List<Page> pages)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("page(\"TeklaStructures\", \"\")");
        sb.AppendLine("{");
        sb.AppendLine("    joint(1, \"TEMPBEAM-MOMENT\")");
        sb.AppendLine("    {");
        for (int i = 0; i < pages.Count; i++)
        {
            Page p = pages[i];
            sb.AppendLine("        tab_page(\"\", \"" + p.Title + "\", " + (i + 1) + ")");
            sb.AppendLine("        {");
            if (p.Bitmap != null)
            {
                sb.AppendLine("            picture(\"" + p.Bitmap + "\", " + p.PicW + ", " + p.PicH + ", " + p.PicX + ", " + p.PicY + ")");
                foreach (string e in p.Lines) sb.AppendLine(e);
                // cột bên phải hình + dải dưới hình
                int x = p.PicX + p.PicW + 20, lw = 150, fw = 120, y = p.PicY + 10;
                sb.AppendLine(Label("GP / SF prefix", x, y)); sb.AppendLine(Param("P30", "string", "text", x + lw, y, fw)); y += 22;
                sb.AppendLine(Label("(trong = theo Part 1)", x, y)); y += 34;
                sb.AppendLine(Label("CWS (900, SPS 700)", x, y)); sb.AppendLine(Param("CWS", "integer", "number", x + lw, y, fw)); y += 22;
                sb.AppendLine(Label("(trong = auto)", x, y)); y += 34;
                sb.AppendLine(Label("Gia cuong canh duoi", x, y)); sb.AppendLine(Param("P43", "YesNo", "number", x + lw, y, fw)); y += 22;
                sb.AppendLine(Label("(trong = auto, P39 >= 21)", x, y)); y += 40;
                sb.AppendLine(Label("GP side: No = +Z dam, Yes = -Z dam", x, y)); y += 22;
                sb.AppendLine(Label("Back SF: Yes = co tam phia sau", x, y)); y += 22;
                sb.AppendLine(Label("Slot / R / Gia cuong: trong = tinh nhu .uel", x, y));
                int by = p.PicY + p.PicH + 12;
                sb.AppendLine(Label("Bang tra dam (trong = YGY-TB-MOMENT.txt)", p.PicX, by));
                sb.AppendLine(Param("P65", "file_in", "text", p.PicX + 260, by, 480));
            }
            else foreach (string e in p.Lines) sb.AppendLine(e);
            sb.AppendLine("        }");
        }
        sb.AppendLine("    }");
        sb.AppendLine("}");
        return sb.ToString();
    }
}
