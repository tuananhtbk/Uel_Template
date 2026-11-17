# 0-DM-WALE+T_GIRDER

- File: `WALE+T_GIRDER.uel`  (43,164 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	0-DM-WALE+T_GIRDER	All`
- Dialog parameters: 0 | Variables (P/custom): 52 | Distance vars (D): 267 | Parts with bound profile: 11
- Lookup files (fVF): -
- Functions used: cos, fAD, fP, if, sin, tan
- Dialog bitmaps: 0 | Saved presets: #KK.p_0-DM-WALE+T_GIRDER, #KK1.p_0-DM-WALE+T_GIRDER, 0-H582.p_0-DM-WALE+T_GIRDER, 00-H300x300x10x15.p_0-DM-WALE+T_GIRDER, 00-H350x350x12x19.p_0-DM-WALE+T_GIRDER, 1.p_0-DM-WALE+T_GIRDER, 2.p_0-DM-WALE+T_GIRDER, BH1200.p_0-DM-WALE+T_GIRDER

## Dialog (.inp)
```
page("TeklaStructures", "")
{
    joint(1, "0-DM-WALE+T_GIRDER")
    {
        tab_page("", " Parameters 1 ", 1)
        {
            parameter("", "P5", string, text, 322, 24, 160)
            attribute("", "Base Plate Mark", label, "%s", none, none, "0", "0", 57, 24)
            attribute("", "FSF-Mark", label, "%s", none, none, "0", "0", 536, 29)
            parameter("", "P1", string, text, 721, 30, 160)
            attribute("", "Thk.", label, "%s", none, none, "0", "0", 58, 57)
            attribute("", "FSF-Thk.", label, "%s", none, none, "0", "0", 536, 57)
            parameter("", "P29", distance, number, 321, 58, 160)
            parameter("", "P2", distance, number, 721, 58, 160)
            attribute("", "Mat.", label, "%s", none, none, "0", "0", 58, 85)
            parameter("", "P30", material, text, 321, 87, 160)
            parameter("", "P31", material, text, 722, 92, 160)
            attribute("", "FSFM", label, "%s", none, none, "0", "0", 548, 94)
            parameter("", "P36", distance, number, 725, 127, 160)
            parameter("", "P21", distance, number, 322, 129, 160)
            attribute("", "T", label, "%s", none, none, "0", "0", 217, 131)
            attribute("", "V", label, "%s", none, none, "0", "0", 568, 131)
            parameter("", "P37", distance, number, 725, 155, 160)
            parameter("", "P22", distance, number, 322, 157, 160)
            attribute("", "B", label, "%s", none, none, "0", "0", 217, 160)
            attribute("", "h", label, "%s", none, none, "0", "0", 568, 160)
            parameter("", "P23", distance, number, 322, 185, 160)
            attribute("", "L", label, "%s", none, none, "0", "0", 217, 188)
            parameter("", "P3", string, text, 781, 231, 160)
            attribute("", "Stiff Plate Mark", label, "%s", none, none, "0", "0", 548, 232)
            parameter("", "P24", distance, number, 322, 214, 160)
            attribute("", "R", label, "%s", none, none, "0", "0", 217, 216)
            attribute("", "Stiff Plate Thk", label, "%s", none, none, "0", "0", 548, 260)
            parameter("", "P4", distance, number, 782, 263, 160)
            parameter("", "P34", material, text, 784, 292, 160)
            attribute("", "Mat.", label, "%s", none, none, "0", "0", 570, 294)
            attribute("", "Flat_Bar Mark", label, "%s", none, none, "0", "0", 62, 287)
            parameter("", "P9", string, text, 325, 288, 160)
            attribute("", "Mat.", label, "%s", none, none, "0", "0", 64, 322)
            parameter("", "P33", material, text, 326, 323, 160)
            attribute("", "H", label, "%s", none, none, "0", "0", 580, 438)
            parameter("", "P14", distance, number, 732, 432, 160)
            attribute("", "Rip Plate Mark", label, "%s", none, none, "0", "0", 552, 332)
            parameter("", "P7", string, text, 729, 332, 160)
            attribute("", "h", label, "%s", none, none, "0", "0", 580, 467)
            parameter("", "P15", distance, number, 732, 461, 160)
            attribute("", "Rip Plate Thk", label, "%s", none, none, "0", "0", 552, 361)
            parameter("", "P8", distance, number, 729, 361, 160)
            attribute("", "Y/N", label, "%s", none, none, "0", "0", 64, 358)
            parameter("", "P10", YesNo, number, 326, 360, 160)
            attribute("", "RIP_1(Y/N)", label, "%s", none, none, "0", "0", 546, 538)
            parameter("", "P16", YesNo, number, 729, 534, 160)
            attribute("", "RIP_2(Y/N)", label, "%s", none, none, "0", "0", 548, 572)
            parameter("", "P17", YesNo, number, 732, 571, 160)
            attribute("", "W_GAP", label, "%s", none, none, "0", "0", 66, 408)
            parameter("", "P26", integer, number, 329, 409, 160)
            attribute("", "W-Angle", label, "%s", none, none, "0", "0", 66, 435)
            parameter("", "P27", angle, number, 329, 436, 160)
            attribute("", "Rip PL to CL", label, "%s", none, none, "0", "0", 542, 501)
            parameter("", "P35", distance, number, 730, 496, 160)
            attribute("", "R", label, "%s", none, none, "0", "0", 66, 475)
            parameter("", "P38", YesNo, number, 329, 476, 160)
            attribute("", "W", label, "%s", none, none, "0", "0", 66, 503)
            parameter("", "P39", YesNo, number, 329, 504, 160)
            attribute("", "SF-Left (Y/N)", label, "%s", none, none, "0", "0", 57, 560)
            parameter("", "P40", YesNo, number, 322, 563, 160)
            attribute("", "SF-Right (Y/N)", label, "%s", none, none, "0", "0", 57, 588)
            parameter("", "P41", YesNo, number, 322, 591, 160)
            attribute("", "SF-CTR'D (Y/N)", label, "%s", none, none, "0", "0", 57, 616)
            parameter("", "P42", YesNo, number, 322, 620, 160)
            attribute("", "SF-Top", label, "%s", none, none, "0", "0", 58, 658)
            parameter("", "P43", distance, number, 324, 662, 160)
            attribute("", "RPM", label, "%s", none, none, "0", "0", 560, 398)
            parameter("", "P32", material, text, 729, 396, 160)
            attribute("", "SF-Bott.", label, "%s", none, none, "0", "0", 58, 687)
            parameter("", "P44", distance, number, 324, 690, 160)
            attribute("", "Rip Left Cut 1 (Y/N) ", label, "%s", none, none, "0", "0", 529, 616)
            parameter("", "P45", YesNo, number, 786, 612, 160)
            attribute("", "Rip Left Cut 2 (Y/N) ", label, "%s", none, none, "0", "0", 529, 644)
            parameter("", "P46", YesNo, number, 786, 641, 160)
            attribute("", "Rip Right Cut 1 (Y/N) ", label, "%s", none, none, "0", "0", 529, 672)
            parameter("", "P47", YesNo, number, 786, 669, 160)
            attribute("", "Rip Right Cut 2 (Y/N) ", label, "%s", none, none, "0", "0", 529, 700)
            parameter("", "P48", YesNo, number, 786, 696, 160)
            attribute("", "Gap Cutting", label, "%s", none, none, "0", "0", 566, 732)
            parameter("", "P49", distance, number, 730, 735, 160)
            attribute("", "Angle Cutting", label, "%s", none, none, "0", "0", 566, 761)
            parameter("", "P50", angle, number, 730, 763, 160)
            attribute("", "WALE CUTTING GAP", label, "%s", none, none, "0", "0", 58, 742)
            parameter("", "P51", distance, number, 324, 745, 160)
            attribute("", "FSF (Y/N)", label, "%s", none, none, "0", "0", 546, 196)
            parameter("", "P52", YesNo, number, 728, 189, 160)
        }
        tab_page("", " Parameters 2 ", 2)
        {
        }
    }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | FSF-Mark | Text | `0.000` |
| P2 | FSF-Thk. | Length | `15.000` |
| P3 | Stiff Plate Mark | Text | `SF` |
| P4 | Stiff Plate Thk | Length | `20.000` |
| P5 | Base Plate Mark | Text | `BP` |
| P6 | Base Plate Thk | Length | `if (P29!=0) then P29 else 0.01 endif` *(formula)* |
| P7 | Rip Plate Mark | Text | `RP` |
| P8 | Rip Plate Thk | Length | `15.00` |
| P9 | Flat_Bar Mark | Text | `B5-FB` |
| P10 | Flat_Bar(Y/N) | Yes/No | `0` |
| P11 | Component | Length | `fP(proT,"ID651B600B-0000-8D30-3136-393633303630")` *(formula)* |
| P12 | Angle | Angle | `fAD("slope","ID651B600B-0000-8D30-3136-393633303630")` *(formula)* |
| P13 | Parameter13 | Length | `cos(P12)` *(formula)* |
| P14 | Rip Plate Length (H) | Length | `300.00` |
| P15 | Rip Plate Length (h) | Length | `150.00` |
| P16 | RIP_1(Y/N) | Yes/No | `0` |
| P17 | RIP_2(Y/N) | Yes/No | `0` |
| P18 | MAT-T BEAM | Material | `fP(proMATERIAL,"ID651B600B-0000-8D30-3136-393633303630")` *(formula)* |
| P19 | MAT-WALE | Material | `fP(proMATERIAL,"ID651B600B-0000-8D2C-3136-393633303630")` *(formula)* |
| P20 | Component Standard | Length | `fP(proB,"ID651B600B-0000-8D30-3136-393633303630")` *(formula)* |
| P21 | TOP | Length | `20.00` |
| P22 | BOTT. | Length | `20.00` |
| P23 | L | Length | `20.00` |
| P24 | R | Length | `20.00` |
| P25 | BASE PLate (Y/N) | Integer | `if (P29==0) then 0 else 1 endif` *(formula)* |
| P26 | W_GAP | Integer | `5` |
| P27 | W-Angle | Angle | `45.0000` |
| P28 | BASE HIDE | Length | `if (P29==0) then 0.01 else P29 endif` *(formula)* |
| P29 | BASE Thk. | Length | `10.00` |
| P30 | BPM | Material | `SS400` |
| P31 | FSFM | Material | `SS400` |
| P32 | RPM | Material | `SS400` |
| P33 | FBM | Material | `SS400` |
| P34 | SFM | Material | `SS400` |
| P35 | Rip PL to CL | Length | `0.00` |
| P36 | V | Length | `120.00` |
| P37 | h | Length | `200.00` |
| P38 | R | Yes/No | `1` |
| P39 | W | Yes/No | `1` |
| P40 | SF-Left (Y/N) | Yes/No | `1` |
| P41 | SF-Right (Y/N) | Yes/No | `1` |
| P42 | SF-CTR'D (Y/N) | Yes/No | `1` |
| P43 | SF-Top | Length | `20.00` |
| P44 | SF-Bott. | Length | `20.00` |
| P45 | Rip Left Cut 1 (Y/N)  | Yes/No | `1` |
| P46 | Rip Left Cut 2 (Y/N)  | Yes/No | `1` |
| P47 | Rip Right Cut 1 (Y/N)  | Yes/No | `1` |
| P48 | Rip Right Cut 2 (Y/N)  | Yes/No | `1` |
| P49 | Gap Cutting | Length | `5.00` |
| P50 | Angle Cutting | Angle | `45.0000` |
| P51 | WALE CUTTING GAP | Length | `2.00` |
| P52 | FSF (Y/N) | Yes/No | `1` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.BEAM | `` |
| D2 | D2.PLATE.BEAM | `` |
| D3 | D3.PLATE.BEAM | `` |
| D4 | D4.PLATE.BEAM | `` |
| D5 | D5.PLATE.BEAM | `` |
| D6 | D6.PLATE.BEAM | `` |
| D7 | D7.PLATE.BEAM | `` |
| D8 | D8.PLATE.BEAM | `` |
| D9 | D9.PLATE.BEAM | `` |
| D10 | D10.PLATE.BEAM | `` |
| D11 | D11.PLATE.BEAM | `` |
| D12 | D12.PLATE.BEAM | `` |
| D13 | D13.PLATE.BEAM | `` |
| D14 | D14.PLATE.BEAM | `` |
| D15 | D15.PLATE.BEAM | `P36` |
| D16 | D16.PLATE.BEAM | `P36` |
| D17 | D17.PLATE.BEAM | `` |
| D18 | D18.PLATE.BEAM | `` |
| D19 | D19.PLATE.BEAM | `P37*P13` |
| D20 | D20.PLATE.BEAM | `P37*P13` |
| D21 | D21.PLATE.BEAM | `` |
| D22 | D22.PLATE.BEAM | `` |
| D23 | D23.PLATE.BEAM | `50*P13` |
| D24 | D24.PLATE.BEAM | `50*P13` |
| D25 | D25.PLATE.PLATE | `` |
| D26 | D26.PLATE.PLATE | `` |
| D27 | D27.PLATE.PLATE | `` |
| D28 | D28.PLATE.PLATE | `` |
| D29 | D29.PLATE.PLATE | `` |
| D30 | D30.PLATE.PLATE | `` |
| D31 | D31.PLATE.PLATE | `` |
| D32 | D32.PLATE.PLATE | `` |
| D33 | D33.PLATE.BEAM | `-P43` |
| D34 | D34.PLATE.BEAM | `-P43` |
| D35 | D35.PLATE.BEAM | `` |
| D36 | D36.PLATE.BEAM | `` |
| D37 | D37.PLATE.BEAM | `` |
| D38 | D38.PLATE.BEAM | `` |
| D39 | D39.PLATE.BEAM | `-P44` |
| D40 | D40.PLATE.BEAM | `-P44` |
| D41 | D41.PLATE.BEAM | `` |
| D42 | D42.PLATE.BEAM | `` |
| D43 | D43.PLATE.BEAM | `` |
| D44 | D44.PLATE.BEAM | `` |
| D45 | D45.PLATE.BEAM | `` |
| D46 | D46.PLATE.BEAM | `` |
| D47 | D47.PLATE.BEAM | `` |
| D48 | D48.PLATE.BEAM | `` |
| D49 | D49.PLATE.PLATE | `` |
| D50 | D50.PLATE.PLATE | `` |
| D51 | D51.PLATE.PLATE | `` |
| D52 | D52.PLATE.PLATE | `` |
| D53 | D53.PLATE.PLATE | `` |
| D54 | D54.PLATE.PLATE | `` |
| D55 | D55.PLATE.PLATE | `` |
| D56 | D56.PLATE.PLATE | `` |
| D57 | D57.PLATE.BEAM | `-P43` |
| D58 | D58.PLATE.BEAM | `-P43` |
| D59 | D59.PLATE.BEAM | `` |
| D60 | D60.PLATE.BEAM | `` |
| D61 | D61.PLATE.BEAM | `` |
| D62 | D62.PLATE.BEAM | `` |
| D63 | D63.PLATE.BEAM | `-P44` |
| D64 | D64.PLATE.BEAM | `-P44` |
| D65 | D65.PLATE.BEAM | `` |
| D66 | D66.PLATE.BEAM | `` |
| D67 | D67.PLATE.BEAM | `` |
| D68 | D68.PLATE.BEAM | `` |
| D69 | D69.PLATE.BEAM | `` |
| D70 | D70.PLATE.BEAM | `` |
| D71 | D71.PLATE.BEAM | `` |
| D72 | D72.PLATE.BEAM | `` |
| D73 | D73.PLATE.BEAM | `P6` |
| D74 | D74.PLATE.BEAM | `P6` |
| D75 | D75.PLATE.BEAM | `P6` |
| D76 | D76.PLATE.BEAM | `P6` |
| D77 | D77.PLATE.BEAM | `` |
| D78 | D78.PLATE.BEAM | `` |
| D79 | D79.PLATE.BEAM | `` |
| D80 | D80.PLATE.BEAM | `` |
| D81 | D81.PLATE.BEAM | `P6+P26` |
| D82 | D82.PLATE.BEAM | `P6+P26` |
| D83 | D83.PLATE.BEAM | `P6+P26` |
| D84 | D84.PLATE.BEAM | `P6+P26` |
| D85 | D85.PLATE.PLATE | `` |
| D86 | D86.PLATE.PLATE | `` |
| D87 | D87.PLATE.PLATE | `` |
| D88 | D88.PLATE.PLATE | `` |
| D89 | D89.PLATE.BEAM | `` |
| D90 | D90.PLATE.BEAM | `` |
| D91 | D91.PLATE.BEAM | `` |
| D92 | D92.PLATE.BEAM | `` |
| D93 | D93.PLATE.BEAM | `P35` |
| D94 | D94.PLATE.BEAM | `P35` |
| D95 | D95.PLATE.BEAM | `P35` |
| D96 | D96.PLATE.BEAM | `P35` |
| D97 | D97.PLATE.BEAM | `P35` |
| D98 | D98.PLATE.BEAM | `P35` |
| D99 | D99.PLATE.BEAM | `P35` |
| D100 | D100.PLATE.BEAM | `P35` |
| D101 | D101.PLATE.BEAM | `P35` |
| D102 | D102.PLATE.BEAM | `P35` |
| D103 | D103.PLATE.BEAM | `` |
| D104 | D104.PLATE.BEAM | `` |
| D105 | D105.PLATE.BEAM | `` |
| D106 | D106.PLATE.BEAM | `` |
| D107 | D107.PLATE.BEAM | `` |
| D108 | D108.PLATE.BEAM | `` |
| D109 | D109.PLATE.BEAM | `` |
| D110 | D110.PLATE.BEAM | `` |
| D111 | D111.PLATE.BEAM | `` |
| D112 | D112.PLATE.BEAM | `` |
| D113 | D113.BEAM.BEAM | `` |
| D114 | D114.BEAM.BEAM | `` |
| D115 | D115.BEAM.BEAM | `` |
| D116 | D116.BEAM.BEAM | `` |
| D117 | D117.PLATE.Plane | `` |
| D118 | D118.PLATE.Plane | `` |
| D119 | D119.PLATE.Plane | `` |
| D120 | D120.PLATE.Plane | `` |
| D121 | D121.PLATE.BEAM | `` |
| D122 | D122.PLATE.BEAM | `` |
| D123 | D123.PLATE.BEAM | `` |
| D124 | D124.PLATE.BEAM | `` |
| D125 | D125.Plane.BEAM | `P6` |
| D126 | D126.BEAM.PLATE | `0.5*P20*sin(P12)` |
| D127 | D127.BEAM.PLATE | `0.5*P20*sin(P12)` |
| D128 | D128.PLATE.BEAM | `P14` |
| D129 | D129.PLATE.BEAM | `P14` |
| D130 | D130.PLATE.BEAM | `P14` |
| D131 | D131.PLATE.BEAM | `P14` |
| D132 | D132.PLATE.BEAM | `P15` |
| D133 | D133.PLATE.BEAM | `0.5*P6` |
| D134 | D134.PLATE.BEAM | `0.5*P6` |
| D135 | D135.PLATE.BEAM | `0.5*P6` |
| D136 | D136.PLATE.BEAM | `0.5*P6` |
| D137 | D137.PLATE.BEAM | `P22` |
| D138 | D138.PLATE.BEAM | `P21` |
| D139 | D139.PLATE.BEAM | `P21` |
| D140 | D140.PLATE.BEAM | `P22` |
| D141 | D141.PLATE.PLATE | `P23` |
| D142 | D142.PLATE.PLATE | `P23` |
| D143 | D143.PLATE.PLATE | `P24` |
| D144 | D144.PLATE.PLATE | `-P24` |
| D145 | D145.Plane.BEAM | `P6` |
| D146 | D146.Plane.BEAM | `P6` |
| D147 | D147.Plane.BEAM | `` |
| D148 | D148.Plane.BEAM | `` |
| D149 | D149.Plane.BEAM | `` |
| D150 | D150.PLATE.BEAM | `P15` |
| D151 | D151.CUTPART.Plane | `P6` |
| D152 | D152.CUTPART.Plane | `35` |
| D153 | D153.CUTPART.Plane | `35` |
| D154 | D154.FITTING.BEAM | `P6` |
| D155 | D155.FITTING.BEAM | `P6` |
| D156 | D156.FITTING.BEAM | `P6` |
| D157 | D157.CUTPART.PLATE | `` |
| D158 | D158.CUTPART.PLATE | `` |
| D159 | D159.CUTPART.PLATE | `` |
| D160 | D160.CUTPART.PLATE | `` |
| D161 | D161.CUTPART.PLATE | `` |
| D162 | D162.CUTPART.BEAM | `` |
| D163 | D163.CUTPART.BEAM | `` |
| D164 | D164.CUTPART.BEAM | `` |
| D165 | D165.CUTPART.BEAM | `` |
| D166 | D166.CUTPART.BEAM | `` |
| D167 | D167.CUTPART.BEAM | `P6+P26` |
| D168 | D168.CUTPART.BEAM | `P6` |
| D169 | D169.CUTPART.BEAM | `P6` |
| D170 | D170.CUTPART.BEAM | `(P11-2)*tan(P27)+P26+P6` |
| D171 | D171.CUTPART.BEAM | `P6+P26` |
| D172 | D172.CUTPART.PLATE | `` |
| D173 | D173.CUTPART.PLATE | `` |
| D174 | D174.CUTPART.PLATE | `` |
| D175 | D175.CUTPART.Plane | `P6` |
| D176 | D176.CUTPART.BEAM | `` |
| D177 | D177.CUTPART.BEAM | `` |
| D178 | D178.CUTPART.BEAM | `` |
| D179 | D179.CUTPART.BEAM | `` |
| D180 | D180.CUTPART.PLATE | `` |
| D181 | D181.CUTPART.PLATE | `` |
| D182 | D182.CUTPART.PLATE | `` |
| D183 | D183.CUTPART.PLATE | `` |
| D184 | D184.CUTPART.BEAM | `` |
| D185 | D185.CUTPART.BEAM | `` |
| D186 | D186.CUTPART.BEAM | `` |
| D187 | D187.CUTPART.BEAM | `` |
| D188 | D188.CUTPART.Plane | `P6` |
| D189 | D189.CUTPART.Plane | `` |
| D190 | D190.CUTPART.Plane | `` |
| D191 | D191.CUTPART.Plane | `P6` |
| D192 | D192.CUTPART.PLATE | `` |
| D193 | D193.CUTPART.PLATE | `` |
| D194 | D194.CUTPART.PLATE | `` |
| D195 | D195.CUTPART.PLATE | `` |
| D196 | D196.CUTPART.PLATE | `` |
| D197 | D197.CUTPART.PLATE | `` |
| D198 | D198.CUTPART.BEAM | `` |
| D199 | D199.CUTPART.BEAM | `` |
| D200 | D200.CUTPART.BEAM | `P51` |
| D201 | D201.CUTPART.BEAM | `` |
| D202 | D202.CUTPART.BEAM | `` |
| D203 | D203.CUTPART.BEAM | `P6+P26` |
| D204 | D204.CUTPART.BEAM | `P6` |
| D205 | D205.CUTPART.BEAM | `P6` |
| D206 | D206.CUTPART.BEAM | `(P11-2)*tan(P27)+P26+P6` |
| D207 | D207.CUTPART.BEAM | `P6+P26` |
| D208 | D208.PLATE.BEAM | `` |
| D209 | D209.PLATE.BEAM | `` |
| D210 | D210.PLATE.BEAM | `` |
| D211 | D211.PLATE.BEAM | `` |
| D212 | D212.PLATE.BEAM | `` |
| D213 | D213.PLATE.BEAM | `` |
| D214 | D214.PLATE.BEAM | `` |
| D215 | D215.PLATE.BEAM | `` |
| D216 | D216.PLATE.BEAM | `P43` |
| D217 | D217.PLATE.BEAM | `P43` |
| D218 | D218.PLATE.BEAM | `` |
| D219 | D219.PLATE.BEAM | `` |
| D220 | D220.PLATE.BEAM | `P44` |
| D221 | D221.PLATE.BEAM | `P44` |
| D222 | D222.PLATE.BEAM | `` |
| D223 | D223.PLATE.BEAM | `` |
| D224 | D224.PLATE.PLATE | `` |
| D225 | D225.PLATE.PLATE | `` |
| D226 | D226.PLATE.PLATE | `` |
| D227 | D227.PLATE.PLATE | `` |
| D228 | D228.PLATE.PLATE | `` |
| D229 | D229.PLATE.PLATE | `` |
| D230 | D230.PLATE.PLATE | `` |
| D231 | D231.PLATE.PLATE | `` |
| D232 | D232.CUTPART.PLATE | `` |
| D233 | D233.CUTPART.PLATE | `` |
| D234 | D234.CUTPART.PLATE | `` |
| D235 | D235.CUTPART.PLATE | `P49+100` |
| D236 | D236.CUTPART.PLATE | `P49+100` |
| D237 | D237.CUTPART.PLATE | `P49` |
| D238 | D238.CUTPART.PLATE | `` |
| D239 | D239.CUTPART.PLATE | `` |
| D240 | D240.CUTPART.PLATE | `` |
| D241 | D241.CUTPART.PLATE | `100*tan(P50)` |
| D242 | D242.CUTPART.PLATE | `` |
| D243 | D243.CUTPART.PLATE | `` |
| D244 | D244.CUTPART.PLATE | `P49+100` |
| D245 | D245.CUTPART.PLATE | `P49+100` |
| D246 | D246.CUTPART.PLATE | `P49` |
| D247 | D247.CUTPART.PLATE | `100*tan(P50)` |
| D248 | D248.CUTPART.PLATE | `` |
| D249 | D249.CUTPART.PLATE | `` |
| D250 | D250.CUTPART.PLATE | `` |
| D251 | D251.CUTPART.PLATE | `` |
| D252 | D252.CUTPART.PLATE | `` |
| D253 | D253.CUTPART.PLATE | `P49+100` |
| D254 | D254.CUTPART.PLATE | `P49+100` |
| D255 | D255.CUTPART.PLATE | `P49` |
| D256 | D256.CUTPART.PLATE | `100*tan(P50)` |
| D257 | D257.CUTPART.PLATE | `` |
| D258 | D258.CUTPART.PLATE | `` |
| D259 | D259.CUTPART.PLATE | `` |
| D260 | D260.CUTPART.PLATE | `` |
| D261 | D261.CUTPART.PLATE | `` |
| D262 | D262.CUTPART.PLATE | `100*tan(P50)` |
| D263 | D263.CUTPART.PLATE | `` |
| D264 | D264.CUTPART.PLATE | `` |
| D265 | D265.CUTPART.PLATE | `P49+100` |
| D266 | D266.CUTPART.PLATE | `P49+100` |
| D267 | D267.CUTPART.PLATE | `P49` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#212 | CREATION | `0` |
| obj#233 | CREATION | `P42` |
| obj#233 | MATERIAL | `P34` |
| obj#233 | PROFILE | `"PL"+P4` |
| obj#233 | SERIE | `P3` |
| obj#245 | CREATION | `P42` |
| obj#245 | MATERIAL | `P34` |
| obj#245 | PROFILE | `"PL"+P4` |
| obj#245 | SERIE | `P3` |
| obj#252 | CREATION | `P25` |
| obj#252 | MATERIAL | `P30` |
| obj#252 | PROFILE | `"PL"+P6` |
| obj#252 | SERIE | `P5` |
| obj#263 | CREATION | `0` |
| obj#263 | MATERIAL | `P18` |
| obj#263 | PROFILE | `"PL"+P28` |
| obj#263 | SERIE | `P5` |
| obj#272 | CREATION | `P41` |
| obj#272 | MATERIAL | `P34` |
| obj#272 | PROFILE | `"PL"+P4` |
| obj#272 | SERIE | `P3` |
| obj#279 | CREATION | `P41` |
| obj#279 | MATERIAL | `P34` |
| obj#279 | PROFILE | `"PL"+P4` |
| obj#279 | SERIE | `P3` |
| obj#285 | CREATION | `P40` |
| obj#285 | MATERIAL | `P34` |
| obj#285 | PROFILE | `"PL"+P4` |
| obj#285 | SERIE | `P3` |
| obj#292 | CREATION | `P40` |
| obj#292 | MATERIAL | `P34` |
| obj#292 | PROFILE | `"PL"+P4` |
| obj#292 | SERIE | `P3` |
| obj#298 | CREATION | `P52` |
| obj#298 | MATERIAL | `P31` |
| obj#298 | PROFILE | `"PL"+P2` |
| obj#298 | SERIE | `P1` |
| obj#308 | CREATION | `P17` |
| obj#308 | MATERIAL | `P32` |
| obj#308 | PROFILE | `"PL"+P8` |
| obj#308 | SERIE | `P7` |
| obj#318 | CREATION | `P16` |
| obj#318 | MATERIAL | `P32` |
| obj#318 | PROFILE | `"PL"+P8` |
| obj#318 | SERIE | `P7` |
| obj#326 | CREATION | `P10` |
| obj#326 | MATERIAL | `P33` |
| obj#326 | SERIE | `P9` |
| obj#336 | CREATION | `P10` |
| obj#336 | MATERIAL | `P33` |
| obj#336 | SERIE | `P9` |
| obj#349 | CREATION | `P39` |
| obj#362 | CREATION | `P38` |
| obj#372 | CREATION | `P38` |
| obj#381 | CREATION | `P39` |
| obj#390 | CREATION | `P48` |
| obj#402 | CREATION | `P47` |
| obj#413 | CREATION | `P45` |
| obj#422 | CREATION | `P46` |

## Preset `#KK.p_0-DM-WALE+T_GIRDER`
```
joint_attributes.saveas_file "#KK"
joint_attributes.get_menu "#KK1"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P39 1
joint_attributes.P38 1
joint_attributes.P35 -100.000000
joint_attributes.P37 200.000000
joint_attributes.P36 200.000000
joint_attributes.P34 "SM355"
joint_attributes.P33 "SS275"
joint_attributes.P32 "SM355"
joint_attributes.P31 "SM355"
joint_attributes.P30 "SM355"
joint_attributes.P29 0.000000
joint_attributes.P27 45.000000
joint_attributes.P26 5
joint_attributes.P24 20.000000
joint_attributes.P23 20.000000
joint_attributes.P22 20.000000
joint_attributes.P21 20.000000
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 200.000000
joint_attributes.P14 500.000000
joint_attributes.P10 1
joint_attributes.P9 "1FB"
joint_attributes.P8 -2147483648.000000
joint_attributes.P7 ""
joint_attributes.P5 ""
joint_attributes.P4 18.000000
joint_attributes.P3 "1SF"
joint_attributes.P2 12.000000
joint_attributes.P1 "1SF"
```

## Preset `#KK1.p_0-DM-WALE+T_GIRDER`
```
joint_attributes.saveas_file "#KK1"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P51 5.000000
joint_attributes.P50 35.000000
joint_attributes.P49 5.000000
joint_attributes.P48 1
joint_attributes.P47 1
joint_attributes.P46 1
joint_attributes.P45 1
joint_attributes.P44 10.000000
joint_attributes.P32 "SM355"
joint_attributes.P43 10.000000
joint_attributes.P42 1
joint_attributes.P41 0
joint_attributes.P40 0
joint_attributes.P39 1
joint_attributes.P38 1
joint_attributes.P35 -100.000000
joint_attributes.P27 45.000000
joint_attributes.P26 5
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P10 1
joint_attributes.P15 200.000000
joint_attributes.P14 500.000000
joint_attributes.P33 "SS275"
joint_attributes.P9 "B5-FB"
joint_attributes.P34 "SM355"
joint_attributes.P4 18.000000
joint_attributes.P24 20.000000
joint_attributes.P3 "B5-SF"
joint_attributes.P23 20.000000
joint_attributes.P22 20.000000
joint_attributes.P37 200.000000
joint_attributes.P21 20.000000
joint_attributes.P36 200.000000
joint_attributes.P31 "SM355"
joint_attributes.P30 "SM355"
joint_attributes.P2 12.000000
joint_attributes.P29 0.000000
joint_attributes.P1 "B5-FSF"
```

## Preset `0-H582.p_0-DM-WALE+T_GIRDER`
```
joint_attributes.saveas_file "0-H582"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P39 1
joint_attributes.P38 1
joint_attributes.P35 0.000000
joint_attributes.P37 200.000000
joint_attributes.P36 120.000000
joint_attributes.P34 "SM355"
joint_attributes.P33 "SS275"
joint_attributes.P32 "SM355"
joint_attributes.P31 "SS275"
joint_attributes.P30 "SM355"
joint_attributes.P29 17.000000
joint_attributes.P26 5
joint_attributes.P24 20.000000
joint_attributes.P23 20.000000
joint_attributes.P22 0.000000
joint_attributes.P21 20.000000
joint_attributes.P17 1
joint_attributes.P16 1
joint_attributes.P15 150.000000
joint_attributes.P14 250.000000
joint_attributes.P10 1
joint_attributes.P9 "B1FB"
joint_attributes.P8 15.000000
joint_attributes.P7 "B1RP"
joint_attributes.P5 "B1BP"
joint_attributes.P4 12.000000
joint_attributes.P3 "B1SF"
joint_attributes.P2 12.000000
joint_attributes.P1 "B1LP"
```

## Preset `00-H300x300x10x15.p_0-DM-WALE+T_GIRDER`
```
joint_attributes.saveas_file "00-H300x300x10x15"
joint_attributes.get_menu "00-H350x350x12x19"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P40 1
joint_attributes.P39 1
joint_attributes.P38 1
joint_attributes.P37 200.000000
joint_attributes.P36 120.000000
joint_attributes.P35 0.000000
joint_attributes.P34 "SM355"
joint_attributes.P33 "SS275"
joint_attributes.P32 "SM355"
joint_attributes.P31 "SS275"
joint_attributes.P30 "SM355"
joint_attributes.P29 0.000000
joint_attributes.P26 5
joint_attributes.P24 20.000000
joint_attributes.P23 20.000000
joint_attributes.P22 0.000000
joint_attributes.P21 20.000000
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 150.000000
joint_attributes.P14 250.000000
joint_attributes.P10 1
joint_attributes.P9 "1FB"
joint_attributes.P8 15.000000
joint_attributes.P7 "1RP"
joint_attributes.P5 "1BP"
joint_attributes.P4 12.000000
joint_attributes.P3 "1SF"
joint_attributes.P2 12.000000
joint_attributes.P1 "1LP"
```

## Preset `00-H350x350x12x19.p_0-DM-WALE+T_GIRDER`
```
joint_attributes.saveas_file "00-H350x350x12x19"
joint_attributes.get_menu "00-H350x350x12x19"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P39 1
joint_attributes.P38 1
joint_attributes.P35 0.000000
joint_attributes.P37 200.000000
joint_attributes.P36 120.000000
joint_attributes.P34 "SM355"
joint_attributes.P33 "SS275"
joint_attributes.P32 "SM355"
joint_attributes.P31 "SS275"
joint_attributes.P30 "SM355"
joint_attributes.P29 0.000000
joint_attributes.P26 5
joint_attributes.P24 20.000000
joint_attributes.P23 20.000000
joint_attributes.P22 0.000000
joint_attributes.P21 20.000000
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 150.000000
joint_attributes.P14 250.000000
joint_attributes.P10 1
joint_attributes.P9 "B5FB"
joint_attributes.P8 15.000000
joint_attributes.P7 "B5RP"
joint_attributes.P5 "B5BP"
joint_attributes.P4 12.000000
joint_attributes.P3 "B5SF"
joint_attributes.P2 12.000000
joint_attributes.P1 "B5LP"
```

## Preset `1.p_0-DM-WALE+T_GIRDER`
```
joint_attributes.saveas_file "1"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
```

## Preset `2.p_0-DM-WALE+T_GIRDER`
```
joint_attributes.saveas_file "2"
joint_attributes.get_menu "2"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
```

## Preset `BH1200.p_0-DM-WALE+T_GIRDER`
```
joint_attributes.saveas_file "BH1200"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P39 1
joint_attributes.P38 1
joint_attributes.P37 200.000000
joint_attributes.P36 120.000000
joint_attributes.P34 "SM355"
joint_attributes.P32 "SM355"
joint_attributes.P31 "SS275"
joint_attributes.P30 "SM355"
joint_attributes.P29 30.000000
joint_attributes.P24 20.000000
joint_attributes.P23 20.000000
joint_attributes.P22 20.000000
joint_attributes.P21 20.000000
joint_attributes.P17 1
joint_attributes.P16 1
joint_attributes.P15 150.000000
joint_attributes.P14 250.000000
joint_attributes.P10 0
joint_attributes.P8 15.000000
joint_attributes.P7 "1RP"
joint_attributes.P5 "1BP"
joint_attributes.P4 20.000000
joint_attributes.P3 "1SF"
joint_attributes.P2 12.000000
joint_attributes.P1 "1FSF"
```