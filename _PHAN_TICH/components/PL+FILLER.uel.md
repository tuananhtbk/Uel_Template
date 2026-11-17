# PL+FILLER

- File: `PL+FILLER.uel`  (3,749 bytes, gzip)
- Type in .inp: **detail**  | Catalog line: `-100	PL+FILLER	All`
- Dialog parameters: 1 | Variables (P/custom): 3 | Distance vars (D): 4 | Parts with bound profile: 0
- Lookup files (fVF): -
- Functions used: fP, mid
- Dialog bitmaps: 0 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   detail(1, "PL+FILLER")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("ZONE", "P3", string, text, 1)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Text | `fP(proSERIE,"ID673A8EA2-0000-543C-3137-333138393133")` *(formula)* |
| P2 | Parameter2 | Text | `mid(P1,0,find(P1,"M"))` *(formula)* |
| P3 | ZONE | Text | `P2+"SF"` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.FILLER | `` |
| D2 | D2.PLATE.FILLER | `` |
| D3 | D3.PLATE.FILLER | `` |
| D4 | D4.PLATE.FILLER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#73 | SERIE | `P3` |