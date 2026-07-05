# NetTiers F11 — reescribe MAT.Entities.csproj con solo archivos .cs existentes
$ErrorActionPreference = 'Stop'
$root = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
if (-not (Test-Path (Join-Path $root 'MAT.sln'))) { $root = Split-Path $PSScriptRoot -Parent }

$entitiesDir = Join-Path $root 'MAT.Entities'
$csproj = Join-Path $entitiesDir 'MAT.Entities.csproj'

$csFiles = Get-ChildItem $entitiesDir -Recurse -Filter '*.cs' |
    Where-Object { $_.FullName -notmatch '\\obj\\|\\bin\\' } |
    Sort-Object FullName

$relPaths = $csFiles | ForEach-Object { $_.FullName.Substring($entitiesDir.Length + 1) }

[xml]$proj = Get-Content $csproj
$groupsToRemove = @()
foreach ($ig in $proj.Project.ItemGroup) {
    if ($ig.Compile) { $groupsToRemove += $ig }
}
foreach ($ig in $groupsToRemove) {
    [void]$proj.Project.RemoveChild($ig)
}

$newIg = $proj.CreateElement('ItemGroup', $proj.Project.NamespaceURI)
foreach ($rp in $relPaths) {
    $el = $proj.CreateElement('Compile', $proj.Project.NamespaceURI)
    $el.SetAttribute('Include', $rp)
    [void]$newIg.AppendChild($el)
}

$refGroup = $proj.Project.ItemGroup | Where-Object { $_.Reference } | Select-Object -First 1
[void]$proj.Project.InsertAfter($newIg, $refGroup)
$proj.Save($csproj)
Write-Host "csproj: $($relPaths.Count) Compile entries"
