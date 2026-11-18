# COL_TO_GIRDER — liên kết Cột → Dầm H (CGP + bu lông, cánh hàn CJP)

| | |
|---|---|
| Tên trong Tekla | `COL_TO_GIRDER` (bản DEV: `COL_TO_GIRDER_DEV`) |
| Component gốc | `0-DM_S-Col2Gir`, file [`File goc\COL TO GIRDER\COL_TO_GIRDER.uel`](../../File%20goc/COL%20TO%20GIRDER/COL_TO_GIRDER.uel) (cùng nội dung với `0-DM-S-COL TO GIR.uel` cũ) |
| Phân tích | [`_PHAN_TICH\components\COL TO GIRDER__0-DM-S-COL TO GIR.uel.md`](../../_PHAN_TICH/components/COL%20TO%20GIRDER__0-DM-S-COL%20TO%20GIR.uel.md) |
| Loại | connection (`-1000`), 1 cấu kiện phụ |
| **Part 1** | cột (primary, chọn trước) — *giả định*, xem mục Giả định |
| **Part 2** | dầm H (secondary) |
| Bảng tra bắt buộc | `YGY-S_COL2GIR-운용층.txt` (cp949, TAB) — tra theo profile Part 2 |
| Trạng thái | 18/11/2026: viết xong, `build.bat core` OK cả 2 bộ. **Chưa chạy trong Tekla.** |

## Các file (cùng một chỗ để dễ thử)

| File | Nội dung |
|---|---|
| `Params.cs` | Thông số, tên biến đúng `.uel` gốc (P4, P11 … P65) |
| `Builder.cs` | Hình học |
| `Component.cs` | Tiêu đề, câu nhắc chọn, đọc bảng tra bắt buộc |
| `Plugin.cs` | Hộp thoại (2 tab như gốc, có hình) + `[StructuresField]` |
| `COL_TO_GIRDER.txt` | Bảng tra mặc định, **định dạng chuẩn** (18/11/2026): ghi chú `#` ở đầu, cột 1–7 như bảng gốc `YGY-S_COL2GIR-운용층.txt` (cùng số liệu, bỏ dòng trống và 1 dòng trùng `HUN1400-1000*400*23*60`, bỏ cột 8 `BL`), thêm cột tuỳ chọn `e`, `L1`, `tip` (để trống). TAB, CRLF, ASCII. `install.bat` chép vào thư mục plugin của Tekla nếu ở đó chưa có |
| `bitmaps\UEL_COL2GIR_1.bmp`, `_2.bmp` | Hình hộp thoại |
| `_hinh\VeHinh.cs`, `_hinh\dialog.inp` | Chương trình vẽ hình + sinh `.inp` |

## Cách thử

1. `build.bat dev COL_TO_GIRDER` (component mới → phải build vỏ DEV).
2. Tắt Tekla → cmd **Administrator** → `install.bat dev <bản> COL_TO_GIRDER` (chép DLL, hình, `COL_TO_GIRDER.txt`).
3. Mở Tekla, model có 1 cột hộp và 1 dầm H (profile có trong bảng, vd. `H500*200*10*16`). Hoặc dùng `_Dev\UEL_TESTER.exe`.
4. Đặt `COL_TO_GIRDER_DEV`: chọn **cột trước, dầm sau**. Xem log `%TEMP%\UEL_PLUGINS.log`.
5. So với component gốc bằng nút **So sánh** của EXE (cùng preset, vd. `+B5`).

Bảng tra: không có dòng cho profile dầm thì **dừng** và ghi `LOI` (không tự điền số).

