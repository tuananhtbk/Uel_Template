# NEC_POST

- File: `NEC\NEC_POST.uel`  (50,836 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	NEC_POST	All`
- Dialog parameters: 3 | Variables (P/custom): 31 | Distance vars (D): 23 | Parts with bound profile: 8
- Lookup files (fVF): -
- Functions used: fMarketSize, fP, if, int
- Dialog bitmaps: 5 | Saved presets: standard.p_NEC_POST, standard.p_NEC_POST

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "NEC_POST")
   {
       tab_page("", " Pictures ", 1)
       {
           
           picture("NEC_LOGO",8,2,420,500)
           picture("NEC_POST1",100,50,50,100)
           picture("NEC_POST_ST2",100,50,540,112)
           

           parameter("", "P27", distance, number, 470,157,50)
           parameter("", "P23", distance, number, 285,75,60)
           parameter("", "P24", distance, number, 175,75,60)
           parameter("", "P25", distance, number, 380,75,50)
           parameter("", "P26", distance, number, 90,75,50)
           parameter("Connection type", "P28_name", ComponentName, text, 15)
           parameter("Connection Attribute", "P28_attrfile", ComponentAttributeFile, text, 16)
           attribute("", " ", label,"%s",  none, none, "0.0", "0.0",380,520)
           attribute("", "스티프너 옵션", label,"%s",  none, none, "0.0", "0.0",605,87)
           attribute("P29", "", option, "%s", no, none, "0.0", "0.0", 540,112,250)
                {
                     value("NEC_POST_ST2.xbm",1)
                     value("NEC_POST_ST1.xbm",0)
                }
           
       }
       tab_page("", " Plates ", 2)
       {
           
           picture("NEC_POST2",100,50,200,200)
           
           attribute("", "1.Stiffener", label,"%s",  none, none, "0.0", "0.0",20,50)
           attribute("", "2.Rib Plate", label,"%s",  none, none, "0.0", "0.0",20,75)
           attribute("t", "t", label,"%s",  none, none, "0.0", "0.0",190,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",320,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",520,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",670,25)
           
           parameter("", "P1", distance, number, 180,50,50)
           parameter("", "P2", string, text, 180,75,50)
           parameter("", "P6", string, text, 320,50,50)
           parameter("", "P7", string, text, 400,50,40)
           parameter("", "P8", string, text, 320,75,50)
           parameter("", "P9", string, text, 400,75,40)
           parameter("", "P10", material, text, 520,50,90)
           parameter("", "P11", material, text, 520,75,90)
           parameter("", "P12", string, text, 670,50,120)
           parameter("", "P13", string, text, 670,75,120)
           
           parameter("", "P14", chamfer_type, number, 150,283,80)
           parameter("", "P15", distance, number, 150,308,50)
           parameter("", "P16", distance, number, 150,333,50)
           parameter("", "P18", distance, number, 515,223,60)
           parameter("", "P19", distance, number, 515,248,60)
           parameter("", "P20", distance, number, 480,303,60)
           parameter("", "P21", distance, number, 350,430,60)
           parameter("", "P22", distance, number, 455,365,60)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | 1번 두께 | Length | `12.00` |
| P2 | 2번 두께 | Text | `` |
| P3 | 메인파트 플랜지 두께 참고 | Length | `fP(proT,"ID67C2A424-0000-D1A6-3137-343038313036")` *(formula)* |
| P4 | marketsize.dat 참조 | Length | `int(fMarketSize("SS400",P3,0))` *(formula)* |
| P5 | 2번 프로파일 구하는 공식 | Profile | `if (P2 != "") then ("PL"+int(P2)) else ("PL"+int(P4)) endif` *(formula)* |
| P6 | 1번 파트 프리픽스 | Text | `` |
| P7 | 1번 파트 스타트 넘버 | Text | `` |
| P8 | 2번 파트 프리픽스 | Text | `` |
| P9 | 2번 파트 스타트 넘버 | Text | `` |
| P10 | 1번 재질 | Material | `` |
| P11 | 2번 재질 | Material | `` |
| P12 | 1번 이름 | Text | `` |
| P13 | 2번 이름 | Text | `` |
| P14 | 1번 챔퍼 타입 | Chamfer | `` |
| P15 | 1번 챔퍼 크기 X | Length | `` |
| P16 | 1번 챔퍼 크기 Y | Length | `` |
| P17 | 1번 공식 | Profile | `"PL"+P1` *(formula)* |
| P18 | Parameter18 | Text | `10.00` |
| P19 | Parameter19 | Length | `50.0000` |
| P20 | Parameter20 | Length | `10.00` |
| P21 | Parameter21 | Length | `10.00` |
| P22 | Parameter22 | Length | `10.00` |
| P23 | Parameter23 | Length | `200.00` |
| P24 | Parameter24 | Length | `200.00` |
| P25 | Parameter25 | Length | `50.0000` |
| P26 | Parameter26 | Length | `50.00` |
| P27 | Parameter23 | Length | `90.00` |
| P28_attrfile | Parameter29 | 21 | `` |
| P28_name | Parameter28 | 13 | `` |
| P29 | 옵션 | Integer | `0` |
| P30 | 메인부재 너비참고 | Length | `fP(proB,"ID67C2A424-0000-D1A6-3137-343038313036")*0.5` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.Plane.albl_PlaneWebInnerRight1수평 | `P22` |
| D2 | D2.Plane.Bottom plane | `P18` |
| D3 | D3.Plane.Extrema top plane수평 | `` |
| D4 | D4.Plane.Web right plane | `P19` |
| D5 | D5.Plane.End plane | `P20` |
| D6 | D6.Plane.ZX plane우측 리브꺽전 | `P23` |
| D7 | D7.Plane.ZX plane | `P24` |
| D8 | D8.Plane.ZX plane우측 리브꺽후 | `P23+P25` |
| D9 | D9.Plane.ZX plane우측 리브꺽후 | `P24+P26` |
| D10 | D10.Plane.End plane | `P20` |
| D11 | D11.Plane.Extrema behind plane | `P21` |
| D12 | D12.Plane.Extrema front plane | `P21` |
| D13 | D13.Plane.XY plane | `P30+P27` |
| D14 | D14.Plane.XY plane | `P30+P27` |
| D15 | D15.Plane.XY plane | `` |
| D16 | D16.Plane.XY plane | `fP(proB,"ID67C2A424-0000-D1A6-3137-343038313036")*0.5` |
| D17 | D17.Plane.XY plane | `fP(proB,"ID67C2A424-0000-D1A6-3137-343038313036")*0.5` |
| D23 | D23.Plane.Top plane | `` |
| D24 | D24.Plane.albl_PlaneBottom2 | `` |
| D25 | D25.Plane.Central axis horizontal plane | `` |
| D26 | D26.Plane.ZX plane | `` |
| D27 | D27.Plane.Extrema behind plane | `` |
| D28 | D28.Plane.Extrema front plane | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#177 | CREATION | `if (P29==0) then 1 else 0 endif` |
| obj#177 | MATERIAL | `P10` |
| obj#177 | NAME | `P12` |
| obj#177 | PROFILE | `P17` |
| obj#177 | SERIE | `P6` |
| obj#177 | STARTNUMBER | `P7` |
| obj#190 | MATERIAL | `P11` |
| obj#190 | NAME | `P13` |
| obj#190 | PROFILE | `P5` |
| obj#190 | SERIE | `P8` |
| obj#190 | STARTNUMBER | `P9` |
| obj#200 | MATERIAL | `P11` |
| obj#200 | NAME | `P13` |
| obj#200 | PROFILE | `P5` |
| obj#200 | SERIE | `P8` |
| obj#200 | STARTNUMBER | `P9` |
| obj#209 | CREATION | `if (P29==1) then 1 else 0 endif` |
| obj#209 | MATERIAL | `P10` |
| obj#209 | NAME | `P12` |
| obj#209 | PROFILE | `P17` |
| obj#209 | SERIE | `P6` |
| obj#209 | STARTNUMBER | `P7` |
| obj#219 | CREATION | `if (P29==1) then 1 else 0 endif` |
| obj#219 | MATERIAL | `P10` |
| obj#219 | NAME | `P12` |
| obj#219 | PROFILE | `P17` |
| obj#219 | SERIE | `P6` |
| obj#219 | STARTNUMBER | `P7` |
| obj#226 | CREATION | `if (P29==0) then 1 else 0 endif` |
| obj#226 | MATERIAL | `P10` |
| obj#226 | NAME | `P12` |
| obj#226 | PROFILE | `P17` |
| obj#226 | SERIE | `P6` |
| obj#226 | STARTNUMBER | `P7` |
| obj#235 | CREATION | `if (P29==0) then 1 else 0 endif` |
| obj#235 | MATERIAL | `P10` |
| obj#235 | NAME | `P12` |
| obj#235 | PROFILE | `P17` |
| obj#235 | SERIE | `P6` |
| obj#235 | STARTNUMBER | `P7` |
| obj#243 | CREATION | `if (P29==0) then 1 else 0 endif` |
| obj#243 | MATERIAL | `P10` |
| obj#243 | NAME | `P12` |
| obj#243 | PROFILE | `P17` |
| obj#243 | SERIE | `P6` |
| obj#243 | STARTNUMBER | `P7` |
| obj#250 | ATTRIBUTE_FILE | `P28_attrfile` |
| obj#250 | NAME | `P28_name` |

## Preset `standard.p_NEC_POST`
```
joint_attributes.saveas_file "1"
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
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 -0.000000
joint_attributes.zsuunta 7
joint_attributes.P22 0.000000
joint_attributes.P21 0.000000
joint_attributes.P20 0.000000
joint_attributes.P19 50.000000
joint_attributes.P18 0.000000
joint_attributes.P16 30.000000
joint_attributes.P15 30.000000
joint_attributes.P14 3
joint_attributes.P13 "PLATE"
joint_attributes.P12 "PLATE"
joint_attributes.P11 "SS400"
joint_attributes.P10 "SS400"
joint_attributes.P9 "1"
joint_attributes.P8 "PL"
joint_attributes.P7 "1"
joint_attributes.P6 "PL"
joint_attributes.P1 12.000000
joint_attributes.P29 0
joint_attributes.P28_name "71"
joint_attributes.P26 50.000000
joint_attributes.P25 50.000000
joint_attributes.P24 200.000000
joint_attributes.P23 200.000000
joint_attributes.P27 100.000000
```

## Preset `standard.p_NEC_POST`
```
joint_attributes.saveas_file "1"
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
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 -0.000000
joint_attributes.zsuunta 7
joint_attributes.P22 0.000000
joint_attributes.P21 0.000000
joint_attributes.P20 0.000000
joint_attributes.P19 50.000000
joint_attributes.P18 0.000000
joint_attributes.P16 30.000000
joint_attributes.P15 30.000000
joint_attributes.P14 3
joint_attributes.P13 "PLATE"
joint_attributes.P12 "PLATE"
joint_attributes.P11 "SS400"
joint_attributes.P10 "SS400"
joint_attributes.P9 "1"
joint_attributes.P8 "PL"
joint_attributes.P7 "1"
joint_attributes.P6 "PL"
joint_attributes.P1 12.000000
joint_attributes.P29 0
joint_attributes.P28_name "71"
joint_attributes.P26 50.000000
joint_attributes.P25 50.000000
joint_attributes.P24 200.000000
joint_attributes.P23 200.000000
joint_attributes.P27 100.000000
```