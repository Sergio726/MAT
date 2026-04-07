# PROGRESS.md — Bitácora de desarrollo

---

### [2026-04-07] — Retiro Admin: PlanillaServicios / PlanillasGeneradas (SPEC)

- Archivos eliminados: `MAT.MVC/Views/Admin/PlanillaServicios.cshtml`, `GridPlanillasGeneradas.cshtml`, `GridPlanillaServicioItemContext.cshtml`, `PartialGridServiciosAdmin.cshtml`, `PartialDropDownHotel.cshtml`
- Archivos modificados: `MAT.MVC/Controllers/Admin/AdminController.cs` (eliminadas acciones del wizard en sesión, `PartialDropDownHotel`, `GridPlanillaHotel`, `PartialResumenGridPlanillaHotel`, `PlanillasGeneradas`, `GridPlanillasGeneradas`; `DeletePlanilla` → `RedirectToAction("Index","Admin")`), `MAT.MVC/Views/Shared/_LayoutAdmin.cshtml` (sin menú Planillas ni enlaces rotos), `MAT.MVC/Scripts/mat.jquery.binding.js` (handlers solo del wizard), `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Rutas `/Admin/PlanillaServicios` y `/Admin/PlanillasGeneradas` fuera de producto. Siguen **EditarPlanilla**, **ImprimirPlanilla**, **ImprimirPlanillaDetalle**, partials por `planillaid`, y `GuardarDatos*`.
- Problemas encontrados: Ninguno en compilación.
- Estado: ✅ MSBuild MAT.MVC Debug OK; SPEC ítem eliminación marcado `[x]`

### [2026-04-07] — Estado inicial del proyecto (baseline)

- Archivos auditados: `CLAUDE.md`, `SPEC.md`, `DOCUMENTACION\GUIA_SISTEMA_MAT.md`, `DOCUMENTACION\MAT_DB.md`, `DOCUMENTACION\RESUMEN_EJECUTIVO_MAT2026.md`, `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md`, `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md`
- Qué se implementó: Auditoría completa para establecer baseline; creación de CLAUDE.md actualizado, SPEC.md y PROGRESS.md
- Problemas encontrados:
  - Vistas pendientes de modernización identificadas (Bootstrap 5 / jQuery 3)
  - Deuda de librerías JS/CSS obsoletas documentada en `LIBRERIAS_OBSOLETAS_2026-01-04.md`
- Estado: ✅ baseline establecido — SPEC.md y PROGRESS.md creados

#### Resumen al 2026-04-07

**Completado y funcionando:**
- Gestión de reservas, viajes, pasajeros, paquetes, hoteles, transporte y excursiones
- Módulo de precios, clientes, vendedores y proveedores
- Facturación, cuenta corriente, pagos
- Módulo de presupuestos con seguimiento (ver `RESUMEN_EJECUTIVO_MAT2026.md`)
- Manejo de errores centralizado vía `ErrorUtil.LogAndGetPublicMessage`
- Logging de errores con `usp_MAT_ErrorLog_Insert`

**Pendiente (ver SPEC.md):**
- P1: Actualizar vistas pendientes de modernización a Bootstrap 5 / jQuery 3
- P2: (por definir con el equipo)
- P3: (por definir con el equipo)

---

### [2026-04-07] — P1 (avance): Viaje/Create modernizado

- Archivos modificados: `MAT.MVC/Views/Viaje/Create.cshtml`, `MAT.MVC/Controllers/Viaje/ViajeController.cs`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Reemplazo del layout legacy (formulario tipo BS2 `btn-inverse`, `.form` / `.form-label`) por contenedor `modern-page-container`, grid `modern-form-grid`, alertas `modern-alert-error`, botones `btn-modern`, iconos Bootstrap Icons; mismos names/POST y datepicker + timepicker que antes. `Create` GET y respuestas de error POST ahora devuelven `View(new MAT.Entities.Viaje())` para evitar modelo nulo y ambigüedad de tipo con el namespace del controller.
- Problemas encontrados: El listado de vistas pendientes tenía Fase 4 parcialmente desactualizada respecto al código (varias vistas ya estaban modernas); solo **Viaje/Create** requería cambio sustantivo en esta iteración.
- Estado: 🔄 P1 sigue abierto en SPEC — este commit avanza un ítem de Fase 4; MSBuild MAT.MVC OK

### [2026-04-07] — P1 (avance): hoteles víaje, reserva habitación, selección pasajeros, planilla

- Archivos modificados: `MAT.MVC/Scripts/mat.jquery.binding.js`, `MAT.MVC/Views/Viaje/Hoteles.cshtml`, `MAT.MVC/Views/Viaje/Hoteles07122016.cshtml`, `MAT.MVC/Views/ReservaHabitacion/ReservaHabitacion.cshtml`, `MAT.MVC/Views/Reserva/SeleccionarPasajero.cshtml`, `MAT.MVC/Views/Admin/PlanillaServicios.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Handler de desvincular hotel pasó de `#btn-desvincular-hotel` a `.js-desvincular-hotel-viaje` (varias filas tenían el mismo id). `Hoteles07122016.cshtml` modernizado; cancelar en `SeleccionarPasajero` con `preventDefault` al usar `href="#"`. Formulario modal `ReservaHabitacion.cshtml` con inputs/botón modernos manteniendo `#btnReservarHabitacion`. `PlanillaServicios`: botón BS5 `btn-primary btn-sm` en lugar de `btn-inverse`.
- Problemas encontrados: Ninguno bloqueante; vista `Hoteles07122016` sigue siendo legacy respecto al flujo principal (`Hoteles.cshtml`).
- Estado: 🔄 P1 abierto; MSBuild MAT.MVC OK

