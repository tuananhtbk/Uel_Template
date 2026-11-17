// Dựng hình học cho liên kết Raker -> T-girder (dùng chung cho plugin DLL và EXE thử nghiệm).
//
// Cách hiểu (suy ra từ công thức trong .uel gốc, xem README):
//   - Bản đế (PL P1) đặt NGOÀI dầm T-girder: áp lên mặt bao của tiết diện (mặt ngoài cánh, hoặc mặt phẳng
//     qua mép 2 cánh nếu raker đi vào phía bụng). Kích thước: Top/Bott = tiết diện dầm chính + gap;
//     Left/Right = điểm xa nhất của raker trên mặt bản đế (dọc dầm chính) + gap.
//   - Raker fit vào mặt trên bản đế (chừa khe hàn P10 nếu có FB); bụng lùi lại P46; scallop ở góc bụng-cánh.
//   - Bản mã (PL P20) dựng trên bản đế, áp sát một bên bụng raker (lệch (tw+tg)/2): mặc định phía trong góc
//     primary/secondary (GussetSide), nhập P24 thì theo P24;
//     bu lông P31 x P34 qua bụng raker, hàng đầu cách đầu bụng P29 dọc trục raker.
//   - Cánh hàn CJP công trường: vát mặt TRÊN của cả 2 cánh (góc P12, khe chân P10, mặt chân 2 mm - như D53/D58/D108 gốc).
//   - FB (thanh lót) ở phía chân mối hàn: dưới mặt trong cánh trên, dưới mặt ngoài cánh dưới; một cạnh tì vào bản đế.
//   - Bản mã: giữ nguyên hình dạng, không cắt; đẩy cả tấm dọc trục raker tới khi góc sâu nhất chạm mặt bản đế.
//   - FB: cạnh tì dựng lấn vào bản đế rồi cắt bỏ theo mặt trên bản đế (Reach + TrimAtBasePlate) -> áp khít bản đế.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSMPlane = Tekla.Structures.Model.Plane;

using UEL.Core;

namespace UEL.Core.Components.RakerTG
{
    public class RakerTGBuilder
    {
        private readonly Model _model;
        private readonly RakerTGParams _p;
        private readonly List<ModelObject> _created = new List<ModelObject>();

        private V3 _n;              // pháp tuyến bản đế trong hệ cục bộ (hướng về phía raker)
        private V3 _yp, _zp;        // 2 trục nằm trong mặt bản đế
        private bool _gussetPos;    // bản mã ở phía +Z cục bộ (true) hay -Z (false) của bụng raker
        private double _up = 1.0;   // +1: trục Y cục bộ của raker hướng lên (cánh +Y là cánh trên); -1: ngược lại

        // Hệ cục bộ đầu raker (toàn cục) + dữ liệu dầm chính, để tính kích thước bản đế theo primary (CreateBasePlate)
        private V3 _A, _X, _Y, _Z;
        private V3 _topG;           // hướng "Top" của bản đế (toàn cục): theo chiều tiết diện dầm chính, hướng lên
        private V3 _g0;             // 1 điểm trên trục dầm chính
        private double _primHalf;   // nửa kích thước tiết diện dầm chính theo _topG

        public RakerTGBuilder(Model model, RakerTGParams p)
        {
            _model = model;
            _p = p;
        }

        /// Các đối tượng đã tạo (EXE dùng để xoá khi dựng lại).
        public List<ModelObject> Created { get { return _created; } }

        public bool Build(Beam girder, Beam raker)
        {
            Logger.Log("Thong so: " + _p);
            WorkPlaneHandler wph = _model.GetWorkPlaneHandler();
            TransformationPlane original = wph.GetCurrentTransformationPlane();
            try
            {
                wph.SetCurrentTransformationPlane(new TransformationPlane());   // làm việc ở hệ toàn cục
                return BuildInGlobal(wph, girder, raker);
            }
            finally
            {
                wph.SetCurrentTransformationPlane(original);
            }
        }

