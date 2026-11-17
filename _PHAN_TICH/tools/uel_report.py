"""Build a per-component markdown report + JSON index for all .uel files under a folder."""
import os, re, sys, json, gzip
sys.path.insert(0, os.path.dirname(__file__))
import uel_parse as U
import uel_decode as D
from uel_pool import decode

TYPES = {0: 'Integer', 1: 'Length', 2: 'Text', 3: 'Angle', 4: 'Material', 5: 'Profile',
         11: 'Chamfer', 14: 'Yes/No'}
CAT = {'-1000': 'Detail?', '-100': 'Connection?'}

def comp_kind(inp):
    m = re.search(r'\b(joint|detail|part|seam|macro)\s*\(\s*(-?\d+)\s*,\s*"([^"]*)"', inp or '')
    return (m.group(1), m.group(3)) if m else ('(no dialog)', None)

def inp_params(inp):
    a = re.findall(r'parameter\("([^"]*)",\s*"([^"]+)",\s*(\w+),\s*(\w+),\s*(\d+)\)', inp or '')
    b = re.findall(r'attribute\("([^"]+)",\s*"([^"]*)",\s*(\w+)', inp or '')
    b = [x for x in b if x[2] != 'label']
    return a + b

def run(src, outdir):
    os.makedirs(outdir, exist_ok=True)
    index = []
    for root, _, files in os.walk(src):
        for f in sorted(files):
            if not f.lower().endswith('.uel'):
                continue
            path = os.path.join(root, f)
            rel = os.path.relpath(path, src)
            data = gzip.open(path).read()
            ents = U.entries(data)
            inp = next((decode(e['payload']) for e in ents if e['name'].endswith('.inp')), '')
            cat = next((decode(e['payload']).strip() for e in ents if e['name'].endswith('_ec_uel.txt')), '')
            temp = next((e['payload'] for e in ents if e['name'].endswith('.uel_temp')), b'')
            presets = [(e['name'], decode(e['payload'])) for e in ents if '.p_' in e['name']]
            bmps = [e['name'] for e in ents if e['name'].lower().endswith('.bmp')]
            kind, cname = comp_kind(inp)
            dec = D.decode_component(temp)
            formulas = [b for b in dec['bindings']]
            allf = ' '.join(b['value'] for b in formulas) + ' ' + ' '.join(v['text'] for v in dec['pool'].values())
            vf_files = sorted(set(re.findall(r'fVF\("([^"]+)"', allf)))
            funcs = sorted(set(re.findall(r'\b(f[A-Z][A-Za-z]*|mid|if|sqrt|tan|sin|cos|atan|abs|int|round|min|max)\s*\(', allf)))
            ui = inp_params(inp)
            nparts = len({b['obj'] for b in formulas if b['prop'] == 'proPROFILE'})
            rec = dict(file=rel, size=os.path.getsize(path), kind=kind, name=cname, catalog=cat,
                       ui_params=len(ui), variables=len(dec['params']), dvars=len(dec['dims']),
                       bound_parts=nparts, vf_files=vf_files, functions=funcs, bitmaps=len(bmps),
                       presets=[n for n, _ in presets])
            index.append(rec)
            # markdown
            md = [f'# {cname or f}', '', f'- File: `{rel}`  ({rec["size"]:,} bytes, gzip)',
                  f'- Type in .inp: **{kind}**  | Catalog line: `{cat}`',
                  f'- Dialog parameters: {len(ui)} | Variables (P/custom): {len(dec["params"])} | '
                  f'Distance vars (D): {len(dec["dims"])} | Parts with bound profile: {nparts}',
                  f'- Lookup files (fVF): {", ".join(vf_files) or "-"}',
                  f'- Functions used: {", ".join(funcs) or "-"}',
                  f'- Dialog bitmaps: {len(bmps)} | Saved presets: {", ".join(n for n, _ in presets) or "-"}', '']
            md += ['## Dialog (.inp)', '```', inp.strip(), '```', '']
            md += ['## Variables', '', '| Name | Label | Type | Value / formula |', '|---|---|---|---|']
            fmap = {b['owner']: b['value'] for b in formulas if b['prop'] == 'proVALUE'}
            def esc(s): return (s or '').replace('|', '\\|').replace('\n', ' ')
            for p in sorted(dec['params'], key=lambda r: (not r['name'].startswith('P'), int(re.sub(r'\D', '', r['name']) or 0) if r['name'].startswith('P') else 0, r['name'])):
                val = fmap.get(p['name'], p['value'])
                tag = '' if p['name'] not in fmap else ' *(formula)*'
                md.append(f'| {p["name"]} | {esc(p["label"])} | {TYPES.get(p["type"], p["type"])} | `{esc(val)}`{tag} |')
            md += ['', '## Distance variables (D) bound to planes', '', '| D | Binding | Formula |', '|---|---|---|']
            for oid, dv in sorted(dec['dims'].items(), key=lambda kv: int(kv[1]['name'][1:])):
                md.append(f'| {dv["name"]} | {esc(dv["desc"])} | `{esc(fmap.get(dv["name"], ""))}` |')
            md += ['', '## Part attribute bindings', '', '| Object | Property | Formula |', '|---|---|---|']
            for b in sorted(formulas, key=lambda b: (b['obj'], b['prop'])):
                if b['prop'] == 'proVALUE':
                    continue
                md.append(f'| {b["owner"]} | {b["prop"][3:]} | `{esc(b["value"])}` |')
            for n, t in presets:
                md += ['', f'## Preset `{n}`', '```', t.strip(), '```']
            safe = re.sub(r'[\\/:]', '__', rel)
            open(os.path.join(outdir, safe + '.md'), 'w', encoding='utf-8').write('\n'.join(md))
    json.dump(index, open(os.path.join(outdir, '_index.json'), 'w', encoding='utf-8'), ensure_ascii=False, indent=1)
    return index

if __name__ == '__main__':
    idx = run(sys.argv[1], sys.argv[2])
    for r in idx:
        print(f"{r['file'][:48]:48} {str(r['kind']):6} {str(r['name'])[:28]:28} ui={r['ui_params']:3} var={r['variables']:3} D={r['dvars']:3} parts={r['bound_parts']:3} bmp={r['bitmaps']:2} vf={','.join(r['vf_files'])}")
