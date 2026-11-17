# Phân tích bộ Custom Component Tekla 18.0 (thư mục `UEL Template`)

> Tạo tự động từ 76 file `.uel` + các file tra cứu `.txt/.dat/.xls`. Chi tiết từng component nằm trong `components/<thư mục>__<tên file>.uel.md`.
> Công cụ giải mã ở `tools/` (Python 3, không cần thư viện ngoài): `python tools/uel_report.py "<thư mục UEL>" "<thư mục xuất>"`.

## 1. Cấu trúc file `.uel` (đã giải mã)

File `.uel` = **gzip** của một chuỗi các "file con", mỗi file con có header dạng:

```
<FileInfo><Name>TÊN</Name><Size>N</Size><Compressed>0|1</Compressed></FileInfo> + N byte dữ liệu
```

| File con | Nội dung |
|---|---|
| `<Tên>.inp` | Định nghĩa hộp thoại (dialog) của component: `page()`, `joint()/detail()/macro()`, `tab_page()`, `parameter()` / `attribute()`, `picture()` |
| `<Tên>_ec_uel.txt` | Dòng đăng ký vào Component Catalog: `<số> <tên> <nhóm>` (vd. `-1000  0-DM-Z_BEAM  All`) |
| `<Tên>.uel_temp` | Cơ sở dữ liệu nhị phân "Xsteel 7.64" của component: các part mẫu, mặt phẳng (plane), bảng biến P/D, công thức, ràng buộc (binding) |
| `inp_bitmaps.arc` + `*.bmp` | Hình minh họa hiển thị trong dialog (chỉ có ở bộ NEC/TJ/TKK) |
| `attribute_files.arc` + `*.p_<Tên>` | Các bộ thông số đã lưu (Save as) – mở bằng "Load" trong dialog |
| `uel_name.txt` | Tên file gốc khi export |

Bên trong `.uel_temp` (Tekla 18, little-endian, mỗi bản ghi bắt đầu bằng byte `0x04`):

| Bản ghi | Bố cục |
|---|---|
| Chuỗi (string pool) | `id, cont_id, hash, refcount, char[29]` – chuỗi dài > 27 ký tự được nối qua `cont_id` |
| Biến tham số | `id, name[31], label[33], value_sid, type` – type: 0 Integer, 1 Length, 2 Text, 3 Angle, 4 Material, 5 Profile, 11 Chamfer, 14 Yes/No |
| Biến khoảng cách D | `objid, name_sid ("D12"), desc_sid ("D12.PLATE.BEAM")` |
| Binding | `recid, objid, 0, value_sid, propName` – `proVALUE` (giá trị biến), `proPROFILE`, `proMATERIAL`, `proCLASS`, `proSERIE` (prefix), `proCREATION` (có tạo part hay không), `proCHAMFER_X/Y`, `proSLOTTED_HOLE_X`… |

Chuỗi hiển thị (label tiếng Hàn) được mã hóa **CP949** – mở bằng Notepad trên Windows tiếng Việt sẽ bị lỗi font, đó là lý do tên như `♥♥♥YGY_TempBeam-Moment`, `★YGY_꺽보-R1_nopw` hiển thị sai trong một số trình xem.

## 2. Danh mục component

Loại trong `.inp`: **joint** = Connection (nối 2+ part), **detail** = Detail (gắn vào 1 part), **macro** = Custom part/seam (xem mục tài liệu). "Var" = số biến P/biến tự đặt; "D" = số biến khoảng cách (ràng buộc mặt phẳng); "Part" = số part có profile được gán công thức.

