# CUT_WALEEE

- File: `CUT-WALE.uel`  (5,780 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	CUT_WALEEE	All`
- Dialog parameters: 0 | Variables (P/custom): 4 | Distance vars (D): 16 | Parts with bound profile: 1
- Lookup files (fVF): -
- Functions used: fP
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "CUT_WALEEE")
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
| P1 | Parameter1 | Integer | `fP(proS,"ID673A8EA2-0000-53B0-3137-333138393132")` *(formula)* |
| P2 | Parameter2 | Length | `fP(proH,"ID673A8EA2-0000-53B0-3137-333138393132")` *(formula)* |
| P3 | Parameter3 | Length | `fP(proT,"ID673A8EA2-0000-53B0-3137-333138393132")` *(formula)* |
| P4 | Parameter4 | Length | `P2-2*P3-30` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.CUTPART.W_GIRDER | `` |
| D2 | D2.CUTPART.W_GIRDER | `` |
| D3 | D3.CUTPART.W_GIRDER | `` |
| D4 | D4.CUTPART.W_GIRDER | `` |
| D5 | D5.CUTPART.W_GIRDER | `` |
| D6 | D6.CUTPART.PLATE | `P1-D7` |
| D7 | D7.CUTPART.W_GIRDER | `` |
| D8 | D8.CUTPART.PLATE | `` |
| D9 | D9.CUTPART.PLATE | `` |
| D10 | D10.CUTPART.PLATE | `P1-D7` |
| D11 | D11.CUTPART.W_GIRDER | `` |
| D12 | D12.CUTPART.W_GIRDER | `` |
| D13 | D13.CUTPART.W_GIRDER | `` |
| D14 | D14.CUTPART.W_GIRDER | `` |
| D15 | D15.CUTPART.W_GIRDER | `` |
| D16 | D16.CUTPART.W_GIRDER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#93 | PROFILE | `"BL"+P4` |