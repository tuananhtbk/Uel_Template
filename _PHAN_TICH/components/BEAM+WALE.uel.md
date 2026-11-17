# 0-DM-BEAM+WALE

- File: `BEAM+WALE.uel`  (28,708 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	0-DM-BEAM+WALE	All`
- Dialog parameters: 0 | Variables (P/custom): 35 | Distance vars (D): 156 | Parts with bound profile: 4
- Lookup files (fVF): -
- Functions used: cos, fAD, fP, if
- Dialog bitmaps: 0 | Saved presets: #KK.p_0-DM-BEAM+WALE, +1.p_0-DM-BEAM+WALE, 1.p_0-DM-BEAM+WALE

## Dialog (.inp)
```
page("TeklaStructures", "")
{
    joint(1, "0-DM-BEAM+WALE")
    {
        tab_page("", " Parameters 1 ", 1)
        {
            attribute("", "Base Mark", label, "%s", none, none, "0", "0", 20, 0)
            parameter("", "P1", string, text, 374, 0, 160)
            attribute("", "Thk.", label, "%s", none, none, "0", "0", 20, 28)
            parameter("", "P2", distance, number, 374, 28, 160)
            attribute("", "Matt.", label, "%s", none, none, "0", "0", 20, 56)
            parameter("", "P3", material, text, 374, 56, 160)
            attribute("", "Top", label, "%s", none, none, "0", "0", 20, 84)
            parameter("", "P4", distance, number, 374, 84, 160)
            attribute("", "Bott", label, "%s", none, none, "0", "0", 20, 112)
            parameter("", "P5", distance, number, 374, 112, 160)
            attribute("", "Left", label, "%s", none, none, "0", "0", 20, 140)
            parameter("", "P6", distance, number, 374, 140, 160)
            attribute("", "Right", label, "%s", none, none, "0", "0", 20, 168)
            parameter("", "P7", distance, number, 374, 168, 160)
            parameter("", "P8", string, text, 815, 0, 160)
            attribute("", "Thk.", label, "%s", none, none, "0", "0", 660, 30)
            parameter("", "P9", distance, number, 815, 30, 160)
            attribute("", "Matt.", label, "%s", none, none, "0", "0", 660, 55)
            parameter("", "P10", material, text, 815, 60, 160)
            attribute("", "h", label, "%s", none, none, "0", "0", 680, 90)
            parameter("", "P11", distance, number, 815, 90, 160)
            attribute("", "Stiff Weld (Y/N)", label, "%s", none, none, "0", "0", 610, 115)
            parameter("", "P12", YesNo, number, 815, 120, 160)
            attribute("", "Angle Size", label, "%s", none, none, "0", "0", 15, 260)
            parameter("", "P13", profile, text, 370, 260, 160)
            attribute("", "(Y/N)", label, "%s", none, none, "0", "0", 55, 325)
            parameter("", "P14", YesNo, number, 370, 325, 160)
            attribute("", "T_bar Size", label, "%s", none, none, "0", "0", 635, 260)
            parameter("", "P15", profile, text, 810, 260, 160)
            attribute("", "Length", label, "%s", none, none, "0", "0", 650, 295)
            parameter("", "P16", distance, number, 815, 290, 160)
            attribute("", "Matt.", label, "%s", none, none, "0", "0", 670, 325)
            parameter("", "P17", material, text, 815, 325, 160)
            attribute("", "T_bar (Y/N)", label, "%s", none, none, "0", "0", 625, 360)
            parameter("", "P18", YesNo, number, 815, 360, 160)
            attribute("", "Matt.", label, "%s", none, none, "0", "0", 60, 290)
            parameter("", "P13a", material, text, 370, 295, 160)
            attribute("", "FB Mark", label, "%s", none, none, "0", "0", 20, 410)
            parameter("", "P19", string, text, 370, 405, 160)
            attribute("", "Matt.", label, "%s", none, none, "0", "0", 45, 445)
            parameter("", "P20", material, text, 370, 440, 160)
            attribute("", "SF(Y/N)", label, "%s", none, none, "0", "0", 655, 150)
            parameter("", "P11a", YesNo, number, 815, 150, 160)
            attribute("", "L_Mark", label, "%s", none, none, "0", "0", 20, 230)
            parameter("", "P13b", string, text, 370, 225, 160)
            attribute("", "T_Bar Mark", label, "%s", none, none, "0", "0", 630, 225)
            parameter("", "P15a", string, text, 810, 225, 160)
            attribute("", "Wale Gap", label, "%s", none, none, "0", "0", 10, 515)
            parameter("", "P21", distance, number, 370, 510, 160)
            attribute("", "Stiff Mark", label, "%s", none, none, "0", "0", 630, 0)
            attribute("", "Wale weld (L or R)", label, "%s", none, none, "0", "0", 5, 565)
            parameter("", "P22", YesNo, number, 370, 545, 160)
            attribute("", "Type", label, "%s", none, none, "0", "0", 45, 665)
            parameter("", "P26", chamfer_type, number, 365, 660, 160)
            attribute("", "R", label, "%s", none, none, "0", "0", 55, 630)
            parameter("", "P27", distance, number, 365, 620, 160)
            attribute("", "R (Y/N)", label, "%s", none, none, "0", "0", 40, 700)
            parameter("", "P28", YesNo, number, 365, 695, 160)
            attribute("", "Wale welding (Y/N)", label, "%s", none, none, "0", "0", 5, 600)
            parameter("", "P31", YesNo, number, 365, 585, 160)
            attribute("", "T_BAR to BASE Plate", label, "%s", none, none, "0", "0", 585, 405)
            parameter("", "D114", distance, number, 815, 400, 160)
        }
    }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Base Mark | Text | `BP` |
| P2 | Thk. | Length | `20.00` |
| P3 | Matt. | Material | `SS400` |
| P4 | Top | Length | `70.00` |
| P5 | Bott | Length | `0.00` |
| P6 | Left | Length | `50.00` |
| P7 | Right | Length | `0.00` |
| P8 | Stiff Mark | Text | `SF` |
| P9 | Thk. | Length | `5.00` |
| P10 | Matt. | Material | `SS400` |
| P11 | h | Length | `0.00` |
| P11a | SF(Y/N) | Yes/No | `1` |
| P12 | Stiff Weld (Y/N) | Yes/No | `1` |
| P13 | Angle Size | Profile | `L75*75*6` |
| P13a | Matt. | Material | `SS400` |
| P13b | L_Mark | Text | `LL` |
| P14 | (Y/N) | Yes/No | `1` |
| P15 | T_bar Size | Profile | `CT97*150*6*9` |
| P15a | T_Bar Mark | Text | `BT` |
| P16 | Length | Length | `100.00` |
| P17 | Matt. | Material | `SS400` |
| P18 | T_bar (Y/N) | Yes/No | `1` |
| P19 | FB Mark | Text | `FB` |
| P20 | Matt. | Material | `SS400` |
| P21 | Wale Gap | Length | `5.00` |
| P22 | Wale weld (L or R) | Yes/No | `1` |
| P23 | Parameter23 | Yes/No | `if (P22==1) then 0 else 1 endif` *(formula)* |
| P24 | Parameter24 | Length | `fP(proT,"ID651B600B-0000-89EB-3136-393633303630")` *(formula)* |
| P25 | Parameter25 | Length | `if (P22==1) then (-1*(9+P24)) else 0 endif` *(formula)* |
| P26 | Type | Chamfer | `35.00` |
| P27 | R | Length | `35.00` |
| P28 | R (Y/N) | Yes/No | `1` |
| P29 | Parameter29 | Angle | `P30` *(formula)* |
| P30 | Parameter30 | Angle | `fAD("slope","ID651B600B-0000-89EB-3136-393633303630")` *(formula)* |
| P31 | Wale welding (Y/N) | Yes/No | `1` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.GIRDER | `` |
| D2 | D2.PLATE.GIRDER | `` |
| D3 | D3.PLATE.GIRDER | `` |
| D4 | D4.PLATE.GIRDER | `` |
| D5 | D5.PLATE.GIRDER | `P4` |
| D6 | D6.PLATE.GIRDER | `P4` |
| D7 | D7.PLATE.GIRDER | `P5` |
| D8 | D8.PLATE.GIRDER | `P5` |
| D9 | D9.PLATE.WALE_BEAM | `P6` |
| D10 | D10.PLATE.WALE_BEAM | `P6` |
| D11 | D11.PLATE.WALE_BEAM | `P7` |
| D12 | D12.PLATE.WALE_BEAM | `P7` |
| D13 | D13.PLATE.WALE_BEAM | `P11` |
| D14 | D14.PLATE.WALE_BEAM | `P11` |
| D15 | D15.PLATE.WALE_BEAM | `P11` |
| D16 | D16.PLATE.WALE_BEAM | `P11` |
| D17 | D17.CUTPART.PLATE | `` |
| D18 | D18.CUTPART.PLATE | `` |
| D19 | D19.CUTPART.PLATE | `` |
| D20 | D20.CUTPART.PLATE | `` |
| D21 | D21.CUTPART.PLATE | `` |
| D22 | D22.CUTPART.PLATE | `` |
| D23 | D23.CUTPART.PLATE | `` |
| D24 | D24.CUTPART.PLATE | `P9-2` |
| D25 | D25.CUTPART.PLATE | `` |
| D26 | D26.PLATE.PLATE | `` |
| D27 | D27.PLATE.PLATE | `` |
| D28 | D28.PLATE.PLATE | `` |
| D29 | D29.PLATE.PLATE | `` |
| D30 | D30.PLATE.GIRDER | `` |
| D31 | D31.PLATE.GIRDER | `` |
| D32 | D32.PLATE.PLATE | `` |
| D33 | D33.PLATE.PLATE | `` |
| D34 | D34.Angle.WALE_BEAM | `` |
| D35 | D35.Angle.WALE_BEAM | `` |
| D36 | D36.Angle.PLATE | `` |
| D37 | D37.Angle.PLATE | `` |
| D38 | D38.Angle.PLATE | `` |
| D39 | D39.Angle.PLATE | `` |
| D40 | D40.PLATE.WALE_BEAM | `P25` |
| D41 | D41.PLATE.WALE_BEAM | `P25` |
| D42 | D42.PLATE.WALE_BEAM | `P25` |
| D43 | D43.PLATE.WALE_BEAM | `P25` |
| D44 | D44.PLATE.WALE_BEAM | `P25` |
| D45 | D45.PLATE.WALE_BEAM | `P25` |
| D46 | D46.PLATE.WALE_BEAM | `P25` |
| D47 | D47.PLATE.WALE_BEAM | `P25` |
| D48 | D48.PLATE.PLATE | `25*cos(P30)` |
| D49 | D49.PLATE.PLATE | `` |
| D50 | D50.PLATE.PLATE | `` |
| D51 | D51.PLATE.PLATE | `25*cos(P30)` |
| D52 | D52.FITTING.PLATE | `` |
| D53 | D53.FITTING.PLATE | `` |
| D54 | D54.FITTING.PLATE | `` |
| D55 | D55.CUTPART.WALE_BEAM | `` |
| D56 | D56.CUTPART.WALE_BEAM | `` |
| D57 | D57.CUTPART.WALE_BEAM | `` |
| D58 | D58.CUTPART.WALE_BEAM | `` |
| D59 | D59.CUTPART.WALE_BEAM | `` |
| D60 | D60.CUTPART.WALE_BEAM | `` |
| D61 | D61.CUTPART.WALE_BEAM | `` |
| D62 | D62.CUTPART.WALE_BEAM | `` |
| D63 | D63.CUTPART.WALE_BEAM | `` |
| D64 | D64.CUTPART.PLATE | `P21` |
| D65 | D65.CUTPART.PLATE | `` |
| D66 | D66.CUTPART.PLATE | `` |
| D67 | D67.CUTPART.WALE_BEAM | `` |
| D68 | D68.CUTPART.WALE_BEAM | `` |
| D69 | D69.CUTPART.WALE_BEAM | `` |
| D70 | D70.CUTPART.WALE_BEAM | `` |
| D71 | D71.CUTPART.WALE_BEAM | `` |
| D72 | D72.CUTPART.WALE_BEAM | `` |
| D73 | D73.CUTPART.WALE_BEAM | `` |
| D74 | D74.CUTPART.WALE_BEAM | `` |
| D75 | D75.CUTPART.WALE_BEAM | `` |
| D76 | D76.CUTPART.WALE_BEAM | `` |
| D77 | D77.CUTPART.WALE_BEAM | `` |
| D78 | D78.CUTPART.WALE_BEAM | `` |
| D79 | D79.CUTPART.WALE_BEAM | `` |
| D80 | D80.CUTPART.WALE_BEAM | `` |
| D81 | D81.CUTPART.WALE_BEAM | `` |
| D82 | D82.CUTPART.WALE_BEAM | `` |
| D83 | D83.CUTPART.WALE_BEAM | `P24` |
| D84 | D84.CUTPART.WALE_BEAM | `P24` |
| D85 | D85.CUTPART.WALE_BEAM | `` |
| D86 | D86.CUTPART.PLATE | `P21` |
| D87 | D87.CUTPART.PLATE | `` |
| D88 | D88.CUTPART.PLATE | `` |
| D89 | D89.CUTPART.PLATE | `P21+P24-D85` |
| D90 | D90.CUTPART.PLATE | `P21` |
| D91 | D91.CUTPART.PLATE | `P21` |
| D92 | D92.CUTPART.PLATE | `` |
| D93 | D93.CUTPART.PLATE | `` |
| D94 | D94.CUTPART.PLATE | `P21+P24-D100` |
| D95 | D95.CUTPART.PLATE | `P21` |
| D96 | D96.CUTPART.WALE_BEAM | `` |
| D97 | D97.CUTPART.WALE_BEAM | `` |
| D98 | D98.CUTPART.WALE_BEAM | `P24` |
| D99 | D99.CUTPART.WALE_BEAM | `P24` |
| D100 | D100.CUTPART.WALE_BEAM | `` |
| D101 | D101.CUTPART.PLATE | `` |
| D102 | D102.CUTPART.PLATE | `` |
| D103 | D103.CUTPART.PLATE | `` |
| D104 | D104.CUTPART.PLATE | `` |
| D105 | D105.CUTPART.PLATE | `P21+P24-D117` |
| D106 | D106.CUTPART.PLATE | `P21` |
| D107 | D107.CUTPART.WALE_BEAM | `` |
| D108 | D108.CUTPART.WALE_BEAM | `` |
| D109 | D109.T_BAR.GIRDER | `` |
| D110 | D110.T_BAR.GIRDER | `` |
| D111 | D111.T_BAR.GIRDER | `` |
| D112 | D112.T_BAR.GIRDER | `` |
| D113 | D113.T_BAR.PLATE | `P16-D114` |
| D114 | T_BAR to BASE Plate | `` |
| D115 | D115.CUTPART.WALE_BEAM | `P24` |
| D116 | D116.CUTPART.WALE_BEAM | `P24` |
| D117 | D117.CUTPART.WALE_BEAM | `` |
| D118 | D118.CUTPART.PLATE | `P21` |
| D119 | D119.CUTPART.PLATE | `` |
| D120 | D120.CUTPART.PLATE | `` |
| D121 | D121.CUTPART.PLATE | `P21+P24-D127` |
| D122 | D122.CUTPART.PLATE | `P21` |
| D123 | D123.CUTPART.WALE_BEAM | `` |
| D124 | D124.CUTPART.WALE_BEAM | `` |
| D125 | D125.CUTPART.WALE_BEAM | `P24` |
| D126 | D126.CUTPART.WALE_BEAM | `P24` |
| D127 | D127.CUTPART.WALE_BEAM | `` |
| D128 | D128.CUTPART.WALE_BEAM | `` |
| D129 | D129.CUTPART.WALE_BEAM | `` |
| D130 | D130.CUTPART.WALE_BEAM | `` |
| D131 | D131.CUTPART.WALE_BEAM | `` |
| D132 | D132.CUTPART.WALE_BEAM | `` |
| D133 | D133.CUTPART.WALE_BEAM | `` |
| D134 | D134.CUTPART.WALE_BEAM | `` |
| D135 | D135.CUTPART.WALE_BEAM | `` |
| D136 | D136.CUTPART.WALE_BEAM | `` |
| D137 | D137.CUTPART.PLATE | `` |
| D138 | D138.CUTPART.PLATE | `` |
| D139 | D139.CUTPART.WALE_BEAM | `` |
| D140 | D140.CUTPART.WALE_BEAM | `` |
| D141 | D141.CUTPART.WALE_BEAM | `` |
| D142 | D142.CUTPART.WALE_BEAM | `` |
| D143 | D143.CUTPART.PLATE | `` |
| D144 | D144.CUTPART.PLATE | `` |
| D145 | D145.PLATE.PLATE | `25*cos(P30)` |
| D146 | D146.PLATE.PLATE | `` |
| D147 | D147.PLATE.PLATE | `` |
| D148 | D148.PLATE.PLATE | `25*cos(P30)` |
| D149 | D149.PLATE.WALE_BEAM | `` |
| D150 | D150.PLATE.WALE_BEAM | `` |
| D151 | D151.PLATE.WALE_BEAM | `` |
| D152 | D152.PLATE.WALE_BEAM | `` |
| D153 | D153.PLATE.WALE_BEAM | `` |
| D154 | D154.PLATE.WALE_BEAM | `` |
| D155 | D155.PLATE.WALE_BEAM | `` |
| D156 | D156.PLATE.WALE_BEAM | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#215 | CREATION | `P14` |
| obj#215 | MATERIAL | `P13a` |
| obj#215 | PROFILE | `P13` |
| obj#215 | SERIE | `P13b` |
| obj#227 | MATERIAL | `P3` |
| obj#227 | PROFILE | `"PL"+P2` |
| obj#227 | SERIE | `P1` |
| obj#239 | CREATION | `P11a` |
| obj#239 | MATERIAL | `P10` |
| obj#239 | PROFILE | `"PL"+P9` |
| obj#239 | SERIE | `P8` |
| obj#250 | CREATION | `P18` |
| obj#250 | MATERIAL | `P17` |
| obj#250 | PROFILE | `P15` |
| obj#250 | SERIE | `P15a` |
| obj#260 | MATERIAL | `P20` |
| obj#260 | SERIE | `P19` |
| obj#271 | MATERIAL | `P20` |
| obj#271 | SERIE | `P19` |
| obj#278 | CREATION | `if (P31==0) then P31 else P22 endif` |
| obj#291 | CREATION | `if (P31==0) then P31 else P22 endif` |
| obj#299 | CREATION | `if (P31==0) then P31 else P23 endif` |
| obj#310 | CREATION | `P28` |
| obj#321 | CREATION | `P28` |
| obj#337 | CREATION | `if (P31==0) then P31 else P23 endif` |
| obj#345 | CREATION | `P12` |

## Preset `#KK.p_0-DM-BEAM+WALE`
```
joint_attributes.saveas_file "#KK"
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
joint_attributes.D114 50.000000
joint_attributes.P31 1
joint_attributes.P28 1
joint_attributes.P26 2
joint_attributes.P22 0
joint_attributes.P15a "1MTB"
joint_attributes.P11a 0
joint_attributes.P20 "SS275"
joint_attributes.P19 "1FB"
joint_attributes.P18 1
joint_attributes.P17 "SM355"
joint_attributes.P16 550.000000
joint_attributes.P15 "BT150*174*6*9"
joint_attributes.P14 0
joint_attributes.P12 0
joint_attributes.P10 "SM355"
joint_attributes.P9 14.000000
joint_attributes.P8 "1SF"
joint_attributes.P7 50.000000
joint_attributes.P6 50.000000
joint_attributes.P5 150.000000
joint_attributes.P4 20.000000
joint_attributes.P3 "SM355"
joint_attributes.P2 21.000000
joint_attributes.P1 "1BP"
```

## Preset `+1.p_0-DM-BEAM+WALE`
```
joint_attributes.saveas_file "+1"
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
joint_attributes.D114 -2147483648.000000
joint_attributes.P31 1
joint_attributes.P28 1
joint_attributes.P27 -2147483648.000000
joint_attributes.P26 0
joint_attributes.P22 1
joint_attributes.P21 -2147483648.000000
joint_attributes.P15a ""
joint_attributes.P13b ""
joint_attributes.P11a 1
joint_attributes.P20 "SS275"
joint_attributes.P19 "FB"
joint_attributes.P13a ""
joint_attributes.P18 1
joint_attributes.P17 ""
joint_attributes.P16 -2147483648.000000
joint_attributes.P15 ""
joint_attributes.P14 0
joint_attributes.P13 ""
joint_attributes.P12 1
joint_attributes.P11 -2147483648.000000
joint_attributes.P10 ""
joint_attributes.P9 -2147483648.000000
joint_attributes.P8 ""
joint_attributes.P7 0.000000
joint_attributes.P6 0.000000
joint_attributes.P5 0.000000
joint_attributes.P4 0.000000
joint_attributes.P3 "SM355"
joint_attributes.P2 19.000000
joint_attributes.P1 "BP"
```

## Preset `1.p_0-DM-BEAM+WALE`
```
joint_attributes.saveas_file "1"
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
joint_attributes.D114 -2147483648.000000
joint_attributes.P31 1
joint_attributes.P28 1
joint_attributes.P27 -2147483648.000000
joint_attributes.P26 0
joint_attributes.P22 1
joint_attributes.P21 -2147483648.000000
joint_attributes.P15a ""
joint_attributes.P13b ""
joint_attributes.P11a 1
joint_attributes.P20 ""
joint_attributes.P19 ""
joint_attributes.P13a ""
joint_attributes.P18 1
joint_attributes.P17 ""
joint_attributes.P16 -2147483648.000000
joint_attributes.P15 ""
joint_attributes.P14 1
joint_attributes.P13 ""
joint_attributes.P12 1
joint_attributes.P11 -2147483648.000000
joint_attributes.P10 ""
joint_attributes.P9 -2147483648.000000
joint_attributes.P8 ""
joint_attributes.P7 -2147483648.000000
joint_attributes.P6 -2147483648.000000
joint_attributes.P5 -2147483648.000000
joint_attributes.P4 -2147483648.000000
joint_attributes.P3 ""
joint_attributes.P2 -2147483648.000000
joint_attributes.P1 ""
```