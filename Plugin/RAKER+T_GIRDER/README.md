# RAKER+T_GIRDER

Chân raker tựa lên T-girder. Viết lại từ custom component `0-RAKER+TG`, file gốc `UEL Template\UEL\RAKER+T_GIRDER.uel` (bản trùng: `UEL\RAKER+TG.uel`).

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

## So sánh với component gốc `0-RAKER+TG` (17/11/2026)

Nguồn: `_PHAN_TICH\components\RAKER+T_GIRDER.uel.md` (biến D, công thức, thuộc tính part).

### Part và đường cắt

| Gốc (obj#, điều kiện tạo) | Plugin | Trạng thái |
|---|---|---|
| #310 bản đế `PL P1`, P2/P3/P4 | `CreateBasePlate` | Có. Kích thước theo yêu cầu người dùng: Top/Bott từ dầm chính, Left/Right từ điểm xa nhất của raker |
| #290 bản mã `PL P20`, P21/P22/P23, tạo khi P13 = 0 | `CreateGusset` | Có. Lệch tâm bụng ±P25 = (tw + P20)/2 (D115–118). Đẩy khỏi bản đế P26 = 0,5·P20·sin(P14) (D119/120): đo trên model ra 10,29 mm, **khớp** |
| Mép xa bản mã P49 = P46 + (P29 + (P31−1)·P32 + P44)·cos(P14) (D38, D153, D156) | `xf` trong `CreateGusset` | Khớp công thức |
| #1278 nhóm bu lông, tạo khi P13 = 0: P33/P35/P38/P39/P40/P41/P42 | `CreateBolts` | Có. Hàng đầu cách bản đế P46 + P29·cos(P14) (D127): khớp. Lỗ ô van theo P42 |
| #321 FB, tạo khi P52 = 1; #328 FB, tạo khi P52 = 1 và P13 = 0 | 2 thanh `CreateFlatBar` | Có. FB cánh dưới đặt **dưới** cánh dưới (theo người dùng). Tiết diện 25×9 lấy từ chuỗi "25*9" trong `.uel` |
| #300 FB, tạo khi P52 = 1 và P13 = 1 | — | Thiếu: thuộc nhánh P13 = 1 |
| Đường cắt vát cánh D53/D58/D108 = P10 + (P17−2)·tan(P12) (P17 = dày cánh) | `CutBevel` × 2 | **Mới thêm**: vát mặt trên cả 2 cánh, mặt chân 2 mm, góc P12, khe P10. Gốc có 3 đường vát; đường thứ 3 chưa rõ thuộc đâu |
| Fitting / cắt đầu raker tại P10 (D50, D54, D55, D59, D105, D109) | `FitRaker` | Có |
| Lùi bụng + scallop R35 tại P46 + 35 (D66, D67, D70, D71); #368 tạo khi P51; #335 tạo khi P53 | `CutWeb`, `CutScallop` | Có |
| #347, #1121, #1638, tạo khi P13 = 0 (P18) | — | Chưa rõ là loại đối tượng gì (cắt? hàn?). Cần xem trong Custom Component Editor |
| #358, #402, tạo khi P13 = 1 | — | Thiếu: nhánh P13 = 1 |
| #234, #254, #270 (CREATION = 0), "linecut" D15–D17 | — | Không bao giờ được tạo; có thể là đối tượng phụ trợ |
| Các cắt ±P1 (D51, D52, D56, D57, D106, D107) | — | Chưa rõ; có thể là cắt raker theo chiều dày bản đế. Cần ảnh Editor |

### Biến trong hộp thoại
Đủ 34 biến của hộp thoại gốc, tên trùng khớp. EXE kiểm tra: không có ô vàng nào.
- Biến tính toán của gốc: P14 (góc), P17 (dày cánh), P18, P25, P26, P33, P35, P42, P47–P50. Plugin tự tính các biến này từ hình học.
- P27, P28, P30, P36, P37 có trong danh sách biến của gốc nhưng không có trong hộp thoại và không dùng trong công thức nào → bỏ qua.

## Theo yêu cầu người dùng (khác hoặc chưa rõ so với bản gốc)
- **FB:** hình chữ nhật, không cắt theo góc nghiêng. Thanh chạy dọc đường hàn, cạnh dài tì vào bản đế. Dài bằng bề rộng cánh đo dọc đường hàn + 15 mỗi đầu (`FlatBarExtend`). Cánh dưới: FB nằm dưới cánh dưới.
- **Bản mã:** hình bình hành, cạnh ngắn ngoài song song cạnh ngắn trong (tì bản đế). Không cắt; đẩy dọc trục raker cho chạm bản đế. Mặc định đặt phía trong.
- **Bu lông:**
  - Mỗi cột song song cạnh ngắn bản mã.
  - P43 (Bolt distance y) và P45 (eV) đo **vuông góc cạnh dài** bản mã. Đo dọc cột nghiêng thì khoảng cách = P43 / cos(góc).
  - Bỏ washer thứ nhất (Washer1).
- **Vát mép hàn:** mặt trên của cả 2 cánh.
- **Bản đế:** Top/Bott theo dầm chính, Left/Right theo điểm xa nhất của raker.

## Giả định
- FB: tiết diện 25 × 9, cạnh tì **chạm** bản đế. Biến gốc D24/D25/D30/D31 (FLAT_BAR–PLATE = P10) có thể là FB cách bản đế P10; cần ảnh Editor để chắc.
- Chiều của gap bản đế: dương = rộng ra ngoài mốc.
- Bản mã mặc định ở phía trong góc primary/secondary khi P24 để trống.

## Chưa làm
- `P13 = 1` (kiểu không có bản mã và bulông): hiện vẫn dựng theo kiểu `P13 = 0`, và ghi cảnh báo vào log.
- Các đối tượng #347, #1121, #1638 và các cắt ±P1 (xem bảng trên).
