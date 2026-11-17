# CUT_PL_V2

- File: `CUT-PL-WALE.uel`  (4,429 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	CUT_PL_V2	All`
- Dialog parameters: 0 | Variables (P/custom): 1 | Distance vars (D): 9 | Parts with bound profile: 0
- Lookup files (fVF): -
- Functions used: fP
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "CUT_PL_V2")
   {
       tab_page("", " Parameters 1 ", 1)
       {
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Integer | `fP(proT,"ID673A8EA2-0000-540A-3137-333138393132")` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.CUTPART.PLATE | `` |
| D2 | D2.CUTPART.PLATE | `` |
| D3 | D3.CUTPART.PLATE | `` |
| D4 | D4.CUTPART.PLATE | `` |
| D5 | D5.CUTPART.PLATE | `P1` |
| D6 | D6.CUTPART.PLATE | `P1` |
| D7 | D7.CUTPART.PLATE | `P1-2` |
| D8 | D8.CUTPART.PLATE | `` |
| D9 | D9.CUTPART.PLATE | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|