        private bool BuildInGlobal(WorkPlaneHandler wph, Beam girder, Beam raker)
        {
            // ---- 1. Trục 2 cấu kiện ----
            V3 g0, g1, r0, r1;
            if (!CenterLine(girder, out g0, out g1) || !CenterLine(raker, out r0, out r1))
            {
                Logger.Log("Khong doc duoc truc cau kien.");
                return false;
            }
            Line girderAxis = new Line(g0.ToPoint(), g1.ToPoint());
            bool r1IsNear = Distance.PointToLine(r1.ToPoint(), girderAxis) < Distance.PointToLine(r0.ToPoint(), girderAxis);
            V3 near = r1IsNear ? r1 : r0;
            V3 far = r1IsNear ? r0 : r1;
            V3 dir = (near - far).Unit();                       // từ raker hướng vào dầm

            // ---- 2. Hệ trục dầm T-girder ----
            CoordinateSystem csG = girder.GetCoordinateSystem();
            V3 gx = (g1 - g0).Unit();
            V3 gy = V3.From(csG.AxisY).OrthoTo(gx).Unit();      // chiều cao tiết diện
            V3 gz = gx.Cross(gy).Unit();                        // bề rộng cánh
            double hg = Report(girder, "PROFILE.HEIGHT");
            double bg = Report(girder, "PROFILE.WIDTH");

            // ---- 3. Mặt đặt bản đế = mặt bao NGOÀI của tiết diện dầm mà trục raker đi vào ----
            V3 contact, normal;
            if (!EnvelopeEntry(far, dir, g0, gy, gz, hg, bg, out contact, out normal))
            {
                Logger.Log("Truc raker khong di vao mat bao cua dam T-girder (kiem tra lai cach chon dam).");
                return false;
            }
            Logger.Log(string.Format(CultureInfo.InvariantCulture,
                "Mat dat ban de: {0} (girder H={1}, B={2})", Math.Abs(normal.Dot(gy)) > 0.5 ? "mat ngoai canh" : "mat qua mep 2 canh", hg, bg));

            // ---- 4. Hệ toạ độ cục bộ tại đầu raker ----
            //   gốc A = giao trục raker với mặt trên bản đế, X dọc raker (ra xa dầm), Y = chiều cao tiết diện raker
            V3 plateTop = contact + normal * _p.BasePlateThk;
            V3 X = (far - near).Unit();
            double t = (plateTop - far).Dot(normal) / X.Dot(normal);
            V3 A = far + X * t;
            CoordinateSystem csR = raker.GetCoordinateSystem();
            V3 Y = V3.From(csR.AxisY).OrthoTo(X).Unit();
            V3 Z = X.Cross(Y).Unit();
            _A = A; _X = X; _Y = Y; _Z = Z;
            _up = Y.Z >= 0 ? 1.0 : -1.0;
            if (Math.Abs(Y.Z) < 0.05)
                Logger.Log("CANH BAO: raker xoay ~90 do quanh truc, khong ro canh tren/duoi - lay canh +Y cuc bo lam canh tren.");

            // Top/Bott của bản đế đo theo dầm chính: mặt đặt là mặt ngoài cánh (pháp tuyến ~gy) -> chiều bề rộng cánh gz;
            // mặt qua mép 2 cánh (pháp tuyến ~gz) -> chiều cao dầm gy. "Top" = hướng có thành phần Z toàn cục dương.
            bool onFlange = Math.Abs(normal.Dot(gy)) > 0.5;
            _topG = onFlange ? gz : gy;
            _primHalf = onFlange ? bg / 2.0 : hg / 2.0;
            if (_topG.Z < -1e-6) _topG = _topG * -1.0;
            _g0 = g0;

            _n = new V3(normal.Dot(X), normal.Dot(Y), normal.Dot(Z));
            if (_n.X < 0.1)
            {
                Logger.Log("Raker gan nhu song song voi mat dam, khong dung duoc ban de.");
                return false;
            }
            _yp = new V3(0, 1, 0).OrthoTo(_n).Unit();
            _zp = _n.Cross(_yp).Unit();
            if (_zp.Z < 0) _zp = _zp * -1.0;

            double angleDeg = Math.Acos(Math.Min(1.0, _n.X)) * 180.0 / Math.PI;   // tương ứng P14 trong .uel gốc
            Logger.Log(string.Format(CultureInfo.InvariantCulture, "Goc raker - phap tuyen ban de (P14) = {0:0.###} do", angleDeg));
            _gussetPos = GussetSide();

            double h = Report(raker, "PROFILE.HEIGHT");
            double b = Report(raker, "PROFILE.WIDTH");
            double tw = Report(raker, "PROFILE.WEB_THICKNESS");
            double tf = Report(raker, "PROFILE.FLANGE_THICKNESS");
            if (h <= 0 || b <= 0 || tw <= 0 || tf <= 0)
            {
                Logger.Log("Raker phai la tiet dien H/I (khong doc duoc HEIGHT/WIDTH/WEB/FLANGE).");
                return false;
            }

            wph.SetCurrentTransformationPlane(new TransformationPlane(A.ToPoint(), X.ToVector(), Y.ToVector()));

            // ---- 5. Tạo cấu kiện (toạ độ cục bộ) ----
            double rootGap = _p.FlatBar ? _p.FlangeWeldGap : 0.0;
            double webEnd = Math.Max(_p.WebGap, rootGap);       // khoảng cách đầu bụng tới mặt bản đế
            double yi = h / 2.0 - tf;                           // mặt trong cánh

            ContourPlate basePlate = CreateBasePlate(h, b);
            if (basePlate == null) return false;

            FitRaker(raker, rootGap);
            // Vát mép hàn CJP ở mặt TRÊN của cả 2 cánh (P10, P12; .uel gốc D53/D58/D108):
            //   cánh trên: chân (root) ở mặt trong, vát mở lên mặt ngoài; cánh dưới: chân ở mặt ngoài, vát mở lên mặt trong.
            double s = _up;
            CutBevel(raker, rootGap, s * yi, s * h / 2.0, b);
            CutBevel(raker, rootGap, -s * h / 2.0, -s * yi, b);
            if (webEnd > rootGap + 0.5) CutWeb(raker, yi, tw, rootGap, webEnd);
            if (_p.TopScallop) CutScallop(raker, yi, tw, webEnd, +1);
            if (_p.BottScallop) CutScallop(raker, yi, tw, webEnd, -1);

            ContourPlate gusset = CreateGusset(tw, webEnd);
            if (gusset == null) return false;
            CreateBolts(raker, gusset, tw, webEnd);

            List<ContourPlate> bars = new List<ContourPlate>();
            if (_p.FlatBar)
            {
                // thanh lót nằm ở phía CHÂN mối hàn (đối diện mặt vát): cánh trên -> dưới mặt trong cánh trên;
                // cánh dưới -> dưới mặt ngoài cánh dưới (yêu cầu người dùng 17/11/2026)
                bars.Add(CreateFlatBar(_up * yi, b));
                bars.Add(CreateFlatBar(-_up * h / 2.0, b));
            }

            // ---- 6. Mối hàn ----
            Fillet(girder, basePlate, true);
            Fillet(basePlate, gusset, true);
            foreach (ContourPlate fb in bars) if (fb != null) Fillet(raker, fb, true);
            FlangeCjp(raker, basePlate);
            return true;
        }

