// Dựng hình học cho liên kết moment dầm tạm (Part 2) -> dầm chính (Part 1), từ custom component "♥♥♥YGY_TempBeam-Moment".
//
// Nguồn hình học: file JSON xuất từ component gốc trên model mẫu (Plugin\_Dev\Export\20261118_160409_...json,
// Part 1 = H500*200*10*16, Part 2 = H400*200*8*13, mọi ô để trống) + ảnh Variables / ảnh 3D người dùng gửi 18/11/2026.
// Toạ độ đo được ghi cạnh code (hệ cục bộ dưới đây, mm).
//
// Đã dựng:
//   - Fitting Part 2 cách mép cánh Part 1 một khe (5).
//   - Vát CJP 2 cánh Part 2: góc P27 = 45, mặt chân 2 mm (D43/D48 = (P4-2)*tan(P27)); cả 2 cánh vát mở LÊN TRÊN
//     (cánh trên vát mặt ngoài, cánh dưới vát mặt trong) - như JSON.
//   - Scallop 1/4 cung tròn R = P41 tâm ở góc đầu bụng - mặt trong cánh, cả cánh trên và cánh dưới.
//   - GP (PL P7, dày theo bảng T3) áp 1 bên bụng Part 2 (D1 = ((P3+P7)/2)*P16), chạy hết chiều cao giữa 2 cánh Part 1
//     trong phần tiếp giáp bụng Part 1, rồi thò ra thành tab cao P8 (bảng H1) tới chiều dài tab (bảng B3/L1).
//   - BACK SF (P31): PL P7 phía bên kia bụng Part 1, cùng mặt phẳng GP.
//   - SF dưới (P25 = P23 > 0): PL P39 nằm ngang ở cao độ cánh dưới Part 2, từ bụng tới mép cánh Part 1, có rãnh cho GP.
//   - 2 FB lót cánh (FB25*9): dưới cánh trên, dưới cánh dưới; dài = bề rộng cánh Part 2 + 20 mỗi bên (D103 = P5/2+20).
//   - Bu lông P12 qua GP + bụng Part 2: P9 khoảng P18 (P19 = P9*P18), lỗ ô van P15 ở Part 2, dung sai P13.
//   - Hàn góc GP / BACK SF / SF dưới vào Part 1.
// Chưa dựng (ghi CANH BAO): tấm bù cánh trên T1 ("PL"+T4), tấm bù cánh dưới P129 ("PL"+P133), gia cường cánh dưới P43
//   (part dài P46), Part 2 nghiêng / xiên / xoay.
//
// Hệ cục bộ (đặt bằng TransformationPlane, trả lại plane cũ trong finally):
//   gốc O = giao trục Part 2 với mặt phẳng giữa bụng Part 1; X dọc Part 2 ra xa Part 1; Y = hướng lên (Z toàn cục dương);
//   Z = X x Y. Bụng Part 1: |x| <= tw1/2, mép cánh Part 1 phía Part 2: x = b1/2. Trục Part 2 ở y = 0.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSMPlane = Tekla.Structures.Model.Plane;

using UEL.Core;

namespace UEL.Core.Components.TempBeam
{
    public class TempBeamBuilder
    {
        private static readonly CultureInfo CI = CultureInfo.InvariantCulture;

        // Số hình học không thuộc biến .uel - chỉnh bằng tab Knobs của UEL_TESTER.exe. Giá trị = số đo trên JSON 18/11/2026.
        // <KNOBS>
        internal static double FitGap = 5.0;          // [0..30, 0.5] Khe dau Part 2 -> mep canh Part 1 (JSON: 5)
        internal static double BoltFromEnd = 65.0;    // [20..200, 1] Cot bu long cach dau Part 2 (JSON: 65)
        internal static double GpInset = 20.0;        // [0..80, 1] GP: phan trong Part 1 lui vao tu mep canh Part 1 (JSON: 20)
        internal static double BackInset = 10.0;      // [0..80, 1] BACK SF lui vao tu mep canh Part 1 (JSON: 10)
        internal static double CornerClipR = 35.0;    // [0..60, 1] GP / BACK SF: cung khoet goc bung - canh Part 1 (JSON: R35)
        internal static double NotchExtra = 5.0;      // [0..30, 1] SF duoi: ranh cho GP dai qua canh GP (JSON: 5)
        internal static double BevelExtra = 3.0;      // [0..20, 0.5] Vat mep canh: khoi cat vuot mat canh (nhu RAKER)
        internal static double FilletSize = 6.0;      // [3..12, 1] Han goc GP / SF vao Part 1 (JSON: 6)
        internal static double BoltCutLength = 200.0; // [50..400, 10] Cut length nhom bu long (JSON: 200)
        // </KNOBS>

        private const double P27 = 45.0;              // P27 = 45 (góc vát, hằng trong .uel)
        private const double RootFace = 2.0;          // (P4-2) trong D43/D48 -> mặt chân 2 mm
        private const double P55 = 10.0;             // P55 = if (D174<10) then 0 else 10 -> bo góc chân tab R10 (JSON). Giả định D174 >= 10.

        private readonly Model _model;
        private readonly TempBeamParams _p;
        private readonly GirderRow _row;
        private readonly double _p39;
        private readonly List<ModelObject> _created = new List<ModelObject>();
        private string _head1 = "", _head2 = "";

        // tiết diện
        private double _h1, _b1, _tw1, _tf1, _h2, _b2, _tw2, _tf2;

        public TempBeamBuilder(Model model, TempBeamParams p, GirderRow row, double p39)
        {
            _model = model;
            _p = p;
            _row = row;
            _p39 = p39;
        }

        public List<ModelObject> Created { get { return _created; } }

        public bool Build(Beam main, Beam temp)
        {
            Logger.Log("Thong so: " + _p);
            WorkPlaneHandler wph = _model.GetWorkPlaneHandler();
            TransformationPlane original = wph.GetCurrentTransformationPlane();
            try
            {
                wph.SetCurrentTransformationPlane(new TransformationPlane());   // làm việc ở hệ toàn cục
                return BuildInGlobal(wph, main, temp);
            }
            finally
            {
                wph.SetCurrentTransformationPlane(original);
            }
        }

