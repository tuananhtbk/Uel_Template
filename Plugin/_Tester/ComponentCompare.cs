// So sánh 2 component đang có trong model, thường là component gốc (.uel, vd. 0-RAKER+TG) và bản plugin (vd. RAKER+T_GIRDER_DEV).
// Hai component đặt trên 2 cặp cấu kiện giống hệt nhau (copy sang chỗ khác) hoặc trên cùng 1 cặp.
// Mọi toạ độ được đọc trong hệ toạ độ CẤU KIỆN CHÍNH của từng component, nên cặp copy (dời / xoay) vẫn so được.
//
// Đối tượng được so: part con, bu lông, mối hàn, đường cắt (boolean), fitting, cut plane, component con,
// và chính các cấu kiện đầu vào (thể tích + kích thước bao -> thấy được đường cắt / fitting lên dầm).
// Ghép từng đối tượng A với đối tượng B cùng loại gần nhất, rồi chia: khớp / lệch / chỉ có ở A / chỉ có ở B.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace UEL.Tester
{
    internal sealed class CmpNum
    {
        public string Label;
        public double Value;
        public double Tol;
    }

    /// Một đối tượng đã đọc: thuộc tính dạng chữ (phải giống hệt) + số (so theo dung sai).
    internal sealed class CmpItem
    {
        public string Group;          // loại để ghép: "Part", "Bu long", "Han", "Cat", ... hoặc "Dau vao: chinh"
        public string Title;
        public bool AlwaysPair;       // cấu kiện đầu vào: luôn ghép với vai trò tương ứng bên kia
        public ModelObject Obj;
        public int Id;
        public double CX, CY, CZ;     // tâm, dùng để ghép
        public readonly List<KeyValuePair<string, string>> Attrs = new List<KeyValuePair<string, string>>();
        public readonly List<CmpNum> Nums = new List<CmpNum>();

        public void Attr(string k, string v) { Attrs.Add(new KeyValuePair<string, string>(k, v ?? "")); }

        public void Num(string k, double v, double tol)
        {
            CmpNum n = new CmpNum();
            n.Label = k;
            n.Value = v;
            n.Tol = tol;
            Nums.Add(n);
        }

        public void Box(Point mn, Point mx, double tol)
        {
            Num("X min", mn.X, tol); Num("Y min", mn.Y, tol); Num("Z min", mn.Z, tol);
            Num("X max", mx.X, tol); Num("Y max", mx.Y, tol); Num("Z max", mx.Z, tol);
            SetCenter((mn.X + mx.X) / 2, (mn.Y + mx.Y) / 2, (mn.Z + mx.Z) / 2);
        }

        public void SetCenter(double x, double y, double z) { CX = x; CY = y; CZ = z; }

        public string Get(string k)
        {
            foreach (KeyValuePair<string, string> kv in Attrs) if (kv.Key == k) return kv.Value;
            return null;
        }

        public string CenterText
        {
            get { return "(" + ComponentCompare.F(CX) + ", " + ComponentCompare.F(CY) + ", " + ComponentCompare.F(CZ) + ")"; }
        }
    }

    internal sealed class CmpPair
    {
        public CmpItem A, B;
        public List<string> Diffs;
    }

    internal sealed class CmpResult
    {
        public string NameA, NameB;
        public int IdA, IdB;
        public List<CmpItem> ItemsA, ItemsB;
        public readonly List<CmpPair> Same = new List<CmpPair>();
        public readonly List<CmpPair> Diff = new List<CmpPair>();
        public readonly List<CmpItem> OnlyA = new List<CmpItem>();
        public readonly List<CmpItem> OnlyB = new List<CmpItem>();
        public readonly List<string> Notes = new List<string>();
        public string Report;
    }

    internal static class ComponentCompare
    {
        /// Dung sai toạ độ (mm).
        public static double Tol = 1.0;
        /// Hai đối tượng có thuộc tính khác nhau chỉ được ghép khi tâm cách nhau không quá khoảng này (mm).
        public const double PairMax = 300.0;
        /// Mỗi thuộc tính khác nhau tính như lệch thêm 100 mm khi chọn cặp ghép.
        private const double AttrWeight = 100.0;
        private const string OperativeClass = "BlOpCl";
        private const int MaxDiffLines = 15;

        public static string F(double v)
        {
            if (Math.Abs(v) < 0.005) v = 0;
            return v.ToString("0.##", CultureInfo.InvariantCulture);
        }

        public static CmpResult Run(Model model, BaseComponent a, BaseComponent b)
        {
            CmpResult r = new CmpResult();
            r.NameA = a.Name;
            r.NameB = b.Name;
            r.IdA = a.Identifier.ID;
            r.IdB = b.Identifier.ID;
            List<int> inA = new List<int>();
            List<int> inB = new List<int>();
            r.ItemsA = Collect(model, a, inA, r.Notes, "A");
            r.ItemsB = Collect(model, b, inB, r.Notes, "B");
            foreach (int id in inA)
            {
                if (!inB.Contains(id)) continue;
                r.Notes.Add("A va B dung chung cau kien dau vao (id " + id + "): duong cat / fitting tren cau kien dau vao " +
                            "thuoc ca 2 component nen KHONG so duoc. Nen dat 2 component tren 2 cap dam copy rieng.");
                break;
            }
            Match(r);
            r.Report = BuildReport(r);
            return r;
        }

        // ---------------------------------------------------------------- đọc

        private static List<CmpItem> Collect(Model model, BaseComponent comp, List<int> inputIds, List<string> notes, string tag)
        {
            List<CmpItem> items = new List<CmpItem>();
            Dictionary<int, bool> seen = new Dictionary<int, bool>();
            WorkPlaneHandler wp = model.GetWorkPlaneHandler();
            TransformationPlane saved = wp.GetCurrentTransformationPlane();
            try
            {
                wp.SetCurrentTransformationPlane(new TransformationPlane());
                comp.Select();
                List<ModelObject> inputs = Inputs(comp);
                Part prim = inputs.Count > 0 ? inputs[0] as Part : null;
                if (prim != null && prim.Select())
                    wp.SetCurrentTransformationPlane(new TransformationPlane(prim.GetCoordinateSystem()));
                else
                    notes.Add(tag + ": khong doc duoc cau kien chinh -> so theo toa do tong the (2 component phai dat cung cho).");

                List<Part> parts = new List<Part>();
                for (int i = 0; i < inputs.Count; i++)
                {
                    ModelObject o = inputs[i];
                    if (o == null) continue;
                    o.Select();
                    inputIds.Add(o.Identifier.ID);
                    seen[o.Identifier.ID] = true;
                    Part p = o as Part;
                    if (p == null) continue;
                    CmpItem it = PartItem(p, i == 0 ? "Dau vao: chinh" : "Dau vao: phu " + i);
                    it.AlwaysPair = true;
                    items.Add(it);
                    parts.Add(p);
                }

                int before = items.Count;
                Walk(comp.GetChildren(), items, seen, parts, 0);
                if (items.Count == before)
                    notes.Add(tag + " (" + comp.Name + "): khong co doi tuong con nao - component chua dung duoc hoac bi loi.");

                // đường cắt / fitting nằm trên part (cả cấu kiện đầu vào) mà GetChildren không trả về
                foreach (Part p in parts)
                {
                    ModelObjectEnumerator e = p.GetBooleans();
                    while (e != null && e.MoveNext())
                    {
                        ModelObject o = e.Current;
                        if (o == null || seen.ContainsKey(o.Identifier.ID)) continue;
                        seen[o.Identifier.ID] = true;
                        o.Select();
                        CmpItem it = Describe(o);
                        if (it != null) items.Add(it);
                    }
                }
            }
            finally
            {
                wp.SetCurrentTransformationPlane(saved);
            }
            return items;
        }

        private static List<ModelObject> Inputs(BaseComponent comp)
        {
            List<ModelObject> r = new List<ModelObject>();
            Connection c = comp as Connection;
            if (c != null)
            {
                r.Add(c.GetPrimaryObject());
                AddAll(r, c.GetSecondaryObjects());
                return r;
            }
            Seam s = comp as Seam;
            if (s != null)
            {
                r.Add(s.GetPrimaryObject());
                AddAll(r, s.GetSecondaryObjects());
                return r;
            }
            Detail d = comp as Detail;
            if (d != null) r.Add(d.GetPrimaryObject());
            return r;
        }

        private static void AddAll(List<ModelObject> r, ArrayList list)
        {
            if (list == null) return;
            foreach (object o in list) r.Add(o as ModelObject);
        }

        private static void Walk(ModelObjectEnumerator e, List<CmpItem> items, Dictionary<int, bool> seen, List<Part> parts, int depth)
        {
            while (e != null && e.MoveNext())
            {
                ModelObject o = e.Current;
                if (o == null || seen.ContainsKey(o.Identifier.ID)) continue;
                seen[o.Identifier.ID] = true;
                try { o.Select(); } catch { }

                BaseComponent sub = o as BaseComponent;
                if (sub != null)
                {
                    CmpItem it = New("Component con", o);
                    it.Title = sub.Name;
                    it.Attr("Name", sub.Name);
                    items.Add(it);
                    if (depth < 5) Walk(sub.GetChildren(), items, seen, parts, depth + 1);
                    continue;
                }
                CmpItem x = Describe(o);
                if (x == null) continue;
                items.Add(x);
                Part p = o as Part;
                if (p != null) parts.Add(p);
            }
        }

        private static CmpItem New(string group, ModelObject o)
        {
            CmpItem it = new CmpItem();
            it.Group = group;
            it.Obj = o;
            it.Id = o.Identifier.ID;
            return it;
        }

        private static CmpItem Describe(ModelObject o)
        {
            try
            {
                Part p = o as Part;
                if (p != null) return p.Class == OperativeClass ? null : PartItem(p, "Part");
                BoltGroup bg = o as BoltGroup;
                if (bg != null) return BoltItem(bg);
                BaseWeld w = o as BaseWeld;
                if (w != null) return WeldItem(w);
                BooleanPart bp = o as BooleanPart;
                if (bp != null) return BoolItem(bp);
                Fitting f = o as Fitting;
                if (f != null) return PlaneItem("Fitting", o, f.Plane, f.Father, true);
                CutPlane cp = o as CutPlane;
                if (cp != null) return PlaneItem("Cut plane", o, cp.Plane, cp.Father, false);

                CmpItem it = New("Khac", o);
                it.Title = o.GetType().Name;
                it.Attr("Loai", o.GetType().Name);
                return it;
            }
            catch (Exception ex)
            {
                CmpItem it = New("Loi doc", o);
                it.Title = o.GetType().Name + ": " + ex.Message;
                return it;
            }
        }

        private static CmpItem PartItem(Part p, string group)
        {
            CmpItem it = New(group, p);
            string prof = p.Profile.ProfileString;
            string mat = p.Material.MaterialString;
            it.Title = (p.Name + " " + prof + " " + mat).Trim();
            it.Attr("Loai", p.GetType().Name);
            it.Attr("Name", p.Name);
            it.Attr("Profile", prof);
            it.Attr("Material", mat);
            it.Attr("Class", p.Class);
            it.Attr("Finish", p.Finish);
            it.Attr("Part pos.", p.PartNumber.Prefix + "/" + p.PartNumber.StartNumber);
            it.Attr("Ass. pos.", p.AssemblyNumber.Prefix + "/" + p.AssemblyNumber.StartNumber);
            BoxOf(it, p);
            double v = 0;
            if (p.GetReportProperty("VOLUME", ref v))
            {
                double cm3 = v / 1000.0;
                it.Num("The tich cm3", cm3, Math.Max(0.01 * Math.Abs(cm3), 1.0));
            }
            return it;
        }

        private static bool BoxOf(CmpItem it, Part p)
        {
            Solid s = p.GetSolid();
            if (s == null) return false;
            it.Box(s.MinimumPoint, s.MaximumPoint, Tol);
            return true;
        }

        private static Point CenterOf(ModelObject o)
        {
            Part p = o as Part;
            if (p == null) return null;
            try
            {
                p.Select();
                Solid s = p.GetSolid();
                if (s == null) return null;
                return new Point((s.MinimumPoint.X + s.MaximumPoint.X) / 2, (s.MinimumPoint.Y + s.MaximumPoint.Y) / 2,
                                 (s.MinimumPoint.Z + s.MaximumPoint.Z) / 2);
            }
            catch
            {
                return null;
            }
        }

        private static string Desc(ModelObject o)
        {
            Part p = o as Part;
            if (p == null) return o == null ? "-" : o.GetType().Name;
            try { p.Select(); } catch { }
            return (p.Name + " " + p.Profile.ProfileString).Trim();
        }

        private static void PointNums(CmpItem it, string label, Point c)
        {
            if (c == null) return;
            it.Num(label + " X", c.X, Tol);
            it.Num(label + " Y", c.Y, Tol);
            it.Num(label + " Z", c.Z, Tol);
        }

        private static int ComparePts(Point a, Point b)
        {
            int c = Math.Round(a.X, 1).CompareTo(Math.Round(b.X, 1));
            if (c == 0) c = Math.Round(a.Y, 1).CompareTo(Math.Round(b.Y, 1));
            if (c == 0) c = Math.Round(a.Z, 1).CompareTo(Math.Round(b.Z, 1));
            return c;
        }

        private static CmpItem BoltItem(BoltGroup b)
        {
            CmpItem it = New("Bu long", b);
            List<Point> pts = new List<Point>();
            if (b.BoltPositions != null)
                foreach (object o in b.BoltPositions)
                {
                    Point p = o as Point;
                    if (p != null) pts.Add(new Point(p.X, p.Y, p.Z));
                }
            pts.Sort(ComparePts);

            it.Title = b.BoltStandard + " M" + F(b.BoltSize) + " x " + pts.Count + " (" + Desc(b.PartToBoltTo) + " / " + Desc(b.PartToBeBolted) + ")";
            it.Attr("Loai", b.GetType().Name);
            it.Attr("Standard", b.BoltStandard);
            it.Attr("Size", F(b.BoltSize));
            it.Attr("So bu long", pts.Count.ToString(CultureInfo.InvariantCulture));
            it.Attr("Co bu long", b.Bolt ? "co" : "chi lo");
            it.Attr("Dung sai lo", F(b.Tolerance));
            it.Attr("Lo oval X", F(b.SlottedHoleX));
            it.Attr("Lo oval Y", F(b.SlottedHoleY));
            it.Attr("Cut length", F(b.CutLength));
            it.Attr("Bat vao", Desc(b.PartToBoltTo));
            it.Attr("Bat", Desc(b.PartToBeBolted));

            double sx = 0, sy = 0, sz = 0;
            for (int i = 0; i < pts.Count; i++)
            {
                PointNums(it, "BL" + (i + 1), pts[i]);
                sx += pts[i].X; sy += pts[i].Y; sz += pts[i].Z;
            }
            if (pts.Count > 0) it.SetCenter(sx / pts.Count, sy / pts.Count, sz / pts.Count);

            try
            {
                CoordinateSystem cs = b.GetCoordinateSystem();
                double[] n = Normal(cs.AxisX, cs.AxisY);
                if (n != null)
                {
                    it.Num("Truc than X", n[0], 0.02);
                    it.Num("Truc than Y", n[1], 0.02);
                    it.Num("Truc than Z", n[2], 0.02);
                }
            }
            catch { }
            return it;
        }

        private static CmpItem WeldItem(BaseWeld w)
        {
            CmpItem it = New("Han", w);
            string m = Desc(w.MainObject);
            string s = Desc(w.SecondaryObject);
            it.Title = "a" + F(w.SizeAbove) + "/" + F(w.SizeBelow) + ": " + m + " + " + s;
            it.Attr("Loai", w.GetType().Name);
            it.Attr("Cai chinh", m);
            it.Attr("Cai phu", s);
            it.Attr("Kieu tren", w.TypeAbove.ToString());
            it.Attr("Size tren", F(w.SizeAbove));
            it.Attr("Kieu duoi", w.TypeBelow.ToString());
            it.Attr("Size duoi", F(w.SizeBelow));
            it.Attr("Shop/Site", w.ShopWeld ? "shop" : "site");
            Point cm = CenterOf(w.MainObject);
            Point cs = CenterOf(w.SecondaryObject);
            PointNums(it, "Tam cai chinh", cm);
            PointNums(it, "Tam cai phu", cs);
            if (cm != null && cs != null) it.SetCenter((cm.X + cs.X) / 2, (cm.Y + cs.Y) / 2, (cm.Z + cs.Z) / 2);
            else if (cs != null) it.SetCenter(cs.X, cs.Y, cs.Z);
            return it;
        }

        private static CmpItem BoolItem(BooleanPart bp)
        {
            CmpItem it = New("Cat", bp);
            Part op = bp.OperativePart;
            string prof = op == null ? "-" : op.Profile.ProfileString;
            it.Title = bp.Type + " " + prof + " vao " + Desc(bp.Father);
            it.Attr("Kieu", bp.Type.ToString());
            it.Attr("Cat vao", Desc(bp.Father));
            it.Attr("Loai cat", op == null ? "-" : op.GetType().Name);
            it.Attr("Profile cat", prof);

            bool boxed = false;
            if (op != null)
            {
                try { op.Select(); boxed = BoxOf(it, op); } catch { }
                if (!boxed) PointsOf(it, op);
            }
            PointNums(it, "Tam cai bi cat", CenterOf(bp.Father));
            return it;
        }

        /// Khi không lấy được khối đặc của part cắt: dùng điểm đầu/cuối hoặc các điểm contour.
        private static void PointsOf(CmpItem it, Part op)
        {
            List<Point> pts = new List<Point>();
            Beam bm = op as Beam;
            if (bm != null) { pts.Add(bm.StartPoint); pts.Add(bm.EndPoint); }
            ContourPlate cp = op as ContourPlate;
            if (cp != null && cp.Contour != null && cp.Contour.ContourPoints != null)
                foreach (object o in cp.Contour.ContourPoints) { Point p = o as Point; if (p != null) pts.Add(p); }
            double sx = 0, sy = 0, sz = 0;
            for (int i = 0; i < pts.Count; i++)
            {
                PointNums(it, "Diem " + (i + 1), pts[i]);
                sx += pts[i].X; sy += pts[i].Y; sz += pts[i].Z;
            }
            if (pts.Count > 0) it.SetCenter(sx / pts.Count, sy / pts.Count, sz / pts.Count);
        }

        private static double[] Normal(Vector ax, Vector ay)
        {
            double x = ax.Y * ay.Z - ax.Z * ay.Y;
            double y = ax.Z * ay.X - ax.X * ay.Z;
            double z = ax.X * ay.Y - ax.Y * ay.X;
            double l = Math.Sqrt(x * x + y * y + z * z);
            if (l < 1e-9) return null;
            return new[] { x / l, y / l, z / l };
        }

        /// Mặt phẳng: pháp tuyến + khoảng cách tới gốc toạ độ cấu kiện chính. Fitting không có hướng -> chuẩn hoá dấu.
        private static CmpItem PlaneItem(string group, ModelObject o, Plane pl, ModelObject father, bool unsigned)
        {
            CmpItem it = New(group, o);
            string fd = Desc(father);
            it.Title = group + " tren " + fd;
            it.Attr("Cat vao", fd);
            double[] n = Normal(pl.AxisX, pl.AxisY);
            if (n == null) return it;
            if (unsigned)
            {
                const double e = 1e-6;
                bool flip = n[0] < -e || (Math.Abs(n[0]) <= e && (n[1] < -e || (Math.Abs(n[1]) <= e && n[2] < 0)));
                if (flip) { n[0] = -n[0]; n[1] = -n[1]; n[2] = -n[2]; }
            }
            double d = n[0] * pl.Origin.X + n[1] * pl.Origin.Y + n[2] * pl.Origin.Z;
            it.Num("Phap tuyen X", n[0], 0.01);
            it.Num("Phap tuyen Y", n[1], 0.01);
            it.Num("Phap tuyen Z", n[2], 0.01);
            it.Num("Khoang cach toi goc", d, Tol);
            Point c = CenterOf(father) ?? pl.Origin;
            double k = n[0] * c.X + n[1] * c.Y + n[2] * c.Z - d;
            it.SetCenter(c.X - k * n[0], c.Y - k * n[1], c.Z - k * n[2]);
            return it;
        }

        // ---------------------------------------------------------------- ghép + so

        private static double Dist(CmpItem a, CmpItem b)
        {
            double dx = a.CX - b.CX, dy = a.CY - b.CY, dz = a.CZ - b.CZ;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        private static int AttrMismatch(CmpItem a, CmpItem b)
        {
            int n = 0;
            foreach (KeyValuePair<string, string> kv in a.Attrs)
                if (b.Get(kv.Key) != kv.Value) n++;
            return n;
        }

        private static void Match(CmpResult r)
        {
            List<double[]> cand = new List<double[]>();
            for (int i = 0; i < r.ItemsA.Count; i++)
                for (int j = 0; j < r.ItemsB.Count; j++)
                {
                    CmpItem a = r.ItemsA[i], b = r.ItemsB[j];
                    if (a.Group != b.Group) continue;
                    int mis = AttrMismatch(a, b);
                    double d = Dist(a, b);
                    if (a.AlwaysPair || d <= PairMax || mis == 0) cand.Add(new[] { d + AttrWeight * mis, i, j });
                }
            cand.Sort(delegate(double[] x, double[] y) { return x[0].CompareTo(y[0]); });

            bool[] usedA = new bool[r.ItemsA.Count];
            bool[] usedB = new bool[r.ItemsB.Count];
            foreach (double[] c in cand)
            {
                int i = (int)c[1], j = (int)c[2];
                if (usedA[i] || usedB[j]) continue;
                usedA[i] = usedB[j] = true;
                CmpPair p = new CmpPair();
                p.A = r.ItemsA[i];
                p.B = r.ItemsB[j];
                p.Diffs = Diff(p.A, p.B);
                (p.Diffs.Count == 0 ? r.Same : r.Diff).Add(p);
            }
            for (int i = 0; i < usedA.Length; i++) if (!usedA[i]) r.OnlyA.Add(r.ItemsA[i]);
            for (int j = 0; j < usedB.Length; j++) if (!usedB[j]) r.OnlyB.Add(r.ItemsB[j]);

            Comparison<CmpPair> byPair = delegate(CmpPair x, CmpPair y) { return Order(x.A, y.A); };
            r.Same.Sort(byPair);
            r.Diff.Sort(byPair);
            r.OnlyA.Sort(Order);
            r.OnlyB.Sort(Order);
        }

        private static int Order(CmpItem a, CmpItem b)
        {
            int c = GroupRank(a.Group).CompareTo(GroupRank(b.Group));
            if (c == 0) c = string.CompareOrdinal(a.Group, b.Group);
            if (c == 0) c = Math.Round(a.CX).CompareTo(Math.Round(b.CX));
            if (c == 0) c = Math.Round(a.CY).CompareTo(Math.Round(b.CY));
            if (c == 0) c = Math.Round(a.CZ).CompareTo(Math.Round(b.CZ));
            return c;
        }

        private static int GroupRank(string g)
        {
            if (g.StartsWith("Dau vao")) return 0;
            switch (g)
            {
                case "Part": return 1;
                case "Bu long": return 2;
                case "Han": return 3;
                case "Cat": return 4;
                case "Fitting": return 5;
                case "Cut plane": return 6;
                case "Component con": return 7;
                default: return 8;
            }
        }

        private static List<string> Diff(CmpItem a, CmpItem b)
        {
            List<string> d = new List<string>();
            foreach (KeyValuePair<string, string> kv in a.Attrs)
            {
                string vb = b.Get(kv.Key);
                if (vb == null) d.Add(kv.Key + ": " + kv.Value + "  |  (khong co)");
                else if (vb != kv.Value) d.Add(kv.Key + ": " + kv.Value + "  |  " + vb);
            }
            foreach (KeyValuePair<string, string> kv in b.Attrs)
                if (a.Get(kv.Key) == null) d.Add(kv.Key + ": (khong co)  |  " + kv.Value);

            Dictionary<string, CmpNum> nb = new Dictionary<string, CmpNum>();
            foreach (CmpNum n in b.Nums) nb[n.Label] = n;
            Dictionary<string, bool> inA = new Dictionary<string, bool>();
            foreach (CmpNum na in a.Nums)
            {
                inA[na.Label] = true;
                CmpNum n2;
                if (!nb.TryGetValue(na.Label, out n2)) { d.Add(na.Label + ": " + F(na.Value) + "  |  (khong co)"); continue; }
                double delta = n2.Value - na.Value;
                if (Math.Abs(delta) > Math.Max(na.Tol, n2.Tol))
                    d.Add(na.Label + ": " + F(na.Value) + "  |  " + F(n2.Value) + "   (lech " + (delta > 0 ? "+" : "") + F(delta) + ")");
            }
            foreach (CmpNum n in b.Nums)
                if (!inA.ContainsKey(n.Label)) d.Add(n.Label + ": (khong co)  |  " + F(n.Value));
            return d;
        }

        // ---------------------------------------------------------------- báo cáo

        private static string BuildReport(CmpResult r)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SO SANH COMPONENT   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("A (goc)    : " + r.NameA + "   id " + r.IdA + "   (" + r.ItemsA.Count + " doi tuong)");
            sb.AppendLine("B (plugin) : " + r.NameB + "   id " + r.IdB + "   (" + r.ItemsB.Count + " doi tuong)");
            sb.AppendLine("Toa do lay theo he toa do cau kien chinh cua tung component. Dung sai " + F(Tol) + " mm, the tich 1%.");
            sb.AppendLine("Moi dong lech ghi:  ten: gia tri A  |  gia tri B");
            sb.AppendLine();
            bool allOk = r.Diff.Count == 0 && r.OnlyA.Count == 0 && r.OnlyB.Count == 0;
            sb.AppendLine("KET QUA: " + (allOk ? "GIONG NHAU" : "CO KHAC BIET") + "  -  khop " + r.Same.Count + ", lech " + r.Diff.Count +
                          ", B thieu " + r.OnlyA.Count + ", B thua " + r.OnlyB.Count);
            foreach (string n in r.Notes) sb.AppendLine("Luu y: " + n);

            if (r.Diff.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("---- LECH (" + r.Diff.Count + ") ----");
                foreach (CmpPair p in r.Diff)
                {
                    sb.AppendLine("[" + p.A.Group + "] " + p.A.Title + "   (A id " + p.A.Id + " / B id " + p.B.Id + ")");
                    for (int i = 0; i < p.Diffs.Count && i < MaxDiffLines; i++) sb.AppendLine("      " + p.Diffs[i]);
                    if (p.Diffs.Count > MaxDiffLines) sb.AppendLine("      ... va " + (p.Diffs.Count - MaxDiffLines) + " dong khac");
                }
            }
            AppendItems(sb, "CHI CO O A -> plugin THIEU", r.OnlyA);
            AppendItems(sb, "CHI CO O B -> plugin THUA", r.OnlyB);
            if (r.Same.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("---- KHOP (" + r.Same.Count + ") ----");
                foreach (CmpPair p in r.Same)
                    sb.AppendLine("[" + p.A.Group + "] " + p.A.Title + "   (A id " + p.A.Id + " / B id " + p.B.Id + ")");
            }
            return sb.ToString();
        }

        private static void AppendItems(StringBuilder sb, string title, List<CmpItem> list)
        {
            if (list.Count == 0) return;
            sb.AppendLine();
            sb.AppendLine("---- " + title + " (" + list.Count + ") ----");
            foreach (CmpItem it in list)
            {
                sb.AppendLine("[" + it.Group + "] " + it.Title + "   id " + it.Id + "   tam " + it.CenterText);
                foreach (KeyValuePair<string, string> kv in it.Attrs)
                    if (kv.Key != "Loai" && kv.Value != "") sb.AppendLine("      " + kv.Key + ": " + kv.Value);
            }
        }
    }
}
