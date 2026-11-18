// Vẽ hình minh hoạ cho hộp thoại COL_TO_GIRDER và sinh phần .inp tương ứng (ô điền đặt đúng chỗ trên hình).
// Thư mục bắt đầu bằng "_" nên build.bat không đưa file này vào plugin.
//
// Chạy (ra bitmaps\*.bmp và _hinh\dialog.inp):
//   csc /out:VeHinh.exe /reference:System.Drawing.dll VeHinh.cs  &&  VeHinh.exe "<thư mục COL_TO_GIRDER>"
// Sau đó chép nội dung dialog.inp vào hằng UserInterface trong Plugin.cs (nhân đôi dấu ", thay tên joint).
//
// Hình vẽ theo ảnh component gốc người dùng gửi (18/11/2026): nét mảnh, ít chữ.
//   Tab 1: trái = mặt đứng (dầm Part 2 bên trái, cột Part 1 bên phải, ring bao quanh cột ở cao độ 2 cánh, CGP + bu lông
//          gần mép ngoài CGP, scallop ở đầu bụng); phải = mặt bằng (WP hình thang 2 bên cánh, rộng tới mép cột).
//   Tab 2: mặt đứng, vùng cắt P62..P65 + bảng tra P78.
// Giữ đúng tab của .inp gốc (P4..P60 ở tab 1, P61..P65 ở tab 2). Biến mới P74..P77 ở tab 1, P78 ở tab 2.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

internal static class VeHinh
{
    private const int SlotW = 50, SlotH = 20;

