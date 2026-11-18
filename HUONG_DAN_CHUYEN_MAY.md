# Hướng dẫn chuyển thư viện UEL sang máy khác

Thư mục này là **thư viện custom component của Tekla Structures**, gồm 76 file `.uel`, 35 bảng tra `.txt`/`.dat` và 7 file `.xls`.

Bố cục (sắp xếp lại ngày 17/11/2026, nội dung từng file không đổi):
- `File goc\<thư mục con>\`: 16 thư mục con của thư viện gốc, gồm 47 `.uel` kèm bảng tra.
- `UEL\`: 29 `.uel` lẻ, kèm vài bảng tra.

Các đường dẫn trong tài liệu này viết theo bố cục mới. Riêng `_PHAN_TICH\` vẫn ghi theo bố cục cũ, ví dụ `JP11\…` thì nay là `File goc\JP11\…`.
Tài liệu này nói cách mang thư mục sang máy khác mà không làm hỏng component, đồng thời tóm tắt những gì đã phân tích được, để làm tiếp việc viết plugin.

Mọi thông tin dưới đây được rút ra khi phân tích ngày 16/11/2026, trên máy gốc đang chạy Tekla 18.0.

---

## 1. Nguyên tắc khi sao chép

1. **Chép nguyên cả thư mục `UEL Template`** (gồm `File goc\` và `UEL\`), giữ đúng cấu trúc thư mục con. Bảng tra phải đi cùng component gọi tới nó.
2. **Không đổi tên các file `.txt`/`.dat`.** Component gọi bảng tra bằng **tên file chính xác**, ví dụ `fVF("YGY-S_COL2GIR-운용층.txt", P15, 2)`. Chỉ cần đổi tên, xoá dấu hay bỏ chữ Hàn là component sẽ không tìm thấy bảng.
3. **Không mở rồi lưu lại bảng tra bằng trình soạn thảo đổi encoding**, chẳng hạn Notepad lưu thành UTF-8 hay VS Code. Các file này dùng **cp949 (EUC-KR)**, phân cách bằng **TAB** và xuống dòng **CRLF**. Nếu cần sửa, hãy dùng Excel rồi lưu dạng *Text (Tab delimited)*, hoặc dùng Notepad++ và giữ nguyên encoding ANSI/EUC-KR.
4. **Khi nén:** dùng **7-Zip hoặc WinRAR** để tên file chữ Hàn (`운용층`) và chữ Trung (`副本`) giữ đúng. Chức năng "Send to → Compressed folder" của Windows có thể làm hỏng tên các file này trên máy không dùng locale Hàn. Có thể chép trực tiếp qua USB hoặc mạng thay vì nén.
5. **Sau khi chép xong, kiểm tra lại** bằng hai lệnh ở mục 6 (`dups`, `missing`). Kết quả phải giống với danh sách ở mục 4 và mục 5.

## 2. Môi trường trên máy mới

| Hạng mục | Yêu cầu / ghi chú |
|---|---|
| Tekla Structures | File `.uel` được xuất từ **Tekla 18.0** (định dạng nội bộ "Xsteel 7.64"). Tekla **bản mới hơn** nhập được, nhưng nên kiểm tra lại từng component. Tekla **bản cũ hơn 18.0** thì không chắc nhập được. |
| Locale Windows | Nhãn và tên component chứa chữ Hàn. Tekla 18 là ứng dụng cũ, không phải Unicode, nên **rất có thể** phải đặt *Control Panel → Region → Administrative → Language for non-Unicode programs = Korean* thì chữ mới hiển thị đúng. Cài đặt này cũng có thể cần để `fVF` tìm được những bảng có tên chữ Hàn. Hãy thử với component `COL TO GIRDER` trước. |
| Python (tuỳ chọn) | Python 3.7 trở lên, dùng để chạy công cụ trong `_tools/` (mục 6). |
| Viết plugin (tuỳ chọn) | Visual Studio 2019/2022, có gói targeting **.NET Framework 4.0**. Tham chiếu các DLL của **đúng phiên bản Tekla** trên máy đó (xem mục 7). |

## 3. Cách đưa vào Tekla

1. **Nhập component:** mở Tekla, vào *Component catalog* (Ctrl+F), bấm chuột phải, chọn **Import…**, rồi chọn file `.uel`.
2. **Đặt bảng tra (`.txt`/`.dat`)** vào thư mục model, hoặc vào thư mục dùng chung được khai báo trong `XS_FIRM` / `XS_PROJECT`. Nếu dùng cho nhiều model thì nên đặt ở thư mục firm. Khi nhập xong, gắn thử component lên một liên kết. Nếu kích thước ra 0 hoặc sai thì thường là do component không tìm thấy bảng tra.
3. **Nhập lần lượt từng file**, và đọc kỹ mục 4 trước khi nhập. Có những file `.uel` khác tên file nhưng **cùng tên component bên trong**. Khi nhập file thứ hai, Tekla sẽ **ghi đè** lên component đã nhập trước đó.

## 4. Component trùng tên bên trong: chọn bản nào

Danh sách dưới đây là kết quả của lệnh `python _tools/uel_tool.py dups .`

**Có vẻ giống nhau** (chỉ cần nhập một bản):

| Tên component | File |
|---|---|
| `0-DM-PIN MOMENT-V2` | `File goc/B-TO-TG/PIN-MOMENT.uel` = `File goc/PIN-MOMENT/0-DM-PIN-MOMENT-V2.uel` |
| `0-RAKER+TG` | `UEL/RAKER+TG.uel` = `UEL/RAKER+T_GIRDER.uel` |
| `0-RING PL` | `UEL/RING PLATE.uel` = `UEL/RING-PL.uel` |
| `0-DM-T_BAR+SF` | `UEL/T_BAR SF.uel` = `UEL/T_BAR+SF.uel` |

**Khác nhau** (phải chọn một bản, hoặc đổi tên component trong Tekla trước khi nhập bản kia):

| Tên component | Các bản | Khác ở đâu |
|---|---|---|
| `0-DM_S-Col2Gir_TANG_DUONG` | `File goc/COL-GIRDER-P-BOX/…TANG_DUONG.uel` · `File goc/PCOL/S-COL2GIR-TANG DUONG.uel` | Hộp thoại khác nhau (31 và 30 tham số) |
| `♥♥♥YGY_HAUNCH_Moment` | `File goc/COL-HUN-PBOX/YGY-HAUNCH_Moment.uel` · `File goc/HAUNCH/YGY HAUNCH.uel` | Bản COL-HUN có thêm tham số `FB-GWP FREFIX` |
| `♥♥♥YGY_TempBeam-Moment` | `File goc/PIN-MOMENT CHANGSIN/YGY_TEMPBEAM-MOMENT.uel` = `File goc/PINMOMENT CHANGSIN/PIN MOMENT CHANGSIN.uel` · `File goc/YGY MOMENT/YGY.uel` | Bản `YGY MOMENT` có mô hình lớn hơn |
| `0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP)` | `UEL/DM-HAU.uel` · `File goc/HAUNCH/HAUCH.uel` | Mô hình hơi khác nhau |
| `♥YGY_BK-SAME` | `UEL/BKKKKKKKKK SAME.uel` · `File goc/YGY/BK-SAME.uel` | Mô hình khác nhau |
| `★YGY_꺽보-R1` | `UEL/YGY-R1.uel` · `File goc/YGY/SUNG YGY.uel` | Mô hình khác nhau |

Ngoài ra có các file bảng tra trùng nhau từng byte: `P-BOX TO GIRDER.xls` (2 bản), `INNOX-BEAM.dat` = `INNOX-BEAM-DOUBLE.dat`, `BEAM-2.txt` = `副本BEAM-2.txt`, và các file `YGY-TB-MOMENT-PLTHK/WELD-K.txt` trong 3 thư mục.

## 5. Bảng tra đang thiếu

Kết quả của lệnh `python _tools/uel_tool.py missing .` Các bảng này cần tìm lại từ máy gốc hoặc từ người tạo component:

| Bảng tra thiếu | Component cần nó |
|---|---|
| `CT-BAR-H_COLUMN.txt` | `UEL/H-COL-T_BAR.uel` |
| `Shear-JP11-SS275.dat` | `File goc/JP11/01-SHEAR-JP11(DM).uel` (lưu ý: file `UEL/Shear-JP11-SM355.dat` lại ghi mác SS275 bên trong, có thể đã bị đổi tên nhầm; `File goc/JP11/` cũng có một bản `Shear-JP11-SM355.dat`) |
| `DM-1F-RING+WING.txt` | `UEL/RING PLATE.uel`, `UEL/RING-PL.uel` |
| `YGY-HAUNCH_Moment-미운용층.txt` | `UEL/YGY-HAUNCH-MOMENT-TANG AM.uel` |

## 6. Công cụ đi kèm: `_tools/uel_tool.py`

Script Python dùng để đọc file `.uel` mà không cần mở Tekla. Chạy từ thư mục `UEL Template`; `.` nghĩa là quét cả `File goc\` lẫn `UEL\`:

```bash
python _tools/uel_tool.py info .
```
```bash
python _tools/uel_tool.py dups .
```
```bash
python _tools/uel_tool.py missing .
```
```bash
python _tools/uel_tool.py formulas "File goc/COL TO GIRDER/0-DM-S-COL TO GIR.uel"
```
```bash
python _tools/uel_tool.py unpack "UEL/0-PL WELD.uel" out_plweld
```

- `info`: liệt kê từng component với tên bên trong, loại, các tab, các tham số và các bảng tra mà nó gọi.
- `dups`: tìm các component trùng tên bên trong.
- `missing`: tìm các bảng tra được gọi nhưng không có trong thư mục.
- `formulas`: in các công thức như `if … then … else … endif`, `fVF(...)`, `fTpl(...)`, `fP(...)`.
- `unpack`: tách các phần bên trong file `.uel` ra thành file riêng.

## 7. Tóm tắt phân tích, để làm tiếp phần plugin

**Cấu trúc file `.uel`:** là file nén gzip, bên trong gồm:
- `.inp`: định nghĩa hộp thoại.
- `_ec_uel.txt`: tên và loại component. Mã `-1000` thường là connection, `-100` là detail, `-10` là part. Mã `-10000` gặp ở các component con `BH`, `BT`, `ACT`, hiện chưa rõ ý nghĩa.
- `.uel_temp`: mô hình mẫu dạng nhị phân.
- Các file thuộc tính `.p_*` và ảnh thumbnail.

**Đọc được từ file:**
- Hộp thoại, tên biến P1…Pn và nhãn của chúng.
- Công thức, có thể in ra bằng lệnh `formulas`.
- Bảng tra.
- *(Cập nhật 17/11/2026)* **Liên kết giữa từng công thức và biến nó gán vào**, kể cả biến khoảng cách `D1…Dn` (tên dạng `D53.CUTPART.BEAM`). Xem `_PHAN_TICH/components/<tên>.uel.md`.
- *(Cập nhật 17/11/2026)* **Thuộc tính gán cho từng chi tiết**: `PROFILE`, `MATERIAL`, `CLASS`, `SERIE` (prefix), `CREATION` (điều kiện có tạo chi tiết hay không), thông số bu lông (`SIZE`, `STANDARD`, `BOLT_GROUP_DISTX/Y`, `SLOTTED_HOLE_*`).
- *(Cập nhật 17/11/2026)* Danh sách các mặt phẳng mà component dùng (`albl_PlaneTop`, `albl_PlaneExtremaTop`, `albl_PlaneCentralAxisVertical`…).

Bố cục nhị phân đã giải mã và cách chạy lại công cụ: xem `_PHAN_TICH/README.md` mục 1 và `_PHAN_TICH/tools/`.

**Chưa đọc được, phải mở Custom Component Editor trong Tekla để xem:**
- Vị trí và hình học của từng chi tiết.
- Biến D nào buộc vào mặt phẳng nào.
- Loại của từng chi tiết theo số hiệu nội bộ (`obj#…`) — chỉ biết gián tiếp qua tên biến D và thuộc tính được gán.

