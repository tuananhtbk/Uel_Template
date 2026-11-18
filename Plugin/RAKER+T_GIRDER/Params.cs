// Thông số liên kết Raker -> T-girder (custom component gốc "0-RAKER+TG", file RAKER+T_GIRDER.uel).
// Giá trị mặc định lấy từ bộ thuộc tính "KK350" trong .uel gốc (raker H350x357x19x19);
// dòng ghi "giả định" là giá trị KK350 không lưu, cần chỉnh theo tiêu chuẩn công ty.
// [UelParam] = tên biến trong hộp thoại gốc. [Category]/[DisplayName] chỉ dùng cho bảng của EXE thử nghiệm.
using System.ComponentModel;
using UEL.Core;

namespace UEL.Core.Components.RakerTG
{
    public class RakerTGParams
    {
        public RakerTGParams()
        {
            BasePlateThk = 19; BasePlateMat = "SM355"; BasePlateClass = 3; BasePlateMark = "RSF";       // class: giả định
            // Mark (P4/P21/P11) chỉ là PHẦN ĐUÔI: prefix = phần trước chữ "M" của prefix Part 1 + Mark (Plugin\CLAUDE.md 4.3).
            // KK350 lưu "B1D-RSF"/"B1D-RGP"/"B1D-RFB", #KRAC lưu "1RSF"/"1RGP"/"1RFB" -> phần đuôi chung là RSF/RGP/RFB.
            TopGap = 25; LeftGap = 25; BottGap = 25; RightGap = 25;
            BasePlateThkSource = 2;                                                                     // P58 mới (18/11/2026): 2 = file text

            ConnType = 0;
            GussetThk = 30; GussetMat = "SM355"; GussetMark = "RGP"; GussetClass = 4;               // class: giả định
            GussetOnPositiveSide = true; GussetSideAuto = true;
            BoltToWebEnd = 230; BoltQtyX = 1; BoltDistX = 90;                                           // BoltDistX: giả định
            BoltQtyY = 2; BoltDistY = 130; BoltEdgeX = 60; BoltEdgeY = 40; WebGap = 15;
            BoltSize = 24; BoltStandard = "T.S.B"; Tolerance = 3; SlotX = 0; SlotY = 0;

            FlatBar = true; FlatBarMark = "RFB"; FlatBarMat = "SS275"; FlatBarClass = 5;            // class: giả định; SS275 = mặc định P5 của .uel
            FlatBarProfile = "FB25*9";                                                                  // P55 mới (người dùng 17/11/2026)
            GussetLength = 0;                                                                           // P54 mới: 0 = tự tính theo bu lông + eH
            TablePath = ""; UseTable = true;                                                            // P56/P57 mới: bảng tra theo profile Part 2
            FlangeWeldGap = 7; WeldAngle = 35;                                                          // giả định
            TopScallop = true; BottScallop = true;

            FlatBarExtend = 15; ScallopRadius = 35; FilletSize = 6;                      // chuỗi "25*9" và D66 = P46+35 có trong .uel
        }

        // ---------------- Base Plate
        [UelParam("P1", Table = "Base Plate Thk"), Category("1. Base Plate"), DisplayName("P1  Base Plate Thk.")] public double BasePlateThk { get; set; }
        /// BIẾN MỚI, không có trong .uel gốc (người dùng 18/11/2026). Nguồn chiều dày bản đế:
        ///   0 = bằng dày cánh Part 1 (T-girder), 1 = bằng dày cánh Part 2 (raker),
        ///   2 = giá trị cột "Base Plate Thk" trong file text (mặc định; P57 = No thì lấy ô P1 của hộp thoại).
        [UelParam("P58", Table = "Base Plate Thk Source"), Category("1. Base Plate"), DisplayName("P58 Thk source (0 flange P1, 1 flange P2, 2 file text)")] public int BasePlateThkSource { get; set; }
        [UelParam("P2", Table = "Base Plate Mat"), Category("1. Base Plate"), DisplayName("P2  Mat.")] public string BasePlateMat { get; set; }
        [UelParam("P3", Table = "Base Plate Class"), Category("1. Base Plate"), DisplayName("P3  Class")] public int BasePlateClass { get; set; }
        [UelParam("P4", Table = "Base Plate Mark"), Category("1. Base Plate"), DisplayName("P4  Mark")] public string BasePlateMark { get; set; }
        [UelParam("P6", Table = "Top Gap"), Category("1. Base Plate"), DisplayName("P6  Top gap")] public double TopGap { get; set; }
        [UelParam("P7", Table = "Left Gap"), Category("1. Base Plate"), DisplayName("P7  Left gap")] public double LeftGap { get; set; }
        [UelParam("P8", Table = "Bott Gap"), Category("1. Base Plate"), DisplayName("P8  Bott. gap")] public double BottGap { get; set; }
        [UelParam("P19", Table = "Right Gap"), Category("1. Base Plate"), DisplayName("P19 Right gap")] public double RightGap { get; set; }

