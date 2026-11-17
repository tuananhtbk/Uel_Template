# mk_PLATE개선_(CJP_FB)_3SIDE

- File: `240103\CJP_FB_3SIDES.uel`  (16,003 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	mk_PLATE개선_(CJP_FB)_3SIDE	All`
- Dialog parameters: 5 | Variables (P/custom): 27 | Distance vars (D): 0 | Parts with bound profile: 6
- Lookup files (fVF): -
- Functions used: fP, if, max, mid, min, tan
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "mk_PLATE개선_(CJP_FB)_3SIDE")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("개선_Root", "vP2", distance, number, 1)
           parameter("개선각", "vP3", angle, number, 2)
           parameter("개선방향", "vP5", YesNo, number, 3)
           parameter("ZONE", "vP8", string, text, 4)
           parameter("CUT-여장", "vP15", distance, number, 5)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| Password | Parameter24 | Text | `din951` |
| FFP1 | Parameter1 | Length | `fP(proB,"ID6594FAAB-0000-5C3A-3137-303432363332")` *(formula)* |
| FFP2 | Parameter1 | Text | `fP(proSERIE,"ID6594FAAB-0000-5C15-3137-303432363332")` *(formula)* |
| FFP3 | Parameter1 | Text | `mid(FFP2,0,2)` *(formula)* |
| vP1 | Parameter1 | Length | `fP(proB,"ID6594FAAB-0000-5C15-3137-303432363332")` *(formula)* |
| vP10 | Parameter10 | Length | `max(vD25,vD26)` *(formula)* |
| vP11 | Parameter11 | Length | `min(vD25,vD26)` *(formula)* |
| vP12 | Parameter12 | Length | `(vP1)+(FFP1/2)` *(formula)* |
| vP13 | Parameter13 | Length | `if (vP5==0) then (FFP1/2) else -vP12 endif` *(formula)* |
| vP14 | FB_방향 | Yes/No | `1` |
| vP15 | CUT-여장 | Length | `0.00` |
| vP16 | Parameter16 | Length | `0.00` |
| vP17 | Parameter17 | Length | `0.00` |
| vP18 | Parameter18 | Length | `0.00` |
| vP19 | Parameter19 | Length | `0.00` |
| vP2 | 개선_Root | Length | `0.00` |
| vP20 | Parameter20 | Length | `0.00` |
| vP21 | Parameter21 | Length | `0.00` |
| vP22 | Parameter22 | Length | `0.00` |
| vP23 | Parameter23 | Length | `0.00` |
| vP3 | 개선각 | Angle | `35.0000` |
| vP4 | Parameter4 | Length | `(vP1-2)*tan(vP3)` *(formula)* |
| vP5 | 개선방향 | Yes/No | `0` |
| vP6 | Parameter6 | Length | `if (vP5==0) then vP1 else 0 endif` *(formula)* |
| vP7 | Parameter7 | Length | `if (vP5==0) then 2 else (vP1-2) endif` *(formula)* |
| vP8 | ZONE | Text | `FFP3` *(formula)* |
| vP9 | Parameter9 | Text | `if (vP8==0) then "" else vP8 endif` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#135 | SERIE | `vP8+"FB"` |
| obj#147 | SERIE | `vP8+"FB"` |
| obj#155 | SERIE | `vP8+"FB"` |
| obj#161 | PROFILE | `vP10+50+vP15` |
| obj#175 | PROFILE | `vP10+70+vP15` |
| obj#186 | PROFILE | `vP11+50+vP15` |
| obj#197 | PROFILE | `vP11+70+vP15` |
| obj#208 | PROFILE | `vP11+50+vP15` |
| obj#217 | PROFILE | `vP11+70+vP15` |