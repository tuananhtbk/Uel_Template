// XUẤT COMPONENT RA JSON - dữ liệu để viết / kiểm tra plugin (chỉ ĐỌC model, không sửa gì).
// Mọi loại component: plugin, custom component, component hệ thống; dạng connection, detail, seam, part (Component).
//
// Nội dung 1 file:
//   component   : loại, số hiệu, tên, mọi thuộc tính (Class, Code, UpVector, PositionType, ...), toạ độ riêng
//   inputs      : cấu kiện / điểm đã chọn (primary, secondary, điểm tham chiếu, điểm đầu-cuối seam, input của Component)
//                 + đường cắt / fitting nằm trên cấu kiện đầu vào (đánh dấu cái nào do chính component này tạo)
//   frames      : hệ toạ độ của primary và secondary (để đổi toạ độ toàn cục -> cục bộ)
//   geometry    : trục, độ dốc, góc trên mặt bằng của cấu kiện đầu vào; góc + khoảng cách giữa primary và từng secondary
//   attributes  : giá trị mọi biến (P1...), kèm nhãn và kiểu lấy từ hộp thoại .inp; dialog_inp = nguyên văn .inp
//   children    : mọi đối tượng con (đệ quy cả component con):
//                 part (thuộc tính, điểm định nghĩa, contour + vát góc, trục, khối đặc đầy đủ các mặt, khối bao theo
//                 primary / secondary, các đường cắt trên part), bu lông, mối hàn, part cắt (boolean), fitting, cut plane...
// Toạ độ: hệ toàn cục của model (mm, độ); khối bao còn ghi thêm theo hệ primary và secondary.
// Thuộc tính đọc bằng reflection -> lấy được cả thuộc tính có ở bản Tekla mới mà không phải sửa code.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSMPlane = Tekla.Structures.Model.Plane;
using TsFace = Tekla.Structures.Solid.Face;
using TsLoop = Tekla.Structures.Solid.Loop;

namespace UEL.Tester
{
    internal static class ComponentExport
    {
        private const int MaxNestedDepth = 3;
        private const int MaxList = 5000;

        private sealed class Frame
        {
            public string Name;
            public double[] O, X, Y, Z;

            public double[] Local(Point p)
            {
                double dx = p.X - O[0], dy = p.Y - O[1], dz = p.Z - O[2];
                return new[] { dx * X[0] + dy * X[1] + dz * X[2], dx * Y[0] + dy * Y[1] + dz * Y[2], dx * Z[0] + dy * Z[1] + dz * Z[2] };
            }
        }

        private sealed class Ctx
        {
            public Model Model;
            public string ModelPath;
            public Action<string> Log;
            public List<Frame> Frames = new List<Frame>();
            public Dictionary<int, bool> Seen = new Dictionary<int, bool>();
            public int Parts, Bolts, Welds, Cuts, Others, Nested;
        }

