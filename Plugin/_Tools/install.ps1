# Cai plugin UEL vao Tekla Structures. Goi qua install.bat.
#
#   install.bat <ban> [ten component]          ban phat hanh
#   install.bat dev <ban> [ten component]      ban phat trien (<Ten>_DEV.dll, Core nap lai moi lan chay)
#     <ban>         : 18 | 2019 | 2020 | 2021 | 2025 | all   (bo trong = 18.0)
#     ten component : vd. RAKER+T_GIRDER  (bo trong = moi component)
#
# Tekla 18.0, 2019 lay DLL trong thu muc 18.0\ ; Tekla 2020 tro di lay trong 2020+\
# - Tekla 2019 tro di cai trong C:\Program Files -> chay bang quyen Administrator.
# - DLL dang duoc Tekla nap thi bi khoa: tat ban Tekla do truoc.
#   (DLL chua tung co trong thu muc thi van chep duoc khi Tekla mo; chi can khoi dong lai Tekla.)
# - Ban DEV chi can cai lai khi THEM component hoac DOI hop thoai/thuoc tinh.
#   Sua hinh hoc thi chi can "build.bat core" roi bam Modify trong Tekla.
$ErrorActionPreference = 'Stop'
$Root = Split-Path -Parent $PSScriptRoot
. (Join-Path $PSScriptRoot 'tekla.ps1')

$Kind = 'release'; $Target = '18.0'; $Only = $null
$rest = @($args | ForEach-Object { "$_".Trim() } | Where-Object { $_ -ne '' })
if ($rest.Count -gt 0 -and $rest[0] -ieq 'dev') { $Kind = 'dev'; $rest = @($rest | Select-Object -Skip 1) }
if ($rest.Count -gt 0) { $Target = $rest[0] }
if ($rest.Count -gt 1) { $Only = $rest[1] }

function Get-Dlls([string]$bo) {
  if ($Kind -eq 'dev') { $dir = Join-Path $Root "_Dev\$bo"; $pat = '*_DEV.dll' }
  else { $dir = Join-Path $Root $bo; $pat = '*.dll' }
  if (-not (Test-Path -LiteralPath $dir)) { return @() }
  $files = @(Get-ChildItem -LiteralPath $dir -Filter $pat | Where-Object { $_.Name -notlike '*.Core.dll' })
  if ($Kind -ne 'dev') { $files = @($files | Where-Object { $_.Name -notlike '*_DEV.dll' }) }
  if ($Only) {
    $want = if ($Kind -eq 'dev') { "$Only`_DEV.dll" } else { "$Only.dll" }
    $files = @($files | Where-Object { $_.Name -ieq $want })
  }
  return $files
}

$okCount = 0; $failCount = 0

