<#
  MAT - Instala el certificado del servidor en ESTA PC, para que el navegador no muestre "no es seguro".
  Lo ejecuta 2-CLIENTE-instalar-certificado.bat. Necesita el archivo MAT-servidor.cer en la misma carpeta
  (lo genera 1-SERVIDOR-crear-certificado.bat en el servidor).

  - Con permisos de administrador: lo instala para todos los usuarios de la PC (LocalMachine\Root).
  - Sin permisos: lo instala solo para el usuario actual (CurrentUser\Root); Windows pide confirmar.
  Solo se instala la parte pública del certificado (no permite firmar nada en esta PC).
#>
$ErrorActionPreference = 'Stop'
$carpeta = Split-Path -Parent $MyInvocation.MyCommand.Path
$cer = Join-Path $carpeta 'MAT-servidor.cer'

Write-Host ''
Write-Host '=== MAT - instalar certificado del servidor en esta PC ===' -ForegroundColor Cyan
if (-not (Test-Path $cer)) {
    Write-Host "No encuentro $cer. Copiá el MAT-servidor.cer que generó el servidor a esta carpeta." -ForegroundColor Red
    exit 1
}

$info = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2 $cer
Write-Host "Certificado: $($info.Subject)  ·  vence $($info.NotAfter.ToString('dd/MM/yyyy'))"
$nombres = ($info.Extensions | Where-Object { $_.Oid.Value -eq '2.5.29.17' } | ForEach-Object { $_.Format($false) }) -join ''
if ($nombres) { Write-Host "Válido para: $nombres" }

$esAdmin = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
$almacen = if ($esAdmin) { 'Cert:\LocalMachine\Root' } else { 'Cert:\CurrentUser\Root' }
Import-Certificate -FilePath $cer -CertStoreLocation $almacen | Out-Null

Write-Host ''
Write-Host "Instalado en $almacen." -ForegroundColor Green
Write-Host 'Cerrá TODAS las ventanas del navegador y volvé a abrir MAT, entrando con uno de los nombres de arriba.'
Write-Host 'Firefox: si sigue el aviso, en about:config poné security.enterprise_roots.enabled = true.' -ForegroundColor DarkGray