### Nhóm chung (gốc thư mục)
| File | Tên component | Loại | Dialog | Var | D | Part | File tra cứu |
|---|---|---|---|---|---|---|---|
| 0-DM-LUG+RB-OK-REV1 | 0-DM-LUG+RB-OK-REV1 | detail | 0 | 31 | 90 | 7 | – |
| 0-PL WELD | 0-PL WELD | detail | 11 | 15 | 72 | 0 | – |
| AIR HOLE YGY | ♥♥♥YGY_WG Air-Hole | detail | 3 | 7 | 6 | 0 | – |
| AIR HOLE | 0-P-AIR-HOLE | detail | 13 | 26 | 10 | 0 | – |
| BEAM+WALE | 0-DM-BEAM+WALE | joint | 0 | 35 | 156 | 4 | – |
| BK+SF CUT | BK+SF-CUT | joint | 0 | 0 | 12 | 0 | – |
| BKKKKKKKKK SAME | ♥YGY_BK-SAME | joint | 6 | 28 | 111 | 8 | – |
| BT | *(không có dialog)* | ? | – | 0 | 0 | 0 | – |
| CUT-PL-WALE | CUT_PL_V2 | joint | 0 | 1 | 9 | 0 | – |
| CUT-WALE | CUT_WALEEE | joint | 0 | 4 | 16 | 1 | – |
| CUTTING | 0-DM-PLATE (BEAM) CUT | detail | 4 | 9 | 18 | 2 | – |
| DM-HAU | 0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP) | macro | 9 | 40 | 213 | 5 | DM-HAUNCH.txt |
| H-COL-T_BAR | CWS-H_COLUMN-T-BAR-1(OK) | joint | 20 | 37 | 91 | 7 | **CT-BAR-H_COLUMN.txt (thiếu)** |
| PL+FILLER | PL+FILLER | detail | 1 | 3 | 4 | 0 | – |
| RAKER+TG / RAKER+T_GIRDER | 0-RAKER+TG | joint | 0 | 52 | 156 | 2 | – |
| RING PLATE / RING-PL | 0-RING PL | joint | 0 | 62 | 264 | 22 | **DM-1F-RING+WING.txt (thiếu)** |
| STUD H-COL | 0-P-STUD-(FLG+WEB)-1 | detail | 22 | 26 | 20 | 0 | – |
| STUD-ACT | 0-P-STUD-(FLG+WEB)-2 | detail | 22 | 26 | 20 | 0 | – |
| STUD WALL | 0-P-STUD(WALL) | detail | 7 | 7 | 10 | 0 | – |
| STUD4SIDES | 0-STUD-4SIDES | detail | 0 | 0 | 24 | 0 | – |
| TJ | TJ_WeldPrep_Scallop | detail | 3 | 8 | 0 | 2 | – |
| T_BAR SF / T_BAR+SF | 0-DM-T_BAR+SF | detail | 14 | 14 | 24 | 2 | – |
| WALE+T_GIRDER | 0-DM-WALE+T_GIRDER | joint | 0 | 52 | 267 | 11 | – |
| YGY-HAUNCH-MOMENT-TANG AM | ♥♥♥YGY_HAUNCH_Moment-TANG AM | joint | 16 | 46 | 137 | 6 | **YGY-HAUNCH_Moment-미운용층.txt (thiếu)** |
| YGY-R1 | ★YGY_꺽보-R1_nopw | joint | 9 | 146 | 246 | 38 | – (lồng TJ_WeldPrep_Flg_V, TJ_WeldPrep_Scallop, WeldBar_H) |
| Z_BEAM | 0-DM-Z_BEAM | joint | 10 | 15 | 41 | 4 | – |

