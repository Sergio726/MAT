# Ejecuta verificación estática, build y tests NUnit (unit + opcional integración SQL).
param(
    [switch]$Integration,
    [switch]$SkipVerify
)

$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
Set-Location $root

function Find-Tool([string[]]$Candidates) {
    foreach ($path in $Candidates) {
        if ($path -and (Test-Path $path)) { return $path }
    }
    return $null
}

$msbuild = Find-Tool @(
    "${env:ProgramFiles}\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
    "${env:ProgramFiles}\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe",
    "${env:ProgramFiles}\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
)
if (-not $msbuild) { throw "MSBuild no encontrado." }

$vstest = Find-Tool @(
    "${env:ProgramFiles}\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe",
    "${env:ProgramFiles}\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe",
    "${env:ProgramFiles}\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
)
if (-not $vstest) { throw "vstest.console.exe no encontrado." }

$nuget = Find-Tool @(
    "$root\tools\nuget.exe",
    "$root\.nuget\nuget.exe",
    "${env:ProgramFiles(x86)}\NuGet\nuget.exe",
    (Get-Command nuget.exe -ErrorAction SilentlyContinue | Select-Object -ExpandProperty Source)
)

$nunitAdapter = Join-Path $root "packages\NUnit3TestAdapter.4.6.0\build\net462"
if (-not (Test-Path $nunitAdapter)) {
    if (-not $nuget) { throw "NuGet no encontrado y faltan paquetes NUnit en packages\" }
    Write-Host "Restaurando paquetes NuGet..." -ForegroundColor Cyan
    & $nuget restore "$root\MAT.sln" | Out-Host
    if (-not (Test-Path $nunitAdapter)) { throw "NUnit3TestAdapter no restaurado en $nunitAdapter" }
}

if (-not $SkipVerify) {
    Write-Host "`n=== Verificación migración NetTiers ===" -ForegroundColor Cyan
    & "$PSScriptRoot\Verify-NetTiersMigration.ps1"
    if ($LASTEXITCODE -ne 0) { throw "Verify-NetTiersMigration.ps1 falló (exit $LASTEXITCODE)" }
}

Write-Host "`n=== Build MAT.sln Debug ===" -ForegroundColor Cyan
& $msbuild "$root\MAT.sln" /t:Build /p:Configuration=Debug /v:minimal /nologo
if ($LASTEXITCODE -ne 0) { throw "MSBuild falló (exit $LASTEXITCODE)" }

Write-Host "`n=== Tests unitarios (MAT.MVC.Tests) ===" -ForegroundColor Cyan
$unitDll = Join-Path $root "MAT.MVC.Tests\bin\Debug\MAT.MVC.Tests.dll"
if (-not (Test-Path $unitDll)) { throw "No se encontró $unitDll" }
& $vstest $unitDll "/TestAdapterPath:$nunitAdapter" /Logger:console
if ($LASTEXITCODE -ne 0) { throw "Tests unitarios fallaron (exit $LASTEXITCODE)" }

if ($Integration) {
    if ([string]::IsNullOrWhiteSpace($env:MAT_TEST_CONNECTION_STRING)) {
        throw "Parámetro -Integration requiere MAT_TEST_CONNECTION_STRING definida."
    }
    Write-Host "`n=== Tests integración (MAT.Integration.Tests) ===" -ForegroundColor Cyan
    $integDll = Join-Path $root "MAT.Integration.Tests\bin\Debug\MAT.Integration.Tests.dll"
    if (-not (Test-Path $integDll)) { throw "No se encontró $integDll" }
    & $vstest $integDll "/TestAdapterPath:$nunitAdapter" /Logger:console
    if ($LASTEXITCODE -ne 0) { throw "Tests integración fallaron (exit $LASTEXITCODE)" }
}

Write-Host "`n[ OK ] Todos los tests completados." -ForegroundColor Green
