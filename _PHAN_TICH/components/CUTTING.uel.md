# 0-DM-PLATE (BEAM) CUT

- File: `CUTTING.uel`  (6,660 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	0-DM-PLATE (BEAM) CUT	All`
- Dialog parameters: 4 | Variables (P/custom): 9 | Distance vars (D): 18 | Parts with bound profile: 2
- Lookup files (fVF): -
- Functions used: fP, fTpl, if, tan
- Dialog bitmaps: 0 | Saved presets: 1.p_0-DM-PLATE (BEAM) CUT

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "0-DM-PLATE (BEAM) CUT")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("L_(Y/N)", "P5", YesNo, number, 1)
           parameter("R_(Y/N)", "P6", YesNo, number, 2)
           parameter("Angle", "P7", angle, number, 3)
           parameter("GAP", "P8", distance, number, 4)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | THK | Length | `fP(proT,"ID651B600B-0000-875B-3136-393633303539")` *(formula)* |
| P2 | Parameter2 | Length | `fTpl("LENGTH","ID651B600B-0000-875B-3136-393633303539")` *(formula)* |
| P3 | Parameter3 | Profile | `"BL"+(P2+200)` *(formula)* |
| P4 | Parameter4 | Length | `ceil(P1/3)` *(formula)* |
| P5 | L_(Y/N) | Yes/No | `1` |
| P6 | R_(Y/N) | Yes/No | `1` |
| P7 | Angle | Angle | `45.0000` |
| P8 | GAP | Length | `2.00` |
| P9 | Parameter9 | Length | `if (P5==0 \|\| P6==0) then (P1-P8) else P4 endif` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.CUTPART.PLATE | `` |
| D2 | D2.CUTPART.PLATE | `` |
| D3 | D3.CUTPART.PLATE | `` |
| D4 | D4.CUTPART.PLATE | `` |
| D5 | D5.CUTPART.PLATE | `` |
| D6 | D6.CUTPART.PLATE | `` |
| D7 | D7.CUTPART.PLATE | `` |
| D8 | D8.CUTPART.PLATE | `(P9+10)*tan(P7)` |
| D9 | D9.CUTPART.PLATE | `` |
| D10 | D10.CUTPART.PLATE | `` |
| D11 | D11.CUTPART.PLATE | `(P9+10)*tan(P7)` |
| D12 | D12.CUTPART.PLATE | `` |
| D13 | D13.CUTPART.PLATE | `` |
| D14 | D14.CUTPART.PLATE | `` |
| D15 | D15.CUTPART.PLATE | `P9` |
| D16 | D16.CUTPART.PLATE | `` |
| D17 | D17.CUTPART.PLATE | `` |
| D18 | D18.CUTPART.PLATE | `P9` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#89 | CREATION | `P6` |
| obj#89 | PROFILE | `P3` |
| obj#103 | CREATION | `P5` |
| obj#103 | PROFILE | `P3` |

## Preset `1.p_0-DM-PLATE (BEAM) CUT`
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
joint_attributes.zang2 -0.000000
joint_attributes.zang1 0.000100
joint_attributes.zsuunta 5
joint_attributes.P6 1
joint_attributes.P5 1
```