        // ---------------- Bảng tra theo profile Part 2 (BIẾN MỚI, người dùng 17/11/2026; không có trong .uel gốc)
        /// Đường dẫn bảng tra. Trống = "RAKER+T_GIRDER.txt"; chỉ tên file thì tìm ở thư mục model -> XS_PROJECT -> XS_FIRM -> thư mục plugin.
        [UelParam("P56"), Category("0. Bang tra"), DisplayName("P56 Bang tra (file .txt)")] public string TablePath { get; set; }
        /// Yes (mặc định) = cột nào có trong bảng thì lấy theo bảng, đè giá trị hộp thoại. No = chỉ dùng hộp thoại.
        [UelParam("P57"), Category("0. Bang tra"), DisplayName("P57 Dung bang tra")] public bool UseTable { get; set; }

        // ---------------- Gusset
        [UelParam("P13", Table = "Conn Type"), Category("2. Gusset"), DisplayName("P13 Conn. Type (0 = ban ma + bu long)")] public int ConnType { get; set; }
        [UelParam("P20", Table = "Gusset Thk"), Category("2. Gusset"), DisplayName("P20 Gusset Thk.")] public double GussetThk { get; set; }
        [UelParam("P22", Table = "Gusset Mat"), Category("2. Gusset"), DisplayName("P22 Matt")] public string GussetMat { get; set; }
        [UelParam("P21", Table = "Gusset Mark"), Category("2. Gusset"), DisplayName("P21 Mark")] public string GussetMark { get; set; }
        [UelParam("P23", Table = "Gusset Class"), Category("2. Gusset"), DisplayName("P23 Class")] public int GussetClass { get; set; }
        [UelParam("P24", Table = "Gusset L or R"), Category("2. Gusset"), DisplayName("P24 Guss. L or R (true = 1)")] public bool GussetOnPositiveSide { get; set; }
        /// Mặc định (17/11/2026, theo hướng lắp raker): bản mã đặt PHÍA TRONG góc giữa primary và secondary.
        /// Tắt khi người dùng nhập P24 trong hộp thoại (xem RakerTGComponent.AfterApply) -> theo P24.
        [Category("2. Gusset"), DisplayName("Ban ma phia trong (tu dong khi P24 de trong)")] public bool GussetSideAuto { get; set; }
        [UelParam("P46", Table = "Web Gap"), Category("2. Gusset"), DisplayName("P46 Gap Web beam to base pl")] public double WebGap { get; set; }
        /// BIẾN MỚI, không có trong .uel gốc (người dùng 17/11/2026): chiều dài cạnh dài bản mã (hình bình hành, đo dọc trục raker).
        /// 0 hoặc để trống = tự tính như cũ (cột bu lông cuối + eH P44).
        [UelParam("P54", Table = "Gusset Length"), Category("2. Gusset"), DisplayName("P54 Gusset length (0 = auto)")] public double GussetLength { get; set; }

