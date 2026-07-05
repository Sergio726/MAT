# NetTiers F11 — inventario de propiedades por entidad
$ErrorActionPreference = 'Stop'
$root = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
if (-not (Test-Path (Join-Path $root 'MAT.sln'))) { $root = Split-Path $PSScriptRoot -Parent }

$entitiesDir = Join-Path $root 'MAT.Entities'
$searchDirs = @(
    (Join-Path $root 'MAT.MVC'),
    (Join-Path $root 'MAT.Utilities'),
    (Join-Path $root 'MAT.MVC.Tests')
)

$generated = Get-ChildItem -Path $entitiesDir -Recurse -Filter '*Base.generated.cs'
$rows = @()

foreach ($file in $generated) {
    $entityName = [System.IO.Path]::GetFileName($file.Name) -replace 'Base\.generated\.cs$',''
    $content = Get-Content $file.FullName -Raw

    $props = @()
    if ($content -match '(?s)internal protected class \w+EntityData.*?#region Primary key\(s\)(.*?)#endregion') {
        $block = $matches[0]
        $fieldMatches = [regex]::Matches($block, 'public System\.(\S+)\s+(\w+)\s*[;=]')
        foreach ($m in $fieldMatches) {
            $name = $m.Groups[2].Value
            if ($name -notmatch '^Original') { $props += $name }
        }
    }
    elseif ($content -match '#region Variable Declarations') {
        $fieldMatches = [regex]::Matches($content, 'private System\.(\S+)\s+_(\w+)\s*=')
        foreach ($m in $fieldMatches) {
            $raw = $m.Groups[2].Value
            $pascal = (Get-Culture).TextInfo.ToTitleCase($raw) -replace 'Id$','Id' -replace 'id$','Id'
            if ($raw -match 'Id$') { $pascal = $raw.Substring(0,1).ToUpper() + $raw.Substring(1) }
            else { $pascal = $raw.Substring(0,1).ToUpper() + $raw.Substring(1) }
            $props += $pascal
        }
    }

    $usedProps = @()
    foreach ($dir in $searchDirs) {
        if (-not (Test-Path $dir)) { continue }
        foreach ($prop in $props) {
            $pattern = "\.$prop\b"
            $hits = Select-String -Path (Join-Path $dir '**\*.cs') -Pattern $pattern -SimpleMatch:$false -ErrorAction SilentlyContinue
            if ($hits) { $usedProps += $prop }
        }
    }
    $usedProps = $usedProps | Select-Object -Unique

    $rows += [PSCustomObject]@{
        Entity = $entityName
        GeneratedFile = $file.FullName.Replace($root + '\', '')
        PropertyCount = $props.Count
        Properties = ($props -join ';')
        UsedInApp = ($usedProps -join ';')
    }
}

$outPath = Join-Path $root 'DOCUMENTACION\F11_ENTITY_INVENTORY.csv'
$rows | Export-Csv -Path $outPath -NoTypeInformation -Encoding UTF8
Write-Host "Inventario: $($rows.Count) entidades -> $outPath"
