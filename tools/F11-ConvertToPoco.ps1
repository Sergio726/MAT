# NetTiers F11 — convierte *Base.generated.cs a POCO plano
param(
    [string[]]$EntityNames = @()
)
$ErrorActionPreference = 'Stop'
$root = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
if (-not (Test-Path (Join-Path $root 'MAT.sln'))) { $root = Split-Path $PSScriptRoot -Parent }

function Get-CsTypeName([string]$netType) {
    $t = $netType.TrimEnd('?')
    switch ($t) {
        'Guid' { return 'Guid' }
        'String' { return 'string' }
        'Int32' { return 'int' }
        'Int16' { return 'short' }
        'Int64' { return 'long' }
        'Double' { return 'double' }
        'Single' { return 'float' }
        'Boolean' { return 'bool' }
        'DateTime' { return 'DateTime' }
        'Byte' { return 'byte' }
        'Decimal' { return 'decimal' }
        default { return $t }
    }
}

function Get-EntityProperties([string]$content) {
    $result = @()
    if ($content -match '(?s)internal protected class \w+EntityData.*') {
        $block = $matches[0]
        $fieldMatches = [regex]::Matches($block, 'public System\.(\S+)\s+(\w+)\s*[;=]')
        foreach ($m in $fieldMatches) {
            $name = $m.Groups[2].Value
            if ($name -match '^Original|^currentEntity') { continue }
            $netType = $m.Groups[1].Value
            $nullable = $netType.EndsWith('?')
            $csType = Get-CsTypeName $netType
            if ($nullable -and -not $csType.EndsWith('?')) { $csType += '?' }
            $result += [PSCustomObject]@{ Name = $name; Type = $csType }
        }
    }
    else {
        $fieldMatches = [regex]::Matches($content, 'private System\.(\S+)\s+_(\w+)\s*=')
        foreach ($m in $fieldMatches) {
            $raw = $m.Groups[2].Value
            $name = $raw.Substring(0,1).ToUpper() + $raw.Substring(1)
            $netType = $m.Groups[1].Value
            $nullable = $netType.EndsWith('?')
            $csType = Get-CsTypeName $netType
            if ($nullable -and -not $csType.EndsWith('?')) { $csType += '?' }
            $result += [PSCustomObject]@{ Name = $name; Type = $csType }
        }
    }
    return $result
}

$entitiesDir = Join-Path $root 'MAT.Entities'
$generatedFiles = Get-ChildItem -Path $entitiesDir -Recurse -Filter '*Base.generated.cs'
$converted = 0

foreach ($genFile in $generatedFiles) {
    $entityName = [System.IO.Path]::GetFileName($genFile.Name) -replace 'Base\.generated\.cs$',''
    if ($EntityNames.Count -gt 0 -and ($EntityNames -notcontains $entityName)) { continue }

    $relDir = Split-Path $genFile.FullName -Parent
    $entityFile = Join-Path $relDir ($entityName + '.cs')
    if (-not (Test-Path $entityFile)) {
        $entityFile = Join-Path $relDir ($entityName + '.cs')
        New-Item -Path $entityFile -ItemType File -Force | Out-Null
    }

    $content = Get-Content $genFile.FullName -Raw
    $props = Get-EntityProperties $content
    if ($props.Count -eq 0) {
        Write-Warning "Sin propiedades: $entityName"
        continue
    }

    $sb = New-Object System.Text.StringBuilder
    [void]$sb.AppendLine('using System;')
    [void]$sb.AppendLine('')
    [void]$sb.AppendLine('namespace MAT.Entities')
    [void]$sb.AppendLine('{')
    [void]$sb.AppendLine("`t/// <summary>POCO manual (NetTiers F11). Tabla/vista: $entityName</summary>")
    [void]$sb.AppendLine('    [Serializable]')
    [void]$sb.AppendLine("    public class $entityName")
    [void]$sb.AppendLine('    {')
    foreach ($p in $props) {
        [void]$sb.AppendLine("`t`tpublic $($p.Type) $($p.Name) { get; set; }")
    }
    [void]$sb.AppendLine('    }')
    [void]$sb.AppendLine('}')

    Set-Content -Path $entityFile -Value $sb.ToString() -Encoding UTF8
    Remove-Item $genFile.FullName -Force
    $converted++
    Write-Host "OK $entityName ($($props.Count) props)"
}

Write-Host "Convertidos: $converted"