        // ---------------- Bolts
        [UelParam("P29", Table = "Bolt To Web"), Category("3. Bolt"), DisplayName("P29 Bolt To web Beam")] public double BoltToWebEnd { get; set; }
        [UelParam("P31", Table = "Bolt Qty X"), Category("3. Bolt"), DisplayName("P31 Bolt Q'ty x")] public int BoltQtyX { get; set; }
        [UelParam("P32", Table = "Bolt Dist X"), Category("3. Bolt"), DisplayName("P32 Bolt distance x")] public double BoltDistX { get; set; }
        [UelParam("P34", Table = "Bolt Qty Y"), Category("3. Bolt"), DisplayName("P34 Bolt Q'ty y")] public int BoltQtyY { get; set; }
        [UelParam("P43", Table = "Bolt Dist Y"), Category("3. Bolt"), DisplayName("P43 Bolt distance y")] public double BoltDistY { get; set; }
        [UelParam("P44", Table = "Bolt Edge X"), Category("3. Bolt"), DisplayName("P44 Bolt to Gp_x (eH)")] public double BoltEdgeX { get; set; }
        [UelParam("P45", Table = "Bolt Edge Y"), Category("3. Bolt"), DisplayName("P45 Bolt to Gp_y (eV)")] public double BoltEdgeY { get; set; }
        [UelParam("P38_diameter", Table = "Bolt Size"), Category("3. Bolt"), DisplayName("P38 Bolt size")] public double BoltSize { get; set; }
        [UelParam("P38_screwdin", Table = "Bolt Standard"), Category("3. Bolt"), DisplayName("P38 Bolt standard")] public string BoltStandard { get; set; }
        [UelParam("P40", Table = "Tolerance"), Category("3. Bolt"), DisplayName("P40 Tolerance")] public double Tolerance { get; set; }
        [UelParam("P39", Table = "Slotted Hole X"), Category("3. Bolt"), DisplayName("P39 Slotted hole X")] public double SlotX { get; set; }
        [UelParam("P41", Table = "Slotted Hole Y"), Category("3. Bolt"), DisplayName("P41 Slotted hole Y")] public double SlotY { get; set; }

        // ---------------- FB / weld
        [UelParam("P52", Table = "Flat Bar"), Category("4. FB & Weld"), DisplayName("P52 FB (Y/N)")] public bool FlatBar { get; set; }
        [UelParam("P11", Table = "FB Mark"), Category("4. FB & Weld"), DisplayName("P11 FB Mark")] public string FlatBarMark { get; set; }
        [UelParam("P5", Table = "FB Mat"), Category("4. FB & Weld"), DisplayName("P5  FB Mat.")] public string FlatBarMat { get; set; }
        /// BIẾN MỚI, không có trong .uel gốc (người dùng 17/11/2026). Gốc chỉ có chuỗi "25*9".
        [UelParam("P55", Table = "FB Profile"), Category("4. FB & Weld"), DisplayName("P55 FB Profile")] public string FlatBarProfile { get; set; }
        [Category("4. FB & Weld"), DisplayName("FB Class (gia dinh)")] public int FlatBarClass { get; set; }
        [UelParam("P10", Table = "Flange Weld Gap"), Category("4. FB & Weld"), DisplayName("P10 FLG. Weld gap")] public double FlangeWeldGap { get; set; }
        [UelParam("P12", Table = "Weld Angle"), Category("4. FB & Weld"), DisplayName("P12 Weld Angle")] public double WeldAngle { get; set; }
        [UelParam("P51", Table = "Top Scallop"), Category("4. FB & Weld"), DisplayName("P51 Top Scallop (Y/N)")] public bool TopScallop { get; set; }
        [UelParam("P53", Table = "Bott Scallop"), Category("4. FB & Weld"), DisplayName("P53 Bott Scallop (Y/N)")] public bool BottScallop { get; set; }

        // ---------------- Hằng số chưa có trong hộp thoại gốc
        [Category("5. Gia dinh (khong co trong hop thoai)"), DisplayName("FB dai them moi dau")] public double FlatBarExtend { get; set; }
        [Category("5. Gia dinh (khong co trong hop thoai)"), DisplayName("Scallop R")] public double ScallopRadius { get; set; }
        [Category("5. Gia dinh (khong co trong hop thoai)"), DisplayName("Fillet size")] public double FilletSize { get; set; }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "BP PL{0} {1} cls={2} mark={3} gaps T{4}/B{5}/L{6}/R{7} | GP PL{8} {9} cls={10} side={11} L={19} | bolts {12}x{13} M{14} {15} | FB={16} {20} {21} cls={17} | P13={18}",
                BasePlateThk, BasePlateMat, BasePlateClass, BasePlateMark, TopGap, BottGap, LeftGap, RightGap,
                GussetThk, GussetMat, GussetClass, GussetSideAuto ? "tu dong (phia trong)" : (GussetOnPositiveSide ? "1" : "0"),
                BoltQtyX, BoltQtyY, BoltSize, BoltStandard, FlatBar ? 1 : 0, FlatBarClass, ConnType,
                GussetLength > 0 ? GussetLength.ToString(System.Globalization.CultureInfo.InvariantCulture) : "auto", FlatBarProfile, FlatBarMat)
                + " | bang tra: " + (UseTable ? (string.IsNullOrEmpty(TablePath) ? "(mac dinh)" : TablePath) : "tat");
        }
    }
}
