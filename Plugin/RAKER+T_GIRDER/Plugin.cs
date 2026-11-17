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

        // Hộp thoại: lấy nguyên từ 0-RAKER+TG.inp, chỉ đổi tên joint và bỏ 2 dòng picture (file .uel gốc không kèm ảnh).
        public const string UserInterface = @"
page(""TeklaStructures"", """")
{
    joint(1, """ + PluginName + @""")
    {
        tab_page("""", ""Base Plate"", 1)
        {
            attribute("""", ""Base Plate Thk."", label, ""%s"", none, none, ""0"", ""0"", 5, 15)
            parameter("""", ""P1"", distance, number, 225, 15, 160)
            attribute("""", ""Mat."", label, ""%s"", none, none, ""0"", ""0"", 5, 50)
            parameter("""", ""P2"", material, text, 225, 50, 160)
            attribute("""", ""Class"", label, ""%s"", none, none, ""0"", ""0"", 10, 95)
            parameter("""", ""P3"", integer, number, 225, 90, 160)
            attribute("""", ""Mark"", label, ""%s"", none, none, ""0"", ""0"", 10, 140)
            parameter("""", ""P4"", string, text, 225, 130, 160)
            attribute("""", ""Top gap"", label, ""%s"", none, none, ""0"", ""0"", 450, 10)
            parameter("""", ""P6"", distance, number, 610, 10, 160)
            attribute("""", ""Left gap"", label, ""%s"", none, none, ""0"", ""0"", 450, 85)
            parameter("""", ""P7"", distance, number, 610, 85, 160)
            attribute("""", ""Bott. gap"", label, ""%s"", none, none, ""0"", ""0"", 450, 45)
            parameter("""", ""P8"", distance, number, 610, 50, 160)
            attribute("""", ""Right gap"", label, ""%s"", none, none, ""0"", ""0"", 440, 125)
            parameter("""", ""P19"", distance, number, 610, 125, 160)
        }
        tab_page("""", ""Connection"", 2)
        {
            attribute("""", ""Slotted hole X"", label, ""%s"", none, none, ""0"", ""0"", 30, 285)
            parameter("""", ""P39"", distance, number, 210, 285, 160)
            attribute("""", ""Slotted hole Y"", label, ""%s"", none, none, ""0"", ""0"", 30, 330)
            parameter("""", ""P41"", distance, number, 210, 330, 160)
            attribute("""", ""Bolt distance y"", label, ""%s"", none, none, ""0"", ""0"", 1165, 285)
            parameter("""", ""P43"", distance, number, 1360, 290, 160)
            attribute("""", ""Bolt to Gp_x (eH)"", label, ""%s"", none, none, ""0"", ""0"", 1315, 370)
            parameter("""", ""P44"", distance, number, 1355, 410, 160)
            attribute("""", ""Bolt to Gp_y (eV)"", label, ""%s"", none, none, ""0"", ""0"", 1160, 195)
            parameter("""", ""P45"", distance, number, 1360, 195, 160)
            attribute("""", ""Gap Web beam to base pl"", label, ""%s"", none, none, ""0"", ""0"", 555, 135)
            parameter("""", ""P46"", distance, number, 830, 130, 160)
            parameter("""", ""P51"", YesNo, number, 1165, 595, 160)
            attribute("""", ""FB (Y/N)"", label, ""%s"", none, none, ""0"", ""0"", 460, 670)
            parameter("""", ""P52"", YesNo, number, 605, 670, 160)
            attribute("""", ""Bott Scallop (Y/N)"", label, ""%s"", none, none, ""0"", ""0"", 980, 710)
            parameter("""", ""P53"", YesNo, number, 1190, 705, 160)
            attribute("""", ""Gusset Thk."", label, ""%s"", none, none, ""0"", ""0"", 664, 11)
            parameter("""", ""P20"", distance, number, 828, 7, 160)
            attribute("""", ""Matt"", label, ""%s"", none, none, ""0"", ""0"", 669, 48)
            parameter("""", ""P22"", material, text, 830, 50, 160)
            attribute("""", ""Mark"", label, ""%s"", none, none, ""0"", ""0"", 1045, 5)
            parameter("""", ""P21"", string, text, 1175, 5, 160)
            attribute("""", ""Class"", label, ""%s"", none, none, ""0"", ""0"", 1045, 45)
            parameter("""", ""P23"", integer, number, 1175, 45, 160)
            attribute("""", ""Guss. L or R"", label, ""%s"", none, none, ""0"", ""0"", 665, 90)
            parameter("""", ""P24"", YesNo, number, 830, 90, 160)
            attribute("""", ""Bolt To web Beam"", label, ""%s"", none, none, ""0"", ""0"", 1030, 130)
            parameter("""", ""P29"", distance, number, 1255, 125, 160)
            attribute("""", ""Bolt Q'ty x"", label, ""%s"", none, none, ""0"", ""0"", 925, 370)
            parameter("""", ""P31"", integer, number, 1095, 370, 160)
            attribute("""", ""Bolt distance x"", label, ""%s"", none, none, ""0"", ""0"", 920, 415)
            parameter("""", ""P32"", distance, number, 1100, 410, 160)
            attribute("""", ""Bolt Q'ty y"", label, ""%s"", none, none, ""0"", ""0"", 1170, 255)
            parameter("""", ""P34"", integer, number, 1360, 255, 160)
            attribute("""", ""Bolt size"", label, ""%s"", none, none, ""0"", ""0"", 75, 160)
            parameter("""", ""P38_diameter"", bolt_size, number, 210, 155, 160)
            attribute("""", ""Bolt standard"", label, ""%s"", none, none, ""0"", ""0"", 35, 200)
            parameter("""", ""P38_screwdin"", bolt_standard, text, 210, 200, 160)
            attribute("""", ""Tolerance"", label, ""%s"", none, none, ""0"", ""0"", 55, 240)
            parameter("""", ""P40"", distance, number, 210, 245, 160)
            attribute("""", ""FB Mat."", label, ""%s"", none, none, ""0"", ""0"", 472, 630)
            attribute("""", ""Top Scallop (Y/N)"", label, ""%s"", none, none, ""0"", ""0"", 960, 600)
            parameter("""", ""P5"", material, text, 606, 629, 160)
            attribute("""", ""FB Mark"", label, ""%s"", none, none, ""0"", ""0"", 465, 590)
            parameter("""", ""P11"", string, text, 605, 590, 160)
            attribute("""", ""FLG. Weld gap"", label, ""%s"", none, none, ""0"", ""0"", 960, 530)
            parameter("""", ""P10"", distance, number, 1165, 525, 160)
            attribute("""", ""Weld Angle"", label, ""%s"", none, none, ""0"", ""0"", 980, 565)
            parameter("""", ""P12"", angle, number, 1165, 560, 160)
            attribute("""", ""Conn. Type ( 0 or 1 )"", label, ""%s"", none, none, ""0"", ""0"", 200, 20)
            parameter("""", ""P13"", integer, number, 217, 64, 160)
        }
    }
}
";
    }
}
