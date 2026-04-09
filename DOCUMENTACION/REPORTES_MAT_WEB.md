# Reportes MAT Web — Especificación de referencia

**Uso en MAT (intranet):** Describe el módulo de reportes del sistema **MAT Web**, que comparte base de datos con este proyecto. La implementación en `MAT.MVC` debe lograr **paridad funcional** (filtros, mapeo a DTO, exportación Excel, reglas de fechas). Las rutas HTTP exactas (`/reportes/...`) son las de MAT Web; en MAT pueden registrarse alias o rutas bajo `Admin` (ver tareas en `SPEC.md`). **En MAT se prioriza EPPlus** (ya en la solución), no ClosedXML, salvo decisión explícita.

**Nota:** El cuerpo siguiente está en inglés (documento fuente) para mantener coincidencia literal con nombres de columnas y contratos.

---

# Reports Module - Requirements Specification

## Overview

The reports module provides 3 report types accessible only by ADMIN users:

1. **Sales Report** - Invoice-level detail with trip, seller, client, amounts, and balances
2. **Payments Report** - Payment-level detail with payment method, sale type, and ranking
3. **Purchase Ranking Report** - Client and trip ranking based on purchase volume

Each report has a data endpoint (JSON) and an Excel export endpoint.

---

## Endpoints

| Method | Route                          | Description                | Auth   |
|--------|--------------------------------|----------------------------|--------|
| GET    | `/reportes/ventas`             | Sales report data          | ADMIN  |
| GET    | `/reportes/ventas/excel`       | Sales report Excel export  | ADMIN  |
| GET    | `/reportes/pagos`              | Payments report data       | ADMIN  |
| GET    | `/reportes/pagos/excel`        | Payments report Excel export | ADMIN |
| GET    | `/reportes/ranking-compras`    | Purchase ranking data      | ADMIN  |
| GET    | `/reportes/ranking-compras/excel` | Purchase ranking Excel export | ADMIN |

**Rutas JSON en MAT.MVC (2026-04):** `GET /Admin/Reportes/Ventas`, `/Admin/Reportes/Pagos`, `/Admin/Reportes/Ranking` — mismos query params que en las tablas siguientes; rol **Administrador**; respuesta `{ "ok", "message", "data" }` en camelCase vía Newtonsoft (ver `ReportesController`).

**Rutas Excel en MAT.MVC (2026-04):** `GET /Admin/Reportes/VentasExcel`, `/Admin/Reportes/PagosExcel`, `/Admin/Reportes/RankingExcel` — mismos query params que el JSON correspondiente; respuesta binaria `.xlsx` (ver `ReportesExcelExport`).

**Hub y vistas HTML:** `GET /Admin/Reportes` (índice), `ReporteVentas`, `ReportePagos`, `ReporteRanking` — `RequireAdministratorView()`.

**Operación y pruebas en MAT.MVC:** ver `DOCUMENTACION/REPORTES_MAT_MVC_OPERACION.md`.

---

## Standard Response Format

All data endpoints (non-Excel) must wrap responses in:

```json
{
  "ok": true,
  "data": [ ...array of DTOs... ]
}
```

All property names in the JSON response must be **camelCase**.

Excel endpoints return a binary `.xlsx` file with:
- Content-Type: `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`
- Filename pattern: `{report-name}-{YYYY-MM-DD}.xlsx`

---

## 1. Sales Report

### Endpoint: `GET /reportes/ventas`

#### Query Parameters

| Parameter    | Type   | Required    | Format / Values    | Description                    |
|--------------|--------|-------------|--------------------|--------------------------------|
| `from`       | string | Conditional | `DD-MM-YYYY` or `YYYY-MM-DD` | Range start date. Required if filtering by date range. |
| `to`         | string | Conditional | `DD-MM-YYYY` or `YYYY-MM-DD` | Range end date. Required if filtering by date range. |
| `viajeId`    | string | Conditional | UUID               | Trip ID. Required if filtering by trip. |
| `vendedorId` | string | Optional    | UUID               | Seller ID. |
| `clienteId`  | string | Optional    | UUID               | Client ID. |

#### Execution Condition

The query must only execute if at least one condition is met:
- `from` AND `to` are both present (date range filter), OR
- `viajeId` is present (trip filter)

These are mutually exclusive from the UI: the user filters by date range OR by trip, never both simultaneously.

