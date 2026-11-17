# ♥YGY_BK-SAME

- File: `BKKKKKKKKK SAME.uel`  (20,591 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	♥YGY_BK-SAME	All`
- Dialog parameters: 6 | Variables (P/custom): 28 | Distance vars (D): 111 | Parts with bound profile: 8
- Lookup files (fVF): -
- Functions used: fP, if, mid
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "♥YGY_BK-SAME")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("SF Yes/No", "P10", YesNo, number, 1)
           parameter("SF THK(자동)", "sfthk", profile, text, 2)
           parameter("ZONE", "zone", string, text, 3)
           parameter("MAT(자동)", "mat2", string, text, 4)
           parameter("SC TYPE", "sctype", chamfer_type, number, 5)
           parameter("SC 크기(자동)", "sc", distance, number, 6)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | ROOT값 | Length | `2.00` |
| P3 | FB Material | Material | `SS275` |
| P4 | FLG. Gap | Length | `5.00` |
| P5 | WEB Scallope | Length | `35.00` |
| P6 | SF Scallope | Length | `30.00` |
| P7 | SF End Gap | Length | `10.00` |
| P8 | SF THK. | Length | `13.00` |
| P9 | SF Material | Material | `SM355` |
| P10 | SF Yes/No | Yes/No | `0` |
| P11 | TEST | Length | `(fP(proB,"ID673A8EA2-0000-3614-3137-333138393130")-fP(proS,"ID673A8EA2-0000-3614-3137-333138393130"))/2+P4+(fP(proT,"ID673A8EA2-0000-3618-3137-333138393130")-P1)` *(formula)* |
| P12 | SF PREFIX | Text | `SF` |
| P13 | SF STNUMBER | Integer | `1` |
| P14 | FB PREFIX | Text | `FB` |
| P15 | FB STNUMBER | Integer | `1` |
| P16 | Parameter16 | Length | `0.00` |
| P17 | Parameter17 | Length | `0.00` |
| P18 | Parameter18 | Length | `0.00` |
| P19 | Parameter19 | Text | `fP(proSERIE,"ID673A8EA2-0000-3614-3137-333138393130")` *(formula)* |
| P20 | Parameter20 | Text | `mid(P19,0,find(P19,"M"))` *(formula)* |
| P21 | Parameter21 | Text | `fP(proSERIE,"ID673A8EA2-0000-3618-3137-333138393130")` *(formula)* |
| P22 | Parameter22 | Text | `mid(P21,0,find(P21,"M"))` *(formula)* |
| gw | Parameter19 | Length | `fP(proB,"ID673A8EA2-0000-3614-3137-333138393130")` *(formula)* |
| mat | Parameter19 | Material | `fP(proMATERIAL,"ID673A8EA2-0000-3614-3137-333138393130")` *(formula)* |
| mat2 | MAT(자동) | Text | `if (mat=="SHN355") then "SN355" else if (mat=="SHN275") then "SN275" else mat endif endif` *(formula)* |
| sc | SC 크기(자동) | Length | `if (gw<181) then 25 else if (gw<200) then 35 else if (250<gw) then 35 else 35 endif endif endif` *(formula)* |
| sctype | SC TYPE | Chamfer | `3` |
| sfthk | SF THK(자동) | Profile | `fP(proS,"ID673A8EA2-0000-3618-3137-333138393130")` *(formula)* |
| zone | ZONE | Text | `P20` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.FITTING.Web right plane | `` |
| D2 | D2.CUTPART.GIRDER | `` |
| D3 | D3.CUTPART.GIRDER | `` |
| D4 | D4.CUTPART.GIRDER | `` |
| D5 | D5.CUTPART.GIRDER | `` |
| D6 | D6.CUTPART.GIRDER | `` |
| D7 | D7.CUTPART.GIRDER | `` |
| D8 | D8.CUTPART.GIRDER | `` |
| D9 | D9.PLATE.GIRDER | `` |
| D10 | D10.PLATE.GIRDER | `` |
| D11 | D11.PLATE.GIRDER | `` |
| D12 | D12.PLATE.GIRDER | `` |
| D13 | D13.PLATE.GIRDER | `` |
| D14 | D14.PLATE.GIRDER | `` |
| D15 | D15.CUTPART.GIRDER | `` |
| D16 | D16.CUTPART.GIRDER | `` |
| D17 | D17.CUTPART.GIRDER | `` |
| D18 | D18.CUTPART.GIRDER | `` |
| D19 | D19.CUTPART.GIRDER | `P6+.3` |
| D20 | D20.CUTPART.GIRDER | `P6+.3` |
| D21 | D21.CUTPART.GIRDER | `P6` |
| D22 | D22.CUTPART.GIRDER | `` |
| D23 | D23.CUTPART.GIRDER | `` |
| D24 | D24.CUTPART.GIRDER | `P1` |
| D25 | D25.CUTPART.GIRDER | `` |
| D26 | D26.CUTPART.GIRDER | `` |
| D27 | D27.CUTPART.GIRDER | `P6+.3` |
| D28 | D28.CUTPART.GIRDER | `P6+.3` |
| D29 | D29.CUTPART.GIRDER | `` |
| D30 | D30.CUTPART.GIRDER | `` |
| D31 | D31.CUTPART.GIRDER | `P6` |
| D32 | D32.CUTPART.GIRDER | `P6` |
| D33 | D33.CUTPART.GIRDER | `P4` |
| D34 | D34.CUTPART.GIRDER | `P4` |
| D35 | D35.CUTPART.GIRDER | `P4+fP(proT,"ID673A8EA2-0000-3618-3137-333138393130")+5-P1` |
| D36 | D36.PLATE.GIRDER | `P5` |
| D37 | D37.PLATE.GIRDER | `` |
| D38 | D38.PLATE.GIRDER | `` |
| D39 | D39.PLATE.GIRDER | `P4` |
| D40 | D40.PLATE.GIRDER | `P5-P4` |
| D41 | D41.PLATE.GIRDER | `P5+P4` |
| D42 | D42.PLATE.GIRDER | `P5+P4` |
| D43 | D43.PLATE.GIRDER | `P4` |
| D44 | D44.PLATE.GIRDER | `P5-P4` |
| D45 | D45.PLATE.GIRDER | `` |
| D46 | D46.PLATE.GIRDER | `` |
| D47 | D47.PLATE.GIRDER | `P5` |
| D48 | D48.CUTPART.GIRDER | `P6` |
| D49 | D49.CUTPART.GIRDER | `` |
| D50 | D50.CUTPART.GIRDER | `` |
| D51 | D51.CUTPART.GIRDER | `` |
| D52 | D52.CUTPART.GIRDER | `` |
| D53 | D53.CUTPART.GIRDER | `` |
| D54 | D54.CUTPART.GIRDER | `` |
| D55 | D55.CUTPART.GIRDER | `` |
| D56 | D56.CUTPART.GIRDER | `` |
| D57 | D57.CUTPART.GIRDER | `` |
| D58 | D58.CUTPART.GIRDER | `` |
| D59 | D59.CUTPART.GIRDER | `` |
| D60 | D60.CUTPART.GIRDER | `` |
| D61 | FB 여장길이 | `` |
| D62 | D62.FLAT_BAR.GIRDER | `D61` |
| D63 | D63.FLAT_BAR.GIRDER | `D61` |
| D64 | D64.FLAT_BAR.GIRDER | `D61` |
| D65 | D65.FLAT_BAR.GIRDER | `` |
| D66 | D66.FLAT_BAR.GIRDER | `` |
| D67 | D67.FLAT_BAR.GIRDER | `` |
| D68 | D68.FLAT_BAR.GIRDER | `` |
| D69 | D69.FLAT_BAR.GIRDER | `` |
| D70 | D70.FLAT_BAR.GIRDER | `` |
| D71 | D71.FLAT_BAR.GIRDER | `` |
| D72 | D72.FLAT_BAR.GIRDER | `` |
| D73 | D73.PLATE.GIRDER | `-P7` |
| D74 | D74.PLATE.GIRDER | `-P7` |
| D75 | D75.PLATE.GIRDER | `` |
| D76 | D76.PLATE.GIRDER | `` |
| D77 | D77.PLATE.GIRDER | `` |
| D78 | D78.PLATE.GIRDER | `` |
| D79 | D79.PLATE.GIRDER | `` |
| D80 | D80.PLATE.GIRDER | `` |
| D81 | D81.PLATE.GIRDER | `` |
| D82 | D82.PLATE.GIRDER | `` |
| D83 | D83.PLATE.GIRDER | `` |
| D84 | D84.PLATE.GIRDER | `` |
| D85 | D85.CUTPART.GIRDER | `` |
| D86 | D86.CUTPART.GIRDER | `` |
| D87 | D87.CUTPART.GIRDER | `` |
| D88 | D88.CUTPART.GIRDER | `` |
| D89 | D89.CUTPART.GIRDER | `` |
| D90 | D90.CUTPART.GIRDER | `` |
| D91 | D91.CUTPART.GIRDER | `` |
| D92 | D92.CUTPART.GIRDER | `P1` |
| D93 | D93.CUTPART.GIRDER | `P4` |
| D94 | D94.CUTPART.GIRDER | `P4` |
| D95 | D95.CUTPART.GIRDER | `P4+fP(proT,"ID673A8EA2-0000-3618-3137-333138393130")+5-P1` |
| D96 | D96.CUTPART.GIRDER | `` |
| D97 | D97.CUTPART.GIRDER | `` |
| D98 | D98.CUTPART.GIRDER | `P4` |
| D99 | D99.CUTPART.GIRDER | `` |
| D100 | D100.CUTPART.GIRDER | `` |
| D101 | D101.CUTPART.GIRDER | `P6+.3` |
| D102 | D102.CUTPART.GIRDER | `P6+.3` |
| D103 | D103.CUTPART.GIRDER | `P6` |
| D104 | D104.CUTPART.GIRDER | `` |
| D105 | D105.CUTPART.GIRDER | `P6` |
| D106 | D106.CUTPART.GIRDER | `` |
| D107 | D107.CUTPART.GIRDER | `P4` |
| D108 | D108.CUTPART.GIRDER | `P4` |
| D109 | D109.CUTPART.GIRDER | `P4` |
| D110 | D110.CUTPART.GIRDER | `` |
| D111 | D111.CUTPART.GIRDER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#150 | MATERIAL | `P3` |
| obj#150 | PROFILE | `P2` |
| obj#150 | SERIE | `P22+"FB"` |
| obj#150 | STARTNUMBER | `P15` |
| obj#162 | MATERIAL | `P3` |
| obj#162 | PROFILE | `P2` |
| obj#162 | SERIE | `P22+"FB"` |
| obj#162 | STARTNUMBER | `P15` |
| obj#169 | CREATION | `P10` |
| obj#169 | MATERIAL | `mat2` |
| obj#169 | PROFILE | `"PL"+sfthk` |
| obj#169 | SERIE | `zone+"SF"` |
| obj#169 | STARTNUMBER | `P13` |
| obj#180 | PROFILE | `fP(proB,"ID673A8EA2-0000-3618-3137-333138393130")+20` |
| obj#193 | PROFILE | `fP(proB,"ID673A8EA2-0000-3618-3137-333138393130")+20` |
| obj#204 | PROFILE | `fP(proB,"ID673A8EA2-0000-3618-3137-333138393130")+20` |
| obj#223 | PROFILE | `fP(proB,"ID673A8EA2-0000-3618-3137-333138393130")+20` |
| obj#233 | PROFILE | `fP(proB,"ID673A8EA2-0000-3618-3137-333138393130")+20` |