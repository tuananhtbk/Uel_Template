# Quy tắc viết component plugin trong thư mục `Plugin\`

Áp dụng cho mọi component chuyển từ `.uel` (custom component Tekla 18.0) sang plugin C#, từ `RAKER+T_GIRDER` trở về sau.
Người và Claude đều làm theo file này. Cách dùng lệnh xem [README.md](README.md).
**Bản tóm tắt nguyên tắc và bài học rút ra từ RAKER+T_GIRDER: [NGUYEN_TAC_TU_RAKER.md](NGUYEN_TAC_TU_RAKER.md). Đọc trước khi làm component mới.**
**Bổ sung từ COL_TO_GIRDER (18/11/2026): [NGUYEN_TAC_TU_COL_TO_GIRDER.md](NGUYEN_TAC_TU_COL_TO_GIRDER.md)** — hộp thoại 1 tab + tab Advanced, nhãn "(auto)", ô danh sách, bảng tra `<Ten>.txt` chuẩn, đo trên mặt tiếp xúc, scallop, lỗ ô van, phía đặt tấm theo model. Chỗ khác với RAKER thì theo file này.
Trao đổi với người dùng bằng **tiếng Việt**.
Người dùng mô tả yêu cầu theo [MAU_YEU_CAU.md](MAU_YEU_CAU.md): từ vựng hướng/trục, mẫu sửa, mẫu component mới, JSON hình dạng chuẩn.
Khi có file JSON hình dạng chuẩn thì coi đó là chuẩn: dựng theo đúng file đó, chỗ nào khác bản gốc thì ghi vào mục "Giả định".
Khi người dùng nói tên model: đọc trực tiếp model qua API (chỉ đọc) để đo, không đoán.
File `_Dev\Export\*.json` (nút "Xuat JSON component" của EXE) chứa đủ dữ liệu một component: inputs, frames, geometry, attributes + presets + .inp, children kèm khối đặc. Định dạng mô tả ở đầu `_Tester\ComponentExport.cs`. Tekla mở nhiều cửa sổ thì đặt `SESSIONNAME` theo pipe (xem `_Tester\Program.cs`).

## 1. Điều cấm

1. **Không sửa, đổi tên hay đổi encoding** các file `.uel`, `.txt`, `.dat` trong `UEL Template\`. Đó là tài liệu gốc.
2. **Không đoán hình học.** File `.uel` chưa cho biết vị trí từng part, biến D nào buộc vào mặt phẳng nào, part cắt nằm ở đâu.
   - Những thông tin này phải lấy từ ảnh chụp **Custom Component Editor** do người dùng gửi.
   - Nếu chưa có ảnh: dừng lại, liệt kê đúng những ảnh cần chụp (mục 2, bước 3) rồi hỏi người dùng.
   - Nếu buộc phải tạm giả định một giá trị:
     - ghi chú `// giả định` ngay tại dòng code;
     - đưa thuộc tính vào nhóm cuối cùng của `Params.cs`, tên `Category("<số>. Gia dinh (khong co trong hop thoai)")`, như `RAKER+T_GIRDER\Params.cs`;
     - ghi vào mục "Giả định" trong README của component.
