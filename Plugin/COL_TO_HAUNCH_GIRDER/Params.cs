// Thông số liên kết Cột -> Dầm HAUNCH (custom component gốc "♥♥♥YGY_HAUNCH_Moment", File goc\COL-HUN-PBOX\YGY-HAUNCH_Moment.uel).
// Clone COL_TO_GIRDER theo yêu cầu người dùng 18/11/2026 ("chỉ khác hình dạng Part 2"): GIỮ TÊN BIẾN CỦA COL_TO_GIRDER để 2 plugin
// dùng chung cách nhập / preset. Biến của YGY-HAUNCH_Moment.uel ứng với biến plugin (bảng đầy đủ ở README):
//   1_GAP -> P75 (D4..D7 = 1_GAP+5), P11_NSFS -> P11/P83, P18 ZONE -> P12, P38 -> P54, P30 CGP 재질 -> P13 (SM355),
//   P29_diameter/screwdin -> P40_*, P31 SLOT -> P24, VAR3 -> P21A, VAR4 -> P21B, BOLT_ST 80 -> P4, BOLT_TOP -> P99 (biến mới),
//   P34 R_TOP -> P100 (biến mới), P17 R_BOT -> P101 (biến mới), P21/P22 WING -> P50/P51, đuôi "GWP" -> P76.
// Giá trị mặc định = mặc định của YGY-HAUNCH_Moment.uel; chỗ .uel không có thì như COL_TO_GIRDER.
// [UelParam] = tên biến trong hộp thoại gốc. [Category]/[DisplayName] chỉ dùng cho bảng của EXE thử nghiệm.
//
// Biến có mặc định là CÔNG THỨC trong .uel (P12, P22, P24, P28, P37, P47, P50, P51, P54): ô để trống = tính như .uel;
// nhập giá trị thì dùng giá trị nhập (cờ *Set do ColToHaunchGirderComponent.AfterApply đặt).
using System.ComponentModel;
using System.Globalization;
using UEL.Core;

