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
| #321 FB, tạo khi P52 = 1; #328 FB, tạo khi P52 = 1 và P13 = 0 | 2 thanh `CreateFlatBar` | Có. FB cánh dưới đặt **dưới** cánh dưới (theo người dùng). Profile P55 (biến mới), mặc định `FB25*9`, theo chuỗi "25*9" trong `.uel` |
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
Thêm 5 **biến mới** theo yêu cầu người dùng, không có trong `.uel` gốc:
- **P54:** chiều dài bản mã (tab Gusset & Bolt, 17/11/2026);
- **P55:** profile FB (tab Flange & FB, 17/11/2026);
- **P56:** file bảng tra (tab Base Plate, 17/11/2026);
- **P57:** dùng bảng tra Yes/No (tab Base Plate, 17/11/2026);
- **P58:** nguồn chiều dày bản đế "Thk. source", ô danh sách ngay dưới Mark (tab Base Plate, 18/11/2026). Xem mục dưới.
- Biến tính toán của gốc: P14 (góc), P17 (dày cánh), P18, P25, P26, P33, P35, P42, P47–P50. Plugin tự tính các biến này từ hình học.
- P27, P28, P30, P36, P37 có trong danh sách biến của gốc nhưng không có trong hộp thoại và không dùng trong công thức nào → bỏ qua.

## Theo yêu cầu người dùng (khác hoặc chưa rõ so với bản gốc)
- **FB:** hình chữ nhật, không cắt theo góc nghiêng. Thanh chạy dọc đường hàn, cạnh dài tì vào bản đế. Dài bằng bề rộng cánh đo dọc đường hàn + 15 mỗi đầu (`FlatBarExtend`). Cánh dưới: FB nằm dưới cánh dưới.
  - *(17/11/2026)* FB là **Beam profile P55**, mặc định `FB25*9`. Cạnh rộng (25) nằm trong mặt cánh, vuông góc đường hàn ("quay ngang" như trước). Trước đây FB là ContourPlate `PL9`.
  - Lỗi đã sửa (17/11/2026): bản đầu dựng FB trong mặt phẳng tạm tạo ra khi đang ở hệ cục bộ, nên FB nằm cách component hàng chục mét. Nay về hệ toàn cục trước rồi mới tạo mặt phẳng tạm, và kiểm tra lại vị trí FB sau khi dựng (lệch hơn 1 mm thì ghi CANH BAO).
  - Chiều xoay profile không đoán trước. Thanh được dựng xong thì đọc lại khối đặc; nếu đang dựng đứng thì xoay 90° (FRONT ↔ TOP).
  - Vật liệu mặc định là **SS275**, đúng mặc định của P5 trong `.uel`. Trước đây để trống thì lấy theo vật liệu bản đế.
- **Chiều dày bản đế, P58 "Thk. source" (biến mới, 18/11/2026):** ô danh sách 3 lựa chọn, code ở `Component.ApplyThkSource`.

  | Lựa chọn | Lưu | Chiều dày bản đế |
  |---|---|---|
  | `= Flange Part 1` | 0 | Dày cánh T-girder (`PROFILE.FLANGE_THICKNESS`). Bỏ qua ô P1 và cột `Base Plate Thk` của bảng tra (log có ghi). |
  | `= Flange Part 2` | 1 | Dày cánh raker, tương tự. |
  | `File text` (mặc định) | 2 | Cột `Base Plate Thk` của bảng tra, theo dòng profile raker. Dòng đó không có giá trị thì **dừng**, không dựng. P57 = No thì lấy ô P1 của hộp thoại. |

  - Không đọc được dày cánh của profile thì **dừng** và ghi `LOI`.
  - Component đặt trước khi có P58 coi như chọn `File text`, nên chạy như cũ. Riêng trường hợp bảng tra không có cột `Base Plate Thk` thì nay sẽ dừng.
  - Profile cánh không đều (2 cánh dày khác nhau): Tekla trả về một giá trị `FLANGE_THICKNESS`, chưa kiểm tra lấy cánh nào.
  - Bảng tra có thể có cột `Base Plate Thk Source` (0/1/2) để chọn theo profile.
