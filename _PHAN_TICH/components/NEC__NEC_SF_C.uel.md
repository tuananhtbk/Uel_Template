# NEC_STIFFENER_C

- File: `NEC\NEC_SF_C.uel`  (55,602 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	NEC_STIFFENER_C	All`
- Dialog parameters: 0 | Variables (P/custom): 42 | Distance vars (D): 22 | Parts with bound profile: 4
- Lookup files (fVF): -
- Functions used: fMarketSize, fP, if
- Dialog bitmaps: 6 | Saved presets: standard.p_NEC_STIFFENER_C, standard.p_NEC_STIFFENER_C

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "NEC_STIFFENER_C")
   {
       tab_page("", " Pictures ", 1)
       {
           picture("NEC_LOGO",8,2,440,570)
           picture("NEC5",64,80,175,50)
           picture("NEC6",100,50,175,370)
           parameter("", "P25", distance, number, 460,65,60)
           parameter("", "P26", distance, number, 460,100,60)
           parameter("", "P27", distance, number, 380,190,60)
           parameter("", "P30", distance, number, 300,285,60)
           parameter("", "P32", distance, number, 207,340,60)
           parameter("", "P31", distance, number, 328,340,80)
           parameter("", "P33", distance, number, 469,340,60)
           parameter("", "P28", distance, number, 600,430,60)
           attribute("플레이트", "중앙 플레이트 생성 유무", label,"%s",  none, none, "0.0", "0.0",30,600)
           parameter("", "P34", YesNo, number, 300,600,80)

           
           
       }
       tab_page("", " Plates ", 2)
       {
           picture("NEC8",80,100,200,250)
           attribute("1.PLATE", "1.PLATE", label,"%s",  none, none, "0.0", "0.0",30,50)
           attribute("2.PLATE", "2.PLATE", label,"%s",  none, none, "0.0", "0.0",30,100)
           attribute("t", "t", label,"%s",  none, none, "0.0", "0.0",160,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",350,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",550,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",700,25)
           attribute("Slot hole", "Name", label,"%s",  none, none, "0.0", "0.0",700,25)
           
           
           parameter("", "P1", string, text, 150,50,50)
           parameter("", "P2", string, text, 150,100,50)
           parameter("", "P7", string, text, 350,50,50)
           parameter("", "P8", string, text, 430,50,40)
           parameter("", "P9", string, text, 350,75,50)
           parameter("", "P10", string, text, 430,75,40)
           parameter("", "P11", string, text, 350,100,50)
           parameter("", "P12", string, text, 430,100,40)
           parameter("", "P13", string, text, 350,125,50)
           parameter("", "P14", string, text, 430,125,40)
           parameter("", "P15", material, text, 550,50,90)
           parameter("", "P16", material, text, 550,100,90)
           parameter("", "P17", string, text, 700,50,120)
           parameter("", "P18", string, text, 700,100,120)
           
           parameter("", "P19", chamfer_type, number, 470,270,100)
           parameter("", "P20", distance, number, 470,300,50)
           parameter("", "P21", distance, number, 470,325,50)
           parameter("", "P22", chamfer_type, number, 470,430,100)
           parameter("", "P23", distance, number, 470,460,50)
           parameter("", "P24", distance, number, 470,485,50)
           
           
           

       }
       tab_page("", " Bolts", 3)
       {
           picture("NEC_SLOT",10,5,450,50)
           attribute("size", "Bolt size", label,"%s",  none, none, "0.0", "0.0",30,50)
           attribute("standard", "Bolt standard", label,"%s",  none, none, "0.0", "0.0",30,75)
           attribute("Tolerance", "Tolerance", label,"%s",  none, none, "0.0", "0.0",30,100)
           
           parameter("", "P35_diameter", bolt_size, number, 210,50,80)
           parameter("", "P35_screwdin", bolt_standard, text, 210,75,80)
           parameter("", "P36", distance, number, 210,100,80)
           parameter("", "P37", distance, number, 370,62,60)
           parameter("", "P38", distance, number, 480,120,60)
           
           
           
           
           picture("NEC7",100,50,150,230)
           parameter("", "P40", integer, number, 270,200,50)
           parameter("", "P43", distance, number, 360,200,80)
           parameter("", "P39", distance, number, 600,345,80)
           
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | 1번 플레이트 두께 | Text | `9` |
| P2 | 2번 플레이트 두께 | Text | `` |
| P3 | 1번 플레이트 두께 공식 | Profile | `"PL"+P1` *(formula)* |
| P4 | 2번 플레이트 두께 공식 | Profile | `if (P2 !="") then ("PL"+double(P2)) else ("PL"+double(P5)) endif` *(formula)* |
| P5 | MarketSize 사용공식 | Text | `double(fMarketSize("SS400",P6,0))` *(formula)* |
| P6 | 메인 부재 플렌지 두께 참고 | Text | `double(fP(proT,"ID67C2A424-0000-D399-3137-343038313036"))` *(formula)* |
| P7 | 1번 플레이트 Part prefix | Text | `` |
| P8 | 1번 플레이트 Part Start number | Text | `` |
| P9 | 1번 플레이트 Assembly prefix | Text | `` |
| P10 | 1번 플레이트 Assembly Start nu | Text | `` |
| P11 | 2번 플레이트 Part prefix | Text | `` |
| P12 | 2번 플레이트 Part Start number | Text | `` |
| P13 | 2번 플레이트 Assembly prefix | Text | `` |
| P16 | 2번 플레이트 재질 | Material | `` |
| P17 | 1번 플레이트 이름 | Text | `` |
| P18 | 2번 플레이트 이름 | Text | `` |
| P19 | 상단 챔퍼 타입 | Chamfer | `` |
| P20 | 상단 챔퍼 X | Length | `` |
| P21 | 상단 챔퍼 Y | Length | `` |
| P22 | 하단 챔퍼 타입 | Chamfer | `` |
| P23 | 하단 챔퍼 X | Length | `` |
| P24 | 하단 챔퍼 Y | Length | `` |
| P25 | 2번 하단부에서 떨어진 거리 | Length | `` |
| P26 | 1번 튀어나온부분의 높이 | Length | `50.00` |
| P27 | 1번 부재 끝부부에서 엣지거리 | Length | `` |
| P28 | 2번 플레이트 높이 | Length | `50.00` |
| P29 | P27번 공식 | Length | `P28-P27` *(formula)* |
| P30 | 1번 하단부에서 엣지까지 | Length | `` |
| P31 | 2번 플레이트 너비 | Length | `200.00` |
| P32 | 2번 우측 날개 길이 | Length | `50.00` |
| P33 | 2번 좌측 날개 길이 | Length | `50.00` |
| P34 | 1번 중앙 플레이트 생성 유무 | Yes/No | `1` |
| P35_diameter | 볼트 사이즈 | 6 | `20.00` |
| P35_screwdin | 볼트 스탠다드 | 7 | `` |
| P36 | tolerance | Length | `` |
| P37 | sholt hole X | Length | `` |
| P38 | sholt hole Y | Length | `` |
| P39 | bolt edge | Length | `30.00` |
| P40 | bolt number | Integer | `2` |
| P41 | bolt distance 계산공식 | 9 | `P42+"*"+P43` *(formula)* |
| P42 | bolt number 계산공식 | Integer | `ceil(fabs(P40-1.9))` *(formula)* |
| P43 | bolt distance | Length | `60.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.Plane.Web inner left plane | `` |
| D2 | D2.Plane.Extrema bottom plane | `P30` |
| D3 | D3.Plane.2번 플레이트 높이 바인드 | `P28` |
| D4 | D4.Plane.Extrema behind plane | `` |
| D5 | D5.Plane.2번의 하단에 바인드 | `P25` |
| D6 | D6.Plane.메인부재의 하단 플랜지 인사이드 바인드 | `` |
| D7 | D7.Plane.YZ plane | `P31*0.5` |
| D8 | D8.Plane.Extrema bottom plane | `` |
| D9 | D9.Plane.YZ plane | `P31*0.5` |
| D10 | D10.Plane.YZ plane | `(P31*0.5)+P32` |
| D11 | D11.Plane.1번 우측 플레이드 를 센터에 바인드 | `(P31*0.5)-(double(P1)*0.5)` |
| D12 | D12.Plane.1번 좌측 플레이드 를 센터에 바인드 | `(P31*0.5)-(double(P1)*0.5)` |
| D13 | D13.PLATE.BEAM | `P28-P27` |
| D14 | D14.PLATE.BEAM | `P28-P27` |
| D15 | D15.PLATE.BEAM | `P28-P27` |
| D16 | D16.PLATE.BEAM | `P28-P27` |
| D17 | D17.PLATE.BEAM | `P28-P27` |
| D18 | D18.PLATE.BEAM | `P28-P27` |
| D19 | D19.Plane.YZ plane | `(P31*0.5)+P33` |
| D20 | D20.Plane.1번 튀어나온 높이를 바인드 | `P26` |
| D21 | D21.BOLT.BEAM | `` |
| D22 | D22.Plane.YZ plane | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#169 | ASSEMBLYSERIE | `P9` |
| obj#169 | ASSEMBLYSTARTNUMBER | `P10` |
| obj#169 | CREATION | `P34` |
| obj#169 | MATERIAL | `P15` |
| obj#169 | NAME | `P17` |
| obj#169 | PROFILE | `P3` |
| obj#169 | SERIE | `P7` |
| obj#169 | STARTNUMBER | `P8` |
| obj#182 | ASSEMBLYSERIE | `P13` |
| obj#182 | ASSEMBLYSTARTNUMBER | `P14` |
| obj#182 | MATERIAL | `P16` |
| obj#182 | NAME | `P18` |
| obj#182 | PROFILE | `P4` |
| obj#182 | SERIE | `P11` |
| obj#182 | STARTNUMBER | `P12` |
| obj#192 | ASSEMBLYSERIE | `P9` |
| obj#192 | ASSEMBLYSTARTNUMBER | `P10` |
| obj#192 | MATERIAL | `P15` |
| obj#192 | NAME | `P17` |
| obj#192 | PROFILE | `P3` |
| obj#192 | SERIE | `P7` |
| obj#192 | STARTNUMBER | `P8` |
| obj#199 | ASSEMBLYSERIE | `P9` |
| obj#199 | ASSEMBLYSTARTNUMBER | `P10` |
| obj#199 | MATERIAL | `P15` |
| obj#199 | NAME | `P17` |
| obj#199 | PROFILE | `P3` |
| obj#199 | SERIE | `P7` |
| obj#199 | STARTNUMBER | `P8` |
| obj#206 | BOLT_GROUP_DISTY | `P41` |
| obj#206 | DX1 | `P39` |
| obj#206 | SIZE | `P35_diameter` |
| obj#206 | SLOTTED_HOLE_X | `P37` |
| obj#206 | SLOTTED_HOLE_Y | `P38` |
| obj#206 | STANDARD | `P35_screwdin` |
| obj#206 | TOLERANCE | `P36` |

## Preset `standard.p_NEC_STIFFENER_C`
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
joint_attributes.P39 40.000000
joint_attributes.P43 60.000000
joint_attributes.P40 2
joint_attributes.P38 0.000000
joint_attributes.P37 0.000000
joint_attributes.P36 2.000000
joint_attributes.P35_diameter 20.000000
joint_attributes.P24 30.000000
joint_attributes.P23 30.000000
joint_attributes.P22 3
joint_attributes.P21 30.000000
joint_attributes.P20 30.000000
joint_attributes.P19 3
joint_attributes.P18 "PLATE"
joint_attributes.P17 "PLATE"
joint_attributes.P16 "SS400"
joint_attributes.P15 "SS400"
joint_attributes.P14 "1"
joint_attributes.P13 "PL"
joint_attributes.P12 "1"
joint_attributes.P11 "PL"
joint_attributes.P10 "1"
joint_attributes.P9 "PL"
joint_attributes.P8 "1"
joint_attributes.P7 "PL"
joint_attributes.P1 "12"
joint_attributes.P34 1
joint_attributes.P28 50.000000
joint_attributes.P33 50.000000
joint_attributes.P31 200.000000
joint_attributes.P32 50.000000
joint_attributes.P30 0.000000
joint_attributes.P27 0.000000
joint_attributes.P26 50.000000
joint_attributes.P25 0.000000
```

## Preset `standard.p_NEC_STIFFENER_C`
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
joint_attributes.P39 40.000000
joint_attributes.P43 60.000000
joint_attributes.P40 2
joint_attributes.P38 0.000000
joint_attributes.P37 0.000000
joint_attributes.P36 2.000000
joint_attributes.P35_diameter 20.000000
joint_attributes.P24 30.000000
joint_attributes.P23 30.000000
joint_attributes.P22 3
joint_attributes.P21 30.000000
joint_attributes.P20 30.000000
joint_attributes.P19 3
joint_attributes.P18 "PLATE"
joint_attributes.P17 "PLATE"
joint_attributes.P16 "SS400"
joint_attributes.P15 "SS400"
joint_attributes.P14 "1"
joint_attributes.P13 "PL"
joint_attributes.P12 "1"
joint_attributes.P11 "PL"
joint_attributes.P10 "1"
joint_attributes.P9 "PL"
joint_attributes.P8 "1"
joint_attributes.P7 "PL"
joint_attributes.P1 "12"
joint_attributes.P34 1
joint_attributes.P28 50.000000
joint_attributes.P33 50.000000
joint_attributes.P31 200.000000
joint_attributes.P32 50.000000
joint_attributes.P30 0.000000
joint_attributes.P27 0.000000
joint_attributes.P26 50.000000
joint_attributes.P25 0.000000
```