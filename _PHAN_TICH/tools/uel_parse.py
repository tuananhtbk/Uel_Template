"""Parse Tekla .uel (custom component export) files.
.uel = gzip( sequence of <FileInfo><Name>..</Name><Size>N</Size><Compressed>c</Compressed></FileInfo> + N bytes )
"""
import gzip, os, re, sys, json

HDR = re.compile(rb'<FileInfo><Name>(.*?)</Name><Size>(\d+)</Size><Compressed>(\d)</Compressed></FileInfo>\r?\n?')

def entries(data):
    pos, out = 0, []
    while True:
        m = HDR.search(data, pos)
        if not m:
            break
        try:
            name = m.group(1).decode('cp949')
        except UnicodeDecodeError:
            name = m.group(1).decode('cp1252', 'replace')
        size = int(m.group(2)); comp = int(m.group(3))
        start = m.end()
        # payload: up to next header (Size may refer to uncompressed size)
        nxt = HDR.search(data, start)
        end = nxt.start() if nxt else len(data)
        out.append(dict(name=name, size=size, comp=comp, payload=data[start:end]))
        pos = end
    return out

def ascii_strings(b, minlen=2):
    return [s.decode('ascii') for s in re.findall(rb'[\x20-\x7E]{%d,}' % minlen, b)]

FUNC = re.compile(r'\bf(VF|Tpl|P|Abs|Int|If|Max|Min|Sqrt|Sin|Cos|Tan|Atan|Round|Floor|Ceil|Mod|Exp|Ln|Log|Hypot|ValueOf|Excel|Data|Sum)\s*\(')

def analyze(path):
    data = gzip.open(path).read()
    ents = entries(data)
    info = dict(file=path, entries=[(e['name'], e['size'], e['comp'], len(e['payload'])) for e in ents])
    for e in ents:
        n, p = e['name'], e['payload']
        if n.endswith('.inp'):
            info['inp_name'] = n
            info['inp'] = p.decode('cp1252', 'replace')
        elif n.endswith('_ec_uel.txt'):
            info['catalog'] = p.decode('cp1252', 'replace').strip()
        elif n.endswith('.uel_temp'):
            info['temp_head'] = ' '.join(ascii_strings(p[:64]))
            strs = ascii_strings(p, 2)
            info['n_strings'] = len(strs)
            info['temp_strings'] = strs
    return info, ents

if __name__ == '__main__':
    info, ents = analyze(sys.argv[1])
    for e in info['entries']:
        print(e)
    print(info.get('catalog'))
    print(info.get('temp_head'))
