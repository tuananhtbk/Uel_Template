# 0-P-STUD-(FLG+WEB)-2

- File: `STUD-ACT.uel`  (12,491 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	0-P-STUD-(FLG+WEB)-2	All|	ACT-COL`
- Dialog parameters: 22 | Variables (P/custom): 26 | Distance vars (D): 20 | Parts with bound profile: 0
- Lookup files (fVF): -
- Functions used: if
- Dialog bitmaps: 0 | Saved presets: #10K.p_0-P-STUD-(FLG+WEB)-2, #11K.p_0-P-STUD-(FLG+WEB)-2, #12K.p_0-P-STUD-(FLG+WEB)-2, #14K.p_0-P-STUD-(FLG+WEB)-2, #8K.p_0-P-STUD-(FLG+WEB)-2, #9K.p_0-P-STUD-(FLG+WEB)-2, +LAM.p_0-P-STUD-(FLG+WEB)-2, 1.p_0-P-STUD-(FLG+WEB)-2, 1N2W.p_0-P-STUD-(FLG+WEB)-2, 2.p_0-P-STUD-(FLG+WEB)-2, 3-200-2.p_0-P-STUD-(FLG+WEB)-2, 3-200.p_0-P-STUD-(FLG+WEB)-2, 300.p_0-P-STUD-(FLG+WEB)-2, 4-150-2.p_0-P-STUD-(FLG+WEB)-2, 4-150.p_0-P-STUD-(FLG+WEB)-2, 4-200-2.p_0-P-STUD-(FLG+WEB)-2, 4-200.p_0-P-STUD-(FLG+WEB)-2, 600.p_0-P-STUD-(FLG+WEB)-2, 7-100.p_0-P-STUD-(FLG+WEB)-2, 7-150-2.p_0-P-STUD-(FLG+WEB)-2, 7-150.p_0-P-STUD-(FLG+WEB)-2, A-1.p_0-P-STUD-(FLG+WEB)-2, A-2.p_0-P-STUD-(FLG+WEB)-2, KK.p_0-P-STUD-(FLG+WEB)-2

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "0-P-STUD-(FLG+WEB)-2")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("BOLT Q'TY(FLG.)", "P1", integer, number, 1)
           parameter("BOLT TO CENTER", "P2", distance, number, 2)
           parameter("START FLG.", "P3", distance, number, 3)
           parameter("@", "P5", distance, number, 4)
           parameter("Stud standard", "P6_standard", stud_standard, text, 5)
           parameter("Stud size", "P6_size", stud_size, number, 6)
           parameter("Stud length", "P6_length", stud_length, number, 7)
           parameter("Top(Y/N)", "P8", YesNo, number, 8)
           parameter("Bott(Y/N)", "P9", YesNo, number, 9)
           parameter("Stud standard", "P7_standard", stud_standard, text, 10)
           parameter("Stud length", "P7_length", stud_length, number, 11)
           parameter("Stud size", "P7_size", stud_size, number, 12)
           parameter("BOLT Q'TY-X (WEB)", "P10", integer, number, 13)
           parameter("WEB@X", "P11", distance, number, 14)
           parameter("UP/DN", "P12", distance, number, 15)
           parameter("BOLT Q'TY-Y (WEB)", "P14", integer, number, 16)
           parameter("START WEB.", "P16", integer, number, 17)
           parameter("NS(Y/N)", "P17", YesNo, number, 18)
           parameter("FS(Y/N)", "P18", YesNo, number, 19)
           parameter("WEB@Y", "P19", distance, number, 20)
           parameter("BOLT Q'TY-Y (FLG)", "P20", integer, number, 21)
           parameter("FLG@Y", "P22", distance, number, 22)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | BOLT Q'TY(FLG.) | Integer | `6` |
| P2 | BOLT TO CENTER | Length | `0.00` |
| P3 | START FLG. | Length | `175.00` |
| P4 | Parameter4 | 9 | `(P1-1)+"*"+P5` *(formula)* |
| P5 | @ | Length | `200.00` |
| P6_length | Stud length | 23 | `0.00` |
| P6_size | Stud size | 15 | `0.00` |
| P6_standard | Stud standard | 16 | `0.00` |
| P7_length | Stud length | 23 | `150.00` |
| P7_size | Stud size | 15 | `19.00` |
| P7_standard | Stud standard | 16 | `0.00` |
| P8 | Top(Y/N) | Yes/No | `1` |
| P9 | Bott(Y/N) | Yes/No | `1` |
| P10 | BOLT Q'TY-X (WEB) | Integer | `7` |
| P11 | WEB@X | Length | `100.00` |
| P12 | UP/DN | Length | `0.00` |
| P13 | Parameter13 | 9 | `(P10-1)+"*"+P11` *(formula)* |
| P14 | BOLT Q'TY-Y (WEB) | Integer | `2` |
| P15 | Parameter15 | 9 | `if (P14==1) then 0 else (P14-1)+"*"+P19  endif` *(formula)* |
| P16 | START WEB. | Integer | `100` |
| P17 | NS(Y/N) | Yes/No | `1` |
| P18 | FS(Y/N) | Yes/No | `1` |
| P19 | WEB@Y | Length | `200.00` |
| P20 | BOLT Q'TY-Y (FLG) | Integer | `2` |
| P21 | Parameter21 | 9 | `if (P20==1) then 0 else (P20-1)+"*"+P22  endif` *(formula)* |
| P22 | FLG@Y | Length | `150.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.BOLT.T-BEAM | `` |
| D2 | D2.BOLT.T-BEAM | `` |
| D3 | D3.BOLT.T-BEAM | `` |
| D4 | D4.BOLT.T-BEAM | `` |
| D5 | D5.BOLT.0-P-STUD(T+B) | `` |
| D6 | D6.BOLT.0-P-STUD(T+B) | `` |
| D7 | D7.BOLT.T-BEAM | `P2` |
| D8 | D8.BOLT.T-BEAM | `P2` |
| D9 | D9.BOLT.T-BEAM | `P2` |
| D10 | D10.BOLT.T-BEAM | `P2` |
| D11 | D11.BOLT.T-BEAM | `` |
| D12 | D12.BOLT.T-BEAM | `` |
| D13 | D13.BOLT.T-BEAM | `` |
| D14 | D14.BOLT.T-BEAM | `` |
| D15 | D15.BOLT.T-BEAM | `P12` |
| D16 | D16.BOLT.T-BEAM | `P12` |
| D17 | D17.BOLT.T-BEAM | `P12` |
| D18 | D18.BOLT.T-BEAM | `P12` |
| D19 | D19.BOLT.0-P-STUD-(FLG+WEB) | `` |
| D20 | D20.BOLT.0-P-STUD-(FLG+WEB) | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#221 | BOLT_GROUP_DISTX | `P4` |
| obj#221 | BOLT_GROUP_DISTY | `P21` |
| obj#221 | CREATION | `P8` |
| obj#221 | DX1 | `P3` |
| obj#221 | EXTRA_LENGTH | `P6_length` |
| obj#221 | SIZE | `P6_size` |
| obj#235 | BOLT_GROUP_DISTX | `P4` |
| obj#235 | BOLT_GROUP_DISTY | `P21` |
| obj#235 | CREATION | `P9` |
| obj#235 | DX1 | `P3` |
| obj#235 | EXTRA_LENGTH | `P6_length` |
| obj#235 | SIZE | `P6_size` |
| obj#363 | BOLT_GROUP_DISTX | `P13` |
| obj#363 | BOLT_GROUP_DISTY | `P15` |
| obj#363 | CREATION | `P17` |
| obj#363 | DX1 | `P16` |
| obj#363 | EXTRA_LENGTH | `P7_length` |
| obj#363 | SIZE | `P7_size` |
| obj#377 | BOLT_GROUP_DISTX | `P13` |
| obj#377 | BOLT_GROUP_DISTY | `P15` |
| obj#377 | CREATION | `P18` |
| obj#377 | DX1 | `P16` |
| obj#377 | EXTRA_LENGTH | `P7_length` |
| obj#377 | SIZE | `P7_size` |

## Preset `#10K.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "#10K"
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
joint_attributes.P22 200.000000
joint_attributes.P20 3
joint_attributes.P19 200.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 500
joint_attributes.P14 3
joint_attributes.P11 120.000000
joint_attributes.P10 20
joint_attributes.P7_size 22.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 22.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 120.000000
joint_attributes.P3 500.000000
joint_attributes.P1 20
```

## Preset `#11K.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "#11K"
joint_attributes.get_menu "#12K"
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
joint_attributes.P22 200.000000
joint_attributes.P20 3
joint_attributes.P19 200.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 500
joint_attributes.P14 3
joint_attributes.P11 120.000000
joint_attributes.P10 22
joint_attributes.P7_size 22.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 22.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 120.000000
joint_attributes.P3 500.000000
joint_attributes.P1 22
```

## Preset `#12K.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "#12K"
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
joint_attributes.P22 200.000000
joint_attributes.P20 3
joint_attributes.P19 200.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 500
joint_attributes.P14 3
joint_attributes.P11 120.000000
joint_attributes.P10 24
joint_attributes.P7_size 22.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 22.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 120.000000
joint_attributes.P3 500.000000
joint_attributes.P1 24
```

## Preset `#14K.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "#14K"
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
joint_attributes.P22 200.000000
joint_attributes.P20 3
joint_attributes.P19 200.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 500
joint_attributes.P14 3
joint_attributes.P11 120.000000
joint_attributes.P10 27
joint_attributes.P7_size 22.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 22.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 120.000000
joint_attributes.P3 500.000000
joint_attributes.P1 27
```

## Preset `#8K.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "#8K"
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
joint_attributes.zang1 -0.000000
joint_attributes.zsuunta 7
joint_attributes.P22 200.000000
joint_attributes.P20 3
joint_attributes.P19 200.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 500
joint_attributes.P14 3
joint_attributes.P11 120.000000
joint_attributes.P10 16
joint_attributes.P7_size 22.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 22.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 120.000000
joint_attributes.P3 500.000000
joint_attributes.P1 16
```

## Preset `#9K.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "#9K"
joint_attributes.get_menu "#11K"
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
joint_attributes.P22 200.000000
joint_attributes.P20 3
joint_attributes.P19 200.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 500
joint_attributes.P14 3
joint_attributes.P11 120.000000
joint_attributes.P10 18
joint_attributes.P7_size 22.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 22.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 120.000000
joint_attributes.P3 500.000000
joint_attributes.P1 18
```

## Preset `+LAM.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "+LAM"
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
joint_attributes.P22 250.000000
joint_attributes.P20 2
joint_attributes.P19 200.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 2
joint_attributes.P11 200.000000
joint_attributes.P10 3
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 95.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 95.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 200.000000
joint_attributes.P3 100.000000
joint_attributes.P2 0.000000
joint_attributes.P1 3
```

## Preset `1.p_0-P-STUD-(FLG+WEB)-2`
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P22 0.000000
joint_attributes.P20 1
joint_attributes.P19 0.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 1
joint_attributes.P11 300.000000
joint_attributes.P10 10
joint_attributes.P7_size 22.000000
joint_attributes.P7_length 110.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 110.000000
joint_attributes.P6_size 22.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 300.000000
joint_attributes.P3 100.000000
joint_attributes.P1 10
```

## Preset `1N2W.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "1N2W"
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
joint_attributes.P22 150.000000
joint_attributes.P20 2
joint_attributes.P19 150.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 2
joint_attributes.P11 300.000000
joint_attributes.P10 4
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 300.000000
joint_attributes.P3 100.000000
joint_attributes.P1 4
```

## Preset `2.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "2"
joint_attributes.get_menu "1"
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
joint_attributes.P22 150.000000
joint_attributes.P20 2
joint_attributes.P19 150.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 400
joint_attributes.P14 2
joint_attributes.P11 400.000000
joint_attributes.P10 8
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 400.000000
joint_attributes.P3 400.000000
joint_attributes.P1 8
```

## Preset `3-200-2.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "3-200-2"
joint_attributes.get_menu "3-200"
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
joint_attributes.P22 250.000000
joint_attributes.P20 2
joint_attributes.P19 250.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 400
joint_attributes.P14 2
joint_attributes.P11 400.000000
joint_attributes.P10 6
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 400.000000
joint_attributes.P3 400.000000
joint_attributes.P1 6
```

## Preset `3-200.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "3-200"
joint_attributes.get_menu "300"
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
joint_attributes.P22 250.000000
joint_attributes.P20 2
joint_attributes.P19 250.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 2
joint_attributes.P11 200.000000
joint_attributes.P10 3
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 200.000000
joint_attributes.P3 100.000000
joint_attributes.P1 3
```

## Preset `300.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "300"
joint_attributes.get_menu "300"
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
joint_attributes.P22 250.000000
joint_attributes.P20 2
joint_attributes.P19 250.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 2
joint_attributes.P11 300.000000
joint_attributes.P10 4
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 300.000000
joint_attributes.P3 100.000000
joint_attributes.P1 4
```

## Preset `4-150-2.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "4-150-2"
joint_attributes.get_menu "4-150"
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
joint_attributes.P22 150.000000
joint_attributes.P20 2
joint_attributes.P19 150.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 300
joint_attributes.P14 2
joint_attributes.P11 300.000000
joint_attributes.P10 8
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 300.000000
joint_attributes.P3 300.000000
joint_attributes.P1 8
```

## Preset `4-150.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "4-150"
joint_attributes.get_menu "3-200-2"
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
joint_attributes.P22 150.000000
joint_attributes.P20 2
joint_attributes.P19 150.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 2
joint_attributes.P11 150.000000
joint_attributes.P10 4
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 150.000000
joint_attributes.P3 100.000000
joint_attributes.P1 4
```

## Preset `4-200-2.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "4-200-2"
joint_attributes.get_menu "3-200-2"
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
joint_attributes.P22 250.000000
joint_attributes.P20 2
joint_attributes.P19 200.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 400
joint_attributes.P14 2
joint_attributes.P11 400.000000
joint_attributes.P10 6
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 400.000000
joint_attributes.P3 400.000000
joint_attributes.P2 0.000000
joint_attributes.P1 6
```

## Preset `4-200.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "4-200"
joint_attributes.get_menu "4-200"
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
joint_attributes.P22 250.000000
joint_attributes.P20 2
joint_attributes.P19 200.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 2
joint_attributes.P11 200.000000
joint_attributes.P10 4
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 200.000000
joint_attributes.P3 100.000000
joint_attributes.P2 0.000000
joint_attributes.P1 4
```

## Preset `600.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "600"
joint_attributes.get_menu "600"
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
joint_attributes.P22 250.000000
joint_attributes.P20 2
joint_attributes.P19 250.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 600
joint_attributes.P14 2
joint_attributes.P11 600.000000
joint_attributes.P10 4
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 600.000000
joint_attributes.P3 600.000000
joint_attributes.P1 4
```

## Preset `7-100.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "7-100"
joint_attributes.get_menu "4-200-2"
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
joint_attributes.P22 250.000000
joint_attributes.P20 2
joint_attributes.P19 200.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 2
joint_attributes.P11 100.000000
joint_attributes.P10 7
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 100.000000
joint_attributes.P3 100.000000
joint_attributes.P2 0.000000
joint_attributes.P1 7
```

## Preset `7-150-2.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "7-150-2"
joint_attributes.get_menu "7-150"
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
joint_attributes.P22 150.000000
joint_attributes.P20 2
joint_attributes.P19 150.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 300
joint_attributes.P14 2
joint_attributes.P11 300.000000
joint_attributes.P10 7
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 300.000000
joint_attributes.P3 300.000000
joint_attributes.P1 7
```

## Preset `7-150.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "7-150"
joint_attributes.get_menu "7-150"
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
joint_attributes.P22 150.000000
joint_attributes.P20 2
joint_attributes.P19 150.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 2
joint_attributes.P11 150.000000
joint_attributes.P10 7
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 100.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 100.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 150.000000
joint_attributes.P3 100.000000
joint_attributes.P1 7
```

## Preset `A-1.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "A-2"
joint_attributes.get_menu "A-1"
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
joint_attributes.P22 0.000000
joint_attributes.P19 0.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 1100
joint_attributes.P11 150.000000
joint_attributes.P10 4
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 110.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 110.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 150.000000
joint_attributes.P3 1100.000000
joint_attributes.P1 4
```

## Preset `A-2.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "A-2"
joint_attributes.get_menu "A-2"
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
joint_attributes.P22 0.000000
joint_attributes.P19 0.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 1300
joint_attributes.P11 300.000000
joint_attributes.P10 6
joint_attributes.P7_size 19.000000
joint_attributes.P7_length 110.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 110.000000
joint_attributes.P6_size 19.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 300.000000
joint_attributes.P3 1300.000000
joint_attributes.P1 6
```

## Preset `KK.p_0-P-STUD-(FLG+WEB)-2`
```
joint_attributes.saveas_file "KK"
joint_attributes.get_menu "1"
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
joint_attributes.P22 0.000000
joint_attributes.P20 1
joint_attributes.P19 0.000000
joint_attributes.P18 1
joint_attributes.P17 1
joint_attributes.P16 100
joint_attributes.P14 1
joint_attributes.P11 300.000000
joint_attributes.P10 16
joint_attributes.P7_size 22.000000
joint_attributes.P7_length 110.000000
joint_attributes.P7_standard "STUD-BOLT"
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P6_length 110.000000
joint_attributes.P6_size 22.000000
joint_attributes.P6_standard "STUD-BOLT"
joint_attributes.P5 300.000000
joint_attributes.P3 100.000000
joint_attributes.P1 16
```