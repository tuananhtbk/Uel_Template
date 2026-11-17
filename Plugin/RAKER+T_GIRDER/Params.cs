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
            BasePlateThk = 19; BasePlateMat = "SM355"; BasePlateClass = 3; BasePlateMark = "B1D-RSF";   // class: giả định
            TopGap = 25; LeftGap = 25; BottGap = 25; RightGap = 25;

            ConnType = 0;
            GussetThk = 30; GussetMat = "SM355"; GussetMark = "B1D-RGP"; GussetClass = 4;               // class: giả định
            GussetOnPositiveSide = true; GussetSideAuto = true;
            BoltToWebEnd = 230; BoltQtyX = 1; BoltDistX = 90;                                           // BoltDistX: giả định
            BoltQtyY = 2; BoltDistY = 130; BoltEdgeX = 60; BoltEdgeY = 40; WebGap = 15;
            BoltSize = 24; BoltStandard = "T.S.B"; Tolerance = 3; SlotX = 0; SlotY = 0;

            FlatBar = true; FlatBarMark = "B1D-RFB"; FlatBarMat = "SM355"; FlatBarClass = 5;            // class: giả định
            FlangeWeldGap = 7; WeldAngle = 35;                                                          // giả định
            TopScallop = true; BottScallop = true;

            FlatBarThk = 9; FlatBarWidth = 25; FlatBarExtend = 15; ScallopRadius = 35; FilletSize = 6;                      // chuỗi "25*9" và D66 = P46+35 có trong .uel
        }

        // ---------------- Base Plate
        [UelParam("P1"), Category("1. Base Plate"), DisplayName("P1  Base Plate Thk.")] public double BasePlateThk { get; set; }
        [UelParam("P2"), Category("1. Base Plate"), DisplayName("P2  Mat.")] public string BasePlateMat { get; set; }
        [UelParam("P3"), Category("1. Base Plate"), DisplayName("P3  Class")] public int BasePlateClass { get; set; }
        [UelParam("P4"), Category("1. Base Plate"), DisplayName("P4  Mark")] public string BasePlateMark { get; set; }
        [UelParam("P6"), Category("1. Base Plate"), DisplayName("P6  Top gap")] public double TopGap { get; set; }
        [UelParam("P7"), Category("1. Base Plate"), DisplayName("P7  Left gap")] public double LeftGap { get; set; }
        [UelParam("P8"), Category("1. Base Plate"), DisplayName("P8  Bott. gap")] public double BottGap { get; set; }
        [UelParam("P19"), Category("1. Base Plate"), DisplayName("P19 Right gap")] public double RightGap { get; set; }

        // ---------------- Gusset
        [UelParam("P13"), Category("2. Gusset"), DisplayName("P13 Conn. Type (0 = ban ma + bu long)")] public int ConnType { get; set; }
        [UelParam("P20"), Category("2. Gusset"), DisplayName("P20 Gusset Thk.")] public double GussetThk { get; set; }
        [UelParam("P22"), Category("2. Gusset"), DisplayName("P22 Matt")] public string GussetMat { get; set; }
        [UelParam("P21"), Category("2. Gusset"), DisplayName("P21 Mark")] public string GussetMark { get; set; }
        [UelParam("P23"), Category("2. Gusset"), DisplayName("P23 Class")] public int GussetClass { get; set; }
        [UelParam("P24"), Category("2. Gusset"), DisplayName("P24 Guss. L or R (true = 1)")] public bool GussetOnPositiveSide { get; set; }
        /// Mặc định (17/11/2026, theo hướng lắp raker): bản mã đặt PHÍA TRONG góc giữa primary và secondary.
        /// Tắt khi người dùng nhập P24 trong hộp thoại (xem RakerTGComponent.AfterApply) -> theo P24.
        [Category("2. Gusset"), DisplayName("Ban ma phia trong (tu dong khi P24 de trong)")] public bool GussetSideAuto { get; set; }
        [UelParam("P46"), Category("2. Gusset"), DisplayName("P46 Gap Web beam to base pl")] public double WebGap { get; set; }

        // ---------------- Bolts
        [UelParam("P29"), Category("3. Bolt"), DisplayName("P29 Bolt To web Beam")] public double BoltToWebEnd { get; set; }
        [UelParam("P31"), Category("3. Bolt"), DisplayName("P31 Bolt Q'ty x")] public int BoltQtyX { get; set; }
        [UelParam("P32"), Category("3. Bolt"), DisplayName("P32 Bolt distance x")] public double BoltDistX { get; set; }
        [UelParam("P34"), Category("3. Bolt"), DisplayName("P34 Bolt Q'ty y")] public int BoltQtyY { get; set; }
        [UelParam("P43"), Category("3. Bolt"), DisplayName("P43 Bolt distance y")] public double BoltDistY { get; set; }
        [UelParam("P44"), Category("3. Bolt"), DisplayName("P44 Bolt to Gp_x (eH)")] public double BoltEdgeX { get; set; }
        [UelParam("P45"), Category("3. Bolt"), DisplayName("P45 Bolt to Gp_y (eV)")] public double BoltEdgeY { get; set; }
        [UelParam("P38_diameter"), Category("3. Bolt"), DisplayName("P38 Bolt size")] public double BoltSize { get; set; }
        [UelParam("P38_screwdin"), Category("3. Bolt"), DisplayName("P38 Bolt standard")] public string BoltStandard { get; set; }
        [UelParam("P40"), Category("3. Bolt"), DisplayName("P40 Tolerance")] public double Tolerance { get; set; }
        [UelParam("P39"), Category("3. Bolt"), DisplayName("P39 Slotted hole X")] public double SlotX { get; set; }
        [UelParam("P41"), Category("3. Bolt"), DisplayName("P41 Slotted hole Y")] public double SlotY { get; set; }

        // ---------------- FB / weld
        [UelParam("P52"), Category("4. FB & Weld"), DisplayName("P52 FB (Y/N)")] public bool FlatBar { get; set; }
        [UelParam("P11"), Category("4. FB & Weld"), DisplayName("P11 FB Mark")] public string FlatBarMark { get; set; }
        [UelParam("P5"), Category("4. FB & Weld"), DisplayName("P5  FB Mat.")] public string FlatBarMat { get; set; }
        [Category("4. FB & Weld"), DisplayName("FB Class (gia dinh)")] public int FlatBarClass { get; set; }
        [UelParam("P10"), Category("4. FB & Weld"), DisplayName("P10 FLG. Weld gap")] public double FlangeWeldGap { get; set; }
        [UelParam("P12"), Category("4. FB & Weld"), DisplayName("P12 Weld Angle")] public double WeldAngle { get; set; }
        [UelParam("P51"), Category("4. FB & Weld"), DisplayName("P51 Top Scallop (Y/N)")] public bool TopScallop { get; set; }
        [UelParam("P53"), Category("4. FB & Weld"), DisplayName("P53 Bott Scallop (Y/N)")] public bool BottScallop { get; set; }

        // ---------------- Hằng số chưa có trong hộp thoại gốc
        [Category("5. Gia dinh (khong co trong hop thoai)"), DisplayName("FB thk")] public double FlatBarThk { get; set; }
        [Category("5. Gia dinh (khong co trong hop thoai)"), DisplayName("FB width")] public double FlatBarWidth { get; set; }
        [Category("5. Gia dinh (khong co trong hop thoai)"), DisplayName("FB dai them moi dau")] public double FlatBarExtend { get; set; }
        [Category("5. Gia dinh (khong co trong hop thoai)"), DisplayName("Scallop R")] public double ScallopRadius { get; set; }
        [Category("5. Gia dinh (khong co trong hop thoai)"), DisplayName("Fillet size")] public double FilletSize { get; set; }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "BP PL{0} {1} cls={2} mark={3} gaps T{4}/B{5}/L{6}/R{7} | GP PL{8} {9} cls={10} side={11} | bolts {12}x{13} M{14} {15} | FB={16} cls={17} | P13={18}",
                BasePlateThk, BasePlateMat, BasePlateClass, BasePlateMark, TopGap, BottGap, LeftGap, RightGap,
                GussetThk, GussetMat, GussetClass, GussetSideAuto ? "tu dong (phia trong)" : (GussetOnPositiveSide ? "1" : "0"),
                BoltQtyX, BoltQtyY, BoltSize, BoltStandard, FlatBar ? 1 : 0, FlatBarClass, ConnType);
        }
    }
}
