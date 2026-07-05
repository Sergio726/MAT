# Viaje — Rentabilidad y gastos asociados

**Fecha:** 2026-06-30  
**Estado:** Investigación (sin implementación)  
**SPEC:** sección *Viaje — Rentabilidad y gastos* en `SPEC.md`

---

## 1. Objetivo

Permitir **asociar gastos/costos a un viaje específico** (`Viaje.ViajeID`) y contrastarlos con **ingresos** ya registrados en MAT, para responder:

- ¿Cuánto facturó / cobró esta salida?
- ¿Cuánto costó operarla?
- ¿Cuál es el **margen** (absoluto y %)?

---

## 2. Estado actual en MAT

### 2.1 Ingresos — bien vinculados al viaje

| Fuente | Cadena | UI / SP |
|--------|--------|---------|
| Factura operativa | `Factura` ← `Pasaje` → `Viaje.ViajeID` | Reserva, PersonaCliente |
| Reporte ventas | `usp_MAT_Reportes_Ventas` acepta `@ViajeId` | Admin Reportes, Home stats |
| Pagos por viaje | `usp_MAT_Pago_GetPagosByViaje` | `/Admin/ResumenPagos`, Excel EPPlus |

El lado **ingreso** ya permite agregar por viaje. No hay un único “total ingreso del viaje” expuesto como KPI en la ficha del viaje, pero los datos existen.

### 2.2 Costos — fragmentados o sin vínculo a viaje

| Fuente | Tabla / módulo | `ViajeID` | Observaciones |
|--------|----------------|-----------|---------------|
| Factura fiscal **compra** | `FacturaFiscal` (Tipo=1) | **No** | Registro contable/fiscal de proveedor; ideal candidato para imputar costo, pero hoy es global. |
| Planilla de costos | `Planilla` → `PlanillaServicioItem`, `PlanillaHabitacionItem` | **Sí** (`Planilla.ViajeID`) | Sumaba servicios (catálogo) + habitaciones con subtotales. **Retiro total confirmado (NetTiers F8, 2026-07-05):** menú Admin eliminado 2026-04/06; acciones `EditarPlanilla`/`ImprimirPlanilla*` en `AdminController` eliminadas 2026-06-17 (commit `f3c75f9`); campos huérfanos en `MATContext` eliminados en F8. Solo persisten las **tablas** en BD, sin ningún acceso desde `MAT.MVC`. |
| Catálogo excursión | `Excursion.Costo` | No | Costo de referencia del proveedor, no imputación por viaje. |
| Catálogo servicio | `Servicio.Precio` | No | Tarifa de venta/referencia; puede no coincidir con costo real negociado por salida. |
| Paquete / precio venta | `Precio`, `Paquete` | Indirecto | Precio al cliente, no costo operativo. |

**Conclusión:** no existe un módulo unificado de “gastos del viaje”. La brecha principal es **costo real imputado a `ViajeID`**.

### 2.3 Planilla — legado relevante

Esquema simplificado:

```
Viaje (1) ──< Planilla (N) ──< PlanillaServicioItem (ServicioID, Cantidad, Subtotal)
                          └──< PlanillaHabitacionItem (HabitacionID, Cantidad, Subtotal)
```

- `Planilla.Total` almacena total de la planilla.
- **Actualización (NetTiers F8, 2026-07-05):** el retiro es total, no solo del menú. Las acciones `EditarPlanilla`/`ImprimirPlanilla*`/`GridPlanillaServiciosItem*`/`GridPlanillaHotel*` **ya no existen** en `AdminController` (eliminadas 2026-06-17, commit `f3c75f9`, antes de que se ejecutara F8). Los campos estáticos `MATContext.Planilla`/`ColeccionPlanillas`/`ServiciosSeleccionados`/`HabitacionesPlanilla` (no eran de `HttpContext.Session`, sino estáticos de clase — estado compartido a nivel de proceso) estaban huérfanos y se eliminaron en F8. Los servicios NetTiers `PlanillaService`/`PlanillaServicioItemService`/`PlanillaHabitacionItemService` tenían cero callers y no requirieron migración a DBHelper/SP.
- Nota: la entidad NetTiers `PlanillaServicio` (distinta de `PlanillaServicioItem`) es un artefacto huérfano — no existe tabla `PlanillaServicio` separada; apunta a la misma tabla física `Planilla` (su `PRIMARY KEY` se llama `PK_PlanillaServicio`, rastro de un rename histórico).
- Solo las **tablas** (`Planilla`, `PlanillaServicioItem`, `PlanillaHabitacionItem`) permanecen en la base de datos, intactas y sin ningún acceso desde el código fuente actual.

