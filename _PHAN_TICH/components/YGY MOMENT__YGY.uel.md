# ♥♥♥YGY_TempBeam-Moment

- File: `YGY MOMENT\YGY.uel`  (38,857 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	♥♥♥YGY_TempBeam-Moment	All`
- Dialog parameters: 9 | Variables (P/custom): 87 | Distance vars (D): 181 | Parts with bound profile: 9
- Lookup files (fVF): YGY-TB-MOMENT-PLTHK.txt, YGY-TB-MOMENT-WELD-K.txt, YGY-TB-MOMENT.txt
- Functions used: fP, fTpl, fVF, if, mid, tan
- Dialog bitmaps: 0 | Saved presets: +104.p_♥♥♥YGY_TempBeam-Moment, 104L.p_♥♥♥YGY_TempBeam-Moment

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "♥♥♥YGY_TempBeam-Moment")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("PLATE방향", "P11", YesNo, number, 1)
           parameter("BOLT_SIZE", "P12", integer, number, 2)
           parameter("SLOT HOLE", "P15", integer, number, 3)
           parameter("GP PREFIX", "P30", string, text, 4)
           parameter("BACK SF 유무", "P31", YesNo, number, 5)
           parameter("SCALLOP_TYPE", "P40", chamfer_type, number, 6)
           parameter("SCALLOP_크기(자동)", "P41", distance, number, 7)
           parameter("하부FLG보강 개선", "P43", YesNo, number, 8)
           parameter("CWS=900 SPS=700", "CWS", integer, number, 9)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Text | `fP(proPROFILE_NAME,"ID67174322-0000-3C4A-3137-323935383333")` *(formula)* |
| P2 | Parameter2 | Integer | `fP(proH,"ID67174322-0000-3C4A-3137-323935383333")` *(formula)* |
| P3 | Parameter3 | Length | `fP(proS,"ID67174322-0000-3C4A-3137-323935383333")` *(formula)* |
| P4 | Parameter4 | Integer | `fP(proT,"ID67174322-0000-3C4A-3137-323935383333")` *(formula)* |
| P5 | Parameter5 | Integer | `fP(proB,"ID67174322-0000-3C4A-3137-323935383333")` *(formula)* |
| P6 | Parameter6 | Text | `fVF("YGY-TB-MOMENT.txt",P1,1)` *(formula)* |
| P7 | Parameter7 | Integer | `fVF("YGY-TB-MOMENT.txt",P1,2)` *(formula)* |
| P8 | Parameter8 | Length | `fVF("YGY-TB-MOMENT.txt",P1,3)` *(formula)* |
| P9 | Parameter9 | Integer | `if (P2>=CWS) then 2 else 1 endif` *(formula)* |
| P10 | Parameter10 | Length | `(P3+P7)/2` *(formula)* |
| P11 | PLATE방향 | Yes/No | `0` |
| P12 | BOLT_SIZE | Integer | `24` |
| P13 | Parameter13 | Integer | `if (P12>=24) then 3 else 2 endif` *(formula)* |
| P14 | SLOT_HOLE_LENGTH | Integer | `if (P12==16) then 40 else if (P12==20) then 50 else if (P12==22) then 55 else if (P12==24) then 60 else if (P12==27) then 67 else endif endif endif endif endif` *(formula)* |
| P15 | SLOT HOLE | Integer | `P14-P12-P13` *(formula)* |
| P16 | Parameter16 | Integer | `if (P11==1) then 1 else -1 endif` *(formula)* |
| P17 | Parameter17 | Integer | `if (P11==1) then 10000 else 1000 endif` *(formula)* |
| P18 | Parameter18 | Length | `if (P2>=CWS) then ((P8/2)-40) else (P8-80) endif` *(formula)* |
| P19 | Parameter19 | Text | `if (P9==0) then 0 else (P9+"*"+P18) endif` *(formula)* |
| P20 | Parameter20 | Angle | `45.0000` |
| P21 | Parameter21 | Length | `fP(proH,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| P22 | Parameter22 | Length | `fP(proT,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| P23 | Parameter23 | Length | `P21-P2` *(formula)* |
| P24 | Parameter24 | Length | `if (P23>0) then P4 else 0 endif` *(formula)* |
| P25 | Parameter25 | Yes/No | `if (P23>0) then 1 else 0 endif` *(formula)* |
| P26 | Parameter26 | Length | `if (P22>P4) then P4 else P22 endif` *(formula)* |
| P27 | Parameter27 | Angle | `45.0000` |
| P28 | Parameter28 | Length | `if (P22>P4) then (P22-P4) else (P4-P22) endif` *(formula)* |
| P29 | Parameter29 | Length | `if (P25==1) then (P21-P2-P22+P4) else 0 endif` *(formula)* |
| P30 | GP PREFIX | Text | `P63` *(formula)* |
| P31 | BACK SF 유무 | Yes/No | `1` |
| P32 | Parameter32 | Length | `if (P23<=70) then -35 else 0 endif` *(formula)* |
| P33 | Parameter33 | Length | `if (P23<=70) then 0 else 35 endif` *(formula)* |
| P34 | PLATE_MAT | Material | `fP(proMATERIAL,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| P35 | Parameter35 | Text | `if (P34=="SHN355") then "SN355" else if (P34=="SHN275") then "SN275" else P34 endif endif` *(formula)* |
| P36 | Parameter36 | Length | `if (P25==1) then 0 else 0 endif` *(formula)* |
| P37 | Parameter37 | Yes/No | `if (P23>0) then 0 else 0 endif` *(formula)* |
| P38 | Parameter38 | Integer | `fVF("YGY-TB-MOMENT-PLTHK.txt",P4,1)` *(formula)* |
| P39 | Parameter39 | Integer | `fVF("YGY-TB-MOMENT-PLTHK.txt",P4,2)` *(formula)* |
| P40 | SCALLOP_TYPE | Chamfer | `3` |
| P41 | SCALLOP_크기(자동) | Length | `if (gw<181) then 25 else if (gw<200) then 35 else if (250<gw) then 35 else 35 endif endif endif` *(formula)* |
| P42 | Parameter42 | Length | `fP(proR,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| P43 | 하부FLG보강 개선 | Yes/No | `if (P39>=21) then 1 else 0 endif` *(formula)* |
| P44 | Parameter44 | Length | `fVF("YGY-TB-MOMENT-WELD-K.txt",P39,1)` *(formula)* |
| P45 | Parameter45 | Length | `fVF("YGY-TB-MOMENT-WELD-K.txt",P39,3)` *(formula)* |
| P46 | Parameter46 | Length | `fTpl("LENGTH","ID67174322-0000-3CAA-3137-323935383333")+100` *(formula)* |
| P47 | Parameter47 | Length | `(P44-P45)/2` *(formula)* |
| P48 | Parameter48 | Text | `fP(proASSEMBLYSERIE,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| P49 | START_NO1 | Integer | `fP(proASSEMBLYSTARTNUMBER,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| P50 | START_NO2 | Integer | `fP(proASSEMBLYSTARTNUMBER,"ID67174322-0000-3C4A-3137-323935383333")` *(formula)* |
| P51 | Parameter51 | Text | `fP(proASSEMBLYSERIE,"ID67174322-0000-3C4A-3137-323935383333")` *(formula)* |
| P52 | Parameter52 | Text | `mid(P51,0,2)` *(formula)* |
| P53 | Parameter53 | Length | `18.00` |
| P54 | Parameter54 | Yes/No | `if (P7>=21) then 1 else 0 endif` *(formula)* |
| P55 | Parameter55 | Integer | `if (D174<10) then 0 else 10 endif` *(formula)* |
| P56 | 하부FLG보강 위치 이동 | Length | `0.00` |
| P57 | GP_WEB위치조정 | Length | `0.00` |
| P58 | Parameter58 | Length | `if (T2<T3) then T4 else 0 endif` *(formula)* |
| P59 | Parameter59 | Text | `fVF("YGY-TB-MOMENT.txt",P1,5)` *(formula)* |
| P60 | Parameter60 | Yes/No | `if (P43>P133 \|\| P44<P133) then 0 else P133 endif` *(formula)* |
| P61 | Parameter61 | Text | `fP(proSERIE,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| P62 | Parameter62 | Text | `fP(proSERIE,"ID67174322-0000-3C4A-3137-323935383333")` *(formula)* |
| P63 | Parameter63 | Text | `mid(P61,0,find(P61,"M"))` *(formula)* |
| P64 | Parameter64 | Text | `mid(P62,0,find(P62,"M"))` *(formula)* |
| P117 | Parameter58 | Length | `P125-TF3` *(formula)* |
| P124 | Parameter58 | Yes/No | `if (P128==1)  then 0 else if (P129==1)  then 0 else if (P125<=P126) then 0 else if (P117<P126) then 0 else 1 endif endif endif endif` *(formula)* |
| P125 | Parameter58 | Length | `fP(proH,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| P126 | Parameter58 | Length | `fP(proH,"ID67174322-0000-3C4A-3137-323935383333")` *(formula)* |
| P127 | Parameter58 | Length | `fP(proT,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| P128 | Parameter58 | Yes/No | `if (P125<P126) then 1 else 0 endif` *(formula)* |
| P129 | Parameter58 | Yes/No | `if (P43>P133 \|\| P44<P133) then 0 else 1 endif` *(formula)* |
| P130 | Parameter58 | Yes/No | `if (P128==1) then 0 else P134 endif` *(formula)* |
| P131 | Parameter58 | Length | `P125-P127` *(formula)* |
| P132 | Parameter58 | Yes/No | `if (P44>P133) then 1 else 0 endif` *(formula)* |
| P133 | Parameter58 | Length | `P131-(P126-fP(proT,"ID67174322-0000-3C4A-3137-323935383333"))` *(formula)* |
| P134 | Parameter58 | Yes/No | `if (P131==P44) then 0 else if (P131<P44) then 0 else 1 endif endif` *(formula)* |
| CWS | CWS=900 SPS=700 | Integer | `if (P59=="SPS") then 700 else 900 endif` *(formula)* |
| T1 | TF1 | Yes/No | `if (T2<T3) then 1 else 0 endif` *(formula)* |
| T2 | TF1 | Length | `5.00` |
| T3 | TF1 | Length | `fP(proT,"ID67174322-0000-3C4A-3137-323935383333")-fP(proT,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| T4 | TF1 | Length | `fP(proT,"ID67174322-0000-3C4A-3137-323935383333")-fP(proT,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |
| TBF1 | TBF MARK | Text | `fVF("YGY-TB-MOMENT.txt",P1,7)` *(formula)* |
| TF2 | TBF2 MIN. | Length | `5.00` |
| TF3 | TBF2 MAX. | Length | `30.00` |
| TGP1 | TGP MARK | Text | `fVF("YGY-TB-MOMENT.txt",P1,5)` *(formula)* |
| TSF1 | TSF MARK | Text | `fVF("YGY-TB-MOMENT.txt",P1,6)` *(formula)* |
| gw | Parameter58 | Length | `fP(proB,"ID67174322-0000-3C46-3137-323935383333")` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.T.BEAM | `((P3+P7)/2)*P16` |
| D2 | D2.PLATE.T.BEAM | `D1` |
| D3 | D3.PLATE.T.BEAM | `D1` |
| D4 | D4.PLATE.T.BEAM | `D1` |
| D5 | D5.PLATE.T.BEAM | `D1` |
| D6 | D6.PLATE.T.BEAM | `D1` |
| D7 | D7.PLATE.T.BEAM | `D1` |
| D8 | D8.PLATE.T.BEAM | `D1` |
| D9 | D9.PLATE.T.BEAM | `D1` |
| D10 | D10.PLATE.T.BEAM | `D1` |
| D11 | D11.PLATE.T.BEAM | `D1` |
| D12 | D12.PLATE.T.BEAM | `D1` |
| D13 | D13.PLATE.T.BEAM | `` |
| D14 | D14.PLATE.T.BEAM | `` |
| D15 | D15.PLATE.T.BEAM | `P56+0` |
| D16 | D16.PLATE.T.BEAM | `P56+0` |
| D17 | D17.PLATE.T.BEAM | `P8/2` |
| D18 | D18.PLATE.T.BEAM | `D17` |
| D19 | D19.PLATE.T.BEAM | `D17` |
| D20 | D20.PLATE.T.BEAM | `D17` |
| D21 | D21.BOLT.T.BEAM | `` |
| D22 | D22.BOLT.T.BEAM | `` |
| D23 | D23.BOLT.FITTING | `` |
| D24 | D24.BOLT.FITTING | `` |
| D25 | D25.BOLT.T.BEAM | `` |
| D26 | D26.BOLT.T.BEAM | `` |
| D27 | D27.PLATE.T.BEAM | `P36` |
| D28 | D28.PLATE.T.BEAM | `D27` |
| D29 | D29.PLATE.T.BEAM | `P58` |
| D30 | D30.PLATE.T.BEAM | `-P58` |
| D31 | D31.PLATE.T.BEAM | `P57` |
| D32 | D32.PLATE.T.BEAM | `-P57` |
| D33 | D33.PLATE.T.BEAM | `` |
| D34 | D34.PLATE.T.BEAM | `` |
| D35 | D35.PLATE.T.BEAM | `` |
| D36 | D36.PLATE.T.BEAM | `` |
| D37 | D37.PLATE.T.BEAM | `` |
| D38 | D38.PLATE.T.BEAM | `` |
| D39 | D39.PLATE.T.BEAM | `` |
| D40 | D40.PLATE.T.BEAM | `` |
| D41 | D41.CUTPART.T.BEAM | `P4` |
| D42 | D42.CUTPART.T.BEAM | `P4` |
| D43 | D43.CUTPART.T.BEAM | `(P4-2)*tan(P27)` |
| D44 | D44.CUTPART.T.BEAM | `` |
| D45 | D45.CUTPART.T.BEAM | `` |
| D46 | D46.CUTPART.T.BEAM | `P4` |
| D47 | D47.CUTPART.T.BEAM | `P4` |
| D48 | D48.CUTPART.T.BEAM | `(P4-2)*tan(P27)` |
| D49 | D49.CUTPART.T.BEAM | `` |
| D50 | D50.CUTPART.T.BEAM | `` |
| D51 | D51.CUTPART.FITTING | `` |
| D52 | D52.CUTPART.FITTING | `` |
| D53 | D53.CUTPART.FITTING | `` |
| D54 | D54.CUTPART.FITTING | `(P4-2)*tan(P27)` |
| D55 | D55.CUTPART.FITTING | `D51` |
| D56 | D56.CUTPART.FITTING | `D51` |
| D57 | D57.CUTPART.FITTING | `` |
| D58 | D58.CUTPART.FITTING | `` |
| D59 | D59.CUTPART.FITTING | `(P4-2)*tan(P27)` |
| D60 | D60.CUTPART.FITTING | `D51` |
| D61 | D61.PLATE.T.BEAM | `` |
| D62 | D62.PLATE.T.BEAM | `D61` |
| D63 | D63.PLATE.T.BEAM | `D61` |
| D64 | D64.PLATE.T.BEAM | `D61` |
| D65 | D65.PLATE.T.BEAM | `` |
| D66 | D66.PLATE.T.BEAM | `D65` |
| D67 | D67.CUTPART.T.BEAM | `` |
| D68 | D68.CUTPART.T.BEAM | `` |
| D69 | D69.CUTPART.T.BEAM | `` |
| D70 | D70.CUTPART.T.BEAM | `` |
| D71 | D71.CUTPART.T.BEAM | `` |
| D72 | D72.CUTPART.T.BEAM | `` |
| D73 | D73.CUTPART.T.BEAM | `` |
| D74 | D74.CUTPART.T.BEAM | `` |
| D75 | D75.CUTPART.T.BEAM | `` |
| D76 | D76.CUTPART.T.BEAM | `` |
| D77 | D77.CUTPART.T.BEAM | `` |
| D78 | D78.CUTPART.T.BEAM | `` |
| D79 | D79.CUTPART.T.BEAM | `` |
| D80 | D80.CUTPART.T.BEAM | `` |
| D81 | D81.CUTPART.T.BEAM | `` |
| D82 | D82.CUTPART.T.BEAM | `` |
| D83 | D83.CUTPART.FITTING | `` |
| D84 | D84.CUTPART.FITTING | `` |
| D85 | D85.CUTPART.FITTING | `` |
| D86 | D86.CUTPART.FITTING | `` |
| D87 | D87.CUTPART.FITTING | `` |
| D88 | D88.CUTPART.FITTING | `` |
| D89 | D89.CUTPART.FITTING | `` |
| D90 | D90.CUTPART.FITTING | `` |
| D91 | D91.PLATE.PLATE | `` |
| D92 | D92.PLATE.PLATE | `` |
| D93 | D93.PLATE.T.BEAM | `` |
| D94 | D94.PLATE.T.BEAM | `` |
| D95 | D95.PLATE.T.BEAM | `P32` |
| D96 | D96.PLATE.T.BEAM | `P32` |
| D97 | D97.PLATE.PLATE | `` |
| D98 | D98.PLATE.PLATE | `` |
| D99 | D99.FLAT_BAR.FITTING | `` |
| D100 | D100.FLAT_BAR.FITTING | `` |
| D101 | D101.FLAT_BAR.FITTING | `` |
| D102 | D102.FLAT_BAR.FITTING | `` |
| D103 | D103.FLAT_BAR.T.BEAM | `P5/2+20` |
| D104 | D104.FLAT_BAR.T.BEAM | `D103` |
| D105 | D105.FLAT_BAR.T.BEAM | `D103` |
| D106 | D106.FLAT_BAR.T.BEAM | `D103` |
| D107 | D107.FLAT_BAR.T.BEAM | `` |
| D108 | D108.FLAT_BAR.T.BEAM | `` |
| D109 | D109.FLAT_BAR.T.BEAM | `` |
| D110 | D110.FLAT_BAR.T.BEAM | `` |
| D111 | D111.PLATE.T.BEAM | `` |
| D112 | D112.PLATE.T.BEAM | `` |
| D113 | D113.PLATE.T.BEAM | `` |
| D114 | D114.PLATE.T.BEAM | `` |
| D115 | D115.PLATE.T.BEAM | `` |
| D116 | D116.PLATE.T.BEAM | `` |
| D117 | D117.PLATE.T.BEAM | `` |
| D118 | D118.PLATE.T.BEAM | `D117` |
| D119 | D119.FITTING.T.BEAM | `` |
| D120 | D120.FITTING.T.BEAM | `` |
| D121 | D121.FITTING.T.BEAM | `` |
| D122 | D122.PLATE.0-GIR+T_GIR_CONN | `((P3+P7)/2)*P16` |
| D123 | D123.PLATE.0-GIR+T_GIR_CONN | `D122` |
| D124 | D124.PLATE.0-GIR+T_GIR_CONN | `D122` |
| D125 | D125.PLATE.0-GIR+T_GIR_CONN | `D122` |
| D126 | D126.TFPPLATE.T.BEAM | `` |
| D127 | D127.TFPPLATE.T.BEAM | `` |
| D128 | D128.TFPPLATE.T.BEAM | `` |
| D129 | D129.TFPPLATE.T.BEAM | `` |
| D130 | D130.TFPPLATE.T.BEAM | `` |
| D131 | D131.TFPPLATE.T.BEAM | `` |
| D132 | D132.CUTPART.PLATE | `` |
| D133 | D133.CUTPART.PLATE | `` |
| D134 | D134.CUTPART.PLATE | `` |
| D135 | D135.CUTPART.PLATE | `` |
| D136 | D136.CUTPART.PLATE | `(P7/2)+2` |
| D137 | D137.CUTPART.PLATE | `D136` |
| D138 | D138.CUTPART.PLATE | `D136` |
| D139 | D139.CUTPART.PLATE | `D136` |
| D140 | D140.TFPPLATE.T.BEAM | `` |
| D141 | D141.TFPPLATE.T.BEAM | `` |
| D142 | D142.TFPPLATE.T.BEAM | `` |
| D143 | D143.TFPPLATE.T.BEAM | `` |
| D144 | D144.TFPPLATE.T.BEAM | `` |
| D145 | D145.TFPPLATE.T.BEAM | `` |
| D146 | D146.TFP2PLATE.T.BEAM | `` |
| D147 | D147.TFP2PLATE.T.BEAM | `` |
| D148 | D148.TFP2PLATE.T.BEAM | `` |
| D149 | D149.TFP2PLATE.T.BEAM | `` |
| D150 | D150.TFP2PLATE.T.BEAM | `` |
| D151 | D151.TFP2PLATE.T.BEAM | `` |
| D152 | D152.CUTPART.T.BEAM | `` |
| D153 | D153.CUTPART.T.BEAM | `` |
| D154 | D154.CUTPART.PLATE | `` |
| D155 | D155.CUTPART.PLATE | `D154` |
| D156 | D156.CUTPART.PLATE | `` |
| D157 | D157.CUTPART.PLATE | `P39` |
| D158 | D158.CUTPART.PLATE | `P39` |
| D159 | D159.CUTPART.PLATE | `` |
| D160 | D160.CUTPART.T.BEAM | `` |
| D161 | D161.CUTPART.T.BEAM | `P39-2` |
| D162 | D162.CUTPART.T.BEAM | `` |
| D163 | D163.CUTPART.00-GIR+T_GIR_CONN | `` |
| D164 | D164.CUTPART.00-GIR+T_GIR_CONN | `` |
| D165 | D165.CUTPART.00-GIR+T_GIR_CONN | `` |
| D166 | D166.CUTPART.00-GIR+T_GIR_CONN | `` |
| D167 | D167.CUTPART.00-GIR+T_GIR_CONN | `` |
| D168 | D168.CUTPART.00-GIR+T_GIR_CONN | `` |
| D169 | D169.CUTPART.00-GIR+T_GIR_CONN | `` |
| D170 | D170.CUTPART.00-GIR+T_GIR_CONN | `` |
| D171 | D171.CUTPART.00-GIR+T_GIR_CONN | `` |
| D172 | D172.CUTPART.00-GIR+T_GIR_CONN | `` |
| D173 | D173.CUTPART.00-GIR+T_GIR_CONN | `` |
| D174 | D174.PLATE.T.BEAM | `` |
| D175 | D175.CUTPART.00-GIR+T_GIR_CONN | `` |
| D176 | D176.TFP2PLATE.T.BEAM | `` |
| D177 | D177.TFP2PLATE.T.BEAM | `` |
| D178 | D178.TFP2PLATE.T.BEAM | `` |
| D179 | D179.TFP2PLATE.T.BEAM | `` |
| D180 | D180.TFP2PLATE.T.BEAM | `` |
| D181 | D181.TFP2PLATE.T.BEAM | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#3602 | CREATION | `T1` |
| obj#3602 | MATERIAL | `P35` |
| obj#3602 | PROFILE | `"PL"+T4` |
| obj#3602 | SERIE | `P30+"SF"` |
| obj#3614 | CREATION | `P25` |
| obj#3614 | MATERIAL | `P35` |
| obj#3614 | PROFILE | `"PL"+P39` |
| obj#3614 | SERIE | `P30+"SF"` |
| obj#3623 | SERIE | `P64+"FB"` |
| obj#3623 | STARTNUMBER | `P50` |
| obj#3636 | SERIE | `P64+"FB"` |
| obj#3643 | MATERIAL | `P35` |
| obj#3643 | PROFILE | `"PL"+P7` |
| obj#3643 | SERIE | `P30+"GP"` |
| obj#3653 | CREATION | `P37` |
| obj#3653 | MATERIAL | `P35` |
| obj#3653 | PROFILE | `"PL"+P7` |
| obj#3653 | SERIE | `P30+"SF"` |
| obj#3662 | CREATION | `P31` |
| obj#3662 | MATERIAL | `P35` |
| obj#3662 | PROFILE | `"PL"+P7` |
| obj#3662 | SERIE | `P30+"SF"` |
| obj#3671 | CREATION | `P129` |
| obj#3671 | MATERIAL | `P35` |
| obj#3671 | PROFILE | `"PL"+P133` |
| obj#3671 | SERIE | `P30+"SF"` |
| obj#3706 | PROFILE | `P5+500` |
| obj#3718 | PROFILE | `P5+500` |
| obj#3725 | CREATION | `P43` |
| obj#3725 | PROFILE | `P46` |
| obj#3763 | BOLT_GROUP_DISTY | `P19` |
| obj#3763 | SIZE | `P12` |
| obj#3763 | SLOTTED_HOLE_PARTS | `P17` |
| obj#3763 | SLOTTED_HOLE_X | `P15` |
| obj#3763 | TOLERANCE | `P13` |

## Preset `+104.p_♥♥♥YGY_TempBeam-Moment`
```
joint_attributes.saveas_file "+104"
joint_attributes.get_menu "+104"
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
joint_attributes.P43 1
joint_attributes.P40 3
joint_attributes.P31 0
joint_attributes.P11 0
```

## Preset `104L.p_♥♥♥YGY_TempBeam-Moment`
```
joint_attributes.saveas_file "104L"
joint_attributes.get_menu "< Defaults >"
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
joint_attributes.CWS 900
joint_attributes.P43 0
joint_attributes.P40 3
joint_attributes.P31 0
joint_attributes.P30 "1"
joint_attributes.P15 0
joint_attributes.P12 24
joint_attributes.P11 1
```