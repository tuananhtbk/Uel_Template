# NEC_BASEPLATE

- File: `NEC\NEC_BASEPLATE.uel`  (131,891 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	NEC_BASEPLATE	All`
- Dialog parameters: 21 | Variables (P/custom): 43 | Distance vars (D): 230 | Parts with bound profile: 16
- Lookup files (fVF): -
- Functions used: fP
- Dialog bitmaps: 23 | Saved presets: standard.p_NEC_BASEPLATE, standard.p_NEC_BASEPLATE

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "NEC_BASEPLATE")
   {
       tab_page("", " Pictures ", 1)
       {

           picture("NEC_LOGO",8,2,480,630)
           picture("NEC_BASEPLATE2",8,2,150,100)
           //picture("NEC_BASEPLATE_THICKNESS",8,2,200,25)
           picture("NEC_BASEPLATE3",8,2,625,25)
           picture("NEC_BASEPLATE4",8,2,625,250)
           picture("NEC_BASEPLATE5",8,2,625,475)
           attribute("", "", label,"%s",  none, none, "0.0", "0.0",0,650)

           //parameter("1.웨브부재 B", "P4", distance, number, 3)
           //parameter("1.웨브부재 H", "P5", distance, number, 4)
           //parameter("2.플랜지부재 B", "P6", distance, number, 5)
           //parameter("2.플랜지부재 H", "P7", distance, number, 6)
           //parameter("3.플랜지부재 B", "P8", distance, number, 7)
           //parameter("3.플랜지부재 H", "P9", distance, number, 8)
           //parameter("1.상부 코너 간격", "P10", distance, number, 9)
           //parameter("1.하부 코너 간격", "P11", distance, number, 10)
           //parameter("2.상부 코너 간격", "P12", distance, number, 11)
           //parameter("2.하부 코너 간격", "P13", distance, number, 12)
           //parameter("3.상부 코너 간격", "P14", distance, number, 13)
           //parameter("3.하부 코너 간격", "P15", distance, number, 14)

           parameter("", "P4", distance, number, 685,175,60)
           parameter("", "P5", distance, number, 800,85,60)
           parameter("", "P6", distance, number, 685,400,60)
           parameter("", "P7", distance, number, 800,315,60)
           parameter("", "P8", distance, number, 685,625,60)
           parameter("", "P9", distance, number, 800,535,60)
           parameter("", "P10", distance, number, 730,2,60)
           parameter("", "P11", distance, number, 565,126,60)
           parameter("", "P12", distance, number, 730,227,60)
           parameter("", "P13", distance, number, 565,352,60)
           parameter("", "P14", distance, number, 730,453,60)
           parameter("", "P15", distance, number, 565,577,60)

           parameter("", "P38", distance, number, 80,300,60)
           parameter("", "P39", distance, number, 80,270,60)
           parameter("", "P40", distance, number, 375,55,60)
           parameter("", "P41", distance, number, 272,55,60)

           //parameter("1.갯수 설정(우)", "P18", integer, number, 17)
           //parameter("1.갯수 설정(좌)", "P19", integer, number, 18)
           //parameter("2.갯수 설정(상)", "P20", integer, number, 19)
           //parameter("2.갯수 설정(하)", "P21", integer, number, 20)

           attribute("", "3.On/Off", label,"%s",  none, none, "0.0", "0.0",50,375)   
           parameter("", "P22", YesNo, number, 50, 400, 70)
           //parameter("3.On/Off", "P22", YesNo, number, 21)
           attribute("", "Component Name", label,"%s",  none, none, "0.0", "0.0",25,550)
           attribute("", "Component Attribute", label,"%s",  none, none, "0.0", "0.0",25,575)
           parameter("", "P1_name", ComponentName, text, 300,550,120)
           parameter("", "P1_attrfile", ComponentAttributeFile, text, 300,575,120)

           attribute("", "챔퍼설정", label,"%s",  none, none, "0.0", "0.0",820,150)
           parameter("", "P16", chamfer_type, number, 830,175,80)
           parameter("", "P17", distance, number, 830,200,80)


           attribute("P18", "", option, "%s", no, none, "0.0", "0.0", 360,240,145)
                {
                     value("NEC_BASEPLATE(RIGHT_0).xbm",1)
                     value("NEC_BASEPLATE(RIGHT_1).xbm",0)
                     value("NEC_BASEPLATE(RIGHT_2).xbm",0)
                     value("NEC_BASEPLATE(RIGHT_3).xbm",0)
                }

           attribute("P19", "", option, "%s", no, none, "0.0", "0.0", 150,240,185)
                {
                     value("NEC_BASEPLATE(LEFT_0).xbm",1)
                     value("NEC_BASEPLATE(LEFT_1).xbm",0)
                     value("NEC_BASEPLATE(LEFT_2).xbm",0)
                     value("NEC_BASEPLATE(LEFT_3).xbm",0)
                }

           attribute("P20", "", option, "%s", no, none, "0.0", "0.0", 272,85,165)
                {
                     value("NEC_BASEPLATE(TOP_0).xbm",1)
                     value("NEC_BASEPLATE(TOP_1).xbm",0)
                     value("NEC_BASEPLATE(TOP_2).xbm",0)
                     value("NEC_BASEPLATE(TOP_3).xbm",0)
                }

           attribute("P21", "", option, "%s", no, none, "0.0", "0.0", 270,397,165)
                {
                     value("NEC_BASEPLATE(BOTTOM_0).xbm",1)
                     value("NEC_BASEPLATE(BOTTOM_1).xbm",0)
                     value("NEC_BASEPLATE(BOTTOM_2).xbm",0)
                     value("NEC_BASEPLATE(BOTTOM_3).xbm",0)
                }
       }
       tab_page("", " Plates ", 2)
       {
           picture("NEC_BASEPLATE_THICKNESS",8,2,350,150)
           attribute("", "1.RibPlate", label,"%s",  none, none, "0.0", "0.0",20,50)
           attribute("", "2.RibPlate", label,"%s",  none, none, "0.0", "0.0",20,75)
           attribute("", "3.RibPlate", label,"%s",  none, none, "0.0", "0.0",20,100)
           attribute("t", "t", label,"%s",  none, none, "0.0", "0.0",230,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",400,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",590,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",740,25)
           parameter("", "P29", string, text, 350,75,50)

           parameter("", "P35", distance, number, 220,50,50)
           parameter("", "P36", distance, number, 220,75,50)
           parameter("", "P37", distance, number, 220,100,50)
           parameter("", "P25", string, text, 350,50,50)           
           parameter("", "P27", string, text, 690,75,120)
           
           parameter("", "P33", string, text, 350,100,50)
           parameter("", "P26", string, text, 430,50,40)
           parameter("", "P30", string, text, 430,75,40)
           parameter("", "P34", string, text, 430,100,40)
           parameter("", "P24", material, text, 540,50,90)
           parameter("", "P28", material, text, 540,75,90)
           parameter("", "P32", material, text, 540,100,90)
           parameter("", "P23", string, text, 690,50,120)
           parameter("", "P31", string, text, 690,100,120)
    
       }
tab_page("", " 설명서 ", 3)
       {
           picture("NEC_BASEPLATE(Manual)",8,2,15,10)

       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1_attrfile | Component Attribute | 21 | `0.00` |
| P1_name | Component Name | 13 | `0.00` |
| P2 | Parameter2 | Length | `P5+P3` *(formula)* |
| P3 | 베이스 플레이트 두께 참조 | Length | `fP("tpl1","ID67C2A424-0000-B708-3137-343038313035")` *(formula)* |
| P4 | 1.웨브부재 B | Length | `200.00` |
| P5 | 1.웨브부재 H | Length | `200.00` |
| P6 | 2.플랜지부재 B | Length | `200.00` |
| P7 | 2.플랜지부재 H | Length | `200.00` |
| P8 | 3.플랜지부재 B | Length | `200.00` |
| P9 | 3.플랜지부재 H | Length | `200.00` |
| P10 | 1.상부 코너 간격 | Length | `25.00` |
| P11 | 1.하부 코너 간격 | Length | `25.00` |
| P12 | 2.상부 코너 간격 | Length | `25.00` |
| P13 | 2.하부 코너 간격 | Length | `25.00` |
| P14 | 3.상부 코너 간격 | Length | `25.00` |
| P15 | 3.하부 코너 간격 | Length | `25.00` |
| P16 | 챔퍼 타입 | Chamfer | `3` |
| P17 | 챔퍼 크기 | Length | `30.00` |
| P18 | 1.갯수 설정(우) | Integer | `0` |
| P19 | 1.갯수 설정(좌) | Integer | `0` |
| P20 | 2.갯수 설정(상) | Integer | `0` |
| P21 | 2.갯수 설정(하) | Integer | `0` |
| P22 | 3.On/Off | Yes/No | `0` |
| P23 | 1.이름 | Text | `PLATE` |
| P24 | 1.재질 | Material | `SS400` |
| P25 | 1.프리픽스 | Text | `PL` |
| P26 | 1.스타트 넘버 | Text | `1` |
| P27 | 2.이름 | Text | `PLATE` |
| P28 | 2.재질 | Material | `SS400` |
| P29 | 2.프리픽스 | Text | `PL` |
| P30 | 2.스타트 넘버 | Text | `1` |
| P31 | 3.이름 | Text | `PLATE` |
| P32 | 3.재질 | Material | `SS400` |
| P33 | 3.프리픽스 | Text | `PL` |
| P34 | 3.스타트 넘버 | Text | `1` |
| P35 | 1.부재 두께 | Length | `10.00` |
| P36 | 2.부재 두께 | Length | `10.00` |
| P37 | 3.부재 두께 | Length | `10.00` |
| P38 | 1.WEB 간격(아래) | Length | `150.00` |
| P39 | 1.WEB 간격(위) | Length | `150.00` |
| P40 | 2.FLANGE 간격(아래) | Length | `50.00` |
| P41 | 2.FLANGE 간격(위) | Length | `50.00` |
| P42 | Parameter42 | Length | `0.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.COLUMN | `` |
| D2 | D2.PLATE.COLUMN | `` |
| D3 | D3.PLATE.COLUMN | `` |
| D4 | D4.PLATE.COLUMN | `` |
| D5 | D5.PLATE.COLUMN | `` |
| D6 | D6.PLATE.COLUMN | `` |
| D7 | D7.PLATE.COLUMN | `` |
| D8 | D8.PLATE.COLUMN | `` |
| D9 | D9.PLATE.COLUMN | `` |
| D10 | D10.PLATE.COLUMN | `` |
| D11 | D11.PLATE.COLUMN | `` |
| D12 | D12.PLATE.COLUMN | `` |
| D13 | D13.PLATE.COLUMN | `` |
| D14 | D14.PLATE.COLUMN | `` |
| D15 | D15.PLATE.COLUMN | `` |
| D16 | D16.PLATE.COLUMN | `` |
| D17 | D17.PLATE.COLUMN | `` |
| D18 | D18.PLATE.COLUMN | `` |
| D19 | D19.PLATE.COLUMN | `` |
| D20 | D20.PLATE.COLUMN | `` |
| D21 | D21.PLATE.COLUMN | `` |
| D22 | D22.PLATE.COLUMN | `` |
| D23 | D23.PLATE.COLUMN | `` |
| D24 | D24.PLATE.COLUMN | `` |
| D25 | D25.PLATE.COLUMN | `` |
| D26 | D26.PLATE.COLUMN | `` |
| D27 | D27.PLATE.COLUMN | `` |
| D28 | D28.PLATE.COLUMN | `` |
| D29 | D29.PLATE.COLUMN | `` |
| D30 | D30.PLATE.COLUMN | `` |
| D31 | D31.PLATE.COLUMN | `` |
| D32 | D32.PLATE.COLUMN | `` |
| D33 | D33.PLATE.COLUMN | `` |
| D34 | D34.PLATE.COLUMN | `` |
| D35 | D35.PLATE.COLUMN | `` |
| D36 | D36.PLATE.COLUMN | `` |
| D37 | D37.PLATE.COLUMN | `` |
| D38 | D38.PLATE.COLUMN | `` |
| D39 | D39.PLATE.COLUMN | `P4` |
| D40 | D40.PLATE.COLUMN | `P4` |
| D41 | D41.PLATE.COLUMN | `P4` |
| D42 | D42.PLATE.COLUMN | `P4` |
| D43 | D43.PLATE.COLUMN | `P4` |
| D44 | D44.PLATE.COLUMN | `P4` |
| D45 | D45.PLATE.COLUMN | `P5` |
| D46 | D46.PLATE.COLUMN | `P5` |
| D47 | D47.PLATE.COLUMN | `P5` |
| D48 | D48.PLATE.COLUMN | `P5` |
| D49 | D49.PLATE.COLUMN | `P5` |
| D50 | D50.PLATE.COLUMN | `P5` |
| D51 | D51.PLATE.COLUMN | `` |
| D52 | D52.PLATE.COLUMN | `` |
| D53 | D53.PLATE.COLUMN | `` |
| D54 | D54.PLATE.COLUMN | `` |
| D55 | D55.PLATE.COLUMN | `` |
| D56 | D56.PLATE.COLUMN | `` |
| D57 | D57.PLATE.COLUMN | `P4` |
| D58 | D58.PLATE.COLUMN | `P4` |
| D59 | D59.PLATE.COLUMN | `P4` |
| D60 | D60.PLATE.COLUMN | `P4` |
| D61 | D61.PLATE.COLUMN | `P4` |
| D62 | D62.PLATE.COLUMN | `P4` |
| D63 | D63.PLATE.COLUMN | `P5` |
| D64 | D64.PLATE.COLUMN | `P5` |
| D65 | D65.PLATE.COLUMN | `P5` |
| D66 | D66.PLATE.COLUMN | `P5` |
| D67 | D67.PLATE.COLUMN | `P5` |
| D68 | D68.PLATE.COLUMN | `P5` |
| D69 | D69.PLATE.COLUMN | `` |
| D70 | D70.PLATE.COLUMN | `` |
| D71 | D71.PLATE.COLUMN | `` |
| D72 | D72.PLATE.COLUMN | `` |
| D73 | D73.PLATE.COLUMN | `` |
| D74 | D74.PLATE.COLUMN | `` |
| D75 | D75.PLATE.COLUMN | `P6` |
| D76 | D76.PLATE.COLUMN | `P6` |
| D77 | D77.PLATE.COLUMN | `P6` |
| D78 | D78.PLATE.COLUMN | `P6` |
| D79 | D79.PLATE.COLUMN | `P6` |
| D80 | D80.PLATE.COLUMN | `P6` |
| D81 | D81.PLATE.COLUMN | `P7` |
| D82 | D82.PLATE.COLUMN | `P7` |
| D83 | D83.PLATE.COLUMN | `P7` |
| D84 | D84.PLATE.COLUMN | `P7` |
| D85 | D85.PLATE.COLUMN | `P7` |
| D86 | D86.PLATE.COLUMN | `P7` |
| D87 | D87.PLATE.COLUMN | `` |
| D88 | D88.PLATE.COLUMN | `` |
| D89 | D89.PLATE.COLUMN | `` |
| D90 | D90.PLATE.COLUMN | `` |
| D91 | D91.PLATE.COLUMN | `` |
| D92 | D92.PLATE.COLUMN | `` |
| D93 | D93.PLATE.COLUMN | `P6` |
| D94 | D94.PLATE.COLUMN | `P6` |
| D95 | D95.PLATE.COLUMN | `P6` |
| D96 | D96.PLATE.COLUMN | `P6` |
| D97 | D97.PLATE.COLUMN | `P6` |
| D98 | D98.PLATE.COLUMN | `P6` |
| D99 | D99.PLATE.COLUMN | `P7` |
| D100 | D100.PLATE.COLUMN | `P7` |
| D101 | D101.PLATE.COLUMN | `P7` |
| D102 | D102.PLATE.COLUMN | `P7` |
| D103 | D103.PLATE.COLUMN | `P7` |
| D104 | D104.PLATE.COLUMN | `P7` |
| D105 | D105.PLATE.COLUMN | `` |
| D106 | D106.PLATE.COLUMN | `` |
| D107 | D107.PLATE.COLUMN | `` |
| D108 | D108.PLATE.COLUMN | `` |
| D109 | D109.PLATE.COLUMN | `P9` |
| D110 | D110.PLATE.COLUMN | `P9` |
| D111 | D111.PLATE.COLUMN | `P9` |
| D112 | D112.PLATE.COLUMN | `P9` |
| D113 | D113.PLATE.COLUMN | `P8` |
| D114 | D114.PLATE.COLUMN | `P8` |
| D115 | D115.PLATE.COLUMN | `P8` |
| D116 | D116.PLATE.COLUMN | `P8` |
| D117 | D117.PLATE.COLUMN | `P8` |
| D118 | D118.PLATE.COLUMN | `P8` |
| D119 | D119.PLATE.COLUMN | `P8` |
| D120 | D120.PLATE.COLUMN | `P8` |
| D121 | D121.PLATE.COLUMN | `` |
| D122 | D122.PLATE.COLUMN | `` |
| D123 | D123.PLATE.COLUMN | `` |
| D124 | D124.PLATE.COLUMN | `` |
| D125 | D125.PLATE.COLUMN | `P9` |
| D126 | D126.PLATE.COLUMN | `P9` |
| D127 | D127.PLATE.COLUMN | `P9` |
| D128 | D128.PLATE.COLUMN | `P9` |
| D129 | D129.PLATE.COLUMN | `` |
| D130 | D130.PLATE.COLUMN | `` |
| D131 | D131.PLATE.COLUMN | `` |
| D132 | D132.PLATE.COLUMN | `` |
| D133 | D133.PLATE.COLUMN | `` |
| D134 | D134.PLATE.COLUMN | `` |
| D135 | D135.PLATE.COLUMN | `` |
| D136 | D136.PLATE.COLUMN | `` |
| D137 | D137.PLATE.COLUMN | `` |
| D138 | D138.PLATE.COLUMN | `` |
| D139 | D139.PLATE.COLUMN | `` |
| D140 | D140.PLATE.COLUMN | `` |
| D141 | D141.PLATE.COLUMN | `` |
| D142 | D142.PLATE.COLUMN | `` |
| D143 | D143.PLATE.COLUMN | `` |
| D144 | D144.PLATE.COLUMN | `` |
| D145 | D145.PLATE.COLUMN | `` |
| D146 | D146.PLATE.COLUMN | `` |
| D147 | D147.PLATE.COLUMN | `` |
| D148 | D148.PLATE.COLUMN | `` |
| D149 | D149.PLATE.COLUMN | `P11` |
| D150 | D150.PLATE.COLUMN | `P11` |
| D151 | D151.PLATE.COLUMN | `P11` |
| D152 | D152.PLATE.COLUMN | `P11` |
| D153 | D153.PLATE.COLUMN | `P11` |
| D154 | D154.PLATE.COLUMN | `P11` |
| D155 | D155.PLATE.COLUMN | `P10` |
| D156 | D156.PLATE.COLUMN | `P10` |
| D157 | D157.PLATE.COLUMN | `P10` |
| D158 | D158.PLATE.COLUMN | `P10` |
| D159 | D159.PLATE.COLUMN | `P10` |
| D160 | D160.PLATE.COLUMN | `P10` |
| D161 | D161.PLATE.COLUMN | `P13` |
| D162 | D162.PLATE.COLUMN | `P13` |
| D163 | D163.PLATE.COLUMN | `P13` |
| D164 | D164.PLATE.COLUMN | `P13` |
| D165 | D165.PLATE.COLUMN | `P13` |
| D166 | D166.PLATE.COLUMN | `P13` |
| D167 | D167.PLATE.COLUMN | `P12` |
| D168 | D168.PLATE.COLUMN | `P12` |
| D169 | D169.PLATE.COLUMN | `P12` |
| D170 | D170.PLATE.COLUMN | `P12` |
| D171 | D171.PLATE.COLUMN | `P12` |
| D172 | D172.PLATE.COLUMN | `P12` |
| D173 | D173.PLATE.COLUMN | `P14` |
| D174 | D174.PLATE.COLUMN | `P14` |
| D175 | D175.PLATE.COLUMN | `P14` |
| D176 | D176.PLATE.COLUMN | `P14` |
| D177 | D177.PLATE.COLUMN | `P15` |
| D178 | D178.PLATE.COLUMN | `P15` |
| D179 | D179.PLATE.COLUMN | `P15` |
| D180 | D180.PLATE.COLUMN | `P15` |
| D181 | D181.PLATE.COLUMN | `` |
| D182 | D182.PLATE.COLUMN | `` |
| D183 | D183.PLATE.COLUMN | `` |
| D184 | D184.PLATE.COLUMN | `` |
| D185 | D185.PLATE.COLUMN | `` |
| D186 | D186.PLATE.COLUMN | `` |
| D187 | D187.PLATE.COLUMN | `` |
| D188 | D188.PLATE.COLUMN | `` |
| D189 | D189.PLATE.COLUMN | `` |
| D190 | D190.PLATE.COLUMN | `` |
| D191 | D191.PLATE.COLUMN | `P38` |
| D192 | D192.PLATE.COLUMN | `P38` |
| D193 | D193.PLATE.COLUMN | `P38` |
| D194 | D194.PLATE.COLUMN | `P38` |
| D195 | D195.PLATE.COLUMN | `P38` |
| D196 | D196.PLATE.COLUMN | `P38` |
| D197 | D197.PLATE.COLUMN | `P38` |
| D198 | D198.PLATE.COLUMN | `P38` |
| D199 | D199.PLATE.COLUMN | `P38` |
| D200 | D200.PLATE.COLUMN | `P38` |
| D201 | D201.PLATE.COLUMN | `P39` |
| D202 | D202.PLATE.COLUMN | `P39` |
| D203 | D203.PLATE.COLUMN | `P39` |
| D204 | D204.PLATE.COLUMN | `P39` |
| D205 | D205.PLATE.COLUMN | `P39` |
| D206 | D206.PLATE.COLUMN | `P39` |
| D207 | D207.PLATE.COLUMN | `P39` |
| D208 | D208.PLATE.COLUMN | `P39` |
| D209 | D209.PLATE.COLUMN | `P39` |
| D210 | D210.PLATE.COLUMN | `P39` |
| D211 | D211.PLATE.COLUMN | `P40` |
| D212 | D212.PLATE.COLUMN | `P40` |
| D213 | D213.PLATE.COLUMN | `P40` |
| D214 | D214.PLATE.COLUMN | `P40` |
| D215 | D215.PLATE.COLUMN | `P40` |
| D216 | D216.PLATE.COLUMN | `P40` |
| D217 | D217.PLATE.COLUMN | `P40` |
| D218 | D218.PLATE.COLUMN | `P40` |
| D219 | D219.PLATE.COLUMN | `P40` |
| D220 | D220.PLATE.COLUMN | `P40` |
| D221 | D221.PLATE.COLUMN | `P41` |
| D222 | D222.PLATE.COLUMN | `P41` |
| D223 | D223.PLATE.COLUMN | `P41` |
| D224 | D224.PLATE.COLUMN | `P41` |
| D225 | D225.PLATE.COLUMN | `P41` |
| D226 | D226.PLATE.COLUMN | `P41` |
| D227 | D227.PLATE.COLUMN | `P41` |
| D228 | D228.PLATE.COLUMN | `P41` |
| D229 | D229.PLATE.COLUMN | `P41` |
| D230 | D230.PLATE.COLUMN | `P41` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#186 | CREATION | `if P20==1 \|\| P20==3 then 1 else 0 endif` |
| obj#186 | MATERIAL | `P28` |
| obj#186 | NAME | `P27` |
| obj#186 | PROFILE | `"PL"+P36` |
| obj#186 | SERIE | `P29` |
| obj#186 | STARTNUMBER | `P30` |
| obj#199 | CREATION | `if P21==1 \|\| P21==3 then 1 else 0 endif` |
| obj#199 | MATERIAL | `P28` |
| obj#199 | NAME | `P27` |
| obj#199 | PROFILE | `"PL"+P36` |
| obj#199 | SERIE | `P29` |
| obj#199 | STARTNUMBER | `P30` |
| obj#207 | CREATION | `P22` |
| obj#207 | MATERIAL | `P32` |
| obj#207 | NAME | `P31` |
| obj#207 | PROFILE | `"PL"+P37` |
| obj#207 | SERIE | `P33` |
| obj#207 | STARTNUMBER | `P34` |
| obj#216 | CREATION | `P22` |
| obj#216 | MATERIAL | `P32` |
| obj#216 | NAME | `P31` |
| obj#216 | PROFILE | `"PL"+P37` |
| obj#216 | SERIE | `P33` |
| obj#216 | STARTNUMBER | `P34` |
| obj#223 | CREATION | `P22` |
| obj#223 | MATERIAL | `P32` |
| obj#223 | NAME | `P31` |
| obj#223 | PROFILE | `"PL"+P37` |
| obj#223 | SERIE | `P33` |
| obj#223 | STARTNUMBER | `P34` |
| obj#230 | CREATION | `P22` |
| obj#230 | MATERIAL | `P32` |
| obj#230 | NAME | `P31` |
| obj#230 | PROFILE | `"PL"+P37` |
| obj#230 | SERIE | `P33` |
| obj#230 | STARTNUMBER | `P34` |
| obj#236 | CREATION | `if P18==1 \|\| P18==3 then 1 else 0 endif` |
| obj#236 | MATERIAL | `P24` |
| obj#236 | NAME | `P23` |
| obj#236 | PROFILE | `"PL"+P35` |
| obj#236 | SERIE | `P25` |
| obj#236 | STARTNUMBER | `P26` |
| obj#243 | CREATION | `if P19==1 \|\| P19==3 then 1 else 0 endif` |
| obj#243 | MATERIAL | `P24` |
| obj#243 | NAME | `P23` |
| obj#243 | PROFILE | `"PL"+P35` |
| obj#243 | SERIE | `P25` |
| obj#243 | STARTNUMBER | `P26` |
| obj#249 | CREATION | `if P19==2 \|\| P19==3 then 1 else 0 endif` |
| obj#249 | MATERIAL | `P24` |
| obj#249 | NAME | `P23` |
| obj#249 | PROFILE | `"PL"+P35` |
| obj#249 | SERIE | `P25` |
| obj#249 | STARTNUMBER | `P26` |
| obj#255 | CREATION | `if P19==2 \|\| P19==3 then 1 else 0 endif` |
| obj#255 | MATERIAL | `P24` |
| obj#255 | NAME | `P23` |
| obj#255 | PROFILE | `"PL"+P35` |
| obj#255 | SERIE | `P25` |
| obj#255 | STARTNUMBER | `P26` |
| obj#261 | CREATION | `if P18==2 \|\| P18==3 then 1 else 0 endif` |
| obj#261 | MATERIAL | `P24` |
| obj#261 | NAME | `P23` |
| obj#261 | PROFILE | `"PL"+P35` |
| obj#261 | SERIE | `P25` |
| obj#261 | STARTNUMBER | `P26` |
| obj#267 | CREATION | `if P18==2 \|\| P18==3 then 1 else 0 endif` |
| obj#267 | MATERIAL | `P24` |
| obj#267 | NAME | `P23` |
| obj#267 | PROFILE | `"PL"+P35` |
| obj#267 | SERIE | `P25` |
| obj#267 | STARTNUMBER | `P26` |
| obj#273 | CREATION | `if P21==2 \|\| P21==3 then 1 else 0 endif` |
| obj#273 | MATERIAL | `P28` |
| obj#273 | NAME | `P27` |
| obj#273 | PROFILE | `"PL"+P36` |
| obj#273 | SERIE | `P29` |
| obj#273 | STARTNUMBER | `P30` |
| obj#281 | CREATION | `if P21==2 \|\| P21==3 then 1 else 0 endif` |
| obj#281 | MATERIAL | `P28` |
| obj#281 | NAME | `P27` |
| obj#281 | PROFILE | `"PL"+P36` |
| obj#281 | SERIE | `P29` |
| obj#281 | STARTNUMBER | `P30` |
| obj#289 | CREATION | `if P20==2 \|\| P20==3 then 1 else 0 endif` |
| obj#289 | MATERIAL | `P28` |
| obj#289 | NAME | `P27` |
| obj#289 | PROFILE | `"PL"+P36` |
| obj#289 | SERIE | `P29` |
| obj#289 | STARTNUMBER | `P30` |
| obj#297 | CREATION | `if P20==2 \|\| P20==3 then 1 else 0 endif` |
| obj#297 | MATERIAL | `P28` |
| obj#297 | NAME | `P27` |
| obj#297 | PROFILE | `"PL"+P36` |
| obj#297 | SERIE | `P29` |
| obj#297 | STARTNUMBER | `P30` |
| obj#305 | ATTRIBUTE_FILE | `P1_attrfile` |
| obj#305 | NAME | `P1_name` |

## Preset `standard.p_NEC_BASEPLATE`
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
joint_attributes.P17 30.000000
joint_attributes.P16 3
joint_attributes.P31 "PLATE"
joint_attributes.P23 "PLATE"
joint_attributes.P32 "SS400"
joint_attributes.P28 "SS400"
joint_attributes.P24 "SS400"
joint_attributes.P34 "1"
joint_attributes.P30 "1"
joint_attributes.P26 "1"
joint_attributes.P33 "PL"
joint_attributes.P27 "PLATE"
joint_attributes.P25 "PL"
joint_attributes.P37 10.000000
joint_attributes.P36 10.000000
joint_attributes.P35 10.000000
joint_attributes.P29 "PL"
joint_attributes.P21 3
joint_attributes.P20 3
joint_attributes.P19 3
joint_attributes.P18 3
joint_attributes.P1_attrfile "10"
joint_attributes.P1_name "1047"
joint_attributes.P22 1
joint_attributes.P41 150.000000
joint_attributes.P40 150.000000
joint_attributes.P39 200.000000
joint_attributes.P38 200.000000
joint_attributes.P15 25.000000
joint_attributes.P14 25.000000
joint_attributes.P13 25.000000
joint_attributes.P12 25.000000
joint_attributes.P11 25.000000
joint_attributes.P10 25.000000
joint_attributes.P9 200.000000
joint_attributes.P8 200.000000
joint_attributes.P7 200.000000
joint_attributes.P6 200.000000
joint_attributes.P5 200.000000
joint_attributes.P4 200.000000
```

## Preset `standard.p_NEC_BASEPLATE`
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
joint_attributes.P17 30.000000
joint_attributes.P16 3
joint_attributes.P31 "PLATE"
joint_attributes.P23 "PLATE"
joint_attributes.P32 "SS400"
joint_attributes.P28 "SS400"
joint_attributes.P24 "SS400"
joint_attributes.P34 "1"
joint_attributes.P30 "1"
joint_attributes.P26 "1"
joint_attributes.P33 "PL"
joint_attributes.P27 "PLATE"
joint_attributes.P25 "PL"
joint_attributes.P37 10.000000
joint_attributes.P36 10.000000
joint_attributes.P35 10.000000
joint_attributes.P29 "PL"
joint_attributes.P21 3
joint_attributes.P20 3
joint_attributes.P19 3
joint_attributes.P18 3
joint_attributes.P1_attrfile "10"
joint_attributes.P1_name "1047"
joint_attributes.P22 1
joint_attributes.P41 150.000000
joint_attributes.P40 150.000000
joint_attributes.P39 200.000000
joint_attributes.P38 200.000000
joint_attributes.P15 25.000000
joint_attributes.P14 25.000000
joint_attributes.P13 25.000000
joint_attributes.P12 25.000000
joint_attributes.P11 25.000000
joint_attributes.P10 25.000000
joint_attributes.P9 200.000000
joint_attributes.P8 200.000000
joint_attributes.P7 200.000000
joint_attributes.P6 200.000000
joint_attributes.P5 200.000000
joint_attributes.P4 200.000000
```