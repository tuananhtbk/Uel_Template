// MẪU - Thông số của component. Mỗi biến hộp thoại là một thuộc tính;
// [UelParam] = ĐÚNG tên biến trong .uel gốc (xem _PHAN_TICH/components/<ten>.uel.md).
// Kiểu: bool <- Yes/No (0/1), int <- integer, double <- distance/angle, string <- text/material/profile.
// [Category]/[DisplayName] chỉ dùng cho bảng thông số của EXE thử nghiệm.
using System.ComponentModel;
using UEL.Core;

namespace UEL.Core.Components.Xxx
{
    public class XxxParams
    {
        public XxxParams()
        {
            PlateThk = 12; PlateMat = "SS275"; Prefix = "PL";
        }

        [UelParam("P1"), Category("1. Plate"), DisplayName("P1 Plate thk.")] public double PlateThk { get; set; }
        [UelParam("P2"), Category("1. Plate"), DisplayName("P2 Mat.")] public string PlateMat { get; set; }
        [UelParam("P3"), Category("1. Plate"), DisplayName("P3 Mark")] public string Prefix { get; set; }

        public override string ToString()
        {
            return "P1=" + PlateThk + " P2=" + PlateMat + " P3=" + Prefix;
        }
    }
}
