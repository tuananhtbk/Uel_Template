# Build bo plugin UEL (.NET Framework 4.0, khong can Visual Studio). Goi qua build.bat.
#
#   build.bat [che do] [ten component] [bo]
#     che do : all (mac dinh) | core | dev | release | tester | check
#     ten component : ten thu muc, vd. RAKER+T_GIRDER  (bo trong = moi component)
#     bo     : 18 | 2020        (bo trong = ca 2 bo)
#
#   release : 18.0\<Ten>.dll   (build voi API Tekla 18.0  -> dung cho Tekla 18.0, 2019)
#             2020+\<Ten>.dll  (build voi API Tekla 2020  -> dung cho Tekla 2020, 2021, 2025...)
#   dev     : _Dev\<bo>\<Ten>_DEV.dll (cai vao Tekla 1 lan) + _Dev\<bo>\<Ten>.Core.dll (nap lai moi lan chay)
#   core    : CHI build _Dev\<bo>\<Ten>.Core.dll  -> vong lap sua-thu nhanh, Tekla/EXE van mo
#   tester  : _Dev\UEL_TESTER.exe (EXE thu nghiem chung, tu chon bo theo ban Tekla dang mo)
#   check   : build tat ca roi kiem tra voi MOI ban Tekla tren may
#
# Component = moi thu muc trong Plugin\ (khong bat dau bang "_") co file Plugin.cs.
#   Plugin*.cs        -> vo plugin Tekla (hop thoai)
#   cac file .cs khac -> Core (thong so, hinh hoc)
$ErrorActionPreference = 'Stop'
$Root = Split-Path -Parent $PSScriptRoot
. (Join-Path $PSScriptRoot 'tekla.ps1')

# ---------------------------------------------------------------- tham so
$Mode = 'all'; $Only = $null; $Targets = @('18.0', '2020+')
$i = 0
foreach ($a in $args) {
  $s = "$a".Trim()
  if ($s -eq '') { continue }
  if ($i -eq 0 -and @('all','core','dev','release','tester','check') -contains $s.ToLower()) { $Mode = $s.ToLower() }
  elseif (@('18','18.0') -contains $s) { $Targets = @('18.0') }
  elseif (@('2020','2020.0','2020+') -contains $s) { $Targets = @('2020+') }
  else { $Only = $s }
  $i++
}

$csc = "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
if (-not (Test-Path $csc)) { $csc = "$env:WINDIR\Microsoft.NET\Framework\v4.0.30319\csc.exe" }
$Obj = Join-Path $Root '_obj'
$Dev = Join-Path $Root '_Dev'
$Common = Join-Path $Root '_Common'

# ---------------------------------------------------------------- component
function Get-Components {
  Get-ChildItem -LiteralPath $Root -Directory |
    Where-Object { -not $_.Name.StartsWith('_') -and (Test-Path -LiteralPath (Join-Path $_.FullName 'Plugin.cs')) } |
    Sort-Object Name
}
$comps = @(Get-Components)
if ($Only) {
  $comps = @($comps | Where-Object { $_.Name -ieq $Only })
  if ($comps.Count -eq 0) {
    Write-Host "Khong co component '$Only'. Cac component: $((Get-Components | ForEach-Object Name) -join ', ')"
    exit 1
  }
}

function Get-Sources($comp) {
  # bo qua thu muc con bat dau bang "_" (vd. _old) trong thu muc component
  $all = @(Get-ChildItem -LiteralPath $comp.FullName -Recurse -Filter *.cs | Where-Object { -not $_.FullName.Substring($comp.FullName.Length).Contains('\_') })
  [pscustomobject]@{
    Core  = @($all | Where-Object { -not $_.Name.StartsWith('Plugin', 'OrdinalIgnoreCase') } | ForEach-Object FullName)
    Shell = @($all | Where-Object { $_.Name.StartsWith('Plugin', 'OrdinalIgnoreCase') } | ForEach-Object FullName)
  }
}
function Get-CommonFiles([string]$sub) {
  @(Get-ChildItem -LiteralPath (Join-Path $Common $sub) -Filter *.cs | ForEach-Object FullName)
}

# ---------------------------------------------------------------- csc
# Goi csc qua file .rsp (duong dan co dau cach, dau + ... khong bi loi).
function Invoke-Csc([string]$label, [string]$out, [string]$kind, [string]$api, [string[]]$files, [string[]]$extra) {
  $dir = Split-Path -Parent $out
  if (-not (Test-Path -LiteralPath $dir)) { New-Item -ItemType Directory -Force $dir | Out-Null }
  $lines = @('/nologo', '/codepage:65001', "/target:$kind", '/optimize+', '/platform:anycpu', '/nowarn:1591', "/out:""$out""")
  foreach ($n in 'Tekla.Structures', 'Tekla.Structures.Model', 'Tekla.Structures.Plugins', 'Tekla.Structures.Datatype') {
    $p = Join-Path $api "$n.dll"
    if (Test-Path -LiteralPath $p) { $lines += "/reference:""$p""" }
  }
  if ($extra) { $lines += $extra }
  foreach ($f in $files) { $lines += """$f""" }
  $rsp = Join-Path $Obj ("csc_" + [guid]::NewGuid().ToString('N') + '.rsp')
  [IO.File]::WriteAllLines($rsp, $lines, (New-Object Text.UTF8Encoding($false)))
  Write-Host ("  {0,-9} {1}" -f $label, $out.Substring($Root.Length + 1))
  $o = & $csc "@$rsp" 2>&1
  $code = $LASTEXITCODE
  Remove-Item -LiteralPath $rsp -ErrorAction SilentlyContinue
  if ($code -ne 0) { $o | ForEach-Object { Write-Host "    $_" }; throw "csc loi ($label)" }
}

