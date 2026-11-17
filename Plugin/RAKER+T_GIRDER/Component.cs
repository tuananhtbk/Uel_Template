// Khai báo component RAKER+T_GIRDER: tiêu đề, câu nhắc chọn cấu kiện, và nối sang RakerTGBuilder (Builder.cs).
// Tên component/plugin = tên thư mục (build.bat tự lấy), không khai báo ở đây.
using System;
using System.Collections;
using Tekla.Structures.Model;
using UEL.Core;

namespace UEL.Core.Components.RakerTG
{
    public class RakerTGComponent : UelComponent
    {
        public override string Title { get { return "Raker -> T-girder (0-RAKER+TG)"; } }
        public override string SourceUel { get { return "RAKER+T_GIRDER.uel"; } }
        public override string PrimaryPrompt { get { return "Chon dam T-girder (dam chinh)"; } }
        public override string[] SecondaryPrompts { get { return new[] { "Chon raker (dam phu)" }; } }

        public override object NewParams() { return new RakerTGParams(); }

        public override void AfterApply(object prm, IDictionary values)
        {
            RakerTGParams p = (RakerTGParams)prm;
            // FB Mat. (P5) để trống -> SS275 như mặc định của .uel gốc (người dùng 17/11/2026; trước đây lấy theo bản đế)
            if (string.IsNullOrEmpty(p.FlatBarProfile)) p.FlatBarProfile = "FB25*9";
            // P24 để trống -> bản mã tự đặt phía trong góc primary/secondary; có nhập P24 -> theo P24
            if (values != null && values.Contains("P24")) p.GussetSideAuto = false;
            p.BoltQtyX = Math.Max(1, p.BoltQtyX);
            p.BoltQtyY = Math.Max(1, p.BoltQtyY);
            if (p.TablePath == null) p.TablePath = "";
            if (p.ConnType == 1)
                Logger.Log("CANH BAO: P13 = 1 (kieu khong ban ma/bu long) chua duoc lam - dang dung theo kieu P13 = 0.");
        }

        public override bool Build(Model model, object prm, ModelObject primary, IList secondaries, IList created)
        {
            Beam girder = primary as Beam;
            Beam raker = secondaries.Count > 0 ? secondaries[0] as Beam : null;
            if (girder == null || raker == null)
            {
                Logger.Log("Can chon 1 dam chinh (T-girder) va 1 dam phu (raker) dang Beam.");
                return false;
            }
            RakerTGParams p = (RakerTGParams)prm;
            if (p.UseTable)
            {
                // Bảng tra theo profile Part 2 (người dùng 17/11/2026): làm trên BẢN SAO để không đổi bộ thông số của
                // hộp thoại/EXE (lần dựng sau với P57 = No vẫn ra đúng giá trị hộp thoại).
                p = (RakerTGParams)typeof(object).GetMethod("MemberwiseClone",
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(prm, null);
                string modelPath = "";
                try { modelPath = model.GetInfo().ModelPath; } catch (Exception) { }
                IDictionary got = LookupTable.ApplyByKey(p, p.TablePath, UEL.ComponentInfo.Name + ".txt", modelPath,
                                                         raker.Profile.ProfileString, "P56", "P57");
                // giữ đúng các quy tắc của AfterApply cho giá trị lấy từ bảng
                if (got.Contains("P24")) p.GussetSideAuto = false;
                p.BoltQtyX = Math.Max(1, p.BoltQtyX);
                p.BoltQtyY = Math.Max(1, p.BoltQtyY);
            }
            else
            {
                Logger.Log("Bang tra: tat (P57 = No) -> chi dung gia tri hop thoai.");
            }
            RakerTGBuilder b = new RakerTGBuilder(model, p);
            bool ok;
            try
            {
                ok = b.Build(girder, raker);
            }
            finally
            {
                foreach (ModelObject o in b.Created) created.Add(o);
            }
            return ok;
        }
    }
}