        /// Bên đặt bản mã (theo hướng lắp raker, yêu cầu người dùng 17/11/2026):
        ///   mặc định PHÍA TRONG góc nhọn giữa primary (T-girder) và secondary (raker). Raker đi lệch ngang so với
        ///   bản đế (pháp tuyến bản đế có thành phần Z cục bộ nz) thì mặt bản đế ngả về phía bụng raker ở bên -sign(nz):
        ///   bên đó là góc nhọn = phía trong.
        ///   Người dùng nhập P24 -> theo P24. Raker không lệch ngang (|nz| nhỏ) -> không có "phía trong" rõ ràng -> theo P24.
        private bool GussetSide()
        {
            if (!_p.GussetSideAuto)
            {
                Logger.Log("Ban ma: theo P24 = " + (_p.GussetOnPositiveSide ? "1" : "0"));
                return _p.GussetOnPositiveSide;
            }
            if (Math.Abs(_n.Z) < 0.02)
            {
                Logger.Log("Ban ma: raker khong lech ngang so voi ban de -> khong xac dinh phia trong, theo P24 = " +
                           (_p.GussetOnPositiveSide ? "1" : "0"));
                return _p.GussetOnPositiveSide;
            }
            bool pos = _n.Z < 0;
            Logger.Log(string.Format(CultureInfo.InvariantCulture,
                "Ban ma: tu dong dat phia trong goc primary/secondary (ben {0}Z cuc bo, nz={1:0.###}). Nhap P24 de chon ben khac.",
                pos ? "+" : "-", _n.Z));
            return pos;
        }

        /// Tia từ "far" theo "dir" đi vào hộp bao tiết diện dầm (|y| <= hg/2, |z| <= bg/2, trục x vô hạn).
        /// Trả về điểm vào và pháp tuyến mặt vào (hướng ra ngoài dầm, về phía raker).
        private static bool EnvelopeEntry(V3 far, V3 dir, V3 g0, V3 gy, V3 gz, double hg, double bg,
                                          out V3 contact, out V3 normal)
        {
            contact = far; normal = gy;
            if (hg <= 0 || bg <= 0) return false;
            V3 rel = far - g0;
            double fy = rel.Dot(gy), fz = rel.Dot(gz);
            double dy = dir.Dot(gy), dz = dir.Dot(gz);

            double inY, outY, inZ, outZ;
            if (!Slab(fy, dy, hg / 2.0, out inY, out outY)) return false;
            if (!Slab(fz, dz, bg / 2.0, out inZ, out outZ)) return false;
            double tIn = Math.Max(inY, inZ), tOut = Math.Min(outY, outZ);
            if (tIn > tOut || tOut < 0 || double.IsInfinity(tIn)) return false;

            contact = far + dir * tIn;
            normal = inY >= inZ ? gy * -Math.Sign(dy) : gz * -Math.Sign(dz);
            return true;
        }

