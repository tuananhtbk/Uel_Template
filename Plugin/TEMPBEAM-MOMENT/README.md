# TEMPBEAM-MOMENT: liên kết moment dầm tạm → dầm chính (GP + bu lông, cánh hàn CJP)

| | |
|---|---|
| Tên trong Tekla | `TEMPBEAM-MOMENT` (bản DEV: `TEMPBEAM-MOMENT_DEV`) |
| Component gốc | `♥♥♥YGY_TempBeam-Moment`, file [`File goc\PIN-MOMENT CHANGSIN\YGY_TEMPBEAM-MOMENT.uel`](../../File%20goc/PIN-MOMENT%20CHANGSIN/YGY_TEMPBEAM-MOMENT.uel). Bản mới hơn cùng tên: `File goc\YGY MOMENT\YGY.uel`. Bản này lấy prefix theo main part (P30 = P63, FB = P64), và plugin làm theo cách đó. |
| Phân tích | [`_PHAN_TICH\components\PIN-MOMENT CHANGSIN__YGY_TEMPBEAM-MOMENT.uel.md`](../../_PHAN_TICH/components/PIN-MOMENT%20CHANGSIN__YGY_TEMPBEAM-MOMENT.uel.md) |
| Nguồn hình học | File JSON của component gốc `_Dev\Export\20261118_160409_♥♥♥YGY_TempBeam-Moment_47786671.json` (model `SAMPLE V2020`, Part 1 = `H500*200*10*16`, Part 2 = `H400*200*8*13`, mọi ô để trống), ảnh Variables, ảnh mặt đứng và ảnh 3D do người dùng gửi ngày 18/11/2026 |
| Loại | connection (`-1000`), 1 cấu kiện phụ |
| **Part 1** | dầm chính (primary, chọn trước). JSON: primary = `H500*200*10*16` |
| **Part 2** | dầm tạm (secondary), profile tra trong bảng `YGY-TB-MOMENT.txt` |
| Bảng tra bắt buộc | `YGY-TB-MOMENT.txt` (tra theo profile Part 2); `YGY-TB-MOMENT-PLTHK.txt` (chỉ bắt buộc khi có SF dưới) |
| Trạng thái | 18/11/2026: viết xong, `build.bat core` OK cả 2 bộ. **Chưa build DEV, chưa chạy trong Tekla.** |

## Các file

| File | Nội dung |
|---|---|
| `Params.cs` | Thông số: đủ 9 biến của `.inp` gốc, cộng biến mới P65–P77 |
| `Builder.cs` | Hình học, toạ độ JSON ghi cạnh code |
| `Component.cs` | Tiêu đề, câu nhắc chọn, đọc 2 bảng tra |
| `Plugin.cs` | Hộp thoại (tab ` Parameters 1 ` có hình, tab ` Advanced `) và `[StructuresField]` |
| `TEMPBEAM-MOMENT.txt` | Bảng mẫu, chép từ `YGY-TB-MOMENT.txt` của model mẫu (đã bỏ 2 dòng `...-HOLD` riêng của dự án). Chỉ dùng khi không tìm thấy `YGY-TB-MOMENT.txt` |
| `bitmaps\UEL_TBM_1.bmp` | Hình hộp thoại, vẽ theo phong cách hình mặc định của Tekla (người dùng 18/11/2026), đúng tỉ lệ cặp dầm trong JSON |
| `_hinh\VeHinh.cs`, `_hinh\dialog.inp` | Chương trình sinh hình và `.inp`. Ảnh chụp component gốc lưu ở `_hinh\anh_goc_mat_dung.png`, `_hinh\anh_goc_3d.png` để đối chiếu |

## Cách thử

