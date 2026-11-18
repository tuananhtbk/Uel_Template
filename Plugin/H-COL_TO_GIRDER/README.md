# H-COL_TO_GIRDER — liên kết Cột H → Dầm H (CGP + bu lông, cánh hàn CJP)

| | |
|---|---|
| Tên trong Tekla | `H-COL_TO_GIRDER` (bản DEV: `H-COL_TO_GIRDER_DEV`) |
| Component gốc | `0-DM_H-Col2Gir_H-COLUMN`, file [`File goc\H-COL-GIRDER\H-COL.uel`](../../File%20goc/H-COL-GIRDER/H-COL.uel) |
| Phân tích | [`_PHAN_TICH\components\H-COL-GIRDER__H-COL.uel.md`](../../_PHAN_TICH/components/H-COL-GIRDER__H-COL.uel.md) |
| Loại | connection (`-1000`), 1 cấu kiện phụ |
| **Part 1** | cột **H** (primary, chọn trước) — giả định như COL_TO_GIRDER |
| **Part 2** | dầm H (secondary) |
| Bảng tra | `H-COL_TO_GIRDER.txt` (mặc định); tên gốc của `.uel`: `YGY-H_COL2GIR-운용층-H-COL.txt` |
| Trạng thái | 18/11/2026: clone từ [COL_TO_GIRDER](../COL_TO_GIRDER/README.md), `build.bat core` OK cả 2 bộ. **Chưa build DEV, chưa chạy trong Tekla.** |

## Quan hệ với COL_TO_GIRDER

`H-COL.uel` dùng **cùng tên biến** với `COL_TO_GIRDER.uel` (P4, P11 … P73), nên plugin này là bản chép của COL_TO_GIRDER: cùng hộp thoại (1 tab chính + tab Advanced), cùng biến mới P74–P98, cùng cách đọc bảng tra, cùng hình học dầm (fitting, vát CJP, scallop, CGP hình bình hành khi nghiêng, bu lông, lỗ ô van ở CGP, FB, phía CGP P83/P11).
Mọi mục "Giả định" / "Chưa làm" của [COL_TO_GIRDER/README.md](../COL_TO_GIRDER/README.md) áp dụng nguyên cho plugin này. Dưới đây chỉ ghi phần **khác**.

| | COL_TO_GIRDER.uel | H-COL.uel → plugin này |
|---|---|---|
| P4 (bu lông → mép ngoài CGP) | 65 | **55** |
| P13 (vật liệu CGP) | SN355 | **SM355** |
| D5 khi PIN | 20 | **30** → mặc định P74 = 30 |
| WP (P48/P49) | theo phần cột thò ra ≥ 74 | **hằng 0** → P50/P51 để trống = **không có WP**. Nhập P50/P51 = 1 thì vẫn dựng như COL_TO_GIRDER |
| FB | D56/D57 = 20 (hoặc theo WP) | `P6/2+20` = cánh + 20 mỗi bên (như COL_TO_GIRDER khi không có WP) |
| Part 1 | cột hộp | **cột H** (xem dưới) |
| Bảng tra | `YGY-S_COL2GIR-운용층.txt` | `YGY-H_COL2GIR-운용층-H-COL.txt` (cùng vị trí cột 2/3/4/5/7) |

## Cột H (người dùng 18/11/2026)

- Vào **cánh** hay vào **bụng** cột đều dùng **hình bao ngoài** của tiết diện như cột hộp, y hệt COL_TO_GIRDER. Vào bụng thì mặt tiếp xúc là mặt phẳng qua mép 2 cánh cột; phần bù (t_bar) do component khác làm. Log `Cot H: dam vao MAT CANH cot` / `phia BUNG cot`.
- **Không có WP, không có ring.** Đã bỏ khỏi hộp thoại: P28, P37, P50, P51, P53, P76, P77, P81, P82 (WP), P55, P56, P58–P61 (ring), P62–P65 (cắt theo ring), cột WP ở tab Advanced (P88–P91). Thuộc tính vẫn còn trong `Params.cs` (không `[UelParam]`, luôn = mặc định) để dùng chung code với COL_TO_GIRDER: ring = 0, P50 = P51 = 0 cố định trong `Builder.cs`.
- Hậu quả: cánh không lùi theo ring, FB = bề rộng cánh + 20 mỗi bên, chỉ còn ô FB Zone (P54) ở cột phải.
- Cột hộp dùng COL_TO_GIRDER, cột H dùng plugin này (quản lý riêng).

## Bảng tra `H-COL_TO_GIRDER.txt`

Chép từ `File goc\H-COL-GIRDER\YGY-H_COL2GIR-운용층-H-COL.txt` (36 dòng, không có dòng trùng), định dạng chuẩn như `COL_TO_GIRDER.txt`: ghi chú `#` ở đầu, TAB, CRLF, ASCII.
Cột: 1 Girder Size, 2 L3, 3 B3, 4 t3, 5 WING PL (= 0), 6 WING PL t1 (tham khảo), 7 BOLT ROW, tuỳ chọn `e`, `L1`, `tip` (trống), tham khảo `Ht`, `Ht_thk`, `REMARK`.
Bảng lưu trong model như COL_TO_GIRDER (`LookupTable.ResolveInModel`): P78 trống → `H-COL_TO_GIRDER.txt`; không thấy thì tìm tên gốc `YGY-H_COL2GIR-운용층-H-COL.txt` (`CANH BAO`).

## Hộp thoại

Như COL_TO_GIRDER nhưng không có ring, WP, ô cắt P62–P65; tab Advanced chỉ có CGP và FB. Hình `bitmaps\UEL_HCOL2GIR_1.bmp` vẽ cột H (mặt đứng: cánh trước + bụng + cánh sau; mặt bằng: chữ H). Sinh bằng `_hinh\VeHinh.cs`, `.inp` chép vào `Plugin.cs` bằng script.

## Cách thử

1. `build.bat dev H-COL_TO_GIRDER` (component mới → phải build vỏ DEV).
2. Tắt Tekla → cmd **Administrator** → `install.bat dev <bản> H-COL_TO_GIRDER`.
3. Đặt `H-COL_TO_GIRDER_DEV`: chọn **cột H trước, dầm sau**. Xem log `%TEMP%\UEL_PLUGINS.log`.
4. Thử: dầm vào mặt cánh cột, dầm vào bụng cột (mặt bao qua mép cánh), dầm dốc, dầm xiên.

## Giả định

1. Như COL_TO_GIRDER (Part 1 = cột, CGP, bu lông, FB, scallop…).

## Chưa làm

Các đối tượng cắt `P6+P23` (obj#364/#375) của `.uel`. (P62–P65 đã bỏ theo yêu cầu, cùng ring.)

## Kết quả so sánh

Chưa so sánh (chưa chạy trong Tekla).
