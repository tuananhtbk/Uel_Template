# TJ_LiftingHolesColumn

- File: `NEC\TJ_LIFTTING_HOLE_COL.uel`  (6,515 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	TJ_LiftingHolesColumn	All`
- Dialog parameters: 4 | Variables (P/custom): 7 | Distance vars (D): 12 | Parts with bound profile: 0
- Lookup files (fVF): -
- Functions used: fP, if
- Dialog bitmaps: 0 | Saved presets: standard.p_TJ_LiftingHolesColumn

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "TJ_LiftingHolesColumn")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("�EŒa", "P1", distance, number, 1)
           parameter("’[•”ƒIƒtƒZƒbƒg", "L1", distance, number, 2)
           parameter("ƒtƒ‰ƒ“ƒW‰�’[‹——£(0=Ž©“®)", "L3", distance, number, 3)
           parameter("”½“][0/1]", "SW", YesNo, number, 4)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | 뛇똞 | Length | `24.00` |
| L1 | ’[•”ƒIƒtƒZƒbƒg | Length | `300.00` |
| L2 | �i‰�’[‚ÌŽ©“®”»’è�j | Length | `if(L3) then L3 else (W>=200)*40+(W<200)*(W>=175)*35+(W<175)*(W>=150)*30+(W<150)*25 endif` *(formula)* |
| L3 | ƒtƒ‰ƒ“ƒW‰�’[‹——£(0=Ž©“®) | Length | `` |
| L4 | �i”½“]‚ÌŒvŽZ�j | Length | `if(SW==1) then L2 else (W-L2) endif` *(formula)* |
| SW | ”½“][0/1] | Yes/No | `0` |
| W | 걁듩뭽궻븴걂 | Length | `fP(proB,"ID67C2A424-0000-D7E0-3137-343038313037")` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.BOLT.G1 | `L1` |
| D2 | D2.BOLT.G1 | `L1+50` |
| D3 | D3.BOLT.G1 | `L4` |
| D4 | D4.BOLT.G1 | `L4` |
| D5 | D5.BOLT.G1 | `` |
| D6 | D6.BOLT.G1 | `` |
| D7 | D7.BOLT.G1 | `L4` |
| D8 | D8.BOLT.G1 | `L4` |
| D9 | D9.BOLT.G1 | `` |
| D10 | D10.BOLT.G1 | `` |
| D11 | D11.BOLT.G1 | `L1` |
| D12 | D12.BOLT.G1 | `L1+50` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#93 | CUT_LENGTH | `2*fP(proT,"ID67C2A424-0000-D7E0-3137-343038313037")+5` |
| obj#93 | SIZE | `P1` |
| obj#121 | CUT_LENGTH | `2*fP(proT,"ID67C2A424-0000-D7E0-3137-343038313037")+5` |
| obj#121 | SIZE | `P1` |

## Preset `standard.p_TJ_LiftingHolesColumn`
```
joint_attributes.saveas_file "standard"
joint_attributes.get_menu "standard"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 172647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 172647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 172647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 172647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 172647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 172647168
joint_attributes.support_condition 172647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ad_root "albl_no_root"
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.detail_type 0
joint_attributes.vertical_position -2147483648
joint_attributes.horizontal_position -2147483648
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
joint_attributes.SW 0
joint_attributes.P1 24.000000
joint_attributes.L3 0.000000
joint_attributes.L1 300.000000
```