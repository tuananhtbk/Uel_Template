# NEC_PIPE_COLUMN2

- File: `NEC\NEC_PIPE_COL2.uel`  (57,614 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	NEC_PIPE_COLUMN2	All`
- Dialog parameters: 7 | Variables (P/custom): 23 | Distance vars (D): 27 | Parts with bound profile: 3
- Lookup files (fVF): -
- Functions used: fP, mid
- Dialog bitmaps: 6 | Saved presets: standard.p_NEC_PIPE_COLUMN2, standard.p_NEC_PIPE_COLUMN2

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "NEC_PIPE_COLUMN2")
   {
       tab_page("", " Pictures ", 1)
       {
           
           picture("NEC_LOGO",8,2,420,420)
           picture("NEC_PIPE_COLUMN4",100,50,150,125)

           parameter("", "P2", distance, number, 460,161,60)
           //parameter("덮개 너비", "P2", distance, number, 290,105,60)

       }

       tab_page("", " Plates ", 2)
       {
           
           picture("NEC_PIPE_COLUMN3",100,50,150,150)
           attribute("", "Plate", label,"%s",  none, none, "0.0", "0.0",20,50)
           attribute("t", "t", label,"%s",  none, none, "0.0", "0.0",150,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",270,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",440,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",590,25)
           attribute("Class", "Class", label,"%s",  none, none, "0.0", "0.0",750,25)
           attribute("", "", label,"%s",  none, none, "0.0", "0.0",0,410)

           parameter("", "P21", string, text, 130,50,50)
           parameter(" ", "P23", string, text, 590,50,120)
           parameter("", "P24", material, text, 440,50,90)
           parameter("", "P25", integer, number, 750,50,50)
           parameter("", "P26", string, text, 270,50,50)
           parameter("", "P27", integer, number, 350,50,40)

           //parameter("하단부 보강재 두께", "P21", string, text, 130,75,50)
           //parameter("하단부 보강재 이름 ", "P23", string, text, 590,50,120)
           //parameter("하단부 보강재 재질", "P24", material, text, 440,50,90)
           //parameter("하단부 보강재 클래스", "P25", integer, number, 750,50,50)
           //parameter("하단부 보강재 프리픽스", "P26", string, text, 270,50,50)
           //parameter("하단부 보강재 스타트 넘버", "P27", integer, number, 350,50,40)


           parameter("", "P7", distance, number, 280,150,60)


           parameter("", "P28", chamfer_type, number, 450,350,90)
           parameter("", "P29", distance, number, 450,375,90)

           attribute("P20", "", option, "%s", no, none, "0.0", "0.0", 340,257,65)
                {
                     value("NEC_PIPE_COLUMN5.xbm",1)
                     value("NEC_PIPE_COLUMN6.xbm",0)
                }

           //parameter("상부 플레이트 두께", "P7", distance, number, 12)
           //parameter("보강 플레이트 너비", "P5", distance, number, 13)
           //parameter("보강 플레이트 높이", "P6", distance, number, 14)
           //parameter("하단부 보강재 On/Off 옵션", "P20", integer, number, 15)
           //parameter("하단부 보강재 챔퍼 타입", "P28", chamfer_type, number, 16)
           //parameter("하단부 보강재 챔퍼 크기", "P29", distance, number, 17)

       }
       tab_page("", " 설명서 ", 3)
       {
           
           picture("NEC_PIPE_COLUMN_DIALOG4",8,2,15,10)

       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Length | `P19*0.5+P2` *(formula)* |
| P2 | Parameter2 | Length | `80.00` |
| P3 | Parameter3 | Length | `D10-(P19*0.5)-P2` *(formula)* |
| P4 | 덮개 너비 | Length | `600.00` |
| P5 | 보강 플레이트 너비 | Length | `200.00` |
| P6 | 보강 플레이트 높이 | Length | `200.00` |
| P7 | 상부 플레이트 두께 | Length | `17.00` |
| P9 | Parameter9 | Profile | `fP(proPROFILE,"ID67C2A424-0000-D0C2-3137-343038313036")` *(formula)* |
| P11 | 메인부재 두께 참조 | Length | `fP(proB,"ID67C2A424-0000-D0C2-3137-343038313036")*0.5` *(formula)* |
| P14 | 빔 컷 옵션2 | Length | `300.00` |
| P16 | Parameter16 | Length | `mid(P9,2,3)` *(formula)* |
| P17 | Parameter17 | Length | `280.0` |
| P18 | Parameter18 | Length | `(P16*0.5)-30` *(formula)* |
| P19 | Parameter19 | Length | `fP(proH,"ID67C2A424-0000-D0C2-3137-343038313036")` *(formula)* |
| P20 | 하단부 보강재 On/Off 옵션 | Integer | `0` |
| P21 | 하단부 보강재 두께 | Text | `` |
| P22 | 하단부 보강재 두께 공식 | Profile | `if P21=="" then "PL"+fP(proS,"ID67C2A424-0000-D0C6-3137-343038313036") else "PL"+P21 endif` *(formula)* |
| P24 | 하단부 보강재 재질 | Material | `SS400` |
| P25 | 하단부 보강재 클래스 | Integer | `3` |
| P26 | 하단부 보강재 프리픽스 | Text | `PL` |
| P27 | 하단부 보강재 스타트 넘버 | Integer | `1` |
| P28 | 하단부 보강재 챔퍼 타입 | Chamfer | `3` |
| P29 | 하단부 보강재 챔퍼 크기 | Length | `30.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.COLUMN.COLUMN | `` |
| D2 | D2.COLUMN.COLUMN | `` |
| D3 | D3.PLATE.BEAM | `` |
| D4 | D4.PLATE.BEAM | `` |
| D5 | D5.PLATE.BEAM | `` |
| D6 | D6.PLATE.BEAM | `` |
| D7 | D7.PLATE.BEAM | `D10-(P19*0.5)` |
| D8 | D8.PLATE.BEAM | `D10-(P19*0.5)` |
| D9 | D9.PLATE.BEAM | `D10-(P19*0.5)-P2` |
| D10 | D10.BEAM.BEAM | `` |
| D11 | D11.PLATE.BEAM | `` |
| D12 | D12.PLATE.BEAM | `` |
| D13 | D13.PLATE.COLUMN | `` |
| D14 | D14.PLATE.COLUMN | `` |
| D15 | D15.COLUMN.TEST1 | `` |
| D16 | D16.CUTPART.TEST1 | `` |
| D17 | D17.CUTPART.TEST1 | `` |
| D18 | D18.PLATE.BEAM | `D10-(P19*0.5)-P2` |
| D22 | D22.CUTPART.NEC_PIPE_COLUMN | `P1` |
| D23 | D23.CUTPART.NEC_PIPE_COLUMN | `P1` |
| D24 | D24.CUTPART.NEC_PIPE_COLUMN | `P1` |
| D25 | D25.CUTPART.NEC_PIPE_COLUMN | `P1` |
| D32 | D32.COLUMN.NEC_PIPE_COLUMN | `` |
| D38 | D38.CUTPART.BEAM | `` |
| D39 | D39.CUTPART.BEAM | `` |
| D40 | D40.CUTPART.BEAM | `` |
| D41 | D41.CUTPART.BEAM | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#141 | CLASS | `P25` |
| obj#141 | CREATION | `if P20==0 then 1 else 0 endif` |
| obj#141 | MATERIAL | `P24` |
| obj#141 | NAME | `P23` |
| obj#141 | PROFILE | `P22` |
| obj#141 | SERIE | `P26` |
| obj#141 | STARTNUMBER | `P27` |
| obj#154 | PROFILE | `P9` |
| obj#164 | PROFILE | `"BL"+P7` |

## Preset `standard.p_NEC_PIPE_COLUMN2`
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
joint_attributes.P20 0
joint_attributes.P29 30.000000
joint_attributes.P28 3
joint_attributes.P7 10.000000
joint_attributes.P27 1
joint_attributes.P26 "PL"
joint_attributes.P25 1
joint_attributes.P24 "SS400"
joint_attributes.P23 "PLATE"
joint_attributes.P21 ""
joint_attributes.P2 80.000000
```

## Preset `standard.p_NEC_PIPE_COLUMN2`
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
joint_attributes.P20 0
joint_attributes.P29 30.000000
joint_attributes.P28 3
joint_attributes.P7 10.000000
joint_attributes.P27 1
joint_attributes.P26 "PL"
joint_attributes.P25 1
joint_attributes.P24 "SS400"
joint_attributes.P23 "PLATE"
joint_attributes.P21 ""
joint_attributes.P2 80.000000
```