# CWS-H_COLUMN-T-BAR-1(OK)

- File: `H-COL-T_BAR.uel`  (21,357 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	CWS-H_COLUMN-T-BAR-1(OK)	All`
- Dialog parameters: 20 | Variables (P/custom): 37 | Distance vars (D): 91 | Parts with bound profile: 7
- Lookup files (fVF): CT-BAR-H_COLUMN.txt
- Functions used: fP, fVF, if, mid, tan
- Dialog bitmaps: 0 | Saved presets: #K11.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH250_250_9_14.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH300_300_10_15.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH350_350_12_19.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH400_400.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH414_405.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH440_300.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH458_417_30_50.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH500_200_10_16.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH588_300_12_20.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH596_199_10_15.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH606_201_12_20.p_CWS-H_COLUMN-T-BAR-1(OK), #KKH612_202_13_23.p_CWS-H_COLUMN-T-BAR-1(OK), 0- K 0K.p_CWS-H_COLUMN-T-BAR-1(OK), 0-0000.p_CWS-H_COLUMN-T-BAR-1(OK), 0-91111111111.p_CWS-H_COLUMN-T-BAR-1(OK), 0-H300x300x10x15.p_CWS-H_COLUMN-T-BAR-1(OK), 0-H350x350x12x19.p_CWS-H_COLUMN-T-BAR-1(OK), 0-H582x300x12x17.p_CWS-H_COLUMN-T-BAR-1(OK), 1.p_CWS-H_COLUMN-T-BAR-1(OK), 111-OK.p_CWS-H_COLUMN-T-BAR-1(OK), CT.p_CWS-H_COLUMN-T-BAR-1(OK), H200_200_8_12.p_CWS-H_COLUMN-T-BAR-1(OK), H350_175_7_11.p_CWS-H_COLUMN-T-BAR-1(OK), H600_200_11_17.p_CWS-H_COLUMN-T-BAR-1(OK), TB.p_CWS-H_COLUMN-T-BAR-1(OK)

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "CWS-H_COLUMN-T-BAR-1(OK)")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("CT MAT'L", "P1", material, text, 1)
           parameter("PROFILE", "P2", profile, text, 2)
           parameter("NAME", "P3", string, text, 3)
           parameter("LENGTH", "P4", distance, number, 4)
           parameter("PART PREFIX", "P5", string, text, 5)
           parameter("PART STA_NUMBER", "P6", string, text, 6)
           parameter("ASS PREFIX", "P7", string, text, 7)
           parameter("ASS STA_NUMBER", "P8", string, text, 8)
           parameter("용접길이(L1)", "P10", distance, number, 9)
           parameter("용접길이(L2)", "P11", distance, number, 10)
           parameter("FLAT_BAR", "P14", string, text, 11)
           parameter("MAT'L FB", "P15", material, text, 12)
           parameter("TOP", "P16", YesNo, number, 13)
           parameter("BOTT", "P17", YesNo, number, 14)
           parameter("FB(Y/N)", "P18", YesNo, number, 15)
           parameter("T bar_CLASS", "P19", integer, number, 16)
           parameter("GAP", "P20", distance, number, 17)
           parameter("Angle", "P21", angle, number, 18)
           parameter("Parameter34", "P34", material, text, 19)
           parameter("하부길이지정", "D6", distance, number, 20)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | CT MAT'L | Material | `if(P34=="SHN355") then "SN355" else "SM355" endif` *(formula)* |
| P2 | PROFILE | Profile | `"CT"+P32+"*"+P33+"*"+P27+"*"+P35` *(formula)* |
| P3 | NAME | Text | `T.BAR` |
| P4 | LENGTH | Length | `P24` *(formula)* |
| P5 | PART PREFIX | Text | `P37+"CBT"` *(formula)* |
| P6 | PART STA_NUMBER | Text | `1.00` |
| P7 | ASS PREFIX | Text | `CBT` |
| P8 | ASS STA_NUMBER | Text | `1` |
| P9 | CUT_LENGTH(BL숫자) | Profile | `"BL"+P10` *(formula)* |
| P10 | 용접길이(L1) | Length | `P25` *(formula)* |
| P11 | 용접길이(L2) | Length | `P26` *(formula)* |
| P12 | Parameter12 | Profile | `"BL"+P11` *(formula)* |
| P13 | Parameter13 | Profile | `"BL"+(P4+40)` *(formula)* |
| P14 | FLAT_BAR | Text | `P37+"CFB"` *(formula)* |
| P15 | MAT'L FB | Material | `SS275` |
| P16 | TOP | Yes/No | `0` |
| P17 | BOTT | Yes/No | `0` |
| P18 | FB(Y/N) | Yes/No | `1` |
| P19 | T bar_CLASS | Integer | `P27` *(formula)* |
| P20 | GAP | Length | `10.00` |
| P21 | Angle | Angle | `45.0000` |
| P22 | Parameter22 | Length | `(60-P20)*tan(P21)` *(formula)* |
| P23 | Parameter23 | Profile | `fP(proPROFILE,"ID67A31E8D-0000-1D62-3137-333837343537")` *(formula)* |
| P24 | Parameter24 | Integer | `fVF("CT-BAR-H_COLUMN.txt",P23, 2)` *(formula)* |
| P25 | Parameter25 | Integer | `fVF("CT-BAR-H_COLUMN.txt",P23, 3)` *(formula)* |
| P26 | Parameter26 | Integer | `fVF("CT-BAR-H_COLUMN.txt",P23, 4)` *(formula)* |
| P27 | Parameter27 | Text | `fVF("CT-BAR-H_COLUMN.txt",P23, 5)` *(formula)* |
| P28 | Parameter28 | Integer | `fP(proH,"ID67A31E8D-0000-1D5E-3137-333837343537")` *(formula)* |
| P29 | Parameter29 | Integer | `fP(proT,"ID67A31E8D-0000-1D5E-3137-333837343537")` *(formula)* |
| P30 | Parameter30 | Integer | `fP(proB,"ID67A31E8D-0000-1D5E-3137-333837343537")` *(formula)* |
| P31 | Parameter31 | Integer | `fP(proS,"ID67A31E8D-0000-1D5E-3137-333837343537")` *(formula)* |
| P32 | Parameter32 | Text | `P30/2-P31/2` *(formula)* |
| P33 | Parameter33 | Text | `P28-2*P29` *(formula)* |
| P34 | Parameter34 | Material | `fP(proMATERIAL,"ID67A31E8D-0000-1D5E-3137-333837343537")` *(formula)* |
| P35 | Parameter35 | Text | `fVF("CT-BAR-H_COLUMN.txt",P23, 6)` *(formula)* |
| P36 | Parameter36 | Text | `fP(proSERIE,"ID67A31E8D-0000-1D5E-3137-333837343537")` *(formula)* |
| P37 | Parameter37 | Text | `mid(P36,0,find(P36,"-")+1)` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.T.BAR.COLUMN | `` |
| D2 | D2.T.BAR.COLUMN | `` |
| D3 | D3.T.BAR.COLUMN | `` |
| D4 | D4.T.BAR.COLUMN | `` |
| D5 | D5.T.BAR.GIRDER | `(P4-fP(proH,"ID67A31E8D-0000-1D62-3137-333837343537")-D6)` |
| D6 | 하부길이지정 | `` |
| D7 | D7.PLATE.T.BAR | `` |
| D8 | D8.PLATE.T.BAR | `` |
| D9 | D9.PLATE.T.BAR | `` |
| D10 | D10.PLATE.T.BAR | `` |
| D11 | D11.CUTPART.T.BAR | `` |
| D12 | D12.PLATE.T.BAR | `` |
| D13 | D13.PLATE.T.BAR | `` |
| D14 | D14.PLATE.T.BAR | `` |
| D15 | D15.PLATE.T.BAR | `` |
| D16 | D16.PLATE.T.BAR | `` |
| D17 | D17.PLATE.T.BAR | `` |
| D18 | D18.PLATE.T.BAR | `` |
| D19 | D19.PLATE.T.BAR | `` |
| D20 | D20.CUTPART.T.BAR | `` |
| D21 | D21.CUTPART.T.BAR | `` |
| D22 | D22.CUTPART.T.BAR | `` |
| D23 | D23.CUTPART.T.BAR | `` |
| D24 | D24.CUTPART.T.BAR | `` |
| D25 | D25.CUTPART.T.BAR | `` |
| D26 | D26.CUTPART.T.BAR | `` |
| D27 | D27.CUTPART.T.BAR | `` |
| D28 | D28.CUTPART.T.BAR | `` |
| D29 | D29.CUTPART.T.BAR | `` |
| D30 | D30.CUTPART.T.BAR | `` |
| D31 | D31.CUTPART.T.BAR | `` |
| D32 | D32.CUTPART.T.BAR | `` |
| D33 | D33.CUTPART.T.BAR | `` |
| D34 | D34.CUTPART.T.BAR | `` |
| D35 | D35.CUTPART.CUTPART | `` |
| D36 | D36.CUTPART.T.BAR | `` |
| D37 | D37.CUTPART.T.BAR | `` |
| D38 | D38.CUTPART.T.BAR | `P22` |
| D39 | D39.CUTPART.T.BAR | `` |
| D40 | D40.CUTPART.T.BAR | `` |
| D41 | D41.CUTPART.T.BAR | `` |
| D42 | D42.CUTPART.T.BAR | `` |
| D43 | D43.CUTPART.T.BAR | `` |
| D44 | D44.CUTPART.T.BAR | `` |
| D45 | D45.CUTPART.T.BAR | `P22` |
| D46 | D46.CUTPART.T.BAR | `` |
| D47 | D47.CUTPART.T.BAR | `` |
| D48 | D48.CUTPART.T.BAR | `P20` |
| D49 | D49.CUTPART.T.BAR | `P20` |
| D50 | D50.CUTPART.T.BAR | `` |
| D51 | D51.CUTPART.T.BAR | `` |
| D52 | D52.CUTPART.T.BAR | `` |
| D53 | D53.CUTPART.T.BAR | `` |
| D54 | D54.CUTPART.T.BAR | `` |
| D55 | D55.CUTPART.T.BAR | `` |
| D56 | D56.CUTPART.T.BAR | `` |
| D57 | D57.CUTPART.T.BAR | `` |
| D58 | D58.CUTPART.T.BAR | `` |
| D59 | D59.CUTPART.T.BAR | `` |
| D60 | D60.CUTPART.T.BAR | `` |
| D61 | D61.CUTPART.T.BAR | `` |
| D62 | D62.CUTPART.T.BAR | `` |
| D63 | D63.CUTPART.T.BAR | `` |
| D64 | D64.CUTPART.T.BAR | `` |
| D65 | D65.CUTPART.T.BAR | `` |
| D66 | D66.CUTPART.T.BAR | `` |
| D67 | D67.CUTPART.T.BAR | `` |
| D68 | D68.CUTPART.T.BAR | `` |
| D69 | D69.CUTPART.T.BAR | `` |
| D70 | D70.CUTPART.T.BAR | `` |
| D71 | D71.CUTPART.T.BAR | `` |
| D72 | D72.CUTPART.T.BAR | `` |
| D73 | D73.CUTPART.T.BAR | `` |
| D74 | D74.CUTPART.T.BAR | `` |
| D75 | D75.CUTPART.T.BAR | `` |
| D76 | D76.CUTPART.T.BAR | `` |
| D77 | D77.CUTPART.T.BAR | `` |
| D78 | D78.CUTPART.T.BAR | `` |
| D79 | D79.CUTPART.T.BAR | `-P20` |
| D80 | D80.CUTPART.T.BAR | `` |
| D81 | D81.CUTPART.T.BAR | `` |
| D82 | D82.CUTPART.T.BAR | `P22` |
| D83 | D83.CUTPART.T.BAR | `` |
| D84 | D84.CUTPART.T.BAR | `` |
| D85 | D85.CUTPART.T.BAR | `` |
| D86 | D86.CUTPART.T.BAR | `` |
| D87 | D87.CUTPART.T.BAR | `` |
| D88 | D88.CUTPART.T.BAR | `P20` |
| D89 | D89.CUTPART.T.BAR | `P22` |
| D90 | D90.CUTPART.T.BAR | `` |
| D91 | D91.CUTPART.T.BAR | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#2413 | CREATION | `P18` |
| obj#2413 | MATERIAL | `P15` |
| obj#2413 | SERIE | `P14` |
| obj#2424 | ASSEMBLYSERIE | `P7` |
| obj#2424 | ASSEMBLYSTARTNUMBER | `P8` |
| obj#2424 | CLASS | `P19` |
| obj#2424 | MATERIAL | `P1` |
| obj#2424 | NAME | `P3` |
| obj#2424 | PROFILE | `P2` |
| obj#2424 | SERIE | `P5` |
| obj#2424 | STARTNUMBER | `P6` |
| obj#2437 | CREATION | `P18` |
| obj#2437 | MATERIAL | `P15` |
| obj#2437 | SERIE | `P14` |
| obj#2444 | PROFILE | `P13` |
| obj#2457 | PROFILE | `P13` |
| obj#2467 | PROFILE | `P9` |
| obj#2478 | CREATION | `P17` |
| obj#2489 | CREATION | `P16` |
| obj#2498 | PROFILE | `P12` |
| obj#2508 | PROFILE | `P12` |
| obj#2518 | PROFILE | `P9` |

## Preset `#K11.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#K11"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 130.000000
joint_attributes.P21 45.000000
joint_attributes.P20 5.000000
joint_attributes.P19 7.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 200.000000
joint_attributes.P10 320.000000
joint_attributes.P8 "1"
joint_attributes.P7 "P"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 748.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "CT193.5*358*20*35"
joint_attributes.P1 "SM355"
```

## Preset `#KKH250_250_9_14.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH250*250*9*14"
joint_attributes.get_menu "#KKH300_300_10_15"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 7.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 500.000000
joint_attributes.P10 500.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 500.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*21*21"
joint_attributes.P1 "SM355"
```

## Preset `#KKH300_300_10_15.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH300*300*10*15"
joint_attributes.get_menu "#KKH350*350*12*19"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 300300.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 575.000000
joint_attributes.P10 575.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 575.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*21*21"
joint_attributes.P1 "SM355"
```

## Preset `#KKH350_350_12_19.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH350*350*12*19"
joint_attributes.get_menu "#KKH458*417*30*50"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 7.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 620.000000
joint_attributes.P10 620.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 620.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*30*50"
joint_attributes.P1 "SM355"
```

## Preset `#KKH400_400.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH400_400"
joint_attributes.get_menu "#KKH400_400"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 400.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 670.000000
joint_attributes.P10 670.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 670.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*30*50"
joint_attributes.P1 "SM355"
```

## Preset `#KKH414_405.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH414_405"
joint_attributes.get_menu "#KKH350_350_12_19"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 7.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 684.000000
joint_attributes.P10 684.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 684.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*30*50"
joint_attributes.P1 "SM355"
```

## Preset `#KKH440_300.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH440_300"
joint_attributes.get_menu "#KKH440_300"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 440.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 200.000000
joint_attributes.P10 320.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 700.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*20*35"
joint_attributes.P1 "SM355"
```

## Preset `#KKH458_417_30_50.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH458*417*30*50"
joint_attributes.get_menu "#KKH458*417*30*50"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 7.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 758.000000
joint_attributes.P10 758.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 758.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*30*50"
joint_attributes.P1 "SM355"
```

## Preset `#KKH500_200_10_16.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH500*200*10*16"
joint_attributes.get_menu "#KKH606_201_12_20"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 500.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 200.000000
joint_attributes.P10 300.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 750.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*18*28"
joint_attributes.P1 "SM355"
```

## Preset `#KKH588_300_12_20.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH588_300_12_20"
joint_attributes.get_menu "#KKH612_202_13_23"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 588.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 200.000000
joint_attributes.P10 320.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 848.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*20*35"
joint_attributes.P1 "SM355"
```

## Preset `#KKH596_199_10_15.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH596_199_10_15"
joint_attributes.get_menu "#KKH596_199_10_15"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 596.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 200.000000
joint_attributes.P10 300.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 846.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*18*28"
joint_attributes.P1 "SM355"
```

## Preset `#KKH606_201_12_20.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH606*201*12*20"
joint_attributes.get_menu "#KKH596*199*10*15"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 7.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 200.000000
joint_attributes.P10 300.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 856.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*18*28"
joint_attributes.P1 "SM355"
```

## Preset `#KKH612_202_13_23.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "#KKH612_202_13_23"
joint_attributes.get_menu "#KKH612_202_13_23"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 100.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 612.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 200.000000
joint_attributes.P10 320.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 872.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT280*500*18*28"
joint_attributes.P1 "SM355"
```

## Preset `0- K 0K.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "0- K 0K"
joint_attributes.get_menu "0- K 0K"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P20 5.000000
joint_attributes.P19 8.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P11 200.000000
joint_attributes.P10 300.000000
joint_attributes.P7 "P"
joint_attributes.P5 "DCBT"
joint_attributes.P4 700.000000
joint_attributes.P2 "BT193.5*358*13*21"
```

## Preset `0-0000.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "0-0000"
joint_attributes.get_menu "0-0000"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P20 5.000000
joint_attributes.P19 8.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P11 200.000000
joint_attributes.P10 300.000000
joint_attributes.P7 "P"
joint_attributes.P5 "ECBT"
joint_attributes.P2 "BT193.5*358*13*21"
```

## Preset `0-91111111111.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "0-91111111111"
joint_attributes.get_menu "0-91111111111"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 110.000000
joint_attributes.P21 45.000000
joint_attributes.P20 2.000000
joint_attributes.P19 7.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 230.000000
joint_attributes.P10 230.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 620.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT193.5*348*13*21"
joint_attributes.P1 "SM355"
```

## Preset `0-H300x300x10x15.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "0-H300x300x10x15"
joint_attributes.get_menu "0-H582x300x12x17"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 110.000000
joint_attributes.P21 45.000000
joint_attributes.P20 5.000000
joint_attributes.P19 7.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 230.000000
joint_attributes.P10 230.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 520.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT193.5*358*13*21"
joint_attributes.P1 "SM355"
```

## Preset `0-H350x350x12x19.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "0-H350x350x12x19"
joint_attributes.get_menu "0-H300x300x10x15"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 110.000000
joint_attributes.P21 45.000000
joint_attributes.P20 5.000000
joint_attributes.P19 7.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 240.000000
joint_attributes.P10 240.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 570.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT193.5*358*21*21"
joint_attributes.P1 "SM355"
```

## Preset `0-H582x300x12x17.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "0-H582x300x12x17"
joint_attributes.get_menu "0-H582x300x12x17"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 129.000000
joint_attributes.P21 45.000000
joint_attributes.P20 5.000000
joint_attributes.P19 7.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P15 "SS275"
joint_attributes.P14 "CFB"
joint_attributes.P11 270.000000
joint_attributes.P10 270.000000
joint_attributes.P8 "1"
joint_attributes.P7 "CBT"
joint_attributes.P6 "1"
joint_attributes.P5 "CBT"
joint_attributes.P4 840.000000
joint_attributes.P3 "T.BAR"
joint_attributes.P2 "BT193.5*358*18*28"
joint_attributes.P1 "SM355"
```

## Preset `1.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "1"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
```

## Preset `111-OK.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "111-OK"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P20 0.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
```

## Preset `CT.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "CT"
joint_attributes.get_menu "CT"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 145.000000
joint_attributes.P20 2.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P11 320.000000
joint_attributes.P10 320.000000
joint_attributes.P7 "P"
joint_attributes.P4 1090.000000
joint_attributes.P2 "CT193.5*348*20*35"
joint_attributes.P1 "SM355"
```

## Preset `H200_200_8_12.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "H200*200*8*12"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P20 5.000000
joint_attributes.P19 7.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P11 150.000000
joint_attributes.P10 150.000000
joint_attributes.P4 400.000000
joint_attributes.P2 "CT193.5*358*13*21"
joint_attributes.P1 "SM355"
```

## Preset `H350_175_7_11.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "H350*175*7*11"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P20 5.000000
joint_attributes.P19 7.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P11 200.000000
joint_attributes.P10 300.000000
joint_attributes.P4 600.000000
joint_attributes.P2 "CT193.5*358*13*21"
joint_attributes.P1 "SM355"
```

## Preset `H600_200_11_17.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "H600*200*11*17"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P20 5.000000
joint_attributes.P19 7.000000
joint_attributes.P18 0
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P11 200.000000
joint_attributes.P10 300.000000
joint_attributes.P4 850.000000
joint_attributes.P2 "CT193.5*358*18*28"
joint_attributes.P1 "SM355"
```

## Preset `TB.p_CWS-H_COLUMN-T-BAR-1(OK)`
```
joint_attributes.saveas_file "TB"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D6 126.000000
joint_attributes.P20 2.000000
joint_attributes.P18 1
joint_attributes.P17 0
joint_attributes.P16 0
joint_attributes.P11 270.000000
joint_attributes.P10 270.000000
joint_attributes.P7 "P"
joint_attributes.P4 740.000000
joint_attributes.P2 "CT193.5*348*18*28"
joint_attributes.P1 "SM355"
```