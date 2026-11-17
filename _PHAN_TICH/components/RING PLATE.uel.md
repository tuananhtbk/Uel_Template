# 0-RING PL

- File: `RING PLATE.uel`  (41,755 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	0-RING PL	All`
- Dialog parameters: 0 | Variables (P/custom): 62 | Distance vars (D): 264 | Parts with bound profile: 22
- Lookup files (fVF): DM-1F-RING+WING.txt
- Functions used: fP, fVF, tan
- Dialog bitmaps: 0 | Saved presets: #1F-D.p_0-RING PL, +1.p_0-RING PL, 0-111111111111111.p_0-RING PL, 0-B1 H700_300.p_0-RING PL, 0-T+4.p_0-RING PL

## Dialog (.inp)
```
page("TeklaStructures", "")
{
    joint(1, "0-RING PL")
    {
        tab_page("", " Parameters 1 ", 1)
        {
            parameter("", "P38", distance, number, 313, 10, 160)
            parameter("", "P31", distance, number, 765, 12, 160)
            attribute("", "Ring Plate Thk. (Auto)--------", label, "%s", none, none, "0", "0", 28, 15)
            attribute("", "RING TOP (+/-)", label, "%s", none, none, "0", "0", 530, 16)
            parameter("", "P32", distance, number, 766, 47, 160)
            parameter("", "P3", string, text, 312, 48, 160)
            attribute("", "RING BOTT (+/-)", label, "%s", none, none, "0", "0", 532, 49)
            attribute("", "PRIFIX", label, "%s", none, none, "0", "0", 26, 52)
            parameter("", "P16", YesNo, number, 768, 82, 160)
            parameter("", "P4", material, text, 313, 85, 160)
            attribute("", "TOP (Y/N)", label, "%s", none, none, "0", "0", 533, 85)
            attribute("", "MAT'L", label, "%s", none, none, "0", "0", 28, 90)
            parameter("", "P17", YesNo, number, 768, 109, 160)
            attribute("", "BOTT (Y/N)", label, "%s", none, none, "0", "0", 533, 112)
            parameter("", "P9", string, text, 314, 122, 160)
            attribute("", "START-NO.", label, "%s", none, none, "0", "0", 29, 127)
            parameter("", "P8", integer, number, 768, 144, 160)
            attribute("", "Class (Auto)------------------", label, "%s", none, none, "0", "0", 533, 148)
            parameter("", "P10", distance, number, 314, 157, 160)
            attribute("", "GAP-RING", label, "%s", none, none, "0", "0", 29, 161)
            parameter("", "P11", distance, number, 317, 195, 160)
            attribute("", "Angle-RING", label, "%s", none, none, "0", "0", 33, 196)
            parameter("", "P15", distance, number, 317, 229, 160)
            attribute("", "D (mm) (Auto)----------------", label, "%s", none, none, "0", "0", 32, 234)
            attribute("", "***********************************************************************************************************************************************", label, "%s", none, none, "0", "0", 20, 278)
            parameter("", "P60", distance, number, 819, 307, 160)
            attribute("", "WING-LENGTH (Auto)------------", label, "%s", none, none, "0", "0", 534, 309)
            attribute("", "Top Plate Thk. (Auto)---------", label, "%s", none, none, "0", "0", 30, 314)
            parameter("", "P18", distance, number, 312, 314, 160)
            parameter("", "P23", integer, number, 820, 343, 160)
            attribute("", "WING Class(Auto)--------------", label, "%s", none, none, "0", "0", 538, 345)
            parameter("", "P19", string, text, 313, 348, 160)
            attribute("", "Mark", label, "%s", none, none, "0", "0", 33, 349)
            parameter("", "P20", integer, number, 313, 376, 160)
            attribute("", "FB-Mark", label, "%s", none, none, "0", "0", 538, 376)
            parameter("", "P28", string, text, 823, 377, 160)
            attribute("", "Start No.", label, "%s", none, none, "0", "0", 33, 377)
            parameter("", "P21", distance, number, 313, 404, 160)
            parameter("", "P30", YesNo, number, 821, 405, 160)
            attribute("", "Gap to Ring PL", label, "%s", none, none, "0", "0", 33, 405)
            attribute("", "Top FB (Y/N)", label, "%s", none, none, "0", "0", 537, 408)
            attribute("", "Angle-BEAM", label, "%s", none, none, "0", "0", 33, 432)
            parameter("", "P22", string, text, 313, 431, 160)
            parameter("", "P24", YesNo, number, 821, 441, 160)
            attribute("", "Top (Y/N)", label, "%s", none, none, "0", "0", 541, 442)
            attribute("", "WING_GAP CUTTING", label, "%s", none, none, "0", "0", 34, 469)
            parameter("", "P62", distance, number, 309, 467, 160)
            parameter("", "P33", YesNo, number, 823, 474, 160)
            attribute("", "Bott (Y/N)", label, "%s", none, none, "0", "0", 542, 475)
            attribute("", "************************************************************************************************************************************************", label, "%s", none, none, "0", "0", 18, 521)
            parameter("", "P39", distance, number, 313, 558, 160)
            attribute("", "Beam Top FLG cutting gap", label, "%s", none, none, "0", "0", 21, 560)
            parameter("", "P55", YesNo, number, 809, 560, 160)
            attribute("", "WEB-CUT-TOP(Y/N)", label, "%s", none, none, "0", "0", 549, 563)
            attribute("", "Beam Web cutting gap X", label, "%s", none, none, "0", "0", 20, 590)
            parameter("", "P47", distance, number, 314, 594, 160)
            parameter("", "P57", YesNo, number, 808, 588, 160)
            attribute("", "WEB-CUT-BOTT(Y/N)", label, "%s", none, none, "0", "0", 540, 598)
            attribute("", "Beam Web cutting gap Y", label, "%s", none, none, "0", "0", 20, 618)
            parameter("", "P48", distance, number, 314, 622, 160)
            attribute("", "FLG-TOP CUT(Y/N)", label, "%s", none, none, "0", "0", 548, 629)
            parameter("", "P58", YesNo, number, 808, 629, 160)
            attribute("", "SCALLOP-TOP(Y/N)", label, "%s", none, none, "0", "0", 57, 655)
            attribute("", "FLG-BOTT CUT(Y/N)", label, "%s", none, none, "0", "0", 548, 661)
            parameter("", "P59", YesNo, number, 808, 661, 160)
            parameter("", "P54", YesNo, number, 313, 660, 160)
            attribute("", "SCALLOP-BOTT(Y/N)", label, "%s", none, none, "0", "0", 50, 688)
            parameter("", "P56", YesNo, number, 313, 694, 160)
        }
    }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Ring Plate Thk. (Auto)-------- | Length | `fP(proT,"ID651B600B-0000-A2BF-3136-393633303631")+4` *(formula)* |
| P2 | PROFILE | Profile | `"PL"+P38` *(formula)* |
| P3 | PRIFIX | Text | `1RP` |
| P4 | MAT'L | Material | `SM355` |
| P5 | L | Length | `P15` *(formula)* |
| P6 | Parameter6 | Integer | `99` |
| P7 | L1 | Length | `0.5*P15` *(formula)* |
| P8 | Class (Auto)------------------ | Integer | `P38` *(formula)* |
| P9 | STARNUMBER | Text | `1` |
| P10 | GAP-RING | Length | `2.00` |
| P11 | Angle-RING | Length | `45.00` |
| P12 | Parameter12 | Length | `P38-P10` *(formula)* |
| P13 | Parameter13 | Profile | `"TRANG"+P12+"*"+P11` *(formula)* |
| P14 | Parameter14 | Length | `fP(proT,"ID651B600B-0000-A2BF-3136-393633303631")` *(formula)* |
| P15 | D (mm) (Auto)---------------- | Length | `if P37==0 then 12*P14 else P37 endif` *(formula)* |
| P16 | TOP (Y/N) | Yes/No | `1` |
| P17 | BOTT (Y/N) | Yes/No | `1` |
| P18 | Top Plate Thk. (Auto)--------- | Length | `P29` *(formula)* |
| P19 | Mark | Text | `TSF` |
| P20 | Start No. | Integer | `1` |
| P21 | Gap to Ring PL | Length | `10.00` |
| P22 | Angle-BEAM | Text | `45` |
| P23 | WING Class(Auto)-------------- | Integer | `P35` *(formula)* |
| P24 | Top (Y/N) | Yes/No | `1` |
| P25 | Parameter25 | Material | `fP(proMATERIAL,"ID651B600B-0000-A2BF-3136-393633303631")` *(formula)* |
| P26 | WING LENGTH (Not Auto) | Length | `3000.00` |
| P27 | Parameter27 | Profile | `"TRANG"+(P18-P62)+"*"+P22` *(formula)* |
| P28 | FB-Mark | Text | `B1FB` |
| P29 | Parameter29 | Length | `fP(proT,"ID651B600B-0000-A2BF-3136-393633303631")` *(formula)* |
| P30 | Top FB (Y/N) | Yes/No | `1` |
| P31 | RING TOP (+/-) | Length | `0.00` |
| P32 | RING BOTT (+/-) | Length | `0.00` |
| P33 | Bott (Y/N) | Yes/No | `1` |
| P34 | Parameter34 | Profile | `fP(proPROFILE,"ID651B600B-0000-A2BF-3136-393633303631")` *(formula)* |
| P35 | WING_LENGTH | Length | `fVF("DM-1F-RING+WING.txt",P34,2)` *(formula)* |
| P36 | RING_THK | Length | `fVF("DM-1F-RING+WING.txt",P34,3)` *(formula)* |
| P37 | RING_DEEP | Length | `fVF("DM-1F-RING+WING.txt",P34,4)` *(formula)* |
| P38 | Ring Plate Thk. (Auto)-------- | Length | `if P36==0 then P1 else P36 endif` *(formula)* |
| P39 | Beam Top FLG cutting gap | Length | `25.00` |
| P40 | Parameter40 | Profile | `P14+"*"+400` *(formula)* |
| P41 | Parameter41 | Length | `0.00` |
| P42 | Parameter42 | Length | `0.00` |
| P43 | Parameter43 | Length | `0.00` |
| P44 | Parameter44 | Length | `P38` *(formula)* |
| P45 | Parameter45 | Angle | `P22` *(formula)* |
| P46 | Parameter46 | Length | `P21+P38+(P14-P10)*tan(P45)` *(formula)* |
| P47 | Beam Web cutting gap X | Length | `10.00` |
| P48 | Beam Web cutting gap Y | Length | `50.00` |
| P49 | Parameter49 | Length | `0.00` |
| P50 | Parameter50 | Length | `20.00` |
| P51 | Parameter51 | Text | `0.00` |
| P52 | Parameter52 | Length | `20.00` |
| P53 | Parameter53 | Length | `0.00` |
| P54 | R-TOP(Y/N) | Yes/No | `1` |
| P55 | WEB-CUT-TOP(Y/N) | Yes/No | `1` |
| P56 | R-BOTT(Y/N) | Yes/No | `1` |
| P57 | WEB-CUT-BOTT(Y/N) | Yes/No | `1` |
| P58 | FLG-TOP CUT(Y/N) | Yes/No | `1` |
| P59 | FLG-BOTT CUT(Y/N) | Yes/No | `1` |
| P60 | WING-LENGTH (Auto)------------ | Length | `if P35==0 then 1000 else P35 endif` *(formula)* |
| P61 | Parameter61 | Length | `P60+P38+P21` *(formula)* |
| P62 | WING_GAP CUTTING | Length | `0.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.COLUMN | `` |
| D2 | D2.PLATE.COLUMN | `` |
| D3 | D3.PLATE.COLUMN | `` |
| D4 | D4.PLATE.COLUMN | `` |
| D5 | D5.PLATE.COLUMN | `` |
| D6 | D6.PLATE.COLUMN | `` |
| D7 | D7.PLATE.COLUMN | `` |
| D8 | D8.PLATE.COLUMN | `` |
| D9 | D9.PLATE.COLUMN | `` |
| D10 | D10.PLATE.COLUMN | `` |
| D11 | D11.PLATE.COLUMN | `` |
| D12 | D12.PLATE.COLUMN | `` |
| D13 | D13.PLATE.COLUMN | `` |
| D14 | D14.PLATE.COLUMN | `` |
| D15 | D15.PLATE.COLUMN | `` |
| D16 | D16.PLATE.COLUMN | `` |
| D17 | D17.PLATE.COLUMN | `` |
| D18 | D18.PLATE.COLUMN | `` |
| D19 | D19.PLATE.COLUMN | `` |
| D20 | D20.PLATE.COLUMN | `` |
| D21 | D21.PLATE.COLUMN | `` |
| D22 | D22.PLATE.COLUMN | `` |
| D23 | D23.PLATE.COLUMN | `` |
| D24 | D24.PLATE.COLUMN | `` |
| D25 | D25.PLATE.PLATE | `` |
| D26 | D26.PLATE.PLATE | `` |
| D27 | D27.PLATE.PLATE | `` |
| D28 | D28.PLATE.PLATE | `` |
| D29 | D29.PLATE.PLATE | `` |
| D30 | D30.PLATE.PLATE | `` |
| D31 | D31.PLATE.PLATE | `` |
| D32 | D32.PLATE.PLATE | `` |
| D33 | D33.BEAM.PLATE | `` |
| D34 | D34.BEAM.PLATE | `` |
| D35 | D35.BEAM.PLATE | `` |
| D36 | D36.BEAM.PLATE | `` |
| D37 | D37.BEAM.PLATE | `` |
| D38 | D38.BEAM.PLATE | `` |
| D39 | D39.BEAM.PLATE | `` |
| D40 | D40.BEAM.PLATE | `` |
| D41 | D41.BEAM.COLUMN | `` |
| D42 | D42.BEAM.COLUMN | `` |
| D43 | D43.BEAM.COLUMN | `` |
| D44 | D44.BEAM.COLUMN | `` |
| D45 | D45.BEAM.PLATE | `` |
| D46 | D46.BEAM.PLATE | `` |
| D47 | D47.BEAM.PLATE | `` |
| D48 | D48.BEAM.PLATE | `` |
| D49 | D49.BEAM.PLATE | `` |
| D50 | D50.BEAM.PLATE | `` |
| D51 | D51.BEAM.PLATE | `` |
| D52 | D52.BEAM.PLATE | `` |
| D53 | D53.BEAM.COLUMN | `` |
| D54 | D54.BEAM.COLUMN | `` |
| D55 | D55.BEAM.COLUMN | `` |
| D56 | D56.BEAM.COLUMN | `` |
| D57 | D57.PLATE.COLUMN | `` |
| D58 | D58.PLATE.COLUMN | `` |
| D59 | D59.PLATE.COLUMN | `` |
| D60 | D60.PLATE.COLUMN | `` |
| D61 | D61.PLATE.COLUMN | `` |
| D62 | D62.PLATE.COLUMN | `` |
| D63 | D63.PLATE.COLUMN | `` |
| D64 | D64.PLATE.COLUMN | `` |
| D65 | D65.PLATE.COLUMN | `` |
| D66 | D66.PLATE.COLUMN | `` |
| D67 | D67.PLATE.COLUMN | `` |
| D68 | D68.PLATE.COLUMN | `` |
| D69 | D69.PLATE.GIRDER | `P5-P7+P31` |
| D70 | D70.PLATE.GIRDER | `P5-P7+P31` |
| D71 | D71.PLATE.GIRDER | `P5-P7+P31` |
| D72 | D72.PLATE.GIRDER | `P5-P7+P31` |
| D73 | D73.PLATE.GIRDER | `P5-P7+P31` |
| D74 | D74.PLATE.GIRDER | `P5-P7+P31` |
| D75 | D75.PLATE.GIRDER | `P5-P7+P31` |
| D76 | D76.PLATE.GIRDER | `P5-P7+P31` |
| D77 | D77.PLATE.GIRDER | `P7-P31` |
| D78 | D78.PLATE.GIRDER | `P7-P31` |
| D79 | D79.PLATE.GIRDER | `P7-P31` |
| D80 | D80.PLATE.GIRDER | `P7-P31` |
| D81 | D81.PLATE.GIRDER | `P7-P31` |
| D82 | D82.PLATE.GIRDER | `P7-P31` |
| D83 | D83.PLATE.GIRDER | `P7-P31` |
| D84 | D84.PLATE.GIRDER | `P7-P31` |
| D85 | D85.PLATE.COLUMN | `` |
| D86 | D86.PLATE.COLUMN | `` |
| D87 | D87.PLATE.COLUMN | `` |
| D88 | D88.PLATE.COLUMN | `` |
| D89 | D89.PLATE.COLUMN | `` |
| D90 | D90.PLATE.COLUMN | `` |
| D91 | D91.PLATE.COLUMN | `` |
| D92 | D92.PLATE.COLUMN | `` |
| D93 | D93.PLATE.PLATE | `` |
| D94 | D94.PLATE.PLATE | `` |
| D95 | D95.PLATE.PLATE | `` |
| D96 | D96.PLATE.PLATE | `` |
| D97 | D97.PLATE.COLUMN | `` |
| D98 | D98.PLATE.COLUMN | `` |
| D99 | D99.PLATE.COLUMN | `` |
| D100 | D100.PLATE.COLUMN | `` |
| D101 | D101.PLATE.PLATE | `` |
| D102 | D102.PLATE.PLATE | `` |
| D103 | D103.PLATE.PLATE | `` |
| D104 | D104.PLATE.PLATE | `` |
| D105 | D105.BEAM.PLATE | `` |
| D106 | D106.BEAM.PLATE | `` |
| D107 | D107.BEAM.PLATE | `` |
| D108 | D108.BEAM.PLATE | `` |
| D109 | D109.BEAM.PLATE | `` |
| D110 | D110.BEAM.PLATE | `` |
| D111 | D111.BEAM.PLATE | `` |
| D112 | D112.BEAM.PLATE | `` |
| D113 | D113.BEAM.COLUMN | `` |
| D114 | D114.BEAM.COLUMN | `` |
| D115 | D115.BEAM.COLUMN | `` |
| D116 | D116.BEAM.COLUMN | `` |
| D117 | D117.BEAM.PLATE | `` |
| D118 | D118.BEAM.PLATE | `` |
| D119 | D119.BEAM.PLATE | `` |
| D120 | D120.BEAM.PLATE | `` |
| D121 | D121.BEAM.PLATE | `` |
| D122 | D122.BEAM.PLATE | `` |
| D123 | D123.BEAM.PLATE | `` |
| D124 | D124.BEAM.PLATE | `` |
| D125 | D125.BEAM.COLUMN | `` |
| D126 | D126.BEAM.COLUMN | `` |
| D127 | D127.BEAM.COLUMN | `` |
| D128 | D128.BEAM.COLUMN | `` |
| D129 | D129.PLATE.GIRDER | `0.5*P15+P32` |
| D130 | D130.PLATE.GIRDER | `0.5*P15+P32` |
| D131 | D131.PLATE.GIRDER | `0.5*P15+P32` |
| D132 | D132.PLATE.GIRDER | `0.5*P15+P32` |
| D133 | D133.PLATE.GIRDER | `0.5*P15+P32` |
| D134 | D134.PLATE.GIRDER | `0.5*P15+P32` |
| D135 | D135.PLATE.GIRDER | `0.5*P15+P32` |
| D136 | D136.PLATE.GIRDER | `0.5*P15+P32` |
| D137 | D137.PLATE.GIRDER | `0.5*P15-P32` |
| D138 | D138.PLATE.GIRDER | `0.5*P15-P32` |
| D139 | D139.PLATE.GIRDER | `0.5*P15-P32` |
| D140 | D140.PLATE.GIRDER | `0.5*P15-P32` |
| D141 | D141.PLATE.GIRDER | `0.5*P15-P32` |
| D142 | D142.PLATE.GIRDER | `0.5*P15-P32` |
| D143 | D143.PLATE.GIRDER | `0.5*P15-P32` |
| D144 | D144.PLATE.GIRDER | `0.5*P15-P32` |
| D145 | D145.PLATE.GIRDER | `` |
| D146 | D146.PLATE.GIRDER | `` |
| D147 | D147.PLATE.GIRDER | `` |
| D148 | D148.PLATE.GIRDER | `` |
| D149 | D149.PLATE.GIRDER | `` |
| D150 | D150.PLATE.GIRDER | `` |
| D151 | D151.PLATE.COLUMN | `P38` |
| D152 | D152.PLATE.COLUMN | `P38` |
| D153 | D153.PLATE.COLUMN | `P38` |
| D154 | D154.PLATE.COLUMN | `P38` |
| D155 | D155.PLATE.COLUMN | `P38+P21` |
| D156 | D156.PLATE.COLUMN | `P38+P21` |
| D157 | D157.PLATE.COLUMN | `P38+P21+2*(P18-2)` |
| D158 | D158.PLATE.COLUMN | `P38+P21+2*(P18-2)` |
| D159 | D159.PLATE.GIRDER | `2*(P18-2)` |
| D160 | D160.PLATE.GIRDER | `2*(P18-2)` |
| D161 | D161.PLATE.COLUMN | `P61` |
| D162 | D162.PLATE.COLUMN | `P61` |
| D163 | D163.BEAM.PLATE | `` |
| D164 | D164.BEAM.PLATE | `` |
| D165 | D165.BEAM.COLUMN | `P38` |
| D166 | D166.BEAM.COLUMN | `P38` |
| D167 | D167.BEAM.PLATE | `` |
| D168 | D168.BEAM.PLATE | `` |
| D169 | D169.FLAT_BAR.PLATE | `` |
| D170 | D170.FLAT_BAR.PLATE | `` |
| D171 | D171.FLAT_BAR.COLUMN | `P38` |
| D172 | D172.FLAT_BAR.COLUMN | `P38` |
| D173 | D173.FLAT_BAR.PLATE | `` |
| D174 | D174.FLAT_BAR.PLATE | `` |
| D175 | D175.PLATE.GIRDER | `` |
| D176 | D176.PLATE.GIRDER | `` |
| D177 | D177.PLATE.GIRDER | `` |
| D178 | D178.PLATE.GIRDER | `` |
| D179 | D179.PLATE.GIRDER | `` |
| D180 | D180.PLATE.GIRDER | `` |
| D181 | D181.PLATE.COLUMN | `P38+P21` |
| D182 | D182.PLATE.COLUMN | `P38+P21` |
| D183 | D183.PLATE.COLUMN | `P38+P21+2*(P18-2)` |
| D184 | D184.PLATE.COLUMN | `P38+P21+2*(P18-2)` |
| D185 | D185.PLATE.COLUMN | `P61` |
| D186 | D186.PLATE.COLUMN | `P61` |
| D187 | D187.PLATE.COLUMN | `P38` |
| D188 | D188.PLATE.COLUMN | `P38` |
| D189 | D189.PLATE.COLUMN | `P38` |
| D190 | D190.PLATE.COLUMN | `P38` |
| D191 | D191.PLATE.GIRDER | `2*(P18-2)` |
| D192 | D192.PLATE.GIRDER | `2*(P18-2)` |
| D193 | D193.FLAT_BAR.PLATE | `` |
| D194 | D194.FLAT_BAR.PLATE | `` |
| D195 | D195.FLAT_BAR.PLATE | `` |
| D196 | D196.FLAT_BAR.PLATE | `` |
| D197 | D197.FLAT_BAR.PLATE | `` |
| D198 | D198.FLAT_BAR.PLATE | `` |
| D199 | D199.BEAM.COLUMN | `P38` |
| D200 | D200.BEAM.COLUMN | `P38` |
| D201 | D201.BEAM.PLATE | `` |
| D202 | D202.BEAM.PLATE | `` |
| D203 | D203.BEAM.PLATE | `` |
| D204 | D204.BEAM.PLATE | `` |
| D205 | D205.BEAM.GIRDER | `` |
| D206 | D206.BEAM.GIRDER | `` |
| D207 | D207.BEAM.COLUMN | `P38+P39` |
| D208 | D208.BEAM.COLUMN | `P38+P39` |
| D209 | D209.BEAM.GIRDER | `` |
| D210 | D210.BEAM.GIRDER | `` |
| D211 | D211.BEAM.GIRDER | `` |
| D212 | D212.BEAM.GIRDER | `` |
| D213 | D213.BEAM.GIRDER | `` |
| D214 | D214.BEAM.GIRDER | `` |
| D215 | D215.BEAM.COLUMN | `P46+P47` |
| D216 | D216.BEAM.COLUMN | `P46+P47` |
| D217 | D217.CUTPART.GIRDER | `` |
| D218 | D218.CUTPART.GIRDER | `` |
| D219 | D219.CUTPART.GIRDER | `` |
| D220 | D220.CUTPART.GIRDER | `` |
| D221 | D221.CUTPART.GIRDER | `` |
| D222 | D222.CUTPART.GIRDER | `` |
| D223 | D223.CUTPART.GIRDER | `` |
| D224 | D224.CUTPART.GIRDER | `` |
| D225 | D225.CUTPART.GIRDER | `` |
| D226 | D226.CUTPART.GIRDER | `` |
| D227 | D227.CUTPART.GIRDER | `` |
| D228 | D228.CUTPART.GIRDER | `` |
| D229 | D229.CUTPART.GIRDER | `` |
| D230 | D230.CUTPART.GIRDER | `` |
| D231 | D231.CUTPART.GIRDER | `` |
| D232 | D232.CUTPART.GIRDER | `` |
| D233 | D233.CUTPART.COLUMN | `` |
| D234 | D234.CUTPART.COLUMN | `` |
| D235 | D235.CUTPART.COLUMN | `` |
| D236 | D236.CUTPART.COLUMN | `` |
| D237 | D237.CUTPART.COLUMN | `P38+P39+35` |
| D238 | D238.CUTPART.COLUMN | `P38+P39+35` |
| D239 | D239.CUTPART.COLUMN | `P46+P47+35` |
| D240 | D240.CUTPART.COLUMN | `P46+P47+35` |
| D241 | D241.CUTPART.GIRDER | `` |
| D242 | D242.CUTPART.GIRDER | `` |
| D243 | D243.CUTPART.GIRDER | `` |
| D244 | D244.CUTPART.GIRDER | `` |
| D245 | D245.CUTPART.GIRDER | `` |
| D246 | D246.CUTPART.GIRDER | `` |
| D247 | D247.CUTPART.GIRDER | `` |
| D248 | D248.CUTPART.GIRDER | `` |
| D249 | D249.CUTPART.COLUMN | `` |
| D250 | D250.CUTPART.COLUMN | `` |
| D251 | D251.CUTPART.COLUMN | `` |
| D252 | D252.CUTPART.COLUMN | `` |
| D253 | D253.CUTPART.GIRDER | `` |
| D254 | D254.CUTPART.GIRDER | `` |
| D255 | D255.CUTPART.GIRDER | `P5-P7-P31+P48` |
| D256 | D256.CUTPART.GIRDER | `P5-P7-P31+P48` |
| D257 | D257.CUTPART.COLUMN | `P38+P47` |
| D258 | D258.CUTPART.COLUMN | `P38+P47` |
| D259 | D259.CUTPART.GIRDER | `` |
| D260 | D260.CUTPART.GIRDER | `` |
| D261 | D261.CUTPART.GIRDER | `0.5*P15+P32+P48` |
| D262 | D262.CUTPART.GIRDER | `0.5*P15+P32+P48` |
| D263 | D263.CUTPART.COLUMN | `P38+P47` |
| D264 | D264.CUTPART.COLUMN | `P38+P47` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#241 | CLASS | `P8` |
| obj#241 | CREATION | `P17` |
| obj#241 | MATERIAL | `P4` |
| obj#241 | PROFILE | `P2` |
| obj#241 | SERIE | `P3` |
| obj#241 | STARTNUMBER | `P9` |
| obj#253 | CLASS | `P8` |
| obj#253 | CREATION | `P17` |
| obj#253 | MATERIAL | `P4` |
| obj#253 | PROFILE | `P2` |
| obj#253 | SERIE | `P3` |
| obj#253 | STARTNUMBER | `P9` |
| obj#260 | CLASS | `P8` |
| obj#260 | CREATION | `P17` |
| obj#260 | MATERIAL | `P4` |
| obj#260 | PROFILE | `P2` |
| obj#260 | SERIE | `P3` |
| obj#260 | STARTNUMBER | `P9` |
| obj#268 | CLASS | `P8` |
| obj#268 | CREATION | `P17` |
| obj#268 | MATERIAL | `P4` |
| obj#268 | PROFILE | `P2` |
| obj#268 | SERIE | `P3` |
| obj#268 | STARTNUMBER | `P9` |
| obj#275 | CLASS | `P8` |
| obj#275 | CREATION | `P16` |
| obj#275 | MATERIAL | `P4` |
| obj#275 | PROFILE | `P2` |
| obj#275 | SERIE | `P3` |
| obj#275 | STARTNUMBER | `P9` |
| obj#281 | CLASS | `P8` |
| obj#281 | CREATION | `P16` |
| obj#281 | MATERIAL | `P4` |
| obj#281 | PROFILE | `P2` |
| obj#281 | SERIE | `P3` |
| obj#281 | STARTNUMBER | `P9` |
| obj#287 | CLASS | `P8` |
| obj#287 | CREATION | `P16` |
| obj#287 | MATERIAL | `P4` |
| obj#287 | PROFILE | `P2` |
| obj#287 | SERIE | `P3` |
| obj#287 | STARTNUMBER | `P9` |
| obj#293 | CLASS | `P8` |
| obj#293 | CREATION | `P16` |
| obj#293 | MATERIAL | `P4` |
| obj#293 | PROFILE | `P2` |
| obj#293 | SERIE | `P3` |
| obj#293 | STARTNUMBER | `P9` |
| obj#299 | CLASS | `P23` |
| obj#299 | CREATION | `P24` |
| obj#299 | MATERIAL | `P25` |
| obj#299 | PROFILE | `"PL"+P18` |
| obj#299 | SERIE | `P19` |
| obj#299 | STARTNUMBER | `P20` |
| obj#310 | CREATION | `P30` |
| obj#310 | SERIE | `P28` |
| obj#321 | CLASS | `P23` |
| obj#321 | CREATION | `P33` |
| obj#321 | MATERIAL | `P25` |
| obj#321 | PROFILE | `"PL"+P18` |
| obj#321 | SERIE | `P19` |
| obj#321 | STARTNUMBER | `P20` |
| obj#328 | CREATION | `P33` |
| obj#328 | SERIE | `P28` |
| obj#334 | CREATION | `P57` |
| obj#347 | CREATION | `P55` |
| obj#354 | CREATION | `P59` |
| obj#354 | PROFILE | `P40` |
| obj#367 | CREATION | `P56` |
| obj#375 | CREATION | `P54` |
| obj#383 | CREATION | `P58` |
| obj#383 | PROFILE | `P40` |
| obj#391 | PROFILE | `P13` |
| obj#401 | PROFILE | `P13` |
| obj#408 | PROFILE | `P13` |
| obj#415 | PROFILE | `P13` |
| obj#423 | PROFILE | `P13` |
| obj#430 | PROFILE | `P13` |
| obj#437 | PROFILE | `P13` |
| obj#444 | PROFILE | `P13` |
| obj#451 | PROFILE | `P27` |
| obj#460 | PROFILE | `P27` |

## Preset `#1F-D.p_0-RING PL`
```
joint_attributes.saveas_file "#1F-D"
joint_attributes.get_menu "#-1D"
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
joint_attributes.P56 0
joint_attributes.P57 0
joint_attributes.P55 0
joint_attributes.P59 0
joint_attributes.P58 1
joint_attributes.P54 1
joint_attributes.P48 10.000000
joint_attributes.P39 25.000000
joint_attributes.P30 1
joint_attributes.P28 "1D-FB"
joint_attributes.P23 11
joint_attributes.P33 0
joint_attributes.P24 1
joint_attributes.P22 "45"
joint_attributes.P21 10.000000
joint_attributes.P20 1
joint_attributes.P19 "1D-WPT"
joint_attributes.P11 45.000000
joint_attributes.P10 2.000000
joint_attributes.P8 100
joint_attributes.P9 "1"
joint_attributes.P17 0
joint_attributes.P16 1
joint_attributes.P3 "1D-RPT"
joint_attributes.P32 0.000000
joint_attributes.P31 0.000000
```

## Preset `+1.p_0-RING PL`
```
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
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P56 0
joint_attributes.P54 0
joint_attributes.P59 0
joint_attributes.P58 0
joint_attributes.P57 0
joint_attributes.P55 0
joint_attributes.P33 0
joint_attributes.P24 1
joint_attributes.P30 1
joint_attributes.P17 0
joint_attributes.P16 0
```

## Preset `0-111111111111111.p_0-RING PL`
```
joint_attributes.saveas_file "0-111111111111111"
joint_attributes.get_menu "0-111111111111111"
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
joint_attributes.zsuunta 2
joint_attributes.P9 "1001"
joint_attributes.P8 12.000000
joint_attributes.P7 145.000000
joint_attributes.P5 290.000000
joint_attributes.P4 "SM355"
joint_attributes.P3 "B1RP"
joint_attributes.P1 "24"
```

## Preset `0-B1 H700_300.p_0-RING PL`
```
joint_attributes.saveas_file "0-B1 H700*300"
joint_attributes.get_menu "0-B1 H700*300"
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
joint_attributes.P33 0
joint_attributes.P30 1
joint_attributes.P28 "B1FB"
joint_attributes.P26 410.000000
joint_attributes.P24 1
joint_attributes.P23 2022
joint_attributes.P22 "35"
joint_attributes.P21 10.000000
joint_attributes.P20 1001
joint_attributes.P19 "B1TSF"
joint_attributes.P17 0
joint_attributes.P16 1
joint_attributes.P15 264.000000
joint_attributes.P11 45.000000
joint_attributes.P10 2.000000
joint_attributes.P9 "1001"
joint_attributes.P8 99
joint_attributes.P4 "SM355"
joint_attributes.P3 "B1RP"
```

## Preset `0-T+4.p_0-RING PL`
```
joint_attributes.saveas_file "0-T+4"
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
joint_attributes.P8 9.000000
joint_attributes.P7 75.000000
joint_attributes.P5 150.000000
joint_attributes.P4 "SM355"
joint_attributes.P3 "B1RP"
joint_attributes.P1 "24"
```