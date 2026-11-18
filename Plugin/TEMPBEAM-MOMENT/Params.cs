// Thông số liên kết moment dầm tạm (Part 2) -> dầm chính (Part 1), custom component gốc "♥♥♥YGY_TempBeam-Moment"
// (file File goc\PIN-MOMENT CHANGSIN\YGY_TEMPBEAM-MOMENT.uel; bản mới hơn cùng tên: File goc\YGY MOMENT\YGY.uel).
// Giá trị mặc định = giá trị mặc định của biến trong .uel gốc (bộ "< Defaults >"), là bộ mà model mẫu của người dùng
// đã dùng khi xuất JSON 18/11/2026 (mọi ô để trống). Preset có thật "+104" khác ở: P31 = 0 (không BACK SF),
// P43 = 1 (gia cường cánh dưới - CHƯA LÀM) -> không lấy làm mặc định.
// [UelParam] = tên biến trong hộp thoại gốc. [Category]/[DisplayName] chỉ dùng cho bảng của EXE thử nghiệm.
//
// Biến có mặc định là CÔNG THỨC trong .uel (P15, P41, P43, CWS): ô để trống = tính như .uel;
// nhập giá trị thì dùng giá trị nhập (cờ *Set do TempBeamComponent.AfterApply đặt).
// P30 (GP PREFIX): bản YGY.uel mới = P63 = mid(prefix Part 1, 0, find(.., "M")) -> trống = theo main part Part 1.
using System.ComponentModel;
using System.Globalization;
using UEL.Core;

namespace UEL.Core.Components.TempBeam
{
    public class TempBeamParams
    {
        public TempBeamParams()
        {
            PlateSide = false;                       // P11 = 0 -> P16 = -1: GP ở phía +Z riêng của Part 2
            BoltSize = 24;                           // P12
            SlotHole = 0;                            // P15 = P14-P12-P13 (tính)
            GpPrefix = "";                           // P30 = P63 (theo main part Part 1)
            BackSf = true;                           // P31 = 1
            ScallopType = 3;                         // P40 = 3 (cung tròn)
            ScallopSize = 0;                         // P41 (tính theo bề rộng Part 1)
            LowerFlgImprove = false;                 // P43 = if (P39>=21) then 1 else 0 (tính)
            Cws = 900;                               // CWS = if (P59=="SPS") then 700 else 900 (tính)

            // BIẾN MỚI (không có trong .uel gốc). Để trống = như .uel gốc.
            TablePath = "";                          // P65: bảng tra dầm (trống = YGY-TB-MOMENT.txt, rồi TEMPBEAM-MOMENT.txt)
            GpName = "PLATE"; GpClass = "99"; GpFinish = "";        // P66..P68 (JSON 18/11/2026: GP class 99)
            SfName = "PLATE"; SfClass = ""; SfFinish = "";          // P69..P71 (trống: BACK SF 99, SF dưới 83 như gốc)
            FbName = "PLATE"; FbClass = "275"; FbFinish = "";       // P72..P74 (JSON: FB class 275)
            FbProfile = "FB25*9"; FbMat = "SS275";                  // P75, P76 (JSON: FB25*9 SS275)
            BoltStandard = "T.S.B";                                 // P77 (JSON: T.S.B)
        }

        // ---------------- Tab " Parameters 1 " (đủ 9 biến của .inp gốc)
        [UelParam("P11", Table = "Plate Side"), Category("1. GP & Bolt"), DisplayName("P11 PLATE huong (0 = +Z dam, 1 = -Z dam)")] public bool PlateSide { get; set; }
        [UelParam("P12", Table = "Bolt Size"), Category("1. GP & Bolt"), DisplayName("P12 BOLT_SIZE")] public int BoltSize { get; set; }
        [UelParam("P15", Table = "Slot Hole"), Category("1. GP & Bolt"), DisplayName("P15 SLOT HOLE (trong = P14-P12-P13)")] public int SlotHole { get; set; }
        [UelParam("P30", Table = "GP Prefix"), Category("1. GP & Bolt"), DisplayName("P30 GP PREFIX (trong = theo Part 1)")] public string GpPrefix { get; set; }
        [UelParam("P31", Table = "Back SF"), Category("2. SF & Scallop"), DisplayName("P31 BACK SF")] public bool BackSf { get; set; }
        [UelParam("P40", Table = "Scallop Type"), Category("2. SF & Scallop"), DisplayName("P40 SCALLOP_TYPE")] public int ScallopType { get; set; }
        [UelParam("P41", Table = "Scallop Size"), Category("2. SF & Scallop"), DisplayName("P41 SCALLOP size (trong = auto)")] public double ScallopSize { get; set; }
        [UelParam("P43", Table = "Lower Flg Improve"), Category("2. SF & Scallop"), DisplayName("P43 Gia cuong canh duoi (trong = P39>=21)")] public bool LowerFlgImprove { get; set; }
        [UelParam("CWS", Table = "CWS"), Category("1. GP & Bolt"), DisplayName("CWS (900 / SPS 700) (trong = auto)")] public int Cws { get; set; }