- **Bản mã, P54 (biến mới):** chiều dài cạnh dài bản mã, đo dọc trục raker. Để trống hoặc 0 thì tự tính như cũ, theo cột bu lông cuối + eH (P44). Khi nhập P54 thì P44 bị bỏ qua; log ghi eH thực tế và cảnh báo nếu bu lông quá sát mép.
- **Vát mép hàn 2 cánh** *(17/11/2026)*: khối cắt dài thêm `2·dU·|u.Z|/|w.Z| + 100` mm, để vẫn cắt hết đường hàn khi raker lệch ngang góc nhọn.
- **Bản mã:** hình bình hành, cạnh ngắn ngoài song song cạnh ngắn trong (tì bản đế). Không cắt; đẩy dọc trục raker cho chạm bản đế. Mặc định đặt phía trong.
- **Bu lông:**
  - Mỗi cột song song cạnh ngắn bản mã.
  - P43 (Bolt distance y) và P45 (eV) đo **vuông góc cạnh dài** bản mã. Đo dọc cột nghiêng thì khoảng cách = P43 / cos(góc).
  - Chỉ giữ 1 washer: bỏ Washer1 và Washer3, giữ Washer2 + Nut1 (17/11/2026).
- **Vát mép hàn:** mặt trên của cả 2 cánh.
- **Bản đế:** Top/Bott theo dầm chính, Left/Right theo điểm xa nhất của raker.
- **Prefix (17/11/2026, `Plugin\CLAUDE.md` mục 4.3):** prefix = phần đứng trước chữ `M` đầu tiên trong prefix của **main part của assembly chứa part đó**, cộng giá trị ô Mark. Gốc dùng thẳng giá trị ô.
  - Bản đế (P4) và bản mã (P21) hàn vào T-girder, nên theo main part của assembly Part 1.
  - FB (P11) hàn vào raker, nên theo main part của assembly Part 2.
  - Ví dụ: main part có prefix `B1D-M`, P21 = `RGP`, thì bản mã có prefix `B1D-RGP`.
  - Ô Mark nay chỉ là **phần đuôi**. Mặc định là `RSF` / `RGP` / `RFB`, lấy phần chung của preset `KK350` (`B1D-RSF`…) và `#KRAC` (`1RSF`…).
  - Một component `_DEV` đã đặt trong model mà còn lưu Mark đầy đủ, như `B1D-RSF`, thì sẽ ra prefix bị lặp phần đầu. Phải sửa ô Mark thành phần đuôi rồi bấm Modify.

## Bảng tra theo profile Part 2 (P56, P57, biến mới 17/11/2026)
Bản đế, bản mã và bu lông thường đổi theo profile của raker, nên component có bảng tra để lấy các giá trị này.
- **File:** ô P56 ở tab Base Plate, kiểu `file_in`, có nút **"..."** để chọn file (18/11/2026). Trong `UEL_TESTER.exe`, nút "..." mở sẵn thư mục model.
  - Để trống thì dùng `RAKER+T_GIRDER.txt`.
- **QUY TẮC BẮT BUỘC: mặc định và ưu tiên dùng file txt trong thư mục model** (người dùng yêu cầu 18/11/2026). Dữ liệu bảng tra quyết định kích thước bản, bu lông, nên không được để xảy ra sơ sót hay nhầm file. Code ở `_Common\Core\LookupTable.cs`, hàm `ResolveInModel`. Mỗi lần chạy (Insert/Modify), plugin chọn file như sau:
  1. Lấy **tên file** trong P56, bỏ phần thư mục; trống thì lấy `RAKER+T_GIRDER.txt`.
  2. Thư mục model đã có file **cùng tên** thì **luôn dùng file trong model**, kể cả khi P56 chọn một file ở chỗ khác. Lúc đó log ghi rõ "dung ban trong model, KHONG dung <file ngoài>".
  3. Model chưa có thì tìm file nguồn: đường dẫn đầy đủ thì dùng đúng file đó; chỉ có tên thì tìm `XS_PROJECT` → `XS_FIRM` → thư mục plugin. Tìm thấy thì **chép 1 bản vào thư mục model** rồi dùng bản trong model.
  4. **Không bao giờ ghi đè** file đã có trong model.
  5. Mỗi lần chạy, log ghi **đường dẫn đầy đủ** của file đã dùng, dòng khớp và các biến đã gán. Không chép được vào model (ví dụ thiếu quyền) thì log ghi `CANH BAO` và dùng tạm file nguồn.
