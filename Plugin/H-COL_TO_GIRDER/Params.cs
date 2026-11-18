// Thông số liên kết Cột H -> Dầm H (custom component gốc "0-DM_H-Col2Gir_H-COLUMN", file File goc\H-COL-GIRDER\H-COL.uel).
// Clone từ COL_TO_GIRDER (18/11/2026): H-COL.uel dùng cùng tên biến với COL_TO_GIRDER.uel, chỉ khác:
//   P4 = 55, P13 = SM355, D5 khi PIN = 30 (-> P74), P48/P49 = 0 (không có WP), FB = cánh + 20 mỗi bên, Part 1 là cột H.
// Giá trị mặc định = giá trị mặc định của biến trong H-COL.uel (không có preset).
// Người dùng 18/11/2026: BỎ WP và ring (P28, P37, P50, P51, P53, P76, P77, P81, P82, P88-P91, P55-P65) khỏi hộp thoại;
// thuộc tính giữ lại (không có [UelParam], luôn = mặc định: ring 0, không WP) để dùng chung code với COL_TO_GIRDER.
// [UelParam] = tên biến trong hộp thoại gốc. [Category]/[DisplayName] chỉ dùng cho bảng của EXE thử nghiệm.
//
// Biến có mặc định là CÔNG THỨC trong .uel (P12, P22, P24, P28, P37, P47, P50, P51, P54): ô để trống = tính như .uel;
// nhập giá trị thì dùng giá trị nhập (cờ *Set do HColToGirderComponent.AfterApply đặt).
using System.ComponentModel;
using System.Globalization;
using UEL.Core;

namespace UEL.Core.Components.HColToGirder
{
    public class HColToGirderParams
    {
        public HColToGirderParams()
        {
            BoltToFlange = 55;                       // P4 (H-COL.uel: 55)
            CgpDirection = false;                    // P11 = 0 -> P9 = +1
            CgpZone = "";                            // P12 = P73 = mid(prefix cot, 0, find(.., "M"))
            CgpMat = "SM355";                        // P13 (H-COL.uel: SM355)
            Moment = true;                           // P14 = 1 (MO)
            BoltPitch = "";                          // P22 = P18 + "*" + P21 (tính)
            BevelExtra = 400;                        // P23
            SlotLength = 0;                          // P24 (tính theo cỡ bu lông)
            WingLength = 0;                          // P28 (bảng tra cột 5)
            WingMat = "";                            // P37 (theo vật liệu dầm)
            BoltSize = 24; BoltStandard = "T.S.B";   // P40
            BoltEdgeSum = 80;                        // P21A
            TwoPitchHeight = 900;                    // P21B
            L3 = 0;                                  // P47 (bảng tra cột 2)
            Wp1 = 0; Wp2 = 0;                        // P50, P51 (tính)
            GussetStartNo = 1; WingStartNo = 1;      // P52, P53
            GwpZone = "";                            // P54 = P71 = mid(prefix dam, 0, find(.., "M"))
            TopRingThk = 0; BottRingThk = 0;         // P55, P56
            TopRingPlus = 0; BottRingPlus = 0;       // P58, P59
            TopFlgToBotRing = 100; BotFlgToTopRing = 100;   // P60, P61
            TopWebCut = false; BotWebCut = false;    // P62, P63
            CgpTopCut = false; CgpBotCut = false;    // P64, P65

            // BIẾN MỚI (người dùng 18/11/2026): gộp các bản tuỳ biến theo dự án (TANG_DUONG P-BOX / PCOL) vào 1 plugin.
            // Để trống = chạy như H-COL_TO_GIRDER.uel gốc.
            PinGap = 30; MoGap = 5;                  // P74, P75 (H-COL.uel: D5 = if (P14==1) then 5 else 30)
            WpMark = "WP";                           // P76 (TANG_DUONG: "GWP")
            FbSymmetric = false;                     // P77 (P-BOX: FB 2 bên theo WP1)
            TablePath = "";                          // P78 (trống = YGY-S_COL2GIR-운용층.txt)

            BevelAngle = 45;                         // P7 của .uel (hằng, không có trong hộp thoại)
            RootFace = 2;                            // (P8-2) trong D48/D53 của .uel = mặt chân 2 mm (như RAKER)
            FbExtend = 20;                           // D56/D57 = 20 khi không có WP
            FbProfile = "FB25*9";                    // giả định: chuỗi "25*9" có trong .uel_temp
            FbMat = "SS275";                         // giả định: chuỗi "SS275" có trong .uel_temp
            CgpClass = "";                           // trống = không gán class
            FbClass = "";
            // Tab Advanced (biến mới 18/11/2026): trống = giữ như mặc định
            CgpName = "CGP"; CgpFinish = ""; CgpUda = "";
            WpName = "WP"; WpClass = ""; WpFinish = ""; WpUda = "";
            FbName = "FB"; FbFinish = ""; FbUda = ""; FbStartNo = 1;
            FilletSize = 6;                          // giả định
            ScallopSize = 35;                        // P80 (biến mới): cạnh vát scallop (35 hoặc 30)
            ScallopLength = 200;                     // khối cắt scallop dài 200 (người dùng 18/11/2026)
            SlotInBeam = false;                      // P79 (biến mới): mặc định lỗ ô van ở CGP
            CgpSideMode = 0;                         // P83 (biến mới): 0 = +X/+Y của model (mặc định)
            WpBothFlanges = false;                   // người dùng xác nhận 18/11/2026: WP chỉ ở cánh trên
        }