**Tekla 18.0 trên máy gốc:**
- Thư mục cài đặt `C:\TeklaStructures\18.0` (bản 18.0.107, SR7).
- DLL API nằm ở `C:\TeklaStructures\18.0\nt\bin\plugins\`, dùng .NET Framework 4.0.
- API có `ConnectionBase` (dùng cho connection và detail) và `PluginBase`. Thuộc tính `PluginUserInterface` nhận thẳng nội dung `.inp`.
- Plugin phải build riêng cho từng phiên bản Tekla, và phải khởi động lại Tekla mỗi khi thay DLL.

**Hướng làm plugin đã thống nhất:**
- Làm thử trước với `0-PL WELD`: là detail, có 11 tham số, công thức đơn giản như `P7*tan(P5)` hay `if(P6==1) then P2-P4 else P3 endif`.
- Bạn chụp lại danh sách chi tiết, đường cắt và cửa sổ Variables trong Custom Component Editor.
- Sau đó dựng project C# (.NET 4.0) trong thư mục `Plugin\`, và giữ nguyên `File goc\` cùng `UEL\` làm tài liệu gốc.
- Thứ tự làm: nhóm dễ (`CUTTING`, `PL+FILLER`, `AIR HOLE`, `STUD*`), rồi nhóm trung bình (`BK-*`, `T_BAR+SF`, `JP11`), cuối cùng nhóm khó (`COL TO GIRDER`/`P-BOX`, `HAUNCH`, `PIN-MOMENT`, `RING PL`, `WALE+T_GIRDER`, `YGY-R1`, `NEC_AUTO_SF`).

## 8. Plugin đã viết

| Plugin | Thư mục | Từ component | Trạng thái |
|---|---|---|---|
| `RAKER+T_GIRDER` | `Plugin/RAKER+T_GIRDER/` | `RAKER+T_GIRDER.uel` (`0-RAKER+TG`) | Bản hiện tại, trong khung `Plugin/`. Chưa thử trong Tekla. |
| `COL_TO_GIRDER` | `Plugin/COL_TO_GIRDER/` | `File goc/COL TO GIRDER/COL_TO_GIRDER.uel` (`0-DM_S-Col2Gir`, = `0-DM-S-COL TO GIR.uel` cũ) | 18/11/2026: chưa thử. Đã dựng fitting, vát CJP, CGP, bu lông, FB, WP1/WP2 (theo ảnh mặt bằng); chưa có các cắt P62–P65 (chờ ảnh Custom Component Editor). Gộp luôn 2 bản `0-DM_S-Col2Gir_TANG_DUONG` (`COL-GIRDER-P-BOX`, `PCOL`) bằng biến mới P74–P78. |
| `H-COL_TO_GIRDER` | `Plugin/H-COL_TO_GIRDER/` | `File goc/H-COL-GIRDER/H-COL.uel` (`0-DM_H-Col2Gir_H-COLUMN`) | 18/11/2026: chưa thử. Clone COL_TO_GIRDER: P4 55, SM355, khe PIN 30, Part 1 cột H theo hình bao ngoài như cột hộp (vào bụng: t_bar bù riêng). Đã bỏ WP và ring khỏi hộp thoại (người dùng 18/11/2026). Bảng `H-COL_TO_GIRDER.txt`. |
| `COL_TO_HAUNCH_GIRDER` | `Plugin/COL_TO_HAUNCH_GIRDER/` | `File goc/COL-HUN-PBOX/YGY-HAUNCH_Moment.uel` (`♥♥♥YGY_HAUNCH_Moment`) | 18/11/2026: chưa thử. Clone COL_TO_GIRDER (giữ tên biến COL_TO_GIRDER), Part 2 dầm haunch: đo 2 cánh trên solid, cánh trên / dưới dựng theo hệ riêng; CGP theo TOP2BOLT (P99); scallop P100/P101. Bảng `COL_TO_HAUNCH_GIRDER.txt`. |
| `TEMPBEAM-MOMENT` | `Plugin/TEMPBEAM-MOMENT/` | `File goc/PIN-MOMENT CHANGSIN/YGY_TEMPBEAM-MOMENT.uel` (`♥♥♥YGY_TempBeam-Moment`) | 18/11/2026: chưa thử, `build.bat core` OK. Hình học theo JSON của component gốc (GP + tab, BACK SF, SF dưới, FB, vát CJP, scallop, bu lông). Bảng `YGY-TB-MOMENT.txt` đọc cột theo tên vì bảng trong `File goc` khác bảng trong model. Chưa làm: tấm bù T1/P129, gia cường P43, dầm xiên/dốc. |
| `RAKER_TG_PLUGIN` (bản đầu) | `../Plugins/RakerTG/` | `RAKER+T_GIRDER.uel` (`0-RAKER+TG`) | Giữ để tham khảo. Có EXE thử nghiệm `RAKER_TG_TEST.exe` và DLL. Đã sửa lỗi bản đế dính vào bụng dầm. Lỗi class bị đổi thành 99 đang chờ xác nhận. Xem `Plugins/RakerTG/README.md` mục 4 và mục 6. |

Khung plugin hiện tại là thư mục [Plugin/](Plugin/README.md) ngay trong thư mục này (17/11/2026):
- Mỗi component một thư mục `Plugin\<tên file .uel gốc>\`. Tên thư mục cũng là tên DLL và tên component trong Tekla, ví dụ `RAKER+T_GIRDER`.
- Hai bộ DLL: `Plugin\18.0\` dùng cho Tekla 18.0 và 2019; `Plugin\2020+\` dùng cho Tekla 2020, 2021 và 2025.
- Bản DEV tự nạp lại code khi bấm Modify. Có một EXE thử nghiệm dùng chung cho mọi component.
- Đóng gói bằng `pack.bat`.

Component mới thì làm theo `Plugin/README.md` mục 5. Khung cũ `../Plugins/UEL_Plugins/` (một `UEL_PLUGINS.dll`, plugin tên `RAKER_TG_PLUGIN`) không dùng nữa.

Khi chuyển sang máy khác, chép kèm thư mục `Plugin/` và thư mục `_PHAN_TICH/` (kết quả giải mã công thức theo từng biến, dùng khi viết plugin). Build lại bằng `build.bat` trên máy mới. Nếu Tekla cài ở đường dẫn khác thì đặt biến `TEKLA_ROOT_<bản>`, hoặc `UEL_API_18` / `UEL_API_2020`.
