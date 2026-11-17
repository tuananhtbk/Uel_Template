# 0-DM-PIN MOMENT-V2

- File: `B-TO-TG\PIN-MOMENT.uel`  (40,204 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	0-DM-PIN MOMENT-V2	All`
- Dialog parameters: 0 | Variables (P/custom): 104 | Distance vars (D): 156 | Parts with bound profile: 7
- Lookup files (fVF): BEAM_TO_TG.txt
- Functions used: cos, fAD, fP, fVF, if, mid, sin, tan
- Dialog bitmaps: 0 | Saved presets: +000.p_0-DM-PIN MOMENT-V2, +104.p_0-DM-PIN MOMENT-V2, +94 B3.p_0-DM-PIN MOMENT-V2, +AKC.p_0-DM-PIN MOMENT-V2, +SAMPLE.p_0-DM-PIN MOMENT-V2, B1104.p_0-DM-PIN MOMENT-V2

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "0-DM-PIN MOMENT-V2")
   {
       tab_page("", "PLATE", 1)
        {
            parameter("", "P86", integer, number, 621, 0, 160)
            parameter("", "FB", distance, number, 960, 2, 160)
            attribute("", "FB_THK.", label, "%s", none, none, "0", "0", 826, 4)
            attribute("", "START NUMBER", label, "%s", none, none, "0", "0", 398, 7)
            parameter("", "P7", string, text, 190, 10, 160)
            attribute("", "GP_Pos", label, "%s", none, none, "0", "0", 28, 17)
            parameter("", "P71", material, text, 960, 35, 160)
            parameter("", "P10", material, text, 624, 40, 160)
            attribute("", "Material (XXX)", label, "%s", none, none, "0", "0", 401, 41)
            attribute("", "FB_MAT", label, "%s", none, none, "0", "0", 833, 42)
            parameter("", "P8", string, text, 189, 51, 160)
            attribute("", "SF_Pos", label, "%s", none, none, "0", "0", 29, 55)
            parameter("", "P58", YesNo, number, 958, 67, 160)
            attribute("", "(Y/N)", label, "%s", none, none, "0", "0", 842, 69)
            parameter("", "P15", YesNo, number, 622, 72, 160)
            attribute("", "Gusset_L_or_R", label, "%s", none, none, "0", "0", 404, 75)
            parameter("", "P9", string, text, 190, 87, 160)
            attribute("", "FB_Pos", label, "%s", none, none, "0", "0", 20, 89)
            attribute("", "Gusset class", label, "%s", none, none, "0", "0", 408, 104)
            parameter("", "P59", integer, number, 622, 105, 160)
            attribute("", "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", label, "%s", none, none, "0", "0", 26, 137)
            parameter("", "P72", distance, number, 1664, 164, 160)
            parameter("", "P1", distance, number, 210, 165, 160)
            attribute("", "PL5_Thk", label, "%s", none, none, "0", "0", 1538, 165)
            parameter("", "P4", distance, number, 1272, 167, 160)
            parameter("", "P3", distance, number, 921, 168, 160)
            attribute("", "PL1 THK", label, "%s", none, none, "0", "0", 22, 169)
            parameter("", "P2", distance, number, 570, 169, 160)
            attribute("", "PL2_Thk", label, "%s", none, none, "0", "0", 446, 170)
            attribute("", "PL3_Thk", label, "%s", none, none, "0", "0", 800, 171)
            attribute("", "PL4_Thk", label, "%s", none, none, "0", "0", 1141, 171)
            parameter("", "P73", YesNo, number, 1665, 196, 160)
            attribute("", "(Y/N)", label, "%s", none, none, "0", "0", 1562, 198)
            parameter("", "P63", chamfer_type, number, 213, 200, 160)
            attribute("", "(Y/N)", label, "%s", none, none, "0", "0", 476, 202)
            parameter("", "P11", YesNo, number, 572, 203, 160)
            attribute("", "(Y/N)", label, "%s", none, none, "0", "0", 825, 203)
            parameter("", "P12", YesNo, number, 921, 203, 160)
            attribute("", "(Y/N)", label, "%s", none, none, "0", "0", 1172, 203)
            parameter("", "P13", YesNo, number, 1273, 203, 160)
            attribute("", "Top chamfer (out)", label, "%s", none, none, "0", "0", 22, 212)
            attribute("", "PL5 to WEB BEAM", label, "%s", none, none, "0", "0", 1462, 229)
            parameter("", "P75", distance, number, 1666, 230, 160)
            parameter("", "P64", distance, number, 212, 234, 160)
            parameter("", "P50", chamfer_type, number, 570, 237, 160)
            attribute("", "Chamfer Type", label, "%s", none, none, "0", "0", 410, 238)
            attribute("", "Offset", label, "%s", none, none, "0", "0", 821, 238)
            parameter("", "P14", distance, number, 1274, 238, 160)
            attribute("", "X", label, "%s", none, none, "0", "0", 137, 240)
            parameter("", "P21", distance, number, 921, 240, 160)
            attribute("", "Offset", label, "%s", none, none, "0", "0", 1173, 242)
            parameter("", "P22", distance, number, 921, 269, 160)
            parameter("", "P51", distance, number, 572, 270, 160)
            attribute("", "Length", label, "%s", none, none, "0", "0", 814, 271)
            parameter("", "P76", distance, number, 1276, 272, 160)
            parameter("", "P65", distance, number, 213, 274, 160)
            attribute("", "X", label, "%s", none, none, "0", "0", 501, 274)
            attribute("", "Y", label, "%s", none, none, "0", "0", 134, 275)
            attribute("", "PL4 to FLG.", label, "%s", none, none, "0", "0", 1134, 281)
            attribute("", "Offset", label, "%s", none, none, "0", "0", 474, 303)
            parameter("", "P19", distance, number, 569, 304, 160)
            attribute("", "(Up/Dw)", label, "%s", none, none, "0", "0", 810, 304)
            parameter("", "P69", distance, number, 925, 303, 160)
            parameter("", "P33", chamfer_type, number, 1277, 311, 160)
            parameter("", "P77", chamfer_type, number, 214, 312, 160)
            attribute("", "Chamfer", label, "%s", none, none, "0", "0", 1158, 314)
            attribute("", "Top chamfer (ins)", label, "%s", none, none, "0", "0", 18, 316)
            parameter("", "P78", distance, number, 214, 340, 160)
            parameter("", "P49", distance, number, 1277, 340, 160)
            attribute("", "X", label, "%s", none, none, "0", "0", 133, 343)
            parameter("", "P70", distance, number, 974, 343, 160)
            attribute("", "X", label, "%s", none, none, "0", "0", 1214, 344)
            parameter("", "P52", chamfer_type, number, 572, 348, 160)
            attribute("", "PL 3 to WEB BEAM", label, "%s", none, none, "0", "0", 774, 349)
            attribute("", "r Type", label, "%s", none, none, "0", "0", 464, 350)
            parameter("", "P79", distance, number, 214, 368, 160)
            attribute("", "Y", label, "%s", none, none, "0", "0", 133, 370)
            parameter("", "P53", distance, number, 573, 375, 160)
            attribute("", "x", label, "%s", none, none, "0", "0", 500, 377)
            parameter("", "P80", distance, number, 974, 377, 160)
            attribute("", "GAP CUTTING", label, "%s", none, none, "0", "0", 776, 383)
            parameter("", "P66", chamfer_type, number, 214, 402, 160)
            attribute("", "Bott chamfer (out)", label, "%s", none, none, "0", "0", 22, 405)
            parameter("", "P82", YesNo, number, 978, 407, 160)
            parameter("", "P34", distance, number, 572, 411, 160)
            attribute("", "CUT TOP (Y/N)", label, "%s", none, none, "0", "0", 777, 410)
            attribute("", "PL2 to Web Gi", label, "%s", none, none, "0", "0", 402, 417)
            parameter("", "P83", YesNo, number, 978, 435, 160)
            attribute("", "CUT BOTT (Y/N)", label, "%s", none, none, "0", "0", 778, 436)
            parameter("", "P67", distance, number, 214, 440, 160)
            attribute("", "X", label, "%s", none, none, "0", "0", 133, 441)
            parameter("", "P68", distance, number, 214, 468, 160)
            attribute("", "Y", label, "%s", none, none, "0", "0", 134, 469)
            attribute("", "Bott chamfer (ins)", label, "%s", none, none, "0", "0", 16, 502)
            parameter("", "P87", chamfer_type, number, 214, 502, 160)
            attribute("", "X", label, "%s", none, none, "0", "0", 134, 536)
            parameter("", "P88", distance, number, 216, 537, 160)
            attribute("", "Y", label, "%s", none, none, "0", "0", 136, 564)
            parameter("", "P89", distance, number, 216, 565, 160)
            parameter("", "P20", distance, number, 214, 602, 160)
            attribute("", "PL1_Offset", label, "%s", none, none, "0", "0", 37, 605)
        }
        tab_page("", "BOLT", 2)
        {
            parameter("", "P23", distance, number, 870, 27, 160)
            attribute("", "Gap_1(Gi_Flg Beam)", label, "%s", none, none, "0", "0", 618, 31)
            attribute("", "Tack weld (Y/N)", label, "%s", none, none, "0", "0", 42, 35)
            parameter("", "P32", YesNo, number, 397, 35, 160)
            parameter("", "P26", YesNo, number, 870, 54, 160)
            attribute("", "Beam web scallop", label, "%s", none, none, "0", "0", 618, 60)
            attribute("", "Bolt to web beam", label, "%s", none, none, "0", "0", 42, 63)
            parameter("", "P35", distance, number, 397, 63, 160)
            parameter("", "P27", chamfer_type, number, 870, 82, 160)
            attribute("", "Beam Chamfer Type", label, "%s", none, none, "0", "0", 618, 87)
            attribute("", "Bolt Q'ty x", label, "%s", none, none, "0", "0", 42, 91)
            parameter("", "P37", integer, number, 397, 91, 160)
            parameter("", "P28", distance, number, 870, 111, 160)
            parameter("", "P38", distance, number, 400, 117, 160)
            attribute("", "Beam Chamfer X", label, "%s", none, none, "0", "0", 620, 116)
            attribute("", "Bolt distance x", label, "%s", none, none, "0", "0", 42, 118)
            parameter("", "P29", distance, number, 870, 140, 160)
            attribute("", "Beam Chamfer Y", label, "%s", none, none, "0", "0", 618, 144)
            parameter("", "P40", integer, number, 400, 145, 160)
            attribute("", "Bolt Q'ty y", label, "%s", none, none, "0", "0", 42, 147)
            parameter("", "P30", distance, number, 870, 167, 160)
            attribute("", "Bolt distance y", label, "%s", none, none, "0", "0", 45, 174)
            parameter("", "P41", distance, number, 400, 174, 160)
            attribute("", "Tack Gap", label, "%s", none, none, "0", "0", 617, 174)
            parameter("", "P31", angle, number, 870, 194, 160)
            attribute("", "Top beam tack angle", label, "%s", none, none, "0", "0", 618, 200)
            attribute("", "Bolt To Gusset Plate", label, "%s", none, none, "0", "0", 45, 202)
            parameter("", "P44", distance, number, 400, 202, 160)
            parameter("", "P48_diameter", bolt_size, number, 870, 235, 160)
            attribute("", "Bolt size", label, "%s", none, none, "0", "0", 628, 237)
            attribute("", "Bolt to Top plate (ev1)", label, "%s", none, none, "0", "0", 45, 241)
            parameter("", "P60", distance, number, 400, 241, 160)
            parameter("", "P48_screwdin", bolt_standard, text, 870, 263, 160)
            attribute("", "Bolt standard", label, "%s", none, none, "0", "0", 628, 265)
            attribute("", "Bolt to Bott plate (ev2)", label, "%s", none, none, "0", "0", 45, 269)
            parameter("", "P61", distance, number, 400, 269, 160)
            attribute("", "Center Bolt to Beam", label, "%s", none, none, "0", "0", 45, 296)
            parameter("", "P62", distance, number, 400, 296, 160)
            parameter("", "P55", distance, number, 874, 312, 160)
            attribute("", "Tolerance", label, "%s", none, none, "0", "0", 632, 315)
            parameter("", "P56", distance, number, 874, 341, 160)
            attribute("", "Slotted hole X", label, "%s", none, none, "0", "0", 632, 343)
            parameter("", "P57", distance, number, 874, 368, 160)
            attribute("", "Slotted hole Y", label, "%s", none, none, "0", "0", 632, 370)
        }
    }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | PLATE_1 | Length | `P94` *(formula)* |
| P2 | PLATE_2 | Length | `P94` *(formula)* |
| P3 | PLATE_3 | Length | `P95` *(formula)* |
| P4 | PLATE_4 | Length | `P94` *(formula)* |
| P5 | FLAT_BAR Width | Length | `25.00` |
| P6 | FLAT_BAR PROFILE | Profile | `"FB"+P5+"*"+FB` *(formula)* |
| P7 | Gusset_Pos | Text | `P98+"GP"` *(formula)* |
| P8 | Stiff_Pos | Text | `P98+"SF"` *(formula)* |
| P9 | Flat_bar_Pos | Text | `P100+"FB"` *(formula)* |
| P10 | Material (XXX) | Material | `P101` *(formula)* |
| P11 | PLATE_2(Y/N) | Yes/No | `1` |
| P12 | PLATE_3(Y/N) | Yes/No | `1` |
| P13 | PLATE_4(Y/N) | Yes/No | `1` |
| P14 | PLATE_4 Offset | Length | `0.00` |
| P15 | Gusset_L_or_R | Yes/No | `1` |
| P16 | Web Beam+Gusset | Length | `fP(proS,"ID67174322-0000-3715-3137-323935383235")+P1` *(formula)* |
| P17 | 0.5*Web Beam+Gusset | Length | `if(P15==1) then 0.5*P16 else -0.5*P16 endif` *(formula)* |
| P18 | Beam Slope | Angle | `fAD("slope","ID67174322-0000-3715-3137-323935383235")` *(formula)* |
| P19 | PL2_Offset | Length | `10.00` |
| P20 | PL1_Offset | Length | `10.00` |
| P21 | PL3_Offset | Length | `P22/2` *(formula)* |
| P22 | PL3-Length | Length | `fP(proB,"ID67174322-0000-3715-3137-323935383235")` *(formula)* |
| P23 | Gap_1(Gi_Flg Beam) | Length | `5.00` |
| P24 | Gap web to flg beam | Length | `10.00` |
| P25 | Gap web to flg beam-X | Length | `P24*cos(P18)` *(formula)* |
| P26 | beam web scallop | Yes/No | `1` |
| P27 | Beam Chamfer Type | Chamfer | `` |
| P28 | Beam Chamfer X | Length | `50.00` |
| P29 | Beam Chamfer Y | Length | `50.00` |
| P30 | Tack Gap | Length | `2.00` |
| P31 | Top beam tack angle | Angle | `45.0000` |
| P32 | Tack weld (Y/N) | Yes/No | `0` |
| P33 | Plate Chamfer Type | Chamfer | `` |
| P34 | Plate_2 to Web Gi | Length | `0.00` |
| P35 | Bolt to web beam | Length | `40.00` |
| P36 | Bolt to top beam | Length | `0.5*(fP(proH,"ID67174322-0000-3715-3137-323935383235")-P43)` *(formula)* |
| P37 | Bolt Q'ty x | Integer | `1` |
| P38 | Bolt distance x | Length | `60.00` |
| P39 | Group bolt x | 9 | `if(P37==1) then 0 else (P37-1)+"*"+P38 endif` *(formula)* |
| P40 | Bolt Q'ty y | Integer | `2` |
| P41 | Bolt distance y | Length | `P93-P60-P61` *(formula)* |
| P42 | Group bolt y | 9 | `if(P40==1) then 0 else (P40-1)+"*"+P41 endif` *(formula)* |
| P43 | bolt to bolt | Length | `(P40-1)*P41` *(formula)* |
| P44 | Bolt To Gusset Plate | Length | `60.00` |
| P45 | Parameter45 | 24 | `if P15==1 then 01000 else if P15==0 then 10000 else 00000 endif endif` *(formula)* |
| P46 | Parameter46 | Length | `fP(proH,"ID67174322-0000-3711-3137-323935383235")-fP(proH,"ID67174322-0000-3715-3137-323935383235")-fP(proT,"ID67174322-0000-3711-3137-323935383235")+P3+P69` *(formula)* |
| P47 | Bolt to web beam xxx | Length | `P35` *(formula)* |
| P48_diameter | Bolt size | 6 | `20.00` |
| P48_screwdin | Bolt standard | 7 | `T.S.B` |
| P49 | Plate Chamfer X | Length | `35.00` |
| P50 | Plate_2 Chamfer Type | Chamfer | `` |
| P51 | Plate_2 Chamfer X | Length | `35.00` |
| P52 | Plate_2 Chamfer Type | Chamfer | `0.000` |
| P53 | r | Length | `0.00` |
| P54 | Parameter54 | Integer | `if(P12==0) then 0 else P11 endif` *(formula)* |
| P55 | Tolerance | Length | `0.00` |
| P56 | Slotted hole X | Length | `0.00` |
| P57 | Slotted hole Y | Length | `0.00` |
| P58 | Flat bar(Y/N) | Yes/No | `0` |
| P59 | Gusset class | Integer | `P1` *(formula)* |
| P60 | Bolt to Top plate (ev1) | Length | `40.00` |
| P61 | Bolt to Bott plate (ev2) | Length | `40.00` |
| P62 | Center Bolt to Beam | Length | `0.00` |
| P63 | Plate 1 top chamfer type | Chamfer | `` |
| P64 | Plate 1 top chamfer X | Length | `0.00` |
| P65 | Plate 1 top chamfer Y | Length | `0.00` |
| P66 | Plate 1 bott chamfer type | Chamfer | `0.000` |
| P67 | Plate 1 bott chamfer X | Length | `0.00` |
| P68 | Plate 1 bott chamfer Y | Length | `0.00` |
| P69 | Plate 3 (Up/Dw) | Length | `0.00` |
| P70 | Plate 3 to WEB BEAM | Length | `0.00` |
| P71 | FB MAT | Material | `SS275` |
| P72 | PLATE_5 | Length | `20.00` |
| P73 | PLATE_5(Y/N) | Yes/No | `1` |
| P74 | Parameter74 | Length | `if(P73==1) then P72 else 0 endif` *(formula)* |
| P75 | PLATE 5 to WEB BEAM | Length | `30.00` |
| P76 | PLATE 4 Tto FLG. | Length | `10.00` |
| P77 | Plate 1 top chamfer type (insi | Chamfer | `0.00` |
| P78 | Plate 1 top chamfer X | Length | `0.00` |
| P79 | Plate 1 top chamfer Y | Length | `0.00` |
| P80 | PLATE 3 GAP CUTTING | Length | `12.00` |
| P81 | PLATE ANGLE CUTTING | Angle | `45.0000` |
| P82 | PLATE 3 CUTTING TOP (Y/N) | Yes/No | `0` |
| P83 | PLATE 3 CUTTING BOTT (Y/N) | Yes/No | `0` |
| P84 | Parameter84 | Length | `2*ceil(P3/3)` *(formula)* |
| P85 | Parameter85 | Length | `if(P82==1&&P83==1 ) then P84 else P80 endif` *(formula)* |
| P86 | START NUMBER | Integer | `1` |
| P87 | Plate 1 Bott chamfer type (ins | Chamfer | `0.00` |
| P88 | Plate 1 Bott chamfer X | Length | `0.00` |
| P89 | Plate 1 Bott chamfer Y | Length | `0.00` |
| P90 | Parameter90 | Text | `fP(proPROFILE,"ID67174322-0000-3715-3137-323935383235")` *(formula)* |
| P91 | Parameter91 | Text | `fVF("BEAM_TO_TG.txt",P90,1)` *(formula)* |
| P92 | Parameter92 | Integer | `fVF("BEAM_TO_TG.txt",P90,2)` *(formula)* |
| P93 | Parameter93 | Integer | `fVF("BEAM_TO_TG.txt",P90,3)` *(formula)* |
| P94 | Parameter94 | Integer | `fVF("BEAM_TO_TG.txt",P90,4)` *(formula)* |
| P95 | Parameter95 | Length | `fP(proT,"ID67174322-0000-3715-3137-323935383235")` *(formula)* |
| P96 | Parameter96 | Text | `fP(proSERIE,"ID67174322-0000-3711-3137-323935383235")` *(formula)* |
| P97 | Parameter97 | Text | `fP(proMATERIAL,"ID67174322-0000-3711-3137-323935383235")` *(formula)* |
| P98 | Parameter98 | Text | `mid(P96,0,find(P96,"M"))` *(formula)* |
| P99 | Parameter99 | Text | `fP(proSERIE,"ID67174322-0000-3715-3137-323935383235")` *(formula)* |
| P100 | Parameter100 | Text | `mid(P99,0,find(P99,"M"))` *(formula)* |
| P101 | Parameter101 | Text | `if(P97=="SHN355") then "SN355" else "SM355" endif` *(formula)* |
| FB | FLAT_BAR THK. | Length | `9.00` |
| K | K | Length | `0.5*P1*sin(P18)` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | PL4 | `` |
| D2 | PL4 | `` |
| D3 | PL4 | `` |
| D4 | PL4 | `` |
| D5 | PL4 | `P76` |
| D6 | PL4 | `P76` |
| D7 | PL4 | `` |
| D8 | PL4 | `` |
| D9 | PL4 Offset | `P14` |
| D10 | PL4 Offset | `P14` |
| D11 | PL4 Offset | `P14` |
| D12 | PL4 Offset | `P14` |
| D13 | PL2 | `` |
| D14 | PL2 | `` |
| D15 | PL2 | `-P69` |
| D16 | PL2 | `-P69` |
| D17 | PL2 | `if(P34 !=0) then P34 else K endif` |
| D18 | PL2 | `if(P34 !=0) then P34 else K endif` |
| D19 | PL2_to_Beam_Center | `P17` |
| D20 | PL2_to_Beam_Center | `P17` |
| D21 | PL2_to_Beam_Center | `P17` |
| D22 | PL2_to_Beam_Center | `P17` |
| D23 | PL1 to Gi | `P74` |
| D24 | PL1 to Gi | `P74` |
| D25 | D25.PLATE.BEAM | `if(P12==1) then P46 else 0 endif` |
| D26 | D26.PLATE.BEAM | `if(P12==1) then P46 else 0 endif` |
| D27 | PL1_to_Beam_Center | `P17` |
| D28 | PL1_to_Beam_Center | `P17` |
| D29 | PL1_to_Beam_Center | `P17` |
| D30 | PL1_to_Beam_Center | `P17` |
| D31 | PL1_to_Beam_Center | `P17` |
| D32 | PL1_to_Beam_Center | `P17` |
| D33 | PL1_to_Beam_Center | `P17` |
| D34 | PL1_to_Beam_Center | `P17` |
| D35 | PL1-K | `K` |
| D36 | PL1-K | `K` |
| D37 | PL2_Offset | `P19` |
| D38 | PL2_Offset | `P19` |
| D39 | PL1_Offset | `P20` |
| D40 | PL1_Offset | `P20` |
| D41 | PL1_Offset | `P20` |
| D42 | PL1_Offset | `-P20` |
| D43 | PL3 to Gi | `P70` |
| D44 | PL3 to Gi | `P70` |
| D45 | PL3 to Gi | `` |
| D46 | PL3 to Gi | `` |
| D47 | PL3 to BE | `0.5*P3+P69` |
| D48 | PL3 to BE | `0.5*P3+P69` |
| D49 | PL3 to BE | `0.5*P3+P69` |
| D50 | PL3 to BE | `0.5*P3+P69` |
| D51 | PL3_Offset X | `P21` |
| D52 | PL3_Offset X | `` |
| D53 | PL3_Offset X | `` |
| D54 | PL3-Length X | `P22` |
| D55 | PL3-Length X | `P22` |
| D56 | Gap_1(Gi_Flg Beam) xxx | `P23` |
| D57 | Flat bar | `` |
| D58 | Flat bar | `` |
| D59 | Flat bar | `` |
| D60 | Flat bar | `` |
| D61 | Flat bar | `` |
| D62 | Flat bar | `` |
| D63 | D63.CUTPART.BEAM | `` |
| D64 | D64.CUTPART.BEAM | `` |
| D65 | D65.CUTPART.BEAM | `P29` |
| D66 | D66.CUTPART.BEAM | `P29` |
| D67 | D67.CUTPART.BEAM | `` |
| D68 | D68.CUTPART.BEAM | `` |
| D69 | D69.CUTPART.BEAM | `` |
| D70 | D70.CUTPART.BEAM | `` |
| D71 | D71.CUTPART.BEAM | `` |
| D72 | D72.CUTPART.BEAM | `` |
| D73 | D73.CUTPART.BEAM | `P28` |
| D74 | D74.CUTPART.BEAM | `P28` |
| D75 | con'c beam | `` |
| D76 | con'c beam | `` |
| D77 | con'c beam | `` |
| D78 | Gus to web beam | `P35+P44+(P37-1)*P38` |
| D79 | D79.CUTPART.BEAM | `` |
| D80 | D80.CUTPART.BEAM | `` |
| D81 | D81.CUTPART.BEAM | `P29` |
| D82 | D82.CUTPART.BEAM | `P29` |
| D83 | Gus to web beam | `P35+P44+(P37-1)*P38` |
| D84 | Bolt to web beam | `P35` |
| D85 | D85.CUTPART.BEAM | `` |
| D86 | D86.CUTPART.BEAM | `` |
| D87 | top beam tack xx | `P30` |
| D88 | top beam tack xx | `` |
| D89 | top beam tack xx | `` |
| D90 | top beam tack xx | `` |
| D91 | top beam tack xx | `` |
| D92 | top beam tack xx | `(fP(proT,"ID67174322-0000-3715-3137-323935383235")-P30)*tan(P31)` |
| D93 | bott. beam tack xx | `` |
| D94 | bott. beam tack xx | `` |
| D95 | bott. beam tack xx | `P30` |
| D96 | bott. beam tack xx | `` |
| D97 | bott. beam tack xx | `` |
| D98 | bott. beam tack xx | `D92` |
| D99 | Bolt to web beam | `-P62` |
| D100 | Bolt to web beam | `-P62` |
| D101 | D101.CUTPART.BEAM | `` |
| D102 | D102.CUTPART.BEAM | `` |
| D103 | Bolt to web beam | `` |
| D104 | D104.PLATE.BEAM | `P36-P60-P62` |
| D105 | D105.PLATE.BEAM | `P36-P60-P62` |
| D106 | D106.PLATE.BEAM | `0.5*(fP(proH,"ID67174322-0000-3715-3137-323935383235")+P43)+P61-P62` |
| D107 | D107.PLATE.BEAM | `0.5*(fP(proH,"ID67174322-0000-3715-3137-323935383235")+P43)+P61-P62` |
| D108 | Bolt end | `D78+100` |
| D109 | D109.BEAM.BEAM | `` |
| D110 | Flat bar | `` |
| D111 | Flat bar | `` |
| D112 | D112.CUTPART.BEAM | `` |
| D113 | D113.CUTPART.BEAM | `` |
| D114 | D114.CUTPART.BEAM | `P28` |
| D115 | D115.CUTPART.BEAM | `P28` |
| D116 | Flat bar | `` |
| D117 | Flat bar | `` |
| D118 | Flat bar | `` |
| D119 | D119.PLATE.BEAM | `` |
| D120 | Flat bar | `` |
| D121 | D121.PLATE.BEAM | `` |
| D122 | D122.PLATE.Plane | `` |
| D123 | D123.PLATE.Plane | `` |
| D124 | D124.PLATE.Plane | `` |
| D125 | D125.PLATE.Plane | `` |
| D126 | D126.PLATE.Plane | `` |
| D127 | D127.PLATE.Plane | `` |
| D128 | Bolt to web beam | `` |
| D129 | D129.PLATE.BEAM | `` |
| D130 | D130.PLATE.BEAM | `` |
| D131 | D131.PLATE.BEAM | `` |
| D132 | D132.PLATE.BEAM | `` |
| D133 | D133.PLATE.BEAM | `P75` |
| D134 | D134.PLATE.BEAM | `P75` |
| D135 | D135.PLATE.0-P-G_TO_B(PIN MOMENT_SLOTS HOLE) | `P21` |
| D136 | D136.PLATE.0-P-G_TO_B(PIN MOMENT_SLOTS HOLE) | `P21` |
| D137 | D137.PLATE.0-P-G_TO_B(PIN MOMENT_SLOTS HOLE) | `P22-P21` |
| D138 | D138.PLATE.0-P-G_TO_B(PIN MOMENT_SLOTS HOLE) | `P22-P21` |
| D139 | D139.CUTPART.PLATE | `` |
| D140 | D140.CUTPART.PLATE | `` |
| D141 | D141.CUTPART.PLATE | `` |
| D142 | D142.CUTPART.PLATE | `60*tan(P81)` |
| D143 | D143.CUTPART.PLATE | `` |
| D144 | D144.CUTPART.PLATE | `` |
| D145 | D145.CUTPART.PLATE | `P85+60` |
| D146 | D146.CUTPART.PLATE | `P85+60` |
| D147 | D147.CUTPART.PLATE | `P85` |
| D148 | D148.CUTPART.PLATE | `` |
| D149 | D149.CUTPART.PLATE | `` |
| D150 | D150.CUTPART.PLATE | `` |
| D151 | D151.CUTPART.PLATE | `P85+60` |
| D152 | D152.CUTPART.PLATE | `P85+60` |
| D153 | D153.CUTPART.PLATE | `P85` |
| D154 | D154.CUTPART.PLATE | `60*tan(P81)` |
| D155 | D155.CUTPART.PLATE | `` |
| D156 | D156.CUTPART.PLATE | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#1460 | CREATION | `0` |
| obj#1482 | CREATION | `P73` |
| obj#1482 | MATERIAL | `P10` |
| obj#1482 | PROFILE | `"PL"+P72` |
| obj#1482 | SERIE | `P8` |
| obj#1482 | STARTNUMBER | `P86` |
| obj#1493 | CLASS | `P59` |
| obj#1493 | MATERIAL | `P10` |
| obj#1493 | PROFILE | `"PL"+P1` |
| obj#1493 | SERIE | `P7` |
| obj#1493 | STARTNUMBER | `P86` |
| obj#1503 | CLASS | `P59` |
| obj#1503 | CREATION | `P54` |
| obj#1503 | MATERIAL | `P10` |
| obj#1503 | PROFILE | `"PL"+P2` |
| obj#1503 | SERIE | `P8` |
| obj#1503 | STARTNUMBER | `P86` |
| obj#1512 | CLASS | `P59` |
| obj#1512 | CREATION | `P13` |
| obj#1512 | MATERIAL | `P10` |
| obj#1512 | PROFILE | `"PL"+P4` |
| obj#1512 | SERIE | `P8` |
| obj#1512 | STARTNUMBER | `P86` |
| obj#1520 | CLASS | `P3` |
| obj#1520 | CREATION | `P12` |
| obj#1520 | MATERIAL | `P10` |
| obj#1520 | PROFILE | `"PL"+P3` |
| obj#1520 | SERIE | `P8` |
| obj#1520 | STARTNUMBER | `P86` |
| obj#1528 | CREATION | `P58` |
| obj#1528 | MATERIAL | `P71` |
| obj#1528 | PROFILE | `P6` |
| obj#1528 | SERIE | `P9` |
| obj#1541 | CREATION | `P58` |
| obj#1541 | MATERIAL | `P71` |
| obj#1541 | PROFILE | `P6` |
| obj#1541 | SERIE | `P9` |
| obj#1549 | CREATION | `P32` |
| obj#1562 | CREATION | `P32` |
| obj#1578 | CREATION | `P26` |
| obj#1596 | CREATION | `P26` |
| obj#1609 | CREATION | `P83` |
| obj#1619 | CREATION | `P82` |
| obj#2315 | BOLT_GROUP_DISTX | `P39` |
| obj#2315 | BOLT_GROUP_DISTY | `P42` |
| obj#2315 | SIZE | `P48_diameter` |
| obj#2315 | SLOTTED_HOLE_PARTS | `P45` |
| obj#2315 | SLOTTED_HOLE_X | `P56` |
| obj#2315 | SLOTTED_HOLE_Y | `P57` |
| obj#2315 | STANDARD | `P48_screwdin` |
| obj#2315 | TOLERANCE | `P55` |

## Preset `+000.p_0-DM-PIN MOMENT-V2`
```
joint_attributes.saveas_file "+000"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
```

## Preset `+104.p_0-DM-PIN MOMENT-V2`
```
joint_attributes.saveas_file "+104"
joint_attributes.get_menu "+104"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P56 33.000000
joint_attributes.P55 3.000000
joint_attributes.P48_screwdin "T.S.B"
joint_attributes.P48_diameter 24.000000
joint_attributes.P44 65.000000
joint_attributes.P30 2.000000
joint_attributes.P29 35.000000
joint_attributes.P28 35.000000
joint_attributes.P27 2
joint_attributes.P35 65.000000
joint_attributes.P26 1
joint_attributes.P32 1
joint_attributes.P89 35.000000
joint_attributes.P88 35.000000
joint_attributes.P87 3
joint_attributes.P68 5.000000
joint_attributes.P67 5.000000
joint_attributes.P83 0
joint_attributes.P82 0
joint_attributes.P66 2
joint_attributes.P80 2.000000
joint_attributes.P79 35.000000
joint_attributes.P52 2
joint_attributes.P78 35.000000
joint_attributes.P77 3
joint_attributes.P33 3
joint_attributes.P19 10.000000
joint_attributes.P65 5.000000
joint_attributes.P14 16.000000
joint_attributes.P50 3
joint_attributes.P64 5.000000
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P63 2
joint_attributes.P73 0
joint_attributes.P72 6.000000
joint_attributes.P59 3
joint_attributes.P15 0
joint_attributes.P58 1
```

## Preset `+94 B3.p_0-DM-PIN MOMENT-V2`
```
joint_attributes.saveas_file "+94 B3"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P89 35.000000
joint_attributes.P88 35.000000
joint_attributes.P87 3
joint_attributes.P83 0
joint_attributes.P82 0
joint_attributes.P80 2.000000
joint_attributes.P79 35.000000
joint_attributes.P78 35.000000
joint_attributes.P77 3
joint_attributes.P73 0
joint_attributes.P68 5.000000
joint_attributes.P67 5.000000
joint_attributes.P66 2
joint_attributes.P65 5.000000
joint_attributes.P64 5.000000
joint_attributes.P63 2
joint_attributes.P59 3
joint_attributes.P58 1
joint_attributes.P56 28.000000
joint_attributes.P55 2.000000
joint_attributes.P52 2
joint_attributes.P50 3
joint_attributes.P33 3
joint_attributes.P48_screwdin "T.S.B"
joint_attributes.P48_diameter 20.000000
joint_attributes.P44 65.000000
joint_attributes.P35 65.000000
joint_attributes.P32 1
joint_attributes.P30 2.000000
joint_attributes.P29 35.000000
joint_attributes.P28 35.000000
joint_attributes.P27 2
joint_attributes.P26 1
joint_attributes.P21 100.000000
joint_attributes.P19 10.000000
joint_attributes.P15 1
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
```

## Preset `+AKC.p_0-DM-PIN MOMENT-V2`
```
joint_attributes.saveas_file "+AKC"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P89 -2147483648.000000
joint_attributes.P88 -2147483648.000000
joint_attributes.P87 0
joint_attributes.P86 -2147483648
joint_attributes.P83 1
joint_attributes.P82 1
joint_attributes.P81 -2147483648.000000
joint_attributes.P80 -2147483648.000000
joint_attributes.P79 -2147483648.000000
joint_attributes.P78 -2147483648.000000
joint_attributes.P77 0
joint_attributes.P76 -2147483648.000000
joint_attributes.P75 -2147483648.000000
joint_attributes.P73 1
joint_attributes.P72 -2147483648.000000
joint_attributes.P71 ""
joint_attributes.P70 -2147483648.000000
joint_attributes.P69 -2147483648.000000
joint_attributes.P68 -2147483648.000000
joint_attributes.P67 -2147483648.000000
joint_attributes.P66 0
joint_attributes.P65 -2147483648.000000
joint_attributes.P64 -2147483648.000000
joint_attributes.P63 0
joint_attributes.P62 -2147483648.000000
joint_attributes.P61 -2147483648.000000
joint_attributes.P60 -2147483648.000000
joint_attributes.P59 -2147483648
joint_attributes.P58 1
joint_attributes.P57 -2147483648.000000
joint_attributes.P56 -2147483648.000000
joint_attributes.P55 -2147483648.000000
joint_attributes.P53 -2147483648.000000
joint_attributes.P52 0
joint_attributes.P51 -2147483648.000000
joint_attributes.P50 0
joint_attributes.P49 -2147483648.000000
joint_attributes.P33 0
joint_attributes.P48_screwdin ""
joint_attributes.P48_diameter -2147483648.000000
joint_attributes.P34 -2147483648.000000
joint_attributes.P44 -2147483648.000000
joint_attributes.P41 -2147483648.000000
joint_attributes.P40 -2147483648
joint_attributes.P38 -2147483648.000000
joint_attributes.P37 -2147483648
joint_attributes.P35 -2147483648.000000
joint_attributes.P32 1
joint_attributes.P31 -2147483648.000000
joint_attributes.P30 -2147483648.000000
joint_attributes.P29 -2147483648.000000
joint_attributes.P28 -2147483648.000000
joint_attributes.P27 0
joint_attributes.P26 1
joint_attributes.P23 -2147483648.000000
joint_attributes.P22 -2147483648.000000
joint_attributes.P21 -2147483648.000000
joint_attributes.P20 -2147483648.000000
joint_attributes.P19 -2147483648.000000
joint_attributes.P15 1
joint_attributes.P14 -2147483648.000000
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P10 ""
joint_attributes.P9 ""
joint_attributes.P8 ""
joint_attributes.P7 ""
joint_attributes.FB -2147483648.000000
joint_attributes.P4 -2147483648.000000
joint_attributes.P3 -2147483648.000000
joint_attributes.P2 -2147483648.000000
joint_attributes.P1 -2147483648.000000
```

## Preset `+SAMPLE.p_0-DM-PIN MOMENT-V2`
```
joint_attributes.saveas_file "+SAMPLE"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P89 35.000000
joint_attributes.P88 35.000000
joint_attributes.P87 3
joint_attributes.P83 0
joint_attributes.P82 0
joint_attributes.P79 35.000000
joint_attributes.P78 35.000000
joint_attributes.P77 3
joint_attributes.P73 0
joint_attributes.P68 5.000000
joint_attributes.P67 5.000000
joint_attributes.P66 2
joint_attributes.P65 5.000000
joint_attributes.P64 5.000000
joint_attributes.P63 2
joint_attributes.P58 1
joint_attributes.P52 2
joint_attributes.P51 25.000000
joint_attributes.P50 3
joint_attributes.P33 3
joint_attributes.P48_screwdin "T.S.B"
joint_attributes.P48_diameter 20.000000
joint_attributes.P32 1
joint_attributes.P30 2.000000
joint_attributes.P29 35.000000
joint_attributes.P28 35.000000
joint_attributes.P27 2
joint_attributes.P26 1
joint_attributes.P22 300.000000
joint_attributes.P21 0.000000
joint_attributes.P19 10.000000
joint_attributes.P15 1
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
```

## Preset `B1104.p_0-DM-PIN MOMENT-V2`
```
joint_attributes.saveas_file "B1104"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P56 0.000000
joint_attributes.P55 3.000000
joint_attributes.P48_screwdin "T.S.B"
joint_attributes.P48_diameter 24.000000
joint_attributes.P44 65.000000
joint_attributes.P30 2.000000
joint_attributes.P29 35.000000
joint_attributes.P28 35.000000
joint_attributes.P27 2
joint_attributes.P35 65.000000
joint_attributes.P26 1
joint_attributes.P32 1
joint_attributes.P89 35.000000
joint_attributes.P88 35.000000
joint_attributes.P87 3
joint_attributes.P68 5.000000
joint_attributes.P67 5.000000
joint_attributes.P83 0
joint_attributes.P82 0
joint_attributes.P66 2
joint_attributes.P80 2.000000
joint_attributes.P79 35.000000
joint_attributes.P52 2
joint_attributes.P70 30.000000
joint_attributes.P78 35.000000
joint_attributes.P77 3
joint_attributes.P33 3
joint_attributes.P69 1.000000
joint_attributes.P19 10.000000
joint_attributes.P65 5.000000
joint_attributes.P21 150.000000
joint_attributes.P14 16.000000
joint_attributes.P50 3
joint_attributes.P64 5.000000
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 0
joint_attributes.P63 2
joint_attributes.P73 0
joint_attributes.P3 16.000000
joint_attributes.P72 6.000000
joint_attributes.P59 3
joint_attributes.P15 0
joint_attributes.P58 1
```