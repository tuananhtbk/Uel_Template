// Dựng hình học cho liên kết Raker -> T-girder (dùng chung cho plugin DLL và EXE thử nghiệm).
//
// Cách hiểu (suy ra từ công thức trong .uel gốc, xem README):
//   - Bản đế (PL P1) đặt NGOÀI dầm T-girder: áp lên mặt bao của tiết diện (mặt ngoài cánh, hoặc mặt phẳng
//     qua mép 2 cánh nếu raker đi vào phía bụng). Kích thước = hình chiếu tiết diện raker + Top/Bott/Left/Right gap.
//   - Raker fit vào mặt trên bản đế (chừa khe hàn P10 nếu có FB); bụng lùi lại P46; scallop ở góc bụng-cánh.
//   - Bản mã (PL P20) dựng trên bản đế, áp sát một bên bụng raker (lệch (tw+tg)/2 sang bên P24),
//     bu lông P31 x P34 qua bụng raker, hàng đầu cách đầu bụng P29 dọc trục raker.
//   - FB (thanh lót) trên bản đế, sát mặt trong mỗi cánh raker; cánh hàn CJP công trường (vát P12, khe P10).
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
            if (webEnd > rootGap + 0.5) CutWeb(raker, yi, tw, rootGap, webEnd);
            if (_p.TopScallop) CutScallop(raker, yi, tw, webEnd, +1);
            if (_p.BottScallop) CutScallop(raker, yi, tw, webEnd, -1);

            ContourPlate gusset = CreateGusset(tw, webEnd);
            if (gusset == null) return false;
            CreateBolts(raker, gusset, tw, webEnd);

            List<ContourPlate> bars = new List<ContourPlate>();
            if (_p.FlatBar)
            {
                bars.Add(CreateFlatBar(yi, b, +1));
                bars.Add(CreateFlatBar(yi, b, -1));
            }

            // ---- 6. Mối hàn ----
            Fillet(girder, basePlate, true);
            Fillet(basePlate, gusset, true);
            foreach (ContourPlate fb in bars) if (fb != null) Fillet(raker, fb, true);
            FlangeCjp(raker, basePlate);
            return true;
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

        private ContourPlate CreateBasePlate(double h, double b)
        {
            double umin = double.MaxValue, umax = double.MinValue, vmin = double.MaxValue, vmax = double.MinValue;
            double[] ys = { h / 2, -h / 2 };
            double[] zs = { b / 2, -b / 2 };
            foreach (double y in ys)
                foreach (double z in zs)
                {
                    V3 c = OnPlane(0.0, y, z);
                    umin = Math.Min(umin, c.Dot(_yp)); umax = Math.Max(umax, c.Dot(_yp));
                    vmin = Math.Min(vmin, c.Dot(_zp)); vmax = Math.Max(vmax, c.Dot(_zp));
                }
            umax += _p.TopGap; umin -= _p.BottGap;
            vmax += _p.LeftGap; vmin -= _p.RightGap;

            V3 mid = _n * (-_p.BasePlateThk / 2.0);             // mặt giữa bản đế
            List<V3> pts = new List<V3>();
            pts.Add(_yp * umin + _zp * vmin + mid);
            pts.Add(_yp * umax + _zp * vmin + mid);
            pts.Add(_yp * umax + _zp * vmax + mid);
            pts.Add(_yp * umin + _zp * vmax + mid);
            return Plate(pts, _p.BasePlateThk, _p.BasePlateMat, _p.BasePlateClass, _p.BasePlateMark, "PLATE");
        }

        private ContourPlate CreateGusset(double tw, double webEnd)
        {
            double side = _p.GussetOnPositiveSide ? 1.0 : -1.0;
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
            List<V3> pts = new List<V3>();
            pts.Add(new V3(xf, H, z0));
            pts.Add(new V3(xf, -H, z0));
            pts.Add(nearBot);
            pts.Add(nearTop);
            return Plate(pts, _p.GussetThk, _p.GussetMat, _p.GussetClass, _p.GussetMark, "PLATE");
        }

        private ContourPlate CreateFlatBar(double yi, double b, int flange)
        {
            double yOut = flange * yi;                          // sát mặt trong cánh
            double yIn = flange * (yi - _p.FlatBarWidth);
            V3 lift = _n * (_p.FlatBarThk / 2.0);
            List<V3> pts = new List<V3>();
            pts.Add(OnPlane(0.0, yOut, -b / 2) + lift);
            pts.Add(OnPlane(0.0, yOut, b / 2) + lift);
            pts.Add(OnPlane(0.0, yIn, b / 2) + lift);
            pts.Add(OnPlane(0.0, yIn, -b / 2) + lift);
            return Plate(pts, _p.FlatBarThk, _p.FlatBarMat, _p.FlatBarClass, _p.FlatBarMark, "FLAT_BAR");
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
                bolts.Hole1 = !_p.GussetOnPositiveSide;
                bolts.Hole2 = _p.GussetOnPositiveSide;
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