### [2026-04-07] — P1: GridPlanillasGeneradas + NuevaReserva SeleccionarPasajero

- Archivos modificados: `MAT.MVC/Views/Admin/GridPlanillasGeneradas.cshtml`, `MAT.MVC/Scripts/mat.jquery.binding.js`, `MAT.MVC/Views/NuevaReserva/SeleccionarPasajero.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Impresión detalle planilla delegada por **clase** `.js-imprimir-planilla-detalle` (evita múltiples elementos con el mismo `id`). Tabla de planillas con contenedor y celdas alineadas al patrón moderno. Botón **Asignar** en selección de pasajeros (NuevaReserva) con `btn-modern`. Doc: línea de avance global **~46%** sobre inventario ~127.
- Problemas encontrados: Ninguno.
- Estado: 🔄 P1 abierto; MSBuild verificado en esta tanda

### [2026-04-07] — P1: NuevaReserva FormReserva + Index

- Archivos modificados: `MAT.MVC/Views/NuevaReserva/FormReserva.cshtml`, `MAT.MVC/Views/NuevaReserva/Index.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Botones Aceptar/Cancelar del formulario de pago con `btn-modern`; encabezados de secciones del wizard con iconos `bi-*` y clase `nr-section-title` (reemplazo de `heading glyphicons`); botón Pagar con `btn-modern-danger`; enlace dinámico "Seleccionar" responsable menor con clases modernas. Avance doc P1 actualizado a **~48%**.
- Estado: MSBuild MAT.MVC OK

### [2026-04-07] — Documentación: sincronizar listas VISTAS_PENDIENTES con código

- Archivos modificados: `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Marcado **✅** en listas (Fase 4 completa en repo, Fase 5 NuevaReserva parcial, Fase 6 Paquetes/Viajes/NuevaReserva/Admin parcial). Tabla principal ampliada a filas **#52–#61**. Resumen global **~52%** y Fase 4 como completada en tabla de prioridades. Nota: `VISTAS_PENDIENTES_ACTUALIZACION.md` en la raíz del repo no se tocó (formato distinto).
- Estado: ✅ documentación al día para handoff

### [2026-04-07] — P1 (+ SPEC P2 cumplido): Admin ErrorLog y Logs a Bootstrap 5

- Archivos modificados: `MAT.MVC/Views/Admin/ErrorLog.cshtml`, `MAT.MVC/Views/Admin/Logs.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md` (tabla #62–#63, sección Admin, registro), `SPEC.md` (task P2 ErrorLog/Logs → `[x]`), `PROGRESS.md`
- Qué se implementó: Eliminación de **glyphicons** y clases BS2 (`form-inline`, `table-condensed`, `btn-default` tabla). **ErrorLog**: botones/outline BS5, iconos `bi-*`, tabla `table-sm table-bordered table-hover`, filas `table-danger`/`table-warning`; **fetch**, `renderizarTabla`, `copiarTodo`, filtros JSON sin cambio de contrato; filas stack y copiar fila conservados. **Logs**: layout `d-flex`, `btn-outline-secondary`, iconos en Filtrar / texto plano.
- Problemas encontrados: Ninguno.
- Estado: MSBuild MAT.MVC OK; P2 ítem ErrorLog/Logs marcado completado en SPEC (coincide con criterio del task)

### [2026-04-07] — P1: Admin EditarPlanilla + GridPlanillaServicioItemContext; doc Reserva/FormReserva

- Archivos modificados: `MAT.MVC/Views/Admin/EditarPlanilla.cshtml`, `MAT.MVC/Views/Admin/GridPlanillaServicioItemContext.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: **EditarPlanilla** con contenedor/card, grid de campos, Bootstrap Icons en título y botón Guardar; conservados `#viaje-id`, `#planilla-id`, `#fecha-planilla`, `#total-planilla`, `#btn-guardar-planilla` y los mismos `load` a GridPlanillaHotelDetalleEdit / GridPlanillaServiciosItemEdit; init opcional de datepicker si existe plugin. **GridPlanillaServicioItemContext**: icono en “Generar planilla”, `#btn-generar-planilla` sin cambios de id. Documentación: filas **#64–#65**, Reserva/FormReserva como ✅, avance global **~54%**.
- Problemas encontrados: Ninguno.
- Estado: 🔄 P1 sigue `[ ]` en SPEC hasta cierre global; MSBuild MAT.MVC OK