### Theo thư mục con
| File | Tên component | Loại | Dialog | Var | D | Part | File tra cứu |
|---|---|---|---|---|---|---|---|
| 1066666666\BEAM DOUBLE SHEAR | BEAM-DOUBLE-H | joint | 2 | 20 | 63 | 4 | BEAM-2.txt |
| 1066666666\BEAM-GIRDER | 00-BEAM-GIRDER | joint | 0 | 22 | 126 | 8 | BEAM-2.txt, BEAM.txt |
| 1066666666\IB-H588-PLATE | IB-H588-PLATE | joint | 0 | 0 | 0 | 0 | – (gần như rỗng) |
| 240103\BE COL | ♥YGY_접합용 COL-FLG보강 | joint | 4 | 6 | 33 | 2 | – |
| 240103\CJP_FB_3SIDES | mk_PLATE개선_(CJP_FB)_3SIDE | detail | 5 | 27 | 0 | 6 | – |
| 240103\CSF TO CGP (+ _A) | YGY_(개선)-CSFtoCGP(_A) | joint | 28 | 0 | 36 | 0 | – |
| B-TO-TG\PIN-MOMENT | 0-DM-PIN MOMENT-V2 | joint | 0 | 104 | 156 | 7 | BEAM_TO_TG.txt |
| PIN-MOMENT\0-DM-PIN-MOMENT-V2 | 0-DM-PIN MOMENT-V2 | joint | 0 | 104 | 156 | 7 | BEAM_TO_TG.txt |
| COL TO GIRDER\0-DM-S-COL TO GIR | 0-DM_S-Col2Gir | joint | 30 | 76 | 197 | 7 | YGY-S_COL2GIR-운용층.txt |
| COL-GIRDER-P-BOX\YGY-S_COL2GIR-운용층-TANG_DUONG | 0-DM_S-Col2Gir_TANG_DUONG | joint | 31 | 76 | 197 | 7 | YGY-S_COL2GIR-운용층-TANG_DUONG.txt |
| PCOL\S-COL2GIR-TANG DUONG | 0-DM_S-Col2Gir_TANG_DUONG | joint | 30 | 71 | 197 | 7 | YGY-S_COL2GIR-운용층-TANG_DUONG.txt |
| COL-HUN-PBOX\YGY-HAUNCH_Moment | ♥♥♥YGY_HAUNCH_Moment | joint | 16 | 48 | 137 | 6 | YGY-HAUNCH_Moment.txt |
| HAUNCH\YGY HAUNCH | ♥♥♥YGY_HAUNCH_Moment | joint | 15 | 46 | 137 | 6 | YGY-HAUNCH_Moment.txt |
| HAUNCH\HAUCH | 0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP) | macro | 9 | 40 | 213 | 5 | DM-HAUNCH.txt |
| H-COL-GIRDER\H-COL | 0-DM_H-Col2Gir_H-COLUMN | joint | 13 | 75 | 135 | 3 | YGY-H_COL2GIR-운용층-H-COL.txt |
| JP11\01-SHEAR-JP11(DM) | 01-Shear-JP11(DM) | joint | 7 | 66 | 24 | 0 | Shear-JP11-SM355.dat, **Shear-JP11-SS275.dat (thiếu)** |
| PIN-MOMENT CHANGSIN\YGY_TEMPBEAM-MOMENT | ♥♥♥YGY_TempBeam-Moment | joint | 9 | 83 | 181 | 9 | YGY-TB-MOMENT(.txt, -PLTHK, -WELD-K) |
| PINMOMENT CHANGSIN\PIN MOMENT CHANGSIN | ♥♥♥YGY_TempBeam-Moment | joint | 9 | 83 | 181 | 9 | như trên |
| YGY MOMENT\YGY | ♥♥♥YGY_TempBeam-Moment | joint | 9 | 87 | 181 | 9 | như trên |
| YGY\BK K BANG NHAU | ♥YGY_BK-SMALL | joint | 9 | 21 | 122 | 10 | – |
| YGY\BK-END_PLATE | ♥YGY_BK-END_NEW | joint | 13 | 40 | 184 | 15 | – |
| YGY\BK-SAME | ♥YGY_BK-SAME | joint | 6 | 24 | 111 | 8 | – |
| YGY\BU DUONG HAN TSB | ♥YGY_접합용 하부PL보강-50 | joint | 2 | 6 | 45 | 3 | – |
| YGY\SUNG YGY | ★YGY_꺽보-R1_nopw | joint | 9 | 146 | 246 | 38 | – (giống YGY-R1) |
| YGY\THEM CSF COT | YGY_(개선)-COL-FLG보강 | joint | 4 | 6 | 33 | 2 | – |
| YGY\THEM PL TAM GIAC DUOI BEAM | YGY_(ACT)-삼각PLATE | joint | 6 | 5 | 9 | 1 | – |