1. Cần chạy `build.bat dev TEMPBEAM-MOMENT`, vì đây là component mới nên phải build vỏ DEV. **Chỉ chạy khi bạn ra lệnh.**
2. Tắt Tekla, mở cmd bằng quyền **Administrator**, chạy `install.bat dev <bản> TEMPBEAM-MOMENT` (lệnh này chép DLL, hình và bảng mẫu).
3. Dùng model mẫu `SAMPLE V2020` (đã có `YGY-TB-MOMENT*.txt`): đặt `TEMPBEAM-MOMENT_DEV`, chọn **dầm chính trước, dầm tạm sau**. Log ở `%TEMP%\UEL_PLUGINS.log`.
4. So với component gốc bằng nút **So sánh** của EXE, trên 2 cặp dầm copy giống cặp trong JSON.

## Bảng tra

### `YGY-TB-MOMENT.txt` (bảng dầm)
- Ô **P65** có nhập thì dùng file đó. Không tìm thấy thì **dừng**.
- P65 trống thì dùng **tên gốc** `YGY-TB-MOMENT.txt`. Tên gốc được ưu tiên vì model của dự án thường đã có bảng riêng (ví dụ model mẫu có thêm dòng `H400*400*13*21`).
- Không thấy tên gốc ở đâu thì dùng bảng mẫu `TEMPBEAM-MOMENT.txt` và ghi `CANH BAO`.
- Không có dòng cho profile Part 2 thì **dừng** (`LOI`).
- **Đọc cột theo tên tiêu đề** (T3, H1, B3/L1), không theo vị trí như `fVF`. Lý do: có 2 phiên bản bảng cùng tên.

