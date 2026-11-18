// Dựng hình học cho liên kết Cột H (Part 1) -> Dầm H (Part 2), từ custom component "0-DM_H-Col2Gir_H-COLUMN" (H-COL.uel).
// Clone COL_TO_GIRDER (18/11/2026). Khác: Part 1 là cột H (mặt cánh / mặt bụng), không có WP mặc định (P48 = P49 = 0),
// D5 khi PIN = 30 (P74). Phần còn lại giữ nguyên như COL_TO_GIRDER.
//
// Đã dựng (công thức .uel ghi cạnh code):
//   - Fitting đầu dầm cách mặt cột D5 (= P75, mặc định 5, khi P14 = 1 MO; = P74, mặc định 20, khi PIN); cánh lùi thêm theo ring (D46/D47, D51/D52).
//   Biến mới P74..P78 (18/11/2026): gộp các bản tuỳ biến theo dự án (TANG_DUONG P-BOX, PCOL) - xem README.
//   - Vát mép CJP 2 cánh khi P14 = 1: góc P7 = 45, mặt chân 2 mm (D48/D53 = (P8-2)*tan(P7) + D5 + ring).
//   - CGP (bản mã hàn vào cột): PL t3 (P17), cao B3 (P19), dài L3 (P47) dọc dầm, áp 1 bên bụng dầm lệch (P1+P3)/2*P9.
//   - Bu lông qua CGP + bụng dầm: P18*P21 (P22), lỗ ô van P27 = P24 - d - P26, dung sai P26.
//   - FB lót cánh (P14 = 1), dài = bề rộng cánh + 20 mỗi bên (D56/D57).
//   - WP1/WP2 (bản cánh mở rộng, P50/P51) theo ảnh mặt bằng component gốc (18/11/2026), xem CreateWing.
// Chưa dựng (ghi CANH BAO): cắt bụng dầm P62/P63, cắt CGP P64/P65 - chưa có ảnh Custom Component Editor.
//
// Hệ cục bộ (Plugin\CLAUDE.md mục 4.4, theo RAKER+T_GIRDER):
//   gốc A = giao trục dầm với mặt bao ngoài của cột mà trục dầm đi vào; X dọc dầm ra xa cột; Y = chiều cao dầm; Z = X x Y.
//   _n = pháp tuyến mặt cột (hướng về phía dầm) trong hệ cục bộ; góc xiên P66 = acos(n.X).
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSMPlane = Tekla.Structures.Model.Plane;

using UEL.Core;

namespace UEL.Core.Components.HColToGirder
{
    public class HColToGirderBuilder
    {
        private static readonly CultureInfo CI = CultureInfo.InvariantCulture;

        private readonly Model _model;
        private readonly HColToGirderParams _p;
        private readonly TableRow _row;
        private readonly List<ModelObject> _created = new List<ModelObject>();

        private V3 _n;              // pháp tuyến mặt cột trong hệ cục bộ (hướng về phía dầm)
        private V3 _yp, _zp;        // 2 trục nằm trong mặt cột
        private double _up = 1.0;   // +1: Y cục bộ hướng lên (cánh +Y là cánh trên)
        private V3 _A, _X, _Y, _Z;  // hệ cục bộ (toàn cục)
        private string _headCol = "", _headGir = "";

        // dầm
        private double _h, _b, _tw, _tf;

        public HColToGirderBuilder(Model model, HColToGirderParams p, TableRow row)
        {
            _model = model;
            _p = p;
            _row = row;
        }

        public List<ModelObject> Created { get { return _created; } }

        public bool Build(Beam column, Beam girder)
        {
            Logger.Log("Thong so: " + _p);
            WorkPlaneHandler wph = _model.GetWorkPlaneHandler();
            TransformationPlane original = wph.GetCurrentTransformationPlane();
            try
            {
                wph.SetCurrentTransformationPlane(new TransformationPlane());   // làm việc ở hệ toàn cục
                return BuildInGlobal(wph, column, girder);
            }
            finally
            {
                wph.SetCurrentTransformationPlane(original);
            }
        }

