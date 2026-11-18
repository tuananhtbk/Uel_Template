# Bộ nguyên tắc rút ra từ RAKER+T_GIRDER (18/11/2026)

`RAKER+T_GIRDER` là component chuẩn. Mọi component tiếp theo làm theo các nguyên tắc dưới đây.
- Quy trình 14 bước, điều cấm và quy ước code chi tiết: [CLAUDE.md](CLAUDE.md).
- Code mẫu: [RAKER+T_GIRDER/](RAKER+T_GIRDER/README.md).

Gặp tình huống mà RAKER chưa gặp thì **hỏi trước, không đoán**. Thống nhất xong thì ghi bổ sung vào file này.

---

## 1. Trước khi viết: cần chuẩn bị gì

| Người dùng gửi | Để làm gì |
|---|---|
| Tên file `.uel` gốc và tên component trong catalog | Tên thư mục và tên DLL (giữ nguyên dấu cách, `+ - _`) |
| Part 1 là gì, Part 2 là gì (một hay nhiều cấu kiện) | Câu nhắc chọn, hướng dựng, prefix |
| Ảnh **Custom Component Editor**: cây đối tượng, cửa sổ Variables, mặt phẳng buộc từng biến D, ảnh 3D component gốc kèm tên preset | Hình học. **Không có ảnh thì không đoán** |
| Model mẫu có component gốc đã đặt (nút "Xuat JSON component" của EXE) | Đo thật vị trí, kích thước part |
| Yêu cầu khác bản gốc (biến mới, bảng tra, chi tiết dựng khác…) | Ghi vào README, mục "Theo yêu cầu người dùng" |

Phía Claude đọc `_PHAN_TICH\components\<file>.uel.md` để lấy danh sách biến P/D, công thức, thuộc tính part, preset.

## 2. Tên và biến

1. **Thư mục = tên DLL = tên trong Tekla** = tên file `.uel` gốc bỏ đuôi. Không thêm `PLUGIN`. Không đổi tên sau khi đã dùng trong model.
2. **Giữ đúng tên biến gốc** (`P1`, `P38_diameter`…) ở `[UelParam]` và `[StructuresField]`. Model lưu component theo tên biến.
3. **Biến mới** (người dùng yêu cầu thêm): lấy số P kế tiếp chưa dùng trong `.uel` gốc. Ô để trống thì component phải chạy **như cũ**. Ghi "biến mới" trong `Params.cs`, `Plugin.cs` và README.
   - Ví dụ ở RAKER: P54 chiều dài bản mã (0 = tự tính), P55 profile FB, P56 file bảng tra, P57 dùng bảng tra Yes/No.
4. **Giá trị mặc định** lấy từ một preset có thật của `.uel` (RAKER dùng `KK350`). Vật liệu và profile mặc định cũng theo `.uel` (vd. FB `SS275`). Giá trị không có trong preset thì ghi `// giả định`.
5. Mỗi biến khai báo luôn **tên cột bảng tra dễ đọc**: `[UelParam("P1", Table = "Base Plate Thk")]`. Tên lấy theo nhãn hộp thoại, không trùng nhau. Nhãn bị trùng ("Mat.", "Class", "Mark") thì ghép tên part vào: `Base Plate Mat`, `Gusset Mat`, `FB Mat`.

## 3. Hộp thoại

1. **Luôn có hình minh hoạ** (`_hinh\VeHinh.cs` sinh `bitmaps\*.bmp` và `_hinh\dialog.inp`). Ô nhập đặt **ngay trên hình**, cạnh kích thước mà nó điều khiển. Có đường dim, mũi tên chiều dương. Hình ghi Part 1 / Part 2.
2. Giữ nguyên **tab, tên biến, kiểu** như `.inp` gốc; chỉ đổi vị trí ô và thêm hình. Ô thuộc tính (dày, vật liệu, class, mark) đặt thành cột bên phải hình.
3. Nhãn tự viết dùng **chữ không dấu** (Tekla 18 không Unicode).
4. Ô chọn file dùng kiểu **`file_in`** (có nút "..."): `parameter("", "P56", file_in, text, x, y, w)`.
5. Ô Mark chỉ chứa **phần đuôi** prefix (`RSF`, `RGP`, `RFB`), xem mục 4.
6. Sửa hộp thoại xong phải sửa đủ 3 chỗ: `_hinh\VeHinh.cs`, `_hinh\dialog.inp`, `Plugin.cs`.

## 4. Prefix part mới

- Part mới **hàn vào assembly nào** thì lấy prefix theo **main part của assembly đó**:
  `prefix = phần trước chữ "M" đầu tiên trong prefix của main part + ô Mark`.
  Ví dụ: main part `B1D-M`, Mark `RGP` → `B1D-RGP`.
- RAKER: bản đế và bản mã theo assembly Part 1; FB hàn vào raker nên theo assembly Part 2.
- Code: `PartPrefix.HeadOfAssembly(part, "<ten>")` một lần cho mỗi assembly, rồi `PartPrefix.Make(head, mark)`.
- Main part không có chữ `M`: dùng nguyên ô Mark và ghi `CANH BAO`.
- `.uel` gốc lấy prefix kiểu khác thì làm đúng như gốc và ghi README.

