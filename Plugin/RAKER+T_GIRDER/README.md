# RAKER+T_GIRDER

Chân raker tựa lên T-girder. Viết lại từ custom component `0-RAKER+TG`, file gốc `UEL Template\RAKER+T_GIRDER.uel`.

- Tên trong Tekla: `RAKER+T_GIRDER`; bản DEV là `RAKER+T_GIRDER_DEV`.
- Primary là T-girder (dầm nhận lực). Secondary là raker (thanh chống xiên, tiết diện H).
- Tên tham số `P1`, `P2`… giữ đúng như component gốc.

| File | Nội dung |
|---|---|
| `Params.cs` | Thông số và giá trị mặc định, lấy từ bộ thuộc tính `KK350` |
| `Builder.cs` | Hình học: bản đế, bản mã (gusset), bulông, thanh dẹt, cắt raker |
| `Component.cs` | Tiêu đề, câu nhắc chọn cấu kiện, chỉnh giá trị sau khi đọc hộp thoại |
| `Plugin.cs` | Hộp thoại, lấy từ `.inp` gốc |

Sửa xong thì chạy:
```bash
build.bat core RAKER+T_GIRDER
```
rồi bấm Modify trên component `_DEV` trong Tekla.

## Tài liệu liên quan
- Công thức từng biến P/D và thuộc tính gán cho part: `UEL Template\_PHAN_TICH\components\RAKER+T_GIRDER.uel.md`.
- Lỗi đã biết, ý nghĩa của P13, bảng công thức D và checklist: `ALL IN ONE\Plugins\RakerTG\README.md` (mục 5b và checklist).

## Chưa làm
- `P13 = 1` (kiểu không có bản mã và bulông): hiện vẫn dựng theo kiểu `P13 = 0`, và ghi cảnh báo vào log.
