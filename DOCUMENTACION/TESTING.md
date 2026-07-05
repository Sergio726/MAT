# Testing — MAT

Guía para ejecutar tests unitarios, contrato post-NetTiers e integración SQL.

## Proyectos

| Proyecto | Tipo | Requiere BD |
|----------|------|-------------|
| `MAT.MVC.Tests` | Unitarios + contrato estático | No |
| `MAT.Integration.Tests` | Smoke SQL (SPs F9/F12, lookups) | Sí |

## Ejecución rápida (recomendada)

Desde la raíz del repo:

```powershell
# Unit + verificación estática (sin BD)
.\tools\Run-Tests.ps1

# Incluir integración SQL (requiere connection string)
$env:MAT_TEST_CONNECTION_STRING = "Data Source=...;Initial Catalog=MAT.Intranet;Integrated Security=True;"
.\tools\Run-Tests.ps1 -Integration

# Omitir Verify-NetTiersMigration.ps1
.\tools\Run-Tests.ps1 -SkipVerify
```

El script:

1. Restaura paquetes NuGet (`NUnit`, `NUnit3TestAdapter`) si faltan en `packages\`.
2. Compila `MAT.sln` Debug.
3. Ejecuta `tools\Verify-NetTiersMigration.ps1` (checks estáticos post-NetTiers).
4. Corre `vstest` sobre `MAT.MVC.Tests`.
5. Con `-Integration`, corre `MAT.Integration.Tests` si `MAT_TEST_CONNECTION_STRING` está definida.

## Variable de entorno

`MAT_TEST_CONNECTION_STRING` — connection string de SQL Server para tests de integración (típicamente MAT_DEV / `MAT.Intranet`).

Si no está definida, los fixtures de integración hacen `Assert.Ignore` y no fallan el build.

## Prerrequisitos integración SQL

Antes de correr tests de integración, desplegar en la BD objetivo:

- `database/2026-07-05_NetTiers_F9_Lookup_SPs.sql`
- `database/2026-07-05_NetTiers_F12_Lookup_Perf_SPs.sql`

Los tests validan que los SP existen en `sys.procedures` y que los lookups ejecutan sin excepción.

## Ejecución manual con vstest

```powershell
nuget restore MAT.sln

& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" MAT.sln /t:Build /p:Configuration=Debug

$adapter = "packages\NUnit3TestAdapter.4.6.0\build\net462"
$vstest = "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"

& $vstest MAT.MVC.Tests\bin\Debug\MAT.MVC.Tests.dll "/TestAdapterPath:$adapter"
```

## Cobertura actual

### MAT.MVC.Tests

- `ReportesQueryHelperTests` — parseo de fechas, rangos, viaje vs fechas.
- `HelperTests` — `ToSelectItem` (Guid/int inválidos), `IsNumeric`, `FillEntity`.
- `MigrationContractTests` — sin `*.generated.cs`, sin carpetas NetTiers legado, SPs F9/F12 en `MAT.DB`.

### MAT.Integration.Tests

- Existencia de SPs F9/F12 en la BD.
- Smoke: `GetProveedorSelectItems`, `GetViajeSelectItems`, `GetHotelSelectItems`.
- `CountReservasByHabitacionAndViaje` con GUIDs dummy (espera ≥ 0).

## Deuda conocida

- `usp_MAT_Viaje_CancelViaje` referenciado en `ViajeModel.cs` pero ausente en `MAT.DB`. El script de verificación lo reporta como **advertencia**, no como fallo bloqueante.

## CI futuro

`tools\Run-Tests.ps1` está listo para invocarse desde GitHub Actions o Azure DevOps. Los tests de integración son opcionales vía secret/variable `MAT_TEST_CONNECTION_STRING`.
