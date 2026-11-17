# Plugin: các component UEL viết lại thành plugin C# cho Tekla 18.0 → 2025

- Mỗi component nằm trong **một thư mục riêng**. Tên thư mục giống tên file `.uel` gốc, và cũng là **tên file DLL** và **tên component trong Tekla**.
- Có **hai bộ DLL**:
  - `18.0\` dùng cho Tekla 18.0 và 2019. Bộ này build bằng DLL API của Tekla 18.0.
  - `2020+\` dùng cho Tekla 2020, 2021, 2025… Bộ này build bằng DLL API của Tekla 2020.
- **Sửa code xong là thử được ngay**, không phải khởi động lại Tekla (bản DEV, xem mục 3).

| Component (thư mục = DLL = tên trong Tekla) | Từ file gốc | Ghi chú |
|---|---|---|
| `RAKER+T_GIRDER` | `RAKER+T_GIRDER.uel` (tên cũ trong catalog: `0-RAKER+TG`) | Xem [RAKER+T_GIRDER/README.md](RAKER+T_GIRDER/README.md) |

## 1. Cấu trúc thư mục

```
Plugin\
  RAKER+T_GIRDER\     <- 1 component = 1 thu muc. SUA CODE O DAY
     Params.cs          thong so (P1, P2... dung ten bien cua .uel goc)
     Builder.cs         hinh hoc (Tekla Open API)
     Component.cs       tieu de, cau nhac chon cau kien
     Plugin.cs          hop thoai Tekla (.inp) + truong [StructuresField]
     README.md          ghi chu, loi da biet, checklist
  18.0\               <- DLL phat hanh cho Tekla 18.0, 2019   (RAKER+T_GIRDER.dll ...)
  2020+\              <- DLL phat hanh cho Tekla 2020 tro di  (RAKER+T_GIRDER.dll ...)
  build.bat           build (khong can Visual Studio)
  install.bat         cai vao Tekla
  pack.bat            dong goi bo cai -> _Dist\UEL_<ngay>\

  _Common\            code dung chung cho moi component (it khi phai sua)
  _Template\          mau component moi
  _Tester\            ma nguon EXE thu nghiem
  _Tools\             script cua build/install/pack/check
  _Dev\               ban DEV + EXE thu nghiem (tu sinh khi build)
  _obj\  _Dist\       file tam, bo cai (tu sinh)