        private bool BuildInGlobal(WorkPlaneHandler wph, Beam column, Beam girder)
        {
            // Prefix (Plugin\CLAUDE.md 4.3): CGP hàn vào cột -> assembly Part 1; FB/WP hàn vào dầm -> assembly Part 2.
            // .uel gốc: P12 = P73 = mid(prefix cột,0,find(..,"M")), SERIE = P12+"CGP"; P54 = P71 (theo dầm), SERIE = P54+"FB"/"WP".
            // Ô P12/P54 nhập tay thì dùng nguyên giá trị đó như .uel.
            _headCol = _p.CgpZone.Trim().Length > 0 ? _p.CgpZone.Trim() : PartPrefix.HeadOfAssembly(column, "Part 1 (cot)");
            _headGir = _p.GwpZone.Trim().Length > 0 ? _p.GwpZone.Trim() : PartPrefix.HeadOfAssembly(girder, "Part 2 (dam)");

            // ---- 1. Trục 2 cấu kiện ----
            V3 c0, c1, g0, g1;
            if (!CenterLine(column, out c0, out c1) || !CenterLine(girder, out g0, out g1))
            {
                Logger.Log("Khong doc duoc truc cau kien.");
                return false;
            }
            Line colAxis = new Line(c0.ToPoint(), c1.ToPoint());
            bool g1IsNear = Distance.PointToLine(g1.ToPoint(), colAxis) < Distance.PointToLine(g0.ToPoint(), colAxis);
            V3 near = g1IsNear ? g1 : g0;
            V3 far = g1IsNear ? g0 : g1;
            V3 dir = (near - far).Unit();                       // từ dầm hướng vào cột

            // ---- 2. Tiết diện ----
            CoordinateSystem csC = column.GetCoordinateSystem();
            V3 cx = (c1 - c0).Unit();
            V3 cy = V3.From(csC.AxisY).OrthoTo(cx).Unit();
            V3 cz = cx.Cross(cy).Unit();
            double hc = Report(column, "PROFILE.HEIGHT");
            double bc = Report(column, "PROFILE.WIDTH");
            _h = Report(girder, "PROFILE.HEIGHT");
            _b = Report(girder, "PROFILE.WIDTH");
            _tw = Report(girder, "PROFILE.WEB_THICKNESS");
            _tf = Report(girder, "PROFILE.FLANGE_THICKNESS");
            if (_h <= 0 || _b <= 0 || _tw <= 0 || _tf <= 0)
            {
                Logger.Log("Dam phai la tiet dien H/I (khong doc duoc HEIGHT/WIDTH/WEB/FLANGE).");
                return false;
            }
            if (hc <= 0 || bc <= 0)
            {
                Logger.Log("Khong doc duoc kich thuoc tiet dien cot.");
                return false;
            }

            // ---- 3. Mặt cột mà trục dầm đi vào ----
            V3 contact, normal;
            if (!EnvelopeEntry(far, dir, c0, cy, cz, hc, bc, out contact, out normal))
            {
                Logger.Log("Truc dam khong di vao mat bao cua cot (kiem tra lai cach chon: Part 1 = cot, Part 2 = dam).");
                return false;
            }
            bool faceIsWidth = Math.Abs(normal.Dot(cy)) > 0.5;          // mặt vuông góc cy -> bề rộng mặt = bc
            double faceHalf = (faceIsWidth ? bc : hc) / 2.0;
            // Part 1 là CỘT H (người dùng 18/11/2026): vào cánh hay vào bụng đều dùng HÌNH BAO NGOÀI như cột hộp
            // (y hệt COL_TO_GIRDER). Vào bụng thì mặt tiếp xúc là mặt phẳng qua mép 2 cánh cột - phần bù (t_bar) do component khác làm.
            Logger.Log("Cot H: dam vao " + (faceIsWidth ? "MAT CANH cot" : "phia BUNG cot (mat bao qua mep 2 canh, phan bu t_bar lam rieng)") + ".");

            // ---- 4. Hệ cục bộ ----
            V3 X = (far - near).Unit();
            double t = (contact - far).Dot(normal) / X.Dot(normal);
            V3 A = far + X * t;
            CoordinateSystem csG = girder.GetCoordinateSystem();
            V3 Y = V3.From(csG.AxisY).OrthoTo(X).Unit();
            V3 Z = X.Cross(Y).Unit();
            _A = A; _X = X; _Y = Y; _Z = Z;
            _up = Y.Z >= 0 ? 1.0 : -1.0;
            if (Math.Abs(Y.Z) < 0.05)
                Logger.Log("CANH BAO: dam xoay ~90 do quanh truc, khong ro canh tren/duoi - lay canh +Y cuc bo lam canh tren.");

            _n = new V3(normal.Dot(X), normal.Dot(Y), normal.Dot(Z));
            if (_n.X < 0.1)
            {
                Logger.Log("Dam gan nhu song song voi mat cot, khong dung duoc.");
                return false;
            }
            _yp = new V3(0, 1, 0).OrthoTo(_n).Unit();
            _zp = _n.Cross(_yp).Unit();

            // P66 = fAD("skew", dầm); P67 = 0.5*P17*sin(P66); P68 = P47*cos(P66); P69 = P67 + P68
            double skew = Math.Acos(Math.Min(1.0, _n.X));
            Logger.Log(string.Format(CI, "Mat cot: {0} (cot H={1}, B={2}); goc dam - phap tuyen mat cot = {3:0.###} do (doc {4:0.###}, xien {5:0.###})",
                faceIsWidth ? "mat rong B" : "mat cao H", hc, bc, skew * 180 / Math.PI,
                Math.Asin(Math.Max(-1, Math.Min(1, _n.Y))) * 180 / Math.PI, Math.Atan2(_n.Z, _n.X) * 180 / Math.PI));
            if (Math.Abs(_n.Y) > 0.02)
                Logger.Log("CANH BAO: dam doc so voi mat cot - .uel goc chi tinh goc xien (skew); kich thuoc theo chieu cao do theo Y cua dam.");

            // ---- 5. Các giá trị tính như .uel ----
            double P1 = _tw, P3 = _row.T3, P8 = _tf;
            double P9 = _p.CgpDirection ? -1.0 : 1.0;                               // P9 = if (P11==0) then 1 else -1
            double P17 = _row.T3, P19 = _row.B3, P18 = _row.BoltRow;
            double P20 = Math.Round(_h);                                            // P20 = fP(proH, dầm) (Integer)
            double P21 = P20 >= _p.TwoPitchHeight ? (P19 - _p.BoltEdgeSum) / 2.0 : (P19 - _p.BoltEdgeSum);
            string P22 = _p.BoltPitchSet ? _p.BoltPitch
                       : (P18 == 0 ? "0" : P18.ToString(CI) + "*" + P21.ToString("0.###", CI));   // P22 = P18+"*"+P21
            double d = _p.BoltSize;
            // 18/11/2026: cỡ bu lông không có trong công thức P24/P26 của .uel -> thông báo, không dựng
            if (!KnownBoltSize(d))
            {
                Logger.Error(string.Format(CI, "Bolt size M{0} khong co trong cong thuc P24/P26 cua .uel (chi co M16, M20, M22, M24, M27).", d));
                return false;
            }
            double P24 = _p.SlotLengthSet ? _p.SlotLength : SlotByDiameter(d);      // P24 theo cỡ bu lông
            double P26 = ToleranceByDiameter(d);                                    // P26 theo cỡ bu lông
            double P27 = P24 - d - P26;                                             // P27 = P24-P25-P26
            double P47 = _p.L3Set ? _p.L3 : _row.L3;                                // P47 = fVF(..,P15,2)
            double P57 = Math.Max(_p.TopRingThk, _p.BottRingThk);                   // P57 = max(P55,P56)
            double P67 = 0.5 * P17 * Math.Sin(skew), P68 = P47 * Math.Cos(skew), P69 = P67 + P68;
            // D5 = if (P14==1) then 5 else 20 (P-BOX TANG_DUONG: else 30) -> P75 / P74 (biến mới)
            double D5 = _p.Moment ? _p.MoGap : _p.PinGap;
            double P29 = bc, P30 = _b, P31 = P29 - P30;                             // COL_WIDTH, GIR_WIDTH (proB)
            double P35 = P31 >= 150 ? 40 : P31 / 2.0 / 2.0;                         // P35 / P36
            Logger.Log(string.Format(CI,
                "Tinh: P1(tw)={0} P3=P17(t3)={1} P9={2} P18={3} P19(B3)={4} P20(H)={5} P21={6:0.##} P22='{7}' P24={8} P26={9} P27={10} " +
                "P47(L3)={11} P57={12} P66={13:0.###} P67={14:0.##} P69={15:0.##} D5={16} P31={17} P32(wing)={18} P35={19}",
                P1, P3, P9, P18, P19, P20, P21, P22, P24, P26, P27, P47, P57, skew * 180 / Math.PI, P67, P69, D5, P31, P31 >= 150 ? 1 : 0, P35));

            // WP1/WP2: P48/P49 = floor(D113 / D122) < 74 ? 0 : 1, P50/P51 = (P48/P49==0 || P14==0) ? 0 : 1.
            // D113/D122 là khoảng đo trong .uel -> GIẢ ĐỊNH = phần cột thò ra ngoài mép cánh dầm mỗi bên (đo trên mặt cột):
            //   WP1 = bên -W, WP2 = bên +W (người dùng 18/11/2026: bản trước WP1 = +W bị nhầm bên -> đổi trái - phải).
            //   Ảnh mặt bằng component gốc: WP 2 bên cánh, rộng tới mép cột (bản P-BOX: D101..D104 = P29/2 = nửa bề rộng cột).
            V3 W = _n.Cross(new V3(0, 1, 0)).Unit();                               // vết mặt phẳng cánh trên mặt cột
            double wz = Math.Max(0.05, Math.Abs(W.Z));
            double flangeHalfW = _b / 2.0 / wz;                                     // nửa vết cánh trên mặt cột (đo theo W)
            const int sideWp1 = -1, sideWp2 = +1;
            // Sửa 18/11/2026 (người dùng: dầm nghiêng / xiên tính sai điều kiện WP): đo HOÀN TOÀN TRÊN MẶT CỘT TIẾP XÚC DẦM,
            // theo phương NẰM NGANG vuông góc trục cột (Hl), tại cao độ giữa cánh trên:
            //   phần cột thò ra = từ điểm mép cánh chạm mặt cột tới mép cột (không tính chiều dốc / xiên của cả dầm).
            //   Bản trước đo theo W và lấy trục cột tại điểm chân cột -> khi W không nằm ngang (dầm nghiêng/vặn) thì sai.
            V3 Hl = _n.Cross(Local(cx)).Unit();                                     // ngang trên mặt cột (cột đứng)
            if (Hl.Dot(W) < 0) Hl = Hl * -1.0;                                      // cùng chiều +W
            double kWH = Math.Max(0.05, W.Dot(Hl));                                 // 1 mm theo Hl = 1/kWH mm theo W
            V3 Pc = Local(c0 - _A);                                                 // 1 điểm trên trục cột (Hl vuông góc trục)
            double yTop = _up * (_h / 2.0 - _tf / 2.0);
            Func<V3, double> Hc = delegate(V3 q) { return (q - Pc).Dot(Hl); };
            double hE1 = Hc(OnPlane(0, yTop, _b / 2.0)), hE2 = Hc(OnPlane(0, yTop, -_b / 2.0));
            double ovPlus = faceHalf - Math.Max(hE1, hE2);                          // mép cánh bên +W -> mép cột bên +W
            double ovMinus = faceHalf + Math.Min(hE1, hE2);                         // bên -W
            // mép cột đo theo W trên vết cánh (để dựng WP: rộng đúng từ mép cánh tới mép cột)
            double hO = Hc(OnPlane(0, yTop, 0));
            double wColPlus = (faceHalf - hO) / kWH, wColMinus = (-faceHalf - hO) / kWH;
            Logger.Log(string.Format(CI, "Mat cot: rong {0:0.#}, mep canh dam cham mat cot tai {1:0.#} / {2:0.#} (tu truc cot, phuong ngang) -> cot tho ra +W {3:0.#}, -W {4:0.#}",
                2 * faceHalf, Math.Max(hE1, hE2), Math.Min(hE1, hE2), ovPlus, ovMinus));
            double D113 = sideWp1 > 0 ? ovPlus : ovMinus;                           // bên WP1
            double D122 = sideWp2 > 0 ? ovPlus : ovMinus;                           // bên WP2
            // Điều kiện có WP (như .uel: P48 = floor(D113)<74 ? 0 : 1; P50 = (P48==0 || P14==0) ? 0 : 1; P50 nhập tay thì thắng):
            //   1. P14 = Yes (MO);  2. cột thò ra ngoài mép cánh bên đó >= 74 mm;  3. (plugin) P28 > 0 (bảng tra cột 5 / ô P28);
            //   4. (plugin) khi dựng: cột thò ra >= 10 mm (P50/P51 nhập tay = 1 mà cột không thò ra thì vẫn bỏ qua).
            // Người dùng 18/11/2026: H-COL_TO_GIRDER KHÔNG có WP và KHÔNG có ring (đã bỏ khỏi hộp thoại; H-COL.uel cũng có P48 = P49 = 0).
            // Ring = 0 -> không lùi cánh theo ring; FB = cánh + 20 mỗi bên. Code WP còn lại của COL_TO_GIRDER không chạy (P50 = P51 = 0).
            const int P50 = 0, P51 = 0;
            string P37 = girder.Material.MaterialString;
            double P28 = 0;
            Logger.Log("H-COL_TO_GIRDER: khong co WP, khong co ring.");

            wph.SetCurrentTransformationPlane(new TransformationPlane(A.ToPoint(), X.ToVector(), Y.ToVector()));

            // ---- 6. Dầm: fitting, lùi cánh theo ring, vát mép ----
            Fit(girder, D5);                                                        // D5.FITTING.COLUMN
            double yi = _h / 2.0 - _tf;
            double topEnd = Math.Max(D5, 5.0 + _p.TopRingThk);                      // D51/D52 = 5 + P55
            double botEnd = Math.Max(D5, 5.0 + _p.BottRingThk);                     // D46/D47 = 5 + P56
            double s = _up;
            if (topEnd > D5 + 0.1) SlabCut(girder, D5 - 50.0, topEnd, s * yi - 1.0, s * (_h / 2.0 + 5.0));
            if (botEnd > D5 + 0.1) SlabCut(girder, D5 - 50.0, botEnd, -s * (_h / 2.0 + 5.0), -s * yi + 1.0);
            if (_p.Moment)
            {
                // D53 = (P8-2)*tan(P7)+D5+P55 (cánh trên), D48 = (P8-2)*tan(P7)+D5+P56 (cánh dưới)
                CutBevel(girder, topEnd, s * yi, s * _h / 2.0, _b);
                CutBevel(girder, botEnd, -s * _h / 2.0, -s * yi, _b);
                // Scallop ở góc bụng - cánh, CẢ cánh trên lẫn cánh dưới (người dùng 18/11/2026).
                CutScallop(girder, topEnd, s * yi);
                CutScallop(girder, botEnd, -s * yi);
                // Cánh dưới phải đứt hết sát cột (người dùng 18/11/2026, ảnh: còn sót): khối cắt xuyên hết chiều dày cánh dưới,
                // song song mặt cột, từ đầu cánh tại mặt chân (botEnd) lùi về phía cột ScallopLength (200). Vượt mặt ngoài
                // cánh 5 mm, tới mặt trong cánh (phần bụng phía trên do scallop cắt). Không đụng tới phần cánh đã vát.
                SlabCut(girder, botEnd - _p.ScallopLength, botEnd, -s * (_h / 2.0 + 5.0), -s * yi);
                Logger.Log(string.Format(CI, "Canh duoi: cat xuyen chieu day canh tu dau canh ({0:0.#} mm tu mat cot) lui ve phia cot {1}",
                    botEnd, _p.ScallopLength));
            }

            // ---- 7. CGP ----
            // D1..D4 = ((P1+P3)/2)*P9. Phía CGP:
            //   P83 = Yes (mặc định, người dùng 18/11/2026): theo hướng MODEL - P11 = 0 -> phía +X / +Y toàn cục, P11 = 1 -> -X / -Y.
            //     Lấy thành phần lớn hơn của pháp tuyến bụng (Z cục bộ, toàn cục): |Z.X| >= |Z.Y| -> xét X, ngược lại xét Y.
            //     => mọi dầm, cả 2 đầu, CGP cùng 1 phía theo model, không phụ thuộc chiều vẽ dầm.
            //   P83 = No: theo +Z riêng của dầm (AxisX x AxisY) - cùng phía ở 2 đầu 1 dầm, nhưng phụ thuộc chiều vẽ.
            // P83 (người dùng 18/11/2026, 4 trường hợp + theo dầm): 0 +X/+Y, 1 +X/-Y, 2 -X/+Y, 3 -X/-Y, 4 theo +Z riêng của dầm.
            // P11 = Yes: đảo sang phía đối diện cho riêng liên kết này (P9 = -1).
            string[] modeName = { "+X / +Y", "+X / -Y", "-X / +Y", "-X / -Y", "theo dam" };
            int mode = _p.CgpSideMode < 0 || _p.CgpSideMode > 4 ? 0 : _p.CgpSideMode;
            double zSign;
            string sideInfo;
            if (mode <= 3 && (Math.Abs(Z.X) > 0.05 || Math.Abs(Z.Y) > 0.05))
            {
                bool useX = Math.Abs(Z.X) >= Math.Abs(Z.Y);
                double wantX = mode == 0 || mode == 1 ? 1.0 : -1.0;
                double wantY = mode == 0 || mode == 2 ? 1.0 : -1.0;
                double want = useX ? wantX : wantY;
                zSign = ((useX ? Z.X : Z.Y) >= 0 ? 1.0 : -1.0) * want;          // +Z cục bộ cùng hướng phía muốn đặt?
                sideInfo = (want * P9 > 0 ? "+" : "-") + (useX ? "X" : "Y") + " model";
            }
            else
            {
                if (mode <= 3) Logger.Log("CANH BAO: bung dam gan nam ngang -> khong xac dinh phia X/Y, dat CGP theo +Z rieng cua dam.");
                V3 gzNative = V3.From(csG.AxisX).Cross(V3.From(csG.AxisY)).Unit();
                zSign = gzNative.Dot(Z) >= 0 ? 1.0 : -1.0;
                sideInfo = (P9 > 0 ? "+" : "-") + "Z rieng cua dam";
            }
            double z0 = zSign * P9 * (P1 + P3) / 2.0;
            Logger.Log("CGP: dat phia " + sideInfo + " (P83 = " + mode + " " + modeName[mode] + (_p.CgpDirection ? ", P11 = Yes: dao phia" : "") + ")");
            double xNear0, k;
            ContourPlate cgp = CreateCgp(z0, P17, P19, P47, out xNear0, out k);
            if (cgp == null) return false;

            // ---- 8. Bu lông ----
            // proDX1 = P4: cột bu lông cách cạnh ngắn ngoài CGP (phía xa cột) đúng P4, đo vuông góc cạnh đó (như eH của RAKER)
            // -> dọc trục dầm = P4 * sqrt(1+k^2). Sửa 18/11/2026 (đo trên model: e ra 36.47 thay vì 65): bỏ "+ P57" của
            // D8 = P57+P47, vì CGP không lùi theo ring nên cộng P57 làm bu lông lệch đúng bằng chiều dày ring.
            // P4: ô hộp thoại nếu có nhập; trống thì lấy cột "e" của bảng tra (nếu có); không có nữa thì mặc định 65.
            double P4 = _p.BoltToFlangeSet || double.IsNaN(_row.E) ? _p.BoltToFlange : _row.E;
            Logger.Log(string.Format(CI, "e (P4) = {0} ({1})", P4,
                _p.BoltToFlangeSet ? "hop thoai" : (double.IsNaN(_row.E) ? "mac dinh" : "bang tra, cot e")));
            double sk = Math.Sqrt(1.0 + k * k);
            double xb0 = xNear0 + (P47 - P4) * sk;
            CreateBolts(girder, cgp, P17, xb0, k, P22, P26, P27);

            // ---- 9. WP (bản cánh mở rộng) ----
            List<ContourPlate> wings = new List<ContourPlate>();
            if (P50 == 1 || P51 == 1)
            {
                if (P28 <= 1.0)
                    Logger.Log("CANH BAO: P28 (WING PL) = " + P28.ToString(CI) + " -> khong dung WP (bang tra cot 5 = 0?).");
                else
                {
                    string wpPrefix = PartPrefix.Make(_headGir, _p.WpMark);         // SERIE = P54 + "WP" (TANG_DUONG: "GWP", P76)
                    // P81 (biến mới): đoạn rộng tới mép cột, đo từ mặt cột; P82 (biến mới): bề rộng đầu xa ngoài mép cánh.
                    // Thứ tự: ô hộp thoại -> cột tuỳ chọn "L1"/"tip" của bảng tra -> công thức .uel ((P28+ring)/2, P35).
                    double tip = _p.WpTipSet ? _p.WpTip : (!double.IsNaN(_row.Tip) ? _row.Tip : P35);
                    Logger.Log(string.Format(CI, "WP: tip (P82) = {0} ({1}), L1 (P81) {2}", tip,
                        _p.WpTipSet ? "hop thoai" : (!double.IsNaN(_row.Tip) ? "bang tra, cot tip" : "P35"),
                        _p.WpStraightSet ? "= " + _p.WpStraight.ToString(CI) + " (hop thoai)"
                        : (!double.IsNaN(_row.L1) ? "= " + _row.L1.ToString(CI) + " (bang tra, cot L1)" : "= (P28+ring)/2")));
                    // {y giữa cánh, ring, đầu cánh, yRoot, yUp} - vát hàn như cánh cùng cao độ (cánh trên: vát mặt ngoài)
                    List<double[]> flanges = new List<double[]>();
                    flanges.Add(new[] { s * (_h / 2.0 - _tf / 2.0), _p.TopRingThk, topEnd, s * yi, s * _h / 2.0 });
                    if (_p.WpBothFlanges) flanges.Add(new[] { -s * (_h / 2.0 - _tf / 2.0), _p.BottRingThk, botEnd, -s * _h / 2.0, -s * yi });
                    double bevelWidth = 2.0 * Math.Max(Math.Abs(wColPlus), Math.Abs(wColMinus)) + 100.0;   // phủ hết bề rộng mặt cột
                    foreach (double[] f in flanges)
                    {
                        double straight = _p.WpStraightSet ? _p.WpStraight : (!double.IsNaN(_row.L1) ? _row.L1 : (P28 + f[1]) / 2.0);
                        // mép cột (phương ngang trên mặt cột) quy ra toạ độ W trên vết cánh ở cao độ cánh này
                        double hOf = Hc(OnPlane(0, f[0], 0));
                        double wcol1 = (sideWp1 * faceHalf - hOf) / kWH, wcol2 = (sideWp2 * faceHalf - hOf) / kWH;
                        ContourPlate w1 = P50 == 1 ? CreateWing(f[0], f[1], f[2], P28, straight, tip, sideWp1,
                            wcol1, W, P8, P37, wpPrefix, "WP1") : null;
                        ContourPlate w2 = P51 == 1 ? CreateWing(f[0], f[1], f[2], P28, straight, tip, sideWp2,
                            wcol2, W, P8, P37, wpPrefix, "WP2") : null;
                        // Vát hàn WP (người dùng 18/11/2026), góc P7, mặt chân 2 mm, như cánh dầm cùng cao độ:
                        //   - cạnh phía cột: khe chân = đầu cánh (đã tính ring);
                        //   - cạnh tiếp xúc mép cánh dầm (Part 2): vát cùng kiểu, khe chân 0.
                        int[] sides = { sideWp1, sideWp2 };
                        ContourPlate[] wps = { w1, w2 };
                        for (int i = 0; i < 2; i++)
                        {
                            ContourPlate wp = wps[i];
                            if (wp == null) continue;
                            wings.Add(wp);
                            if (!_p.Moment) continue;
                            CutBevel(wp, f[2], f[3], f[4], bevelWidth);
                            CutEdgeBevel(wp, f[0], f[3], f[4], sides[i], W, f[2], P28 + f[1]);
                        }
                    }
                }
            }

            // ---- 10. FB ----
            List<Beam> bars = new List<Beam>();
            if (_p.Moment)
            {
                // FB ở phía chân mối hàn: dưới mặt trong cánh trên, dưới mặt ngoài cánh dưới; tì vào mặt cột (ring).
                // 2 đầu FB = 2 mặt D57 (bên WP1) / D56 (bên WP2): if (P50/P51==0) then 20 else (D113/D122 + 20) ngoài mép cánh.
                double ext1 = _p.FbExtend, ext2 = _p.FbExtend;                      // bên WP1, bên WP2
                if (_p.FbSymmetric)
                {
                    // P77 (P-BOX): D56 = D57 = P44 = if (P50==0) then P6/2+20 else (P30/2+D113)+20 -> 2 bên theo WP1
                    ext1 = ext2 = P50 == 1 ? D113 / kWH + _p.FbExtend : _p.FbExtend;
                    Logger.Log("FB: 2 ben doi xung theo WP1 (P77 = Yes).");
                }
                else
                {
                    // D113/D122 đo theo phương ngang trên mặt cột -> quy ra chiều dài dọc FB (theo W): / kWH
                    if (P50 == 1) ext1 = D113 / kWH + _p.FbExtend;
                    if (P51 == 1) ext2 = D122 / kWH + _p.FbExtend;
                }
                double extA = sideWp1 > 0 ? ext1 : ext2;                             // bên +W
                double extB = sideWp1 > 0 ? ext2 : ext1;                             // bên -W
                bars.Add(CreateFlatBar(_up * yi, _p.TopRingThk, flangeHalfW, extA, extB, W));
                // FB cánh dưới giữ như cũ (người dùng 18/11/2026): WP chỉ ở cánh trên nên chỉ FB cánh trên dài theo WP;
                // FB cánh dưới = bề rộng cánh + 20 mỗi bên (trừ khi bật giả định "WP ca canh duoi").
                double botA = _p.WpBothFlanges ? extA : _p.FbExtend, botB = _p.WpBothFlanges ? extB : _p.FbExtend;
                bars.Add(CreateFlatBar(-_up * _h / 2.0, _p.BottRingThk, flangeHalfW, botA, botB, W));
            }

            // ---- 11. Mối hàn ----
            Fillet(column, cgp, true);
            foreach (ContourPlate wp in wings)
            {
                if (wp == null) continue;
                if (_p.Moment)
                {
                    WpGirderCjp(girder, wp);                                         // WP - mép cánh dầm: vát, hàn CJP xưởng
                    FlangeCjp(wp, column, D5);                                       // WP hàn CJP vào cột như cánh dầm
                }
                else Fillet(girder, wp, true);
            }
            foreach (Beam fb in bars) if (fb != null) Fillet(girder, fb, true);
            if (_p.Moment) FlangeCjp(girder, column, D5);
            return true;
        }

