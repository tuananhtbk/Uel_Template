# COL_TO_HAUNCH_GIRDER — liên kết Cột → Dầm HAUNCH (CGP + bu lông, cánh hàn CJP)

| | |
|---|---|
| Tên trong Tekla | `COL_TO_HAUNCH_GIRDER` (bản DEV: `COL_TO_HAUNCH_GIRDER_DEV`) |
| Component gốc | `♥♥♥YGY_HAUNCH_Moment`, file [`File goc\COL-HUN-PBOX\YGY-HAUNCH_Moment.uel`](../../File%20goc/COL-HUN-PBOX/YGY-HAUNCH_Moment.uel) |
| Phân tích | [`_PHAN_TICH\components\COL-HUN-PBOX__YGY-HAUNCH_Moment.uel.md`](../../_PHAN_TICH/components/COL-HUN-PBOX__YGY-HAUNCH_Moment.uel.md) |
| Loại | connection (`-1000`), 1 cấu kiện phụ |
| **Part 1** | cột (S-COLUMN / P-BOX, chọn trước) |
| **Part 2** | dầm **haunch** `HUN<h1>-<h2>*B*tw*tf` (cao thay đổi, cánh dưới vát) |
| Bảng tra | `COL_TO_HAUNCH_GIRDER.txt` (mặc định); bảng gốc `YGY-HAUNCH_Moment.txt` cũng đọc được |
| Trạng thái | 18/11/2026: clone từ [COL_TO_GIRDER](../COL_TO_GIRDER/README.md), `build.bat core` OK cả 2 bộ. **Chưa build DEV, chưa chạy trong Tekla.** |

## Quan hệ với COL_TO_GIRDER

Theo người dùng (18/11/2026): "cơ bản chỉ khác hình dạng Part 2". Plugin này là bản chép của COL_TO_GIRDER, **giữ tên biến của COL_TO_GIRDER** (P4, P11 … P98) để 2 plugin nhập giống nhau, cùng hộp thoại, cùng bảng tra chuẩn.
Vì tên component mới (`COL_TO_HAUNCH_GIRDER`), model đang dùng `♥♥♥YGY_HAUNCH_Moment` không bị ảnh hưởng; preset của `.uel` gốc **không** nạp sang plugin được (tên biến khác).
Mọi "Giả định" / "Chưa làm" của [COL_TO_GIRDER/README.md](../COL_TO_GIRDER/README.md) áp dụng nguyên. Dưới đây chỉ ghi phần **khác**.

### Biến `.uel` haunch ↔ biến plugin

| `YGY-HAUNCH_Moment.uel` | Plugin | Ghi chú |
|---|---|---|
| `1_GAP` (D4..D7 = 1_GAP+5) | P75 (khe đầu dầm MO, mặc định 5) | `1_GAP` không có giá trị mặc định trong `.uel` → coi = 0 |
| `P11_NSFS` CGP 방향 | P11 + P83 | như COL_TO_GIRDER |
| `P18` ZONE (theo prefix cột) | P12 | |
| `P38` FB-GWP PREFIX (theo prefix dầm) | P54 | đuôi `"GWP"` → P76 mặc định `GWP` |
| `P30` CGP 재질 = SM355 | P13 = SM355 | |
| `P29_diameter`, `P29_screwdin` | P40_diameter, P40_screwdin | M24, T.S.B |
| `P31` SLOT 길이, `P32`, `P33` | P24 (+ P26, P27 tính) | cùng công thức |
| `VAR3` CWS_SPS EDGE = 80 | P21A | |
| `VAR4` CWS_SPS = 900 | P21B | `BOLT_X_DIST = H < VAR4 ? 1*(B3-80) : 2*(B3-80)/2` = cách COL_TO_GIRDER tính P22 |
| `BOLT_ST` = 80 (DX1) | P4 = 80 (bảng tra cột `e`) | |
| `BOLT_TOP` = bảng cột 6 | **P99 (biến mới)** "top2bolt (auto)" | |
| `P4_PLATE_H` = bảng cột 2 B3 | B3 (P19) | |
| `P7_PLATE_THK` = bảng cột 3 T3 | t3 (P17) | |
| `WING_L1` = bảng cột 4 | WING PL (P28) | bảng gốc toàn 0 → không có WP |
| `P21`, `P22` WING1/2 (D136/D137 > 74.5) | P50, P51 | như COL_TO_GIRDER (≥ 74) |
| `P34` R_TOP, `P17` R_BOT | **P100, P101 (biến mới)** | bật / tắt scallop cánh trên / dưới, mặc định Yes |
| D20/D23 = 1_GAP+5+160 (CGP từ mặt cột) | L3 = 165 | bảng chuẩn ghi 165; bảng gốc → 160 + P75 |
| `P19` 철판위치 조절 | — | **chưa làm** (không biết buộc vào mặt nào) |

## Hình học dầm haunch (khác COL_TO_GIRDER)

1. **Đo trên solid dầm** (`HaunchFrames`): cắt solid bằng đường thẳng theo Y qua trục dầm tại 2 tiết diện (cách mặt cột ~40 mm và ~40 + 150…600 mm) → điểm ngoài cùng = mặt ngoài cánh trên / cánh dưới. Nối 2 tiết diện → phương mỗi cánh; kéo tới mặt cột → **chiều cao tại mặt cột**.
   Không đo được (solid không cắt được) → `CANH BAO`, làm như dầm cao đều theo trục dầm.
   Profile không báo được HEIGHT/WIDTH/… → đọc từ tên `HUN450-350*200*6*11`.
