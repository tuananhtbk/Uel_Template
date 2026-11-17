# Tóm tắt phiên làm việc 17/11/2026 (máy thứ hai)

Dùng file này để tiếp tục trên máy khác. Chi tiết kỹ thuật nằm trong các file được nhắc tới. Các file đó đều đã commit.

## 1. Đã làm gì

### Máy và quy tắc dự án
- Đã kiểm tra máy thứ hai làm tiếp được: git khớp `origin/main`, có csc v4, có Python 3.13, locale cp949.
- [CLAUDE.md](CLAUDE.md) mô tả cả hai máy. Đầu phiên chạy `build.bat check` để biết đang ở máy nào.
- [Plugin/CLAUDE.md](Plugin/CLAUDE.md) có thêm **mục 4, "Quy tắc riêng của người dùng"**:
  - 4.1: Part 1 là primary, Part 2 là secondary (một hoặc nhiều part).
  - 4.2: Mỗi component có hình minh hoạ dựng theo mô tả của người dùng và file `.uel` cũ. Ô nhập đặt ngay trên hình (`_hinh/VeHinh.cs`). Giữ nguyên tên biến, kiểu, giá trị mặc định và tab như bản gốc.
  - 4.3: Prefix của part mới lấy theo **main part của assembly chứa part đó**: `mid(P99,0,find(P99,"M")) + giá trị ô`. Code dùng `PartPrefix.HeadOfAssembly` và `PartPrefix.Make`.
  - 4.4: Với connection có Part 2 nghiêng hoặc vặn, lấy RAKER+T_GIRDER làm chuẩn. Luôn trả về mặt phẳng global trước khi tạo hoặc đặt mặt phẳng khác.
  - Thêm quy tắc biến mới (lấy tên P kế tiếp chưa dùng; để trống thì giữ hành vi cũ) và quy tắc bảng tra. Checklist chuyển sang mục 5.

### Code chung (mới)
- `Plugin/_Common/Core/PartPrefix.cs`: lấy phần đầu prefix theo main part của assembly. Nếu prefix không có chữ "M" thì chỉ dùng Mark và ghi CANH BAO.
- `Plugin/_Common/Core/LookupTable.cs`: đọc bảng tra TAB.
  - Mỗi cột đặt tên theo biến P. Khoá là cột đầu.
  - Dòng `*` là dòng mặc định.
  - Tự nhận encoding cp949 hoặc UTF-8.
  - Khoá được chuẩn hoá: viết `x` hay `*` đều khớp, không phân biệt hoa thường, bỏ dấu cách.
  - Tìm file theo thứ tự: đường dẫn đầy đủ → thư mục model → XS_PROJECT → XS_FIRM → thư mục DLL plugin.

### RAKER+T_GIRDER (chi tiết trong [README](Plugin/RAKER+T_GIRDER/README.md))
- **Prefix:**
  - Base plate (P4) và gusset (P21) theo assembly của Part 1.
  - Flat bar (P11) theo assembly của Part 2.
  - Mặc định là RSF, RGP, RFB.
- **Flat bar:**
  - Tạo dạng Beam, profile **P55** mặc định `FB25*9`, vẫn quay ngang, vật liệu SS275.
  - Đã sửa lỗi FB bị tạo cách xa khoảng 80 m (do đặt sai mặt phẳng). Đã kiểm tra trên model SAMPLE: FB nằm sát base plate.
- **Cắt vát** hai cánh dài hơn, để góc rất nhọn vẫn cắt hết đường hàn.
- **Bu lông:** chỉ còn Washer2 + Nut1 (Washer1 và Washer3 tắt).
- **P54** (biến mới): chiều dài gusset. Để trống hoặc 0 thì tự tính như cũ.
- **Bảng tra theo profile Part 2** (biến mới):
  - **P56** là đường dẫn file; để trống thì dùng `RAKER+T_GIRDER.txt`.
  - **P57** là Yes/No để bật/tắt bảng.
  - Giá trị trong bảng ghi đè giá trị hộp thoại.
  - Profile không có trong bảng và không có dòng `*` thì dùng giá trị hộp thoại, ghi CANH BAO vào `%TEMP%\UEL_PLUGINS.log`.
- **Hộp thoại và hình minh hoạ** đã vẽ lại với các ô P54–P57.
- **Bảng tra trong thư mục component:**
  - `RAKER+T_GIRDER.txt` là bảng mẫu mặc định. Có dòng H350\*357\*19\*19 (KK350); dòng KK250 đang để ghi chú vì chưa biết profile.
  - `RAKER_TG_THU.txt` là file thử, có 4 profile, trong đó H550\*200\*10\*16 là raker của model SAMPLE. **Giá trị chỉ để thử chức năng, chưa kiểm tra kết cấu.**
- **install.ps1 và pack.ps1:** chép bảng mẫu `<Ten>.txt` vào thư mục plugin của Tekla, chỉ khi ở đó chưa có file, không ghi đè.

## 2. Sang máy kia làm gì
1. `git pull` (DLL không lưu trong git).
2. Ở thư mục `Plugin\`, chạy `build.bat check`, rồi `build.bat`.
3. Tắt Tekla, rồi chạy `install.bat dev 18` bằng quyền Administrator. Cần cài lại vì hộp thoại có thêm P54–P57.
4. Mở Tekla, dùng component `RAKER+T_GIRDER_DEV`:
   - Tab Base Plate, ô P56: dán đường dẫn **đầy đủ** tới `RAKER_TG_THU.txt` trên máy đó.
   - Để P57 = Yes, rồi bấm Modify.
5. Xem `%TEMP%\UEL_PLUGINS.log` để thấy dòng `Bang tra ...: dong '...' -> ...`.

## 3. Còn mở / chưa kiểm tra
- **Bảng tra chưa thử trong Tekla.** Mới thử bằng chương trình ngoài với `Params.cs` thật, kết quả đúng.
- Chưa làm:
  - giá trị thật cho bảng `RAKER+T_GIRDER.txt`, gồm profile của KK250;
  - bản phát hành (`install.bat 18` / `2020`…).
- Điều cần biết khi kiểm tra: part do plugin tạo trong Tekla 18.0 trả `GetFatherComponent()` = null. Đây là đặc điểm của API, không phải lỗi.
- Bước tiếp theo theo kế hoạch chung ([HUONG_DAN_CHUYEN_MAY.md](HUONG_DAN_CHUYEN_MAY.md)): làm tiếp component khác (thử trước với `0-PL WELD`) theo quy trình trong `Plugin/CLAUDE.md`.
