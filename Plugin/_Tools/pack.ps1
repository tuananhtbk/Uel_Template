# Dong goi bo cai (goi qua pack.bat): build release roi chep sang _Dist\UEL_<yyyyMMdd>\
$ErrorActionPreference = 'Stop'
$Root = Split-Path -Parent $PSScriptRoot
& powershell -NoProfile -ExecutionPolicy Bypass -File (Join-Path $PSScriptRoot 'build.ps1') release
if ($LASTEXITCODE -ne 0) { exit 1 }

$out = Join-Path $Root ("_Dist\UEL_" + (Get-Date -Format 'yyyyMMdd'))
if (Test-Path -LiteralPath $out) { Remove-Item -LiteralPath $out -Recurse -Force }
New-Item -ItemType Directory -Force (Join-Path $out '_Tools') | Out-Null
foreach ($bo in '18.0', '2020+') {
  $src = Join-Path $Root $bo
  if (-not (Test-Path -LiteralPath $src)) { continue }
  New-Item -ItemType Directory -Force (Join-Path $out $bo) | Out-Null
  Get-ChildItem -LiteralPath $src -Filter *.dll | Copy-Item -Destination (Join-Path $out $bo)
}
# anh hop thoai cua moi component -> <bo cai>\bitmaps\ (install.ps1 chep vao thu muc anh cua Tekla)
$bmpOut = Join-Path $out 'bitmaps'
Get-ChildItem -LiteralPath $Root -Directory | Where-Object { -not $_.Name.StartsWith('_') -and (Test-Path -LiteralPath (Join-Path $_.FullName 'bitmaps')) } | ForEach-Object {
  New-Item -ItemType Directory -Force $bmpOut | Out-Null
  Get-ChildItem -LiteralPath (Join-Path $_.FullName 'bitmaps') -File | Copy-Item -Destination $bmpOut
}
Copy-Item -LiteralPath (Join-Path $Root 'install.bat') -Destination $out
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'install.ps1'), (Join-Path $PSScriptRoot 'tekla.ps1') -Destination (Join-Path $out '_Tools')

$names = (Get-ChildItem -LiteralPath (Join-Path $Root '18.0'), (Join-Path $Root '2020+') -Filter *.dll -ErrorAction SilentlyContinue |
  ForEach-Object { $_.BaseName } | Sort-Object -Unique) -join ', '
$txt = @"
Bo plugin UEL cho Tekla Structures
Component: $names

  18.0\   DLL cho Tekla 18.0 va 2019
  2020+\  DLL cho Tekla 2020, 2021, 2025...
  bitmaps\  anh minh hoa hop thoai (install.bat tu chep vao thu muc anh cua Tekla)

1. Tat ban Tekla can cai.
2. Chuot phai install.bat - Run as administrator (Tekla 2019 tro di nam trong Program Files),
   hoac mo cmd quyen admin roi go:
     install.bat 18                 -> Tekla 18.0
     install.bat 2025               -> Tekla 2025.0   (tuong tu 2019, 2020, 2021...)
     install.bat all                -> moi ban Tekla tim thay tren may
     install.bat 2025 RAKER+T_GIRDER   -> chi 1 component
   Tekla cai o cho khac: dat bien TEKLA_ROOT_<ban>, vd.  set "TEKLA_ROOT_2025.0=D:\Tekla\2025.0"
3. Mo Tekla, tim component trong Component catalog theo ten (vd. RAKER+T_GIRDER).

Bang tra .txt/.dat (neu component can) dat vao thu muc model hoac XS_FIRM.
"@
[IO.File]::WriteAllText((Join-Path $out 'HUONG_DAN_CAI.txt'), $txt.Replace("`n", "`r`n").Replace("`r`r", "`r"), [Text.Encoding]::ASCII)
Write-Host "Da dong goi: $out"
