# TJ_WeldPrep_Scallop

- File: `TJ.uel`  (9,636 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	TJ_WeldPrep_Scallop	All`
- Dialog parameters: 3 | Variables (P/custom): 8 | Distance vars (D): 0 | Parts with bound profile: 2
- Lookup files (fVF): -
- Functions used: fP
- Dialog bitmaps: 4 | Saved presets: 0-R35.p_TJ_WeldPrep_Scallop, 0IL-스칼렙.p_TJ_WeldPrep_Scallop, 0JOY-스칼렙.p_TJ_WeldPrep_Scallop, 0Plus-스칼렙.p_TJ_WeldPrep_Scallop, 0일흥-스칼렙.p_TJ_WeldPrep_Scallop, 1.p_TJ_WeldPrep_Scallop, 30.p_TJ_WeldPrep_Scallop, 35.p_TJ_WeldPrep_Scallop, 45.p_TJ_WeldPrep_Scallop, R40.p_TJ_WeldPrep_Scallop, STD.p_TJ_WeldPrep_Scallop

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "TJ_WeldPrep_Scallop")
   {
       tab_page("", "j_rat_holes", 1)
       {
           picture("TJ_BeamScallop_General",270,150,150,100)
           parameter(" j_root_face", "ScallopSize", distance, number, 80, 195, 60)
           attribute("ScallopShape", "", option,"%s",  none, none, "0.0", "0.0", 221, 172, 160)
           {
             value("TJ_BeamScallop_Square.xbm", 0)
             value("TJ_BeamScallop_Snip.xbm", 0)
             value("TJ_BeamScallop_Round.xbm",  1)
           }

           attribute("j_Rat_Hole_At_Top", "j_Rat_Hole_At_Top", label, "%s",  none, none, "0.0", "0.0", 500, 65)
           parameter("", "ScallopTopCreate", YesNo, number, 500, 95, 100, "toggle_field:UflgOffset=1;")

           parameter(" j_weld_gap", "UflgOffset", distance, number, 540, 160, 60)
           parameter(" j_weld_gap", "LflgOffset", distance, number, 540, 235, 60)

           parameter("", "ScallopBottomCreate", YesNo, number, 500, 300, 100, "toggle_field:LflgOffset=1;")
           attribute("j_Rat_Hole_At_Bottom", "j_Rat_Hole_At_Bottom", label, "%s",  none, none, "0.0", "0.0", 500, 330)

           attribute("1.", "1.", label2, "%s",  none, none, "0.0", "0.0", 80, 350)
           attribute("prompt_Pick_main_part", "prompt_Pick_main_part", label, "%s",  none, none, "0.0", "0.0", 100, 350)
           attribute("2.", "2.", label2, "%s",  none, none, "0.0", "0.0", 80, 375)
           attribute("prompt_Pick_position", "prompt_Pick_position", label, "%s",  none, none, "0.0", "0.0", 100, 375)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| Ext | Cut extension | Length | `8.00` |
| FlgT | Flange thickness | Length | `fP(proT,"ID651B600B-0000-8943-3136-393633303630")` *(formula)* |
| LflgOffset | lower offset | Length | `20.00` |
| ScallopBottomCreate | j_Rat_Hole_At_Bottom | Yes/No | `1` |
| ScallopShape | j_rat_holes | Chamfer | `2` |
| ScallopSize | j_size | Length | `35.00` |
| ScallopTopCreate | j_Rat_Hole_At_Top | Yes/No | `1` |
| UflgOffset | upper offset | Length | `20.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#90 | CREATION | `ScallopBottomCreate` |
| obj#90 | PROFILE | `fP(proS,"ID651B600B-0000-8943-3136-393633303630")+5` |
| obj#104 | CREATION | `ScallopTopCreate` |
| obj#104 | PROFILE | `fP(proS,"ID651B600B-0000-8943-3136-393633303630")+5` |

## Preset `0-R35.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "0-R35"
joint_attributes.get_menu "0-R35"
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
joint_attributes.zang1 -44.999500
joint_attributes.zsuunta 7
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset -280.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
```

## Preset `0IL-스칼렙.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "0일흥-스칼렙"
joint_attributes.get_menu "0일흥-스칼렙"
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
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
```

## Preset `0JOY-스칼렙.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "0일흥-스칼렙"
joint_attributes.get_menu "0일흥-스칼렙"
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
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
```

## Preset `0Plus-스칼렙.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "0일흥-스칼렙"
joint_attributes.get_menu "0일흥-스칼렙"
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
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
```

## Preset `0일흥-스칼렙.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "0일흥-스칼렙"
joint_attributes.get_menu "0일흥-스칼렙"
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
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
```

## Preset `1.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "1"
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
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
joint_attributes.ScallopSize 35.000000
```

## Preset `30.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "30"
joint_attributes.get_menu "30"
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
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
joint_attributes.ScallopSize 30.000000
```

## Preset `35.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "35"
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
joint_attributes.zang1 -15.000000
joint_attributes.zsuunta 7
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
joint_attributes.ScallopSize 35.000000
```

## Preset `45.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "45"
joint_attributes.get_menu "35"
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
joint_attributes.zang1 -15.000000
joint_attributes.zsuunta 7
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
joint_attributes.ScallopSize 45.000000
```

## Preset `R40.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "R40"
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
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
joint_attributes.ScallopSize 40.000000
```

## Preset `STD.p_TJ_WeldPrep_Scallop`
```
joint_attributes.saveas_file "STD"
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
joint_attributes.ScallopBottomCreate 1
joint_attributes.LflgOffset 0.000000
joint_attributes.UflgOffset 0.000000
joint_attributes.ScallopTopCreate 1
joint_attributes.ScallopShape 2
joint_attributes.ScallopSize 35.000000
```