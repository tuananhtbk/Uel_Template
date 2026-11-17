# Tim cac ban Tekla tren may. Dung chung cho build.ps1, install.ps1 (dot-source: . "$PSScriptRoot\tekla.ps1").
#
# Bo cuc da biet:  18.0      = C:\TeklaStructures\18.0\nt\bin\plugins        (API + plugin)
#                  2019-2021 = C:\Program Files\Tekla Structures\<v>\nt\bin\plugins
#                  2025      = C:\Program Files\Tekla Structures\2025.0\bin (API) va ...\bin\plugins
# Tekla cai o cho khac: dat bien moi truong TEKLA_ROOT_<ban>, vd. TEKLA_ROOT_2025.0 = D:\Tekla\2025.0
#
# Moi ban Tekla dung 1 trong 2 bo DLL (2 thu muc cung ten trong Plugin\):
#   "18.0"  : Tekla 18.x va 2019   (build voi DLL API cua Tekla 18.0)
#   "2020+" : Tekla 2020 tro di    (build voi DLL API cua Tekla 2020 - ban 2020+ thap nhat tren may)

function Normalize-TeklaVersion([string]$v) {
  if (-not $v) { return $null }
  $v = $v.Trim()
  if ($v -notmatch '\.') { $v = "$v.0" }
  return $v
}

function Get-TeklaMajor([string]$v) {
  $m = [regex]::Match($v, '^\d+')
  if ($m.Success) { return [int]$m.Value } else { return 0 }
}

function Get-TeklaTarget([int]$major) {
  if ($major -ge 2020) { return '2020+' } else { return '18.0' }
}

# Tra ve doi tuong { Ver, Major, Target, Root, Api, Plugins } hoac $null neu khong co / thieu DLL API.
function Get-TeklaInfo([string]$ver) {
  $ver = Normalize-TeklaVersion $ver
  if (-not $ver) { return $null }
  $roots = @()
  $envRoot = [Environment]::GetEnvironmentVariable("TEKLA_ROOT_$ver")
  if ($envRoot) { $roots += $envRoot }
  $roots += (Join-Path 'C:\TeklaStructures' $ver)
  $roots += (Join-Path (Join-Path $env:ProgramFiles 'Tekla Structures') $ver)
  foreach ($r in $roots) {
    if (-not (Test-Path -LiteralPath $r)) { continue }
    $api = $null; $plg = $null
    foreach ($a in (Join-Path $r 'nt\bin\plugins'), (Join-Path $r 'bin')) {
      if (Test-Path -LiteralPath (Join-Path $a 'Tekla.Structures.Model.dll')) { $api = $a; break }
    }
    foreach ($p in (Join-Path $r 'nt\bin\plugins'), (Join-Path $r 'bin\plugins')) {
      if (Test-Path -LiteralPath $p) { $plg = $p; break }
    }
    # thu muc con sot lai sau khi go cai dat (khong co DLL API) -> coi nhu khong co
    if (-not $api -or -not $plg) { continue }
    $major = Get-TeklaMajor $ver
    return [pscustomobject]@{ Ver = $ver; Major = $major; Target = (Get-TeklaTarget $major); Root = $r; Api = $api; Plugins = $plg }
  }
  return $null
}

# Moi ban Tekla cai tren may (sap xep theo so ban).
function Get-TeklaVersions {
  $names = @()
  foreach ($base in 'C:\TeklaStructures', (Join-Path $env:ProgramFiles 'Tekla Structures')) {
    if (Test-Path -LiteralPath $base) { $names += (Get-ChildItem -LiteralPath $base -Directory | ForEach-Object { $_.Name }) }
  }
  $seen = @{}
  $list = @()
  foreach ($n in $names) {
    if ($seen.ContainsKey($n)) { continue }
    $seen[$n] = $true
    $i = Get-TeklaInfo $n
    if ($i) { $list += $i }
  }
  return @($list | Sort-Object Major, Ver)
}

# Thu muc DLL API dung de BUILD cho 1 bo ("18.0" / "2020+").
#   18.0  : Tekla 18.x (hoac bien UEL_API_18)
#   2020+ : ban 2020+ thap nhat tren may (hoac bien UEL_API_2020)
function Get-BuildApi([string]$target) {
  $envName = if ($target -eq '18.0') { 'UEL_API_18' } else { 'UEL_API_2020' }
  $e = [Environment]::GetEnvironmentVariable($envName)
  if ($e -and (Test-Path -LiteralPath (Join-Path $e 'Tekla.Structures.Model.dll'))) {
    return [pscustomobject]@{ Ver = "($envName)"; Api = $e }
  }
  $all = Get-TeklaVersions
  if ($target -eq '18.0') { $c = $all | Where-Object { $_.Major -eq 18 } | Select-Object -First 1 }
  else { $c = $all | Where-Object { $_.Major -ge 2020 } | Select-Object -First 1 }
  if ($c) { return [pscustomobject]@{ Ver = $c.Ver; Api = $c.Api } }
  return $null
}
