# 0-P-STUD(WALL)

- File: `STUD WALL.uel`  (4,903 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	0-P-STUD(WALL)	All`
- Dialog parameters: 7 | Variables (P/custom): 7 | Distance vars (D): 10 | Parts with bound profile: 0
- Lookup files (fVF): -
- Functions used: -
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "0-P-STUD(WALL)")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("Stud Standard", "P1_standard", stud_standard, text, 1)
           parameter("Size", "P1_size", stud_size, number, 2)
           parameter("Length", "P1_length", stud_length, number, 3)
           parameter("Start point", "P2", distance, number, 4)
           parameter("N/S", "P3", YesNo, number, 5)
           parameter("F/S", "P4", YesNo, number, 6)
           parameter("Bolt distance y ", "P5", string, text, 7)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1_length | Length | 23 | `150.000` |
| P1_size | Size | 15 | `22.000` |
| P1_standard | Stud Standard | 16 | `0.000` |
| P2 | Start point | Length | `150.000` |
| P3 | N/S | Yes/No | `0` |
| P4 | F/S | Yes/No | `0` |
| P5 | Bolt distance y  | Text | `200` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.BOLT.BEAM | `` |
| D2 | D2.BOLT.BEAM | `` |
| D3 | D3.BOLT.BEAM | `` |
| D4 | D4.BOLT.BEAM | `` |
| D5 | D5.BOLT.BEAM | `` |
| D6 | D6.BOLT.BEAM | `` |
| D7 | D7.BOLT.BEAM | `` |
| D8 | D8.BOLT.BEAM | `` |
| D9 | D9.BOLT.0-P-STUD(WALL) | `P2` |
| D10 | D10.BOLT.0-P-STUD(WALL) | `P2` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#85 | BOLT_GROUP_DISTY | `P5` |
| obj#85 | CREATION | `P3` |
| obj#85 | EXTRA_LENGTH | `P1_length` |
| obj#85 | SIZE | `P1_size` |
| obj#98 | BOLT_GROUP_DISTY | `P5` |
| obj#98 | CREATION | `P4` |
| obj#98 | EXTRA_LENGTH | `P1_length` |
| obj#98 | SIZE | `P1_size` |