3. **Không đổi tên thư mục component** sau khi đã dùng trong model, vì model lưu component theo tên đó. Không thêm hậu tố `PLUGIN` hay `_PLUGIN`.
4. **Không đặt code ngoài `Plugin\`.** Thư mục `ALL IN ONE\Plugins\` là khung cũ, chỉ để tham khảo.
5. **Không phát hành bản `_DEV`.** Chỉ phát hành `18.0\<Ten>.dll` và `2020+\<Ten>.dll`.
6. Chỉ sửa `_Common\` khi thay đổi đó dùng chung cho mọi component. Sửa xong phải chạy `build.bat check`, rồi cài lại bản DEV cho mọi bản Tekla đang dùng.
7. Không coi một component là "xong" khi người dùng chưa chạy thử nó trong Tekla.

## 2. Quy trình cho mỗi component

| Bước | Việc làm | Kết quả |
|---|---|---|
| 1. Chọn | Đi theo thứ tự ở `HUONG_DAN_CHUYEN_MAY.md` mục 7: dễ, rồi trung bình, rồi khó. Component kế tiếp là `0-PL WELD`. Xem mục 4 (component trùng tên) và mục 5 (bảng tra thiếu) để biết dùng bản `.uel` nào và còn thiếu bảng nào. | Tên file `.uel` gốc |
| 2. Đọc phân tích | Đọc `_PHAN_TICH\components\<file>.uel.md`: Dialog (.inp), Variables, Distance variables (D), Part attribute bindings, các preset `.p_*`. Có thể chạy thêm `python -I _tools/uel_tool.py formulas "<file>.uel"`. | Danh sách P/D, công thức, thuộc tính part |
| 3. Xin ảnh Editor | Xin người dùng chụp trong Custom Component Editor: (a) cây đối tượng (part, cắt, bu lông, hàn); (b) cửa sổ Variables; (c) mặt phẳng mà từng biến D buộc vào; (d) ảnh 3D component gốc dựng trên một cặp dầm mẫu, kèm tên preset đã dùng. | Hình học đã xác nhận |
| 4. Tạo thư mục | Chép `_Template` thành `Plugin\<tên file .uel gốc, bỏ đuôi>`, giữ nguyên dấu cách và ký tự `+ - _`. Trong 4 file `.cs`, đổi `Xxx` thành một tên ngắn viết PascalCase, không dấu (ví dụ `PlWeld`). | Thư mục component |
| 5. `Params.cs` | Xem mục 3. | |
| 6. `Plugin.cs` + hình | Vẽ hình minh hoạ bằng `_hinh\VeHinh.cs` theo mô tả và `.uel` gốc, đặt ô điền lên hình (mục 4.2). Xem thêm mục 3. | `bitmaps\*.bmp`, hộp thoại |
| 7. `Component.cs` | `Title` là tên component trong catalog gốc (ví dụ `0-PL WELD`). `SourceUel` là đường dẫn file `.uel`. Câu nhắc chọn và thứ tự chọn cấu kiện phải giống bản gốc. | |
| 8. `Builder.cs` | Xem mục 3. | |
| 9. README | Viết `Plugin\<Ten>\README.md` theo mẫu `RAKER+T_GIRDER\README.md`, gồm: tên trong Tekla, Part 1 / Part 2 là gì (mục 4.1), bảng file, tài liệu liên quan, **Giả định**, **Chưa làm**, **Kết quả so sánh**. | |
| 10. Build | Chạy `build.bat dev <Ten>`, sau đó `build.bat check`. Mọi bản Tekla (18.0 đến 2025) đều phải ra **OK**. | |
| 11. Cài | Tắt Tekla, mở cmd bằng quyền Administrator, chạy `install.bat dev <bản> <Ten>`. | `<Ten>_DEV` có trong catalog |
| 12. So sánh | Dùng nút **So sánh** trên `UEL_TESTER.exe` (README mục 3). Đặt component gốc và bản `_DEV` lên 2 cặp cấu kiện copy, nạp cùng một preset. Lặp lại với **từng preset** và **từng nhánh `if`** / `CREATION`. | File `_Dev\SoSanh\*.txt` |
| 13. Sửa | Sửa code, chạy `build.bat core <Ten>`, bấm Modify, so lại. Làm đến khi kết quả là `GIONG NHAU`, hoặc mọi dòng lệch đều đã được giải thích trong README. | |
| 14. Ghi nhận | Cập nhật bảng component trong `Plugin\README.md` và `HUONG_DAN_CHUYEN_MAY.md` mục 8, ghi trạng thái: *chưa thử* / *đã so sánh* / *đã dùng thật*. | |

## 3. Quy ước code

### Tên và thông số
- **Tên biến phải giữ đúng như `.uel` gốc.** `[UelParam("P7")]` trong `Params.cs` và `[StructuresField("P7")]` trong `Plugin.cs` phải trùng nhau và trùng tên biến gốc. Tên thuộc tính C# đặt theo nghĩa, ví dụ `PlateThk`.
- **Biến mới** mà người dùng yêu cầu thêm (không có trong `.uel` gốc):
  - đặt tên P tiếp theo, chưa dùng trong `.uel` gốc (với RAKER: P54, P55);
  - ghi chú "biến mới" trong `Params.cs`, `Plugin.cs` và README;
  - để trống thì component phải chạy như cũ.
- **Kiểu dữ liệu:**

  | Kiểu trong `.inp` | Kiểu C# |
  |---|---|
  | Yes/No (0/1) | `bool` trong Params, `int` trong Plugin |
  | integer | `int` |
  | distance, angle | `double` |
  | text, material, profile | `string` |

- **Giá trị mặc định** lấy từ một preset có thật trong file `.md`. Ghi tên preset đó ở đầu `Params.cs`. Giá trị nào không có trong preset thì đánh dấu là giả định.
- Người dùng có thể đổi mặc định bằng nút "Ghi bang thong so vao Params.cs" của EXE (18/11/2026). Dòng đã đổi có ghi chú `EXE dd/MM/yyyy: P4 65 -> 55`. Gặp ghi chú này thì coi giá trị mới là ý người dùng, không trả về mặc định `.uel`, và đưa vào mục "Giả định" của README nếu chưa có. Để EXE ghi được, giữ phép gán mặc định dạng `Ten = <hằng>;` trong hàm khởi tạo, và ô "trống = tính" có `(trong` trong `DisplayName`.

### `Plugin.cs`
- Phần `.inp` lấy từ `_hinh\dialog.inp`, do `VeHinh.cs` sinh ra (có hình minh hoạ, xem mục 4.2). Tên biến, kiểu, nhãn và tab giữ như `.inp` gốc. Khi chép vào: nhân đôi dấu `"`, và thay tên trong `joint(...)` hoặc `detail(...)` bằng `""" + PluginName + @"""`.
- Chọn loại theo mã trong `_ec_uel.txt`:
  - `-1000` (connection): `SECONDARYTYPE_ONE` hoặc số phụ tương ứng;
  - `-100` (detail): `SECONDARYTYPE_ZERO` + `DetailType`.
- File này **không được dùng lớp nào của Core**. Mọi thứ đi qua ranh giới Core ↔ Plugin/EXE chỉ dùng kiểu của .NET hoặc của Tekla.

### `Builder.cs`
- Ghi lại công thức gốc ngay cạnh code, giữ tên biến. Ví dụ: `// D12.PLATE.BEAM = 0.5*(P34-1)*P43+P45`.
- Gán `PROFILE`, `MATERIAL`, `CLASS`, `SERIE` (prefix) cho part đúng như mục "Part attribute bindings". Prefix lấy theo main part (mục 4.3).
- `CREATION` là điều kiện có tạo part hay không, phải làm đủ.
- Nhánh nào chưa làm: ghi `Logger.Log("CANH BAO: ...")`, và ghi vào mục "Chưa làm" trong README.
- Con số hình học **không phải biến `.uel`** (khoảng vượt khối cắt, khoảng dư, ngưỡng bỏ qua…) thì khai báo trong khối `// <KNOBS>` … `// </KNOBS>` ở đầu lớp, dạng `internal static double Ten = 3.0;  // [min..max, buoc] mo ta`, để chỉnh được bằng tab Knobs của EXE (xem `README.md` mục 3). Không dùng `const`/`readonly` cho các số này. Biến P/D của `.uel` không đưa vào KNOBS.
- Mỗi đối tượng `Insert()` thành công thì thêm vào `created`, để EXE xoá được.
- Đầu mỗi lần dựng, ghi thông số vào log bằng `Logger.Log("Thong so: " + _p)`.
- Nếu đổi `TransformationPlane` thì phải trả lại plane cũ trong khối `finally`.

### Bảng tra `fVF(...)`
- **BẮT BUỘC cho mọi bảng tra (cả bảng `fVF` của `.uel` gốc lẫn bảng tuỳ chọn), người dùng 18/11/2026: mặc định và ưu tiên dùng file txt trong thư mục model.** Dữ liệu bảng tra rất quan trọng, không được sơ sót hay nhầm file. Luôn tìm file qua `LookupTable.ResolveInModel`, không tự viết đường tìm khác:
  - model có file cùng tên thì luôn dùng file đó;
  - chưa có thì tìm file nguồn (`XS_PROJECT` → `XS_FIRM` → thư mục plugin), chép 1 bản vào model rồi dùng bản trong model;
  - không ghi đè file trong model;
  - log ghi đường dẫn đầy đủ của file đã dùng.
  - README của component có mục "Tắt plugin / tắt model rồi mở lại" theo mẫu RAKER.
- File dùng **cp949**, phân cách **TAB**, xuống dòng **CRLF**. Không sửa hay đổi encoding file nguồn (`File goc\`, `UEL\`, `XS_FIRM`); chỉ chép sang model.
- Dùng hàm chung `_Common\Core\LookupTable.cs` (có từ 17/11/2026):
  - `Load` / `FindRow` để tra kiểu `fVF`;
  - `ApplyByKey` để gán cả dòng vào Params, theo tiêu đề cột là tên biến P.
  - Ví dụ: `RAKER+T_GIRDER\Component.cs`.
- Hàm này đọc được cả cp949 lẫn UTF-8. Khoá được chuẩn hoá: không phân biệt hoa thường, bỏ dấu cách, `x` giữa 2 chữ số thành `*`.
- **Không tra được thì không dựng** (người dùng 18/11/2026, áp cho MỌI component, cả bảng bắt buộc lẫn bảng tuỳ chọn đang bật):
  - không tìm thấy file, không đọc được, không có dòng khoá (girder size / profile Part 2), thiếu ô bắt buộc, hoặc ô không đúng kiểu (vd. `M24` ở cột số);
  - cỡ bu lông không có trong công thức tra của `.uel` (vd. P24/P26 của COL_TO_GIRDER chỉ có M16, M20, M22, M24, M27);
  - → gọi `Logger.Error("...")` (ghi rõ tên file, khoá, cột) rồi `return false` **trước khi tạo bất kỳ đối tượng nào**. `Entry.Build` sẽ hiện **một hộp thông báo** liệt kê các lỗi (`_Common\Core\Logger.cs`). Không tự điền số, không lùi về giá trị hộp thoại.
  - Ô **trống** ở cột tuỳ chọn vẫn là "dùng giá trị hộp thoại / tự tính" (vd. `e`, `L1`, `tip`, Gusset Length).
- **Bảng tuỳ chọn theo profile Part 2** (người dùng thêm, như RAKER P56/P57): cột có trong bảng thì **đè** giá trị hộp thoại. Muốn tắt bảng cho một liên kết thì đặt ô "Dung bang tra" = No (khi đó không tra, không báo lỗi).
- Bảng mẫu đặt tại `Plugin\<Ten>\<Ten>.txt`. `install.bat` chép bảng này vào thư mục plugin của Tekla nếu ở đó chưa có.
- Bảng tuỳ chọn có ô chọn file: ô đó chọn file có đường dẫn đầy đủ thì file đó chỉ là **nguồn** cho lần chép đầu. Model đã có file cùng tên thì vẫn dùng bản trong model (theo quy tắc bắt buộc ở trên).
- **Tên cột bảng tra dễ đọc** (người dùng 18/11/2026): mỗi biến trong `Params.cs` khai báo `[UelParam("P1", Table = "Base Plate Thk")]`, tên theo nhãn hộp thoại, không trùng nhau. File bảng mẫu dùng tên này làm tiêu đề và liệt kê đủ tên ở phần ghi chú đầu file. Tên biến P vẫn được nhận.
- Ô chọn file bảng tra dùng kiểu `file_in` trong `.inp` (có nút "..."), ví dụ `parameter("", "P56", file_in, text, x, y, w)`.

### Ngôn ngữ và tương thích
- Viết bằng **C# 4 / .NET 4.0**, vì build bằng csc v4. **Không dùng:**
  - `$"..."`, `?.`, `nameof`;
  - thân hàm dạng `=>`;
  - `out var`, tuple;
  - property có giá trị khởi tạo (`{ get; set; } = ...`).
- Chuỗi hiện trong Tekla (câu nhắc, log, nhãn hộp thoại tự viết) dùng **không dấu**, vì Tekla 18 không phải Unicode. Comment trong code viết có dấu được (file UTF-8).
- Chỉ dùng API Tekla có ở cả 18.0 lẫn 2025. `build.bat check` sẽ báo lỗi nếu dùng sai.

## 4. Quy tắc riêng của người dùng (17/11/2026)

### 4.1. Part 1 và Part 2
- **Part 1 = primary** (main part, cấu kiện chọn đầu tiên). **Part 2 = secondary**, có thể là một hoặc nhiều cấu kiện.
- Trong README, comment, log, hình minh hoạ và khi trao đổi với người dùng đều gọi là **Part 1 / Part 2**. Có thể ghi thêm nghĩa trong ngoặc, ví dụ "Part 1 (T-girder)".
- Part 2 có nhiều cấu kiện: dựng lần lượt cho từng cấu kiện. Mỗi cấu kiện có hệ toạ độ riêng, tính theo mục 4.4.

### 4.2. Hình minh hoạ hộp thoại: luôn có
- **Mỗi component đều có hình minh hoạ.** Ô điền của biến đặt **ngay trên hình**, cạnh kích thước, chi tiết hoặc hướng mà biến đó điều khiển, để dễ hình dung và dễ sửa.
- Hình vẽ **dựa vào mô tả của người dùng và `.uel` gốc**, không vẽ theo tưởng tượng. Nguồn: `.inp` gốc (tab, nhãn), công thức P/D trong `_PHAN_TICH\components\<file>.uel.md`, ảnh Custom Component Editor. Chi tiết nào chưa rõ thì vẽ theo giả định và ghi vào mục "Giả định" trong README của component.
- Làm theo mẫu `RAKER+T_GIRDER\_hinh\VeHinh.cs` (Plugin\README.md, mục "Hình minh hoạ trong hộp thoại"). Chương trình này vẽ `bitmaps\*.bmp` và sinh luôn `_hinh\dialog.inp`, nên ô điền nằm đúng chỗ trên hình.
- Trên hình ghi Part 1 và Part 2. Kích thước vẽ bằng đường dim, có mũi tên chỉ chiều dương.
- Bố trí lại hộp thoại thì **chỉ đổi vị trí ô và thêm hình**. Phải giữ nguyên tên biến `P..`, kiểu, giá trị mặc định và các tab của `.uel` gốc, vì model lưu component theo tên biến.

### 4.3. Prefix của part mới: theo main part của assembly chứa nó
- Áp dụng cho component dạng connection và detail. Part mới **thuộc assembly nào** (hàn vào part nào) thì lấy prefix theo **main part của assembly đó**:
  - hàn vào Part 1, như bản đế hay bản mã của RAKER, thì theo main part của assembly Part 1;
  - hàn vào Part 2, như FB hàn vào raker, thì theo main part của assembly Part 2.
- Công thức giống công thức người dùng hay dùng trong `.uel`:
  ```
  prefix = mid(P99, 0, find(P99, "M")) + <giá trị ô Mark trong hộp thoại>
  ```
  Trong đó `P99 = fP(proSERIE, <main part>)` là part prefix của main part. Công thức lấy phần đứng **trước chữ `M` đầu tiên** (phân biệt hoa thường), rồi nối thêm giá trị ô Mark.
  - Ví dụ: Part 1 có prefix `B1D-RM`, ô Mark = `GP`, thì prefix part mới = `B1D-RGP`.
- Trong C#: dùng hàm chung `_Common\Core\PartPrefix.cs`.
  - Ở đầu lần dựng, gọi `PartPrefix.HeadOfAssembly(part, "<tên>")` một lần cho mỗi assembly. Hàm này đọc `GetAssembly().GetMainPart()`.
  - Mỗi part tạo ra thì gán prefix `PartPrefix.Make(head, <ô Mark>)`. Trường hợp cắt theo `"-"` và giữ luôn dấu đó: dùng `Head(part, "-", true)`. Xem cách dùng ở `RAKER+T_GIRDER\Builder.cs`.
- Ô Mark trong hộp thoại và giá trị mặc định trong `Params.cs` chỉ chứa **phần đuôi** (`GP`, `SF`, `FB`…), không chứa phần lấy từ main part.
- `.uel` gốc lấy prefix từ part khác (`fP(proSERIE, ...)` trỏ vào Part 2, hoặc cắt theo ký tự khác `"M"`, ví dụ `"-"`) thì **làm đúng như `.uel` gốc**. Ghi rõ điều này trong README.
- **Giả định, cần người dùng xác nhận:** prefix của main part không có chữ `M` thì dùng nguyên giá trị ô Mark, và ghi log `CANH BAO`.
- `RAKER+T_GIRDER` đã làm theo quy tắc này từ 17/11/2026:
  - P4 (bản đế) và P21 (bản mã) lấy theo assembly Part 1; P11 (FB) lấy theo assembly Part 2;
  - mặc định của các ô này là `RSF`/`RGP`/`RFB`.

### 4.4. Connection có Part 2 nghiêng hoặc vặn so với Part 1: chuẩn là `RAKER+T_GIRDER`
Mọi connection có Part 2 nghiêng, lệch ngang hoặc xoay quanh trục so với Part 1 đều làm theo cách của `RAKER+T_GIRDER\Builder.cs`. Đọc file đó và `RAKER+T_GIRDER\README.md` trước khi viết.
1. **Làm việc ở hệ toàn cục**, rồi dựng một hệ cục bộ tại đầu Part 2. Đổi `TransformationPlane` thì trả lại plane cũ trong `finally`.
   - **Luôn về hệ toàn cục** (`SetCurrentTransformationPlane(new TransformationPlane())`) **trước khi** tạo hoặc đặt một mặt phẳng khác.
   - Nếu đang ở hệ cục bộ mà tạo mặt phẳng mới bằng toạ độ toàn cục, Tekla sẽ hiểu toạ độ đó theo hệ cục bộ. Lỗi này đã gặp ở RAKER (17/11/2026): FB nằm cách component hàng chục mét.
2. **Mặt tiếp xúc trên Part 1** = mặt bao **ngoài** của tiết diện Part 1 mà trục Part 2 đi vào (`EnvelopeEntry`, giao tia với hộp bao). Pháp tuyến hướng về phía Part 2. Mặt bao có thể là mặt ngoài cánh, hoặc mặt phẳng qua mép 2 cánh.
3. **Hệ cục bộ:**
   - gốc A = giao trục Part 2 với mặt tiếp xúc (mặt trên bản đế, nếu có);
   - X dọc trục Part 2, hướng ra xa Part 1;
   - Y = chiều cao tiết diện Part 2, lấy `AxisY` của Part 2 làm vuông góc với X;
   - Z = X × Y.
4. **Đo độ nghiêng bằng pháp tuyến mặt tiếp xúc trong hệ cục bộ** (`_n`):
   - góc nghiêng = acos(n.X), tương ứng P14 của RAKER gốc;
   - n.Y ≠ 0: nghiêng theo chiều cao tiết diện;
   - n.Z ≠ 0: lệch ngang. Phía có góc nhọn là **phía trong**, dùng làm mặc định cho chi tiết một bên, như bản mã;
   - n.X quá nhỏ (Part 2 gần song song mặt Part 1): báo lỗi, không dựng.
5. **Hướng "trên"** = phía có thành phần Z toàn cục dương. Part 2 xoay khoảng 90° quanh trục (|Y.Z| nhỏ) thì ghi `CANH BAO` và lấy +Y cục bộ.
6. **Kích thước bản đế / bản tì:** Top/Bott lấy theo tiết diện **Part 1**. Left/Right lấy theo **điểm xa nhất của Part 2** trên mặt tiếp xúc. Cộng thêm gap; gap dương là rộng ra ngoài.
7. **Chi tiết trên Part 2 khi bị nghiêng:**
   - bản mã là **hình bình hành**, cạnh ngắn song song mặt tiếp xúc;
   - khoảng cách và mép bu lông đo **vuông góc cạnh dài**. Đo dọc cột nghiêng thì khoảng cách = d / cos(góc);
   - mỗi cột bu lông song song cạnh ngắn.
8. **Không để chi tiết cắm vào mặt nghiêng:** hoặc đẩy chi tiết dọc trục cho tới khi góc sâu nhất vừa chạm mặt tiếp xúc, hoặc dựng lấn vào rồi cắt theo mặt đó, như FB.
9. Fitting và cắt đầu Part 2 theo mặt tiếp xúc, chừa khe hàn theo biến của `.uel` gốc.
10. Ghi log: góc nghiêng, phía được chọn, kích thước bản đế, và mọi trường hợp đặc biệt.
11. Tình huống mới mà RAKER chưa gặp: hỏi người dùng. Nếu cách làm mới thay cho chuẩn này thì cập nhật mục 4.4.

## 5. Kiểm tra trước khi báo xong với người dùng
- [ ] `build.bat check` ra OK với mọi bản Tekla.
- [ ] Có hình minh hoạ, ô điền nằm đúng chỗ trên hình, tên biến giữ như gốc (mục 4.2).
- [ ] Prefix part mới lấy theo main part (mục 4.3), hoặc đã ghi rõ lý do làm khác.
- [ ] Connection có Part 2 nghiêng hoặc vặn: đã thử ít nhất một trường hợp nghiêng và một trường hợp lệch ngang (mục 4.4).
- [ ] Tên `[UelParam]` và `[StructuresField]` khớp nhau và khớp tên biến gốc.
- [ ] README của component có đủ các mục Giả định, Chưa làm, Kết quả so sánh.
- [ ] Đã cập nhật bảng component trong `Plugin\README.md` và `HUONG_DAN_CHUYEN_MAY.md` mục 8.
- [ ] Báo rõ phần nào **đã** chạy trong Tekla, phần nào **chưa**, và còn những giả định nào.