### Bảng tra lưu trong model (BẮT BUỘC, người dùng 18/11/2026)
Mặc định và ưu tiên dùng file txt **trong thư mục model**, vì số liệu bảng tra quyết định kích thước CGP, WP, bu lông, không được nhầm file. Code ở `Component.ReadTable` → `LookupTable.ResolveInModel`. Mỗi lần chạy (Insert/Modify), plugin chọn file như sau:
1. Lấy **tên file** trong P78, bỏ phần thư mục. P78 trống thì lấy **`COL_TO_GIRDER.txt`** (người dùng 18/11/2026; trước là `YGY-S_COL2GIR-운용층.txt`).
2. Thư mục model đã có file **cùng tên** thì **luôn dùng file đó**, kể cả khi P78 trỏ tới chỗ khác. Lúc đó log ghi "dung ban trong model, KHONG dung …".
3. Model chưa có thì tìm file nguồn: đường dẫn đầy đủ thì dùng đúng file đó; chỉ có tên thì tìm `XS_PROJECT` → `XS_FIRM` → thư mục plugin. Tìm thấy thì **chép 1 bản vào model** rồi dùng bản trong model.
4. P78 trống mà không thấy `COL_TO_GIRDER.txt` thì tìm tên gốc của `.uel` `YGY-S_COL2GIR-운용층.txt` (ghi `CANH BAO`), bản này cũng được chép vào model. Nhập P78 mà không thấy file thì **dừng**.

Trên hộp thoại, ô có giá trị đọc từ bảng tra ghi **"(auto)"**: L3 (P47), pitch (P22), e (P4), L (P28), L1 (P81), tip (P82). Ô trống = theo bảng, nhập = theo ô.
5. **Không bao giờ ghi đè** file trong model. Log ghi đường dẫn đầy đủ của file đã dùng, dòng khớp và các số đã lấy.

**Tắt plugin / tắt model rồi mở lại:** P78 được lưu cùng component trong model, nên bước 2 luôn tìm thấy bản đã chép.
- Xoá hoặc di chuyển file ngoài: không ảnh hưởng.
- Sửa file ngoài: model **không** nhận thay đổi.
- Chép cả thư mục model sang máy khác: bảng tra đi theo model.
- Muốn đổi bảng: sửa file trong model rồi bấm Modify, hoặc xoá file trong model để lần sau chép lại. Sau đó kiểm tra log.