| Bảng | Tiêu đề | `fVF(...,2)` / `fVF(...,3)` |
|---|---|---|
| Trong model mẫu (mới) | `GIRDER SIZE, T3, H1, B3, NUMBER OF BOLT, GP, SF, REMARK` | T3 / H1. Khớp JSON: PL11, cao 275 |
| `File goc\PIN-MOMENT CHANGSIN\` (cũ) | `GIRDER SIZE, L1, T3, H1, NUMBER OF BOLT, GP, SF, REMARK` | L1 = 130 / T3 (sai nếu đọc theo vị trí) |

- `fVF` đếm cột từ 1, cột 1 là khoá. Điều này được kiểm chứng trên JSON: cột 2 = T3 = 11 = `PL11`. Bảng PLTHK cũng vậy: cột 2 = FIX_PL_THK, khoá 13 cho `PL14`.

### `YGY-TB-MOMENT-PLTHK.txt`
- P39 = cột 2 (FIX_PL_THK), tra theo P4 = chiều dày cánh Part 2 (làm tròn).
- Bắt buộc khi Part 1 cao hơn Part 2 (P25, có SF dưới). Khi đó không có bảng hoặc không có dòng thì **dừng**.
- Khi không bắt buộc thì vẫn tra để tính P43; không có bảng thì chỉ ghi log.
- `YGY-TB-MOMENT-WELD-K.txt` không đọc: P44 = cột khoá = P39, chỉ dùng cho nhánh chưa làm (P129).

### Tắt plugin / tắt model rồi mở lại
- Cả 2 bảng tìm qua `LookupTable.ResolveInModel`:
  - thư mục model có file cùng tên thì **luôn dùng file đó**;
  - chưa có thì tìm `XS_PROJECT` → `XS_FIRM` → thư mục plugin, **chép 1 bản vào model**, rồi dùng bản đó;
  - không bao giờ ghi đè file trong model.
- Sửa file ngoài model thì model **không** nhận. Muốn đổi bảng thì sửa file trong model rồi bấm Modify, hoặc xoá file trong model để lần sau chép lại.
- Chép thư mục model sang máy khác thì bảng đi theo model.

## So sánh với component gốc (theo JSON)

Hệ cục bộ: gốc ở giao trục Part 2 với mặt giữa bụng Part 1; X dọc Part 2; Y hướng lên. Số trong ngoặc là số đo trên JSON.

| Đối tượng gốc | Công thức `.uel` / số đo | Plugin | Trạng thái |
|---|---|---|---|
| Fitting Part 2 | đầu Part 2 cách mép cánh Part 1 **5** | `Fit` (knob `FitGap`) | đã làm |
| Vát CJP cánh trên | `D43 = (P4-2)*tan(P27)`, P27 = 45. Vát mặt ngoài, mặt chân 2 ở mặt trong | `CutBevel` | đã làm |
| Vát CJP cánh dưới | `D48 = (P4-2)*tan(P27)`. Vát mặt **trong** (mở lên), mặt chân 2 ở mặt ngoài | `CutBevel` | đã làm |
| Scallop 2 góc bụng (cắt `BL255`) | 1/4 cung R = P41 (35), tâm tại góc đầu bụng – mặt trong cánh | `CutScallop` (vẽ cung bằng điểm) | đã làm, chỉ kiểu P40 = 3 |
| GP `"PL"+P7`, SERIE `P30+"GP"`, class 99 | phần trong Part 1: từ bụng tới mép cánh **−20**, hết chiều cao giữa 2 cánh, khoét góc **R35**; tab cao P8, tới mép cánh **+B3 (135)**, bo chân tab R10 (P55); mặt giữa `D1 = ((P3+P7)/2)*P16` | `CreateGp` | đã làm |
| BACK SF `"PL"+P7` (P31), class 99 | phía bên kia bụng, tới mép cánh **−10**, khoét góc R35, cùng mặt phẳng GP | `CreateSide` | đã làm |
| SF dưới `"PL"+P39` (P25), class 83 | nằm ngang, mặt dưới = mặt dưới cánh dưới Part 2; x từ bụng tới mép cánh Part 1; z = ±P5/2; rãnh cho GP (cắt `BL70`): `D136 = P7/2+2`, dài tới cạnh GP **+5** | `CreateLowerSf` | đã làm |
| 2 FB, SERIE `P64+"FB"`, STARTNUMBER P50 | FB25*9 SS275 class 275; x từ mép cánh Part 1 rộng 25; dưới cánh trên / dưới cánh dưới; `D103 = P5/2+20` | `CreateFlatBar` | đã làm |
| Bu lông | M P12 T.S.B, `P19 = P9*P18`, lỗ ô van P15 ở Part 2, dung sai P13, cut length 200; x = đầu Part 2 **+65** | `CreateBolts` + `FixSlotSide` | đã làm |
| Hàn | GP, BACK SF, SF dưới hàn góc 6 vào Part 1, hàn xưởng | `Fillet` | đã làm |
| Tấm bù cánh trên `"PL"+T4` (T1) | khi cánh Part 2 dày hơn cánh Part 1 hơn 5 mm | — | **chưa làm** (`CANH BAO`) |
| Tấm bù cánh dưới `"PL"+P133` (P129) | điều kiện theo P43, P44, P133 | — | **chưa làm** (`CANH BAO`) |
| Gia cường cánh dưới, profile P46 (P43) | `P46 = fTpl("LENGTH", ...)+100` | — | **chưa làm** (`CANH BAO`) |
| obj#343 `"PL"+P7` (P37) | P37 luôn = 0, gốc không bao giờ tạo | — | bỏ qua |

## Biến trong hộp thoại
- **Đủ 9 biến của `.inp` gốc**, tên và kiểu giữ nguyên: P11, P12, P15, P30, P31, P40, P41, P43, CWS.
- Ô có mặc định là công thức (để trống thì tính như `.uel`, có nhập thì dùng giá trị nhập):
  - **P15** = P14 − P12 − P13;
  - **P41**: 25 khi Part 1 rộng < 181, còn lại 35;
  - **P43** = (P39 ≥ 21);
  - **CWS**: 700 khi cột 5 của bảng là `SPS`, còn lại 900.
- **P30** để trống thì lấy prefix theo main part Part 1 (`mid(prefix, 0, find(.., "M"))`), như `YGY.uel` mới và quy tắc 4.3. Bản CHANGSIN cũ mặc định là `B1`.
- **Biến mới:**
  - P65: file bảng tra dầm (`file_in`);
  - P66–P74: Name, Class, Finish của GP, SF và FB (tab Advanced);
  - P75, P76: profile và vật liệu FB;
  - P77: tiêu chuẩn bu lông.
  - Để trống thì giống bản gốc.

## Theo yêu cầu người dùng
- Hình minh hoạ vẽ theo phong cách hình mặc định của Tekla (người dùng 18/11/2026): nền xám nhạt, dầm xanh thép, tấm cam vàng, nét khuất đứt, bu lông là vòng tròn có dấu cộng. Mặt đứng nhìn dọc Part 1, đúng tỉ lệ cặp dầm trong JSON. Ô P30, CWS, P43 ở cột bên phải hình (P43 là chi tiết chưa rõ vị trí).
- Bu lông dùng **Washer2 + Nut1** theo NGUYEN_TAC_TU_RAKER mục 6. Bản gốc dùng Washer3 + Nut1.

## Giả định
1. **Chiều dài tab GP** lấy từ bảng tra: cột **B3** tính từ mép cánh Part 1 (bảng mới), hoặc cột **L1** tính từ đầu Part 2 (bảng cũ). Công thức `.uel` không có số này; trên JSON tab dài đúng 135 = B3 = L1 + 5. Không có cột nào thì dừng.
2. Các số đo trên JSON không ứng với biến `.uel` nào nên đưa vào KNOBS:
   - khe 5, bu lông cách đầu dầm 65;
   - GP lùi 20, BACK SF lùi 10 so với mép cánh Part 1;
   - khoét góc R35, rãnh SF dưới vượt cạnh GP 5;
   - hàn góc 6, cut length 200.
   - Chưa biết các số này có đổi theo profile hay không. Cần so thêm ít nhất một cặp dầm khác (ví dụ cặp trong ảnh Variables: Part 1 `H446*199`, Part 2 `H440*300*11*18`).
3. P55 = 10 (bo chân tab), tức giả định D174 ≥ 10.
4. **Phía GP:** P11 = 0 thì GP ở phía **+Z riêng của Part 2** (AxisX × AxisY của dầm), như JSON. Vì vậy phía GP phụ thuộc chiều vẽ Part 2, giống component gốc nếu gốc cũng theo hệ của dầm (chưa kiểm chứng khi vẽ ngược).
5. **Lỗ ô van luôn ở Part 2.** JSON: P11 = 0 cho Hole2 = Part 2. Với P11 = 1, `.uel` đổi P17 thành Hole1, plugin coi đó là để bù thứ tự tấm, nên vẫn đặt ở Part 2. Cần kiểm tra.
6. Lấy SF dưới rộng theo bề rộng Part 2 (P5) vì trên JSON Part 1 và Part 2 cùng rộng 200, chưa phân biệt được. Cần so trên cặp dầm khác bề rộng.
7. Giá trị Variables trong ảnh Editor (P7 = 0, P8 = 0, CWS = 700…) không khớp với D1/D17 trong cùng ảnh, có thể là số cũ chưa tính lại. Plugin không dựa vào các số đó.

## Chưa làm
- Tấm bù cánh trên T1, tấm bù cánh dưới P129, gia cường cánh dưới P43 (part dài P46). Chưa có vị trí, cần một model mẫu có các chi tiết này (xuất JSON) hoặc ảnh Editor.
- Part 2 xiên, dốc, xoay quanh trục, hoặc Part 1 dốc: hiện **dừng** và báo lỗi, chưa làm theo mục 4.4.
- Scallop kiểu khác P40 = 3.
- UDA cho part (tab Advanced chưa có).

## Kết quả so sánh
Chưa so: chưa chạy trong Tekla.
