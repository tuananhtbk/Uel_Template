# BEAM-DOUBLE-H

- File: `1066666666\BEAM DOUBLE SHEAR.uel`  (15,390 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	BEAM-DOUBLE-H	All`
- Dialog parameters: 2 | Variables (P/custom): 20 | Distance vars (D): 63 | Parts with bound profile: 4
- Lookup files (fVF): BEAM-2.txt
- Functions used: fP, fVF, if, mid
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "BEAM-DOUBLE-H")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("SF (YES/NO)", "P13", YesNo, number, 1)
           parameter("Parameter20", "P20", string, text, 2)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Text | `fVF("BEAM-2.txt",fP(proPROFILE,"ID66A6E5E3-0000-0D47-3137-323232313934"),1)` *(formula)* |
| P2 | Parameter2 | Integer | `fVF("BEAM-2.txt",fP(proPROFILE,"ID66A6E5E3-0000-0D47-3137-323232313934"),2)` *(formula)* |
| P3 | Parameter3 | Integer | `fVF("BEAM-2.txt",fP(proPROFILE,"ID66A6E5E3-0000-0D47-3137-323232313934"),3)` *(formula)* |
| P4 | Parameter4 | Text | `fVF("BEAM-2.txt",fP(proPROFILE,"ID66A6E5E3-0000-0D47-3137-323232313934"),4)` *(formula)* |
| P5 | Parameter5 | Length | `fVF("BEAM-2.txt",fP(proPROFILE,"ID66A6E5E3-0000-0D47-3137-323232313934"),5)` *(formula)* |
| P6 | Parameter6 | Length | `fVF("BEAM-2.txt",fP(proPROFILE,"ID66A6E5E3-0000-0D47-3137-323232313934"),6)` *(formula)* |
| P7 | Parameter7 | Length | `fVF("BEAM-2.txt",fP(proPROFILE,"ID66A6E5E3-0000-0D47-3137-323232313934"),7)` *(formula)* |
| P8 | Parameter8 | Length | `fVF("BEAM-2.txt",fP(proPROFILE,"ID66A6E5E3-0000-0D47-3137-323232313934"),8)` *(formula)* |
| P9 | Parameter9 | Integer | `fVF("BEAM-2.txt",fP(proPROFILE,"ID66A6E5E3-0000-0D47-3137-323232313934"),9)` *(formula)* |
| P10 | Parameter10 | Length | `(fP(proH,"ID66A6E5E3-0000-0D47-3137-323232313934")-P7)/2` *(formula)* |
| P11 | Parameter11 | Text | `fP(proMATERIAL,"ID66A6E5E3-0000-0D43-3137-323232313934")` *(formula)* |
| P12 | Parameter12 | Text | `fP(proMATERIAL,"ID66A6E5E3-0000-0D47-3137-323232313934")` *(formula)* |
| P13 | SF (YES/NO) | Yes/No | `0` |
| P14 | Parameter14 | Text | `mid(fP(proSERIE,"ID66A6E5E3-0000-0D47-3137-323232313934"),0,find(fP(proSERIE,"ID66A6E5E3-0000-0D47-3137-323232313934"), "-")+1 )` *(formula)* |
| P15 | Parameter15 | Text | `mid(fP(proSERIE,"ID66A6E5E3-0000-0D43-3137-323232313934"),0,find(fP(proSERIE,"ID66A6E5E3-0000-0D43-3137-323232313934"), "-")+1 )` *(formula)* |
| P16 | Parameter16 | Length | `fP(proB,"ID66A6E5E3-0000-0D43-3137-323232313934")` *(formula)* |
| P17 | Parameter17 | Length | `if(P16<=150) then 15 else if(P16>150 && P9<=40) then 35 else if(P16>150 && P9<=30) then 40 else if(P16>150 && P9<=40) then 45 else if(P16>150 && P9<=50) then 50 else P9+10 endif endif endif endif endif` *(formula)* |
| P18 | Parameter18 | Integer | `if(P16<=150) then 1 else 3 endif` *(formula)* |
| P19 | Parameter19 | Text | `mid(fP(proSERIE,"ID66A6E5E3-0000-0D43-3137-323232313934"),0,6)` *(formula)* |
| P20 | Parameter20 | Text | `mid(fP(proSERIE,"ID66A6E5E3-0000-0D47-3137-323232313934"),0,6)` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.STIFFENER PLATE.BEAM | `` |
| D2 | D2.STIFFENER PLATE.BEAM | `` |
| D3 | D3.STIFFENER PLATE.BEAM | `` |
| D4 | D4.STIFFENER PLATE.BEAM | `` |
| D5 | D5.STIFFENER PLATE.GIRDER | `` |
| D6 | D6.STIFFENER PLATE.GIRDER | `` |
| D7 | D7.STIFFENER PLATE.GIRDER | `` |
| D8 | D8.STIFFENER PLATE.GIRDER | `` |
| D9 | D9.STIFFENER PLATE.GIRDER | `` |
| D10 | D10.STIFFENER PLATE.GIRDER | `` |
| D11 | D11.STIFFENER PLATE.GIRDER | `` |
| D12 | D12.STIFFENER PLATE.GIRDER | `` |
| D13 | D13.GUSSET PLATE.GIRDER | `` |
| D14 | D14.GUSSET PLATE.GIRDER | `` |
| D15 | D15.GUSSET PLATE.GIRDER | `` |
| D16 | D16.GUSSET PLATE.GIRDER | `` |
| D17 | D17.GUSSET PLATE.GIRDER | `` |
| D18 | D18.GUSSET PLATE.GIRDER | `` |
| D19 | D19.GUSSET PLATE.GIRDER | `` |
| D20 | D20.GUSSET PLATE.GIRDER | `` |
| D21 | D21.GUSSET PLATE.BEAM | `` |
| D22 | D22.GUSSET PLATE.BEAM | `` |
| D23 | D23.GUSSET PLATE.BEAM | `` |
| D24 | D24.GUSSET PLATE.BEAM | `` |
| D25 | D25.GUSSET PLATE.GUSSET PLATE | `` |
| D26 | D26.GUSSET PLATE.GUSSET PLATE | `` |
| D27 | D27.GUSSET PLATE.GUSSET PLATE | `` |
| D28 | D28.GUSSET PLATE.GUSSET PLATE | `` |
| D29 | D29.GUSSET PLATE.GUSSET PLATE | `` |
| D30 | D30.GUSSET PLATE.GUSSET PLATE | `` |
| D31 | D31.GUSSET PLATE.GUSSET PLATE | `` |
| D32 | D32.GUSSET PLATE.GUSSET PLATE | `` |
| D33 | D33.GUSSET PLATE.BEAM | `2*P5` |
| D34 | D34.GUSSET PLATE.BEAM | `2*P5` |
| D35 | D35.GUSSET PLATE.BEAM | `2*P5` |
| D36 | D36.GUSSET PLATE.BEAM | `2*P5` |
| D37 | D37.GUSSET PLATE.GIRDER | `2*P5` |
| D38 | D38.GUSSET PLATE.GIRDER | `2*P5` |
| D39 | D39.GUSSET PLATE.GIRDER | `2*P5` |
| D40 | D40.GUSSET PLATE.GIRDER | `2*P5` |
| D41 | D41.GUSSET PLATE.BEAM | `P10` |
| D42 | D42.GUSSET PLATE.BEAM | `P10` |
| D43 | D43.GUSSET PLATE.BEAM | `P10` |
| D44 | D44.GUSSET PLATE.BEAM | `P10` |
| D45 | D45.GUSSET PLATE.BEAM | `P10` |
| D46 | D46.GUSSET PLATE.BEAM | `P10` |
| D47 | D47.GUSSET PLATE.BEAM | `P10` |
| D48 | D48.GUSSET PLATE.BEAM | `P10` |
| D49 | D49.FITTING.GIRDER | `` |
| D50 | D50.FITTING.GIRDER | `` |
| D51 | D51.FITTING.GIRDER | `` |
| D52 | D52.BOLT.BEAM | `` |
| D53 | D53.BOLT.BEAM | `` |
| D54 | D54.BOLT.BEAM | `` |
| D55 | D55.BOLT.BEAM | `` |
| D56 | D56.BOLT.GIRDER | `P5` |
| D57 | D57.BOLT.GIRDER | `P5` |
| D58 | D58.BOLT.BEAM | `P5` |
| D59 | D59.BOLT.BEAM | `P5` |
| D60 | D60.BOLT.BEAM | `P10` |
| D61 | D61.BOLT.BEAM | `P10` |
| D62 | D62.BOLT.BEAM | `` |
| D63 | D63.BOLT.BEAM | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#160 | MATERIAL | `P11` |
| obj#160 | PROFILE | `"PL"+P9` |
| obj#160 | SERIE | `P19+"GP"` |
| obj#171 | CREATION | `P13` |
| obj#171 | MATERIAL | `P11` |
| obj#171 | PROFILE | `"PL"+P9` |
| obj#171 | SERIE | `P15+"SF"` |
| obj#181 | MATERIAL | `P12` |
| obj#181 | PROFILE | `"PL"+P3` |
| obj#181 | SERIE | `P20+"SP"` |
| obj#190 | MATERIAL | `P12` |
| obj#190 | PROFILE | `"PL"+P3` |
| obj#190 | SERIE | `P20+"SP"` |
| obj#204 | BOLT_GROUP_DISTX | `P4` |
| obj#204 | DX1 | `P5` |
| obj#204 | SIZE | `P2` |
| obj#223 | BOLT_GROUP_DISTX | `P4` |
| obj#223 | DX1 | `P5` |
| obj#223 | SIZE | `P2` |