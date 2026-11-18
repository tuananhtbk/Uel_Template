// Khai báo component H-COL_TO_GIRDER (clone COL_TO_GIRDER, Part 1 = cột H): tiêu đề, câu nhắc chọn cấu kiện, bảng tra bắt buộc, và nối sang HColToGirderBuilder.
// Tên component/plugin = tên thư mục (build.bat tự lấy), không khai báo ở đây.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Tekla.Structures.Model;
using UEL.Core;

namespace UEL.Core.Components.HColToGirder
{
    public class HColToGirderComponent : UelComponent
    {
        /// Bảng tra bắt buộc của .uel gốc: fVF("YGY-H_COL2GIR-운용층-H-COL.txt", P15, cột). Viết bằng mã \u để không phụ thuộc
        /// encoding lúc biên dịch.
        public const string TableFile = "YGY-H_COL2GIR-운용층-H-COL.txt";

        public override string Title { get { return "Cot H -> Dam (0-DM_H-Col2Gir_H-COLUMN)"; } }
        public override string SourceUel { get { return @"File goc\H-COL-GIRDER\H-COL.uel"; } }
        // Giả định: Part 1 (primary) = cột, Part 2 (secondary) = dầm (xem README, mục Giả định).
        public override string PrimaryPrompt { get { return "Chon cot H (Part 1)"; } }
        public override string[] SecondaryPrompts { get { return new[] { "Chon dam (Part 2)" }; } }

        public override object NewParams() { return new HColToGirderParams(); }

        public override void AfterApply(object prm, IDictionary values)
        {
            HColToGirderParams p = (HColToGirderParams)prm;
            if (values == null) values = new Hashtable();
            // Ô có mặc định là công thức: nhập thì dùng giá trị nhập, để trống thì tính như .uel.
            p.BoltToFlangeSet = values.Contains("P4");
            p.WpStraightSet = values.Contains("P81");
            p.WpTipSet = values.Contains("P82");
            p.BoltPitchSet = values.Contains("P22") && !string.IsNullOrEmpty(p.BoltPitch);
            p.SlotLengthSet = values.Contains("P24");
            p.WingLengthSet = values.Contains("P28");
            p.L3Set = values.Contains("P47");
            p.Wp1Set = values.Contains("P50");
            p.Wp2Set = values.Contains("P51");
            if (p.CgpZone == null) p.CgpZone = "";
            if (p.GwpZone == null) p.GwpZone = "";
            if (p.WingMat == null) p.WingMat = "";
            if (p.BoltPitch == null) p.BoltPitch = "";
            if (string.IsNullOrEmpty(p.WpMark) || p.WpMark.Trim().Length == 0) p.WpMark = "WP";
            if (p.TablePath == null) p.TablePath = "";
        }

        public override bool Build(Model model, object prm, ModelObject primary, IList secondaries, IList created)
        {
            Beam column = primary as Beam;
            Beam girder = secondaries.Count > 0 ? secondaries[0] as Beam : null;
            if (column == null || girder == null)
            {
                Logger.Log("Can chon 1 cot (Part 1) va 1 dam (Part 2) dang Beam.");
                return false;
            }
            if (secondaries.Count > 1)
                Logger.Log("CANH BAO: chon " + secondaries.Count + " dam phu - chi dung dam dau tien (.uel goc la 1 dam).");

            HColToGirderParams p = (HColToGirderParams)prm;
            string modelPath = "";
            try { modelPath = model.GetInfo().ModelPath; } catch (Exception) { }

            TableRow row = ReadTable(p.TablePath, modelPath, girder.Profile.ProfileString);
            if (row == null) return false;

            HColToGirderBuilder b = new HColToGirderBuilder(model, p, row);
            bool ok;
            try
            {
                ok = b.Build(column, girder);
            }
            finally
            {
                foreach (ModelObject o in b.Created) created.Add(o);
            }
            return ok;
        }

