"""Decode variables / bindings from a Tekla .uel_temp database (Xsteel 7.64 format, Tekla 18).

Record layouts found by inspection (little-endian, each record starts with byte 0x04):
  string  : 04 | id | cont_id | hash | refcnt | char[29]          (long strings chained via cont_id)
  param   : 04 | id | char[31] name | char[33] label | value_sid | type
  dimobj  : 04 | objid | name_sid | desc_sid | ...                 (D-variables: distance bindings)
  binding : 04 | recid | objid | 0 | value_sid | char[] propname   (propname = proVALUE/proPROFILE/...)
"""
import re, struct
from uel_pool import string_pool, decode

U = lambda b, o: struct.unpack_from('<I', b, o)[0]

def bindings(p):
    out = []
    for m in re.finditer(rb'pro[A-Z][A-Z_0-9]*\x00', p):
        o = m.start()
        if o < 17 or p[o - 17] != 4:
            continue
        recid, objid, z, sid = (U(p, o - 16 + 4 * i) for i in range(4))
        if z != 0 or sid > 0x200000 or objid > 0x200000:
            continue
        out.append(dict(rec=recid, obj=objid, sid=sid, prop=m.group()[:-1].decode()))
    return out

def params(p, pool):
    out = []
    for m in re.finditer(rb'\x04(.{4})([A-Za-z_][\x20-\x7E]{0,29})\x00', p, re.S):
        st = m.start(2)
        if st + 72 > len(p):
            continue
        name = p[st:st + 31].split(b'\x00')[0]
        if p[st + len(name):st + 31].strip(b'\x00'):
            continue
        lab = p[st + 31:st + 64].split(b'\x00')[0]
        if not lab or p[st + 31 + len(lab):st + 64].strip(b'\x00'):
            continue
        if not all(32 <= c < 127 or c >= 0x80 for c in lab):
            continue
        sid, typ = U(p, st + 64), U(p, st + 68)
        if typ > 64:
            continue
        val = pool.get(sid, {}).get('text') if sid else None
        out.append(dict(id=U(p, m.start(1)), name=decode(name), label=decode(lab), sid=sid,
                        value=val, type=typ))
    # dedupe by name keeping first
    seen, res = set(), []
    for r in out:
        if r['name'] in seen:
            continue
        seen.add(r['name']); res.append(r)
    return res

def dim_objects(p, pool):
    """objects whose 2nd/3rd field point to pool strings 'Dn' and 'Dn.X.Y'."""
    names = {sid: v['text'] for sid, v in pool.items()}
    dsids = {sid for sid, t in names.items() if re.fullmatch(r'D\d+', t)}
    out = {}
    for sid in dsids:
        k = struct.pack('<I', sid)
        i = 0
        while True:
            i = p.find(k, i + 1)
            if i < 0:
                break
            if i >= 5 and p[i - 5] == 4:
                objid = U(p, i - 4)
                desc = names.get(U(p, i + 4), '')
                if objid != sid:
                    out[objid] = dict(name=names[sid], desc=desc)
    return out

def decode_component(p):
    pool = string_pool(p)
    pool = {k: v for k, v in pool.items() if not v['text'].startswith('pro')}
    prm = params(p, pool)
    dims = dim_objects(p, pool)
    pid = {r['id']: r['name'] for r in prm}
    binds = []
    for b in bindings(p):
        txt = pool.get(b['sid'], {}).get('text')
        if txt is None:
            continue
        owner = dims.get(b['obj'], {}).get('name') or pid.get(b['obj']) or ('obj#%d' % b['obj'])
        binds.append(dict(owner=owner, obj=b['obj'], prop=b['prop'], value=txt))
    return dict(pool=pool, params=prm, dims=dims, bindings=binds)
