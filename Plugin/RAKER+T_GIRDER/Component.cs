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
            // Như bản RakerTG cũ: không nhập FB Mat. thì lấy theo vật liệu bản đế
            if (values != null && !values.Contains("P5")) p.FlatBarMat = p.BasePlateMat;
            p.BoltQtyX = Math.Max(1, p.BoltQtyX);
            p.BoltQtyY = Math.Max(1, p.BoltQtyY);
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
            RakerTGBuilder b = new RakerTGBuilder(model, (RakerTGParams)prm);
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
