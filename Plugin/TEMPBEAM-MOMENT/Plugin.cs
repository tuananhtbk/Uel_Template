// Plugin TEMPBEAM-MOMENT (bản DEV: TEMPBEAM-MOMENT_DEV): liên kết moment dầm tạm -> dầm chính (GP + bu lông,
// cánh hàn CJP, FB, BACK SF, SF dưới). Viết lại từ custom component "♥♥♥YGY_TempBeam-Moment"
// (File goc\PIN-MOMENT CHANGSIN\YGY_TEMPBEAM-MOMENT.uel). Hình học nằm ở Builder.cs.
//   Part 1 (primary)   = dầm chính
//   Part 2 (secondary) = dầm tạm (profile tra trong bảng YGY-TB-MOMENT.txt)
// Tên thuộc tính (P11, P12, ...) giữ ĐÚNG như component gốc. File này chỉ có hộp thoại và [StructuresField], không dùng lớp nào của Core.
using Tekla.Structures.Plugins;

#if UEL_DEV
namespace UEL.PluginsDev
#else
namespace UEL.Plugins
#endif
{
    public class TempBeamData
    {
        // ---- Tab " Parameters 1 " (đủ 9 biến của .inp gốc) ----
        [StructuresField("P11")] public int PlateSide;
        [StructuresField("P12")] public int BoltSize;
        [StructuresField("P15")] public int SlotHole;
        [StructuresField("P30")] public string GpPrefix;
        [StructuresField("P31")] public int BackSf;
        [StructuresField("P40")] public int ScallopType;
        [StructuresField("P41")] public double ScallopSize;
        [StructuresField("P43")] public int LowerFlgImprove;
        [StructuresField("CWS")] public int Cws;

        // ---- Biến mới (không có trong .uel gốc, 18/11/2026): trống = như gốc ----
        [StructuresField("P65")] public string TablePath;   // file bảng tra dầm (ô file_in)

        // ---- Tab " Advanced " (biến mới): thuộc tính part GP / SF / FB ----
        [StructuresField("P66")] public string GpName;
        [StructuresField("P67")] public string GpClass;
        [StructuresField("P68")] public string GpFinish;
        [StructuresField("P69")] public string SfName;
        [StructuresField("P70")] public string SfClass;
        [StructuresField("P71")] public string SfFinish;
        [StructuresField("P72")] public string FbName;
        [StructuresField("P73")] public string FbClass;
        [StructuresField("P74")] public string FbFinish;
        [StructuresField("P75")] public string FbProfile;
        [StructuresField("P76")] public string FbMat;
        [StructuresField("P77")] public string BoltStandard;
    }

    [Plugin(TempBeamPlugin.PluginName)]
    [PluginUserInterface(TempBeamPlugin.UserInterface)]
    [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ONE)]
    [AutoDirectionType(Tekla.Structures.AutoDirectionTypeEnum.AUTODIR_BASIC)]
    [PositionType(Tekla.Structures.PositionTypeEnum.MIDDLE_PLANE)]
    public class TempBeamPlugin : UelConnection
    {
        public const string PluginName = PluginNames.Name;   // = tên thư mục, bản DEV thêm _DEV

        private readonly TempBeamData _data;

        public TempBeamPlugin(TempBeamData data)
        {
            _data = data;
        }

        protected override object DialogData { get { return _data; } }

        // Hộp thoại sinh bởi _hinh\VeHinh.cs (ra _hinh\dialog.inp + bitmaps\UEL_TBM_1.bmp, vẽ theo phong cách hình mặc định của Tekla).
        // Giữ tên biến, kiểu và tab " Parameters 1 " như .inp gốc; thêm tab " Advanced ".
        public const string UserInterface = @"
page(""TeklaStructures"", """")
{
    joint(1, """ + PluginName + @""")
    {
        tab_page("""", "" Parameters 1 "", 1)
        {
            picture(""UEL_TBM_1"", 640, 480, 10, 10)
            attribute("""", ""Bolt M"", label, ""%s"", none, none, ""0"", ""0"", 222, 155)
            parameter("""", ""P12"", integer, number, 264, 152, 40)
            attribute("""", ""Slot"", label, ""%s"", none, none, ""0"", ""0"", 234, 311)
            parameter("""", ""P15"", integer, number, 264, 308, 40)
            attribute("""", ""Scallop R"", label, ""%s"", none, none, ""0"", ""0"", 252, 55)
            parameter("""", ""P41"", distance, number, 312, 52, 40)
            attribute("""", ""Scallop"", label, ""%s"", none, none, ""0"", ""0"", 212, 425)
            parameter("""", ""P40"", chamfer_type, number, 260, 422, 50)
            parameter("""", ""P11"", YesNo, number, 452, 228, 52)
            parameter("""", ""P31"", YesNo, number, 520, 158, 52)
            attribute("""", ""GP / SF prefix"", label, ""%s"", none, none, ""0"", ""0"", 670, 23)
            parameter("""", ""P30"", string, text, 820, 20, 120)
            attribute("""", ""(trong = theo Part 1)"", label, ""%s"", none, none, ""0"", ""0"", 670, 45)
            attribute("""", ""CWS (900, SPS 700)"", label, ""%s"", none, none, ""0"", ""0"", 670, 79)
            parameter("""", ""CWS"", integer, number, 820, 76, 120)
            attribute("""", ""(trong = auto)"", label, ""%s"", none, none, ""0"", ""0"", 670, 101)
            attribute("""", ""Gia cuong canh duoi"", label, ""%s"", none, none, ""0"", ""0"", 670, 135)
            parameter("""", ""P43"", YesNo, number, 820, 132, 120)
            attribute("""", ""(trong = auto, P39 >= 21)"", label, ""%s"", none, none, ""0"", ""0"", 670, 157)
            attribute("""", ""GP side: No = +Z dam, Yes = -Z dam"", label, ""%s"", none, none, ""0"", ""0"", 670, 197)
            attribute("""", ""Back SF: Yes = co tam phia sau"", label, ""%s"", none, none, ""0"", ""0"", 670, 219)
            attribute("""", ""Slot / R / Gia cuong: trong = tinh nhu .uel"", label, ""%s"", none, none, ""0"", ""0"", 670, 241)
            attribute("""", ""Bang tra dam (trong = YGY-TB-MOMENT.txt)"", label, ""%s"", none, none, ""0"", ""0"", 10, 505)
            parameter("""", ""P65"", file_in, text, 270, 502, 480)
        }
        tab_page("""", "" Advanced "", 2)
        {
            attribute("""", ""GP"", label, ""%s"", none, none, ""0"", ""0"", 130, 18)
            attribute("""", ""SF (Back SF / SF duoi)"", label, ""%s"", none, none, ""0"", ""0"", 350, 18)
            attribute("""", ""FB"", label, ""%s"", none, none, ""0"", ""0"", 570, 18)
            attribute("""", ""Name"", label, ""%s"", none, none, ""0"", ""0"", 20, 44)
            parameter("""", ""P66"", string, text, 130, 41, 200)
            parameter("""", ""P69"", string, text, 350, 41, 200)
            parameter("""", ""P72"", string, text, 570, 41, 200)
            attribute("""", ""Class"", label, ""%s"", none, none, ""0"", ""0"", 20, 74)
            parameter("""", ""P67"", string, text, 130, 71, 200)
            parameter("""", ""P70"", string, text, 350, 71, 200)
            parameter("""", ""P73"", string, text, 570, 71, 200)
            attribute("""", ""Finish"", label, ""%s"", none, none, ""0"", ""0"", 20, 104)
            parameter("""", ""P68"", string, text, 130, 101, 200)
            parameter("""", ""P71"", string, text, 350, 101, 200)
            parameter("""", ""P74"", string, text, 570, 101, 200)
            attribute("""", ""SF class trong = 99 (Back SF) / 83 (SF duoi)"", label, ""%s"", none, none, ""0"", ""0"", 350, 134)
            attribute("""", ""FB profile"", label, ""%s"", none, none, ""0"", ""0"", 20, 170)
            parameter("""", ""P75"", profile, text, 130, 167, 200)
            attribute("""", ""FB material"", label, ""%s"", none, none, ""0"", ""0"", 20, 200)
            parameter("""", ""P76"", material, text, 130, 197, 200)
            attribute("""", ""Bolt standard"", label, ""%s"", none, none, ""0"", ""0"", 20, 230)
            parameter("""", ""P77"", bolt_standard, text, 130, 227, 200)
        }
    }
}
";
    }
}
