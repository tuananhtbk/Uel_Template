# ♥YGY_접합용 하부PL보강-50

- File: `YGY\BU DUONG HAN TSB.uel`  (14,172 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	♥YGY_접합용 하부PL보강-50	All`
- Dialog parameters: 2 | Variables (P/custom): 6 | Distance vars (D): 45 | Parts with bound profile: 3
- Lookup files (fVF): -
- Functions used: fP, if
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "♥YGY_접합용 하부PL보강-50")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("ZONE", "P1", string, text, 1)
           parameter("FLG THK(자동)", "P3", profile, text, 2)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | ZONE | Text | `B2` |
| P2 | 재질 (자동) | Text | `SM355` |
| P3 | FLG THK(자동) | Profile | `"PL"+P4` *(formula)* |
| P4 | Parameter4 | Integer | `fP(proT,"ID664306C2-0000-4CEB-3137-313536363935")` *(formula)* |
| P5 | Parameter5 | Material | `fP(proMATERIAL,"ID664306C2-0000-4CAB-3137-313536363935")` *(formula)* |
| P6 | 재질(자동) | Text | `if (P5=="SHN355") then "SN355" else if (P5=="SHN275") then "SN275" else P5 endif endif` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.TSFPLATE.T_GIRDER | `` |
| D2 | D2.TSFPLATE.T_GIRDER | `` |
| D3 | D3.TSFPLATE.T_GIRDER | `` |
| D4 | D4.TSFPLATE.T_GIRDER | `` |
| D5 | D5.TSFPLATE.T_BEAM | `` |
| D6 | D6.TSFPLATE.T_BEAM | `` |
| D7 | D7.TSFPLATE.T_GIRDER | `` |
| D8 | D8.TSFPLATE.T_GIRDER | `` |
| D9 | D9.TSFPLATE.T_GIRDER | `` |
| D10 | D10.TSFPLATE.T_GIRDER | `` |
| D11 | D11.TSFPLATE.T_BEAM | `` |
| D12 | D12.TSFPLATE.T_BEAM | `` |
| D13 | D13.CUTPART.TSFPLATE | `` |
| D14 | D14.CUTPART.TSFPLATE | `` |
| D15 | D15.CUTPART.TSFPLATE | `` |
| D16 | D16.CUTPART.T_GIRDER | `` |
| D17 | D17.CUTPART.T_GIRDER | `` |
| D18 | D18.CUTPART.T_GIRDER | `` |
| D19 | D19.CUTPART.T_BEAM | `` |
| D20 | D20.CUTPART.T_BEAM | `` |
| D21 | D21.CUTPART.T_BEAM | `` |
| D22 | D22.TSFPLATE.T_GIRDER | `` |
| D23 | D23.TSFPLATE.T_GIRDER | `` |
| D24 | D24.TSFPLATE.T_GIRDER | `` |
| D25 | D25.TSFPLATE.T_GIRDER | `` |
| D26 | D26.TSFPLATE.T_GIRDER | `` |
| D27 | D27.TSFPLATE.T_GIRDER | `` |
| D28 | D28.TSFPLATE.T_GIRDER | `` |
| D29 | D29.TSFPLATE.T_GIRDER | `` |
| D30 | D30.TSFPLATE.TSFPLATE | `` |
| D31 | D31.TSFPLATE.TSFPLATE | `` |
| D32 | D32.TSFPLATE.TSFPLATE | `` |
| D33 | D33.TSFPLATE.TSFPLATE | `` |
| D34 | D34.TSFPLATE.T_GIRDER | `` |
| D35 | D35.TSFPLATE.T_GIRDER | `` |
| D36 | D36.TSFPLATE.T_GIRDER | `` |
| D37 | D37.TSFPLATE.T_GIRDER | `` |
| D40 | D40.TSFPLATE.T_BEAM | `` |
| D41 | D41.TSFPLATE.T_BEAM | `` |
| D42 | D42.TSFPLATE.T_BEAM | `` |
| D43 | D43.TSFPLATE.T_BEAM | `` |
| D44 | D44.TSFPLATE.T_BEAM | `` |
| D45 | D45.TSFPLATE.T_BEAM | `` |
| D46 | D46.TSFPLATE.T_BEAM | `` |
| D47 | D47.TSFPLATE.T_BEAM | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#7825 | MATERIAL | `P6` |
| obj#7825 | PROFILE | `P3` |
| obj#7825 | SERIE | `P1+"TSF"` |
| obj#7840 | MATERIAL | `P6` |
| obj#7840 | PROFILE | `P3` |
| obj#7840 | SERIE | `P1+"TSF"` |
| obj#7850 | MATERIAL | `P6` |
| obj#7850 | PROFILE | `P3` |
| obj#7850 | SERIE | `P1+"TSF"` |