    private static readonly Color Bg = Color.FromArgb(243, 243, 243);        // màu kiểu Tekla (người dùng 18/11/2026)
    private static readonly Color Line = Color.FromArgb(28, 40, 70);          // viền xanh đậm
    private static readonly Color ColFill = Color.FromArgb(108, 156, 206);    // cột: xanh thép
    private static readonly Color GirFill = Color.FromArgb(108, 156, 206);    // dầm: xanh thép
    private static readonly Color FlgFill = Color.FromArgb(84, 128, 178);     // cánh dầm: xanh thép đậm
    private static readonly Color RingFill = Color.FromArgb(245, 170, 80);    // tấm phụ: cam đậm
    private static readonly Color CgpFill = Color.FromArgb(255, 204, 112);    // tấm chính: cam vàng
    private static readonly Color WpFill = Color.FromArgb(255, 204, 112);     // tấm chính: cam vàng
    private static readonly Color FbFill = Color.FromArgb(245, 170, 80);      // tấm phụ: cam đậm
    private static readonly Color BoltFill = Color.White;                     // bu lông kiểu Tekla: tròn trắng + dấu cộng
    private static readonly Color Dim = Color.FromArgb(30, 30, 30);           // kích thước, mũi chỉ: đen
    private static readonly Color Hatch = Color.FromArgb(170, 40, 40);        // vùng cắt: gạch đỏ sẫm (nổi trên nền xanh / cam)

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
        public readonly List<string> Extra = new List<string>();
    }

    private static Graphics _g;
    private static Page _page;
    private static Pen _pen;
    private static readonly Font F = new Font("Arial", 8f);
    private static readonly Font FS = new Font("Arial", 7.5f);

    private static int Main(string[] args)
    {
        string dir = args.Length > 0 ? args[0] : ".";
        string bmpDir = Path.Combine(dir, "bitmaps");
        Directory.CreateDirectory(bmpDir);
        List<Page> pages = new List<Page>();
        // Người dùng 18/11/2026: nội dung tab " Parameters 2 " gộp vào tab 1 (Page2 không dùng nữa);
        // thêm tab " Advanced " (thuộc tính part CGP / WP / FB, không có hình).
        pages.Add(Page1(bmpDir));
        pages.Add(Advanced());
        File.WriteAllText(Path.Combine(Path.Combine(dir, "_hinh"), "dialog.inp"), Inp(pages), new UTF8Encoding(false));
        Console.WriteLine("Xong: " + pages.Count + " hinh + _hinh\\dialog.inp");
        return 0;
    }

    // ------------------------------------------------------------------ tiện ích vẽ (nét 1 px)

    private static Bitmap Begin(Page p, int w, int h)
    {
        p.PicW = w; p.PicH = h;
        Bitmap bmp = new Bitmap(w, h, PixelFormat.Format24bppRgb);
        _g = Graphics.FromImage(bmp);
        _g.SmoothingMode = SmoothingMode.AntiAlias;
        _g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        _g.Clear(Bg);
        _pen = new Pen(Line, 1f);
        _page = p;
        return bmp;
    }

    private static void End(Bitmap bmp, string file)
    {
        _pen.Dispose();
        _g.Dispose();
        bmp.Save(file, ImageFormat.Bmp);
        bmp.Dispose();
    }

    private static Point[] Pts(int[] xy)
    {
        Point[] pts = new Point[xy.Length / 2];
        for (int i = 0; i < pts.Length; i++) pts[i] = new Point(xy[2 * i], xy[2 * i + 1]);
        return pts;
    }

    private static void Poly(Color fill, params int[] xy)
    {
        Point[] pts = Pts(xy);
        using (SolidBrush b = new SolidBrush(fill)) _g.FillPolygon(b, pts);
        _g.DrawPolygon(_pen, pts);
    }

    private static void Rect(Color fill, int x, int y, int w, int h)
    {
        using (SolidBrush b = new SolidBrush(fill)) _g.FillRectangle(b, x, y, w, h);
        _g.DrawRectangle(_pen, x, y, w, h);
    }

    private static void DashRect(Color c, int x, int y, int w, int h)
    {
        using (Pen p = new Pen(c, 1f) { DashStyle = DashStyle.Dash }) _g.DrawRectangle(p, x, y, w, h);
    }

    private static void HatchRect(int x, int y, int w, int h)
    {
        using (HatchBrush b = new HatchBrush(HatchStyle.BackwardDiagonal, Hatch, Color.Transparent)) _g.FillRectangle(b, x, y, w, h);
        using (Pen p = new Pen(Hatch, 1f)) _g.DrawRectangle(p, x, y, w, h);
    }

    private static void CenterLine(int x1, int y1, int x2, int y2)
    {
        using (Pen p = new Pen(Color.FromArgb(120, 120, 120), 1f) { DashStyle = DashStyle.DashDot }) _g.DrawLine(p, x1, y1, x2, y2);
    }

    private static void Text(string s, int x, int y, Color? c = null)
    {
        using (SolidBrush b = new SolidBrush(c ?? Color.FromArgb(40, 40, 40))) _g.DrawString(s, F, b, x, y);
    }

    private static void TextR(string s, int xRight, int y)
    {
        SizeF sz = _g.MeasureString(s, FS);
        using (SolidBrush b = new SolidBrush(Color.FromArgb(40, 40, 40))) _g.DrawString(s, FS, b, xRight - sz.Width, y);
    }

    private static void BoltAt(int x, int y)
    {
        using (SolidBrush b = new SolidBrush(BoltFill)) _g.FillEllipse(b, x - 5, y - 5, 10, 10);
        _g.DrawEllipse(_pen, x - 5, y - 5, 10, 10);
        _g.DrawLine(_pen, x - 3, y, x + 3, y);
        _g.DrawLine(_pen, x, y - 3, x, y + 3);
    }

    private static void Arrow(int x, int y, int dx, int dy, Pen pen)
    {
        double l = Math.Sqrt(dx * dx + dy * dy);
        if (l < 1) return;
        double ux = dx / l, uy = dy / l;
        PointF tip = new PointF(x, y);
        PointF a = new PointF((float)(x - ux * 5 - uy * 2), (float)(y - uy * 5 + ux * 2));
        PointF b = new PointF((float)(x - ux * 5 + uy * 2), (float)(y - uy * 5 - ux * 2));
        using (SolidBrush br = new SolidBrush(pen.Color)) _g.FillPolygon(br, new[] { tip, a, b });
    }

    /// Kích thước ngang tại cao độ y (x1..x2), đường gióng từ ext1/ext2. Mũi tên trong; khoảng hẹp thì mũi tên ngoài.
    private static void DimH(int x1, int x2, int y, int ext1, int ext2)
    {
        using (Pen p = new Pen(Dim, 1f))
        using (Pen d = new Pen(Dim, 1f) { DashStyle = DashStyle.Dot })
        {
            if (ext1 != y) _g.DrawLine(d, x1, ext1, x1, y + 3 * Math.Sign(y - ext1));
            if (ext2 != y) _g.DrawLine(d, x2, ext2, x2, y + 3 * Math.Sign(y - ext2));
            if (Math.Abs(x2 - x1) >= 14)
            {
                _g.DrawLine(p, x1, y, x2, y);
                Arrow(x1, y, x1 - x2, 0, p);
                Arrow(x2, y, x2 - x1, 0, p);
            }
            else
            {
                int s = Math.Sign(x2 - x1);
                _g.DrawLine(p, x1 - s * 10, y, x2 + s * 10, y);
                Arrow(x1, y, s, 0, p);
                Arrow(x2, y, -s, 0, p);
            }
        }
    }

    private static void DimV(int y1, int y2, int x, int ext1, int ext2)
    {
        using (Pen p = new Pen(Dim, 1f))
        using (Pen d = new Pen(Dim, 1f) { DashStyle = DashStyle.Dot })
        {
            if (ext1 != x) _g.DrawLine(d, ext1, y1, x + 3 * Math.Sign(x - ext1), y1);
            if (ext2 != x) _g.DrawLine(d, ext2, y2, x + 3 * Math.Sign(x - ext2), y2);
            if (Math.Abs(y2 - y1) >= 14)
            {
                _g.DrawLine(p, x, y1, x, y2);
                Arrow(x, y1, 0, y1 - y2, p);
                Arrow(x, y2, 0, y2 - y1, p);
            }
            else
            {
                int s = Math.Sign(y2 - y1);
                _g.DrawLine(p, x, y1 - s * 10, x, y2 + s * 10);
                Arrow(x, y1, 0, s, p);
                Arrow(x, y2, 0, -s, p);
            }
        }
    }

    private static void Leader(int x1, int y1, int x2, int y2)
    {
        using (Pen p = new Pen(Dim, 1f)) { _g.DrawLine(p, x1, y1, x2, y2); Arrow(x2, y2, x2 - x1, y2 - y1, p); }
    }

    /// Ô điền trên hình + nhãn ngắn (trái / phải / trên).
    private static void Slot(string name, string type, string fmt, int x, int y, string label, string where = "left", int w = SlotW)
    {
        _g.FillRectangle(Brushes.White, x, y, w, SlotH);
        _g.DrawRectangle(Pens.DarkGray, x, y, w, SlotH);
        if (label != null)
        {
            if (where == "left") TextR(label, x - 2, y + 4);
            else if (where == "right") { using (SolidBrush b = new SolidBrush(Color.FromArgb(40, 40, 40))) _g.DrawString(label, FS, b, x + w + 2, y + 4); }
            else { using (SolidBrush b = new SolidBrush(Color.FromArgb(40, 40, 40))) _g.DrawString(label, FS, b, x, y - 13); }
        }
        Field f = new Field();
        f.Name = name; f.Type = type; f.Format = fmt; f.X = x; f.Y = y; f.W = w;
        _page.Fields.Add(f);
    }

    // ------------------------------------------------------------------ mặt đứng (theo ảnh 1)
    // Dầm bên trái, cột bên phải. Mặt cột x = ColL; ring dày RingT ra ngoài cột (2 bên).
    private const int ColL = 330, ColR = 410, RingT = 8;
    private const int FTt = 80, FTb = 91, FBt = 361, FBb = 372;        // cánh trên / dưới
    private const int RTt = 50, RTb = 122, RBt = 328, RBb = 400;       // ring trên / dưới (P58, P60 / P61, P59)
    private const int FlgEnd = 318, WebEnd = 326;                      // đầu cánh (cách mặt ring), đầu bụng (cách mặt cột)
    private const int CgpL = 228, CgpT = 136, CgpB = 316, BoltX = 248;  // CGP: từ CgpL tới mặt cột
    private const int DimX = 430, SlotX = 440;                          // kích thước ring (bên phải cột) và ô của nó
    private const int Sc = 12;                                          // scallop (vẽ)

    private static void Elevation(int h)
    {
        // cột + ring (ring bao quanh cột ở cao độ 2 cánh)
        Rect(ColFill, ColL, 4, ColR - ColL, h - 8);
        CenterLine((ColL + ColR) / 2, 4, (ColL + ColR) / 2, h - 4);
        Rect(RingFill, ColL - RingT, RTt, ColR - ColL + 2 * RingT, RTb - RTt);
        Rect(RingFill, ColL - RingT, RBt, ColR - ColL + 2 * RingT, RBb - RBt);

        // dầm: bụng (có scallop ở đầu) + cánh vát mép (cánh trên vát mặt ngoài, cánh dưới vát mặt trong)
        // bụng có scallop ở 2 góc gần cột: chữ nhật sâu Sc, góc phía dầm bo tròn R = Sc
        int dE = WebEnd - 2 * Sc;
        using (GraphicsPath web = new GraphicsPath())
        {
            web.AddLine(6, FTb, dE, FTb);
            web.AddArc(dE, FTb - Sc, 2 * Sc, 2 * Sc, 180, -90);
            web.AddLine(dE + Sc, FTb + Sc, WebEnd, FTb + Sc);
            web.AddLine(WebEnd, FTb + Sc, WebEnd, FBt - Sc);
            web.AddLine(WebEnd, FBt - Sc, dE + Sc, FBt - Sc);
            web.AddArc(dE, FBt - Sc, 2 * Sc, 2 * Sc, 270, -90);
            web.AddLine(dE, FBt, 6, FBt);
            web.CloseFigure();
            using (SolidBrush b = new SolidBrush(GirFill)) _g.FillPath(b, web);
            _g.DrawPath(_pen, web);
        }
        Poly(FlgFill, 6, FTt, FlgEnd - 11, FTt, FlgEnd, FTb, 6, FTb);
        Poly(FlgFill, 6, FBt, FlgEnd - 11, FBt, FlgEnd, FBb, 6, FBb);
        // FB lót (dưới cánh trên, dưới cánh dưới)
        Rect(FbFill, FlgEnd - 14, FTb, 18, 5);
        Rect(FbFill, FlgEnd - 14, FBb, 18, 5);
        CenterLine(6, (FTt + FBb) / 2, ColL - 12, (FTt + FBb) / 2);

        // CGP + bu lông (gần mép ngoài CGP)
        Rect(CgpFill, CgpL, CgpT, ColL - CgpL, CgpB - CgpT);
        BoltAt(BoltX, CgpT + 22);
        BoltAt(BoltX, CgpB - 22);

        Text("Part 2", 30, 200);
        Text("Part 1", ColL + 18, 200);
    }

    // ------------------------------------------------------------------ mặt bằng (theo ảnh 2)
    // Cột hộp bên phải (mặt cột x = PL), dầm vào từ trái; WP hình thang 2 bên cánh, rộng tới mép cột.
    private const int PL = 720, PR = 880, PT = 50, PB = 390, PWall = 16;   // cột
    private const int GT = 186, GB = 254;                                  // mép cánh dầm
    private const int WpL = 600, WpC = 650;                                // WP: đầu xa, điểm gãy vát

    private static void Plan()
    {
        Rect(ColFill, PL, PT, PR - PL, PB - PT);
        Rect(Bg, PL + PWall, PT + PWall, PR - PL - 2 * PWall, PB - PT - 2 * PWall);
        Rect(RingFill, PL - RingT, PT - 4, RingT, PB - PT + 8);            // ring (mặt bằng: dải mỏng ở mặt cột)
        int gEnd = PL - RingT - 2;
        Rect(FlgFill, 490, GT, gEnd - 490, GB - GT);                       // cánh dầm
        CenterLine(490, (GT + GB) / 2, PR, (GT + GB) / 2);
        // WP1 (trên), WP2 (dưới): cạnh trong theo mép cánh, cạnh ngoài tới mép cột, đầu xa vát
        Poly(WpFill, gEnd, PT, WpC, PT, WpL, GT - 40, WpL, GT, gEnd, GT);
        Poly(WpFill, gEnd, PB, WpC, PB, WpL, GB + 40, WpL, GB, gEnd, GB);
        // FB (dưới cánh, nét đứt) và CGP + bu lông ở 1 bên bụng
        DashRect(Color.FromArgb(190, 110, 30), gEnd - 8, PT - 4, 8, PB - PT + 8);
        DashRect(Line, 560, (GT + GB) / 2 - 2, gEnd - 560, 4);             // bụng dầm
        Rect(CgpFill, 640, (GT + GB) / 2 + 2, PL - 640, 7);
        BoltAt(660, (GT + GB) / 2);
    }

    // ------------------------------------------------------------------ tab 1

    private static Page Page1(string dir)
    {
        Page p = new Page();
        p.Title = " Parameters 1 ";
        p.Bitmap = "UEL_COL2GIR_1";
        Bitmap bmp = Begin(p, 900, 440);
        Elevation(440);
        Plan();
        using (Pen sep = new Pen(Color.FromArgb(200, 200, 200), 1f)) _g.DrawLine(sep, 478, 6, 478, 434);

        // ---- mặt đứng: CGP & bu lông
        DimH(CgpL, ColL, CgpB + 6, CgpB, CgpB);                            // L3
        Slot("P47", "distance", "number", 254, CgpB + 12, "L3 (auto)");
        DimH(CgpL, BoltX, CgpT - 8, CgpT, CgpT + 22);                      // P4: bu lông -> mép ngoài CGP
        Slot("P4", "distance", "number", 160, CgpT - 38, "e (auto)", "left");
        Leader(185, CgpT - 18, CgpL + 8, CgpT - 8);
        DimV(CgpT + 22, CgpB - 22, 214, BoltX, BoltX);                     // P22: bước bu lông
        Slot("P22", "string", "text", 148, 215, "pitch (auto)", "top", 60);
        DimV(CgpT, CgpT + 22, 214, CgpL, BoltX);                           // P21A: mép (tổng 2 mép)
        Slot("P21A", "distance", "number", 148, 150, "2 x edge", "top", 60);
        Slot("P24", "distance", "number", 262, 220, "slot", "top");        // lỗ ô van
        Leader(270, 240, BoltX + 4, CgpB - 25);

        // ---- mặt đứng: ring + khe đầu dầm
        DimH(ColL - RingT, ColL, RTt - 10, RTt, RTt);                      // P55
        Slot("P55", "distance", "number", 352, 14, "t", "left");
        Leader(352, 24, ColL - 2, RTt - 10);
        // kích thước ring: đường dim x = DimX, ô ngay bên phải (không đè lên nhau)
        DimV(RTt, FTt, DimX, ColR + RingT, ColR + RingT);                  // P58: ring trên cánh trên
        Slot("P58", "distance", "number", SlotX, (RTt + FTt) / 2 - 10, null, "left", 34);
        DimV(FTt, RTb, DimX, ColR + RingT, ColR + RingT);                  // P60: cánh trên -> mép dưới ring trên
        Slot("P60", "distance", "number", SlotX, (FTt + RTb) / 2 - 6, null, "left", 34);
        DimV(FBb, RBb, DimX, ColR + RingT, ColR + RingT);                  // P59: ring dưới cánh dưới
        Slot("P59", "distance", "number", SlotX, (FBb + RBb) / 2 - 10, null, "left", 34);
        DimV(RBt, FBb, DimX, ColR + RingT, ColR + RingT);                  // P61: cánh dưới -> mép trên ring dưới (từ tab 2)
        Slot("P61", "distance", "number", SlotX, (RBt + FBb) / 2 - 12, null, "left", 34);
        // vùng cắt P62..P65 (từ tab 2): gạch trên hình, ô Yes/No ở dải dưới hình
        HatchRect(WebEnd - 14, FTb + Sc, 14, RTb - FTb - Sc);
        HatchRect(WebEnd - 14, RBt, 14, FBt - Sc - RBt);
        HatchRect(ColL - 16, CgpT, 16, 12);
        HatchRect(ColL - 16, CgpB - 12, 16, 12);
        DimH(ColL - RingT, ColL, RBb + 10, RBb, RBb);                      // P56
        Slot("P56", "distance", "number", 352, 412, "t", "left");
        Leader(352, 422, ColL - 2, RBb + 10);
        // khe đầu dầm D5 (biến mới P75 MO / P74 PIN): đầu cánh -> mặt ring
        DimH(FlgEnd, ColL - RingT, FTt - 8, FTt, FTt - 8);
        Slot("P75", "distance", "number", 250, 14, "gap MO", "left", 40);
        Slot("P74", "distance", "number", 250, 38, "PIN", "left", 40);
        Leader(290, 48, FlgEnd - 6, FTt - 8);
        // góc vát mép hàn (P7) và scallop (P80): cạnh vát Sc x Sc ở góc bụng - cánh
        Slot("P7", "angle", "number", 160, 52, "bevel", "left", 40);
        Leader(200, 62, FlgEnd - 6, FTt + 4);
        DimH(WebEnd - 2 * Sc, WebEnd - Sc, FTb + Sc + 6, FTb, FTb + Sc);
        Slot("P80", "distance", "number", 250, 114, "scallop", "left", 40);
        Leader(290, 118, WebEnd - Sc - 2, FTb + Sc + 7);

        // ---- mặt bằng: WP
        int gEnd = PL - RingT - 2;
        DimH(WpL, gEnd, PT - 18, GT, PT);                                  // P28: dài WP
        Slot("P28", "distance", "number", 600, 8, "L (auto)", "left");
        // WP1 / WP2 (đổi bên 18/11/2026: WP1 = bên -W)
        Slot("P51", "integer", "number", 520, 104, "WP2", "top", 40);
        Leader(560, 114, 640, 114);
        Slot("P50", "integer", "number", 520, 316, "WP1", "top", 40);
        Leader(560, 326, 640, 326);
        // P81: đoạn rộng tới mép cột (từ mặt cột); P82: bề rộng đầu xa ngoài mép cánh
        DimH(WpC, PL, PT + 12, PT, PT);
        Slot("P81", "distance", "number", 662, 70, "L1 (auto)", "left", 44);
        DimV(GT - 40, GT, 588, WpL, WpL);
        Slot("P82", "distance", "number", 540, 152, "tip (auto)", "left", 40);
        Leader(580, 162, 586, 166);
        Slot("P11", "YesNo", "number", 500, 270, "Dao phia CGP", "top", 46);
        Leader(546, 280, 650, (GT + GB) / 2 + 7);
        Slot("P77", "YesNo", "number", 760, 404, "FB sym.", "left", 46);
        Leader(760, 414, gEnd - 4, PB + 2);

        End(bmp, Path.Combine(dir, p.Bitmap + ".bmp"));

        // ---- thuộc tính (ngoài hình), nhãn ngắn
        int x = p.PicX + p.PicW + 20, y = p.PicY + 2, lw = 90, fw = 110;
        AddRow(p, "MO (Y) / PIN (N)", "P14", "YesNo", "number", x, ref y, lw, fw);
        y += 6;
        p.Extra.Add(Label("CGP", x, y)); y += 22;
        AddRow(p, "Zone", "P12", "string", "text", x, ref y, lw, fw);
        AddRow(p, "Mat.", "P13", "material", "text", x, ref y, lw, fw);
        AddRow(p, "Start no.", "P52", "integer", "number", x, ref y, lw, fw);
        AddRow(p, "Slot o dam", "P79", "YesNo", "number", x, ref y, lw, fw);
        OptionRow(p, "CGP side", "P83", new[] { "+X / +Y", "+X / -Y", "-X / +Y", "-X / -Y", "Theo dam" }, 0, x, ref y, lw, fw);
        AddRow(p, "Bolt size", "P40_diameter", "bolt_size", "number", x, ref y, lw, fw);
        AddRow(p, "Standard", "P40_screwdin", "bolt_standard", "text", x, ref y, lw, fw);
        AddRow(p, "H 2 pitch", "P21B", "distance", "number", x, ref y, lw, fw);
        AddRow(p, "Bevel extra", "P23", "distance", "number", x, ref y, lw, fw);
        y += 6;
        p.Extra.Add(Label("WP / FB", x, y)); y += 22;
        AddRow(p, "Zone", "P54", "string", "text", x, ref y, lw, fw);
        AddRow(p, "Mark", "P76", "string", "text", x, ref y, lw, fw);
        AddRow(p, "Mat.", "P37", "material", "text", x, ref y, lw, fw);
        AddRow(p, "Start no.", "P53", "integer", "number", x, ref y, lw, fw);

        // ---- dải dưới hình (từ tab 2): vùng cắt gạch P62..P65 + bảng tra P78
        int by = p.PicY + p.PicH + 12, bx = p.PicX;
        string[,] cuts = { { "Web cut top", "P62" }, { "Web cut bot", "P63" }, { "CGP cut top", "P64" }, { "CGP cut bot", "P65" } };
        for (int i = 0; i < 4; i++)
        {
            int cx = bx + i * 170;
            p.Extra.Add(Label(cuts[i, 0], cx, by));
            p.Extra.Add(Param(cuts[i, 1], "YesNo", "number", cx + 80, by, 70));
        }
        int fy = by + 32;
        p.Extra.Add(Label("Bang tra (trong = COL_TO_GIRDER.txt)", bx, fy));
        p.Extra.Add(Param("P78", "file_in", "text", bx + 240, fy, 420));
        return p;
    }

    /// Ô danh sách chọn của Tekla: attribute("P83", "", option, ...) { value("A", 1) value("B", 0) ... } - giá trị lưu = số thứ tự (0..).
    private static void OptionRow(Page p, string label, string name, string[] options, int def, int x, ref int y, int lw, int fw)
    {
        p.Extra.Add(Label(label, x, y));
        p.Extra.Add("            attribute(\"" + name + "\", \"\", option, \"%s\", none, none, \"0.0\", \"0.0\", " + (x + lw) + ", " + y + ", " + fw + ")");
        p.Extra.Add("            {");
        for (int i = 0; i < options.Length; i++)
            p.Extra.Add("                value(\"" + options[i] + "\", " + (i == def ? 1 : 0) + ")");
        p.Extra.Add("            }");
        y += 27;
    }

    private static void AddRow(Page p, string label, string name, string type, string fmt, int x, ref int y, int lw, int fw)
    {
        p.Extra.Add(Label(label, x, y));
        p.Extra.Add(Param(name, type, fmt, x + lw, y, fw));
        y += 27;
    }

    // ------------------------------------------------------------------ tab 2

    private static Page Page2(string dir)
    {
        Page p = new Page();
        p.Title = " Parameters 2 ";
        p.Bitmap = "UEL_COL2GIR_2";
        Bitmap bmp = Begin(p, 480, 440);
        Elevation(440);

        // P61: cánh dưới -> mép trên ring dưới
        DimV(RBt, FBb, DimX, ColR + RingT, ColR + RingT);
        Slot("P61", "distance", "number", SlotX, (RBt + FBb) / 2 - 10, null, "left", 34);

        // vùng cắt (gạch): bụng dầm ở đầu (P62/P63), góc CGP sát cột (P64/P65)
        HatchRect(WebEnd - 14, FTb, 14, RTb - FTb);
        HatchRect(WebEnd - 14, RBt, 14, FBt - RBt);
        HatchRect(ColL - 16, CgpT, 16, 12);
        HatchRect(ColL - 16, CgpB - 12, 16, 12);
        Slot("P62", "YesNo", "number", 150, 100, "web cut", "left", 46);
        Leader(196, 110, WebEnd - 14, 108);
        Slot("P64", "YesNo", "number", 150, 150, "CGP cut", "left", 46);
        Leader(196, 160, ColL - 16, CgpT + 6);
        Slot("P65", "YesNo", "number", 150, 300, "CGP cut", "left", 46);
        Leader(196, 310, ColL - 16, CgpB - 6);
        Slot("P63", "YesNo", "number", 150, 340, "web cut", "left", 46);
        Leader(196, 350, WebEnd - 14, 345);
        End(bmp, Path.Combine(dir, p.Bitmap + ".bmp"));

        int x = p.PicX + p.PicW + 20, y = p.PicY + 4;
        p.Extra.Add(Label("Bang tra (trong = COL_TO_GIRDER.txt)", x, y)); y += 22;
        p.Extra.Add(Param("P78", "file_in", "text", x, y, 300));
        return p;
    }

    // ------------------------------------------------------------------ tab Advanced (không hình)

    private static Page Advanced()
    {
        Page p = new Page();
        p.Title = " Advanced ";
        p.Bitmap = null;
        int x0 = 20, lw = 110, cw = 200, gap = 20, y = 15;
        int[] cx = { x0 + lw, x0 + lw + cw + gap, x0 + lw + 2 * (cw + gap) };
        string[] head = { "CGP", "WP", "FB" };
        for (int i = 0; i < 3; i++) p.Extra.Add(Label(head[i], cx[i], y));
        y += 26;
        string[,] rows = {
            { "Name",   "P84", "P88", "P92" },
            { "Class",  "P85", "P89", "P93" },
            { "Finish", "P86", "P90", "P94" },
        };
        for (int r = 0; r < rows.GetLength(0); r++)
        {
            p.Extra.Add(Label(rows[r, 0], x0, y));
            for (int i = 0; i < 3; i++) p.Extra.Add(Param(rows[r, i + 1], "string", "text", cx[i], y, cw));
            y += 30;
        }
        y += 10;
        p.Extra.Add(Label("User fields (UDA):  TEN=GIA_TRI; TEN2=GIA_TRI2", x0, y)); y += 26;
        string[,] uda = { { "CGP", "P87" }, { "WP", "P91" }, { "FB", "P95" } };
        for (int i = 0; i < 3; i++)
        {
            p.Extra.Add(Label(uda[i, 0], x0, y));
            p.Extra.Add(Param(uda[i, 1], "string", "text", cx[0], y, 3 * cw + 2 * gap));
            y += 30;
        }
        y += 10;
        p.Extra.Add(Label("FB", x0, y)); y += 26;
        p.Extra.Add(Label("Profile", x0, y)); p.Extra.Add(Param("P96", "profile", "text", cx[0], y, cw)); y += 30;
        p.Extra.Add(Label("Material", x0, y)); p.Extra.Add(Param("P97", "material", "text", cx[0], y, cw)); y += 30;
        p.Extra.Add(Label("Start no.", x0, y)); p.Extra.Add(Param("P98", "integer", "number", cx[0], y, cw));
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
        sb.AppendLine("    joint(1, \"COL_TO_GIRDER\")");
        sb.AppendLine("    {");
        for (int i = 0; i < pages.Count; i++)
        {
            Page p = pages[i];
            sb.AppendLine("        tab_page(\"\", \"" + p.Title + "\", " + (i + 1) + ")");
            sb.AppendLine("        {");
            if (p.Bitmap != null)
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