        // ----------------------------------------------------------------- công thức .uel

        /// P24 = if d==16 then 40 else if 20 then 50 else if 22 then 55 else if 24 then 60 else if 27 then 67 (khác: trống = 0)
        private static bool KnownBoltSize(double d)
        {
            return Same(d, 16) || Same(d, 20) || Same(d, 22) || Same(d, 24) || Same(d, 27);
        }

        private static double SlotByDiameter(double d)
        {
            if (Same(d, 16)) return 40;
            if (Same(d, 20)) return 50;
            if (Same(d, 22)) return 55;
            if (Same(d, 24)) return 60;
            if (Same(d, 27)) return 67;
            Logger.Log("CANH BAO: P24 cua .uel khong co gia tri cho M" + d.ToString(CI) + " -> khong co lo o van (nhap P24 neu can).");
            return 0;
        }

        /// P26 = if d==16/20/22 then 2 else if d==24/27 then 3 (khác: trống = 0)
        private static double ToleranceByDiameter(double d)
        {
            if (Same(d, 16) || Same(d, 20) || Same(d, 22)) return 2;
            if (Same(d, 24) || Same(d, 27)) return 3;
            Logger.Log("CANH BAO: P26 (dung sai lo) cua .uel khong co gia tri cho M" + d.ToString(CI) + " -> dung 2 (gia dinh).");
            return 2;
        }

