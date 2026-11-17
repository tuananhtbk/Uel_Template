// Ghi JSON tối giản (.NET 4.0 không có sẵn bộ ghi JSON tiện dùng): JObj = object giữ thứ tự khoá, IList = mảng.
// Số làm tròn 3 chữ số thập phân; mảng chỉ gồm số được ghi trên 1 dòng cho dễ đọc.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace UEL.Tester
{
    internal sealed class JObj : List<KeyValuePair<string, object>>
    {
        public JObj Set(string key, object value)
        {
            Add(new KeyValuePair<string, object>(key, value));
            return this;
        }
    }

    internal static class JsonOut
    {
        public static string Write(object root)
        {
            StringBuilder sb = new StringBuilder();
            Emit(sb, root, 0);
            sb.Append('\n');
            return sb.ToString();
        }

        public static string Num(double v)
        {
            if (double.IsNaN(v) || double.IsInfinity(v)) return "null";
            double r = Math.Round(v, 3);
            if (r == 0) r = 0;   // bỏ -0
            return r.ToString("0.###", CultureInfo.InvariantCulture);
        }

        private static void Indent(StringBuilder sb, int n)
        {
            sb.Append('\n').Append(' ', n * 2);
        }

        private static bool IsScalar(object v)
        {
            return v == null || v is string || v is bool || v is double || v is float || v is int || v is long || v is decimal || v is Enum;
        }

        private static void Emit(StringBuilder sb, object v, int level)
        {
            if (v == null) { sb.Append("null"); return; }
            string s = v as string;
            if (s != null) { Str(sb, s); return; }
            if (v is bool) { sb.Append((bool)v ? "true" : "false"); return; }
            if (v is double) { sb.Append(Num((double)v)); return; }
            if (v is float) { sb.Append(Num((float)v)); return; }
            if (v is int || v is long) { sb.Append(Convert.ToString(v, CultureInfo.InvariantCulture)); return; }
            if (v is Enum) { Str(sb, v.ToString()); return; }

            JObj o = v as JObj;
            if (o != null)
            {
                if (o.Count == 0) { sb.Append("{}"); return; }
                sb.Append('{');
                for (int i = 0; i < o.Count; i++)
                {
                    if (i > 0) sb.Append(',');
                    Indent(sb, level + 1);
                    Str(sb, o[i].Key);
                    sb.Append(": ");
                    Emit(sb, o[i].Value, level + 1);
                }
                Indent(sb, level);
                sb.Append('}');
                return;
            }

            IList list = v as IList;
            if (list != null)
            {
                if (list.Count == 0) { sb.Append("[]"); return; }
                bool flat = true;
                foreach (object x in list) if (!IsScalar(x)) { flat = false; break; }
                if (!flat)
                {
                    // mảng các điểm [x,y,z] cũng ghi gọn trên 1 dòng mỗi phần tử
                    bool allPts = true;
                    foreach (object x in list)
                    {
                        IList xl = x as IList;
                        if (xl == null || x is JObj) { allPts = false; break; }
                        foreach (object y in xl) if (!IsScalar(y)) { allPts = false; break; }
                        if (!allPts) break;
                    }
                    if (allPts && list.Count <= 64)
                    {
                        sb.Append('[');
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (i > 0) sb.Append(", ");
                            Emit(sb, list[i], level + 1);
                        }
                        sb.Append(']');
                        return;
                    }
                }
                sb.Append('[');
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0) sb.Append(flat ? ", " : ",");
                    if (!flat) Indent(sb, level + 1);
                    Emit(sb, list[i], level + 1);
                }
                if (!flat) Indent(sb, level);
                sb.Append(']');
                return;
            }
            Str(sb, Convert.ToString(v, CultureInfo.InvariantCulture));
        }

        private static void Str(StringBuilder sb, string s)
        {
            sb.Append('"');
            foreach (char c in s)
            {
                switch (c)
                {
                    case '"': sb.Append("\\\""); break;
                    case '\\': sb.Append("\\\\"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\r': sb.Append("\\r"); break;
                    case '\t': sb.Append("\\t"); break;
                    default:
                        if (c < 0x20) sb.Append("\\u").Append(((int)c).ToString("x4"));
                        else sb.Append(c);
                        break;
                }
            }
            sb.Append('"');
        }
    }
}
