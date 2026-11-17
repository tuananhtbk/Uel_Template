# TJ_PipeCap

- File: `NEC\TJ_PIPECAP.uel`  (9,754 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	TJ_PipeCap	All`
- Dialog parameters: 4 | Variables (P/custom): 17 | Distance vars (D): 12 | Parts with bound profile: 1
- Lookup files (fVF): -
- Functions used: fP, fTpl, if, max, min
- Dialog bitmaps: 1 | Saved presets: standard.p_TJ_PipeCap

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "TJ_PipeCap")
   {
       helpurl("TKKComponentHelp.chm::/TJ_PipeCap.html")
       tab_page("", "jd_Picture", 1)
       {
    			part("j_Cap", "CAP_t_user", "CAP_rad_user", , , , , , 1)
    			parameter("", "CAP_prefix", string, text,		380, 25, 50)
    			parameter("", "CAP_startno", integer, number,	455, 25, 40)
    			parameter("", "CAP_mat_user", material, text,	525, 25, 100)
    			parameter("", "CAP_name", string, text,			685, 25, 120)

          parameter("j_fit_to_cap", "FIT_sw", YesNo, number, 4)
          parameter("j_welding", "WELD_sw", YesNo, number, 5)
          parameter("j_welding_size", "WLD_size", distance, number, 6)
          parameter("j_param_avoid_solid_error", "CAP_err_user", distance, number, 7)
    			picture("TJ_PipeCap", 400, 125, 150, 200)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| CAP_err | Solid error correction | Integer | `CAP_err_user+(CAP_err_user==0)*CAP_err_auto` *(formula)* |
| CAP_err_auto | AutoCorrection | Length | `min(max(1,CAP_rad*0.025),2)` *(formula)* |
| CAP_err_user | j_param_avoid_solid_error | Length | `` |
| CAP_finish | j_finish | Text | `` |
| CAP_mat_user | j_material | Material | `` |
| CAP_name | j_name | Text | `CAP` |
| CAP_prefix | j_Part_Pos_Prefix | Text | `P` |
| CAP_rad | Cap radius | Length | `CAP_rad_user+(CAP_rad_user==0)*CAP_rad_auto` *(formula)* |
| CAP_rad_auto | Auto radius | Length | `fP(proH,"ID67C2A424-0000-D84A-3137-343038313037")` *(formula)* |
| CAP_rad_user | d (0=Auto) | Length | `` |
| CAP_startno | j_Part_Pos_Start_No | Integer | `1` |
| CAP_t | Cap t | Length | `CAP_t_user+(CAP_t_user==0)*CAP_t_auto` *(formula)* |
| CAP_t_auto | Auto t | Length | `fP(proT,"ID67C2A424-0000-D84A-3137-343038313037")` *(formula)* |
| CAP_t_user | t (0=Auto) | Length | `` |
| FIT_sw | j_fit_to_cap | Yes/No | `1` |
| WELD_sw | j_welding | Yes/No | `1` |
| WLD_size | j_welding_size | Length | `6.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.G1.PipeCap01 | `0.5*CAP_rad+CAP_err` |
| D2 | D2.G1.PipeCap01 | `` |
| D3 | D3.G1.PipeCap01 | `` |
| D4 | D4.G1.PipeCap01 | `0.5*CAP_rad+CAP_err` |
| D5 | D5.G1.PipeCap01 | `` |
| D6 | D6.G1.PipeCap01 | `` |
| D7 | D7.G1.PipeCap01 | `` |
| D8 | D8.G1.PipeCap01 | `` |
| D9 | D9.G1.PipeCap01 | `D1` |
| D10 | D10.linecut.PipeCap01 | `D12` |
| D11 | D11.linecut.PipeCap01 | `D12` |
| D12 | D12.linecut.PipeCap01 | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#117 | FINISH | `CAP_finish` |
| obj#117 | MATERIAL | `CAP_mat` |
| obj#117 | NAME | `CAP_name` |
| obj#117 | PROFILE | `"CAP"+CAP_rad + "*" + CAP_t` |
| obj#117 | SERIE | `CAP_prefix` |
| obj#117 | STARTNUMBER | `CAP_startno` |
| obj#131 | CREATION | `FIT_sw` |
| obj#145 | CREATION | `WELD_sw` |
| obj#145 | LENGTH | `fP(proH,"ID67C2A424-0000-D84A-3137-343038313037")*PI` |
| obj#145 | SIZE | `WLD_size` |

## Preset `standard.p_TJ_PipeCap`
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
joint_attributes.use_restraints -2147483648
joint_attributes.ad_root "albl_no_root"
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
joint_attributes.CAP_err_user -2147483648.000000
joint_attributes.WLD_size 6.000000
joint_attributes.WELD_sw 1
joint_attributes.FIT_sw 1
joint_attributes.CAP_name "CAP"
joint_attributes.CAP_startno 1
joint_attributes.CAP_prefix "P"
joint_attributes.CAP_rad_user -2147483648.000000
joint_attributes.CAP_t_user -2147483648.000000
```