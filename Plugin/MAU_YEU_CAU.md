# Mẫu yêu cầu: làm component mới và sửa component đã làm

Chép phần mẫu cần dùng, điền vào rồi gửi kèm ảnh. Bỏ trống mục nào không biết; **đừng đoán số**.

---

## 1. Từ vựng chung

### Gọi tên
| Gọi là | Ý nghĩa |
|---|---|
| **primary** | cấu kiện chọn đầu tiên (vd. dầm T-girder) |
| **secondary** | cấu kiện chọn thứ hai (vd. raker) |
| **mark** của part | `B1D-RSF`, `B1D-RGP`… (prefix). Dùng mark thay cho màu, vì màu đổi theo class |
| **P…** | tên biến trong hộp thoại (`P10`), hoặc nhãn của ô đó ("FLG. Weld gap") |

### Hướng: theo 3 trục của secondary
| Hướng | Nghĩa |
|---|---|
| **dọc** (+ = ra xa bản đế / primary) | dọc trục secondary |
| **trên / dưới** | theo chiều cao tiết diện secondary (cánh trên, cánh dưới) |
| **trái / phải** | theo bề rộng cánh, **nhìn từ secondary vào primary** |
| **phía trong / ngoài** | trong / ngoài góc nhọn giữa primary và secondary |

---

## 2. Sửa component đã làm

```
Component : RAKER+T_GIRDER
Model     : SAMPLE V2020        (để nguyên component trong model để tôi đọc số đo)
Có component gốc đặt cạnh để so sánh? : có / không

1.
[Part/đường cắt]  B1D-RFB cánh dưới
[Hiện tại]        nằm trên cánh dưới
[Muốn]            nằm dưới cánh dưới, cạnh chạm bản đế
[Điều khiển bởi]  P52 (có/không) ; kích thước cố định 25x9
[Mốc đo, chiều +] (nếu có số) đo từ ..., dương là ...
[Ảnh]             1.png (khoanh đỏ)

2.
...
```

Kèm theo nếu kết quả lạ:
- vài dòng cuối của `%TEMP%\UEL_PLUGINS.log`. Dòng `Core <giờ build>` cho biết đang chạy code nào;
- file `_Dev\SoSanh\*.txt` từ nút **So sánh** của EXE.

---

## 3. Làm component mới

```
File .uel gốc       : 0-PL WELD.uel        (tên trong catalog: 0-PL WELD)
Loại                : detail / connection   ; thứ tự chọn: 1. ...  2. ...
Model mẫu           : ...                  (component gốc đã dựng sẵn trên cấu kiện mẫu)
Preset đã dùng      : standard / KK350 / ...
Bản Tekla cần chạy  : 18.0 / 2020 / 2025
Khác với bản gốc    : (những chỗ muốn làm KHÁC component gốc - sẽ ghi vào mục "Giả định")
Bảng tra (.txt/.dat): (nếu component dùng fVF)
```

Ảnh **Custom Component Editor** cần có (phần này không đọc được từ file `.uel`):
1. Cây đối tượng: part, đường cắt, fitting, bu lông, mối hàn.
2. Cửa sổ **Variables**: đủ các cột, kể cả các biến D.
3. Mặt phẳng mà từng biến D buộc vào. Chọn biến D thì Editor tô sáng mặt phẳng đó.
4. Ảnh 3D component gốc trên cấu kiện mẫu, nhìn từ 2–3 góc.

---

## 3b. Gửi dữ liệu component bằng nút "Xuat JSON component"

Cách nhanh nhất để tôi có **đủ dữ liệu** của một component (gốc hoặc plugin):
1. Dựng component trên một cặp cấu kiện mẫu, **nạp preset** (hoặc nhập đủ các ô) rồi Modify.
2. Chọn component đó trong Tekla, bấm **"Xuat JSON component"** trên `UEL_TESTER.exe`.
3. Gửi file `_Dev\Export\*.json`, hoặc chỉ cần nói tên file, kèm file `.uel` gốc.

Với cấu kiện nghiêng, chéo: xuất **vài trường hợp** (đứng, nghiêng, lệch ngang, nghiêng + lệch) của cùng một component. Từ các file đó tôi thấy được component gốc chạy sai ở đâu, và viết plugin tính đúng.

## 4. (Tuỳ chọn) Hình dạng chuẩn dạng JSON

Dùng khi muốn chốt chính xác kích thước hoặc vị trí, hay khi chưa có component gốc để so. Có thể viết tay vài part quan trọng, không cần đủ.

**Quy ước toạ độ**
- Gốc O = giao điểm trục secondary với **mặt trên bản đế** (mặt tì của secondary).
- **x**: dọc secondary, + ra xa primary.
- **y**: lên, theo chiều cao tiết diện secondary.
- **z**: sang trái, nhìn từ secondary vào primary.
- Đơn vị mm và độ.

Giá trị có thể là **số** hoặc **công thức theo biến**: `"P10"`, `"P1+25"`, `"h/2"`, `"tf"`. Tên có sẵn cho tiết diện secondary là `h`, `b`, `tw`, `tf`; cho primary là `H`, `B`, `TW`, `TF`.

```json
{
  "component": "RAKER+T_GIRDER",
  "preset": "KK350",
  "parts": [
    {
      "mark": "B1D-RSF", "name": "PLATE", "profile": "PL{P1}", "material": "{P2}", "class": "{P3}",
      "shape": "rectangle", "on": "mat tren ban de",
      "size": { "top": "H/2 + P6", "bottom": "-(H/2 + P8)", "left": "max_z_secondary + P7", "right": "min_z_secondary - P19" },
      "note": "Top/Bott theo primary, Left/Right theo diem xa nhat cua secondary"
    },
    {
      "mark": "B1D-RGP", "profile": "PL{P20}", "create_if": "P13 == 0",
      "side": "phia trong (P24 de trong) | theo P24",
      "x": ["P46 + P26", "P46 + P29 + (P31-1)*P32 + P44"], "y": ["-(0.5*(P34-1)*P43 + P45)", "0.5*(P34-1)*P43 + P45"],
      "z": "±(tw + P20)/2", "rule": "khong cat; day doc truc den khi goc sau nhat cham ban de"
    },
    {
      "mark": "B1D-RFB", "profile": "PL9", "count": 2, "create_if": "P52 == 1",
      "where": ["duoi mat trong canh tren", "duoi mat ngoai canh duoi"],
      "x": [0, 25], "thickness_dir": "y", "length": "b"
    }
  ],
  "cuts": [
    { "on": "secondary", "type": "vat mep han", "where": "mat tren ca 2 canh",
      "root_gap": "P10", "angle": "P12", "root_face": 2, "setback_top": "P10 + (tf-2)*tan(P12)" }
  ],
  "bolts": [
    { "parts": ["B1D-RGP", "secondary"], "size": "P38_diameter", "standard": "P38_screwdin",
      "x_first": "P46 + P29", "nx": "P31", "dx": "P32", "ny": "P34", "dy": "P43", "slot": ["P39", "P41"] }
  ],
  "welds": [
    { "parts": ["B1D-RSF", "B1D-RGP"], "type": "fillet", "size": 6 },
    { "parts": ["secondary", "B1D-RSF"], "type": "CJP 1/2V", "angle": "P12", "site": true }
  ]
}
```

Gửi file JSON này cùng yêu cầu. Tôi sẽ coi nó là **chuẩn**:
- code dựng theo đúng các số / công thức trong file;
- chỗ nào khác với component gốc thì ghi vào mục "Giả định" của component.
