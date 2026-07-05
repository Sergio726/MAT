# Hotfix B1 — reemplaza e.Message expuesto al usuario por ErrorUtil
$ErrorActionPreference = 'Stop'
$root = 'c:\Users\Sebastian\source\repos\MAT'
$files = @(
    'MAT.MVC\Controllers\PersonaCliente\PersonaClienteController.cs',
    'MAT.MVC\Controllers\NuevaReserva\NuevaReservaController.cs',
    'MAT.MVC\Controllers\Hotel\HotelController.cs',
    'MAT.MVC\Controllers\Habitacion\HabitacionController.cs',
    'MAT.MVC\Controllers\RevervaHabitacion\ReservaHabitacionController.cs',
    'MAT.MVC\Controllers\Precio\PrecioController.cs',
    'MAT.MVC\Controllers\NotaCredito\NotaCreditoController.cs',
    'MAT.MVC\Controllers\HotelHabitacionViaje\HotelHabitacionViajeController.cs',
    'MAT.MVC\Models\PaqueteModel.cs',
    'MAT.MVC\Models\ViajeModel.cs',
    'MAT.MVC\Models\ListaEsperaModel.cs',
    'MAT.MVC\Models\ItinerarioModel.cs',
    'MAT.MVC\Models\Habitacion.cs'
)

foreach ($rel in $files) {
    $path = Join-Path $root $rel
    if (-not (Test-Path $path)) { continue }
    $name = [System.IO.Path]::GetFileNameWithoutExtension($path)
    $ctx = $name
    $text = Get-Content $path -Raw -Encoding UTF8

    if ($text -notmatch 'using MAT\.MVC\.Infrastructure') {
        $text = $text -replace '(using MAT\.MVC\.Common;)', "`$1`r`nusing MAT.MVC.Infrastructure;"
        if ($text -notmatch 'using MAT\.MVC\.Infrastructure') {
            $text = $text -replace '(using System;)', "`$1`r`nusing MAT.MVC.Infrastructure;"
        }
    }

    $text = $text -replace 'ViewBag\.Error\s*=\s*"Error:\s*"\s*\+\s*e\.Message\s*;', "ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, `"$ctx`");"
    $text = $text -replace 'ViewBag\.Error\s*=\s*e\.Message\s*;', "ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, `"$ctx`");"
    $text = $text -replace 'ViewBag\.Error\s*=\s*ex\.Message\s*;', "ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(ex, `"$ctx`");"
    $text = $text -replace 'sResult\[1\]\s*=\s*e\.Message\s*;', "sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, `"$ctx`");"
    $text = $text -replace 'sResult\[1\]\s*=\s*ex\.Message\s*;', "sResult[1] = ErrorUtil.LogAndGetPublicMessage(ex, `"$ctx`");"

    Set-Content -Path $path -Value $text -Encoding UTF8 -NoNewline
    Write-Host "Fixed $name"
}