function Write-Gen([string]$path, [string]$text) {
  $d = Split-Path -Parent $path
  if (-not (Test-Path -LiteralPath $d)) { New-Item -ItemType Directory -Force $d | Out-Null }
  [IO.File]::WriteAllText($path, $text, (New-Object Text.UTF8Encoding($false)))
  return $path
}

New-Item -ItemType Directory -Force $Obj | Out-Null
$stamp = Get-Date -Format 'yyyy-MM-dd HH:mm:ss'
$buildInfo = Write-Gen (Join-Path $Obj 'BuildInfo.g.cs') "namespace UEL.Core { internal static class BuildInfo { internal const string Stamp = ""$stamp""; } }"

function Get-Gen($comp, [string]$target) {
  $g = Join-Path $Obj ("gen\" + $target + "\" + $comp.Name)
  $info = Write-Gen (Join-Path $g 'ComponentInfo.g.cs') ("namespace UEL { internal static class ComponentInfo { internal const string Name = """ + $comp.Name + """; } }")
  $core = Join-Path $Dev ("$target\" + $comp.Name + '.Core.dll')
  $devc = Write-Gen (Join-Path $g 'DevConfig.g.cs') ("namespace UEL.Host { internal static class DevConfig { internal const string CorePath = @""" + $core + """; } }")
  [pscustomobject]@{ Info = $info; Dev = $devc; CorePath = $core }
}

# Build 1 component cho 1 bo. $what: release / core / shell
function Build-Component($comp, [string]$target, [string]$api, [string[]]$what, [string]$outRoot) {
  $src = Get-Sources $comp
  $gen = Get-Gen $comp $target
  $coreFiles = @(Get-CommonFiles 'Core') + $src.Core + @($gen.Info, $buildInfo)
  $shellFiles = @(Get-CommonFiles 'Host') + @(Get-CommonFiles 'Plugin') + $src.Shell + @($gen.Info)
  if ($what -contains 'release') {
    Invoke-Csc 'release' (Join-Path $outRoot ("$target\" + $comp.Name + '.dll')) 'library' $api ($coreFiles + $shellFiles) @()
  }
  if ($what -contains 'core') {
    Invoke-Csc 'dev-core' $gen.CorePath 'library' $api $coreFiles @('/debug:pdbonly')
  }
  if ($what -contains 'shell') {
    Invoke-Csc 'dev' (Join-Path $Dev ("$target\" + $comp.Name + '_DEV.dll')) 'library' $api ($shellFiles + @($gen.Dev)) @('/define:UEL_DEV')
  }
}

function Build-Tester([string]$api) {
  $files = @(Get-CommonFiles 'Host') + @(Get-ChildItem -LiteralPath (Join-Path $Root '_Tester') -Filter *.cs | ForEach-Object FullName)
  Invoke-Csc 'tester' (Join-Path $Dev 'UEL_TESTER.exe') 'winexe' $api $files @('/reference:System.Windows.Forms.dll', '/reference:System.Drawing.dll')
}

# ---------------------------------------------------------------- chay
$what = switch ($Mode) {
  'core'    { @('core') }
  'dev'     { @('core', 'shell') }
  'release' { @('release') }
  'tester'  { @('core') }
  default   { @('release', 'core', 'shell') }
}
$ok = $true
try {
  foreach ($t in $Targets) {
    $b = Get-BuildApi $t
    if (-not $b) { Write-Host "[$t] BO QUA: khong tim thay DLL API Tekla cho bo $t (dat bien UEL_API_$(if ($t -eq '18.0') {'18'} else {'2020'}))"; continue }
    Write-Host "[$t] build voi DLL API Tekla $($b.Ver): $($b.Api)"
    foreach ($c in $comps) {
      Write-Host " $($c.Name)"
      Build-Component $c $t $b.Api $what $Root
    }
  }
  if (@('all', 'tester', 'check') -contains $Mode) {
    $b = Get-BuildApi '18.0'
    if (-not $b) { $b = Get-BuildApi '2020+' }
    Write-Host "[EXE] build voi DLL API Tekla $($b.Ver)"
    Build-Tester $b.Api
  }
  if ($Mode -eq 'check') {
    Write-Host ''
    Write-Host '[Check] kiem tra voi cac ban Tekla tren may'
    . (Join-Path $PSScriptRoot 'check.ps1')
    if (-not (Invoke-Check)) { $ok = $false }
  }
} catch {
  Write-Host $_.Exception.Message
  $ok = $false
}
if ($ok) { Write-Host 'Xong.'; exit 0 } else { Write-Host 'BUILD THAT BAI'; exit 1 }