If neither condition is met, return an empty array.

#### Stored Procedure: `[dbo].[usp_MAT_Reportes_Ventas]`

| SP Parameter  | SQL Type           | Nullable |
|---------------|--------------------|----------|
| `@From`       | VARCHAR / NVARCHAR | YES      |
| `@To`         | VARCHAR / NVARCHAR | YES      |
| `@ViajeId`    | UNIQUEIDENTIFIER   | YES      |
| `@VendedorId` | UNIQUEIDENTIFIER   | YES      |
| `@ClienteId`  | UNIQUEIDENTIFIER   | YES      |

The SP expects dates in `DD-MM-YYYY` format. The backend must normalize input dates before calling the SP (see [Date Format Handling](#date-format-handling)).

#### SP Output Columns → Response DTO Mapping

| SP Column         | SQL Type         | DTO Field          | JSON Type | Notes |
|-------------------|------------------|--------------------|-----------|-------|
| `ViajeId`         | UNIQUEIDENTIFIER | `viajeId`          | string    |       |
| `ViajeDescripcion`| NVARCHAR         | `viajeDescripcion` | string    |       |
| `VendedorId`      | UNIQUEIDENTIFIER | `vendedorId`       | string    |       |
| `VendedorFullName`| NVARCHAR         | `vendedorFullName` | string    |       |
| `ClienteId`       | UNIQUEIDENTIFIER | `clienteId`        | string    |       |
| `ClienteFullName` | NVARCHAR         | `clienteFullName`  | string    |       |
| `FacturaId`       | UNIQUEIDENTIFIER | `facturaId`        | string    |       |
| `FacturaFecha`    | VARCHAR or DATE  | `facturaFecha`     | Date (ISO)| May come as string `DD/MM/YYYY` or `DD-MM-YYYY` from SP |
| `FacturaEstado`   | NVARCHAR         | `facturaEstado`    | string    | Known values: `"Pagado"`, `"Señado"` |
| `MonedaTipo`      | NVARCHAR         | `monedaTipo`       | string    | `"1"` = ARS, `"3"` = USD. Must remain string, not int. |
| `TotalFactura`    | MONEY / DECIMAL  | `totalFactura`     | number    |       |
| `MontoPagado`     | MONEY / DECIMAL  | `montoPagado`      | number    |       |
| `Saldo`           | MONEY / DECIMAL  | `saldo`            | number    |       |
| `FechaSalida`     | VARCHAR or DATE  | `fechaSalida`      | Date (ISO)| May come as string from SP |
| `CantidadButacas` | INT              | `cantidadButacas`  | number    |       |

#### Excel Export: `GET /reportes/ventas/excel`

Same query parameters as the data endpoint (only `from`, `to`, `viajeId`).

Excel columns in order:
Viaje ID, Viaje, Fecha Salida, Cantidad Butacas, Vendedor ID, Vendedor, Cliente ID, Cliente, Factura ID, Fecha Factura, Estado, Moneda, Total Factura, Monto Pagado, Saldo

---

## 2. Payments Report

### Endpoint: `GET /reportes/pagos`

#### Query Parameters

| Parameter      | Type   | Required    | Format / Values              | Description                    |
|----------------|--------|-------------|------------------------------|--------------------------------|
| `from`         | string | Conditional | `DD-MM-YYYY` or `YYYY-MM-DD` | Range start date. Required if filtering by date range. |
| `to`           | string | Conditional | `DD-MM-YYYY` or `YYYY-MM-DD` | Range end date. Required if filtering by date range. |
| `viajeId`      | string | Conditional | UUID                         | Trip ID. Required if filtering by trip. |
| `vendedorId`   | string | Optional    | UUID                         | Seller ID. |
| `clienteId`    | string | Optional    | UUID                         | Client ID. |
| `tipoVentaId`  | number | Optional    | `1` = Office, `2` = Online   | Sale type. Not currently sent by the UI (filtered client-side). |

#### Execution Condition

Same as Sales: requires (`from` + `to`) or `viajeId`. Otherwise return empty array.

#### Stored Procedure: `[dbo].[usp_MAT_Reportes_Pagos]`

| SP Parameter   | SQL Type           | Nullable |
|----------------|--------------------|----------|
| `@From`        | VARCHAR / NVARCHAR | YES      |
| `@To`          | VARCHAR / NVARCHAR | YES      |
| `@VendedorId`  | UNIQUEIDENTIFIER   | YES      |
| `@ClienteId`   | UNIQUEIDENTIFIER   | YES      |
| `@TipoVentaId` | INT                | YES      |
| `@ViajeId`     | UNIQUEIDENTIFIER   | YES      |

#### SP Output Columns → Response DTO Mapping

| SP Column              | SQL Type         | DTO Field              | JSON Type | Notes |
|------------------------|------------------|------------------------|-----------|-------|
| `FacturaID`            | UNIQUEIDENTIFIER | `facturaId`            | string    | Note: SP returns `FacturaID` (uppercase ID) |
| `FechaPago`            | DATETIME         | `fechaPago`            | Date (ISO)|       |
| `Monto`                | MONEY / DECIMAL  | `monto`                | number    |       |
| `MonedaTipo`           | NVARCHAR         | `monedaTipo`           | string    | `"1"` = ARS, `"3"` = USD |
| `PagoTipoId`           | INT              | `pagoTipoId`           | number    | Payment method ID |
| `Descripcion`          | NVARCHAR         | `pagoDescripcion`      | string    | Payment method name. Note: SP column is `Descripcion`, DTO field is `pagoDescripcion` |
| `TipoVentaId`          | INT              | `tipoVentaId`          | number    | `1` = Office, `2` = Online |
| `TipoVentaDescripcion` | NVARCHAR         | `tipoVentaDescripcion` | string    | `"Venta Oficina"`, `"Venta Online"` |
| `CantidadTipoPago`     | INT              | `cantidadTipoPago`     | number    |       |
| `RankingTipoPago`       | INT              | `rankingTipoPago`      | number    |       |
| `VendedorId`           | UNIQUEIDENTIFIER | `vendedorId`           | string    |       |
| `VendedorFullName`     | NVARCHAR         | `vendedorFullName`     | string    |       |
| `ClienteId`            | UNIQUEIDENTIFIER | `clienteId`            | string    |       |
| `ClienteFullName`      | NVARCHAR         | `clienteFullName`      | string    |       |
| `Viaje`                | NVARCHAR         | `viaje`                | string    | Trip description (not UUID) |

#### Excel Export: `GET /reportes/pagos/excel`

Same query parameters as the data endpoint.

Excel columns in order:
Factura ID, Fecha Pago, Monto, Moneda, Tipo Pago ID, Tipo Pago, Tipo Venta ID, Tipo Venta, Cant. Tipo Pago, Ranking Tipo Pago, Vendedor ID, Vendedor, Cliente ID, Cliente, Viaje

---

## 3. Purchase Ranking Report

### Endpoint: `GET /reportes/ranking-compras`

#### Query Parameters

| Parameter    | Type   | Required    | Format / Values              | Description                    |
|--------------|--------|-------------|------------------------------|--------------------------------|
| `from`       | string | Conditional | `DD-MM-YYYY` or `YYYY-MM-DD` | Range start date. |
| `to`         | string | Conditional | `DD-MM-YYYY` or `YYYY-MM-DD` | Range end date. |
| `viajeId`    | string | Conditional | UUID                         | Trip ID. |
| `clienteId`  | string | Optional    | UUID                         | Client ID. |

#### Execution Condition

Same as other reports: requires (`from` + `to`) or `viajeId`.

#### Stored Procedure: `[dbo].[usp_MAT_Reportes_RankingCompras]`

| SP Parameter  | SQL Type           | Nullable | Notes |
|---------------|--------------------|----------|-------|
| `@From`       | **DATE**           | YES      | **Not** `NVARCHAR`: backend should pass `SqlDbType.Date` / parsed `DateTime`, not `DD-MM-YYYY` strings like Ventas/Pagos. |
| `@To`         | **DATE**           | YES      | Same as `@From`. |
| `@ViajeId`    | UNIQUEIDENTIFIER   | YES      | |
| `@VendedorId` | UNIQUEIDENTIFIER   | YES      | Present in signature; **currently not applied** in the SP `WHERE` (filter commented in `MAT.DB`). |
| `@ClienteId`  | UNIQUEIDENTIFIER   | YES      | |

See `DOCUMENTACION/REPORTES_SP_INVENTARIO_ESTRATEGIA.md` for full inventory and MAT.MVC strategy.

#### SP Output Columns → Response DTO Mapping

| SP Column                         | SQL Type         | DTO Field                        | JSON Type | Notes |
|-----------------------------------|------------------|----------------------------------|-----------|-------|
| `FacturaID`                       | UNIQUEIDENTIFIER | `facturaId`                      | string    |       |
| `Fecha`                           | DATETIME         | `fecha`                          | Date (ISO)|       |
| `ClienteID`                       | UNIQUEIDENTIFIER | `clienteId`                      | string    | Note: uppercase ID |
| `FullName`                        | NVARCHAR         | `clienteFullName`                | string    | Note: SP column is `FullName`, DTO is `clienteFullName` |
| `ViajeID`                         | UNIQUEIDENTIFIER | `viajeId`                        | string    | Note: uppercase ID |
| `ViajeDescripcion`                | NVARCHAR         | `viajeDescripcion`               | string    |       |
| `ViajeFechaSalida`                | DATETIME         | `viajeFechaSalida`               | Date (ISO)|       |
| `CantidadPasajesXFactura`         | INT              | `cantidadPasajesXFactura`        | number    |       |
| `CantViajesCompradosXCliente`     | INT              | `cantViajesCompradosXCliente`    | number    |       |
| `CantPasajesCompradosXCliente`    | INT              | `cantPasajesCompradosXCliente`   | number    |       |
| `CantClientesEligieronViaje`      | INT              | `cantClientesEligieronViaje`     | number    |       |
| `RankingClientesCompradoresViajes`| INT              | `rankingClientesCompradoresViajes`| number   |       |
| `RankingViajes`                   | INT              | `rankingViajes`                  | number    |       |

#### Excel Export: `GET /reportes/ranking-compras/excel`

Same query parameters as the data endpoint.

Excel columns in order:
Factura ID, Fecha, Cliente ID, Cliente, Viaje ID, Viaje, Fecha Salida, Pasajes x Factura, Viajes Comprados, Pasajes Comprados, Clientes Eligieron Viaje, Ranking Clientes, Ranking Viajes

---

## Period Comparison (Trends)

The frontend performs **2 simultaneous calls** to `GET /reportes/ventas` to compare a current period vs. the previous one. No additional endpoint is needed — the same sales endpoint is reused.

### Available Periods

| Period                      | Call 1 (Current)                                          | Call 2 (Previous)                                          |
|-----------------------------|-----------------------------------------------------------|------------------------------------------------------------|
| Month vs Previous Month     | `from`: 1st of current month, `to`: last of current month | `from`: 1st of previous month, `to`: last of previous month |
| Year vs Previous Year       | `from`: Jan 1 current year, `to`: Dec 31 current year     | `from`: Jan 1 previous year, `to`: Dec 31 previous year    |
| Semester vs Previous Semester | Current semester dates                                   | Same semester of previous year                              |
| Quarter vs Previous Quarter | Current quarter dates                                      | Same quarter of previous year                               |
| Bimester vs Previous Bimester | Current bimester dates                                   | Same bimester of previous year                              |

**Important:** These comparison calls send dates in `YYYY-MM-DD` format (not `DD-MM-YYYY`). See [Date Format Handling](#date-format-handling).

An optional `viajeId` filter may be included in both calls.

---

## Implementation Notes

### Date Format Handling

**CRITICAL.** The frontend sends dates in 2 different formats:

| Context                          | Format sent    |
|----------------------------------|----------------|
| Manual date filters (statistics) | `DD-MM-YYYY`   |
| Period comparison (trends)       | `YYYY-MM-DD`   |

The stored procedures **`usp_MAT_Reportes_Ventas`** and **`usp_MAT_Reportes_Pagos`** expect `DD-MM-YYYY` format for their `@From` and `@To` **string** parameters.

**Exception:** **`usp_MAT_Reportes_RankingCompras`** uses SQL **`DATE`** parameters for `@From` and `@To`. After parsing the HTTP query to `DateTime`, pass them as date-typed parameters to SQL (do not send `DD-MM-YYYY` strings to that procedure).

The backend **must**:
1. Accept both formats in `from` and `to` query parameters.
2. Detect the format (check if the string matches `YYYY-MM-DD` or `DD-MM-YYYY`).
3. For **Ventas** and **Pagos**: normalize to `DD-MM-YYYY` before passing to the SP.
4. For **Ranking**: parse to `DateTime` and pass as `DATE` (`SqlDbType.Date` or equivalent).

**Recommended approach:** Parse the input string to a `DateTime` object, then format as `DD-MM-YYYY` for Ventas/Pagos SPs only; use the same `DateTime` for Ranking with correct SQL type.

### Date Parsing from SP Results

The Sales SP may return `FacturaFecha` and `FechaSalida` as strings in `DD/MM/YYYY` or `DD-MM-YYYY` format instead of native `DATETIME`. The backend must handle both:
- If `VARCHAR`: parse with `dd/MM/yyyy` and `dd-MM-yyyy` patterns.
- If `DATETIME`: map directly.

### SP Column Name Casing Inconsistencies

The stored procedures are inconsistent in their column name casing:

| Field      | Sales SP     | Payments SP  | Ranking SP   |
|------------|-------------|-------------|--------------|
| Invoice ID | `FacturaId` | `FacturaID` | `FacturaID`  |
| Client ID  | `ClienteId` | `ClienteId` | `ClienteID`  |
| Trip ID    | `ViajeId`   | -           | `ViajeID`    |
| Seller ID  | `VendedorId`| `VendedorId`| -            |

Use case-insensitive column mapping to avoid issues.

### SP Column → DTO Field Renames

Some SP column names differ from the DTO field names:

| SP Column       | DTO Field          | Report   |
|-----------------|--------------------|----------|
| `Descripcion`   | `pagoDescripcion`  | Payments |
| `FullName`      | `clienteFullName`  | Ranking  |
| `Viaje`         | `viaje`            | Payments (this is a description string, not a UUID) |

### MonedaTipo Must Remain a String

`monedaTipo` is returned as a **string** (`"1"`, `"3"`), not an integer. The frontend depends on this. Do not convert to `int`.

### MONEY Type Mapping

SQL Server `MONEY` fields (`Monto`, `TotalFactura`, `MontoPagado`, `Saldo`) should map to `decimal` in .NET.

### NULL Handling for SP Parameters

Parameters that are empty, undefined, or not provided must be passed as `NULL` to the stored procedures.

### Frontend Validation Rules (for reference)

These validations are enforced by the frontend but should also be considered on the backend:
- **Maximum date range:** 365 days between `from` and `to`
- **Mutually exclusive filters:** `(from + to)` OR `viajeId`, never both together
- **Unused parameters:** `vendedorId` and `clienteId` are defined in the DTOs but never sent from the current UI. `tipoVentaId` (payments only) is also not sent — it's filtered client-side.

### Excel Export Format

All Excel exports share the same styling:
- **Header row:** Background color `#4472C4` (blue), white bold text, centered alignment
- **Date formatting:** Locale `es-AR` (Argentine Spanish)
- **Content-Type:** `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`
- **Filename:** `{report-name}-{YYYY-MM-DD}.xlsx`

Recommended .NET libraries: **ClosedXML** (free, open source) or **EPPlus** (commercial license from v5).

---

## Database Schema (Reference)

Key tables involved in the stored procedures:

| Table        | Primary Key              | Key Fields                                                |
|--------------|--------------------------|-----------------------------------------------------------|
| `Factura`    | `FacturaID` (UUID)       | NroFactura, Monto, Fecha, Tipo, Estado, ClienteID (FK), VendedorID (FK), MonedaTipo (FK) |
| `Pago`       | `PagoID` (UUID)          | FechaPago, Monto (money), TipoPago (FK), VendedorId (FK) |
| `Viaje`      | `ViajeID` (UUID)         | PaqueteID (FK), Origen, FechaSalida, Descripcion, BusID (FK) |
| `Vendedor`   | `VendedorID` (UUID)      | Descripcion, PersonaID (FK)                               |
| `Cliente`    | `ClienteID` (UUID)       | RazonSocial, Cuit, Moneda, VendedorID (FK), PersonaID (FK) |
| `Persona`    | `PersonaID` (UUID)       | Apellido, Nombre, FullName, Email, NroDocumento           |
| `MonedaTipo` | `Id` (int)               | Descripcion, Codigo                                       |
| `PagoTipo`   | `Id` (int)               | Descripcion                                               |

All join logic is encapsulated within the stored procedures. The backend only needs to call the SPs and map the results.
