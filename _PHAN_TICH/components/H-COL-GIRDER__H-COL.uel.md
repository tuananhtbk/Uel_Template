# 0-DM_H-Col2Gir_H-COLUMN

- File: `H-COL-GIRDER\H-COL.uel`  (29,842 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	0-DM_H-Col2Gir_H-COLUMN	All`
- Dialog parameters: 13 | Variables (P/custom): 75 | Distance vars (D): 135 | Parts with bound profile: 3
- Lookup files (fVF): YGY-H_COL2GIR-운용층-H-COL.txt
- Functions used: cos, fAD, fP, fVF, if, max, mid, sin, tan
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "0-DM_H-Col2Gir_H-COLUMN")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("BOLT to BEAM FLG.", "P4", distance, number, 1)
           parameter("CGP방향", "P11", YesNo, number, 2)
           parameter("CGP-ZONE", "P12", string, text, 3)
           parameter("CGP MAT'L", "P13", material, text, 4)
           parameter("BOLT_PITCH--------------", "P22", string, text, 5)
           parameter("개선여장", "P23", distance, number, 6)
           parameter("SLOT_HOLE길이---------", "P24", distance, number, 7)
           parameter("BOLT_SIZE", "P40_diameter", bolt_size, number, 8)
           parameter("BOLT_STANDARD", "P40_screwdin", bolt_standard, text, 9)
           parameter("CWS-SPS-EDGE (Bolt To GP/y)", "P21A", distance, number, 10)
           parameter("CWS_SPS", "P21B", distance, number, 11)
           parameter("GUSSET-START NO", "P52", integer, number, 12)
           parameter("L3-Auto--------------", "P69", distance, number, 13)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Length | `fP(proS,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P2 | Parameter2 | Text | `fP(proPROFILE,"ID67176807-0000-99D1-3137-323935393037")` *(formula)* |
| P3 | Parameter3 | Length | `fVF("YGY-H_COL2GIR-운용층-H-COL.txt",P15,4)` *(formula)* |
| P4 | BOLT to BEAM FLG. | Length | `55.00` |
| P5 | BOLT_PITCH | Length | `320.00` |
| P6 | Parameter6 | Length | `fP(proB,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P7 | Parameter7 | Angle | `45.0000` |
| P8 | Parameter8 | Length | `fP(proT,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P9 | PLATE방향(1&-1) | Length | `if (P11==0) then 1 else -1 endif` *(formula)* |
| P10 | Parameter10 | Length | `if (P9==1) then 10000 else 1000 endif` *(formula)* |
| P11 | CGP방향 | Yes/No | `0` |
| P13 | CGP MAT'L | Material | `SM355` |
| P14 | MO(YES)_PIN(NO) | Yes/No | `1` |
| P15 | GIR_MEMBER | Text | `fP(proPROFILE,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P16 | Parameter16 | Text | `fVF("YGY-H_COL2GIR-운용층-H-COL.txt",P15,1)` *(formula)* |
| P17 | Parameter17 | Integer | `fVF("YGY-H_COL2GIR-운용층-H-COL.txt",P15,4)` *(formula)* |
| P18 | Parameter18 | Integer | `fVF("YGY-H_COL2GIR-운용층-H-COL.txt",P15,7)` *(formula)* |
| P19 | Parameter19 | Length | `fVF("YGY-H_COL2GIR-운용층-H-COL.txt",P15,3)` *(formula)* |
| P20 | Parameter20 | Integer | `fP(proH,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P21 | Parameter21 | Length | `if (P20>=P21B) then ((P19-P21A)/2) else (P19-P21A) endif` *(formula)* |
| P21A | CWS-SPS-EDGE (Bolt To GP/y) | Length | `80.00` |
| P21B | CWS_SPS | Length | `900.00` |
| P22 | BOLT_PITCH-------------- | Text | `if (P18==0) then 0 else (P18+"*"+P21) endif` *(formula)* |
| P23 | 개선여장 | Length | `400.00` |
| P24 | SLOT_HOLE길이--------- | Length | `if (P40_diameter==16) then 40 else if (P40_diameter==20) then 50 else if (P40_diameter==22) then 55 else if (P40_diameter==24) then 60 else if (P40_diameter==27) then 67 else endif endif endif endif endif` *(formula)* |
| P25 | Parameter25 | Length | `P40_diameter` *(formula)* |
| P26 | Parameter26 | Length | `if (P40_diameter==16) then 2 else if (P40_diameter==20) then 2 else if (P40_diameter==22) then 2 else if (P40_diameter==24) then 3 else if (P40_diameter==27) then 3 else endif endif endif endif endif` *(formula)* |
| P27 | Parameter27 | Length | `P24-P25-P26` *(formula)* |
| P28 | WING_PL_길이----------- | Length | `fVF("YGY-H_COL2GIR-운용층-H-COL.txt",P15,5)` *(formula)* |
| P29 | COL_WIDTH | Length | `fP(proB,"ID67176807-0000-997B-3137-323935393037")` *(formula)* |
| P30 | GIR_WIDTH | Length | `fP(proB,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P31 | Parameter31 | Length | `P29-P30` *(formula)* |
| P32 | WING_PL_유무 | Yes/No | `if (P31>=150) then 1 else 0 endif` *(formula)* |
| P33 | Parameter33 | Length | `0.00` |
| P34 | GIRDER_MAT | Material | `fP(proMATERIAL,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P35 | Parameter35 | Length | `if (P31>=150) then 40 else P36 endif` *(formula)* |
| P36 | Parameter36 | Length | `P31/2/2` *(formula)* |
| P37 | WING_MAT'L--------------- | Material | `if (P34=="SHN355") then "SN355" else if (P34=="SHN275") then "SN275" else P34 endif endif` *(formula)* |
| P38 | Parameter38 | Text | `fP(proASSEMBLYSERIE,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P39 | START_NO | Integer | `fP(proASSEMBLYSTARTNUMBER,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P40_diameter | BOLT_SIZE | 6 | `24.00` |
| P40_screwdin | BOLT_STANDARD | 7 | `T.S.B` |
| P41 | Parameter41 | Length | `if (P11==0) then 10000 else 20000 endif` *(formula)* |
| P42 | WING1_유무 | Yes/No | `0` |
| P43 | WING2_유무 | Yes/No | `0` |
| P44 | Parameter44 | Length | `194.474416260411` |
| P45 | Parameter45 | Length | `P44` *(formula)* |
| P46 | Parameter46 | Length | `100.00` |
| P47 | L3-Auto-------------- | Length | `fVF("YGY-H_COL2GIR-운용층-H-COL.txt",P15,2)` *(formula)* |
| P48 | Parameter48 | Integer | `0` |
| P49 | Parameter49 | Integer | `0` |
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
| P66 | Parameter66 | Angle | `fAD("skew","ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P67 | Parameter67 | Length | `0.5*P17*sin(P66)` *(formula)* |
| P68 | Parameter68 | Length | `P47*cos(P66)` *(formula)* |
| P69 | L3-Auto-------------- | Length | `P67+P68` *(formula)* |
| P70 | Parameter70 | Text | `fP(proSERIE,"ID67176807-0000-997B-3137-323935393037")` *(formula)* |
| P71 | Parameter71 | Text | `fP(proSERIE,"ID67176807-0000-997F-3137-323935393037")` *(formula)* |
| P72 | Parameter72 | Text | `mid(P70,0,find(P70,"M"))` *(formula)* |
| P73 | PREFIX | Text | `mid(P71,0,find(P71,"M"))` *(formula)* |

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
| D56 | D56.PLATE.GIRDER | `P6/2+20` |
| D57 | D57.PLATE.GIRDER | `P6/2+20` |
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
| D123 | D123.PLATE.GIRDER | `` |
| D124 | D124.PLATE.GIRDER | `` |
| D128 | D128.PLATE.GIRDER | `` |
| D129 | D129.PLATE.GIRDER | `` |
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
| obj#275 | MATERIAL | `P13` |
| obj#275 | PROFILE | `"PL"+P17` |
| obj#275 | SERIE | `P12+"CGP"` |
| obj#275 | STARTNUMBER | `P52` |
| obj#287 | CREATION | `P14` |
| obj#287 | SERIE | `P54+"FB"` |
| obj#298 | CREATION | `P14` |
| obj#298 | SERIE | `P54+"FB"` |
| obj#305 | CREATION | `P14` |
| obj#317 | CREATION | `P14` |
| obj#336 | CREATION | `P63` |
| obj#346 | CREATION | `P62` |
| obj#355 | CREATION | `P14` |
| obj#364 | CREATION | `P14` |
| obj#364 | PROFILE | `P6+P23` |
| obj#375 | CREATION | `P14` |
| obj#375 | PROFILE | `P6+P23` |
| obj#382 | CREATION | `P65` |
| obj#393 | CREATION | `P64` |
| obj#403 | BOLT_GROUP_DISTY | `P22` |
| obj#403 | DX1 | `P4` |
| obj#403 | SIZE | `P40_diameter` |
| obj#403 | SLOTTED_HOLE_PARTS | `P41` |
| obj#403 | SLOTTED_HOLE_X | `P27` |
| obj#403 | STANDARD | `P40_screwdin` |
| obj#403 | TOLERANCE | `P26` |