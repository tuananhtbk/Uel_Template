// Khai báo component TEMPBEAM-MOMENT: tiêu đề, câu nhắc chọn cấu kiện, 2 bảng tra bắt buộc, nối sang TempBeamBuilder.
// Tên component/plugin = tên thư mục (build.bat tự lấy), không khai báo ở đây.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Tekla.Structures.Model;
using UEL.Core;

namespace UEL.Core.Components.TempBeam
{
    public class TempBeamComponent : UelComponent
    {
        private static readonly CultureInfo CI = CultureInfo.InvariantCulture;

        /// Bảng dầm của .uel: fVF("YGY-TB-MOMENT.txt", P1, n), P1 = profile Part 2.
        public const string GirderTable = "YGY-TB-MOMENT.txt";
        /// Bảng chiều dày tấm SF dưới: P39 = fVF("YGY-TB-MOMENT-PLTHK.txt", P4, 2), P4 = chiều dày cánh Part 2.
        public const string PlThkTable = "YGY-TB-MOMENT-PLTHK.txt";

        public override string Title { get { return "Dam tam -> dam chinh, moment (YGY_TempBeam-Moment)"; } }
        public override string SourceUel { get { return @"File goc\PIN-MOMENT CHANGSIN\YGY_TEMPBEAM-MOMENT.uel"; } }
        // JSON 18/11/2026: primary = dầm chính H500 (Part 1), secondary = dầm tạm H400 tra trong bảng "GIRDER SIZE" (Part 2).
        public override string PrimaryPrompt { get { return "Chon dam chinh (Part 1)"; } }
        public override string[] SecondaryPrompts { get { return new[] { "Chon dam tam (Part 2)" }; } }

        public override object NewParams() { return new TempBeamParams(); }

        public override void AfterApply(object prm, IDictionary values)
        {
            TempBeamParams p = (TempBeamParams)prm;
            if (values == null) values = new Hashtable();
            // Ô có mặc định là công thức: nhập thì dùng giá trị nhập, để trống thì tính như .uel.
            p.SlotHoleSet = values.Contains("P15");
            p.ScallopSizeSet = values.Contains("P41");
            p.LowerFlgImproveSet = values.Contains("P43");
            p.CwsSet = values.Contains("CWS");
            if (p.GpPrefix == null) p.GpPrefix = "";
            if (p.TablePath == null) p.TablePath = "";
            if (string.IsNullOrEmpty(p.FbProfile) || p.FbProfile.Trim().Length == 0) p.FbProfile = "FB25*9";
            if (string.IsNullOrEmpty(p.FbMat) || p.FbMat.Trim().Length == 0) p.FbMat = "SS275";
            if (string.IsNullOrEmpty(p.BoltStandard) || p.BoltStandard.Trim().Length == 0) p.BoltStandard = "T.S.B";
            if (p.GpName == null) p.GpName = "";
            if (p.GpClass == null) p.GpClass = "";
            if (p.GpFinish == null) p.GpFinish = "";
            if (p.SfName == null) p.SfName = "";
            if (p.SfClass == null) p.SfClass = "";
            if (p.SfFinish == null) p.SfFinish = "";
            if (p.FbName == null) p.FbName = "";
            if (p.FbClass == null) p.FbClass = "";
            if (p.FbFinish == null) p.FbFinish = "";
        }

        public override bool Build(Model model, object prm, ModelObject primary, IList secondaries, IList created)
        {
            Beam main = primary as Beam;
            Beam temp = secondaries.Count > 0 ? secondaries[0] as Beam : null;
            if (main == null || temp == null)
            {
                Logger.Log("Can chon 1 dam chinh (Part 1) va 1 dam tam (Part 2) dang Beam.");
                return false;
            }
            if (secondaries.Count > 1)
                Logger.Log("CANH BAO: chon " + secondaries.Count + " dam phu - chi dung dam dau tien (.uel goc la 1 dam).");

            TempBeamParams p = (TempBeamParams)prm;
            string modelPath = "";
            try { modelPath = model.GetInfo().ModelPath; } catch (Exception) { }

            // ---- bảng dầm (bắt buộc)
            GirderRow row = ReadGirderTable(p.TablePath, modelPath, temp.Profile.ProfileString);
            if (row == null) return false;

            // ---- bảng PLTHK: P39 = fVF(PLTHK, P4, 2), P4 = fP(proT, Part 2) (Integer).
            // Chỉ BẮT BUỘC khi có tấm SF dưới (P25 = P23 > 0, P23 = H Part 1 - H Part 2); không cần thì tra để tính P43,
            // không có cũng không dừng (P39 = NaN, P43 tự tính = 0).
            double h1 = Report(main, "PROFILE.HEIGHT"), h2 = Report(temp, "PROFILE.HEIGHT");
            double tf2 = Report(temp, "PROFILE.FLANGE_THICKNESS");
            bool needP39 = h1 - Math.Round(h2) > 0;
            double p39 = ReadPlThk(modelPath, (int)Math.Round(tf2), needP39);
            if (needP39 && double.IsNaN(p39)) return false;

            TempBeamBuilder b = new TempBeamBuilder(model, p, row, p39);
            bool ok;
            try
            {
                ok = b.Build(main, temp);
            }
            finally
            {
                foreach (ModelObject o in b.Created) created.Add(o);
            }
            return ok;
        }