## 5. Hình học (connection có Part 2 nghiêng / lệch / xoay)

Làm theo [CLAUDE.md](CLAUDE.md) mục 4.4. Các bài học đã trả giá ở RAKER:

1. **Luôn về hệ toàn cục trước khi tạo hoặc đặt mặt phẳng khác.** Lỗi đã gặp: FB nằm cách component ~80 m vì tạo mặt phẳng bằng toạ độ toàn cục khi đang ở hệ cục bộ. Đổi `TransformationPlane` thì trả lại trong `finally`.
2. **Kiểm tra lại vị trí sau khi dựng.** Part lệch hơn 1 mm so với chỗ định đặt thì ghi `CANH BAO`.
3. **Chiều xoay profile Beam không đoán trước được.** Dựng xong thì đọc lại khối đặc; sai chiều thì xoay 90°.
4. Mặt tiếp xúc trên Part 1 là **mặt bao ngoài** mà trục Part 2 đi vào. Độ nghiêng đo bằng pháp tuyến mặt đó trong hệ cục bộ của Part 2.
5. Chi tiết trên Part 2 nghiêng: bản mã là **hình bình hành**, cạnh ngắn song song mặt tiếp xúc. Khoảng cách bu lông đo **vuông góc cạnh dài**. Mỗi cột bu lông song song cạnh ngắn.
6. **Không để chi tiết cắm vào mặt nghiêng**: đẩy dọc trục cho tới khi vừa chạm, hoặc dựng lấn vào rồi cắt.
7. Khối cắt (vát mép hàn…) phải **đủ dài** cho cả góc rất nhọn. RAKER cộng thêm `2·dU·|u.Z|/|w.Z| + 100` mm.
8. Bản đế: Top/Bott theo tiết diện Part 1, Left/Right theo điểm xa nhất của Part 2. Gap dương = rộng ra ngoài.
9. Chi tiết một bên (bản mã): mặc định đặt **phía trong** góc Part 1 / Part 2. Người dùng nhập ô L/R thì theo ô đó.
10. Ghi log góc nghiêng, phía đã chọn, kích thước chính và mọi trường hợp đặc biệt.

## 6. Bu lông

- Theo RAKER: chỉ dùng **Washer2 + Nut1** (tắt Washer1, Washer3). Component mới cũng làm vậy, trừ khi người dùng nói khác.
- Lỗ ô van theo biến của `.uel` gốc.

## 7. Bảng tra theo profile Part 2 (khi người dùng yêu cầu)

1. Thêm 2 biến mới: **file bảng tra** (`file_in`, có nút "...") và **dùng bảng tra** (Yes/No, mặc định Yes). Đặt ở tab đầu, cạnh hình.
2. Gọi `LookupTable.ApplyByKey(...)` trên **bản sao** của Params. Bỏ qua chính 2 biến đó. Mẫu ở `RAKER+T_GIRDER\Component.cs`.
3. **BẮT BUỘC: mặc định và ưu tiên dùng file txt trong thư mục model** (người dùng 18/11/2026; dữ liệu rất quan trọng, không được sơ sót hay nhầm file). Luôn gọi `LookupTable.ResolveInModel` (qua `ApplyByKey`), không tự viết đường tìm file khác:
   - model đã có file cùng tên thì luôn dùng file đó, kể cả khi ô file chọn chỗ khác. Log ghi rõ là đang dùng bản trong model;
   - chưa có thì tìm file nguồn (đường dẫn đầy đủ, hoặc `XS_PROJECT` → `XS_FIRM` → thư mục plugin), rồi chép 1 bản vào thư mục model và dùng bản đó;
   - không bao giờ ghi đè file trong model;
   - mỗi lần chạy, log ghi đường dẫn đầy đủ của file đã dùng, dòng khớp và các biến đã gán.
   - README của component phải có mục **"Tắt plugin / tắt model rồi mở lại"**: file nào được dùng, sửa file ngoài thì model không nhận, muốn đổi bảng thì sửa file trong model hoặc xoá để chép lại. Mẫu ở README của RAKER.
   - Quy tắc này áp dụng cho **mọi** bảng tra của component mới, cả bảng bắt buộc của `.uel` gốc (`fVF`). Không bao giờ sửa file gốc trong `File goc\` hay `UEL\`; chỉ chép sang model.
4. Ô file để trống thì dùng `<Ten>.txt`. Đặt bảng mẫu `Plugin\<Ten>\<Ten>.txt`; `install.bat` tự chép vào thư mục plugin của Tekla.
5. Định dạng: TAB, CRLF, dòng `#` là ghi chú. Cột đầu là profile Part 2 (không phân biệt hoa thường, `x` = `*`). Dòng `*` là dòng mặc định. Ô trống = giữ giá trị hộp thoại.
6. **Tiêu đề cột dùng tên dễ đọc** (`Base Plate Thk`, `Bolt Size`…). Phần ghi chú đầu file liệt kê đủ tên, kèm tên biến trong ngoặc. Tên `P1` cũ vẫn được nhận.
7. Giá trị trong bảng **đè** giá trị hộp thoại. Không có dòng khớp thì dùng hộp thoại và ghi `CANH BAO`. Cột sai tên thì bỏ qua, ghi `CANH BAO` kèm danh sách tên đúng.
8. Kèm một **file thử** (`<ten>_THU.txt`) có dòng profile của model mẫu. Ghi rõ "chưa kiểm tra kết cấu".