namespace UEL.Core.Components.ColToHaunchGirder
{
    public class ColToHaunchGirderParams
    {
        public ColToHaunchGirderParams()
        {
            BoltToFlange = 80;                       // P4 (HAUNCH .uel: DX1 = BOLT_ST = 80)
            CgpDirection = false;                    // P11 = 0 -> P9 = +1
            CgpZone = "";                            // P12 = P73 = mid(prefix cot, 0, find(.., "M"))
            CgpMat = "SM355";                        // P13 (HAUNCH .uel: P30 = SM355)
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
            // Để trống = chạy như COL_TO_HAUNCH_GIRDER.uel gốc.
            PinGap = 20; MoGap = 5;                  // P74, P75 (P-BOX: PIN = 30)
            WpMark = "GWP";                          // P76 (HAUNCH .uel: SERIE = P38 + "GWP")
            Top2Bolt = 0;                            // P99 (biến mới): trống = bảng tra cột TOP2BOLT
            ScallopTop = true; ScallopBot = true;    // P100 = R_TOP (P34), P101 = R_BOT (P17) của .uel, mặc định 1
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
        [UelParam("P28", Table = "Wing PL Length"), Category("2. Wing / FB"), DisplayName("P28 WING_PL length (trong = bang tra)")] public double WingLength { get; set; }
        [UelParam("P37", Table = "Wing Mat"), Category("2. Wing / FB"), DisplayName("P37 WING_MAT'L (trong = theo dam)")] public string WingMat { get; set; }
        [UelParam("P40_diameter", Table = "Bolt Size"), Category("1. CGP & Bolt"), DisplayName("P40 BOLT_SIZE")] public double BoltSize { get; set; }
        [UelParam("P40_screwdin", Table = "Bolt Standard"), Category("1. CGP & Bolt"), DisplayName("P40 BOLT_STANDARD")] public string BoltStandard { get; set; }
        [UelParam("P21A", Table = "Bolt Edge Sum"), Category("1. CGP & Bolt"), DisplayName("P21A CWS-SPS-EDGE (Bolt To GP/y)")] public double BoltEdgeSum { get; set; }
        [UelParam("P21B", Table = "Two Pitch Height"), Category("1. CGP & Bolt"), DisplayName("P21B CWS_SPS")] public double TwoPitchHeight { get; set; }
        [UelParam("P47", Table = "L3"), Category("1. CGP & Bolt"), DisplayName("P47 L3 (trong = bang tra)")] public double L3 { get; set; }
        [UelParam("P50", Table = "WP1"), Category("2. Wing / FB"), DisplayName("P50 WP1 (Y/N) (trong = tinh)")] public int Wp1 { get; set; }
        [UelParam("P51", Table = "WP2"), Category("2. Wing / FB"), DisplayName("P51 WP2 (Y/N) (trong = tinh)")] public int Wp2 { get; set; }
        [UelParam("P52", Table = "Gusset Start No"), Category("1. CGP & Bolt"), DisplayName("P52 GUSSET-START NO")] public int GussetStartNo { get; set; }
        [UelParam("P53", Table = "Wing Start No"), Category("2. Wing / FB"), DisplayName("P53 WING-START NO")] public int WingStartNo { get; set; }
        [UelParam("P54", Table = "GWP Zone"), Category("2. Wing / FB"), DisplayName("P54 GWP_ZONE (trong = theo prefix dam)")] public string GwpZone { get; set; }
        [UelParam("P55", Table = "Top Ring Thk"), Category("3. Ring"), DisplayName("P55 TOP RING THK")] public double TopRingThk { get; set; }
        [UelParam("P56", Table = "Bott Ring Thk"), Category("3. Ring"), DisplayName("P56 BOTT RING THK")] public double BottRingThk { get; set; }
        [UelParam("P58", Table = "Top R Plus"), Category("3. Ring"), DisplayName("P58 TOP / R+")] public double TopRingPlus { get; set; }
        [UelParam("P59", Table = "Bott R Plus"), Category("3. Ring"), DisplayName("P59 BOTT / R+")] public double BottRingPlus { get; set; }
        [UelParam("P60", Table = "Top Flg To Bot Ring"), Category("3. Ring"), DisplayName("P60 TOP FLG TO BOT RING")] public double TopFlgToBotRing { get; set; }

        // ---------------- Tab " Parameters 2 "
        [UelParam("P61", Table = "Bot Flg To Top Ring"), Category("3. Ring"), DisplayName("P61 BOT FLG TO TOP RING")] public double BotFlgToTopRing { get; set; }
        [UelParam("P62", Table = "Beam Top Web Cut"), Category("4. Cut"), DisplayName("P62 BEAM TOP WEB CUT (Y/N)")] public bool TopWebCut { get; set; }
        [UelParam("P63", Table = "Beam Bot Web Cut"), Category("4. Cut"), DisplayName("P63 BEAM BOT WEB CUT (Y/N)")] public bool BotWebCut { get; set; }
        [UelParam("P64", Table = "CGP Top Cut"), Category("4. Cut"), DisplayName("P64 CGP TOP CUT (Y/N)")] public bool CgpTopCut { get; set; }
        [UelParam("P65", Table = "CGP Bot Cut"), Category("4. Cut"), DisplayName("P65 CGP BOT CUT (Y/N)")] public bool CgpBotCut { get; set; }

        // ---------------- BIẾN MỚI (không có trong .uel gốc): phần khác nhau giữa các bản tuỳ biến theo dự án
        /// D5 khi P14 = No (PIN). COL_TO_HAUNCH_GIRDER/PCOL: 20; P-BOX TANG_DUONG: 30.
        [UelParam("P74", Table = "Gap PIN"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P74 Khe dau dam PIN (D5)")] public double PinGap { get; set; }
        /// D5 khi P14 = Yes (MO). Cả 3 bản: 5.
        [UelParam("P75", Table = "Gap MO"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P75 Khe dau dam MO (D5)")] public double MoGap { get; set; }
        /// Đuôi mark bản cánh: SERIE = P54 + P76. COL_TO_HAUNCH_GIRDER: "WP"; TANG_DUONG: "GWP".
        [UelParam("P76", Table = "WP Mark"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P76 Duoi mark ban canh")] public string WpMark { get; set; }
        /// FB dài 2 bên theo WP1 (P-BOX: P44 = if P50==0 then P6/2+20 else P30/2+D113+20, P45 = P44).
        /// No = mỗi bên theo WP của bên đó (COL_TO_HAUNCH_GIRDER, PCOL).
        [UelParam("P77", Table = "FB Symmetric"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P77 FB doi xung theo WP1")] public bool FbSymmetric { get; set; }
        /// File bảng tra (bắt buộc). Trống = YGY-S_COL2GIR-운용층.txt. Không chép vào model (khác bảng tuỳ chọn của RAKER).
        [UelParam("P78"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P78 Bang tra (file .txt)")] public string TablePath { get; set; }

        // ---------------- BIẾN MỚI riêng cho dầm HAUNCH (18/11/2026)
        /// BOLT_TOP của .uel = fVF("YGY-HAUNCH_Moment.txt", profile, 6): mặt ngoài cánh trên -> hàng bu lông trên.
        /// Trống = cột "TOP2BOLT" của bảng tra; không có nữa thì CGP đặt giữa chiều cao dầm tại mặt cột (như COL_TO_GIRDER).
        [UelParam("P99", Table = "TOP2BOLT"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P99 Mat ngoai canh tren -> bu long (trong = bang tra)")] public double Top2Bolt { get; set; }
        /// R_TOP (P34 của .uel): scallop góc bụng - cánh trên.
        [UelParam("P100", Table = "Scallop Top"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P100 Scallop canh tren (R_TOP)")] public bool ScallopTop { get; set; }
        /// R_BOT (P17 của .uel): scallop góc bụng - cánh dưới.
        [UelParam("P101", Table = "Scallop Bot"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P101 Scallop canh duoi (R_BOT)")] public bool ScallopBot { get; set; }
        [Browsable(false)] public bool Top2BoltSet { get; set; }

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
        [UelParam("P81", Table = "WP Straight"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P81 WP doan thang (trong = (P28+ring)/2)")] public double WpStraight { get; set; }
        /// BIẾN MỚI (18/11/2026): WP - bề rộng đầu xa ngoài mép cánh. Trống = P35 như .uel (D114/D130). 0 = đầu nhọn.
        [UelParam("P82", Table = "WP Tip"), Category("5. Tuy bien du an (bien moi)"), DisplayName("P82 WP dau xa (trong = P35)")] public double WpTip { get; set; }
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
        [UelParam("P88", Table = "WP Name"), Category("6. Advanced"), DisplayName("P88 WP Name")] public string WpName { get; set; }
        [UelParam("P89", Table = "WP Class"), Category("6. Advanced"), DisplayName("P89 WP Class")] public string WpClass { get; set; }
        [UelParam("P90", Table = "WP Finish"), Category("6. Advanced"), DisplayName("P90 WP Finish")] public string WpFinish { get; set; }
        [UelParam("P91", Table = "WP UDA"), Category("6. Advanced"), DisplayName("P91 WP UDA (TEN=GIA_TRI; ...)")] public string WpUda { get; set; }
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
                    MoGap, PinGap, WpMark, FbSymmetric ? 1 : 0, TablePath)
                + string.Format(ci, " | TOP2BOLT {0}{1} | scallop tren {2} duoi {3}",
                    Top2Bolt, Top2BoltSet ? "" : "(bang tra)", ScallopTop ? 1 : 0, ScallopBot ? 1 : 0);
        }
    }
}
