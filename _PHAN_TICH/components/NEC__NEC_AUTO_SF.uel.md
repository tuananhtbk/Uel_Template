# NEC_COLUMN_STIFFENERS

- File: `NEC\NEC_AUTO_SF.uel`  (156,904 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	NEC_AUTO_STIFFENERS	All`
- Dialog parameters: 8 | Variables (P/custom): 142 | Distance vars (D): 454 | Parts with bound profile: 38
- Lookup files (fVF): -
- Functions used: fP, if, max
- Dialog bitmaps: 9 | Saved presets: standard.p_NEC_AUTO_STIFFENERS, standard.p_NEC_AUTO_STIFFENERS

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "NEC_COLUMN_STIFFENERS")
   {
       tab_page("", " Pictures ", 1)
       {
           picture("NEC_LOGO",8,2,450,500)
           

           attribute(" ", " ", label,"%s",  none, none, "0.0", "0.0",0,530)
           parameter("", "P26", distance, number, 435,295,60)
           //parameter("Upper Add Plate", "P34", integer, number, 3)
           attribute("P34", "", option, "%s", no, none, "0.0", "0.0", 85,75,320,"toggle_field:P135=1")
                {
                     value("NEC_AUTO_STIFFENERS3.xbm",1)
                     value("NEC_AUTO_STIFFENERS4.xbm",0)

                }
           
           
           attribute("P135", "", option, "%s", no, none, "0.0", "0.0", 125,400,180)
                {
                     value("NEC_AUTO_STIFFENERS2.xbm",1)
                     value("NEC_AUTO_STIFFENERS1.xbm",0)

                }

           attribute("P128", "", option, "%s", no, none, "0.0", "0.0", 500,75,300)
                {
                     value("NEC_AUTO_STIFFENERS5.xbm",1)
                     value("NEC_AUTO_STIFFENERS6.xbm",0)

                }
           //parameter("쪽쪽이", "P128", YesNo, number, 3)

           //parameter("Side Add Plate 옵션1", "P135", integer, number, 5)


       }

tab_page("", " Plates ", 2)
       {
           attribute("t", "t", label,"%s",  none, none, "0.0", "0.0",230,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",350,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",540,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",690,25)
           attribute("", "Top_Stiffener", label,"%s",  none, none, "0.0", "0.0",20,50)
           attribute("", "1_Bott.Stiff", label,"%s",  none, none, "0.0", "0.0",20,75)
           attribute("", "2_Bott.Stiff", label,"%s",  none, none, "0.0", "0.0",20,100)
           attribute("", "3_Bott.Stiff", label,"%s",  none, none, "0.0", "0.0",20,125)
           attribute("", "4_Bott.Stiff", label,"%s",  none, none, "0.0", "0.0",20,150)
           picture("NEC_AUTO_STIFFENERS8",8,2,320,150)
           
           parameter("TOP_두께", "P2", string, text, 220,50,50)
           parameter("", "P140", string, text, 220,100,50)
           parameter("", "P141", string, text, 220,125,50)
           parameter("", "P142", string, text, 220,150,50)
           parameter("", "P143", string, text, 220,75,50)

           parameter("", "P148", string, text, 350,50,50)
           parameter("", "P149", string, text, 430,50,40)
           parameter("", "P150", material, text, 540,50,90)
           parameter("", "P151", string, text, 690,50,120)

           parameter("보강재 챔퍼 타입", "P144", chamfer_type, number, 16)
           parameter("보강재 챔퍼 크기", "P145", distance, number, 17)
       }
tab_page("", " 설명서 ", 3)
       {
           picture("NEC_AUTO_STIFFENERS9",8,2,15,10)



       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | TOP 공식 | Profile | `if (P2 !="") then ("PL"+P2) else "PL"+P18 endif` *(formula)* |
| P2 | TOP_두께 | Text | `` |
| P3 | BOTTOM 공식(1) | Profile | `if (P4 !="") then P32 else "PL"+P47 endif` *(formula)* |
| P4 | BOTTOM_두께 | Text | `` |
| P5 | 2번 부재 두께 | Profile | `"PL"+P27` *(formula)* |
| P6 | 3번 부재 두께 | Profile | `"PL"+P28` *(formula)* |
| P7 | 4번 부재 두께 | Profile | `"PL"+P25` *(formula)* |
| P8 | 1번 부재 두께 | Profile | `"PL"+P21` *(formula)* |
| P9 | 1번 부재 폭 참조 | Length | `fP(proH,"ID67C2A424-0000-A293-3137-343038313034")` *(formula)* |
| P10 | 2번 부재 폭 참조 | Length | `fP(proH,"ID67C2A424-0000-A299-3137-343038313034")` *(formula)* |
| P11 | 3번 부재 폭 참조 | Length | `fP(proH,"ID67C2A424-0000-A29F-3137-343038313034")` *(formula)* |
| P12 | 4번 주재 폭 참조 | Length | `fP(proH,"ID67C2A424-0000-A2A5-3137-343038313034")` *(formula)* |
| P13 | 가장 폭이 넑은 놈 | Length | `max(P9,P10,P11,P12)` *(formula)* |
| P14 | 1번 부재 플랜지 두께 참조 | Length | `fP(proT,"ID67C2A424-0000-A293-3137-343038313034")` *(formula)* |
| P15 | 2번 부재 플랜지 두께 참조 | Length | `fP(proT,"ID67C2A424-0000-A299-3137-343038313034")` *(formula)* |
| P16 | 3번 부재 플랜지 두께 참조 | Length | `fP(proT,"ID67C2A424-0000-A29F-3137-343038313034")` *(formula)* |
| P17 | 4번 부재 플랜지 두께 참조 | Length | `fP(proT,"ID67C2A424-0000-A2A5-3137-343038313034")` *(formula)* |
| P18 | 가장 두꺼운 놈 | Length | `max(P14,P15,P16,P17)` *(formula)* |
| P19 | Parameter19 | Length | `if (P9 == P11) then P9-max(P14,P16)  else P9 endif` *(formula)* |
| P21 | 1번 하부보강재 공식1 | Length | `if (fabs(P9-P10)) < 100 && (fabs(P9-P11)) < 100 && (fabs(P9-P12)) < 100 then P18 else P22 endif` *(formula)* |
| P22 | 1번 하부보강재 공식2 | Length | `if (fabs(P9-P10)) < 100 then max(P14,P15) else P23 endif` *(formula)* |
| P23 | 1번 하부보강재 공식3 | Length | `if (fabs(P9-P12)) < 100 then max(P14,P17) else P24 endif` *(formula)* |
| P24 | 1번 하부보강재 공식4 | Length | `if (fabs(P9-P11)) < 100 then max(P14,P16) else P14 endif` *(formula)* |
| P25 | 4번 하부보강재 공식1 | Length | `if (fabs(P10-P12)) < 100 then max(P17,P15) else P17 endif` *(formula)* |
| P26 | 보강재 어퍼 간격 | Length | `30.00` |
| P27 | 2번 하부보강재 공식1 | Length | `if (fabs(P10-P12)) < 100 then max(P17,P15) else P15 endif` *(formula)* |
| P28 | 3번 하부보강재 공식1 | Length | `if (fabs(P11-P9)) < 100 && (fabs(P11-P10)) < 100 && (fabs(P11-P12)) < 100 then P18 else P29 endif` *(formula)* |
| P29 | 3번 하부보강재 공식2 | Length | `if (fabs(P11-P9)) < 100 then max(P16,P14) else P30 endif` *(formula)* |
| P30 | 3번 하부보강재 공식3 | Length | `if (fabs(P11-P10)) < 100 then max(P16,P15) else P31 endif` *(formula)* |
| P31 | 3번 하부보강재 공식4 | Length | `if (fabs(P11-P12)) < 100 then max(P16,P17) else P16 endif` *(formula)* |
| P32 | Parameter32 | Profile | `if P13==P9 then "PL"+P4 else "PL"+P47 endif` *(formula)* |
| P33 | 2번 부재 폭 참조 | Yes/No | `0` |
| P34 | Upper Add Plate | Integer | `1` |
| P35 | Side Add Plate | Integer | `0` |
| P36 | Parameter36 | Yes/No | `if (P12 == P9) \|\| (P12 > P9) \|\| (fabs(P12-P9)) < 100 \|\| (fabs(P12-P10)) < 100 then 0 else P39 endif` *(formula)* |
| P37 | Upp 옵션 | Yes/No | `if P34==1 then 1 else 0 endif` *(formula)* |
| P38 | Side 옵션 | Yes/No | `if P34!=1 then 1 else 0 endif` *(formula)* |
| P39 | Parameter39 | Yes/No | `if (P12 < P9) && ((fabs(P12-P9)) >= 100) && ((fabs(P12-P11)) >= 100) then 1 else 0 endif` *(formula)* |
| P40 | 1 | Text | `1` |
| P41 | 2 | Text | `` |
| P42 | 3 | Length | `` |
| P43 | 1번 부재 하부두께공식1 | Length | `if (fabs(P9-P10)) < 100 then max(P14,P15) else P44 endif` *(formula)* |
| P44 | 1번 부재 하부두께공식2 | Length | `if (fabs(P9-P11)) < 100 then max(P14,P16) else P45 endif` *(formula)* |
| P45 | 1번 부재 하부두께공식3 | Length | `if (fabs(P9-P12)) < 100 then max(P14,P17) else P14 endif` *(formula)* |
| P46 | 2번 부재 하부두께공식1 | Length | `if (fabs(P9-P10)) < 100 then max(P9,P10) else P44 endif` *(formula)* |
| P47 | 1번 부재 하부두께공식 최종 | Length | `if P13 == P9 then P43 else P21 endif` *(formula)* |
| P55 | Parameter55 | Length | `if (fabs(P10-P12)) < 100 then max(P15,P17) else P56 endif` *(formula)* |
| P56 | Parameter56 | Length | `if (fabs(P10-P11)) < 100 then max(P15,P16) else P15 endif` *(formula)* |
| P57 | Parameter57 | Yes/No | `if  P11 ==P9 then 0 else 1 endif` *(formula)* |
| P58 | Parameter58 | Yes/No | `if P11== P9 then 0 else 1 endif` *(formula)* |
| P59 | 2번 보강재 온장 변수1 | Yes/No | `if (P10 > P9) && (fabs(P10-P9) >= 100) then 1 else 0 endif` *(formula)* |
| P60 | 2번 보강재 온장 변수2 | Yes/No | `if (P10 == P9) \|\| (P10==P11) \|\| (fabs(P10-P11)) < 100 \|\| (fabs(P10-P9)) < 100 then 0 else P59 endif` *(formula)* |
| P61 | Parameter61 | Yes/No | `if (P11==P9) \|\| ((fabs(P11-P9)) < 100) then 0 else 1 endif` *(formula)* |
| P62 | Parameter62 | Yes/No | `if (P10 > P9) \|\| (P10 > P12)  then 1 else P80 endif` *(formula)* |
| P63 | Parameter63 | Yes/No | `if (P11 < P9) && ((fabs(P11-P9)) >= 100) then 1 else P104 endif` *(formula)* |
| P64 | Parameter64 | Yes/No | `if (P12 == P9) \|\| (P12 == P11) \|\| (P12 == P10) then 0 else P65 endif` *(formula)* |
| P65 | Parameter65 | Yes/No | `if (P12 > P9) && (fabs(P12-P9) >= 100) then 1 else 0 endif` *(formula)* |
| P66 | Parameter66 | Yes/No | `if ((fabs(P12 - P9)) >= 100)  then 0 else P67 endif` *(formula)* |
| P67 | Parameter67 | Yes/No | `if ((fabs(P12-P11)) < 100) then 0 else P68 endif` *(formula)* |
| P68 | Parameter68 | Yes/No | `if P12 < P9 then 1 else 0 endif` *(formula)* |
| P69 | Parameter69 | Yes/No | `if (P12 == P9) \|\| (P12 <= P11) \|\| (P12 == P10) \|\| ((fabs(P12 - P9)) < 100) \|\| ((fabs(P12 - P10)) < 100) then 0 else P70 endif` *(formula)* |
| P70 | Parameter70 | Yes/No | `if (P12 > P11) && ((fabs(P12 - P11)) >= 100)  then 1 else 0 endif` *(formula)* |
| P71 | Parameter71 | Yes/No | `if (P12 < P11) && ((fabs(P12-P11)) >= 100) then 1 else P72 endif` *(formula)* |
| P72 | Parameter72 | Yes/No | `if (P12 == P11) \|\| (P12 == P10) \|\| (P12 > P11) then 0 else 1 endif` *(formula)* |
| P73 | Parameter73 | Yes/No | `if (fabs(P12 - P11)) < 100 && P12 < P11 then 1 else 0 endif` *(formula)* |
| P74 | Parameter74 | Yes/No | `if (P11 > P9) && (P11 > P12) && (P11 > P10) then 0 else P76 endif` *(formula)* |
| P75 | Parameter75 | Yes/No | `if P9 > P11 then 0 else P77 endif` *(formula)* |
| P76 | Parameter76 | Yes/No | `if (fabs(P9 - P11)) >= 100 && P11 < P9 then 1 else 0 endif` *(formula)* |
| P77 | Parameter77 | Yes/No | `if (P9 == P10) \|\| (P9 == P11) \|\| (P9 == P12) then 0 else P78 endif` *(formula)* |
| P78 | Parameter78 | Yes/No | `if P9 < P11 && (fabs(P9 - P11)) >= 100 then 1 else 0 endif` *(formula)* |
| P79 | Parameter79 | Yes/No | `=if ((fabs(P11-P10)) >= 100) && ` |
| P80 | Parameter80 | Yes/No | `if (P10 == P9) \|\| (P10 == P11) then 0 else P81 endif` *(formula)* |
| P81 | Parameter81 | Yes/No | `if (P10 < P9) && (fabs(P9-P10)) >= 100 then 1 else 0 endif` *(formula)* |
| P82 | Parameter82 | Yes/No | `if (P11 < P9) && (fabs(P11-P9)) >= 100 then 1 else 0 endif` *(formula)* |
| P83 | Parameter83 | Yes/No | `if P11 > P9 && (fabs(P11-P9)) >= 100 then 1 else 0 endif` *(formula)* |
| P84 | Parameter84 | Yes/No | `if (P10 == P9) \|\| (P10==P11) \|\| (fabs(P10-P9)) < 100 \|\| (P10 < P11) then 0 else P85 endif` *(formula)* |
| P85 | Parameter85 | Yes/No | `if (P10 > P11) && (fabs(P10-P11)) >= 100 then 1 else 0 endif` *(formula)* |
| P86 | Parameter86 | Yes/No | `if (P10>=P11) \|\| (fabs(P10-P11)) < 100 then 0 else P87 endif` *(formula)* |
| P87 | Parameter87 | Yes/No | `if  (fabs(P10-P11)) >= 100 then 1 else 0 endif` *(formula)* |
| P88 | Parameter88 | Yes/No | `=if (P10 < P9) && (fabs(P10-P9)) >= 100 then 1 else P86 endif -----------------------------------` |
| P89 | Parameter89 | Yes/No | `if P86==1 &&((fabs(P12-P10)) > 100) then 0 else P71 endif` *(formula)* |
| P90 | Parameter90 | Yes/No | `if P12 == (max(P9,P10,P11)) \|\| ((fabs(P12 - P9)) < 100) \|\| (fabs(P11-P12)) < 100 \|\| (fabs(P10-P12)) < 100 \|\| (P12>P9) \|\| P12==P10 then 0 else 1 endif` *(formula)* |
| P91 | Parameter91 | Yes/No | `if P10 > P12 then P62 else 0 endif` *(formula)* |
| P92 | Parameter92 | Yes/No | `if (fabs(P12-P11)) >= 100 then P93 else 0 endif` *(formula)* |
| P93 | Parameter93 | Yes/No | `if (P12 > P9) && (P12 > P10) && (P12 > P11) then 0 else P94 endif` *(formula)* |
| P94 | Parameter94 | Yes/No | `if (P12 == P9) \|\| (P12 == P11) \|\| (P12 == P10) \|\| (fabs(P12-P9)) < 100 \|\| (fabs(P10-P12)) < 100 then 0 else P95 endif` *(formula)* |
| P95 | Parameter95 | Yes/No | `if (fabs(P12 - P11)) >= 100 && P12 < P11 then 1 else 0 endif` *(formula)* |
| P96 | Parameter96 | Yes/No | `if (P10 < P9) && ((fabs(P10-P9)) >= 100) && ((fabs(P10-P11)) >= 100) then 1 else 0 endif` *(formula)* |
| P97 | Parameter97 | Yes/No | `if (P10 == P9) \|\| (P10==P11) then 0 else P88 endif` *(formula)* |
| P98 | Parameter98 | Yes/No | `if (P10 == P12) \|\| ((fabs(P10 -P12)) >= 100) then 1 else 0 endif` *(formula)* |
| P99 | Parameter99 | Yes/No | `if (P10 < P11) && (fabs(P10-P11)) < 100 then 0 else 1 endif` *(formula)* |
| P100 | Parameter100 | Yes/No | `if (fabs(P10 - P12)) > 100 && ((fabs(P19 -P10)) >= 100) then 1 else 0 endif` *(formula)* |
| P101 | Parameter101 | Yes/No | `if (fabs(P10 - P12)) < 100 && P10 < P12 && (fabs(P10-P9)) >= 100  then 1 else 0 endif` *(formula)* |
| P102 | Parameter102 | Yes/No | `if P11 > P9 then 0 else 1 endif` *(formula)* |
| P103 | Parameter103 | Yes/No | `if (P10 == P9) \|\| (P10 > P9) \|\| ((fabs(P10-P9)) < 100) then 0 else P96 endif` *(formula)* |
| P104 | Parameter104 | Yes/No | `if P11==P10 \|\| P11==P12 \|\| ((fabs(P11-P9)) < 100) then 0 else 1 endif` *(formula)* |
| P105 | Parameter105 | Length | `16.00` |
| P106 | 3번 하부두께공식1 | Profile | `if (P4 !="") then P107 else "PL"+P108 endif` *(formula)* |
| P107 | 3번 하부두께공식2 | Profile | `if P13 == P11 then "PL"+P4 else "PL"+P108 endif` *(formula)* |
| P108 | 3번 하부두께공식3 | Length | `if P13 == P11 then P109 else P112 endif` *(formula)* |
| P109 | 3번 하부두께공식4 | Length | `if (fabs(P11-P9)) < 100 then max(P14,P16) else P110 endif` *(formula)* |
| P110 | 3번 하부두께공식5 | Length | `if (fabs(P11-P10)) < 100 then max(P15,P16) else P111 endif` *(formula)* |
| P111 | 3번 하부두께공식6 | Length | `if (fabs(P11-P12)) < 100 then max(P16,P17) else P16 endif` *(formula)* |
| P112 | 3번 하부두께공식7 | Length | `if (fabs(P11-P10)) < 100 && (fabs(P11-P9)) < 100 && (fabs(P11-P12)) < 100 then P18 else P113 endif` *(formula)* |
| P113 | 3번 하부두께공식8 | Length | `if (fabs(P11-P9)) < 100 then max(P14,P16) else P114 endif` *(formula)* |
| P114 | 3번 하부두께공식9 | Length | `if (fabs(P11-P10)) < 100 then max(P15,P16) else P115 endif` *(formula)* |
| P115 | 3번 하부두께공식10 | Length | `if (fabs(P11-P12)) < 100 then max(P16,P17) else P16 endif` *(formula)* |
| P116 | Parameter116 | Yes/No | `1` |
| P117 | 4번 하부두께공식1 | Profile | `if (P4 !="") then P118 else "PL"+P119 endif` *(formula)* |
| P118 | 4번 하부두께공식2 | Profile | `if P13 == P12 then "PL"+P4 else "PL"+P119 endif` *(formula)* |
| P119 | 4번 하부두께공식3 | Length | `if P13 == P12 then P120 else P124 endif` *(formula)* |
| P120 | 4번 하부두께공식4 | Length | `if (fabs(P12-P9)) < 100 then max(P14,P17) else P121 endif` *(formula)* |
| P121 | 4번 하부두께공식5 | Length | `if (fabs(P12-P10)) < 100 then max(P15,P17) else P122 endif` *(formula)* |
| P122 | 4번 하부두께공식6 | Length | `if (fabs(P12-P11)) < 100 then max(P16,P17) else P17 endif` *(formula)* |
| P123 | 4번 하부두께공식7 | Length | `if (fabs(P12-P9)) < 100 && (fabs(P12-P10)) < 100 && (fabs(P11-P12)) < 100 then P18 else P124 endif` *(formula)* |
| P124 | 4번 하부두께공식8 | Length | `if (fabs(P12-P9)) < 100 then max(P14,P17) else P125 endif` *(formula)* |
| P125 | 4번 하부두께공식9 | Length | `if (fabs(P12-P10)) < 100 then max(P15,P17) else P126 endif` *(formula)* |
| P126 | 4번 하부두께공식10 | Length | `if (fabs(P11-P12)) < 100 then max(P16,P17) else P17 endif` *(formula)* |
| P127 | Parameter127 | Yes/No | `if P128==1 then P36 else 0 endif` *(formula)* |
| P128 | 쪽쪽이 | Yes/No | `1` |
| P129 | Parameter129 | Profile | `if P4=="" then P3 else "PL"+P4 endif` *(formula)* |
| P130 | Parameter130 | Yes/No | `if P128==1 then P92 else 0 endif` *(formula)* |
| P131 | Parameter131 | Profile | `if P4=="" then P8 else "PL"+P6 endif` *(formula)* |
| P132 | Parameter132 | Yes/No | `if P128==1 then P63 else 0 endif` *(formula)* |
| P133 | Side Add Plate 공식1 | Yes/No | `if P34==1 then 0 else P139 endif` *(formula)* |
| P134 | Parameter134 | Yes/No | `if P128==1 then P103 else 0 endif` *(formula)* |
| P135 | Side Add Plate 옵션1 | Integer | `1` |
| P136 | Parameter136 | Yes/No | `if P128==1 then P86 else 0 endif` *(formula)* |
| P138 | Parameter138 | Yes/No | `if P128==1 then P75 else 0 endif` *(formula)* |
| P139 | Side Add Plate 공식2 | Yes/No | `if P135==1 then 1 else 0 endif` *(formula)* |
| P140 | 2번부재 두께는 | Text | `` |
| P141 | 3번부재 두께는 | Text | `` |
| P142 | 4번부재 두께는 | Text | `` |
| P143 | 1번부재 두께는 | Text | `` |
| P144 | 보강재 챔퍼 타입 | Chamfer | `3` |
| P145 | 보강재 챔퍼 크기 | Length | `30.00` |
| P146 | Parameter146 | Yes/No | `if P34==1 then 0 else P147 endif` *(formula)* |
| P147 | Parameter147 | Yes/No | `if P135 !=1 then 1 else 0 endif` *(formula)* |
| P148 | Pos_No | Text | `PL` |
| P149 | Pos_No_Start No | Text | `1` |
| P150 | 재질 | Material | `SS400` |
| P151 | 이름 | Text | `PLATE` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.BRACKET | `` |
| D2 | D2.PLATE.BRACKET | `` |
| D3 | D3.PLATE.BRACKET | `` |
| D4 | D4.PLATE.BRACKET | `` |
| D5 | D5.PLATE.BRACKET | `` |
| D6 | D6.PLATE.BRACKET | `` |
| D7 | D7.PLATE.BRACKET | `` |
| D8 | D8.PLATE.BRACKET | `` |
| D9 | D9.PLATE.BRACKET | `` |
| D10 | D10.PLATE.BRACKET | `` |
| D11 | D11.PLATE.BRACKET | `` |
| D12 | D12.PLATE.BRACKET | `` |
| D13 | D13.PLATE.BRACKET | `` |
| D14 | D14.PLATE.BRACKET | `` |
| D15 | D15.PLATE.BRACKET | `` |
| D16 | D16.PLATE.BRACKET | `` |
| D17 | D17.PLATE.COLUMN | `` |
| D18 | D18.PLATE.COLUMN | `` |
| D19 | D19.PLATE.COLUMN | `` |
| D20 | D20.PLATE.COLUMN | `` |
| D21 | D21.PLATE.COLUMN | `` |
| D22 | D22.PLATE.COLUMN | `` |
| D23 | D23.PLATE.COLUMN | `` |
| D24 | D24.PLATE.COLUMN | `` |
| D25 | D25.PLATE.COLUMN | `` |
| D26 | D26.PLATE.COLUMN | `P26` |
| D27 | D27.PLATE.COLUMN | `` |
| D28 | D28.PLATE.COLUMN | `` |
| D29 | D29.PLATE.COLUMN | `` |
| D30 | D30.PLATE.COLUMN | `P26` |
| D31 | D31.PLATE.COLUMN | `` |
| D32 | D32.PLATE.COLUMN | `` |
| D33 | D33.PLATE.COLUMN | `` |
| D34 | D34.PLATE.COLUMN | `P26` |
| D35 | D35.PLATE.COLUMN | `` |
| D36 | D36.PLATE.COLUMN | `` |
| D37 | D37.PLATE.COLUMN | `` |
| D38 | D38.PLATE.COLUMN | `` |
| D39 | D39.PLATE.COLUMN | `` |
| D40 | D40.PLATE.COLUMN | `P26` |
| D41 | D41.PLATE.COLUMN | `P26` |
| D42 | D42.PLATE.COLUMN | `P26` |
| D43 | D43.PLATE.COLUMN | `P26` |
| D44 | D44.PLATE.COLUMN | `P26` |
| D45 | D45.PLATE.COLUMN | `` |
| D46 | D46.PLATE.COLUMN | `` |
| D47 | D47.PLATE.COLUMN | `` |
| D48 | D48.PLATE.COLUMN | `` |
| D49 | D49.PLATE.COLUMN | `` |
| D50 | D50.PLATE.COLUMN | `` |
| D51 | D51.PLATE.COLUMN | `` |
| D52 | D52.PLATE.COLUMN | `` |
| D53 | D53.PLATE.COLUMN | `` |
| D54 | D54.PLATE.COLUMN | `` |
| D55 | D55.PLATE.BRACKET | `` |
| D56 | D56.PLATE.BRACKET | `` |
| D57 | D57.PLATE.BRACKET | `` |
| D58 | D58.PLATE.BRACKET | `` |
| D59 | D59.PLATE.BRACKET | `` |
| D60 | D60.PLATE.BRACKET | `` |
| D61 | D61.PLATE.BRACKET | `` |
| D62 | D62.PLATE.BRACKET | `` |
| D63 | D63.PLATE.BRACKET | `` |
| D64 | D64.PLATE.BRACKET | `` |
| D65 | D65.PLATE.BRACKET | `` |
| D66 | D66.PLATE.BRACKET | `` |
| D67 | D67.PLATE.COLUMN | `` |
| D68 | D68.PLATE.COLUMN | `` |
| D69 | D69.PLATE.COLUMN | `` |
| D70 | D70.PLATE.COLUMN | `` |
| D71 | D71.PLATE.COLUMN | `` |
| D72 | D72.PLATE.COLUMN | `` |
| D73 | D73.PLATE.COLUMN | `` |
| D74 | D74.PLATE.COLUMN | `` |
| D75 | D75.PLATE.COLUMN | `` |
| D76 | D76.PLATE.COLUMN | `` |
| D77 | D77.PLATE.COLUMN | `` |
| D78 | D78.PLATE.COLUMN | `` |
| D79 | D79.PLATE.COLUMN | `` |
| D80 | D80.PLATE.COLUMN | `` |
| D81 | D81.PLATE.COLUMN | `` |
| D82 | D82.PLATE.COLUMN | `` |
| D83 | D83.PLATE.COLUMN | `` |
| D84 | D84.PLATE.COLUMN | `` |
| D85 | D85.PLATE.COLUMN | `` |
| D86 | D86.PLATE.COLUMN | `` |
| D87 | D87.PLATE.COLUMN | `` |
| D88 | D88.PLATE.COLUMN | `` |
| D89 | D89.PLATE.COLUMN | `` |
| D90 | D90.PLATE.COLUMN | `` |
| D91 | D91.PLATE.COLUMN | `` |
| D92 | D92.PLATE.COLUMN | `` |
| D93 | D93.PLATE.COLUMN | `` |
| D94 | D94.PLATE.COLUMN | `` |
| D95 | D95.PLATE.COLUMN | `` |
| D96 | D96.PLATE.COLUMN | `` |
| D97 | D97.PLATE.COLUMN | `` |
| D98 | D98.PLATE.COLUMN | `` |
| D99 | D99.PLATE.COLUMN | `` |
| D100 | D100.PLATE.COLUMN | `` |
| D101 | D101.PLATE.COLUMN | `` |
| D102 | D102.PLATE.COLUMN | `` |
| D103 | D103.PLATE.COLUMN | `` |
| D104 | D104.PLATE.COLUMN | `` |
| D105 | D105.PLATE.COLUMN | `` |
| D106 | D106.PLATE.COLUMN | `` |
| D107 | D107.PLATE.COLUMN | `` |
| D108 | D108.PLATE.COLUMN | `` |
| D109 | D109.PLATE.COLUMN | `` |
| D110 | D110.PLATE.COLUMN | `` |
| D111 | D111.PLATE.COLUMN | `` |
| D112 | D112.PLATE.COLUMN | `` |
| D113 | D113.PLATE.COLUMN | `` |
| D114 | D114.PLATE.COLUMN | `` |
| D115 | D115.PLATE.COLUMN | `` |
| D116 | D116.PLATE.COLUMN | `` |
| D117 | D117.PLATE.COLUMN | `` |
| D118 | D118.PLATE.COLUMN | `` |
| D119 | D119.PLATE.COLUMN | `` |
| D120 | D120.PLATE.COLUMN | `` |
| D121 | D121.PLATE.BRACKET | `` |
| D122 | D122.PLATE.BRACKET | `` |
| D123 | D123.PLATE.BRACKET | `` |
| D124 | D124.PLATE.BRACKET | `` |
| D125 | D125.PLATE.BRACKET | `` |
| D126 | D126.PLATE.BRACKET | `` |
| D127 | D127.PLATE.BRACKET | `` |
| D128 | D128.PLATE.BRACKET | `` |
| D129 | D129.PLATE.BRACKET | `` |
| D130 | D130.PLATE.BRACKET | `` |
| D131 | D131.PLATE.BRACKET | `` |
| D132 | D132.PLATE.BRACKET | `` |
| D133 | D133.PLATE.BRACKET | `` |
| D134 | D134.PLATE.BRACKET | `` |
| D135 | D135.PLATE.BRACKET | `` |
| D136 | D136.PLATE.BRACKET | `` |
| D137 | D137.PLATE.BRACKET | `` |
| D138 | D138.PLATE.BRACKET | `` |
| D139 | D139.PLATE.BRACKET | `` |
| D140 | D140.PLATE.BRACKET | `` |
| D141 | D141.PLATE.BRACKET | `` |
| D142 | D142.PLATE.BRACKET | `` |
| D143 | D143.PLATE.BRACKET | `` |
| D144 | D144.PLATE.BRACKET | `` |
| D145 | D145.PLATE.BRACKET | `` |
| D146 | D146.PLATE.BRACKET | `` |
| D147 | D147.PLATE.BRACKET | `` |
| D148 | D148.PLATE.BRACKET | `` |
| D149 | D149.PLATE.BRACKET | `` |
| D150 | D150.PLATE.BRACKET | `` |
| D151 | D151.PLATE.BRACKET | `` |
| D152 | D152.PLATE.BRACKET | `` |
| D153 | D153.PLATE.BRACKET | `` |
| D154 | D154.PLATE.BRACKET | `` |
| D155 | D155.PLATE.BRACKET | `` |
| D156 | D156.PLATE.BRACKET | `` |
| D157 | D157.PLATE.BRACKET | `` |
| D158 | D158.PLATE.BRACKET | `` |
| D159 | D159.PLATE.BRACKET | `` |
| D160 | D160.PLATE.BRACKET | `` |
| D161 | D161.PLATE.BRACKET | `` |
| D162 | D162.PLATE.BRACKET | `` |
| D163 | D163.PLATE.BRACKET | `` |
| D164 | D164.PLATE.BRACKET | `` |
| D165 | D165.PLATE.BRACKET | `` |
| D166 | D166.PLATE.BRACKET | `` |
| D167 | D167.PLATE.BRACKET | `` |
| D168 | D168.PLATE.BRACKET | `` |
| D169 | D169.PLATE.BRACKET | `` |
| D170 | D170.PLATE.BRACKET | `` |
| D171 | D171.PLATE.BRACKET | `` |
| D172 | D172.PLATE.BRACKET | `` |
| D173 | D173.PLATE.BRACKET | `` |
| D174 | D174.PLATE.BRACKET | `` |
| D175 | D175.PLATE.BRACKET | `` |
| D176 | D176.PLATE.BRACKET | `` |
| D177 | D177.PLATE.BRACKET | `` |
| D178 | D178.PLATE.BRACKET | `` |
| D179 | D179.PLATE.BRACKET | `` |
| D180 | D180.PLATE.BRACKET | `` |
| D181 | D181.PLATE.BRACKET | `` |
| D182 | D182.PLATE.BRACKET | `` |
| D183 | D183.PLATE.BRACKET | `` |
| D184 | D184.PLATE.BRACKET | `` |
| D185 | D185.PLATE.BRACKET | `` |
| D186 | D186.PLATE.BRACKET | `` |
| D187 | D187.PLATE.BRACKET | `` |
| D188 | D188.PLATE.BRACKET | `` |
| D189 | D189.PLATE.COLUMN | `` |
| D190 | D190.PLATE.COLUMN | `` |
| D191 | D191.PLATE.COLUMN | `` |
| D192 | D192.PLATE.COLUMN | `` |
| D193 | D193.PLATE.COLUMN | `` |
| D194 | D194.PLATE.COLUMN | `` |
| D195 | D195.PLATE.COLUMN | `` |
| D196 | D196.PLATE.COLUMN | `` |
| D197 | D197.PLATE.BRACKET | `` |
| D198 | D198.PLATE.BRACKET | `` |
| D199 | D199.PLATE.BRACKET | `` |
| D200 | D200.PLATE.BRACKET | `` |
| D201 | D201.PLATE.BRACKET | `` |
| D202 | D202.PLATE.BRACKET | `` |
| D203 | D203.PLATE.BRACKET | `` |
| D204 | D204.PLATE.BRACKET | `` |
| D205 | D205.PLATE.COLUMN | `` |
| D206 | D206.PLATE.COLUMN | `` |
| D207 | D207.PLATE.COLUMN | `` |
| D208 | D208.PLATE.COLUMN | `` |
| D209 | D209.PLATE.BRACKET | `` |
| D210 | D210.PLATE.BRACKET | `` |
| D211 | D211.PLATE.BRACKET | `` |
| D212 | D212.PLATE.BRACKET | `` |
| D213 | D213.PLATE.COLUMN | `` |
| D214 | D214.PLATE.COLUMN | `` |
| D215 | D215.PLATE.COLUMN | `` |
| D216 | D216.PLATE.COLUMN | `` |
| D217 | D217.PLATE.COLUMN | `` |
| D218 | D218.PLATE.COLUMN | `` |
| D219 | D219.PLATE.COLUMN | `` |
| D220 | D220.PLATE.COLUMN | `` |
| D221 | D221.PLATE.COLUMN | `` |
| D222 | D222.PLATE.COLUMN | `` |
| D223 | D223.PLATE.COLUMN | `` |
| D224 | D224.PLATE.COLUMN | `` |
| D225 | D225.PLATE.BRACKET | `` |
| D226 | D226.PLATE.BRACKET | `` |
| D227 | D227.PLATE.BRACKET | `` |
| D228 | D228.PLATE.BRACKET | `` |
| D229 | D229.PLATE.COLUMN | `` |
| D230 | D230.PLATE.COLUMN | `` |
| D231 | D231.PLATE.COLUMN | `` |
| D232 | D232.PLATE.COLUMN | `` |
| D233 | D233.PLATE.COLUMN | `` |
| D234 | D234.PLATE.COLUMN | `` |
| D235 | D235.PLATE.COLUMN | `` |
| D236 | D236.PLATE.COLUMN | `` |
| D237 | D237.PLATE.COLUMN | `` |
| D238 | D238.PLATE.COLUMN | `` |
| D239 | D239.PLATE.COLUMN | `` |
| D240 | D240.PLATE.COLUMN | `` |
| D241 | D241.PLATE.COLUMN | `` |
| D242 | D242.PLATE.COLUMN | `` |
| D243 | D243.PLATE.COLUMN | `` |
| D244 | D244.PLATE.COLUMN | `` |
| D245 | D245.PLATE.COLUMN | `` |
| D246 | D246.PLATE.COLUMN | `` |
| D247 | D247.PLATE.COLUMN | `` |
| D248 | D248.PLATE.COLUMN | `` |
| D249 | D249.PLATE.COLUMN | `` |
| D250 | D250.PLATE.COLUMN | `` |
| D251 | D251.PLATE.COLUMN | `` |
| D252 | D252.PLATE.COLUMN | `` |
| D253 | D253.PLATE.COLUMN | `` |
| D254 | D254.PLATE.COLUMN | `` |
| D255 | D255.PLATE.COLUMN | `` |
| D256 | D256.PLATE.COLUMN | `` |
| D257 | D257.PLATE.COLUMN | `` |
| D258 | D258.PLATE.COLUMN | `` |
| D259 | D259.PLATE.COLUMN | `` |
| D260 | D260.PLATE.COLUMN | `` |
| D261 | D261.PLATE.COLUMN | `` |
| D262 | D262.PLATE.COLUMN | `` |
| D263 | D263.PLATE.COLUMN | `` |
| D264 | D264.PLATE.COLUMN | `` |
| D265 | D265.PLATE.BRACKET | `` |
| D266 | D266.PLATE.BRACKET | `` |
| D267 | D267.PLATE.BRACKET | `` |
| D268 | D268.PLATE.BRACKET | `` |
| D269 | D269.PLATE.BRACKET | `` |
| D270 | D270.PLATE.BRACKET | `` |
| D271 | D271.PLATE.BRACKET | `` |
| D272 | D272.PLATE.BRACKET | `` |
| D273 | D273.PLATE.COLUMN | `` |
| D274 | D274.PLATE.COLUMN | `` |
| D275 | D275.PLATE.COLUMN | `` |
| D276 | D276.PLATE.COLUMN | `` |
| D277 | D277.PLATE.COLUMN | `` |
| D278 | D278.PLATE.COLUMN | `` |
| D279 | D279.PLATE.BRACKET | `` |
| D280 | D280.PLATE.BRACKET | `` |
| D281 | D281.PLATE.COLUMN | `` |
| D282 | D282.PLATE.COLUMN | `` |
| D283 | D283.PLATE.COLUMN | `` |
| D284 | D284.PLATE.COLUMN | `` |
| D285 | D285.PLATE.BRACKET | `` |
| D286 | D286.PLATE.BRACKET | `` |
| D287 | D287.PLATE.COLUMN | `` |
| D288 | D288.PLATE.COLUMN | `` |
| D289 | D289.PLATE.COLUMN | `` |
| D290 | D290.PLATE.COLUMN | `` |
| D291 | D291.PLATE.COLUMN | `` |
| D292 | D292.PLATE.COLUMN | `` |
| D293 | D293.PLATE.BRACKET | `` |
| D294 | D294.PLATE.BRACKET | `` |
| D295 | D295.PLATE.BRACKET | `` |
| D296 | D296.PLATE.BRACKET | `` |
| D297 | D297.PLATE.BRACKET | `` |
| D298 | D298.PLATE.BRACKET | `` |
| D299 | D299.PLATE.BRACKET | `` |
| D300 | D300.PLATE.BRACKET | `` |
| D301 | D301.PLATE.COLUMN | `` |
| D302 | D302.PLATE.COLUMN | `` |
| D303 | D303.PLATE.COLUMN | `` |
| D304 | D304.PLATE.COLUMN | `` |
| D305 | D305.PLATE.COLUMN | `` |
| D306 | D306.PLATE.COLUMN | `` |
| D307 | D307.PLATE.COLUMN | `` |
| D308 | D308.PLATE.COLUMN | `` |
| D309 | D309.PLATE.COLUMN | `` |
| D310 | D310.PLATE.COLUMN | `` |
| D311 | D311.PLATE.BRACKET | `` |
| D312 | D312.PLATE.BRACKET | `` |
| D313 | D313.PLATE.BRACKET | `` |
| D314 | D314.PLATE.BRACKET | `` |
| D315 | D315.PLATE.BRACKET | `` |
| D316 | D316.PLATE.BRACKET | `` |
| D317 | D317.PLATE.BRACKET | `` |
| D318 | D318.PLATE.BRACKET | `` |
| D319 | D319.PLATE.BRACKET | `` |
| D320 | D320.PLATE.BRACKET | `` |
| D321 | D321.PLATE.BRACKET | `` |
| D322 | D322.PLATE.BRACKET | `` |
| D323 | D323.PLATE.COLUMN | `` |
| D324 | D324.PLATE.COLUMN | `` |
| D325 | D325.PLATE.COLUMN | `` |
| D326 | D326.PLATE.COLUMN | `` |
| D327 | D327.PLATE.COLUMN | `` |
| D328 | D328.PLATE.COLUMN | `` |
| D329 | D329.PLATE.COLUMN | `` |
| D330 | D330.PLATE.COLUMN | `` |
| D331 | D331.PLATE.COLUMN | `` |
| D332 | D332.PLATE.COLUMN | `` |
| D333 | D333.PLATE.COLUMN | `` |
| D334 | D334.PLATE.COLUMN | `` |
| D335 | D335.PLATE.BRACKET | `` |
| D336 | D336.PLATE.BRACKET | `` |
| D337 | D337.PLATE.BRACKET | `` |
| D338 | D338.PLATE.BRACKET | `` |
| D339 | D339.PLATE.COLUMN | `` |
| D340 | D340.PLATE.COLUMN | `` |
| D341 | D341.PLATE.COLUMN | `` |
| D342 | D342.PLATE.COLUMN | `` |
| D343 | D343.PLATE.COLUMN | `` |
| D344 | D344.PLATE.COLUMN | `` |
| D345 | D345.PLATE.COLUMN | `` |
| D346 | D346.PLATE.COLUMN | `` |
| D347 | D347.PLATE.BRACKET | `` |
| D348 | D348.PLATE.BRACKET | `` |
| D349 | D349.PLATE.BRACKET | `` |
| D350 | D350.PLATE.BRACKET | `` |
| D351 | D351.PLATE.BRACKET | `` |
| D352 | D352.PLATE.BRACKET | `` |
| D353 | D353.PLATE.BRACKET | `` |
| D354 | D354.PLATE.BRACKET | `` |
| D355 | D355.PLATE.COLUMN | `` |
| D356 | D356.PLATE.COLUMN | `` |
| D357 | D357.PLATE.BRACKET | `` |
| D358 | D358.PLATE.BRACKET | `` |
| D359 | D359.PLATE.BRACKET | `` |
| D360 | D360.PLATE.BRACKET | `` |
| D361 | D361.PLATE.BRACKET | `` |
| D362 | D362.PLATE.BRACKET | `` |
| D363 | D363.PLATE.COLUMN | `P26` |
| D364 | D364.PLATE.COLUMN | `P26` |
| D365 | D365.PLATE.COLUMN | `` |
| D366 | D366.PLATE.COLUMN | `` |
| D367 | D367.PLATE.COLUMN | `` |
| D368 | D368.PLATE.COLUMN | `` |
| D369 | D369.PLATE.COLUMN | `P26` |
| D370 | D370.PLATE.COLUMN | `P26` |
| D371 | D371.PLATE.BRACKET | `` |
| D372 | D372.PLATE.BRACKET | `` |
| D373 | D373.PLATE.COLUMN | `` |
| D374 | D374.PLATE.COLUMN | `` |
| D375 | D375.PLATE.BRACKET | `` |
| D376 | D376.PLATE.BRACKET | `` |
| D377 | D377.PLATE.BRACKET | `` |
| D378 | D378.PLATE.BRACKET | `` |
| D379 | D379.PLATE.BRACKET | `` |
| D380 | D380.PLATE.BRACKET | `` |
| D381 | D381.PLATE.BRACKET | `` |
| D382 | D382.PLATE.BRACKET | `` |
| D383 | D383.PLATE.BRACKET | `` |
| D384 | D384.PLATE.BRACKET | `` |
| D385 | D385.PLATE.COLUMN | `` |
| D386 | D386.PLATE.COLUMN | `` |
| D387 | D387.PLATE.COLUMN | `` |
| D388 | D388.PLATE.COLUMN | `` |
| D389 | D389.PLATE.COLUMN | `P26` |
| D390 | D390.PLATE.COLUMN | `P26` |
| D391 | D391.PLATE.COLUMN | `` |
| D392 | D392.PLATE.COLUMN | `` |
| D393 | D393.PLATE.COLUMN | `P26` |
| D394 | D394.PLATE.COLUMN | `P26` |
| D395 | D395.PLATE.BRACKET | `` |
| D396 | D396.PLATE.BRACKET | `` |
| D397 | D397.PLATE.COLUMN | `` |
| D398 | D398.PLATE.COLUMN | `` |
| D399 | D399.PLATE.BRACKET | `` |
| D400 | D400.PLATE.BRACKET | `` |
| D401 | D401.PLATE.BRACKET | `` |
| D402 | D402.PLATE.BRACKET | `` |
| D403 | D403.PLATE.BRACKET | `` |
| D404 | D404.PLATE.BRACKET | `` |
| D405 | D405.PLATE.BRACKET | `` |
| D406 | D406.PLATE.BRACKET | `` |
| D407 | D407.PLATE.COLUMN | `` |
| D408 | D408.PLATE.COLUMN | `` |
| D409 | D409.PLATE.BRACKET | `` |
| D410 | D410.PLATE.BRACKET | `` |
| D411 | D411.PLATE.COLUMN | `` |
| D412 | D412.PLATE.COLUMN | `` |
| D413 | D413.PLATE.COLUMN | `P26` |
| D414 | D414.PLATE.COLUMN | `P26` |
| D415 | D415.PLATE.COLUMN | `` |
| D416 | D416.PLATE.COLUMN | `` |
| D417 | D417.PLATE.COLUMN | `P26` |
| D418 | D418.PLATE.COLUMN | `P26` |
| D419 | D419.PLATE.BRACKET | `` |
| D420 | D420.PLATE.BRACKET | `` |
| D421 | D421.PLATE.BRACKET | `` |
| D422 | D422.PLATE.BRACKET | `` |
| D423 | D423.PLATE.BRACKET | `` |
| D424 | D424.PLATE.BRACKET | `` |
| D425 | D425.PLATE.COLUMN | `` |
| D426 | D426.PLATE.COLUMN | `` |
| D427 | D427.PLATE.BRACKET | `` |
| D428 | D428.PLATE.BRACKET | `` |
| D429 | D429.PLATE.COLUMN | `` |
| D430 | D430.PLATE.COLUMN | `` |
| D431 | D431.PLATE.BRACKET | `` |
| D432 | D432.PLATE.BRACKET | `` |
| D433 | D433.PLATE.BRACKET | `` |
| D434 | D434.PLATE.BRACKET | `` |
| D435 | D435.PLATE.COLUMN | `` |
| D436 | D436.PLATE.COLUMN | `` |
| D437 | D437.PLATE.COLUMN | `P26` |
| D438 | D438.PLATE.COLUMN | `P26` |
| D439 | D439.PLATE.COLUMN | `P26` |
| D440 | D440.PLATE.COLUMN | `P26` |
| D441 | D441.PLATE.COLUMN | `` |
| D442 | D442.PLATE.COLUMN | `` |
| D443 | D443.PLATE.BRACKET | `` |
| D444 | D444.PLATE.BRACKET | `` |
| D445 | D445.PLATE.BRACKET | `` |
| D446 | D446.PLATE.BRACKET | `` |
| D447 | D447.PLATE.BRACKET | `` |
| D448 | D448.PLATE.BRACKET | `` |
| D449 | D449.PLATE.COLUMN | `` |
| D450 | D450.PLATE.COLUMN | `` |
| D451 | D451.PLATE.BRACKET | `` |
| D452 | D452.PLATE.BRACKET | `` |
| D453 | D453.PLATE.BRACKET | `` |
| D454 | D454.PLATE.BRACKET | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#479 | CREATION | `P133` |
| obj#479 | MATERIAL | `P150` |
| obj#479 | NAME | `P151` |
| obj#479 | PROFILE | `P1` |
| obj#479 | SERIE | `P148` |
| obj#479 | STARTNUMBER | `P149` |
| obj#491 | CREATION | `P133` |
| obj#491 | MATERIAL | `P150` |
| obj#491 | NAME | `P151` |
| obj#491 | PROFILE | `P1` |
| obj#491 | SERIE | `P148` |
| obj#491 | STARTNUMBER | `P149` |
| obj#497 | CREATION | `P133` |
| obj#497 | MATERIAL | `P150` |
| obj#497 | NAME | `P151` |
| obj#497 | PROFILE | `P1` |
| obj#497 | SERIE | `P148` |
| obj#497 | STARTNUMBER | `P149` |
| obj#505 | CREATION | `P133` |
| obj#505 | MATERIAL | `P150` |
| obj#505 | NAME | `P151` |
| obj#505 | PROFILE | `P1` |
| obj#505 | SERIE | `P148` |
| obj#505 | STARTNUMBER | `P149` |
| obj#511 | CREATION | `P133` |
| obj#511 | MATERIAL | `P150` |
| obj#511 | NAME | `P151` |
| obj#511 | PROFILE | `if P143=="" then P3 else "PL"+P143 endif` |
| obj#511 | SERIE | `P148` |
| obj#511 | STARTNUMBER | `P149` |
| obj#518 | CREATION | `P133` |
| obj#518 | MATERIAL | `P150` |
| obj#518 | NAME | `P151` |
| obj#518 | PROFILE | `if P143=="" then P3 else "PL"+P143 endif` |
| obj#518 | SERIE | `P148` |
| obj#518 | STARTNUMBER | `P149` |
| obj#524 | CREATION | `P133` |
| obj#524 | MATERIAL | `P150` |
| obj#524 | NAME | `P151` |
| obj#524 | PROFILE | `if P141=="" then P6 else "PL"+P141 endif` |
| obj#524 | SERIE | `P148` |
| obj#524 | STARTNUMBER | `P149` |
| obj#532 | CREATION | `P133` |
| obj#532 | MATERIAL | `P150` |
| obj#532 | NAME | `P151` |
| obj#532 | PROFILE | `if P141=="" then P6 else "PL"+P141 endif` |
| obj#532 | SERIE | `P148` |
| obj#532 | STARTNUMBER | `P149` |
| obj#538 | CREATION | `P146` |
| obj#538 | MATERIAL | `P150` |
| obj#538 | NAME | `P151` |
| obj#538 | PROFILE | `P1` |
| obj#538 | SERIE | `P148` |
| obj#538 | STARTNUMBER | `P149` |
| obj#545 | CREATION | `P146` |
| obj#545 | MATERIAL | `P150` |
| obj#545 | NAME | `P151` |
| obj#545 | PROFILE | `P1` |
| obj#545 | SERIE | `P149` |
| obj#545 | STARTNUMBER | `P149` |
| obj#552 | CREATION | `P146` |
| obj#552 | MATERIAL | `P150` |
| obj#552 | NAME | `P151` |
| obj#552 | PROFILE | `P1` |
| obj#552 | SERIE | `P148` |
| obj#552 | STARTNUMBER | `P149` |
| obj#558 | CREATION | `P146` |
| obj#558 | MATERIAL | `P150` |
| obj#558 | NAME | `P151` |
| obj#558 | PROFILE | `P1` |
| obj#558 | SERIE | `P148` |
| obj#558 | STARTNUMBER | `P149` |
| obj#564 | CREATION | `P146` |
| obj#564 | MATERIAL | `P150` |
| obj#564 | NAME | `P151` |
| obj#564 | PROFILE | `if P143=="" then P3 else "PL"+P143 endif` |
| obj#564 | SERIE | `P148` |
| obj#564 | STARTNUMBER | `P149` |
| obj#570 | CREATION | `P146` |
| obj#570 | MATERIAL | `P150` |
| obj#570 | NAME | `P151` |
| obj#570 | PROFILE | `if P143=="" then P3 else "PL"+P143 endif` |
| obj#570 | SERIE | `P148` |
| obj#570 | STARTNUMBER | `P149` |
| obj#576 | CREATION | `P146` |
| obj#576 | MATERIAL | `P150` |
| obj#576 | NAME | `P151` |
| obj#576 | PROFILE | `if P141=="" then P6 else "PL"+P141 endif` |
| obj#576 | SERIE | `P148` |
| obj#576 | STARTNUMBER | `P149` |
| obj#583 | CREATION | `P146` |
| obj#583 | MATERIAL | `P150` |
| obj#583 | NAME | `P151` |
| obj#583 | PROFILE | `if P141=="" then P6 else "PL"+P141 endif` |
| obj#583 | SERIE | `P148` |
| obj#583 | STARTNUMBER | `P149` |
| obj#589 | CREATION | `P60` |
| obj#589 | MATERIAL | `P150` |
| obj#589 | NAME | `P151` |
| obj#589 | PROFILE | `if P140=="" then P48 else "PL"+P140 endif` |
| obj#589 | SERIE | `P148` |
| obj#589 | STARTNUMBER | `P149` |
| obj#597 | CREATION | `P84` |
| obj#597 | MATERIAL | `P150` |
| obj#597 | NAME | `P151` |
| obj#597 | PROFILE | `if P140=="" then P48 else "PL"+P140 endif` |
| obj#597 | SERIE | `P148` |
| obj#597 | STARTNUMBER | `P149` |
| obj#604 | CREATION | `P130` |
| obj#604 | MATERIAL | `P150` |
| obj#604 | NAME | `P151` |
| obj#604 | PROFILE | `if P142=="" then P7 else "PL"+P142 endif` |
| obj#604 | SERIE | `P148` |
| obj#604 | STARTNUMBER | `P149` |
| obj#612 | CREATION | `P130` |
| obj#612 | MATERIAL | `P150` |
| obj#612 | NAME | `P151` |
| obj#612 | PROFILE | `if P142=="" then P7 else "PL"+P142 endif` |
| obj#612 | SERIE | `P148` |
| obj#612 | STARTNUMBER | `P149` |
| obj#618 | CREATION | `P64` |
| obj#618 | MATERIAL | `P150` |
| obj#618 | NAME | `P151` |
| obj#618 | PROFILE | `if P142=="" then P117 else "PL"+P142 endif` |
| obj#618 | SERIE | `P148` |
| obj#618 | STARTNUMBER | `P149` |
| obj#626 | CREATION | `P83` |
| obj#626 | MATERIAL | `P150` |
| obj#626 | NAME | `P151` |
| obj#626 | PROFILE | `if P141=="" then P106 else "PL"+P141 endif` |
| obj#626 | SERIE | `P148` |
| obj#626 | STARTNUMBER | `P149` |
| obj#633 | CREATION | `P138` |
| obj#633 | MATERIAL | `P150` |
| obj#633 | NAME | `P151` |
| obj#633 | PROFILE | `if P143=="" then P8 else "PL"+P143 endif` |
| obj#633 | SERIE | `P148` |
| obj#633 | STARTNUMBER | `P149` |
| obj#640 | CREATION | `P138` |
| obj#640 | MATERIAL | `P150` |
| obj#640 | NAME | `P151` |
| obj#640 | PROFILE | `if P143=="" then P8 else "PL"+P143 endif` |
| obj#640 | SERIE | `P148` |
| obj#640 | STARTNUMBER | `P149` |
| obj#646 | CREATION | `P37` |
| obj#646 | MATERIAL | `P150` |
| obj#646 | NAME | `P151` |
| obj#646 | PROFILE | `P1` |
| obj#646 | SERIE | `P148` |
| obj#646 | STARTNUMBER | `P149` |
| obj#653 | CREATION | `P37` |
| obj#653 | MATERIAL | `P150` |
| obj#653 | NAME | `P151` |
| obj#653 | PROFILE | `if P141=="" then P106 else "PL"+P141 endif` |
| obj#653 | SERIE | `P148` |
| obj#653 | STARTNUMBER | `P149` |
| obj#660 | CREATION | `P37` |
| obj#660 | MATERIAL | `P150` |
| obj#660 | NAME | `P151` |
| obj#660 | PROFILE | `P1` |
| obj#660 | SERIE | `P148` |
| obj#660 | STARTNUMBER | `P149` |
| obj#667 | CREATION | `P37` |
| obj#667 | MATERIAL | `P150` |
| obj#667 | NAME | `P151` |
| obj#667 | PROFILE | `if P143=="" then P3 else "PL"+P143 endif` |
| obj#667 | SERIE | `P148` |
| obj#667 | STARTNUMBER | `P149` |
| obj#673 | CREATION | `P132` |
| obj#673 | MATERIAL | `P150` |
| obj#673 | NAME | `P151` |
| obj#673 | PROFILE | `if P141=="" then P6 else "PL"+P141 endif` |
| obj#673 | SERIE | `P148` |
| obj#673 | STARTNUMBER | `P149` |
| obj#680 | CREATION | `P127` |
| obj#680 | MATERIAL | `P150` |
| obj#680 | NAME | `P151` |
| obj#680 | PROFILE | `if P142=="" then P7 else "PL"+P142 endif` |
| obj#680 | SERIE | `P148` |
| obj#680 | STARTNUMBER | `P149` |
| obj#687 | CREATION | `P69` |
| obj#687 | MATERIAL | `P150` |
| obj#687 | NAME | `P151` |
| obj#687 | PROFILE | `if P142=="" then P117 else "PL"+P142 endif` |
| obj#687 | SERIE | `P148` |
| obj#687 | STARTNUMBER | `P149` |
| obj#693 | CREATION | `P134` |
| obj#693 | MATERIAL | `P150` |
| obj#693 | NAME | `P151` |
| obj#693 | PROFILE | `if P140=="" then P5 else "PL"+P140 endif` |
| obj#693 | SERIE | `P148` |
| obj#693 | STARTNUMBER | `P149` |
| obj#700 | CREATION | `P136` |
| obj#700 | MATERIAL | `P150` |
| obj#700 | NAME | `P151` |
| obj#700 | PROFILE | `if P140=="" then P5 else "PL"+P140 endif` |
| obj#700 | SERIE | `P148` |
| obj#700 | STARTNUMBER | `P149` |
| obj#708 | CREATION | `P74` |
| obj#708 | MATERIAL | `P150` |
| obj#708 | NAME | `P151` |
| obj#708 | PROFILE | `if P143=="" then P3 else "PL"+P143 endif` |
| obj#708 | SERIE | `P148` |
| obj#708 | STARTNUMBER | `P149` |
| obj#715 | CREATION | `P134` |
| obj#715 | MATERIAL | `P150` |
| obj#715 | NAME | `P151` |
| obj#715 | PROFILE | `if P140=="" then P5 else "PL"+P140 endif` |
| obj#715 | SERIE | `P148` |
| obj#715 | STARTNUMBER | `P149` |
| obj#721 | CREATION | `P132` |
| obj#721 | MATERIAL | `P150` |
| obj#721 | NAME | `P151` |
| obj#721 | PROFILE | `if P141=="" then P6 else "PL"+P141 endif` |
| obj#721 | SERIE | `P148` |
| obj#721 | STARTNUMBER | `P149` |
| obj#728 | CREATION | `P127` |
| obj#728 | MATERIAL | `P150` |
| obj#728 | NAME | `P151` |
| obj#728 | PROFILE | `if P142=="" then P7 else "PL"+P142 endif` |
| obj#728 | SERIE | `P148` |
| obj#728 | STARTNUMBER | `P149` |
| obj#735 | CREATION | `P136` |
| obj#735 | MATERIAL | `P150` |
| obj#735 | NAME | `P151` |
| obj#735 | PROFILE | `if P140=="" then P5 else "PL"+P140 endif` |
| obj#735 | SERIE | `P148` |
| obj#735 | STARTNUMBER | `P149` |

## Preset `standard.p_NEC_AUTO_STIFFENERS`
```
joint_attributes.saveas_file "standard"
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
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P145 30.000000
joint_attributes.P144 3
joint_attributes.P151 "PLATE"
joint_attributes.P150 "SS400"
joint_attributes.P149 "1"
joint_attributes.P148 "PL"
joint_attributes.P143 ""
joint_attributes.P142 ""
joint_attributes.P141 ""
joint_attributes.P140 ""
joint_attributes.P2 ""
joint_attributes.P128 0
joint_attributes.P135 0
joint_attributes.P34 1
joint_attributes.P26 30.000000
```

## Preset `standard.p_NEC_AUTO_STIFFENERS`
```
joint_attributes.saveas_file "standard"
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
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P145 30.000000
joint_attributes.P144 3
joint_attributes.P151 "PLATE"
joint_attributes.P150 "SS400"
joint_attributes.P149 "1"
joint_attributes.P148 "PL"
joint_attributes.P143 ""
joint_attributes.P142 ""
joint_attributes.P141 ""
joint_attributes.P140 ""
joint_attributes.P2 ""
joint_attributes.P128 0
joint_attributes.P135 0
joint_attributes.P34 1
joint_attributes.P26 30.000000
```