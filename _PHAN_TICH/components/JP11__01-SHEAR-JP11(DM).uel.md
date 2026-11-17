# 01-Shear-JP11(DM)

- File: `JP11\01-SHEAR-JP11(DM).uel`  (17,572 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	01-Shear-JP11(DM)	All`
- Dialog parameters: 7 | Variables (P/custom): 66 | Distance vars (D): 24 | Parts with bound profile: 0
- Lookup files (fVF): Shear-JP11-SM355.dat, Shear-JP11-SS275.dat
- Functions used: fP, fVF, if, mid
- Dialog bitmaps: 0 | Saved presets: STD.p_01-Shear-JP11(DM)

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "01-Shear-JP11(DM)")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("SF Y/N", "sf", YesNo, number, 1)
           parameter("R/L", "rl", YesNo, number, 2)
           parameter("Edge_y (bot)", "rb2", integer, number, 3)
           parameter("Edge_y (top)", "rb1", integer, number, 4)
           parameter("Edge_x (out)", "rw2", integer, number, 5)
           parameter("Edge_x (int)", "rw1", integer, number, 6)
           parameter("Start_No", "pos", integer, number, 7)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | Parameter1 | Material | `if match(fP(proMATERIAL,"ID651B600B-0000-80AB-3136-393633303538"),"SHN355") then "SN355" else fP(proMATERIAL,"ID651B600B-0000-80AB-3136-393633303538") endif` *(formula)* |
| P2 | Floor | Text | `B3` |
| P2X | Parameter2 | Text | `mid(m8,0,find(m8,"M"))` *(formula)* |
| P3 | GP_MARK | Text | `P2X+"GP"` *(formula)* |
| P4 | SF_MARK | Text | `P2X+"SF"` *(formula)* |
| P5 | Parameter5 | Material | `if (m7=="SHN355") then "SN355" else if (m7=="SHN275") then "SN275" else m7 endif endif` *(formula)* |
| R | Parameter5 | Length | `if m3<=150 then 25 else if (150<m3&&m3<=200) then 30 else if (200<m3&&m3<=300) then 35 else 40 endif endif endif` *(formula)* |
| b1 | Bolt | Integer | `mid(dat6,0,find(dat6,"*"))` *(formula)* |
| b2 | Bolt | Integer | `mid(dat6,find(dat6,"*")+1,length(dat6))` *(formula)* |
| b3 | Bolt | Integer | `mid(dat7,0,find(dat7,"*"))` *(formula)* |
| b4 | Bolt | Integer | `mid(dat7,find(dat7,"*")+1,length(dat7))` *(formula)* |
| ch1 | Chamfer Y/N | Yes/No | `1` |
| ch2 | Chamfer Type | Integer | `if ch1==1 then 3 else if match(m1,"BH*") then 3 else 1 endif endif` *(formula)* |
| chs1 | Parameter1 | Integer | `if ch1==1 then 35 else if match(m1,"BH*") then 35 else m6 endif endif` *(formula)* |
| dat1 | Member | Profile | `if s7=="SS275" then fVF("Shear-JP11-SS275.dat",s1,1) else fVF("Shear-JP11-SM355.dat",s1,1) endif` *(formula)* |
| dat10 | Note | Length | `0.00` |
| dat11 | Note | Length | `0.00` |
| dat12 | Note | Length | `0.00` |
| dat13 | Note | Length | `0.00` |
| dat14 | Note | Length | `0.00` |
| dat15 | Note | Length | `0.00` |
| dat16 | Note | Length | `0.00` |
| dat17 | Note | Length | `0.00` |
| dat18 | Note | Length | `0.00` |
| dat19 | Note | Length | `0.00` |
| dat2 | Clear | Length | `if s7=="SS275" then fVF("Shear-JP11-SS275.dat",s1,2) else fVF("Shear-JP11-SM355.dat",s1,2) endif` *(formula)* |
| dat20 | Note | Length | `0.00` |
| dat3 | GP_Thk | Length | `if s7=="SS275" then fVF("Shear-JP11-SS275.dat",s1,3) else fVF("Shear-JP11-SM355.dat",s1,3) endif` *(formula)* |
| dat4 | B/Standard | Material | `if s7=="SS275" then fVF("Shear-JP11-SS275.dat",s1,4) else fVF("Shear-JP11-SM355.dat",s1,4) endif` *(formula)* |
| dat5 | B/Size | Integer | `if s7=="SS275" then fVF("Shear-JP11-SS275.dat",s1,5) else fVF("Shear-JP11-SM355.dat",s1,5) endif` *(formula)* |
| dat6 | Hor/Bolt | 9 | `if s7=="SS275" then fVF("Shear-JP11-SS275.dat",s1,6) else fVF("Shear-JP11-SM355.dat",s1,6) endif` *(formula)* |
| dat7 | Ver/Bolt | 9 | `if s7=="SS275" then fVF("Shear-JP11-SS275.dat",s1,7) else fVF("Shear-JP11-SM355.dat",s1,7) endif` *(formula)* |
| dat8 | Note | Length | `if s7=="SS275" then fVF("Shear-JP11-SS275.dat",s1,8) else fVF("Shear-JP11-SM355.dat",s1,8) endif` *(formula)* |
| dat9 | Note | Length | `if s7=="SS275" then fVF("Shear-JP11-SS275.dat",s1,9) else fVF("Shear-JP11-SM355.dat",s1,9) endif` *(formula)* |
| edge | Edge | Integer | `40` |
| eg | Parameter1 | Yes/No | `if m3<190 then 5 else 10 endif` *(formula)* |
| gppos | Parameter1 | Text | `if zpos== then "GP" else zpos+"GP" endif` *(formula)* |
| m1 | Parameter1 | Profile | `fP(proPROFILE,"ID651B600B-0000-80A7-3136-393633303538")` *(formula)* |
| m2 | Parameter2 | Length | `fP(proH,"ID651B600B-0000-80A7-3136-393633303538")` *(formula)* |
| m3 | Parameter3 | Length | `fP(proB,"ID651B600B-0000-80A7-3136-393633303538")` *(formula)* |
| m4 | Parameter4 | Length | `fP(proS,"ID651B600B-0000-80A7-3136-393633303538")` *(formula)* |
| m5 | Parameter5 | Length | `fP(proT,"ID651B600B-0000-80A7-3136-393633303538")` *(formula)* |
| m7 | Parameter7 | Material | `fP(proMATERIAL,"ID651B600B-0000-80A7-3136-393633303538")` *(formula)* |
| m8 | Parameter8 | Text | `fP(proSERIE,"ID651B600B-0000-80A7-3136-393633303538")` *(formula)* |
| mt | Moment/Shear | Yes/No | `0` |
| plt | USER GP_Thk | Length | `0.00` |
| pos | Start_No | Integer | `1` |
| rb1 | Edge_y (top) | Integer | `40` |
| rb2 | Edge_y (bot) | Integer | `40` |
| rl | R/L | Yes/No | `0` |
| rl1 | Parameter3 | Yes/No | `if rl==1 then 1 else 3 endif` *(formula)* |
| rw1 | Edge_x (int) | Integer | `40` |
| rw2 | Edge_x (out) | Integer | `40` |
| s1 | Parameter1 | Profile | `fP(proPROFILE,"ID651B600B-0000-80AB-3136-393633303538")` *(formula)* |
| s2 | Parameter2 | Length | `fP(proH,"ID651B600B-0000-80AB-3136-393633303538")` *(formula)* |
| s3 | Parameter3 | Length | `fP(proB,"ID651B600B-0000-80AB-3136-393633303538")` *(formula)* |
| s4 | Parameter4 | Length | `fP(proS,"ID651B600B-0000-80AB-3136-393633303538")` *(formula)* |
| s5 | Parameter5 | Length | `fP(proT,"ID651B600B-0000-80AB-3136-393633303538")` *(formula)* |
| s6 | Parameter6 | Length | `fP(proR,"ID651B600B-0000-80AB-3136-393633303538")` *(formula)* |
| s7 | Parameter7 | Material | `P1` *(formula)* |
| s8 | Parameter8 | Length | `0.00` |
| sf | SF Y/N | Yes/No | `1` |
| sf1 | Parameter1 | Yes/No | `if sf==1 then 0 else 3 endif` *(formula)* |
| sfpos | Parameter2 | Text | `if zpos== then "SF" else zpos+"SF" endif` *(formula)* |
| tol | Tol | Integer | `if dat5>=24 then 3 else 2 endif` *(formula)* |
| zpos | GP Prefix | Text | `P2` *(formula)* |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.PLATE.GIRDER | `` |
| D2 | D2.PLATE.GIRDER | `` |
| D3 | D3.PLATE.GIRDER | `` |
| D4 | D4.PLATE.GIRDER | `` |
| D5 | D5.PLATE.GIRDER | `` |
| D6 | D6.PLATE.GIRDER | `` |
| D7 | D7.PLATE.GIRDER | `` |
| D8 | D8.PLATE.GIRDER | `` |
| D9 | D9.PLATE.GIRDER | `` |
| D10 | D10.PLATE.GIRDER | `` |
| D11 | D11.PLATE.GIRDER | `` |
| D12 | D12.PLATE.GIRDER | `` |
| D13 | D13.PLATE.GIRDER | `` |
| D14 | D14.PLATE.GIRDER | `` |
| D15 | D15.PLATE.GIRDER | `` |
| D16 | D16.PLATE.GIRDER | `` |
| D17 | D17.PLATE.GIRDER | `` |
| D18 | D18.PLATE.GIRDER | `` |
| D19 | D19.PLATE.GIRDER | `` |
| D20 | D20.PLATE.GIRDER | `` |
| D21 | D21.PLATE.GIRDER | `` |
| D22 | D22.PLATE.GIRDER | `` |
| D23 | D23.PLATE.GIRDER | `` |
| D24 | D24.PLATE.GIRDER | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#271 | CREATION | `mt` |
| obj#285 | CREATION | `mt` |

## Preset `STD.p_01-Shear-JP11(DM)`
```
joint_attributes.saveas_file "STD"
joint_attributes.get_menu "STD"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 21
joint_attributes.ry_spring "0"
joint_attributes.ry_type 21
joint_attributes.rx_spring "0"
joint_attributes.rx_type 21
joint_attributes.uz_spring "0"
joint_attributes.uz_type 21
joint_attributes.uy_spring "0"
joint_attributes.uy_type 21
joint_attributes.ux_spring "0"
joint_attributes.ux_type 21
joint_attributes.support_condition 11
joint_attributes.restraint_comb 34
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 1
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 99
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.pos 1
joint_attributes.rl 0
joint_attributes.sf 0
```