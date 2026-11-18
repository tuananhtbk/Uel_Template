// Plugin COL_TO_HAUNCH_GIRDER (bản DEV: COL_TO_HAUNCH_GIRDER_DEV): liên kết cột -> dầm HAUNCH (CGP + bu lông, cánh hàn CJP).
// Clone COL_TO_GIRDER, theo custom component "YGY_HAUNCH_Moment" (File goc\COL-HUN-PBOX\YGY-HAUNCH_Moment.uel). Hình học nằm ở Builder.cs.
//   Part 1 (primary)   = cột   (giả định, xem README)
//   Part 2 (secondary) = dầm haunch (HUN..., cao thay đổi)
// Tên thuộc tính (P4, P11, ...) giữ ĐÚNG như component gốc. File này chỉ có hộp thoại và [StructuresField], không dùng lớp nào của Core.
using Tekla.Structures.Plugins;

#if UEL_DEV
namespace UEL.PluginsDev
#else
namespace UEL.Plugins
#endif
{
    public class ColToHaunchGirderData
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
        [StructuresField("P28")] public double WingLength;
        [StructuresField("P37")] public string WingMat;
        [StructuresField("P40_diameter")] public double BoltSize;
        [StructuresField("P40_screwdin")] public string BoltStandard;
        [StructuresField("P21A")] public double BoltEdgeSum;
        [StructuresField("P21B")] public double TwoPitchHeight;
        [StructuresField("P47")] public double L3;
        [StructuresField("P50")] public int Wp1;
        [StructuresField("P51")] public int Wp2;
        [StructuresField("P52")] public int GussetStartNo;
        [StructuresField("P53")] public int WingStartNo;
        [StructuresField("P54")] public string GwpZone;
        [StructuresField("P55")] public double TopRingThk;
        [StructuresField("P56")] public double BottRingThk;
        [StructuresField("P58")] public double TopRingPlus;
        [StructuresField("P59")] public double BottRingPlus;
        [StructuresField("P60")] public double TopFlgToBotRing;

        // ---- Tab " Parameters 2 " ----
        [StructuresField("P61")] public double BotFlgToTopRing;
        [StructuresField("P62")] public int TopWebCut;
        [StructuresField("P63")] public int BotWebCut;
        [StructuresField("P64")] public int CgpTopCut;
        [StructuresField("P65")] public int CgpBotCut;

        // ---- Biến mới (không có trong .uel gốc, người dùng 18/11/2026): tuỳ biến theo dự án, trống = như gốc ----
        [StructuresField("P74")] public double PinGap;     // khe đầu dầm khi PIN (gốc 20, P-BOX 30)
        [StructuresField("P75")] public double MoGap;      // khe đầu dầm khi MO (5)
        [StructuresField("P76")] public string WpMark;     // đuôi mark bản cánh (WP / GWP)
        [StructuresField("P77")] public int FbSymmetric;   // FB 2 bên theo WP1 (P-BOX)
        [StructuresField("P78")] public string TablePath;  // file bảng tra (ô file_in)
        [StructuresField("P7")] public double BevelAngle;  // góc vát mép hàn (P7 có trong .uel, không có trong hộp thoại gốc)
        [StructuresField("P79")] public int SlotInBeam;    // lỗ ô van ở dầm (mặc định No = ở CGP)
        [StructuresField("P80")] public double ScallopSize; // scallop R (35 / 30)
        [StructuresField("P81")] public double WpStraight;  // WP: đoạn rộng tới mép cột (trống = (P28+ring)/2)
        [StructuresField("P83")] public int CgpSideMode;    // ô danh sách: 0 +X/+Y (mặc định), 1 +X/-Y, 2 -X/+Y, 3 -X/-Y, 4 theo dầm
        [StructuresField("P82")] public double WpTip;

        // ---- Tab " Advanced " (biến mới 18/11/2026): thuộc tính part CGP / WP / FB ----
        [StructuresField("P84")] public string CgpName;
        [StructuresField("P85")] public string CgpClass;
        [StructuresField("P86")] public string CgpFinish;
        [StructuresField("P87")] public string CgpUda;
        [StructuresField("P88")] public string WpName;
        [StructuresField("P89")] public string WpClass;
        [StructuresField("P90")] public string WpFinish;
        [StructuresField("P91")] public string WpUda;
        [StructuresField("P92")] public string FbName;
        [StructuresField("P93")] public string FbClass;
        [StructuresField("P94")] public string FbFinish;
        [StructuresField("P95")] public string FbUda;
        [StructuresField("P96")] public string FbProfile;
        [StructuresField("P97")] public string FbMat;
        [StructuresField("P98")] public int FbStartNo;

        // ---- Biến mới riêng cho dầm HAUNCH (18/11/2026) ----
        [StructuresField("P99")] public double Top2Bolt;     // mặt ngoài cánh trên -> hàng bu lông trên (BOLT_TOP, trống = bảng tra)
        [StructuresField("P100")] public int ScallopTop;     // R_TOP (P34 của .uel)
        [StructuresField("P101")] public int ScallopBot;     // R_BOT (P17 của .uel)
    }

    [Plugin(ColToHaunchGirderPlugin.PluginName)]
    [PluginUserInterface(ColToHaunchGirderPlugin.UserInterface)]
    [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ONE)]
    [AutoDirectionType(Tekla.Structures.AutoDirectionTypeEnum.AUTODIR_BASIC)]
    [PositionType(Tekla.Structures.PositionTypeEnum.COLLISION_PLANE)]
    public class ColToHaunchGirderPlugin : UelConnection
    {
        public const string PluginName = PluginNames.Name;   // = tên thư mục, bản DEV thêm _DEV

        private readonly ColToHaunchGirderData _data;

        public ColToHaunchGirderPlugin(ColToHaunchGirderData data)
        {
            _data = data;
        }

        protected override object DialogData { get { return _data; } }

        // Hộp thoại sinh bởi _hinh\VeHinh.cs (ra _hinh\dialog.inp + bitmaps\UEL_COL2HUN_*.bmp). Giữ tên biến như .inp gốc; 1 tab duy nhất
        // (người dùng 18/11/2026: gộp tab " Parameters 2 " vào tab 1 - tên biến không đổi nên model cũ vẫn đọc được).
        public const string UserInterface = @"
page(""TeklaStructures"", """")
{
    joint(1, """ + PluginName + @""")
    {
        tab_page("""", "" Parameters 1 "", 1)
        {
            picture(""UEL_COL2HUN_1"", 900, 440, 10, 10)
            parameter("""", ""P47"", distance, number, 264, 338, 50)
            parameter("""", ""P4"", distance, number, 170, 108, 50)
            parameter("""", ""P22"", string, text, 158, 225, 60)
            parameter("""", ""P21A"", distance, number, 158, 160, 60)
            parameter("""", ""P24"", distance, number, 272, 230, 50)
            parameter("""", ""P99"", distance, number, 272, 180, 46)
            parameter("""", ""P55"", distance, number, 362, 24, 50)
            parameter("""", ""P58"", distance, number, 450, 65, 34)
            parameter("""", ""P60"", distance, number, 450, 105, 34)
            parameter("""", ""P59"", distance, number, 450, 386, 34)
            parameter("""", ""P61"", distance, number, 450, 348, 34)
            parameter("""", ""P56"", distance, number, 362, 422, 50)
            parameter("""", ""P75"", distance, number, 260, 24, 40)
            parameter("""", ""P74"", distance, number, 260, 48, 40)
            parameter("""", ""P7"", angle, number, 170, 62, 40)
            parameter("""", ""P80"", distance, number, 260, 124, 40)
            parameter("""", ""P28"", distance, number, 610, 18, 50)
            parameter("""", ""P51"", integer, number, 530, 114, 40)
            parameter("""", ""P50"", integer, number, 530, 326, 40)
            parameter("""", ""P81"", distance, number, 672, 80, 44)
            parameter("""", ""P82"", distance, number, 550, 162, 40)
            parameter("""", ""P11"", YesNo, number, 510, 280, 46)
            parameter("""", ""P77"", YesNo, number, 770, 414, 46)
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
            attribute("""", ""WP / FB"", label, ""%s"", none, none, ""0"", ""0"", 930, 319)
            attribute("""", ""Zone"", label, ""%s"", none, none, ""0"", ""0"", 930, 341)
            parameter("""", ""P54"", string, text, 1020, 338, 110)
            attribute("""", ""Mark"", label, ""%s"", none, none, ""0"", ""0"", 930, 368)
            parameter("""", ""P76"", string, text, 1020, 365, 110)
            attribute("""", ""Mat."", label, ""%s"", none, none, ""0"", ""0"", 930, 395)
            parameter("""", ""P37"", material, text, 1020, 392, 110)
            attribute("""", ""Start no."", label, ""%s"", none, none, ""0"", ""0"", 930, 422)
            parameter("""", ""P53"", integer, number, 1020, 419, 110)
            attribute("""", ""Web cut top"", label, ""%s"", none, none, ""0"", ""0"", 10, 465)
            parameter("""", ""P62"", YesNo, number, 90, 462, 70)
            attribute("""", ""Web cut bot"", label, ""%s"", none, none, ""0"", ""0"", 180, 465)
            parameter("""", ""P63"", YesNo, number, 260, 462, 70)
            attribute("""", ""CGP cut top"", label, ""%s"", none, none, ""0"", ""0"", 350, 465)
            parameter("""", ""P64"", YesNo, number, 430, 462, 70)
            attribute("""", ""CGP cut bot"", label, ""%s"", none, none, ""0"", ""0"", 520, 465)
            parameter("""", ""P65"", YesNo, number, 600, 462, 70)
            attribute("""", ""Scallop tren"", label, ""%s"", none, none, ""0"", ""0"", 700, 465)
            parameter("""", ""P100"", YesNo, number, 780, 462, 60)
            attribute("""", ""Scallop duoi"", label, ""%s"", none, none, ""0"", ""0"", 700, 497)
            parameter("""", ""P101"", YesNo, number, 780, 494, 60)
            attribute("""", ""Bang tra (trong = COL_TO_HAUNCH_GIRDER.txt)"", label, ""%s"", none, none, ""0"", ""0"", 10, 497)
            parameter("""", ""P78"", file_in, text, 250, 494, 420)
        }
        tab_page("""", "" Advanced "", 2)
        {
            attribute("""", ""CGP"", label, ""%s"", none, none, ""0"", ""0"", 130, 18)
            attribute("""", ""WP"", label, ""%s"", none, none, ""0"", ""0"", 350, 18)
            attribute("""", ""FB"", label, ""%s"", none, none, ""0"", ""0"", 570, 18)
            attribute("""", ""Name"", label, ""%s"", none, none, ""0"", ""0"", 20, 44)
            parameter("""", ""P84"", string, text, 130, 41, 200)
            parameter("""", ""P88"", string, text, 350, 41, 200)
            parameter("""", ""P92"", string, text, 570, 41, 200)
            attribute("""", ""Class"", label, ""%s"", none, none, ""0"", ""0"", 20, 74)
            parameter("""", ""P85"", string, text, 130, 71, 200)
            parameter("""", ""P89"", string, text, 350, 71, 200)
            parameter("""", ""P93"", string, text, 570, 71, 200)
            attribute("""", ""Finish"", label, ""%s"", none, none, ""0"", ""0"", 20, 104)
            parameter("""", ""P86"", string, text, 130, 101, 200)
            parameter("""", ""P90"", string, text, 350, 101, 200)
            parameter("""", ""P94"", string, text, 570, 101, 200)
            attribute("""", ""User fields (UDA):  TEN=GIA_TRI; TEN2=GIA_TRI2"", label, ""%s"", none, none, ""0"", ""0"", 20, 144)
            attribute("""", ""CGP"", label, ""%s"", none, none, ""0"", ""0"", 20, 170)
            parameter("""", ""P87"", string, text, 130, 167, 640)
            attribute("""", ""WP"", label, ""%s"", none, none, ""0"", ""0"", 20, 200)
            parameter("""", ""P91"", string, text, 130, 197, 640)
            attribute("""", ""FB"", label, ""%s"", none, none, ""0"", ""0"", 20, 230)
            parameter("""", ""P95"", string, text, 130, 227, 640)
            attribute("""", ""FB"", label, ""%s"", none, none, ""0"", ""0"", 20, 270)
            attribute("""", ""Profile"", label, ""%s"", none, none, ""0"", ""0"", 20, 296)
            parameter("""", ""P96"", profile, text, 130, 293, 200)
            attribute("""", ""Material"", label, ""%s"", none, none, ""0"", ""0"", 20, 326)
            parameter("""", ""P97"", material, text, 130, 323, 200)
            attribute("""", ""Start no."", label, ""%s"", none, none, ""0"", ""0"", 20, 356)
            parameter("""", ""P98"", integer, number, 130, 353, 200)
        }
    }
}
";
    }
}