**Riesgo:** reactivar planilla “como estaba” reintroduce complejidad de UI abandonada (no de sesión — ese problema ya no aplica tras F8). **Oportunidad:** reutilizar tablas si el negocio ya tiene datos históricos por viaje.

---

## 3. Definiciones pendientes (negocio)

Antes de implementar, acordar:

1. **Ingresos:** ¿total facturado, total cobrado (`Payment`), o ambos en columnas separadas?
2. **Estados de factura:** ¿incluir pre-reserva, anuladas, NC?
3. **Costos:** ¿solo compras con factura fiscal, gastos menores sin comprobante, estimados de catálogo, sueldos/coordinación?
4. **Moneda:** viajes en USD vs ARS — ¿tipo de cambio manual por viaje o solo moneda local?
5. **Prorrateo:** factura de proveedor que cubre varios viajes (ej. bus compartido).
6. **Permisos:** solo administradores vs visibilidad para vendedores/coordinadores.
7. **Momento:** ¿rentabilidad en tiempo real durante venta o solo post-salida?

Registrar respuestas en la sección **Decisiones de negocio** al final de este documento.

---

## 4. Opciones de diseño

### Opción A — Extender `FacturaFiscal` (compras)

**Qué:** `ALTER TABLE FacturaFiscal ADD ViajeID UNIQUEIDENTIFIER NULL` + FK a `Viaje`.

**Pros:** Reutiliza módulo fiscal ya en uso; compras reales con proveedor; trazabilidad a comprobante adjunto.  
**Contras:** No cubre gastos sin factura; compras multi-viaje requieren prorrateo o tabla puente; regeneración NetTiers si se toca entidad generated.

**UI:** selector de viaje en `FacturaFiscal/Create` (compras); filtro “sin viaje asignado” en Index.

### Opción B — Nueva tabla `ViajeGasto`

**Qué:** gastos operativos explícitos por viaje, con o sin enlace a `FacturaFiscalID`.

Campos sugeridos:

- `ViajeGastoID`, `ViajeID` (FK), `Fecha`, `Concepto`, `Categoria` (transporte, hotel, guía, comisión, otro)
- `ProveedorID` nullable, `Monto`, `Moneda`, `FacturaFiscalID` nullable
- `CreatedAt`, `CreatedBy`, `Observaciones`

**Pros:** Flexible; imprevistos y estimados; no contamina facturación fiscal.  
**Contras:** Carga manual duplicada si también se registran compras fiscales (mitigar con enlace opcional a FacturaFiscal).

### Opción C — Revivir / simplificar Planilla

**Qué:** UI nueva “Costos del viaje” que persiste en `Planilla*` o migra a `ViajeGasto` estructurado.

**Pros:** Alineado con práctica histórica (servicios + hoteles por salida).  
**Contras:** Modelo antiguo (`FLOAT`, sin auditoría rica); wizard en sesión no deseado; overlap con Opción B.

### Opción D — Costos estimados desde catálogo

**Qué:** calcular borrador: sum(`Excursion.Costo` × pax), servicios del paquete del viaje, etc.

**Pros:** Útil antes de cerrar la salida; comparar estimado vs real.  
**Contras:** No sustituye gastos reales; requiere reglas de negocio por tipo de producto.

### Recomendación preliminar

**Híbrido A + B:**

- Compras con comprobante → `FacturaFiscal.ViajeID`
- Gastos menores / ajustes → `ViajeGasto`
- Reporte único suma ambas fuentes (+ planillas legacy si siguen cargándose por SQL/herramientas externas)

Evaluar **C** solo si operación confirma uso activo de planillas históricas y desea la misma granularidad servicio/hotel.

---

## 5. Capa de presentación y reportes

### 5.1 Stored procedure propuesto

`usp_MAT_Viaje_GetRentabilidad @ViajeId`:

| Columna / bloque | Origen |
|------------------|--------|
| Ingresos facturados | Agregación `Factura`/`Pasaje` filtrado por viaje (reporters reutilizar subquery de `usp_MAT_Reportes_Ventas`) |
| Ingresos cobrados | `Payment` / lógica de `GetPagosByViaje` |
| Costos fiscales | `SUM(FacturaFiscal.Total)` Tipo=1, `ViajeID`, Estado activa |
| Costos operativos | `SUM(ViajeGasto.Monto)` |
| Costos planilla (opcional) | `SUM(Planilla.Total)` por `ViajeID` — evitar doble conteo si se migra a B
| Margen | Cobrado − Costos (definición acordada) |

Crear **SP nuevo**; no alterar `usp_MAT_Reportes_Ventas` (regla del módulo Reportes).

### 5.2 UI sugerida

1. **Admin — Rentabilidad por viaje:** buscador de viaje (patcheddar como `ResumenPagos`) + grilla desglose + Excel.
2. **Reserva/Index — KPI strip (futuro):** ingresos / costos / margen en hero (ver `RESERVA_INDEX_UX_MEJORAS.md` Fase 3+).
3. **FacturaFiscal compra:** campo viaje opcional al guardar.

Patrones existentes: `AdminResumenPagosExcelExport.cs`, `ErrorUtil`, `RequireAdministrador()`.

---

## 6. Fases de implementación sugeridas

| Fase | Contenido | Entregable |
|------|-----------|------------|
| 0 | Investigación | Este doc + SPEC ✅ |
| 1 | Workshop negocio | Decisiones §3 completadas |
| 2 | Schema + gastos manuales (B) | `ViajeGasto`, CRUD Admin |
| 3 | Compras fiscales (A) | `FacturaFiscal.ViajeID`, UI |
| 4 | Reporte rentabilidad | SP + pantalla + Excel |
| 5 | Estimados catálogo (D) | Opcional |
| 6 | Planilla simplificada (C) | Solo si negocio lo pide |

---

## 7. Riesgos y consideraciones técnicas

- **NetTiers:** cambios en entidades generated requieren regeneración o acceso SqlClient directo (patrón ya usado en reportes Admin).
- **Doble conteo:** si se usa Planilla y ViajeGasto para lo mismo, definir una fuente canónica.
- **FLOAT en Planilla:** preferir `DECIMAL(18,2)` en tablas nuevas.
- **Seguridad:** datos de margen sensibles; restringir a rol Administrador salvo decisión contraria.
- **Performance:** índice `(ViajeID)` en tablas de costo; agregaciones en SP, no en memoria en MVC.

---

## 8. Archivos de referencia en el repo

| Área | Ruta |
|------|------|
| Schema factura fiscal | `MAT.DB/dbo/Tables/FacturaFiscal.sql` |
| Schema planilla | `MAT.DB/dbo/Tables/Planilla.sql`, `PlanillaServicioItem.sql`, `PlanillaHabitacionItem.sql` |
| Ventas por viaje | `MAT.DB/dbo/Stored Procedures/usp_MAT_Reportes_Ventas.sql` |
| Pagos por viaje | `MAT.MVC/Models/PagoModel.cs` → `GetPagosByViaje` |
| Admin pagos | `MAT.MVC/Controllers/Admin/AdminController.cs` — `ResumenPagos`, `GridResumenPagos` |
| Retiro planillas UI | `DOCUMENTACION/MANUAL_USUARIO_ADMINISTRADOR.md`, `VISTAS_PENDIENTES_ACTUALIZACION.md` |
| Hero viaje (KPI futuro) | `MAT.MVC/Views/Reserva/_ReservaViajeHero.cshtml`, `RESERVA_INDEX_UX_MEJORAS.md` |

---

## 9. Decisiones de negocio

*(Completar tras validación con operaciones/contabilidad.)*

| Pregunta | Decisión | Fecha |
|----------|----------|-------|
| Base de ingresos | | |
| Qué costos incluir | | |
| Opción preferida (A/B/C/D) | | |
| Permisos | | |

---

## 10. Próximo paso

Task SPEC: **Viaje [P2]: Validación con negocio — definición de rentabilidad**. Sin cambios de código hasta cerrar §3 y §9.
