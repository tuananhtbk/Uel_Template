# 0-DM-LUG+RB-OK-REV1

- File: `0-DM-LUG+RB-OK-REV1.uel`  (20,763 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	0-DM-LUG+RB-OK-REV1	All`
- Dialog parameters: 0 | Variables (P/custom): 31 | Distance vars (D): 90 | Parts with bound profile: 7
- Lookup files (fVF): -
- Functions used: fP, fTpl, if, int, mid
- Dialog bitmaps: 0 | Saved presets: +1.p_0-DM-LUG+RB-OK-REV1, +82ROFF.p_0-DM-LUG+RB-OK-REV1, 1B.p_0-DM-LUG+RB-OK-REV1, B2.p_0-DM-LUG+RB-OK-REV1

## Dialog (.inp)
```
page("TeklaStructures", "")
{
    detail(1, "0-DM-LUG+RB-OK-REV1")
    {
        tab_page("", " Parameters 1 ", 1)
        {
            attribute("", "LUG Mark", label, "%s", none, none, "0", "0", 20, 0)
            parameter("", "P1", string, text, 374, 0, 160)
            attribute("", "Matt.", label, "%s", none, none, "0", "0", 20, 28)
            parameter("", "P2", material, text, 374, 28, 160)
            attribute("", "LUG Hole", label, "%s", none, none, "0", "0", 686, 0)
            parameter("", "P3", distance, number, 924, 0, 160)
            attribute("", "LUG. Thk", label, "%s", none, none, "0", "0", 20, 84)
            parameter("", "P7", integer, number, 374, 84, 160)
            attribute("", "CTR (Y/N)*****", label, "%s", none, none, "0", "0", 679, 59)
            parameter("", "P10", YesNo, number, 926, 58, 160)
            attribute("", "RB Mark", label, "%s", none, none, "0", "0", 16, 223)
            parameter("", "P11", string, text, 372, 218, 160)
            attribute("", "Profile", label, "%s", none, none, "0", "0", 15, 254)
            parameter("", "P12", profile, text, 372, 253, 160)
            attribute("", "Matt.", label, "%s", none, none, "0", "0", 19, 293)
            parameter("", "P13", material, text, 371, 288, 160)
            attribute("", "NS (Y/N)", label, "%s", none, none, "0", "0", 16, 329)
            parameter("", "P14", YesNo, number, 371, 325, 160)
            attribute("", "FS (Y/N)", label, "%s", none, none, "0", "0", 20, 361)
            parameter("", "P15", YesNo, number, 372, 362, 160)
            attribute("", "START POINT", label, "%s", none, none, "0", "0", 16, 401)
            parameter("", "P20", integer, number, 374, 401, 160)
            attribute("", "LUG Height", label, "%s", none, none, "0", "0", 19, 55)
            parameter("", "P8", distance, number, 375, 58, 160)
            attribute("", "CLASS", label, "%s", none, none, "0", "0", 19, 113)
            parameter("", "P23", integer, number, 374, 115, 160)
            attribute("", "@RB", label, "%s", none, none, "0", "0", 22, 440)
            parameter("", "P24", integer, number, 374, 438, 160)
            attribute("", "L(Y/N)**********", label, "%s", none, none, "0", "0", 679, 91)
            parameter("", "P27", YesNo, number, 927, 85, 160)
            attribute("", "R(Y/N)**********", label, "%s", none, none, "0", "0", 681, 120)
            parameter("", "P28", YesNo, number, 928, 116, 160)
            attribute("", "CTR'D to LUG", label, "%s", none, none, "0", "0", 678, 29)
            parameter("", "P29", distance, number, 926, 29, 160)
            attribute("", "RB Copy (Y/N)", label, "%s", none, none, "0", "0", 20, 476)
            parameter("", "P30", YesNo, number, 374, 476, 160)
            attribute("", "------------------------------------------------------------------------------------------------------------------------------------------------------------", label, "%s", none, none, "0", "0", 19, 172)
        }
    }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | LUG Mark | Text | `P32+"LG"` *(formula)* |
| P2 | Matt. | Material | `SS275` |
| P3 | Hole | Length | `30.00` |
| P4 | LUG Profile | Profile | `"PL"+P7` *(formula)* |
| P5 | Gap to Center | Length | `500.00` |
| P6 | Parameter6 | Length | `fP(proH,"ID658CEFAB-0000-31D8-3137-303337333635")` *(formula)* |
| P7 | LUG. Thk | Integer | `12` |
| P8 | LUG Height | Length | `100.00` |
| P9 | Parameter9 | Integer | `0` |
| P10 | CTR (Y/N)***** | Yes/No | `1` |
| P11 | RB Mark | Text | `P32+"RB"` *(formula)* |
| P13 | Matt. | Material | `SS275` |
| P14 | NS (Y/N) | Yes/No | `1` |
| P15 | FS (Y/N) | Yes/No | `1` |
| P16 | Parameter16 | Length | `fTpl("LENGTH","ID658CEFAB-0000-31D8-3137-303337333635")` *(formula)* |
| P17 | Parameter17 | Length | `if (P16<3000) then 1000 else 2000 endif` *(formula)* |
| P18 | Parameter18 | Integer | `if (int(P19/P17)-1<=0) then 0 else int(P19/P17)-1 endif` *(formula)* |
| P19 | L-2xSTART POINT | Integer | `P16-2*P20` *(formula)* |
| P20 | START POINT | Integer | `500` |
| P21 | Parameter21 | Length | `fTpl("LENGTH","ID658CEFAB-0000-31D8-3137-303337333635")` *(formula)* |
| P22 | Parameter22 | Length | `floor(P21/40)*10` *(formula)* |
| P23 | CLASS | Integer | `12` |
| P24 | @RB | Integer | `1500` |
| P25 | Parameter25 | Integer | `ceil(P19/P24)` *(formula)* |
| P26 | Parameter26 | Integer | `1*(0.5*P16-P20)` *(formula)* |
| P27 | L(Y/N)********** | Yes/No | `1` |
| P28 | R(Y/N)********** | Yes/No | `1` |
| P29 | CTR'D to LUG | Length | `0.00` |
| P30 | RB Copy | Yes/No | `1` |
| P31 | Parameter31 | Text | `fP(proSERIE,"ID658CEFAB-0000-31D8-3137-303337333635")` *(formula)* |
| P32 | Parameter32 | Text | `mid(P31,0,find(P31,"M"))` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.BEAM | `P22+50+P29` |
| D2 | D2.PLATE.BEAM | `P22-50+P29` |
| D3 | D3.PLATE.BEAM | `P22-50+P29` |
| D4 | D4.PLATE.BEAM | `50.00+P29` |
| D5 | D5.PLATE.BEAM | `50.00-P29` |
| D6 | D6.PLATE.BEAM | `50.00-P29` |
| D7 | D7.PLATE.BEAM | `50.00+P29` |
| D8 | D8.PLATE.BEAM | `P8` |
| D9 | D9.PLATE.BEAM | `P8` |
| D10 | D10.BOLT.PLATE | `` |
| D11 | D11.BOLT.PLATE | `` |
| D12 | D12.BOLT.PLATE | `` |
| D13 | D13.BOLT.PLATE | `` |
| D14 | D14.BOLT.PLATE | `` |
| D15 | D15.PLATE.BEAM | `` |
| D16 | D16.PLATE.BEAM | `` |
| D17 | D17.PLATE.BEAM | `` |
| D18 | D18.PLATE.BEAM | `` |
| D19 | D19.BOLT.PLATE | `` |
| D20 | D20.BOLT.PLATE | `` |
| D21 | D21.BOLT.PLATE | `` |
| D22 | D22.BOLT.PLATE | `` |
| D23 | D23.BOLT.PLATE | `` |
| D24 | D24.BOLT.PLATE | `` |
| D25 | D25.BOLT.PLATE | `` |
| D26 | D26.BOLT.PLATE | `` |
| D27 | D27.BOLT.BEAM | `` |
| D28 | D28.BOLT.PLATE | `` |
| D29 | D29.PLATE.BEAM | `` |
| D30 | D30.PLATE.BEAM | `` |
| D31 | D31.BOLT.PLATE | `` |
| D32 | D32.BOLT.PLATE | `` |
| D33 | D33.BOLT.PLATE | `` |
| D37 | D37.PLATE.BEAM | `P8` |
| D38 | D38.PLATE.BEAM | `P8` |
| D39 | D39.PLATE.BEAM | `` |
| D40 | D40.PLATE.BEAM | `` |
| D41 | D41.PLATE.BEAM | `P8` |
| D42 | D42.PLATE.BEAM | `P8` |
| D43 | D43.PLATE.BEAM | `` |
| D44 | D44.PLATE.BEAM | `` |
| D45 | D45.PLATE.BEAM | `` |
| D46 | D46.PLATE.BEAM | `` |
| D47 | D47.PLATE.BEAM | `` |
| D48 | D48.PLATE.BEAM | `` |
| D49 | D49.PLATE.BEAM | `` |
| D50 | D50.PLATE.BEAM | `` |
| D51 | D51.PLATE.BEAM | `` |
| D52 | D52.PLATE.BEAM | `` |
| D55 | D55.ROUND_BAR.BEAM | `` |
| D56 | D56.ROUND_BAR.BEAM | `` |
| D57 | D57.ROUND_BAR.BEAM | `` |
| D58 | D58.ROUND_BAR.BEAM | `` |
| D59 | D59.ROUND_BAR.BEAM | `` |
| D60 | D60.ROUND_BAR.BEAM | `` |
| D61 | D61.ROUND_BAR.BEAM | `` |
| D62 | D62.ROUND_BAR.BEAM | `` |
| D63 | D63.ROUND_BAR.BEAM | `` |
| D64 | D64.ROUND_BAR.BEAM | `` |
| D65 | D65.ROUND_BAR.BEAM | `` |
| D66 | D66.ROUND_BAR.BEAM | `` |
| D67 | D67.ROUND_BAR.BEAM | `P26` |
| D68 | D68.ROUND_BAR.BEAM | `P26` |
| D69 | D69.ROUND_BAR.BEAM | `P26` |
| D70 | D70.ROUND_BAR.BEAM | `P26` |
| D71 | D71.ROUND_BAR.BEAM | `P26` |
| D72 | D72.ROUND_BAR.BEAM | `P26` |
| D73 | D73.ROUND_BAR.BEAM | `` |
| D74 | D74.ROUND_BAR.BEAM | `` |
| D75 | D75.ROUND_BAR.BEAM | `` |
| D76 | D76.ROUND_BAR.BEAM | `` |
| D77 | D77.ROUND_BAR.BEAM | `` |
| D78 | D78.ROUND_BAR.BEAM | `` |
| D79 | D79.ROUND_BAR.BEAM | `` |
| D80 | D80.ROUND_BAR.BEAM | `` |
| D81 | D81.ROUND_BAR.BEAM | `` |
| D82 | D82.ROUND_BAR.BEAM | `` |
| D83 | D83.ROUND_BAR.BEAM | `` |
| D84 | D84.ROUND_BAR.BEAM | `` |
| D85 | D85.ROUND_BAR.BEAM | `P26` |
| D86 | D86.ROUND_BAR.BEAM | `P26` |
| D87 | D87.ROUND_BAR.BEAM | `P26` |
| D88 | D88.ROUND_BAR.BEAM | `P26` |
| D89 | D89.ROUND_BAR.BEAM | `P26` |
| D90 | D90.ROUND_BAR.BEAM | `P26` |
| D145 | D145.PLATE.BEAM | `P22-50-P29` |
| D146 | D146.PLATE.BEAM | `P22+50-P29` |
| D147 | D147.PLATE.BEAM | `P22+50-P29` |
| D148 | D148.PLATE.BEAM | `P22-50-P29` |
| D149 | D149.PLATE.BEAM | `P22+50+P29` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#158 | CLASS | `P23` |
| obj#158 | CREATION | `P10` |
| obj#158 | MATERIAL | `P2` |
| obj#158 | PROFILE | `P4` |
| obj#158 | SERIE | `P1` |
| obj#169 | CLASS | `P23` |
| obj#169 | CREATION | `P28` |
| obj#169 | MATERIAL | `P2` |
| obj#169 | PROFILE | `P4` |
| obj#169 | SERIE | `P1` |
| obj#175 | CLASS | `P23` |
| obj#175 | CREATION | `P27` |
| obj#175 | MATERIAL | `P2` |
| obj#175 | PROFILE | `P4` |
| obj#175 | SERIE | `P1` |
| obj#181 | CREATION | `P15` |
| obj#181 | MATERIAL | `P13` |
| obj#181 | PROFILE | `P12` |
| obj#181 | SERIE | `P11` |
| obj#193 | CREATION | `P14` |
| obj#193 | MATERIAL | `P13` |
| obj#193 | PROFILE | `P12` |
| obj#193 | SERIE | `P11` |
| obj#202 | CREATION | `P15` |
| obj#202 | MATERIAL | `P13` |
| obj#202 | PROFILE | `P12` |
| obj#202 | SERIE | `P11` |
| obj#209 | CREATION | `P14` |
| obj#209 | MATERIAL | `P13` |
| obj#209 | PROFILE | `P12` |
| obj#209 | SERIE | `P11` |
| obj#889 | CREATION | `P30` |
| obj#920 | CREATION | `P30` |
| obj#974 | DX1 | `P8/2` |
| obj#974 | TOLERANCE | `P3-20` |
| obj#1012 | DX1 | `P8/2` |
| obj#1012 | TOLERANCE | `P3-20` |
| obj#1018 | DX1 | `P8/2` |
| obj#1018 | TOLERANCE | `P3-20` |

## Preset `+1.p_0-DM-LUG+RB-OK-REV1`
```
joint_attributes.saveas_file "+1"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.BottomRight 0
joint_attributes.BottomMiddle 0
joint_attributes.BottomLeft 0
joint_attributes.MiddleRight 0
joint_attributes.MiddleLeft 0
joint_attributes.TopRight 0
joint_attributes.TopMiddle 0
joint_attributes.TopLeft 0
joint_attributes.UpMiddleRight 0
joint_attributes.UpMiddleMiddle 0
joint_attributes.UpMiddleLeft 0
joint_attributes.MiddleMiddle 1
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P30 1
joint_attributes.P29 -2147483648.000000
joint_attributes.P28 1
joint_attributes.P27 1
joint_attributes.P24 -2147483648
joint_attributes.P23 -2147483648
joint_attributes.P8 -2147483648.000000
joint_attributes.P20 -2147483648
joint_attributes.P15 1
joint_attributes.P14 1
joint_attributes.P13 ""
joint_attributes.P12 ""
joint_attributes.P11 ""
joint_attributes.P10 1
joint_attributes.P7 -2147483648
joint_attributes.P3 -2147483648.000000
joint_attributes.P2 ""
joint_attributes.P1 ""
```

## Preset `+82ROFF.p_0-DM-LUG+RB-OK-REV1`
```
joint_attributes.saveas_file "+82ROFF"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.BottomRight 0
joint_attributes.BottomMiddle 0
joint_attributes.BottomLeft 0
joint_attributes.MiddleRight 0
joint_attributes.MiddleLeft 0
joint_attributes.TopRight 0
joint_attributes.TopMiddle 0
joint_attributes.TopLeft 0
joint_attributes.UpMiddleRight 0
joint_attributes.UpMiddleMiddle 0
joint_attributes.UpMiddleLeft 0
joint_attributes.MiddleMiddle 1
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P30 1
joint_attributes.P29 0.000000
joint_attributes.P28 1
joint_attributes.P27 1
joint_attributes.P24 1500
joint_attributes.P23 22
joint_attributes.P8 80.000000
joint_attributes.P20 500
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 "SS275"
joint_attributes.P12 "RB12"
joint_attributes.P11 "1B-RB"
joint_attributes.P10 0
joint_attributes.P7 12
joint_attributes.P3 30.000000
joint_attributes.P2 "SS275"
joint_attributes.P1 "1B-LG"
```

## Preset `1B.p_0-DM-LUG+RB-OK-REV1`
```
joint_attributes.saveas_file "1B"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.BottomRight 0
joint_attributes.BottomMiddle 0
joint_attributes.BottomLeft 0
joint_attributes.MiddleRight 0
joint_attributes.MiddleLeft 0
joint_attributes.TopRight 0
joint_attributes.TopMiddle 0
joint_attributes.TopLeft 0
joint_attributes.UpMiddleRight 0
joint_attributes.UpMiddleMiddle 0
joint_attributes.UpMiddleLeft 0
joint_attributes.MiddleMiddle 1
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P30 1
joint_attributes.P29 0.000000
joint_attributes.P28 1
joint_attributes.P27 1
joint_attributes.P24 1500
joint_attributes.P23 12
joint_attributes.P8 80.000000
joint_attributes.P20 500
joint_attributes.P15 1
joint_attributes.P14 1
joint_attributes.P13 "SS275"
joint_attributes.P12 "RB12"
joint_attributes.P11 "1B-RB"
joint_attributes.P10 1
joint_attributes.P7 12
joint_attributes.P3 30.000000
joint_attributes.P2 "SS275"
joint_attributes.P1 "1B-LG"
```

## Preset `B2.p_0-DM-LUG+RB-OK-REV1`
```
joint_attributes.saveas_file "B2"
joint_attributes.get_menu "B2"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.BottomRight 0
joint_attributes.BottomMiddle 0
joint_attributes.BottomLeft 0
joint_attributes.MiddleRight 0
joint_attributes.MiddleLeft 0
joint_attributes.TopRight 0
joint_attributes.TopMiddle 0
joint_attributes.TopLeft 0
joint_attributes.UpMiddleRight 0
joint_attributes.UpMiddleMiddle 0
joint_attributes.UpMiddleLeft 0
joint_attributes.MiddleMiddle 1
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P30 1
joint_attributes.P29 0.000000
joint_attributes.P28 1
joint_attributes.P27 1
joint_attributes.P24 1500
joint_attributes.P23 7
joint_attributes.P8 80.000000
joint_attributes.P20 500
joint_attributes.P15 1
joint_attributes.P14 1
joint_attributes.P13 "SS275"
joint_attributes.P12 "RB12"
joint_attributes.P11 "B2-RB"
joint_attributes.P10 0
joint_attributes.P7 12
joint_attributes.P3 30.000000
joint_attributes.P2 "SS275"
joint_attributes.P1 "B2-LG"
```