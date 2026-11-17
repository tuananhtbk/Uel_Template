# Bàn giao – phân tích Custom Component Tekla 18.0 (để tiếp tục trên máy khác)

Dán đoạn "Bối cảnh cho Claude" bên dưới vào phiên Claude mới (mở trong thư mục `UEL Template`) để tiếp tục.

## Đã làm
1. Giải mã 76 file `.uel` (gzip → các file con `<FileInfo>`; phần `.uel_temp` là DB nhị phân "Xsteel 7.64"). Kết quả:
   - `README.md` – cấu trúc file, danh mục component, hàm dùng, file tra cứu thiếu/trùng, tài liệu Tekla tham khảo.
   - `components/*.md` – từng component: dialog `.inp`, bảng biến P + công thức, biến D, binding thuộc tính part, preset `.p_*`.
   - `tools/` – script Python giải mã (`uel_report.py "<thư mục UEL>" "<thư mục xuất>"`).
2. Phân tích RAKER (`RAKER+TG.uel`, `RAKER+T_GIRDER.uel`):
   - Hai file logic giống hệt, chỉ khác GUID raker mẫu (`…4FFD…` vs `…9F0C…`) và preset → giữ `RAKER+TG.uel`.
   - Nguyên nhân lỗi tạo part khi raker đổi hướng:
     - `P14 = fAD("slope", raker)` đổi dấu / >90° khi vẽ ngược chiều; công thức dùng thẳng `sin(P14)` (P26 → D119/D120) và `cos(P14)` (P49 → D38/D153/D156; D127) → khoảng cách âm → part không tạo / lệch phía. `P16` (trị tuyệt đối) có sẵn nhưng không dùng; P48, P50 thừa.
     - Nhiều ràng buộc vào mặt `Extrema*` và `CentralAxisVertical/Horizontal/Perpendicular` (phụ thuộc hệ toạ độ component) và mặt "Right" (`UpperFlangeBottomRight`, `LowerFlangeTopRight`).
     - Preset lưu nhiều góc `zang1/zang2` khác nhau (xoay tay hệ toạ độ cho từng trường hợp).
   - Đề xuất: biến `Pa = |P14|`, `Pb = if (Pa>90) then 180-Pa else Pa endif`, thay `cos/sin(P14)` bằng `cos/sin(Pb)`; để `P24` quyết định phía gusset; bind lại vào boundary plane; thống nhất chiều vẽ raker; test 8 trường hợp.
3. Yêu cầu: gusset luôn có cạnh trên/dưới song song cánh raker:
   - Gusset = contour plate `"PL"+P20`; D115–D118 = `±P25` (áp bụng), D129–D132 = `0.5*(P34-1)*P43+P45` (nửa chiều cao), D119/D120 = `0.5*P20*sin(P14)`.
   - Cách 1: 2 điểm mỗi cạnh bind cùng một mặt của raker, cùng khoảng cách (mặt tâm song song cánh ±H/2, hoặc `albl_PlaneLowerFlangeTop` / `albl_PlaneUpperFlangeBottom` + khe hở); đầu base plate bind mặt trên base plate; đầu kia bind mặt vuông góc trục raker.
   - Cách 2 (chắc hơn): gusset dạng beam-plate `"PL"+P20+"*"+Ph`, `Ph = (P34-1)*P43+2*P45`, trục song song raker (offset `±P25`), fitting đầu theo base plate.
   - Chưa xác định được D129–D132 đang bind mặt nào → cần ảnh Custom component browser của gusset.

4. *(17/11/2026)* Gộp với phần làm ở máy khác: `CLAUDE.md`, `HUONG_DAN_CHUYEN_MAY.md`, `_tools/uel_tool.py` và plugin `../Plugins/RakerTG/`, viết lại `0-RAKER+TG` bằng C#.
   - Đã cập nhật `CLAUDE.md` và `HUONG_DAN_CHUYEN_MAY.md`: công thức của từng biến, công thức biến D và thuộc tính part **đã đọc được**, xem `_PHAN_TICH`.
   - Đã bổ sung mục 5b vào `Plugins/RakerTG/README.md`:
     - P13 = 0 là kiểu bản mã + bu lông; P13 = 1 là kiểu không có bản mã và bu lông.
     - Có 3 chi tiết luôn `CREATION = 0`.
     - Bảng công thức các biến D: vát CJP, R35, vị trí bu lông.

## Việc còn lại
- Người dùng gửi ảnh danh sách binding của tấm gusset → chỉ chính xác binding cần xoá/bind lại.
- Bổ sung 4 file tra cứu thiếu: `CT-BAR-H_COLUMN.txt`, `DM-1F-RING+WING.txt`, `YGY-HAUNCH_Moment-미운용층.txt`, `Shear-JP11-SS275.dat`.
- Thống nhất các bản `YGY-TB-MOMENT.txt` (component cần bản 8 cột có L1, thư mục `PIN-MOMENT CHANGSIN`).
- Chưa test gì trong Tekla.

## Bối cảnh cho Claude (dán vào phiên mới)
> Tôi đang làm việc với custom component Tekla 18.0 trong thư mục này. Hãy đọc `_PHAN_TICH/BAN_GIAO.md` và `_PHAN_TICH/README.md` trước; chi tiết từng component ở `_PHAN_TICH/components/`, công cụ giải mã ở `_PHAN_TICH/tools/`. Đang dừng ở bước: sửa component RAKER (`RAKER+TG.uel`) để gusset luôn song song cánh raker và không lỗi khi raker đổi hướng.
