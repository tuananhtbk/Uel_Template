# 0-P-AIR-HOLE

- File: `AIR HOLE.uel`  (8,209 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	0-P-AIR-HOLE	All`
- Dialog parameters: 13 | Variables (P/custom): 26 | Distance vars (D): 10 | Parts with bound profile: 0
- Lookup files (fVF): -
- Functions used: if
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "0-P-AIR-HOLE")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("BOLT Q'TY(FLG.)", "P1", integer, number, 1)
           parameter("BOLT TO CENTER", "P2", distance, number, 2)
           parameter("START FLG.", "P3", distance, number, 3)
           parameter("@", "P5", distance, number, 4)
           parameter("BOLT standard", "P6_standard_screwdin", bolt_standard, text, 5)
           parameter("BOLT size", "P6_size_diameter", bolt_size, number, 6)
           parameter("BOLT length", "P6_length", stud_length, number, 7)
           parameter("Top(Y/N)", "P8", YesNo, number, 8)
           parameter("Bott(Y/N)", "P9", YesNo, number, 9)
           parameter("BOLT standard", "P7_standard_screwin_screwdin", bolt_standard, text, 10)
           parameter("BOLT size", "P7_size_diameter", bolt_size, number, 11)
           parameter("BOLT Q'TY-Y (FLG)", "P20", integer, number, 12)
           parameter("FLG@Y", "P22", distance, number, 13)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | BOLT Q'TY(FLG.) | Integer | `6` |
| P2 | BOLT TO CENTER | Length | `0.00` |
| P3 | START FLG. | Length | `175.00` |
| P4 | Parameter4 | 9 | `(P1-1)+"*"+P5` *(formula)* |
| P5 | @ | Length | `200.00` |
| P6_length | BOLT length | 23 | `0.00` |
| P6_size_diameter | BOLT size | 6 | `0.00` |
| P6_standard_screwdin | BOLT standard | 7 | `0.00` |
| P7_length_diameter | BOLT TYPE | 8 | `150.00` |
| P7_size_diameter | BOLT size | 6 | `19.00` |
| P7_standard_screwin_screwdin | BOLT standard | 7 | `0.00` |
| P8 | Top(Y/N) | Yes/No | `1` |
| P9 | Bott(Y/N) | Yes/No | `1` |
| P10 | BOLT Q'TY-X (WEB) | Integer | `7` |
| P11 | WEB@X | Length | `100.00` |
| P12 | UP/DN | Length | `0.00` |
| P13 | Parameter13 | 9 | `(P10-1)+"*"+P11` *(formula)* |
| P14 | BOLT Q'TY-Y (WEB) | Integer | `2` |
| P15 | Parameter15 | 9 | `if (P14==1) then 0 else (P14-1)+"*"+P19  endif` *(formula)* |
| P16 | START WEB. | Integer | `100` |
| P17 | NS(Y/N) | Yes/No | `1` |
| P18 | FS(Y/N) | Yes/No | `1` |
| P19 | WEB@Y | Length | `200.00` |
| P20 | BOLT Q'TY-Y (FLG) | Integer | `2` |
| P21 | Parameter21 | 9 | `if (P20==1) then 0 else (P20-1)+"*"+P22  endif` *(formula)* |
| P22 | FLG@Y | Length | `150.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.BOLT.T-BEAM | `` |
| D2 | D2.BOLT.T-BEAM | `` |
| D3 | D3.BOLT.T-BEAM | `` |
| D4 | D4.BOLT.T-BEAM | `` |
| D5 | D5.BOLT.0-P-STUD(T+B) | `` |
| D6 | D6.BOLT.0-P-STUD(T+B) | `` |
| D7 | D7.BOLT.T-BEAM | `P2` |
| D8 | D8.BOLT.T-BEAM | `P2` |
| D9 | D9.BOLT.T-BEAM | `P2` |
| D10 | D10.BOLT.T-BEAM | `P2` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#225 | BOLT_GROUP_DISTX | `P4` |
| obj#225 | BOLT_GROUP_DISTY | `P21` |
| obj#225 | CREATION | `P8` |
| obj#225 | DX1 | `P3` |
| obj#225 | EXTRA_LENGTH | `P6_length` |
| obj#225 | SIZE | `P6_size` |
| obj#239 | BOLT_GROUP_DISTX | `P4` |
| obj#239 | BOLT_GROUP_DISTY | `P21` |
| obj#239 | CREATION | `P9` |
| obj#239 | DX1 | `P3` |
| obj#239 | EXTRA_LENGTH | `P6_length` |
| obj#239 | SIZE | `P6_size` |