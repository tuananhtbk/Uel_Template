# Bộ nguyên tắc rút ra từ COL_TO_GIRDER (18/11/2026)

`COL_TO_GIRDER` (cột → dầm H: CGP + bu lông, cánh hàn CJP, WP, FB, scallop) là component thứ hai sau `RAKER+T_GIRDER`.
Các nguyên tắc dưới đây **bổ sung** cho [NGUYEN_TAC_TU_RAKER.md](NGUYEN_TAC_TU_RAKER.md) và [CLAUDE.md](CLAUDE.md). Chỗ nào khác thì file này mới hơn.
Code mẫu: [COL_TO_GIRDER/](COL_TO_GIRDER/README.md).

---

## 1. Cách làm việc với người dùng

1. **Từ ngữ mơ hồ thì hỏi, không đoán.** Ở COL_TO_GIRDER đã đoán sai 2 lần ("làm dài vết cắt", "cắt chưa hết", "nhầm bên") và phải sửa lại.
   Hỏi bằng 2–3 lựa chọn cụ thể, có hình ASCII minh hoạ nếu là hình học.
2. Người dùng nói "quay về kết quả trước" thì khôi phục **đúng** bản trước, không giữ lại phần sửa dở.
3. Sửa code Core xong thì tự chạy `build.bat core <Ten>`. **Bản DEV chỉ build khi người dùng nói "Build DEV".**
   Sửa hộp thoại (thêm ô, đổi nhãn, đổi hình) thì nhắc rằng phải build DEV và cài lại mới thấy trong Tekla.
4. Mỗi lần sửa hình hộp thoại: chạy `VeHinh`, chuyển `.bmp` sang `.png` để **tự xem lại**, rồi chép `dialog.inp` vào `Plugin.cs` bằng script (không chép tay).
5. Không nói "chắc chắn 100%" với điều chưa thử trên model. Ghi rõ phần nào chưa kiểm tra và cần người dùng xem gì trong log.

## 2. Gộp các bản `.uel` tuỳ biến theo dự án

1. Nhiều file `.uel` thực chất là một component sửa cho từng dự án → **một plugin**, mỗi chỗ khác nhau thành **một biến mới** (số P kế tiếp chưa dùng).
2. Biến mới **để trống = chạy như `.uel` gốc**. Mỗi dự án lưu 1 preset.
3. README có bảng: biến mới ↔ giá trị của từng bản `.uel`.

## 3. Hộp thoại

1. **Một tab chính** chứa toàn bộ thông số hình học (gộp các tab gốc lại nếu người dùng yêu cầu; tên biến giữ nguyên nên model cũ vẫn đọc được).
2. Hình minh hoạ vẽ **theo ảnh component gốc** người dùng gửi: nét mảnh 1 px, mũi tên nhỏ, **ít chữ**, nhãn ngắn. Có thể ghép mặt đứng + mặt bằng trong 1 hình.
   **Màu theo phong cách hình mặc định của Tekla** (người dùng 18/11/2026, đã áp cho mọi component; mẫu: `TEMPBEAM-MOMENT\_hinh\VeHinh.cs`):
   nền `243,243,243`; viền `28,40,70`; dầm / cột `108,156,206`, cánh `84,128,178`; tấm chính (CGP, GP, WP, bản mã, bản đế) `255,204,112`;
   tấm phụ (FB, ring, SF ngang) `245,170,80`; bu lông = tròn trắng + dấu cộng, viền xanh đậm; kích thước và mũi chỉ đen `30,30,30`;
   vùng cắt gạch đỏ sẫm `170,40,40`; nét khuất đứt.