        private bool BuildInGlobal(WorkPlaneHandler wph, Beam main, Beam temp)
        {
            // Prefix (Plugin\CLAUDE.md 4.3, giống YGY.uel mới): GP/SF hàn vào Part 1 -> P30 = P63 = mid(prefix Part 1,0,find(..,"M"));
            // FB thuộc assembly Part 2 (JSON) -> P64 = mid(prefix Part 2, ...). Ô P30 nhập tay thì dùng nguyên giá trị như .uel.
            _head1 = _p.GpPrefix.Trim().Length > 0 ? _p.GpPrefix.Trim() : PartPrefix.HeadOfAssembly(main, "Part 1 (dam chinh)");
            _head2 = PartPrefix.HeadOfAssembly(temp, "Part 2 (dam tam)");

            // ---- 1. Trục, tiết diện ----
            V3 a1, b1, a2, b2;
            if (!CenterLine(main, out a1, out b1) || !CenterLine(temp, out a2, out b2))
            {
                Logger.Error("Khong doc duoc truc cau kien.");
                return false;
            }
            _h1 = Report(main, "PROFILE.HEIGHT"); _b1 = Report(main, "PROFILE.WIDTH");
            _tw1 = Report(main, "PROFILE.WEB_THICKNESS"); _tf1 = Report(main, "PROFILE.FLANGE_THICKNESS");
            _h2 = Report(temp, "PROFILE.HEIGHT"); _b2 = Report(temp, "PROFILE.WIDTH");
            _tw2 = Report(temp, "PROFILE.WEB_THICKNESS"); _tf2 = Report(temp, "PROFILE.FLANGE_THICKNESS");
            if (_h1 <= 0 || _b1 <= 0 || _tw1 <= 0 || _tf1 <= 0 || _h2 <= 0 || _b2 <= 0 || _tw2 <= 0 || _tf2 <= 0)
            {
                Logger.Error("Part 1 va Part 2 phai la tiet dien H/I (khong doc duoc HEIGHT/WIDTH/WEB/FLANGE).");
                return false;
            }

            V3 e1 = (b1 - a1).Unit();
            Line axis1 = new Line(a1.ToPoint(), b1.ToPoint());
            bool b2Near = Distance.PointToLine(b2.ToPoint(), axis1) < Distance.PointToLine(a2.ToPoint(), axis1);
            V3 near = b2Near ? b2 : a2, far = b2Near ? a2 : b2;
            V3 X = (far - near).Unit();
            CoordinateSystem cs2 = temp.GetCoordinateSystem();
            V3 y2 = V3.From(cs2.AxisY).OrthoTo(X).Unit();
            V3 Y = y2.Z >= 0 ? y2 : y2 * -1.0;
            CoordinateSystem cs1 = main.GetCoordinateSystem();
            V3 y1 = V3.From(cs1.AxisY).OrthoTo(e1).Unit();

            // .uel gốc chỉ cho dầm vuông góc, nằm ngang, bụng đứng (JSON). Trường hợp khác CHƯA LÀM -> dừng, không đoán.
            double skewDeg = Math.Asin(Math.Min(1.0, Math.Abs(X.Dot(e1)))) * 180.0 / Math.PI;
            double slope2 = Math.Asin(Math.Min(1.0, Math.Abs(X.Z))) * 180.0 / Math.PI;
            double slope1 = Math.Asin(Math.Min(1.0, Math.Abs(e1.Z))) * 180.0 / Math.PI;
            Logger.Log(string.Format(CI, "Goc: Part 2 lech vuong goc {0:0.##} do, doc Part 2 {1:0.##} do, doc Part 1 {2:0.##} do, bung Part 2 nghieng {3:0.##} do, bung Part 1 nghieng {4:0.##} do",
                skewDeg, slope2, slope1, Math.Acos(Math.Min(1.0, Math.Abs(y2.Z))) * 180 / Math.PI, Math.Acos(Math.Min(1.0, Math.Abs(y1.Z))) * 180 / Math.PI));
            if (skewDeg > 1.0 || slope2 > 1.0 || slope1 > 1.0 || Math.Abs(y2.Z) < 0.9998 || Math.Abs(y1.Z) < 0.9998)
            {
                Logger.Error("Chi lam truong hop Part 2 vuong goc Part 1, 2 dam nam ngang, bung dung (nhu component goc tren model mau). " +
                             "Part 2 xien / doc / xoay: CHUA LAM - bao lai de bo sung.");
                return false;
            }
            V3 Z = X.Cross(Y).Unit();

            // gốc O: giao trục Part 2 với mặt giữa bụng Part 1 (mặt chứa trục Part 1 và phương đứng, pháp tuyến ~ X)
            V3 O = near + X * (a1 - near).Dot(X);
            double y1c = (a1 - O).Dot(Y);                       // tâm Part 1 so với trục Part 2 (JSON: -50)
            double tip = _b1 / 2.0;                              // mép cánh Part 1 phía Part 2 (JSON: x = 100)
            double tw = _tw1 / 2.0;                              // mặt bụng Part 1 (JSON: x = 5)
            double yT1 = y1c + _h1 / 2.0 - _tf1;                 // mặt trong cánh trên Part 1 (JSON: 184)
            double yB1 = y1c - _h1 / 2.0 + _tf1;                 // mặt trong cánh dưới Part 1 (JSON: -284)
            double yiT = _h2 / 2.0 - _tf2, yiB = -_h2 / 2.0 + _tf2;   // mặt trong 2 cánh Part 2
            double gapEnd = (near - O).Dot(X);
            Logger.Log(string.Format(CI, "Part 1 {0} (H={1} B={2} tw={3} tf={4}), Part 2 {5} (H={6} B={7} tw={8} tf={9}); tam Part 1 lech {10:0.#} theo Y; dau Part 2 cach bung Part 1 {11:0.#}",
                main.Profile.ProfileString, _h1, _b1, _tw1, _tf1, temp.Profile.ProfileString, _h2, _b2, _tw2, _tf2, y1c, gapEnd));
            if (_h2 / 2.0 > y1c + _h1 / 2.0 + 0.5 || -_h2 / 2.0 < y1c - _h1 / 2.0 - 0.5)
                Logger.Log("CANH BAO: Part 2 vuot ra ngoai chieu cao Part 1 - component goc khong tinh truong hop nay.");

            // ---- 2. Các giá trị tính như .uel ----
            double P2 = Math.Round(_h2), P3 = _tw2, P4 = Math.Round(_tf2), P5 = Math.Round(_b2);
            double P7 = _row.T3, P8 = _row.H1;
            int CWS = _p.CwsSet ? _p.Cws : (string.Equals(_row.Col5, "SPS", StringComparison.OrdinalIgnoreCase) ? 700 : 900);
            int P9 = P2 >= CWS ? 2 : 1;                                             // P9 = if (P2>=CWS) then 2 else 1
            double P18 = P2 >= CWS ? P8 / 2.0 - 40.0 : P8 - 80.0;                   // P18
            int P12 = _p.BoltSize;
            double P13 = P12 >= 24 ? 3 : 2;                                         // P13
            double P14;
            if (!SlotByDiameter(P12, out P14))
            {
                Logger.Error(string.Format(CI, "Bolt size M{0} khong co trong cong thuc P14 cua .uel (chi co M16, M20, M22, M24, M27).", P12));
                return false;
            }
            double P15 = _p.SlotHoleSet ? _p.SlotHole : P14 - P12 - P13;            // P15 = P14-P12-P13
            int P16 = _p.PlateSide ? 1 : -1;                                        // P16
            double P21 = _h1, P22 = _tf1, P23 = P21 - P2;                           // P23 = P21-P2
            bool P25 = P23 > 0;                                                     // P25 = if (P23>0) then 1 else 0
            string P34 = main.Material.MaterialString;
            string P35 = P34 == "SHN355" ? "SN355" : (P34 == "SHN275" ? "SN275" : P34);
            double gw = _b1;
            double P41 = _p.ScallopSizeSet ? _p.ScallopSize : (gw < 181 ? 25 : 35);  // P41 (các nhánh sau của .uel đều = 35)
            bool P43 = _p.LowerFlgImproveSet ? _p.LowerFlgImprove : (!double.IsNaN(_p39) && _p39 >= 21);
            int P50 = temp.AssemblyNumber.StartNumber;                              // P50 = fP(proASSEMBLYSTARTNUMBER, Part 2)
            Logger.Log(string.Format(CI,
                "Tinh: P2={0} P3={1} P4={2} P5={3} P7(T3)={4} P8(H1)={5} CWS={6}{7} P9={8} P18={9} P19='{8}*{9}' P12=M{10} P13={11} P14={12} P15={13}{14} " +
                "P16={15} P23={16} P25={17} P35={18} gw={19} P41={20}{21} P39={22} P43={23}{24} P50={25}",
                P2, P3, P4, P5, P7, P8, CWS, _p.CwsSet ? " (hop thoai)" : "", P9, P18, P12, P13, P14, P15, _p.SlotHoleSet ? " (hop thoai)" : "",
                P16, P23, P25 ? 1 : 0, P35, gw, P41, _p.ScallopSizeSet ? " (hop thoai)" : "", double.IsNaN(_p39) ? "-" : _p39.ToString(CI),
                P43 ? 1 : 0, _p.LowerFlgImproveSet ? " (hop thoai)" : "", P50));

            // Các nhánh của .uel CHƯA LÀM (không có trong JSON mẫu, chưa có ảnh vị trí) - ghi rõ khi .uel sẽ tạo:
            double T3 = _tf2 - _tf1;                                                // T3 = T4 = tf Part 2 - tf Part 1
            if (5.0 < T3) Logger.Log(string.Format(CI, "CANH BAO: .uel tao tam bu canh T1 (\"PL\"+T4 = PL{0}, SF) vi canh Part 2 day hon Part 1 > 5 mm - CHUA LAM.", T3));
            double P133 = (_h1 - _tf1) - (_h2 - _tf2);                              // P133 = P131-(P126-tf Part 2)
            double P44 = double.IsNaN(_p39) ? 0 : _p39;                             // P44 = fVF(WELD-K, P39, 1) = cột khoá = P39
            bool P129 = !((P43 ? 1 : 0) > P133 || P44 < P133);
            if (P129) Logger.Log(string.Format(CI, "CANH BAO: .uel tao tam bu canh duoi P129 (\"PL\"+P133 = PL{0}, SF) - CHUA LAM.", P133));
            if (P43) Logger.Log("CANH BAO: P43 = 1 -> .uel tao gia cuong canh duoi (part dai P46 = chieu dai + 100) - CHUA LAM.");
            if (_p.ScallopType != 3) Logger.Log("CANH BAO: P40 = " + _p.ScallopType + " - plugin chi lam scallop kieu cung tron (P40 = 3).");

            // ---- 3. Phía GP ----
            // JSON: P11 = 0 (P16 = -1, D1 = -9.5) -> GP ở phía +Z RIÊNG của Part 2 (AxisX x AxisY của dầm). P11 = 1 -> phía -Z.
            V3 zNative = V3.From(cs2.AxisX).Cross(V3.From(cs2.AxisY)).Unit();
            double sz = (zNative.Dot(Z) >= 0 ? 1.0 : -1.0) * (-P16);
            double zc = sz * (P3 + P7) / 2.0;                                       // D1 = ((P3+P7)/2)*P16
            Logger.Log(string.Format(CI, "GP: phia {0}Z rieng cua Part 2 (P11 = {1}) -> z = {2:0.##}", -P16 > 0 ? "+" : "-", _p.PlateSide ? 1 : 0, zc));

            wph.SetCurrentTransformationPlane(new TransformationPlane(O.ToPoint(), X.ToVector(), Y.ToVector()));

            // ---- 4. Part 2: fitting, vát mép, scallop ----
            double xe = tip + FitGap;                                               // JSON: đầu Part 2 ở x = 105
            Fit(temp, xe);
            CutBevel(temp, xe, yiT + RootFace, _h2 / 2.0, "tren (vat mat ngoai)");  // JSON: (115,187..189) -> (126,200)
            CutBevel(temp, xe, -_h2 / 2.0 + RootFace, yiB, "duoi (vat mat trong)");// JSON: (115,-200..-198) -> (126,-187)
            CutScallop(temp, xe, yiT, -1.0, P41);
            CutScallop(temp, xe, yiB, +1.0, P41);

            // ---- 5. GP ----
            double xg = tip - GpInset;                                              // JSON: x = 80
            double xt = _row.TabFromTip ? tip + _row.TabLen : xe + _row.TabLen;     // JSON: x = 235 = mép cánh + B3 (135) = đầu dầm + L1 (130)
            ContourPlate gp = CreateGp(zc, P7, P8, tw, xg, xt, yT1, yB1, P35);
            if (gp == null) return false;

            // ---- 6. BACK SF (P31) ----
            ContourPlate back = null;
            if (_p.BackSf)
            {
                // JSON: x từ -5 (mặt bụng bên kia) tới -90 (= -(b1/2 - 10)), y giữa 2 cánh Part 1, cùng mặt phẳng GP, góc R35
                back = CreateSide(zc, P7, -tw, -(tip - BackInset), yT1, yB1, P35, "BACK SF", NameOr(_p.SfName, "PLATE"),
                                  NonEmpty(_p.SfClass, "99"), PartPrefix.Make(_head1, "SF"), _p.SfFinish);
            }
            else Logger.Log("BACK SF: P31 = 0 -> khong co.");

            // ---- 7. SF dưới (P25) ----
            ContourPlate sfb = null;
            if (P25)
            {
                if (double.IsNaN(_p39) || _p39 <= 0)
                {
                    Logger.Error("P39 (bang PLTHK) khong hop le -> khong dung SF duoi.");
                    return false;
                }
                sfb = CreateLowerSf(_p39, tw, tip, zc, P7, xg, yB1, P35);
            }
            else Logger.Log(string.Format(CI, "SF duoi: P23 = {0} <= 0 (Part 1 khong cao hon Part 2) -> khong co.", P23));

            // ---- 8. FB ----
            List<Beam> bars = new List<Beam>();
            bars.Add(CreateFlatBar(tip, yiT, -1.0, P5, P50, "tren"));               // JSON: x 100..125, y 178..187 (dưới cánh trên)
            bars.Add(CreateFlatBar(tip, -_h2 / 2.0, -1.0, P5, P50, "duoi"));       // JSON: y -209..-200 (dưới cánh dưới)

            // ---- 9. Bu lông ----
            CreateBolts(temp, gp, xe + BoltFromEnd, zc, P9, P18, P12, P13, P15);

            // ---- 10. Hàn (JSON: hàn góc 6, hàn xưởng, Part 1 là main) ----
            Fillet(main, gp);
            Fillet(main, back);
            Fillet(main, sfb);
            return true;
        }

