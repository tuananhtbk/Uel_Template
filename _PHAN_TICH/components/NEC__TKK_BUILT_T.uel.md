# TKK_BuiltT

- File: `NEC\TKK_BUILT_T.uel`  (9,052 bytes, gzip)
- Type in .inp: **macro**  | Catalog line: `-10	TKK_BuiltT	All`
- Dialog parameters: 4 | Variables (P/custom): 26 | Distance vars (D): 12 | Parts with bound profile: 2
- Lookup files (fVF): -
- Functions used: mid
- Dialog bitmaps: 1 | Saved presets: standard.p_TKK_BuiltT

## Dialog (.inp)
```
page("Tekla Structures","")
{
   macro(1, "TKK_BuiltT")
   {
       helpurl("TKKComponentHelp.chm::/TKK_BuiltT.html")
       tab_page("", "albl_Attributes", 1)
       {
           picture("470_2_line", 400, 2, 3, 5)

           attribute(" NumberSeries","albl_NumberSeries", label,"%s", none, none, "0.0", "0.0", 15, -4)
           attribute(" Prefix","albl_Prefix_", label,"%s", none, none, "0.0", "0.0", 250,10)
           attribute(" Start number","albl_Start_number_", label,"%s", none, none, "0.0", "0.0", 400,10)
           attribute(" UFL","j_flange", label,"%s", none, none, "0.0", "0.0", 15,35,150)
           attribute(" WEB","j_web", label,"%s", none, none, "0.0", "0.0", 15,60,150)
           attribute(" Assembly","albl_Ass", label,"%s", none, none, "0.0", "0.0", 15,85,150)
           parameter(" Uflg p-mark", "UFL_prefix", string, text, 250,35,120)
           parameter(" Web p-mark", "WEB_prefix", string, text, 250,60,120)
           parameter(" Uflg no", "UFL_startno", integer, number, 400,35,120)
           parameter(" Web no", "WEB_startno", integer, number, 400,60,120)
           parameter(" Assembly mark", "BEAM_prefix", string, text, 250,85,120)
           parameter(" Assembly no", "BEAM_startno", integer, number, 400,85,120)

           picture("470_2_line", 400, 2, 3, 130)

           attribute("NumberSeries","albl_Attributes", label,"%s", none, none, "0.0", "0.0", 15,122)
           attribute(" ", "albl_Name_", label,"%s", none, none, "0.0", "0.0", 15, 150)
           attribute(" ", "albl_Profile_", label,"%s", none, none, "0.0", "0.0", 15, 175)
           parameter(" ", "BEAM_name", string, text, 250, 150, 270)
           parameter(" ", "PROF", string, text, 250, 175, 270)

           attribute(" ","albl_Material_", label,"%s", none, none, "0.0", "0.0", 15, 200)
           attribute(" ", "j_flange", label,"%s", none, none, "0.0", "0.0", 25, 225)
           parameter(" ", "UFL_mat", material, text, 250, 225, 270)
           attribute(" ", "j_web", label,"%s", none, none, "0.0", "0.0", 25, 250)
           parameter(" j_web", "WEB_mat", material, text, 250, 250, 270)
           attribute(" ", "albl_Finish_", label,"%s", none, none, "0.0", "0.0", 15, 275)
           parameter(" albl_Finish", "BEAM_finish", string, text, 250, 275, 270)
           attribute(" ", "albl_Class_", label,"%s", none, none, "0.0", "0.0", 15, 300)
           parameter(" albl_Class", "BEAM_class", string, text, 250, 300, 270)

       }
       tab_page("", "albl_Welds", 2)
       {
           parameter("albl_Size_above_line", "WLD_size", distance, number, 1)
           parameter("albl_Type_above_line", "WLD_type", weld_type, number, 2)
           parameter("albl_Size_below_line", "WLD2_size", distance, number, 3)
           parameter("albl_Type_below_line", "WLD2_type", weld_type, number, 4)
           picture("TKK_BuiltT_welds", 180, 150, 100, 150)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| PROF | User input profile | Text | `BT300*200*12*25` |
| PROF2 | Truncate | Text | `mid(PROF,LOC1+1)` *(formula)* |
| PROF3 | Truncate | Text | `mid(PROF2,LOC2+1)` *(formula)* |
| BEAM_class | Class | Text | `4` |
| BEAM_finish | Beam finish | Text | `` |
| BEAM_name | Beam name | Text | `G1` |
| BEAM_prefix | Assembly mark | Text | `A` |
| BEAM_startno | Assembly no | Integer | `1` |
| H | Get first parameter | Length | `mid(PROF,2,LOC1-2)` *(formula)* |
| LOC1 | Find first * | Integer | `find(PROF,"*")` *(formula)* |
| LOC2 | Find second * | Integer | `find(PROF2,"*")` *(formula)* |
| LOC3 | Find third * | Integer | `find(PROF3,"*")` *(formula)* |
| S | Get third parameter | Length | `mid(PROF3,0,LOC3)` *(formula)* |
| T | Get last parameter | Length | `mid(PROF3,LOC3+1)` *(formula)* |
| UFL_name | Uflg Name | Text | `BEAM_name` *(formula)* |
| UFL_prefix | Uflg p-mark | Text | `P` |
| UFL_startno | Uflg no | Integer | `1` |
| W | Get second parameter | Length | `mid(PROF2,0,LOC2)` *(formula)* |
| WEB_mat | Web material | Material | `` |
| WEB_name | Web Name | Text | `BEAM_name` *(formula)* |
| WEB_prefix | Wen p-mark | Text | `P` |
| WEB_startno | Web no | Integer | `1` |
| WLD2_size | Parameter1 | Length | `6` |
| WLD2_type | Parameter1 | 10 | `10` |
| WLD_size | Weld size | Length | `6.00` |
| WLD_type | Parameter1 | 10 | `10` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.G1.ƒJƒXƒ^ƒ€•”�Þ’[YZ•½–Ê | `` |
| D2 | D2.G1.ƒJƒXƒ^ƒ€•”�Þ’[YZ•½–Ê | `` |
| D3 | D3.G1.Custom part ZX plane | `T*0.5` |
| D4 | D4.G1.Custom part ZX plane | `T*0.5` |
| D5 | D5.G1.ƒJƒXƒ^ƒ€•”�ÞYZ•½–Ê | `` |
| D6 | D6.G1.ƒJƒXƒ^ƒ€•”�ÞXY•½–Ê | `` |
| D7 | D7.G1.ƒJƒXƒ^ƒ€•”�ÞXY•½–Ê | `` |
| D8 | D8.G1.ƒJƒXƒ^ƒ€•”�ÞYZ•½–Ê | `` |
| D15 | D15.G1.ƒJƒXƒ^ƒ€•”�ÞZX•½–Ê | `H*0.5` |
| D16 | D16.G1.ƒJƒXƒ^ƒ€•”�ÞZX•½–Ê | `H*0.5` |
| D19 | D19.G1.ƒJƒXƒ^ƒ€•”�ÞXY•½–Ê | `` |
| D20 | D20.G1.ƒJƒXƒ^ƒ€•”�ÞXY•½–Ê | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#110 | ASSEMBLYSERIE | `BEAM_prefix` |
| obj#110 | ASSEMBLYSTARTNUMBER | `BEAM_startno` |
| obj#110 | CLASS | `BEAM_class` |
| obj#110 | FINISH | `BEAM_finish` |
| obj#110 | MATERIAL | `WEB_mat` |
| obj#110 | NAME | `WEB_name` |
| obj#110 | PROFILE | `"PL"+(H-T)+"*"+S` |
| obj#110 | SERIE | `WEB_prefix` |
| obj#110 | STARTNUMBER | `WEB_startno` |
| obj#134 | ASSEMBLYSERIE | `BEAM_prefix` |
| obj#134 | ASSEMBLYSTARTNUMBER | `BEAM_startno` |
| obj#134 | CLASS | `BEAM_class` |
| obj#134 | FINISH | `BEAM_finish` |
| obj#134 | MATERIAL | `UFL_mat` |
| obj#134 | NAME | `UFL_name` |
| obj#134 | PROFILE | `"PL"+W+"*"+T` |
| obj#134 | SERIE | `UFL_prefix` |
| obj#134 | STARTNUMBER | `UFL_startno` |
| obj#280 | SIZE | `WLD_size` |
| obj#280 | SIZE2 | `WLD2_size` |
| obj#280 | TYPE | `WLD_type` |
| obj#280 | TYPE2 | `WLD2_type` |

## Preset `standard.p_TKK_BuiltT`
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
joint_attributes.BEAM_class "4"
joint_attributes.WEB_mat "SN490B"
joint_attributes.UFL_mat "SN490B"
joint_attributes.PROF "BT300*200*12*25"
joint_attributes.BEAM_name "G1"
joint_attributes.BEAM_startno 1
joint_attributes.BEAM_prefix "A"
joint_attributes.WEB_startno 1
joint_attributes.UFL_startno 1
joint_attributes.WEB_prefix "P"
joint_attributes.UFL_prefix "P"
```