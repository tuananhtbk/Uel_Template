# CustomStep_v2

- File: `NEC\CUSTOM_STEP.uel`  (40,489 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	CustomStep_v2	All`
- Dialog parameters: 11 | Variables (P/custom): 40 | Distance vars (D): 82 | Parts with bound profile: 3
- Lookup files (fVF): -
- Functions used: -
- Dialog bitmaps: 7 | Saved presets: -

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "CustomStep_v2")
   {
       helpurl("TeklaInc_CC.chm::/steel/custom_step.html")
       tab_page("", " Picture ", 1)
       {
	   picture("usa_custom_step_v2",700, 330, 50, 50)
	     attribute("P32", "Tread Type", option, "%s", no, none, "0.0", "0.0", 1)
                {
                     value("Just Plate",1)
                     value("Checkered_Plate",0)
                     value("Expanded_Metal",0)
                     value("Grating",0)
                     value("Grip_Strut",0)
                }
           parameter("", "P1", distance, number, 125,35,65)
           parameter("", "P3", distance, number, 312,189,65)
           parameter("", "P4", distance, number, 312,354,80)

           parameter("", "P21", distance, number, 565,170,65)
           parameter("", "P26", distance, number, 930,155,65)
           parameter("", "P25", distance, number, 730,282,65)
           parameter("", "P24", distance, number, 830,282,65)
           parameter("", "P22", distance, number, 653,335,65)
           parameter("", "P23", distance, number, 730,388,65)
	 }
       tab_page("", " Parts ", 2)
       {
		profile ("", , , P8 , ,P11 ,1)
		parameter("", "P12", string, text, 385,25,40)
		parameter("", "P13", integer, number, 455,25,40)
		parameter("", "P14", string, text, 385,49,40)
		parameter("", "P15", integer, number, 455,49,40)

		attribute("LABEL_F", "Finish", label2, "%s", no, none, "0.0", "0.0", 830, 5)
		parameter("", "P6", string, text, 830,26,80)
		attribute("LABEL_C", "Class", label2, "%s", no, none, "0.0", "0.0", 940, 5)
		parameter("", "P5", integer, number, 940,26,40)

		attribute("LABEL1", "Step Profile Prefix", label2, "%s", none, none, "0.0", "0.0", 20, 25)
           attribute("P7", "", option, "%s", no, none, "0.0", "0.0", 180,25,125)
	     {
	         value("GRTG", 1)
		   value("ALUM_GRTG", 0)
		   value("PL", 0)
		   value("PLT", 0)
		   value("PLATE", 0)
	     }
           attribute("P9", "Bought Out Item?", option, "%s", yes, none, "0.0","0.0", 4)
	     {
	         value("Yes", 1)
		   value("No", 0)
	     }
           parameter("BOI Name", "P10", string, text, 5)

		picture("usa_horz_line", 200, 5, 25, 160)

	  	attribute("number_lb", "Bolt size", label2,"%s",  none, none, "0.0", "0.0",20,180)
	 	attribute("bolt_standard_lb", "j_bolt_standard", label3,"%s",  none, none, "0.0", "0.0",20,205)
	  	attribute("tolerance_lb", "j_tolerance", label2,"%s",  none, none, "0.0", "0.0",20,230)
	  	parameter("","P27_diameter", bolt_size, number,180,180,125)
	 	parameter("","P27_screwdin", bolt_standard, text,180,205,125)
 	 	parameter("","P30", distance, number,180,230,125)
		attribute("P29", "", option,"%s", none, none, "0.0", "0.0",180,255,125)
		{
		value("j_site", 1)
		value("j_workshop", 0)
		}

		picture("usa_bolt_slots", 155, 155, 350, 175)
		parameter("","P28", distance, number, 475,245,60)


       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Step endplate thickness | Length | `25.4000` |
| P2 | Endplate profile | Text | `"PL"+(P1)` *(formula)* |
| P3 | Grating thickness | Length | `19.0500` |
| P4 | Thread width | Length | `228.6000` |
| P5 | Class | Text | `1` |
| P6 | FINISH | Text | `` |
| P8 | Material | Material | `A36` |
| P9 | BOI? | Text | `Yes` |
| P10 | BOI Name | Text | `BUY THIS` |
| P11 | Part Name | Text | `STEP` |
| P12 | Part Prefix | Text | `p` |
| P13 | Part Start No | Integer | `1` |
| P14 | Assy Prefix | Text | `P` |
| P15 | Assy Start No | Integer | `1` |
| P16 | GRTG profile | Profile | `"GRTG"+(P3)+"X"+(P4)` *(formula)* |
| P17 | ALUM_GRTG profile | Profile | `"ALUM_GRTG"+(P3)+"X"+(P4)` *(formula)* |
| P18 | PL profile | Profile | `"PL"+(P3)+"X"+(P4)` *(formula)* |
| P19 | PLT profile | Profile | `"PLT"+(P3)+"X"+(P4)` *(formula)* |
| P20 | PLATE profile | Profile | `"PLATE"+(P3)+"X"+(P4)` *(formula)* |
| P21 | Bracket Depth | Length | `88.9000` |
| P22 | Bracket V Clip | Length | `12.7000` |
| P23 | Bracket H Clip | Length | `12.7000` |
| P24 | H Bolt Edge | Length | `50.8000` |
| P25 | H Bolt Spacing | Length | `114.3000` |
| P26 | V Bolt Edge | Length | `57.1500` |
| P27_diameter | Bolt Size | 6 | `19.0500` |
| P27_screwdin | Bolt Standard | 7 | `A325N` |
| P28 | H Slot Length | Length | `50.8000` |
| P29 | Bolt Type | 8 | `1` |
| P30 | Hole Tolerance | Length | `3.1750` |
| P31 | Surfacing Thickness | Length | `if P32 == 3 then P3 else 1.5875 endif` *(formula)* |
| P32 | Tread Type | Text | `3` |
| P33 | Tread Type help1 | Text | `if P32 == 1 then "Checkered_Plate" else P34 endif` *(formula)* |
| P34 | Tread Type help2 | Text | `if P32 == 2 then "Expanded_Metal" else P35 endif` *(formula)* |
| P35 | Tread Type help3 | Text | `if P32 == 3 then "Grating" else P36 endif` *(formula)* |
| P36 | Tread Type help4 | Text | `if P32 == 4 then "Grip_Strut" else "1_1" endif` *(formula)* |
| C1 | CP-BColor EM-PColor | Text | `13158600` |
| C2 | CP-PColor EM-BColor | Text | `6579300` |
| C3 | G-BColor | Text | `16448250` |
| C4 | G-PColor | Text | `1644825` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | UpDown_Surface.ComponentPlane | `if P32 == 3 then -P3 else 0 endif` |
| D2 | UpDown_Surface.ComponentPlane | `if P32 == 3 then -P3 else 0 endif` |
| D3 | D3.PLATE.CustomStep_v2 | `` |
| D4 | D4.PLATE.CustomStep_v2 | `` |
| D5 | D5.PLATE.CustomStep_v2 | `` |
| D6 | D6.PLATE.CustomStep_v2 | `` |
| D7 | D7.PLATE.CustomStep_v2 | `` |
| D8 | D8.PLATE.CustomStep_v2 | `` |
| D9 | UpDown_Surface.ComponentPlane | `if P32 == 3 then -P3 else 0 endif` |
| D10 | UpDown_Surface.ComponentPlane | `if P32 == 3 then -P3 else 0 endif` |
| D11 | UpDown_Surface.ComponentPlane | `if P32 == 3 then -P3 else 0 endif` |
| D12 | D12.PLATE.CustomStep_v2 | `` |
| D13 | D13.BOLT.Step1 | `` |
| D14 | D14.BOLT.Step1 | `` |
| D15 | D15.BOLT.Step1 | `` |
| D16 | D16.BOLT.Step1 | `` |
| D17 | D17.PLATE.CustomStep_v2 | `` |
| D18 | D18.PLATE.CustomStep_v2 | `` |
| D19 | D19.PLATE.CustomStep_v2 | `` |
| D20 | D20.PLATE.CustomStep_v2 | `` |
| D21 | D21.PLATE.CustomStep_v2 | `` |
| D22 | D22.PLATE.CustomStep_v2 | `P23` |
| D23 | UpDown_Surface.ComponentPlane | `if P32 == 3 then -P3 else 0 endif` |
| D24 | D24.PLATE.CustomStep_v2 | `P23` |
| D25 | D25.PLATE.CustomStep_v2 | `` |
| D26 | D26.PLATE.CustomStep_v2 | `` |
| D27 | D27.PLATE.CustomStep_v2 | `` |
| D28 | D28.PLATE.CustomStep_v2 | `` |
| D29 | D29.PLATE.CustomStep_v2 | `P4` |
| D30 | D30.BEAM.Step1 | `` |
| D31 | D31.BEAM.Step1 | `` |
| D32 | D32.STEP.PLATE | `` |
| D33 | InOut_Surface.ComponentPlane | `if P32 == 3 then 109 else P4/2 endif` |
| D34 | InOut_Surface.ComponentPlane | `if P32 == 3 then 109 else P4/2 endif` |
| D35 | InOut_Surface.ComponentPlane | `` |
| D36 | InOut_Surface.ComponentPlane | `P4` |
| D37 | D37.STEP.PLATE | `` |
| D38 | InOut_Surface.ComponentPlane | `P4` |
| D39 | InOut_Surface.ComponentPlane | `` |
| D40 | RightSide_Surface.ComponentPlane | `P1` |
| D41 | RightSide_Surface.ComponentPlane | `P1` |
| D42 | LeftSide_Surface.ComponentPlane | `` |
| D43 | LeftSide_Surface.ComponentPlane | `P1` |
| D44 | D44.BOLT.BEAM | `P24` |
| D45 | D45.BOLT.BEAM | `P24+P25` |
| D46 | D46.BOLT.BEAM | `P24` |
| D47 | D47.BOLT.BEAM | `P24+P25` |
| D48 | D48.PLATE.CustomStep_v2 | `P4` |
| D49 | D49.PLATE.CustomStep_v2 | `P4-P23` |
| D50 | D50.PLATE.CustomStep_v2 | `P4` |
| D51 | D51.PLATE.CustomStep_v2 | `P4` |
| D52 | D52.BEAM.Step1 | `` |
| D53 | D53.BEAM.Step1 | `` |
| D54 | D54.PLATE.CustomStep_v2 | `P4-P23` |
| D55 | D55.PLATE.CustomStep_v2 | `P21` |
| D56 | D56.PLATE.CustomStep_v2 | `P21-P22` |
| D57 | D57.PLATE.CustomStep_v2 | `` |
| D58 | D58.PLATE.CustomStep_v2 | `` |
| D59 | D59.PLATE.CustomStep_v2 | `P21-P22` |
| D60 | D60.PLATE.CustomStep_v2 | `P21` |
| D61 | D61.PLATE.CustomStep_v2 | `P21` |
| D62 | D62.PLATE.CustomStep_v2 | `P21-P22` |
| D63 | LeftSide_Surface.ComponentPlane | `P1` |
| D64 | D64.PLATE.CustomStep_v2 | `P21` |
| D65 | D65.PLATE.CustomStep_v2 | `P21-P22` |
| D66 | D66.BOLT.STEP | `P24` |
| D67 | D67.BOLT.STEP | `P24` |
| D68 | D68.BOLT.CustomStep | `P26` |
| D69 | D69.BOLT.CustomStep | `P26` |
| D70 | D70.BOLT.CustomStep | `P26` |
| D71 | D71.BOLT.CustomStep | `P26` |
| D72 | D72.BOLT.STEP | `P24+P25` |
| D73 | D73.BOLT.STEP | `P24+P25` |
| D74 | D74.BOLT.CustomStep_v2 | `P26` |
| D75 | D75.BOLT.CustomStep_v2 | `P26` |
| D76 | D76.BOLT.CustomStep_v2 | `` |
| D77 | D77.BOLT.CustomStep_v2 | `` |
| D78 | D78.BOLT.CustomStep_v2 | `P26` |
| D79 | D79.BOLT.CustomStep_v2 | `P26` |
| D80 | D80.BOLT.CustomStep_v2 | `` |
| D81 | D81.BOLT.CustomStep_v2 | `` |
| D82 | LeftSide_Surface.ComponentPlane | `P1` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#192 | ASSEMBLYSERIE | `P14` |
| obj#192 | ASSEMBLYSTARTNUMBER | `P15` |
| obj#192 | CLASS | `P5` |
| obj#192 | MATERIAL | `P8` |
| obj#192 | NAME | `P11` |
| obj#192 | PROFILE | `if P7 == 0 then P16 else if P7 == 1 then P17 else if P7 == 2 then P18 else if P7 == 3 then P19 else P20 endif endif endif endif` |
| obj#192 | SERIE | `P12` |
| obj#192 | STARTNUMBER | `P13` |
| obj#209 | CLASS | `P5` |
| obj#209 | PROFILE | `"PL"+(P1)` |
| obj#221 | CLASS | `P5` |
| obj#221 | PROFILE | `"PL"+(P1)` |
| obj#230 | CREATION | `if P32 == 0 then 0 else 1 endif` |
| obj#230 | NAME | `P11` |
| obj#230 | POSITION_AT_DEPTH | `2` |
| obj#230 | SURF_PATTERN_TYPE | `P33` |
| obj#298 | BOLT_TYPE | `P29` |
| obj#298 | SIZE | `P27_diameter` |
| obj#298 | SLOTTED_HOLE_X | `P28` |
| obj#298 | STANDARD | `P27_screwdin` |
| obj#298 | TOLERANCE | `P30` |
| obj#314 | BOLT_TYPE | `P29` |
| obj#314 | SIZE | `P27_diameter` |
| obj#314 | STANDARD | `P27_screwdin` |
| obj#314 | TOLERANCE | `P30` |
| obj#336 | BOLT_TYPE | `P29` |
| obj#336 | SIZE | `P27_diameter` |
| obj#336 | SLOTTED_HOLE_X | `P28` |
| obj#336 | STANDARD | `P27_screwdin` |
| obj#336 | TOLERANCE | `P30` |
| obj#349 | BOLT_TYPE | `P29` |
| obj#349 | SIZE | `P27_diameter` |
| obj#349 | STANDARD | `P27_screwdin` |
| obj#349 | TOLERANCE | `P30` |