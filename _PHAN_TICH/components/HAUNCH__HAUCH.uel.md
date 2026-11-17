# 0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP)

- File: `HAUNCH\HAUCH.uel`  (32,267 bytes, gzip)
- Type in .inp: **macro**  | Catalog line: `-10	0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP)	All`
- Dialog parameters: 9 | Variables (P/custom): 40 | Distance vars (D): 213 | Parts with bound profile: 5
- Lookup files (fVF): DM-HAUNCH.txt
- Functions used: fP, fTpl, fVF, if, mid
- Dialog bitmaps: 0 | Saved presets: #-1SG1.p_0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP), 0-2BEN 1F.p_0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP)

## Dialog (.inp)
```
page("TeklaStructures","")
{
   macro(1, "0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP)")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("FINISH", "P4", string, text, 1)
           parameter("LEFT TAPER BEAM (Y/N)", "P8", YesNo, number, 2)
           parameter("RIGHT TAPER BEAM (Y/N)", "P9", YesNo, number, 3)
           parameter("WEB_GAP (Y/N)", "P17", YesNo, number, 4)
           parameter("ASS_START NUM", "P19", integer, number, 5)
           parameter("Part-Start Num", "P20", integer, number, 6)
           parameter("Ln/L (Auto)", "P25", distance, number, 7)
           parameter("Gap_Conn. (Col2Gir)", "P28", distance, number, 8)
           parameter("ZONE", "P32", string, text, 9)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | MAIN_PROFILE | Profile | `P34` *(formula)* |
| P2 | SUB_PROFILE | Profile | `P33` *(formula)* |
| P3 | MAIN_MAT'L | Material | `P39` *(formula)* |
| P4 | FINISH | Text | `-4SG1` |
| P5 | ASS_PREFIX | Text | `P41+P32+"G"` *(formula)* |
| P6 | MAIN_PART_PREFIX | Text | `P41+P32+"MG"` *(formula)* |
| P7 | SUB_PART_PREFIX | Text | `P41+P32+"MHN"` *(formula)* |
| P8 | LEFT TAPER BEAM (Y/N) | Yes/No | `1` |
| P9 | RIGHT TAPER BEAM (Y/N) | Yes/No | `0` |
| P10 | SUB-LENGTH (Auto) | Length | `P26` *(formula)* |
| P11 | Parameter11 | Length | `fP(proS,"ID65546C6A-0000-80CD-3137-303030333136")` *(formula)* |
| P12 | Parameter12 | Length | `fP(proH,"ID65546C6A-0000-80CD-3137-303030333136")` *(formula)* |
| P13 | Parameter13 | Length | `fP(proT,"ID65546C6A-0000-80CD-3137-303030333136")` *(formula)* |
| P14 | Parameter14 | Length | `P12-2*P13` *(formula)* |
| P15 | GAP | Length | `5.00` |
| P16 | FB _ PREFIX | Text | `P41+P32+"FB"` *(formula)* |
| P18 | SUB_MAT'L | Material | `P38` *(formula)* |
| P19 | ASS_START NUM | Integer | `1` |
| P20 | Part-Start Num | Integer | `1` |
| P21 | Parameter21 | Yes/No | `if (P8==0 \|\| P17==0) then 0 else P17 endif` *(formula)* |
| P22 | R | Yes/No | `if (P9==0 \|\| P17==0) then 0 else P17 endif` *(formula)* |
| P23 | TAPER CLASS | Integer | `P35` *(formula)* |
| P24 | Parameter24 | Length | `fTpl("LENGTH_GROSS","ID65546C6A-0000-80CD-3137-303030333136")` *(formula)* |
| P25 | Ln/L (Auto) | Length | `P37` *(formula)* |
| P26 | Parameter26 | Length | `ceil(P24*P25/10)*10` *(formula)* |
| P27 | Parameter27 | Length | `fP(proH,"ID65546C6A-0000-80CD-3137-303030333136")` *(formula)* |
| P28 | Gap_Conn. (Col2Gir) | Length | `5.00` |
| P29 | Parameter29 | Integer | `find(P2, "-")` *(formula)* |
| P30 | Parameter30 | Integer | `find(P2, "*")` *(formula)* |
| P31 | Parameter31 | Length | `mid(P2,P29+1,P30-1)` *(formula)* |
| P32 | ZONE | Text | `` |
| P33 | Parameter33 | Profile | `fVF("DM-HAUNCH.txt",P4,2)` *(formula)* |
| P34 | Parameter34 | Profile | `fVF("DM-HAUNCH.txt",P4,3)` *(formula)* |
| P35 | Parameter35 | Integer | `fVF("DM-HAUNCH.txt",P4,4)` *(formula)* |
| P36 | Parameter36 | Length | `fVF("DM-HAUNCH.txt",P4,5)` *(formula)* |
| P37 | Parameter37 | Length | `fVF("DM-HAUNCH.txt",P4,6)` *(formula)* |
| P38 | Parameter38 | Text | `fVF("DM-HAUNCH.txt",P4,7)` *(formula)* |
| P39 | Parameter39 | Text | `fVF("DM-HAUNCH.txt",P4,8)` *(formula)* |
| P40 | Parameter40 | Length | `0.00` |
| P41 | Parameter41 | Text | `fVF("DM-HAUNCH.txt",P4,9)` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.linecut.B1SG11 BM | `D5` |
| D2 | D2.linecut.B1SG11 BM | `D5` |
| D3 | D3.linecut.B1SG11 BM | `D5` |
| D4 | D4.GIRDER.B1SG11 BM | `` |
| D5 | SUB-LENGTH | `P10+P28` |
| D6 | D6.HAUNCH.B1SG11 BM | `-P28` |
| D7 | D7.HAUNCH.B1SG11 BM | `D5` |
| D8 | D8.HAUNCH.B1SG11 BM | `P28` |
| D9 | D9.linecut.B1SG11 BM | `D5` |
| D10 | D10.linecut.B1SG11 BM | `D5` |
| D11 | D11.linecut.B1SG11 BM | `D5` |
| D12 | D12.CUTPART.GIRDER | `` |
| D13 | D13.CUTPART.GIRDER | `` |
| D14 | D14.CUTPART.GIRDER | `` |
| D15 | D15.CUTPART.linecut | `` |
| D16 | D16.CUTPART.linecut | `` |
| D17 | D17.CUTPART.linecut | `` |
| D18 | D18.CUTPART.GIRDER | `` |
| D19 | D19.CUTPART.GIRDER | `` |
| D20 | D20.CUTPART.GIRDER | `` |
| D21 | D21.CUTPART.linecut | `` |
| D22 | D22.CUTPART.linecut | `` |
| D23 | D23.CUTPART.linecut | `` |
| D24 | D24.CUTPART.linecut | `` |
| D25 | D25.CUTPART.linecut | `` |
| D26 | D26.CUTPART.linecut | `` |
| D27 | D27.CUTPART.linecut | `` |
| D28 | D28.CUTPART.linecut | `` |
| D29 | D29.CUTPART.linecut | `` |
| D30 | D30.CUTPART.GIRDER | `` |
| D31 | D31.CUTPART.GIRDER | `` |
| D32 | D32.CUTPART.GIRDER | `` |
| D33 | D33.CUTPART.GIRDER | `` |
| D34 | D34.CUTPART.GIRDER | `` |
| D35 | D35.CUTPART.GIRDER | `` |
| D36 | D36.CUTPART.GIRDER | `` |
| D37 | D37.CUTPART.GIRDER | `` |
| D38 | D38.CUTPART.GIRDER | `` |
| D39 | D39.CUTPART.GIRDER | `` |
| D40 | D40.CUTPART.linecut | `` |
| D41 | D41.CUTPART.linecut | `` |
| D42 | D42.CUTPART.linecut | `` |
| D43 | D43.CUTPART.linecut | `` |
| D44 | D44.CUTPART.linecut | `` |
| D45 | D45.CUTPART.linecut | `` |
| D46 | D46.CUTPART.linecut | `` |
| D47 | D47.CUTPART.linecut | `` |
| D48 | D48.CUTPART.GIRDER | `` |
| D49 | D49.CUTPART.GIRDER | `` |
| D50 | D50.CUTPART.GIRDER | `` |
| D51 | D51.CUTPART.GIRDER | `` |
| D52 | D52.CUTPART.GIRDER | `` |
| D53 | D53.CUTPART.GIRDER | `` |
| D54 | D54.CUTPART.GIRDER | `` |
| D55 | D55.CUTPART.GIRDER | `` |
| D56 | D56.CUTPART.GIRDER | `` |
| D57 | D57.CUTPART.GIRDER | `` |
| D58 | D58.CUTPART.GIRDER | `` |
| D59 | D59.CUTPART.GIRDER | `` |
| D60 | D60.CUTPART.linecut | `` |
| D61 | D61.CUTPART.linecut | `` |
| D62 | D62.CUTPART.linecut | `` |
| D63 | D63.CUTPART.linecut | `` |
| D64 | D64.CUTPART.linecut | `` |
| D65 | D65.CUTPART.linecut | `` |
| D66 | D66.CUTPART.linecut | `` |
| D67 | D67.CUTPART.linecut | `` |
| D68 | D68.CUTPART.linecut | `` |
| D69 | D69.CUTPART.linecut | `` |
| D70 | D70.CUTPART.linecut | `` |
| D71 | D71.CUTPART.linecut | `` |
| D72 | D72.CUTPART.HAUNCH | `` |
| D73 | D73.CUTPART.HAUNCH | `` |
| D74 | D74.CUTPART.HAUNCH | `` |
| D75 | D75.CUTPART.HAUNCH | `` |
| D76 | D76.CUTPART.HAUNCH | `` |
| D77 | D77.CUTPART.HAUNCH | `` |
| D78 | D78.CUTPART.HAUNCH | `` |
| D79 | D79.CUTPART.HAUNCH | `` |
| D80 | D80.CUTPART.linecut | `` |
| D81 | D81.CUTPART.linecut | `` |
| D82 | D82.CUTPART.linecut | `` |
| D83 | D83.CUTPART.linecut | `` |
| D84 | D84.CUTPART.linecut | `` |
| D85 | D85.CUTPART.linecut | `` |
| D86 | D86.CUTPART.linecut | `` |
| D87 | D87.CUTPART.linecut | `` |
| D88 | D88.CUTPART.linecut | `` |
| D89 | D89.CUTPART.linecut | `` |
| D90 | D90.CUTPART.linecut | `` |
| D91 | D91.CUTPART.linecut | `` |
| D92 | D92.CUTPART.HAUNCH | `` |
| D93 | D93.CUTPART.HAUNCH | `` |
| D94 | D94.CUTPART.HAUNCH | `` |
| D95 | D95.CUTPART.HAUNCH | `` |
| D96 | D96.CUTPART.HAUNCH | `` |
| D97 | D97.CUTPART.HAUNCH | `` |
| D98 | D98.CUTPART.HAUNCH | `` |
| D99 | D99.CUTPART.HAUNCH | `` |
| D100 | D100.CUTPART.linecut | `` |
| D101 | D101.CUTPART.linecut | `` |
| D102 | D102.CUTPART.linecut | `` |
| D103 | D103.CUTPART.linecut | `` |
| D104 | D104.CUTPART.GIRDER | `` |
| D105 | D105.CUTPART.GIRDER | `` |
| D106 | D106.CUTPART.GIRDER | `` |
| D107 | D107.CUTPART.GIRDER | `` |
| D108 | D108.CUTPART.GIRDER | `` |
| D109 | D109.CUTPART.GIRDER | `` |
| D110 | D110.CUTPART.GIRDER | `` |
| D111 | D111.CUTPART.GIRDER | `` |
| D112 | D112.CUTPART.linecut | `` |
| D113 | D113.CUTPART.linecut | `` |
| D114 | D114.CUTPART.linecut | `` |
| D115 | D115.CUTPART.linecut | `` |
| D116 | D116.CUTPART.GIRDER | `` |
| D117 | D117.CUTPART.GIRDER | `` |
| D118 | D118.CUTPART.GIRDER | `` |
| D119 | D119.CUTPART.GIRDER | `` |
| D120 | D120.CUTPART.linecut | `` |
| D121 | D121.CUTPART.linecut | `` |
| D122 | D122.CUTPART.linecut | `` |
| D123 | D123.CUTPART.linecut | `` |
| D124 | D124.CUTPART.GIRDER | `` |
| D125 | D125.CUTPART.GIRDER | `` |
| D126 | D126.CUTPART.GIRDER | `` |
| D127 | D127.CUTPART.GIRDER | `` |
| D128 | D128.CUTPART.linecut | `` |
| D129 | D129.CUTPART.linecut | `` |
| D130 | D130.CUTPART.linecut | `` |
| D131 | D131.CUTPART.linecut | `` |
| D132 | D132.PLATE.GIRDER | `` |
| D133 | D133.PLATE.GIRDER | `` |
| D134 | D134.PLATE.GIRDER | `` |
| D135 | D135.PLATE.GIRDER | `` |
| D136 | D136.PLATE.GIRDER | `` |
| D137 | D137.PLATE.GIRDER | `` |
| D138 | D138.PLATE.GIRDER | `` |
| D139 | D139.PLATE.GIRDER | `` |
| D140 | D140.PLATE.GIRDER | `` |
| D141 | D141.PLATE.GIRDER | `` |
| D142 | D142.PLATE.GIRDER | `` |
| D143 | D143.PLATE.GIRDER | `` |
| D144 | D144.PLATE.GIRDER | `` |
| D145 | D145.PLATE.GIRDER | `` |
| D146 | D146.PLATE.GIRDER | `` |
| D147 | D147.PLATE.GIRDER | `` |
| D148 | D148.PLATE.linecut | `` |
| D149 | D149.PLATE.linecut | `` |
| D150 | D150.PLATE.linecut | `` |
| D151 | D151.PLATE.linecut | `` |
| D152 | D152.PLATE.linecut | `` |
| D153 | D153.PLATE.linecut | `` |
| D154 | D154.PLATE.linecut | `` |
| D155 | D155.PLATE.linecut | `` |
| D156 | D156.HAUNCH.GIRDER | `D158` |
| D157 | D157.HAUNCH.GIRDER | `D158` |
| D158 | HAUN_Z | `-0.5*P12` |
| D159 | D159.HAUNCH.GIRDER | `D158` |
| D160 | D160.CUTPART.GIRDER | `` |
| D161 | D161.CUTPART.GIRDER | `` |
| D162 | D162.CUTPART.GIRDER | `` |
| D163 | D163.CUTPART.GIRDER | `` |
| D164 | D164.CUTPART.HAUNCH | `P15` |
| D165 | D165.CUTPART.HAUNCH | `` |
| D166 | D166.CUTPART.HAUNCH | `P15` |
| D167 | D167.CUTPART.HAUNCH | `` |
| D168 | D168.CUTPART.HAUNCH | `` |
| D169 | D169.CUTPART.HAUNCH | `` |
| D170 | D170.CUTPART.HAUNCH | `P15` |
| D171 | D171.CUTPART.HAUNCH | `P15` |
| D172 | D172.CUTPART.GIRDER | `` |
| D173 | D173.CUTPART.GIRDER | `` |
| D174 | D174.CUTPART.GIRDER | `` |
| D175 | D175.CUTPART.GIRDER | `` |
| D176 | D176.CUTPART.GIRDER | `` |
| D177 | D177.CUTPART.GIRDER | `` |
| D178 | D178.CUTPART.GIRDER | `` |
| D179 | D179.CUTPART.GIRDER | `` |
| D180 | D180.CUTPART.GIRDER | `` |
| D181 | D181.CUTPART.GIRDER | `` |
| D182 | D182.CUTPART.CUTPART | `P15+P11-2` |
| D183 | D183.CUTPART.CUTPART | `P15` |
| D184 | D184.CUTPART.CUTPART | `P15` |
| D185 | D185.CUTPART.GIRDER | `` |
| D186 | D186.CUTPART.GIRDER | `P11-2` |
| D187 | D187.CUTPART.GIRDER | `` |
| D188 | D188.CUTPART.GIRDER | `` |
| D189 | D189.CUTPART.GIRDER | `P11-2` |
| D190 | D190.CUTPART.GIRDER | `` |
| D191 | D191.CUTPART.CUTPART | `P15+P11-2` |
| D192 | D192.CUTPART.CUTPART | `P15` |
| D193 | D193.CUTPART.CUTPART | `P15` |
| D194 | D194.PLATE.GIRDER | `` |
| D195 | D195.PLATE.GIRDER | `` |
| D196 | D196.PLATE.GIRDER | `` |
| D197 | D197.PLATE.GIRDER | `` |
| D198 | D198.PLATE.PLATE | `` |
| D199 | D199.PLATE.PLATE | `` |
| D200 | D200.PLATE.PLATE | `` |
| D201 | D201.PLATE.PLATE | `` |
| D202 | D202.PLATE.PLATE | `` |
| D203 | D203.PLATE.PLATE | `` |
| D204 | D204.PLATE.PLATE | `` |
| D205 | D205.PLATE.PLATE | `` |
| D206 | D206.CUTPART.GIRDER | `` |
| D207 | D207.CUTPART.GIRDER | `` |
| D208 | D208.CUTPART.GIRDER | `` |
| D209 | D209.CUTPART.GIRDER | `` |
| D210 | D210.CUTPART.GIRDER | `` |
| D211 | D211.CUTPART.GIRDER | `` |
| D212 | D212.CUTPART.GIRDER | `` |
| D213 | D213.CUTPART.GIRDER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#144 | ASSEMBLYSERIE | `P5` |
| obj#144 | ASSEMBLYSTARTNUMBER | `P19` |
| obj#144 | MATERIAL | `P3` |
| obj#144 | PROFILE | `P1` |
| obj#144 | SERIE | `P6` |
| obj#144 | STARTNUMBER | `P20` |
| obj#168 | ASSEMBLYSERIE | `P5` |
| obj#168 | CLASS | `P23` |
| obj#168 | CREATION | `P8` |
| obj#168 | MATERIAL | `P18` |
| obj#168 | PROFILE | `P2` |
| obj#168 | SERIE | `P7` |
| obj#168 | STARTNUMBER | `P20` |
| obj#179 | ASSEMBLYSERIE | `P5` |
| obj#179 | CLASS | `P23` |
| obj#179 | CREATION | `P9` |
| obj#179 | MATERIAL | `P18` |
| obj#179 | PROFILE | `P2` |
| obj#179 | SERIE | `P7` |
| obj#179 | STARTNUMBER | `P20` |
| obj#187 | CREATION | `P9` |
| obj#187 | SERIE | `P16` |
| obj#197 | CREATION | `P9` |
| obj#197 | SERIE | `P16` |
| obj#204 | CREATION | `P8` |
| obj#204 | SERIE | `P16` |
| obj#211 | CREATION | `P8` |
| obj#211 | SERIE | `P16` |
| obj#218 | CREATION | `P21` |
| obj#218 | SERIE | `P16` |
| obj#226 | CREATION | `P22` |
| obj#226 | SERIE | `P16` |
| obj#233 | CREATION | `P9` |
| obj#245 | CREATION | `P8` |
| obj#253 | CREATION | `P9` |
| obj#262 | CREATION | `P22` |
| obj#272 | CREATION | `P21` |
| obj#279 | CREATION | `P8` |
| obj#288 | CREATION | `P9` |
| obj#295 | CREATION | `P9` |
| obj#304 | CREATION | `P8` |
| obj#310 | CREATION | `P21` |
| obj#310 | PROFILE | `"BL"+P14` |
| obj#320 | CREATION | `P22` |
| obj#320 | PROFILE | `"BL"+P14` |
| obj#328 | CREATION | `P8` |
| obj#336 | CREATION | `P8` |
| obj#344 | CREATION | `P8` |
| obj#351 | CREATION | `P9` |
| obj#359 | CREATION | `P8` |
| obj#366 | CREATION | `P9` |
| obj#374 | CREATION | `P9` |
| obj#393 | CREATION | `P8` |

## Preset `#-1SG1.p_0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP)`
```
joint_attributes.get_menu "< ExternalDesign >"
joint_attributes.name_enable 0
joint_attributes.ThirdHandle 0
joint_attributes.AtDepthValue -0.000000
joint_attributes.AtDepth 2
joint_attributes.RotationValue -0.000000
joint_attributes.Rotation 1
joint_attributes.OnPlaneValue 0.000000
joint_attributes.OnPlane 0
joint_attributes.P32 "B1A"
joint_attributes.P20 1
joint_attributes.P19 1
joint_attributes.P17 1
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P4 "-1SG1"
```

## Preset `0-2BEN 1F.p_0-DM-HAUNCH-BEAM2 (FLG+WEB-CJP)`
```
joint_attributes.saveas_file "0-2BEN 1F"
joint_attributes.get_menu "0-2BEN 1F"
joint_attributes.name_enable 0
joint_attributes.ThirdHandle 0
joint_attributes.AtDepthValue -0.000000
joint_attributes.AtDepth 2
joint_attributes.RotationValue -0.000000
joint_attributes.Rotation 1
joint_attributes.OnPlaneValue 0.000000
joint_attributes.OnPlane 0
joint_attributes.P32 "B"
joint_attributes.P20 1
joint_attributes.P19 1
joint_attributes.P17 1
joint_attributes.P9 1
joint_attributes.P8 1
joint_attributes.P4 "1SG1"
```