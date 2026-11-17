// Prefix của part mới tạo ra theo main part (Part 1) - quy tắc người dùng, Plugin\CLAUDE.md mục 4.3.
//
// Trong .uel gốc người dùng hay viết:
//   P99  = fP(proSERIE, <Part 1>)                     -> part prefix của main part, vd. "B1D-RM"
//   SERIE = mid(P99, 0, find(P99, "M")) + <ô Mark>     -> "B1D-R" + "GP" = "B1D-RGP"
// Một vài .uel cắt theo ký tự khác và giữ luôn ký tự đó: mid(P36, 0, find(P36, "-") + 1) -> dùng includeMarker = true.
using System;
using Tekla.Structures.Model;

namespace UEL.Core
{
    public static class PartPrefix
    {
        public const string DefaultMarker = "M";

        /// Phần đầu lấy từ prefix của main part = mid(P99, 0, find(P99, "M")): phần đứng TRƯỚC chữ "M" đầu tiên
        /// (phân biệt hoa thường, giống find của Tekla).
        /// Main part không có prefix, hoặc prefix không có chữ marker -> trả "" (giả định: dùng nguyên giá trị ô Mark) + CANH BAO.
        public static string Head(Part mainPart, string marker, bool includeMarker)
        {
            string p = mainPart == null ? null : mainPart.PartNumber.Prefix;
            return Head(p, marker, includeMarker);
        }

        public static string Head(Part mainPart)
        {
            return Head(mainPart, DefaultMarker, false);
        }

        public static string Head(string mainPrefix, string marker, bool includeMarker)
        {
            if (string.IsNullOrEmpty(marker)) marker = DefaultMarker;
            if (string.IsNullOrEmpty(mainPrefix))
            {
                Logger.Log("CANH BAO: main part khong co prefix -> prefix part moi = nguyen gia tri o Mark.");
                return "";
            }
            int i = mainPrefix.IndexOf(marker, StringComparison.Ordinal);
            if (i < 0)
            {
                Logger.Log("CANH BAO: prefix main part '" + mainPrefix + "' khong co '" + marker +
                           "' -> prefix part moi = nguyen gia tri o Mark (gia dinh, can xac nhan).");
                return "";
            }
            string head = mainPrefix.Substring(0, includeMarker ? i + marker.Length : i);
            Logger.Log("Prefix main part '" + mainPrefix + "' -> phan dau '" + head + "' (cat truoc '" + marker + "').");
            return head;
        }

        /// Main part của assembly chứa part (part mới hàn vào part nào thì thuộc assembly đó).
        /// Không đọc được assembly -> trả lại chính part.
        public static Part AssemblyMainPart(Part part)
        {
            if (part == null) return null;
            try
            {
                Assembly a = part.GetAssembly();
                Part main = a == null ? null : a.GetMainPart() as Part;
                if (main != null)
                {
                    main.Select();
                    return main;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("CANH BAO: khong doc duoc assembly cua part " + part.Identifier.ID + ": " + ex.Message);
            }
            return part;
        }

        /// Phần đầu prefix theo main part của assembly chứa part: mid(P99,0,find(P99,"M")), P99 = prefix main part đó.
        /// Dùng cho part mới hàn (thuộc assembly) vào part này - quy tắc người dùng 17/11/2026, Plugin\CLAUDE.md mục 4.3.
        public static string HeadOfAssembly(Part part, string who)
        {
            Part main = AssemblyMainPart(part);
            if (main != null && part != null && main.Identifier.ID != part.Identifier.ID)
                Logger.Log(who + ": main part cua assembly la part " + main.Identifier.ID + " (khong phai chinh no).");
            Logger.Log(who + ": lay prefix theo main part cua assembly.");
            return Head(main);
        }

        /// Prefix part mới = phần đầu (Head) + giá trị ô Mark trong hộp thoại.
        public static string Make(string head, string mark)
        {
            return (head ?? "") + (mark ?? "").Trim();
        }
    }
}