- **Tắt plugin, tắt model rồi mở lại thì sao:** ô P56 được lưu cùng component trong model. Bước 2 ở trên luôn tìm thấy bản đã chép, nên plugin vẫn dùng file trong model (ví dụ `F:\TUANANH\SAMPLE V2020\RAKER+T_GIRDER.txt`).
  - Xoá hoặc di chuyển file txt bên ngoài: không ảnh hưởng.
  - Sửa file txt bên ngoài: model **không** nhận thay đổi.
  - Chép cả thư mục model sang máy khác: bảng tra đi theo model.
- **Muốn đổi bảng tra của model:** sửa thẳng file trong thư mục model rồi bấm Modify, hoặc xoá file đó để lần chạy sau chép lại từ file nguồn. Sau khi sửa, kiểm tra log xem đã dùng đúng file và đúng dòng chưa.
- **Mẫu:** [RAKER+T_GIRDER.txt](RAKER+T_GIRDER.txt). `install.bat` chép file này vào thư mục plugin nếu ở đó chưa có. Lần đầu chạy với P56 trống, file mẫu được chép vào model.
- **Định dạng:**
  - phân cách TAB, dòng `#` là ghi chú;
  - cột đầu là profile raker. Không phân biệt hoa thường; `x` được coi như `*`;
  - các cột sau đặt **tên dễ đọc** (18/11/2026), như `Base Plate Thk`, `Gusset Thk`, `Bolt Size`, `Bolt Qty Y`, `Bolt Dist Y`…. Danh sách đủ ở đầu file mẫu, khai báo bằng `[UelParam("P1", Table = "Base Plate Thk")]` trong `Params.cs`. Không phân biệt hoa thường, dấu cách tuỳ ý. Ghi tên biến (`P1`, `P38_diameter`) hoặc `Base Plate Thk (P1)` cũng được;
  - cột sai tên thì bị bỏ qua, log ghi `CANH BAO` kèm danh sách tên đúng;
  - ô trống thì giữ giá trị hộp thoại;
  - dòng `*` là dòng mặc định;
  - lưu bằng cp949 hay UTF-8 đều được, nên lưu từ Excel dạng "Text (Tab delimited)" là dùng được.
- **Ưu tiên:** cột có trong bảng **đè** giá trị hộp thoại, giống công thức `fVF` của `.uel`. P57 "Dung bang tra" = No thì chỉ dùng hộp thoại.
- **Không tra được thì không dựng** (người dùng 18/11/2026). Khi P57 = Yes, các trường hợp sau đều hiện hộp thông báo và component **không** được tạo:
  - không tìm thấy hoặc không đọc được file bảng;
  - profile raker không có trong bảng, và bảng cũng không có dòng `*`;
  - một ô không đúng kiểu, ví dụ `M22` ở cột `Bolt Size`.

  Trước đây các trường hợp này dùng giá trị hộp thoại và chỉ ghi `CANH BAO`. Muốn dựng bằng giá trị hộp thoại thì đặt P57 = No.
- Component đọc lại bảng mỗi lần chạy, nên sửa file xong chỉ cần bấm Modify. Log ghi file đã dùng, dòng khớp và các biến đã gán.
- Bảng mẫu mới có dòng `H350*357*19*19`, lấy từ preset KK350. Preset KK250 chưa rõ profile raker nên dòng đó đang để dạng ghi chú.

## Giả định
- FB: cạnh tì **chạm** bản đế. Kích thước dùng để đặt thanh đọc từ chuỗi profile P55 (số lớn = rộng, số nhỏ = dày); không đọc được thì tạm lấy 25 × 9 và ghi CANH BAO. Biến gốc D24/D25/D30/D31 (FLAT_BAR–PLATE = P10) có thể là FB cách bản đế P10; cần ảnh Editor để chắc.
- Chiều của gap bản đế: dương = rộng ra ngoài mốc.
- Bản mã mặc định ở phía trong góc primary/secondary khi P24 để trống.
- Prefix của Part 1 không có chữ `M`: prefix part mới = nguyên giá trị ô Mark, kèm `CANH BAO` trong log.

## Chưa làm
- `P13 = 1` (kiểu không có bản mã và bulông): hiện vẫn dựng theo kiểu `P13 = 0`, và ghi cảnh báo vào log.
- Các đối tượng #347, #1121, #1638 và các cắt ±P1 (xem bảng trên).