### [2026-04-07] — P1: Impresión planilla (ImprimirPlanilla / ImprimirPlanillaDetalle)

- Archivos modificados: `MAT.MVC/Views/Admin/ImprimirPlanilla.cshtml`, `MAT.MVC/Views/Admin/ImprimirPlanillaDetalle.cshtml`, `MAT.MVC/Content/mat.planillaprint.css`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Sustitución de **jQuery 1.8.2** por **3.7.1** en vistas parciales abiertas en nueva ventana; encabezado con etiquetas `<strong>` y clase `planilla-print-header`; en **ImprimirPlanilla** la línea “Fecha” pasa de `ToShortTimeString()` a **`ToShortDateString()`** (corrección respecto al rótulo). CSS: margen tipográfico para párrafos del encabezado. Sin cambios en URLs de `.load()` ni contrato de grids.
- Problemas encontrados: Ninguno.
- Estado: ✅ MSBuild MAT.MVC OK

### [2026-04-07] — P1: Resumen pagos Admin (auditoría + orden Razor)

- Archivos modificados: `MAT.MVC/Views/Admin/GridResumenPagosFecha.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: **`@model`** movido a la primera línea en **GridResumenPagosFecha** (buena práctica Razor). Documentación: **ResumenPagos**, **ResumenPagosPorFecha**, **GridResumenPagos**, **GridResumenPagosFecha** marcadas ✅ como ya modernas; tabla principal fila **#75**; avance **~59%**.
- Problemas encontrados: Ninguno.
- Estado: ✅ MSBuild MAT.MVC OK

### [2026-04-07] — P1: PartialGridServiciosAdmin — id duplicado → clase

- Archivos modificados: `MAT.MVC/Views/Admin/PartialGridServiciosAdmin.cshtml`, `MAT.MVC/Scripts/mat.jquery.binding.js`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Cada fila del grid usaba el mismo `id="btn-seleccionar-servicio-admin"`; reemplazado por **`class="js-seleccionar-servicio-admin"`** y delegación `$(document).on("click", ".js-...")` con **`preventDefault`**. Doc: fila **#74**, **PartialDropDownHotel** marcado verificado, avance **~58%**.
- Problemas encontrados: Ninguno.
- Estado: ✅ MSBuild MAT.MVC OK

### [2026-04-07] — P1: Grids planilla edición e impresión (Admin)

- Archivos modificados: `MAT.MVC/Views/Admin/GridPlanillaHotelDetalleEdit.cshtml`, `GridPlanillaServiciosItemEdit.cshtml`, `GridPlanillaHotelDetallePrint.cshtml`, `GridPlanillaServiciosItemPrint.cshtml`, `PartialGridResumenPlanillaHotelPrint.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Tablas con Bootstrap 5 (`table-sm`, `table-bordered`, `form-control-sm`, encabezados) conservando **contrato JS** en `mat.jquery.binding.js` (`planilla-habitaciones`, `txt-dias`, `text-total-hotel`, `table.servicios`, `item-id`, `cantidad-servicio-item`, `text-total-servicios`). **GridPlanillaServiciosItemEdit**: Razor válido (antes HTML dentro de `@{ }`) y total con `Sum`. Vistas impresión/resumen: totales con `Sum` y listas null-safe. Doc: filas **#68–#73**, avance **~57%**; `PartialGridPlanillaHotelPrint.cshtml` sigue vacío.
- Problemas encontrados: Ninguno.
- Estado: ✅ MSBuild MAT.MVC OK
