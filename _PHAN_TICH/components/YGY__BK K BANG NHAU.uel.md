# ♥YGY_BK-SMALL

- File: `YGY\BK K BANG NHAU.uel`  (22,936 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	♥YGY_BK-SMALL	All`
- Dialog parameters: 9 | Variables (P/custom): 21 | Distance vars (D): 122 | Parts with bound profile: 10
- Lookup files (fVF): -
- Functions used: fP, if
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "♥YGY_BK-SMALL")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("SF THK.", "P1", distance, number, 1)
           parameter("SF Material", "P2", material, text, 2)
           parameter("SF End Gap", "P5", distance, number, 3)
           parameter("SF Scallope", "P9", distance, number, 4)
           parameter("Back SF Yes/No", "P10", YesNo, number, 5)
           parameter("FNG. Gap", "P11", distance, number, 6)
           parameter("보강SF Min. Hieght", "P16", distance, number, 7)
           parameter("PREFIX", "P20", string, text, 8)
           parameter("PREFIX", "P22", string, text, 9)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P2 | SF Material | Material | `SM355` |
| P4 | FB Material | Material | `SS275` |
| P5 | SF End Gap | Length | `5.00` |
| P6 | FB 여장길이 | Length | `20.00` |
| P7 | ROOT값 | Length | `2.00` |
| P8 | 형강 Scallope | Length | `35.00` |
| P9 | SF Scallope | Length | `30.00` |
| P10 | Back SF Yes/No | Yes/No | `0` |
| P11 | FNG. Gap | Length | `5.00` |
| P12 | 보강SF Scallope Yes/No | Yes/No | `1` |
| P13 | Parameter13 | Length | `fP(proH,"ID665D23F6-0000-611B-3137-313733383131")-fP(proH,"ID665D23F6-0000-611F-3137-313733383131")-fP(proT,"ID665D23F6-0000-611B-3137-313733383131")` *(formula)* |
| P14 | Parameter14 | Length | `if (P19>P16) then 0 else 1 endif` *(formula)* |
| P15 | Parameter15 | Length | `if (P13<P16) then 0 else 1 endif` *(formula)* |
| P16 | 보강SF Min. Hieght | Length | `100.00` |
| P17 | Parameter17 | Length | `fP(proT,"ID665D23F6-0000-6144-3137-313733383131")+fP(proT,"ID665D23F6-0000-611F-3137-313733383131")` *(formula)* |
| P18 | Parameter18 | Length | `if (P19>P16) then 0 else P17 endif` *(formula)* |
| P19 | Parameter19 | Length | `fP(proH,"ID665D23F6-0000-611B-3137-313733383131")+1-fP(proH,"ID665D23F6-0000-611F-3137-313733383131")-fP(proT,"ID665D23F6-0000-611B-3137-313733383131")` *(formula)* |
| P20 | PREFIX | Text | `SF` |
| P21 | START NUMBER | Integer | `1` |
| P22 | PREFIX | Text | `FB` |
| P23 | START NUMBER | Integer | `1` |

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
| D9 | D9.CUTPART.GIRDER | `` |
| D10 | D10.Top Flg. Part Cut | `fP(proT,"ID665D23F6-0000-611F-3137-313733383131")-P7` |
| D11 | D11.CUTPART.GIRDER | `fP(proT,"ID665D23F6-0000-611B-3137-313733383131")+.3` |
| D12 | D12.CUTPART.GIRDER | `fP(proT,"ID665D23F6-0000-611B-3137-313733383131")+P9+.3` |
| D13 | D13.CUTPART.GIRDER | `fP(proT,"ID665D23F6-0000-611B-3137-313733383131")+P9+.3` |
| D14 | D14.CUTPART.GIRDER | `fP(proT,"ID665D23F6-0000-611B-3137-313733383131")+.3` |
| D15 | D15.CUTPART.GIRDER | `` |
| D16 | D16.CUTPART.GIRDER | `fP(proS,"ID665D23F6-0000-611B-3137-313733383131")/2` |
| D17 | D17.CUTPART.GIRDER | `fP(proS,"ID665D23F6-0000-611B-3137-313733383131")/2` |
| D18 | D18.CUTPART.GIRDER | `P9+fP(proS,"ID665D23F6-0000-611B-3137-313733383131")/2` |
| D19 | D19.CUTPART.GIRDER | `(fP(proT,"ID665D23F6-0000-611F-3137-313733383131")-P7)+P11+fP(proB,"ID665D23F6-0000-611B-3137-313733383131")/2` |
| D20 | D20.CUTPART.GIRDER | `P9+fP(proS,"ID665D23F6-0000-611B-3137-313733383131")/2` |
| D21 | D21.CUTPART.GIRDER | `P11+fP(proB,"ID665D23F6-0000-611B-3137-313733383131")/2` |
| D22 | D22.CUTPART.GIRDER | `P11+fP(proB,"ID665D23F6-0000-611B-3137-313733383131")/2` |
| D23 | D23.PLATE.GIRDER | `` |
| D24 | D24.PLATE.GIRDER | `` |
| D25 | D25.PLATE.GIRDER | `` |
| D26 | D26.PLATE.GIRDER | `` |
| D27 | D27.PLATE.GIRDER | `` |
| D28 | D28.PLATE.GIRDER | `P8` |
| D29 | D29.PLATE.GIRDER | `P8-P11` |
| D30 | D30.PLATE.GIRDER | `P8+P11` |
| D31 | D31.PLATE.GIRDER | `P11` |
| D32 | D32.CUTPART.GIRDER | `` |
| D33 | D33.CUTPART.GIRDER | `` |
| D34 | D34.CUTPART.GIRDER | `` |
| D35 | D35.CUTPART.GIRDER | `` |
| D36 | D36.CUTPART.GIRDER | `` |
| D37 | D37.CUTPART.GIRDER | `` |
| D38 | D38.CUTPART.GIRDER | `` |
| D39 | D39.CUTPART.GIRDER | `` |
| D40 | D40.CUTPART.GIRDER | `P11+(fP(proT,"ID665D23F6-0000-611F-3137-313733383131")-P7)` |
| D41 | D41.CUTPART.GIRDER | `P11` |
| D42 | D42.CUTPART.GIRDER | `P11` |
| D43 | D43.CUTPART.GIRDER | `P8` |
| D44 | D44.CUTPART.GIRDER | `P8` |
| D45 | D45.CUTPART.GIRDER | `` |
| D46 | D46.CUTPART.GIRDER | `` |
| D47 | D47.CUTPART.GIRDER | `` |
| D48 | D48.CUTPART.GIRDER | `fP(proT,"ID665D23F6-0000-611F-3137-313733383131")-P7` |
| D49 | D49.CUTPART.GIRDER | `fP(proT,"ID665D23F6-0000-611F-3137-313733383131")` |
| D50 | D50.CUTPART.GIRDER | `fP(proT,"ID665D23F6-0000-611F-3137-313733383131")` |
| D51 | D51.CUTPART.GIRDER | `fP(proT,"ID665D23F6-0000-611F-3137-313733383131")+P8` |
| D52 | D52.CUTPART.GIRDER | `fP(proT,"ID665D23F6-0000-611F-3137-313733383131")+P8` |
| D53 | D53.FLAT_BAR.GIRDER | `P6` |
| D54 | D54.FLAT_BAR.GIRDER | `P6` |
| D55 | D55.FLAT_BAR.GIRDER | `P6` |
| D56 | D56.FLAT_BAR.GIRDER | `P6` |
| D57 | D57.FLAT_BAR.GIRDER | `` |
| D58 | D58.FLAT_BAR.GIRDER | `` |
| D59 | D59.FLAT_BAR.GIRDER | `` |
| D60 | D60.FLAT_BAR.GIRDER | `` |
| D61 | D61.FLAT_BAR.GIRDER | `` |
| D62 | D62.FLAT_BAR.GIRDER | `P18` |
| D63 | D63.FLAT_BAR.GIRDER | `P18` |
| D64 | D64.FLAT_BAR.GIRDER | `` |
| D65 | D65.PLATE.GIRDER | `` |
| D66 | D66.PLATE.GIRDER | `` |
| D67 | D67.PLATE.GIRDER | `` |
| D68 | D68.PLATE.GIRDER | `` |
| D69 | D69.PLATE.GIRDER | `` |
| D70 | D70.PLATE.GIRDER | `` |
| D71 | D71.PLATE.GIRDER | `` |
| D72 | D72.PLATE.GIRDER | `` |
| D73 | D73.PLATE.GIRDER | `` |
| D74 | D74.PLATE.GIRDER | `` |
| D75 | D75.PLATE.GIRDER | `` |
| D76 | D76.PLATE.GIRDER | `` |
| D77 | D77.PLATE.GIRDER | `` |
| D78 | D78.PLATE.GIRDER | `P5` |
| D79 | D79.PLATE.GIRDER | `P5` |
| D80 | D80.PLATE.GIRDER | `P5` |
| D81 | D81.PLATE.GIRDER | `P5` |
| D82 | D82.PLATE.GIRDER | `` |
| D83 | D83.PLATE.GIRDER | `` |
| D84 | D84.PLATE.GIRDER | `` |
| D85 | D85.PLATE.GIRDER | `` |
| D86 | D86.PLATE.GIRDER | `` |
| D87 | D87.PLATE.GIRDER | `` |
| D88 | D88.PLATE.GIRDER | `` |
| D89 | D89.CUTPART.GIRDER | `` |
| D90 | D90.CUTPART.GIRDER | `` |
| D91 | D91.CUTPART.GIRDER | `` |
| D92 | D92.CUTPART.GIRDER | `` |
| D93 | D93.CUTPART.GIRDER | `` |
| D94 | D94.CUTPART.GIRDER | `P9` |
| D95 | D95.CUTPART.GIRDER | `P9` |
| D96 | D96.CUTPART.GIRDER | `` |
| D97 | D97.CUTPART.GIRDER | `` |
| D98 | D98.CUTPART.GIRDER | `` |
| D99 | D99.CUTPART.GIRDER | `` |
| D100 | D100.CUTPART.GIRDER | `` |
| D101 | D101.CUTPART.GIRDER | `P9+.3` |
| D102 | D102.CUTPART.GIRDER | `P9+.3` |
| D103 | D103.CUTPART.GIRDER | `P5` |
| D104 | D104.CUTPART.GIRDER | `P5` |
| D105 | D105.CUTPART.GIRDER | `P9` |
| D106 | D106.CUTPART.GIRDER | `P9` |
| D107 | D107.CUTPART.GIRDER | `` |
| D108 | D108.CUTPART.GIRDER | `` |
| D109 | D109.CUTPART.GIRDER | `` |
| D110 | D110.CUTPART.GIRDER | `` |
| D111 | D111.CUTPART.GIRDER | `` |
| D112 | D112.CUTPART.GIRDER | `P5` |
| D113 | D113.CUTPART.GIRDER | `P5` |
| D114 | D114.CUTPART.GIRDER | `P7` |
| D115 | D115.CUTPART.GIRDER | `` |
| D116 | D116.CUTPART.GIRDER | `P11+(fP(proT,"ID665D23F6-0000-611F-3137-313733383131")-P7)` |
| D117 | D117.CUTPART.GIRDER | `` |
| D118 | D118.CUTPART.GIRDER | `P8` |
| D119 | D119.CUTPART.GIRDER | `P8` |
| D120 | D120.CUTPART.GIRDER | `P8` |
| D121 | D121.CUTPART.GIRDER | `` |
| D122 | D122.CUTPART.GIRDER | `P8` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#136 | CREATION | `P10` |
| obj#136 | MATERIAL | `P2` |
| obj#136 | PROFILE | `"PL"+P1` |
| obj#136 | SERIE | `P20` |
| obj#136 | STARTNUMBER | `P21` |
| obj#149 | MATERIAL | `P2` |
| obj#149 | PROFILE | `"PL"+P1` |
| obj#149 | SERIE | `P20` |
| obj#149 | STARTNUMBER | `P21` |
| obj#157 | MATERIAL | `P4` |
| obj#157 | PROFILE | `P3` |
| obj#157 | SERIE | `P22` |
| obj#157 | STARTNUMBER | `P23` |
| obj#168 | MATERIAL | `P4` |
| obj#168 | PROFILE | `P3` |
| obj#168 | SERIE | `P22` |
| obj#168 | STARTNUMBER | `P23` |
| obj#175 | CREATION | `P14` |
| obj#175 | PROFILE | `fP(proB,"ID665D23F6-0000-611F-3137-313733383131")+20` |
| obj#188 | PROFILE | `fP(proB,"ID665D23F6-0000-611F-3137-313733383131")+20` |
| obj#197 | PROFILE | `fP(proB,"ID665D23F6-0000-611F-3137-313733383131")+20` |
| obj#207 | CREATION | `P15` |
| obj#207 | PROFILE | `fP(proB,"ID665D23F6-0000-611F-3137-313733383131")+20` |
| obj#223 | PROFILE | `fP(proB,"ID665D23F6-0000-611F-3137-313733383131")+20` |
| obj#233 | CREATION | `P14` |
| obj#233 | PROFILE | `fP(proT,"ID665D23F6-0000-613E-3137-313733383131")+10` |