        /// Xuất 1 component; trả về đường dẫn file JSON.
        public static string Run(Model model, BaseComponent comp, string outDir, Action<string> log)
        {
            Ctx c = new Ctx();
            c.Model = model;
            c.Log = log;
            try { c.ModelPath = model.GetInfo().ModelPath; } catch { }

            JObj root = new JObj();
            WorkPlaneHandler wp = model.GetWorkPlaneHandler();
            TransformationPlane saved = wp.GetCurrentTransformationPlane();
            try
            {
                wp.SetCurrentTransformationPlane(new TransformationPlane());
                comp.Select();
                root.Set("format", "UEL_COMPONENT_EXPORT 1");
                root.Set("exported", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
                JObj m = new JObj();
                try { m.Set("name", model.GetInfo().ModelName); } catch { }
                m.Set("path", c.ModelPath);
                m.Set("tekla", Program.InstanceInfo);
                m.Set("api_dir", Program.ApiDir);
                root.Set("model", m);
                root.Set("units", "mm, do; toa do toan cuc cua model (work plane = global)");
                root.Set("component", Component(comp, c, 0));
                JObj stats = new JObj();
                stats.Set("parts", c.Parts).Set("bolts", c.Bolts).Set("welds", c.Welds).Set("cuts_fittings", c.Cuts)
                     .Set("others", c.Others).Set("nested_components", c.Nested);
                root.Set("stats", stats);
            }
            finally
            {
                wp.SetCurrentTransformationPlane(saved);
            }

            Directory.CreateDirectory(outDir);
            string file = Path.Combine(outDir, DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + Clean(comp.Name) + "_" + comp.Identifier.ID + ".json");
            File.WriteAllText(file, JsonOut.Write(root), new UTF8Encoding(false));
            log(string.Format("Xuat {0} (id {1}): {2} part, {3} bu long, {4} han, {5} cat/fitting, {6} component con -> {7}",
                comp.Name, comp.Identifier.ID, c.Parts, c.Bolts, c.Welds, c.Cuts, c.Nested, file));
            return file;
        }

        private static string Clean(string s)
        {
            if (string.IsNullOrEmpty(s)) return "_";
            foreach (char ch in Path.GetInvalidFileNameChars()) s = s.Replace(ch, '_');
            return s.Replace(' ', '_');
        }

        // ---------------------------------------------------------------- component

        private static string KindOf(BaseComponent comp)
        {
            if (comp.Number == -100000) return "plugin";
            if (comp.Number == -1) return "custom";
            if (comp.Number > 0) return "system";
            return "other(" + comp.Number + ")";
        }

        private static JObj Component(BaseComponent comp, Ctx c, int depth)
        {
            c.Seen[comp.Identifier.ID] = true;
            JObj o = new JObj();
            o.Set("object", comp.GetType().Name);              // Connection | Detail | Seam | Component
            o.Set("kind", KindOf(comp));                       // plugin | custom | system
            o.Set("name", comp.Name);
            o.Set("number", comp.Number);
            o.Set("id", comp.Identifier.ID);
            o.Set("guid", Guid(comp.Identifier));
            o.Set("props", Props(comp, c));
            try { o.Set("coordinate_system", Cs(comp.GetCoordinateSystem())); } catch { }

            // ---- inputs
            List<ModelObject> inputParts = new List<ModelObject>();
            o.Set("inputs", Inputs(comp, c, inputParts));

            if (depth == 0)
            {
                // hệ toạ độ primary / secondary
                JObj frames = new JObj();
                for (int i = 0; i < inputParts.Count && i < 2; i++)
                {
                    Part p = inputParts[i] as Part;
                    if (p == null) continue;
                    Frame f = MakeFrame(i == 0 ? "primary" : "secondary", p);
                    if (f == null) continue;
                    c.Frames.Add(f);
                    frames.Set(f.Name, new JObj().Set("origin", L(f.O)).Set("x", L(f.X)).Set("y", L(f.Y)).Set("z", L(f.Z))
                                                 .Set("note", "he toa do cua part (GetCoordinateSystem); diem cuc bo = (p-origin).[x,y,z]"));
                }
                o.Set("frames", frames);
                o.Set("geometry", Geometry(inputParts));
            }

            // ---- input part chi tiết (sau khi có frames để ghi khối bao cục bộ)
            List<object> ip = new List<object>();
            for (int i = 0; i < inputParts.Count; i++)
            {
                Part p = inputParts[i] as Part;
                if (p == null) continue;
                c.Seen[p.Identifier.ID] = true;
                JObj pj = PartFull(p, c, true, comp.Identifier.ID);
                pj.Set("role", i == 0 ? "primary" : "secondary " + i);
                ip.Add(pj);
            }
            o.Set("input_parts", ip);

            // ---- biến
            o.Set("attributes", Attributes(comp, c, o));

            // ---- con
            List<object> kids = new List<object>();
            ModelObjectEnumerator e = null;
            try { e = comp.GetChildren(); } catch (Exception ex) { c.Log("GetChildren loi: " + ex.Message); }
            while (e != null && e.MoveNext())
            {
                ModelObject ch = e.Current;
                if (ch == null || c.Seen.ContainsKey(ch.Identifier.ID)) continue;
                try { ch.Select(); } catch { }
                BaseComponent sub = ch as BaseComponent;
                if (sub != null)
                {
                    c.Nested++;
                    if (depth < MaxNestedDepth) kids.Add(new JObj().Set("child", "component").Set("component", Component(sub, c, depth + 1)));
                    else kids.Add(Ref(sub));
                    continue;
                }
                c.Seen[ch.Identifier.ID] = true;
                kids.Add(Child(ch, c, comp.Identifier.ID));
            }
            o.Set("children", kids);
            return o;
        }

        // ---------------------------------------------------------------- inputs

        private static object Call(object target, string method, params object[] args)
        {
            foreach (MethodInfo mi in target.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (mi.Name != method || mi.GetParameters().Length != args.Length) continue;
                return mi.Invoke(target, args);
            }
            throw new MissingMethodException(target.GetType().Name, method);
        }

        private static List<object> Inputs(BaseComponent comp, Ctx c, List<ModelObject> parts)
        {
            List<object> r = new List<object>();
            // primary / secondaries: Connection, Detail, Seam
            try
            {
                ModelObject prim = Call(comp, "GetPrimaryObject") as ModelObject;
                if (prim != null)
                {
                    prim.Select();
                    parts.Add(prim);
                    r.Add(new JObj().Set("role", "primary").Set("object", Ref(prim)));
                }
            }
            catch (MissingMethodException) { }
            catch (Exception ex) { c.Log("GetPrimaryObject: " + Inner(ex)); }
            try
            {
                ArrayList secs = Call(comp, "GetSecondaryObjects") as ArrayList;
                if (secs != null)
                    for (int i = 0; i < secs.Count; i++)
                    {
                        ModelObject s = secs[i] as ModelObject;
                        if (s == null) continue;
                        s.Select();
                        parts.Add(s);
                        r.Add(new JObj().Set("role", "secondary " + (i + 1)).Set("object", Ref(s)));
                    }
            }
            catch (MissingMethodException) { }
            catch (Exception ex) { c.Log("GetSecondaryObjects: " + Inner(ex)); }

            // Detail: điểm tham chiếu
            foreach (string mname in new[] { "GetReferencePoint" })
            {
                try
                {
                    Point p = Call(comp, mname) as Point;
                    if (p != null) r.Add(new JObj().Set("role", "reference_point").Set("point", P(p)));
                }
                catch (MissingMethodException) { }
                catch (Exception ex) { c.Log(mname + ": " + Inner(ex)); }
            }

            // Seam: điểm đầu - cuối
            try
            {
                object[] args = { new Point(), new Point() };
                MethodInfo mi = comp.GetType().GetMethod("GetStartAndEndPositions");
                if (mi != null)
                {
                    mi.Invoke(comp, args);
                    r.Add(new JObj().Set("role", "seam_start").Set("point", P((Point)args[0])));
                    r.Add(new JObj().Set("role", "seam_end").Set("point", P((Point)args[1])));
                }
            }
            catch (Exception ex) { c.Log("GetStartAndEndPositions: " + Inner(ex)); }

            // Component (custom part / plugin dạng part): danh sách input
            try
            {
                object input = Call(comp, "GetComponentInput");
                IEnumerable items = input as IEnumerable;
                if (items != null)
                {
                    int k = 0;
                    foreach (object it in items)
                    {
                        k++;
                        JObj io = new JObj().Set("role", "input " + k);
                        try { io.Set("input_type", Convert.ToString(Call(it, "GetInputType"), CultureInfo.InvariantCulture)); } catch { }
                        object data = null;
                        try { data = Call(it, "GetData"); } catch { }
                        ModelObject mo = data as ModelObject;
                        if (mo != null) { mo.Select(); parts.Add(mo); }
                        IEnumerable col = data as IEnumerable;
                        if (col != null && !(data is string))
                            foreach (object x in col) { ModelObject xo = x as ModelObject; if (xo != null) { xo.Select(); parts.Add(xo); } }
                        io.Set("data", Dump(data, 1, c));
                        r.Add(io);
                    }
                }
            }
            catch (MissingMethodException) { }
            catch (Exception ex) { c.Log("GetComponentInput: " + Inner(ex)); }
            return r;
        }

        private static string Inner(Exception ex)
        {
            return ex is TargetInvocationException && ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }

        // ---------------------------------------------------------------- biến

        private static JObj Attributes(BaseComponent comp, Ctx c, JObj compJson)
        {
            JObj o = new JObj();
            List<AttrDef> defs = new List<AttrDef>();
            KeyValuePair<string, string>? inp = null;
            try { inp = AttrNames.FindInp(comp.Name, c.ModelPath, Program.ApiDir, c.Log); }
            catch (Exception ex) { c.Log("Tim .inp loi: " + ex.Message); }
            if (inp != null)
            {
                defs = AttrNames.FromInp(inp.Value.Value, inp.Value.Key);
                o.Set("dialog_source", inp.Value.Key);
            }
            else o.Set("dialog_source", null);

            List<object> values = new List<object>();
            List<object> notSet = new List<object>();
            Dictionary<string, bool> done = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            foreach (AttrDef d in defs)
            {
                done[d.Name] = true;
                string kind;
                object v = Read(comp, d.Name, d.Kind, out kind);
                JObj a = new JObj().Set("name", d.Name);
                if (!string.IsNullOrEmpty(d.Label)) a.Set("label", d.Label);
                a.Set("inp_type", d.InpType);
                if (d.Options != null && d.Options.Count > 0) a.Set("options", d.Options);
                if (v == null) { notSet.Add(a); continue; }
                a.Set("type", kind).Set("value", v);
                values.Add(a);
            }

            // dò P1..P300 (biến tính toán của custom component, biến không có trong hộp thoại)
            int last = 0;
            List<object> probed = new List<object>();
            for (int i = 1; i <= 300; i++)
            {
                if (i > 60 && i > last + 40) break;
                string name = "P" + i;
                if (done.ContainsKey(name)) { last = i; continue; }
                string kind;
                object v = Read(comp, name, "", out kind);
                if (v == null) continue;
                last = i;
                probed.Add(new JObj().Set("name", name).Set("type", kind).Set("value", v));
            }
            o.Set("values", values);
            o.Set("not_set_note", "Bien trong 'not_set' = o de trong trong hop thoai -> component dung gia tri MAC DINH trong dinh nghia " +
                                  "cua no (API khong doc duoc). Xem 'presets' (standard) va file .uel goc (uel_file).");
            o.Set("not_set", notSet);
            o.Set("probed_not_in_dialog", probed);

            // file .uel gốc + các preset (standard, KK350, ...)
            string uel = null;
            try { uel = AttrNames.FindUel(comp.Name, c.ModelPath, c.Log); } catch { }
            o.Set("uel_file", uel);
            List<object> presets = new List<object>();
            try
            {
                foreach (KeyValuePair<string, string> pr in AttrNames.Presets(comp.Name, c.ModelPath, uel))
                    presets.Add(new JObj().Set("source", pr.Key).Set("values", ParsePreset(pr.Value)));
            }
            catch (Exception ex) { c.Log("Doc preset loi: " + ex.Message); }
            o.Set("presets", presets);
            if (inp != null) o.Set("dialog_inp", inp.Value.Value);
            return o;
        }

        /// Dòng preset "joint_attributes.P19 25.000000" -> P19: "25.000000".
        private static JObj ParsePreset(string text)
        {
            JObj r = new JObj();
            foreach (string raw in text.Split('\n'))
            {
                string line = raw.Trim();
                if (line.Length == 0) continue;
                int sp = line.IndexOfAny(new[] { ' ', '\t' });
                string key = sp < 0 ? line : line.Substring(0, sp);
                string val = sp < 0 ? "" : line.Substring(sp + 1).Trim();
                int dot = key.IndexOf('.');
                if (dot > 0 && key.Substring(0, dot).EndsWith("_attributes", StringComparison.OrdinalIgnoreCase)) key = key.Substring(dot + 1);
                r.Set(key, val);
            }
            return r;
        }

        /// GetAttribute theo kiểu đã biết, không có thì thử int -> double -> string.
        private static object Read(BaseComponent comp, string name, string kind, out string got)
        {
            List<string> order = new List<string>();
            if (!string.IsNullOrEmpty(kind)) order.Add(kind);
            foreach (string k in new[] { "int", "double", "string" }) if (!order.Contains(k)) order.Add(k);
            foreach (string k in order)
            {
                try
                {
                    if (k == "int") { int v = 0; if (comp.GetAttribute(name, ref v) && v != int.MinValue) { got = k; return v; } }
                    else if (k == "double") { double v = 0; if (comp.GetAttribute(name, ref v) && v > -2.1e9) { got = k; return v; } }
                    else { string v = ""; if (comp.GetAttribute(name, ref v)) { got = k; return v; } }
                }
                catch { }
            }
            got = null;
            return null;
        }

        // ---------------------------------------------------------------- đối tượng con

        private static JObj Child(ModelObject o, Ctx c, int compId)
        {
            Part p = o as Part;
            if (p != null)
            {
                c.Parts++;
                return PartFull(p, c, true, compId);
            }
            BoltGroup b = o as BoltGroup;
            if (b != null)
            {
                c.Bolts++;
                JObj j = Header(o, "bolt").Set("props", Props(o, c));
                j.Set("dist_x", Dists(o, "X")).Set("dist_y", Dists(o, "Y"));
                try { j.Set("coordinate_system", Cs(o.GetCoordinateSystem())); } catch { }
                try { j.Set("solid", SolidJson(Call(b, "GetSolid") as Solid, c, false)); } catch { }
                return j;
            }
            BaseWeld w = o as BaseWeld;
            if (w != null)
            {
                c.Welds++;
                JObj j = Header(o, "weld").Set("props", Props(o, c));
                try { j.Set("coordinate_system", Cs(o.GetCoordinateSystem())); } catch { }
                return j;
            }
            if (o is BooleanPart || o is Fitting || o is CutPlane)
            {
                c.Cuts++;
                return CutJson(o, c, compId);
            }
            if (o.GetType().Name == "ControlPoint")
            {
                c.Others++;
                return Header(o, "control_point").Set("props", Props(o, c));
            }
            c.Others++;
            return Header(o, "other").Set("props", Props(o, c));
        }

        /// Khoảng cách bu lông của BoltArray (GetBoltDistX(i) ...), gọi qua reflection.
        private static List<object> Dists(ModelObject b, string axis)
        {
            List<object> r = new List<object>();
            try
            {
                int n = Convert.ToInt32(Call(b, "GetBoltDist" + axis + "Count"));
                for (int i = 0; i < n; i++) r.Add(Convert.ToDouble(Call(b, "GetBoltDist" + axis, i)));
            }
            catch { }
            return r;
        }

        private static JObj Header(ModelObject o, string what)
        {
            return new JObj().Set("child", what).Set("object", o.GetType().Name).Set("id", o.Identifier.ID).Set("guid", Guid(o.Identifier));
        }

        private static JObj PartFull(Part p, Ctx c, bool withCuts, int compId)
        {
            JObj o = Header(p, "part");
            o.Set("props", Props(p, c));
            o.Set("report", Report(p));
            try { o.Set("coordinate_system", Cs(p.GetCoordinateSystem())); } catch { }
            try { o.Set("center_line", Pts(p.GetCenterLine(false))); } catch { }
            try { o.Set("reference_line", Pts(Call(p, "GetReferenceLine", false) as ArrayList)); } catch { }
            try { o.Set("solid", SolidJson(p.GetSolid(), c, true)); } catch (Exception ex) { o.Set("solid_error", ex.Message); }
            try
            {
                BaseComponent fc = p.GetFatherComponent();
                if (fc != null) o.Set("father_component", new JObj().Set("id", fc.Identifier.ID).Set("name", fc.Name));
            }
            catch { }
            try
            {
                Tekla.Structures.Model.Assembly a = p.GetAssembly();
                if (a != null)
                {
                    a.Select();
                    ModelObject main = a.GetMainPart();
                    o.Set("assembly", new JObj().Set("id", a.Identifier.ID).Set("main_part", main == null ? (object)null : main.Identifier.ID)
                                                .Set("is_main_part", main != null && main.Identifier.ID == p.Identifier.ID));
                }
            }
            catch { }
            try
            {
                Hashtable ht = new Hashtable();
                object[] args = { ht };
                MethodInfo mi = p.GetType().GetMethod("GetAllUserProperties");
                if (mi != null)
                {
                    mi.Invoke(p, args);
                    Hashtable res = args[0] as Hashtable;
                    if (res != null && res.Count > 0)
                    {
                        JObj up = new JObj();
                        List<string> keys = new List<string>();
                        foreach (object k in res.Keys) keys.Add(Convert.ToString(k));
                        keys.Sort(StringComparer.Ordinal);
                        foreach (string k in keys) up.Set(k, Dump(res[k], 1, c));
                        o.Set("user_properties", up);
                    }
                }
            }
            catch { }
            try { o.Set("bolts", Ids(Call(p, "GetBolts") as ModelObjectEnumerator)); } catch { }
            try { o.Set("welds", Ids(Call(p, "GetWelds") as ModelObjectEnumerator)); } catch { }
            if (withCuts)
            {
                List<object> cuts = new List<object>();
                try
                {
                    ModelObjectEnumerator e = p.GetBooleans();
                    while (e != null && e.MoveNext())
                    {
                        ModelObject b = e.Current;
                        if (b == null) continue;
                        if (c.Seen.ContainsKey(b.Identifier.ID)) { cuts.Add(new JObj().Set("ref", b.Identifier.ID).Set("object", b.GetType().Name)); continue; }
                        try { b.Select(); } catch { }
                        c.Seen[b.Identifier.ID] = true;
                        c.Cuts++;
                        cuts.Add(CutJson(b, c, compId));
                    }
                }
                catch { }
                o.Set("cuts_on_this_part", cuts);
            }
            return o;
        }

        /// Part cắt (boolean), fitting, cut plane: thuộc tính + khối part cắt + component đã tạo nó.
        private static JObj CutJson(ModelObject b, Ctx c, int compId)
        {
            JObj o = Header(b, "cut");
            o.Set("props", Props(b, c));
            try
            {
                BaseComponent fc = b.GetFatherComponent();
                o.Set("made_by_component", fc == null ? (object)null : new JObj().Set("id", fc.Identifier.ID).Set("name", fc.Name)
                                                                                 .Set("is_this_component", fc.Identifier.ID == compId));
            }
            catch { }
            BooleanPart bp = b as BooleanPart;
            if (bp != null && bp.OperativePart != null)
            {
                Part op = bp.OperativePart;
                try { op.Select(); } catch { }
                o.Set("operative_part", PartFull(op, c, false, compId));
            }
            TSMPlane pl = null;
            Fitting f = b as Fitting;
            if (f != null) pl = f.Plane;
            CutPlane cp = b as CutPlane;
            if (cp != null) pl = cp.Plane;
            if (pl != null)
            {
                double[] n = Norm(Cross(V(pl.AxisX), V(pl.AxisY)));
                JObj pj = new JObj().Set("origin", P(pl.Origin)).Set("axis_x", P(pl.AxisX)).Set("axis_y", P(pl.AxisY)).Set("normal", L(n));
                foreach (Frame fr in c.Frames)
                    pj.Set("in_" + fr.Name, new JObj().Set("origin", L(fr.Local(pl.Origin))).Set("normal", L(Dir(fr, n))));
                o.Set("plane", pj);
            }
            return o;
        }

        private static List<object> Ids(ModelObjectEnumerator e)
        {
            List<object> r = new List<object>();
            while (e != null && e.MoveNext()) if (e.Current != null) r.Add(e.Current.Identifier.ID);
            return r;
        }

        private static readonly string[] ReportDouble =
        {
            "LENGTH", "WIDTH", "HEIGHT", "VOLUME", "WEIGHT", "AREA",
            "PROFILE.HEIGHT", "PROFILE.WIDTH", "PROFILE.WEB_THICKNESS", "PROFILE.FLANGE_THICKNESS",
            "PROFILE.FLANGE_WIDTH", "PROFILE.ROUNDING_RADIUS_1", "PROFILE.ROUNDING_RADIUS"
        };

        private static readonly string[] ReportString = { "PROFILE_TYPE", "PROFILE", "MATERIAL", "ASSEMBLY_POS", "PART_POS", "NAME", "CLASS" };

        private static JObj Report(Part p)
        {
            JObj o = new JObj();
            foreach (string k in ReportDouble)
            {
                double v = 0;
                try { if (p.GetReportProperty(k, ref v)) o.Set(k, v); } catch { }
            }
            foreach (string k in ReportString)
            {
                string v = "";
                try { if (p.GetReportProperty(k, ref v)) o.Set(k, v); } catch { }
            }
            return o;
        }

        // ---------------------------------------------------------------- khối đặc

        private static JObj SolidJson(Solid s, Ctx c, bool faces)
        {
            if (s == null) return null;
            JObj o = new JObj();
            o.Set("min", P(s.MinimumPoint)).Set("max", P(s.MaximumPoint));
            List<Point> all = new List<Point>();
            List<object> fl = new List<object>();
            try
            {
                // Current đọc qua Cur(): Tekla 18 trả về object, 2019+ trả về kiểu riêng -> gọi thẳng sẽ lỗi ở bản khác
                Tekla.Structures.Solid.FaceEnumerator fe = s.GetFaceEnumerator();
                while (fe.MoveNext())
                {
                    TsFace f = Cur(fe) as TsFace;
                    if (f == null) continue;
                    JObj fj = new JObj();
                    fj.Set("normal", P(f.Normal));
                    List<object> loops = new List<object>();
                    Tekla.Structures.Solid.LoopEnumerator le = f.GetLoopEnumerator();
                    while (le.MoveNext())
                    {
                        TsLoop l = Cur(le) as TsLoop;
                        if (l == null) continue;
                        List<object> lp = new List<object>();
                        Tekla.Structures.Solid.VertexEnumerator ve = l.GetVertexEnumerator();
                        while (ve.MoveNext())
                        {
                            Point q = Cur(ve) as Point;
                            if (q == null) continue;
                            lp.Add(P(q));
                            all.Add(q);
                        }
                        loops.Add(lp);
                    }
                    fj.Set("loops", loops);
                    fl.Add(fj);
                }
            }
            catch (Exception ex) { o.Set("faces_error", ex.Message); }
            o.Set("face_count", fl.Count);
            if (faces) o.Set("faces", fl);

            // khối bao theo hệ primary / secondary (từ mọi đỉnh của khối đặc)
            foreach (Frame fr in c.Frames)
            {
                if (all.Count == 0) break;
                double[] mn = { double.MaxValue, double.MaxValue, double.MaxValue };
                double[] mx = { double.MinValue, double.MinValue, double.MinValue };
                foreach (Point q in all)
                {
                    double[] l = fr.Local(q);
                    for (int k = 0; k < 3; k++) { mn[k] = Math.Min(mn[k], l[k]); mx[k] = Math.Max(mx[k], l[k]); }
                }
                o.Set("bbox_in_" + fr.Name, new JObj().Set("min", L(mn)).Set("max", L(mx)));
            }
            return o;
        }

        /// Phần tử hiện tại của bộ duyệt, không phụ thuộc chữ ký get_Current của từng bản Tekla.
        private static object Cur(object enumerator)
        {
            IEnumerator ie = enumerator as IEnumerator;
            if (ie != null) return ie.Current;
            PropertyInfo pi = enumerator.GetType().GetProperty("Current");
            return pi == null ? null : pi.GetValue(enumerator, null);
        }

        // ---------------------------------------------------------------- hình học đầu vào

        private static Frame MakeFrame(string name, Part p)
        {
            try
            {
                CoordinateSystem cs = p.GetCoordinateSystem();
                Frame f = new Frame();
                f.Name = name;
                f.O = V(cs.Origin);
                f.X = Norm(V(cs.AxisX));
                f.Y = Norm(V(cs.AxisY));
                f.Z = Norm(Cross(f.X, f.Y));
                f.Y = Norm(Cross(f.Z, f.X));
                return f;
            }
            catch
            {
                return null;
            }
        }

        private static JObj Geometry(List<ModelObject> inputs)
        {
            JObj o = new JObj();
            List<object> axes = new List<object>();
            List<double[][]> lines = new List<double[][]>();
            for (int i = 0; i < inputs.Count; i++)
            {
                Part p = inputs[i] as Part;
                if (p == null) { lines.Add(null); continue; }
                ArrayList cl = null;
                try { cl = p.GetCenterLine(false); } catch { }
                if (cl == null || cl.Count < 2) { lines.Add(null); continue; }
                double[] a = V((Point)cl[0]), b = V((Point)cl[cl.Count - 1]);
                double[] d = Sub(b, a);
                double len = Len(d);
                double[] u = Norm(d);
                lines.Add(new[] { a, u });
                axes.Add(new JObj().Set("role", i == 0 ? "primary" : "secondary " + i).Set("start", L(a)).Set("end", L(b))
                    .Set("dir", L(u)).Set("length", len)
                    .Set("slope_deg", Math.Asin(Math.Max(-1, Math.Min(1, u[2]))) * 180 / Math.PI)
                    .Set("plan_deg", Math.Atan2(u[1], u[0]) * 180 / Math.PI));
            }
            o.Set("axes", axes);
            List<object> pairs = new List<object>();
            if (lines.Count > 1 && lines[0] != null)
                for (int i = 1; i < lines.Count; i++)
                {
                    if (lines[i] == null) continue;
                    double[] p1 = lines[0][0], d1 = lines[0][1], p2 = lines[i][0], d2 = lines[i][1];
                    double cosA = Math.Abs(Dot(d1, d2));
                    double[] w0 = Sub(p1, p2);
                    double b = Dot(d1, d2), dd = Dot(d1, w0), e = Dot(d2, w0);
                    double den = 1 - b * b;
                    double s = den < 1e-9 ? 0 : (b * e - dd) / den;
                    double t = den < 1e-9 ? e : (e - b * dd) / den;
                    double[] q1 = Add(p1, Mul(d1, s)), q2 = Add(p2, Mul(d2, t));
                    pairs.Add(new JObj().Set("between", "primary - secondary " + i)
                        .Set("angle_deg", Math.Acos(Math.Min(1, cosA)) * 180 / Math.PI)
                        .Set("closest_on_primary", L(q1)).Set("closest_on_secondary", L(q2)).Set("axis_distance", Len(Sub(q1, q2))));
                }
            o.Set("pairs", pairs);
            return o;
        }

        // ---------------------------------------------------------------- reflection

        private static readonly Dictionary<string, bool> SkipProp = new Dictionary<string, bool>
        {
            { "Identifier", true }, { "IsUpToDate", true }
        };

        private static JObj Props(object o, Ctx c)
        {
            JObj r = new JObj();
            foreach (PropertyInfo pi in o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!pi.CanRead || pi.GetIndexParameters().Length > 0 || SkipProp.ContainsKey(pi.Name)) continue;
                object v;
                try { v = pi.GetValue(o, null); }
                catch { continue; }
                r.Set(pi.Name, Dump(v, 1, c));
            }
            return r;
        }