        /// Ghi rõ vì sao có / không có WP ở 1 bên.
        private void LogWpCondition(string name, bool manual, int flag, double over, double p28)
        {
            string why;
            if (manual) why = "nhap tay = " + flag;
            else why = string.Format(CI, "H-COL.uel: P48/P49 = 0 -> khong co (cot tho ra {0:0.#}; nhap P50/P51 = 1 de dung)", over);
            if (flag == 1 && p28 <= 1.0) why += " (nhung P28 = 0 -> khong dung)";
            if (flag == 1 && over < 10.0) why += " (nhung cot tho ra < 10 mm -> khong dung)";
            Logger.Log(name + ": " + why);
        }

        private static bool Same(double a, double b) { return Math.Abs(a - b) < 0.01; }

        // ----------------------------------------------------------------- hình học

        /// Tia từ "far" theo "dir" đi vào hộp bao tiết diện cột (|y| <= hc/2, |z| <= bc/2, trục x vô hạn).
        private static bool EnvelopeEntry(V3 far, V3 dir, V3 c0, V3 cy, V3 cz, double hc, double bc, out V3 contact, out V3 normal)
        {
            contact = far; normal = cy;
            V3 rel = far - c0;
            double fy = rel.Dot(cy), fz = rel.Dot(cz);
            double dy = dir.Dot(cy), dz = dir.Dot(cz);
            double inY, outY, inZ, outZ;
            if (!Slab(fy, dy, hc / 2.0, out inY, out outY)) return false;
            if (!Slab(fz, dz, bc / 2.0, out inZ, out outZ)) return false;
            double tIn = Math.Max(inY, inZ), tOut = Math.Min(outY, outZ);
            if (tIn > tOut || tOut < 0 || double.IsInfinity(tIn)) return false;
            contact = far + dir * tIn;
            normal = inY >= inZ ? cy * -Math.Sign(dy) : cz * -Math.Sign(dz);
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

        private void Fit(Beam girder, double gap)
        {
            Fitting fit = new Fitting();
            fit.Father = girder;
            TSMPlane pl = new TSMPlane();
            pl.Origin = (_n * gap).ToPoint();
            pl.AxisX = _yp.ToVector();
            pl.AxisY = _zp.ToVector();
            fit.Plane = pl;
            if (fit.Insert()) _created.Add(fit);
            else Logger.Log("Khong tao duoc fitting dau dam.");
        }

        /// Cắt bỏ phần dầm giữa 2 mặt song song mặt cột (khoảng cách d0..d1) trong dải cao độ y1..y2 (Y CỤC BỘ của dầm).
        /// Sửa 18/11/2026: mặt cắt ngang của khối nằm trong mặt phẳng chứa pháp tuyến mặt cột n và trục Y (vuông góc W),
        /// khối đùn theo W (nằm ngang trong mặt cột) -> 2 đầu song song mặt cột, dải y đúng cao độ dầm kể cả khi dầm dốc
        /// và khối dài (bản trước đo y theo trục trong mặt cột nên dầm dốc + khối dài thì dải y bị trượt).
        private void SlabCut(Beam girder, double d0, double d1, double y1, double y2)
        {
            V3 W = _n.Cross(new V3(0, 1, 0)).Unit();
            double ny = _n.Y, den = 1.0 - ny * ny;
            if (den < 1e-6) return;
            Func<double, double, V3> Q = delegate(double d, double y)
            {
                // điểm có khoảng cách d tới mặt cột, cao độ y, toạ độ W = 0: Q = a*n + b*(0,1,0)
                double a = (d - y * ny) / den, b = (y - d * ny) / den;
                return _n * a + new V3(0, b, 0);
            };
            List<V3> pts = new List<V3>();
            pts.Add(Q(d0, y1));
            pts.Add(Q(d1, y1));
            pts.Add(Q(d1, y2));
            pts.Add(Q(d0, y2));
            double width = _b / Math.Max(0.05, Math.Abs(W.Z)) + 200.0;     // vết cánh trên mặt cột + dư
            Cut(girder, pts, width);
        }

        /// Vát mép hàn CJP của 1 cánh (như RAKER+T_GIRDER): đầu cánh ở mặt cách mặt cột rootGap; mặt chân RootFace ở phía
        /// yRoot; phần còn lại tới mặt yUp vát góc P7 so với mặt cột, mở về phía yUp.
        /// width = bề rộng cần phủ theo phương ngang (cánh dầm: _b; WP: cả bề rộng mặt cột). Khối cắt chỉ cắt "girder" (part cha).
        private void CutBevel(Part girder, double rootGap, double yRoot, double yUp, double width)
        {
            // Giống hệt CutBevel của RAKER+T_GIRDER (người dùng 18/11/2026: quay về kết quả trước, làm theo RAKER):
            // mặt chân RootFace (2 mm), khối cắt vượt mặt yUp 3 mm và lùi về phía cột 3 mm.
            const double extra = 3.0;
            double rootFace = _p.RootFace;
            double a = _p.BevelAngle * Math.PI / 180.0;
            double depth = Math.Abs(yUp - yRoot);
            if (_p.BevelAngle <= 0.1 || _p.BevelAngle >= 89.0 || depth <= rootFace + 0.5) return;
            double sg = Math.Sign(yUp - yRoot);

            V3 Yv = new V3(0, 1, 0);
            V3 w = _n.Cross(Yv).Unit();
            V3 u = Yv.Cross(w).Unit();
            if (u.Dot(_n) < 0) u = u * -1.0;
            double nu = _n.Dot(u);
            if (nu < 1e-3 || Math.Abs(w.Z) < 1e-3) return;

            V3 q0 = OnPlane(rootGap, yRoot + sg * rootFace, 0.0);
            V3 fdir = (Yv * sg + u * (-sg * _n.Y / nu)).Unit();
            V3 qn = (u - fdir * u.Dot(fdir)).Unit();
            V3 bdir = fdir * Math.Cos(a) + qn * Math.Sin(a);
            double rise = depth - rootFace + extra;
            V3 pFit = q0 + fdir * (rise / Math.Abs(fdir.Y));
            V3 pBev = q0 + bdir * (rise / Math.Abs(bdir.Y));
            List<V3> pts = new List<V3>();
            pts.Add(q0 - u * extra);
            pts.Add(q0);
            pts.Add(pBev);
            pts.Add(pFit - u * extra);
            double dU = 0.0;
            foreach (V3 q in pts) dU = Math.Max(dU, Math.Abs((q - q0).Dot(u)));
            double len = (width + 2.0 * dU * Math.Abs(u.Z)) / Math.Abs(w.Z) + 100.0;
            Cut(girder, pts, len);
            Logger.Log(string.Format(CI, "Vat mep canh {0}: goc {1:0.#} do, khe chan {2:0.#}, mat chan {3:0.#} mm",
                yRoot * _up > 0 ? "tren (vat mat ngoai)" : "duoi (vat mat trong)", _p.BevelAngle, rootGap, rootFace));
        }

        /// Vát hàn cạnh WP tiếp xúc mép cánh dầm (người dùng 18/11/2026: "tương tự vết đang có"):
        ///   mép cánh dầm là mặt phẳng z = sz*b/2 (song song trục dầm); mặt chân RootFace ở phía yRoot (như cánh cùng cao độ),
        ///   phần còn lại tới mặt yUp vát góc P7 so với mặt mép cánh, mở về phía yUp, ăn vào WP (ra xa cánh).
        ///   Khối cắt là lăng trụ chạy dọc trục dầm, phủ hết chiều dài WP (+50 mỗi đầu); vượt mặt yUp 3 mm, lùi vào cánh 3 mm.
        private void CutEdgeBevel(Part wp, double yMid, double yRoot, double yUp, int side, V3 W, double uNear, double uFar)
        {
            const double extra = 3.0;
            double rootFace = _p.RootFace;
            double a = _p.BevelAngle * Math.PI / 180.0;
            double depth = Math.Abs(yUp - yRoot);
            if (_p.BevelAngle <= 0.1 || _p.BevelAngle >= 89.0 || depth <= rootFace + 0.5) return;
            double sg = Math.Sign(yUp - yRoot);
            double dz = (OnPlane(0, yMid, 1.0) - OnPlane(0, yMid, 0)).Dot(W);
            double sz = (dz >= 0 ? 1.0 : -1.0) * side;                         // phía WP theo Z cục bộ
            double zE = sz * _b / 2.0;                                         // mép cánh dầm
            double x0 = OnPlane(uNear, yMid, zE).X, x1 = OnPlane(uFar, yMid, zE).X;
            double xc = (x0 + x1) / 2.0, len = Math.Abs(x1 - x0) + 100.0;

            double y0 = yRoot + sg * rootFace;                                 // đầu đường vát (trên mặt chân)
            double rise = depth - rootFace + extra;
            double y1 = y0 + sg * rise;
            List<V3> pts = new List<V3>();
            pts.Add(new V3(xc, y0, zE - sz * extra));
            pts.Add(new V3(xc, y0, zE));
            pts.Add(new V3(xc, y1, zE + sz * rise * Math.Tan(a)));
            pts.Add(new V3(xc, y1, zE - sz * extra));
            Cut(wp, pts, len);
            Logger.Log(string.Format(CI, "WP: vat canh tiep xuc mep canh dam, goc {0:0.#}, mat chan {1:0.#}", _p.BevelAngle, rootFace));
        }

        /// Mối hàn WP - mép cánh dầm: CJP vát 1 phía, hàn xưởng.
        private void WpGirderCjp(Beam girder, ContourPlate wp)
        {
            Weld w = new Weld();
            w.MainObject = girder;
            w.SecondaryObject = wp;
            w.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_BEVEL_GROOVE_SINGLE_BEVEL_BUTT;
            w.AngleAbove = _p.BevelAngle;
            w.ShopWeld = true;
            if (w.Insert()) _created.Add(w);
        }

        /// Scallop ở góc bụng - mặt trong cánh (yFace) (người dùng 18/11/2026, theo ảnh Chamfer Properties: Type = rounding, x = 35):
        ///   khối cắt HÌNH CHỮ NHẬT dài ScallopLength (200) dọc dầm, cao S = P80 (35, hoặc 30) tính từ mặt trong cánh vào bụng;
        ///   góc phía dầm, xa cánh được BO TRÒN bán kính S (chamfer rounding) -> mép scallop là 1/4 cung tròn R = S,
        ///   gặp mặt trong cánh vuông góc tại khoảng cách đầu cánh + S tính từ mặt cột.
        ///   Khối cắt kéo dài về phía cột (dài 200) nên luôn cắt hết đầu bụng. Chỉ cắt trong chiều dày bụng (tw + 4),
        ///   không chạm cánh. Toạ độ dọc dầm đi theo vết mặt cột (dầm dốc).
        private void CutScallop(Beam girder, double flangeEnd, double yFace)
        {
            double S = _p.ScallopSize, L = Math.Max(_p.ScallopLength, S + 10.0);
            if (S <= 1.0) return;
            double sg = Math.Sign(yFace);
            double yIn = yFace - sg * S;                                       // mép trong (phía bụng) của khối cắt
            double dEnd = flangeEnd + S;                                       // đầu phía dầm, tính từ mặt cột
            Func<double, double, V3> P = delegate(double d, double y) { return new V3(OnPlane(d, y, 0).X, y, 0); };

            // Sửa 18/11/2026 (người dùng: dầm mất hình dạng): KHÔNG dùng Chamfer CHAMFER_ROUNDING của Tekla - cạnh đứng của
            // khối cắt dài đúng bằng bán kính (= S) nên phần bo nuốt hết cạnh, đỉnh suy biến, boolean hỏng làm dầm biến mất.
            // Vẽ 1/4 cung tròn bằng các điểm (cách 7,5 độ), tâm (dEnd - S, yFace): đi từ (dEnd, yFace) xuống tới (dEnd - S, yIn).
            List<V3> pts = new List<V3>();
            pts.Add(P(dEnd - L, yFace));
            for (int i = 0; i <= 12; i++)
            {
                double a = i * 7.5 * Math.PI / 180.0;                          // 0 .. 90 độ
                pts.Add(P(dEnd - S + S * Math.Cos(a), yFace - sg * S * Math.Sin(a)));
            }
            pts.Add(P(dEnd - L, yIn));
            Cut(girder, pts, _tw + 4.0);
            Logger.Log(string.Format(CI, "Scallop {0}: chu nhat dai {1}, sau {2}, bo tron R{2} goc phia dam, toi {3:0.#} mm tu mat cot",
                yFace * _up > 0 ? "tren" : "duoi", L, S, dEnd));
        }

        /// CGP: PL P17, cao P19 (D26..D29 = P19/2 quanh trục dầm), dài P47 dọc dầm, mặt giữa ở z0.
        /// Làm như bản mã RAKER+T_GIRDER (người dùng 18/11/2026, NGUYEN_TAC_TU_RAKER mục 5.5-5.6):
        ///   - Part 2 nghiêng so với cột (mặt cột nghiêng theo chiều cao dầm, k = -n.Y/n.X != 0) -> CGP là HÌNH BÌNH HÀNH:
        ///     cạnh ngắn phía cột nằm trên vết mặt cột (dx = k*dy), cạnh ngắn phía ngoài song song cạnh đó, cách L3 dọc dầm;
        ///     cạnh dài dọc trục dầm; chiều cao B3 đo VUÔNG GÓC cạnh dài (theo Y).
        ///   - Không cắm vào mặt cột: dầm xiên (n.Z != 0) thì 1 mặt tấm cắm vào cột -> đẩy cả tấm dọc dầm tới khi góc sâu nhất
        ///     vừa chạm mặt cột (.uel: D24/D25 = P67 = 0.5*P17*sin(P66)).
        /// xNear0 = cạnh phía cột tại y = 0 (sau khi đẩy); k = độ nghiêng cạnh ngắn.
        private ContourPlate CreateCgp(double z0, double thk, double height, double length, out double xNear0, out double k)
        {
            double H = height / 2.0, half = thk / 2.0;
            k = -_n.Y / _n.X;
            xNear0 = 0;
            if (length <= 1.0 || height <= 1.0)
            {
                Logger.Log("CGP co kich thuoc <= 0 - kiem tra bang tra (L3, B3).");
                return null;
            }
            if (H > _h / 2.0 - _tf)
                Logger.Log(string.Format(CI, "CANH BAO: CGP cao {0} > khoang giua 2 canh dam {1} - kiem tra bang tra B3.", height, _h - 2 * _tf));

            V3 nearTop = OnPlane(0.0, H, z0);
            V3 nearBot = OnPlane(0.0, -H, z0);
            double push = 0.0;
            foreach (V3 q in new[] { nearTop, nearBot })
                foreach (double s in new[] { -1.0, 1.0 })
                    push = Math.Max(push, OnPlane(0.0, q.Y, q.Z + s * half).X - q.X);
            V3 shift = new V3(push, 0, 0);
            // L3 đo VUÔNG GÓC 2 cạnh ngắn (người dùng 18/11/2026) -> dọc trục dầm = L3 * sqrt(1+k^2)
            V3 along = new V3(length * Math.Sqrt(1.0 + k * k), 0, 0);
            xNear0 = OnPlane(0.0, 0.0, z0).X + push;

            List<V3> pts = new List<V3>();
            pts.Add(nearTop + shift);
            pts.Add(nearTop + shift + along);
            pts.Add(nearBot + shift + along);
            pts.Add(nearBot + shift);
            double tilt = Math.Atan(k) * 180.0 / Math.PI;
            Logger.Log(string.Format(CI, "CGP: PL{0} {1} x {2}, z = {3:0.##} (ben {4}), canh trong cach A {5:0.##} doc dam (day {6:0.##} de cham cot){7}",
                thk, length, height, z0, z0 >= 0 ? "+Z" : "-Z", xNear0, push,
                Math.Abs(tilt) > 0.05 ? string.Format(CI, "; hinh binh hanh, canh ngan nghieng {0:0.##} do so voi phuong vuong goc truc dam, L3 do vuong goc canh ngan (doc dam {1:0.##})", tilt, along.X) : ""));
            ContourPlate cp = Plate(pts, thk, _p.CgpMat, _p.CgpClass, PartPrefix.Make(_headCol, "CGP"), _p.GussetStartNo, NameOr(_p.CgpName, "CGP"));
            if (cp != null) Decorate(cp, "CGP", _p.CgpFinish, _p.CgpUda);
            if (cp != null)
            {
                double x0 = Math.Min(pts[0].X, pts[3].X), x1 = Math.Max(pts[1].X, pts[2].X);
                CheckBox(cp, "CGP", x0, x1, -H, H, z0 - half, z0 + half);
            }
            return cp;
        }

        /// WP (bản cánh mở rộng) 1 bên cánh, nằm trong mặt phẳng cánh (PL tf, mặt giữa ở yMid), theo ảnh mặt bằng gốc:
        ///   cạnh trong áp mép cánh; cạnh phía cột từ mép cánh tới mép cột (wCol, đo theo W trên mặt cột);
        ///   rộng tới mép cột đến khoảng cách (P28+ring)/2 từ mặt cột (D96/D121), rồi vát tới đầu xa P28+ring (D97/D98,
        ///   D125/D126) - đầu xa còn rộng P35 ngoài mép cánh (D114/D130 = P35).
        ///   Cạnh phía cột cách mặt cột bằng đầu cánh (ring + khe) - giả định.
        ///   Biến mới (18/11/2026): straight = đoạn rộng tới mép cột, đo từ mặt cột (P81, trống = (P28+ring)/2);
        ///   p35 = bề rộng đầu xa ngoài mép cánh (P82, trống = P35).
        /// side = +1 (bên +W) / -1 (bên -W). WP1 = bên -W, WP2 = bên +W.
        private ContourPlate CreateWing(double yMid, double ring, double uNear, double p28, double straight, double p35, int side,
                                        double wCol, V3 W, double tf, string mat, string prefix, string name)
        {
            double uFar = p28 + ring, uMid = Math.Min(uFar, Math.Max(uNear, straight));
            if (uFar <= uNear + 1.0)
            {
                Logger.Log("CANH BAO: " + name + ": P28 + ring <= khe dau canh -> bo qua.");
                return null;
            }
            // vết mép cánh trên mặt song song mặt cột ở khoảng cách u, đo theo W (dầm xiên thì mép cánh lệch theo u)
            double dz = (OnPlane(0, yMid, 1.0) - OnPlane(0, yMid, 0)).Dot(W);
            double sz = (dz >= 0 ? 1.0 : -1.0) * side;
            Func<double, double> wEdge = delegate(double u) { return (OnPlane(u, yMid, sz * _b / 2.0) - OnPlane(u, yMid, 0)).Dot(W); };
            Func<double, double, V3> P = delegate(double u, double w) { return OnPlane(u, yMid, 0) + W * w; };

            double over = (wCol - wEdge(uNear)) * side;
            if (over < 10.0)
            {
                Logger.Log(string.Format(CI, "{0}: cot chi tho {1:0.#} mm ngoai mep canh -> bo qua.", name, over));
                return null;
            }
            double wFar = wEdge(uFar) + side * p35;
            if ((wFar - wCol) * side > 0) wFar = wCol;

            List<V3> pts = new List<V3>();
            pts.Add(P(uNear, wEdge(uNear)));
            pts.Add(P(uNear, wCol));
            if (uMid > uNear + 0.5) pts.Add(P(uMid, wCol));
            if (uFar > uMid + 0.5 || Math.Abs(wFar - wCol) > 0.5) pts.Add(P(uFar, wFar));
            if (Math.Abs(p35) > 0.5) pts.Add(P(uFar, wEdge(uFar)));      // đầu xa rộng 0 -> nhọn, bỏ điểm trùng
            Logger.Log(string.Format(CI, "{0} ({1}): PL{2} {3}, dai {4:0.#} tu mat cot (vat tu {5:0.#}), rong {6:0.#} -> {7:0.#} ngoai mep canh",
                name, yMid * _up > 0 ? "canh tren" : "canh duoi", tf, mat, uFar, uMid, over, p35));
            ContourPlate wp = Plate(pts, tf, mat, _p.WpClass, prefix, _p.WingStartNo, NameOr(_p.WpName, "WP"));
            if (wp != null) Decorate(wp, name, _p.WpFinish, _p.WpUda);
            return wp;
        }

        /// Bu lông: 1 cột bu lông, các con theo P22 ("1*370" = 1 khoảng 370), đối xứng quanh trục dầm. Lỗ ô van P27 theo X
        /// (dọc dầm) ở CGP (giả định). Như RAKER: cột bu lông SONG SONG CẠNH NGẮN của CGP (x = xb0 + k*y); khoảng cách
        /// P22 và mép P21A đo VUÔNG GÓC cạnh dài (theo Y) -> đo dọc cột nghiêng = P22 * sqrt(1+k^2).
        /// Trục Y của nhóm bu lông có thể ngược Y cục bộ -> so vị trí thật, ngược thì tạo lại với Y đảo dấu.
        private void CreateBolts(Beam girder, ContourPlate cgp, double thk, double xb0, double k, string pitch, double tol, double slotX)
        {
            List<double> gaps = ParsePitch(pitch);
            if (gaps == null)
            {
                Logger.Log("CANH BAO: khong doc duoc P22 = '" + pitch + "' -> khong tao bu long.");
                return;
            }
            double total = 0; foreach (double g in gaps) total += g;
            List<double> ys = new List<double>(), xs = new List<double>();
            double y = -total / 2.0;
            ys.Add(y);
            foreach (double g in gaps) { y += g; ys.Add(y); }
            foreach (double yy in ys) xs.Add(k * yy);

            BoltXYList bolts = InsertBolts(girder, cgp, thk, xb0, xs, ys, 1.0, tol, slotX);
            if (bolts == null) { Logger.Log("Khong tao duoc nhom bu long."); return; }
            if (Math.Abs(k) > 1e-6 && ys.Count > 1 && bolts.Select())
            {
                double errAsIs = 0, errFlip = 0;
                ArrayList got = bolts.BoltPositions;
                for (int m = 0; m < xs.Count; m++)
                {
                    errAsIs += Nearest(got, xb0 + xs[m], ys[m]);
                    errFlip += Nearest(got, xb0 + xs[m], -ys[m]);
                }
                if (errFlip < errAsIs)
                {
                    bolts.Delete();
                    bolts = InsertBolts(girder, cgp, thk, xb0, xs, ys, -1.0, tol, slotX);
                    Logger.Log("Bu long: truc Y cua nhom nguoc chieu Y cuc bo -> da tao lai voi Y dao dau.");
                    if (bolts == null) { Logger.Log("Khong tao lai duoc nhom bu long."); return; }
                }
            }
            _created.Add(bolts);
            if (slotX > 0.01) FixSlotSide(bolts, cgp, girder);
            Logger.Log(string.Format(CI, "Bu long: {0} con M{1} {2}, cach A {3:0.##} doc dam (tai y = 0), khoang Y '{4}', lo o van X {5} (P41 = {6}){7}",
                ys.Count, _p.BoltSize, _p.BoltStandard, xb0, pitch, slotX + (slotX > 0.01 ? (_p.SlotInBeam ? " o DAM" : " o CGP") : ""),
                _p.CgpDirection ? "20000" : "10000",
                Math.Abs(k) > 1e-6 ? string.Format(CI, "; cot bu long song song canh nghieng CGP (lech {0:0.##} mm moi 100 mm theo Y)", k * 100) : ""));
        }

        private BoltXYList InsertBolts(Beam girder, ContourPlate cgp, double thk, double xb0, List<double> xs, List<double> ys,
                                       double ySign, double tol, double slotX)
        {
            BoltXYList bolts = new BoltXYList();
            bolts.PartToBoltTo = cgp;
            bolts.PartToBeBolted = girder;
            bolts.FirstPosition = new Point(xb0, 0, 0);
            bolts.SecondPosition = new Point(xb0 + 100.0, 0, 0);
            bolts.BoltSize = _p.BoltSize;
            bolts.BoltStandard = _p.BoltStandard;
            bolts.Tolerance = tol;
            bolts.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
            bolts.CutLength = 2.0 * (_tw + thk) + 60.0;
            bolts.ExtraLength = 0.0;                                  // người dùng 18/11/2026: Extra length mặc định 0
            bolts.Position.Depth = Position.DepthEnum.MIDDLE;
            bolts.Position.Plane = Position.PlaneEnum.MIDDLE;
            bolts.Position.Rotation = Position.RotationEnum.FRONT;   // thân bu lông theo Z cục bộ (xuyên bụng)
            bolts.Bolt = true;
            bolts.Washer1 = false; bolts.Washer2 = true; bolts.Washer3 = false;   // NGUYEN_TAC_TU_RAKER mục 6
            bolts.Nut1 = true; bolts.Nut2 = false;
            for (int m = 0; m < xs.Count; m++) { bolts.AddBoltDistX(xs[m]); bolts.AddBoltDistY(ys[m] * ySign); }
            if (slotX > 0.01)
            {
                // .uel: SLOTTED_HOLE_PARTS = P41 = if (P11==0) then 10000 else 20000; SLOTTED_HOLE_X = P27
                bolts.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_SLOTTED;
                bolts.SlottedHoleX = slotX;
                bolts.SlottedHoleY = 0.0;
                // P79: mặc định lỗ ô van ở CGP. Hole1/Hole2 = tấm thứ 1/thứ 2 của nhóm bu lông; tạo với tấm 1 = CGP
                // (PartToBoltTo), sau đó FixSlotSide đọc lại thứ tự Tekla thực sự dùng và sửa cờ nếu cần.
                bolts.Hole1 = !_p.SlotInBeam;
                bolts.Hole2 = _p.SlotInBeam;
            }
            return bolts.Insert() ? bolts : null;
        }

        /// Lỗ ô van LUÔN ở CGP (người dùng 18/11/2026: dầm nghiêng / vặn thì lỗ ô van có lúc rơi vào dầm).
        /// Cờ Hole1/Hole2 áp cho tấm thứ 1/thứ 2 theo thứ tự Tekla lưu trong nhóm bu lông; Tekla có thể đổi thứ tự này
        /// theo hình học khi Insert. API không cho đọc tấm nào có lỗ ô van, nên: đọc lại nhóm bu lông (Select), xem tấm
        /// thứ 1 (PartToBoltTo) và thứ 2 (PartToBeBolted) Tekla trả về là tấm nào, rồi đặt cờ đúng cho CGP và Modify.
        private void FixSlotSide(BoltGroup bolts, ContourPlate cgp, Beam girder)
        {
            if (!bolts.Select()) { Logger.Log("CANH BAO: khong doc lai duoc nhom bu long de kiem tra lo o van."); return; }
            int idCgp = cgp.Identifier.ID, idGir = girder.Identifier.ID;
            int id1 = bolts.PartToBoltTo == null ? 0 : bolts.PartToBoltTo.Identifier.ID;
            int id2 = bolts.PartToBeBolted == null ? 0 : bolts.PartToBeBolted.Identifier.ID;
            bool cgpFirst;
            if (id1 == idCgp || id2 == idGir) cgpFirst = true;
            else if (id1 == idGir || id2 == idCgp) cgpFirst = false;
            else
            {
                Logger.Log(string.Format(CI, "CANH BAO: nhom bu long tra ve tam {0} / {1}, khong khop CGP {2} / dam {3} - giu nguyen co lo o van.",
                    id1, id2, idCgp, idGir));
                return;
            }
            bool slotCgp = !_p.SlotInBeam;
            bool want1 = cgpFirst ? slotCgp : !slotCgp;
            bool want2 = !want1;
            if (bolts.Hole1 != want1 || bolts.Hole2 != want2 || bolts.Hole3 || bolts.Hole4 || bolts.Hole5)
            {
                bolts.Hole1 = want1; bolts.Hole2 = want2;
                bolts.Hole3 = false; bolts.Hole4 = false; bolts.Hole5 = false;
                bolts.Modify();
                Logger.Log("Lo o van: Tekla xep tam 1 = " + (cgpFirst ? "CGP" : "DAM") + " -> da sua co de lo o van o " + (slotCgp ? "CGP" : "DAM") + ".");
            }
            else Logger.Log("Lo o van: tam 1 = " + (cgpFirst ? "CGP" : "DAM") + ", lo o van o " + (slotCgp ? "CGP" : "DAM") + ".");
        }

        private static double Nearest(ArrayList pts, double x, double y)
        {
            double best = double.MaxValue;
            if (pts == null) return best;
            foreach (object o in pts)
            {
                Point p = o as Point;
                if (p == null) continue;
                best = Math.Min(best, Math.Sqrt((p.X - x) * (p.X - x) + (p.Y - y) * (p.Y - y)));
            }
            return best;
        }

        /// "1*370" -> [370]; "2*235" -> [235, 235]; "100 150" -> [100, 150]; "0" -> [] (1 con).
        private static List<double> ParsePitch(string s)
        {
            List<double> r = new List<double>();
            if (s == null) return null;
            foreach (string tok in s.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int k = tok.IndexOf('*');
                double v; int n = 1;
                if (k > 0)
                {
                    double nn;
                    if (!double.TryParse(tok.Substring(0, k), NumberStyles.Float, CI, out nn)) return null;
                    n = (int)Math.Round(nn);
                    if (!double.TryParse(tok.Substring(k + 1), NumberStyles.Float, CI, out v)) return null;
                }
                else if (!double.TryParse(tok, NumberStyles.Float, CI, out v)) return null;
                if (Math.Abs(v) < 1e-6) continue;
                for (int i = 0; i < n; i++) r.Add(v);
            }
            return r;
        }

        /// FB ngang dưới mặt yFace của cánh, tì vào mặt cột (cách mặt cột ring = độ dày ring của cánh đó),
        /// chạy song song mặt cột (theo W), dài = vết cánh + extA (bên +W) + extB (bên -W).
        private Beam CreateFlatBar(double yFace, double ring, double flangeHalfW, double extA, double extB, V3 W)
        {
            double wide, thin;
            FlatBarSize(out wide, out thin);
            double ym = yFace - _up * thin / 2.0;
            // đẩy dọc pháp tuyến để mặt sâu nhất (2 mặt trên/dưới thanh) vừa chạm mặt cột
            double dc = ring + wide / 2.0;
            V3 c = OnPlane(dc, ym, 0.0);
            double extraN = 0.0;
            foreach (double sy in new[] { -1.0, 1.0 })
            {
                V3 q = c + new V3(0, sy * thin / 2.0, 0);
                extraN = Math.Max(extraN, ring + wide / 2.0 - q.Dot(_n));
            }
            c = c + _n * extraN;
            V3 s = c + W * (flangeHalfW + extA);
            V3 e = c - W * (flangeHalfW + extB);

            Beam fb = new Beam();
            fb.StartPoint = s.ToPoint();
            fb.EndPoint = e.ToPoint();
            fb.Profile.ProfileString = _p.FbProfile;
            fb.Material.MaterialString = _p.FbMat;
            if (!string.IsNullOrEmpty(_p.FbClass)) fb.Class = _p.FbClass;
            fb.Name = NameOr(_p.FbName, "FB");
            fb.PartNumber.Prefix = PartPrefix.Make(_headGir, "FB");
            fb.PartNumber.StartNumber = Math.Max(1, _p.FbStartNo);
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
            double ey, ez;
            Extents(fb, out ey, out ez);
            if (Math.Abs(ey - thin) > 0.5)
            {
                fb.Position.Rotation = Position.RotationEnum.TOP;
                fb.Modify();
                fb.Select();
                Extents(fb, out ey, out ez);
            }
            Logger.Log(string.Format(CI, "FB {0} {1} mark={2}: dai {3:0.#}, tai y={4:0.#}, cao theo Y = {5:0.#}{6}",
                _p.FbProfile, _p.FbMat, fb.PartNumber.Prefix, (s - e).Norm(), ym, ey, Math.Abs(ey - thin) > 0.5 ? " CANH BAO: sai chieu" : ""));
            Decorate(fb, "FB", _p.FbFinish, _p.FbUda);
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

        private void FlangeCjp(Part girder, Beam column, double gap)
        {
            Weld w = new Weld();
            w.MainObject = column;
            w.SecondaryObject = girder;
            w.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_BEVEL_GROOVE_SINGLE_BEVEL_BUTT;
            w.AngleAbove = _p.BevelAngle;
            w.RootOpeningAbove = gap;
            w.ShopWeld = false;                                 // hàn công trường
            if (w.Insert()) _created.Add(w);
        }

        // ----------------------------------------------------------------- tiện ích

        /// Điểm (x, y, z) trên mặt phẳng song song mặt cột, cách mặt cột d (dọc pháp tuyến).
        private V3 OnPlane(double d, double y, double z)
        {
            return new V3((d - y * _n.Y - z * _n.Z) / _n.X, y, z);
        }

        private V3 Local(V3 v)
        {
            return new V3(v.Dot(_X), v.Dot(_Y), v.Dot(_Z));
        }

        private ContourPlate Plate(List<V3> pts, double thk, string mat, string cls, string prefix, int startNo, string name)
        {
            ContourPlate cp = new ContourPlate();
            foreach (V3 q in pts) cp.AddContourPoint(new ContourPoint(q.ToPoint(), null));
            cp.Profile.ProfileString = "PL" + thk.ToString("0.###", CI);
            cp.Material.MaterialString = mat;
            if (!string.IsNullOrEmpty(cls)) cp.Class = cls;
            cp.Name = name;
            cp.PartNumber.Prefix = prefix;
            cp.PartNumber.StartNumber = Math.Max(1, startNo);
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
            Logger.Log("Tao " + name + " " + cp.Profile.ProfileString + " " + mat + " mark=" + prefix + " start=" + cp.PartNumber.StartNumber);
            return cp;
        }

        private static string NameOr(string name, string fallback)
        {
            return string.IsNullOrEmpty(name) || name.Trim().Length == 0 ? fallback : name.Trim();
        }

        /// Tab Advanced (người dùng 18/11/2026): Finish và user-defined attributes cho part vừa tạo.
        /// uda = "TEN=GIA_TRI; TEN2=GIA_TRI2" (phân cách ; hoặc xuống dòng). Giá trị: số nguyên -> int, số thực -> double,
        /// còn lại -> chuỗi (bỏ dấu " bao ngoài nếu có). Thuộc tính phải có trong objects.inp của Tekla mới hiện trong hộp thoại part.
        private void Decorate(Part part, string who, string finish, string uda)
        {
            if (part == null) return;
            if (!string.IsNullOrEmpty(finish) && finish.Trim().Length > 0)
            {
                part.Finish = finish.Trim();
                if (!part.Modify()) Logger.Log("CANH BAO: " + who + ": khong gan duoc Finish '" + finish + "'.");
            }
            if (string.IsNullOrEmpty(uda)) return;
            foreach (string item in uda.Split(new[] { ';', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int eq = item.IndexOf('=');
                if (eq <= 0) { if (item.Trim().Length > 0) Logger.Log("CANH BAO: " + who + ": UDA '" + item.Trim() + "' thieu dau '=' -> bo qua."); continue; }
                string key = item.Substring(0, eq).Trim();
                string val = item.Substring(eq + 1).Trim();
                bool ok;
                int iv; double dv;
                if (val.Length >= 2 && val.StartsWith("\"") && val.EndsWith("\"")) ok = part.SetUserProperty(key, val.Substring(1, val.Length - 2));
                else if (int.TryParse(val, NumberStyles.Integer, CI, out iv)) ok = part.SetUserProperty(key, iv);
                else if (double.TryParse(val, NumberStyles.Float, CI, out dv)) ok = part.SetUserProperty(key, dv);
                else ok = part.SetUserProperty(key, val);
                Logger.Log(who + ": UDA " + key + " = " + val + (ok ? "" : "  CANH BAO: khong gan duoc"));
            }
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

        private static void Extents(Part part, out double ey, out double ez)
        {
            ey = ez = 0;
            Solid sol = part.GetSolid();
            if (sol == null) return;
            ey = sol.MaximumPoint.Y - sol.MinimumPoint.Y;
            ez = sol.MaximumPoint.Z - sol.MinimumPoint.Z;
        }

        private void Cut(Part father, List<V3> pts, double thk)
        {
            ContourPlate cutter = new ContourPlate();
            foreach (V3 q in pts) cutter.AddContourPoint(new ContourPoint(q.ToPoint(), null));
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