        // ----------------------------------------------------------------- công thức .uel

        /// P14 = if (P12==16) then 40 else if 20 then 50 else if 22 then 55 else if 24 then 60 else if 27 then 67 (khác: trống)
        private static bool SlotByDiameter(int d, out double len)
        {
            switch (d)
            {
                case 16: len = 40; return true;
                case 20: len = 50; return true;
                case 22: len = 55; return true;
                case 24: len = 60; return true;
                case 27: len = 67; return true;
            }
            len = 0;
            return false;
        }

        // ----------------------------------------------------------------- Part 2

        private void Fit(Beam temp, double x)
        {
            Fitting fit = new Fitting();
            fit.Father = temp;
            TSMPlane pl = new TSMPlane();
            pl.Origin = new Point(x, 0, 0);
            pl.AxisX = new Vector(0, 1, 0);
            pl.AxisY = new Vector(0, 0, 1);                     // pháp tuyến = +X: giữ phần Part 2 phía xa Part 1
            fit.Plane = pl;
            if (fit.Insert()) _created.Add(fit);
            else Logger.Log("Khong tao duoc fitting dau Part 2.");
            Logger.Log(string.Format(CI, "Fitting Part 2: cach mep canh Part 1 {0} mm", FitGap));
        }

        /// Vát mép CJP 1 cánh (như CutBevel của RAKER, dầm vuông góc): đầu cánh ở x = xe; mặt chân RootFace phía dưới y0;
        /// từ y0 lên tới yTop vát góc P27, mở lên trên (về phía +Y). Khối cắt vượt mặt yTop BevelExtra, lùi về phía Part 1 BevelExtra.
        private void CutBevel(Beam temp, double xe, double y0, double yTop, string name)
        {
            double extra = BevelExtra;
            double rise = yTop - y0 + extra;
            if (rise <= extra + 0.5) return;
            double t = Math.Tan(P27 * Math.PI / 180.0);
            List<V3> pts = new List<V3>();
            pts.Add(new V3(xe - extra, y0, 0));
            pts.Add(new V3(xe, y0, 0));
            pts.Add(new V3(xe + rise * t, y0 + rise, 0));
            pts.Add(new V3(xe - extra, y0 + rise, 0));
            Cut(temp, pts, _b2 + 100.0);
            Logger.Log(string.Format(CI, "Vat mep canh {0}: goc {1}, mat chan {2} mm, vat sau {3:0.#}", name, P27, RootFace, yTop - y0));
        }

