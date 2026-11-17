# NEC_PIPE_COLUMN1

- File: `NEC\NEC_PIPE_COL1.uel`  (60,626 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	NEC_PIPE_COLUMN1	All`
- Dialog parameters: 4 | Variables (P/custom): 23 | Distance vars (D): 46 | Parts with bound profile: 5
- Lookup files (fVF): -
- Functions used: fP, mid
- Dialog bitmaps: 4 | Saved presets: standard.p_NEC_PIPE_COLUMN1, standard.p_NEC_PIPE_COLUMN1

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "NEC_PIPE_COLUMN1")
   {
       tab_page("", " Pictures ", 1)
       {
           picture("NEC_LOGO",8,2,420,420)
           picture("NEC_PIPE_COLUMN1",100,50,150,125)

           parameter("", "P25", distance, number, 460,161,60)
           //parameter("덮개 너비", "P4", distance, number, 1)

           parameter("모든부재 On/Off", "P23", YesNo, number, 1)

       }

       tab_page("", " Plates ", 2)
       {

           picture("NEC_PIPE_COLUMN2",100,50,150,200)
           attribute("", "1.Pipe", label,"%s",  none, none, "0.0", "0.0",20,50)
           attribute("", "2.Plate", label,"%s",  none, none, "0.0", "0.0",20,75)
           attribute("", "3.Plate", label,"%s",  none, none, "0.0", "0.0",20,100)
           attribute("t", "t", label,"%s",  none, none, "0.0", "0.0",150,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",270,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",440,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",590,25)
           attribute("Class", "Class", label,"%s",  none, none, "0.0", "0.0",750,25)
           attribute("", "", label,"%s",  none, none, "0.0", "0.0",0,410)



           parameter("", "P7", distance, number, 130,75,50 )
           parameter("", "P8", distance, number, 130,100,50)
           parameter("", "P1", string, text, 590,50,120)
           parameter("", "P2", material, text, 440,50,90)
           parameter("", "P3", integer, number, 750,50,50)
           parameter("", "P5", string, text, 270,50,50)
           parameter("", "P6", integer, number, 350,50,40)
           parameter("", "P12", string, text, 590,75,120)
           parameter("", "P13", material, text, 440,75,90)
           parameter("", "P14", integer, number, 750,75,50)
           parameter("", "P21", integer, number, 750,100,50)
           parameter("", "P15", string, text, 270,75,50)
           parameter("", "P20", integer, number, 350,75,40)


           //parameter("상부 플레이트 두께", "P7", distance, number, 180,75,50 )
           //parameter("하부 플레이트 두께", "P8", distance, number, 180,100,50)
           //parameter("1번 부재 이름", "P1", string, text, 670,50,120)
           //parameter("1번 부재 재질", "P2", material, text, 520,50,90)
           //parameter("1번 부재 클래스", "P3", integer, number, 7)
           //parameter("1번 부재 프리픽스", "P5", string, text, 320,50,50)
           //parameter("1번 부재 스타트 넘버", "P6", integer, number, 400,50,40)
           //parameter("2번 부재 이름", "P12", string, text, 670,75,120)
           //parameter("2번 부재 재질", "P13", material, text, 520,75,90)
           //parameter("2번 부재 클래스", "P14", integer, number, 8)
           //parameter("2번 부재 프리픽스", "P15", string, text, 320,75,50)
           //parameter("2번 부재 스타트 넘버", "P20", integer, number, 400,75,40)
       }
       tab_page("", " 설명서 ", 3)
       {
           picture("NEC_PIPE_COLUMN_DIALOG3",8,2,15,10)

       }

   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P2 | 1번 부재 재질 | Material | `SS400` |
| P3 | 1번 부재 클래스 | Integer | `3` |
| P4 | 덮개 너비 | Length | `600.00` |
| P5 | 1번 부재 프리픽스 | Text | `P` |
| P6 | 1번 부재 스타트 넘버 | Integer | `1` |
| P7 | 상부 플레이트 두께 | Length | `16.00` |
| P8 | 하부 플레이트 두께 | Length | `16.00` |
| P9 | Parameter9 | Profile | `fP(proPROFILE,"ID67C2A424-0000-CF64-3137-343038313036")` *(formula)* |
| P10 | 3번 부재 클래스 | Integer | `3` |
| P11 | 메인부재 두께 참조 | Length | `fP(proB,"ID67C2A424-0000-CF64-3137-343038313036")*0.5` *(formula)* |
| P13 | 2번 부재 재질 | Material | `SS400` |
| P14 | 2번 부재 클래스 | Integer | `1` |
| P15 | 2번 부재 프리픽스 | Text | `PL` |
| P16 | Parameter16 | Length | `mid(P9,2,3)` *(formula)* |
| P17 | Parameter17 | Length | `280.0` |
| P18 | Parameter18 | Length | `(P16*0.5)-30` *(formula)* |
| P19 | Parameter19 | Length | `fP(proH,"ID67C2A424-0000-CF64-3137-343038313036")` *(formula)* |
| P20 | 2번 부재 스타트 넘버 | Integer | `1` |
| P21 | 3번 클래스 | Integer | `3` |
| P22 | Parameter22 | Integer | `0` |
| P23 | Parameter23 | Yes/No | `0` |
| P24 | Parameter24 | Length | `(P19*0.5)+P25` *(formula)* |
| P25 | Parameter25 | Length | `80.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.FITTING.TEST | `P19*0.5` |
| D2 | D2.FITTING.TEST | `P19*0.5` |
| D3 | D3.CUTPART.PLATE | `` |
| D4 | D4.COLUMN.PLATE | `` |
| D5 | D5.COLUMN.PLATE | `` |
| D6 | D6.PLATE.BEAM | `` |
| D7 | D7.PLATE.BEAM | `` |
| D8 | D8.CUTPART.PLATE | `` |
| D9 | D9.PLATE.BEAM | `` |
| D10 | D10.PLATE.BEAM | `` |
| D11 | D11.PLATE.BEAM | `` |
| D12 | D12.PLATE.BEAM | `` |
| D13 | D13.PLATE.BEAM | `` |
| D14 | D14.PLATE.NEC_PIPE_COLUMN | `P24` |
| D15 | D15.PLATE.NEC_PIPE_COLUMN | `P24` |
| D16 | D16.PLATE.NEC_PIPE_COLUMN | `P24` |
| D17 | D17.PLATE.NEC_PIPE_COLUMN | `P24` |
| D18 | D18.PLATE.NEC_PIPE_COLUMN | `P24` |
| D19 | D19.PLATE.NEC_PIPE_COLUMN | `P24` |
| D20 | D20.PLATE.NEC_PIPE_COLUMN | `P24` |
| D21 | D21.PLATE.NEC_PIPE_COLUMN | `P24` |
| D22 | D22.CUTPART.NEC_PIPE_COLUMN | `P24` |
| D23 | D23.CUTPART.NEC_PIPE_COLUMN | `P24` |
| D24 | D24.CUTPART.NEC_PIPE_COLUMN | `P24` |
| D25 | D25.CUTPART.NEC_PIPE_COLUMN | `P24` |
| D26 | D26.CUTPART.NEC_PIPE_COLUMN | `P24` |
| D27 | D27.CUTPART.NEC_PIPE_COLUMN | `P24` |
| D28 | D28.CUTPART.NEC_PIPE_COLUMN | `P24` |
| D29 | D29.CUTPART.NEC_PIPE_COLUMN | `P24` |
| D30 | D30.CUTPART.PLATE | `` |
| D31 | D31.CUTPART.PLATE | `` |
| D32 | D32.FITTING.TEST | `P19*0.5` |
| D33 | D33.FITTING.BEAM | `` |
| D34 | D34.FITTING.BEAM | `` |
| D35 | D35.FITTING.BEAM | `` |
| D36 | D36.CUTPART.NEC_PIPE_COLUMN | `` |
| D37 | D37.CUTPART.NEC_PIPE_COLUMN | `` |
| D38 | D38.CUTPART.BEAM | `` |
| D39 | D39.CUTPART.BEAM | `` |
| D40 | D40.CUTPART.BEAM | `` |
| D41 | D41.CUTPART.BEAM | `` |
| D42 | D42.COLUMN.NEC_PIPE_COLUMN | `` |
| D43 | D43.COLUMN.NEC_PIPE_COLUMN | `` |
| D44 | D44.0.000.TEST | `` |
| D45 | D45.0.000.TEST | `` |
| D57 | D57.PLATE.BEAM | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#139 | CLASS | `P14` |
| obj#139 | CREATION | `P23` |
| obj#139 | MATERIAL | `P13` |
| obj#139 | NAME | `P12` |
| obj#139 | PROFILE | `"PL"+P7` |
| obj#139 | SERIE | `P15` |
| obj#139 | STARTNUMBER | `P20` |
| obj#152 | CLASS | `P3` |
| obj#152 | CREATION | `P23` |
| obj#152 | MATERIAL | `P2` |
| obj#152 | NAME | `P1` |
| obj#152 | PROFILE | `P9` |
| obj#152 | SERIE | `P5` |
| obj#152 | STARTNUMBER | `P6` |
| obj#162 | CLASS | `P21` |
| obj#162 | CREATION | `P23` |
| obj#162 | MATERIAL | `P13` |
| obj#162 | NAME | `P12` |
| obj#162 | PROFILE | `"PL"+P8` |
| obj#162 | SERIE | `P15` |
| obj#162 | STARTNUMBER | `P20` |
| obj#177 | PROFILE | `"BL"+P7` |
| obj#191 | PROFILE | `"BL"+P8` |

## Preset `standard.p_NEC_PIPE_COLUMN1`
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
joint_attributes.P20 1
joint_attributes.P15 "PL"
joint_attributes.P21 1
joint_attributes.P14 1
joint_attributes.P13 "SS400"
joint_attributes.P12 "PLATE"
joint_attributes.P6 1
joint_attributes.P5 "P"
joint_attributes.P3 3
joint_attributes.P2 "SS400"
joint_attributes.P1 ""
joint_attributes.P8 20.000000
joint_attributes.P7 20.000000
joint_attributes.P23 1
joint_attributes.P25 80.000000
```

## Preset `standard.p_NEC_PIPE_COLUMN1`
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
joint_attributes.P20 1
joint_attributes.P15 "PL"
joint_attributes.P21 1
joint_attributes.P14 1
joint_attributes.P13 "SS400"
joint_attributes.P12 "PLATE"
joint_attributes.P6 1
joint_attributes.P5 "P"
joint_attributes.P3 3
joint_attributes.P2 "SS400"
joint_attributes.P1 ""
joint_attributes.P8 20.000000
joint_attributes.P7 20.000000
joint_attributes.P23 1
joint_attributes.P25 80.000000
```