3. Ô điền đặt ngay cạnh kích thước; ô không phải kích thước (vật liệu, class, zone…) đặt thành cột bên phải; ô phụ (cắt Y/N, file bảng tra) đặt thành dải dưới hình.
4. Ô có giá trị lấy từ bảng tra: nhãn thêm **"(auto)"**.
5. Lựa chọn nhiều trường hợp: dùng **ô danh sách** của Tekla (giá trị lưu là số thứ tự 0, 1, 2…):
   ```
   attribute("P83", "", option, "%s", none, none, "0.0", "0.0", x, y, w)
   {
       value("+X / +Y", 1)
       value("+X / -Y", 0)
   }
   ```
   `VeHinh.cs` có hàm `OptionRow`. EXE đọc được kiểu này.
6. **Tab ` Advanced `** (không hình) cho mọi thuộc tính của part sinh ra: Name, Class (màu), Finish, UDA (`TEN=GIA_TRI; ...`), và profile / vật liệu / số bắt đầu nếu tab chính chưa có.
   Không có prefix / số bắt đầu **assembly** cho part phụ (assembly theo main part).

## 4. Bảng tra

1. Bảng mặc định tên **`<Ten>.txt`** (đặt ở `Plugin\<Ten>\`, `install.bat` chép vào thư mục plugin của Tekla). Không thấy thì mới tìm tên bảng gốc của `.uel`.
2. Ô chọn file (`file_in`) để đổi bảng theo dự án. Nhập file mà không thấy → **dừng**, không lùi về bảng mặc định.
3. Bảng bắt buộc của `.uel` đọc **theo vị trí cột** như `fVF` (cột đếm từ 1). Cột tuỳ chọn mới đọc **theo tên tiêu đề**, đặt **sau** các cột bắt buộc. Ô trống = dùng hộp thoại / công thức; ô hộp thoại có nhập thì thắng bảng.
4. File chuẩn: đầu file có dòng `#` ghi chú từng cột, rồi dòng tiêu đề, rồi dữ liệu; TAB, CRLF, **chỉ chữ không dấu**; bỏ dòng trống, dòng trùng.
5. So khoá profile: không phân biệt hoa thường, `x` = `*`, bỏ `-` ngay sau chữ cái đầu (`H-500x200…` = `H500*200…`), giữ `-` giữa 2 số (`HUN1100-792…`).
6. Bảng lưu trong model (`ResolveInModel`), không ghi đè file đã có. Khi đổi bảng mặc định, nhắc người dùng xoá bản cũ trong model / thư mục plugin.

## 5. Hình học

1. **Đo trên mặt tiếp xúc, theo phương nằm ngang vuông góc trục cột** (dầm nghiêng / xiên / vặn):
   - không đo theo một phương nghiêng rồi chiếu điểm chân cột vào — chiều cao cột sẽ lọt vào phép đo;
   - lấy điểm mép cánh chạm mặt cột, đo tới mép cột.
2. **Tấm trên Part 2 nghiêng**: hình bình hành như bản mã RAKER; chiều dài (L3) và chiều cao đo **vuông góc** cạnh tương ứng; cột bu lông song song cạnh ngắn; khoảng mép bu lông (e) đo vuông góc cạnh ngắn ngoài.
3. Không tự cộng thêm đại lượng của `.uel` (vd. `+P57` chiều dày ring vào vị trí bu lông) khi chưa kiểm chứng — đo trên model nếu người dùng gửi kích thước.
4. **Khối cắt song song mặt tiếp xúc** (lùi cánh, cắt xuyên cánh): mặt cắt ngang nằm trong mặt phẳng chứa pháp tuyến mặt tiếp xúc và trục Y của dầm, đùn theo phương ngang trên mặt (`SlabCut`). Không đo dải cao độ theo trục nằm trong mặt khi dầm dốc.
5. **Vát mép hàn**: dùng **đúng** `CutBevel` của RAKER (mặt chân 2 mm, khối cắt vượt mặt cánh 3 mm). Người dùng đã thử bỏ mặt chân / kéo dài khối cắt rồi yêu cầu quay về.
   Tấm hàn CJP (vd. WP) vát **mọi cạnh hàn**: cạnh vào cột và cạnh vào mép cánh dầm, cùng kiểu với cánh dầm.
6. **Scallop**: khối cắt chữ nhật dài 200, sâu R, góc phía dầm **bo tròn R** — vẽ cung bằng điểm.
   **Không dùng** `Chamfer CHAMFER_ROUNDING` của Tekla khi bán kính bằng chiều dài cạnh: boolean hỏng, **dầm biến mất**.
   Scallop ở cả cánh trên và cánh dưới.
7. Chi tiết tuỳ chọn (WP…): ghi log **lý do có / không có** từng cái (`WP1: MO va cot tho ra 150 >= 74 mm -> co`).
8. **Part 2 cao thay đổi (dầm haunch, `COL_TO_HAUNCH_GIRDER`, 18/11/2026, chưa thử trên model):** không dựa vào trục / HEIGHT của profile.
   Cắt solid dầm (`Solid.Intersect(LineSegment)`) tại 2 tiết diện gần cột → mặt ngoài 2 cánh → **mỗi cánh một hệ cục bộ** (X dọc cánh đó);
   chi tiết thuộc cánh nào dựng trong hệ của cánh đó, đổi hệ thì về hệ toàn cục trước. Dầm cao đều thì 2 hệ trùng nhau.

## 6. Bu lông

1. Mặc định: **Extra length = 0**, chỉ Washer2 + Nut1.
2. **Lỗ ô van luôn ở tấm (CGP)**, có ô chọn chuyển sang dầm. Cờ `Hole1..5` áp theo **thứ tự tấm Tekla lưu**, Tekla có thể đổi thứ tự theo hình học:
   tạo bu lông → `Select()` → xem `PartToBoltTo` / `PartToBeBolted` → đặt lại cờ cho đúng tấm → `Modify()` (`FixSlotSide`). API không đọc được tấm nào có lỗ ô van, nên phải nhờ người dùng kiểm tra trường hợp nghiêng / vặn.

## 7. Phía đặt tấm (CGP)

1. Phía đặt tấm theo **model**, không theo chiều vẽ cấu kiện: ô danh sách `+X/+Y` (mặc định), `+X/-Y`, `-X/+Y`, `-X/-Y`, `Theo dam`.
   Dầm gần song song trục Y xét dấu X; gần song song trục X xét dấu Y.
2. Luôn có thêm **một ô sửa tay** cho riêng liên kết đó (đảo phía).
3. Bụng gần nằm ngang (không xác định được X/Y) → `CANH BAO`, đặt theo trục riêng của dầm.

## 8. Thuộc tính part

1. Vật liệu tấm phụ (WP…) mặc định **theo vật liệu Part 2**, có ô để đổi.
2. Profile / vật liệu đang là "giả định" (vd. FB `FB25*9`, `SS275`) → đưa ra hộp thoại (tab Advanced).
3. Chi tiết chỉ thuộc một cánh (WP ở cánh trên) thì chỉ ảnh hưởng chi tiết của cánh đó (FB cánh trên dài theo WP, FB cánh dưới giữ nguyên).

## 9. Checklist bổ sung trước khi báo xong

- [ ] Ô lấy từ bảng tra có nhãn "(auto)"; file bảng tra mẫu `<Ten>.txt` đúng định dạng chuẩn (mục 4.4).
- [ ] Đã thử dầm vuông góc, dầm dốc, dầm xiên, dầm vặn — kiểm tra phía tấm, lỗ ô van, điều kiện tạo chi tiết tuỳ chọn.
- [ ] Không dùng chamfer rounding của Tekla cho khối cắt có bán kính bằng cạnh.
- [ ] Tab Advanced có đủ Name / Class / Finish / UDA cho mọi part sinh ra.
- [ ] Log ghi lý do có / không có từng chi tiết tuỳ chọn, phía đặt tấm, file bảng tra đã dùng.