        /// Scallop 1/4 cung tròn bán kính R, TÂM tại góc đầu bụng - mặt trong cánh (xe, yFace) (JSON: tâm (105, ±187), R35).
        /// dir = -1: cánh trên (scallop đi xuống bụng), +1: cánh dưới. Không dùng chamfer rounding của Tekla (NGUYEN_TAC_TU_COL 5.6).
        private void CutScallop(Beam temp, double xe, double yFace, double dir, double R)
        {
            if (R <= 1.0) return;
            List<V3> pts = new List<V3>();
            pts.Add(new V3(xe - 10.0, yFace, 0));
            pts.Add(new V3(xe - 10.0, yFace + dir * R, 0));
            for (int i = 0; i <= 12; i++)
            {
                double a = i * 7.5 * Math.PI / 180.0;          // 0 .. 90 độ: từ (xe, yFace + dir*R) tới (xe + R, yFace)
                pts.Add(new V3(xe + R * Math.Sin(a), yFace + dir * R * Math.Cos(a), 0));
            }
            Cut(temp, pts, _tw2 + 4.0);
            Logger.Log(string.Format(CI, "Scallop {0}: R{1} tam goc dau bung - mat trong canh", dir < 0 ? "tren" : "duoi", R));
        }

        // ----------------------------------------------------------------- tấm

