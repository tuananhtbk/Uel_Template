# NEC_EMBED

- File: `NEC\NEC_EMBED.uel`  (81,298 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	NEC_EMBED	All`
- Dialog parameters: 2 | Variables (P/custom): 91 | Distance vars (D): 82 | Parts with bound profile: 5
- Lookup files (fVF): -
- Functions used: fP, if, int, mid
- Dialog bitmaps: 9 | Saved presets: H294.p_NEC_EMBED, H900_300.p_NEC_EMBED, H294.p_NEC_EMBED, H900_300.p_NEC_EMBED, standard.p_NEC_EMBED

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "NEC_EMBED")
   {
       tab_page("", " Pictures ", 1)
       {
           picture("NEC_LOGO",8,2,450,630)
           picture("NEC_EMBED11",10,5,200,100)
           attribute(" ", " ", label,"%s",  none, none, "0.0", "0.0",0,650)
           parameter("", "P5", distance, number, 270,150,60)
           parameter("", "P10", distance, number, 440,105,60)
           parameter("", "P43", welding_site, number, 265,428,80)
           parameter("", "P11", distance, number, 380,480,60)
           parameter("", "P15", distance, number, 440,435,60)
           parameter("", "P14", distance, number, 545,390,60)
           parameter("", "P67", welding_site, number, 518,236,80)
       }
       tab_page("", " Plates ", 2)
       {
           attribute("", "1.E_Plate", label,"%s",  none, none, "0.0", "0.0",15,50)
           attribute("", "2.G_Plate", label,"%s",  none, none, "0.0", "0.0",15,75)
           attribute("", "3.Anchor", label,"%s",  none, none, "0.0", "0.0",15,100)
           attribute("", "t", label,"%s",  none, none, "0.0", "0.0",150,25)
           attribute("", "B", label,"%s",  none, none, "0.0", "0.0",230,25)
           attribute("", "H", label,"%s",  none, none, "0.0", "0.0",300,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",350,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",540,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",690,25)

           parameter("", "P6", distance, number, 130,50,50)
           parameter("", "P7", distance, number, 210,50,50)
           parameter("", "P8", distance, number, 290,50,50)        
           parameter("", "P59", string, text, 370,50,50)
           parameter("", "P60", string, text, 450,50,40)
           parameter("", "P61", material, text, 540,50,90)
           parameter("", "P62", string, text, 690,50,120)
           parameter("", "P46", distance, number, 130,75,50)
           parameter("", "P55", string, text, 370,75,50)
           parameter("", "P56", string, text, 450,75,40)
           parameter("", "P57", material, text, 540,75,90)
           parameter("", "P58", string, text, 690,75,120)
           parameter("", "P51", string, text, 370,100,50)
           parameter("", "P52", string, text, 450,100,40)
           parameter("", "P53", material, text, 540,100,90)
           parameter("", "P54", string, text, 690,100,120)
           parameter("", "P3", profile, text, 130,100,120)
           picture("NEC_EMBED_R",10,5,350,200)
           parameter("", "P45", distance, number, 395,265,60)
           attribute("P44", "", option, "%s", no, none, "0.0", "0.0", 100,200,150)
                {
                     value("NEC_EMBED_PLATE2.xbm",1)
                     value("NEC_EMBED_PLATE1.xbm",0)
                }
       }
       tab_page("", " Bolt ", 3)
       {
           	
          picture("NEC_EMBED12",10,5,150,180)
          picture("NEC_EMBED_SLOT1",10,5,495,17)
          picture("jnt_bpic_bolt_parts_in_modify",50,100,331,18)
		toggle_group(367, 120,
			"BLT_w1",-20,-20,"",
			"BLT_w2",  7,-20,"",
			"BLT_w3", 32,-20,"",
			"BLT_n1", 57,-20,"",
			"BLT_n2",102,-40,"",
			"BLT_bolt",102,-80,"")

           attribute("", "Bolt size", label,"%s",  none, none, "0.0", "0.0",30,25)
           attribute("", "Bolt standard", label,"%s",  none, none, "0.0", "0.0",30,50)
           attribute("", "Tolerance", label,"%s",  none, none, "0.0", "0.0",30,75)
           parameter("", "P24_diameter", bolt_size, number, 210,25,80)
           parameter("", "P24_screwdin", bolt_standard, text, 210,50,80)
           parameter("", "P65", bolt_type, number, 210,100,80)
           parameter("", "P26", distance, number, 210,75,80)
           parameter("", "P64", distance, number, 530,50,60)
           parameter("", "P66", distance, number, 620,100,60)
           parameter("", "P35", integer, number, 355,525,40)
           parameter("", "P37", distance, number, 435,525,60)
           attribute("SOLT_set", "", option, "%s", no, none, "0.0", "0.0", 620,125,80)
                {
                     value("Default",1)
                     value("No",0)
                     value("Plate",0)
                     value("Beam",0)
                     value("Both",0)
                }
           parameter("", "P18", distance, number, 320,165,60)
           parameter("", "P19", distance, number, 410,165,60)
           parameter("", "P12", distance, number, 460,290,60)
           parameter("", "P13", distance, number, 560,280,60)
           parameter("", "P20", integer, number, 460,350,40)
           parameter("", "P22", distance, number, 530,350,60)
       }

 tab_page("", " Anchor & Nail hole ", 6)
       {
           picture("NEC_EMBED3",10,5,500,200)
           picture("NEC_EMBED2",10,5,100,200)
           attribute("", "Nail Hole Size", label,"%s",  none, none, "0.0", "0.0",30,25)
           attribute("", "Nail Hole No.", label,"%s",  none, none, "0.0", "0.0",230,175)
           attribute("", "Anchor Size", label,"%s",  none, none, "0.0", "0.0",630,150)           
           parameter("", "P1", distance, number, 640,175,60)
           parameter("", "P9", distance, number, 470,287,60)
           parameter("", "P2", distance, number, 542,385,60)
           parameter("", "P34", distance, number, 240,25,80)
           parameter("", "P31", distance, number, 125,180,60)
           parameter("", "P4", distance, number, 350,240,60)
           parameter("", "P70", integer, number, 350,340,40)
           parameter("", "P71", distance, number,420,340,60)
           parameter("", "P25", integer, number, 240,200,60)
           parameter("", "P17", distance, number, 626,437,60)

       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | 앵커거리1 | Length | `200.00` |
| P2 | 앵커 우측 엣지 거리 | Length | `100.00` |
| P3 | 앵커 프로파일 | Profile | `D19` |
| P4 | 엥커 하단부 엣지 간격 | Length | `75.00` |
| P5 | 플레이트&부재 상단간격 | Length | `25.00` |
| P6 | 1.플레이트 두께 | Length | `20.00` |
| P7 | 1.플레이트 너비 | Length | `300.00` |
| P8 | 1.플레이트 높이 | Length | `1050.00` |
| P9 | 앵커 높이 | Length | `300.00` |
| P10 | GAP1 | Length | `15.00` |
| P11 | 2.GP 짜투리 간격 | Length | `20.00` |
| P12 | 볼트 상단 엣지 거리 | Length | `50.00` |
| P13 | 2.GP 플랜지 상단에서 거리 | Length | `125.00` |
| P14 | 2.GP  하단부 사선거리 | Length | `100.00` |
| P15 | GAP2 | Length | `10.00` |
| P16 | Parameter16 | Length | `fP(proT,"ID67C2A424-0000-BCE6-3137-343038313035")` *(formula)* |
| P17 | R | Length | `20.00` |
| P18 | 볼트 왼쪽 엣지 | Length | `50.00` |
| P19 | 볼트 오른쪽 엣지 | Length | `50.00` |
| P20 | 볼트 개수 | Integer | `11` |
| P21 | 볼트 개수 공식 | Integer | `ceil(fabs(P20-1.9))` *(formula)* |
| P22 | 볼트 피치 | Length | `65.00` |
| P23 | Parameter23 | 9 | `P21+"*"+P22` *(formula)* |
| P24_diameter | 볼트 사이즈 | 6 | `24.00` |
| P24_screwdin | 볼트 종류 | 7 | `T.S.B` |
| P25 | Nail hole 개수 | Integer | `8` |
| P26 | 톨러런스 | Length | `2.00` |
| P27 | Parameter27 | Length | `0.00` |
| P28 | Parameter28 | Integer | `ceil(fabs(P25*0.5)-1.9)` *(formula)* |
| P29 | Parameter29 | 9 | `P28+"*"+P30` *(formula)* |
| P30 | Parameter30 | Length | `int((P8-(P31*2))/P28)` *(formula)* |
| P31 | Nail hole 엣지 간격 | Length | `30.00` |
| P32 | Parameter32 | 9 | `1+"*"+(P7-(P31*2))` *(formula)* |
| P33_diameter | Parameter33 | 6 | `16.00` |
| P33_screwdin | --------------- | 7 | `16S` |
| P34 | Nail hole 톨러런스 | Length | `16.00` |
| P35 | 수평 볼트 개수 | Integer | `1` |
| P36 | Parameter36 | 9 | `int((P35-1))+"*"+P37` *(formula)* |
| P37 | 수평 볼트 피치 | Length | `60.00` |
| P38 | Parameter38 | Integer | `P35-1` *(formula)* |
| P39 | Parameter39 | 9 | `if P35==1 then 0 else (P35-1)+"*"+P37 endif` *(formula)* |
| P40 | Parameter40 | Length | `if P35==1 then 0 else (P35-1)*P37 endif` *(formula)* |
| P41 | Parameter41 | Length | `if P35==1 then 0 else ((P35-1)*P37*0.5) endif` *(formula)* |
| P42 | 앵커 두께 참조 | Length | `double(mid(P3,1,2))*0.5` *(formula)* |
| P43 | 1.부재와 2.GP 용접 옵션 | 12 | `0` |
| P44 | 2.GP 위치 옵션 | Integer | `1` |
| P45 | 빔컷 챔퍼 크기 | Length | `10.00` |
| P46 | 2.GP 두께 | Length | `20.00` |
| P47 | Parameter47 | Length | `(P46*0.5)+(fP(proS,"ID67C2A424-0000-BCE6-3137-343038313035")*0.5)` *(formula)* |
| P48 | Parameter48 | Length | `if P44==0 then P47 else -P47 endif` *(formula)* |
| P49 | Parameter49 | Length | `-P50` *(formula)* |
| P50 | Parameter49 | Length | `P47*2` *(formula)* |
| P51 | 3.앵커 파트 프리픽스 | Text | `P` |
| P52 | 3.앵커 파트 스타트 | Text | `1` |
| P53 | 3.앵커 재질 | Material | `SS400` |
| P54 | 3.앵커 이름 | Text | `` |
| P55 | 2.GP 파트 프리픽스 | Text | `PL` |
| P56 | 2.GP 파트 스타트 | Text | `1` |
| P57 | 2.GP 재질 | Material | `SS400` |
| P58 | 2.GP 이름 | Text | `PLATE` |
| P59 | 1. 파트 프리픽스 | Text | `PL` |
| P60 | 1. 파트 스타트 | Text | `1` |
| P61 | 1. 재질 | Material | `SS400` |
| P62 | 1. 이름 | Text | `PLATE` |
| P63 | ------------------- | 24 | `11000` |
| P64 | 슬롯 X | Length | `20.00` |
| P65 | site/workshop | 8 | `0` |
| P66 | 슬롯 Y | Length | `20.00` |
| P67 | Beam&GP: site/workshop | 12 | `1` |
| P70 | 앵커 개수 | Integer | `11` |
| P71 | 앵커 간격 | Length | `90.00` |
| P72 | Parameter72 | Integer | `P70-1` *(formula)* |
| BLT_bolt | Parameter68 | Integer | `1` |
| BLT_n1 | Parameter66 | Integer | `1` |
| BLT_n2 | Parameter67 | Integer | `0` |
| BLT_set | Parameter69 | Text | `if (BLT_bolt) then (int(BLT_w1)+""+int(BLT_w2)+""+int(BLT_w3)+""+int(BLT_n1)+""+int(BLT_n2)) else ("100000") endif` *(formula)* |
| BLT_w1 | Parameter63 | Integer | `0` |
| BLT_w2 | Parameter64 | Integer | `0` |
| BLT_w3 | Parameter65 | Integer | `1` |
| SLOT_1 | Parameter64 | Integer | `1` |
| SLOT_2 | Parameter65 | Integer | `1` |
| SLOT_3 | Parameter66 | Integer | `0` |
| SLOT_4 | Parameter67 | Integer | `0` |
| SLOT_5 | Parameter68 | Integer | `0` |
| SLOT_set | Parameter69 | Integer | `0` |
| SOLT_1 | SLOT_DEFAULT | 24 | `if SOLT_set==0 then 00000 else SOLT_2 endif` *(formula)* |
| SOLT_2 | SLOT_NO | 24 | `if SOLT_set==1 then 00000 else SOLT_3 endif` *(formula)* |
| SOLT_3 | SLOT_PLATE | 24 | `if SOLT_set==2 then 10000 else SOLT_4 endif` *(formula)* |
| SOLT_4 | SLOT_BEAM | 24 | `if SOLT_set==3 then 01000 else SOLT_5 endif` *(formula)* |
| SOLT_5 | SLOT_BOTH | 24 | `if SOLT_set==4 then 11000 else 00000 endif` *(formula)* |
| SOLT_set | 슬롯홀 옵션 | Integer | `0` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.ANC.BAR.PLATE | `P9+P42` |
| D2 | D2.ANC.BAR.PLATE | `P9+P42` |
| D3 | D3.ANC.BAR.PLATE | `P42` |
| D4 | D4.ANC.BAR.PLATE | `P42` |
| D5 | D5.ANC.BAR.PLATE | `P9+P42` |
| D6 | D6.ANC.BAR.PLATE | `P9+P42` |
| D7 | D7.ANC.BAR.NEC_EMBED | `P1*0.5` |
| D8 | D8.ANC.BAR.NEC_EMBED | `P1*0.5` |
| D9 | D9.ANC.BAR.NEC_EMBED | `P1*0.5` |
| D10 | D10.ANC.BAR.NEC_EMBED | `P1*0.5` |
| D11 | D11.ANC.BAR.NEC_EMBED | `D8+P2` |
| D12 | D12.ANC.BAR.NEC_EMBED | `D9+P2` |
| D13 | D13.ANC.BAR.PLATE | `P4` |
| D14 | D14.ANC.BAR.PLATE | `P4` |
| D15 | D15.ANC.BAR.PLATE | `P4` |
| D16 | D16.ANC.BAR.PLATE | `P4` |
| D17 | D17.ANC.BAR.PLATE | `P4` |
| D18 | D18.ANC.BAR.PLATE | `P4` |
| D19 | D19.PLATE.NEC_EMBED | `` |
| D20 | D20.PLATE.NEC_EMBED | `` |
| D21 | D21.PLATE.NEC_EMBED | `` |
| D22 | D22.PLATE.NEC_EMBED | `` |
| D23 | D23.PLATE.GIRDER | `P5` |
| D24 | D24.PLATE.GIRDER | `P5` |
| D25 | D25.PLATE.GIRDER | `P8-P5` |
| D26 | D26.PLATE.GIRDER | `P8-P5` |
| D27 | D27.PLATE.GIRDER | `P7*0.5` |
| D28 | D28.PLATE.GIRDER | `P7*0.5` |
| D29 | D29.PLATE.GIRDER | `P7*0.5` |
| D30 | D30.PLATE.GIRDER | `P7*0.5` |
| D31 | D31.FITTING.NEC_EMBED | `P10` |
| D32 | D32.FITTING.NEC_EMBED | `P10` |
| D33 | D33.FITTING.NEC_EMBED | `P10` |
| D34 | D34.PLATE.NEC_EMBED | `` |
| D35 | D35.PLATE.NEC_EMBED | `` |
| D36 | D36.PLATE.NEC_EMBED | `P10+P18+P19+P40` |
| D37 | D37.PLATE.NEC_EMBED | `P10+P18+P19+P40` |
| D38 | D38.PLATE.PLATE | `P11` |
| D39 | D39.PLATE.GIRDER | `fabs(P13-P12)` |
| D40 | D40.PLATE.GIRDER | `fabs(P13-P12)` |
| D41 | D41.PLATE.GIRDER | `` |
| D42 | D42.PLATE.GIRDER | `P14` |
| D43 | D43.PLATE.GIRDER | `P14` |
| D44 | D44.CUTPART.GIRDER | `` |
| D45 | D45.CUTPART.GIRDER | `` |
| D46 | D46.CUTPART.GIRDER | `` |
| D47 | D47.CUTPART.GIRDER | `` |
| D48 | D48.CUTPART.GIRDER | `` |
| D49 | D49.CUTPART.GIRDER | `` |
| D50 | D50.CUTPART.GIRDER | `` |
| D51 | D51.CUTPART.GIRDER | `` |
| D52 | D52.CUTPART.PLATE | `P15` |
| D53 | D53.CUTPART.PLATE | `P15` |
| D54 | D54.CUTPART.PLATE | `P15` |
| D55 | D55.CUTPART.PLATE | `P15` |
| D56 | D56.CUTPART.GIRDER | `` |
| D57 | D57.CUTPART.GIRDER | `` |
| D58 | D58.CUTPART.GIRDER | `` |
| D59 | D59.CUTPART.GIRDER | `` |
| D60 | D60.BOLT.PLATE | `` |
| D61 | D61.BOLT.GIRDER | `-P18` |
| D62 | D62.BOLT.GIRDER | `-P18` |
| D63 | D63.BOLT.GIRDER | `` |
| D64 | D64.BOLT.PLATE | `` |
| D65 | D65.BOLT.PLATE | `` |
| D66 | D66.BOLT.PLATE | `` |
| D67 | D67.BOLT.PLATE | `` |
| D68 | D68.BOLT.NEC_EMBED | `` |
| D69 | D69.BOLT.NEC_EMBED | `` |
| D70 | D70.PLATE.GIRDER | `P48` |
| D71 | D71.PLATE.GIRDER | `P48` |
| D72 | D72.PLATE.GIRDER | `P48` |
| D73 | D73.PLATE.GIRDER | `P48` |
| D74 | D74.PLATE.GIRDER | `P48` |
| D75 | D75.PLATE.GIRDER | `` |
| D76 | D76.PLATE.GIRDER | `` |
| D77 | D77.PLATE.GIRDER | `` |
| D78 | D78.PLATE.GIRDER | `` |
| D79 | D79.CUTPART.GIRDER | `` |
| D80 | D80.CUTPART.GIRDER | `` |
| D81 | D81.CUTPART.GIRDER | `` |
| D82 | D82.CUTPART.GIRDER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#313 | MATERIAL | `P61` |
| obj#313 | NAME | `P62` |
| obj#313 | PROFILE | `"PL"+P6` |
| obj#313 | SERIE | `P59` |
| obj#313 | STARTNUMBER | `P60` |
| obj#334 | MATERIAL | `P53` |
| obj#334 | NAME | `P54` |
| obj#334 | PROFILE | `P3` |
| obj#334 | SERIE | `P51` |
| obj#334 | STARTNUMBER | `P52` |
| obj#356 | PROFILE | `"PL"+P46` |
| obj#366 | CREATION | `if P44==1 then 0 else 1 endif` |
| obj#366 | PROFILE | `"BL"+P16` |
| obj#385 | CREATION | `if P44==1 then 1 else 0 endif` |
| obj#385 | NAME | `P58` |
| obj#385 | PROFILE | `"BL"+P16` |
| obj#394 | BOLT_GROUP_DISTX | `P23` |
| obj#394 | BOLT_GROUP_DISTY | `P39` |
| obj#394 | BOLT_TYPE | `P65` |
| obj#394 | DX1 | `P12` |
| obj#394 | DY1 | `P41` |
| obj#394 | DY2 | `P41` |
| obj#394 | SIZE | `P24_diameter` |
| obj#394 | SLOTTED_HOLE_PARTS | `SOLT_1` |
| obj#394 | SLOTTED_HOLE_X | `P64` |
| obj#394 | SLOTTED_HOLE_Y | `P66` |
| obj#394 | STANDARD | `P24_screwdin` |
| obj#394 | STRUCTURE | `BLT_set` |
| obj#394 | TOLERANCE | `P26` |
| obj#408 | BOLT_GROUP_DISTX | `P29` |
| obj#408 | BOLT_GROUP_DISTY | `P32` |
| obj#408 | DX1 | `P31` |
| obj#408 | SIZE | `0` |
| obj#408 | TOLERANCE | `P34` |
| obj#419 | WELDING_SITE | `P67` |
| obj#1084 | WELDING_SITE | `P43` |

## Preset `H294.p_NEC_EMBED`
```
joint_attributes.saveas_file "H294"
joint_attributes.get_menu "standard"
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
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P17 20.000000
joint_attributes.P25 6
joint_attributes.P71 100.000000
joint_attributes.P70 4
joint_attributes.P4 75.000000
joint_attributes.P31 30.000000
joint_attributes.P34 16.000000
joint_attributes.P2 100.000000
joint_attributes.P9 200.000000
joint_attributes.P1 150.000000
joint_attributes.P22 60.000000
joint_attributes.P20 3
joint_attributes.P13 87.000000
joint_attributes.P12 40.000000
joint_attributes.P19 40.000000
joint_attributes.P18 40.000000
joint_attributes.SOLT_set 0
joint_attributes.P37 60.000000
joint_attributes.P35 1
joint_attributes.P66 0.000000
joint_attributes.P64 0.000000
joint_attributes.P26 2.000000
joint_attributes.P65 0
joint_attributes.P24_screwdin "T.S.B"
joint_attributes.P24_diameter 20.000000
joint_attributes.BLT_bolt 1
joint_attributes.BLT_n2 0
joint_attributes.BLT_n1 1
joint_attributes.BLT_w3 1
joint_attributes.BLT_w2 0
joint_attributes.BLT_w1 0
joint_attributes.P44 1
joint_attributes.P45 10.000000
joint_attributes.P3 "D19"
joint_attributes.P54 "ANCHOR"
joint_attributes.P53 "SS400"
joint_attributes.P52 "1"
joint_attributes.P51 "P"
joint_attributes.P58 "PLATE"
joint_attributes.P57 "SS400"
joint_attributes.P56 "1"
joint_attributes.P55 "PL"
joint_attributes.P46 9.000000
joint_attributes.P62 "PLATE"
joint_attributes.P61 "SS400"
joint_attributes.P60 "1"
joint_attributes.P59 "PL"
joint_attributes.P8 450.000000
joint_attributes.P7 250.000000
joint_attributes.P6 12.000000
joint_attributes.P67 1
joint_attributes.P14 100.000000
joint_attributes.P15 10.000000
joint_attributes.P11 20.000000
joint_attributes.P43 0
joint_attributes.P10 15.000000
joint_attributes.P5 50.000000
```

## Preset `H900_300.p_NEC_EMBED`
```
joint_attributes.saveas_file "H900_300"
joint_attributes.get_menu "H294"
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
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P17 20.000000
joint_attributes.P25 8
joint_attributes.P71 90.000000
joint_attributes.P70 11
joint_attributes.P4 75.000000
joint_attributes.P31 30.000000
joint_attributes.P34 16.000000
joint_attributes.P2 100.000000
joint_attributes.P9 300.000000
joint_attributes.P1 200.000000
joint_attributes.P22 65.000000
joint_attributes.P20 11
joint_attributes.P13 125.000000
joint_attributes.P12 50.000000
joint_attributes.P19 50.000000
joint_attributes.P18 50.000000
joint_attributes.SOLT_set 0
joint_attributes.P37 60.000000
joint_attributes.P35 1
joint_attributes.P66 0.000000
joint_attributes.P64 0.000000
joint_attributes.P26 2.000000
joint_attributes.P65 0
joint_attributes.P24_screwdin "T.S.B"
joint_attributes.P24_diameter 24.000000
joint_attributes.BLT_bolt 1
joint_attributes.BLT_n2 0
joint_attributes.BLT_n1 1
joint_attributes.BLT_w3 1
joint_attributes.BLT_w2 0
joint_attributes.BLT_w1 0
joint_attributes.P44 1
joint_attributes.P45 10.000000
joint_attributes.P3 "D19"
joint_attributes.P54 "ANCHOR"
joint_attributes.P53 "SS400"
joint_attributes.P52 "1"
joint_attributes.P51 "P"
joint_attributes.P58 "PLATE"
joint_attributes.P57 "SS400"
joint_attributes.P56 "1"
joint_attributes.P55 "PL"
joint_attributes.P46 20.000000
joint_attributes.P62 "PLATE"
joint_attributes.P61 "SS400"
joint_attributes.P60 "1"
joint_attributes.P59 "PL"
joint_attributes.P8 1050.000000
joint_attributes.P7 300.000000
joint_attributes.P6 20.000000
joint_attributes.P67 1
joint_attributes.P14 100.000000
joint_attributes.P15 10.000000
joint_attributes.P11 20.000000
joint_attributes.P43 0
joint_attributes.P10 15.000000
joint_attributes.P5 50.000000
```

## Preset `H294.p_NEC_EMBED`
```
joint_attributes.saveas_file "H294"
joint_attributes.get_menu "standard"
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
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P17 20.000000
joint_attributes.P25 6
joint_attributes.P71 100.000000
joint_attributes.P70 4
joint_attributes.P4 75.000000
joint_attributes.P31 30.000000
joint_attributes.P34 16.000000
joint_attributes.P2 100.000000
joint_attributes.P9 200.000000
joint_attributes.P1 150.000000
joint_attributes.P22 60.000000
joint_attributes.P20 3
joint_attributes.P13 87.000000
joint_attributes.P12 40.000000
joint_attributes.P19 40.000000
joint_attributes.P18 40.000000
joint_attributes.SOLT_set 0
joint_attributes.P37 60.000000
joint_attributes.P35 1
joint_attributes.P66 0.000000
joint_attributes.P64 0.000000
joint_attributes.P26 2.000000
joint_attributes.P65 0
joint_attributes.P24_screwdin "T.S.B"
joint_attributes.P24_diameter 20.000000
joint_attributes.BLT_bolt 1
joint_attributes.BLT_n2 0
joint_attributes.BLT_n1 1
joint_attributes.BLT_w3 1
joint_attributes.BLT_w2 0
joint_attributes.BLT_w1 0
joint_attributes.P44 1
joint_attributes.P45 10.000000
joint_attributes.P3 "D19"
joint_attributes.P54 "ANCHOR"
joint_attributes.P53 "SS400"
joint_attributes.P52 "1"
joint_attributes.P51 "P"
joint_attributes.P58 "PLATE"
joint_attributes.P57 "SS400"
joint_attributes.P56 "1"
joint_attributes.P55 "PL"
joint_attributes.P46 9.000000
joint_attributes.P62 "PLATE"
joint_attributes.P61 "SS400"
joint_attributes.P60 "1"
joint_attributes.P59 "PL"
joint_attributes.P8 450.000000
joint_attributes.P7 250.000000
joint_attributes.P6 12.000000
joint_attributes.P67 1
joint_attributes.P14 100.000000
joint_attributes.P15 10.000000
joint_attributes.P11 20.000000
joint_attributes.P43 0
joint_attributes.P10 15.000000
joint_attributes.P5 50.000000
```

## Preset `H900_300.p_NEC_EMBED`
```
joint_attributes.saveas_file "H900_300"
joint_attributes.get_menu "H294"
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
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P17 20.000000
joint_attributes.P25 8
joint_attributes.P71 90.000000
joint_attributes.P70 11
joint_attributes.P4 75.000000
joint_attributes.P31 30.000000
joint_attributes.P34 16.000000
joint_attributes.P2 100.000000
joint_attributes.P9 300.000000
joint_attributes.P1 200.000000
joint_attributes.P22 65.000000
joint_attributes.P20 11
joint_attributes.P13 125.000000
joint_attributes.P12 50.000000
joint_attributes.P19 50.000000
joint_attributes.P18 50.000000
joint_attributes.SOLT_set 0
joint_attributes.P37 60.000000
joint_attributes.P35 1
joint_attributes.P66 0.000000
joint_attributes.P64 0.000000
joint_attributes.P26 2.000000
joint_attributes.P65 0
joint_attributes.P24_screwdin "T.S.B"
joint_attributes.P24_diameter 24.000000
joint_attributes.BLT_bolt 1
joint_attributes.BLT_n2 0
joint_attributes.BLT_n1 1
joint_attributes.BLT_w3 1
joint_attributes.BLT_w2 0
joint_attributes.BLT_w1 0
joint_attributes.P44 1
joint_attributes.P45 10.000000
joint_attributes.P3 "D19"
joint_attributes.P54 "ANCHOR"
joint_attributes.P53 "SS400"
joint_attributes.P52 "1"
joint_attributes.P51 "P"
joint_attributes.P58 "PLATE"
joint_attributes.P57 "SS400"
joint_attributes.P56 "1"
joint_attributes.P55 "PL"
joint_attributes.P46 20.000000
joint_attributes.P62 "PLATE"
joint_attributes.P61 "SS400"
joint_attributes.P60 "1"
joint_attributes.P59 "PL"
joint_attributes.P8 1050.000000
joint_attributes.P7 300.000000
joint_attributes.P6 20.000000
joint_attributes.P67 1
joint_attributes.P14 100.000000
joint_attributes.P15 10.000000
joint_attributes.P11 20.000000
joint_attributes.P43 0
joint_attributes.P10 15.000000
joint_attributes.P5 50.000000
```

## Preset `standard.p_NEC_EMBED`
```
joint_attributes.saveas_file "standard"
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
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P17 20.000000
joint_attributes.P25 6
joint_attributes.P71 100.000000
joint_attributes.P70 4
joint_attributes.P4 75.000000
joint_attributes.P31 30.000000
joint_attributes.P34 16.000000
joint_attributes.P2 100.000000
joint_attributes.P9 200.000000
joint_attributes.P1 150.000000
joint_attributes.P22 60.000000
joint_attributes.P20 3
joint_attributes.P13 87.000000
joint_attributes.P12 40.000000
joint_attributes.P19 40.000000
joint_attributes.P18 40.000000
joint_attributes.SOLT_set 0
joint_attributes.P37 60.000000
joint_attributes.P35 1
joint_attributes.P66 0.000000
joint_attributes.P64 0.000000
joint_attributes.P26 2.000000
joint_attributes.P65 0
joint_attributes.P24_screwdin "T.S.B"
joint_attributes.P24_diameter 20.000000
joint_attributes.BLT_bolt 1
joint_attributes.BLT_n2 0
joint_attributes.BLT_n1 1
joint_attributes.BLT_w3 1
joint_attributes.BLT_w2 0
joint_attributes.BLT_w1 0
joint_attributes.P44 1
joint_attributes.P45 10.000000
joint_attributes.P3 "D19"
joint_attributes.P54 "ANCHOR"
joint_attributes.P53 "SS400"
joint_attributes.P52 "1"
joint_attributes.P51 "P"
joint_attributes.P58 "PLATE"
joint_attributes.P57 "SS400"
joint_attributes.P56 "1"
joint_attributes.P55 "PL"
joint_attributes.P46 9.000000
joint_attributes.P62 "PLATE"
joint_attributes.P61 "SS400"
joint_attributes.P60 "1"
joint_attributes.P59 "PL"
joint_attributes.P8 450.000000
joint_attributes.P7 250.000000
joint_attributes.P6 12.000000
joint_attributes.P67 1
joint_attributes.P14 100.000000
joint_attributes.P15 10.000000
joint_attributes.P11 20.000000
joint_attributes.P43 0
joint_attributes.P10 15.000000
joint_attributes.P5 50.000000
```