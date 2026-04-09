# Reportes administrativos — Inventario de SP (read-only) y estrategia MAT.MVC

**Fecha:** 2026-04-08  
**Alcance:** Solo inventario y decisión de implementación. **No se modifican** los archivos `.sql` de `usp_MAT_Reportes_Ventas`, `usp_MAT_Reportes_Pagos` ni `usp_MAT_Reportes_RankingCompras`.

---

## Resumen ejecutivo

| SP | ¿Contrato suficiente para MAT.MVC + paridad REPORTES_MAT_WEB? | Estrategia |
|----|----------------------------------------------------------------|------------|
| `usp_MAT_Reportes_Ventas` | Sí | Invocar SP actual; capa C# normaliza fechas a `DD-MM-YYYY` en `@From`/`@To`; mapeo case-insensitive y `monedaTipo` como string. |
| `usp_MAT_Reportes_Pagos` | Sí | Igual; incluye `@TipoVentaId` (solo rama por fechas). |
| `usp_MAT_Reportes_RankingCompras` | Sí, con matices | Invocar SP actual; **`@From`/`@To` son tipo SQL `DATE`**, no `NVARCHAR`: en C# usar `SqlDbType.Date`/`DateTime` parseado desde `from`/`to` del query. `@VendedorId` existe en la firma pero **no filtra** en el `WHERE` actual (línea comentada). |

**Decisión:** No crear SP nuevos en esta etapa. **No** usar `ALTER` sobre los tres SP originales. Si más adelante el negocio exige filtrar ranking por vendedor u homogeneizar tipos de fecha en T-SQL, valorar un SP con nombre nuevo (p. ej. `_Admin` / `_V2`) según `SPEC.md`, sin tocar los procedimientos actuales.

---

## 1. `dbo.usp_MAT_Reportes_Ventas`

### Parámetros (MAT.DB)

| Parámetro | Tipo | Default | Uso |
|-----------|------|---------|-----|
| `@From` | `NVARCHAR(10)` | NULL | Texto fecha estilo `dd-mm-yyyy` ( conversión con estilo **105** en el SP). |
| `@To` | `NVARCHAR(10)` | NULL | Igual. |
| `@ViajeId` | `UNIQUEIDENTIFIER` | NULL | Si **no es NULL**, rama única: filtra por viaje; `@From`/`@To` no aplican en el filtro. |
| `@VendedorId` | `UNIQUEIDENTIFIER` | NULL | Solo rama por **rango de fechas**: filtro opcional. |
| `@ClienteId` | `UNIQUEIDENTIFIER` | NULL | Solo rama por **rango de fechas**: filtro opcional. |

### Ramas

- **`@ViajeId` presente:** `WHERE tViaje.ViajeId = @ViajeId` (sin filtro de fechas ni vendedor/cliente en esa rama).
- **Si no:** `f.Fecha BETWEEN @Dfrom AND DATEADD(DAY, 1, @Dto)` más filtros opcionales vendedor/cliente.

### Columnas devueltas (principales)

`ViajeId`, `ViajeDescripcion`, `FechaSalida` (varchar `dd/mm/yyyy`), `CantidadButacas`, `VendedorId`, `VendedorFullName`, `ClienteId`, `ClienteFullName`, `FacturaId`, `FacturaFecha`, `FacturaEstado`, `MonedaTipo`, `TotalFactura`, `MontoPagado`, `Saldo`.

### Notas

- Incluye `PRINT` de depuración (fechas); no afecta resultados pero puede ensuciar trazas SQL.
- Alineado con `REPORTES_MAT_WEB.md` para Ventas (DTO / filtros mutuamente excluyentes rango vs viaje a nivel de negocio).

---

## 2. `dbo.usp_MAT_Reportes_Pagos`

### Parámetros (MAT.DB)

| Parámetro | Tipo | Default | Uso |
|-----------|------|---------|-----|
| `@From` | `NVARCHAR(10)` | NULL | `dd-mm-yyyy` vía estilo 105. |
| `@To` | `NVARCHAR(10)` | NULL | Igual. |
| `@VendedorId` | `UNIQUEIDENTIFIER` | NULL | Rama fechas solamente. |
| `@ClienteId` | `UNIQUEIDENTIFIER` | NULL | Rama fechas solamente. |
| `@TipoVentaId` | `INT` | NULL | Rama fechas solamente. |
| `@ViajeId` | `UNIQUEIDENTIFIER` | NULL | Si **no es NULL**, rama por viaje: solo `WHERE v.ViajeID = @ViajeId` (no aplica `@TipoVentaId` / vendedor / cliente en ese bloque). |

### Columnas devueltas (principales)