        /// GP: mặt giữa z = zc, dày P7. JSON (mặt đứng, x dọc Part 2, y lên):
        ///   phần trong Part 1: x từ mặt bụng (tw) tới xg = mép cánh - 20, y giữa 2 mặt trong cánh Part 1, khoét góc R35 ở 2 góc bụng - cánh;
        ///   tab: từ xg tới xt, cao P8 đối xứng trục Part 2, bo góc chân tab R P55 (10).
        private ContourPlate CreateGp(double zc, double thk, double P8, double tw, double xg, double xt, double yT1, double yB1, string mat)
        {
            double h = P8 / 2.0, f = P55, rc = CornerClipR;
            if (xt <= xg + f + 1.0)
            {
                Logger.Error(string.Format(CI, "GP: dau tab x = {0:0.#} khong vuot qua x = {1:0.#} (mep canh Part 1 - {2}) - kiem tra bang tra B3/L1.", xt, xg, GpInset));
                return null;
            }
            if (xg - tw < rc + 1.0)
            {
                rc = Math.Max(0.0, xg - tw - 1.0);
                Logger.Log(string.Format(CI, "CANH BAO: GP: phan trong Part 1 hep ({0:0.#}) -> giam cung khoet goc con R{1:0.#}.", xg - tw, rc));
            }
            bool fillet = true;
            if (h + f > yT1 - 0.5 || -h - f < yB1 + 0.5)
            {
                fillet = false;
                Logger.Log(string.Format(CI, "CANH BAO: GP: tab cao {0} sat / vuot mat trong canh Part 1 (y {1:0.#} .. {2:0.#}) -> bo bo goc chan tab, tab cat theo canh Part 1.", P8, yB1, yT1));
                h = Math.Min(h, Math.Min(yT1, -yB1));
            }
            List<V3> pts = new List<V3>();
            ArcPts(pts, tw, yB1, rc, 0, 90, 1, 1);                  // khoét góc dưới: (tw, yB1+rc) -> (tw+rc, yB1)
            pts.Add(new V3(xg, yB1, 0));
            if (fillet)
            {
                for (int i = 0; i <= 12; i++)                        // bo chân tab dưới: (xg, -h-f) -> (xg+f, -h)
                {
                    double a = i * 7.5 * Math.PI / 180.0;
                    pts.Add(new V3(xg + f - f * Math.Cos(a), -h - f + f * Math.Sin(a), 0));
                }
            }
            else pts.Add(new V3(xg, -h, 0));
            pts.Add(new V3(xt, -h, 0));
            pts.Add(new V3(xt, h, 0));
            if (fillet)
            {
                for (int i = 0; i <= 12; i++)                        // bo chân tab trên: (xg+f, h) -> (xg, h+f)
                {
                    double a = i * 7.5 * Math.PI / 180.0;
                    pts.Add(new V3(xg + f - f * Math.Sin(a), h + f - f * Math.Cos(a), 0));
                }
            }
            else pts.Add(new V3(xg, h, 0));
            pts.Add(new V3(xg, yT1, 0));
            ArcPts(pts, tw, yT1, rc, 0, 90, 1, -1);                 // khoét góc trên: (tw+rc, yT1) -> (tw, yT1-rc)
            List<V3> world = Shift(pts, zc);
            Logger.Log(string.Format(CI, "GP: PL{0} {1}, phan trong Part 1 x {2:0.#}..{3:0.#}, y {4:0.#}..{5:0.#}; tab den x = {6:0.#} ({7} {8}), cao {9}",
                thk, mat, tw, xg, yB1, yT1, xt, _row.TabFromTip ? "B3 tu mep canh Part 1 =" : "L1 tu dau Part 2 =", _row.TabLen, P8));
            ContourPlate cp = Plate(Clean(world), thk, mat, NonEmpty(_p.GpClass, "99"), PartPrefix.Make(_head1, "GP"), NameOr(_p.GpName, "PLATE"));
            if (cp != null)
            {
                Decorate(cp, "GP", _p.GpFinish);
                CheckBox(cp, "GP", tw, xt, yB1, yT1, zc - thk / 2.0, zc + thk / 2.0);
            }
            return cp;
        }

        /// BACK SF: chữ nhật x x0..x1 (phía bên kia bụng Part 1), y giữa 2 cánh Part 1, khoét góc R35 ở góc bụng - cánh.
        private ContourPlate CreateSide(double zc, double thk, double x0, double x1, double yT1, double yB1, string mat, string who,
                                        string name, string cls, string prefix, string finish)
        {
            double rc = Math.Min(CornerClipR, Math.Max(0.0, Math.Abs(x1 - x0) - 1.0));
            double s = Math.Sign(x1 - x0);
            List<V3> pts = new List<V3>();
            ArcPts(pts, x0, yB1, rc, 0, 90, s, 1);                  // (x0, yB1+rc) -> (x0+s*rc, yB1)
            pts.Add(new V3(x1, yB1, 0));
            pts.Add(new V3(x1, yT1, 0));
            ArcPts(pts, x0, yT1, rc, 0, 90, s, -1);                 // (x0+s*rc, yT1) -> (x0, yT1-rc)
            Logger.Log(string.Format(CI, "{0}: PL{1} {2}, x {3:0.#}..{4:0.#}, y {5:0.#}..{6:0.#}", who, thk, mat, x0, x1, yB1, yT1));
            ContourPlate cp = Plate(Clean(Shift(pts, zc)), thk, mat, cls, prefix, name);
            if (cp != null)
            {
                Decorate(cp, who, finish);
                CheckBox(cp, who, Math.Min(x0, x1), Math.Max(x0, x1), yB1, yT1, zc - thk / 2.0, zc + thk / 2.0);
            }
            return cp;
        }

