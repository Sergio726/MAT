# Reportes en MAT.MVC — Operación y pruebas

Referencia rápida para desarrolladores y QA. Contrato funcional detallado: `DOCUMENTACION/REPORTES_MAT_WEB.md`. Inventario de SP: `DOCUMENTACION/REPORTES_SP_INVENTARIO_ESTRATEGIA.md`.

## Autenticación y rol

- Todas las acciones requieren usuario autenticado.
- Rol **Administrador** (SimpleMembership / `Roles.IsUserInRole(..., "Administrador")`).
- **JSON:** sin sesión → **401** y cuerpo JSON `{ "ok": false, "message": "...", "data": null }`; con sesión pero sin rol → **403** y el mismo formato.
- **Excel:** sin sesión → **401**; sin permiso → **403**; validación → **400** (texto plano); error interno → **500** (mensaje público vía `ErrorUtil`).
- **Vistas HTML** (`Index`, `ReporteVentas`, …): redirección a login o a Home si no corresponde.

## URLs principales (hub y pantallas)

| Uso | GET |
|-----|-----|
| Índice (cards) | `/Admin/Reportes` |
| Pantalla ventas | `/Admin/Reportes/ReporteVentas` |
| Pantalla pagos | `/Admin/Reportes/ReportePagos` |
| Pantalla ranking | `/Admin/Reportes/ReporteRanking` |

Entrada desde **Admin → Índice** (tarjeta “Reportes operativos”) y menú lateral en `_LayoutAdmin`.

## Endpoints JSON

| Reporte | GET |
|---------|-----|
| Ventas | `/Admin/Reportes/Ventas` |
| Pagos | `/Admin/Reportes/Pagos` |
| Ranking compras | `/Admin/Reportes/Ranking` |
| Búsqueda de viajes (autocompletar UI) | `/Admin/Reportes/BuscarViajes?q=texto` o `?recent=true` |

**Respuesta:** JSON con `{ "ok": true|false, "message": "...", "data": [ ... ] }` (propiedades en **camelCase**).

**`BuscarViajes`:**
- Con **`recent=true`**: lista los **últimos 30** viajes con descripción, ordenados por **fecha de salida** descendente (sin filtro de texto). Útil para el botón “flecha abajo” en la UI.
- Con **`q`**: mínimo **2** caracteres; búsqueda por nombre (`LIKE`, insensible a acentos); máximo 30 filas.
- Cada ítem: `{ "id", "descripcion", "fechaSalida" }` (`fechaSalida` en ISO `yyyy-MM-dd`).
- SP: `usp_MAT_Reportes_BuscarViajes` (`@RecentOnly`, `@q`). Si `q` tiene menos de 2 caracteres y no es `recent`, el controller devuelve `data: []` sin ejecutar el SP.

**Parámetros query** (misma semántica que en `REPORTES_MAT_WEB.md`):

- Filtro por rango: `from`, `to` (`DD-MM-YYYY` o `YYYY-MM-DD`).
- Filtro por viaje: `viajeId` (GUID). **No** combinar con `from`/`to` en la misma petición.
- Opcionales: `vendedorId`, `clienteId`; en pagos también `tipoVentaId` (**solo 1 u 2** si se envía).
- Reglas de validación en servidor: rango máximo 365 días; viaje y fechas mutuamente excluyentes.

Ejemplo mínimo (ventas por fechas, curl):

```http
GET /Admin/Reportes/Ventas?from=2026-01-01&to=2026-01-31
Cookie: .ASPXAUTH=...
```

## Endpoints Excel (EPPlus)

| Reporte | GET |
|---------|-----|
| Ventas | `/Admin/Reportes/VentasExcel` |
| Pagos | `/Admin/Reportes/PagosExcel` |
| Ranking | `/Admin/Reportes/RankingExcel` |

Mismos query params que el JSON del mismo reporte.

- **Content-Type:** `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`
- **Nombre sugerido:** `ventas-YYYY-MM-DD.xlsx`, `pagos-YYYY-MM-DD.xlsx`, `ranking-compras-YYYY-MM-DD.xlsx`
- Cabecera de tabla: fondo `#4472C4`, texto blanco y negrita; fechas cultura `es-AR`; montos formato `#,##0.00`.

## Alias REST (compatibilidad MAT Web)

Mismas acciones que arriba, rutas alternativas:

| JSON | Excel |
|------|-------|
| `GET /reportes/ventas` | `GET /reportes/ventas/excel` |
| `GET /reportes/pagos` | `GET /reportes/pagos/excel` |
| `GET /reportes/ranking-compras` | `GET /reportes/ranking-compras/excel` |

## Stored procedures (sin modificar legacy por defecto)

| Reporte | SP |
|---------|-----|
| Ventas | `[dbo].[usp_MAT_Reportes_Ventas]` |
| Pagos | `[dbo].[usp_MAT_Reportes_Pagos]` |
| Ranking | `[dbo].[usp_MAT_Reportes_RankingCompras]` |
| Búsqueda viajes (Admin) | `[dbo].[usp_MAT_Reportes_BuscarViajes]` |

**Política:** los cambios de esquema van a `MAT.DB` (SSDT) y se documentan; no alterar SP por este módulo salvo requisito acordado.

### Ranking de compras — lectura de columnas (SP `usp_MAT_Reportes_RankingCompras`)

- **`CantClientesEligieronViaje` (UI “Clientes viaje”):** en el SP es `COUNT(ViajeID) OVER (PARTITION BY ViajeID)` sobre el conjunto filtrado: cuenta **filas** (facturas con pasajes a ese viaje), **no** `COUNT(DISTINCT ClienteID)`. El nombre histórico en SQL puede interpretarse mal.
- **`RankingViajes` (UI “Rank. viajes”):** `DENSE_RANK() OVER (ORDER BY CantClientesEligieronViaje DESC)`. El **1** indica el grupo de viajes con el **mayor** valor de esa métrica (empates comparten rank).
- **`RankingClientesCompradoresViajes` (UI “Rank. clientes”):** `DENSE_RANK() OVER (ORDER BY CantViajesCompradosXCliente DESC)` sobre el mismo CTE.

**Nota:** el **Home** sigue usando `usp_MAT_Reportes_Ventas` vía `HomeController` para estadísticas; el módulo Admin no reemplaza ese flujo.

## Archivos clave en código

- `MAT.MVC/Controllers/Admin/ReportesController.cs`
- `MAT.MVC/Infrastructure/ReportesQueryHelper.cs`, `ReportesQueryParseResult.cs`
- `MAT.MVC/Infrastructure/ReportesDataReaderMapper.cs`
- `MAT.MVC/Infrastructure/ReportesExcelExport.cs`
- `MAT.MVC/App_Start/RouteConfig.cs` (rutas `Admin/Reportes` y alias `reportes/...`)
- `MAT.MVC/Scripts/mat.reportes-excel-export.js` (descarga Excel + parseo de errores HTTP)
- Vistas: `MAT.MVC/Views/Reportes/*.cshtml`

## Criterios de prueba manual sugeridos

1. Login como admin → abrir hub y cada pantalla; **Consultar** y **Exportar Excel**.
2. Probar solo fechas, solo `viajeId`, y error esperado si se mezclan.
3. Llamar JSON y Excel con mismos filtros y comprobar paridad de filas.
4. Usuario no admin: JSON/Excel → 401/403; vistas → redirect lejos del reporte.
5. (Opcional) Llamar alias `/reportes/ventas` con mismos params que `/Admin/Reportes/Ventas`.
