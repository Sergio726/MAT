# Verificación post-migración NetTiers F9-F12 (estático, sin IIS)
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
$failures = @()
$warnings = @()

function Add-Fail($msg) { $script:failures += $msg; Write-Host "[FAIL] $msg" -ForegroundColor Red }
function Add-Warn($msg) { $script:warnings += $msg; Write-Host "[WARN] $msg" -ForegroundColor Yellow }
function Add-Ok($msg)   { Write-Host "[ OK ] $msg" -ForegroundColor Green }

Write-Host "`n=== NetTiers migration verification ===" -ForegroundColor Cyan

# 1. MSBuild
$msbuild = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
if (-not (Test-Path $msbuild)) { Add-Fail "MSBuild no encontrado" }
else {
    $build = & $msbuild "$root\MAT.sln" /t:Build /p:Configuration=Debug /v:minimal 2>&1
    if ($LASTEXITCODE -ne 0) { Add-Fail "MSBuild falló (exit $LASTEXITCODE)"; $build | Select-Object -Last 15 | ForEach-Object { Write-Host $_ } }
    else { Add-Ok "MSBuild MAT.sln Debug OK" }
}

# 2. Sin *.generated.cs en MAT.Entities
$generated = Get-ChildItem "$root\MAT.Entities" -Recurse -Filter '*.generated.cs' -ErrorAction SilentlyContinue
if ($generated.Count -gt 0) { Add-Fail "Quedan $($generated.Count) *.generated.cs en MAT.Entities" }
else { Add-Ok "Cero *.generated.cs en MAT.Entities" }

# 3. Carpetas legado fuera del repo
foreach ($d in @('MAT.Data','MAT.Data.SqlClient','MAT.Services','MAT.Web','MAT.WCF')) {
    if (Test-Path (Join-Path $root $d)) { Add-Fail "Carpeta legado presente: $d" }
    else { Add-Ok "Sin carpeta $d" }
}

# 4. Referencias NetTiers en código activo
$activeDirs = @("$root\MAT.MVC", "$root\MAT.Utilities", "$root\MAT.Entities")
$forbidden = @('using MAT\.Services', 'using MAT\.Data[^.]', 'SqlNetTiersProvider', 'DataRepository\.Provider', 'nettiers\.com')
foreach ($pat in $forbidden) {
    $hits = Select-String -Path ($activeDirs | ForEach-Object { Join-Path $_ '**\*.cs' }) -Pattern $pat -ErrorAction SilentlyContinue |
        Where-Object { $_.Line -notmatch '^\s*//' }
    if ($hits) {
        $sample = ($hits | Select-Object -First 3 | ForEach-Object { "$($_.Filename):$($_.LineNumber)" }) -join ', '
        Add-Fail "Patron '$pat' en codigo activo ($($hits.Count) hits). Ej: $sample"
    }
    else { Add-Ok "Sin '$pat' en MVC/Utilities/Entities" }
}

# 5. SPs referenciados en codigo vs MAT.DB
$spRefs = Select-String -Path @(
    "$root\MAT.MVC\**\*.cs",
    "$root\MAT.Utilities\**\*.cs"
) -Pattern 'dbo\.usp_MAT_[A-Za-z0-9_]+' -AllMatches -ErrorAction SilentlyContinue |
    ForEach-Object { $_.Matches } |
    ForEach-Object { $_.Value -replace '^dbo\.', '' } |
    Select-Object -Unique | Sort-Object

$dbSps = Get-ChildItem "$root\MAT.DB\dbo\Stored Procedures" -Filter 'usp_MAT_*.sql' |
    ForEach-Object { $_.BaseName }

$knownMissingSps = @('usp_MAT_Viaje_CancelViaje')
$missingSps = @()
foreach ($sp in $spRefs) {
    if ($dbSps -notcontains $sp) {
        if ($knownMissingSps -contains $sp) {
            Add-Warn "SP referenciado en codigo pero ausente en MAT.DB (deuda conocida): $sp"
        }
        else {
            $missingSps += $sp
        }
    }
}
if ($missingSps.Count -gt 0) {
    Add-Fail "SPs referenciados en codigo pero ausentes en MAT.DB: $($missingSps -join ', ')"
}
else { Add-Ok "Todos los SP referenciados ($($spRefs.Count)) existen en MAT.DB" }

