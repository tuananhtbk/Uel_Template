// MẪU - Vỏ plugin Tekla: dữ liệu hộp thoại + khai báo plugin. KHÔNG chứa hình học, KHÔNG dùng lớp nào của Core.
// Tên plugin = tên thư mục (PluginNames.Name; bản DEV tự thêm _DEV).
using Tekla.Structures.Plugins;

#if UEL_DEV
namespace UEL.PluginsDev
#else
namespace UEL.Plugins
#endif
{
    public class XxxData
    {
        // int / double / string. Tên trong [StructuresField] = tên biến (P1, ...) giống [UelParam] trong Params.cs
        [StructuresField("P1")] public double PlateThk;
        [StructuresField("P2")] public string PlateMat;
        [StructuresField("P3")] public string Prefix;
    }

    [Plugin(XxxPlugin.PluginName)]
    [PluginUserInterface(XxxPlugin.UserInterface)]
    // Detail (1 cấu kiện):  [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ZERO)] + [DetailType(Tekla.Structures.DetailTypeEnum.END)]
    // Connection (1 phụ):   [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ONE)]
    [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ZERO)]
    [PositionType(Tekla.Structures.PositionTypeEnum.MIDDLE_PLANE)]
    public class XxxPlugin : UelConnection
    {
        public const string PluginName = PluginNames.Name;

        private readonly XxxData _data;

        public XxxPlugin(XxxData data)
        {
            _data = data;
        }

        protected override object DialogData { get { return _data; } }

        // Có thể lấy gần như nguyên phần .inp của component gốc:
        //   python -I _tools/uel_tool.py unpack "XXX.uel" out   (hoặc xem _PHAN_TICH/components/XXX.uel.md)
        // nhân đôi dấu " , rồi thay tên trong joint(...)/detail(...) bằng  """ + PluginName + @"""
        public const string UserInterface = @"
page(""TeklaStructures"", """")
{
    detail(1, """ + PluginName + @""")
    {
        tab_page("""", ""Parameters"", 1)
        {
            parameter(""Plate thk."", ""P1"", distance, number, 1)
            parameter(""Mat."", ""P2"", material, text, 2)
            parameter(""Mark"", ""P3"", string, text, 3)
        }
    }
}
";
    }
}