        private static bool Slab(double start, double d, double half, out double tIn, out double tOut)
        {
            if (Math.Abs(d) < 1e-9)
            {
                tIn = double.NegativeInfinity; tOut = double.PositiveInfinity;
                return Math.Abs(start) <= half;
            }
            double t1 = (-half - start) / d, t2 = (half - start) / d;
            tIn = Math.Min(t1, t2); tOut = Math.Max(t1, t2);
            return true;
        }

        // ----------------------------------------------------------------- cấu kiện

        /// Kích thước bản đế (yêu cầu người dùng 17/11/2026):
        ///   Top / Bott (P6 / P8): lấy DẦM CHÍNH làm chuẩn - mép bản đế = mép tiết diện dầm chính (theo chiều "Top")
        ///                         + gap ra ngoài.
        ///   Left / Right (P7 / P19): lấy ĐIỂM XA NHẤT của raker trên mặt bản đế làm chuẩn (theo phương dọc dầm chính)
        ///                         + gap ra ngoài. Left/Right khi nhìn từ phía raker vào bản đế, Top hướng lên.
        ///   Gap dương = bản đế rộng ra ngoài mốc, âm = thụt vào.
        private ContourPlate CreateBasePlate(double h, double b)
        {
            V3 T = Local(_topG);                                // nằm trong mặt bản đế (vuông góc pháp tuyến)
            V3 R = T.Cross(_n).Unit();                          // "Right" nhìn từ phía raker
            T = _n.Cross(R).Unit();

            // Top / Bott theo dầm chính
            double c = LocalPt(_g0).Dot(T);                     // trục dầm chính, đo theo T
            double top = c + _primHalf + _p.TopGap;
            double bot = c - _primHalf - _p.BottGap;

            // Left / Right theo điểm xa nhất của raker trên mặt bản đế
            double rmin = double.MaxValue, rmax = double.MinValue, tmin = double.MaxValue, tmax = double.MinValue;
            foreach (double y in new[] { h / 2, -h / 2 })
                foreach (double z in new[] { b / 2, -b / 2 })
                {
                    V3 q = OnPlane(0.0, y, z);
                    rmin = Math.Min(rmin, q.Dot(R)); rmax = Math.Max(rmax, q.Dot(R));
                    tmin = Math.Min(tmin, q.Dot(T)); tmax = Math.Max(tmax, q.Dot(T));
                }
            double right = rmax + _p.RightGap;
            double left = rmin - _p.LeftGap;

            Logger.Log(string.Format(CultureInfo.InvariantCulture,
                "Ban de: cao {0:0.#} (dam chinh {1:0.#} + Top {2:0.#} + Bott {3:0.#}), rong {4:0.#} (raker {5:0.#} + Left {6:0.#} + Right {7:0.#})",
                top - bot, 2 * _primHalf, _p.TopGap, _p.BottGap, right - left, rmax - rmin, _p.LeftGap, _p.RightGap));
            if (tmax > top + 0.5 || tmin < bot - 0.5)
                Logger.Log("CANH BAO: raker vuot ra ngoai ban de theo chieu Top/Bott - tang P6/P8.");
            if (top - bot < 1.0 || right - left < 1.0)
            {
                Logger.Log("Ban de co kich thuoc <= 0 - kiem tra P6/P7/P8/P19.");
                return null;
            }

            V3 mid = _n * (-_p.BasePlateThk / 2.0);             // mặt giữa bản đế
            List<V3> pts = new List<V3>();
            pts.Add(T * bot + R * left + mid);
            pts.Add(T * top + R * left + mid);
            pts.Add(T * top + R * right + mid);
            pts.Add(T * bot + R * right + mid);
            return Plate(pts, _p.BasePlateThk, _p.BasePlateMat, _p.BasePlateClass, _p.BasePlateMark, "PLATE");
        }

        /// Vector toàn cục -> hệ cục bộ đầu raker.
        private V3 Local(V3 v)
        {
            return new V3(v.Dot(_X), v.Dot(_Y), v.Dot(_Z));
        }

        /// Điểm toàn cục -> hệ cục bộ đầu raker.
        private V3 LocalPt(V3 p)
        {
            return Local(p - _A);
        }