# 6. SPs nuevos F9/F12 en SSDT
foreach ($sp in @(
    'usp_MAT_Proveedor_GetSelectList',
    'usp_MAT_PrecioServicio_GetActiveByServicioId',
    'usp_MAT_Viaje_GetSelectList',
    'usp_MAT_ReservaHabitacion_CountByHabitacionAndViaje'
)) {
    if ($dbSps -notcontains $sp) { Add-Fail "SP F9/F12 faltante en MAT.DB: $sp" }
    else { Add-Ok "SP en SSDT: $sp" }
}

# 7. Propiedades POCO vs DataAccess (muestra Factura, Persona, Pasaje)
function Test-EntityProperties([string]$entityName) {
    $pocoFile = Get-ChildItem "$root\MAT.Entities" -Recurse -Filter "$entityName.cs" | Select-Object -First 1
    if (-not $pocoFile) { Add-Warn "POCO no encontrado: $entityName"; return }
    $pocoProps = [regex]::Matches((Get-Content $pocoFile.FullName -Raw), 'public\s+\S+\s+(\w+)\s*\{') |
        ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique
    $daFiles = Get-ChildItem "$root\MAT.MVC\Infrastructure\Data" -Filter '*DataAccess.cs'
    $usedProps = @()
    foreach ($f in $daFiles) {
        $content = Get-Content $f.FullName -Raw
        if ($content -notmatch "\b$entityName\b") { continue }
        [regex]::Matches($content, "$entityName\s*\{[^}]*?(\w+)\s*=") | ForEach-Object { $usedProps += $_.Groups[1].Value }
        [regex]::Matches($content, "\w+\.$entityName\)\s*$entityName\s*\{[^}]*?(\w+)\s*=") | ForEach-Object { $usedProps += $_.Groups[1].Value }
        [regex]::Matches($content, "new\s+$entityName\s*\{([^}]+)\}") | ForEach-Object {
            [regex]::Matches($_.Groups[1].Value, '(\w+)\s*=') | ForEach-Object { $usedProps += $_.Groups[1].Value }
        }
    }
    $usedProps = $usedProps | Select-Object -Unique
    $missing = $usedProps | Where-Object { $pocoProps -notcontains $_ }
    if ($missing.Count -gt 0) {
        Add-Fail "$entityName`: propiedades en DataAccess ausentes en POCO: $($missing -join ', ')"
    }
    else { Add-Ok "$entityName`: $($usedProps.Count) props usadas en DataAccess OK" }
}

foreach ($e in @('Factura','Pago','Pasaje','Persona','Viaje','Proveedor')) {
    Test-EntityProperties $e
}

# 8. e.Message expuesto (regresion hotfix B1)
$msgHits = Select-String -Path "$root\MAT.MVC\**\*.cs" -Pattern 'ViewBag\.Error\s*=.*\.Message|sResult\[1\]\s*=.*\.Message' -ErrorAction SilentlyContinue |
    Where-Object { $_.Line -notmatch 'ErrorUtil' -and $_.Line -notmatch '^\s*//' }
if ($msgHits) {
    Add-Warn "Posible e.Message expuesto ($($msgHits.Count) lineas). Revisar hotfix B1."
    $msgHits | Select-Object -First 5 | ForEach-Object { Write-Host "       $($_.Path):$($_.LineNumber)" }
}
else { Add-Ok "Sin e.Message directo en ViewBag/sResult" }

# Resumen
Write-Host "`n=== Resumen ===" -ForegroundColor Cyan
Write-Host "Fallos: $($failures.Count) | Advertencias: $($warnings.Count)"
if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Host "  - $_" -ForegroundColor Red }
    exit 1
}
exit 0
