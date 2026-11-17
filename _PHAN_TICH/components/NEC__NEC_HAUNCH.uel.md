# NEC_HAUNCH

- File: `NEC\NEC_HAUNCH.uel`  (73,110 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	NEC_HAUNCH	All`
- Dialog parameters: 3 | Variables (P/custom): 63 | Distance vars (D): 50 | Parts with bound profile: 12
- Lookup files (fVF): -
- Functions used: if
- Dialog bitmaps: 9 | Saved presets: standard.p_NEC_HAUNCH, standard.p_NEC_HAUNCH

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(300, "NEC_HAUNCH")
   {
       tab_page("", " Pictures ", 1)
       {
           picture("NEC_LOGO",8,2,480,630)
           picture("NEC_HAUNCH1",8,2,200,100)
           
           parameter("", "P52", distance, number, 400,90,80)


           parameter("", "P53", distance, number, 339,412,60)
           parameter("", "P54", distance, number, 592,278,60)
           parameter("", "P55", distance, number, 505,270,60)
           
           
           parameter("", "P56", distance, number, 368,370,60)
           
           parameter("Connection Type", "P51_name", ComponentName, text, 19)
           parameter("Connection Attribute", "P51_attrfile", ComponentAttributeFile, text, 20)
           
           picture("NEC_HAUNCH_SELECT1",100,100,300,230)
           picture("NEC_HAUNCH_SELECT2",100,100,300,230)
           picture("NEC_HAUNCH_SELECT3",100,100,300,230)
           picture("NEC_HAUNCH_SELECT4",100,100,300,230)
           attribute("P63", "", option, "%s", no, none, "0.0", "0.0", 300,230,180,"toggle_field:P55=1,2;P56=1,3;=0,2;")
                {
                     value("NEC_HAUNCH_SELECT1.xbm",1)
                     value("NEC_HAUNCH_SELECT2.xbm",0)
                     value("NEC_HAUNCH_SELECT3.xbm",0)
                     value("NEC_HAUNCH_SELECT4.xbm",0)
                }

       }
       tab_page("", " Parts ", 2)
       {
           picture("NEC_HAUNCH_STIFFENER2",8,2,575,490)
           picture("NEC_HAUNCH2",8,2,280,280)
           attribute("", "1.Bracket", label,"%s",  none, none, "0.0", "0.0",20,50)
           attribute("", "2.Haunch Plate", label,"%s",  none, none, "0.0", "0.0",20,75)
           attribute("", "3.Haunch Plate", label,"%s",  none, none, "0.0", "0.0",20,100)
           attribute("", "4.Haunch Plate", label,"%s",  none, none, "0.0", "0.0",20,125)
           attribute("", "5.Haunch Plate", label,"%s",  none, none, "0.0", "0.0",20,150)
           attribute("", "6.HOR Stiffener", label,"%s",  none, none, "0.0", "0.0",20,175)
           attribute("", "7.VER Stiffener", label,"%s",  none, none, "0.0", "0.0",20,200)
           attribute("t", "t", label,"%s",  none, none, "0.0", "0.0",230,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",400,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",590,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",740,25)
           attribute(" ", " ", label,"%s",  none, none, "0.0", "0.0",0,650)
           
           parameter("", "P1", profile, text, 220,50,130)
           parameter("", "P2", distance, number, 220,75,50)
           parameter("", "P3", distance, number, 220,100,50)
           parameter("", "P4", distance, number, 220,125,50)
           parameter("", "P5", distance, number, 220,150,50)
           parameter("", "P6", distance, number, 220,175,50)
           parameter("", "P7", distance, number, 220,200,50)

           parameter("", "P14", string, text, 400,50,50)
           parameter("", "P15", string, text, 480,50,40)
           parameter("", "P16", string, text, 400,75,50)
           parameter("", "P17", string, text, 480,75,40)
           parameter("", "P18", string, text, 400,100,50)
           parameter("", "P19", string, text, 480,100,40)
           parameter("", "P20", string, text, 400,125,50)
           parameter("", "P21", string, text, 480,125,40)
           parameter("", "P22", string, text, 400,150,50)
           parameter("", "P23", string, text, 480,150,40)
           parameter("", "P24", string, text, 400,175,50)
           parameter("", "P25", string, text, 480,175,40)
           parameter("", "P26", string, text, 400,200,50)
           parameter("", "P27", string, text, 480,200,40)
           
           parameter("", "P28", material, text, 590,50,90)
           parameter("", "P29", material, text, 590,75,90)
           parameter("", "P30", material, text, 590,100,90)
           parameter("", "P31", material, text, 590,125,90)
           parameter("", "P32", material, text, 590,150,90)
           parameter("", "P33", material, text, 590,175,90)
           parameter("", "P34", material, text, 590,200,90)
           
           parameter("", "P35", string, text, 740,50,120)
           parameter("", "P36", string, text, 740,75,120)
           parameter("", "P37", string, text, 740,100,120)
           parameter("", "P38", string, text, 740,125,120)
           parameter("", "P39", string, text, 740,150,120)
           parameter("", "P40", string, text, 740,175,120)
           parameter("", "P41", string, text, 740,200,120)
           
           
           attribute("", "7.Stiffener NS", label,"%s",  none, none, "0.0", "0.0",575,440)
           parameter("", "P61", YesNo, number, 760,440,60)
           attribute("", "7.Stiffener FS", label,"%s",  none, none, "0.0", "0.0",575,465)           
           parameter("", "P62", YesNo, number, 760,465,60)
           
           
           picture("NEC_HAUNCH_STIFFENER1",8,2,10,445)
           attribute("", "6.Stiffener NS", label,"%s",  none, none, "0.0", "0.0",10,395)
           parameter("", "P59", YesNo, number, 200,395,60)
           attribute("", "6.Stiffener FS", label,"%s",  none, none, "0.0", "0.0",10,420)
           parameter("", "P60", YesNo, number, 200,420,60)
           parameter("", "P45", chamfer_type, number, 200,470,80)
           parameter("", "P46", distance, number, 200,495,80)
           parameter("", "P47", distance, number, 200,520,80)
           
           parameter("", "P42", chamfer_type, number, 450,550,80)
           parameter("", "P43", distance, number, 450,575,80)
           parameter("", "P44", distance, number, 450,600,80)
           

           
           parameter("", "P48", chamfer_type, number, 760,513,80)
           parameter("", "P49", distance, number, 760,538,80)
           parameter("", "P50", distance, number, 760,563,80)
           
                             
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | 브라켓 프로파일 | Profile | `H400*200*8*13` |
| P2 | 1번 두께 | Length | `10.00` |
| P3 | 2번 두께 | Length | `10.00` |
| P4 | 3번 두께 | Length | `10.00` |
| P5 | 4번 두께 | Length | `10.00` |
| P6 | 5번 두께 | Length | `10.00` |
| P7 | 6번 두께 | Length | `10.00` |
| P8 | 1번 두께 공식 | Profile | `"PL"+P2` *(formula)* |
| P9 | 2번 두께 공식 | Profile | `"PL"+P3` *(formula)* |
| P10 | 3번 두께 공식 | Profile | `"PL"+P4` *(formula)* |
| P11 | 4번 두께 공식 | Profile | `"PL"+P5` *(formula)* |
| P12 | 5번 두께 공식 | Profile | `"PL"+P6` *(formula)* |
| P13 | 6번 두께 공식 | Profile | `"PL"+P7` *(formula)* |
| P14 | 브라켓 Part prefix | Text | `` |
| P15 | 브라켓 Part start number | Text | `` |
| P16 | 1Part prefix | Text | `` |
| P17 | 1Part start number | Text | `` |
| P18 | 2Part prefix | Text | `` |
| P19 | 2Part start number | Text | `` |
| P20 | 3Part prefix | Text | `` |
| P21 | 3Part start number | 18 | `` |
| P22 | 4Part prefix | Text | `` |
| P23 | 4Part start number | Text | `` |
| P24 | 5Part prefix | Text | `` |
| P25 | 5Part start number | Text | `` |
| P26 | 6Part prefix | Text | `` |
| P27 | 6Part start number | Text | `` |
| P28 | 브라켓 재질 | Material | `` |
| P29 | 1 재질 | Material | `` |
| P30 | 2 재질 | Profile | `` |
| P31 | 3 재질 | Material | `` |
| P32 | 4 재질 | Material | `` |
| P33 | 5 재질 | Material | `` |
| P34 | 6 재질 | Profile | `` |
| P35 | 브라켓 이름 | Text | `` |
| P36 | 1 이름 | Text | `` |
| P37 | 2 이름 | Text | `` |
| P38 | 3 이름 | Text | `` |
| P39 | 4 이름 | Text | `` |
| P40 | 5 이름 | Text | `` |
| P41 | 6 이름 | 18 | `` |
| P42 | 1번 챔퍼 타입 | Chamfer | `` |
| P43 | 1번 챔퍼 X | Length | `` |
| P44 | 1번 챔퍼 Y | Length | `` |
| P45 | 5번 챔퍼 타입 | Chamfer | `` |
| P46 | 5번 챔퍼 X | Length | `` |
| P47 | 5번 챔퍼 Y | Length | `` |
| P48 | 6번 챔퍼 타입 | Chamfer | `` |
| P49 | 6번 챔퍼 X | Length | `` |
| P50 | 6번 챔퍼 Y | Length | `` |
| P51_attrfile | 컴퍼넌트 속성 | 21 | `` |
| P51_name | 컴퍼넌트 이름 | 13 | `128` |
| P52 | 브라켓 길이 | Length | `1000.00` |
| P53 | 헌치 너비 | Length | `400.00` |
| P54 | 헌치 높이 | Length | `400.00` |
| P55 | 헌치2번 높이 | Length | `100.00` |
| P56 | 헌치4번 너비 | Length | `100.00` |
| P57 | off | Yes/No | `0` |
| P58 | on | Yes/No | `1` |
| P59 | 5번 nearside stiffener on/off | Yes/No | `0` |
| P60 | 5번 farside stiffener on/off | Yes/No | `0` |
| P61 | 6번 nearside stiffener on/off | Yes/No | `0` |
| P62 | 6번 farside stiffener on/off | Yes/No | `0` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.Plane.XY plane | `` |
| D2 | D2.Plane.Extrema behind plane | `P52` |
| D3 | D3.Plane.Extrema bottom plane | `` |
| D4 | D4.Plane.Extrema top plane | `` |
| D5 | D5.Plane.Extrema bottom plane | `P54-P5` |
| D6 | D6..COLUMN | `` |
| D7 | D7..Plane | `` |
| D8 | D8.Plane.Extrema behind plane | `if (P63==3) \|\| (P63==1) then 0 else P56 endif` |
| D9 | D9.Plane.Extrema behind plane | `` |
| D10 | D10..Plane | `` |
| D11 | D11.. | `` |
| D12 | D12.Plane.Extrema behind plane | `P53-P3` |
| D13 | D13.Plane.Extrema front plane | `` |
| D14 | D14.Plane.Extrema behind plane | `` |
| D15 | D15.Plane.Extrema bottom plane | `if (P63==2) \|\| (P63==1) then 0 else P55 endif` |
| D16 | D16..COLUMN | `` |
| D17 | D17..COLUMN | `` |
| D18 | D18.. | `` |
| D19 | D19.. | `` |
| D20 | D20.Plane.Extrema bottom plane | `` |
| D21 | D21.. | `` |
| D22 | D22.. | `` |
| D23 | D23.. | `` |
| D24 | D24..COLUMN | `` |
| D25 | D25..Plane | `` |
| D26 | D26..COLUMN | `` |
| D27 | D27..Plane | `` |
| D28 | D28..COLUMN | `` |
| D29 | D29..COLUMN | `` |
| D30 | D30..COLUMN | `` |
| D31 | D31..COLUMN | `` |
| D32 | D32..COLUMN | `` |
| D33 | D33.. | `` |
| D34 | D34.. | `` |
| D35 | D35.. | `` |
| D36 | D36.. | `` |
| D37 | D37.. | `` |
| D38 | D38.. | `` |
| D39 | D39.. | `` |
| D40 | D40.. | `` |
| D41 | D41.. | `` |
| D42 | D42.. | `` |
| D43 | D43.. | `` |
| D44 | D44.. | `` |
| D45 | D45.. | `` |
| D46 | D46.. | `` |
| D47 | D47.. | `` |
| D48 | D48.. | `` |
| D49 | D49..COLUMN | `` |
| D50 | D50..COLUMN | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#226 | CREATION | `if (P63==3) then 1 else 0 endif` |
| obj#226 | MATERIAL | `P29` |
| obj#226 | NAME | `P36` |
| obj#226 | PROFILE | `P8` |
| obj#226 | SERIE | `P16` |
| obj#226 | STARTNUMBER | `P17` |
| obj#240 | CREATION | `if (P63==2) then 1 else 0 endif` |
| obj#240 | MATERIAL | `P29` |
| obj#240 | NAME | `P36` |
| obj#240 | PROFILE | `P8` |
| obj#240 | SERIE | `P16` |
| obj#240 | STARTNUMBER | `P17` |
| obj#248 | CREATION | `if (P63 == 1 ) \|\| (P63==2) then 0 else 1 endif` |
| obj#248 | MATERIAL | `P30` |
| obj#248 | NAME | `P37` |
| obj#248 | PROFILE | `P9` |
| obj#248 | SERIE | `P18` |
| obj#248 | STARTNUMBER | `P19` |
| obj#258 | CREATION | `if (P63==1) \|\| (P63==2) \|\| (P63==3)then 0 else 1 endif` |
| obj#258 | MATERIAL | `P29` |
| obj#258 | NAME | `P36` |
| obj#258 | PROFILE | `P8` |
| obj#258 | SERIE | `P16` |
| obj#258 | STARTNUMBER | `P17` |
| obj#267 | CREATION | `if (P63==1) then 1 else 0 endif` |
| obj#267 | MATERIAL | `P29` |
| obj#267 | NAME | `P36` |
| obj#267 | PROFILE | `P8` |
| obj#267 | SERIE | `P16` |
| obj#267 | STARTNUMBER | `P17` |
| obj#276 | CREATION | `if (P63 == 1) \|\| (P63==3)then 0 else 1 endif` |
| obj#276 | NAME | `P39` |
| obj#276 | PROFILE | `P11` |
| obj#276 | SERIE | `P22` |
| obj#276 | STARTNUMBER | `P23` |
| obj#285 | MATERIAL | `P31` |
| obj#285 | NAME | `P38` |
| obj#285 | PROFILE | `P10` |
| obj#285 | SERIE | `P20` |
| obj#285 | STARTNUMBER | `P21` |
| obj#294 | MATERIAL | `P28` |
| obj#294 | NAME | `P35` |
| obj#294 | PROFILE | `P1` |
| obj#294 | SERIE | `P14` |
| obj#294 | STARTNUMBER | `P15` |
| obj#305 | CREATION | `P60` |
| obj#305 | MATERIAL | `P33` |
| obj#305 | NAME | `P40` |
| obj#305 | PROFILE | `P12` |
| obj#305 | SERIE | `P24` |
| obj#305 | STARTNUMBER | `P25` |
| obj#316 | CREATION | `P59` |
| obj#316 | MATERIAL | `P33` |
| obj#316 | NAME | `P40` |
| obj#316 | PROFILE | `P12` |
| obj#316 | SERIE | `P24` |
| obj#316 | STARTNUMBER | `P25` |
| obj#324 | CREATION | `P61` |
| obj#324 | MATERIAL | `P34` |
| obj#324 | NAME | `P41` |
| obj#324 | PROFILE | `P13` |
| obj#324 | SERIE | `P26` |
| obj#324 | STARTNUMBER | `P27` |
| obj#333 | CREATION | `P62` |
| obj#333 | MATERIAL | `P34` |
| obj#333 | NAME | `P41` |
| obj#333 | PROFILE | `P13` |
| obj#333 | SERIE | `P26` |
| obj#333 | STARTNUMBER | `P27` |
| obj#340 | ATTRIBUTE_FILE | `P51_attrfile` |
| obj#340 | NAME | `P51_name` |

## Preset `standard.p_NEC_HAUNCH`
```
joint_attributes.saveas_file "23"
joint_attributes.get_menu "23"
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
joint_attributes.zang1 -0.000000
joint_attributes.zsuunta 7
joint_attributes.P50 30.000000
joint_attributes.P49 30.000000
joint_attributes.P48 3
joint_attributes.P44 30.000000
joint_attributes.P43 30.000000
joint_attributes.P42 3
joint_attributes.P47 30.000000
joint_attributes.P46 30.000000
joint_attributes.P45 3
joint_attributes.P60 1
joint_attributes.P59 1
joint_attributes.P62 1
joint_attributes.P61 1
joint_attributes.P41 "PLATE"
joint_attributes.P40 "PLATE"
joint_attributes.P39 "PLATE"
joint_attributes.P38 "PLATE"
joint_attributes.P37 "PLATE"
joint_attributes.P36 "PLATE"
joint_attributes.P35 "BRACKET"
joint_attributes.P34 "SS400"
joint_attributes.P33 "SS400"
joint_attributes.P32 "SS400"
joint_attributes.P31 "SS400"
joint_attributes.P30 "SS400"
joint_attributes.P29 "SS400"
joint_attributes.P28 "SS400"
joint_attributes.P27 "1"
joint_attributes.P26 "PL"
joint_attributes.P25 "1"
joint_attributes.P24 "PL"
joint_attributes.P23 "1"
joint_attributes.P22 "PL"
joint_attributes.P21 "1"
joint_attributes.P20 "PL"
joint_attributes.P19 "1"
joint_attributes.P18 "PL"
joint_attributes.P17 "1"
joint_attributes.P16 "PL"
joint_attributes.P15 "1"
joint_attributes.P14 "PL"
joint_attributes.P7 10.000000
joint_attributes.P6 10.000000
joint_attributes.P5 10.000000
joint_attributes.P4 10.000000
joint_attributes.P3 10.000000
joint_attributes.P2 10.000000
joint_attributes.P1 "H400*200*8*13"
joint_attributes.P63 0
joint_attributes.P51_attrfile "standard"
joint_attributes.P51_name "128"
joint_attributes.P56 100.000000
joint_attributes.P55 100.000000
joint_attributes.P54 400.000000
joint_attributes.P53 400.000000
joint_attributes.P52 1000.000000
```

## Preset `standard.p_NEC_HAUNCH`
```
joint_attributes.saveas_file "23"
joint_attributes.get_menu "23"
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
joint_attributes.zang1 -0.000000
joint_attributes.zsuunta 7
joint_attributes.P50 30.000000
joint_attributes.P49 30.000000
joint_attributes.P48 3
joint_attributes.P44 30.000000
joint_attributes.P43 30.000000
joint_attributes.P42 3
joint_attributes.P47 30.000000
joint_attributes.P46 30.000000
joint_attributes.P45 3
joint_attributes.P60 1
joint_attributes.P59 1
joint_attributes.P62 1
joint_attributes.P61 1
joint_attributes.P41 "PLATE"
joint_attributes.P40 "PLATE"
joint_attributes.P39 "PLATE"
joint_attributes.P38 "PLATE"
joint_attributes.P37 "PLATE"
joint_attributes.P36 "PLATE"
joint_attributes.P35 "BRACKET"
joint_attributes.P34 "SS400"
joint_attributes.P33 "SS400"
joint_attributes.P32 "SS400"
joint_attributes.P31 "SS400"
joint_attributes.P30 "SS400"
joint_attributes.P29 "SS400"
joint_attributes.P28 "SS400"
joint_attributes.P27 "1"
joint_attributes.P26 "PL"
joint_attributes.P25 "1"
joint_attributes.P24 "PL"
joint_attributes.P23 "1"
joint_attributes.P22 "PL"
joint_attributes.P21 "1"
joint_attributes.P20 "PL"
joint_attributes.P19 "1"
joint_attributes.P18 "PL"
joint_attributes.P17 "1"
joint_attributes.P16 "PL"
joint_attributes.P15 "1"
joint_attributes.P14 "PL"
joint_attributes.P7 10.000000
joint_attributes.P6 10.000000
joint_attributes.P5 10.000000
joint_attributes.P4 10.000000
joint_attributes.P3 10.000000
joint_attributes.P2 10.000000
joint_attributes.P1 "H400*200*8*13"
joint_attributes.P63 0
joint_attributes.P51_attrfile "standard"
joint_attributes.P51_name "128"
joint_attributes.P56 100.000000
joint_attributes.P55 100.000000
joint_attributes.P54 400.000000
joint_attributes.P53 400.000000
joint_attributes.P52 1000.000000
```