`FacturaID`, `FechaPago`, `Monto`, `MonedaTipo`, `PagoTipoId`, `Descripcion`, `TipoVentaId`, `TipoVentaDescripcion`, `CantidadTipoPago`, `RankingTipoPago`, `VendedorId`, `VendedorFullName`, `ClienteId`, `ClienteFullName`, `Viaje` (texto: descripción + fecha).

### Notas

- `Descripcion` → DTO `pagoDescripcion` (camelCase en JSON) según spec MAT Web.

---

## 3. `dbo.usp_MAT_Reportes_RankingCompras`

### Parámetros (MAT.DB)

| Parámetro | Tipo | Default | Uso |
|-----------|------|---------|-----|
| `@To` | `DATE` | NULL | Filtro inclusivo en CTE: `f.Fecha <= @To`. |
| `@From` | `DATE` | NULL | Filtro: `f.Fecha >= @From`. |
| `@ViajeId` | `UNIQUEIDENTIFIER` | NULL | Opcional en CTE. |
| `@VendedorId` | `UNIQUEIDENTIFIER` | NULL | **No efectivo:** condición comentada en el `WHERE` del CTE. |
| `@ClienteId` | `UNIQUEIDENTIFIER` | NULL | Opcional. |

### Diferencia crítica vs Ventas/Pagos

Los dos primeros SP esperan **cadenas** `DD-MM-YYYY` en `@From`/`@To`. El SP de ranking espera **`DATE`**. El backend debe **parsear** `from`/`to` del HTTP (según helper de fechas) y pasar parámetros **tipados fecha** a este SP, no reutilizar sin adaptar el mismo par `NVarChar(10)` que para Ventas/Pagos.

### Columnas devueltas (principales)

`FacturaID`, `Fecha`, `ClienteID`, `FullName`, `ViajeID`, `ViajeDescripcion`, `ViajeFechaSalida`, `CantidadPasajesXFactura`, `CantViajesCompradosXCliente`, `CantPasajesCompradosXCliente`, `CantClientesEligieronViaje`, `RankingClientesCompradoresViajes`, `RankingViajes`.

### Notas

- `FullName` → DTO `clienteFullName` en spec.

---

## Referencia código existente

- `HomeController` ya llama `usp_MAT_Reportes_Ventas` con `SqlParameter` `NVarChar` para `@From`/`@To` (patrón a reutilizar para Ventas/Pagos en el futuro `ReportesController`).

---

## Helper C# (fechas y parámetros)

- `MAT.MVC/Infrastructure/ReportesQueryHelper.cs` + `ReportesQueryParseResult.cs`: parseo de `from`/`to`/`viajeId` y opcionales (`vendedorId`, `clienteId`, `tipoVentaId`); exclusión mutua rango vs viaje (**rechazo** si ambos); máximo **365 días calendario inclusive**; cadenas `dd-MM-yyyy` para Ventas/Pagos y `DateTime` fecha para Ranking.

## Mapeo fila → DTO (`SqlDataReader`)

- `MAT.MVC/Infrastructure/ReportesDataReaderMapper.cs`: `ReadVentas` / `ReadPagos` / `ReadRanking`; columnas vía diccionario **case-insensitive**; `Descripcion` → `PagoDescripcion`, `FullName` → `ClienteFullName` (ranking); `MonedaTipo` siempre string; montos `decimal?`; fechas string o nativas.
- DTOs: `MAT.MVC/Models/Reportes/ReporteVentaRowDto.cs`, `ReportePagoRowDto.cs`, `ReporteRankingRowDto.cs`. JSON camelCase: serializar con **Newtonsoft** y `CamelCasePropertyNamesContractResolver`.

## API JSON en MAT.MVC (Administrador)

- Controlador: `MAT.MVC/Controllers/Admin/ReportesController.cs`; ruta registrada en `RouteConfig`: **`/Admin/Reportes/Ventas`**, **`/Admin/Reportes/Pagos`**, **`/Admin/Reportes/Ranking`** (GET, query params como en `REPORTES_MAT_WEB.md`).
- Respuesta: `{ "ok": bool, "message": string | null, "data": array | null }` en **camelCase** (incl. propiedades de filas). Sin rol Administrador: `ok: false` y mensaje genérico (JSON, no redirección).
- **Nuevo SP:** no requerido tras inventario 2026-04-08; se usan `usp_MAT_Reportes_*` existentes.

## Próximos pasos (otros tasks en SPEC)

1. ~~Helper reutilizable de fechas y exclusión rango vs `viajeId`.~~
2. ~~Mapeo fila → DTO JSON (case-insensitive, renombres, `monedaTipo` string).~~
3. ~~Endpoints JSON bajo Admin (`ReportesController`).~~ Exportación Excel (tasks P2 media en `SPEC.md`).
