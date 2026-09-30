# Pendientes

Qué falta, ordenado por quién depende de cada cosa.

**Cómo usar este archivo:** fuente de verdad de tareas abiertas (sin Issues). Quien cierra un
ítem lo tilda `[x]` en el mismo commit o PR; el documento **se tilda, no se reescribe**.
Roles y forma de trabajo: [`HANDOFF.md`](HANDOFF.md).

Histórico de features ya hechas: [`../SPEC.md`](../SPEC.md). Bitácora: [`../PROGRESS.md`](../PROGRESS.md).

> **Estado en una línea (2026-09-30):** Build y tests OK; falta publicar SPs en BD; backlog = rentabilidad viaje + resumen de pagos por viaje para vendedores (Factura) + Integration API + Excursion UX + higiene de código (ex.Message, Web.config, warnings). Bug en investigación: reserva de menores no vincula al viaje.

---

## Bloquean — no dependen solo del equipo de código

- [ ] **Publicar SPs / scripts pendientes en cada entorno** — sin esto fallan features ya mergeadas en código. Incluye (verificar qué falta por entorno):
  - `usp_MAT_Factura_Search` (columnas Pagado/Saldo)
  - `database/2026-07-15_CambioButacas_PreserveHotel.sql`
  - `database/2026-07-07_DistribucionCoche_FacturaID.sql` y SP DistribucionCoche
  - Otros scripts recientes en `database/` no aplicados
- [ ] **Viaje — definición de rentabilidad con negocio** — acordar base de ingresos (facturado vs cobrado), qué entra como costo, moneda, prorrateo, permisos. Registrar en [`../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md`](../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md) y en [`DECISIONES_ABIERTAS.md`](DECISIONES_ABIERTAS.md). **Bloquea** opciones A–D de schema.

## Necesarios pero no bloquean

- [ ] Smoke visual Admin ResumenPagos (viaje con/sin pagos, Limpiar, Excel) tras UX 2026-08-12
- [ ] Smoke Descuento/Recargo en factura ARS y USD (badge moneda, sin conversión)

---

## Trabajo del equipo

### Factura / vendedores — resumen de pagos por viaje (P2)

**Pedido de operación:** en el módulo de factura que usan los **vendedores** (intranet, no solo Admin), necesitan un lugar para ver el **resumen de pagos hechos en el viaje** y el **método/medio de pago** de cada registro.

Hoy: Admin tiene `/Admin/ResumenPagos` (por viaje, con tipo de pago). En el flujo vendedor hay historial de pagos **por factura/cliente** (`DetalleFactura` → Pagos, `HistorialdePagos`), pero no un resumen consolidado **por viaje** con medios de pago visible en su día a día.

- [ ] **Factura [P2]: Revisar UX del módulo Factura (vista vendedor)** — mapear pantallas actuales (`Factura/Index`, listado por viaje, detalle factura / pagos) y gap vs necesidad de “pagos del viaje + medio de pago”.
- [ ] **Factura [P2]: Resumen de pagos por viaje para vendedores** — exponer (pantalla o sección en flujo existente) listado/resumen de pagos del viaje seleccionado, incluyendo **método de pago** (`TipoPagoDescripcion` / forma de pago), montos, cliente y fecha. Reutilizar lógica/SP de Admin (`GetPagosByViaje` / patrón ResumenPagos) con permisos de vendedor (solo sus viajes o todos según regla de negocio). Criterio: el vendedor encuentra el resumen sin entrar al panel Admin; MSBuild limpio; `ErrorUtil` en controller.

### Viaje — rentabilidad / gastos (P2)

Investigación: [`../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md`](../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md).  
Recomendación preliminar: híbrido A + B tras decisión de negocio.

