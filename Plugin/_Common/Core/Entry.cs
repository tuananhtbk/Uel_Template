// Cửa vào duy nhất của Core. Plugin (bản phát hành và bản DEV) và EXE thử nghiệm đều gọi các hàm
// tĩnh ở đây qua reflection (lớp UEL.Host.CoreHost), nên chữ ký chỉ dùng kiểu .NET/Tekla.
//
// Mỗi DLL chỉ chứa 1 component (thư mục <Ten component>). Entry tự tìm lớp kế thừa UelComponent
// trong cùng DLL, không phải đăng ký tay.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Tekla.Structures.Model;

namespace UEL.Core
{
    public static class Entry
    {
        private static List<UelComponent> _all;

        private static List<UelComponent> All
        {
            get
            {
                if (_all != null) return _all;
                List<UelComponent> list = new List<UelComponent>();
                foreach (Type t in typeof(Entry).Assembly.GetTypes())
                {
                    if (t.IsAbstract || !typeof(UelComponent).IsAssignableFrom(t)) continue;
                    if (t.GetConstructor(Type.EmptyTypes) == null) continue;
                    list.Add((UelComponent)Activator.CreateInstance(t));
                }
                _all = list;
                return _all;
            }
        }

        private static UelComponent Find(string key)
        {
            foreach (UelComponent c in All)
                if (string.Equals(c.Key, key, StringComparison.OrdinalIgnoreCase)) return c;
            throw new ArgumentException("Khong co component: " + key);
        }

        /// Thời điểm build Core (để biết đang chạy bản code nào). BuildInfo.Stamp do build sinh ra.
        public static string BuildStamp()
        {
            return BuildInfo.Stamp;
        }

        public static string[] Keys()
        {
            string[] r = new string[All.Count];
            for (int i = 0; i < All.Count; i++) r[i] = All[i].Key;
            return r;
        }

        /// [0] Title, [1] SourceUel, [2] PrimaryPrompt, [3..] SecondaryPrompts
        public static string[] Describe(string key)
        {
            UelComponent c = Find(key);
            List<string> r = new List<string> { c.Title, c.SourceUel, c.PrimaryPrompt };
            r.AddRange(c.SecondaryPrompts);
            return r.ToArray();
        }

        public static object NewParams(string key)
        {
            return Find(key).NewParams();
        }

        public static void Apply(string key, object prm, IDictionary values)
        {
            UelComponent c = Find(key);
            ParamMapper.Apply(prm, values);
            c.AfterApply(prm, values);
        }

        /// Trả về true nếu dựng thành công; created nhận mọi đối tượng đã tạo (kể cả khi thất bại giữa chừng).
        public static bool Build(string key, object prm, Model model, ModelObject primary, IList secondaries, IList created)
        {
            UelComponent c = Find(key);
            Logger.Log(string.Format("==== {0} (Core {1}) ====", key, BuildStamp()));
            try
            {
                return c.Build(model, prm, primary, secondaries ?? new ArrayList(), created ?? new ArrayList());
            }
            catch (Exception ex)
            {
                Logger.Log("Loi: " + ex);
                return false;
            }
        }
    }
}
