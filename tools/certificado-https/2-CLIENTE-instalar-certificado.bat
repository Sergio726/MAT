@echo off
setlocal
title MAT - Instalar certificado del servidor en esta PC
cd /d "%~dp0"
echo.
echo ================================================================
echo   MAT - Instalar el certificado del servidor en esta PC
echo ================================================================
echo.
if not exist "%~dp0MAT-servidor.cer" (
  echo Falta el archivo MAT-servidor.cer en esta carpeta.
  echo Copialo desde el servidor (lo genera 1-SERVIDOR-crear-certificado.bat).
  echo.
  pause
  exit /b 1
)
net session >nul 2>&1
if errorlevel 1 (
  echo   1 = Para todos los usuarios de esta PC  ^(pide permisos de administrador^)
  echo   2 = Solo para mi usuario                 ^(Windows pide confirmar^)
  echo.
  choice /c 12 /n /m "Elegi 1 o 2: "
  if errorlevel 2 goto usuario
  powershell -NoProfile -Command "Start-Process -FilePath '%~f0' -Verb RunAs"
  exit /b
)
:usuario
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0cliente.ps1"
echo.
pause