# 0-DM-Z_BEAM

- File: `Z_BEAM.uel`  (12,249 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	0-DM-Z_BEAM	All`
- Dialog parameters: 10 | Variables (P/custom): 15 | Distance vars (D): 41 | Parts with bound profile: 4
- Lookup files (fVF): -
- Functions used: fP, fTpl, tan
- Dialog bitmaps: 0 | Saved presets: 0-Z BEAM 87.p_0-DM-Z_BEAM, 0-Z BEAM.p_0-DM-Z_BEAM

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "0-DM-Z_BEAM")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("PL1_THK", "P1", distance, number, 1)
           parameter("MAT.", "P2", material, text, 2)
           parameter("MARK", "P3", string, text, 3)
           parameter("R1", "P5", distance, number, 4)
           parameter("+/-", "P6", distance, number, 5)
           parameter("Y/N", "P7", YesNo, number, 6)
           parameter("PL2_THK", "P9", distance, number, 7)
           parameter("ANGLE", "P13", angle, number, 8)
           parameter("PL3_THK", "P14", distance, number, 9)
           parameter("R3", "P15", distance, number, 10)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P2 | MAT. | Material | `SM355` |
| P3 | MARK | Text | `KK` |
| P4 | CLASS | Length | `5.00` |
| P5 | R1 | Length | `35.00` |
| P6 | +/- | Length | `0.00` |
| P7 | Y/N | Yes/No | `1` |
| P8 | Parameter8 | Length | `0.00` |
| P9 | PL2_THK | Length | `50.00` |
| P10 | PF | Profile | `"PL"+P9+"*"+P11` *(formula)* |
| P11 | Parameter11 | Length | `fP(proB,"ID651B600B-0000-967E-3136-393633303631")` *(formula)* |
| P12 | Parameter12 | Length | `fTpl("LENGTH","ID651B600B-0000-9697-3136-393633303631")` *(formula)* |
| P13 | ANGLE | Angle | `45.0000` |
| P14 | PL3_THK | Length | `12.00` |
| P15 | R3 | Length | `35.00` |
| P16 | Parameter16 | Length | `0.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.BEAM | `` |
| D2 | D2.PLATE.BEAM | `` |
| D3 | D3.PLATE.BEAM | `` |
| D4 | D4.PLATE.BEAM | `` |
| D5 | D5.PLATE.BEAM | `` |
| D6 | D6.PLATE.BEAM | `` |
| D7 | D7.PLATE.BEAM | `` |
| D8 | D8.PLATE.BEAM | `` |
| D9 | D9.PLATE.BEAM | `` |
| D10 | D10.PLATE.BEAM | `` |
| D11 | D11.PLATE.BEAM | `` |
| D12 | D12.PLATE.BEAM | `` |
| D13 | D13.PLATE.BEAM | `` |
| D14 | D14.PLATE.BEAM | `` |
| D15 | D15.PLATE.BEAM | `` |
| D16 | D16.PLATE.BEAM | `` |
| D17 | D17.PLATE.BEAM | `` |
| D18 | D18.PLATE.BEAM | `` |
| D19 | D19.PLATE.BEAM | `P9` |
| D20 | D20.BEAM.BEAM | `` |
| D21 | D21.BEAM.BEAM | `` |
| D22 | D22.PLATE.BEAM | `(P12-P9)*tan(P13)+P6` |
| D23 | D23.PLATE.BEAM | `(P12-P9)*tan(P13)+P6` |
| D24 | D24.PLATE.BEAM | `(P12-P9)*tan(P13)+P6` |
| D25 | D25.PLATE.BEAM | `(P12-P9)*tan(P13)+P6` |
| D26 | D26.PLATE.BEAM | `(P12-P9)*tan(P13)+P6` |
| D27 | D27.PLATE.BEAM | `(P12-P9)*tan(P13)+P6` |
| D28 | D28.PLATE.BEAM | `(P12-P9)*tan(P13)+P6` |
| D29 | D29.PLATE.BEAM | `(P12-P9)*tan(P13)+P6` |
| D30 | D30.PLATE.BEAM | `(P12-P9)*tan(P13)+P6` |
| D31 | D31.PLATE.BEAM | `` |
| D32 | D32.PLATE.BEAM | `` |
| D33 | D33.PLATE.BEAM | `` |
| D34 | D34.PLATE.BEAM | `` |
| D35 | D35.PLATE.BEAM | `` |
| D36 | D36.PLATE.BEAM | `` |
| D37 | D37.PLATE.BEAM | `` |
| D38 | D38.PLATE.BEAM | `` |
| D39 | D39.PLATE.BEAM | `` |
| D40 | D40.PLATE.PLATE | `` |
| D41 | D41.PLATE.BEAM | `D22` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#123 | CREATION | `0` |
| obj#145 | CLASS | `P9` |
| obj#145 | MATERIAL | `P2` |
| obj#145 | PROFILE | `P10` |
| obj#145 | SERIE | `P3` |
| obj#156 | CLASS | `P14` |
| obj#156 | MATERIAL | `P2` |
| obj#156 | PROFILE | `"PL"+P14` |
| obj#156 | SERIE | `P3` |
| obj#167 | CLASS | `P1` |
| obj#167 | CREATION | `P7` |
| obj#167 | MATERIAL | `P2` |
| obj#167 | PROFILE | `"PL"+P1` |
| obj#167 | SERIE | `P3` |
| obj#177 | CLASS | `P1` |
| obj#177 | CREATION | `P7` |
| obj#177 | MATERIAL | `P2` |
| obj#177 | PROFILE | `"PL"+P1` |
| obj#177 | SERIE | `P3` |

## Preset `0-Z BEAM 87.p_0-DM-Z_BEAM`
```
joint_attributes.saveas_file "0-Z BEAM 87"
joint_attributes.get_menu "0-Z BEAM 87"
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
joint_attributes.P14 10.000000
joint_attributes.P9 15.000000
joint_attributes.P7 1
joint_attributes.P5 30.000000
joint_attributes.P3 "B1F-SF"
joint_attributes.P2 "SM275"
joint_attributes.P1 15.000000
```

## Preset `0-Z BEAM.p_0-DM-Z_BEAM`
```
joint_attributes.saveas_file "0-Z BEAM"
joint_attributes.get_menu "0-Z BEAM"
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
joint_attributes.zang1 -0.000000
joint_attributes.zsuunta 7
joint_attributes.P15 35.000000
joint_attributes.P14 12.000000
joint_attributes.P13 45.000000
joint_attributes.P9 20.000000
joint_attributes.P7 1
joint_attributes.P6 0.000000
joint_attributes.P5 35.000000
joint_attributes.P3 "1B-SF"
joint_attributes.P2 "SM275"
joint_attributes.P1 20.000000
```