> Bảng **bắt buộc** của `.uel` gốc (gọi bằng `fVF`) thì khác: không tìm thấy là dừng và báo lỗi, không tự điền số.

## 8. Thử nghiệm

Thứ tự: **EXE trước, DLL sau.**

| Bước | Lệnh / việc | Ghi chú |
|---|---|---|
| 1 | Sửa hình học → `build.bat core <Ten>` | Claude tự làm sau mỗi lần sửa code |
| 2 | Thử bằng `Plugin\_Dev\UEL_TESTER.exe` | Dòng "Core:" phải là `...\UEL Template\Plugin\_Dev\...`, **không** phải bản sao lưu `00\...` |
| 3 | Đổi hộp thoại hoặc thêm biến → `build.bat dev <Ten>` | Chỉ build khi người dùng bảo |
| 4 | Tắt Tekla → cmd **Administrator** → `install.bat dev <bản> <Ten>` | Hộp thoại nằm trong `_DEV.dll`, nên đổi hộp thoại thì **phải cài lại** |
| 5 | Đặt `<Ten>_DEV`, so với component gốc bằng nút **So sánh** của EXE, với từng preset | Kết quả ghi vào README |
| 6 | Sửa `_Common` → `build.bat check` | Chỉ chạy khi người dùng bảo |
| 7 | Phát hành `install.bat <bản> <Ten>` | Chỉ khi người dùng bảo, sau khi đã chạy thật trong Tekla |

Cần biết:
- EXE đọc hộp thoại **thẳng từ `Plugin.cs`**, nên thấy ngay ô mới. Tekla chỉ thấy sau khi cài lại `_DEV.dll`.
- Sửa Core thì chỉ cần `build.bat core` rồi bấm Modify, không phải khởi động lại Tekla.
- Component `_DEV` đã đặt từ trước mà ô Mark còn lưu prefix đầy đủ (vd. `B1D-RSF`) thì prefix bị lặp: sửa ô Mark thành phần đuôi rồi Modify.
- Trong Tekla 18.0, part do plugin tạo trả `GetFatherComponent()` = null. Đây là đặc điểm của API, không phải lỗi.
- Log: khung log của EXE và `%TEMP%\UEL_PLUGINS.log`.

## 9. Hai máy

- Làm xong một phiên: commit và **push** lên `origin/main`. Máy kia `git pull`.
- DLL không lưu trong git: sang máy kia phải `build.bat` rồi `install.bat` lại.
- Cuối phiên viết file tóm tắt (`TOM_TAT_PHIEN_<ngày>.md`) để máy kia làm tiếp.

## 10. README của mỗi component

Theo mẫu `RAKER+T_GIRDER\README.md`, có đủ các mục:
- tên trong Tekla, Part 1 / Part 2 là gì, bảng file;
- **So sánh với component gốc**: từng part / cắt / bu lông của gốc ↔ hàm trong plugin ↔ trạng thái;
- **Biến trong hộp thoại**: đủ biến gốc chưa, biến mới nào;
- **Theo yêu cầu người dùng**: chỗ khác bản gốc;
- **Bảng tra** (nếu có);
- **Giả định**;
- **Chưa làm**.

## 11. Checklist trước khi báo xong

- [ ] Đủ biến của `.inp` gốc, tên khớp; EXE không có ô vàng.
- [ ] Có hình minh hoạ, ô nằm đúng chỗ trên hình.
- [ ] Mỗi biến có tên cột bảng tra (`Table = "..."`).
- [ ] Mọi bảng tra tìm qua `LookupTable.ResolveInModel` (ưu tiên file trong model, chép 1 bản, không ghi đè). Không gọi thẳng `LookupTable.Resolve`. README có mục "Tắt plugin / tắt model rồi mở lại".
- [ ] Prefix theo main part của assembly.
- [ ] Về hệ toàn cục trước khi tạo mặt phẳng; có kiểm tra vị trí sau khi dựng.
- [ ] Đã thử ít nhất một trường hợp nghiêng và một trường hợp lệch ngang (nếu Part 2 có thể nghiêng).
- [ ] `build.bat core` OK; `build.bat check` OK khi người dùng cho chạy.
- [ ] README đủ mục; cập nhật bảng component trong `Plugin\README.md`.
- [ ] Báo rõ phần nào **đã** chạy trong Tekla, phần nào **chưa**, còn những giả định nào.
