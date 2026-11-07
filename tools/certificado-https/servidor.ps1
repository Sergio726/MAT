#Requires -RunAsAdministrator
<#
  MAT - Certificado HTTPS para el SERVIDOR donde corre la aplicación (IIS).
  Lo ejecuta 1-SERVIDOR-crear-certificado.bat (con permisos de administrador).

  Qué hace:
    1. Crea un certificado (5 años) válido para todos los nombres con los que se entra al servidor:
       nombre del equipo, nombre completo de red (FQDN), localhost, 127.0.0.1, sus IPv4 y los nombres extra
       que se le pasen (por ejemplo: -NombresExtra "mat.miempresa.com,intranet").
    2. Lo marca como de confianza en el propio servidor.
    3. Lo asigna a todos los enlaces HTTPS de IIS (o solo a los del sitio indicado con -Sitio).
    4. Exporta la parte pública a MAT-servidor.cer (junto a este script) para instalar en cada PC con
       2-CLIENTE-instalar-certificado.bat. Sin ese paso, las PCs siguen mostrando el aviso.
    5. Guarda en volver-atras.txt los comandos para volver a los certificados anteriores.
#>
param(
    [string]$NombresExtra = '',
    [string]$Sitio = ''
)
$ErrorActionPreference = 'Stop'
$carpeta = Split-Path -Parent $MyInvocation.MyCommand.Path
$appidIIS = '{4dc3e181-e14b-4a21-b022-59fc669b0914}'

Write-Host ''
Write-Host '=== MAT - certificado HTTPS del servidor ===' -ForegroundColor Cyan

# ---------------------------------------------------------------- Nombres del servidor
$dns = New-Object System.Collections.Generic.List[string]
$ips = New-Object System.Collections.Generic.List[string]
function Agregar-Dns($n) { $n = "$n".Trim().ToLower(); if ($n -and -not $dns.Contains($n)) { [void]$dns.Add($n) } }
function Agregar-Ip($n) { $n = "$n".Trim(); if ($n -and -not $ips.Contains($n)) { [void]$ips.Add($n) } }

Agregar-Dns $env:COMPUTERNAME
try { Agregar-Dns ([System.Net.Dns]::GetHostEntry($env:COMPUTERNAME).HostName) } catch { }
Agregar-Dns 'localhost'
Agregar-Ip '127.0.0.1'
Get-NetIPAddress -AddressFamily IPv4 -ErrorAction SilentlyContinue |
    Where-Object { $_.IPAddress -notlike '127.*' -and $_.IPAddress -notlike '169.254.*' } |
    ForEach-Object { Agregar-Ip $_.IPAddress }
foreach ($n in ($NombresExtra -split '[,; ]+')) {
    if (-not $n) { continue }
    if ($n -match '^\d{1,3}(\.\d{1,3}){3}$') { Agregar-Ip $n } else { Agregar-Dns $n }
}

Write-Host "Nombres: $($dns -join ', ')"
Write-Host "IPs:     $($ips -join ', ')"
Write-Host '(Los usuarios tienen que entrar con alguno de estos nombres o IPs. Si usan otro, ejecutar de nuevo con -NombresExtra.)' -ForegroundColor DarkGray