> **Cảnh báo trùng tên:** `File goc\COL-GIRDER-P-BOX\` và `File goc\PCOL\` có hai file **cùng tên** `YGY-S_COL2GIR-운용층-TANG_DUONG.txt` nhưng **khác nội dung**. Trong một model chỉ giữ được một file tên này: file nào chép vào trước thì file kia bị bỏ qua (log có ghi). Nếu một model cần cả hai, hãy tự chép một file vào thư mục model với tên khác (vd. thêm đuôi `-PCOL`) rồi nhập tên mới vào P78. Không đổi tên file trong `File goc\`.

## So sánh với component gốc

| Đối tượng gốc | Công thức `.uel` | Plugin | Trạng thái |
|---|---|---|---|
| Fitting đầu dầm | `D5 = if (P14==1) then 5 else 20` | `Fit` (D5 = P75 / P74) | đã làm |
| Lùi cánh theo ring | `D46/D47 = 5+P56`, `D51/D52 = 5+P55` | `SlabCut` (chỉ khi ring > 0) | đã làm |
| Cắt xuyên cánh dưới sát cột (P14 = 1) | — (người dùng 18/11/2026: cánh dưới còn sót, chưa đứt hết) | `SlabCut` xuyên hết chiều dày cánh dưới, từ đầu cánh tại mặt chân lùi về phía cột 200 mm; 2 đầu song song mặt cột | đã làm |
| Vát mép CJP 2 cánh (P14 = 1) | `D48 = (P8-2)*tan(P7)+D5+P56`, `D53 = …+P55`, `P7 = 45` | `CutBevel` **giống hệt RAKER+T_GIRDER** (người dùng 18/11/2026, sau khi thử kéo dài khối cắt / bỏ mặt chân không đạt): cánh trên vát mặt ngoài, cánh dưới vát mặt trong, mặt chân 2 mm, khối cắt vượt mặt cánh 3 mm. Góc **P7** đưa ra hộp thoại | đã làm |
| CGP (obj#306) | `PROFILE "PL"+P17`, `MATERIAL P13`, `SERIE P12+"CGP"`, `STARTNUMBER P52`; `D1..D4 = ((P1+P3)/2)*P9`, `D26..D29 = P19/2`, `D22/D23 = P69`, `D24/D25 = P67` | `CreateCgp` | đã làm |
| Bu lông (obj#467) | `SIZE P40_diameter`, `STANDARD P40_screwdin`, `BOLT_GROUP_DISTY P22`, `DX1 P4`, `TOLERANCE P26`, `SLOTTED_HOLE_X P27`, `SLOTTED_HOLE_PARTS P41`; `D8 = P57+P47` | `CreateBolts` | đã làm (vị trí theo giả định) |
| FB obj#316/#327 (P14) | `SERIE P54+"FB"`, `D56/D57 = 20` (hoặc `D113/D122+20` khi có WP) | `CreateFlatBar` | đã làm (profile, vật liệu giả định) |
| WP1/WP2 obj#285/#297 | `CREATION P50/P51`, `PROFILE "PL"+P8`, `MATERIAL P37`, `SERIE P54+"WP"`, `STARTNUMBER P53`; `D96/D121 = (P28+P55)/2`, `D97/D98 = P28+P55`, `D114/D130 = P35` | `CreateWing` | đã làm 18/11/2026 theo ảnh mặt bằng, chỉ cánh trên (giả định 11–12) |
| Cắt bụng dầm obj#353/#333 | `CREATION P62/P63`; `D186/D187 = P60+10`, `D194/D195 = P61+10` | — | **chưa làm** (`CANH BAO` khi bật) |
| Cắt CGP obj#457/#446 | `CREATION P64/P65` | — | **chưa làm** (`CANH BAO` khi bật) |
| Cắt obj#362/#372/#381/#389/#400/#417/#435 | `CREATION P14`, `PROFILE P6+P23`, `P28+100`, `P28+500` | — | chưa rõ là gì (có thể là khối cắt vát mép/WP) |
| Scallop góc bụng–cánh (P14 = 1), cả cánh trên và cánh dưới | `D68/D69 = P55+35`, `D72/D73 = 35+P56` | `CutScallop` (người dùng 18/11/2026): khối cắt chữ nhật dài 200, sâu **P80** (35 / 30) từ mặt trong cánh, góc phía dầm **bo tròn R = P80** (như ảnh chamfer rounding người dùng gửi) → mép scallop là 1/4 cung tròn, **vẽ bằng 13 điểm** (không dùng chamfer rounding của Tekla: bán kính bằng đúng chiều cao khối cắt làm boolean hỏng, dầm mất hình dạng — lỗi gặp 18/11/2026); đầu phía dầm cách mặt cột = đầu cánh + P80 | đã làm |
| Hàn | — | CGP–cột (xưởng), FB–dầm (xưởng), cánh–cột CJP (công trường) | đã làm (cỡ 6 giả định) |

## Theo yêu cầu người dùng: một plugin cho mọi bản tuỳ biến theo dự án (18/11/2026)

Ba file `.uel` sau thực chất là một component, người dùng sửa lại cho từng dự án:

| File | Tên trong catalog |
|---|---|
| `File goc\COL TO GIRDER\COL_TO_GIRDER.uel` (gốc của plugin) | `0-DM_S-Col2Gir` |
| `File goc\COL-GIRDER-P-BOX\YGY-S_COL2GIR-운용층-TANG_DUONG.uel` | `0-DM_S-Col2Gir_TANG_DUONG` |
| `File goc\PCOL\S-COL2GIR-TANG DUONG.uel` | `0-DM_S-Col2Gir_TANG_DUONG` |

Chỗ khác nhau → **biến mới** P74–P78. Để trống = chạy như `COL_TO_GIRDER.uel`. Mỗi dự án lưu 1 preset trong hộp thoại.

| Biến mới | Ý nghĩa | COL_TO_GIRDER | P-BOX | PCOL |
|---|---|---|---|---|
| P74 | Khe đầu dầm D5 khi PIN | 20 (mặc định) | **30** | 20 |
| P75 | Khe đầu dầm D5 khi MO | 5 (mặc định) | 5 | 5 |
| P76 | Đuôi mark bản cánh (`P54 + P76`) | `WP` (mặc định) | **`GWP`** | **`GWP`** |
| P77 | FB 2 bên theo WP1 (`P44 = P45`) | No (mặc định) | **Yes** | No |
| P7 | Góc vát mép hàn (có trong `.uel`, nay đưa ra hộp thoại) | 45 | | |
| P79 | Lỗ ô van ở dầm (Yes) / ở CGP (No) | No = CGP | | |
| P80 | Scallop: chiều sâu = bán kính bo tròn | 35 (hoặc 30) | | |
| P81 | WP: đoạn rộng tới mép cột, đo từ mặt cột | trống = (P28 + ring)/2 | | |
| P82 | WP: bề rộng đầu xa ngoài mép cánh (0 = nhọn) | trống = P35 | | |
| P78 | File bảng tra (nút "...") | trống = `YGY-S_COL2GIR-운용층.txt` | `File goc\COL-GIRDER-P-BOX\YGY-S_COL2GIR-운용층-TANG_DUONG.txt` | `File goc\PCOL\YGY-S_COL2GIR-운용층-TANG_DUONG.txt` |
| (P54) | Phần đầu prefix FB/WP | theo prefix dầm | theo prefix dầm | `1A-B1` → nhập vào ô P54 |

- P78: đường dẫn đầy đủ, hoặc chỉ tên file. Model đã có file cùng tên thì dùng bản trong model; chưa có thì chép 1 bản vào model (mục "Bảng tra lưu trong model"). Nhập P78 mà không thấy file thì **dừng** (không lùi về bảng gốc).
- Khoá bảng tra: bỏ dấu `-` ngay sau chữ cái đầu, nên `H-500x200x10x16` (bảng TANG_DUONG) và `H500*200*10*16` (bảng gốc) khớp cùng profile dầm. `-` giữa 2 số (`HUN1100-792…`) giữ nguyên.
- Cột bảng TANG_DUONG cùng vị trí (2 L3, 3 B3, 4 T3, 5 WING PL, 7 BOLT-ROW); cột `Ht`, `Ht_thk`, `REMARK` không dùng (`.uel` cũng không dùng).
- Chưa gộp: khối cắt `P28+500` (gốc) / `P28+100` (TANG_DUONG) — chưa biết là gì. Bản P-BOX cho biết WP buộc vào nửa bề rộng cột (`D101–D104 = P29/2`): dùng khi làm WP.
- Bản P-BOX đưa `P73` ra hộp thoại chỉ để xem phần đầu prefix → không thêm.

## Biến trong hộp thoại

Đủ 30 biến của `.inp` gốc + biến mới (mục trên). Tab ` Parameters 1 ` chứa toàn bộ thông số hình học (người dùng 18/11/2026: gộp tab ` Parameters 2 ` vào; tên biến không đổi nên model đã đặt component vẫn đọc được).

**Tab ` Advanced `** (người dùng 18/11/2026, không có hình): thuộc tính part của các part sinh ra. Ô trống = giữ mặc định.

| | CGP | WP | FB |
|---|---|---|---|
| Name | P84 (`CGP`) | P88 (`WP`) | P92 (`FB`) |
| Class (màu) | P85 | P89 | P93 |
| Finish | P86 | P90 | P94 |
| UDA `TEN=GIA_TRI; TEN2=GIA_TRI2` | P87 | P91 | P95 |
| Profile / Material / Start no. | tab 1 (bảng tra, P13, P52) | tab 1 (P28…, P37, P53) | P96 (`FB25*9`) / P97 (`SS275`) / P98 (1) |

UDA: số nguyên → int, số thực → double, còn lại (hoặc trong `"..."`) → chuỗi; tên thuộc tính phải có trong `objects.inp` của Tekla mới hiện trong hộp thoại part. Không có prefix / số bắt đầu **assembly**: CGP, WP, FB là part phụ, assembly theo main part.
Hình hộp thoại vẽ theo ảnh component gốc: mặt đứng (dầm trái, cột phải, ring, CGP, bu lông, scallop, vùng cắt P62–P65 gạch, P61 cạnh ring dưới) + mặt bằng (WP hình thang 2 bên cánh, rộng tới mép cột). Cột phải: thuộc tính CGP, WP/FB. Dải dưới hình: ô P62–P65 và file bảng tra P78. Nét mảnh, nhãn ngắn, ô đặt cạnh kích thước.
Ô có mặc định là công thức (để trống = tính như `.uel`, nhập = dùng giá trị nhập):

| Biến | Công thức gốc |
|---|---|
| P12 CGP-ZONE | `P73 = mid(prefix cột, 0, find(.., "M"))` → plugin: theo main part của assembly cột |
| P54 GWP_ZONE | `P71 = mid(prefix dầm, 0, find(.., "M"))` → plugin: theo main part của assembly dầm |
| P22 BOLT_PITCH | `P18*P21`, `P21 = (P19-P21A)/2` nếu `H >= P21B`, ngược lại `P19-P21A` |
| P24 SLOT_HOLE | M16 40, M20 50, M22 55, M24 60, M27 67 |
| P28, P47 | bảng tra cột 5 (WING PL), cột 2 (L3) |
| P37 WING_MAT | `SHN355 → SN355`, `SHN275 → SN275`, còn lại = vật liệu dầm |
| P50, P51 | `(floor(D113/D122) < 74 or P14 = 0) ? 0 : 1` |

Bảng tra (cột đếm từ 1, như `fVF`): 2 = L3 (P47), 3 = B3 (P19), 4 = t3 (P3, P17), 5 = WING PL (P28), 7 = BOLT ROW (P18).
**Cột tuỳ chọn** (người dùng 18/11/2026), đặt **sau cột 7** (vd. cuối dòng), tiêu đề:

| Tiêu đề | Biến | Ý nghĩa | Không có cột / ô trống |
|---|---|---|---|
| `e` (hoặc `P4`) | P4 | bu lông → cạnh ngoài CGP | 65 |
| `L1` (hoặc `P81`, `WP L1`) | P81 | WP: đoạn rộng tới mép cột, đo từ mặt cột | (P28 + ring)/2 |
| `tip` (hoặc `P82`, `WP Tip`) | P82 | WP: bề rộng đầu xa ngoài mép cánh | P35 |

Ô hộp thoại có nhập thì thắng bảng. **Chiều dài WP (P28) = cột 5 `WING PL`** (bắt buộc, như `.uel`).

## Prefix

Làm như `.uel`: `SERIE = P12 + "CGP"`, `P54 + "FB"`, `P54 + "WP"` (đuôi cố định, ô hộp thoại là **phần đầu**).
Ô P12/P54 để trống thì phần đầu lấy theo **main part của assembly** (Plugin\CLAUDE.md mục 4.3): CGP hàn vào cột → assembly Part 1; FB/WP hàn vào dầm → assembly Part 2. `.uel` gốc lấy prefix của chính cột/dầm.

## Giả định

1. **Part 1 = cột, Part 2 = dầm.** Suy từ `P12 = P73` (prefix cột cho CGP) và mã đối tượng trong `.uel`; chưa thấy thứ tự chọn của bản gốc.
2. Mặt đặt = mặt bao ngoài của cột mà trục dầm đi vào (như RAKER). Góc xiên `P66 = acos(n.X)`. Dầm dốc: CGP và bu lông làm như RAKER (mục 4, 5); các phần khác chưa thử với dầm dốc.
3. **Phía CGP** (P11, P83 — người dùng 18/11/2026: để lắp dựng, CGP phải cùng 1 phía với mọi dầm):
   - **P83 = ô danh sách** (người dùng 18/11/2026): `0 +X / +Y` (mặc định), `1 +X / −Y`, `2 −X / +Y`, `3 −X / −Y`, `4 Theo dam`. Với 0–3: dầm gần song song trục Y (pháp tuyến bụng theo X) đặt theo dấu X, dầm gần song song trục X đặt theo dấu Y (dầm chéo 45° → xét X). Không phụ thuộc chiều vẽ dầm.
     `4 Theo dam` = theo +Z riêng của dầm (AxisX × AxisY) như bản đầu: cùng phía ở 2 đầu 1 dầm, nhưng phụ thuộc chiều vẽ / xoay dầm.
   - **P11 = Yes**: sửa tay cho riêng liên kết đó — đảo CGP sang phía đối diện với lựa chọn P83.
   - Log: `CGP: dat phia +X model (P83 = 0 +X / +Y)`.
4. CGP nằm trong mặt song song bụng, tâm theo chiều cao ở trục dầm, dài L3 dọc dầm. **Part 2 nghiêng so với cột** (người dùng 18/11/2026, làm như bản mã RAKER+T_GIRDER): CGP là **hình bình hành**, cạnh ngắn phía cột nằm trên vết mặt cột, cạnh ngắn ngoài song song với nó; chiều cao B3 đo vuông góc cạnh dài. Dầm xiên (lệch ngang) thì đẩy cả tấm dọc dầm tới khi góc sâu nhất chạm mặt cột (tương đương `P67`), không cắt tấm. Dầm vuông góc cột thì vẫn là chữ nhật.
5. **Bu lông** (Extra length = 0, người dùng 18/11/2026): 1 cột bu lông **song song cạnh ngắn CGP**, cách cạnh ngắn ngoài đúng P4 (`DX1 = P4`) đo vuông góc cạnh đó. Bỏ `+P57` của `D8 = P57+P47` (18/11/2026: đo trên model, có ring 29 thì e ra 36.47 thay vì 65). **L3 đo vuông góc 2 cạnh ngắn** (dọc dầm = L3·√(1+k²)). Các con theo P22, khoảng cách và mép đo vuông góc cạnh dài (theo chiều cao dầm), đối xứng quanh trục dầm. Đọc lại vị trí sau khi tạo; trục Y của nhóm ngược thì tạo lại. Lỗ ô van dọc dầm, **luôn ở CGP** (P79 = Yes thì ở dầm). Cờ `Hole1`/`Hole2` áp cho tấm thứ 1/thứ 2 theo thứ tự Tekla lưu trong nhóm bu lông, và Tekla có thể đổi thứ tự này khi dầm nghiêng/vặn (lỗi người dùng gặp 18/11/2026). Plugin tạo với tấm 1 = CGP, rồi **đọc lại nhóm bu lông** xem Tekla xếp tấm nào thứ 1 và đặt cờ đúng cho CGP (`FixSlotSide`, log `Lo o van: tam 1 = ...`). API Tekla không cho đọc trực tiếp tấm nào có lỗ ô van → cần kiểm tra trên model các trường hợp nghiêng/vặn. (`P41` = 10000/20000 của `.uel` chưa rõ nghĩa số 2.) Bu lông công trường, chỉ Washer2 + Nut1.
6. Fitting ở D5; cánh lùi thêm theo ring (`5+P55`, `5+P56`). Ring là tấm của component khác (RING PL), plugin không tạo.
7. FB: profile `FB25*9`, vật liệu `SS275` (2 chuỗi này có trong `.uel_temp`), cạnh 25 vuông góc mặt cột, tì vào mặt cột/ring; dưới mặt trong cánh trên và dưới mặt ngoài cánh dưới (như RAKER).
8. `D113/D122` (dùng cho P50/P51) = phần cột thò ra ngoài mép cánh dầm mỗi bên, đo trên mặt cột.
9. Class CGP/FB: không gán (ô giả định để trống). Cỡ hàn góc 6.
10. Cỡ bu lông ngoài M16, M20, M22, M24, M27 (P24/P26 của `.uel` không có giá trị): từ 18/11/2026 plugin **báo lỗi và không dựng** (trước đây dùng P26 = 2). Tương tự với girder size không có trong bảng tra, thiếu ô bắt buộc (L3, B3, t3, WING PL, BOLT ROW) hoặc ô không phải số: hiện hộp thông báo, không tạo component. Áp dụng như nhau cho H-COL_TO_GIRDER và COL_TO_HAUNCH_GIRDER.
11. **WP** (theo ảnh mặt bằng người dùng gửi 18/11/2026): tấm nằm trong mặt phẳng cánh, dày = cánh (P8), cạnh trong áp mép cánh.
    Từ mặt cột: rộng tới mép cột đến `(P28+ring)/2`, rồi vát tới đầu xa `P28+ring`; đầu xa còn rộng P35 ngoài mép cánh
    (`P35 = 40` nếu cột rộng hơn dầm ≥ 150, ngược lại `(B cột − B dầm)/4`). Cạnh phía cột cách mặt cột bằng đầu cánh (ring + khe).
12. WP1 = bên −W (`D113`), WP2 = bên +W (`D122`) — đổi trái/phải 18/11/2026 theo người dùng (bản trước nhầm bên). WP có **vát hàn ở 2 cạnh** như cánh dầm cùng cao độ (góc P7, mặt chân 2 mm): cạnh phía cột (khe chân = đầu cánh có tính ring, hàn CJP công trường vào cột) và cạnh tiếp xúc mép cánh dầm (hàn CJP xưởng vào dầm). Hình dạng chỉnh bằng P81 (đoạn thẳng) và P82 (đầu xa), có thể lấy từ bảng tra. **Vật liệu WP mặc định = vật liệu Part 2** (ô P37 nhập thì theo ô; bỏ phép đổi SHN→SN của `.uel`). **FB cánh dưới không đổi theo WP** (chỉ FB cánh trên dài theo WP).
    **Đo phần cột thò ra** (sửa 18/11/2026 — dầm nghiêng/xiên tính sai): đo **trên mặt cột tiếp xúc dầm**, theo phương **nằm ngang vuông góc trục cột**, tại cao độ giữa cánh trên: từ điểm mép cánh chạm mặt cột tới mép cột (không tính chiều dốc/xiên của cả dầm). WP rộng đúng **từ mép cánh tới mép cột**. Log: `Mat cot: rong ..., mep canh dam cham mat cot tai ... -> cot tho ra +W ..., -W ...`.
    **Điều kiện có WP** mỗi bên (log ghi rõ lý do): P50/P51 nhập tay thì theo ô; để trống thì có khi **P14 = Yes (MO)** và **cột thò ra ngoài mép cánh bên đó ≥ 74 mm** (như `.uel`). Ngoài ra plugin chỉ dựng khi P28 > 0 và cột thò ra ≥ 10 mm; `D113/D122` = phần cột thò ra ngoài mép cánh bên đó. Có WP khi ≥ 74 mm và P14 = Yes (như `.uel`).
13. WP **chỉ ở cánh trên** (người dùng xác nhận 18/11/2026): `.uel` có 2 đối tượng WP = 2 bên cánh trên.
14. **Knobs** (18/11/2026): các số không phải biến `.uel` nằm trong khối `// <KNOBS>` đầu `Builder.cs` và chỉnh được bằng tab Knobs của `UEL_TESTER.exe` (xem `Plugin\README.md` mục 3). Giá trị giữ như trước:
    - `BevelExtra` = 3: vát mép cánh, khối cắt vượt mặt cánh và lùi về phía cột;
    - `EdgeBevelExtra` = 3: vát cạnh WP với mép cánh dầm;
    - `WpMinOverhang` = 10: cột thò ra ngoài mép cánh ít hơn số này thì bỏ qua WP (mục 12);
    - `SlabCutMargin` = 200: khối cắt bụng dầm rộng thêm ngoài vết cánh.

## Chưa làm (cần ảnh Custom Component Editor)

- Cắt bụng dầm P62/P63, cắt CGP P64/P65 theo ring.
- Các đối tượng obj#362/#372/#381/#389/#400/#417/#435.

Ảnh cần chụp trong Custom Component Editor của `0-DM_S-Col2Gir`:
(a) cây đối tượng; (b) cửa sổ Variables; (c) mặt phẳng buộc của D8, D9, D56/D57, D101–D104, D113, D122, D162–D197; (d) ảnh 3D component gốc dựng với preset `+B5` (khi P14 = 1 và có WP), kèm 1 ảnh khi P62–P65 = Yes.
Hoặc: đặt component gốc lên 1 cặp cột–dầm rồi bấm **"Xuat JSON component"** trên EXE, gửi file `_Dev\Export\*.json`.

## Kết quả so sánh

Chưa so sánh (chưa chạy trong Tekla).
