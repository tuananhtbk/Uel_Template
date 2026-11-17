// MẪU - Dựng hình học bằng Tekla Open API. Sửa file này rồi chạy "build.bat core <Ten>" và bấm Modify trong Tekla.
using System.Collections;
using Tekla.Structures.Model;
using UEL.Core;

namespace UEL.Core.Components.Xxx
{
    public class XxxBuilder
    {
        private readonly Model _model;
        private readonly XxxParams _p;

        public XxxBuilder(Model model, XxxParams p)
        {
            _model = model;
            _p = p;
        }

        public bool Build(Part main, IList created)
        {
            Logger.Log("Thong so: " + _p);
            // ... tạo ContourPlate / Beam / BoltArray / CutPlane / Fitting ...
            // mỗi đối tượng Insert() thành công thì: created.Add(obj);
            return true;
        }
    }
}
