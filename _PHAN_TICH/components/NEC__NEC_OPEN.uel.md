# NEC_OPEN

- File: `NEC\NEC_OPEN.uel`  (66,484 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	NEC_OPEN	All`
- Dialog parameters: 1 | Variables (P/custom): 30 | Distance vars (D): 228 | Parts with bound profile: 20
- Lookup files (fVF): -
- Functions used: fP, if
- Dialog bitmaps: 3 | Saved presets: standard.p_NEC_OPEN, standard.p_NEC_OPEN

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(201, "NEC_OPEN")
   {
       tab_page("", " Pictures ", 1)
       {
           picture("NEC_LOGO",8,2,420,480)
           picture("NEC_OPEN2",100,50,220,150)

           parameter("", "P15", distance, number, 135,214,60)
           parameter("", "P12", chamfer_type, number, 625,230,90)
           parameter("", "P13", distance, number, 625,255,90)
           parameter("", "P11", distance, number, 370,260,60)
           parameter("", "P10", distance, number, 390,300,60)
           parameter("", "P14", distance, number, 305,435,60)
           parameter("", "P16", distance, number, 485,435,60)
           parameter("", "P8", YesNo, number, 390,403,60)
           
           


           
       }
       tab_page("", " Plates ", 2)
       {
           attribute("", "수직보강재", label,"%s",  none, none, "0.0", "0.0",20,50)
           attribute("", "수평보강재", label,"%s",  none, none, "0.0", "0.0",20,75)
           attribute("", "t", label,"%s",  none, none, "0.0", "0.0",190,25)
           attribute("", "Pos_No", label,"%s",  none, none, "0.0", "0.0",320,25)
           attribute("", "Material", label,"%s",  none, none, "0.0", "0.0",520,25)
           attribute("", "Name", label,"%s",  none, none, "0.0", "0.0",670,25)
           
           picture("NEC_OPEN4",100,50,270,150)
           parameter("", "P18", string, text, 180,50,50)
           parameter("", "P17", string, text, 180,75,50)
           parameter("", "P22", string, text, 320,50,50)
           parameter("", "P23", string, text, 320,75,50)
           parameter("", "P24", string, text, 400,50,40)
           parameter("", "P25", string, text, 400,75,40)
           parameter("", "P9", material, text, 520,50,90)
           parameter("", "P21", material, text, 520,75,90)
           parameter("", "P2", string, text, 670,50,120)
           parameter("", "P3", string, text, 670,75,120)
           parameter("", "P19", chamfer_type, number, 550,255,90)
           parameter("", "P7", distance, number, 550,280,90)
           parameter("", "P31", chamfer_type, number, 185,255,90)
           parameter("", "P34", distance, number, 185,280,90)
           parameter("", "P20", distance, number, 260,450,60)
           
           parameter("CLASS", "P1", string, text, 19)

           
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | CLASS | Text | `5` |
| P2 | 수직부재 이름 | Text | `PLATE` |
| P3 | 수평부재 이름 | Text | `PLATE` |
| P4 | H STIFF | Profile | `"PL"+P17+"*"+P6` *(formula)* |
| P5 | V STIFF | Profile | `"PL"+P18` *(formula)* |
| P6 | Parameter6 | Length | `((fP(proB,"ID67C2A424-0000-C7AA-3137-343038313035")-fP(proS,"ID67C2A424-0000-C7AA-3137-343038313035"))*0.5)-P20` *(formula)* |
| P7 | 보강재 챔퍼 크기 | Length | `20.00` |
| P9 | 수직부재 재질 | Material | `SS400` |
| P10 | 오프닝 너비 | Length | `600.00` |
| P11 | 오프닝 높이 | Length | `300.00` |
| P12 | 오프닝 챔퍼 타입 | Chamfer | `3` |
| P13 | 오프닝챔퍼 길이 | Length | `15.00` |
| P14 | V_GAP | Length | `20.00` |
| P15 | H_GAP | Length | `20.00` |
| P16 | 날개 여장길이 | Length | `200.00` |
| P17 | 수평 보강재 | Length | `15.00` |
| P18 | 수직 보강재 | Length | `20.00` |
| P19 | 보강재 챔퍼타입   | Chamfer | `3` |
| P21 | 수평부재 재질 | Material | `SS400` |
| P22 | 수직부재 파트 프리픽스 | Text | `PL` |
| P23 | 수평부재 파트 프리픽스 | Text | `PL` |
| P24 | 수직부재 파트 스타트넘버 | Text | `1` |
| P25 | 수평부재 파트 스타트넘버 | Text | `1` |
| P26 | 플랜지 두께 참조 | Text | `fP(proT,"ID67C2A424-0000-C7AA-3137-343038313035")` *(formula)* |
| P27 | 웨브 두께 참조 | Length | `fP(proS,"ID67C2A424-0000-C7AA-3137-343038313035")` *(formula)* |
| P28 | Parameter28 | Length | `(P11*0.5)+P15+fP(proT,"ID67C2A424-0000-C7AA-3137-343038313035")` *(formula)* |
| P29 | Parameter29 | Length | `((fP(proH,"ID67C2A424-0000-C7AA-3137-343038313035")-(fP(proT,"ID67C2A424-0000-C7AA-3137-343038313035")*2))*0.5)-P28` *(formula)* |
| P30 | Parameter30 | Yes/No | `if (P29 <=70) then 0 else 1 endif` *(formula)* |
| P31 | 중앙챔퍼 | Chamfer | `3` |
| P34 | Parameter34 | Length | `20.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.CUTPART.GIRDER | `` |
| D2 | D2.CUTPART.GIRDER | `` |
| D3 | D3.CUTPART.GIRDER | `` |
| D4 | D4.CUTPART.GIRDER | `` |
| D5 | D5.CUTPART.OPEN-REC1-XL | `P10*0.5` |
| D6 | D6.CUTPART.OPEN-REC1-XL | `P10*0.5` |
| D7 | D7.CUTPART.OPEN-REC1-XL | `P10*0.5` |
| D8 | D8.CUTPART.OPEN-REC1-XL | `P10*0.5` |
| D9 | D9.CUTPART.GIRDER | `P11*0.5` |
| D10 | D10.CUTPART.GIRDER | `P11*0.5` |
| D11 | D11.CUTPART.GIRDER | `P11*0.5` |
| D12 | D12.CUTPART.GIRDER | `P11*0.5` |
| D13 | D13.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D14 | D14.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D15 | D15.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D16 | D16.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D17 | D17.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D18 | D18.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D19 | D19.PLATE.GIRDER | `` |
| D20 | D20.PLATE.GIRDER | `` |
| D21 | D21.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D22 | D22.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D23 | D23.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D24 | D24.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D25 | D25.PLATE.GIRDER | `` |
| D26 | D26.PLATE.GIRDER | `` |
| D27 | D27.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D28 | D28.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D29 | D29.PLATE.GIRDER | `` |
| D30 | D30.PLATE.GIRDER | `` |
| D31 | D31.PLATE.GIRDER | `` |
| D32 | D32.PLATE.GIRDER | `` |
| D33 | D33.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D34 | D34.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D35 | D35.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D36 | D36.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D37 | D37.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D38 | D38.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D39 | D39.PLATE.GIRDER | `` |
| D40 | D40.PLATE.GIRDER | `` |
| D41 | D41.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D42 | D42.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D43 | D43.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D44 | D44.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D45 | D45.PLATE.GIRDER | `` |
| D46 | D46.PLATE.GIRDER | `` |
| D47 | D47.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D48 | D48.PLATE.GIRDER | `if (P30==0) then 20 else 0 endif` |
| D49 | D49.PLATE.GIRDER | `` |
| D50 | D50.PLATE.GIRDER | `` |
| D51 | D51.PLATE.GIRDER | `` |
| D52 | D52.PLATE.GIRDER | `` |
| D53 | D53.PLATE.GIRDER | `-P20` |
| D54 | D54.PLATE.GIRDER | `-P20` |
| D55 | D55.PLATE.GIRDER | `-P20` |
| D56 | D56.PLATE.GIRDER | `-P20` |
| D57 | D57.PLATE.GIRDER | `-P20` |
| D58 | D58.PLATE.GIRDER | `-P20` |
| D59 | D59.PLATE.GIRDER | `-P20` |
| D60 | D60.PLATE.GIRDER | `-P20` |
| D61 | D61.PLATE.GIRDER | `-P20` |
| D62 | D62.PLATE.GIRDER | `-P20` |
| D63 | D63.PLATE.GIRDER | `-P20` |
| D64 | D64.PLATE.GIRDER | `-P20` |
| D65 | D65.PLATE.GIRDER | `-P20` |
| D66 | D66.PLATE.GIRDER | `-P20` |
| D67 | D67.PLATE.GIRDER | `-P20` |
| D68 | D68.PLATE.GIRDER | `-P20` |
| D69 | D69.PLATE.GIRDER | `-P20` |
| D70 | D70.PLATE.GIRDER | `-P20` |
| D71 | D71.PLATE.GIRDER | `-P20` |
| D72 | D72.PLATE.GIRDER | `-P20` |
| D73 | D73.PLATE.GIRDER | `-P20` |
| D74 | D74.PLATE.GIRDER | `-P20` |
| D75 | D75.PLATE.GIRDER | `-P20` |
| D76 | D76.PLATE.GIRDER | `-P20` |
| D77 | D77.PLATE.GIRDER | `-P20` |
| D78 | D78.PLATE.GIRDER | `-P20` |
| D79 | D79.PLATE.GIRDER | `-P20` |
| D80 | D80.PLATE.GIRDER | `-P20` |
| D81 | D81.PLATE.GIRDER | `-P20` |
| D82 | D82.PLATE.GIRDER | `-P20` |
| D83 | D83.PLATE.GIRDER | `-P20` |
| D84 | D84.PLATE.GIRDER | `-P20` |
| D85 | D85.PLATE.GIRDER | `` |
| D86 | D86.PLATE.GIRDER | `` |
| D87 | D87.PLATE.GIRDER | `` |
| D88 | D88.PLATE.GIRDER | `` |
| D89 | D89.PLATE.GIRDER | `` |
| D90 | D90.PLATE.GIRDER | `` |
| D91 | D91.PLATE.PLATE | `` |
| D92 | D92.PLATE.PLATE | `` |
| D93 | D93.PLATE.PLATE | `` |
| D94 | D94.PLATE.PLATE | `` |
| D95 | D95.PLATE.PLATE | `` |
| D96 | D96.PLATE.PLATE | `` |
| D97 | D97.PLATE.PLATE | `` |
| D98 | D98.PLATE.PLATE | `` |
| D99 | D99.PLATE.PLATE | `` |
| D100 | D100.PLATE.PLATE | `` |
| D101 | D101.PLATE.PLATE | `` |
| D102 | D102.PLATE.PLATE | `` |
| D103 | D103.PLATE.GIRDER | `` |
| D104 | D104.PLATE.GIRDER | `` |
| D105 | D105.PLATE.GIRDER | `` |
| D106 | D106.PLATE.GIRDER | `` |
| D107 | D107.PLATE.GIRDER | `` |
| D108 | D108.PLATE.GIRDER | `` |
| D109 | D109.PLATE.PLATE | `` |
| D110 | D110.PLATE.PLATE | `` |
| D111 | D111.PLATE.PLATE | `` |
| D112 | D112.PLATE.PLATE | `` |
| D113 | D113.PLATE.PLATE | `` |
| D114 | D114.PLATE.PLATE | `` |
| D115 | D115.PLATE.PLATE | `` |
| D116 | D116.PLATE.PLATE | `` |
| D117 | D117.PLATE.PLATE | `` |
| D118 | D118.PLATE.PLATE | `` |
| D119 | D119.PLATE.PLATE | `` |
| D120 | D120.PLATE.PLATE | `` |
| D121 | D121.PLATE.GIRDER | `` |
| D122 | D122.PLATE.GIRDER | `` |
| D123 | D123.PLATE.GIRDER | `` |
| D124 | D124.PLATE.GIRDER | `` |
| D125 | D125.PLATE.GIRDER | `` |
| D126 | D126.PLATE.GIRDER | `` |
| D127 | D127.PLATE.PLATE | `` |
| D128 | D128.PLATE.PLATE | `` |
| D129 | D129.PLATE.PLATE | `` |
| D130 | D130.PLATE.PLATE | `` |
| D131 | D131.PLATE.PLATE | `` |
| D132 | D132.PLATE.PLATE | `` |
| D133 | D133.PLATE.PLATE | `` |
| D134 | D134.PLATE.PLATE | `` |
| D135 | D135.PLATE.PLATE | `` |
| D136 | D136.PLATE.PLATE | `` |
| D137 | D137.PLATE.PLATE | `` |
| D138 | D138.PLATE.PLATE | `` |
| D139 | D139.PLATE.PLATE | `` |
| D140 | D140.PLATE.PLATE | `` |
| D141 | D141.PLATE.PLATE | `` |
| D142 | D142.PLATE.PLATE | `` |
| D143 | D143.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D144 | D144.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D145 | D145.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D146 | D146.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D147 | D147.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D148 | D148.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D149 | D149.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D150 | D150.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D151 | D151.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D152 | D152.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D153 | D153.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D154 | D154.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D155 | D155.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D156 | D156.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D157 | D157.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D158 | D158.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D159 | D159.PLATE.GIRDER | `(P11*0.5)+P15` |
| D160 | D160.PLATE.GIRDER | `(P11*0.5)+P15` |
| D161 | D161.PLATE.GIRDER | `(P11*0.5)+P15` |
| D162 | D162.PLATE.GIRDER | `(P11*0.5)+P15` |
| D163 | D163.PLATE.GIRDER | `(P11*0.5)+P15` |
| D164 | D164.PLATE.GIRDER | `(P11*0.5)+P15` |
| D165 | D165.PLATE.GIRDER | `(P11*0.5)+P15` |
| D166 | D166.PLATE.GIRDER | `(P11*0.5)+P15` |
| D167 | D167.PLATE.OPEN-REC1-XL | `(P10*0.5)+P14+P18+P16` |
| D168 | D168.PLATE.OPEN-REC1-XL | `(P10*0.5)+P14+P18+P16` |
| D169 | D169.PLATE.OPEN-REC1-XL | `(P10*0.5)+P14+P18+P16` |
| D170 | D170.PLATE.OPEN-REC1-XL | `(P10*0.5)+P14+P18+P16` |
| D171 | D171.PLATE.OPEN-REC1-XL | `(P10*0.5)+P14+P18+P16` |
| D172 | D172.PLATE.OPEN-REC1-XL | `(P10*0.5)+P14+P18+P16` |
| D173 | D173.PLATE.OPEN-REC1-XL | `(P10*0.5)+P14+P18+P16` |
| D174 | D174.PLATE.OPEN-REC1-XL | `(P10*0.5)+P14+P18+P16` |
| D175 | D175.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D176 | D176.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D177 | D177.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D178 | D178.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D179 | D179.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D180 | D180.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D181 | D181.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D182 | D182.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D183 | D183.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D184 | D184.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D185 | D185.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D186 | D186.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D187 | D187.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D188 | D188.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D189 | D189.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D190 | D190.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D191 | D191.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D192 | D192.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D193 | D193.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D194 | D194.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D195 | D195.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D196 | D196.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D197 | D197.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D198 | D198.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D199 | D199.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D200 | D200.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D201 | D201.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D202 | D202.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D203 | D203.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D204 | D204.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D205 | D205.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D206 | D206.PLATE.NEC_OPEN | `(P10*0.5)+P14` |
| D207 | D207.PLATE.OPEN-REC1-XL | `` |
| D208 | D208.PLATE.OPEN-REC1-XL | `` |
| D209 | D209.PLATE.OPEN-REC1-XL | `` |
| D210 | D210.PLATE.OPEN-REC1-XL | `` |
| D211 | D211.PLATE.OPEN-REC1-XL | `` |
| D212 | D212.PLATE.OPEN-REC1-XL | `` |
| D213 | D213.PLATE.OPEN-REC1-XL | `` |
| D214 | D214.PLATE.OPEN-REC1-XL | `` |
| D215 | D215.PLATE.OPEN-REC1-XL | `` |
| D216 | D216.PLATE.OPEN-REC1-XL | `` |
| D217 | D217.PLATE.OPEN-REC1-XL | `` |
| D218 | D218.PLATE.OPEN-REC1-XL | `` |
| D219 | D219.PLATE.OPEN-REC1-XL | `` |
| D220 | D220.PLATE.OPEN-REC1-XL | `` |
| D221 | D221.PLATE.OPEN-REC1-XL | `` |
| D222 | D222.PLATE.OPEN-REC1-XL | `` |
| D223 | D223.PLATE.GIRDER | `` |
| D224 | D224.PLATE.GIRDER | `` |
| D225 | D225.PLATE.GIRDER | `` |
| D226 | D226.PLATE.GIRDER | `` |
| D227 | D227.PLATE.GIRDER | `` |
| D228 | D228.PLATE.GIRDER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#152 | CLASS | `P1` |
| obj#152 | MATERIAL | `P21` |
| obj#152 | NAME | `P3` |
| obj#152 | PROFILE | `P4` |
| obj#152 | SERIE | `P23` |
| obj#152 | STARTNUMBER | `P25` |
| obj#167 | CLASS | `P1` |
| obj#167 | MATERIAL | `P21` |
| obj#167 | NAME | `P3` |
| obj#167 | PROFILE | `P4` |
| obj#167 | SERIE | `P23` |
| obj#167 | STARTNUMBER | `P25` |
| obj#177 | CLASS | `P1` |
| obj#177 | MATERIAL | `P21` |
| obj#177 | NAME | `P3` |
| obj#177 | PROFILE | `P4` |
| obj#177 | SERIE | `P23` |
| obj#177 | STARTNUMBER | `P25` |
| obj#185 | CLASS | `P1` |
| obj#185 | MATERIAL | `P21` |
| obj#185 | NAME | `P3` |
| obj#185 | PROFILE | `P4` |
| obj#185 | SERIE | `P23` |
| obj#185 | STARTNUMBER | `P25` |
| obj#193 | CLASS | `P1` |
| obj#193 | MATERIAL | `P9` |
| obj#193 | NAME | `P2` |
| obj#193 | PROFILE | `P5` |
| obj#193 | SERIE | `P22` |
| obj#193 | STARTNUMBER | `P24` |
| obj#205 | CLASS | `P1` |
| obj#205 | MATERIAL | `P9` |
| obj#205 | NAME | `P2` |
| obj#205 | PROFILE | `P5` |
| obj#205 | SERIE | `P22` |
| obj#205 | STARTNUMBER | `P24` |
| obj#216 | CLASS | `P1` |
| obj#216 | MATERIAL | `P9` |
| obj#216 | NAME | `P2` |
| obj#216 | PROFILE | `P5` |
| obj#216 | SERIE | `P22` |
| obj#216 | STARTNUMBER | `P24` |
| obj#224 | CLASS | `P1` |
| obj#224 | MATERIAL | `P9` |
| obj#224 | NAME | `P2` |
| obj#224 | PROFILE | `P5` |
| obj#224 | SERIE | `P22` |
| obj#224 | STARTNUMBER | `P24` |
| obj#233 | CLASS | `P1` |
| obj#233 | MATERIAL | `P9` |
| obj#233 | NAME | `P2` |
| obj#233 | PROFILE | `P5` |
| obj#233 | SERIE | `P22` |
| obj#233 | STARTNUMBER | `P24` |
| obj#242 | CLASS | `P1` |
| obj#242 | MATERIAL | `P9` |
| obj#242 | NAME | `P2` |
| obj#242 | PROFILE | `P5` |
| obj#242 | SERIE | `P22` |
| obj#242 | STARTNUMBER | `P24` |
| obj#251 | CLASS | `P1` |
| obj#251 | MATERIAL | `P9` |
| obj#251 | NAME | `P2` |
| obj#251 | PROFILE | `P5` |
| obj#251 | SERIE | `P22` |
| obj#251 | STARTNUMBER | `P24` |
| obj#259 | CLASS | `P1` |
| obj#259 | MATERIAL | `P9` |
| obj#259 | NAME | `P2` |
| obj#259 | PROFILE | `P5` |
| obj#259 | SERIE | `P22` |
| obj#259 | STARTNUMBER | `P24` |
| obj#267 | CLASS | `P1` |
| obj#267 | MATERIAL | `P9` |
| obj#267 | NAME | `P2` |
| obj#267 | PROFILE | `P5` |
| obj#267 | SERIE | `P22` |
| obj#267 | STARTNUMBER | `P24` |
| obj#275 | CLASS | `P1` |
| obj#275 | MATERIAL | `P9` |
| obj#275 | NAME | `P2` |
| obj#275 | PROFILE | `P5` |
| obj#275 | SERIE | `P22` |
| obj#275 | STARTNUMBER | `P24` |
| obj#284 | CLASS | `P1` |
| obj#284 | MATERIAL | `P9` |
| obj#284 | NAME | `P2` |
| obj#284 | PROFILE | `P5` |
| obj#284 | SERIE | `P22` |
| obj#284 | STARTNUMBER | `P24` |
| obj#292 | CLASS | `P1` |
| obj#292 | MATERIAL | `P9` |
| obj#292 | NAME | `P2` |
| obj#292 | PROFILE | `P5` |
| obj#292 | SERIE | `P22` |
| obj#292 | STARTNUMBER | `P24` |
| obj#300 | CLASS | `P1` |
| obj#300 | CREATION | `P8` |
| obj#300 | MATERIAL | `P9` |
| obj#300 | NAME | `P2` |
| obj#300 | PROFILE | `P5` |
| obj#300 | SERIE | `P22` |
| obj#300 | STARTNUMBER | `P24` |
| obj#309 | CLASS | `P1` |
| obj#309 | CREATION | `P8` |
| obj#309 | MATERIAL | `P9` |
| obj#309 | NAME | `P2` |
| obj#309 | PROFILE | `P5` |
| obj#309 | SERIE | `P22` |
| obj#309 | STARTNUMBER | `P24` |
| obj#317 | CLASS | `P1` |
| obj#317 | CREATION | `P8` |
| obj#317 | MATERIAL | `P9` |
| obj#317 | NAME | `P2` |
| obj#317 | PROFILE | `P5` |
| obj#317 | SERIE | `P22` |
| obj#317 | STARTNUMBER | `P24` |
| obj#326 | CLASS | `P1` |
| obj#326 | CREATION | `P8` |
| obj#326 | MATERIAL | `P9` |
| obj#326 | NAME | `P2` |
| obj#326 | PROFILE | `P5` |
| obj#326 | SERIE | `P22` |
| obj#326 | STARTNUMBER | `P24` |

## Preset `standard.p_NEC_OPEN`
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
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P1 "5"
joint_attributes.P20 10.000000
joint_attributes.P34 20.000000
joint_attributes.P31 3
joint_attributes.P7 20.000000
joint_attributes.P19 3
joint_attributes.P3 "PLATE"
joint_attributes.P2 "PLATE"
joint_attributes.P21 "SS400"
joint_attributes.P9 "SS400"
joint_attributes.P25 "1"
joint_attributes.P24 "1"
joint_attributes.P23 "PL"
joint_attributes.P22 "PL"
joint_attributes.P17 "15"
joint_attributes.P18 "20"
joint_attributes.P8 1
joint_attributes.P16 200.000000
joint_attributes.P14 20.000000
joint_attributes.P10 600.000000
joint_attributes.P11 300.000000
joint_attributes.P13 20.000000
joint_attributes.P12 2
joint_attributes.P15 20.000000
```

## Preset `standard.p_NEC_OPEN`
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
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P1 "5"
joint_attributes.P20 10.000000
joint_attributes.P34 20.000000
joint_attributes.P31 3
joint_attributes.P7 20.000000
joint_attributes.P19 3
joint_attributes.P3 "PLATE"
joint_attributes.P2 "PLATE"
joint_attributes.P21 "SS400"
joint_attributes.P9 "SS400"
joint_attributes.P25 "1"
joint_attributes.P24 "1"
joint_attributes.P23 "PL"
joint_attributes.P22 "PL"
joint_attributes.P17 "15"
joint_attributes.P18 "20"
joint_attributes.P8 1
joint_attributes.P16 200.000000
joint_attributes.P14 20.000000
joint_attributes.P10 600.000000
joint_attributes.P11 300.000000
joint_attributes.P13 20.000000
joint_attributes.P12 2
joint_attributes.P15 20.000000
```