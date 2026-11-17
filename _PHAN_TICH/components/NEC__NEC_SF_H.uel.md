# NEC_STIFFENER

- File: `NEC\NEC_SF_H.uel`  (67,418 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	NEC_STIFFENER_H	All`
- Dialog parameters: 0 | Variables (P/custom): 60 | Distance vars (D): 25 | Parts with bound profile: 7
- Lookup files (fVF): -
- Functions used: fMarketSize, fP, if, int
- Dialog bitmaps: 6 | Saved presets: standard.p_NEC_STIFFENER_H, standard.p_NEC_STIFFENER_H

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "NEC_STIFFENER")
   {
       tab_page("", " Pictures ", 1)
       {

           picture("NEC_LOGO",8,2,440,600)
           picture("NEC3",64,80,175,50)
           picture("NEC4",100,50,175,370)
           parameter("", "P49", distance, number, 200,25,60)
           parameter("", "P46", distance, number, 110,70,60)
           parameter("", "P47", distance, number, 430,65,60)
           parameter("", "P48", distance, number, 430,100,60)
           parameter("", "P53", distance, number, 390,170,60)
           parameter("", "P45", distance, number, 110,240,60)
           parameter("", "P50", distance, number, 310,285,60)
           parameter("", "P12", distance, number, 330,340,80)
           parameter("", "P42", distance, number, 467,340,60)
           parameter("", "P43", distance, number, 210,340,60)
           parameter("", "P44", distance, number, 600,423,60)
           attribute("플레이트", "중앙 플레이트 생성 유무", label,"%s",  none, none, "0.0", "0.0",30,600)
           parameter("", "P51", YesNo, number, 300,600,80)
           attribute("스티프너", "중앙 스티프너 생성 유무", label,"%s",  none, none, "0.0", "0.0",30,625)
           parameter("", "P52", YesNo, number, 300,625,80)
           


       }
       tab_page("", " Plates ", 2)
       {
           picture("NEC1",80,100,200,250)
           attribute("1.PLATE", "1.PLATE", label,"%s",  none, none, "0.0", "0.0",30,50)
           attribute("2.PLATE", "2.PLATE", label,"%s",  none, none, "0.0", "0.0",30,100)
           attribute("3.PLATE", "3.PLATE", label,"%s",  none, none, "0.0", "0.0",30,150)
           attribute("t", "t", label,"%s",  none, none, "0.0", "0.0",160,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",350,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",550,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",700,25)
           attribute("Slot hole", "Name", label,"%s",  none, none, "0.0", "0.0",700,25)
           
           parameter("", "P6", string, text, 150,50,50)
           parameter("", "P7", string, text, 150,100,50)
           parameter("", "P8", string, text, 150,150,50)
           parameter("", "P14", string, text, 350,50,50)
           parameter("", "P15", string, text, 430,50,40)
           parameter("", "P16", string, text, 350,75,50)
           parameter("", "P17", string, text, 430,75,40)
           parameter("", "P19", string, text, 350,100,50)
           parameter("", "P20", string, text, 430,100,40)
           parameter("", "P21", string, text, 350,125,50)
           parameter("", "P22", string, text, 430,125,40)
           parameter("", "P24", string, text, 350,150,50)
           parameter("", "P25", string, text, 430,150,40)
           parameter("", "P26", string, text, 350,175,50)
           parameter("", "P27", string, text, 430,175,40)
           parameter("", "P4", material, text, 550,50,90)
           parameter("", "P3", material, text, 550,100,90)
           parameter("", "P5", material, text, 550,150,90)
           parameter("", "P18", string, text, 700,50,120)
           parameter("", "P23", string, text, 700,100,120)
           parameter("", "P28", string, text, 700,150,120)
           parameter("", "P32", chamfer_type, number, 460,250,100)
           parameter("", "P33", distance, number, 460,280,50)
           parameter("", "P34", distance, number, 460,305,50)
           parameter("", "P29", chamfer_type, number, 460,430,100)
           parameter("", "P30", distance, number, 460,460,50)
           parameter("", "P31", string, text, 460,485,50)
           parameter("", "P38", chamfer_type, number, 120,250,100)
           parameter("", "P39", distance, number, 120,280,50)
           parameter("", "P40", distance, number, 120,305,50)
           parameter("", "P35", chamfer_type, number, 120,430,100)
           parameter("", "P36", distance, number, 120,460,50)
           parameter("", "P37", distance, number, 120,485,50)



       }
       tab_page("", " Bolts", 3)
       {
           picture("NEC_SLOT",10,5,450,50)
           attribute("size", "Bolt size", label,"%s",  none, none, "0.0", "0.0",30,50)
           attribute("standard", "Bolt standard", label,"%s",  none, none, "0.0", "0.0",30,75)
           attribute("Tolerance", "Tolerance", label,"%s",  none, none, "0.0", "0.0",30,100)
           parameter("", "P56_diameter", bolt_size, number, 210,50,80)
           parameter("", "P56_screwdin", bolt_standard, text, 210,75,80)
           parameter("", "P56_tolerance", string, text, 210,100,80)
           parameter("", "P56_longholeX", distance, number, 370,62,60)
           parameter("", "P56_longholeY", distance, number, 480,120,60)
           
           picture("NEC_BOLT",100,50,150,230)
           parameter("", "P56_number", integer, number, 270,200,50)
           parameter("", "P56_distance", distance, number, 360,200,80)
           parameter("", "P56_edge", distance, number, 600,345,80)

       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | 메인부재의 플랜지 두께를 참고 | Text | `int(fP(proT,"ID67C2A424-0000-D52D-3137-343038313036"))` *(formula)* |
| P5 | 2번 재질 | Material | `SS400` |
| P6 | 1번 두께 | Text | `9` |
| P7 | 2번 두께 | Text | `9` |
| P8 | 3번 두께 | Text | `` |
| P9 | 1번 두께 공식 | Profile | `"PL"+P6` *(formula)* |
| P10 | 2번 두께 공식 | Profile | `"PL"+P7` *(formula)* |
| P11 | 3번 두께 공식 | Profile | `if (P8 != "") then ("PL"+int(P8)) else ("PL"+int(P2)) endif` *(formula)* |
| P12 | 3번 플레이트 길이 | Length | `300.00` |
| P13 | 1번 부재간격 조절 | Length | `(double(P12)*0.5)-(double(P6)*0.5)` *(formula)* |
| P14 | 1번 Part Prefix | Text | `` |
| P15 | 1번 Part Start number | Text | `` |
| P16 | 1번 Assembly Prefix | Text | `` |
| P17 | 1번 Assembly Start number | Text | `` |
| P18 | 1번 이름 | Text | `` |
| P19 | 2번 Part Prefix | Text | `` |
| P20 | 2번 Part Start number | 18 | `` |
| P21 | 2번 Assembly Prefix | Text | `` |
| P22 | 2번 Assembly Start number | Text | `` |
| P23 | 2번 이름 | Text | `` |
| P24 | 3번 Part Prefix | Text | `` |
| P25 | 3번 Part Start number | Text | `` |
| P26 | 3번 Assembly Prefix | Text | `` |
| P27 | 3번 Assembly Start number | Text | `` |
| P28 | 3번 이름 | Text | `` |
| P29 | 1번 하단부 챔퍼 타입 | Chamfer | `` |
| P30 | 1번 하단부 챔퍼 크기 X | Length | `` |
| P31 | 1번 하단부 챔퍼 크기 Y | Length | `` |
| P32 | 1번 상단부 챔퍼 타입 | Chamfer | `` |
| P33 | 1번 상단부 챔퍼 크기 X | Length | `` |
| P34 | 1번 상단부 챔퍼 크기 Y | Length | `` |
| P35 | 2번 하단부 챔퍼 타입 | Chamfer | `` |
| P36 | 2번 하단부 챔퍼 크기 X | Length | `` |
| P37 | 2번 하단부 챔퍼 크기 Y | Length | `` |
| P38 | 2번 상단부 챔퍼 타입 | Chamfer | `` |
| P39 | 2번 상단부 챔퍼 크기 X | Length | `` |
| P40 | 2번 상단부 챔퍼 크기 Y | Length | `` |
| P41 | P42 구하는 공식 | Length | `(P12*0.5)+P42` *(formula)* |
| P42 | 3번 우측 길이 | Length | `50.00` |
| P43 | 3번 좌측 길이 | Length | `50.00` |
| P44 | 3번 높이 | Length | `80.00` |
| P45 | 1번 하단부 갭 | Length | `0.00` |
| P46 | 2번 상단부 갭 | Length | `0.00` |
| P47 | 3번 수평 갭 | Length | `0.00` |
| P48 | 1번 부재 꺽이는 높이 | Length | `50.00` |
| P49 | 2번 엣지거리 외곽 | Length | `0.00` |
| P50 | 1번 부재 하단부 갭 | Length | `0.00` |
| P51 | 1번 3개 옵션 | Yes/No | `1` |
| P52 | 2번 3개 옵션 | Yes/No | `1` |
| P53 | 1번 엣지거 | Length | `0.00` |
| P56_cal | 볼트 갯수 구하는 공식 | Integer | `ceil(fabs(P56_number-1.9))` *(formula)* |
| P56_diameter | bolt size | 6 | `20.00` |
| P56_distance | 볼트 간격 | Length | `80.00` |
| P56_distancelist | 볼트 그룹 거리 Y | 9 | `P56_cal+"*"+P56_distance` *(formula)* |
| P56_edge | 볼트 엣지 | Length | `25.00` |
| P56_longholeX | slot hole X | Length | `0.00` |
| P56_longholeY | slot hole Y | Length | `0.00` |
| P56_number | 볼트 개수 | Integer | `2` |
| P56_screwdin | bolt standard | 7 | `` |
| P56_tolerance | bolt tolerance | Text | `` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.Plane.하단 플랜지 인사이드 | `-P45` |
| D2 | D2.Plane.상단 플랜지 인사이드 | `P46` |
| D3 | D3.Plane.Extrema behind plane | `P47` |
| D4 | D4.Plane.Extrema top plane | `P48` |
| D5 | D5.Plane.빔의 엣지면 우측 | `P49` |
| D6 | D6.Plane.빔의 엣지면 좌측 | `-P50` |
| D7 | D7.Plane.웨브 좌측 | `` |
| D8 | D8.Plane.웨브 우측 | `` |
| D9 | D9.Plane.수평으로 튀어나오는 길이면 | `P44` |
| D10 | D10.Plane.빔의 상단면 | `` |
| D11 | D11.Plane.중심에서 우측거리 | `P12*0.5` |
| D12 | D12.Plane.중심에서 좌측거리 | `P12*0.5` |
| D13 | D13.Plane.상단 플레이트 끝에서 중심 좌측 | `(P12*0.5)+P43` |
| D14 | D14.Plane.상단 플레이트 끝에서 중심 우측 | `(P12*0.5)+P42` |
| D15 | D15.Plane.웨브에 직교되는 지점에서 볼트 바인드 | `` |
| D16 | D16.Plane.YZ plane | `P13` |
| D17 | D17.Plane.YZ plane | `P13` |
| D18 | D18.Plane.상단플렌지 엣지에 바인드 | `` |
| D19 | D19..BEAM | `P44-P53` |
| D20 | D20.Plane.XY plane | `` |
| D21 | D21..BEAM | `P44-P53` |
| D22 | D22..BEAM | `P44-P53` |
| D23 | D23..BEAM | `P44-P53` |
| D24 | D24..BEAM | `P44-P53` |
| D25 | D25..BEAM | `P44-P53` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#208 | ASSEMBLYSERIE | `P16` |
| obj#208 | ASSEMBLYSTARTNUMBER | `P17` |
| obj#208 | MATERIAL | `P4` |
| obj#208 | NAME | `P18` |
| obj#208 | PROFILE | `P9` |
| obj#208 | SERIE | `P14` |
| obj#208 | STARTNUMBER | `P15` |
| obj#221 | ASSEMBLYSERIE | `P21` |
| obj#221 | ASSEMBLYSTARTNUMBER | `P22` |
| obj#221 | MATERIAL | `P5` |
| obj#221 | NAME | `P23` |
| obj#221 | PROFILE | `P10` |
| obj#221 | SERIE | `P19` |
| obj#221 | STARTNUMBER | `P20` |
| obj#231 | ASSEMBLYSERIE | `P16` |
| obj#231 | ASSEMBLYSTARTNUMBER | `P17` |
| obj#231 | CREATION | `P51` |
| obj#231 | MATERIAL | `P4` |
| obj#231 | NAME | `P18` |
| obj#231 | PROFILE | `P9` |
| obj#231 | SERIE | `P14` |
| obj#231 | STARTNUMBER | `P15` |
| obj#239 | ASSEMBLYSERIE | `P21` |
| obj#239 | ASSEMBLYSTARTNUMBER | `P22` |
| obj#239 | CREATION | `P52` |
| obj#239 | MATERIAL | `P5` |
| obj#239 | NAME | `P23` |
| obj#239 | PROFILE | `P10` |
| obj#239 | SERIE | `P19` |
| obj#239 | STARTNUMBER | `P20` |
| obj#247 | ASSEMBLYSERIE | `P26` |
| obj#247 | ASSEMBLYSTARTNUMBER | `P27` |
| obj#247 | MATERIAL | `P3` |
| obj#247 | NAME | `P28` |
| obj#247 | PROFILE | `P11` |
| obj#247 | STARTNUMBER | `P15` |
| obj#258 | ASSEMBLYSERIE | `P16` |
| obj#258 | ASSEMBLYSTARTNUMBER | `P17` |
| obj#258 | MATERIAL | `P4` |
| obj#258 | NAME | `P18` |
| obj#258 | PROFILE | `P9` |
| obj#258 | SERIE | `P14` |
| obj#258 | STARTNUMBER | `P15` |
| obj#266 | ASSEMBLYSERIE | `P21` |
| obj#266 | ASSEMBLYSTARTNUMBER | `P22` |
| obj#266 | MATERIAL | `P5` |
| obj#266 | NAME | `P23` |
| obj#266 | PROFILE | `P10` |
| obj#266 | SERIE | `P19` |
| obj#266 | STARTNUMBER | `P20` |
| obj#691 | BOLT_GROUP_DISTY | `P56_distancelist` |
| obj#691 | DX1 | `P56_edge` |
| obj#691 | SIZE | `P56_diameter` |
| obj#691 | SLOTTED_HOLE_X | `P56_longholeX` |
| obj#691 | SLOTTED_HOLE_Y | `P56_longholeY` |
| obj#691 | STANDARD | `P56_screwdin` |
| obj#691 | TOLERANCE | `P56_tolerance` |

## Preset `standard.p_NEC_STIFFENER_H`
```
joint_attributes.saveas_file "NEC_standard"
joint_attributes.get_menu "< ExternalDesign >"
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
joint_attributes.BottomRight -2147483648
joint_attributes.BottomMiddle -2147483648
joint_attributes.BottomLeft -2147483648
joint_attributes.MiddleRight -2147483648
joint_attributes.MiddleLeft -2147483648
joint_attributes.TopRight -2147483648
joint_attributes.TopMiddle -2147483648
joint_attributes.TopLeft -2147483648
joint_attributes.UpMiddleRight -2147483648
joint_attributes.UpMiddleMiddle -2147483648
joint_attributes.UpMiddleLeft -2147483648
joint_attributes.MiddleMiddle -2147483648
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P56_edge 25.000000
joint_attributes.P56_longholeX 0.000000
joint_attributes.P56_diameter 20.000000
joint_attributes.P37 10.000000
joint_attributes.P36 10.000000
joint_attributes.P35 1
joint_attributes.P40 10.000000
joint_attributes.P39 10.000000
joint_attributes.P38 1
joint_attributes.P31 "10"
joint_attributes.P30 10.000000
joint_attributes.P29 1
joint_attributes.P34 10.000000
joint_attributes.P33 10.000000
joint_attributes.P32 1
joint_attributes.P28 "PLATE"
joint_attributes.P23 "PLATE"
joint_attributes.P18 "PLATE"
joint_attributes.P5 "SS400"
joint_attributes.P3 "SS400"
joint_attributes.P4 "SS400"
joint_attributes.P27 "1"
joint_attributes.P26 "PL"
joint_attributes.P25 "1"
joint_attributes.P24 "PL"
joint_attributes.P22 "1"
joint_attributes.P21 "PL"
joint_attributes.P20 "1"
joint_attributes.P19 "PL"
joint_attributes.P17 "1"
joint_attributes.P16 "PL"
joint_attributes.P15 "1"
joint_attributes.P14 "PL"
joint_attributes.P7 "12"
joint_attributes.P6 "12"
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P44 70.000000
joint_attributes.P43 50.000000
joint_attributes.P42 50.000000
joint_attributes.P12 200.000000
joint_attributes.P50 0.000000
joint_attributes.P45 0.000000
joint_attributes.P53 0.000000
joint_attributes.P48 80.000000
joint_attributes.P47 0.000000
joint_attributes.P46 0.000000
joint_attributes.P49 0.000000
```

## Preset `standard.p_NEC_STIFFENER_H`
```
joint_attributes.saveas_file "NEC_standard"
joint_attributes.get_menu "< ExternalDesign >"
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
joint_attributes.BottomRight -2147483648
joint_attributes.BottomMiddle -2147483648
joint_attributes.BottomLeft -2147483648
joint_attributes.MiddleRight -2147483648
joint_attributes.MiddleLeft -2147483648
joint_attributes.TopRight -2147483648
joint_attributes.TopMiddle -2147483648
joint_attributes.TopLeft -2147483648
joint_attributes.UpMiddleRight -2147483648
joint_attributes.UpMiddleMiddle -2147483648
joint_attributes.UpMiddleLeft -2147483648
joint_attributes.MiddleMiddle -2147483648
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P56_edge 25.000000
joint_attributes.P56_longholeX 0.000000
joint_attributes.P56_diameter 20.000000
joint_attributes.P37 10.000000
joint_attributes.P36 10.000000
joint_attributes.P35 1
joint_attributes.P40 10.000000
joint_attributes.P39 10.000000
joint_attributes.P38 1
joint_attributes.P31 "10"
joint_attributes.P30 10.000000
joint_attributes.P29 1
joint_attributes.P34 10.000000
joint_attributes.P33 10.000000
joint_attributes.P32 1
joint_attributes.P28 "PLATE"
joint_attributes.P23 "PLATE"
joint_attributes.P18 "PLATE"
joint_attributes.P5 "SS400"
joint_attributes.P3 "SS400"
joint_attributes.P4 "SS400"
joint_attributes.P27 "1"
joint_attributes.P26 "PL"
joint_attributes.P25 "1"
joint_attributes.P24 "PL"
joint_attributes.P22 "1"
joint_attributes.P21 "PL"
joint_attributes.P20 "1"
joint_attributes.P19 "PL"
joint_attributes.P17 "1"
joint_attributes.P16 "PL"
joint_attributes.P15 "1"
joint_attributes.P14 "PL"
joint_attributes.P7 "12"
joint_attributes.P6 "12"
joint_attributes.P52 1
joint_attributes.P51 1
joint_attributes.P44 70.000000
joint_attributes.P43 50.000000
joint_attributes.P42 50.000000
joint_attributes.P12 200.000000
joint_attributes.P50 0.000000
joint_attributes.P45 0.000000
joint_attributes.P53 0.000000
joint_attributes.P48 80.000000
joint_attributes.P47 0.000000
joint_attributes.P46 0.000000
joint_attributes.P49 0.000000
```