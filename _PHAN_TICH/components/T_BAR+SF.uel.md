# 0-DM-T_BAR+SF

- File: `T_BAR+SF.uel`  (15,291 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	0-DM-T_BAR+SF	All
-10000	BT	All`
- Dialog parameters: 14 | Variables (P/custom): 14 | Distance vars (D): 24 | Parts with bound profile: 2
- Lookup files (fVF): -
- Functions used: if
- Dialog bitmaps: 0 | Saved presets: 0-SF.p_0-DM-T_BAR+SF

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "0-DM-T_BAR+SF")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("THK", "P1", distance, number, 1)
           parameter("MATT", "P2", material, text, 2)
           parameter("MARK", "P3", string, text, 3)
           parameter("CLASS", "P4", distance, number, 4)
           parameter("GAP to FLG", "P5", distance, number, 5)
           parameter("GAP to WEB", "P6", distance, number, 6)
           parameter("R", "P7", distance, number, 7)
           parameter("N.S (Y/N)", "P8", YesNo, number, 8)
           parameter("F.S (Y/N)", "P9", YesNo, number, 9)
           parameter("NUMBER COPY", "P10", integer, number, 10)
           parameter("@", "P11", distance, number, 11)
           parameter("N.S COPY (Y/N)", "P12", YesNo, number, 12)
           parameter("F.S COPY (Y/N)", "P14", YesNo, number, 13)
           parameter("START", "P15", string, text, 14)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P2 | MATT | Material | `SS275` |
| P3 | MARK | Text | `KK` |
| P4 | CLASS | Length | `33.00` |
| P5 | GAP to FLG | Length | `10.00` |
| P6 | GAP to WEB | Length | `0.00` |
| P7 | R | Length | `50.00` |
| P8 | N.S (Y/N) | Yes/No | `1` |
| P9 | F.S (Y/N) | Yes/No | `1` |
| P10 | NUMBER COPY | Integer | `2` |
| P11 | @ | Length | `1000.00` |
| P12 | N.S COPY (Y/N) | Yes/No | `1` |
| P13 | Parameter13 | Length | `if (P6==0) then P7 else 0 endif` *(formula)* |
| P14 | F.S COPY (Y/N) | Yes/No | `1` |
| P15 | START | Text | `54.5` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.T-BAR | `` |
| D2 | D2.PLATE.T-BAR | `` |
| D3 | D3.PLATE.T-BAR | `` |
| D4 | D4.PLATE.T-BAR | `` |
| D5 | D5.PLATE.T-BAR | `` |
| D6 | D6.PLATE.T-BAR | `` |
| D7 | D7.PLATE.T-BAR | `` |
| D8 | D8.PLATE.T-BAR | `` |
| D9 | D9.PLATE.T-BAR | `P5` |
| D10 | D10.PLATE.T-BAR | `P5` |
| D11 | D11.PLATE.T-BAR | `P5` |
| D12 | D12.PLATE.T-BAR | `P5` |
| D13 | D13.PLATE.T-BAR | `P6` |
| D14 | D14.PLATE.T-BAR | `P6` |
| D15 | D15.PLATE.T-BAR | `P6` |
| D16 | D16.PLATE.T-BAR | `P6` |
| D17 | D17.PLATE.T-BAR | `P15` |
| D18 | D18.PLATE.T-BAR | `P15` |
| D19 | D19.PLATE.T-BAR | `P15` |
| D20 | D20.PLATE.T-BAR | `P15` |
| D21 | D21.PLATE.T-BAR | `P15` |
| D22 | D22.PLATE.T-BAR | `P15` |
| D23 | D23.PLATE.T-BAR | `P15` |
| D24 | D24.PLATE.T-BAR | `P15` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#131 | CLASS | `P4` |
| obj#131 | CREATION | `P9` |
| obj#131 | MATERIAL | `P2` |
| obj#131 | PROFILE | `"PL"+P1` |
| obj#131 | SERIE | `P3` |
| obj#144 | CLASS | `P4` |
| obj#144 | CREATION | `P8` |
| obj#144 | MATERIAL | `P2` |
| obj#144 | PROFILE | `"PL"+P1` |
| obj#144 | SERIE | `P3` |
| obj#346 | CREATION | `P12` |
| obj#377 | CREATION | `P14` |
| obj#995 | APPLIED_VALUE | `b1/2-b2/2` |
| obj#1009 | APPLIED_VALUE | `h2` |

## Preset `0-SF.p_0-DM-T_BAR+SF`
```
joint_attributes.saveas_file "0-SF"
joint_attributes.get_menu "0-SF"
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
joint_attributes.zsuunta 7
joint_attributes.P15 "70"
joint_attributes.P14 1
joint_attributes.P12 1
joint_attributes.P11 1000.000000
joint_attributes.P10 2
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P7 35.000000
joint_attributes.P6 0.000000
joint_attributes.P5 10.000000
joint_attributes.P4 33.000000
joint_attributes.P3 "B1D-SF"
joint_attributes.P2 "SM355"
joint_attributes.P1 9.000000
```