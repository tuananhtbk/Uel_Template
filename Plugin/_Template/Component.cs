// MẪU - Khai báo component: tiêu đề, câu nhắc chọn cấu kiện, nối sang Builder.
// Tên component/plugin = tên thư mục (build.bat tự lấy), không khai báo ở đây.
using System.Collections;
using Tekla.Structures.Model;
using UEL.Core;

namespace UEL.Core.Components.Xxx
{
    public class XxxComponent : UelComponent
    {
        public override string Title { get { return "Mo ta ngan (ten component goc trong Tekla)"; } }
        public override string SourceUel { get { return "XXX.uel"; } }
        public override string PrimaryPrompt { get { return "Chon cau kien chinh"; } }
        // detail: mảng rỗng; connection: 1 câu nhắc cho mỗi cấu kiện phụ
        public override string[] SecondaryPrompts { get { return new string[0]; } }

        public override object NewParams() { return new XxxParams(); }

        // (tuỳ chọn) chỉnh giá trị phụ thuộc nhau sau khi đọc hộp thoại:
        // public override void AfterApply(object prm, IDictionary values) { }

        public override bool Build(Model model, object prm, ModelObject primary, IList secondaries, IList created)
        {
            Part main = primary as Part;
            if (main == null)
            {
                Logger.Log("Can chon 1 part.");
                return false;
            }
            return new XxxBuilder(model, (XxxParams)prm).Build(main, created);
        }
    }
}