        private ContourPlate CreateGusset(double tw, double webEnd)
        {
            double side = _gussetPos ? 1.0 : -1.0;
            double z0 = side * (tw + _p.GussetThk) / 2.0;
            double H = 0.5 * (_p.BoltQtyY - 1) * _p.BoltDistY + _p.BoltEdgeY;
            double xw = webEnd / _n.X;
            double xf = xw + _p.BoltToWebEnd + (_p.BoltQtyX - 1) * _p.BoltDistX + _p.BoltEdgeX;

            V3 nearTop = OnPlane(0.0, H, z0);
            V3 nearBot = OnPlane(0.0, -H, z0);
            if (xf <= Math.Max(nearTop.X, nearBot.X))
            {
                Logger.Log("Ban ma qua ngan: tang P29/P44 hoac giam goc nghieng.");
                return null;
            }

            // Giữ nguyên hình dạng bản mã, KHÔNG cắt (yêu cầu người dùng 17/11/2026): đẩy cả tấm dọc trục raker (X,
            // hướng của secondary) ra xa bản đế tới khi góc sâu nhất của cạnh ngắn vừa chạm mặt trên bản đế.
            // Cạnh ngắn ở trên tính tại mặt giữa chiều dày; khi bản đế nghiêng so với bản mã thì 1 mặt cắm vào bản đế
            // (đo trên model: 10,29 mm) -> đẩy đúng phần đó. Mặt bên kia còn hở 1 khe nêm.
            double half = _p.GussetThk / 2.0;
            double push = 0.0;
            foreach (V3 q in new[] { nearTop, nearBot })
                foreach (double s in new[] { -1.0, 1.0 })
                    push = Math.Max(push, OnPlane(0.0, q.Y, q.Z + s * half).X - q.X);
            V3 shift = new V3(push, 0, 0);
            Logger.Log(string.Format(CultureInfo.InvariantCulture, "Ban ma: day doc truc raker {0:0.##} mm de canh ngan cham ban de", push));

            List<V3> pts = new List<V3>();
            pts.Add(new V3(xf, H, z0) + shift);
            pts.Add(new V3(xf, -H, z0) + shift);
            pts.Add(nearBot + shift);
            pts.Add(nearTop + shift);
            return Plate(pts, _p.GussetThk, _p.GussetMat, _p.GussetClass, _p.GussetMark, "PLATE");
        }

        /// FB (thanh lót mối hàn cánh) nằm NGANG, áp vào mặt cánh ở cao độ yFace, thân thanh nằm PHÍA DƯỚI mặt đó
        /// (17/11/2026, theo ảnh người dùng): dày FlatBarThk theo chiều cao tiết diện (Y);
        ///   rộng FlatBarWidth dọc trục raker (X), một cạnh chạm mặt trên bản đế; dài b theo bề rộng cánh (Z).
        /// Cánh trên: yFace = mặt trong cánh trên. Cánh dưới: yFace = mặt ngoài cánh dưới.
        private ContourPlate CreateFlatBar(double yFace, double b)
        {
            double ym = yFace - _up * _p.FlatBarThk / 2.0;     // mặt giữa thanh (thanh ở phía dưới mặt yFace)
            double halfT = _p.FlatBarThk / 2.0;
            // cạnh tì: lấn vào bản đế rồi cắt theo mặt bản đế; cạnh kia: cách mặt bản đế FlatBarWidth dọc trục raker
            V3 a = Reach(ym, -b / 2, halfT, 0.0);
            V3 c = Reach(ym, b / 2, halfT, 0.0);
            V3 w = new V3(_p.FlatBarWidth, 0, 0);
            List<V3> pts = new List<V3>();
            pts.Add(a);
            pts.Add(c);
            pts.Add(OnPlane(0.0, ym, b / 2) + w);
            pts.Add(OnPlane(0.0, ym, -b / 2) + w);
            ContourPlate fb = Plate(pts, _p.FlatBarThk, _p.FlatBarMat, _p.FlatBarClass, _p.FlatBarMark, "FLAT_BAR");
            TrimAtBasePlate(fb);
            return fb;
        }

        private void FitRaker(Beam raker, double rootGap)
        {
            Fitting fit = new Fitting();
            fit.Father = raker;
            TSMPlane pl = new TSMPlane();
            pl.Origin = (_n * rootGap).ToPoint();
            pl.AxisX = _yp.ToVector();
            pl.AxisY = _zp.ToVector();
            fit.Plane = pl;
            if (fit.Insert()) _created.Add(fit);
        }