        /// SF dưới: PL P39 nằm ngang, mặt dưới = mặt dưới cánh dưới Part 2 (JSON: y -200..-186), x từ mặt bụng Part 1 tới mép
        /// cánh Part 1, z = ±P5/2 quanh trục Part 2; rãnh cho GP: z = zc ± (P7/2+2) (D136), x từ bụng tới xg + 5 (JSON: 2..17, 5..85).
        private ContourPlate CreateLowerSf(double thk, double tw, double tip, double zc, double P7, double xg, double yB1, string mat)
        {
            double y = -_h2 / 2.0 + thk / 2.0, hb = _b2 / 2.0;
            double dn = P7 / 2.0 + 2.0, xn = xg + NotchExtra;
            List<V3> pts = new List<V3>();
            pts.Add(new V3(tw, y, -hb));
            pts.Add(new V3(tip, y, -hb));
            pts.Add(new V3(tip, y, hb));
            pts.Add(new V3(tw, y, hb));
            bool notch = Math.Abs(zc) + dn < hb - 0.5 && xn > tw + 0.5 && xn < tip - 0.5;
            if (notch)
            {
                // đi ngược chiều z ở cạnh x = tw: chèn rãnh (tw, zc+dn) -> (xn, zc+dn) -> (xn, zc-dn) -> (tw, zc-dn)
                double zHi = zc + dn, zLo = zc - dn;
                pts.Add(new V3(tw, y, zHi));
                pts.Add(new V3(xn, y, zHi));
                pts.Add(new V3(xn, y, zLo));
                pts.Add(new V3(tw, y, zLo));
            }
            else Logger.Log("CANH BAO: SF duoi: ranh cho GP khong nam gon trong tam -> khong khoet ranh.");
            if (-_h2 / 2.0 < yB1 + 0.5)
                Logger.Log(string.Format(CI, "CANH BAO: SF duoi (y {0:0.#}) cham / lan vao canh duoi Part 1 (mat trong y {1:0.#}).", -_h2 / 2.0, yB1));
            Logger.Log(string.Format(CI, "SF duoi: PL{0} {1}, y {2:0.#}..{3:0.#}, x {4:0.#}..{5:0.#}, z +-{6:0.#}{7}",
                thk, mat, -_h2 / 2.0, -_h2 / 2.0 + thk, tw, tip, hb, notch ? string.Format(CI, ", ranh GP z {0:0.#}..{1:0.#} x den {2:0.#}", zc - dn, zc + dn, xn) : ""));
            ContourPlate cp = Plate(pts, thk, mat, NonEmpty(_p.SfClass, "83"), PartPrefix.Make(_head1, "SF"), NameOr(_p.SfName, "PLATE"));
            if (cp != null)
            {
                Decorate(cp, "SF duoi", _p.SfFinish);
                CheckBox(cp, "SF duoi", tw, tip, -_h2 / 2.0, -_h2 / 2.0 + thk, -hb, hb);
            }
            return cp;
        }

        /// FB lót: rộng 25 dọc X từ mép cánh Part 1 (JSON: x 100..125), mỏng 9 theo Y, nằm DƯỚI mặt yFace;
        /// dài theo Z = P5 + 2*20 (D103 = P5/2+20).
        private Beam CreateFlatBar(double tip, double yFace, double side, double P5, int startNo, string who)
        {
            double wide, thin;
            FlatBarSize(out wide, out thin);
            double xm = tip + wide / 2.0, ym = yFace + side * thin / 2.0, half = P5 / 2.0 + 20.0;
            Beam fb = new Beam();
            fb.StartPoint = new Point(xm, ym, half);
            fb.EndPoint = new Point(xm, ym, -half);
            fb.Profile.ProfileString = _p.FbProfile;
            fb.Material.MaterialString = _p.FbMat;
            if (!string.IsNullOrEmpty(_p.FbClass)) fb.Class = _p.FbClass;
            fb.Name = NameOr(_p.FbName, "PLATE");
            fb.PartNumber.Prefix = PartPrefix.Make(_head2, "FB");                  // SERIE = P64 + "FB" (YGY.uel)
            fb.PartNumber.StartNumber = Math.Max(1, startNo);                       // STARTNUMBER = P50
            fb.Position.Depth = Position.DepthEnum.MIDDLE;
            fb.Position.Plane = Position.PlaneEnum.MIDDLE;
            fb.Position.Rotation = Position.RotationEnum.FRONT;
            if (!fb.Insert())
            {
                Logger.Log("Khong tao duoc FB " + _p.FbProfile);
                return null;
            }
            _created.Add(fb);
            // Chiều xoay profile không đoán trước được (NGUYEN_TAC 5.3): cạnh mỏng phải theo Y; sai thì xoay 90 độ.
            double ey, ex;
            Extents(fb, out ex, out ey);
            if (Math.Abs(ey - thin) > 0.5)
            {
                fb.Position.Rotation = Position.RotationEnum.TOP;
                fb.Modify();
                fb.Select();
                Extents(fb, out ex, out ey);
            }
            Logger.Log(string.Format(CI, "FB {0} {1} {2} mark={3} start={4}: dai {5:0.#}, x {6:0.#}..{7:0.#}, y {8:0.#}..{9:0.#}{10}",
                who, _p.FbProfile, _p.FbMat, fb.PartNumber.Prefix, fb.PartNumber.StartNumber, 2 * half, tip, tip + wide,
                Math.Min(yFace, yFace + side * thin), Math.Max(yFace, yFace + side * thin), Math.Abs(ey - thin) > 0.5 ? " CANH BAO: sai chieu" : ""));
            Decorate(fb, "FB", _p.FbFinish);
            return fb;
        }

