# Kiem tra bo plugin voi moi ban Tekla cai tren may (build.bat check goi file nay, sau khi build xong).
#  1. Bien dich lai moi component voi DLL API cua tung ban   -> ma nguon con hop le voi ban do?
#  2. Quet DLL trong bo tuong ung (18.0\ cho Tekla 18/2019, 2020+\ cho Tekla 2020 tro di) va EXE thu nghiem:
#     moi lop/ham/truong Tekla API ma DLL goi con ton tai, dung chu ky, trong ban do -> DLL chay duoc o ban do?
# Can Mono.Cecil.dll 0.9.x (co san trong thu muc plugin cua Tekla 18).
# Dot-source tu build.ps1 (dung $Root, $Obj, $comps, Build-Component, Get-TeklaVersions...).

function Check-Binary($apiDir, $files) {
  $asms = @{}
  foreach ($n in 'Tekla.Structures', 'Tekla.Structures.Model', 'Tekla.Structures.Plugins') {
    $asms[$n] = [Mono.Cecil.AssemblyDefinition]::ReadAssembly((Join-Path $apiDir "$n.dll"))
  }
  function FindType($scope, $fullName) {
    $list = if ($scope -and $asms.ContainsKey($scope)) { @($asms[$scope]) } else { $asms.Values }
    foreach ($a in $list) {
      $t = $a.MainModule.GetType($fullName)
      if ($t) { return $t }
      foreach ($et in $a.MainModule.ExportedTypes) { if ($et.FullName -eq $fullName) { return $et.Resolve() } }
    }
    return $null
  }
  function FindDecl($tr) {
    if ($tr.IsNested) {
      $p = FindDecl $tr.DeclaringType
      if ($p) { return ($p.NestedTypes | Where-Object { $_.Name -eq $tr.Name } | Select-Object -First 1) }
      return $null
    }
    return FindType $tr.Scope.Name $tr.FullName
  }
  $bad = New-Object System.Collections.Generic.List[string]
  $checked = 0
  foreach ($file in $files) {
    $m = [Mono.Cecil.ModuleDefinition]::ReadModule($file)
    $typeRefs = New-Object System.Collections.Generic.List[object]
    $memRefs = New-Object System.Collections.Generic.List[object]
    $stack = New-Object System.Collections.Stack
    foreach ($ty in $m.Types) { $stack.Push($ty) }
    while ($stack.Count -gt 0) {
      $ty = $stack.Pop()
      foreach ($n in $ty.NestedTypes) { $stack.Push($n) }
      if ($ty.BaseType) { $typeRefs.Add($ty.BaseType) }
      foreach ($ca in $ty.CustomAttributes) { $memRefs.Add($ca.Constructor) }
      foreach ($fd in $ty.Fields) { $typeRefs.Add($fd.FieldType); foreach ($ca in $fd.CustomAttributes) { $memRefs.Add($ca.Constructor) } }
      foreach ($md in $ty.Methods) {
        if (-not $md.HasBody) { continue }
        foreach ($ins in $md.Body.Instructions) {
          $op = $ins.Operand
          if ($op -is [Mono.Cecil.MethodReference] -and -not ($op -is [Mono.Cecil.MethodDefinition])) { $memRefs.Add($op) }
          elseif ($op -is [Mono.Cecil.FieldReference] -and -not ($op -is [Mono.Cecil.FieldDefinition])) { $memRefs.Add($op) }
          elseif ($op -is [Mono.Cecil.TypeReference] -and -not ($op -is [Mono.Cecil.TypeDefinition])) { $typeRefs.Add($op) }
        }
      }
    }
    foreach ($tr in ($typeRefs | Where-Object { $_ -and -not $_.IsGenericInstance -and -not $_.IsArray })) {
      $sc = if ($tr.IsNested) { $tr.DeclaringType.Scope.Name } else { $tr.Scope.Name }
      if ($sc -notlike 'Tekla.Structures*') { continue }
      $checked++
      if (-not (FindDecl $tr)) { $bad.Add("Thieu lop: $($tr.FullName)") }
    }
    foreach ($mr in $memRefs) {
      $dt = $mr.DeclaringType
      if ($dt.IsGenericInstance) { continue }
      $sc = if ($dt.IsNested) { $dt.DeclaringType.Scope.Name } else { $dt.Scope.Name }
      if ($sc -notlike 'Tekla.Structures*') { continue }
      $checked++
      $cur = FindDecl $dt
      if (-not $cur) { $bad.Add("Thieu lop: $($mr.FullName)"); continue }
      $found = $false
      while ($cur -and -not $found) {
        foreach ($c in (@($cur.Methods) + @($cur.Fields))) {
          if ($c.Name -ne $mr.Name) { continue }
          if ($mr -is [Mono.Cecil.MethodReference]) {
            if ($c -is [Mono.Cecil.MethodDefinition] -and $c.Parameters.Count -eq $mr.Parameters.Count -and $c.ReturnType.FullName -eq $mr.ReturnType.FullName) {
              $ok = $true
              for ($i = 0; $i -lt $c.Parameters.Count; $i++) { if ($c.Parameters[$i].ParameterType.FullName -ne $mr.Parameters[$i].ParameterType.FullName) { $ok = $false } }
              if ($ok) { $found = $true; break }
            }
          } elseif ($c -is [Mono.Cecil.FieldDefinition] -and $c.FieldType.FullName -eq $mr.FieldType.FullName) { $found = $true; break }
        }
        if (-not $found -and $cur.BaseType -and $cur.BaseType.Scope.Name -like 'Tekla.Structures*') { $cur = FindType $cur.BaseType.Scope.Name $cur.BaseType.FullName } else { break }
      }
      if (-not $found) { $bad.Add("Thieu ham/doi chu ky: $($mr.FullName)") }
    }
  }
  return ,@($checked, ($bad | Sort-Object -Unique))
}

