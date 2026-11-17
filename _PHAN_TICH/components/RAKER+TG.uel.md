# 0-RAKER+TG

- File: `RAKER+TG.uel`  (33,571 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	0-RAKER+TG	All`
- Dialog parameters: 0 | Variables (P/custom): 52 | Distance vars (D): 156 | Parts with bound profile: 2
- Lookup files (fVF): -
- Functions used: cos, fAD, fP, if, sin, tan
- Dialog bitmaps: 0 | Saved presets: #KRAC.p_0-RAKER+TG, +B2-104.p_0-RAKER+TG, +B4-104.p_0-RAKER+TG, +B7-104-TSB0.p_0-RAKER+TG, +B7-104.p_0-RAKER+TG, 0-H350x357x19x19.p_0-RAKER+TG, 1.p_0-RAKER+TG, H350_357_19_19.p_0-RAKER+TG, KK#1234.p_0-RAKER+TG, KK111.p_0-RAKER+TG, KK250.p_0-RAKER+TG, KK350.p_0-RAKER+TG, KKKH350_357_19_19.p_0-RAKER+TG

## Dialog (.inp)
```
page("TeklaStructures", "")
{
    joint(1, "0-RAKER+TG")
    {
        tab_page("", "Base Plate", 1)
        {
            attribute("", "Base Plate Thk.", label, "%s", none, none, "0", "0", 5, 15)
            parameter("", "P1", distance, number, 225, 15, 160)
            attribute("", "Mat.", label, "%s", none, none, "0", "0", 5, 50)
            parameter("", "P2", material, text, 225, 50, 160)
            attribute("", "Class", label, "%s", none, none, "0", "0", 10, 95)
            parameter("", "P3", integer, number, 225, 90, 160)
            attribute("", "Mark", label, "%s", none, none, "0", "0", 10, 140)
            parameter("", "P4", string, text, 225, 130, 160)
            attribute("", "Top gap", label, "%s", none, none, "0", "0", 450, 10)
            parameter("", "P6", distance, number, 610, 10, 160)
            attribute("", "Left gap", label, "%s", none, none, "0", "0", 450, 85)
            parameter("", "P7", distance, number, 610, 85, 160)
            attribute("", "Bott. gap", label, "%s", none, none, "0", "0", 450, 45)
            parameter("", "P8", distance, number, 610, 50, 160)
            attribute("", "Right gap", label, "%s", none, none, "0", "0", 440, 125)
            parameter("", "P19", distance, number, 610, 125, 160)
        }
        tab_page("", "Connection", 2)
        {
            attribute("", "Slotted hole X", label, "%s", none, none, "0", "0", 30, 285)
            parameter("", "P39", distance, number, 210, 285, 160)
            attribute("", "Slotted hole Y", label, "%s", none, none, "0", "0", 30, 330)
            parameter("", "P41", distance, number, 210, 330, 160)
            attribute("", "Bolt distance y", label, "%s", none, none, "0", "0", 1165, 285)
            parameter("", "P43", distance, number, 1360, 290, 160)
            attribute("", "Bolt to Gp_x (eH)", label, "%s", none, none, "0", "0", 1315, 370)
            parameter("", "P44", distance, number, 1355, 410, 160)
            attribute("", "Bolt to Gp_y (eV)", label, "%s", none, none, "0", "0", 1160, 195)
            parameter("", "P45", distance, number, 1360, 195, 160)
            attribute("", "Gap Web beam to base pl", label, "%s", none, none, "0", "0", 555, 135)
            parameter("", "P46", distance, number, 830, 130, 160)
            parameter("", "P51", YesNo, number, 1165, 595, 160)
            attribute("", "FB (Y/N)", label, "%s", none, none, "0", "0", 460, 670)
            parameter("", "P52", YesNo, number, 605, 670, 160)
            attribute("", "Bott Scallop (Y/N)", label, "%s", none, none, "0", "0", 980, 710)
            parameter("", "P53", YesNo, number, 1190, 705, 160)
            attribute("", "Gusset Thk.", label, "%s", none, none, "0", "0", 664, 11)
            parameter("", "P20", distance, number, 828, 7, 160)
            attribute("", "Matt", label, "%s", none, none, "0", "0", 669, 48)
            parameter("", "P22", material, text, 830, 50, 160)
            attribute("", "Mark", label, "%s", none, none, "0", "0", 1045, 5)
            parameter("", "P21", string, text, 1175, 5, 160)
            attribute("", "Class", label, "%s", none, none, "0", "0", 1045, 45)
            parameter("", "P23", integer, number, 1175, 45, 160)
            picture("0", 236, 152, 825, 170)
            attribute("", "Guss. L or R", label, "%s", none, none, "0", "0", 665, 90)
            parameter("", "P24", YesNo, number, 830, 90, 160)
            attribute("", "Bolt To web Beam", label, "%s", none, none, "0", "0", 1030, 130)
            parameter("", "P29", distance, number, 1255, 125, 160)
            attribute("", "Bolt Q'ty x", label, "%s", none, none, "0", "0", 925, 370)
            parameter("", "P31", integer, number, 1095, 370, 160)
            attribute("", "Bolt distance x", label, "%s", none, none, "0", "0", 920, 415)
            parameter("", "P32", distance, number, 1100, 410, 160)
            attribute("", "Bolt Q'ty y", label, "%s", none, none, "0", "0", 1170, 255)
            parameter("", "P34", integer, number, 1360, 255, 160)
            attribute("", "Bolt size", label, "%s", none, none, "0", "0", 75, 160)
            parameter("", "P38_diameter", bolt_size, number, 210, 155, 160)
            attribute("", "Bolt standard", label, "%s", none, none, "0", "0", 35, 200)
            parameter("", "P38_screwdin", bolt_standard, text, 210, 200, 160)
            attribute("", "Tolerance", label, "%s", none, none, "0", "0", 55, 240)
            parameter("", "P40", distance, number, 210, 245, 160)
            attribute("", "FB Mat.", label, "%s", none, none, "0", "0", 472, 630)
            picture("00", 80, 43, 835, 635)
            attribute("", "Top Scallop (Y/N)", label, "%s", none, none, "0", "0", 960, 600)
            parameter("", "P5", material, text, 606, 629, 160)
            attribute("", "FB Mark", label, "%s", none, none, "0", "0", 465, 590)
            parameter("", "P11", string, text, 605, 590, 160)
            attribute("", "FLG. Weld gap", label, "%s", none, none, "0", "0", 960, 530)
            parameter("", "P10", distance, number, 1165, 525, 160)
            attribute("", "Weld Angle", label, "%s", none, none, "0", "0", 980, 565)
            parameter("", "P12", angle, number, 1165, 560, 160)
            attribute("", "Conn. Type ( 0 or 1 )", label, "%s", none, none, "0", "0", 200, 20)
            parameter("", "P13", integer, number, 217, 64, 160)
        }
    }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P2 | Mat. | Material | `SS275` |
| P3 | Class | Integer | `7` |
| P4 | Mark | Text | `1BP` |
| P5 | FB Mat. | Material | `SS275` |
| P6 | Top gap | Length | `200.00` |
| P7 | Left gap | Length | `100.00` |
| P8 | Bott. gap | Length | `100.00` |
| P10 | FLG. Weld gap | Length | `5.00` |
| P11 | FB Mark | Text | `1FB` |
| P12 | Weld Angle | Angle | `45.0000` |
| P13 | Conn. Type ( 0 or 1 ) | Integer | `0` |
| P14 | Parameter14 | Angle | `fAD("slope","ID673A8EA2-0000-4FFD-3137-333138393131")` *(formula)* |
| P15 | Parameter15 | Length | `500*cos(P14)` *(formula)* |
| P16 | Parameter16 | Length | `if (P15>=0) then P15 else -P15 endif` *(formula)* |
| P17 | Parameter17 | Length | `fP(proT,"ID673A8EA2-0000-4FFD-3137-333138393131")` *(formula)* |
| P18 | Parameter18 | Integer | `if (P13==1) then 0 else 1 endif` *(formula)* |
| P19 | Right gap | Length | `100.00` |
| P20 | Gusset Thk. | Length | `10.00` |
| P21 | Mark | Text | `gp` |
| P22 | Matt | Material | `SS400` |
| P23 | Class | Integer | `3` |
| P24 | Guss. L or R | Yes/No | `0` |
| P25 | Parameter25 | Integer | `0.5*(fP(proS,"ID673A8EA2-0000-4FFD-3137-333138393131")+P20)` *(formula)* |
| P26 | Parameter26 | Length | `0.5*P20*sin(P14)` *(formula)* |
| P27 | Parameter27 | Length | `0.00` |
| P28 | Parameter28 | Length | `0.00` |
| P29 | Bolt To web Beam | Length | `90.00` |
| P30 | Bolt to top beam | Length | `0.00` |
| P31 | Bolt Q'ty x | Integer | `6` |
| P32 | Bolt distance x | Length | `60.00` |
| P33 | Group bolt x | 9 | `if(P31==1) then 0 else (P31-1)+"*"+P32 endif` *(formula)* |
| P34 | Bolt Q'ty y | Integer | `3` |
| P35 | Group bolt y | 9 | `if(P34==1) then 0 else (P34-1)+"*"+P43 endif` *(formula)* |
| P36 | bolt to bolt | Length | `0.00` |
| P37 | Bolt To Gusset Plate  | Length | `60.00` |
| P38_diameter | Bolt size | 6 | `20.00` |
| P38_screwdin | Bolt standard | 7 | `T.S.B` |
| P39 | Slotted hole X | Length | `50.00` |
| P40 | Tolerance | Length | `2.00` |
| P41 | Slotted hole Y | Length | `0.00` |
| P42 | Parameter42 | 24 | `if P24==1 then 01000 else if P24==0 then 10000 else 00000 endif endif` *(formula)* |
| P43 | Bolt distance y | Length | `100.00` |
| P44 | Bolt to Gp_x (eH) | Length | `50.00` |
| P45 | Bolt to Gp_y (eV) | Length | `70.00` |
| P46 | Gap Web beam to base pl | Length | `100.00` |
| P47 | Parameter47 | Length | `if(P46==0) then -10 else P46 endif` *(formula)* |
| P48 | Parameter48 | Length | `(P29+(P31-1)*P32+P44)*cos(P14)` *(formula)* |
| P49 | Parameter49 | Length | `P46+(P29+(P31-1)*P32+P44)*cos(P14)` *(formula)* |
| P50 | Parameter50 | Length | `fP(proB,"ID673A8EA2-0000-4FFD-3137-333138393131")*0.5*sin(P14)` *(formula)* |
| P51 | Top Scallop (Y/N) | Yes/No | `1` |
| P52 | FB (Y/N) | Yes/No | `1` |
| P53 | Bott Scallop (Y/N) | Yes/No | `1` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.BEAM.BEAM | `` |
| D2 | D2.BEAM.BEAM | `` |
| D3 | D3.BEAM.BEAM | `P1` |
| D4 | D4.BEAM.BEAM | `P1` |
| D5 | D5.BEAM.BEAM | `` |
| D6 | D6.BEAM.BEAM | `` |
| D7 | D7.PLATE.BEAM | `P6` |
| D8 | D8.PLATE.BEAM | `P6` |
| D9 | D9.PLATE.BEAM | `P8` |
| D10 | D10.PLATE.BEAM | `P8` |
| D11 | D11.PLATE.BEAM | `P7` |
| D12 | D12.PLATE.BEAM | `P7` |
| D13 | D13.PLATE.BEAM | `P19` |
| D14 | D14.PLATE.BEAM | `P19` |
| D15 | D15.linecut.BEAM | `` |
| D16 | D16.linecut.BEAM | `` |
| D17 | D17.linecut.BEAM | `` |
| D18 | D18.PLATE.BEAM | `` |
| D19 | D19.PLATE.BEAM | `` |
| D20 | D20.PLATE.BEAM | `` |
| D21 | D21.PLATE.BEAM | `` |
| D22 | D22.FLAT_BAR.BEAM | `` |
| D23 | D23.FLAT_BAR.BEAM | `` |
| D24 | D24.FLAT_BAR.PLATE | `P10` |
| D25 | D25.FLAT_BAR.PLATE | `P10` |
| D26 | D26.FLAT_BAR.BEAM | `` |
| D27 | D27.FLAT_BAR.BEAM | `` |
| D28 | D28.FLAT_BAR.BEAM | `` |
| D29 | D29.FLAT_BAR.BEAM | `` |
| D30 | D30.FLAT_BAR.PLATE | `P10` |
| D31 | D31.FLAT_BAR.PLATE | `P10` |
| D32 | D32.FLAT_BAR.BEAM | `` |
| D33 | D33.FLAT_BAR.BEAM | `` |
| D34 | D34.BEAM.BEAM | `` |
| D35 | D35.BEAM.BEAM | `` |
| D36 | D36.BEAM.BEAM | `` |
| D37 | D37.BEAM.BEAM | `` |
| D38 | D38.BEAM.PLATE | `P49` |
| D39 | D39.BEAM.PLATE | `` |
| D40 | D40.CUTPART.BEAM | `` |
| D41 | D41.CUTPART.BEAM | `` |
| D42 | D42.CUTPART.BEAM | `` |
| D43 | D43.CUTPART.BEAM | `` |
| D44 | D44.CUTPART.BEAM | `` |
| D45 | D45.CUTPART.BEAM | `` |
| D46 | D46.CUTPART.BEAM | `` |
| D47 | D47.CUTPART.BEAM | `` |
| D48 | D48.CUTPART.BEAM | `` |
| D49 | D49.CUTPART.BEAM | `` |
| D50 | D50.CUTPART.BEAM | `P10` |
| D51 | D51.CUTPART.BEAM | `P1` |
| D52 | D52.CUTPART.BEAM | `P1` |
| D53 | D53.CUTPART.BEAM | `P10+(P17-2)*tan(P12)` |
| D54 | D54.CUTPART.BEAM | `P10` |
| D55 | D55.CUTPART.BEAM | `P10` |
| D56 | D56.CUTPART.BEAM | `P1` |
| D57 | D57.CUTPART.BEAM | `-P1` |
| D58 | D58.CUTPART.BEAM | `P10+(P17-2)*tan(P12)` |
| D59 | D59.CUTPART.BEAM | `P10` |
| D60 | D60.CUTPART.BEAM | `` |
| D61 | D61.CUTPART.BEAM | `` |
| D62 | D62.CUTPART.BEAM | `` |
| D63 | D63.CUTPART.BEAM | `` |
| D64 | D64.CUTPART.BEAM | `` |
| D65 | D65.CUTPART.BEAM | `` |
| D66 | D66.CUTPART.BEAM | `P46+35` |
| D67 | D67.CUTPART.BEAM | `P46+35` |
| D68 | D68.CUTPART.BEAM | `` |
| D69 | D69.CUTPART.BEAM | `` |
| D70 | D70.CUTPART.BEAM | `P46+35` |
| D71 | D71.CUTPART.BEAM | `P46+35` |
| D72 | D72.CUTPART.BEAM | `` |
| D73 | D73.CUTPART.BEAM | `` |
| D74 | D74.CUTPART.BEAM | `` |
| D75 | D75.CUTPART.BEAM | `` |
| D76 | D76.CUTPART.BEAM | `` |
| D77 | D77.CUTPART.BEAM | `` |
| D78 | D78.CUTPART.BEAM | `` |
| D79 | D79.CUTPART.BEAM | `` |
| D80 | D80.CUTPART.BEAM | `` |
| D81 | D81.CUTPART.BEAM | `` |
| D82 | D82.CUTPART.BEAM | `` |
| D83 | D83.CUTPART.BEAM | `` |
| D84 | D84.CUTPART.BEAM | `` |
| D85 | D85.CUTPART.BEAM | `` |
| D86 | D86.CUTPART.BEAM | `` |
| D87 | D87.CUTPART.BEAM | `` |
| D88 | D88.CUTPART.BEAM | `` |
| D89 | D89.CUTPART.BEAM | `` |
| D90 | D90.CUTPART.BEAM | `` |
| D91 | D91.CUTPART.BEAM | `` |
| D92 | D92.CUTPART.BEAM | `` |
| D93 | D93.CUTPART.BEAM | `` |
| D94 | D94.CUTPART.BEAM | `` |
| D95 | D95.CUTPART.BEAM | `` |
| D96 | D96.CUTPART.BEAM | `` |
| D97 | D97.CUTPART.BEAM | `` |
| D98 | D98.CUTPART.BEAM | `` |
| D99 | D99.FLAT_BAR.BEAM | `` |
| D100 | D100.FLAT_BAR.BEAM | `` |
| D101 | D101.FLAT_BAR.BEAM | `P10` |
| D102 | D102.FLAT_BAR.BEAM | `P10` |
| D103 | D103.FLAT_BAR.BEAM | `` |
| D104 | D104.FLAT_BAR.BEAM | `` |
| D105 | D105.CUTPART.BEAM | `P10` |
| D106 | D106.CUTPART.BEAM | `P1` |
| D107 | D107.CUTPART.BEAM | `-P1` |
| D108 | D108.CUTPART.BEAM | `P10+(P17-2)*tan(P12)` |
| D109 | D109.CUTPART.BEAM | `P10` |
| D110 | D110.CUTPART.BEAM | `` |
| D111 | D111.CUTPART.BEAM | `` |
| D112 | D112.CUTPART.BEAM | `` |
| D113 | D113.CUTPART.BEAM | `` |
| D114 | D114.CUTPART.BEAM | `` |
| D115 | D115.PLATE.BEAM | `if (P24==1) then P25 else -P25 endif` |
| D116 | D116.PLATE.BEAM | `if (P24==1) then P25 else -P25 endif` |
| D117 | D117.PLATE.BEAM | `if (P24==1) then P25 else -P25 endif` |
| D118 | D118.PLATE.BEAM | `if (P24==1) then P25 else -P25 endif` |
| D119 | D119.PLATE.PLATE | `P26` |
| D120 | D120.PLATE.PLATE | `P26` |
| D121 | D121.BOLT.BEAM | `` |
| D122 | D122.BOLT.BEAM | `` |
| D123 | D123.BOLT.BEAM | `` |
| D124 | D124.BOLT.BEAM | `` |
| D125 | D125.CUTPART.BEAM | `` |
| D126 | D126.CUTPART.BEAM | `` |
| D127 | D127.BOLT.PLATE | `P46+P29*cos(P14)` |
| D128 | D128.BOLT.PLATE | `` |
| D129 | D129.PLATE.BEAM | `0.5*(P34-1)*P43+P45` |
| D130 | D130.PLATE.BEAM | `0.5*(P34-1)*P43+P45` |
| D131 | D131.PLATE.BEAM | `0.5*(P34-1)*P43+P45` |
| D132 | D132.PLATE.BEAM | `0.5*(P34-1)*P43+P45` |
| D133 | D133.CUTPART.PLATE | `P46` |
| D134 | D134.CUTPART.PLATE | `P46` |
| D135 | D135.PLATE.BEAM | `` |
| D136 | D136.PLATE.BEAM | `` |
| D137 | D137.CUTPART.BEAM | `` |
| D138 | D138.CUTPART.BEAM | `` |
| D139 | D139.CUTPART.BEAM | `` |
| D140 | D140.CUTPART.BEAM | `` |
| D141 | D141.CUTPART.BEAM | `` |
| D142 | D142.CUTPART.BEAM | `` |
| D143 | D143.CUTPART.BEAM | `` |
| D144 | D144.CUTPART.BEAM | `` |
| D145 | D145.PLATE.BEAM | `` |
| D146 | D146.PLATE.BEAM | `` |
| D147 | D147.PLATE.BEAM | `` |
| D148 | D148.PLATE.BEAM | `` |
| D149 | D149.PLATE.BEAM | `` |
| D150 | D150.PLATE.BEAM | `` |
| D151 | D151.PLATE.PLATE | `` |
| D152 | D152.PLATE.PLATE | `` |
| D153 | D153.PLATE.PLATE | `P49` |
| D154 | D154.PLATE.PLATE | `` |
| D155 | D155.PLATE.PLATE | `` |
| D156 | D156.PLATE.PLATE | `P49` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#234 | CREATION | `0` |
| obj#254 | CREATION | `0` |
| obj#270 | CREATION | `0` |
| obj#290 | CLASS | `P23` |
| obj#290 | CREATION | `P18` |
| obj#290 | MATERIAL | `P22` |
| obj#290 | PROFILE | `"PL"+P20` |
| obj#290 | SERIE | `P21` |
| obj#300 | CREATION | `if (P52==0) then P52 else P13 endif` |
| obj#300 | MATERIAL | `P5` |
| obj#300 | SERIE | `P11` |
| obj#310 | CLASS | `P3` |
| obj#310 | MATERIAL | `P2` |
| obj#310 | PROFILE | `"PL"+P1` |
| obj#310 | SERIE | `P4` |
| obj#321 | CREATION | `P52` |
| obj#321 | MATERIAL | `P5` |
| obj#321 | SERIE | `P11` |
| obj#328 | CREATION | `if (P52==0) then P52 else P18 endif` |
| obj#328 | MATERIAL | `P5` |
| obj#328 | SERIE | `P11` |
| obj#335 | CREATION | `P53` |
| obj#347 | CREATION | `P18` |
| obj#358 | CREATION | `P13` |
| obj#368 | CREATION | `P51` |
| obj#402 | CREATION | `P13` |
| obj#1121 | CREATION | `P18` |
| obj#1278 | BOLT_GROUP_DISTX | `P33` |
| obj#1278 | BOLT_GROUP_DISTY | `P35` |
| obj#1278 | CREATION | `P18` |
| obj#1278 | SIZE | `P38_diameter` |
| obj#1278 | SLOTTED_HOLE_PARTS | `P42` |
| obj#1278 | SLOTTED_HOLE_X | `P39` |
| obj#1278 | SLOTTED_HOLE_Y | `P41` |
| obj#1278 | STANDARD | `P38_screwdin` |
| obj#1278 | TOLERANCE | `P40` |
| obj#1638 | CREATION | `P18` |

## Preset `#KRAC.p_0-RAKER+TG`
```
joint_attributes.saveas_file "#KRAC"
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
joint_attributes.zang2 -0.000000
joint_attributes.zang1 -0.000000
joint_attributes.zsuunta 7
joint_attributes.P13 0
joint_attributes.P12 45.000000
joint_attributes.P10 5.000000
joint_attributes.P11 "1RFB"
joint_attributes.P5 "SS275"
joint_attributes.P40 2.000000
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 20.000000
joint_attributes.P34 2
joint_attributes.P32 80.000000
joint_attributes.P31 1
joint_attributes.P29 80.000000
joint_attributes.P24 1
joint_attributes.P23 3
joint_attributes.P21 "1RGP"
joint_attributes.P22 "SM355"
joint_attributes.P20 30.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 15.000000
joint_attributes.P45 40.000000
joint_attributes.P44 55.000000
joint_attributes.P43 130.000000
joint_attributes.P41 0.000000
joint_attributes.P39 28.000000
joint_attributes.P19 25.000000
joint_attributes.P8 25.000000
joint_attributes.P7 25.000000
joint_attributes.P6 25.000000
joint_attributes.P4 "1RSF"
joint_attributes.P3 7
joint_attributes.P2 "SM355"
joint_attributes.P1 19.000000
```

## Preset `+B2-104.p_0-RAKER+TG`
```
joint_attributes.saveas_file "+B2-104"
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
joint_attributes.zang1 20.337900
joint_attributes.zsuunta 7
joint_attributes.P40 3.000000
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 24.000000
joint_attributes.P34 2
joint_attributes.P31 1
joint_attributes.P29 115.000000
joint_attributes.P24 1
joint_attributes.P23 2
joint_attributes.P21 "B-B2RGP"
joint_attributes.P22 "SM355"
joint_attributes.P20 19.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 20.000000
joint_attributes.P45 40.000000
joint_attributes.P44 65.000000
joint_attributes.P43 90.000000
joint_attributes.P39 33.000000
joint_attributes.P19 50.000000
joint_attributes.P8 20.000000
joint_attributes.P7 50.000000
joint_attributes.P6 20.000000
joint_attributes.P4 "B-B2RBP"
joint_attributes.P2 "SM355"
```

## Preset `+B4-104.p_0-RAKER+TG`
```
joint_attributes.saveas_file "+B4-104"
joint_attributes.get_menu "+B4-104"
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
joint_attributes.zang1 20.337900
joint_attributes.zsuunta 7
joint_attributes.P11 "B-B4FB"
joint_attributes.P40 3.000000
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 24.000000
joint_attributes.P34 2
joint_attributes.P31 1
joint_attributes.P29 115.000000
joint_attributes.P24 1
joint_attributes.P23 2
joint_attributes.P21 "B-B4GP"
joint_attributes.P22 "SM355"
joint_attributes.P20 19.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 20.000000
joint_attributes.P45 40.000000
joint_attributes.P44 65.000000
joint_attributes.P43 90.000000
joint_attributes.P39 33.000000
joint_attributes.P19 50.000000
joint_attributes.P8 20.000000
joint_attributes.P7 50.000000
joint_attributes.P6 20.000000
joint_attributes.P4 "B-B4BP"
joint_attributes.P2 "SM355"
```

## Preset `+B7-104-TSB0.p_0-RAKER+TG`
```
joint_attributes.saveas_file "+B7-104-TSB0"
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
joint_attributes.zang2 9.660000
joint_attributes.zang1 0.110000
joint_attributes.zsuunta 7
joint_attributes.P11 "1-B7FB"
joint_attributes.P40 3.000000
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 24.000000
joint_attributes.P34 1
joint_attributes.P32 50.000000
joint_attributes.P31 1
joint_attributes.P29 50.000000
joint_attributes.P24 0
joint_attributes.P23 2
joint_attributes.P21 "1-B7GP"
joint_attributes.P22 "SM355"
joint_attributes.P20 14.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 20.000000
joint_attributes.P45 40.000000
joint_attributes.P44 50.000000
joint_attributes.P43 130.000000
joint_attributes.P39 33.000000
joint_attributes.P19 50.000000
joint_attributes.P8 20.000000
joint_attributes.P7 50.000000
joint_attributes.P6 20.000000
joint_attributes.P4 "1-B7BP"
joint_attributes.P2 "SM355"
```

## Preset `+B7-104.p_0-RAKER+TG`
```
joint_attributes.saveas_file "+B7-104"
joint_attributes.get_menu "+B4-104"
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
joint_attributes.zang1 20.340000
joint_attributes.zsuunta 7
joint_attributes.P11 "1-B7FB"
joint_attributes.P40 3.000000
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 24.000000
joint_attributes.P34 2
joint_attributes.P31 1
joint_attributes.P29 115.000000
joint_attributes.P24 1
joint_attributes.P23 2
joint_attributes.P21 "1-B7GP"
joint_attributes.P22 "SM355"
joint_attributes.P20 15.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 20.000000
joint_attributes.P45 40.000000
joint_attributes.P44 65.000000
joint_attributes.P43 130.000000
joint_attributes.P39 33.000000
joint_attributes.P19 50.000000
joint_attributes.P8 20.000000
joint_attributes.P7 50.000000
joint_attributes.P6 20.000000
joint_attributes.P4 "1-B7BP"
joint_attributes.P2 "SM355"
```

## Preset `0-H350x357x19x19.p_0-RAKER+TG`
```
joint_attributes.saveas_file "0-H350x357x19x19"
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
joint_attributes.zang1 -0.000000
joint_attributes.zsuunta 7
joint_attributes.P13 0
joint_attributes.P12 45.000000
joint_attributes.P10 5.000000
joint_attributes.P11 "1RFB"
joint_attributes.P5 "SS275"
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 20.000000
joint_attributes.P34 2
joint_attributes.P31 1
joint_attributes.P24 1
joint_attributes.P23 3
joint_attributes.P21 "1RGP"
joint_attributes.P22 "SM355"
joint_attributes.P20 10.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 15.000000
joint_attributes.P45 70.000000
joint_attributes.P44 100.000000
joint_attributes.P43 90.000000
joint_attributes.P41 0.000000
joint_attributes.P39 0.000000
joint_attributes.P19 25.000000
joint_attributes.P8 25.000000
joint_attributes.P7 25.000000
joint_attributes.P6 25.000000
joint_attributes.P4 "[RBP"
joint_attributes.P3 7
joint_attributes.P2 "SM355"
joint_attributes.P1 19.000000
```

## Preset `1.p_0-RAKER+TG`
```
joint_attributes.saveas_file "1"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.rz_type 21
joint_attributes.ry_type 21
joint_attributes.rx_type 21
joint_attributes.uz_type 21
joint_attributes.uy_type 21
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
joint_attributes.zang1 4.347100
joint_attributes.zsuunta 7
joint_attributes.P38_screwdin ""
joint_attributes.P38_diameter -2147483648.000000
joint_attributes.P24 1
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
```

## Preset `H350_357_19_19.p_0-RAKER+TG`
```
joint_attributes.saveas_file "H350*357*19*19"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.rz_type 21
joint_attributes.ry_type 21
joint_attributes.rx_type 21
joint_attributes.uz_type 21
joint_attributes.uy_type 21
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
joint_attributes.zang2 12.562900
joint_attributes.zang1 16.961700
joint_attributes.zsuunta 7
joint_attributes.P13 0
joint_attributes.P11 "1RFB"
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 20.000000
joint_attributes.P34 2
joint_attributes.P31 1
joint_attributes.P29 60.000000
joint_attributes.P24 1
joint_attributes.P21 "1RGP"
joint_attributes.P22 "SM355"
joint_attributes.P20 30.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 100.000000
joint_attributes.P45 40.000000
joint_attributes.P44 55.000000
joint_attributes.P43 130.000000
joint_attributes.P39 28.000000
joint_attributes.P19 25.000000
joint_attributes.P8 25.000000
joint_attributes.P7 25.000000
joint_attributes.P6 25.000000
joint_attributes.P4 "1RSF"
joint_attributes.P2 "SM355"
joint_attributes.P1 19.000000
```

## Preset `KK#1234.p_0-RAKER+TG`
```
joint_attributes.saveas_file "KK#1234"
joint_attributes.get_menu "KKKH350_357_19_19"
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
joint_attributes.zang2 12.562900
joint_attributes.zang1 16.961700
joint_attributes.zsuunta 7
joint_attributes.P13 0
joint_attributes.P11 "1RFB"
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 20.000000
joint_attributes.P34 2
joint_attributes.P31 1
joint_attributes.P29 60.000000
joint_attributes.P24 1
joint_attributes.P21 "1RGP"
joint_attributes.P22 "SM355"
joint_attributes.P20 30.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 100.000000
joint_attributes.P45 40.000000
joint_attributes.P44 55.000000
joint_attributes.P43 130.000000
joint_attributes.P39 28.000000
joint_attributes.P19 25.000000
joint_attributes.P8 25.000000
joint_attributes.P7 25.000000
joint_attributes.P6 25.000000
joint_attributes.P4 "1RSF"
joint_attributes.P2 "SM355"
joint_attributes.P1 19.000000
```

## Preset `KK111.p_0-RAKER+TG`
```
joint_attributes.saveas_file "KK111"
joint_attributes.get_menu "KK111"
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
joint_attributes.zang2 -24.457500
joint_attributes.zang1 0.001800
joint_attributes.zsuunta 7
joint_attributes.P13 0
joint_attributes.P11 "B1C-RFB"
joint_attributes.P40 3.000000
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 24.000000
joint_attributes.P34 2
joint_attributes.P31 1
joint_attributes.P29 230.000000
joint_attributes.P24 1
joint_attributes.P21 "B1C-RGP"
joint_attributes.P22 "SM355"
joint_attributes.P20 30.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 15.000000
joint_attributes.P45 40.000000
joint_attributes.P44 60.000000
joint_attributes.P43 130.000000
joint_attributes.P39 0.000000
joint_attributes.P19 25.000000
joint_attributes.P8 25.000000
joint_attributes.P7 25.000000
joint_attributes.P6 25.000000
joint_attributes.P4 "B1C-RSF"
joint_attributes.P2 "SM355"
joint_attributes.P1 19.000000
```

## Preset `KK250.p_0-RAKER+TG`
```
joint_attributes.saveas_file "KK250"
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
joint_attributes.zang2 -10.096200
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P13 0
joint_attributes.P12 45.000000
joint_attributes.P10 2.000000
joint_attributes.P11 "1RFB"
joint_attributes.P5 "SS275"
joint_attributes.P40 3.000000
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 24.000000
joint_attributes.P34 2
joint_attributes.P32 60.000000
joint_attributes.P31 1
joint_attributes.P29 230.000000
joint_attributes.P24 1
joint_attributes.P21 "1RGP"
joint_attributes.P22 "SM355"
joint_attributes.P20 16.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 15.000000
joint_attributes.P45 40.000000
joint_attributes.P44 60.000000
joint_attributes.P43 90.000000
joint_attributes.P39 0.000000
joint_attributes.P19 25.000000
joint_attributes.P8 25.000000
joint_attributes.P7 25.000000
joint_attributes.P6 25.000000
joint_attributes.P4 "1RSF"
joint_attributes.P3 7
joint_attributes.P2 "SM355"
joint_attributes.P1 15.000000
```

## Preset `KK350.p_0-RAKER+TG`
```
joint_attributes.saveas_file "KK350"
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
joint_attributes.zang2 -19.732000
joint_attributes.zang1 0.001600
joint_attributes.zsuunta 7
joint_attributes.P13 0
joint_attributes.P11 "B1D-RFB"
joint_attributes.P40 3.000000
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 24.000000
joint_attributes.P34 2
joint_attributes.P31 1
joint_attributes.P29 230.000000
joint_attributes.P24 1
joint_attributes.P21 "B1D-RGP"
joint_attributes.P22 "SM355"
joint_attributes.P20 30.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 15.000000
joint_attributes.P45 40.000000
joint_attributes.P44 60.000000
joint_attributes.P43 130.000000
joint_attributes.P39 0.000000
joint_attributes.P19 25.000000
joint_attributes.P8 25.000000
joint_attributes.P7 25.000000
joint_attributes.P6 25.000000
joint_attributes.P4 "B1D-RSF"
joint_attributes.P2 "SM355"
joint_attributes.P1 19.000000
```

## Preset `KKKH350_357_19_19.p_0-RAKER+TG`
```
joint_attributes.saveas_file "KKKH350_357_19_19"
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
joint_attributes.zang1 -0.000000
joint_attributes.zsuunta 7
joint_attributes.P13 0
joint_attributes.P11 "1RFB"
joint_attributes.P38_screwdin "T.S.B"
joint_attributes.P38_diameter 20.000000
joint_attributes.P34 2
joint_attributes.P31 1
joint_attributes.P29 60.000000
joint_attributes.P24 1
joint_attributes.P21 "1RGP"
joint_attributes.P22 "SM355"
joint_attributes.P20 30.000000
joint_attributes.P53 1
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P46 100.000000
joint_attributes.P45 40.000000
joint_attributes.P44 55.000000
joint_attributes.P43 130.000000
joint_attributes.P39 28.000000
joint_attributes.P19 25.000000
joint_attributes.P8 25.000000
joint_attributes.P7 25.000000
joint_attributes.P6 25.000000
joint_attributes.P4 "1RSF"
joint_attributes.P2 "SM355"
joint_attributes.P1 19.000000
```