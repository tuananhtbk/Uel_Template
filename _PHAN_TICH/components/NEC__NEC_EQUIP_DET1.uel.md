# NEC_EQUIP_Det1

- File: `NEC\NEC_EQUIP_DET1.uel`  (65,595 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	NEC_EQUIP_Det1	All`
- Dialog parameters: 0 | Variables (P/custom): 69 | Distance vars (D): 22 | Parts with bound profile: 17
- Lookup files (fVF): -
- Functions used: if
- Dialog bitmaps: 6 | Saved presets: standard.p_NEC_EQUIP_Det1, standard.p_NEC_EQUIP_Det1

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "NEC_EQUIP_Det1")
   {
       tab_page("", " Pictures ", 1)
       {
           picture("NEC_LOGO",8,2,480,630)
           picture("NEC_EQUIP_1",100,50,100,50)
           picture("NEC_EQUIP_2",50,100,150,320)
           
           parameter("", "P47", distance, number, 340,55,80)
           parameter("", "P48", distance, number, 100,125,60)
           parameter("", "P49", distance, number, 613,125,60)
           parameter("", "P50", distance, number, 710,125,60)
           parameter("", "P51", distance, number, 100,475,60)
           parameter("", "P52", distance, number, 155,295,60)
           parameter("", "P53", distance, number, 314,210,60)
           parameter("", "P54", distance, number, 410,210,60)
           
           attribute("", "보강재 수(3~5)", label,"%s",  none, none, "0.0", "0.0",328,87)
           parameter("", "P57", integer, number, 360,120,50)

           

       }
       tab_page("", " Plates ", 2)
       {
           picture("NEC_EQUIP_3",50,100,210,310)
           attribute("", "1.Top Plate", label,"%s",  none, none, "0.0", "0.0",20,50)
           attribute("", "2.Web Plate", label,"%s",  none, none, "0.0", "0.0",20,100)
           attribute("", "3.Top Stiffener", label,"%s",  none, none, "0.0", "0.0",20,150)
           attribute("", "4.N/S Stiffener", label,"%s",  none, none, "0.0", "0.0",20,200)
           attribute("", "5.F/S Stiffener", label,"%s",  none, none, "0.0", "0.0",20,250)
           attribute("t", "t", label,"%s",  none, none, "0.0", "0.0",230,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",390,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",590,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",740,25)
           attribute(" ", " ", label,"%s",  none, none, "0.0", "0.0",0,650)
           
           parameter("", "P1", distance, number, 220,50,50)
           parameter("", "P2", distance, number, 220,100,50)
           parameter("", "P3", distance, number, 220,150,50)
           parameter("", "P4", distance, number, 220,200,50)
           parameter("", "P5", distance, number, 220,250,50)
           parameter("", "P11", string, text, 390,50,50)
           parameter("", "P12", string, text, 470,50,40)
           parameter("", "P13", string, text, 390,75,50)
           parameter("", "P14", string, text, 470,75,40)
           parameter("", "P15", string, text, 390,100,50)
           parameter("", "P16", string, text, 470,100,40)
           parameter("", "P17", string, text, 390,125,50)
           parameter("", "P18", string, text, 470,125,40)
           parameter("", "P19", string, text, 390,150,50)
           parameter("", "P20", string, text, 470,150,40)
           parameter("", "P21", string, text, 390,175,50)
           parameter("", "P22", string, text, 470,175,40)
           parameter("", "P23", string, text, 390,200,50)
           parameter("", "P24", string, text, 470,200,40)
           parameter("", "P25", string, text, 390,225,50)
           parameter("", "P26", string, text, 470,225,40)
           parameter("", "P27", string, text, 390,250,50)
           parameter("", "P28", string, text, 470,250,40)
           parameter("", "P29", string, text, 390,275,50)
           parameter("", "P30", string, text, 470,275,40)
           parameter("", "P31", material, text, 590,50,90)
           parameter("", "P32", material, text, 590,100,90)
           parameter("", "P33", material, text, 590,150,90)
           parameter("", "P34", material, text, 590,200,90)
           parameter("", "P35", material, text, 590,250,90)
           parameter("", "P36", string, text, 740,50,120)
           parameter("", "P37", string, text, 740,100,120)
           parameter("", "P38", string, text, 740,150,120)
           parameter("", "P39", string, text, 740,200,120)
           parameter("", "P40", string, text, 740,250,120)
           parameter("", "P41", chamfer_type, number, 120,400,100)
           parameter("", "P42", distance, number, 120,430,100)
           parameter("", "P43", distance, number, 120,455,100)
           parameter("", "P44", chamfer_type, number, 530,490,100)
           parameter("", "P45", distance, number, 530,520,100)
           parameter("", "P46", distance, number, 530,555,100)


       }
       tab_page("", " Bolts ", 3)
       {
           
           picture("NEC_SLOT",10,5,450,50)
           attribute("size", "Bolt size", label,"%s",  none, none, "0.0", "0.0",30,50)
           attribute("standard", "Bolt standard", label,"%s",  none, none, "0.0", "0.0",30,75)
           attribute("Tolerance", "Tolerance", label,"%s",  none, none, "0.0", "0.0",30,100)
           
           parameter("", "P58_diameter", bolt_size, number, 210,50,80)
           parameter("", "P58_screwdin", bolt_standard, text, 210,75,80)
           parameter("", "P59", distance, number, 210,100,80)
           parameter("", "P60", distance, number, 370,62,60)
           parameter("", "P61", distance, number, 480,120,60)
           
           picture("NEC_EQUIP_4",100,50,140,230)
           parameter("", "P62", integer, number, 270,200,50)
           parameter("", "P65", distance, number, 360,200,80)
           parameter("", "P66", distance, number, 640,390,80)
           
           attribute("", "Bolt UDA(Comment):", label,"%s",  none, none, "0.0", "0.0",30,500)
           parameter("", "P67", string, text, 260,500,200)



           
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | 1번 플레이드 두께 | Length | `10.00` |
| P2 | 2번 플레이드 두께 | Length | `10.00` |
| P3 | 3번 플레이드 두께 | Length | `10.00` |
| P4 | 4번 플레이드 두께 | Length | `10.00` |
| P5 | 5번 플레이드 두께 | Length | `10.00` |
| P6 | 1번 플레이드 두께 공식 | Profile | `"PL"+P1` *(formula)* |
| P7 | 2번 플레이드 두께 공식 | Profile | `"PL"+P2` *(formula)* |
| P8 | 3번 플레이드 두께 공식 | Profile | `"PL"+P3` *(formula)* |
| P9 | 4번 플레이드 두께 공식 | Profile | `"PL"+P4` *(formula)* |
| P10 | 5번 플레이드 두께 공식 | Profile | `"PL"+P5` *(formula)* |
| P11 | 1번 Part prefix | Text | `` |
| P12 | 1번 Part start number | Text | `` |
| P13 | 1번 Assembly prefix | Text | `` |
| P14 | 1번 Assembly start number | Text | `` |
| P15 | 2번 Part prefix | Text | `` |
| P16 | 2번 Part start number | Text | `` |
| P17 | 2번 Assembly prefix | Text | `` |
| P18 | 2번 Assembly start number | Text | `` |
| P19 | 3번 Part prefix | Text | `` |
| P20 | 3번 Part start number | Text | `` |
| P21 | 3번 Assembly prefix | Text | `` |
| P22 | 3번 Assembly start number | Text | `` |
| P23 | 4번 Part prefix | Text | `` |
| P24 | 4번 Part start number | Text | `` |
| P25 | 4번 Assembly prefix | Text | `` |
| P26 | 4번 Assembly start number | Text | `` |
| P27 | 5번 Part prefix | Text | `` |
| P28 | 5번 Part start number | Text | `` |
| P29 | 5번 Assembly prefix | Text | `` |
| P30 | 5번 Assembly start number | Text | `` |
| P31 | 1번 재질 | Material | `` |
| P32 | 2번 재질 | Material | `` |
| P33 | 3번 재질 | Material | `` |
| P34 | 4번 재질 | Material | `` |
| P35 | 5번 재질 | Material | `` |
| P36 | 1번 이름 | Text | `` |
| P37 | 2번 이름 | Text | `` |
| P38 | 3번 이름 | Text | `` |
| P39 | 4번 이름 | Text | `` |
| P40 | 5번 이름 | 18 | `` |
| P41 | 3번 챔퍼 타입 | Chamfer | `` |
| P42 | 3번 챔퍼 크기X | Length | `` |
| P43 | 3번 챔퍼 크기Y | Length | `` |
| P44 | 4,5번 챔퍼 타입 | Chamfer | `` |
| P45 | 4,5번 챔퍼 크기X | Length | `` |
| P46 | 4,5번 챔퍼 크기Y | Length | `` |
| P47 | 전체 스판의 길 | Length | `2000.00` |
| P48 | 전체 스판에서 3까지 거리 우측 | Length | `50.00` |
| P49 | 전체 스판에서 3까지 거리 좌측 | Length | `50.00` |
| P50 | 1,2,3의 높이 | Length | `250.00` |
| P51 | ¸ÞÀÎ ÇÃ·£Áö ¿§Áö¿¡¼­ 2¹ø±îÁö ° | Length | `20.00` |
| P52 | 1번 플레이트 튀어나온 길이 | Length | `20.00` |
| P53 | 3,4,5 우측의 2번째 거리 | Length | `470.00` |
| P54 | 3,4,5 좌측의 2번째 거리 | Length | `470.00` |
| P55 | off | Yes/No | `0` |
| P56 | on | Yes/No | `1` |
| P57 | 보강재 개수 | Integer | `5` |
| P58_diameter | 볼트 사이즈 | 6 | `20.00` |
| P58_screwdin | 볼트 스탠다드 | 7 | `` |
| P59 | 톨러런스 | Length | `` |
| P60 | 슬롯 홀 X | Length | `` |
| P61 | 슬롯 홀Y | Length | `` |
| P62 | 볼트 개수 | Integer | `2` |
| P63 | option | Integer | `ceil(fabs(P62-1.9))` *(formula)* |
| P64 | 볼트 간격 공식 | 9 | `P63+"*"+P65` *(formula)* |
| P65 | 볼트 간격 | Length | `1500.00` |
| P66 | 볼트 엣지거리 | Length | `100.00` |
| P67 | 볼트 UDA | Text | `Fixed Hole` |
| P70 | 5 | Text | `5` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.Component 중심에 바인드 | `` |
| D2 | D2.Plane.YZ plane | `P53` |
| D3 | D3.Plane.YZ plane | `P54` |
| D4 | D4.Plane.YZ plane | `(P47*0.5)-P48` |
| D5 | D5.Plane.Plane | `(P47*0.5)-P49` |
| D6 | D6.1번 좌측 끝까지 길이에 바인드 | `P47*0.5` |
| D7 | D7.1번 우측 끝까지 길이에 바인드 | `P47*0.5` |
| D8 | D8.Plane.메인부재 하단 플랜지면에 바인드 | `` |
| D9 | D9.Plane.메인부재 상단 내부 플랜지에 바인드 | `` |
| D10 | D10.Plane.메인부재 상단플랜지면에 바인드 | `` |
| D11 | D11.Plane.Extrema behind plane | `P50` |
| D12 | D12.Plane.메인부재 우측 엣지면에 바인드 | `` |
| D13 | D13.Plane.메인부재 좌측 엣지면에 바인드 | `` |
| D14 | D14.Plane.Extrema bottom plane | `P51` |
| D15 | D15.Plane.메인 부재 우측 웨브면에 바인드 | `` |
| D16 | D16.Plane.메인 부재 좌측 웨브면에 바인드 | `` |
| D17 | D17.1번 bottom면에 바인드 | `` |
| D18 | D18.Plane.BEAM | `` |
| D19 | D19.Plane.BEAM | `` |
| D20 | D20.Plane.BEAM | `` |
| D21 | D21.Plane.Extrema front plane | `P52` |
| D22 | D22.Plane.Top plane | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#241 | ASSEMBLYSERIE | `P13` |
| obj#241 | ASSEMBLYSTARTNUMBER | `P14` |
| obj#241 | MATERIAL | `P31` |
| obj#241 | NAME | `P36` |
| obj#241 | PROFILE | `P6` |
| obj#241 | SERIE | `P11` |
| obj#241 | STARTNUMBER | `P12` |
| obj#255 | ASSEMBLYSERIE | `P17` |
| obj#255 | ASSEMBLYSTARTNUMBER | `P18` |
| obj#255 | MATERIAL | `P32` |
| obj#255 | NAME | `P37` |
| obj#255 | PROFILE | `P7` |
| obj#255 | SERIE | `P15` |
| obj#255 | STARTNUMBER | `P16` |
| obj#265 | ASSEMBLYSERIE | `P25` |
| obj#265 | ASSEMBLYSTARTNUMBER | `P26` |
| obj#265 | CREATION | `if (P57=="3") \|\| (P57=="5") then P56 else P55 endif` |
| obj#265 | MATERIAL | `P34` |
| obj#265 | NAME | `P39` |
| obj#265 | PROFILE | `P9` |
| obj#265 | SERIE | `P23` |
| obj#265 | STARTNUMBER | `P24` |
| obj#274 | ASSEMBLYSERIE | `P29` |
| obj#274 | ASSEMBLYSTARTNUMBER | `P30` |
| obj#274 | CREATION | `if (P57=="3") \|\| (P57=="5") then P56 else P55 endif` |
| obj#274 | MATERIAL | `P35` |
| obj#274 | NAME | `P40` |
| obj#274 | PROFILE | `P10` |
| obj#274 | SERIE | `P27` |
| obj#274 | STARTNUMBER | `P28` |
| obj#280 | ASSEMBLYSERIE | `P29` |
| obj#280 | ASSEMBLYSTARTNUMBER | `P30` |
| obj#280 | MATERIAL | `P35` |
| obj#280 | NAME | `P40` |
| obj#280 | PROFILE | `P10` |
| obj#280 | SERIE | `P27` |
| obj#280 | STARTNUMBER | `P28` |
| obj#287 | ASSEMBLYSERIE | `P25` |
| obj#287 | ASSEMBLYSTARTNUMBER | `P26` |
| obj#287 | MATERIAL | `P34` |
| obj#287 | NAME | `P39` |
| obj#287 | PROFILE | `P9` |
| obj#287 | SERIE | `P23` |
| obj#287 | STARTNUMBER | `P24` |
| obj#293 | ASSEMBLYSERIE | `P25` |
| obj#293 | ASSEMBLYSTARTNUMBER | `P26` |
| obj#293 | MATERIAL | `P34` |
| obj#293 | NAME | `P39` |
| obj#293 | PROFILE | `P9` |
| obj#293 | SERIE | `P23` |
| obj#293 | STARTNUMBER | `P24` |
| obj#300 | ASSEMBLYSERIE | `P29` |
| obj#300 | ASSEMBLYSTARTNUMBER | `P30` |
| obj#300 | MATERIAL | `P35` |
| obj#300 | NAME | `P40` |
| obj#300 | PROFILE | `P10` |
| obj#300 | SERIE | `P27` |
| obj#300 | STARTNUMBER | `P28` |
| obj#306 | ASSEMBLYSERIE | `P29` |
| obj#306 | ASSEMBLYSTARTNUMBER | `P30` |
| obj#306 | CREATION | `if (P57=="4") \|\| (P57=="5") then P56 else P55 endif` |
| obj#306 | MATERIAL | `P35` |
| obj#306 | NAME | `P40` |
| obj#306 | PROFILE | `P10` |
| obj#306 | SERIE | `P27` |
| obj#306 | STARTNUMBER | `P28` |
| obj#312 | ASSEMBLYSERIE | `P25` |
| obj#312 | ASSEMBLYSTARTNUMBER | `P26` |
| obj#312 | CREATION | `if (P57=="4") \|\| (P57=="5") then P56 else P55 endif` |
| obj#312 | MATERIAL | `P34` |
| obj#312 | NAME | `P39` |
| obj#312 | PROFILE | `P9` |
| obj#312 | SERIE | `P23` |
| obj#312 | STARTNUMBER | `P24` |
| obj#318 | ASSEMBLYSERIE | `P25` |
| obj#318 | ASSEMBLYSTARTNUMBER | `P26` |
| obj#318 | CREATION | `if (P57=="4") \|\| (P57=="5") then P56 else P55 endif` |
| obj#318 | MATERIAL | `P34` |
| obj#318 | NAME | `P39` |
| obj#318 | PROFILE | `P9` |
| obj#318 | SERIE | `P23` |
| obj#318 | STARTNUMBER | `P24` |
| obj#324 | ASSEMBLYSERIE | `P29` |
| obj#324 | ASSEMBLYSTARTNUMBER | `P30` |
| obj#324 | CREATION | `if (P57=="4") \|\| (P57=="5") then P56 else P55 endif` |
| obj#324 | MATERIAL | `P35` |
| obj#324 | NAME | `P40` |
| obj#324 | PROFILE | `P10` |
| obj#324 | SERIE | `P27` |
| obj#324 | STARTNUMBER | `P28` |
| obj#330 | ASSEMBLYSERIE | `P21` |
| obj#330 | ASSEMBLYSTARTNUMBER | `P22` |
| obj#330 | CREATION | `if (P57=="3") \|\| (P57=="5") then P56 else P55 endif` |
| obj#330 | MATERIAL | `P33` |
| obj#330 | NAME | `P38` |
| obj#330 | PROFILE | `P8` |
| obj#330 | SERIE | `P19` |
| obj#330 | STARTNUMBER | `P20` |
| obj#337 | ASSEMBLYSERIE | `P21` |
| obj#337 | ASSEMBLYSTARTNUMBER | `P22` |
| obj#337 | MATERIAL | `P33` |
| obj#337 | NAME | `P38` |
| obj#337 | PROFILE | `P8` |
| obj#337 | SERIE | `P19` |
| obj#337 | STARTNUMBER | `P20` |
| obj#343 | ASSEMBLYSERIE | `P21` |
| obj#343 | ASSEMBLYSTARTNUMBER | `P22` |
| obj#343 | MATERIAL | `P33` |
| obj#343 | NAME | `P38` |
| obj#343 | PROFILE | `P8` |
| obj#343 | SERIE | `P19` |
| obj#343 | STARTNUMBER | `P20` |
| obj#349 | ASSEMBLYSERIE | `P21` |
| obj#349 | ASSEMBLYSTARTNUMBER | `P22` |
| obj#349 | CREATION | `if (P57=="4") \|\| (P57=="5") then P56 else P55 endif` |
| obj#349 | MATERIAL | `P33` |
| obj#349 | NAME | `P38` |
| obj#349 | PROFILE | `P8` |
| obj#349 | SERIE | `P19` |
| obj#349 | STARTNUMBER | `P20` |
| obj#355 | ASSEMBLYSERIE | `P21` |
| obj#355 | ASSEMBLYSTARTNUMBER | `P22` |
| obj#355 | CREATION | `if (P57=="4") \|\| (P57=="5") then P56 else P55 endif` |
| obj#355 | MATERIAL | `P33` |
| obj#355 | NAME | `P38` |
| obj#355 | PROFILE | `P8` |
| obj#355 | SERIE | `P19` |
| obj#355 | STARTNUMBER | `P20` |
| obj#422 | BOLT_GROUP_DISTY | `P64` |
| obj#422 | DX1 | `P66` |
| obj#422 | SIZE | `P58_diameter` |
| obj#422 | SLOTTED_HOLE_X | `P60` |
| obj#422 | SLOTTED_HOLE_Y | `P61` |
| obj#422 | STANDARD | `P58_screwdin` |
| obj#422 | TOLERANCE | `P59` |

## Preset `standard.p_NEC_EQUIP_Det1`
```
joint_attributes.saveas_file "1"
joint_attributes.get_menu "< ExternalDesign >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0;0.0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0;0.0"
joint_attributes.rz_spring "0;0.0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0;0.0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0;0.0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0;0.0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0;0.0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0;0.0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ad_root ""
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
joint_attributes.P67 "Fixed hole"
joint_attributes.P66 80.000000
joint_attributes.P65 600.000000
joint_attributes.P62 2
joint_attributes.P61 10.000000
joint_attributes.P60 0.000000
joint_attributes.P59 2.000000
joint_attributes.P58_screwdin "T.S.B"
joint_attributes.P58_diameter 20.000000
joint_attributes.P46 30.000000
joint_attributes.P45 30.000000
joint_attributes.P44 3
joint_attributes.P43 30.000000
joint_attributes.P42 30.000000
joint_attributes.P41 3
joint_attributes.P40 "PLATE"
joint_attributes.P39 "PLATE"
joint_attributes.P38 "PLATE"
joint_attributes.P37 "PLATE"
joint_attributes.P36 "PLATE"
joint_attributes.P35 "SS400"
joint_attributes.P34 "SS400"
joint_attributes.P33 "SS400"
joint_attributes.P32 "SS400"
joint_attributes.P31 "SS400"
joint_attributes.P30 "1"
joint_attributes.P29 "PL"
joint_attributes.P28 "1"
joint_attributes.P27 "PL"
joint_attributes.P26 "1"
joint_attributes.P25 "PL"
joint_attributes.P24 "1"
joint_attributes.P23 "PL"
joint_attributes.P22 "1"
joint_attributes.P21 "PL"
joint_attributes.P20 "1"
joint_attributes.P19 "PL"
joint_attributes.P18 "1"
joint_attributes.P17 "PL"
joint_attributes.P16 "1"
joint_attributes.P15 "PL"
joint_attributes.P14 "1"
joint_attributes.P13 "PL"
joint_attributes.P12 "1"
joint_attributes.P11 "PL"
joint_attributes.P5 10.000000
joint_attributes.P4 10.000000
joint_attributes.P3 10.000000
joint_attributes.P2 10.000000
joint_attributes.P1 10.000000
joint_attributes.P57 5
joint_attributes.P54 200.000000
joint_attributes.P53 200.000000
joint_attributes.P52 20.000000
joint_attributes.P51 20.000000
joint_attributes.P50 250.000000
joint_attributes.P49 50.000000
joint_attributes.P48 50.000000
joint_attributes.P47 1000.000000
```

## Preset `standard.p_NEC_EQUIP_Det1`
```
joint_attributes.saveas_file "1"
joint_attributes.get_menu "< ExternalDesign >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0;0.0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0;0.0"
joint_attributes.rz_spring "0;0.0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0;0.0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0;0.0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0;0.0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0;0.0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0;0.0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ad_root ""
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
joint_attributes.P67 "Fixed hole"
joint_attributes.P66 80.000000
joint_attributes.P65 600.000000
joint_attributes.P62 2
joint_attributes.P61 10.000000
joint_attributes.P60 0.000000
joint_attributes.P59 2.000000
joint_attributes.P58_screwdin "T.S.B"
joint_attributes.P58_diameter 20.000000
joint_attributes.P46 30.000000
joint_attributes.P45 30.000000
joint_attributes.P44 3
joint_attributes.P43 30.000000
joint_attributes.P42 30.000000
joint_attributes.P41 3
joint_attributes.P40 "PLATE"
joint_attributes.P39 "PLATE"
joint_attributes.P38 "PLATE"
joint_attributes.P37 "PLATE"
joint_attributes.P36 "PLATE"
joint_attributes.P35 "SS400"
joint_attributes.P34 "SS400"
joint_attributes.P33 "SS400"
joint_attributes.P32 "SS400"
joint_attributes.P31 "SS400"
joint_attributes.P30 "1"
joint_attributes.P29 "PL"
joint_attributes.P28 "1"
joint_attributes.P27 "PL"
joint_attributes.P26 "1"
joint_attributes.P25 "PL"
joint_attributes.P24 "1"
joint_attributes.P23 "PL"
joint_attributes.P22 "1"
joint_attributes.P21 "PL"
joint_attributes.P20 "1"
joint_attributes.P19 "PL"
joint_attributes.P18 "1"
joint_attributes.P17 "PL"
joint_attributes.P16 "1"
joint_attributes.P15 "PL"
joint_attributes.P14 "1"
joint_attributes.P13 "PL"
joint_attributes.P12 "1"
joint_attributes.P11 "PL"
joint_attributes.P5 10.000000
joint_attributes.P4 10.000000
joint_attributes.P3 10.000000
joint_attributes.P2 10.000000
joint_attributes.P1 10.000000
joint_attributes.P57 5
joint_attributes.P54 200.000000
joint_attributes.P53 200.000000
joint_attributes.P52 20.000000
joint_attributes.P51 20.000000
joint_attributes.P50 250.000000
joint_attributes.P49 50.000000
joint_attributes.P48 50.000000
joint_attributes.P47 1000.000000
```