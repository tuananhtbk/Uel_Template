// Khung chung cho mọi component trong bộ UEL.
//
// Mỗi component là 1 thư mục Plugin\<Ten component>\ (tên thư mục = tên file .uel gốc = tên DLL = tên plugin):
//   Params.cs    : thông số, mỗi thuộc tính gắn [UelParam("P..")] đúng tên biến của .uel gốc
//   Builder.cs   : dựng hình học bằng Tekla Open API
//   Component.cs : kế thừa UelComponent, khai báo tiêu đề, câu nhắc chọn cấu kiện, và gọi Builder
//   Plugin.cs    : vỏ plugin Tekla (hộp thoại + trường [StructuresField]) - KHÔNG dùng lớp nào của Core
// Không cần đăng ký ở đâu: build.bat tự nhận thư mục có Plugin.cs.
//
// Quy tắc: mọi thứ đi qua ranh giới Core <-> Plugin/Tester chỉ dùng kiểu của .NET hoặc của Tekla
// (vì Core có thể được nạp động nhiều lần, kiểu riêng của Core ở mỗi lần nạp là khác nhau).
using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using Tekla.Structures.Model;

namespace UEL.Core
{
    /// Gắn lên thuộc tính của lớp Params để ánh xạ với biến trong hộp thoại (P1, P38_diameter, ...).
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class UelParamAttribute : Attribute
    {
        public UelParamAttribute(string name) { Name = name; }
        public string Name { get; private set; }
        /// Tên cột dễ đọc trong bảng tra (vd. "Base Plate Thk"), dùng thay cho tên biến P (18/11/2026).
        /// So khớp không phân biệt hoa thường, bỏ dấu cách và dấu . _ - '. Để trống = chỉ nhận tên biến P.
        public string Table { get; set; }
    }

    public abstract class UelComponent
    {
        /// Khoá của component = tên thư mục = tên plugin phát hành (bản DEV tự thêm hậu tố "_DEV").
        /// build.bat sinh ComponentInfo.Name từ tên thư mục. KHÔNG đổi tên thư mục sau khi đã dùng trong model.
        public virtual string Key { get { return UEL.ComponentInfo.Name; } }
        /// Tên hiển thị trong EXE thử nghiệm.
        public abstract string Title { get; }
        /// File .uel gốc, để đối chiếu.
        public abstract string SourceUel { get; }
        /// Câu nhắc khi chọn cấu kiện chính.
        public abstract string PrimaryPrompt { get; }
        /// Câu nhắc cho từng cấu kiện phụ. Detail: mảng rỗng.
        public abstract string[] SecondaryPrompts { get; }

        /// Bộ thông số mặc định.
        public abstract object NewParams();

        /// Chỉnh thêm sau khi đã nạp giá trị từ hộp thoại (giá trị phụ thuộc nhau, giới hạn...).
        /// values chỉ chứa các ô người dùng đã nhập (ô trống/mặc định không có trong values).
        public virtual void AfterApply(object prm, IDictionary values) { }

        /// Dựng. Trả về true nếu thành công. created: mọi đối tượng đã tạo (EXE dùng để xoá).
        public abstract bool Build(Model model, object prm, ModelObject primary, IList secondaries, IList created);
    }

    /// Ánh xạ giá trị hộp thoại (tên biến -> giá trị) vào lớp Params qua [UelParam].
    public static class ParamMapper
    {
        public static void Apply(object prm, IDictionary values)
        {
            if (prm == null || values == null) return;
            foreach (PropertyInfo pi in prm.GetType().GetProperties())
            {
                UelParamAttribute a = (UelParamAttribute)Attribute.GetCustomAttribute(pi, typeof(UelParamAttribute));
                if (a == null || !pi.CanWrite || !values.Contains(a.Name)) continue;
                object v = values[a.Name];
                try
                {
                    pi.SetValue(prm, ConvertTo(v, pi.PropertyType), null);
                }
                catch (Exception ex)
                {
                    Logger.Log(string.Format("Bo qua {0}={1}: {2}", a.Name, v, ex.Message));
                }
            }
        }

        public static object ConvertTo(object v, Type t)
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            if (v == null) return t.IsValueType ? Activator.CreateInstance(t) : null;
            if (t == typeof(string)) return Convert.ToString(v, ci);
            if (t == typeof(double)) return Convert.ToDouble(v, ci);
            if (t == typeof(int)) return (int)Math.Round(Convert.ToDouble(v, ci));
            if (t == typeof(bool))
            {
                string s = v as string;
                if (s != null) return s == "1" || s.Equals("true", StringComparison.OrdinalIgnoreCase) || s.Equals("yes", StringComparison.OrdinalIgnoreCase);
                return Math.Abs(Convert.ToDouble(v, ci)) > 1e-9;
            }
            return Convert.ChangeType(v, t, ci);
        }
    }
}
