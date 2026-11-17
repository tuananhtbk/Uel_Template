# ♥YGY_BK-END_NEW

- File: `YGY\BK-END_PLATE.uel`  (35,052 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	♥YGY_BK-END_NEW	All`
- Dialog parameters: 13 | Variables (P/custom): 40 | Distance vars (D): 184 | Parts with bound profile: 15
- Lookup files (fVF): -
- Functions used: cos, fP, if
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "♥YGY_BK-END_NEW")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("Zone", "zone", string, text, 1)
           parameter("ON/OFF SF Web", "P14", YesNo, number, 2)
           parameter("ON/OFF SF Flg. ", "P15", YesNo, number, 3)
           parameter("단차보강1", "P16", YesNo, number, 4)
           parameter("단차보강2", "P35", YesNo, number, 5)
           parameter("단차보강 길이1", "P25", distance, number, 6)
           parameter("단차보강 길이2", "P26", distance, number, 7)
           parameter("ENDPL(자동)", "flgthk", distance, number, 8)
           parameter("삼각PL(자동)", "webthk", distance, number, 9)
           parameter("수평PL(자동)", "flgthk2", distance, number, 10)
           parameter("MATEA(자동)", "mat2", string, text, 11)
           parameter("스칼렙 TYPE", "CHAM1", chamfer_type, number, 12)
           parameter("스칼렙 크기", "CHAM2", distance, number, 13)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | END PL. Thk. | Length | `20.00` |
| P2 | SF Web Thk. | Length | `10.00` |
| P4 | Plate Material | Material | `SM355` |
| P5 | SF Scallope | Length | `30.00` |
| P7 | FB Material | Material | `SS275` |
| P8 | FB 여장길이 | Length | `20.00` |
| P9 | Flg. Gap | Length | `5.00` |
| P10 | Root값 | Length | `2.00` |
| P11 | 형강 Scallope | Length | `35.00` |
| P12 | 1ST-BK-END | Text | `1ST-BK-END` |
| P13 | Parameter13 | Length | `if (fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")>fP(proH,"ID66430C93-0000-7F93-3137-313536373131")) then fP(proH,"ID66430C93-0000-7F8F-3137-313536373131") else fP(proH,"ID66430C93-0000-7F93-3137-313536373131") endif` *(formula)* |
| P14 | ON/OFF SF Web | Yes/No | `1` |
| P15 | ON/OFF SF Flg.  | Yes/No | `1` |
| P16 | 단차보강1 | Yes/No | `0` |
| P17 | Parameter17 | Length | `P3` *(formula)* |
| P18 | Parameter18 | Length | `if (fP(proB,"ID66430C93-0000-7F8F-3137-313536373131")>fP(proB,"ID66430C93-0000-7F93-3137-313536373131")) then fP(proB,"ID66430C93-0000-7F8F-3137-313536373131") else fP(proB,"ID66430C93-0000-7F93-3137-313536373131") endif` *(formula)* |
| P19 | SF PREFIX | Text | `SF` |
| P20 | SF NO | Integer | `1` |
| P21 | FB PREFIX | Text | `FB` |
| P22 | FB NO | Integer | `1` |
| P23 | EP PREFIX | Text | `EP` |
| P24 | EP NO | Integer | `1` |
| P25 | 단차보강 길이1 | Length | `250.00` |
| P26 | 단차보강 길이2 | Length | `250.00` |
| P28 | 단차보강 SCALLOPE1 | Yes/No | `0` |
| P29 | 단차보강 SCALLOPE2 | Yes/No | `0` |
| P30 | Parameter30 | Yes/No | `if (P28==1) then 3 else 0 endif` *(formula)* |
| P31 | Parameter31 | Length | `if (P28==1) then 30 else 0 endif` *(formula)* |
| P32 | Parameter32 | Length | `if (P29==1) then 3 else 0 endif` *(formula)* |
| P33 | Parameter33 | Length | `if (P29==1) then 30 else 0 endif` *(formula)* |
| P34 | Parameter34 | Length | `0.00` |
| P35 | 단차보강2 | Yes/No | `0` |
| CHAM1 | 스칼렙 TYPE | Chamfer | `1` |
| CHAM2 | 스칼렙 크기 | Length | `17.00` |
| flgthk | ENDPL(자동) | Length | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")` *(formula)* |
| flgthk2 | 수평PL(자동) | Length | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")` *(formula)* |
| mat | Parameter27 | Material | `fP(proMATERIAL,"ID66430C93-0000-7F8F-3137-313536373131")` *(formula)* |
| mat2 | MATEA(자동) | Text | `if (mat=="SHN355") then "SN355" else if (mat=="SHN275") then "SN275" else mat endif endif` *(formula)* |
| webthk | 삼각PL(자동) | Length | `fP(proS,"ID66430C93-0000-7F8F-3137-313536373131")` *(formula)* |
| zone | Zone | Text | `B2` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.GIRDER | `` |
| D2 | D2.PLATE.GIRDER | `` |
| D3 | D3.PLATE.GIRDER | `P18/2` |
| D4 | D4.PLATE.GIRDER | `P18/2` |
| D5 | D5.PLATE.GIRDER | `P18/2` |
| D6 | D6.PLATE.GIRDER | `P18/2` |
| D7 | D7.PLATE.GIRDER | `P13` |
| D8 | D8.PLATE.GIRDER | `P13` |
| D9 | D9.FITTING.Extrema front plane | `` |
| D10 | D10.FLAT_BAR.PLATE | `` |
| D11 | D11.FLAT_BAR.PLATE | `` |
| D12 | D12.FLAT_BAR.GIRDER | `P8` |
| D13 | D13.FLAT_BAR.GIRDER | `` |
| D14 | D14.FLAT_BAR.GIRDER | `` |
| D15 | D15.FLAT_BAR.GIRDER | `P8` |
| D16 | D16.FLAT_BAR.GIRDER | `P8` |
| D17 | D17.FLAT_BAR.PLATE | `` |
| D18 | D18.FLAT_BAR.GIRDER | `` |
| D19 | D19.FLAT_BAR.GIRDER | `` |
| D20 | D20.FLAT_BAR.PLATE | `` |
| D21 | D21.FLAT_BAR.GIRDER | `P8` |
| D22 | D22.FLAT_BAR.GIRDER | `P8` |
| D23 | D23.FLAT_BAR.GIRDER | `` |
| D24 | D24.FLAT_BAR.PLATE | `` |
| D25 | D25.FLAT_BAR.GIRDER | `` |
| D26 | D26.FLAT_BAR.GIRDER | `P8` |
| D27 | D27.FLAT_BAR.PLATE | `` |
| D28 | D28.FLAT_BAR.GIRDER | `P8` |
| D29 | D29.FLAT_BAR.PLATE | `` |
| D30 | D30.FLAT_BAR.GIRDER | `P8` |
| D31 | D31.FLAT_BAR.PLATE | `` |
| D32 | D32.FLAT_BAR.GIRDER | `` |
| D33 | D33.PLATE.GIRDER | `` |
| D34 | D34.PLATE.GIRDER | `` |
| D35 | D35.PLATE.GIRDER | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D36 | D36.PLATE.PLATE | `` |
| D37 | D37.PLATE.PLATE | `` |
| D38 | D38.PLATE.PLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D39 | D39.PLATE.GIRDER | `` |
| D40 | D40.PLATE.GIRDER | `` |
| D41 | D41.PLATE.GIRDER | `` |
| D42 | D42.PLATE.GIRDER | `` |
| D43 | D43.PLATE.GIRDER | `` |
| D44 | D44.PLATE.GIRDER | `` |
| D45 | D45.PLATE.GIRDER | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D46 | D46.PLATE.GIRDER | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D47 | D47.PLATE.PLATE | `` |
| D48 | D48.PLATE.PLATE | `` |
| D49 | D49.PLATE.PLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D50 | D50.PLATE.PLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D51 | D51.CUTPART.GIRDER | `` |
| D52 | D52.CUTPART.GIRDER | `` |
| D53 | D53.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")-P10` |
| D54 | D54.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")+.3` |
| D55 | D55.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")+P11+.3` |
| D56 | D56.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")+.3` |
| D57 | D57.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")+P11+.3` |
| D58 | D58.CUTPART.PLATE | `P9+fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")-P10+5` |
| D59 | D59.CUTPART.PLATE | `P9` |
| D60 | D60.CUTPART.PLATE | `P9` |
| D61 | D61.CUTPART.PLATE | `` |
| D62 | D62.CUTPART.PLATE | `P11` |
| D63 | D63.CUTPART.PLATE | `P11` |
| D64 | D64.CUTPART.GIRDER | `` |
| D65 | D65.CUTPART.GIRDER | `` |
| D66 | D66.CUTPART.GIRDER | `` |
| D67 | D67.CUTPART.GIRDER | `` |
| D68 | D68.CUTPART.GIRDER | `` |
| D69 | D69.CUTPART.GIRDER | `` |
| D70 | D70.CUTPART.GIRDER | `` |
| D71 | D71.CUTPART.GIRDER | `` |
| D72 | D72.CUTPART.GIRDER | `` |
| D73 | D73.CUTPART.GIRDER | `` |
| D74 | D74.CUTPART.GIRDER | `` |
| D75 | D75.CUTPART.GIRDER | `` |
| D76 | D76.CUTPART.GIRDER | `` |
| D77 | D77.CUTPART.GIRDER | `` |
| D78 | D78.CUTPART.GIRDER | `` |
| D79 | D79.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")+P11+.3` |
| D80 | D80.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")+P11+.3` |
| D81 | D81.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")+.3` |
| D82 | D82.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")+.3` |
| D83 | D83.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")-P10` |
| D84 | D84.CUTPART.PLATE | `` |
| D85 | D85.CUTPART.PLATE | `P11` |
| D86 | D86.CUTPART.PLATE | `P9` |
| D87 | D87.CUTPART.PLATE | `P9` |
| D88 | D88.CUTPART.PLATE | `` |
| D89 | D89.CUTPART.PLATE | `P9+fP(proT,"ID66430C93-0000-7F8F-3137-313536373131")-P10+5` |
| D90 | D90.CUTPART.PLATE | `P11` |
| D91 | D91.CUTPART.GIRDER | `` |
| D92 | D92.CUTPART.GIRDER | `` |
| D93 | D93.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")+.3` |
| D94 | D94.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")+P11+.3` |
| D95 | D95.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")+P11+.3` |
| D96 | D96.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")-P10` |
| D97 | D97.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")+.3` |
| D98 | D98.CUTPART.PLATE | `` |
| D99 | D99.CUTPART.PLATE | `P9+fP(proT,"ID66430C93-0000-7F93-3137-313536373131")-P10+5` |
| D100 | D100.CUTPART.PLATE | `P9` |
| D101 | D101.CUTPART.PLATE | `P9` |
| D102 | D102.CUTPART.PLATE | `` |
| D103 | D103.CUTPART.PLATE | `P11` |
| D104 | D104.CUTPART.PLATE | `P11` |
| D105 | D105.CUTPART.GIRDER | `` |
| D106 | D106.CUTPART.GIRDER | `` |
| D107 | D107.CUTPART.GIRDER | `` |
| D108 | D108.CUTPART.GIRDER | `` |
| D109 | D109.CUTPART.GIRDER | `` |
| D110 | D110.CUTPART.GIRDER | `` |
| D111 | D111.CUTPART.GIRDER | `` |
| D112 | D112.PLATE.GIRDER | `` |
| D113 | D113.PLATE.GIRDER | `` |
| D114 | D114.PLATE.GIRDER | `` |
| D115 | D115.CUTPART.GIRDER | `` |
| D116 | D116.CUTPART.GIRDER | `` |
| D117 | D117.CUTPART.GIRDER | `` |
| D118 | D118.CUTPART.GIRDER | `` |
| D119 | D119.CUTPART.GIRDER | `` |
| D120 | D120.CUTPART.GIRDER | `` |
| D121 | D121.CUTPART.GIRDER | `` |
| D122 | D122.CUTPART.GIRDER | `` |
| D123 | D123.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")+.3` |
| D124 | D124.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")+P11+.3` |
| D125 | D125.CUTPART.GIRDER | `` |
| D126 | D126.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")-P10` |
| D127 | D127.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")+.3` |
| D128 | D128.CUTPART.GIRDER | `fP(proT,"ID66430C93-0000-7F93-3137-313536373131")+P11+.3` |
| D129 | D129.CUTPART.PLATE | `` |
| D130 | D130.CUTPART.PLATE | `P11` |
| D131 | D131.CUTPART.PLATE | `P11` |
| D132 | D132.CUTPART.PLATE | `` |
| D133 | D133.CUTPART.PLATE | `P9+fP(proT,"ID66430C93-0000-7F93-3137-313536373131")-P10+5` |
| D134 | D134.CUTPART.PLATE | `P9` |
| D135 | D135.CUTPART.PLATE | `P9` |
| D136 | D136.FLAT_BAR.GIRDER | `` |
| D137 | D137.PLATE.GIRDER | `` |
| D138 | D138.PLATE.GIRDER | `` |
| D139 | D139.PLATE.GIRDER | `` |
| D140 | D140.PLATE.GIRDER | `` |
| D141 | D141.PLATE.GIRDER | `` |
| D142 | D142.PLATE.GIRDER | `` |
| D143 | D143.PLATE.GIRDER | `` |
| D144 | D144.PLATE.GIRDER | `` |
| D145 | D145.PLATE.GIRDER | `` |
| D146 | D146.PLATE.GIRDER | `` |
| D147 | D147.PLATE.GIRDER | `` |
| D148 | D148.PLATE.GIRDER | `` |
| D149 | D149.PLATE.PLATE | `` |
| D150 | D150.PLATE.PLATE | `P26` |
| D151 | D151.PLATE.PLATE | `P26` |
| D152 | D152.PLATE.PLATE | `` |
| D153 | D153.PLATE.PLATE | `` |
| D154 | D154.PLATE.PLATE | `P25` |
| D155 | D155.PLATE.PLATE | `P25` |
| D156 | D156.PLATE.PLATE | `` |
| D157 | D157.PLATE.GIRDER | `` |
| D158 | D158.PLATE.GIRDER | `` |
| D159 | D159.PLATE.GIRDER | `` |
| D160 | D160.PLATE.GIRDER | `` |
| D161 | D161.PLATE.GIRDER | `` |
| D162 | D162.PLATE.GIRDER | `` |
| D163 | D163.PLATE.GIRDER | `` |
| D164 | D164.PLATE.GIRDER | `` |
| D165 | D165.PLATE.EPPLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D166 | D166.PLATE.EPPLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D167 | D167.PLATE.EPPLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D168 | D168.PLATE.EPPLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D169 | D169.PLATE.EPPLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D170 | D170.PLATE.EPPLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D171 | D171.PLATE.EPPLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D172 | D172.PLATE.EPPLATE | `(fP(proH,"ID66430C93-0000-7F8F-3137-313536373131")-fP(proH,"ID66430C93-0000-7F93-3137-313536373131"))-(fP(proT,"ID66430C93-0000-7F93-3137-313536373131")/cos(d45))` |
| D173 | D173.PLATE.GIRDER | `` |
| D174 | D174.PLATE.GIRDER | `` |
| D175 | D175.PLATE.GIRDER | `` |
| D176 | D176.PLATE.GIRDER | `` |
| D177 | D177.PLATE.GIRDER | `` |
| D178 | D178.PLATE.GIRDER | `` |
| D179 | D179.PLATE.GIRDER | `` |
| D180 | D180.PLATE.GIRDER | `` |
| D181 | D181.PLATE.GIRDER | `` |
| D182 | D182.PLATE.GIRDER | `` |
| D183 | D183.PLATE.GIRDER | `` |
| D184 | D184.PLATE.GIRDER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#186 | CREATION | `P16` |
| obj#186 | MATERIAL | `mat2` |
| obj#186 | PROFILE | `"PL"+flgthk2` |
| obj#186 | SERIE | `zone+"BF"` |
| obj#186 | STARTNUMBER | `P20` |
| obj#198 | CREATION | `P35` |
| obj#198 | MATERIAL | `mat2` |
| obj#198 | PROFILE | `"PL"+flgthk2` |
| obj#198 | SERIE | `zone+"BF"` |
| obj#198 | STARTNUMBER | `P20` |
| obj#205 | MATERIAL | `mat2` |
| obj#205 | PROFILE | `"PL"+flgthk` |
| obj#205 | SERIE | `zone+"EP"` |
| obj#205 | STARTNUMBER | `P24` |
| obj#216 | MATERIAL | `P7` |
| obj#216 | PROFILE | `P6` |
| obj#216 | SERIE | `zone+"FB"` |
| obj#216 | STARTNUMBER | `P22` |
| obj#227 | MATERIAL | `P7` |
| obj#227 | PROFILE | `P6` |
| obj#227 | SERIE | `zone+"FB"` |
| obj#227 | STARTNUMBER | `P22` |
| obj#234 | MATERIAL | `P7` |
| obj#234 | PROFILE | `P6` |
| obj#234 | SERIE | `zone+"FB"` |
| obj#234 | STARTNUMBER | `P22` |
| obj#242 | MATERIAL | `P7` |
| obj#242 | PROFILE | `P6` |
| obj#242 | SERIE | `zone+"FB"` |
| obj#242 | STARTNUMBER | `P22` |
| obj#249 | CREATION | `P15` |
| obj#249 | MATERIAL | `mat2` |
| obj#249 | PROFILE | `"PL"+flgthk` |
| obj#249 | SERIE | `zone+"SF"` |
| obj#249 | STARTNUMBER | `P20` |
| obj#259 | CREATION | `P14` |
| obj#259 | MATERIAL | `mat2` |
| obj#259 | PROFILE | `"PL"+webthk` |
| obj#259 | SERIE | `zone+"WP"` |
| obj#259 | STARTNUMBER | `P20` |
| obj#270 | MATERIAL | `mat2` |
| obj#270 | PROFILE | `"PL"+flgthk` |
| obj#270 | SERIE | `zone+"SF"` |
| obj#279 | MATERIAL | `mat2` |
| obj#279 | PROFILE | `"PL"+flgthk` |
| obj#279 | SERIE | `zone+"SF"` |
| obj#288 | PROFILE | `fP(proB,"ID66430C93-0000-7F8F-3137-313536373131")+20` |
| obj#301 | PROFILE | `fP(proB,"ID66430C93-0000-7F8F-3137-313536373131")+20` |
| obj#311 | PROFILE | `fP(proB,"ID66430C93-0000-7F93-3137-313536373131")+20` |
| obj#322 | PROFILE | `fP(proB,"ID66430C93-0000-7F93-3137-313536373131")+20` |