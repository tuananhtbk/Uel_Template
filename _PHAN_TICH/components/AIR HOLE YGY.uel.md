# ♥♥♥YGY_WG Air-Hole

- File: `AIR HOLE YGY.uel`  (5,090 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	♥♥♥YGY_WG Air-Hole	All`
- Dialog parameters: 3 | Variables (P/custom): 7 | Distance vars (D): 6 | Parts with bound profile: 0
- Lookup files (fVF): -
- Functions used: fTpl, round
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "♥♥♥YGY_WG Air-Hole")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("SPACING", "P2", distance, number, 1)
           parameter("HOLE SIZE", "P3", distance, number, 2)
           parameter("DIST.", "P6", distance_list, text, 3)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Length | `fTpl("LENGTH","ID651CE00F-0000-FA28-3136-393634303430")` *(formula)* |
| P2 | SPACING | Length | `400.00` |
| P3 | HOLE SIZE | Length | `50.00` |
| P4 | Parameter4 | Length | `round(P1/P2, 1)-1` *(formula)* |
| P5 | Parameter5 | Length | `(P1-P4*P2)*.5` *(formula)* |
| P6 | DIST. | 9 | `P4+"*"+P2` *(formula)* |
| P7 | Parameter7 | Length | `P3-20` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.BOLT.W_GIRDER | `` |
| D2 | D2.BOLT.W_GIRDER | `` |
| D3 | D3.BOLT.W_GIRDER | `` |
| D4 | D4.BOLT.W_GIRDER | `` |
| D5 | D5.BOLT.W_GIRDER | `` |
| D6 | D6.BOLT.W_GIRDER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#106 | BOLT_GROUP_DISTX | `P6` |
| obj#106 | DX1 | `P5` |
| obj#106 | TOLERANCE | `P7` |