### Bộ NEC / TJ / TKK (có hình minh họa, có `standard.p_*`)
| File | Tên component | Loại | Var | D | Part | Bitmap |
|---|---|---|---|---|---|---|
| CUSTOM_STEP | CustomStep_v2 | joint | 40 | 82 | 3 | 7 |
| HANDRAIL_SPLICE | Rail Splice | joint | 30 | 4 | 1 | 4 |
| NEC_AUTO_SF | NEC_COLUMN_STIFFENERS | joint | 142 | 454 | 38 | 9 |
| NEC_BASEPLATE | NEC_BASEPLATE | detail | 43 | 230 | 16 | 23 |
| NEC_EMBED | NEC_EMBED | detail | 91 | 82 | 5 | 9 |
| NEC_EQUIP_DET1 | NEC_EQUIP_Det1 | detail | 69 | 22 | 17 | 6 |
| NEC_GIRDER | NEC_GIRDER | macro | 22 | 6 | 3 | 2 |
| NEC_HAUNCH | NEC_HAUNCH | detail | 63 | 50 | 12 | 9 |
| NEC_OPEN | NEC_OPEN | detail | 30 | 228 | 20 | 3 |
| NEC_PIPE_COL1 / COL2 | NEC_PIPE_COLUMN1/2 | joint | 23 | 46/27 | 5/3 | 4/6 |
| NEC_POST | NEC_POST | joint | 31 | 23 | 8 | 5 |
| NEC_SF_C / NEC_SF_H | NEC_STIFFENER_C / NEC_STIFFENER | detail | 42/60 | 22/25 | 4/7 | 6 |
| TJ_LIFTTING_HOLE_BEAM / COL | TJ_LiftingHolesBeam/Column | detail | 7 | 12 | 0 | 1/0 |
| TJ_PIPECAP | TJ_PipeCap | detail | 17 | 12 | 1 | 1 |
| TKK_BH_1HNC_1SEC / TKK_BUILT_T / TKK_SRC_CROSS | TKK_BH_1Hnc_1Sec / TKK_BuiltT / TKK_SRC_Cross | macro | 31/26/18 | 14/12/18 | 3/2/3 | 2/1/2 |

## 3. Các hàm được dùng trong công thức (thống kê toàn bộ)

| Hàm | Số lần | Ý nghĩa (theo cách dùng trong file) |
|---|---|---|
| `fP(proT/proH/proB/proS/proR, "GUID")` | ~415 | Đọc kích thước profile của part đầu vào: T = bề dày cánh, H = chiều cao, B = bề rộng, S = bề dày bụng, R = bán kính |
| `fP(proPROFILE / proPROFILE_NAME / proMATERIAL / proSERIE, …)` | ~108 | Đọc tên profile, vật liệu, prefix part |
| `fP(proASSEMBLYSERIE / proASSEMBLYSTARTNUMBER, …)` | 20 | Đọc prefix / số bắt đầu của assembly |
| `fP("w1_type", …)`, `fP("ctab1", …)`, `fP("tpl1", …)` | ~13 | Đọc thuộc tính của component lồng (weld type, …) |
| `fTpl("LENGTH" / "LENGTH_GROSS" / "TOP_LEVEL_UNFORMATTED", …)` | 22 | Đọc template attribute của part |
| `fVF("file", key, cột)` | 118 | Tra bảng trong file text (mục 4) |
| `fAD("slope" / "skew", …)` | 10 | Góc dốc / góc xiên của part (dùng trong RAKER, WALE, PIN-MOMENT, COL2GIR) |
| `fMarketSize("SS400", P, 0)` | 4 | Làm tròn bề dày lên theo kích thước thương mại (bộ NEC) |
| `if … then … else … endif`, `mid()`, `tan/sin/cos`, `int/round/max/min` | – | Logic và toán học |

## 4. File tra cứu (`fVF`) – tình trạng

`fVF("tên_file", giá_trị_khóa, số_cột)` tìm dòng có cột đầu = khóa (thường là tên profile, vd. `H606*201*12*20`) và trả về cột thứ *n* (cột khóa = 0, theo cách đếm khớp với dữ liệu trong thư mục này).

**Vấn đề phát hiện được:**

1. **File bị thiếu** – component sẽ cho giá trị rỗng/0 khi chèn:
   - `CT-BAR-H_COLUMN.txt` (H-COL-T_BAR.uel)
   - `DM-1F-RING+WING.txt` (RING PLATE.uel, RING-PL.uel)
   - `YGY-HAUNCH_Moment-미운용층.txt` (YGY-HAUNCH-MOMENT-TANG AM.uel) – trong thư mục chỉ có `YGY-HAUNCH_Moment.txt`
   - `Shear-JP11-SS275.dat` (JP11\01-SHEAR-JP11(DM).uel)
