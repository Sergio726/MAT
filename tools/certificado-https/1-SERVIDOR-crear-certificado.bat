@echo off
setlocal
title MAT - Certificado HTTPS del servidor
rem Pide permisos de administrador si no los tiene (se abre el aviso de Windows)
net session >nul 2>&1
if errorlevel 1 (
  echo Se necesitan permisos de administrador. Acepta el aviso de Windows que va a aparecer.
  powershell -NoProfile -Command "Start-Process -FilePath '%~f0' -Verb RunAs"
  exit /b
)
cd /d "%~dp0"
echo.
echo ================================================================
echo   MAT - Certificado HTTPS para el servidor (IIS)
echo ================================================================
echo.
echo Si los usuarios entran a MAT con un nombre o IP distinto del nombre
echo de este servidor (por ejemplo mat.miempresa.com o una IP publica),
echo escribilo ahora, separado por comas. Si no, solo presiona Enter.
echo.
set "EXTRA="
set /p EXTRA=Nombres extra: 
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0servidor.ps1" -NombresExtra "%EXTRA%"
echo.
pause