        // ---------------- Tab " Parameters 1 " (giữ đủ biến của .inp gốc)
        [UelParam("P4", Table = "Bolt To Beam Flg"), Category("1. CGP & Bolt"), DisplayName("P4  BOLT to BEAM FLG.")] public double BoltToFlange { get; set; }
        [UelParam("P11", Table = "CGP Direction"), Category("1. CGP & Bolt"), DisplayName("P11 CGP huong (0 -> +Z, 1 -> -Z)")] public bool CgpDirection { get; set; }
        [UelParam("P12", Table = "CGP Zone"), Category("1. CGP & Bolt"), DisplayName("P12 CGP-ZONE (trong = theo prefix cot)")] public string CgpZone { get; set; }
        [UelParam("P13", Table = "CGP Mat"), Category("1. CGP & Bolt"), DisplayName("P13 CGP MAT'L")] public string CgpMat { get; set; }
        [UelParam("P14", Table = "Moment"), Category("1. CGP & Bolt"), DisplayName("P14 MO(YES)_PIN(NO)")] public bool Moment { get; set; }
        [UelParam("P22", Table = "Bolt Pitch"), Category("1. CGP & Bolt"), DisplayName("P22 BOLT_PITCH (trong = tinh)")] public string BoltPitch { get; set; }
        [UelParam("P23", Table = "Bevel Extra"), Category("1. CGP & Bolt"), DisplayName("P23 Gaeseon yeojang")] public double BevelExtra { get; set; }
        [UelParam("P24", Table = "Slot Hole Length"), Category("1. CGP & Bolt"), DisplayName("P24 SLOT_HOLE length (trong = tinh)")] public double SlotLength { get; set; }
        [Browsable(false)] /* P28: bo (H-COL_TO_GIRDER khong co WP / ring) */ public double WingLength { get; set; }
        [Browsable(false)] /* P37: bo (H-COL_TO_GIRDER khong co WP / ring) */ public string WingMat { get; set; }
        [UelParam("P40_diameter", Table = "Bolt Size"), Category("1. CGP & Bolt"), DisplayName("P40 BOLT_SIZE")] public double BoltSize { get; set; }
        [UelParam("P40_screwdin", Table = "Bolt Standard"), Category("1. CGP & Bolt"), DisplayName("P40 BOLT_STANDARD")] public string BoltStandard { get; set; }
        [UelParam("P21A", Table = "Bolt Edge Sum"), Category("1. CGP & Bolt"), DisplayName("P21A CWS-SPS-EDGE (Bolt To GP/y)")] public double BoltEdgeSum { get; set; }
        [UelParam("P21B", Table = "Two Pitch Height"), Category("1. CGP & Bolt"), DisplayName("P21B CWS_SPS")] public double TwoPitchHeight { get; set; }
        [UelParam("P47", Table = "L3"), Category("1. CGP & Bolt"), DisplayName("P47 L3 (trong = bang tra)")] public double L3 { get; set; }
        [Browsable(false)] /* P50: bo (H-COL_TO_GIRDER khong co WP / ring) */ public int Wp1 { get; set; }
        [Browsable(false)] /* P51: bo (H-COL_TO_GIRDER khong co WP / ring) */ public int Wp2 { get; set; }
        [UelParam("P52", Table = "Gusset Start No"), Category("1. CGP & Bolt"), DisplayName("P52 GUSSET-START NO")] public int GussetStartNo { get; set; }
        [Browsable(false)] /* P53: bo (H-COL_TO_GIRDER khong co WP / ring) */ public int WingStartNo { get; set; }
        [UelParam("P54", Table = "GWP Zone"), Category("2. Wing / FB"), DisplayName("P54 GWP_ZONE (trong = theo prefix dam)")] public string GwpZone { get; set; }
        [Browsable(false)] /* P55: bo (H-COL_TO_GIRDER khong co WP / ring) */ public double TopRingThk { get; set; }
        [Browsable(false)] /* P56: bo (H-COL_TO_GIRDER khong co WP / ring) */ public double BottRingThk { get; set; }
        [Browsable(false)] /* P58: bo (H-COL_TO_GIRDER khong co WP / ring) */ public double TopRingPlus { get; set; }
        [Browsable(false)] /* P59: bo (H-COL_TO_GIRDER khong co WP / ring) */ public double BottRingPlus { get; set; }
        [Browsable(false)] /* P60: bo (H-COL_TO_GIRDER khong co WP / ring) */ public double TopFlgToBotRing { get; set; }

