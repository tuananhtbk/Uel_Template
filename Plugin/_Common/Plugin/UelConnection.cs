// Lớp cha cho mọi plugin connection/detail của bộ UEL.
// Mỗi thư mục component được build thành:
//   18.0\<Ten>.dll , 2020+\<Ten>.dll         (bản phát hành): Core build chung trong DLL, tên plugin = <Ten>
//   _Dev\<bản>\<Ten>_DEV.dll  (/define:UEL_DEV)              : Core nạp động từ _Dev\<bản>\<Ten>.Core.dll mỗi lần Run,
//                                                              tên plugin có hậu tố _DEV
// => sửa code, chạy "build.bat core", rồi bấm Modify trên component _DEV trong Tekla là thấy kết quả mới.
// UEL.ComponentInfo (tên component) và UEL.Host.DevConfig (đường dẫn Core) do build.bat sinh ra.
using System;
using System.Collections;
using System.Reflection;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Plugins;
using UEL.Host;

#if UEL_DEV
namespace UEL.PluginsDev
#else
namespace UEL.Plugins
#endif
{
    public static class PluginNames
    {
#if UEL_DEV
        public const string Suffix = "_DEV";
#else
        public const string Suffix = "";
#endif
        /// Tên plugin trong Tekla (= tên thư mục component, bản DEV thêm _DEV).
        public const string Name = UEL.ComponentInfo.Name + Suffix;
    }

    public abstract class UelConnection : ConnectionBase
    {
        /// Khoá component trong Core (= tên thư mục, không có hậu tố _DEV).
        protected virtual string ComponentKey { get { return UEL.ComponentInfo.Name; } }

        /// Đối tượng dữ liệu hộp thoại (lớp có các trường [StructuresField]).
        protected abstract object DialogData { get; }

        public override bool Run()
        {
            try
            {
                Model model = new Model();
                ModelObject primary = model.SelectModelObject(Primary);
                ArrayList secondaries = new ArrayList();
                foreach (Identifier id in Secondaries)
                {
                    ModelObject o = model.SelectModelObject(id);
                    if (o != null) secondaries.Add(o);
                }

                CoreHost core = GetCore();
                object prm = core.NewParams(ComponentKey);
                core.Apply(ComponentKey, prm, CollectValues(DialogData));
                return core.Build(ComponentKey, prm, model, primary, secondaries, new ArrayList());
            }
            catch (Exception ex)
            {
                HostLog.Write(PluginNames.Name + " loi: " + ex);
                return false;
            }
        }

        private static CoreHost GetCore()
        {
#if UEL_DEV
            return CoreHost.LoadFile(DevConfig.CorePath);
#else
            return CoreHost.FromAssembly(typeof(UelConnection).Assembly);
#endif
        }

        /// Đọc các trường [StructuresField]; bỏ qua ô để trống (giá trị mặc định của Tekla)
        /// để Core giữ giá trị mặc định của component.
        private Hashtable CollectValues(object data)
        {
            Hashtable values = new Hashtable();
            if (data == null) return values;
            foreach (FieldInfo f in data.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                StructuresFieldAttribute a = (StructuresFieldAttribute)Attribute.GetCustomAttribute(f, typeof(StructuresFieldAttribute));
                if (a == null) continue;
                object v = f.GetValue(data);
                if (v is int) { if (IsDefaultValue((int)v)) continue; }
                else if (v is double) { if (IsDefaultValue((double)v)) continue; }
                else if (v is string) { string s = (string)v; if (s == null || s.Trim().Length == 0 || IsDefaultValue(s)) continue; }
                else if (v == null) continue;
                values[a.AttributeName] = v;
            }
            return values;
        }
    }
}