2. **Cùng tên file nhưng khác nội dung/khác số cột** – Tekla chỉ đọc file đầu tiên tìm thấy (thư mục model → XS_PROJECT → XS_FIRM …), nên để chung sẽ dùng nhầm:
   - `YGY-TB-MOMENT.txt` có **4 phiên bản**: gốc & `YGY MOMENT\` (7 cột, không có L1); `PIN-MOMENT CHANGSIN\` (8 cột, có L1); `PINMOMENT CHANGSIN\` (định dạng cũ có dòng tiêu đề tiếng Hàn + dòng `====`). Component TempBeam-Moment đọc cột 1,2,3,5,6,7 → chỉ khớp với bản 8 cột (cột 2 = T3 được so `P7>=21`, cột 3 = H1). Với bản 7 cột, giá trị bị lệch 1 cột.
   - `BEAM_TO_TG.txt` (B-TO-TG vs PIN-MOMENT), `YGY-HAUNCH_Moment.txt` (COL-HUN-PBOX vs PCOL), `YGY-S_COL2GIR-운용층-TANG_DUONG.txt` (COL-GIRDER-P-BOX vs PCOL), `Shear-JP11-SM355.dat` (gốc vs JP11) – mỗi cặp khác nhau.
3. **File không được component nào trong thư mục dùng**: `BEAM-1.txt`, `副本BEAM-2.txt`, `BeamShear-Date.dat`, `GAGE.txt`, `GIRDER-M.txt`, `GS-SPLICE.dat`, `INNOX-BEAM(-DOUBLE).dat`, `K-CUT.dat`, `PJP.dat`, `PLATE K CUT.txt`, `JP11\22222.txt` (có thể dùng bởi component khác không nằm trong thư mục này).
4. Các file `.xls` (`JP11.xls`, `P-BOX TO GIRDER.xls`, `B-TO GIRDER.xls`, `P-BOX TO HUN.xls`, `YGY-*.xls`) là bảng nguồn để xuất ra `.txt` (component Tekla 18 không đọc trực tiếp .xls qua `fVF`).

## 5. Lỗi/điểm cần chú ý trong công thức (ví dụ)

- `♥♥♥YGY_TempBeam-Moment` – `P14 (SLOT_HOLE_LENGTH)`: chuỗi `if … else endif` có nhánh `else` rỗng → với bulông ngoài danh sách 16/20/22/24/27 (vd. M30) biến sẽ rỗng/lỗi.
- Cùng component – `P60` khai báo kiểu Yes/No nhưng công thức trả về `P133` (một chiều dài).
- Nhiều biến có label trùng `Parameter58` (P117–P134, gw) – do copy, không ảnh hưởng tính toán nhưng khó đọc.
- Các cặp file gần như trùng (RAKER+TG/RAKER+T_GIRDER, RING PLATE/RING-PL, T_BAR SF/T_BAR+SF, STUD H-COL/STUD-ACT, YGY-R1/SUNG YGY, PIN-MOMENT ×2, TempBeam-Moment ×3, S-Col2Gir_TANG_DUONG ×2) – nên giữ 1 bản chuẩn.
- `BT.uel` không có dialog và không có biến; `IB-H588-PLATE.uel` không có biến/ràng buộc → có thể là bản export lỗi/rỗng.

## 6. Tài liệu tham khảo

Lưu ý: Tekla không còn trang trợ giúp riêng cho bản 18.0 (support.tekla.com chuyển hướng sang help.trimble.com). Phần lớn thông tin dưới đây lấy từ tài liệu bản 2019–2026; chỗ nào suy ra cho bản 18.0 đều ghi rõ.

**Lưu trữ và trao đổi**
- Có 4 loại custom component: connection, detail, seam và part. Định nghĩa được lưu trong `xslib.db1` của model. Dialog là file `<Tên>.inp` trong `<model>\CustomComponentDialogFiles\`. Nguồn: https://support.tekla.com/article/location-of-custom-component-data
- Export/Import `.uel` qua Component Catalog (Ctrl+F) bằng Export hoặc Import. Không nên đổi tên file `.uel` sau khi export. Nguồn: https://support.tekla.com/doc/tekla-structures/2022/det_import_export_custom_components
- **Chỉ từ bản 2021**, file `.uel` mới đóng gói kèm file dữ liệu `fVF`. Vì vậy `.uel` của bản 18.0 **không chứa** các file `.txt/.dat`, phải chép riêng vào thư mục model, XS_PROJECT hoặc XS_FIRM. Nguồn: https://support.tekla.com/doc/tekla-structures/2021/rel_2021_improvements_in_cc_handling
- Biến môi trường `XS_UEL_IMPORT_FOLDER` tự import mọi `.uel` vào model **mới**. Tài liệu chỉ xác nhận từ bản 2019; chưa rõ bản 18.0 có hay không.

**Hàm trong công thức** (https://support.tekla.com/doc/tekla-structures/2024/det_functions_in_variable_formulas)
- `fP(thuộc_tính,"GUID")` lấy bằng chuột phải → Copy Reference trong Custom component browser. Trong file, tên nội bộ của thuộc tính có dạng `proH`, `proT`, `proB`, `proS`, `proR`…
- Các hàm khác:
  - `fTpl("TEMPLATE_ATTR","GUID")`
  - `fAD("skew"|"slope"|"cant", GUID)`
  - `fMarketSize(vật_liệu, bề_dày, bước)` đọc `marketsize.dat`
  - `fValueOf`
  - `if … then … else … endif`
  - `mid(chuỗi, vị_trí_từ_0, độ_dài)`
  - các hàm toán học `sin cos tan …` (góc có tiền tố `d` hoặc `r`)
- `fVF("file", khóa, cột)`: thứ tự cột tính từ 1 sau cột khóa. Dữ liệu trong thư mục này cũng khớp cách đếm đó.
  - Thư mục tìm file theo thứ tự: model → XS_PROJECT → XS_FIRM → XS_SYSTEM → … Nguồn: https://support.tekla.com/article/how-to-read-data-from-a-text-file-in-custom-components
  - **Trước bản 2021** các cột cách nhau bằng khoảng trắng và **mỗi dòng phải kết thúc bằng một dấu cách**. Các file `-PLTHK` và `-WELD-K` có dấu cách cuối dòng, đúng quy tắc này. Khi sửa các file `.txt` khác trên bản 18.0 nên giữ quy tắc đó.
- Liên kết Excel: thêm biến Yes/No tên `use_externaldesign`, rồi đặt file `component_<tên>.xls` trong `<model>\exceldesign\`. Nguồn: https://support.tekla.com/doc/tekla-structures/2024/det_example_link_excel_spreadsheet_to_cc. Tính năng có thể đã có từ khoảng bản 11–16 nhưng chưa được xác nhận chính thức. Không component nào ở đây dùng tính năng này.

**Biến, mặt phẳng, dialog**
- Biến tham số có tiền tố P. Biến khoảng cách có tiền tố D, dùng để gắn đối tượng vào mặt phẳng (tối đa 3 mặt phẳng mỗi đối tượng). Mỗi biến có thiết lập Visibility: Show hoặc Hide. Nguồn: https://support.tekla.com/doc/tekla-structures/2024/det_add_variables
- Cú pháp `.inp`: `parameter("Nhãn","P1", distance, number, thứ_tự)` và `attribute(…){ value(…) }`. Ảnh minh họa phải là `.bmp`, mỗi tab tối đa 25 trường. Nguồn: https://support.tekla.com/doc/tekla-structures/2024/det_modify_cc_input_file_manually
- File `.p_<Tên component>` (ví dụ `standard.p_NEC_BASEPLATE`) là bộ thông số đã lưu. Bên trong là các dòng `joint_attributes.Pn giá_trị`. Khi dùng, chúng nằm trong `<model>\attributes\` và có thể chép sang thư mục XS_PROJECT hoặc XS_FIRM. Nguồn: https://support.tekla.com/doc/tekla-structures/2024/sys_standard_files

**Tương thích phiên bản**
- `.uel` chỉ import được **từ bản cũ sang bản mới**. Import ngược sẽ báo lỗi "Export version not recognized". Như vậy các file 18.0 này dùng được trên 2017–2025. Nguồn: https://support.tekla.com/article/custom-component-import-failed-export-version-not-recognized
- Khi mở component cũ trong bản mới, Tekla có thể đề nghị cập nhật. Nếu đồng ý, cần kiểm tra lại các dimension và công thức phụ thuộc. Nguồn: https://support.tekla.com/doc/tekla-structures/2024/det_custom_component_tips
- Từ 2021, tên biến không được chứa `+-*/`. Các tên như `P5_PROF` hay `TF2` vẫn hợp lệ.