        // ---------------- Tab " Parameters 2 "
        [Browsable(false)] /* P61: bo (H-COL_TO_GIRDER khong co WP / ring) */ public double BotFlgToTopRing { get; set; }
        [Browsable(false)] /* P62: bo (H-COL_TO_GIRDER khong co WP / ring) */ public bool TopWebCut { get; set; }
        [Browsable(false)] /* P63: bo (H-COL_TO_GIRDER khong co WP / ring) */ public bool BotWebCut { get; set; }
        [Browsable(false)] /* P64: bo (H-COL_TO_GIRDER khong co WP / ring) */ public bool CgpTopCut { get; set; }
        [Browsable(false)] /* P65: bo (H-COL_TO_GIRDER khong co WP / ring) */ public bool CgpBotCut { get; set; }

        // ---------------- BIẾN MỚI (không có trong .uel gốc): phần khác nhau giữa các bản tuỳ biến theo dự án
        /// D5 khi P14 = No (PIN). H-COL_TO_GIRDER/PCOL: 20; P-BOX TANG_DUONG: 30.
        [UelParam("P74", Table = "Gap PIN"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P74 Khe dau dam PIN (D5)")] public double PinGap { get; set; }
        /// D5 khi P14 = Yes (MO). Cả 3 bản: 5.
        [UelParam("P75", Table = "Gap MO"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P75 Khe dau dam MO (D5)")] public double MoGap { get; set; }
        /// Đuôi mark bản cánh: SERIE = P54 + P76. H-COL_TO_GIRDER: "WP"; TANG_DUONG: "GWP".
        [Browsable(false)] /* P76: bo (H-COL_TO_GIRDER khong co WP / ring) */ public string WpMark { get; set; }
        /// FB dài 2 bên theo WP1 (P-BOX: P44 = if P50==0 then P6/2+20 else P30/2+D113+20, P45 = P44).
        /// No = mỗi bên theo WP của bên đó (H-COL_TO_GIRDER, PCOL).
        [Browsable(false)] /* P77: bo (H-COL_TO_GIRDER khong co WP / ring) */ public bool FbSymmetric { get; set; }
        /// File bảng tra (bắt buộc). Trống = YGY-S_COL2GIR-운용층.txt. Không chép vào model (khác bảng tuỳ chọn của RAKER).
        [UelParam("P78"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P78 Bang tra (file .txt)")] public string TablePath { get; set; }

        // ---------------- Cờ: ô có mặc định là công thức đã được nhập trong hộp thoại (AfterApply đặt)
        [Browsable(false)] public bool BoltToFlangeSet { get; set; }
        [Browsable(false)] public bool WpStraightSet { get; set; }
        [Browsable(false)] public bool WpTipSet { get; set; }
        [Browsable(false)] public bool BoltPitchSet { get; set; }
        [Browsable(false)] public bool SlotLengthSet { get; set; }
        [Browsable(false)] public bool WingLengthSet { get; set; }
        [Browsable(false)] public bool L3Set { get; set; }
        [Browsable(false)] public bool Wp1Set { get; set; }
        [Browsable(false)] public bool Wp2Set { get; set; }

        // ---------------- Hằng của .uel (không có trong hộp thoại) và giả định
        /// P7 có trong .uel (Angle = 45) nhưng không có trong hộp thoại gốc; đưa ra hộp thoại theo yêu cầu người dùng 18/11/2026.
        [UelParam("P7", Table = "Bevel Angle"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P7  Goc vat mep han (do)")] public double BevelAngle { get; set; }
        /// BIẾN MỚI: lỗ ô van ở dầm (Yes) hay ở CGP (No, mặc định).
        [UelParam("P79", Table = "Slot In Beam"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P79 Lo o van o dam (No = CGP)")] public bool SlotInBeam { get; set; }
        /// BIẾN MỚI (18/11/2026), ô danh sách: phía CGP theo model cho mọi dầm.
        ///   0 = +X / +Y (mặc định), 1 = +X / -Y, 2 = -X / +Y, 3 = -X / -Y  (dầm gần song song trục Y xét X, gần song song X xét Y),
        ///   4 = theo +Z riêng của dầm (phụ thuộc chiều vẽ). P11 = Yes đảo sang phía đối diện cho từng liên kết (sửa tay).
        [UelParam("P83", Table = "CGP Side"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P83 CGP side (0 +X+Y,1 +X-Y,2 -X+Y,3 -X-Y,4 theo dam)")] public int CgpSideMode { get; set; }
        /// BIẾN MỚI (18/11/2026): WP - đoạn rộng tới mép cột, đo từ mặt cột. Trống = (P28 + ring)/2 như .uel (D96/D121).
        [Browsable(false)] /* P81: bo (H-COL_TO_GIRDER khong co WP / ring) */ public double WpStraight { get; set; }
        /// BIẾN MỚI (18/11/2026): WP - bề rộng đầu xa ngoài mép cánh. Trống = P35 như .uel (D114/D130). 0 = đầu nhọn.
        [Browsable(false)] /* P82: bo (H-COL_TO_GIRDER khong co WP / ring) */ public double WpTip { get; set; }
        /// BIẾN MỚI: scallop = khối cắt chữ nhật dài 200, cao P80, vát góc phía dầm P80 x P80. Mặc định 35 (hoặc 30).
        [UelParam("P80", Table = "Scallop"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P80 Scallop (canh vat)")] public double ScallopSize { get; set; }
        [Category("9. Gia dinh (khong co trong hop thoai)"), DisplayName("Mat chan vat (P8-2)")] public double RootFace { get; set; }
        [Category("9. Gia dinh (khong co trong hop thoai)"), DisplayName("FB dai them moi ben (D56/D57)")] public double FbExtend { get; set; }
        // ---------------- Tab Advanced (BIẾN MỚI, người dùng 18/11/2026): thuộc tính part của CGP / WP / FB.
        //   Class = màu. UDA: "TEN=GIA_TRI; TEN2=GIA_TRI2" (số nguyên / số thực / chữ tự nhận). Trống = không gán.
        //   Không có prefix / số bắt đầu assembly: CGP, WP, FB là part phụ, assembly theo main part.
        [UelParam("P84", Table = "CGP Name"), Category("6. Advanced"), DisplayName("P84 CGP Name")] public string CgpName { get; set; }
        [UelParam("P85", Table = "CGP Class"), Category("6. Advanced"), DisplayName("P85 CGP Class")] public string CgpClass { get; set; }
        [UelParam("P86", Table = "CGP Finish"), Category("6. Advanced"), DisplayName("P86 CGP Finish")] public string CgpFinish { get; set; }
        [UelParam("P87", Table = "CGP UDA"), Category("6. Advanced"), DisplayName("P87 CGP UDA (TEN=GIA_TRI; ...)")] public string CgpUda { get; set; }
        [Browsable(false)] /* P88: bo (H-COL_TO_GIRDER khong co WP / ring) */ public string WpName { get; set; }
        [Browsable(false)] /* P89: bo (H-COL_TO_GIRDER khong co WP / ring) */ public string WpClass { get; set; }
        [Browsable(false)] /* P90: bo (H-COL_TO_GIRDER khong co WP / ring) */ public string WpFinish { get; set; }
        [Browsable(false)] /* P91: bo (H-COL_TO_GIRDER khong co WP / ring) */ public string WpUda { get; set; }
        [UelParam("P92", Table = "FB Name"), Category("6. Advanced"), DisplayName("P92 FB Name")] public string FbName { get; set; }
        [UelParam("P93", Table = "FB Class"), Category("6. Advanced"), DisplayName("P93 FB Class")] public string FbClass { get; set; }
        [UelParam("P94", Table = "FB Finish"), Category("6. Advanced"), DisplayName("P94 FB Finish")] public string FbFinish { get; set; }
        [UelParam("P95", Table = "FB UDA"), Category("6. Advanced"), DisplayName("P95 FB UDA (TEN=GIA_TRI; ...)")] public string FbUda { get; set; }
        /// FB profile / vật liệu: trước là giả định (chuỗi "25*9", "SS275" trong .uel_temp), nay đưa ra hộp thoại.
        [UelParam("P96", Table = "FB Profile"), Category("6. Advanced"), DisplayName("P96 FB Profile")] public string FbProfile { get; set; }
        [UelParam("P97", Table = "FB Mat"), Category("6. Advanced"), DisplayName("P97 FB Mat")] public string FbMat { get; set; }
        [UelParam("P98", Table = "FB Start No"), Category("6. Advanced"), DisplayName("P98 FB Start no")] public int FbStartNo { get; set; }
        [Category("9. Gia dinh (khong co trong hop thoai)"), DisplayName("Fillet size")] public double FilletSize { get; set; }
        [Category("9. Gia dinh (khong co trong hop thoai)"), DisplayName("Scallop: dai khoi cat")] public double ScallopLength { get; set; }
        /// .uel có 2 đối tượng WP (CREATION P50, P51) = 2 bên của CÁNH TRÊN (người dùng xác nhận 18/11/2026).
        /// Mặc định No; Yes chỉ để thử dựng thêm ở cánh dưới.
        [Category("9. Gia dinh (khong co trong hop thoai)"), DisplayName("WP ca canh duoi (mac dinh No)")] public bool WpBothFlanges { get; set; }

        public override string ToString()
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            return string.Format(ci,
                "P14={0} P11={1} | CGP {2} zone='{3}' start={4} | bolt M{5} {6} P4={7} P22='{8}'{9} P21A={10} P21B={11} P24={12}{13} | " +
                "L3={14}{15} P23={16} | ring T{17}/B{18} R+ T{19}/B{20} P60={21} P61={22} | cut web T{23}/B{24} CGP T{25}/B{26} | " +
                "WP P50={27}{28} P51={29}{30} P28={31}{32} P37='{33}' zone='{34}' | FB {35} {36}",
                Moment ? 1 : 0, CgpDirection ? 1 : 0, CgpMat, CgpZone, GussetStartNo,
                BoltSize, BoltStandard, BoltToFlange, BoltPitch, BoltPitchSet ? "" : "(tinh)", BoltEdgeSum, TwoPitchHeight,
                SlotLength, SlotLengthSet ? "" : "(tinh)",
                L3, L3Set ? "" : "(bang tra)", BevelExtra,
                TopRingThk, BottRingThk, TopRingPlus, BottRingPlus, TopFlgToBotRing, BotFlgToTopRing,
                TopWebCut ? 1 : 0, BotWebCut ? 1 : 0, CgpTopCut ? 1 : 0, CgpBotCut ? 1 : 0,
                Wp1, Wp1Set ? "" : "(tinh)", Wp2, Wp2Set ? "" : "(tinh)", WingLength, WingLengthSet ? "" : "(bang tra)", WingMat, GwpZone,
                FbProfile, FbMat)
                + string.Format(ci, " | gap MO {0} PIN {1} | WP mark '{2}' | FB doi xung {3} | bang tra '{4}'",
                    MoGap, PinGap, WpMark, FbSymmetric ? 1 : 0, TablePath);
        }
    }
}