# Thu muc anh hop thoai cua Tekla = phan tu dau cua DAK_BMPPATH trong teklastructures.ini
#   18.0: %XS_DIR%\nt\bitmaps\   2019-2021: %XSDATADIR%\bitmaps\ (C:\ProgramData\Trimble\Tekla Structures\<ban>)
#   2025: %XSDATADIR%\bitmaps\;%XSBIN%Env\bitmaps\
function Get-BitmapDir($info) {
  $dataDir = Join-Path $env:ProgramData ("Trimble\Tekla Structures\" + $info.Ver)
  foreach ($bin in (Join-Path $info.Root 'nt\bin'), (Join-Path $info.Root 'bin')) {
    $ini = Join-Path $bin 'teklastructures.ini'
    if (-not (Test-Path -LiteralPath $ini)) { continue }
    $line = Select-String -LiteralPath $ini -Pattern '^\s*set\s+DAK_BMPPATH\s*=\s*(.+)$' | Select-Object -First 1
    if (-not $line) { continue }
    $first = ($line.Matches[0].Groups[1].Value -split ';')[0].Trim()
    $first = $first.Replace('%XS_DIR%', $info.Root).Replace('%XSDATADIR%', $dataDir).Replace('%XSBIN%', $bin + '\')
    if ($first -notmatch '%') { return $first.TrimEnd('\') }
  }
  if ($info.Major -lt 2019) { return (Join-Path $info.Root 'nt\bitmaps') }
  return (Join-Path $dataDir 'Bitmaps')
}

# Anh hop thoai cua component: Plugin\<Ten>\bitmaps\*.bmp (hinh minh hoa, ve boi <Ten>\_hinh\VeHinh.cs)
function Install-Bitmaps($info) {
  $comps = @(Get-ChildItem -LiteralPath $Root -Directory | Where-Object { -not $_.Name.StartsWith('_') -and (Test-Path -LiteralPath (Join-Path $_.FullName 'Plugin.cs')) })
  if ($Only) { $comps = @($comps | Where-Object { $_.Name -ieq $Only }) }
  $files = @()
  foreach ($c in $comps) {
    $bd = Join-Path $c.FullName 'bitmaps'
    if (Test-Path -LiteralPath $bd) { $files += Get-ChildItem -LiteralPath $bd -File | Where-Object { $_.Extension -match '^\.(bmp|xbm|png)$' } }
  }
  # bo cai mang sang may khac (pack.bat): anh nam chung o <bo cai>\bitmaps\
  $packed = Join-Path $Root 'bitmaps'
  if ($files.Count -eq 0 -and (Test-Path -LiteralPath $packed)) { $files += Get-ChildItem -LiteralPath $packed -File }
  if ($files.Count -eq 0) { return }
  $dest = Get-BitmapDir $info
  try {
    if (-not (Test-Path -LiteralPath $dest)) { New-Item -ItemType Directory -Force $dest | Out-Null }
    foreach ($f in $files) { Copy-Item -LiteralPath $f.FullName -Destination (Join-Path $dest $f.Name) -Force }
    Write-Host "   Da chep $($files.Count) anh hop thoai vao $dest"
  } catch {
    Write-Host "   Anh hop thoai: CHEP THAT BAI vao $dest - chay bang quyen Administrator. ($($_.Exception.Message))"
    $script:failCount++
  }
}

function Install-One($info) {
  Write-Host "[$($info.Ver)] $($info.Plugins)   (bo $($info.Target))"
  $dlls = @(Get-Dlls $info.Target)
  if ($dlls.Count -eq 0) {
    $what = if ($Only) { $Only } else { 'component nao' }
    Write-Host "   Khong co DLL $Kind cua $what trong bo $($info.Target). Chay build.bat truoc."
    $script:failCount++
    return
  }
  $running = $false
  try {
    $running = [bool](Get-Process TeklaStructures -ErrorAction SilentlyContinue | Where-Object { $_.Path -and $_.Path.StartsWith($info.Root + '\', 'OrdinalIgnoreCase') })
  } catch { }
  foreach ($old in 'UEL_PLUGINS.dll', 'UEL_PLUGINS_DEV.dll') {
    if (Test-Path -LiteralPath (Join-Path $info.Plugins $old)) {
      Write-Host "   (Con file cu $old cua bo khung truoc - component ten RAKER_TG_PLUGIN. Xoa khi khong dung nua.)"
    }
  }
  foreach ($d in $dlls) {
    $dest = Join-Path $info.Plugins $d.Name
    if ($running -and (Test-Path -LiteralPath $dest)) {
      Write-Host "   $($d.Name): Tekla $($info.Ver) dang mo va dang dung file nay. Tat Tekla $($info.Ver) roi chay lai."
      $script:failCount++
      continue
    }
    try {
      Copy-Item -LiteralPath $d.FullName -Destination $dest -Force
      Write-Host "   Da cai $($d.Name)"
      $script:okCount++
    } catch {
      Write-Host "   $($d.Name): CHEP THAT BAI - chay install.bat bang quyen Administrator. ($($_.Exception.Message))"
      $script:failCount++
    }
  }
  Install-Bitmaps $info
  if ($running) { Write-Host "   Tekla $($info.Ver) dang mo: khoi dong lai Tekla de thay plugin moi." }
}

if ($Target -ieq 'all') {
  $vs = @(Get-TeklaVersions)
  if ($vs.Count -eq 0) { Write-Host 'Khong tim thay ban Tekla nao.'; $failCount++ }
  foreach ($v in $vs) { Install-One $v }
} else {
  $info = Get-TeklaInfo $Target
  if (-not $info) { Write-Host "[$Target] Khong tim thay Tekla $Target (hoac thieu DLL API)."; $failCount++ }
  else { Install-One $info }
}

Write-Host ''
Write-Host "Thanh cong: $okCount   That bai: $failCount"
if ($Kind -eq 'dev') { Write-Host "Ban DEV nap Core tu: $Root\_Dev\<bo>\<Ten>.Core.dll  (khong duoc xoa/di chuyen thu muc nay)" }
Write-Host 'Mo Tekla, vao Component catalog, tim ten component (vd. RAKER+T_GIRDER hoac RAKER+T_GIRDER_DEV).'
if ($failCount -gt 0) { exit 1 } else { exit 0 }
