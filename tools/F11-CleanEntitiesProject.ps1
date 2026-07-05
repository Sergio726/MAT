# NetTiers F11 — limpia MAT.Entities.csproj y elimina infra NetTiers
$ErrorActionPreference = 'Stop'
$root = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
if (-not (Test-Path (Join-Path $root 'MAT.sln'))) { $root = Split-Path $PSScriptRoot -Parent }

$entitiesDir = Join-Path $root 'MAT.Entities'
$csproj = Join-Path $entitiesDir 'MAT.Entities.csproj'

$removePatterns = @(
    '*Base.generated.cs',
    'EntityBaseCore.generated.cs',
    'EntityKeyBaseCore.generated.cs',
    'EntityBase.cs',
    'EntityKeyBase.cs',
    'TList.cs',
    'ListBase.cs',
    'EntityFactory*.cs',
    'EntityLocator.cs',
    'EntityCache.cs',
    'EntityUtil.cs',
    'EntityFilter.cs',
    'EntityPropertyComparer.cs',
    'WeakRefDictionary.cs',
    'IEntity*.cs',
    'ICloneableEx.cs',
    'I*.cs',
    'Validation\*'
)

# Delete infra files
$toDelete = @(
    'EntityBaseCore.generated.cs','EntityKeyBaseCore.generated.cs','EntityBase.cs','EntityKeyBase.cs',
    'TList.cs','ListBase.cs','EntityFactory.cs','EntityFactoryBase.cs','IEntityFactory.cs',
    'EntityLocator.cs','EntityCache.cs','EntityUtil.cs','EntityFilter.cs','EntityPropertyComparer.cs',
    'WeakRefDictionary.cs','IEntity.cs','IEntityId.cs','IEntityKey.cs','IEntityCacheItem.cs','ICloneableEx.cs'
)
foreach ($f in $toDelete) {
    $p = Join-Path $entitiesDir $f
    if (Test-Path $p) { Remove-Item $p -Force; Write-Host "Deleted $f" }
}
Get-ChildItem (Join-Path $entitiesDir 'Validation') -ErrorAction SilentlyContinue | Remove-Item -Force -Recurse
Remove-Item (Join-Path $entitiesDir 'Validation') -Force -Recurse -ErrorAction SilentlyContinue
Get-ChildItem $entitiesDir -Filter 'I*.cs' -Recurse | Remove-Item -Force
Remove-Item (Join-Path $entitiesDir 'Views\VList.cs') -Force -ErrorAction SilentlyContinue
Get-ChildItem $entitiesDir -Recurse -Filter '*Base.generated.cs' | Remove-Item -Force -ErrorAction SilentlyContinue

# Collect remaining .cs files
$csFiles = Get-ChildItem $entitiesDir -Recurse -Filter '*.cs' |
    Where-Object { $_.FullName -notmatch '\\obj\\|\\bin\\' } |
    Sort-Object FullName

$relPaths = $csFiles | ForEach-Object {
    $_.FullName.Substring($entitiesDir.Length + 1).Replace('\', '\')
}

[xml]$proj = Get-Content $csproj
$compileNodes = $proj.Project.ItemGroup | Where-Object { $_.Compile } | Select-Object -First 1
if (-not $compileNodes) {
    $compileNodes = $proj.CreateElement('ItemGroup', $proj.Project.NamespaceURI)
    $proj.Project.AppendChild($compileNodes) | Out-Null
}
$compileNodes.RemoveAll()
foreach ($rp in $relPaths) {
    $el = $proj.CreateElement('Compile', $proj.Project.NamespaceURI)
    $el.SetAttribute('Include', $rp)
    $compileNodes.AppendChild($el) | Out-Null
}
$proj.Save($csproj)
Write-Host "csproj actualizado: $($relPaths.Count) archivos"
