// Plugin H-COL_TO_GIRDER (bản DEV: H-COL_TO_GIRDER_DEV): liên kết cột H -> dầm H (CGP + bu lông, cánh hàn CJP).
// Clone COL_TO_GIRDER, theo custom component "0-DM_H-Col2Gir_H-COLUMN" (File goc\H-COL-GIRDER\H-COL.uel). Hình học nằm ở Builder.cs.
//   Part 1 (primary)   = cột H (giả định, xem README)
//   Part 2 (secondary) = dầm H
// Tên thuộc tính (P4, P11, ...) giữ ĐÚNG như component gốc. File này chỉ có hộp thoại và [StructuresField], không dùng lớp nào của Core.
using Tekla.Structures.Plugins;

#if UEL_DEV
namespace UEL.PluginsDev
#else
namespace UEL.Plugins
#endif
{
    public class HColToGirderData
    {
        // ---- Tab " Parameters 1 " ----
        [StructuresField("P4")] public double BoltToFlange;
        [StructuresField("P11")] public int CgpDirection;
        [StructuresField("P12")] public string CgpZone;
        [StructuresField("P13")] public string CgpMat;
        [StructuresField("P14")] public int Moment;
        [StructuresField("P22")] public string BoltPitch;
        [StructuresField("P23")] public double BevelExtra;
        [StructuresField("P24")] public double SlotLength;
        [StructuresField("P40_diameter")] public double BoltSize;
        [StructuresField("P40_screwdin")] public string BoltStandard;
        [StructuresField("P21A")] public double BoltEdgeSum;
        [StructuresField("P21B")] public double TwoPitchHeight;
        [StructuresField("P47")] public double L3;
        [StructuresField("P52")] public int GussetStartNo;
        [StructuresField("P54")] public string GwpZone;

        // ---- Tab " Parameters 2 " ----

        // ---- Biến mới (không có trong .uel gốc, người dùng 18/11/2026): tuỳ biến theo dự án, trống = như gốc ----
        [StructuresField("P74")] public double PinGap;     // khe đầu dầm khi PIN (gốc 20, P-BOX 30)
        [StructuresField("P75")] public double MoGap;      // khe đầu dầm khi MO (5)
        [StructuresField("P78")] public string TablePath;  // file bảng tra (ô file_in)
        [StructuresField("P7")] public double BevelAngle;  // góc vát mép hàn (P7 có trong .uel, không có trong hộp thoại gốc)
        [StructuresField("P79")] public int SlotInBeam;    // lỗ ô van ở dầm (mặc định No = ở CGP)
        [StructuresField("P80")] public double ScallopSize; // scallop R (35 / 30)
        [StructuresField("P83")] public int CgpSideMode;    // ô danh sách: 0 +X/+Y (mặc định), 1 +X/-Y, 2 -X/+Y, 3 -X/-Y, 4 theo dầm

        // ---- Tab " Advanced " (biến mới 18/11/2026): thuộc tính part CGP / WP / FB ----
        [StructuresField("P84")] public string CgpName;
        [StructuresField("P85")] public string CgpClass;
        [StructuresField("P86")] public string CgpFinish;
        [StructuresField("P87")] public string CgpUda;
        [StructuresField("P92")] public string FbName;
        [StructuresField("P93")] public string FbClass;
        [StructuresField("P94")] public string FbFinish;
        [StructuresField("P95")] public string FbUda;
        [StructuresField("P96")] public string FbProfile;
        [StructuresField("P97")] public string FbMat;
        [StructuresField("P98")] public int FbStartNo;       // WP: bề rộng đầu xa ngoài mép cánh (trống = P35)
    }

    [Plugin(HColToGirderPlugin.PluginName)]
    [PluginUserInterface(HColToGirderPlugin.UserInterface)]
    [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ONE)]
    [AutoDirectionType(Tekla.Structures.AutoDirectionTypeEnum.AUTODIR_BASIC)]
    [PositionType(Tekla.Structures.PositionTypeEnum.COLLISION_PLANE)]
    public class HColToGirderPlugin : UelConnection
    {
        public const string PluginName = PluginNames.Name;   // = tên thư mục, bản DEV thêm _DEV

        private readonly HColToGirderData _data;

        public HColToGirderPlugin(HColToGirderData data)
        {
            _data = data;
        }

        protected override object DialogData { get { return _data; } }

        // Hộp thoại sinh bởi _hinh\VeHinh.cs (ra _hinh\dialog.inp + bitmaps\UEL_HCOL2GIR_*.bmp). Giữ tên biến như .inp gốc; 1 tab duy nhất
        // (người dùng 18/11/2026: gộp tab " Parameters 2 " vào tab 1 - tên biến không đổi nên model cũ vẫn đọc được).
        public const string UserInterface = @"
page(""TeklaStructures"", """")
{
    joint(1, """ + PluginName + @""")
    {
        tab_page("""", "" Parameters 1 "", 1)
        {
            picture(""UEL_HCOL2GIR_1"", 900, 440, 10, 10)
            parameter("""", ""P47"", distance, number, 264, 338, 50)
            parameter("""", ""P4"", distance, number, 170, 108, 50)
            parameter("""", ""P22"", string, text, 158, 225, 60)
            parameter("""", ""P21A"", distance, number, 158, 160, 60)
            parameter("""", ""P24"", distance, number, 272, 230, 50)
            parameter("""", ""P75"", distance, number, 260, 24, 40)
            parameter("""", ""P74"", distance, number, 260, 48, 40)
            parameter("""", ""P7"", angle, number, 170, 62, 40)
            parameter("""", ""P80"", distance, number, 260, 124, 40)
            parameter("""", ""P11"", YesNo, number, 510, 280, 46)
            attribute("""", ""MO (Y) / PIN (N)"", label, ""%s"", none, none, ""0"", ""0"", 930, 15)
            parameter("""", ""P14"", YesNo, number, 1020, 12, 110)
            attribute("""", ""CGP"", label, ""%s"", none, none, ""0"", ""0"", 930, 48)
            attribute("""", ""Zone"", label, ""%s"", none, none, ""0"", ""0"", 930, 70)
            parameter("""", ""P12"", string, text, 1020, 67, 110)
            attribute("""", ""Mat."", label, ""%s"", none, none, ""0"", ""0"", 930, 97)
            parameter("""", ""P13"", material, text, 1020, 94, 110)
            attribute("""", ""Start no."", label, ""%s"", none, none, ""0"", ""0"", 930, 124)
            parameter("""", ""P52"", integer, number, 1020, 121, 110)
            attribute("""", ""Slot o dam"", label, ""%s"", none, none, ""0"", ""0"", 930, 151)
            parameter("""", ""P79"", YesNo, number, 1020, 148, 110)
            attribute("""", ""CGP side"", label, ""%s"", none, none, ""0"", ""0"", 930, 178)
            attribute(""P83"", """", option, ""%s"", none, none, ""0.0"", ""0.0"", 1020, 175, 110)
            {
                value(""+X / +Y"", 1)
                value(""+X / -Y"", 0)
                value(""-X / +Y"", 0)
                value(""-X / -Y"", 0)
                value(""Theo dam"", 0)
            }
            attribute("""", ""Bolt size"", label, ""%s"", none, none, ""0"", ""0"", 930, 205)
            parameter("""", ""P40_diameter"", bolt_size, number, 1020, 202, 110)
            attribute("""", ""Standard"", label, ""%s"", none, none, ""0"", ""0"", 930, 232)
            parameter("""", ""P40_screwdin"", bolt_standard, text, 1020, 229, 110)
            attribute("""", ""H 2 pitch"", label, ""%s"", none, none, ""0"", ""0"", 930, 259)
            parameter("""", ""P21B"", distance, number, 1020, 256, 110)
            attribute("""", ""Bevel extra"", label, ""%s"", none, none, ""0"", ""0"", 930, 286)
            parameter("""", ""P23"", distance, number, 1020, 283, 110)
            attribute("""", ""FB"", label, ""%s"", none, none, ""0"", ""0"", 930, 319)
            attribute("""", ""Zone"", label, ""%s"", none, none, ""0"", ""0"", 930, 341)
            parameter("""", ""P54"", string, text, 1020, 338, 110)
            attribute("""", ""Bang tra (trong = H-COL_TO_GIRDER.txt)"", label, ""%s"", none, none, ""0"", ""0"", 10, 465)
            parameter("""", ""P78"", file_in, text, 250, 462, 420)
        }
        tab_page("""", "" Advanced "", 2)
        {
            attribute("""", ""CGP"", label, ""%s"", none, none, ""0"", ""0"", 130, 18)
            attribute("""", ""FB"", label, ""%s"", none, none, ""0"", ""0"", 350, 18)
            attribute("""", ""Name"", label, ""%s"", none, none, ""0"", ""0"", 20, 44)
            parameter("""", ""P84"", string, text, 130, 41, 200)
            parameter("""", ""P92"", string, text, 350, 41, 200)
            attribute("""", ""Class"", label, ""%s"", none, none, ""0"", ""0"", 20, 74)
            parameter("""", ""P85"", string, text, 130, 71, 200)
            parameter("""", ""P93"", string, text, 350, 71, 200)
            attribute("""", ""Finish"", label, ""%s"", none, none, ""0"", ""0"", 20, 104)
            parameter("""", ""P86"", string, text, 130, 101, 200)
            parameter("""", ""P94"", string, text, 350, 101, 200)
            attribute("""", ""User fields (UDA):  TEN=GIA_TRI; TEN2=GIA_TRI2"", label, ""%s"", none, none, ""0"", ""0"", 20, 144)
            attribute("""", ""CGP"", label, ""%s"", none, none, ""0"", ""0"", 20, 170)
            parameter("""", ""P87"", string, text, 130, 167, 640)
            attribute("""", ""FB"", label, ""%s"", none, none, ""0"", ""0"", 20, 200)
            parameter("""", ""P95"", string, text, 130, 197, 640)
            attribute("""", ""FB"", label, ""%s"", none, none, ""0"", ""0"", 20, 240)
            attribute("""", ""Profile"", label, ""%s"", none, none, ""0"", ""0"", 20, 266)
            parameter("""", ""P96"", profile, text, 130, 263, 200)
            attribute("""", ""Material"", label, ""%s"", none, none, ""0"", ""0"", 20, 296)
            parameter("""", ""P97"", material, text, 130, 293, 200)
            attribute("""", ""Start no."", label, ""%s"", none, none, ""0"", ""0"", 20, 326)
            parameter("""", ""P98"", integer, number, 130, 323, 200)
        }
    }
}
";
    }
}