2. **Mỗi cánh một hệ cục bộ**: X dọc mặt ngoài cánh đó, gốc trên mặt cột ở giữa chiều cao tại mặt cột.
   - Hệ cánh trên: lùi cánh trên, vát CJP cánh trên, scallop trên, CGP, bu lông, WP, FB trên.
   - Hệ cánh dưới: lùi cánh dưới, vát CJP cánh dưới (cánh dưới dốc so với mặt cột → vát tính theo độ dốc như dầm dốc của COL_TO_GIRDER), scallop dưới, cắt xuyên cánh dưới, FB dưới.
   - Fitting đầu dầm: 1 mặt song song mặt cột, cách D5 (giống nhau ở 2 hệ).
   - Dầm cao đều: 2 hệ trùng nhau → giống COL_TO_GIRDER. Log `Haunch: do solid ... 2 canh lech nhau ... do`.
3. **CGP + bu lông theo cánh trên** (`.uel`: D24 = BOLT_TOP − 0.5·VAR3, D26 = P4_PLATE_H + D24): hàng bu lông trên cách **mặt ngoài cánh trên** đúng TOP2BOLT (P99 / bảng tra); mép trên CGP cách hàng bu lông trên P21A/2; CGP cao B3; các con bu lông đối xứng quanh tâm CGP. TOP2BOLT trống (ô trống và bảng không có cột) → CGP giữa chiều cao tại mặt cột như COL_TO_GIRDER.
   Kiểm tra số liệu: `HUN450-350`, B3 350, TOP2BOLT 90 → CGP từ 50 tới 400 dưới mặt trên cánh, trong khoảng 11…439 giữa 2 cánh ở đầu cao 450 → hợp lý **khi đầu cao (h1) ở phía cột**.
4. P20 (chiều cao dầm dùng cho P21 / P22) = chiều cao **tại mặt cột**.
5. WP chỉ ở cánh trên; ô giả định "WP cả cánh dưới" bị bỏ qua (`CANH BAO`).

## Bảng tra `COL_TO_HAUNCH_GIRDER.txt`

Chép từ `File goc\COL-HUN-PBOX\YGY-HAUNCH_Moment.txt` (44 dòng, không trùng), định dạng chuẩn như `COL_TO_GIRDER.txt` (cột 1–7 cùng vị trí):

| Cột | Giá trị |
|---|---|
| 1 Girder Size | Size |
| 2 L3 | 165 (= 160 + 5) |
| 3 B3 / 4 t3 | B3 / T3 của bảng gốc |
| 5 WING PL | L1 của bảng gốc (toàn 0) |
| 6 t2 | T2 của bảng gốc (tham khảo) |
| 7 BOLT ROW | 1 khi h1 < 900, 2 khi h1 ≥ 900 |
| `e` | 80 |
| `L1`, `tip` | trống |
| `TOP2BOLT` | TOP2BOLT của bảng gốc |
| `Ht`, `Ht_thk`, `REMARK` | tham khảo |

Nhập P78 = bảng gốc `YGY-HAUNCH_Moment.txt` (hoặc P78 trống mà không có `COL_TO_HAUNCH_GIRDER.txt`) cũng được: plugin nhận ra dạng gốc (cột 2 tiêu đề `B3`) và đọc theo vị trí cột của `.uel` (log `Bang tra dang GOC`). Bảng lưu trong model như COL_TO_GIRDER.

## Hộp thoại

Như COL_TO_GIRDER, thêm: ô **P99** "top2bolt (auto)" trên hình (từ mặt trên cánh trên tới bu lông trên), ô **P100 / P101** "Scallop tren / duoi" ở dải dưới hình. Hình `bitmaps\UEL_COL2HUN_1.bmp` vẽ dầm có cánh dưới vát.

## Cách thử

1. `build.bat dev COL_TO_HAUNCH_GIRDER`.
2. Tắt Tekla → cmd **Administrator** → `install.bat dev <bản> COL_TO_HAUNCH_GIRDER`.
3. Đặt `COL_TO_HAUNCH_GIRDER_DEV`: chọn **cột trước, dầm haunch sau**. Xem log `%TEMP%\UEL_PLUGINS.log` (dòng `Haunch:` và `CGP theo canh tren:`).
4. Thử: đầu cao của haunch ở phía cột; dầm H thường (2 hệ trùng nhau); dầm dốc / xiên.

## Giả định

1. Như COL_TO_GIRDER.
2. TOP2BOLT đo từ **mặt ngoài** cánh trên (suy từ số liệu bảng, xem mục Hình học 3).
3. `1_GAP` = 0 → khe đầu dầm 5, L3 = 165.
4. Chiều dày WP (`"PL"+P23` của `.uel`, P23 không có trong phân tích) = chiều dày cánh dầm, như COL_TO_GIRDER.
5. Kích thước scallop (P80 = 35) như COL_TO_GIRDER; `.uel` có khối cắt `CUT(tf+10)*(tf+10)` chưa rõ là gì.

## Chưa làm

- Như COL_TO_GIRDER: cắt bụng P62/P63, cắt CGP P64/P65.
- `P19` (철판위치 조절 — chỉnh vị trí tấm) của `.uel`.
- Khối cắt `P3_CUT_PROF = H − 2tf − 10` và `CUT(tf+10)*(tf+10)` của `.uel` (cần ảnh Custom Component Editor).
- Đầu **thấp** của haunch ở phía cột: plugin vẫn dựng theo số đo thật nhưng CGP theo bảng có thể vượt khoảng giữa 2 cánh (log `CANH BAO`).

## Kết quả so sánh

Chưa so sánh (chưa chạy trong Tekla).