function Invoke-Check {
  $versions = @(Get-TeklaVersions)
  if ($versions.Count -eq 0) { Write-Host 'Khong tim thay ban Tekla nao.'; return $false }
  Write-Host ("Tim thay: " + (($versions | ForEach-Object { "$($_.Ver) -> bo $($_.Target)" }) -join ', '))
  $cecil = $null
  foreach ($v in $versions) {
    $c = Join-Path $v.Api 'Mono.Cecil.dll'
    if ((Test-Path $c) -and ([Reflection.AssemblyName]::GetAssemblyName($c).Version.Major -eq 0)) { $cecil = $c; break }
  }
  if ($cecil) { Add-Type -Path $cecil }
  $tester = Join-Path $Root '_Dev\UEL_TESTER.exe'
  $fail = 0
  foreach ($v in $versions) {
    $out = Join-Path $Obj ("check\" + $v.Ver)
    $err = @()
    foreach ($c in $comps) {
      try { Build-Component $c $v.Target $v.Api @('release') $out *> $null } catch { $err += $c.Name }
    }
    $line = "{0,-8} bien dich lai: {1}" -f $v.Ver, $(if ($err.Count -eq 0) { 'OK' } else { 'LOI ' + ($err -join ', ') })
    if ($err.Count -gt 0) { $fail++ }
    $dlls = @(Get-ChildItem -LiteralPath (Join-Path $Root $v.Target) -Filter *.dll -ErrorAction SilentlyContinue | ForEach-Object FullName)
    if (Test-Path $tester) { $dlls += $tester }
    if ($cecil -and $dlls.Count -gt 0) {
      $r = Check-Binary $v.Api $dlls
      $line += "   | DLL bo {0} goi {1} tham chieu API: {2}" -f $v.Target, $r[0], $(if ($r[1].Count -eq 0) { 'OK' } else { "$($r[1].Count) LOI" })
      Write-Host $line
      $r[1] | ForEach-Object { Write-Host "           $_" }
      if ($r[1].Count -gt 0) { $fail++ }
    } else {
      Write-Host ($line + '   | (bo qua quet nhi phan: thieu Mono.Cecil hoac chua build)')
    }
  }
  return ($fail -eq 0)
}
