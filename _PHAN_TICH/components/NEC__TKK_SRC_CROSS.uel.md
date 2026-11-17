# TKK_SRC_Cross

- File: `NEC\TKK_SRC_CROSS.uel`  (11,154 bytes, gzip)
- Type in .inp: **macro**  | Catalog line: `-10	TKK_SRC_Cross	All`
- Dialog parameters: 4 | Variables (P/custom): 18 | Distance vars (D): 18 | Parts with bound profile: 3
- Lookup files (fVF): -
- Functions used: fP
- Dialog bitmaps: 2 | Saved presets: standard.p_TKK_SRC_Cross

## Dialog (.inp)
```
page("Tekla Structures","")
{
   macro(1, "TKK_SRC_Cross")
   {
       helpurl("TKKComponentHelp.chm::/TKK_SRC_Cross.html")
       tab_page("", "albl_Attributes", 1)
       {
           picture("470_2_line", 400, 2, 3, 5)

           attribute(" NumberSeries","albl_NumberSeries", label,"%s", none, none, "0.0", "0.0", 15, -4)
           attribute(" Prefix","albl_Prefix_", label,"%s", none, none, "0.0", "0.0", 250,10)
           attribute(" Start number","albl_Start_number_", label,"%s", none, none, "0.0", "0.0", 400,10)

           attribute(" FLG","H", label,"%s", none, none, "0.0", "0.0", 15,35,150)
           attribute(" WEB","T", label,"%s", none, none, "0.0", "0.0", 15,60,150)
           attribute(" Assembly","albl_Ass", label,"%s", none, none, "0.0", "0.0", 15,85,150)
           parameter(" H p-mark", "H_prefix", string, text, 250,35,120)
           parameter(" T p-mark", "T_prefix", string, text, 250,60,120)
           parameter(" H no", "H_startno", integer, number, 400,35,120)
           parameter(" T no", "T_startno", integer, number, 400,60,120)
           parameter(" Assembly mark", "BEAM_prefix", string, text, 250,85,120)
           parameter(" Assembly no", "BEAM_startno", integer, number, 400,85,120)

           picture("470_2_line", 400, 2, 3, 130)

           attribute("NumberSeries","albl_Attributes", label,"%s", none, none, "0.0", "0.0", 15,122)
           attribute(" ", "albl_Name_", label,"%s", none, none, "0.0", "0.0", 15, 150)
           parameter(" ", "BEAM_name", string, text, 250, 150, 270)
           attribute(" ", "albl_Profile_", label,"%s", none, none, "0.0", "0.0", 15, 175)
           attribute(" ", "H", label,"%s", none, none, "0.0", "0.0", 25, 200)
           attribute(" ", "T", label,"%s", none, none, "0.0", "0.0", 25, 225)
           parameter(" ", "H_prof", profile, text, 250, 200, 270)
           parameter(" ", "T_prof", profile, text, 250, 225, 270)

           attribute(" ","albl_Material_", label,"%s", none, none, "0.0", "0.0", 15, 250)
           attribute(" ", "H", label,"%s", none, none, "0.0", "0.0", 25, 275)
           parameter(" ", "H_mat", material, text, 250, 275, 270)
           attribute(" ", "T", label,"%s", none, none, "0.0", "0.0", 25, 300)
           parameter(" j_web", "T_mat", material, text, 250, 300, 270)
           attribute(" ", "albl_Finish_", label,"%s", none, none, "0.0", "0.0", 15, 325)
           parameter(" albl_Finish", "BEAM_finish", string, text, 250, 325, 270)
           attribute(" ", "albl_Class_", label,"%s", none, none, "0.0", "0.0", 15, 350)
           parameter(" albl_Class", "BEAM_class", string, text, 250, 350, 270)

           picture("470_2_line", 400, 2, 3, 390)
           parameter(" ", "T_offset", distance, number, 250, 450, 100)
           picture("TKK_SRC_Cross_offset", 150, 100, 350, 400)
       }
       tab_page("", "albl_Welds", 2)
       {
           parameter("albl_Size_above_line", "WLD_size", distance, number, 1)
           parameter("albl_Type_above_line", "WLD_type", weld_type, number, 2)
           parameter("albl_Size_below_line", "WLD2_size", distance, number, 3)
           parameter("albl_Type_below_line", "WLD2_type", weld_type, number, 4)
           picture("TKK_SRC_Cross_welds", 180, 150, 100, 150)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| BEAM_class | Parameter1 | Text | `2` |
| BEAM_finish | Parameter1 | Text | `` |
| BEAM_name | Parameter1 | Text | `C1` |
| BEAM_prefix | Parameter1 | Text | `A` |
| BEAM_startno | Parameter1 | Integer | `1` |
| H_mat | Parameter1 | Material | `` |
| H_prefix | Parameter1 | Text | `P` |
| H_prof | Parameter1 | Profile | `HI600-11-17*200` |
| H_startno | Parameter1 | Integer | `1` |
| T_mat | Parameter1 | Material | `` |
| T_offset | Parameter1 | Length | `0.000` |
| T_prefix | Parameter1 | Text | `P` |
| T_prof | Parameter1 | Profile | `T300-11-17*200` |
| T_startno | Parameter1 | Integer | `1` |
| WLD2_size | Parameter1 | Length | `6.000` |
| WLD2_type | Parameter1 | 10 | `10` |
| WLD_size | Parameter1 | Length | `6.000` |
| WLD_type | Parameter1 | 10 | `10` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.C1.Custom part ZX plane | `0.5*fP(proS,"ID67C2A424-0000-DB0A-3137-343038313038")` |
| D2 | D2.C1.Custom part ZX plane | `0.5*fP(proS,"ID67C2A424-0000-DB0A-3137-343038313038")` |
| D3 | D3.C1.Custom part ZX plane | `0.5*fP(proS,"ID67C2A424-0000-DB0A-3137-343038313038")` |
| D4 | D4.C1.Custom part ZX plane | `0.5*fP(proS,"ID67C2A424-0000-DB0A-3137-343038313038")` |
| D5 | D5.C1.Custom part end YZ plane | `` |
| D6 | D6.C1.Custom part end YZ plane | `` |
| D7 | D7.C1.Custom part end YZ plane | `` |
| D8 | D8.C1.Custom part XY plane | `T_offset` |
| D9 | D9.C1..Custom part XY plane | `T_offset` |
| D10 | D10.C1.Custom part XY plane | `T_offset` |
| D11 | D11.C1.Custom part XY plane | `T_offset` |
| D12 | D12.C1.ƒJƒXƒ^ƒ€•”�ÞZX•½–Ê | `` |
| D13 | D13.C1.ƒJƒXƒ^ƒ€•”�ÞXY•½–Ê | `` |
| D14 | D14.C1.ƒJƒXƒ^ƒ€•”�ÞZX•½–Ê | `` |
| D15 | D15.C1.ƒJƒXƒ^ƒ€•”�ÞXY•½–Ê | `` |
| D16 | D16.C1.ƒJƒXƒ^ƒ€•”�ÞYZ•½–Ê | `` |
| D17 | D17.C1.ƒJƒXƒ^ƒ€•”�ÞYZ•½–Ê | `` |
| D18 | D18.C1.ƒJƒXƒ^ƒ€•”�ÞYZ•½–Ê | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#83 | ASSEMBLYSERIE | `BEAM_prefix` |
| obj#83 | ASSEMBLYSTARTNUMBER | `BEAM_startno` |
| obj#83 | CLASS | `BEAM_class` |
| obj#83 | FINISH | `BEAM_finish` |
| obj#83 | MATERIAL | `H_mat` |
| obj#83 | NAME | `BEAM_name` |
| obj#83 | PROFILE | `H_prof` |
| obj#83 | SERIE | `H_prefix` |
| obj#83 | STARTNUMBER | `H_startno` |
| obj#108 | ASSEMBLYSERIE | `BEAM_prefix` |
| obj#108 | ASSEMBLYSTARTNUMBER | `BEAM_startno` |
| obj#108 | CLASS | `BEAM_class` |
| obj#108 | FINISH | `BEAM_finish` |
| obj#108 | MATERIAL | `T_mat` |
| obj#108 | NAME | `BEAM_name` |
| obj#108 | PROFILE | `T_prof` |
| obj#108 | SERIE | `T_prefix` |
| obj#108 | STARTNUMBER | `T_startno` |
| obj#118 | ASSEMBLYSERIE | `BEAM_prefix` |
| obj#118 | ASSEMBLYSTARTNUMBER | `BEAM_startno` |
| obj#118 | CLASS | `BEAM_class` |
| obj#118 | FINISH | `BEAM_finish` |
| obj#118 | MATERIAL | `T_mat` |
| obj#118 | NAME | `BEAM_name` |
| obj#118 | PROFILE | `T_prof` |
| obj#118 | SERIE | `T_prefix` |
| obj#118 | STARTNUMBER | `T_startno` |
| obj#221 | SIZE | `WLD_size` |
| obj#221 | SIZE2 | `WLD2_size` |
| obj#221 | TYPE | `WLD_type` |
| obj#221 | TYPE2 | `WLD2_type` |
| obj#316 | SIZE | `WLD_size` |
| obj#316 | SIZE2 | `WLD2_size` |
| obj#316 | TYPE | `WLD_type` |
| obj#316 | TYPE2 | `WLD2_type` |

## Preset `standard.p_TKK_SRC_Cross`
```
joint_attributes.saveas_file "standard"
joint_attributes.get_menu "standard"
joint_attributes.name_enable 1
joint_attributes.AtDepthValue 0.000000
joint_attributes.AtDepth 0
joint_attributes.RotationValue 0.000000
joint_attributes.Rotation 1
joint_attributes.OnPlaneValue 0.000000
joint_attributes.OnPlane 0
joint_attributes.WLD2_type 10
joint_attributes.WLD2_size 6.000000
joint_attributes.WLD_type 10
joint_attributes.WLD_size 6.000000
joint_attributes.T_offset 0.000000
joint_attributes.BEAM_class "2"
joint_attributes.T_mat "SN490B"
joint_attributes.H_mat "SN490B"
joint_attributes.T_prof "CT300*200*11*17"
joint_attributes.H_prof "H600*200*11*17"
joint_attributes.BEAM_name "1"
joint_attributes.BEAM_startno 1
joint_attributes.BEAM_prefix "A"
joint_attributes.T_startno 1
joint_attributes.H_startno 1
joint_attributes.T_prefix "P"
joint_attributes.H_prefix "P"
```