# ♥♥♥YGY_HAUNCH_Moment-TANG AM

- File: `YGY-HAUNCH-MOMENT-TANG AM.uel`  (29,774 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	♥♥♥YGY_HAUNCH_Moment-TANG AM	All`
- Dialog parameters: 16 | Variables (P/custom): 46 | Distance vars (D): 137 | Parts with bound profile: 6
- Lookup files (fVF): YGY-HAUNCH_Moment-미운용층.txt
- Functions used: fP, fVF, if
- Dialog bitmaps: 0 | Saved presets: -3SG4A.p_♥♥♥YGY_HAUNCH_Moment-TANG AM

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "♥♥♥YGY_HAUNCH_Moment-TANG AM")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("GAP", "1_GAP", distance, number, 1)
           parameter("CGP 방향", "P11_NSFS", YesNo, number, 2)
           parameter("BOLT_START", "BOLT_ST", distance, number, 3)
           parameter("R_BOTT.", "P17", YesNo, number, 4)
           parameter("ZONE", "P18", string, text, 5)
           parameter("철판위치 조절", "P19", distance, number, 6)
           parameter("WING1_유무", "P21", YesNo, number, 7)
           parameter("WING2_유무", "P22", YesNo, number, 8)
           parameter("BOLT_SIZE", "P29_diameter", bolt_size, number, 9)
           parameter("BOLT_STANDARD", "P29_screwdin", bolt_standard, text, 10)
           parameter("SLOT_길이", "P31", distance, number, 11)
           parameter("CGP 재질", "P30", string, text, 12)
           parameter("CWS_SPS EDGE", "VAR3", distance, number, 13)
           parameter("CWS_SPS", "VAR4", distance, number, 14)
           parameter("FLG. to Bolt", "BOLT_TOP", distance, number, 15)
           parameter("R_TOP", "P34", YesNo, number, 16)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1_PROF_H | Parameter1 | Length | `fP(proH,"ID65F27854-0000-8E98-3137-313033383934")` *(formula)* |
| P2_PROF_T | Parameter2 | Length | `fP(proT,"ID65F27854-0000-8E98-3137-313033383934")` *(formula)* |
| P3_CUT_PROF | Parameter3 | Length | `P1_PROF_H-(P2_PROF_T*2)-10` *(formula)* |
| P4_PLATE_H | Parameter4 | Length | `fVF("YGY-HAUNCH_Moment-미운용층.txt",P5_PROF,2)` *(formula)* |
| P5_PROF | Parameter5 | Profile | `fP(proPROFILE,"ID65F27854-0000-8E98-3137-313033383934")` *(formula)* |
| P6_PLATE_TOP | Parameter6 | Length | `(P2_PROF_T+25)` *(formula)* |
| P7_PLATE_THK | Parameter7 | Length | `fVF("YGY-HAUNCH_Moment-미운용층.txt",P5_PROF,3)` *(formula)* |
| P8_PLAT_PROF | Parameter8 | Profile | `"PL"+P7_PLATE_THK` *(formula)* |
| P9_PORF_WT | Parameter9 | Length | `fP(proS,"ID65F27854-0000-8E98-3137-313033383934")` *(formula)* |
| P10_GP | Parameter10 | Length | `(P9_PORF_WT/2)+(P7_PLATE_THK/2)` *(formula)* |
| P11_NSFS | CGP 방향 | Yes/No | `1` |
| P12_GP_GAP | Parameter12 | Length | `if P11_NSFS==1 then P10_GP else -P10_GP endif` *(formula)* |
| P13_bolt_slot | Parameter13 | Integer | `if P11_NSFS==1 then 10000 else 20000 endif` *(formula)* |
| P14_CUT | Parameter14 | Integer | `P2_PROF_T+10` *(formula)* |
| P15_CUT_PROF | Parameter15 | Profile | `"CUT"+P14_CUT+"*"+P14_CUT` *(formula)* |
| P16_GP_WELD | GP 용접 | Yes/No | `1` |
| P17 | R_BOTT. | Yes/No | `1` |
| P18 | ZONE | Text | `B1` |
| P19 | 철판위치 조절 | Length | `0.00` |
| P20 | Parameter20 | Length | `fP(proH,"ID65F27854-0000-8E38-3137-313033383934")` *(formula)* |
| P21 | WING1_유무 | Yes/No | `if (D136>74.5) then 1 else 0 endif` *(formula)* |
| P22 | WING2_유무 | Yes/No | `if (D137>74.5) then 1 else 0 endif` *(formula)* |
| P23 | Parameter23 | Length | `fP(proT,"ID65F27854-0000-8E98-3137-313033383934")` *(formula)* |
| P24 | Parameter24 | Material | `fP(proMATERIAL,"ID65F27854-0000-8E98-3137-313033383934")` *(formula)* |
| P25 | Parameter25 | Material | `if (P24=="SHN355") then "SN355" else if (P24=="SHN275") then "SN275" else P24 endif endif` *(formula)* |
| P26 | Parameter26 | Length | `fP(proB,"ID65F27854-0000-8E98-3137-313033383934")` *(formula)* |
| P27 | Parameter27 | Length | `if (P21==1) then (P26/2+D136)+20 else  (P26/2+20) endif` *(formula)* |
| P28 | Parameter28 | Length | `if (P22==1) then (P26/2+D137)+20 else  (P26/2+20) endif` *(formula)* |
| P29_diameter | BOLT_SIZE | 6 | `24.00` |
| P29_screwdin | BOLT_STANDARD | 7 | `T.S.B` |
| P30 | CGP 재질 | Text | `SM355` |
| P31 | SLOT_길이 | Length | `if (P29_diameter==16) then 40 else if (P29_diameter==20) then 50 else if (P29_diameter==22) then 55 else if (P29_diameter==24) then 60 else if (P29_diameter==27) then 67 else endif endif endif endif endif` *(formula)* |
| P32 | BOLT_Tolerance | Length | `if (P29_diameter==16) then 2 else if (P29_diameter==20) then 2 else if (P29_diameter==22) then 2 else if (P29_diameter==24) then 3 else if (P29_diameter==27) then 3 else endif endif endif endif endif` *(formula)* |
| P33 | Parameter33 | Length | `P31-P32-P29_diameter` *(formula)* |
| P34 | R_TOP | Yes/No | `1` |
| BOLT_NSFS | Parameter18 | Integer | `if P11_NSFS==1 then 10000 else 20000 endif` *(formula)* |
| BOLT_ST | BOLT_START | Length | `40` *(formula)* |
| BOLT_TOP | FLG. to Bolt | Length | `fVF("YGY-HAUNCH_Moment-미운용층.txt",P5_PROF,6)` *(formula)* |
| BOLT_X1 | Parameter14 | Profile | `VAR1` *(formula)* |
| BOLT_X2 | Parameter14 | Profile | `"2*"+VAR2` *(formula)* |
| BOLT_X_DIST | Parameter14 | Profile | `if P1_PROF_H<VAR4 then BOLT_X1 else BOLT_X2 endif` *(formula)* |
| VAR1 | Parameter14 | Integer | `(P4_PLATE_H-VAR3)` *(formula)* |
| VAR2 | Parameter14 | Integer | `(P4_PLATE_H-VAR3)/2` *(formula)* |
| VAR3 | CWS_SPS EDGE | Length | `80.00` |
| VAR4 | CWS_SPS | Length | `900.00` |
| WING_L1 | Parameter26 | Length | `fVF("YGY-HAUNCH_Moment-미운용층.txt",P5_PROF,4)` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.FITTING.S-COLUMN | `` |
| D2 | D2.FITTING.S-COLUMN | `` |
| D3 | D3.FITTING.S-COLUMN | `` |
| D4 | D4.CUTPART.S-COLUMN | `1_GAP+5` |
| D5 | D5.CUTPART.S-COLUMN | `1_GAP+5` |
| D6 | D6.CUTPART.S-COLUMN | `1_GAP+5` |
| D7 | D7.CUTPART.S-COLUMN | `1_GAP+5` |
| D8 | D8.CUTPART.HAUNCH | `` |
| D9 | D9.CUTPART.HAUNCH | `` |
| D10 | D10.CUTPART.HAUNCH | `` |
| D11 | D11.CUTPART.HAUNCH | `` |
| D12 | D12.CUTPART.HAUNCH | `` |
| D13 | D13.CUTPART.HAUNCH | `` |
| D14 | D14.CUTPART.HAUNCH | `` |
| D15 | D15.CUTPART.HAUNCH | `` |
| D16 | D16.PLATE.HAUNCH | `P12_GP_GAP` |
| D17 | D17.PLATE.HAUNCH | `P12_GP_GAP` |
| D18 | D18.PLATE.HAUNCH | `P12_GP_GAP` |
| D19 | D19.PLATE.HAUNCH | `P12_GP_GAP` |
| D20 | D20.PLATE.S-COLUMN | `1_GAP+5+100` |
| D21 | D21.PLATE.S-COLUMN | `` |
| D22 | D22.PLATE.S-COLUMN | `` |
| D23 | D23.PLATE.S-COLUMN | `1_GAP+5+100` |
| D24 | D24.CGPPLATE.HAUNCH | `BOLT_TOP-0.5*VAR3` |
| D25 | D25.CGPPLATE.HAUNCH | `BOLT_TOP-0.5*VAR3` |
| D26 | D26.CGPPLATE.HAUNCH | `P4_PLATE_H+D24` |
| D27 | D27.CGPPLATE.HAUNCH | `P4_PLATE_H+D25` |
| D28 | D28.BOLT.PLATE | `` |
| D29 | D29.BOLT.PLATE | `` |
| D30 | D30.BOLT.CUTPART | `` |
| D31 | D31.BOLT.CUTPART | `` |
| D32 | D32.BOLT.CGPPLATE | `` |
| D33 | D33.BOLT.CGPPLATE | `` |
| D34 | D34.FLAT_BAR.HAUNCH | `` |
| D35 | D35.FLAT_BAR.HAUNCH | `` |
| D36 | D36.FLAT_BAR.HAUNCH | `` |
| D37 | D37.FLAT_BAR.HAUNCH | `` |
| D38 | D38.FLAT_BAR.HAUNCH | `` |
| D39 | D39.FLAT_BAR.HAUNCH | `` |
| D40 | D40.FLAT_BAR.HAUNCH | `` |
| D41 | D41.FLAT_BAR.HAUNCH | `` |
| D42 | D42.FLAT_BAR.HAUNCH | `` |
| D43 | D43.FLAT_BAR.HAUNCH | `P28` |
| D44 | D44.FLAT_BAR.HAUNCH | `` |
| D45 | D45.FLAT_BAR.HAUNCH | `P27` |
| D46 | D46.X.HAUNCH | `` |
| D47 | D47.X.HAUNCH | `` |
| D48 | D48.X.HAUNCH | `` |
| D49 | D49.X.HAUNCH | `` |
| D50 | D50.X.HAUNCH | `` |
| D51 | D51.X.HAUNCH | `` |
| D52 | D52.X.HAUNCH | `` |
| D53 | D53.X.HAUNCH | `` |
| D54 | D54.X.FITTING | `` |
| D55 | D55.X.FITTING | `` |
| D56 | D56.X.HAUNCH | `` |
| D57 | D57.X.HAUNCH | `` |
| D58 | D58.CUTPART.HAUNCH | `` |
| D59 | D59.CUTPART.HAUNCH | `` |
| D60 | D60.CUTPART.HAUNCH | `` |
| D61 | D61.CUTPART.HAUNCH | `` |
| D62 | D62.CUTPART.HAUNCH | `` |
| D63 | D63.CUTPART.HAUNCH | `` |
| D64 | D64.CUTPART.HAUNCH | `` |
| D65 | D65.CUTPART.HAUNCH | `` |
| D66 | D66.CUTPART.CUTPART | `` |
| D67 | D67.CUTPART.CUTPART | `` |
| D68 | D68.CUTPART.CUTPART | `` |
| D69 | D69.CUTPART.CUTPART | `` |
| D70 | D70.CUTPART.HAUNCH | `` |
| D71 | D71.CUTPART.HAUNCH | `` |
| D72 | D72.CUTPART.HAUNCH | `` |
| D73 | D73.CUTPART.HAUNCH | `` |
| D74 | D74.CUTPART.HAUNCH | `` |
| D75 | D75.CUTPART.HAUNCH | `` |
| D76 | D76.CUTPART.HAUNCH | `` |
| D77 | D77.CUTPART.HAUNCH | `` |
| D78 | D78.CUTPART.CUTPART | `` |
| D79 | D79.CUTPART.CUTPART | `` |
| D80 | D80.CUTPART.CUTPART | `` |
| D81 | D81.CUTPART.CUTPART | `` |
| D82 | D82.PLATE.HAUNCH | `` |
| D83 | D83.PLATE.HAUNCH | `` |
| D84 | D84.PLATE.HAUNCH | `` |
| D85 | D85.PLATE.HAUNCH | `` |
| D86 | D86.PLATE.HAUNCH | `` |
| D87 | D87.PLATE.HAUNCH | `` |
| D88 | D88.PLATE.HAUNCH | `` |
| D89 | D89.PLATE.HAUNCH | `` |
| D90 | D90.PLATE.HAUNCH | `` |
| D91 | D91.PLATE.HAUNCH | `` |
| D92 | D92.PLATE.FITTING | `WING_L1/2` |
| D93 | D93.PLATE.FITTING | `WING_L1` |
| D94 | D94.PLATE.FITTING | `WING_L1` |
| D95 | D95.PLATE.FITTING | `` |
| D96 | D96.PLATE.FITTING | `` |
| D97 | D97.PLATE.FITTING | `WING_L1/2` |
| D98 | D98.PLATE.FITTING | `WING_L1` |
| D99 | D99.PLATE.FITTING | `WING_L1` |
| D100 | D100.PLATE.FITTING | `` |
| D101 | D101.PLATE.FITTING | `` |
| D102 | D102.PLATE.S-COLUMN | `P20/2` |
| D103 | D103.PLATE.HAUNCH | `` |
| D104 | D104.PLATE.HAUNCH | `` |
| D105 | D105.PLATE.HAUNCH | `` |
| D106 | D106.PLATE.S-COLUMN | `P20/2` |
| D107 | D107.PLATE.S-COLUMN | `P20/2` |
| D108 | D108.PLATE.HAUNCH | `` |
| D109 | D109.PLATE.HAUNCH | `` |
| D110 | D110.PLATE.HAUNCH | `` |
| D111 | D111.PLATE.S-COLUMN | `P20/2` |
| D112 | D112.X.PLATE | `` |
| D113 | D113.X.PLATE | `` |
| D114 | D114.X.HAUNCH | `` |
| D115 | D115.X.HAUNCH | `` |
| D116 | D116.X.HAUNCH | `` |
| D117 | D117.X.HAUNCH | `` |
| D118 | D118.X.PLATE | `` |
| D119 | D119.X.PLATE | `` |
| D120 | D120.X.HAUNCH | `` |
| D121 | D121.X.HAUNCH | `` |
| D122 | D122.X.HAUNCH | `` |
| D123 | D123.X.HAUNCH | `` |
| D124 | D124.X.HAUNCH | `` |
| D125 | D125.X.HAUNCH | `` |
| D126 | D126.X.HAUNCH | `` |
| D127 | D127.X.HAUNCH | `` |
| D128 | D128.X.PLATE | `` |
| D129 | D129.X.PLATE | `` |
| D130 | D130.X.PLATE | `` |
| D131 | D131.X.PLATE | `` |
| D132 | D132.X.PLATE | `` |
| D133 | D133.X.PLATE | `` |
| D134 | D134.X.PLATE | `` |
| D135 | D135.X.PLATE | `` |
| D136 | D136.PLATE.S-COLUMN | `` |
| D137 | D137.PLATE.S-COLUMN | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#411 | MATERIAL | `P30` |
| obj#411 | PROFILE | `P8_PLAT_PROF` |
| obj#411 | SERIE | `P18+"CGP"` |
| obj#422 | SERIE | `P18+"FB"` |
| obj#433 | SERIE | `P18+"FB"` |
| obj#440 | CREATION | `P22` |
| obj#440 | MATERIAL | `P25` |
| obj#440 | PROFILE | `"PL"+P23` |
| obj#440 | SERIE | `P18+"GWP"` |
| obj#452 | CREATION | `P21` |
| obj#452 | MATERIAL | `P25` |
| obj#452 | PROFILE | `"PL"+P23` |
| obj#452 | SERIE | `P18+"GWP"` |
| obj#460 | CREATION | `P17` |
| obj#471 | CREATION | `P34` |
| obj#479 | PROFILE | `P3_CUT_PROF` |
| obj#490 | PROFILE | `P15_CUT_PROF` |
| obj#508 | PROFILE | `P15_CUT_PROF` |
| obj#557 | BOLT_GROUP_DISTY | `BOLT_X_DIST` |
| obj#557 | DX1 | `BOLT_ST` |
| obj#557 | SIZE | `P29_diameter` |
| obj#557 | SLOTTED_HOLE_PARTS | `BOLT_NSFS` |
| obj#557 | SLOTTED_HOLE_X | `P33` |
| obj#557 | STANDARD | `P29_screwdin` |
| obj#557 | TOLERANCE | `P32` |
| obj#1027 | WELDING_SITE | `P16_GP_WELD` |

## Preset `-3SG4A.p_♥♥♥YGY_HAUNCH_Moment-TANG AM`
```
joint_attributes.saveas_file "-3SG4A"
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
joint_attributes.group_no 7
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.P34 1
joint_attributes.BOLT_TOP 90.000000
joint_attributes.P29_screwdin "T.S.B"
joint_attributes.P29_diameter 24.000000
joint_attributes.P22 1
joint_attributes.P21 1
joint_attributes.P18 "B3"
joint_attributes.P17 1
joint_attributes.P11_NSFS 1
```