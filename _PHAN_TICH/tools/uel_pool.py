"""String pool + parameter table decoding for Tekla .uel_temp (Xsteel 7.64 db)."""
import re, struct

# string pool record: 04 | u32 id | u32 cont_id | u32 hash | u32 refcnt | char[29]
POOL = re.compile(rb'\x04(.{4})(.{4})(.{4})(.{4})([\x20-\x7E\x80-\xFF][^\x00]{0,27})\x00', re.S)

def decode(b):
    try:
        return b.decode('cp949')
    except UnicodeDecodeError:
        return b.decode('cp1252', 'replace')

def string_pool(p):
    recs = {}
    for m in POOL.finditer(p):
        rid, cont, h, ref = (struct.unpack('<I', m.group(i))[0] for i in range(1, 5))
        if rid > 0x100000 or cont > 0x100000 or ref > 0x10000:
            continue
        # string field is 29 bytes total -> check padding is zero
        s = m.group(5)
        end = m.start(5) + 29
        if p[m.end(5):end].strip(b'\x00'):
            continue
        recs[rid] = dict(cont=cont, hash=h, ref=ref, raw=s, off=m.start())
    # continuation records are those referenced by another's cont
    conts = {r['cont'] for r in recs.values() if r['cont']}
    out = {}
    for rid, r in recs.items():
        if rid in conts:
            continue
        buf, cur, seen = r['raw'], r['cont'], set()
        while cur and cur in recs and cur not in seen:
            seen.add(cur); buf += recs[cur]['raw']; cur = recs[cur]['cont']
        out[rid] = dict(text=decode(buf), ref=r['ref'], off=r['off'])
    return out

# parameter table record: 04 | u32 id | char[32] name | char[32] label | u32 x | u32 type
PARAM = re.compile(rb'\x04(.{4})(P\d{1,3}|[A-Za-z_][A-Za-z0-9_]{0,30})\x00', re.S)

def param_table(p):
    out = []
    for m in PARAM.finditer(p):
        st = m.start(2)
        name = p[st:st + 32].split(b'\x00')[0]
        label = p[st + 32:st + 64].split(b'\x00')[0]
        tail = p[st + 64:st + 72]
        if len(tail) < 8 or not label:
            continue
        if p[st + len(name):st + 32].strip(b'\x00'):
            continue
        if p[st + 32 + len(label):st + 64].strip(b'\x00'):
            continue
        x, t = struct.unpack('<II', tail)
        if t > 64:
            continue
        out.append(dict(name=decode(name), label=decode(label), x=x, type=t, off=st))
    return out

FORMULA_HINT = re.compile(r'[()+\-*/=<>"]|\bif\b|\bthen\b|^f[A-Z]')

def is_formula(s):
    return bool(FORMULA_HINT.search(s)) and not re.fullmatch(r'-?[\d.]+', s)
