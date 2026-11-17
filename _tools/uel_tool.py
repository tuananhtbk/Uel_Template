# -*- coding: utf-8 -*-
"""
Công cụ đọc file .uel (custom component xuất từ Tekla Structures).
Yêu cầu: Python 3.7+ (không cần thư viện ngoài).

Cách dùng (truyền đường dẫn làm tham số):
  python uel_tool.py info     <thư_mục_UEL>           -> liệt kê component, loại, tab, tham số, bảng tra gọi tới
  python uel_tool.py formulas <file.uel>              -> in các công thức (if/then, fVF, fTpl, fP...) trong component
  python uel_tool.py dups     <thư_mục_UEL>           -> các file .uel trùng tên component bên trong
  python uel_tool.py missing  <thư_mục_UEL>           -> bảng tra (.txt/.dat) được gọi nhưng không có trong thư mục
  python uel_tool.py unpack   <file.uel> <thư_mục_ra> -> tách các phần bên trong .uel ra file riêng

Ghi chú định dạng (tự phân tích, Tekla không công bố):
  - .uel = gzip; bên trong là chuỗi khối <FileInfo><Name>..</Name><Size>N</Size>...</FileInfo> + N byte.
  - Chữ Hàn trong tên/nhãn mã hoá cp949 (EUC-KR); các component TJ_* là tiếng Nhật (cp932).
  - .uel_temp (bắt đầu "Xsteel 7.64") là mô hình mẫu dạng nhị phân; chuỗi công thức nằm trong
    các bản ghi 46 byte (text từ offset 0x13, tối đa 27 byte); bản ghi nối tiếp có FF FF FF FF ở offset 0x0B.
"""
import gzip, re, sys, os, glob, collections

if hasattr(sys.stdout, "reconfigure"):
    sys.stdout.reconfigure(encoding="utf-8", errors="replace")

ENC = "cp949"
SEC = re.compile(rb"<FileInfo><Name>(.*?)</Name><Size>(\d+)</Size><Compressed>(\d)</Compressed></FileInfo>", re.S)
FF = b"\xff\xff\xff\xff"
FORMULA_HINT = re.compile(r"(\bif\b|fTpl|fVF|fP\(|fValue|[-+*/]|\b[a-z]+\()")
NOISE = re.compile(r"^\s*(parameter|tab_page|attribute|picture|value|detail|page|joint|<|\}|\{)|CUTPART|FileInfo|Xsteel")
CONTROL = re.compile("[\u0000-\u001f�]")


def load(path):
    return gzip.open(path).read()


def sections(data):
    out = collections.OrderedDict()
    for m in SEC.finditer(data):
        name = m.group(1).decode(ENC, "replace")
        size = int(m.group(2))
        out.setdefault(name, data[m.end():m.end() + size])
    return out


def all_uel(root):
    return sorted(glob.glob(os.path.join(root, "**", "*.uel"), recursive=True))


def formulas(data):
    seen, res = set(), []
    for m in re.finditer(rb"[\x20-\x7e\x80-\xfe]{3,}", data):
        r = m.start() - 0x13
        if r < 0 or data[r + 0x0B:r + 0x0F] == FF:
            continue
        full = data[r + 0x13:r + 46].split(b"\x00")[0]
        if not full.startswith(m.group()[:len(full)]):
            continue
        n = r + 46
        while data[n + 0x0B:n + 0x0F] == FF:
            full += data[n + 0x13:n + 46].split(b"\x00")[0]
            n += 46
        s = full.decode(ENC, "replace")
        if s not in seen:
            seen.add(s)
            res.append(s)
    return res


def table_refs(data):
    refs = set()
    for f in formulas(data):
        refs.update(re.findall(r'fVF\("([^"]+)"', f))
    return sorted(refs)


def cmd_info(root):
    for p in all_uel(root):
        d = load(p)
        t = d.decode(ENC, "replace")
        secs = sections(d)
        ec = [v.decode(ENC, "replace").strip() for k, v in secs.items() if k.endswith("_ec_uel.txt")]
        tabs = re.findall(r'tab_page\("[^"]*",\s*"([^"]*)"', t)
        params = [x for x in re.findall(r'parameter\("([^"]*)"', t) if x.strip()]
        print("##", os.path.relpath(p, root))
        print("   component :", " | ".join(ec))
        print("   tabs      :", [x.strip() for x in tabs])
        print("   params    :", "; ".join(params))
        refs = table_refs(d)
        if refs:
            print("   bảng tra  :", refs)


def cmd_formulas(path):
    for s in formulas(load(path)):
        if CONTROL.search(s) or s.startswith("ID651B"):
            continue
        if FORMULA_HINT.search(s) and not NOISE.search(s) and re.search(r"[A-Za-z]", s):
            print(s)


def cmd_dups(root):
    g = collections.defaultdict(list)
    for p in all_uel(root):
        secs = sections(load(p))
        inp = [k for k in secs if k.endswith(".inp")]
        main = inp[0][:-4] if inp else os.path.basename(p)[:-4]
        tmp = [v for k, v in secs.items() if k.endswith(".uel_temp")]
        inptxt = b"".join(secs[k] for k in inp)
        g[main].append((os.path.relpath(p, root), hash(inptxt) & 0xFFFFFFFF, len(tmp[0]) if tmp else 0))
    for k, v in g.items():
        if len(v) > 1:
            same = len({(x[1], x[2]) for x in v}) == 1
            print("##", k, "-> có vẻ GIỐNG nhau" if same else "-> KHÁC nhau (cẩn thận khi import, sẽ ghi đè)")
            for f, h, s in v:
                print("    %-60s dialog=%08x model_size=%d" % (f, h, s))


def cmd_missing(root):
    have = {os.path.basename(f).lower() for f in glob.glob(os.path.join(root, "**", "*"), recursive=True)}
    for p in all_uel(root):
        for r in table_refs(load(p)):
            if r.lower() not in have:
                print("THIẾU  %-45s <- %s" % (r, os.path.relpath(p, root)))


def cmd_unpack(path, out):
    os.makedirs(out, exist_ok=True)
    for name, blob in sections(load(path)).items():
        safe = re.sub(r'[\\/:*?"<>|]', "_", name)
        with open(os.path.join(out, safe), "wb") as f:
            f.write(blob)
        print(name, len(blob))


if __name__ == "__main__":
    if len(sys.argv) < 3:
        print(__doc__)
        sys.exit(1)
    c, a = sys.argv[1], sys.argv[2:]
    {"info": cmd_info, "formulas": cmd_formulas, "dups": cmd_dups,
     "missing": cmd_missing, "unpack": cmd_unpack}[c](*a)
