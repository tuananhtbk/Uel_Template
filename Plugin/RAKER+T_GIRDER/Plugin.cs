// Plugin RAKER+T_GIRDER (bản DEV: RAKER+T_GIRDER_DEV): chân raker tựa lên T-girder.
// Viết lại từ custom component "0-RAKER+TG" (file RAKER+T_GIRDER.uel). Hình học nằm ở Builder.cs.
//   Primary   = T-girder (dầm nhận lực)
//   Secondary = Raker (thanh chống xiên, tiết diện H)
// Tên thuộc tính (P1, P2, ...) giữ ĐÚNG như component gốc để dùng lại được file thuộc tính.
// File này là "vỏ" plugin Tekla: chỉ có hộp thoại và các trường [StructuresField], không dùng lớp nào của Core.
using Tekla.Structures.Plugins;

#if UEL_DEV
namespace UEL.PluginsDev
#else
namespace UEL.Plugins
#endif
{
    public class RakerTGData
    {
        // ---- Tab "Base Plate" ----
        [StructuresField("P1")] public double BasePlateThk;
        [StructuresField("P2")] public string BasePlateMat;
        [StructuresField("P3")] public int BasePlateClass;
        [StructuresField("P4")] public string BasePlateMark;
        [StructuresField("P6")] public double TopGap;
        [StructuresField("P7")] public double LeftGap;
        [StructuresField("P8")] public double BottGap;
        [StructuresField("P19")] public double RightGap;
        [StructuresField("P56")] public string TablePath;        // biến mới: đường dẫn bảng tra theo profile Part 2 (ô file_in, có nút "...")
        [StructuresField("P57")] public int UseTable;            // biến mới: 1 = dùng bảng tra (mặc định), 0 = chỉ hộp thoại

        // ---- Tab "Connection" ----
        [StructuresField("P13")] public int ConnType;
        [StructuresField("P20")] public double GussetThk;
        [StructuresField("P22")] public string GussetMat;
        [StructuresField("P21")] public string GussetMark;
        [StructuresField("P23")] public int GussetClass;
        [StructuresField("P24")] public int GussetSide;
        [StructuresField("P29")] public double BoltToWebEnd;
        [StructuresField("P31")] public int BoltQtyX;
        [StructuresField("P32")] public double BoltDistX;
        [StructuresField("P34")] public int BoltQtyY;
        [StructuresField("P43")] public double BoltDistY;
        [StructuresField("P44")] public double BoltEdgeX;
        [StructuresField("P45")] public double BoltEdgeY;
        [StructuresField("P46")] public double WebGap;
        [StructuresField("P54")] public double GussetLength;     // biến mới (không có trong .uel gốc): dài cạnh dài bản mã
        [StructuresField("P55")] public string FlatBarProfile;   // biến mới (không có trong .uel gốc): profile FB
        [StructuresField("P38_diameter")] public double BoltSize;
        [StructuresField("P38_screwdin")] public string BoltStandard;
        [StructuresField("P40")] public double Tolerance;
        [StructuresField("P39")] public double SlotX;
        [StructuresField("P41")] public double SlotY;
        [StructuresField("P52")] public int FlatBar;
        [StructuresField("P11")] public string FlatBarMark;
        [StructuresField("P5")] public string FlatBarMat;
        [StructuresField("P10")] public double FlangeWeldGap;
        [StructuresField("P12")] public double WeldAngle;
        [StructuresField("P51")] public int TopScallop;
        [StructuresField("P53")] public int BottScallop;
    }

    [Plugin(RakerTGPlugin.PluginName)]
    [PluginUserInterface(RakerTGPlugin.UserInterface)]
    [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ONE)]
    [AutoDirectionType(Tekla.Structures.AutoDirectionTypeEnum.AUTODIR_BASIC)]
    [PositionType(Tekla.Structures.PositionTypeEnum.COLLISION_PLANE)]
    public class RakerTGPlugin : UelConnection
    {
        public const string PluginName = PluginNames.Name;   // = tên thư mục, bản DEV thêm _DEV

        private readonly RakerTGData _data;

        public RakerTGPlugin(RakerTGData data)
        {
            _data = data;
        }

        protected override object DialogData { get { return _data; } }

        // Hộp thoại (17/11/2026): bố trí lại theo hình minh hoạ - sinh bởi _hinh\VeHinh.cs (ra _hinh\dialog.inp + bitmaps\*.bmp).
        // Ô điền đặt đúng chỗ trên hình. Tên biến P.. giữ nguyên như .uel gốc (model lưu theo tên biến).
        // Ảnh: bitmaps\UEL_RAKER_TG_*.bmp, install.bat chép vào thư mục ảnh hộp thoại của Tekla (DAK_BMPPATH).
        public const string UserInterface = @"
page(""TeklaStructures"", """")
{
    joint(1, """ + PluginName + @""")
    {
        tab_page("""", ""Base Plate"", 1)
        {
            picture(""UEL_RAKER_TG_BP"", 600, 400, 10, 10)
            parameter("""", ""P6"", distance, number, 458, 102, 64)
            parameter("""", ""P8"", distance, number, 458, 304, 64)
            parameter("""", ""P7"", distance, number, 70, 50, 64)
            parameter("""", ""P19"", distance, number, 446, 50, 64)
            attribute("""", ""Base Plate Thk."", label, ""%s"", none, none, ""0"", ""0"", 640, 33)
            parameter("""", ""P1"", distance, number, 760, 30, 120)
            attribute("""", ""Mat."", label, ""%s"", none, none, ""0"", ""0"", 640, 68)
            parameter("""", ""P2"", material, text, 760, 65, 120)
            attribute("""", ""Class"", label, ""%s"", none, none, ""0"", ""0"", 640, 103)
            parameter("""", ""P3"", integer, number, 760, 100, 120)
            attribute("""", ""Mark"", label, ""%s"", none, none, ""0"", ""0"", 640, 138)
            parameter("""", ""P4"", string, text, 760, 135, 120)
            attribute("""", ""Bang tra theo profile Part 2"", label, ""%s"", none, none, ""0"", ""0"", 640, 203)
            attribute("""", ""File (trong = RAKER+T_GIRDER.txt)"", label, ""%s"", none, none, ""0"", ""0"", 640, 233)
            parameter("""", ""P56"", file_in, text, 640, 252, 300)
            attribute("""", ""Dung bang tra"", label, ""%s"", none, none, ""0"", ""0"", 640, 295)
            parameter("""", ""P57"", YesNo, number, 760, 292, 120)
        }
        tab_page("""", ""Gusset & Bolt"", 2)
        {
            picture(""UEL_RAKER_TG_GP"", 700, 440, 10, 10)
            parameter("""", ""P46"", distance, number, 60, 415, 64)
            parameter("""", ""P29"", distance, number, 150, 415, 64)
            parameter("""", ""P32"", distance, number, 250, 415, 64)
            parameter("""", ""P44"", distance, number, 410, 415, 64)
            parameter("""", ""P54"", distance, number, 242, 91, 64)
            parameter("""", ""P45"", distance, number, 510, 129, 64)
            parameter("""", ""P43"", distance, number, 510, 182, 64)
            parameter("""", ""P34"", integer, number, 510, 235, 64)
            parameter("""", ""P31"", integer, number, 510, 270, 64)
            attribute("""", ""Conn. type"", label, ""%s"", none, none, ""0"", ""0"", 740, 23)
            parameter("""", ""P13"", integer, number, 880, 20, 110)
            attribute("""", ""Gusset Thk."", label, ""%s"", none, none, ""0"", ""0"", 740, 68)
            parameter("""", ""P20"", distance, number, 880, 65, 110)
            attribute("""", ""Matt"", label, ""%s"", none, none, ""0"", ""0"", 740, 100)
            parameter("""", ""P22"", material, text, 880, 97, 110)
            attribute("""", ""Mark"", label, ""%s"", none, none, ""0"", ""0"", 740, 132)
            parameter("""", ""P21"", string, text, 880, 129, 110)
            attribute("""", ""Class"", label, ""%s"", none, none, ""0"", ""0"", 740, 164)
            parameter("""", ""P23"", integer, number, 880, 161, 110)
            attribute("""", ""Guss. L or R"", label, ""%s"", none, none, ""0"", ""0"", 740, 196)
            parameter("""", ""P24"", YesNo, number, 880, 209, 110)
            attribute("""", ""Bolt size"", label, ""%s"", none, none, ""0"", ""0"", 740, 256)
            parameter("""", ""P38_diameter"", bolt_size, number, 880, 253, 110)
            attribute("""", ""Bolt standard"", label, ""%s"", none, none, ""0"", ""0"", 740, 288)
            parameter("""", ""P38_screwdin"", bolt_standard, text, 880, 285, 110)
            attribute("""", ""Tolerance"", label, ""%s"", none, none, ""0"", ""0"", 740, 320)
            parameter("""", ""P40"", distance, number, 880, 317, 110)
            attribute("""", ""Slotted hole X"", label, ""%s"", none, none, ""0"", ""0"", 740, 352)
            parameter("""", ""P39"", distance, number, 880, 349, 110)
            attribute("""", ""Slotted hole Y"", label, ""%s"", none, none, ""0"", ""0"", 740, 384)
            parameter("""", ""P41"", distance, number, 880, 381, 110)
        }
        tab_page("""", ""Flange & FB"", 3)
        {
            picture(""UEL_RAKER_TG_FB"", 600, 420, 10, 10)
            parameter("""", ""P10"", distance, number, 140, 16, 64)
            parameter("""", ""P12"", angle, number, 210, 50, 64)
            parameter("""", ""P51"", YesNo, number, 240, 135, 64)
            parameter("""", ""P53"", YesNo, number, 240, 280, 64)
            parameter("""", ""P52"", YesNo, number, 240, 382, 64)
            attribute("""", ""FB Mark"", label, ""%s"", none, none, ""0"", ""0"", 640, 43)
            parameter("""", ""P11"", string, text, 730, 40, 120)
            attribute("""", ""FB Mat."", label, ""%s"", none, none, ""0"", ""0"", 640, 78)
            parameter("""", ""P5"", material, text, 730, 75, 120)
            attribute("""", ""FB Profile"", label, ""%s"", none, none, ""0"", ""0"", 640, 113)
            parameter("""", ""P55"", profile, text, 730, 110, 120)
        }
    }
}
";
    }
}