        /// Bảng dầm: ô P65 nhập -> file đó (không thấy là dừng). Trống -> tên gốc "YGY-TB-MOMENT.txt" (model -> XS_PROJECT
        /// -> XS_FIRM -> thư mục plugin, chép 1 bản vào model); không thấy mới dùng bảng mẫu "TEMPBEAM-MOMENT.txt".
        /// Tên gốc đi trước vì model của dự án thường đã có bảng riêng (thêm dòng profile riêng của dự án).
        ///
        /// Đọc cột THEO TÊN TIÊU ĐỀ (không theo vị trí như fVF), vì có 2 phiên bản bảng cùng tên:
        ///   - bảng trong model mẫu (F:\...\SAMPLE V2020):  GIRDER SIZE | T3 | H1 | B3 | NUMBER OF BOLT | GP | SF | REMARK
        ///     -> fVF cột 2 = T3, cột 3 = H1 (đếm từ 1, cột 1 = khoá): khớp JSON (PL11, cao 275 với H400*200*8*13);
        ///   - bảng trong File goc\PIN-MOMENT CHANGSIN:     GIRDER SIZE | L1 | T3 | H1 | NUMBER OF BOLT | GP | SF | REMARK
        ///     -> fVF cột 2 sẽ là L1 = 130 (sai). Đọc theo tên thì cả 2 bảng đều đúng.
        ///   T3 = P7 (dày GP), H1 = P8 (cao phần tab GP). Chiều dài tab: cột B3 (tính từ mép cánh Part 1) hoặc L1 (tính từ
        ///   đầu Part 2 sau fitting) - .uel không có công thức cho số này, xem README (giả định).
        ///   P59 = fVF(.., P1, 5) = ô cột 5 (chữ) -> CWS = 700 nếu là "SPS".
        private static GirderRow ReadGirderTable(string tablePath, string modelPath, string profile)
        {
            List<string> tried = new List<string>();
            bool custom = tablePath != null && tablePath.Trim().Length > 0;
            string path;
            if (custom)
            {
                string want = tablePath.Trim().Trim('"');
                path = LookupTable.ResolveInModel(want, modelPath, tried);
                if (path == null)
                {
                    Logger.Error("khong tim thay bang tra P65 = '" + want + "'. Da tim: " + string.Join(" | ", tried.ToArray()));
                    return null;
                }
            }
            else
            {
                path = LookupTable.ResolveInModel(GirderTable, modelPath, tried);
                if (path == null)
                {
                    string sample = UEL.ComponentInfo.Name + ".txt";
                    path = LookupTable.ResolveInModel(sample, modelPath, tried);
                    if (path != null) Logger.Log("CANH BAO: khong thay " + GirderTable + " -> dung bang mau " + path);
                }
                if (path == null)
                {
                    Logger.Error("khong tim thay bang tra " + GirderTable + " (hoac " + UEL.ComponentInfo.Name + ".txt). Da tim: " +
                                 string.Join(" | ", tried.ToArray()));
                    return null;
                }
            }
            LookupTable t;
            try { t = LookupTable.Load(path); }
            catch (Exception ex)
            {
                Logger.Error("khong doc duoc bang tra " + path + ": " + ex.Message);
                return null;
            }
            string matched;
            string[] r = FindRow(t, profile, out matched);
            if (r == null)
            {
                Logger.Error("GIRDER SIZE '" + profile + "' (profile Part 2) khong co trong bang tra " + path + ". Them dong nay vao bang roi Modify.");
                return null;
            }
            int iT3 = Col(t, "T3"), iH1 = Col(t, "H1"), iB3 = Col(t, "B3"), iL1 = Col(t, "L1");
            if (iT3 < 0 || iH1 < 0)
            {
                Logger.Error("bang tra " + path + " thieu cot T3 hoac H1 (tieu de: " + string.Join(" | ", t.Header) + ").");
                return null;
            }
            GirderRow row = new GirderRow();
            row.Key = matched;
            row.Path = path;
            bool ok = Num(r, iT3, "T3", out row.T3) & Num(r, iH1, "H1", out row.H1);
            if (iB3 >= 0) { ok &= Num(r, iB3, "B3", out row.TabLen); row.TabFromTip = true; }
            else if (iL1 >= 0) { ok &= Num(r, iL1, "L1", out row.TabLen); row.TabFromTip = false; }
            else
            {
                Logger.Error("bang tra " + path + " thieu cot B3 (hoac L1) = chieu dai tab GP.");
                return null;
            }
            if (!ok)
            {
                Logger.Error("dong '" + matched + "' trong " + path + " thieu so lieu.");
                return null;
            }
            row.Col5 = r.Length >= 5 ? r[4] : "";
            Logger.Log(string.Format(CI, "Bang tra {0}: dong '{1}' -> T3 (P7) = {2}, H1 (P8) = {3}, {4} = {5}, cot 5 (P59) = '{6}'",
                path, matched, row.T3, row.H1, row.TabFromTip ? "B3" : "L1", row.TabLen, row.Col5));
            return row;
        }

