# 0-DM_S-Col2Gir_TANG_DUONG

- File: `COL-GIRDER-P-BOX\YGY-S_COL2GIR-운용층-TANG_DUONG.uel`  (39,457 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	0-DM_S-Col2Gir_TANG_DUONG	All`
- Dialog parameters: 31 | Variables (P/custom): 76 | Distance vars (D): 197 | Parts with bound profile: 7
- Lookup files (fVF): YGY-S_COL2GIR-운용층-TANG_DUONG.txt
- Functions used: cos, fAD, fP, fVF, if, max, mid, sin, tan
- Dialog bitmaps: 0 | Saved presets: +101-B4.p_0-DM_S-Col2Gir_TANG_DUONG, 28-180.p_0-DM_S-Col2Gir_TANG_DUONG

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "0-DM_S-Col2Gir_TANG_DUONG")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("BOLT to BEAM FLG.", "P4", distance, number, 1)
           parameter("CGP방향", "P11", YesNo, number, 2)
           parameter("CGP-ZONE", "P12", string, text, 3)
           parameter("CGP MAT'L", "P13", material, text, 4)
           parameter("MO(YES)_PIN(NO)", "P14", YesNo, number, 5)
           parameter("BOLT_PITCH--------------", "P22", string, text, 6)
           parameter("개선여장", "P23", distance, number, 7)
           parameter("SLOT_HOLE길이---------", "P24", distance, number, 8)
           parameter("WING_PL_길이-----------", "P28", distance, number, 9)
           parameter("WING_MAT'L---------------", "P37", material, text, 10)
           parameter("BOLT_SIZE", "P40_diameter", bolt_size, number, 11)
           parameter("BOLT_STANDARD", "P40_screwdin", bolt_standard, text, 12)
           parameter("CWS-SPS-EDGE (Bolt To GP/y)", "P21A", distance, number, 13)
           parameter("CWS_SPS", "P21B", distance, number, 14)
           parameter("L3-Auto--------------", "P47", distance, number, 15)
           parameter("WP1-(Y/N)-----------", "P50", integer, number, 16)
           parameter("WP2-(Y/N)-----------", "P51", integer, number, 17)
           parameter("GUSSET-START NO", "P52", integer, number, 18)
           parameter("WING-START NO", "P53", integer, number, 19)
           parameter("GWP_ZONE", "P54", string, text, 20)
           parameter("TOP RING THK", "P55", distance, number, 21)
           parameter("BOTT RING THK", "P56", distance, number, 22)
           parameter("TOP / R+", "P58", distance, number, 23)
           parameter("BOTT / R+", "P59", distance, number, 24)
           parameter("TOP FLG TO BOT RING", "P60", distance, number, 25)
       }
       tab_page("", " Parameters 2 ", 2)
       {
           parameter("BOT FLG TO TOP RING", "P61", distance, number, 1)
           parameter("BEAM TOP WEB CUT (Y/N)", "P62", YesNo, number, 2)
           parameter("BEAM BOT WEB CUT (Y/N)", "P63", YesNo, number, 3)
           parameter("CGP TOP CUT (Y/N)", "P64", YesNo, number, 4)
           parameter("CGP BOT CUT (Y/N)", "P65", YesNo, number, 5)
           parameter("Parameter73", "P73", string, text, 6)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Length | `fP(proS,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P2 | Parameter2 | Text | `fP(proPROFILE,"ID67174322-0000-41C3-3137-323935383532")` *(formula)* |
| P3 | Parameter3 | Length | `fVF("YGY-S_COL2GIR-운용층-TANG_DUONG.txt",P15,4)` *(formula)* |
| P4 | BOLT to BEAM FLG. | Length | `65.00` |
| P5 | BOLT_PITCH | Length | `320.00` |
| P6 | Parameter6 | Length | `fP(proB,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P7 | Parameter7 | Angle | `45.0000` |
| P8 | Parameter8 | Length | `fP(proT,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P9 | PLATE방향(1&-1) | Length | `if (P11==0) then 1 else -1 endif` *(formula)* |
| P10 | Parameter10 | Length | `if (P9==1) then 10000 else 1000 endif` *(formula)* |
| P11 | CGP방향 | Yes/No | `0` |
| P12 | CGP-ZONE | Text | `P72` *(formula)* |
| P13 | CGP MAT'L | Material | `SN355` |
| P14 | MO(YES)_PIN(NO) | Yes/No | `1` |
| P15 | GIR_MEMBER | Text | `fP(proPROFILE,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P16 | Parameter16 | Text | `fVF("YGY-S_COL2GIR-운용층-TANG_DUONG.txt",P15,1)` *(formula)* |
| P17 | Parameter17 | Integer | `fVF("YGY-S_COL2GIR-운용층-TANG_DUONG.txt",P15,4)` *(formula)* |
| P18 | Parameter18 | Integer | `fVF("YGY-S_COL2GIR-운용층-TANG_DUONG.txt",P15,7)` *(formula)* |
| P19 | Parameter19 | Length | `fVF("YGY-S_COL2GIR-운용층-TANG_DUONG.txt",P15,3)` *(formula)* |
| P20 | Parameter20 | Integer | `fP(proH,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P21 | Parameter21 | Length | `if (P20>=P21B) then ((P19-P21A)/2) else (P19-P21A) endif` *(formula)* |
| P21A | CWS-SPS-EDGE (Bolt To GP/y) | Length | `80.00` |
| P21B | CWS_SPS | Length | `900.00` |
| P22 | BOLT_PITCH-------------- | Text | `if (P18==0) then 0 else (P18+"*"+P21) endif` *(formula)* |
| P23 | 개선여장 | Length | `400.00` |
| P24 | SLOT_HOLE길이--------- | Length | `if (P40_diameter==16) then 40 else if (P40_diameter==20) then 50 else if (P40_diameter==22) then 55 else if (P40_diameter==24) then 60 else if (P40_diameter==27) then 67 else endif endif endif endif endif` *(formula)* |
| P25 | Parameter25 | Length | `P40_diameter` *(formula)* |
| P26 | Parameter26 | Length | `if (P40_diameter==16) then 2 else if (P40_diameter==20) then 2 else if (P40_diameter==22) then 2 else if (P40_diameter==24) then 3 else if (P40_diameter==27) then 3 else endif endif endif endif endif` *(formula)* |
| P27 | Parameter27 | Length | `P24-P25-P26` *(formula)* |
| P28 | WING_PL_길이----------- | Length | `fVF("YGY-S_COL2GIR-운용층-TANG_DUONG.txt",P15,5)` *(formula)* |
| P29 | COL_WIDTH | Length | `fP(proB,"ID67174322-0000-4167-3137-323935383532")` *(formula)* |
| P30 | GIR_WIDTH | Length | `fP(proB,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P31 | Parameter31 | Length | `P29-P30` *(formula)* |
| P32 | WING_PL_유무 | Yes/No | `if (P31>=150) then 1 else 0 endif` *(formula)* |
| P33 | Parameter33 | Length | `0.00` |
| P34 | GIRDER_MAT | Material | `fP(proMATERIAL,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P35 | Parameter35 | Length | `if (P31>=150) then 40 else P36 endif` *(formula)* |
| P36 | Parameter36 | Length | `P31/2/2` *(formula)* |
| P37 | WING_MAT'L--------------- | Material | `if (P34=="SHN355") then "SN355" else if (P34=="SHN275") then "SN275" else P34 endif endif` *(formula)* |
| P38 | Parameter38 | Text | `fP(proASSEMBLYSERIE,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P39 | START_NO | Integer | `fP(proASSEMBLYSTARTNUMBER,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P40_diameter | BOLT_SIZE | 6 | `24.00` |
| P40_screwdin | BOLT_STANDARD | 7 | `T.S.B` |
| P41 | Parameter41 | Length | `if (P11==0) then 10000 else 20000 endif` *(formula)* |
| P42 | WING1_유무 | Yes/No | `if (P14==0) then 0 else if (D113<74.4) then 0 else 1 endif endif` *(formula)* |
| P43 | WING2_유무 | Yes/No | `if (P14==0) then 0 else if (D122<74.4) then 1 else 1 endif endif` *(formula)* |
| P44 | Parameter44 | Length | `if (P50==0) then D55 else ((P30/2+D113)+20) endif` *(formula)* |
| P45 | Parameter45 | Length | `P44` *(formula)* |
| P46 | Parameter46 | Length | `100.00` |
| P47 | L3-Auto-------------- | Length | `fVF("YGY-S_COL2GIR-운용층-TANG_DUONG.txt",P15,2)` *(formula)* |
| P48 | Parameter48 | Integer | `if (floor(D113)<74) then 0 else 1 endif` *(formula)* |
| P49 | Parameter49 | Integer | `if (floor(D122)<74) then 0 else 1 endif` *(formula)* |
| P50 | WP1-(Y/N)----------- | Integer | `if (P48==0 \|\| P14==0) then 0 else 1 endif` *(formula)* |
| P51 | WP2-(Y/N)----------- | Integer | `if (P49==0 \|\| P14==0) then 0 else 1 endif` *(formula)* |
| P52 | GUSSET-START NO | Integer | `1` |
| P53 | WING-START NO | Integer | `1` |
| P54 | GWP_ZONE | Text | `P73` *(formula)* |
| P55 | TOP RING THK | Length | `0.00` |
| P56 | BOTT RING THK | Length | `0.00` |
| P57 | Parameter57 | Length | `max(P55,P56)` *(formula)* |
| P58 | TOP / R+ | Length | `0.00` |
| P59 | BOTT / R+ | Length | `0.00` |
| P60 | TOP FLG TO BOT RING | Length | `100.00` |
| P61 | BOT FLG TO TOP RING | Length | `100.00` |
| P62 | BEAM TOP WEB CUT (Y/N) | Yes/No | `0` |
| P63 | BEAM BOT WEB CUT (Y/N) | Yes/No | `0` |
| P64 | CGP TOP CUT (Y/N) | Yes/No | `0` |
| P65 | CGP BOT CUT (Y/N) | Yes/No | `0` |
| P66 | Parameter66 | Angle | `fAD("skew","ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P67 | Parameter67 | Length | `0.5*P17*sin(P66)` *(formula)* |
| P68 | Parameter68 | Length | `P47*cos(P66)` *(formula)* |
| P69 | Parameter69 | Length | `P67+P68` *(formula)* |
| P70 | Parameter70 | Text | `fP(proSERIE,"ID67174322-0000-4167-3137-323935383532")` *(formula)* |
| P71 | Parameter71 | Text | `fP(proSERIE,"ID67174322-0000-416B-3137-323935383532")` *(formula)* |
| P72 | Parameter72 | Text | `mid(P70,0,find(P70,"M"))` *(formula)* |
| P73 | Parameter73 | Text | `mid(P71,0,find(P71,"M"))` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.GIRDER | `((P1+P3)/2)*P9` |
| D2 | D2.PLATE.GIRDER | `((P1+P3)/2)*P9` |
| D3 | D3.PLATE.GIRDER | `((P1+P3)/2)*P9` |
| D4 | D4.PLATE.GIRDER | `((P1+P3)/2)*P9` |
| D5 | D5.FITTING.COLUMN | `if (P14==1) then 5 else 30 endif` |
| D6 | D6.FITTING.COLUMN | `D5` |
| D7 | D7.FITTING.COLUMN | `D5` |
| D8 | D8.BOLT.COLUMN | `P57+P47` |
| D9 | D9.BOLT.CGPPLATE | `` |
| D10 | D10.PLATE.GIRDER | `` |
| D11 | D11.PLATE.GIRDER | `` |
| D12 | D12.PLATE.GIRDER | `` |
| D13 | D13.PLATE.GIRDER | `` |
| D14 | D14.PLATE.COLUMN | `` |
| D15 | D15.PLATE.COLUMN | `` |
| D16 | D16.PLATE.COLUMN | `if (P14==1) then 20 else 0 endif` |
| D17 | D17.PLATE.COLUMN | `D16` |
| D18 | D18.BOLT.GIRDER | `` |
| D19 | D19.BOLT.GIRDER | `` |
| D20 | D20.BOLT.GIRDER | `` |
| D21 | D21.BOLT.GIRDER | `` |
| D22 | GP조정-1 | `P69` |
| D23 | D23.PLATE.COLUMN | `P69` |
| D24 | GP조정-2 | `P67` |
| D25 | D25.PLATE.COLUMN | `D24` |
| D26 | D26.PLATE.GIRDER | `P19/2` |
| D27 | D27.PLATE.GIRDER | `D26` |
| D28 | D28.PLATE.GIRDER | `D26` |
| D29 | D29.PLATE.GIRDER | `D26` |
| D30 | D30.PLATE.GIRDER | `` |
| D31 | D31.PLATE.GIRDER | `` |
| D32 | D32.PLATE.GIRDER | `` |
| D33 | D33.PLATE.GIRDER | `` |
| D34 | D34.CUTPART.GIRDER | `P8` |
| D35 | D35.CUTPART.GIRDER | `` |
| D36 | D36.CUTPART.GIRDER | `` |
| D37 | D37.CUTPART.GIRDER | `` |
| D38 | D38.CUTPART.GIRDER | `P8` |
| D39 | D39.CUTPART.GIRDER | `P8` |
| D40 | D40.CUTPART.GIRDER | `` |
| D41 | D41.CUTPART.GIRDER | `` |
| D42 | D42.CUTPART.GIRDER | `` |
| D43 | D43.CUTPART.GIRDER | `P8` |
| D44 | D44.CUTPART.COLUMN | `` |
| D45 | D45.CUTPART.COLUMN | `` |
| D46 | D46.CUTPART.COLUMN | `5.00+P56` |
| D47 | D47.CUTPART.COLUMN | `5.00+P56` |
| D48 | D48.CUTPART.COLUMN | `(P8-2)*tan(P7)+D5+P56` |
| D49 | D49.CUTPART.COLUMN | `` |
| D50 | D50.CUTPART.COLUMN | `` |
| D51 | D51.CUTPART.COLUMN | `5.00+P55` |
| D52 | D52.CUTPART.COLUMN | `5.00+P55` |
| D53 | D53.CUTPART.COLUMN | `(P8-2)*tan(P7)+D5+P55` |
| D54 | D54.PLATE.GIRDER | `P6/2+20` |
| D55 | D55.PLATE.GIRDER | `D54` |
| D56 | D56.PLATE.GIRDER | `P44` |
| D57 | D57.PLATE.GIRDER | `P45` |
| D58 | D58.CUTPART.GIRDER | `` |
| D59 | D59.CUTPART.GIRDER | `` |
| D60 | D60.CUTPART.GIRDER | `` |
| D61 | D61.CUTPART.GIRDER | `` |
| D62 | D62.CUTPART.GIRDER | `` |
| D63 | D63.CUTPART.GIRDER | `` |
| D64 | D64.CUTPART.GIRDER | `` |
| D65 | D65.CUTPART.GIRDER | `` |
| D66 | D66.CUTPART.PLATE | `` |
| D67 | D67.CUTPART.PLATE | `` |
| D68 | D68.CUTPART.PLATE | `(P55+35)` |
| D69 | D69.CUTPART.PLATE | `(P55+35)` |
| D70 | D70.CUTPART.PLATE | `` |
| D71 | D71.CUTPART.PLATE | `` |
| D72 | D72.CUTPART.PLATE | `35.00+P56` |
| D73 | D73.CUTPART.PLATE | `35.00+P56` |
| D74 | D74.PLATE.COLUMN | `-P56` |
| D75 | D75.PLATE.COLUMN | `-P56` |
| D76 | D76.PLATE.COLUMN | `-P55` |
| D77 | D77.PLATE.COLUMN | `-P55` |
| D78 | D78.PLATE.GIRDER | `` |
| D79 | D79.PLATE.GIRDER | `` |
| D80 | D80.PLATE.GIRDER | `` |
| D81 | D81.PLATE.GIRDER | `` |
| D82 | D82.CUTPART.GIRDER | `` |
| D83 | D83.CUTPART.GIRDER | `P58+35.00` |
| D84 | D84.CUTPART.GIRDER | `P58+35.00` |
| D85 | D85.CUTPART.GIRDER | `` |
| D86 | D86.CUTPART.GIRDER | `P59+35.00` |
| D87 | D87.CUTPART.GIRDER | `` |
| D88 | D88.CUTPART.GIRDER | `` |
| D89 | D89.CUTPART.GIRDER | `P59+35.00` |
| D90 | D90.CUTPART.GIRDER | `` |
| D91 | D91.CUTPART.GIRDER | `(P8-2)` |
| D92 | D92.CUTPART.GIRDER | `` |
| D93 | D93.CUTPART.FITTING | `-P55` |
| D94 | D94.CUTPART.FITTING | `-P55` |
| D95 | D95.PLATE.FITTING | `-P55` |
| D96 | D96.PLATE.FITTING | `(P28+P55)/2` |
| D97 | D97.PLATE.FITTING | `P28+P55` |
| D98 | D98.PLATE.FITTING | `P28+P55` |
| D99 | D99.PLATE.FITTING | `-P55` |
| D100 | D100.CUTPART.FITTING | `(P8-2)+P55` |
| D101 | D101.PLATE.COLUMN | `P29/2` |
| D102 | D102.PLATE.COLUMN | `P29/2` |
| D103 | D103.PLATE.COLUMN | `P29/2` |
| D104 | D104.PLATE.COLUMN | `P29/2` |
| D105 | D105.PLATE.GIRDER | `` |
| D106 | D106.PLATE.GIRDER | `` |
| D107 | D107.PLATE.GIRDER | `` |
| D108 | D108.PLATE.GIRDER | `` |
| D109 | D109.PLATE.GIRDER | `` |
| D110 | D110.CUTPART.GIRDER | `` |
| D111 | D111.CUTPART.GIRDER | `(P8-2)` |
| D112 | D112.CUTPART.GIRDER | `` |
| D113 | D113.PLATE.COLUMN | `` |
| D114 | D114.PLATE.GIRDER | `P35` |
| D115 | D115.PLATE.GIRDER | `` |
| D116 | D116.CUTPART.FITTING | `-P55` |
| D117 | D117.CUTPART.FITTING | `-P55` |
| D118 | D118.CUTPART.FITTING | `(P8-2)+P55` |
| D119 | D119.PLATE.GIRDER | `` |
| D120 | D120.PLATE.FITTING | `-P55` |
| D121 | D121.PLATE.FITTING | `(P28+P55)/2` |
| D122 | D122.PLATE.COLUMN | `` |
| D123 | D123.PLATE.GIRDER | `` |
| D124 | D124.PLATE.GIRDER | `` |
| D125 | D125.PLATE.FITTING | `P28+P55` |
| D126 | D126.PLATE.FITTING | `P28+P55` |
| D127 | D127.PLATE.FITTING | `-P55` |
| D128 | D128.PLATE.GIRDER | `` |
| D129 | D129.PLATE.GIRDER | `` |
| D130 | D130.PLATE.GIRDER | `P35` |
| D131 | D131.PLATE.GIRDER | `` |
| D132 | D132.PLATE.GIRDER | `` |
| D133 | D133.CUTPART.GIRDER | `` |
| D134 | D134.CUTPART.GIRDER | `` |
| D135 | D135.CUTPART.GIRDER | `(P8-2)` |
| D136 | D136.CUTPART.GIRDER | `` |
| D137 | D137.CUTPART.GIRDER | `(P8-2)` |
| D138 | D138.CUTPART.GIRDER | `` |
| D139 | D139.CUTPART.GIRDER | `(P8-2)` |
| D140 | D140.CUTPART.GIRDER | `` |
| D141 | D141.CUTPART.GIRDER | `` |
| D142 | D142.CUTPART.GIRDER | `` |
| D143 | D143.CUTPART.GIRDER | `` |
| D144 | D144.CUTPART.GIRDER | `(P8-2)` |
| D145 | D145.CUTPART.PLATE | `` |
| D146 | D146.CUTPART.PLATE | `` |
| D147 | D147.CUTPART.PLATE | `` |
| D148 | D148.CUTPART.PLATE | `` |
| D149 | D149.CUTPART.PLATE | `` |
| D150 | D150.CUTPART.PLATE | `` |
| D151 | D151.PLATE.GIRDER | `` |
| D152 | D152.PLATE.GIRDER | `` |
| D153 | D153.PLATE.GIRDER | `` |
| D154 | D154.PLATE.GIRDER | `` |
| D155 | D155.PLATE.GIRDER | `` |
| D156 | D156.PLATE.COLUMN | `` |
| D157 | D157.PLATE.COLUMN | `` |
| D158 | D158.PLATE.COLUMN | `P55+20` |
| D159 | D159.PLATE.COLUMN | `P55+20` |
| D160 | D160.PLATE.GIRDER | `` |
| D161 | D161.PLATE.GIRDER | `` |
| D162 | D162.PLATE.GIRDER | `P60+10` |
| D163 | D163.PLATE.GIRDER | `P60+10` |
| D164 | D164.PLATE.COLUMN | `` |
| D165 | D165.PLATE.COLUMN | `` |
| D166 | D166.PLATE.COLUMN | `P56+20` |
| D167 | D167.PLATE.COLUMN | `P56+20` |
| D168 | D168.PLATE.GIRDER | `P61+10` |
| D169 | D169.PLATE.GIRDER | `` |
| D170 | D170.PLATE.GIRDER | `` |
| D171 | D171.PLATE.GIRDER | `P61+10` |
| D172 | D172.PLATE.GIRDER | `` |
| D173 | D173.PLATE.GIRDER | `` |
| D174 | D174.PLATE.GIRDER | `` |
| D175 | D175.PLATE.GIRDER | `` |
| D176 | D176.CUTPART.CGPPLATE | `` |
| D177 | D177.CUTPART.CGPPLATE | `` |
| D178 | D178.CUTPART.CGPPLATE | `` |
| D179 | D179.CUTPART.CGPPLATE | `` |
| D180 | D180.CUTPART.COLUMN | `` |
| D181 | D181.CUTPART.COLUMN | `P55` |
| D182 | D182.CUTPART.COLUMN | `P55` |
| D183 | D183.CUTPART.COLUMN | `` |
| D184 | D184.CUTPART.GIRDER | `` |
| D185 | D185.CUTPART.GIRDER | `` |
| D186 | D186.CUTPART.GIRDER | `P60+10` |
| D187 | D187.CUTPART.GIRDER | `P60+10` |
| D188 | D188.CUTPART.CGPPLATE | `` |
| D189 | D189.CUTPART.CGPPLATE | `` |
| D190 | D190.CUTPART.CGPPLATE | `` |
| D191 | D191.CUTPART.CGPPLATE | `` |
| D192 | D192.CUTPART.GIRDER | `` |
| D193 | D193.CUTPART.GIRDER | `` |
| D194 | D194.CUTPART.GIRDER | `P61+10` |
| D195 | D195.CUTPART.GIRDER | `P61+10` |
| D196 | D196.CUTPART.COLUMN | `P56` |
| D197 | D197.CUTPART.COLUMN | `P56` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#5880 | CREATION | `P50` |
| obj#5880 | MATERIAL | `P37` |
| obj#5880 | PROFILE | `"PL"+P8` |
| obj#5880 | SERIE | `P54+"GWP"` |
| obj#5880 | STARTNUMBER | `P53` |
| obj#5892 | CREATION | `P51` |
| obj#5892 | MATERIAL | `P37` |
| obj#5892 | PROFILE | `"PL"+P8` |
| obj#5892 | SERIE | `P54+"GWP"` |
| obj#5892 | STARTNUMBER | `P53` |
| obj#5901 | MATERIAL | `P13` |
| obj#5901 | PROFILE | `"PL"+P17` |
| obj#5901 | SERIE | `P12+"CGP"` |
| obj#5901 | STARTNUMBER | `P52` |
| obj#5911 | CREATION | `P14` |
| obj#5911 | SERIE | `P54+"FB"` |
| obj#5922 | CREATION | `P14` |
| obj#5922 | SERIE | `P54+"FB"` |
| obj#5936 | CREATION | `P63` |
| obj#5949 | CREATION | `P62` |
| obj#5959 | CREATION | `P14` |
| obj#5969 | CREATION | `P14` |
| obj#5979 | CREATION | `P14` |
| obj#5988 | CREATION | `P14` |
| obj#5988 | PROFILE | `P6+P23` |
| obj#5999 | CREATION | `P14` |
| obj#5999 | PROFILE | `P6+P23` |
| obj#6016 | PROFILE | `P28+100` |
| obj#6036 | PROFILE | `P28+100` |
| obj#6045 | CREATION | `P65` |
| obj#6055 | CREATION | `P64` |
| obj#6065 | BOLT_GROUP_DISTY | `P22` |
| obj#6065 | DX1 | `P4` |
| obj#6065 | SIZE | `P40_diameter` |
| obj#6065 | SLOTTED_HOLE_PARTS | `P41` |
| obj#6065 | SLOTTED_HOLE_X | `P27` |
| obj#6065 | STANDARD | `P40_screwdin` |
| obj#6065 | TOLERANCE | `P26` |

## Preset `+101-B4.p_0-DM_S-Col2Gir_TANG_DUONG`
```
joint_attributes.saveas_file "+101-B4"
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
joint_attributes.P65 0
joint_attributes.P64 1
joint_attributes.P63 0
joint_attributes.P62 1
joint_attributes.P60 80.000000
joint_attributes.P55 21.000000
joint_attributes.P54 "B4"
joint_attributes.P50 0
joint_attributes.P47 140.000000
joint_attributes.P40_screwdin "T.S.B"
joint_attributes.P40_diameter 20.000000
joint_attributes.P14 1
joint_attributes.P13 "SM355"
joint_attributes.P12 "B4"
joint_attributes.P11 0
joint_attributes.P4 60.000000
```

## Preset `28-180.p_0-DM_S-Col2Gir_TANG_DUONG`
```
joint_attributes.saveas_file "28-180"
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
joint_attributes.P61 180.000000
joint_attributes.P60 180.000000
joint_attributes.P58 0.000000
joint_attributes.P56 28.000000
joint_attributes.P55 28.000000
joint_attributes.P54 "1"
joint_attributes.P47 150.000000
joint_attributes.P21B 700.000000
joint_attributes.P40_screwdin "T.S.B"
joint_attributes.P40_diameter 20.000000
joint_attributes.P37 "SM355"
joint_attributes.P24 22.000000
joint_attributes.P14 1
joint_attributes.P13 "SM355"
joint_attributes.P12 "1"
joint_attributes.P11 0
joint_attributes.P4 70.000000
```