# Rail Splice

- File: `NEC\HANDRAIL_SPLICE.uel`  (15,164 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	Handrail Splice	All`
- Dialog parameters: 2 | Variables (P/custom): 30 | Distance vars (D): 4 | Parts with bound profile: 1
- Lookup files (fVF): -
- Functions used: fP
- Dialog bitmaps: 4 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "Rail Splice")
   {
       helpurl("TeklaInc_CC.chm::/steel/handrail_splice.html")
       tab_page("", " Picture ", 1)
       {
           picture("usa_pic_pipe_splice", 311,185,120,40)
           parameter("", "P1", distance, number, 295,25,60)
           parameter("", "P4", distance, number, 205,64,80)
           parameter("", "P5", distance, number, 385,64,80)

           attribute("O2", "Gap Type", option, "%s", no, none, "0.0", "0.0", 372,300,160)
                {
                     value("usa_center_fit.xbm",1)
                     value("usa_offset_fit.xbm",0)
                     value("usa_offset_fit2.xbm.",0)
                }

           picture("jbp_bs_hor_line_1", 200, 5, 25, 350)    
           picture("jbp_bs_hor_line_1", 200, 5, 225, 350)             
           picture("jbp_bs_hor_line_1", 50, 5, 450, 350)

           attribute("O1", "Override Parametric Rod with Profile below", option, "%s", no, none, "0.0", "0.0", 372,362,160,"dia_enable_field_ocb:P9=1;P12=0")
                {
                     value("No",1)
                     value("Yes",0)
                }

           picture("jbp_bs_hor_line_1", 200, 5, 25, 397)    
           picture("jbp_bs_hor_line_1", 200, 5, 225, 397)             
           picture("jbp_bs_hor_line_1", 50, 5, 450, 397)

	   part ("Parametric",     P13,   ,   , , P16, , ,18)
           parameter("", "P17", string, text, 385,432,40)
           parameter("", "P18", integer, number, 455,432,40)
           parameter("", "P19", string, text, 385,456,40)
           parameter("", "P20", integer, number, 455,456,40)
	   profile("Inserted Profile",   P12,  ,  , , ,19)

       }
tab_page("", " Weld ", 2)
       {
	     picture("usa_pic_pipe_spliceB", 308,167,120,200)
           
	     attribute("header", "No        Size                   Type                  Wshop/site", label, "%s", no, none, "0.0", "0.0")
	     attribute("weld", "1", label, "%s", no, none, "0.0", "0.0",2)

           parameter("", "P21", weld_type, number, 160,30,100)
           parameter("", "P22", weld_type, number, 160,56,100)
           parameter("", "P23", welding_site, number, 300,43,60)
           parameter("", "P14", distance, number, 64,30,60)
           parameter("", "P15", distance, number, 64,56,60)

	     attribute("weld", "2", label, "%s", no, none, "0.0", "0.0",4)

           parameter("", "P26", weld_type, number, 160,85,100)
           parameter("", "P27", weld_type, number, 160,111,100)
           parameter("", "P28", welding_site, number, 300,98,60)
           parameter("", "P24", distance, number, 64,85,60)
           parameter("", "P25", distance, number, 64,111,60)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Gap | Length | `12.7000` |
| P2 | Prim. Thk | Length | `fP(proS,"ID67C2A424-0000-A1EC-3137-343038313034")` *(formula)* |
| P3 | 1/2 the Gap | Length | `P1*1/2` *(formula)* |
| P4 | Inserted-Prim | Length | `203.2000` |
| P5 | Inserted-Sec | Length | `203.2000` |
| P6 | Outer Dia.-Prim | Length | `fP(proH,"ID67C2A424-0000-A1EC-3137-343038313034")` *(formula)* |
| P7 | Inner Dia-Prim | Length | `(P6-P2)-P2` *(formula)* |
| P8 | Inner Clearence | Length | `3.1750` |
| P9 | Inner Dia. Prim. minus clearen | Length | `P7-P8` *(formula)* |
| P10 | Parameter10 | Length | `if O2>1 then P1 else 0 endif` *(formula)* |
| P11 | Parameter11 | Length | `if O2>1 then 0 else P1 endif` *(formula)* |
| P12 | Insert Profile | Profile | `` |
| P13 | Parametric Rod | Profile | `"ROD"+P9` *(formula)* |
| P14 | FS weld | Length | `0.0000` |
| P15 | NS weld | Length | `4.7625` |
| P16 | Material | Material | `A53` |
| P17 | Serie | Text | `pi` |
| P18 | start no | Integer | `1` |
| P19 | assy serie | Text | `Pi` |
| P20 | assy start no | Integer | `1` |
| P21 | FS wtype | 10 | `0` |
| P22 | NS wtype | 10 | `0` |
| P23 | shop-site | 12 | `0"` |
| P24 | FS weld2 | Length | `0.0000` |
| P25 | NS weld2 | Length | `0.0000` |
| P26 | FS wtype2 | 10 | `0` |
| P27 | Ns wtype2 | 10 | `0` |
| P28 | shop-site2 | 12 | `0"` |
| P29 | Parameter29 | Length | `0.0000` |
| O1 | Yes/No | Integer | `0` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.RAIL.YZ plane | `P4+D4` |
| D2 | D2.RAIL.YZ plane | `P5+D3` |
| D3 | D3.FITTING.YZ plane | `if O2>0 then P10 else P3 endif` |
| D4 | D4.FITTING.YZ plane | `if O2>0 then P11 else P3 endif` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#158 | ASSEMBLYSERIE | `P19` |
| obj#158 | ASSEMBLYSTARTNUMBER | `P20` |
| obj#158 | MATERIAL | `P16` |
| obj#158 | PROFILE | `if O1>0 then P12 else P13 endif` |
| obj#158 | SERIE | `P17` |
| obj#158 | STARTNUMBER | `P18` |
| obj#259 | SIZE | `P24` |
| obj#259 | SIZE2 | `P25` |
| obj#259 | TYPE | `P26` |
| obj#259 | TYPE2 | `P27` |
| obj#259 | WELDING_SITE | `P28` |
| obj#266 | SIZE | `P14` |
| obj#266 | SIZE2 | `P15` |
| obj#266 | TYPE | `P21` |
| obj#266 | TYPE2 | `P22` |
| obj#266 | WELDING_SITE | `P23` |