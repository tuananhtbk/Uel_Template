# TKK_BH_1Hnc_1Sec

- File: `NEC\TKK_BH_1HNC_1SEC.uel`  (13,265 bytes, gzip)
- Type in .inp: **macro**  | Catalog line: `-10	TKK_BH_1Hnc_1Sec	All`
- Dialog parameters: 5 | Variables (P/custom): 31 | Distance vars (D): 14 | Parts with bound profile: 3
- Lookup files (fVF): -
- Functions used: if, max
- Dialog bitmaps: 2 | Saved presets: standard.p_TKK_BH_1Hnc_1Sec

## Dialog (.inp)
```
page("Tekla Structures","")
{
   macro(1, "TKK_BH_1Hnc_1Sec")
   {
       helpurl("TKKComponentHelp.chm::/TKK_BH_Haunch.html")
       tab_page("", "albl_Attributes", 1)
       {
           picture("470_2_line", 400, 2, 3, 5)

           attribute(" NumberSeries","albl_NumberSeries", label,"%s", none, none, "0.0", "0.0", 15, -4)
           attribute(" Prefix","albl_Prefix_", label,"%s", none, none, "0.0", "0.0", 250,10)
           attribute(" Start number","albl_Start_number_", label,"%s", none, none, "0.0", "0.0", 400,10)
           attribute(" PART","albl_Part", label,"%s", none, none, "0.0", "0.0", 15,35,150)
           attribute(" Assembly","albl_Ass", label,"%s", none, none, "0.0", "0.0", 15,60,150)
           parameter(" part-mark", "prefix_PART", string, text, 250,35,120)
           parameter(" part-no", "startno_PART", integer, number, 400,35,120)
           parameter(" Assembly mark", "prefix_ASM", string, text, 250,60,120)
           parameter(" Assembly no", "startno_ASM", integer, number, 400,60,120)

           picture("470_2_line", 400, 2, 3, 105)

           attribute("NumberSeries","albl_Attributes", label,"%s", none, none, "0.0", "0.0", 15,97)
           attribute(" ", "albl_Name_", label,"%s", none, none, "0.0", "0.0", 15, 125)
           parameter(" ", "BEAM_name", string, text, 250, 125, 270)
           attribute(" ","albl_Material_", label,"%s", none, none, "0.0", "0.0", 15, 150)
           parameter(" ", "BEAM_mat", material, text, 250, 150, 270)
           attribute(" ", "albl_Finish_", label,"%s", none, none, "0.0", "0.0", 15, 175)
           parameter(" albl_Finish", "BEAM_finish", string, text, 250, 175, 270)
           attribute(" ", "albl_Class_", label,"%s", none, none, "0.0", "0.0", 15, 200)
           parameter(" albl_Class", "BEAM_class", string, text, 250, 200, 270)
           attribute(" ", "•”�Þ–¼¥�ÞŽ¿¥Ï°¸", label,"%s", none, none, "0.0", "0.0", 15, 225)
           attribute("ADV_part", "", option, "%s", none, none, "0.0", "0.0", 250, 225, 270, "toggle_field:!prefix_UFLG=0;!startno_UFLG=0;!UFLG_name=0;!UFLG_mat=0;!prefix_LFLG=0;!startno_LFLG=0;!LFLG_name=0;!LFLG_mat=0;!startno_WEB=0;!prefix_WEB=0;!WEB_name=0;!WEB_mat=0;!BEAM_name=1;!BEAM_mat=1;!prefix_PART=1;!startno_PART=1;")
           {
               value("‹¤’ÊŽw’è", 1)
               value("ŒÂ•ÊŽw’è", 0)
           }
           part("albl_UpperFlange", UFLG_t, UFLG_w, , , , , , 12)
           part("j_web", WEB_t, , , , , , , 13)
           part("albl_LowerFlange", LFLG_t, LFLG_w, , , , , , 14)

           parameter(" _prefix_UFLG", "prefix_UFLG", string, text, 365, 290, 50)
           parameter(" _prefix_WEB", "prefix_WEB", string, text, 365, 315, 50)
           parameter(" _prefix_LFLG", "prefix_LFLG", string, text, 365, 340, 50)

           parameter(" _startno_UFLG", "startno_UFLG", integer, number, 440, 290, 50)
           parameter(" _startno_WEB", "startno_WEB", integer, number, 440, 315, 50)
           parameter(" _startno_LFLG", "startno_LFLG", integer, number, 440, 340, 50)

           parameter(" _UFLG_mat", "UFLG_mat", material, text, 525, 290, 100)
           parameter(" _WEB_mat", "WEB_mat", material, text, 525, 315, 100)
           parameter(" _LFLG_mat", "LFLG_mat", material, text, 525, 340, 100)


           parameter(" _UFLG_name", "UFLG_name", string, text, 685, 290, 120)
           parameter(" _WEB_name", "WEB_name", string, text, 685, 315, 120)
           parameter(" _LFLG_name", "LFLG_name", string, text, 685, 340, 120)

           attribute(" ", "H1", label,"%s", none, none, "0.0", "0.0", 15, 375)
           parameter(" _H1", "BEAM_h1", distance, number, 75, 375, 120)
           attribute(" ", "H2", label,"%s", none, none, "0.0", "0.0", 615, 375)
           parameter(" _H2", "BEAM_h2", distance, number, 675, 375, 120)
           picture("TKK_BH_1Taper_1Sec", 500, 200, 50, 455)
       }
       tab_page("", "albl_Welds", 2)
       {
           parameter("albl_Size_above_line", "WLD_size", distance, number, 1)
           parameter("albl_Type_above_line", "WLD_type", weld_type, number, 2)
           parameter("albl_Size_below_line", "WLD2_size", distance, number, 3)
           parameter("albl_Type_below_line", "WLD2_type", weld_type, number, 4)
           picture("TKK_BuiltH_welds", 180, 150, 100, 150)
           }
  }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| BEAM_class | Class | Text | `4` |
| BEAM_finish | Finish | Text | `` |
| BEAM_h1 | H1 | Length | `600.00` |
| BEAM_h2 | H2 | Length | `500.00` |
| BEAM_mat | Beam material | Material | `` |
| BEAM_name | Name | Text | `G1` |
| LFLG_mat | LFLG mat | Material | `` |
| LFLG_name | LFLG name | Text | `FLANGE` |
| LFLG_t | LFLG t | Length | `25.00` |
| LFLG_w | LFLG w | Length | `200.00` |
| UFLG_mat | UFLG mat | Material | `` |
| UFLG_name | UFLG name | Text | `FLANGE` |
| UFLG_t | UFLG t | Length | `25.00` |
| UFLG_w | UFLG w | Length | `200.00` |
| WEB_mat | WEB mat | Material | `` |
| WEB_name | WEB name | Text | `WEB` |
| WEB_t | WEB t | Length | `16.00` |
| WLD2_size | Weld size (below) | Length | `6.00` |
| WLD2_type | Weld type (below) | 10 | `10` |
| WLD_size | Weld size (above) | Length | `6.00` |
| WLD_type | Weld type (above) | 10 | `10` |
| prefix_ASM | Assem prefix | Text | `A` |
| prefix_LFLG | LFLG prefix | Text | `P` |
| prefix_PART | Part prefix | Text | `P` |
| prefix_UFLG | UFLG prefix | Text | `P` |
| prefix_WEB | WEB prefix | Text | `P` |
| startno_ASM | Assem startno | Integer | `1` |
| startno_LFLG | LFLF startno | Integer | `1` |
| startno_PART | Part startno | Integer | `1` |
| startno_UFLG | UFLG startno | Integer | `1` |
| startno_WEB | WEB startno | Integer | `1` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.B1 | `` |
| D2 | D2.PLATE.B1 | `` |
| D3 | D3.PLATE.B1 | `` |
| D4 | D4.PLATE.B1 | `` |
| D5 | D5.B1.TaperH_1 | `max(BEAM_h1,BEAM_h2)*0.5` |
| D6 | D6.B1.TaperH_1 | `D5` |
| D7 | D7.B1.TaperH_1 | `BEAM_h1-max(BEAM_h1,BEAM_h2)*0.5` |
| D8 | D8.B1.TaperH_1 | `BEAM_h2-max(BEAM_h1,BEAM_h2)*0.5` |
| D9 | D9.PLATE.SM_End | `` |
| D10 | D10.SM_End.ƒJƒXƒ^ƒ€•”�Þ’[YZ•½–Ê | `` |
| D11 | D11.PLATE.LG_End | `` |
| D12 | D12.PLATE.LG_End | `` |
| D13 | D13.PLATE.SM_End | `` |
| D14 | D14.WEB_PLANE.ƒJƒXƒ^ƒ€•”�ÞXY•½–Ê | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#120 | ASSEMBLYSERIE | `prefix_ASM` |
| obj#120 | ASSEMBLYSTARTNUMBER | `startno_ASM` |
| obj#120 | CLASS | `BEAM_class` |
| obj#120 | MATERIAL | `if(ADV_part) then WEB_mat else BEAM_mat endif` |
| obj#120 | NAME | `if(ADV_part) then WEB_name else BEAM_name endif` |
| obj#120 | PROFILE | `"PL"+WEB_t` |
| obj#120 | SERIE | `if(ADV_part) then prefix_WEB else prefix_PART endif` |
| obj#120 | STARTNUMBER | `if(ADV_part) then startno_WEB else startno_PART endif` |
| obj#150 | ASSEMBLYSERIE | `prefix_ASM` |
| obj#150 | ASSEMBLYSTARTNUMBER | `startno_ASM` |
| obj#150 | CLASS | `BEAM_class` |
| obj#150 | MATERIAL | `if(ADV_part) then UFLG_mat else BEAM_mat endif` |
| obj#150 | NAME | `if(ADV_part) then UFLG_name else BEAM_name endif` |
| obj#150 | PROFILE | `"PL"+UFLG_w+"*"+UFLG_t` |
| obj#150 | SERIE | `if(ADV_part) then prefix_UFLG else prefix_PART endif` |
| obj#150 | STARTNUMBER | `if(ADV_part) then startno_UFLG else startno_PART endif` |
| obj#172 | ASSEMBLYSERIE | `prefix_ASM` |
| obj#172 | ASSEMBLYSTARTNUMBER | `startno_ASM` |
| obj#172 | CLASS | `BEAM_class` |
| obj#172 | MATERIAL | `if(ADV_part) then LFLG_mat else BEAM_mat endif` |
| obj#172 | NAME | `if(ADV_part) then LFLG_name else BEAM_name endif` |
| obj#172 | PROFILE | `"PL"+LFLG_w+"*"+LFLG_t` |
| obj#172 | SERIE | `if(ADV_part) then prefix_LFLG else prefix_PART endif` |
| obj#172 | STARTNUMBER | `if(ADV_part) then startno_LFLG else startno_PART endif` |
| obj#232 | GROUP_TYPE | `WLD_type` |
| obj#232 | SIZE | `WLD_size` |
| obj#232 | SIZE2 | `WLD2_size` |
| obj#232 | TYPE2 | `WLD2_type` |
| obj#339 | GROUP_TYPE | `WLD_type` |
| obj#339 | SIZE | `WLD_size` |
| obj#339 | SIZE2 | `WLD2_size` |
| obj#339 | TYPE2 | `WLD2_type` |

## Preset `standard.p_TKK_BH_1Hnc_1Sec`
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
joint_attributes.BEAM_h2 500.000000
joint_attributes.BEAM_h1 600.000000
joint_attributes.LFLG_w 200.000000
joint_attributes.LFLG_t 25.000000
joint_attributes.WEB_t 16.000000
joint_attributes.UFLG_w 200.000000
joint_attributes.UFLG_t 25.000000
joint_attributes.ADV_part 0
joint_attributes.BEAM_class "4"
joint_attributes.BEAM_mat "SN490B"
joint_attributes.BEAM_name "G1"
joint_attributes.startno_ASM 1
joint_attributes.prefix_ASM "A"
joint_attributes.startno_PART 1
joint_attributes.prefix_PART "P"
```