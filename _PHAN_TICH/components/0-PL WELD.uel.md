# 0-PL WELD

- File: `0-PL WELD.uel`  (14,079 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	0-PL WELD	All`
- Dialog parameters: 11 | Variables (P/custom): 15 | Distance vars (D): 72 | Parts with bound profile: 0
- Lookup files (fVF): -
- Functions used: fP, if, mid, tan
- Dialog bitmaps: 0 | Saved presets: #D1.p_0-PL WELD, #D2.p_0-PL WELD, #T1.p_0-PL WELD, #T2.p_0-PL WELD, 0- ngg.p_0-PL WELD, 0-1.p_0-PL WELD, 0-2.p_0-PL WELD, 0-3.p_0-PL WELD, 0-4.p_0-PL WELD, 0-GP 1.p_0-PL WELD, 0-GP 2.p_0-PL WELD, 0-GP 3.p_0-PL WELD, 0-GP 4.p_0-PL WELD, 0-GP 5.p_0-PL WELD, 0-GP 6.p_0-PL WELD, 0-H500_200_10_16 B2F.p_0-PL WELD, 0-K.p_0-PL WELD, 2-45.p_0-PL WELD, CJP.p_0-PL WELD

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "0-PL WELD")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("Gap", "P4", distance, number, 1)
           parameter("Angle", "P5", angle, number, 2)
           parameter("Type (1=1 side/ 0 = 2 side)", "P6", integer, number, 3)
           parameter("1L(Y/N)", "P8", YesNo, number, 4)
           parameter("1R(Y/N)", "P9", YesNo, number, 5)
           parameter("2L(Y/N)", "P10", YesNo, number, 6)
           parameter("2R(Y/N)", "P11", YesNo, number, 7)
           parameter("3L(Y/N)", "P12", YesNo, number, 8)
           parameter("3R(Y/N)", "P13", YesNo, number, 9)
           parameter("4L(Y/N)", "P14", YesNo, number, 10)
           parameter("4R(Y/N)", "P15", YesNo, number, 11)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Profile | `fP(proPROFILE,"ID651B600B-0000-87D6-3136-393633303539")` *(formula)* |
| P2 | Parameter2 | Length | `mid(P1, 2, 5)` *(formula)* |
| P3 | Parameter3 | Length | `ceil(P2/3)` *(formula)* |
| P4 | Gap | Length | `12.00` |
| P5 | Angle | Angle | `45.0000` |
| P6 | Type (1=1 side/ 0 = 2 side) | Integer | `0` |
| P7 | Parameter7 | Length | `if(P6==1) then P2-P4 else P3 endif` *(formula)* |
| P8 | 1L(Y/N) | Yes/No | `1` |
| P9 | 1R(Y/N) | Yes/No | `1` |
| P10 | 2L(Y/N) | Yes/No | `1` |
| P11 | 2R(Y/N) | Yes/No | `1` |
| P12 | 3L(Y/N) | Yes/No | `1` |
| P13 | 3R(Y/N) | Yes/No | `1` |
| P14 | 4L(Y/N) | Yes/No | `1` |
| P15 | 4R(Y/N) | Yes/No | `1` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.CUTPART.STIFFENER | `` |
| D2 | D2.CUTPART.STIFFENER | `` |
| D3 | D3.CUTPART.STIFFENER | `P7*tan(P5)` |
| D4 | D4.CUTPART.STIFFENER | `` |
| D5 | D5.CUTPART.STIFFENER | `` |
| D6 | D6.CUTPART.STIFFENER | `P7*tan(P5)` |
| D7 | D7.CUTPART.STIFFENER | `` |
| D8 | D8.CUTPART.STIFFENER | `` |
| D9 | D9.CUTPART.STIFFENER | `` |
| D10 | D10.CUTPART.STIFFENER | `` |
| D11 | D11.CUTPART.STIFFENER | `` |
| D12 | D12.CUTPART.STIFFENER | `` |
| D13 | D13.CUTPART.STIFFENER | `` |
| D14 | D14.CUTPART.STIFFENER | `P7` |
| D15 | D15.CUTPART.STIFFENER | `` |
| D16 | D16.CUTPART.STIFFENER | `` |
| D17 | D17.CUTPART.STIFFENER | `P7` |
| D18 | D18.CUTPART.STIFFENER | `` |
| D19 | D19.CUTPART.STIFFENER | `` |
| D20 | D20.CUTPART.STIFFENER | `` |
| D21 | D21.CUTPART.STIFFENER | `` |
| D22 | D22.CUTPART.STIFFENER | `` |
| D23 | D23.CUTPART.STIFFENER | `` |
| D24 | D24.CUTPART.STIFFENER | `` |
| D25 | D25.CUTPART.STIFFENER | `` |
| D26 | D26.CUTPART.STIFFENER | `` |
| D27 | D27.CUTPART.STIFFENER | `P7*tan(P5)` |
| D28 | D28.CUTPART.STIFFENER | `` |
| D29 | D29.CUTPART.STIFFENER | `` |
| D30 | D30.CUTPART.STIFFENER | `P7*tan(P5)` |
| D31 | D31.CUTPART.STIFFENER | `` |
| D32 | D32.CUTPART.STIFFENER | `P7` |
| D33 | D33.CUTPART.STIFFENER | `` |
| D34 | D34.CUTPART.STIFFENER | `` |
| D35 | D35.CUTPART.STIFFENER | `P7` |
| D36 | D36.CUTPART.STIFFENER | `` |
| D37 | D37.CUTPART.STIFFENER | `` |
| D38 | D38.CUTPART.STIFFENER | `` |
| D39 | D39.CUTPART.STIFFENER | `` |
| D40 | D40.CUTPART.STIFFENER | `` |
| D41 | D41.CUTPART.STIFFENER | `` |
| D42 | D42.CUTPART.STIFFENER | `` |
| D43 | D43.CUTPART.STIFFENER | `` |
| D44 | D44.CUTPART.STIFFENER | `P7*tan(P5)` |
| D45 | D45.CUTPART.STIFFENER | `` |
| D46 | D46.CUTPART.STIFFENER | `` |
| D47 | D47.CUTPART.STIFFENER | `P7*tan(P5)` |
| D48 | D48.CUTPART.STIFFENER | `` |
| D49 | D49.CUTPART.STIFFENER | `P7` |
| D50 | D50.CUTPART.STIFFENER | `` |
| D51 | D51.CUTPART.STIFFENER | `` |
| D52 | D52.CUTPART.STIFFENER | `P7` |
| D53 | D53.CUTPART.STIFFENER | `` |
| D54 | D54.CUTPART.STIFFENER | `` |
| D55 | D55.CUTPART.STIFFENER | `` |
| D56 | D56.CUTPART.STIFFENER | `` |
| D57 | D57.CUTPART.STIFFENER | `` |
| D58 | D58.CUTPART.STIFFENER | `` |
| D59 | D59.CUTPART.STIFFENER | `` |
| D60 | D60.CUTPART.STIFFENER | `` |
| D61 | D61.CUTPART.STIFFENER | `` |
| D62 | D62.CUTPART.STIFFENER | `P7*tan(P5)` |
| D63 | D63.CUTPART.STIFFENER | `` |
| D64 | D64.CUTPART.STIFFENER | `` |
| D65 | D65.CUTPART.STIFFENER | `P7*tan(P5)` |
| D66 | D66.CUTPART.STIFFENER | `` |
| D67 | D67.CUTPART.STIFFENER | `P7` |
| D68 | D68.CUTPART.STIFFENER | `` |
| D69 | D69.CUTPART.STIFFENER | `` |
| D70 | D70.CUTPART.STIFFENER | `P7` |
| D71 | D71.CUTPART.STIFFENER | `` |
| D72 | D72.CUTPART.STIFFENER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#122 | CREATION | `P13` |
| obj#136 | CREATION | `P12` |
| obj#146 | CREATION | `P11` |
| obj#157 | CREATION | `P10` |
| obj#167 | CREATION | `P15` |
| obj#175 | CREATION | `P14` |
| obj#183 | CREATION | `P9` |
| obj#192 | CREATION | `P8` |

## Preset `#D1.p_0-PL WELD`
```
joint_attributes.saveas_file "#D1"
joint_attributes.get_menu "#T2"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
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
joint_attributes.zang1 -3.901200
joint_attributes.zsuunta 7
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 0
joint_attributes.P11 1
joint_attributes.P10 0
joint_attributes.P9 1
joint_attributes.P8 0
joint_attributes.P6 1
joint_attributes.P4 2.000000
```

## Preset `#D2.p_0-PL WELD`
```
joint_attributes.saveas_file "#D2"
joint_attributes.get_menu "#D1"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
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
joint_attributes.zang1 -3.901200
joint_attributes.zsuunta 7
joint_attributes.P15 1
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 0
joint_attributes.P11 0
joint_attributes.P10 0
joint_attributes.P9 1
joint_attributes.P8 0
joint_attributes.P6 1
joint_attributes.P4 2.000000
```

## Preset `#T1.p_0-PL WELD`
```
joint_attributes.saveas_file "#T1"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.BottomRight -2147483648
joint_attributes.BottomMiddle -2147483648
joint_attributes.BottomLeft -2147483648
joint_attributes.MiddleRight -2147483648
joint_attributes.MiddleLeft -2147483648
joint_attributes.TopRight -2147483648
joint_attributes.TopMiddle -2147483648
joint_attributes.TopLeft -2147483648
joint_attributes.UpMiddleRight -2147483648
joint_attributes.UpMiddleMiddle -2147483648
joint_attributes.UpMiddleLeft -2147483648
joint_attributes.MiddleMiddle -2147483648
joint_attributes.zang2 0.000000
joint_attributes.zang1 -3.901200
joint_attributes.zsuunta 7
joint_attributes.P15 0
joint_attributes.P14 1
joint_attributes.P13 0
joint_attributes.P12 1
joint_attributes.P11 0
joint_attributes.P10 0
joint_attributes.P9 0
joint_attributes.P8 1
joint_attributes.P6 1
joint_attributes.P4 2.000000
```

## Preset `#T2.p_0-PL WELD`
```
joint_attributes.saveas_file "#T2"
joint_attributes.get_menu "#T1"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.BottomRight -2147483648
joint_attributes.BottomMiddle -2147483648
joint_attributes.BottomLeft -2147483648
joint_attributes.MiddleRight -2147483648
joint_attributes.MiddleLeft -2147483648
joint_attributes.TopRight -2147483648
joint_attributes.TopMiddle -2147483648
joint_attributes.TopLeft -2147483648
joint_attributes.UpMiddleRight -2147483648
joint_attributes.UpMiddleMiddle -2147483648
joint_attributes.UpMiddleLeft -2147483648
joint_attributes.MiddleMiddle -2147483648
joint_attributes.zang2 0.000000
joint_attributes.zang1 -3.901200
joint_attributes.zsuunta 7
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 0
joint_attributes.P12 1
joint_attributes.P11 0
joint_attributes.P10 1
joint_attributes.P9 0
joint_attributes.P8 1
joint_attributes.P6 1
joint_attributes.P4 2.000000
```

## Preset `0- ngg.p_0-PL WELD`
```
joint_attributes.saveas_file "0- ngg"
joint_attributes.get_menu "0-2"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P15 1
joint_attributes.P14 1
joint_attributes.P13 0
joint_attributes.P12 0
joint_attributes.P11 0
joint_attributes.P10 0
joint_attributes.P9 0
joint_attributes.P8 0
```

## Preset `0-1.p_0-PL WELD`
```
joint_attributes.saveas_file "0-1"
joint_attributes.get_menu "0-1"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P10 1
joint_attributes.P9 1
joint_attributes.P8 1
```

## Preset `0-2.p_0-PL WELD`
```
joint_attributes.saveas_file "0-2"
joint_attributes.get_menu "0-2"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P15 1
joint_attributes.P14 1
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 0
joint_attributes.P10 0
joint_attributes.P9 1
joint_attributes.P8 1
```

## Preset `0-3.p_0-PL WELD`
```
joint_attributes.saveas_file "0-3"
joint_attributes.get_menu "0-3"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P15 1
joint_attributes.P14 1
joint_attributes.P13 0
joint_attributes.P12 0
joint_attributes.P11 0
joint_attributes.P10 0
joint_attributes.P9 0
joint_attributes.P8 0
```

## Preset `0-4.p_0-PL WELD`
```
joint_attributes.saveas_file "0-4"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
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
joint_attributes.P15 1
joint_attributes.P14 1
joint_attributes.P13 0
joint_attributes.P12 0
joint_attributes.P11 0
joint_attributes.P10 0
joint_attributes.P9 0
joint_attributes.P8 0
```

## Preset `0-GP 1.p_0-PL WELD`
```
joint_attributes.saveas_file "0-GP 1"
joint_attributes.get_menu "0-GP 1"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 6
joint_attributes.P15 1
joint_attributes.P14 1
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P10 1
joint_attributes.P9 1
joint_attributes.P8 1
```

## Preset `0-GP 2.p_0-PL WELD`
```
joint_attributes.saveas_file "0-GP 2"
joint_attributes.get_menu "0-GP 2"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 2
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P10 1
joint_attributes.P9 1
joint_attributes.P8 1
```

## Preset `0-GP 3.p_0-PL WELD`
```
joint_attributes.saveas_file "0-GP 3"
joint_attributes.get_menu "0-GP 3"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 2
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P10 1
joint_attributes.P9 1
joint_attributes.P8 1
```

## Preset `0-GP 4.p_0-PL WELD`
```
joint_attributes.saveas_file "0-GP 4"
joint_attributes.get_menu "0-GP 4"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
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
joint_attributes.zsuunta 2
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P10 1
joint_attributes.P9 1
joint_attributes.P8 1
```

## Preset `0-GP 5.p_0-PL WELD`
```
joint_attributes.saveas_file "0-GP 5"
joint_attributes.get_menu "0-GP 4"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
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
joint_attributes.zsuunta 1
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P10 1
joint_attributes.P9 1
joint_attributes.P8 1
```

## Preset `0-GP 6.p_0-PL WELD`
```
joint_attributes.saveas_file "0-GP 6"
joint_attributes.get_menu "0-GP 1"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
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
joint_attributes.zsuunta 2
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P10 1
joint_attributes.P9 1
joint_attributes.P8 1
```

## Preset `0-H500_200_10_16 B2F.p_0-PL WELD`
```
joint_attributes.saveas_file "0-H500*200*10*16 B2F"
joint_attributes.get_menu "0-H500*200*10*16 B2F"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
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
joint_attributes.zsuunta 1
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 1
joint_attributes.P10 1
joint_attributes.P9 1
joint_attributes.P8 1
```

## Preset `0-K.p_0-PL WELD`
```
joint_attributes.saveas_file "0-K"
joint_attributes.get_menu "0-K"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P15 1
joint_attributes.P14 1
joint_attributes.P13 1
joint_attributes.P12 1
joint_attributes.P11 0
joint_attributes.P10 0
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P5 45.000000
joint_attributes.P4 12.000000
```

## Preset `2-45.p_0-PL WELD`
```
joint_attributes.saveas_file "2-45"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position 0
joint_attributes.horizontal_position 0
joint_attributes.horizontal_offset 0.000000
joint_attributes.vertical_offset 0.000000
joint_attributes.BottomRight -2147483648
joint_attributes.BottomMiddle -2147483648
joint_attributes.BottomLeft -2147483648
joint_attributes.MiddleRight -2147483648
joint_attributes.MiddleLeft -2147483648
joint_attributes.TopRight -2147483648
joint_attributes.TopMiddle -2147483648
joint_attributes.TopLeft -2147483648
joint_attributes.UpMiddleRight -2147483648
joint_attributes.UpMiddleMiddle -2147483648
joint_attributes.UpMiddleLeft -2147483648
joint_attributes.MiddleMiddle -2147483648
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 2
joint_attributes.P15 1
joint_attributes.P14 0
joint_attributes.P13 1
joint_attributes.P12 0
joint_attributes.P11 0
joint_attributes.P10 0
joint_attributes.P9 1
joint_attributes.P8 0
joint_attributes.P6 1
joint_attributes.P4 2.000000
```

## Preset `CJP.p_0-PL WELD`
```
joint_attributes.saveas_file "CJP"
joint_attributes.get_menu "CJP"
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
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
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
joint_attributes.P15 0
joint_attributes.P14 0
joint_attributes.P13 0
joint_attributes.P12 1
joint_attributes.P11 0
joint_attributes.P10 1
joint_attributes.P9 0
joint_attributes.P8 1
joint_attributes.P6 1
joint_attributes.P5 45.000000
joint_attributes.P4 2.000000
```