        /// fVF("YGY-H_COL2GIR-운용층-H-COL.txt", P15, n), P15 = profile dầm. Cột đếm từ 1 (cột 1 = Girder Size):
        ///   2 = L3 (P47), 3 = B3 (P19), 4 = t3 (P3, P17), 5 = WING PL (P28), 7 = BOLT ROW (P18).
        /// Bảng BẮT BUỘC: không có file hoặc không có dòng -> ghi log và dừng, không tự điền số.
        /// BẮT BUỘC ưu tiên file trong model (người dùng 18/11/2026, LookupTable.ResolveInModel): model có file cùng tên
        /// thì luôn dùng; chưa có thì tìm nguồn (đường dẫn đầy đủ, hoặc XS_PROJECT -> XS_FIRM -> thư mục plugin),
        /// chép 1 bản vào model rồi dùng bản trong model; không ghi đè file trong model.
        /// Ô P78 để trống (người dùng 18/11/2026): bảng mặc định là "H-COL_TO_GIRDER.txt" (install.bat chép vào thư mục
        /// plugin của Tekla); không thấy thì mới tìm tên gốc của .uel "YGY-H_COL2GIR-운용층-H-COL.txt".
        /// P78 (biến mới) = file bảng tra khác (đường dẫn đầy đủ hoặc chỉ tên), vd. bảng TANG_DUONG của từng dự án.
        /// Nhập P78 mà không thấy file -> dừng (không lùi về bảng mặc định, tránh dựng sai số liệu dự án).
        private static TableRow ReadTable(string tablePath, string modelPath, string profile)
        {
            List<string> tried = new List<string>();
            bool custom = tablePath != null && tablePath.Trim().Length > 0;
            string defaultFile = UEL.ComponentInfo.Name + ".txt";
            string want = custom ? tablePath.Trim().Trim('"') : defaultFile;
            string path = LookupTable.ResolveInModel(want, modelPath, tried);
            if (path == null && custom)
            {
                Logger.Error("khong tim thay bang tra P78 = '" + want + "'. Da tim: " + string.Join(" | ", tried.ToArray()));
                return null;
            }
            if (path == null)
            {
                path = LookupTable.ResolveInModel(TableFile, modelPath, tried);
                if (path != null)
                    Logger.Log("CANH BAO: khong thay " + defaultFile + " -> dung bang ten goc cua .uel: " + path);
            }
            if (path == null)
            {
                Logger.Error("khong tim thay bang tra " + defaultFile + " (hoac " + TableFile + "). Da tim: " + string.Join(" | ", tried.ToArray()));
                return null;
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
                Logger.Error("Girder size '" + profile + "' (profile dam) khong co trong bang tra " + path + ". Them dong nay vao bang roi Modify.");
                return null;
            }
            TableRow row = new TableRow();
            row.Key = matched;
            bool ok = Cell(r, 2, "L3", out row.L3) & Cell(r, 3, "B3", out row.B3) & Cell(r, 4, "t3", out row.T3)
                      & Cell(r, 5, "WING PL", out row.Wing) & Cell(r, 7, "BOLT ROW", out row.BoltRow);
            if (!ok)
            {
                Logger.Error("dong '" + matched + "' trong " + path + " thieu so lieu.");
                return null;
            }
            // Cột TUỲ CHỌN (người dùng 18/11/2026), tìm theo tiêu đề, đặt ở bất kỳ cột nào sau cột 7.
            // Ô hộp thoại tương ứng để trống thì lấy theo bảng:
            //   "e"   (hoặc "P4", "Bolt To Beam Flg") = khoảng cách bu lông -> cạnh ngoài CGP;
            //   "L1"  (hoặc "P81", "WP L1")           = WP: đoạn rộng tới mép cột, đo từ mặt cột;
            //   "tip" (hoặc "P82", "WP Tip")          = WP: bề rộng đầu xa ngoài mép cánh.
            // Chiều dài WP (P28) = cột 5 "WING PL" (bắt buộc, như .uel).
            row.E = Optional(t, r, "E", "P4", "BOLTTOBEAMFLG");
            row.L1 = Optional(t, r, "L1", "P81", "WPL1");
            row.Tip = Optional(t, r, "TIP", "P82", "WPTIP");
            if (Logger.HasErrors) return null;   // cot tuy chon co o khong phai so (18/11/2026)
            Logger.Log(string.Format(CultureInfo.InvariantCulture,
                "Bang tra {0}: dong '{1}' -> L3={2} B3={3} t3={4} WING PL={5} BOLT ROW={6} | e={7} L1={8} tip={9}",
                path, matched, row.L3, row.B3, row.T3, row.Wing, row.BoltRow, Show(row.E), Show(row.L1), Show(row.Tip)));
            return row;
        }

        /// Giá trị cột tuỳ chọn có tiêu đề (chuẩn hoá) là một trong các tên; không có cột / ô trống / không phải số -> NaN.
        private static double Optional(LookupTable t, string[] r, params string[] names)
        {
            for (int i = 0; i < t.Header.Length && i < r.Length; i++)
            {
                string h = LookupTable.NormalizeHeader(t.Header[i]);
                if (Array.IndexOf(names, h) < 0) continue;
                double v;
                if (r[i].Length > 0 && double.TryParse(r[i], NumberStyles.Float, CultureInfo.InvariantCulture, out v)) return v;
                if (r[i].Length > 0) Logger.Error("Bang tra: cot '" + t.Header[i] + "' khong phai so: '" + r[i] + "'.");
                return double.NaN;
            }
            return double.NaN;
        }

        private static string Show(double v)
        {
            return double.IsNaN(v) ? "-" : v.ToString(CultureInfo.InvariantCulture);
        }

        /// Như LookupTable.FindRow, thêm: bỏ dấu "-" ngay sau chữ cái đầu ("H-500x200x10x16" = "H500*200*10*16",
        /// bảng TANG_DUONG viết kiểu này). Dấu "-" giữa 2 số (HUN1100-792*350...) giữ nguyên.
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

        private static bool Cell(string[] r, int col, string name, out double v)
        {
            v = 0;
            int i = col - 1;
            if (i >= r.Length || r[i].Length == 0)
            {
                Logger.Error("bang tra thieu cot " + col + " (" + name + ").");
                return false;
            }
            if (!double.TryParse(r[i], NumberStyles.Float, CultureInfo.InvariantCulture, out v))
            {
                Logger.Error("cot " + col + " (" + name + ") khong phai so: '" + r[i] + "'.");
                return false;
            }
            return true;
        }
    }

    /// Một dòng bảng YGY-H_COL2GIR-운용층-H-COL.txt.
    public class TableRow
    {
        public string Key;
        public double L3, B3, T3, Wing, BoltRow;
        /// Cột tuỳ chọn "e" (P4), "L1" (P81), "tip" (P82); NaN = bảng không có cột / ô trống.
        public double E, L1, Tip;
    }
}