        private void FlatBarSize(out double wide, out double thin)
        {
            wide = 25; thin = 9;
            string s = (_p.FbProfile ?? "").ToUpperInvariant().Replace("FB", "").Replace("PL", "").Replace("X", "*");
            string[] a = s.Split('*');
            double v1, v2;
            if (a.Length == 2 && double.TryParse(a[0], NumberStyles.Float, CI, out v1) && double.TryParse(a[1], NumberStyles.Float, CI, out v2))
            {
                wide = Math.Max(v1, v2); thin = Math.Min(v1, v2);
            }
            else Logger.Log("CANH BAO: khong doc duoc kich thuoc FB '" + _p.FbProfile + "' -> coi la 25x9.");
        }

        // ----------------------------------------------------------------- bu lông

        /// JSON: 2 con M24 T.S.B ở x = đầu Part 2 + 65, y = ±97.5 (P18 = 195), lỗ ô van X = P15 (33) ở Part 2 (Hole2 = PartToBeBolted),
        /// dung sai P13 = 3, cut length 200. .uel: SLOTTED_HOLE_PARTS = P17 = if (P11==1) then 10000 else 1000 - plugin giữ lỗ ô van
        /// ở Part 2 cho cả 2 phía (giả định: P17 đổi theo P11 để bù thứ tự tấm), kiểm tra lại bằng FixSlotSide.
        private void CreateBolts(Beam temp, ContourPlate gp, double xb, double zc, int P9, double P18, int size, double tol, double slot)
        {
            if (gp == null) return;
            List<double> ys = new List<double>();
            double total = P9 * P18;
            for (int i = 0; i <= P9; i++) ys.Add(-total / 2.0 + i * P18);
            BoltXYList bolts = new BoltXYList();
            bolts.PartToBoltTo = gp;
            bolts.PartToBeBolted = temp;
            bolts.FirstPosition = new Point(xb, 0, 0);
            bolts.SecondPosition = new Point(xb + 100.0, 0, 0);
            bolts.BoltSize = size;
            bolts.BoltStandard = _p.BoltStandard;
            bolts.Tolerance = tol;
            bolts.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
            bolts.CutLength = BoltCutLength;
            bolts.ExtraLength = 0.0;
            bolts.Position.Depth = Position.DepthEnum.MIDDLE;
            bolts.Position.Plane = Position.PlaneEnum.MIDDLE;
            bolts.Position.Rotation = Position.RotationEnum.FRONT;   // thân bu lông theo Z cục bộ (xuyên bụng)
            bolts.Bolt = true;
            bolts.Washer1 = false; bolts.Washer2 = true; bolts.Washer3 = false;   // NGUYEN_TAC_TU_RAKER mục 6 (gốc: Washer3)
            bolts.Nut1 = true; bolts.Nut2 = false;
            foreach (double y in ys) { bolts.AddBoltDistX(0.0); bolts.AddBoltDistY(y); }
            if (slot > 0.01)
            {
                bolts.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_SLOTTED;
                bolts.SlottedHoleX = slot;
                bolts.SlottedHoleY = 0.0;
                bolts.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_PARALLEL;
                bolts.Hole1 = false;
                bolts.Hole2 = true;                                   // tấm 2 = PartToBeBolted = Part 2 (JSON)
            }
            if (!bolts.Insert())
            {
                Logger.Log("Khong tao duoc nhom bu long.");
                return;
            }
            _created.Add(bolts);
            if (slot > 0.01) FixSlotSide(bolts, gp, temp);
            Logger.Log(string.Format(CI, "Bu long: {0} con M{1} {2}, x = {3:0.#} (dau Part 2 + {4}), khoang Y {5}*{6}, lo o van X {7} o Part 2, dung sai {8}",
                ys.Count, size, _p.BoltStandard, xb, BoltFromEnd, P9, P18, slot, tol));
        }

        /// Lỗ ô van ở Part 2: Tekla có thể đổi thứ tự tấm khi Insert -> đọc lại, đặt cờ đúng tấm, Modify (như COL_TO_GIRDER).
        private void FixSlotSide(BoltGroup bolts, ContourPlate gp, Beam temp)
        {
            if (!bolts.Select()) { Logger.Log("CANH BAO: khong doc lai duoc nhom bu long de kiem tra lo o van."); return; }
            int idGp = gp.Identifier.ID, idTemp = temp.Identifier.ID;
            int id1 = bolts.PartToBoltTo == null ? 0 : bolts.PartToBoltTo.Identifier.ID;
            int id2 = bolts.PartToBeBolted == null ? 0 : bolts.PartToBeBolted.Identifier.ID;
            bool gpFirst;
            if (id1 == idGp || id2 == idTemp) gpFirst = true;
            else if (id1 == idTemp || id2 == idGp) gpFirst = false;
            else
            {
                Logger.Log(string.Format(CI, "CANH BAO: nhom bu long tra ve tam {0} / {1}, khong khop GP {2} / Part 2 {3} - giu nguyen co lo o van.", id1, id2, idGp, idTemp));
                return;
            }
            bool want1 = !gpFirst, want2 = gpFirst;
            if (bolts.Hole1 != want1 || bolts.Hole2 != want2 || bolts.Hole3 || bolts.Hole4 || bolts.Hole5)
            {
                bolts.Hole1 = want1; bolts.Hole2 = want2;
                bolts.Hole3 = false; bolts.Hole4 = false; bolts.Hole5 = false;
                bolts.Modify();
                Logger.Log("Lo o van: Tekla xep tam 1 = " + (gpFirst ? "GP" : "Part 2") + " -> da sua co de lo o van o Part 2.");
            }
            else Logger.Log("Lo o van: tam 1 = " + (gpFirst ? "GP" : "Part 2") + ", lo o van o Part 2.");
        }