        // ---------------- Biến mới
        [UelParam("P65", Table = "Table File"), Category("3. Bang tra"), DisplayName("P65 Bang tra dam (trong = YGY-TB-MOMENT.txt)")] public string TablePath { get; set; }
        [UelParam("P66", Table = "GP Name"), Category("4. Advanced"), DisplayName("P66 GP Name")] public string GpName { get; set; }
        [UelParam("P67", Table = "GP Class"), Category("4. Advanced"), DisplayName("P67 GP Class")] public string GpClass { get; set; }
        [UelParam("P68", Table = "GP Finish"), Category("4. Advanced"), DisplayName("P68 GP Finish")] public string GpFinish { get; set; }
        [UelParam("P69", Table = "SF Name"), Category("4. Advanced"), DisplayName("P69 SF Name")] public string SfName { get; set; }
        [UelParam("P70", Table = "SF Class"), Category("4. Advanced"), DisplayName("P70 SF Class (trong = 99 / 83)")] public string SfClass { get; set; }
        [UelParam("P71", Table = "SF Finish"), Category("4. Advanced"), DisplayName("P71 SF Finish")] public string SfFinish { get; set; }
        [UelParam("P72", Table = "FB Name"), Category("4. Advanced"), DisplayName("P72 FB Name")] public string FbName { get; set; }
        [UelParam("P73", Table = "FB Class"), Category("4. Advanced"), DisplayName("P73 FB Class")] public string FbClass { get; set; }
        [UelParam("P74", Table = "FB Finish"), Category("4. Advanced"), DisplayName("P74 FB Finish")] public string FbFinish { get; set; }
        [UelParam("P75", Table = "FB Profile"), Category("4. Advanced"), DisplayName("P75 FB Profile")] public string FbProfile { get; set; }
        [UelParam("P76", Table = "FB Mat"), Category("4. Advanced"), DisplayName("P76 FB Material")] public string FbMat { get; set; }
        [UelParam("P77", Table = "Bolt Standard"), Category("4. Advanced"), DisplayName("P77 Bolt standard")] public string BoltStandard { get; set; }

        // ---------------- cờ "ô có nhập" (không phải biến hộp thoại)
        [Browsable(false)] public bool SlotHoleSet { get; set; }
        [Browsable(false)] public bool ScallopSizeSet { get; set; }
        [Browsable(false)] public bool LowerFlgImproveSet { get; set; }
        [Browsable(false)] public bool CwsSet { get; set; }

        public override string ToString()
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            return string.Format(ci,
                "P11={0} P12={1} P15={2} P30='{3}' P31={4} P40={5} P41={6} P43={7} CWS={8} | P65='{9}' GP[{10},{11}] SF[{12},{13}] FB[{14},{15},{16},{17}] bolt {18}",
                PlateSide ? 1 : 0, BoltSize, SlotHoleSet ? SlotHole.ToString(ci) : "auto", GpPrefix, BackSf ? 1 : 0, ScallopType,
                ScallopSizeSet ? ScallopSize.ToString(ci) : "auto", LowerFlgImproveSet ? (LowerFlgImprove ? "1" : "0") : "auto",
                CwsSet ? Cws.ToString(ci) : "auto", TablePath, GpName, GpClass, SfName, SfClass, FbName, FbClass, FbProfile, FbMat, BoltStandard);
        }
    }
}