        /// Vát mép hàn CJP của 1 cánh raker (.uel gốc: D53/D58/D108 = P10 + (P17-2)*tan(P12), P17 = dày cánh):
        ///   đầu cánh ở mặt fitting (cách mặt bản đế rootGap); chừa mặt chân 2 mm ở phía yRoot; phần còn lại tới mặt yUp
        ///   vát góc P12 so với mặt bản đế, mở về phía yUp. Khối cắt là lăng trụ chạy dọc mép chân (nằm trong mặt fitting).
        private void CutBevel(Beam raker, double rootGap, double yRoot, double yUp, double b)
        {
            const double rootFace = 2.0, extra = 3.0;
            double a = _p.WeldAngle * Math.PI / 180.0;
            double depth = Math.Abs(yUp - yRoot);
            if (_p.WeldAngle <= 0.1 || _p.WeldAngle >= 89.0 || depth <= rootFace + 0.5) return;
            double sg = Math.Sign(yUp - yRoot);

            V3 Yv = new V3(0, 1, 0);
            V3 w = _n.Cross(Yv).Unit();                         // phương mép chân: trong mặt fitting, vuông góc Y
            V3 u = Yv.Cross(w).Unit();                          // trong mặt cắt ngang, vuông góc Y, hướng ra xa bản đế
            if (u.Dot(_n) < 0) u = u * -1.0;
            double nu = _n.Dot(u);
            if (nu < 1e-3 || Math.Abs(w.Z) < 1e-3) return;

            V3 q0 = OnPlane(rootGap, yRoot + sg * rootFace, 0.0);              // đầu đường vát (trên mặt chân 2 mm)
            V3 fdir = (Yv * sg + u * (-sg * _n.Y / nu)).Unit();                // vết mặt fitting trong mặt cắt, hướng về yUp
            V3 qn = (u - fdir * u.Dot(fdir)).Unit();                           // vuông góc fdir, hướng ra xa bản đế
            V3 bdir = fdir * Math.Cos(a) + qn * Math.Sin(a);                   // đường vát
            double rise = depth - rootFace + extra;                            // vượt qua mặt yUp 3 mm
            V3 pFit = q0 + fdir * (rise / Math.Abs(fdir.Y));
            V3 pBev = q0 + bdir * (rise / Math.Abs(bdir.Y));

            List<V3> pts = new List<V3>();
            pts.Add(q0 - u * extra);
            pts.Add(q0);
            pts.Add(pBev);
            pts.Add(pFit - u * extra);
            double len = b / Math.Abs(w.Z) + 40.0;                            // phủ hết bề rộng cánh dọc theo w
            Cut(raker, pts, len);
            Logger.Log(string.Format(CultureInfo.InvariantCulture,
                "Vat mep canh {0}: goc {1:0.#} do, khe chan {2:0.#}, mat chan 2 mm, lui mep tren {3:0.#} mm",
                yRoot * _up > 0 ? "tren (vat mat ngoai)" : "duoi (vat mat trong)", _p.WeldAngle, rootGap, (depth - rootFace) * Math.Tan(a)));
        }

        private void CutWeb(Beam raker, double yi, double tw, double from, double to)
        {
            List<V3> pts = new List<V3>();
            pts.Add(OnPlane(from - 50.0, yi, 0));
            pts.Add(OnPlane(to, yi, 0));
            pts.Add(OnPlane(to, -yi, 0));
            pts.Add(OnPlane(from - 50.0, -yi, 0));
            Cut(raker, pts, tw + 4.0);
        }

        private void CutScallop(Beam raker, double yi, double tw, double webEnd, int flange)
        {
            V3 c = OnPlane(webEnd, flange * yi, 0);
            double r = _p.ScallopRadius;
            List<V3> pts = new List<V3>();
            for (int deg = 0; deg <= 180; deg += 15)
            {
                double a = deg * Math.PI / 180.0;
                pts.Add(new V3(c.X + r * Math.Cos(a), c.Y - flange * r * Math.Sin(a), 0));
            }
            Cut(raker, pts, tw + 4.0);
        }