# ---------------------------------------------------------------- 1. Certificado
$san = (($dns | ForEach-Object { "DNS=$_" }) + ($ips | ForEach-Object { "IPAddress=$_" })) -join '&'
$cert = New-SelfSignedCertificate `
    -Subject "CN=$($dns[0])" `
    -TextExtension @("2.5.29.17={text}$san", '2.5.29.37={text}1.3.6.1.5.5.7.3.1') `
    -KeyAlgorithm RSA -KeyLength 2048 -HashAlgorithm SHA256 `
    -KeyUsage DigitalSignature, KeyEncipherment `
    -NotAfter (Get-Date).AddYears(5) `
    -FriendlyName "MAT servidor ($($dns[0]))" `
    -CertStoreLocation Cert:\LocalMachine\My
Write-Host "Certificado nuevo: $($cert.Thumbprint)  (vence $($cert.NotAfter.ToString('dd/MM/yyyy')))" -ForegroundColor Green

# ---------------------------------------------------------------- 2. Confianza en el propio servidor + 4. exportar .cer
$cer = Join-Path $carpeta 'MAT-servidor.cer'
Export-Certificate -Cert $cert -FilePath $cer -Force | Out-Null
Import-Certificate -FilePath $cer -CertStoreLocation Cert:\LocalMachine\Root | Out-Null
Write-Host "Parte pública exportada: $cer"

# ---------------------------------------------------------------- 3. Enlaces HTTPS de IIS
Import-Module WebAdministration
$sitios = Get-ChildItem IIS:\Sites
if ($Sitio) { $sitios = $sitios | Where-Object { $_.Name -eq $Sitio } }
$volver = New-Object System.Collections.Generic.List[string]
$cambiados = 0
foreach ($s in $sitios) {
    foreach ($b in ($s.Bindings.Collection | Where-Object { $_.protocol -eq 'https' })) {
        $partes = $b.bindingInformation -split ':'          # ip:puerto:host
        $ip = if ($partes[0] -eq '*' -or -not $partes[0]) { '0.0.0.0' } else { $partes[0] }
        $puerto = $partes[1]; $hostName = $partes[2]
        $sni = ([int]$b.sslFlags -band 1) -eq 1
        if ($sni -and $hostName) { $clave = "hostnameport=${hostName}:$puerto"; $mostrar = "${hostName}:$puerto (SNI)" }
        else { $clave = "ipport=${ip}:$puerto"; $mostrar = "${ip}:$puerto" }

        $actual = netsh http show sslcert $clave | Out-String
        $hashAnterior = [regex]::Match($actual, '(?m):\s*([0-9a-fA-F]{40})\s*$').Groups[1].Value
        $appid = [regex]::Match($actual, '\{[0-9a-fA-F-]{36}\}').Value
        if (-not $appid) { $appid = $appidIIS }

        if ($hashAnterior) {
            netsh http update sslcert $clave certhash=$($cert.Thumbprint) appid=$appid certstorename=MY | Out-Null
            [void]$volver.Add("netsh http update sslcert $clave certhash=$hashAnterior appid=$appid certstorename=MY")
        } else {
            netsh http add sslcert $clave certhash=$($cert.Thumbprint) appid=$appid certstorename=MY | Out-Null
            [void]$volver.Add("netsh http delete sslcert $clave")
        }
        Write-Host "  Sitio '$($s.Name)' $mostrar -> certificado nuevo" -ForegroundColor Green
        $cambiados++
    }
}
if ($cambiados -eq 0) {
    Write-Host 'No encontré enlaces HTTPS en IIS. Agregá un enlace https al sitio (Administrador de IIS > Enlaces) y elegí el certificado "MAT servidor".' -ForegroundColor Yellow
}

# ---------------------------------------------------------------- 5. Volver atrás
$archivo = Join-Path $carpeta 'volver-atras.txt'
$lineas = @('Para volver a los certificados anteriores, en PowerShell como administrador:', '') + $volver + @(
    '',
    "Y, si querés, borrar el certificado nuevo ($($cert.Thumbprint)) de LocalMachine\My y LocalMachine\Root.")
Set-Content -Path $archivo -Value $lineas -Encoding UTF8

Write-Host ''
Write-Host 'Listo en el servidor.' -ForegroundColor Cyan
Write-Host "Ahora copiá MAT-servidor.cer y 2-CLIENTE-instalar-certificado.bat a cada PC que usa MAT"
Write-Host '(o repartí el .cer por directiva de grupo) y cerrá y abrí el navegador.'
Write-Host "Para volver atrás: $archivo"