        // ----------------------------------------------------------------- mối hàn

        private void Fillet(ModelObject main, ModelObject secondary)
        {
            if (main == null || secondary == null) return;
            Weld w = new Weld();
            w.MainObject = main;
            w.SecondaryObject = secondary;
            w.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            w.SizeAbove = FilletSize;
            w.TypeBelow = BaseWeld.WeldTypeEnum.WELD_TYPE_FILLET;
            w.SizeBelow = FilletSize;
            w.ShopWeld = true;
            if (w.Insert()) _created.Add(w);
        }

        // ----------------------------------------------------------------- tiện ích

        /// Thêm điểm cung tròn tâm (cx, cy) bán kính r, góc a0..a1 (độ, bước 7.5): điểm = (cx + sx*r*sin a, cy + sy*r*cos a).
        /// sx/sy chọn góc phần tư. Ví dụ sx = 1, sy = 1: từ (cx, cy+r) tới (cx+r, cy).
        /// sy = -1 đi ngược: từ (cx+sx*r, cy) tới (cx, cy-r).
        private static void ArcPts(List<V3> pts, double cx, double cy, double r, double a0, double a1, double sx, double sy)
        {
            if (r <= 0.5)
            {
                pts.Add(new V3(cx, cy, 0));
                return;
            }
            int n = 12;
            for (int i = 0; i <= n; i++)
            {
                double t = (a0 + (a1 - a0) * i / n) * Math.PI / 180.0;
                if (sy > 0) pts.Add(new V3(cx + sx * r * Math.Sin(t), cy + r * Math.Cos(t), 0));
                else pts.Add(new V3(cx + sx * r * Math.Cos(t), cy - r * Math.Sin(t), 0));
            }
        }

        private static List<V3> Shift(List<V3> pts, double z)
        {
            List<V3> r = new List<V3>();
            foreach (V3 q in pts) r.Add(new V3(q.X, q.Y, q.Z + z));
            return r;
        }

        /// Bỏ điểm trùng liên tiếp (< 0.05 mm), kể cả điểm cuối trùng điểm đầu.
        private static List<V3> Clean(List<V3> pts)
        {
            List<V3> r = new List<V3>();
            foreach (V3 q in pts)
                if (r.Count == 0 || (q - r[r.Count - 1]).Norm() > 0.05) r.Add(q);
            if (r.Count > 2 && (r[0] - r[r.Count - 1]).Norm() <= 0.05) r.RemoveAt(r.Count - 1);
            return r;
        }

        private ContourPlate Plate(List<V3> pts, double thk, string mat, string cls, string prefix, string name)
        {
            ContourPlate cp = new ContourPlate();
            foreach (V3 q in pts) cp.AddContourPoint(new ContourPoint(q.ToPoint(), null));
            cp.Profile.ProfileString = "PL" + thk.ToString("0.###", CI);
            cp.Material.MaterialString = mat;
            if (!string.IsNullOrEmpty(cls)) cp.Class = cls;
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
            if (!string.IsNullOrEmpty(cls))
            {
                // Lỗi đã gặp ở RAKER: tấm ra class của connection -> đọc lại, sai thì Modify.
                ContourPlate check = new ContourPlate();
                check.Identifier = cp.Identifier;
                if (check.Select() && check.Class != cls)
                {
                    check.Class = cls;
                    check.Modify();
                }
            }
            Logger.Log("Tao " + name + " " + cp.Profile.ProfileString + " " + mat + " mark=" + prefix + " class=" + cls);
            return cp;
        }

        private static string NameOr(string name, string fallback)
        {
            return string.IsNullOrEmpty(name) || name.Trim().Length == 0 ? fallback : name.Trim();
        }

        private static string NonEmpty(string s, string fallback)
        {
            return string.IsNullOrEmpty(s) || s.Trim().Length == 0 ? fallback : s.Trim();
        }

        private void Decorate(Part part, string who, string finish)
        {
            if (part == null || string.IsNullOrEmpty(finish) || finish.Trim().Length == 0) return;
            part.Finish = finish.Trim();
            if (!part.Modify()) Logger.Log("CANH BAO: " + who + ": khong gan duoc Finish '" + finish + "'.");
        }

        /// Kiểm tra vị trí sau khi dựng (NGUYEN_TAC 5.2): khối bao trong hệ cục bộ lệch > 1 mm thì CANH BAO.
        private static void CheckBox(Part part, string name, double x0, double x1, double y0, double y1, double z0, double z1)
        {
            Solid sol = part.GetSolid();
            if (sol == null) return;
            double err = Math.Max(Math.Max(Math.Abs(sol.MinimumPoint.X - x0), Math.Abs(sol.MaximumPoint.X - x1)),
                         Math.Max(Math.Max(Math.Abs(sol.MinimumPoint.Y - y0), Math.Abs(sol.MaximumPoint.Y - y1)),
                                  Math.Max(Math.Abs(sol.MinimumPoint.Z - z0), Math.Abs(sol.MaximumPoint.Z - z1))));
            if (err > 1.0)
                Logger.Log(string.Format(CI, "CANH BAO: {0} lech {1:0.##} mm so voi vi tri dinh dat.", name, err));
        }

        private static void Extents(Part part, out double ex, out double ey)
        {
            ex = ey = 0;
            Solid sol = part.GetSolid();
            if (sol == null) return;
            ex = sol.MaximumPoint.X - sol.MinimumPoint.X;
            ey = sol.MaximumPoint.Y - sol.MinimumPoint.Y;
        }

        private void Cut(Part father, List<V3> pts, double thk)
        {
            ContourPlate cutter = new ContourPlate();
            foreach (V3 q in Clean(pts)) cutter.AddContourPoint(new ContourPoint(q.ToPoint(), null));
            cutter.Profile.ProfileString = "PL" + thk.ToString("0.###", CI);
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