        private void CreateBolts(Beam raker, ContourPlate gusset, double tw, double webEnd)
        {
            double x0 = webEnd / _n.X + _p.BoltToWebEnd;
            BoltArray bolts = new BoltArray();
            bolts.PartToBoltTo = gusset;
            bolts.PartToBeBolted = raker;
            bolts.FirstPosition = new Point(x0, 0, 0);
            bolts.SecondPosition = new Point(x0 + 100.0, 0, 0);
            bolts.BoltSize = _p.BoltSize;
            bolts.BoltStandard = _p.BoltStandard;
            bolts.Tolerance = _p.Tolerance;
            bolts.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
            bolts.CutLength = 2.0 * (tw + _p.GussetThk) + 60.0;
            bolts.ExtraLength = 15.0;
            bolts.Position.Depth = Position.DepthEnum.MIDDLE;
            bolts.Position.Plane = Position.PlaneEnum.MIDDLE;
            bolts.Position.Rotation = Position.RotationEnum.FRONT;   // thân bu lông theo trục Z cục bộ (xuyên bụng)
            bolts.Bolt = true;
            bolts.Washer1 = true; bolts.Washer2 = true; bolts.Washer3 = true;
            bolts.Nut1 = true; bolts.Nut2 = false;

            if (_p.BoltQtyX <= 1) bolts.AddBoltDistX(0.0);
            else for (int i = 1; i < _p.BoltQtyX; i++) bolts.AddBoltDistX(_p.BoltDistX);
            if (_p.BoltQtyY <= 1) bolts.AddBoltDistY(0.0);
            else for (int i = 1; i < _p.BoltQtyY; i++) bolts.AddBoltDistY(_p.BoltDistY);

            if (_p.SlotX > 0 || _p.SlotY > 0)
            {
                bolts.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_SLOTTED;
                bolts.SlottedHoleX = _p.SlotX;
                bolts.SlottedHoleY = _p.SlotY;
                // .uel gốc: if P24==1 then 01000 else 10000  -> lỗ ô van ở part 2 hoặc part 1
                bolts.Hole1 = !_gussetPos;
                bolts.Hole2 = _gussetPos;
            }
            if (bolts.Insert()) _created.Add(bolts);
            else Logger.Log("Khong tao duoc nhom bu long.");
        }

        // ----------------------------------------------------------------- mối hàn

        private void Fillet(ModelObject main, ModelObject secondary, bool shop)
        {
            if (main == null || secondary == null) return;
            Weld w = new Weld();
            w.MainObject = main;
            w.SecondaryObject = secondary;
            w.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            w.SizeAbove = _p.FilletSize;
            w.TypeBelow = BaseWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            w.SizeBelow = _p.FilletSize;
            w.ShopWeld = shop;
            if (w.Insert()) _created.Add(w);
        }

        private void FlangeCjp(Beam raker, ContourPlate basePlate)
        {
            Weld w = new Weld();
            w.MainObject = raker;
            w.SecondaryObject = basePlate;
            w.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_BEVEL_GROOVE_SINGLE_BEVEL_BUTT;
            w.AngleAbove = _p.WeldAngle;
            w.RootOpeningAbove = _p.FlatBar ? _p.FlangeWeldGap : 0.0;
            w.ShopWeld = false;                                 // hàn công trường
            if (w.Insert()) _created.Add(w);
        }

        // ----------------------------------------------------------------- tiện ích

        /// Điểm (x, y, z) nằm trên mặt phẳng song song bản đế, cách mặt trên bản đế khoảng d (dọc pháp tuyến).
        private V3 OnPlane(double d, double y, double z)
        {
            return new V3((d - y * _n.Y - z * _n.Z) / _n.X, y, z);
        }

        /// Điểm (x, y, z) trên cạnh tì vào bản đế của một tấm dày (cắt ngang tại y, z):
        /// x = NHỎ nhất trong các góc (y +/- halfY, z +/- halfZ) của mặt cắt dày tấm, lấn thêm 1 mm vào bản đế
        /// => mọi mặt của tấm đều chạm tới bản đế; phần lấn vào bản đế được TrimAtBasePlate cắt bỏ
        /// -> cạnh tì áp khít mặt trên bản đế ở cả 2 mặt tấm, kể cả khi bản đế nghiêng so với tấm.
        /// (17/11/2026: đo trên model, bản mã PL30 nghiêng 46,7 độ so với bản đế, 2 mặt lệch -10,29 / +10,29 mm.)
        private V3 Reach(double y, double z, double halfY, double halfZ)
        {
            double x = double.MaxValue;
            foreach (double sy in new[] { -1.0, 1.0 })
                foreach (double sz in new[] { -1.0, 1.0 })
                    x = Math.Min(x, OnPlane(-1.0, y + sy * halfY, z + sz * halfZ).X);
            return new V3(x, y, z);
        }

        /// Cắt bỏ phần của part nằm sau mặt trên bản đế (phía bản đế / dầm): khối cắt dày 500 mm,
        /// mặt trước trùng mặt trên bản đế, rộng 6 m.
        private void TrimAtBasePlate(Part part)
        {
            if (part == null) return;
            const double depth = 500.0, half = 3000.0;
            V3 mid = _n * (-depth / 2.0);
            List<V3> pts = new List<V3>();
            pts.Add(_yp * -half + _zp * -half + mid);
            pts.Add(_yp * half + _zp * -half + mid);
            pts.Add(_yp * half + _zp * half + mid);
            pts.Add(_yp * -half + _zp * half + mid);
            Cut(part, pts, depth);
        }

