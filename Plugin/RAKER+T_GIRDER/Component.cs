// Khai báo component RAKER+T_GIRDER: tiêu đề, câu nhắc chọn cấu kiện, và nối sang RakerTGBuilder (Builder.cs).
// Tên component/plugin = tên thư mục (build.bat tự lấy), không khai báo ở đây.
using System;
using System.Collections;
using System.Globalization;
using Tekla.Structures.Model;
using UEL.Core;

namespace UEL.Core.Components.RakerTG
{
    public class RakerTGComponent : UelComponent
    {
        public override string Title { get { return "Raker -> T-girder (0-RAKER+TG)"; } }
        public override string SourceUel { get { return "RAKER+T_GIRDER.uel"; } }
        public override string PrimaryPrompt { get { return "Chon dam T-girder (dam chinh)"; } }
        public override string[] SecondaryPrompts { get { return new[] { "Chon raker (dam phu)" }; } }

        public override object NewParams() { return new RakerTGParams(); }

        public override void AfterApply(object prm, IDictionary values)
        {
            RakerTGParams p = (RakerTGParams)prm;
            // FB Mat. (P5) để trống -> SS275 như mặc định của .uel gốc (người dùng 17/11/2026; trước đây lấy theo bản đế)
            if (string.IsNullOrEmpty(p.FlatBarProfile)) p.FlatBarProfile = "FB25*9";
            // P24 để trống -> bản mã tự đặt phía trong góc primary/secondary; có nhập P24 -> theo P24
            if (values != null && values.Contains("P24")) p.GussetSideAuto = false;
            p.BoltQtyX = Math.Max(1, p.BoltQtyX);
            p.BoltQtyY = Math.Max(1, p.BoltQtyY);
            if (p.TablePath == null) p.TablePath = "";
            if (p.ConnType == 1)
                Logger.Log("CANH BAO: P13 = 1 (kieu khong ban ma/bu long) chua duoc lam - dang dung theo kieu P13 = 0.");
        }

        public override bool Build(Model model, object prm, ModelObject primary, IList secondaries, IList created)
        {
            Beam girder = primary as Beam;
            Beam raker = secondaries.Count > 0 ? secondaries[0] as Beam : null;
            if (girder == null || raker == null)
            {
                Logger.Log("Can chon 1 dam chinh (T-girder) va 1 dam phu (raker) dang Beam.");
                return false;
            }
            // Làm trên BẢN SAO để không đổi bộ thông số của hộp thoại/EXE (bảng tra, P58 chỉ áp cho lần dựng này;
            // lần dựng sau với P57 = No vẫn ra đúng giá trị hộp thoại).
            RakerTGParams p = (RakerTGParams)typeof(object).GetMethod("MemberwiseClone",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(prm, null);
            IDictionary got = null;
            if (p.UseTable)
            {
                // Bảng tra theo profile Part 2 (người dùng 17/11/2026)
                string modelPath = "";
                try { modelPath = model.GetInfo().ModelPath; } catch (Exception) { }
                got = LookupTable.ApplyByKey(p, p.TablePath, UEL.ComponentInfo.Name + ".txt", modelPath,
                                             raker.Profile.ProfileString, "P56", "P57");
                // 18/11/2026: không có bảng / không có dòng profile Part 2 / ô sai -> thông báo, không dựng
                if (got == null) return false;
                // giữ đúng các quy tắc của AfterApply cho giá trị lấy từ bảng
                if (got.Contains("P24")) p.GussetSideAuto = false;
                p.BoltQtyX = Math.Max(1, p.BoltQtyX);
                p.BoltQtyY = Math.Max(1, p.BoltQtyY);
            }
            else
            {
                Logger.Log("Bang tra: tat (P57 = No) -> chi dung gia tri hop thoai.");
            }
            if (!ApplyThkSource(p, got, girder, raker)) return false;
            RakerTGBuilder b = new RakerTGBuilder(model, p);
            bool ok;
            try
            {
                ok = b.Build(girder, raker);
            }
            finally
            {
                foreach (ModelObject o in b.Created) created.Add(o);
            }
            return ok;
        }

        /// P58 (biến mới, người dùng 18/11/2026) - nguồn chiều dày bản đế P1:
        ///   0 = dày cánh Part 1 (T-girder), 1 = dày cánh Part 2 (raker): đè cả hộp thoại lẫn bảng tra;
        ///   2 = cột "Base Plate Thk" của file text (mặc định). Bảng bật mà dòng khớp không có giá trị -> dừng (không đoán số).
        ///       P57 = No -> dùng ô P1 của hộp thoại.
        /// Không đọc được dày cánh / giá trị P58 lạ -> dừng.
        private static bool ApplyThkSource(RakerTGParams p, IDictionary got, Beam girder, Beam raker)
        {
            int src = p.BasePlateThkSource;
            if (src == 0 || src == 1)
            {
                Beam part = src == 0 ? girder : raker;
                string name = src == 0 ? "Part 1 (T-girder)" : "Part 2 (raker)";
                double tf = 0.0;
                part.GetReportProperty("PROFILE.FLANGE_THICKNESS", ref tf);
                if (tf <= 0.0)
                {
                    Logger.Error("P58 = " + src + " (ban de day bang canh " + name + ") nhung khong doc duoc day canh cua profile "
                               + part.Profile.ProfileString + ". Chon P58 khac roi Modify.");
                    return false;
                }
                if (got != null && got.Contains("P1"))
                    Logger.Log("Ban de: bo qua cot 'Base Plate Thk' cua bang tra (= " + got["P1"] + ") vi P58 = " + src + ".");
                p.BasePlateThk = tf;
                Logger.Log(string.Format(CultureInfo.InvariantCulture, "Ban de: day = {0} = day canh {1} ({2}) (P58 = {3}).",
                    tf, name, part.Profile.ProfileString, src));
                return true;
            }
            if (src != 2)
            {
                Logger.Error("P58 = " + src + " khong hop le (0 = day canh Part 1, 1 = day canh Part 2, 2 = file text).");
                return false;
            }
            if (!p.UseTable)
            {
                Logger.Log(string.Format(CultureInfo.InvariantCulture,
                    "Ban de: day = {0} lay o P1 hop thoai (P58 = 2 file text, nhung P57 = No: tat bang tra).", p.BasePlateThk));
                return true;
            }
            if (got == null || !got.Contains("P1"))
            {
                Logger.Error("P58 = 2 (day ban de theo file text) nhung dong cua profile " + raker.Profile.ProfileString +
                           " trong bang tra khong co gia tri cot 'Base Plate Thk'. Dien vao bang tra, hoac chon P58 khac.");
                return false;
            }
            Logger.Log(string.Format(CultureInfo.InvariantCulture, "Ban de: day = {0} theo file text (P58 = 2).", p.BasePlateThk));
            return true;
        }
    }
}