```

Mỗi component được build thành các file sau:

| File | Dùng để |
|---|---|
| `18.0\<Ten>.dll` | **Phát hành** cho Tekla 18.0 và 2019 |
| `2020+\<Ten>.dll` | **Phát hành** cho Tekla 2020 trở đi |
| `_Dev\18.0\<Ten>_DEV.dll`, `_Dev\2020+\<Ten>_DEV.dll` | **Phát triển.** Component tên `<Ten>_DEV`. Mỗi lần Tekla chạy component, nó nạp file `<Ten>.Core.dll` mới nhất nằm cùng thư mục |
| `_Dev\<bộ>\<Ten>.Core.dll` (+ `.pdb`) | Phần hình học và thông số. Được nạp dưới dạng byte nên file không bị khoá |
| `_Dev\UEL_TESTER.exe` | EXE thử nghiệm dùng chung cho mọi component |

Trong mỗi thư mục component, file tên `Plugin*.cs` là phần vỏ plugin của Tekla. Mọi file `.cs` còn lại là phần Core. Thư mục con bắt đầu bằng `_` (ví dụ `_old`) không được build.

## 2. Build

```bash
build.bat
```

| Lệnh | Việc làm |
|---|---|
| `build.bat` | Build tất cả: hai bộ phát hành, bản DEV và EXE |
| `build.bat core` | Chỉ build lại Core của bản DEV, mất vài giây. Tekla và EXE vẫn mở được |
| `build.bat core RAKER+T_GIRDER` | Như trên, chỉ cho một component |
| `build.bat core RAKER+T_GIRDER 2020` | Như trên, chỉ cho bộ `2020+` (hoặc `18`) |
| `build.bat dev` | Core cùng `<Ten>_DEV.dll`. Sau đó phải cài lại vào Tekla |
| `build.bat release` | Chỉ hai bộ phát hành `18.0\` và `2020+\` |
| `build.bat check` | Build tất cả, rồi kiểm tra với **mọi** bản Tekla trên máy (mục 6) |

Bộ `2020+` build bằng bản Tekla 2020 trở lên **thấp nhất** tìm được trên máy, hiện là 2020.0. Muốn chỉ định thư mục DLL API thì đặt biến `UEL_API_18` hoặc `UEL_API_2020`.

## 3. Sửa và thử

### Cài đặt một lần
Tắt bản Tekla định dùng. Với Tekla 2019 trở đi, phải chạy cmd **bằng quyền Administrator**.
```bash
build.bat
```
```bash
install.bat dev 2020
```
Mở lại Tekla. Component `RAKER+T_GIRDER_DEV` sẽ có trong Component catalog.

### Vòng lặp hằng ngày (Tekla luôn mở)
1. Sửa code trong thư mục component, ví dụ `RAKER+T_GIRDER\Builder.cs`.
2. Build lại Core:
   ```bash
   build.bat core RAKER+T_GIRDER
   ```
3. Thử theo một trong hai cách:
   - **Trên component thật:** chọn component `…_DEV` trong model, mở hộp thoại rồi bấm **Modify**.
   - **Bằng EXE:** mở `_Dev\UEL_TESTER.exe`, chọn component, chọn cấu kiện rồi dựng.
     - EXE tự nhận ra bản Tekla đang mở và nạp Core của đúng bộ (`_Dev\18.0` hoặc `_Dev\2020+`).
     - Muốn ép một bản cụ thể thì chạy `UEL_TESTER.exe 2025`.
     - Sau khi build lại Core, chỉ cần bấm **"2. Dung lai"**.
     - Đối tượng EXE tạo ra là đối tượng rời. Dọn bằng nút **"Xoa ket qua vua dung"**.

Log nằm ở `%TEMP%\UEL_PLUGINS.log`. Dòng đầu của mỗi lần chạy ghi `Core <thời điểm build>`, nhờ đó biết đang chạy bản code nào.

### So sánh với component gốc
Nút **"So sanh: component goc vs plugin"** trên `UEL_TESTER.exe` cho biết plugin có dựng giống component `.uel` gốc hay không.

**Chuẩn bị (một lần):**
1. Import file `.uel` gốc vào Component catalog.
2. Dựng một cặp cấu kiện, rồi **copy** cặp đó sang chỗ khác. Dời hay xoay đều được.
3. Đặt component gốc lên cặp thứ nhất, component `…_DEV` lên cặp thứ hai. Cả hai nạp **cùng một bộ thuộc tính**.

**So sánh:**
1. Bấm nút.
2. Chọn một part do component **gốc** tạo ra, ví dụ bản mã. Không chọn dầm đầu vào.
3. Chọn một part do component **plugin** tạo ra.

**EXE đọc các đối tượng sau của mỗi component:**
- part con: profile, material, class, name, số hiệu, khối bao, thể tích;
- bu lông: chuẩn, cỡ, số lượng, toạ độ từng con, trục thân, lỗ ô van;
- mối hàn: kiểu, cỡ, nối part nào với part nào;
- đường cắt, fitting, cut plane;
- cấu kiện đầu vào. Thể tích của chúng thay đổi thì nghĩa là đường cắt vào dầm khác nhau.

Toạ độ đọc theo hệ toạ độ của cấu kiện chính. Vì vậy hai cặp copy ở hai chỗ khác nhau vẫn so được.

EXE ghép từng đối tượng với đối tượng gần nhất cùng loại ở bên kia, rồi báo:

| Mục | Nghĩa |
|---|---|
| **khớp** | giống nhau, trong dung sai 1 mm và thể tích 1% |
| **lệch** | ghép được nhưng có thuộc tính hoặc toạ độ khác. Mỗi dòng ghi `tên: giá trị gốc \| giá trị plugin` |
| **B thiếu** | chỉ component gốc có, plugin chưa dựng |
| **B thừa** | plugin dựng ra nhưng component gốc không có |

- Kết quả được lưu vào `_Dev\SoSanh\<thời điểm>_<A>_vs_<B>.txt`.
- Nút **"Chon trong Tekla…"** chọn sẵn các đối tượng lệch, thiếu hoặc thừa trong model để xem.
- Sửa code xong thì chạy `build.bat core …`, bấm Modify trên component `_DEV`, rồi so lại.

### Khi nào phải cài lại và khởi động lại Tekla
- Khi **thêm component**, hoặc khi sửa `Plugin.cs` (hộp thoại, danh sách tham số).
  - Chạy `build.bat dev`, tắt Tekla, chạy `install.bat dev <bản>`, rồi mở lại Tekla.
- Khi sửa `_Common\Host` hoặc `_Common\Plugin`.

### Debug
Trong Visual Studio, chọn *Debug → Attach to Process → TeklaStructures.exe* (kiểu Managed v4). Core được nạp kèm `.pdb`, nên đặt breakpoint thẳng trong file của component được.

### Lưu ý
- Bản DEV đọc Core theo **đường dẫn ghi cứng lúc build**, tức `_Dev\<bộ>\` của thư mục này. Nếu chuyển thư mục đi nơi khác thì build lại và cài lại bản DEV.
- **Đừng phát hành bản DEV.**
- Mỗi lần nạp lại Core tốn thêm một ít bộ nhớ trong Tekla. Thỉnh thoảng khởi động lại Tekla là đủ.

## 4. Cài đặt và phát hành

| Lệnh | Việc làm |
|---|---|
| `install.bat 18` | Chép `18.0\*.dll` vào Tekla 18.0 |
| `install.bat 2020` | Chép `2020+\*.dll` vào Tekla 2020 (tương tự với 2021, 2025) |
| `install.bat all` | Cài cho mọi bản Tekla trên máy, mỗi bản lấy đúng bộ của nó |
| `install.bat 2025 RAKER+T_GIRDER` | Chỉ cài một component |
| `install.bat dev 2020` | Cài bản DEV |

Mang sang máy khác:
```bash
pack.bat
```
Lệnh này tạo `_Dist\UEL_<ngày>\`, gồm `18.0\`, `2020+\`, `install.bat`, `_Tools\` và `HUONG_DAN_CAI.txt`. Chép cả thư mục sang máy kia rồi chạy `install.bat` **bằng quyền Administrator**.

- Tên component, tức tên thư mục, **không được đổi** sau khi đã dùng trong model, vì model lưu component theo tên này.
- Bản phát hành và bản DEV cài chung được, vì tên component khác nhau.
- Bảng tra `.txt`/`.dat` mà component cần thì đặt vào thư mục model hoặc `XS_FIRM`.

## 5. Thêm một component mới

Quy tắc đầy đủ (điều cấm, quy trình, quy ước code, checklist) nằm ở [CLAUDE.md](CLAUDE.md). Dưới đây là tóm tắt.

1. Chép thư mục `_Template` thành `Plugin\<tên file .uel gốc>`, ví dụ `Plugin\0-PL WELD`.
2. Trong 4 file, đổi `Xxx` thành tên ngắn của component, ví dụ `PlWeld`.
3. Sửa các file:
   - `Params.cs`: các biến lấy từ `UEL Template/_PHAN_TICH/components/<file>.uel.md`.
   - `Plugin.cs`: hộp thoại lấy từ phần `.inp` trong file `.md` đó. Chỉ thay tên trong `joint(...)` hoặc `detail(...)` bằng `""" + PluginName + @"""`.
   - `Builder.cs`, `Component.cs`.
4. Tên trong `[StructuresField("P..")]` (Plugin.cs) và `[UelParam("P..")]` (Params.cs) phải **giống nhau** và **đúng tên biến của `.uel` gốc**.
5. Chạy `build.bat dev <Ten>`, tắt Tekla, chạy `install.bat dev <bản> <Ten>`, rồi mở Tekla và thử.

Không cần đăng ký ở đâu. Build tự nhận mọi thư mục có `Plugin.cs`.

Quy tắc: mọi thứ đi qua ranh giới Core ↔ Plugin/EXE chỉ dùng kiểu của .NET hoặc của Tekla, gồm `Model`, `ModelObject`, `IList`, `IDictionary`, `string`… `Plugin.cs` không được dùng lớp nào trong `Params.cs`, `Builder.cs`, `Component.cs`.

## 6. Nhiều phiên bản Tekla

| Bản Tekla | Bộ DLL | Build với |
|---|---|---|
| 18.0, 2019 | `18.0\` | DLL API Tekla 18.0. Tekla 2019 tự chuyển tham chiếu nhờ `bindingRedirect` |
| 2020, 2021, 2025 | `2020+\` | DLL API Tekla 2020. Tekla 2021 và 2025 tự chuyển tham chiếu nhờ `bindingRedirect` |

`build.bat check` làm hai việc với từng bản Tekla tìm thấy trên máy:
1. Biên dịch lại mọi component với DLL API của bản đó.
2. Quét bộ DLL dành cho bản đó: mọi lớp, hàm và trường Tekla API được gọi phải còn tồn tại và đúng chữ ký.

Kết quả ngày 17/11/2026:

| Bản | Biên dịch lại | Quét bộ DLL tương ứng |
|---|---|---|
| 18.0 | OK | `18.0\`: OK |
| 2019.0 | OK | `18.0\`: OK |
| 2020.0 | OK | `2020+\`: OK |
| 2021.0 | OK | `2020+\`: OK |
| 2025.0 | OK | `2020+\`: OK |

Thư mục cài (`_Tools\tekla.ps1` tự tìm):

| Bản | DLL API | Thư mục plugin |
|---|---|---|
| 18.0 | `C:\TeklaStructures\18.0\nt\bin\plugins` | như cột bên |
| 2019–2021 | `C:\Program Files\Tekla Structures\<bản>\nt\bin\plugins` | như cột bên |
| 2025 | `C:\Program Files\Tekla Structures\2025.0\bin` | `…\2025.0\bin\plugins` |

Nếu Tekla cài ở chỗ khác, đặt biến `TEKLA_ROOT_<bản>`, ví dụ `TEKLA_ROOT_2025.0=D:\Tekla\2025.0`.

**Chưa kiểm tra, phải thử thật trong Tekla:** bản DEV chạy bên trong Tekla, hành vi Modify, EXE trên model thật (cả nút So sánh), hộp thoại `.inp` trên Tekla 2025.

## 7. Nguồn gốc

Thư mục này thay cho `ALL IN ONE\Plugins\UEL_Plugins` (bản cũ: tất cả component trong một `UEL_PLUGINS.dll`, plugin tên `RAKER_TG_PLUGIN`). `ALL IN ONE\Plugins\RakerTG` vẫn giữ để tham khảo.
