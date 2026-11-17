# UEL Template – thư viện custom component Tekla Structures

- 76 custom component `.uel` của Tekla 18.0, lưu trong các thư mục con.
- Các bảng tra `.txt` và `.dat` mà component đọc qua hàm `fVF(...)`. Các bảng này dùng mã cp949, phân cách bằng TAB, xuống dòng CRLF.
- Đang viết lại các component này thành plugin C# cho Tekla 18.0 đến 2025.

| Thư mục / file | Nội dung |
|---|---|
| [HUONG_DAN_CHUYEN_MAY.md](HUONG_DAN_CHUYEN_MAY.md) | Cách chép sang máy khác, các component trùng tên, các bảng tra còn thiếu, tóm tắt kết quả phân tích |
| [Plugin/](Plugin/README.md) | Plugin C#: build, cài đặt, thử nghiệm. Quy tắc làm component mới ở [Plugin/CLAUDE.md](Plugin/CLAUDE.md) |
| [_PHAN_TICH/](_PHAN_TICH/README.md) | Kết quả giải mã từng component: công thức P/D, thuộc tính part, preset |
| `_tools/uel_tool.py` | Công cụ đọc file `.uel` từ dòng lệnh |

**Lưu ý:**
- Các file `.uel`, `.txt`, `.dat` là tài liệu gốc. Không sửa, không đổi tên, không đổi encoding.
- `.gitattributes` đặt `* -text` để Git giữ nguyên từng byte.
- Các DLL không được đưa lên repo. Muốn có DLL thì chạy `Plugin\build.bat` để tạo lại.
