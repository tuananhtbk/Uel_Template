# NEC_GIRDER

- File: `NEC\NEC_GIRDER.uel`  (29,571 bytes, gzip)
- Type in .inp: **macro**  | Catalog line: `-10	NEC_GIRDER	All`
- Dialog parameters: 5 | Variables (P/custom): 22 | Distance vars (D): 6 | Parts with bound profile: 3
- Lookup files (fVF): -
- Functions used: -
- Dialog bitmaps: 2 | Saved presets: standard.p_NEC_GIRDER, standard.p_NEC_GIRDER

## Dialog (.inp)
```
page("TeklaStructures","")
{
   macro(1, "NEC_GIRDER")
   {
       tab_page("", " Pictures ", 1)
       {
           picture("NEC_LOGO",8,2,580,480)
           picture("NEC_GIRDER1",100,50,170,230)           

           parameter("", "P1", distance, number, 230,200,80)
           parameter("", "P2", distance, number, 685,200,80)
           parameter("", "P3", YesNo, number, 440,400,80)
           parameter("", "P4_name", ComponentName, text, 440,425,120)
           parameter("", "P4_attrfile", ComponentAttributeFile, text, 440,450,120)

           //parameter("좌측 브라켓 길이", "P1", distance, number, 1)
           //parameter("우측 브라켓 길이", "P2", distance, number, 2)
           //parameter("컴퍼넌트 생성 유무", "P3", YesNo, number, 3)
           //parameter("Component Name", "P4_name", ComponentName, text, 4)
           //parameter("Component Attribute", "P4_attrfile", ComponentAttributeFile, text, 5)

           attribute("", "Main", label,"%s",  none, none, "0.0", "0.0",20,50)
           attribute("", "Secondary", label,"%s",  none, none, "0.0", "0.0",20,100)
           attribute("Profile", "Profile", label,"%s",  none, none, "0.0", "0.0",125,25)
           attribute("Pos_No", "Pos_No", label,"%s",  none, none, "0.0", "0.0",320,25)
           attribute("Material", "Material", label,"%s",  none, none, "0.0", "0.0",490,25)
           attribute("Name", "Name", label,"%s",  none, none, "0.0", "0.0",640,25)
           attribute("Class", "Class", label,"%s",  none, none, "0.0", "0.0",800,25)
           attribute("Finish", "Finish", label,"%s",  none, none, "0.0", "0.0",880,25)
           attribute("", "Component On/Off", label,"%s",  none, none, "0.0", "0.0",200,400)
           attribute("", "Component Name", label,"%s",  none, none, "0.0", "0.0",200,425)
           attribute("", "Component Attribute", label,"%s",  none, none, "0.0", "0.0",200,450)
           attribute("", "", label,"%s",  none, none, "0.0", "0.0",0,500)
           parameter("", "P5", string, text, 640,50,120)
           parameter("", "P6", profile, text, 120,50,150)
           parameter("", "P7", material, text, 490,50,90)
           parameter("", "P8", string, text, 880,50,60)
           parameter("", "P9", integer, number, 800,50,50)
           parameter("", "P10", string, text, 320,50,50)
           parameter("", "P11", integer, number, 400,50,40)
           parameter("", "P12", string, text, 320,75,50)
           parameter("", "P13", integer, number, 400,75,40)
           parameter("", "P14", string, text, 640,100,120)
           parameter("", "P15", material, text, 490,100,90)
           parameter("", "P16", integer, number, 800,100,50)
           parameter("", "P17", string, text, 320,100,50)
           parameter("", "P18", integer, number, 400,100,40)
           parameter("", "P20", string, text, 320,125,50)
           parameter("", "P21", integer, number, 400,125,40)

       }

       tab_page("", " 설명서 ", 2)
       {
           //parameter("브라켓 어셈블리 스타트 넘버", "P21", integer, number, 400,125,40)
           //parameter("메인 부재 이름", "P5", string, text, 670,50,120)

           //parameter("메인 부재 재질", "P7", material, text, 520,50,90)
           //parameter("메인 부재 피니쉬", "P8", string, text, 770,50,60)
           //parameter("메인 부재 클래스", "P9", integer, number, 850,50,50)
           //parameter("메인 부재 파트 프리픽스", "P10", string, text, 320,50,50)
           //parameter("메인 부재 파트 스타트 넘버", "P11", integer, number, 400,50,40)
           //parameter("메인 부재 어셈블리 프리픽스", "P12", string, text, 320,75,50)
           //parameter("메인 부재 어셈블리 스타트 넘버", "P13", integer, number, 400,75,40)
           //parameter("브라켓 이름", "P14", string, text, 670,100,120)
           //parameter("브라켓 재질", "P15", material, text, 520,100,90)
           //parameter("브라켓 클래스", "P16", integer, number, 850,100,50)
           //parameter("브라켓 파트 프리픽스", "P17", string, text, 320,100,50)
           //parameter("브라켓 파트 스타트넘버", "P18", integer, number, 400,100,40)
           //parameter("브라켓 어셈블리 프리픽스", "P20", string, text, 320,125,50)
           //parameter("브라켓 어셈블리 스타트 넘버", "P21", integer, number, 400,125,40)


 
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | 좌측 브라켓 길이 | Length | `1000.00` |
| P2 | 우측 브라켓 길이 | Length | `1000.00` |
| P3 | 컴퍼넌트 생성 유무 | Yes/No | `0` |
| P4_attrfile | Component Attribute | 21 | `` |
| P4_name | Component Name | 13 | `77` |
| P5 | 메인 부재 이름 | Text | `GIRDER` |
| P6 | 메인 부재 프로파일 | Profile | `H400*200*8*13` |
| P7 | 메인 부재 재질 | Material | `SS400` |
| P8 | 메인 부재 피니쉬 | Text | `` |
| P9 | 메인 부재 클래스 | Integer | `1` |
| P10 | 메인 부재 파트 프리픽스 | Text | `M` |
| P11 | 메인 부재 파트 스타트 넘버 | Integer | `1` |
| P12 | 메인 부재 어셈블리 프리픽스 | Text | `G` |
| P13 | 메인 부재 어셈블리 스타트 넘버 | Integer | `1` |
| P15 | 브라켓 재질 | Material | `SS400` |
| P16 | 브라켓 클래스 | Integer | `4` |
| P17 | 브라켓 파트 프리픽스 | Text | `BK` |
| P18 | 브라켓 파트 스타트넘버 | Integer | `1` |
| P19 | Parameter19 | Length | `0.00` |
| P20 | 브라켓 어셈블리 프리픽스 | Text | `` |
| P21 | 브라켓 어셈블리 스타트 넘버 | Integer | `1` |
| P22 | Parameter22 | Profile | `P6` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.GIRDER.NEC_GIRDER | `P1` |
| D2 | D2.GIRDER.NEC_GIRDER | `P2` |
| D3 | D3.BRACKET.NEC_GIRDER | `` |
| D4 | D4.BRACKET.NEC_GIRDER | `P1` |
| D5 | D5.BRACKET.NEC_GIRDER | `` |
| D6 | D6.BRACKET.NEC_GIRDER | `P2` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#91 | ASSEMBLYSERIE | `P12` |
| obj#91 | ASSEMBLYSTARTNUMBER | `P13` |
| obj#91 | CLASS | `P9` |
| obj#91 | MATERIAL | `P7` |
| obj#91 | NAME | `P5` |
| obj#91 | PROFILE | `P6` |
| obj#91 | SERIE | `P10` |
| obj#91 | STARTNUMBER | `P11` |
| obj#111 | ASSEMBLYSERIE | `P20` |
| obj#111 | ASSEMBLYSTARTNUMBER | `P21` |
| obj#111 | CLASS | `P16` |
| obj#111 | MATERIAL | `P15` |
| obj#111 | NAME | `P14` |
| obj#111 | PROFILE | `P6` |
| obj#111 | SERIE | `P17` |
| obj#111 | STARTNUMBER | `P18` |
| obj#127 | ASSEMBLYSERIE | `P20` |
| obj#127 | ASSEMBLYSTARTNUMBER | `21` |
| obj#127 | MATERIAL | `P15` |
| obj#127 | NAME | `P14` |
| obj#127 | PROFILE | `P6` |
| obj#127 | SERIE | `17` |
| obj#127 | STARTNUMBER | `P18` |
| obj#143 | ATTRIBUTE_FILE | `P4_attrfile` |
| obj#143 | CREATION | `if P3==0 then 0 else 1 endif` |
| obj#143 | NAME | `P4_name` |
| obj#499 | ATTRIBUTE_FILE | `P4_attrfile` |
| obj#499 | CREATION | `if P3==0 then 0 else 1 endif` |
| obj#499 | NAME | `P4_name` |

## Preset `standard.p_NEC_GIRDER`
```
joint_attributes.saveas_file "standard"
joint_attributes.get_menu "< ExternalDesign >"
joint_attributes.name_enable 0
joint_attributes.ThirdHandle 0
joint_attributes.AtDepthValue 0.000000
joint_attributes.AtDepth 0
joint_attributes.RotationValue -0.000000
joint_attributes.Rotation 1
joint_attributes.OnPlaneValue 0.000000
joint_attributes.OnPlane 0
joint_attributes.P21 1
joint_attributes.P20 "BK"
joint_attributes.P18 1
joint_attributes.P17 "BK"
joint_attributes.P16 4
joint_attributes.P15 "SS400"
joint_attributes.P14 "BRACKET"
joint_attributes.P13 1
joint_attributes.P12 "G"
joint_attributes.P11 1
joint_attributes.P10 "M"
joint_attributes.P9 1
joint_attributes.P8 "SG1"
joint_attributes.P7 "SS400"
joint_attributes.P6 "H400*200*8*13"
joint_attributes.P5 "GIRDER"
joint_attributes.P4_attrfile "standard"
joint_attributes.P4_name "77"
joint_attributes.P3 1
joint_attributes.P2 1000.000000
joint_attributes.P1 1000.000000
```

## Preset `standard.p_NEC_GIRDER`
```
joint_attributes.saveas_file "standard"
joint_attributes.get_menu "< ExternalDesign >"
joint_attributes.name_enable 0
joint_attributes.ThirdHandle 0
joint_attributes.AtDepthValue 0.000000
joint_attributes.AtDepth 0
joint_attributes.RotationValue -0.000000
joint_attributes.Rotation 1
joint_attributes.OnPlaneValue 0.000000
joint_attributes.OnPlane 0
joint_attributes.P21 1
joint_attributes.P20 "BK"
joint_attributes.P18 1
joint_attributes.P17 "BK"
joint_attributes.P16 4
joint_attributes.P15 "SS400"
joint_attributes.P14 "BRACKET"
joint_attributes.P13 1
joint_attributes.P12 "G"
joint_attributes.P11 1
joint_attributes.P10 "M"
joint_attributes.P9 1
joint_attributes.P8 "SG1"
joint_attributes.P7 "SS400"
joint_attributes.P6 "H400*200*8*13"
joint_attributes.P5 "GIRDER"
joint_attributes.P4_attrfile "standard"
joint_attributes.P4_name "77"
joint_attributes.P3 1
joint_attributes.P2 1000.000000
joint_attributes.P1 1000.000000
```