        private ContourPlate Plate(List<V3> pts, double thk, string mat, int cls, string prefix, string name)
        {
            ContourPlate cp = new ContourPlate();
            foreach (V3 q in pts) cp.AddContourPoint(new ContourPoint(q.ToPoint(), null));
            cp.Profile.ProfileString = "PL" + thk.ToString("0.###", CultureInfo.InvariantCulture);
            cp.Material.MaterialString = mat;
            cp.Class = cls.ToString(CultureInfo.InvariantCulture);
            cp.Name = name;
            cp.PartNumber.Prefix = prefix;
            cp.PartNumber.StartNumber = 1;
            cp.Position.Depth = Position.DepthEnum.MIDDLE;
            if (!cp.Insert())
            {
                Logger.Log("Khong tao duoc " + name + " (" + prefix + ").");
                return null;
            }
            _created.Add(cp);

            // Lỗi đã gặp (16/11/2026): trong plugin, tấm ra class 99 = "Class" ở tab General của connection
            // (joint_attributes.group_no). Đọc lại; nếu bị đổi thì gán lại bằng Modify() và ghi log.
            ContourPlate check = new ContourPlate();
            check.Identifier = cp.Identifier;
            if (check.Select())
            {
                string got = check.Class;
                if (got != cp.Class)
                {
                    check.Class = cp.Class;
                    check.Modify();
                    check.Select();
                }
                Logger.Log("Tao " + name + " " + cp.Profile.ProfileString + " mark=" + prefix +
                           " class dat=" + cp.Class + " doc lai=" + got + (got != cp.Class ? " -> sau Modify=" + check.Class : ""));
            }
            return cp;
        }

        private void Cut(Part father, List<V3> pts, double thk)
        {
            ContourPlate cutter = new ContourPlate();
            foreach (V3 q in pts) cutter.AddContourPoint(new ContourPoint(q.ToPoint(), null));
            cutter.Profile.ProfileString = "PL" + thk.ToString("0.###", CultureInfo.InvariantCulture);
            cutter.Material.MaterialString = "STEEL_UNDEFINED";
            cutter.Class = BooleanPart.BooleanOperativeClassName;
            cutter.Position.Depth = Position.DepthEnum.MIDDLE;
            if (!cutter.Insert()) return;
            BooleanPart bp = new BooleanPart();
            bp.Father = father;
            bp.SetOperativePart(cutter);
            bp.Type = BooleanPart.BooleanTypeEnum.BOOLEAN_CUT;
            if (bp.Insert()) _created.Add(bp);
            cutter.Delete();
        }

        private static bool CenterLine(Part part, out V3 a, out V3 b)
        {
            a = b = new V3(0, 0, 0);
            ArrayList cl = part.GetCenterLine(false);
            if (cl == null || cl.Count < 2) return false;
            a = V3.From((Point)cl[0]);
            b = V3.From((Point)cl[cl.Count - 1]);
            return true;
        }

        private static double Report(Part part, string name)
        {
            double value = 0.0;
            part.GetReportProperty(name, ref value);
            return value;
        }
    }

    /// Vector 3D tối giản (tránh phụ thuộc vào toán tử của Geometry3d bản 18).
    internal struct V3
    {
        public readonly double X, Y, Z;
        public V3(double x, double y, double z) { X = x; Y = y; Z = z; }
        public static V3 From(Point p) { return new V3(p.X, p.Y, p.Z); }
        public static V3 operator +(V3 a, V3 b) { return new V3(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }
        public static V3 operator -(V3 a, V3 b) { return new V3(a.X - b.X, a.Y - b.Y, a.Z - b.Z); }
        public static V3 operator *(V3 a, double k) { return new V3(a.X * k, a.Y * k, a.Z * k); }
        public double Dot(V3 o) { return X * o.X + Y * o.Y + Z * o.Z; }
        public V3 Cross(V3 o) { return new V3(Y * o.Z - Z * o.Y, Z * o.X - X * o.Z, X * o.Y - Y * o.X); }
        public double Norm() { return Math.Sqrt(Dot(this)); }
        public V3 Unit() { double n = Norm(); return n < 1e-12 ? this : this * (1.0 / n); }
        public V3 OrthoTo(V3 axis) { V3 u = axis.Unit(); return this - u * Dot(u); }
        public Point ToPoint() { return new Point(X, Y, Z); }
        public Vector ToVector() { return new Vector(X, Y, Z); }
    }
}
