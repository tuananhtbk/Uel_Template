# Quy tắc viết component plugin trong thư mục `Plugin\`

Áp dụng cho mọi component chuyển từ `.uel` (custom component Tekla 18.0) sang plugin C#, từ `RAKER+T_GIRDER` trở về sau.
Người và Claude đều làm theo file này. Cách dùng lệnh xem [README.md](README.md).
Trao đổi với người dùng bằng **tiếng Việt**.

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
| 6. `Plugin.cs` | Xem mục 3. | |
| 7. `Component.cs` | `Title` là tên component trong catalog gốc (ví dụ `0-PL WELD`). `SourceUel` là đường dẫn file `.uel`. Câu nhắc chọn và thứ tự chọn cấu kiện phải giống bản gốc. | |
| 8. `Builder.cs` | Xem mục 3. | |
| 9. README | Viết `Plugin\<Ten>\README.md` theo mẫu `RAKER+T_GIRDER\README.md`, gồm: tên trong Tekla, primary/secondary, bảng file, tài liệu liên quan, **Giả định**, **Chưa làm**, **Kết quả so sánh**. | |
| 10. Build | Chạy `build.bat dev <Ten>`, sau đó `build.bat check`. Mọi bản Tekla (18.0 đến 2025) đều phải ra **OK**. | |
| 11. Cài | Tắt Tekla, mở cmd bằng quyền Administrator, chạy `install.bat dev <bản> <Ten>`. | `<Ten>_DEV` có trong catalog |
| 12. So sánh | Dùng nút **So sánh** trên `UEL_TESTER.exe` (README mục 3). Đặt component gốc và bản `_DEV` lên 2 cặp cấu kiện copy, nạp cùng một preset. Lặp lại với **từng preset** và **từng nhánh `if`** / `CREATION`. | File `_Dev\SoSanh\*.txt` |
| 13. Sửa | Sửa code, chạy `build.bat core <Ten>`, bấm Modify, so lại. Làm đến khi kết quả là `GIONG NHAU`, hoặc mọi dòng lệch đều đã được giải thích trong README. | |
| 14. Ghi nhận | Cập nhật bảng component trong `Plugin\README.md` và `HUONG_DAN_CHUYEN_MAY.md` mục 8, ghi trạng thái: *chưa thử* / *đã so sánh* / *đã dùng thật*. | |

## 3. Quy ước code

### Tên và thông số
- **Tên biến phải giữ đúng như `.uel` gốc.** `[UelParam("P7")]` trong `Params.cs` và `[StructuresField("P7")]` trong `Plugin.cs` phải trùng nhau và trùng tên biến gốc. Tên thuộc tính C# đặt theo nghĩa, ví dụ `PlateThk`.
- **Kiểu dữ liệu:**

  | Kiểu trong `.inp` | Kiểu C# |
  |---|---|
  | Yes/No (0/1) | `bool` trong Params, `int` trong Plugin |
  | integer | `int` |
  | distance, angle | `double` |
  | text, material, profile | `string` |

- **Giá trị mặc định** lấy từ một preset có thật trong file `.md`. Ghi tên preset đó ở đầu `Params.cs`. Giá trị nào không có trong preset thì đánh dấu là giả định.

### `Plugin.cs`
- Dùng **nguyên văn** phần `.inp` gốc: nhân đôi dấu `"`, và chỉ thay tên trong `joint(...)` hoặc `detail(...)` bằng `""" + PluginName + @"""`.
- Chọn loại theo mã trong `_ec_uel.txt`:
  - `-1000` (connection): `SECONDARYTYPE_ONE` hoặc số phụ tương ứng;
  - `-100` (detail): `SECONDARYTYPE_ZERO` + `DetailType`.
- File này **không được dùng lớp nào của Core**. Mọi thứ đi qua ranh giới Core ↔ Plugin/EXE chỉ dùng kiểu của .NET hoặc của Tekla.

### `Builder.cs`
- Ghi lại công thức gốc ngay cạnh code, giữ tên biến. Ví dụ: `// D12.PLATE.BEAM = 0.5*(P34-1)*P43+P45`.
- Gán `PROFILE`, `MATERIAL`, `CLASS`, `SERIE` (prefix) cho part đúng như mục "Part attribute bindings".
- `CREATION` là điều kiện có tạo part hay không, phải làm đủ.
- Nhánh nào chưa làm: ghi `Logger.Log("CANH BAO: ...")`, và ghi vào mục "Chưa làm" trong README.
- Mỗi đối tượng `Insert()` thành công thì thêm vào `created`, để EXE xoá được.
- Đầu mỗi lần dựng, ghi thông số vào log bằng `Logger.Log("Thong so: " + _p)`.
- Nếu đổi `TransformationPlane` thì phải trả lại plane cũ trong khối `finally`.

### Bảng tra `fVF(...)`
- Đọc bảng tra từ thư mục model, sau đó từ `XS_FIRM`. File dùng **cp949**, phân cách **TAB**, xuống dòng **CRLF**. Chỉ đọc, không chép hay đổi file.
- Component đầu tiên cần bảng tra sẽ viết hàm dùng chung ở `_Common\Core\` (ví dụ `LookupTable.cs`). Các component sau dùng lại hàm đó.
- Không tìm thấy bảng hoặc dòng: ghi log rõ tên file và khoá đang tìm, rồi dừng. Không tự điền số.

### Ngôn ngữ và tương thích
- Viết bằng **C# 4 / .NET 4.0**, vì build bằng csc v4. **Không dùng:**
  - `$"..."`, `?.`, `nameof`;
  - thân hàm dạng `=>`;
  - `out var`, tuple;
  - property có giá trị khởi tạo (`{ get; set; } = ...`).
- Chuỗi hiện trong Tekla (câu nhắc, log, nhãn hộp thoại tự viết) dùng **không dấu**, vì Tekla 18 không phải Unicode. Comment trong code viết có dấu được (file UTF-8).
- Chỉ dùng API Tekla có ở cả 18.0 lẫn 2025. `build.bat check` sẽ báo lỗi nếu dùng sai.

## 4. Kiểm tra trước khi báo xong với người dùng
- [ ] `build.bat check` ra OK với mọi bản Tekla.
- [ ] Tên `[UelParam]` và `[StructuresField]` khớp nhau và khớp tên biến gốc.
- [ ] README của component có đủ các mục Giả định, Chưa làm, Kết quả so sánh.
- [ ] Đã cập nhật bảng component trong `Plugin\README.md` và `HUONG_DAN_CHUYEN_MAY.md` mục 8.
- [ ] Báo rõ phần nào **đã** chạy trong Tekla, phần nào **chưa**, và còn những giả định nào.
