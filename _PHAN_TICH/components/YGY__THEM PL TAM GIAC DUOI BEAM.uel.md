# YGY_(ACT)-삼각PLATE

- File: `YGY\THEM PL TAM GIAC DUOI BEAM.uel`  (29,436 bytes, gzip)
- Type in .inp: **joint**  | Catalog line: `-1000	YGY_(ACT)-삼각PLATE	All
-10000	ACT	All`
- Dialog parameters: 6 | Variables (P/custom): 5 | Distance vars (D): 9 | Parts with bound profile: 1
- Lookup files (fVF): -
- Functions used: -
- Dialog bitmaps: 0 | Saved presets: 0-H669_336_26_48.p_YGY_(ACT)-삼각PLATE, 0-H689_342_32_58.p_YGY_(ACT)-삼각PLATE

## Dialog (.inp)
```
page("TeklaStructures","")
{
   joint(1, "YGY_(ACT)-삼각PLATE")
   {
       tab_page("", " Parameters 1 ", 1)
       {
           parameter("THK", "P1", string, text, 1)
           parameter("MARK", "P2", string, text, 2)
           parameter("FLG 반 값 (위치)", "P3", distance, number, 3)
           parameter("스칼렙  TYPE (0은 없음 1은 SNI", "P4", chamfer_type, number, 4)
           parameter("스칼렙  크기", "P5", distance, number, 5)
           parameter("높이", "D7", distance, number, 6)
       }
   }
}
```

## Variables

| Name | Label | Type | Value / formula |
|---|---|---|---|
| P1 | THK | Text | `22` |
| P2 | MARK | Text | `B1` |
| P3 | FLG 반 값 (위치) | Length | `150.00` |
| P4 | 스칼렙  TYPE (0은 없음 1은 SNI | Chamfer | `3` |
| P5 | 스칼렙  크기 | Length | `50.00` |

## Distance variables (D) bound to planes

| D | Binding | Formula |
|---|---|---|
| D1 | D1.ABPLATE.A_COLUMN | `` |
| D2 | D2.ABPLATE.A_COLUMN | `` |
| D3 | D3.ABPLATE.GIRDER | `P3` |
| D4 | D4.ABPLATE.GIRDER | `P3` |
| D5 | D5.ABPLATE.GIRDER | `` |
| D6 | D6.ABPLATE.GIRDER | `` |
| D7 | 높이 | `` |
| D8 | D8.ABPLATE.GIRDER | `P3` |
| D9 | D9.ABPLATE.A_COLUMN | `` |

## Part attribute bindings

| Object | Property | Formula |
|---|---|---|
| obj#213 | PROFILE | `"PL"+P1` |
| obj#213 | SERIE | `P2+"BRP"` |
| obj#1753 | APPLIED_VALUE | `a1` |
| obj#1766 | APPLIED_VALUE | `a1` |
| obj#1778 | APPLIED_VALUE | `a1` |
| obj#1805 | APPLIED_VALUE | `a5` |
| obj#1819 | APPLIED_VALUE | `a5` |
| obj#1833 | APPLIED_VALUE | `a5` |
| obj#1927 | APPLIED_VALUE | `b2` |
| obj#1942 | APPLIED_VALUE | `h2` |
| obj#1957 | APPLIED_VALUE | `h2` |
| obj#1970 | APPLIED_VALUE | `h2` |
| obj#1996 | APPLIED_VALUE | `b2` |
| obj#2010 | APPLIED_VALUE | `b2` |
| obj#2023 | APPLIED_VALUE | `b2` |
| obj#2037 | APPLIED_VALUE | `b2` |
| obj#2054 | APPLIED_VALUE | `b2` |
| obj#2068 | APPLIED_VALUE | `b2` |
| obj#2083 | APPLIED_VALUE | `b2` |
| obj#2097 | APPLIED_VALUE | `b2` |
| obj#2112 | APPLIED_VALUE | `b2` |
| obj#2126 | APPLIED_VALUE | `b2` |
| obj#2140 | APPLIED_VALUE | `b2` |
| obj#2153 | APPLIED_VALUE | `b1*0.5` |
| obj#2167 | APPLIED_VALUE | `b1*0.5` |
| obj#2180 | APPLIED_VALUE | `h1*0.5` |
| obj#2194 | APPLIED_VALUE | `h1*0.5` |
| obj#2221 | APPLIED_VALUE | `d9` |
| obj#2249 | APPLIED_VALUE | `h6` |
| obj#2262 | APPLIED_VALUE | `h6` |
| obj#2275 | APPLIED_VALUE | `h6` |
| obj#2288 | APPLIED_VALUE | `d9` |
| obj#2302 | APPLIED_VALUE | `d9` |
| obj#2316 | APPLIED_VALUE | `d9` |
| obj#2330 | APPLIED_VALUE | `d9` |
| obj#2344 | APPLIED_VALUE | `d9` |
| obj#2358 | APPLIED_VALUE | `d9` |

## Preset `0-H669_336_26_48.p_YGY_(ACT)-삼각PLATE`
```
joint_attributes.saveas_file "0-H669*336*26*48"
joint_attributes.get_menu "< Defaults >"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D7 250.000000
joint_attributes.P4 3
joint_attributes.P3 168.000000
joint_attributes.P2 "1"
joint_attributes.P1 "30"
```

## Preset `0-H689_342_32_58.p_YGY_(ACT)-삼각PLATE`
```
joint_attributes.saveas_file "0-H689*342*32*58"
joint_attributes.get_menu "0-H689*342*32*58"
joint_attributes.name_enable 1
joint_attributes.AD_profile_len "0"
joint_attributes.AD_profile ";;;;;;;"
joint_attributes.analysis_offset_x "0"
joint_attributes.rz_spring "0"
joint_attributes.rz_type 272647168
joint_attributes.ry_spring "0"
joint_attributes.ry_type 272647168
joint_attributes.rx_spring "0"
joint_attributes.rx_type 272647168
joint_attributes.uz_spring "0"
joint_attributes.uz_type 272647168
joint_attributes.uy_spring "0"
joint_attributes.uy_type 272647168
joint_attributes.ux_spring "0"
joint_attributes.ux_type 272647168
joint_attributes.support_condition 272647168
joint_attributes.restraint_comb 1737418240
joint_attributes.restraints_sec 0
joint_attributes.use_restraints 0
joint_attributes.ac_root ""
joint_attributes.ad_root ""
joint_attributes.joint_code ""
joint_attributes.group_no 0
joint_attributes.OBJECT_LOCKED 0
joint_attributes.zang2 0.000000
joint_attributes.zang1 0.000000
joint_attributes.zsuunta 7
joint_attributes.D7 400.000000
joint_attributes.P4 3
joint_attributes.P3 171.000000
joint_attributes.P2 "1"
joint_attributes.P1 "30"
```