- [ ] **Opción A:** `ViajeID` opcional en `FacturaFiscal` (Tipo=Compra) + UI + SP resumen
- [ ] **Opción B:** tabla `ViajeGasto` + CRUD + totales por SP
- [ ] **Opción C:** reemplazo acotado del flujo Planilla (solo si negocio lo pide)
- [ ] **Opción D:** costos sugeridos desde catálogo (borrador editable)
- [ ] **SP + pantalla rentabilidad por viaje** + Excel (patrón ResumenPagosExcel)

### Integration API (P3)

- [ ] **F0** — Inventario y contrato
- [ ] **F1** — Hiperdocumentación machine-readable
- [ ] **F2** — Autenticación API Key
- [ ] **F3** — REST read-only (MVP)
- [ ] **F4** — Webhooks salientes
- [ ] **F5** — Escrituras controladas (agent-safe)
- [ ] **F6** — Observabilidad y hardening

Detalle de criterios: sección correspondiente en `SPEC.md` (épica Integration API).

### Excursion UX (P3)

- [ ] **F0** — Investigación con usuarios y glosario
- [ ] **F1** — Propuesta de información (wireframe/copy)
- [ ] **F2** — Implementación listado + SP enriquecido
- [ ] **F3** — Alineación Create/Edit (opcional)

### Higiene de código (auditoría 2026-09-30)

- [ ] **`ex.Message` expuesto al usuario** (regla de `CLAUDE.md`): reemplazar por `ErrorUtil.LogAndGetPublicMessage` en `ExternalController.cs:28`, `FacturaFiscalController.cs:548/619/655`, `HabitacionController.cs:193`, `HomeController.cs:215/279`, `NuevaReservaController.cs:195`, `PersonaClienteController.cs:1197`, `PresupuestoController.cs:989`. Extender `tools/Verify-NetTiersMigration.ps1` para que detecte `Json(... ex.Message ...)` y `MsgError = e.Message`.
- [ ] **`Web.config` trackeado con credenciales reales** (`MAT_DEV.*` activas, producción comentadas, mismo servidor). Decidir: mover a transforms `Web.Debug/Release.config` o a archivo excluido del repo + rotar la contraseña. **Área crítica: confirmar antes de tocar.**
- [ ] **Warnings de build:** `ReservaController.cs:317/433` y `ViajeModel.cs:293` (CS0168 `ex` sin usar), `ViajeController.cs:18-22` (CS0105 usings duplicados), `ReservaController.cs:36` (CS1998 `async` sin `await`).

### Backlog UX Admin (opcional)

- [ ] Tabs o pills **Por viaje / Por fecha** entre ResumenPagos hermanos
- [ ] Paridad columnas ResumenPagos vs Pagos por fecha (recibo, vendedor, cancelar) — requiere SP
- [ ] Reemplazar MagicSearch en ResumenPagos por control más mantenible

---

## Ya hecho y verificado — no rehacer

- [x] NetTiers F0–F12 (migración; proyectos NetTiers eliminados del repo)
- [x] Modernización masiva de vistas BS5 / inventario vistas (doc en Archive)
- [x] Admin onboarding, buscador Ctrl+K, tours contextuales, reset onboarding
- [x] Admin Usuarios (roles, DataTables, modal BS5, ErrorLog/Logs en hub)
- [x] DistribucionCoche Fases 1–3 (acciones butaca; publicar SP por entorno)
- [x] Fix cambio butaca conserva badge H (publicar SP por entorno)
- [x] UX FacturaListByViajeID Pagado/Saldo (publicar SP por entorno)
- [x] Enlace intranet ↔ Admin: “Ir a la intranet” / “Ir al administrador” (`_LoginPartial`)
- [x] Admin ResumenPagos: KPIs, contexto viaje, DataTable robusta, numeral en layout Admin
- [x] Descuento/Recargo: labels ARS/USD, validación cliente/servidor, sin conversión

---

## Al publicar / al entregar

- [ ] Checklist de scripts `database/` aplicados en Dev / Staging / Prod
- [ ] Compilar `MAT.MVC` Debug sin errores nuevos antes de push
- [ ] Actualizar `PROGRESS.md` con fecha y archivos tocados