        private static object Dump(object v, int depth, Ctx c)
        {
            if (v == null) return null;
            if (v is string || v is bool || v is int || v is long || v is double) return v;
            if (v is float) return (double)(float)v;
            if (v is short || v is byte || v is uint || v is ulong || v is decimal) return Convert.ToDouble(v, CultureInfo.InvariantCulture);
            if (v is Enum) return v.ToString();
            if (v is DateTime) return ((DateTime)v).ToString("s", CultureInfo.InvariantCulture);
            Tekla.Structures.Identifier id = v as Tekla.Structures.Identifier;
            if (id != null) return new JObj().Set("id", id.ID).Set("guid", Guid(id));
            Point pt = v as Point;
            if (pt != null)
            {
                Type t = v.GetType();
                if (t == typeof(Point) || t == typeof(Vector)) return P(pt);
                JObj po = new JObj().Set("xyz", P(pt));
                foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    if (!pi.CanRead || pi.GetIndexParameters().Length > 0) continue;
                    try { po.Set(pi.Name, Dump(pi.GetValue(v, null), depth + 1, c)); } catch { }
                }
                return po;
            }
            ModelObject mo = v as ModelObject;
            if (mo != null) return Ref(mo);
            IEnumerable en = v as IEnumerable;
            if (en != null)
            {
                List<object> l = new List<object>();
                foreach (object x in en)
                {
                    if (l.Count >= MaxList) { l.Add("..."); break; }
                    l.Add(Dump(x, depth + 1, c));
                }
                return l;
            }
            Type ty = v.GetType();
            if (ty.Namespace != null && ty.Namespace.StartsWith("Tekla.Structures") && depth < 5)
            {
                JObj r = new JObj();
                foreach (PropertyInfo pi in ty.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!pi.CanRead || pi.GetIndexParameters().Length > 0) continue;
                    try { r.Set(pi.Name, Dump(pi.GetValue(v, null), depth + 1, c)); } catch { }
                }
                // trường public (vd. Offset.Dx/Dy/Dz)
                foreach (FieldInfo fi in ty.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    try { r.Set(fi.Name, Dump(fi.GetValue(v), depth + 1, c)); } catch { }
                }
                return r;
            }
            return Convert.ToString(v, CultureInfo.InvariantCulture);
        }

        private static JObj Ref(ModelObject mo)
        {
            JObj r = new JObj().Set("ref", mo.Identifier.ID).Set("object", mo.GetType().Name);
            Part p = mo as Part;
            if (p != null)
            {
                try { r.Set("name", p.Name).Set("profile", p.Profile.ProfileString); } catch { }
            }
            BaseComponent bc = mo as BaseComponent;
            if (bc != null) r.Set("name", bc.Name);
            return r;
        }

        private static string Guid(Tekla.Structures.Identifier id)
        {
            try
            {
                PropertyInfo pi = id.GetType().GetProperty("GUID");
                object g = pi == null ? null : pi.GetValue(id, null);
                return g == null ? null : g.ToString();
            }
            catch
            {
                return null;
            }
        }

        // ---------------------------------------------------------------- tiện ích hình học

        private static List<object> P(Point p)
        {
            return p == null ? null : new List<object> { p.X, p.Y, p.Z };
        }

        private static List<object> L(double[] a)
        {
            return new List<object> { a[0], a[1], a[2] };
        }

        private static List<object> Pts(ArrayList pts)
        {
            if (pts == null) return null;
            List<object> r = new List<object>();
            foreach (object o in pts) { Point p = o as Point; if (p != null) r.Add(P(p)); }
            return r;
        }

        private static JObj Cs(CoordinateSystem cs)
        {
            if (cs == null) return null;
            return new JObj().Set("origin", P(cs.Origin)).Set("axis_x", P(cs.AxisX)).Set("axis_y", P(cs.AxisY));
        }

        private static double[] V(Point p) { return new[] { p.X, p.Y, p.Z }; }
        private static double[] Sub(double[] a, double[] b) { return new[] { a[0] - b[0], a[1] - b[1], a[2] - b[2] }; }
        private static double[] Add(double[] a, double[] b) { return new[] { a[0] + b[0], a[1] + b[1], a[2] + b[2] }; }
        private static double[] Mul(double[] a, double k) { return new[] { a[0] * k, a[1] * k, a[2] * k }; }
        private static double Dot(double[] a, double[] b) { return a[0] * b[0] + a[1] * b[1] + a[2] * b[2]; }
        private static double Len(double[] a) { return Math.Sqrt(Dot(a, a)); }
        private static double[] Cross(double[] a, double[] b) { return new[] { a[1] * b[2] - a[2] * b[1], a[2] * b[0] - a[0] * b[2], a[0] * b[1] - a[1] * b[0] }; }

        private static double[] Norm(double[] a)
        {
            double l = Len(a);
            return l < 1e-12 ? a : Mul(a, 1.0 / l);
        }

        private static double[] Dir(Frame f, double[] d)
        {
            return new[] { Dot(d, f.X), Dot(d, f.Y), Dot(d, f.Z) };
        }
    }
}