        /// P39 = fVF("YGY-TB-MOMENT-PLTHK.txt", P4, 2): cột 2 (đếm từ 1, cột 1 = khoá PL_THK) = FIX_PL_THK.
        /// required = false: không tìm thấy / không có dòng -> NaN, chỉ ghi log.
        private static double ReadPlThk(string modelPath, int p4, bool required)
        {
            List<string> tried = new List<string>();
            string path = LookupTable.ResolveInModel(PlThkTable, modelPath, tried);
            string why = null;
            LookupTable t = null;
            if (path == null) why = "khong tim thay " + PlThkTable + ". Da tim: " + string.Join(" | ", tried.ToArray());
            else
            {
                try { t = LookupTable.Load(path); }
                catch (Exception ex) { why = "khong doc duoc " + path + ": " + ex.Message; }
            }
            double v = double.NaN;
            if (t != null)
            {
                string matched;
                string[] r = t.FindRow(p4.ToString(CI), out matched);
                if (r == null) why = PlThkTable + " (" + path + ") khong co dong P4 = " + p4 + " (chieu day canh Part 2).";
                else if (r.Length < 2 || !double.TryParse(r[1], NumberStyles.Float, CI, out v))
                {
                    v = double.NaN;
                    why = PlThkTable + ", dong '" + matched + "': cot 2 (FIX_PL_THK) khong phai so.";
                }
                else Logger.Log(string.Format(CI, "Bang tra {0}: P4 = {1} -> P39 (FIX_PL_THK) = {2}", path, p4, v));
            }
            if (why != null)
            {
                if (required) Logger.Error(why + " Can P39 de dung tam SF duoi (Part 1 cao hon Part 2).");
                else Logger.Log("P39: " + why + " (khong can vi khong co tam SF duoi).");
            }
            return v;
        }

        private static int Col(LookupTable t, string name)
        {
            for (int i = 1; i < t.Header.Length; i++)
                if (LookupTable.NormalizeHeader(t.Header[i]) == name) return i;
            return -1;
        }

        /// Như LookupTable.FindRow, thêm: bỏ dấu "-" ngay sau chữ cái đầu ("H-400x200x8x13" = "H400*200*8*13").
        private static string[] FindRow(LookupTable t, string key, out string matched)
        {
            string k = TableKey(key);
            foreach (string[] r in t.Rows)
            {
                if (r.Length == 0) continue;
                if (TableKey(r[0]) == k) { matched = r[0]; return r; }
            }
            return t.FindRow(key, out matched);   // dòng "*" nếu có
        }

        private static string TableKey(string s)
        {
            return System.Text.RegularExpressions.Regex.Replace(LookupTable.NormalizeKey(s), @"^([A-Z]+)-(?=\d)", "$1");
        }

        private static bool Num(string[] r, int i, string name, out double v)
        {
            v = 0;
            if (i >= r.Length || r[i].Length == 0)
            {
                Logger.Error("bang tra thieu o cot " + name + ".");
                return false;
            }
            if (!double.TryParse(r[i], NumberStyles.Float, CI, out v))
            {
                Logger.Error("cot " + name + " khong phai so: '" + r[i] + "'.");
                return false;
            }
            return true;
        }

        private static double Report(Part part, string name)
        {
            double value = 0.0;
            part.GetReportProperty(name, ref value);
            return value;
        }
    }

    /// Một dòng bảng YGY-TB-MOMENT.txt.
    public class GirderRow
    {
        public string Key, Path, Col5;
        public double T3, H1;
        /// Chiều dài tab GP: TabFromTip = true -> tính từ mép cánh Part 1 (cột B3); false -> từ đầu Part 2 sau fitting (cột L1).
        public double TabLen;
        public bool TabFromTip;
    }
}
