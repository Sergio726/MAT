# LISTADO DE VISTAS PENDIENTES DE ACTUALIZAR

**Fecha de creación:** 2025-01-27  
**Estado:** En progreso — Fases 2 y 3 completadas; **Fase 4 completada en código** (auditoría 2026-04-07); P1 continúa en Fase 5–6.

> **2026-04-07 — Retiro de producto (SPEC):** se eliminaron del menú Admin y del código las pantallas **PlanillaServicios**, **PlanillasGeneradas** (lista por viaje), el wizard en sesión (`PartialGridServiciosAdmin`, `GridPlanillaServicioItemContext`, `GridPlanillasGeneradas`) y huérfanos asociados (`PartialDropDownHotel`, `GridPlanillaHotel`, `PartialResumenGridPlanillaHotel` en controller). **Siguen disponibles por URL directa** (uso interno): `EditarPlanilla`, `ImprimirPlanilla`, `ImprimirPlanillaDetalle` y partials de datos por `planillaid` / viaje.

---

## ✅ COMPLETADAS (75 vistas en tabla principal; otras fases también ✅):

| # | Vista | Fecha | Mejoras Aplicadas |
|---|-------|-------|-------------------|
| 1 | Home/Index.cshtml | 2025-01-27 | Dashboard moderno con cards, iconos Bootstrap, tooltips |
| 2 | Home/ViajesPorFecha.cshtml | 2025-01-27 | Calendario y lista de viajes con diseño moderno |
| 3 | Reserva/Index.cshtml | 2025-01-27 | Distribución de butacas, modales modernos, badges de estado |
| 4 | Reserva/Observaciones.cshtml | 2025-01-27 | Modal moderno con header y acciones estilizadas |
| 5 | Reserva/ObservacionesGenerales.cshtml | 2025-01-27 | Tabla sin expand/collapse, badges fucsia, diseño limpio |
| 6 | Reserva/ObservacionABM.cshtml | 2025-01-27 | Formulario con grid responsive, iconos en labels |
| 7 | Account/Login.cshtml | 2025-01-27 | Diseño moderno, animación gradual, responsive |
| 8 | Account/Register.cshtml | 2025-01-27 | Formulario moderno con validaciones visuales |
| 9 | Account/RegistrarVendedor.cshtml | 2025-01-27 | Formulario moderno adaptado a móviles |
| 10 | Account/Manage.cshtml | 2025-01-27 | Panel de gestión con diseño consistente |
| 11 | Account/_ChangePasswordPartial.cshtml | 2025-01-27 | Formulario parcial modernizado |
| 12 | Account/_SetPasswordPartial.cshtml | 2025-01-27 | Formulario parcial modernizado |
| 13 | Hotel/ABM.cshtml | 2025-01-27 | Formulario con select de estrellas dinámico, grid responsive |
| 14 | Hotel/Index.cshtml | 2025-01-27 | Tabla moderna con DataTables, botones de acción |
| 15 | Paquete/Index.cshtml | 2025-01-27 | Tabla moderna con DataTables, iconos temáticos |
| 16 | Transporte/Index.cshtml | 2025-01-27 | Tabla moderna, badges para pasajeros, formato de km |
| 17 | Servicio/Index.cshtml | 2025-01-27 | Tabla moderna, badges de precios (pesos/dólares) |
| 18 | Excursion/Index.cshtml | 2025-01-27 | Tabla moderna, observaciones truncadas, badges de costos |
| 19 | Adicional/Index.cshtml | 2025-01-27 | Tabla moderna, badges de montos |
| 20 | Precio/Index.cshtml | 2025-01-27 | Tabla con datos JSON, badges de precios formateados |
| 21 | PersonaPasajero/Index.cshtml | 2025-01-27 | Tabla moderna, iconos de contacto, DataTables |
| 22 | PersonaProveedor/Index.cshtml | 2025-01-27 | Tabla moderna, enlaces a sitios web, iconos informativos |
| 23 | PasajeroViaje/Index.cshtml | 2025-01-27 | Tabla moderna, botones de impresión en header |
| 24 | Cliente/Index.cshtml | 2025-01-27 | Tabla simplificada, solo campos relevantes |
| 25 | Butaca/Index.cshtml | 2025-01-27 | Tabla moderna, filtro con dropdown, badges de coches |
| 26 | Servicio/Create.cshtml | 2025-01-27 | Formulario moderno, checkbox para transporte, validaciones mejoradas |
| 27 | Servicio/Edit.cshtml | 2025-01-27 | Formulario moderno, manejo de valores nullable, checkbox dinámico |
| 28 | Excursion/Create.cshtml | 2025-01-27 | Formulario moderno, textarea para observaciones |
| 29 | Excursion/Edit.cshtml | 2025-01-27 | Formulario moderno, manejo de valores nullable |
| 30 | Adicional/Create.cshtml | 2025-01-27 | Formulario moderno, validaciones JavaScript mejoradas, checkbox seguro menor |
| 31 | Adicional/Edit.cshtml | 2025-01-27 | Formulario moderno simplificado |
| 32 | Transporte/Create.cshtml | 2025-01-27 | Formulario moderno, inputs numéricos y fecha |
| 33 | Transporte/Edit.cshtml | 2025-01-27 | Formulario moderno, inputs numéricos y fecha |
| 34 | Transporte/Details.cshtml | 2025-01-27 | Vista de solo lectura con badges e iconos informativos |
| 35 | Persona/Index.cshtml | 2025-01-27 | Tabla moderna con DataTables, iconos Bootstrap, manejo de nullable |
| 36 | Persona/Create.cshtml | 2025-01-27 | Formulario moderno, inputs con máscaras, date picker |
| 37 | PersonaPasajero/Create.cshtml | 2025-01-27 | Formulario moderno, autocomplete de localidad, máscaras de documento |
| 38 | PersonaPasajero/Edit.cshtml | 2025-01-27 | Formulario moderno, valores prellenados, autocomplete integrado |
| 39 | PersonaPasajero/Details.cshtml | 2025-01-27 | Vista de detalles moderna, grid responsive, badges informativos |
| 40 | PersonaProveedor/Create.cshtml | 2025-01-27 | Formulario moderno, autocomplete doble (localidad personal/empresa) |
| 41 | PersonaProveedor/Edit.cshtml | 2025-01-27 | Formulario moderno, valores prellenados, autocomplete integrado |
| 42 | PersonaProveedor/Details.cshtml | 2025-01-27 | Vista de detalles moderna, secciones separadas (personal/empresa) |
| 43 | Butaca/Create.cshtml | 2025-01-27 | Formulario moderno, dropdowns para enums, validaciones |
| 44 | Butaca/Edit.cshtml | 2025-01-27 | Formulario moderno, valores prellenados, validaciones |
| 45 | Butaca/Details.cshtml | 2025-01-27 | Vista de detalles moderna, badges para estados y tipos |
| 46 | Localidad/Create.cshtml | 2025-01-27 | Formulario moderno, JavaScript para provincias/departamentos |
| 47 | Hotel/Create.cshtml | 2025-01-27 | Formulario moderno, autocomplete localidad, select de categoría |
| 48 | Hotel/Edit.cshtml | 2025-01-27 | Formulario moderno, valores prellenados, autocomplete integrado |
| 49 | Hotel/Details.cshtml | 2025-01-27 | Vista de detalles moderna, integración Google Maps, badges |
| 50 | PersonaCliente/Index.cshtml | 2025-01-27 | Rediseño completo con tarjetas, búsqueda en tiempo real, optimización SQL (TOP 10) |
| 51 | ReservaHabitacion/Index.cshtml | 2025-01-27 | Modal modernizado, formulario de selección hotel, diseño SaaS |
| 52 | Viaje/Create.cshtml | 2026-04-07 | `modern-page-container`, form grid, datepicker/timepicker, modelo no nulo en controller |
| 53 | ReservaHabitacion/ReservaHabitacion.cshtml | 2026-04-07 | Modal fechas/horas con `modern-form-input`, botón `btn-modern` |
| 54 | Reserva/SeleccionarPasajero.cshtml | 2026-04-07 | Botonera Continuar/Cancelar `btn-modern`; `preventDefault` en cancelar |
| 55 | Viaje/Hoteles.cshtml + desvincular | 2026-04-07 | Handler `.js-desvincular-hotel-viaje` (sin ids duplicados) |
| 56 | Viaje/Hoteles07122016.cshtml | 2026-04-07 | Tabla moderna; enlace a `Viaje/Hoteles` |
| 57 | ~~Admin/PlanillaServicios.cshtml~~ | 2026-04-07 | **Eliminada** (retiro menú planillas); ver nota arriba |
| 58 | ~~Admin/GridPlanillasGeneradas.cshtml~~ | 2026-04-07 | **Eliminada** |
| 59 | NuevaReserva/SeleccionarPasajero.cshtml | 2026-04-07 | Botón Asignar `btn-modern` |
| 60 | NuevaReserva/FormReserva.cshtml | 2026-04-07 | Aceptar/Cancelar `btn-modern` |
| 61 | NuevaReserva/Index.cshtml | 2026-04-07 | Títulos de widget con Bootstrap Icons (`nr-section-title`); Pagar `btn-modern-danger`; link Seleccionar en grid |
| 62 | Admin/ErrorLog.cshtml | 2026-04-07 | BS5: sin glyphicons; `bi-*`, `table-sm`, filtros `d-flex`, fetch/render sin cambios de contrato |
| 63 | Admin/Logs.cshtml | 2026-04-07 | BS5: formulario `d-flex`; `btn-outline-secondary`; iconos BI |
| 64 | Admin/EditarPlanilla.cshtml | 2026-04-07 | Card + grid; ids `#fecha-planilla`, `#total-planilla`, `#btn-guardar-planilla`; datepicker opcional si jQuery UI disponible |
| 65 | ~~Admin/GridPlanillaServicioItemContext.cshtml~~ | 2026-04-07 | **Eliminada** (wizard planilla) |
| 66 | Admin/ImprimirPlanilla.cshtml | 2026-04-07 | jQuery 3.7.1; encabezado etiquetado; fecha como fecha (corrección vs hora); mismos loads AJAX |
| 67 | Admin/ImprimirPlanillaDetalle.cshtml | 2026-04-07 | jQuery 3.7.1; encabezado tipografía consistente; mismos loads AJAX |
| 68 | Admin/GridPlanillaHotelDetalleEdit.cshtml | 2026-04-07 | Tabla BS5 `table-sm bordered`; mantiene `planilla-habitaciones`, `txt-dias`, `text-total-hotel`; total en flex (sin wrapper extra entre tabla y total) |
| 69 | Admin/GridPlanillaServiciosItemEdit.cshtml | 2026-04-07 | Razor válido (`Sum`); tabla `servicios`; inputs `form-control-sm`; total servicios coherente |
| 70 | Admin/GridPlanillaHotelDetallePrint.cshtml | 2026-04-07 | Tipografías BS5; tablas impresión + `mat-customtable` |
| 71 | Admin/GridPlanillaServiciosItemPrint.cshtml | 2026-04-07 | Total con `Sum`; formato moneda columnas |
| 72 | Admin/PartialGridResumenPlanillaHotelPrint.cshtml | 2026-04-07 | Mismo patrón impresión resumen hotel |
| 73 | Admin/GridPlanillaHotelPrint.cshtml | 2026-04-07 | Sin cambio de markup (solo `RenderAction` a resumen); verificado |
| 74 | ~~Admin/PartialGridServiciosAdmin.cshtml~~ | 2026-04-07 | **Eliminada** (modal wizard) |
| 75 | Admin: ResumenPagos suite (4 vistas) | 2026-04-07 | Verificado moderno; `GridResumenPagosFecha`: directiva model al inicio del archivo |

---

## 📋 FASE 2: FORMULARIOS SIMPLES (9 vistas) - Costo: BAJO ✅ COMPLETADA

**Estimación:** 2-4 horas cada una  
**Total estimado:** ~25 horas  
**Estado:** ✅ Completada el 2025-01-27

### Formularios Create/Edit básicos:

26. ✅ **Servicio/Create.cshtml** - Formulario simple (3 campos) - Completado 2025-01-27
27. ✅ **Servicio/Edit.cshtml** - Formulario simple - Completado 2025-01-27
28. ✅ **Excursion/Create.cshtml** - Formulario simple (3 campos) - Completado 2025-01-27
29. ✅ **Excursion/Edit.cshtml** - Formulario simple - Completado 2025-01-27
30. ✅ **Adicional/Create.cshtml** - Formulario simple (2 campos) - Completado 2025-01-27
31. ✅ **Adicional/Edit.cshtml** - Formulario simple - Completado 2025-01-27
32. ✅ **Transporte/Create.cshtml** - Formulario simple - Completado 2025-01-27
33. ✅ **Transporte/Edit.cshtml** - Formulario simple - Completado 2025-01-27
34. ✅ **Transporte/Details.cshtml** - Vista de detalles - Completado 2025-01-27

---

## 📋 FASE 3: FORMULARIOS Y TABLAS MEDIANOS (15 vistas) - Costo: MEDIO ✅ COMPLETADA

**Estimación:** 3-5 horas cada una  
**Total estimado:** ~60 horas  
**Estado:** ✅ Completada el 2025-01-27

### Personas y Butacas:

35. ✅ **Persona/Index.cshtml** - Tabla con DataTables - Completado 2025-01-27
36. ✅ **Persona/Create.cshtml** - Formulario medio - Completado 2025-01-27
37. ✅ **PersonaPasajero/Create.cshtml** - Formulario medio - Completado 2025-01-27
38. ✅ **PersonaPasajero/Edit.cshtml** - Formulario medio - Completado 2025-01-27
39. ✅ **PersonaPasajero/Details.cshtml** - Vista de detalles - Completado 2025-01-27
40. ✅ **PersonaProveedor/Create.cshtml** - Formulario medio - Completado 2025-01-27
41. ✅ **PersonaProveedor/Edit.cshtml** - Formulario medio - Completado 2025-01-27
42. ✅ **PersonaProveedor/Details.cshtml** - Vista de detalles - Completado 2025-01-27
43. ✅ **Butaca/Create.cshtml** - Formulario simple - Completado 2025-01-27
44. ✅ **Butaca/Edit.cshtml** - Formulario simple - Completado 2025-01-27
45. ✅ **Butaca/Details.cshtml** - Vista de detalles - Completado 2025-01-27

### Localidad y Hoteles:

46. ✅ **Localidad/Create.cshtml** - Formulario simple - Completado 2025-01-27
47. ✅ **Hotel/Create.cshtml** - Formulario medio - Completado 2025-01-27
48. ✅ **Hotel/Edit.cshtml** - Formulario medio - Completado 2025-01-27
49. ✅ **Hotel/Details.cshtml** - Vista de detalles - Completado 2025-01-27

---

## 📋 FASE 4: FORMULARIOS COMPLEJOS (9 vistas) - Costo: ALTO ✅ COMPLETADA (repositorio)

**Estimación:** 4-8 horas cada una  
**Total estimado:** ~50 horas  
**Estado:** ✅ Las 9 vistas figuran modernizadas en código — verificación 2026-04-07

### ABM y Viajes:

50. ✅ **Precio/ABM.cshtml** — Modal ABM `modern-form-*`, `btn-modern`; jQuery UI dialog solo para cerrar contenedor host — 2026-04-07
51. ✅ **Habitacion/ABM.cshtml** — Modal habitación con grid moderno — 2026-04-07
52. ✅ **Habitacion/Edit.cshtml** — Formulario card/grid con Bootstrap Icons — 2026-04-07
53. ✅ **Habitacion/Details.cshtml** — Vista detalle con badges y secciones — 2026-04-07
54. ✅ **HotelHabitacionViaje/ABM.cshtml** — ABM distribución, estilos variable CSS — 2026-04-07
55. ✅ **Viaje/Index.cshtml** — `viajes-page-container`, filtro año, carga AJAX de lista — 2026-04-07
56. ✅ **Viaje/Create.cshtml** — Patrón moderno completo — Completado 2026-04-07
57. ✅ **Viaje/Edit.cshtml** — Formulario complejo `modern-page` / secciones — 2026-04-07
58. ✅ **Viaje/Details.cshtml** — Detalle con `detail-section` / BI — 2026-04-07

---

## 📋 FASE 5: VISTAS MUY COMPLEJAS (9 vistas) - Costo: MUY ALTO

**Estimación:** 8+ horas cada una  
**Total estimado:** ~75 horas

### PersonaCliente y Facturas:

59. ✅ **PersonaCliente/Index.cshtml** - Rediseño completo con tarjetas, búsqueda optimizada (TOP 10), SP optimizado - Completado 2025-01-27
60. ⏳ **PersonaCliente/Create.cshtml** - Formulario muy complejo (400+ líneas, AJAX, validaciones)
61. ⏳ **PersonaCliente/Edit.cshtml** - Formulario muy complejo
62. ⏳ **PersonaCliente/Details.cshtml** - Vista de detalles compleja
63. ⏳ **Factura/Index.cshtml** - Búsqueda compleja con MagicSearch, múltiples filtros
64. ⏳ **Factura/FacturaListByViajeID.cshtml** - Vista compleja
65. ⏳ **PersonaCliente/DetalleFactura.cshtml** - Vista compleja

### Reservas y Admin:

66. ✅ **NuevaReserva/Index.cshtml** — Parcial 2026-04-07: encabezados `nr-section-title` + Bootstrap Icons; botón Pagar `btn-modern-danger`; enlace «Seleccionar» responsable menor con `btn-modern` (vista muy grande; lógica y widgets legacy restantes)
67. ✅ **ReservaHabitacion/Index.cshtml** - Modal modernizado con diseño SaaS, formulario optimizado - Completado 2025-01-27
68. ⏳ **Admin/Index.cshtml** - Panel de administración con widgets especiales

---

## 📋 FASE 6: VISTAS ESPECIALIZADAS/PRINT (Resto) - Costo: VARIABLE

**Estimación:** Variable según complejidad  
**Total estimado:** ~100 horas

### Vistas parciales y especializadas (menor prioridad):

#### Paquetes:
- ✅ Paquete/Edit.cshtml — Formulario `paquete-container`, modal destino custom, sin BS2/glyphicons — verificado 2026-04-07
- ✅ Paquete/Vinculos.cshtml — `vinculos-container`, tablas y botones alineados a diseño actual — 2026-04-07
- ✅ Paquete/Servicios.cshtml — Sin `glyphicon`/`btn-inverse` en vista — 2026-04-07
- ✅ Paquete/Precios.cshtml — Mismo criterio — 2026-04-07
- ✅ Paquete/Excursiones.cshtml — Mismo criterio — 2026-04-07
- ✅ Paquete/Adicionales.cshtml — Mismo criterio — 2026-04-07

#### NuevaReserva (flujo):
- ✅ NuevaReserva/Index.cshtml — Ver ítem 66 Fase 5 (parcial 2026-04-07)
- ✅ NuevaReserva/FormReserva.cshtml — Botones Aceptar/Cancelar `btn-modern` — 2026-04-07
- ✅ NuevaReserva/SeleccionarPasajero.cshtml — Botón Asignar `btn-modern` — 2026-04-07

#### ReservaHabitacion (vistas adicionales):
- ✅ ReservaHabitacion/ReservaHabitacion.cshtml — Modal fechas/horas `modern-form-input`, botón Reservar `btn-modern` — 2026-04-07 *(además de `ReservaHabitacion/Index` ya en tabla #51)*

#### Reservas:
- ✅ Reserva/FormReserva.cshtml — Flujo reserva con `reserva-container`, secciones BI, `btn-modern` donde aplica — verificado 2026-04-07 *(distinto de `NuevaReserva/FormReserva.cshtml`)*
- ⏳ Reserva/VinculacionMenor.cshtml
- ✅ Reserva/SeleccionarPasajero.cshtml — Botonera confirmar/cancelar con clases `btn-modern` (P1 parcial) — 2026-04-07
- ⏳ Reserva/QuickSearch.cshtml
- ⏳ Reserva/DistribucionCoche.cshtml
- ⏳ Reserva/FormListaMayor.cshtml
- ⏳ Reserva/FormListaMenor.cshtml
- ⏳ Reserva/PartialVinculacionMenor.cshtml
- ⏳ Reserva/GetOffListPassengers.cshtml

#### Pasajeros:
- ⏳ PasajeroViaje/Manifiesto.cshtml
- ⏳ PasajeroViaje/ListadoSimple.cshtml
- ⏳ PasajeroViaje/ListadoSimpleToExport.cshtml
- ⏳ PasajeroViaje/PartialPasajerosHistorial.cshtml

#### PersonaCliente (Vistas adicionales):
- ⏳ PersonaCliente/Voucher.cshtml
- ⏳ PersonaCliente/VoucherGrupal.cshtml
- ⏳ PersonaCliente/CuentaCorriente.cshtml
- ⏳ PersonaCliente/HistorialdePagos.cshtml
- ⏳ PersonaCliente/HistorialCuenta.cshtml
- ⏳ PersonaCliente/RegistrarPago.cshtml
- ⏳ PersonaCliente/RegistrarPagoTotal.cshtml
- ⏳ PersonaCliente/RegistrarNotaCredito.cshtml
- ⏳ PersonaCliente/NotaCreditoList.cshtml
- ⏳ PersonaCliente/PrincipalHabitaciones.cshtml
- ⏳ PersonaCliente/SeleccionarImportes.cshtml
- ⏳ PersonaCliente/CambiarPrecioList.cshtml
- ⏳ PersonaCliente/EliminarVenta.cshtml
- ⏳ PersonaCliente/Facturas.cshtml
- ⏳ PersonaCliente/GridHabitaciones.cshtml
- ⏳ PersonaCliente/PartialFacturas.cshtml
- ⏳ PersonaCliente/PartialCuentaCorriente.cshtml
- ⏳ PersonaCliente/partialHistorialdePagos.cshtml
- ⏳ PersonaCliente/partialHistorialdePagosByFactura.cshtml
- ⏳ PersonaCliente/PopupDetalleFactura.cshtml

#### Viajes:
- ✅ Viaje/ListViajes.cshtml — Partial lista con `viajes-list-container`, tipografía unificada — 2026-04-07
- ✅ Viaje/PopPupViajes.cshtml — `popup-viajes-*`, tabla moderna — 2026-04-07
- ✅ Viaje/Hoteles.cshtml — `hoteles-page-container`, acciones BI, `.js-desvincular-hotel-viaje` — 2026-04-07
- ✅ Viaje/HotelesDisponibles.cshtml — Tabla y `btn-vincular` actuales — 2026-04-07
- ✅ Viaje/Hoteles07122016.cshtml — Tabla y acciones modernas; desvincular vía `.js-desvincular-hotel-viaje`; enlace a `Viaje/Hoteles` — 2026-04-07
- ✅ Viaje/HotelSetIngresoEgreso.cshtml — Form grid ingreso/egreso con variables CSS — 2026-04-07

#### Hoteles:
- ⏳ Hotel/Distribucion.cshtml
- ⏳ Hotel/EsquemaDistribucion.cshtml
- ⏳ Habitacion/PartialListHabitacion.cshtml

#### Admin:
- ✅ Admin/ErrorLog.cshtml — P1/P2: panel tabla/diagnóstico con Bootstrap Icons y clases BS5 — 2026-04-07
- ✅ Admin/Logs.cshtml — Filtro correlation ID y vista pre en layout admin — 2026-04-07
- ⏳ Admin/ServiciosAdminList.cshtml
- ⏳ Admin/ServiciosAdminCreate.cshtml
- ⏳ Admin/ServiciosAdminPrecioHistorial.cshtml
- ⏳ Admin/ServiciosAdminPrecioHistorialGrid.cshtml
- ⏳ Admin/ServiciosAdminPrecioCreate.cshtml
- ⏳ Admin/ServiciosAdminPrecioEdit.cshtml
- ✅ Admin/ResumenPagos.cshtml — Panel búsqueda y layout actuales; verificado 2026-04-07
- ✅ Admin/ResumenPagosPorFecha.cshtml — Filtros card BS5; verificado 2026-04-07
- ✅ Admin/GridResumenPagos.cshtml — DataTables; verificado 2026-04-07
- ✅ Admin/GridResumenPagosFecha.cshtml — Tabla + cancelar pago; `@model` al inicio del archivo — 2026-04-07
- 🗑️ Admin/PlanillaServicios.cshtml — **Eliminada** 2026-04-07 (retiro menú planillas; SPEC)
- ✅ Admin/EditarPlanilla.cshtml — Card encabezado BS5, grid, botón Guardar `btn-danger` + icono BI; datepicker si existe — 2026-04-07
- ⏳ Admin/HistorialPrecios.cshtml
- ⏳ Admin/AuditoriaFacturas.cshtml
- ✅ Admin/ImprimirPlanilla.cshtml — Ventana impresión: jQuery 3.7.1, encabezado legible, fecha corregida — 2026-04-07
- ✅ Admin/ImprimirPlanillaDetalle.cshtml — Igual patrón — 2026-04-07
- 🗑️ Admin/GridPlanillasGeneradas.cshtml — **Eliminada** 2026-04-07
- 🗑️ Admin/PartialGridServiciosAdmin.cshtml — **Eliminada** 2026-04-07
- 🗑️ Admin/GridPlanillaServicioItemContext.cshtml — **Eliminada** 2026-04-07
- ✅ Admin/GridPlanillaServiciosItemEdit.cshtml — Tabla BS5; corrección bloque Razor; clases JS intactas — 2026-04-07
- ✅ Admin/GridPlanillaServiciosItemPrint.cshtml — Tipografía/formatos — 2026-04-07
- ✅ Admin/GridPlanillaHotelPrint.cshtml — Wrapper `RenderAction` sin UI propia; OK — 2026-04-07
- ✅ Admin/GridPlanillaHotelDetallePrint.cshtml — 2026-04-07
- ✅ Admin/GridPlanillaHotelDetalleEdit.cshtml — 2026-04-07
- ✅ Admin/PartialGridResumenPlanillaHotelPrint.cshtml — 2026-04-07
- ⏳ Admin/PartialGridPlanillaHotelPrint.cshtml *(archivo vacío en repo; sin modernizar hasta definir uso)*
- 🗑️ Admin/PartialDropDownHotel.cshtml — **Eliminada** 2026-04-07 (no referenciada; asociada a wizard retirado)

#### Herramientas y Otros:
- ⏳ Herramientas/Cotizador.cshtml
- ⏳ Busqueda/Card.cshtml
- ⏳ Busqueda/Perfil.cshtml
- ⏳ NotaCredito/partialNotaCredito.cshtml
- ⏳ NotaCredito/partialMovimientoNotaCredito.cshtml
- ⏳ CuentaCorriente/DetalleComprobante.cshtml
- ⏳ Home/HistorialPagos.cshtml
- ⏳ Home/TodosLosViajes.cshtml
- ⏳ Home/TodosLosViajesIndex.cshtml
- ⏳ Home/RenderGridHistorialPagos.cshtml
- ⏳ Shared/_ViajesPartial.cshtml
- ⏳ Shared/_Search.cshtml
- ⏳ Shared/_DetalleViaje.cshtml
- ⏳ Shared/Voucher.cshtml
- ⏳ Shared/Error.cshtml
- ⏳ Shared/_LayoutAdmin.cshtml
- ⏳ Shared/_LayoutSplash.cshtml
- ⏳ PersonaVendedor/PartialHistorialPagos.cshtml

---

## 📊 RESUMEN POR PRIORIDAD:

| Fase | Descripción | Vistas | Horas Estimadas | Prioridad |
|------|-------------|--------|-----------------|-----------|
| ✅ Completadas | Vistas modernizadas | 51 | - | ✅ |
| ✅ Fase 2 | Formularios Simples | 9 | ~25 horas | ✅ Completada |
| ✅ Fase 3 | Formularios y Tablas Medios | 15 | ~60 horas | ✅ Completada |
| ✅ Fase 4 | Formularios Complejos | 9 | ~50 horas | ✅ Código 2026-04-07 |
| ⏳ Fase 5 | Vistas Muy Complejas | 8 ítems (1 parcialmente hecho) | ~65 horas | 🟢 Baja |
| ⏳ Fase 6 | Vistas Especializadas | ~40+ ⏳ *(muchas marcadas ✅ en sublistas Viajes/Paquetes/NuevaReserva/Admin parcial)* | ~100 horas | ⚪ Muy Baja |

**TOTAL COMPLETADAS (tabla numerada #1–#61):** 61 filas en la tabla principal + Fase 4 ítem 50–58 ✅ + sublistas Fase 6 actualizadas (Paquetes, Viajes parcial, NuevaReserva, Planillas).  
**TOTAL PENDIENTES (aprox.):** ~55–65 vistas según lo que siga en ⏳ en Fase 5–6.  
**Avance global estimado (P1 según este doc):** **~59%** — resumen pagos admin auditado + fix orden Razor en GridResumenPagosFecha; conviene seguir auditando ⏳ restantes.  
**TOTAL HORAS ESTIMADAS:** pendiente recalcular al cerrar Fase 5–6

---

## 🎯 CRITERIOS DE PRIORIZACIÓN:

1. **Fase 2 (Alta Prioridad):** Formularios simples que se usan frecuentemente y son fáciles de modernizar.
2. **Fase 3 (Media Prioridad):** Formularios y tablas de uso frecuente con complejidad media.
3. **Fase 4 (Media-Baja Prioridad):** Formularios complejos con lógica especial pero importantes para el sistema.
4. **Fase 5 (Baja Prioridad):** Vistas muy complejas que requieren análisis profundo y refactorización.
5. **Fase 6 (Muy Baja Prioridad):** Vistas parciales, especializadas y de impresión que se pueden modernizar según necesidad.

---

## 📝 NOTAS:

- Las clases modernas utilizadas son: `.modern-page-container`, `.modern-page-header`, `.modern-page-title`, `.modern-form-container`, `.modern-table-container`, `.modern-table`, `.btn-modern`, etc.
- Se debe mantener la funcionalidad existente al modernizar.
- Todas las vistas deben usar variables CSS definidas en `saas-variables.css`.
- Se deben usar Bootstrap Icons para los iconos.
- DataTables debe estar configurado con traducción al español.
- Los formularios deben usar las clases modernas: `.modern-form-grid`, `.modern-form-group`, `.modern-form-label`, `.modern-form-input`, etc.

---

## 📐 PATRONES DE DISEÑO GENERALES

### Variables CSS (`saas-variables.css`)

Todas las vistas modernas utilizan variables CSS centralizadas para mantener consistencia:

```css
:root {
    --brand-primary: #e63375;      /* Fucsia del logo */
    --brand-dark: #2b2d42;         /* Azul/Gris oscuro */
    --bg-main: #f4f7f6;            /* Fondo general */
    --card-shadow: 0 10px 30px rgba(0, 0, 0, 0.05);
    --border-radius-lg: 16px;
    --text-primary: #2b2d42;
    --text-secondary: #6b7280;
    --text-muted: #9ca3af;
    --border-color: #e5e7eb;
    --white: #ffffff;
}
```

### Estructura de Páginas Index

Todas las páginas de listado (Index) siguen esta estructura estándar:

```html
<div class="modern-page-container">
    <div class="modern-page-header">
        <h2 class="modern-page-title">
            <i class="bi bi-[icono-tematico]"></i>
            <span>Título de la Página</span>
        </h2>
        <div class="modern-page-actions">
            <a href="/Controller/Create" class="btn-modern btn-modern-primary">
                <i class="bi bi-plus-circle"></i>
                <span>Agregar Nuevo</span>
            </a>
        </div>
    </div>

    @if (ViewBag.Error != null)
    {
        <div class="modern-alert-error">
            <i class="bi bi-exclamation-circle"></i>
            <span>@ViewBag.Error</span>
        </div>
    }

    <div class="modern-table-container">
        <table class="modern-table" id="tblNombre">
            <thead>
                <tr>
                    <th>Columna 1</th>
                    <th>Columna 2</th>
                    <th style="width: 100px;">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <!-- Filas de datos -->
            </tbody>
        </table>
    </div>
</div>
```

### Estructura de Formularios (Create/Edit)

Los formularios siguen esta estructura estándar:

```html
<div class="modern-page-container">
    <div class="modern-page-header">
        <h2 class="modern-page-title">
            <i class="bi bi-[icono-tematico]"></i>
            <span>@(Model.Id == null ? "Nuevo" : "Editar")</span>
        </h2>
    </div>

    <div class="modern-form-container">
        <form id="formNombre" class="modern-form">
            <div class="modern-form-grid">
                <div class="modern-form-group">
                    <label class="modern-form-label">
                        <i class="bi bi-[icono]"></i>
                        Nombre del Campo
                    </label>
                    <input type="text" name="Campo" class="modern-form-input" 
                           placeholder="Placeholder" required />
                </div>
                <!-- Más campos -->
            </div>

            <div class="modern-form-actions">
                <button type="button" class="btn-modern btn-modern-secondary" 
                        onclick="cancelar();">
                    <i class="bi bi-x-circle"></i>
                    <span>Cancelar</span>
                </button>
                <button type="submit" class="btn-modern btn-modern-primary">
                    <i class="bi bi-check-circle"></i>
                    <span>Guardar</span>
                </button>
            </div>
        </form>
    </div>
</div>
```

### Clases CSS Principales

#### Contenedores
- `.modern-page-container`: Contenedor principal de la página con fondo `--bg-main`
- `.modern-form-container`: Contenedor de formularios con card styling
- `.modern-table-container`: Contenedor de tablas con card styling

#### Headers
- `.modern-page-header`: Header con título y acciones
- `.modern-page-title`: Título principal con icono
- `.modern-page-actions`: Contenedor de botones de acción

#### Formularios
- `.modern-form-grid`: Grid responsive para campos (`grid-template-columns: repeat(auto-fit, minmax(280px, 1fr))`)
- `.modern-form-group`: Grupo de campo (label + input)
- `.modern-form-label`: Label con icono opcional
- `.modern-form-input`: Input estilizado
- `.modern-form-select`: Select estilizado
- `.modern-form-textarea`: Textarea estilizado
- `.modern-form-actions`: Contenedor de botones del formulario

#### Tablas
- `.modern-table`: Tabla moderna sin bordes internos
- `.modern-table-actions`: Contenedor de botones de acción en celdas
- `.modern-btn-icon`: Botón circular con icono (36x36px)

#### Botones
- `.btn-modern`: Botón base moderno
- `.btn-modern-primary`: Botón principal (fucsia `--brand-primary`)
- `.btn-modern-secondary`: Botón secundario (blanco con borde)
- `.modern-btn-icon-edit`: Variante azul para editar
- `.btn-danger-icon`: Variante roja para eliminar

#### Badges
- `.badge-pesos`: Badge verde para montos en pesos ($)
- `.badge-dolares`: Badge azul para montos en dólares (U$S)
- `.badge-pasajero`: Badge para información de pasajeros
- `.badge-categoria`: Badge fucsia para categorías

### Configuración DataTables Estándar

Todas las tablas utilizan esta configuración base:

```javascript
$(document).ready(function () {
    $("#tblNombre").DataTable({
        pageLength: 25,
        order: [[0, 'asc']],
        language: {
            search: "Buscar:",
            lengthMenu: "Mostrar _MENU_ registros",
            info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
            infoEmpty: "No hay registros disponibles",
            infoFiltered: "(filtrado de _MAX_ registros totales)",
            paginate: {
                first: "Primero",
                last: "Último",
                next: "Siguiente",
                previous: "Anterior"
            }
        },
        columnDefs: [
            { orderable: false, targets: [indice-columna-acciones] }
        ]
    });
});
```

### Iconos Bootstrap

**Regla:** Siempre usar Bootstrap Icons (`bi bi-[nombre-icono]`)

**Iconos comunes utilizados:**
- `bi-building`: Hoteles, edificios
- `bi-bus-front`: Transportes, coches
- `bi-people`: Pasajeros, clientes
- `bi-person-badge`: Clientes, personas
- `bi-box-seam`: Paquetes
- `bi-tag`: Precios
- `bi-geo-alt`: Excursiones, ubicaciones
- `bi-plus-circle`: Agregar nuevo
- `bi-pencil`: Editar
- `bi-eye`: Ver detalles
- `bi-trash`: Eliminar
- `bi-telephone`: Teléfono
- `bi-envelope`: Email
- `bi-globe`: Sitio web
- `bi-calendar3`: Fechas
- `bi-printer`: Imprimir

### Manejo de Valores Nullable

**Patrón para decimales nullable:**
```csharp
@if (item.Precio.HasValue)
{
    <span class="badge badge-pesos">$@item.Precio.Value.ToString("N2")</span>
}
else
{
    <span class="text-muted">-</span>
}
```

**Patrón para enteros no nullable:**
```csharp
@if (item.KmRecorridos > 0)
{
    <span>@item.KmRecorridos.ToString("N0") km</span>
}
else
{
    <span class="text-muted">-</span>
}
```

### Formato de Precios y Monedas

**Badges de moneda:**
- Pesos: `<span class="badge badge-pesos">$@precio.ToString("N2")</span>`
- Dólares: `<span class="badge badge-dolares">U$S@precio.ToString("N2")</span>`

**Detección de moneda:**
```csharp
@if (item.Moneda == "PES" || item.Moneda == "ARS")
{
    <span class="badge badge-pesos">$@item.Precio.Value.ToString("N2")</span>
}
else
{
    <span class="badge badge-dolares">U$S@item.Precio.Value.ToString("N2")</span>
}
```

---

## 📝 REGISTRO DE MEJORAS

| Fecha | Vista | Tipo | Descripción | Patrones Aplicados |
|-------|-------|------|-------------|-------------------|
| 2025-01-27 | Servicio/Index.cshtml | Index | Modernización completa de tabla, badges de precios, botones de acción | `.modern-page-container`, `.modern-table`, `.badge-pesos`, `.badge-dolares`, DataTables |
| 2025-01-27 | Excursion/Index.cshtml | Index | Modernización de tabla, observaciones truncadas, badges de costos | `.modern-page-container`, `.modern-table`, DataTables, tooltips |
| 2025-01-27 | Adicional/Index.cshtml | Index | Modernización de tabla, badges de montos | `.modern-page-container`, `.modern-table`, DataTables |
| 2025-01-27 | Precio/Index.cshtml | Index | Modernización de tabla con datos JSON, badges formateados | `.modern-page-container`, `.modern-table`, DataTables con `aaData` |
| 2025-01-27 | PersonaPasajero/Index.cshtml | Index | Modernización de tabla, iconos de contacto | `.modern-page-container`, `.modern-table`, Bootstrap Icons, DataTables |
| 2025-01-27 | PersonaProveedor/Index.cshtml | Index | Modernización de tabla, enlaces a sitios web | `.modern-page-container`, `.modern-table`, Bootstrap Icons, DataTables |
| 2025-01-27 | PasajeroViaje/Index.cshtml | Index | Modernización de tabla, botones de impresión en header | `.modern-page-container`, `.modern-page-actions`, DataTables |
| 2025-01-27 | Cliente/Index.cshtml | Index | Simplificación de tabla, solo campos relevantes | `.modern-page-container`, `.modern-table`, DataTables |
| 2025-01-27 | Butaca/Index.cshtml | Index | Modernización de tabla, filtro con dropdown, badges de coches | `.modern-page-container`, `.modern-form-select`, DataTables |
| 2025-01-27 | Transporte/Index.cshtml | Index | Modernización de tabla, formato de km, badges de pasajeros | `.modern-page-container`, `.modern-table`, manejo de int no nullable |
| 2025-01-27 | Servicio/Index.cshtml | Corrección | Corrección de manejo de decimales nullable | Validación `.HasValue` para `decimal?` |
| 2025-01-27 | Excursion/Index.cshtml | Corrección | Corrección de manejo de decimales nullable | Validación `.HasValue` para `decimal?` |
| 2025-01-27 | Adicional/Index.cshtml | Corrección | Corrección de manejo de decimales nullable | Validación `.HasValue` para `decimal?` |
| 2025-01-27 | Transporte/Index.cshtml | Corrección | Corrección de manejo de int no nullable | Validación `> 0` para `int` |
| 2025-01-27 | Servicio/Create.cshtml | Formulario | Modernización completa, checkbox para transporte, validaciones | `.modern-page-container`, `.modern-form-grid`, `.modern-form-group`, Bootstrap Icons, JavaScript para checkbox |
| 2025-01-27 | Servicio/Edit.cshtml | Formulario | Modernización completa, manejo de valores nullable, checkbox dinámico | `.modern-page-container`, `.modern-form-grid`, manejo de `HasValue`, JavaScript para checkbox |
| 2025-01-27 | Excursion/Create.cshtml | Formulario | Modernización completa, textarea para observaciones | `.modern-page-container`, `.modern-form-grid`, `.modern-form-textarea`, Bootstrap Icons |
| 2025-01-27 | Excursion/Edit.cshtml | Formulario | Modernización completa, manejo de valores nullable | `.modern-page-container`, `.modern-form-grid`, manejo de `HasValue` |
| 2025-01-27 | Adicional/Create.cshtml | Formulario | Modernización completa, validaciones mejoradas, checkbox seguro menor | `.modern-page-container`, `.modern-form-grid`, validaciones JavaScript mejoradas, clases de error |
| 2025-01-27 | Adicional/Edit.cshtml | Formulario | Modernización completa, formulario simplificado | `.modern-page-container`, `.modern-form-grid`, Bootstrap Icons |
| 2025-01-27 | Transporte/Create.cshtml | Formulario | Modernización completa, inputs numéricos y fecha | `.modern-page-container`, `.modern-form-grid`, `type="number"`, `type="date"`, Bootstrap Icons |
| 2025-01-27 | Transporte/Edit.cshtml | Formulario | Modernización completa, inputs numéricos y fecha | `.modern-page-container`, `.modern-form-grid`, `type="number"`, `type="date"`, Bootstrap Icons |
| 2025-01-27 | Transporte/Details.cshtml | Vista Detalles | Modernización completa, vista de solo lectura con badges | `.modern-page-container`, `.modern-form-container`, badges, iconos informativos, botón de editar en header |
| 2025-01-27 | Persona/Index.cshtml | Index | Modernización completa de tabla, DataTables, manejo de nullable | `.modern-page-container`, `.modern-table`, DataTables, validación nullable |
| 2025-01-27 | Persona/Create.cshtml | Formulario | Formulario moderno completo, máscaras de input, date picker | `.modern-page-container`, `.modern-form-grid`, máscaras jQuery, `type="date"` |
| 2025-01-27 | PersonaPasajero/Create.cshtml | Formulario | Formulario moderno, autocomplete localidad integrado | `.modern-page-container`, `.modern-form-grid`, autocomplete, máscaras |
| 2025-01-27 | PersonaPasajero/Edit.cshtml | Formulario | Formulario moderno, valores prellenados, autocomplete | `.modern-page-container`, `.modern-form-grid`, valores existentes |
| 2025-01-27 | PersonaPasajero/Details.cshtml | Vista Detalles | Vista de detalles moderna, grid responsive | `.modern-page-container`, `.modern-detail-grid`, `.modern-detail-value` |
| 2025-01-27 | PersonaProveedor/Create.cshtml | Formulario | Formulario moderno, autocomplete doble (personal/empresa) | `.modern-page-container`, `.modern-form-grid`, autocomplete múltiple |
| 2025-01-27 | PersonaProveedor/Edit.cshtml | Formulario | Formulario moderno, valores prellenados | `.modern-page-container`, `.modern-form-grid` |
| 2025-01-27 | PersonaProveedor/Details.cshtml | Vista Detalles | Vista de detalles con secciones (personal/empresa) | `.modern-page-container`, `.modern-detail-grid`, secciones separadas |
| 2025-01-27 | Butaca/Create.cshtml | Formulario | Formulario moderno, dropdowns para enums | `.modern-page-container`, `.modern-form-grid`, `.modern-form-select` |
| 2025-01-27 | Butaca/Edit.cshtml | Formulario | Formulario moderno, valores prellenados | `.modern-page-container`, `.modern-form-grid` |
| 2025-01-27 | Butaca/Details.cshtml | Vista Detalles | Vista de detalles con badges de estado | `.modern-page-container`, `.modern-detail-grid`, badges |
| 2025-01-27 | Localidad/Create.cshtml | Formulario | Formulario moderno, JavaScript provincias/departamentos | `.modern-page-container`, `.modern-form-grid`, JavaScript AJAX |
| 2025-01-27 | Hotel/Create.cshtml | Formulario | Formulario moderno, autocomplete localidad, textarea | `.modern-page-container`, `.modern-form-grid`, `.modern-form-textarea` |
| 2025-01-27 | Hotel/Edit.cshtml | Formulario | Formulario moderno, valores prellenados | `.modern-page-container`, `.modern-form-grid` |
| 2025-01-27 | Hotel/Details.cshtml | Vista Detalles | Vista de detalles con Google Maps integrado | `.modern-page-container`, `.modern-detail-grid`, integración mapas |
| 2025-01-27 | PersonaCliente/Index.cshtml | Index | Rediseño completo con tarjetas, búsqueda en tiempo real, optimización SQL | Sistema de tarjetas, búsqueda con debounce, SP optimizado `usp_MAT_PersonaCliente_GetTop`, TOP 10 |
| 2025-01-27 | ReservaHabitacion/Index.cshtml | Modal | Modal modernizado, formulario de selección hotel | `.modern-reserva-habitacion-container`, diseño SaaS, jQuery UI mejorado |
| 2025-01-27 | ReservaHabitacion/GridHotelHabitacion.cshtml | Vista Parcial | Tabla moderna de habitaciones, badges de disponibilidad | `.modern-table`, badges éxito/peligro, botones modernos |
| 2026-04-07 | Viaje/Hoteles07122016.cshtml | Lista legacy | Layout moderno; desvincular con clase compartida; enlace a Hoteles | `btn-modern`, `js-desvincular-hotel-viaje`, Bootstrap Icons |
| 2026-04-07 | Viaje/Hoteles.cshtml | Lista hoteles | Desvincular: mismo handler por clase (corrige IDs duplicados) | `.js-desvincular-hotel-viaje` |
| 2026-04-07 | ReservaHabitacion/ReservaHabitacion.cshtml | Modal | Formulario fechas/horas con `modern-form-input`, botón `btn-modern` | Sin cambios en AJAX `#btnReservarHabitacion` |
| 2026-04-07 | Reserva/SeleccionarPasajero.cshtml | Modal pasajeros | Botonera P1 sin BS2 `btn-inverse` | `btn-modern-primary` / `btn-modern-secondary` |
| 2026-04-07 | Admin/PlanillaServicios.cshtml | Admin | Botón agregar item: BS5 `btn-primary btn-sm` | Reemplazo `btn-inverse` |
| 2026-04-07 | Admin/GridPlanillasGeneradas.cshtml | Admin | Tabla envuelta en card; imprimir por `.js-imprimir-planilla-detalle`; sin IDs duplicados | `btn-modern`, fix handler en `mat.jquery.binding.js` |
| 2026-04-07 | NuevaReserva/SeleccionarPasajero.cshtml | Modal | Botón Asignar `btn-modern` | Quitar `btn-inverse`; typo title pajero→pasajero |
| 2026-04-07 | NuevaReserva/FormReserva.cshtml | Form pago | Aceptar/Cancelar `btn-modern`; corregida coma inválida en atributo class | P1 |
| 2026-04-07 | NuevaReserva/Index.cshtml | Wizard | Títulos de widget con Bootstrap Icons (`nr-section-title`); Pagar `btn-modern-danger`; link Seleccionar en tabla pasajeros | Sin glyphicons en esos `<h4>` |
| 2026-04-07 | Admin/ErrorLog.cshtml | Admin | Eliminados glyphicons y `table-condensed`/`form-inline` BS2; mismas funciones JS | Bootstrap Icons, `table-sm`, utilidades BS5 |
| 2026-04-07 | Admin/Logs.cshtml | Admin | Formulario inline → flex; botones outline BS5 | P1 |
| 2026-04-07 | Admin/EditarPlanilla.cshtml | Admin planilla | Reemplazo layout `.form` BS2 por card/grid BS5; mismo contrato AJAX GuardarDatosPlanilla | `#btn-guardar-planilla` sin `href` (evita salto); clase `date` en fecha |
| 2026-04-07 | Admin/GridPlanillaServicioItemContext.cshtml | Parcial admin | Botón generar con icono; mantiene `#btn-generar-planilla` | P1 |
| 2026-04-07 | — | Doc | Reserva/FormReserva.cshtml marcado ✅ (vista ya moderna; distinto de NuevaReserva/FormReserva) | Solo listas |
| 2026-04-07 | Admin/ImprimirPlanilla.cshtml | Impresión | jQuery 3.7.1; labels `<strong>`; `Fecha` con `ToShortDateString` (antes `ToShortTimeString`) | `mat.planillaprint.css` `.planilla-print-header` |
| 2026-04-07 | Admin/ImprimirPlanillaDetalle.cshtml | Impresión | Mismo alineamiento técnico | P1 |
| 2026-04-07 | Admin/GridPlanillaHotelDetalleEdit.cshtml | Planilla edición | BS5 sin romper cadenas `.parent()` del blur `.txt-dias` | `planilla-habitaciones`, `text-total-hotel` |
| 2026-04-07 | Admin/GridPlanillaServiciosItemEdit.cshtml | Planilla edición | Razor válido + `table.servicios` + totales | `cantidad-servicio-item`, `text-total-servicios` |
| 2026-04-07 | Admin/GridPlanilla*Print*.cshtml + PartialGridResumen* | Impresión / resumen | Tipografía y tablas; totales con `Sum` donde aplica | P1 |
| 2026-04-07 | Admin/PartialGridServiciosAdmin.cshtml + binding | Modal servicios planilla | Clase `.js-seleccionar-servicio-admin` reemplaza id duplicado | `mat.jquery.binding.js` |
| 2026-04-07 | Admin/GridResumenPagosFecha.cshtml | Resumen pagos | `@model` antes de bloque error | Orden Razor |
| 2026-04-07 | — | Doc | Cuatro vistas resumen pagos marcadas ✅ | Solo inventario |

**Total de mejoras registradas:** 57+ (ver filas anteriores en esta tabla)

---

**Última actualización:** 2026-04-07  
**Fase 2 completada:** 2025-01-27  
**Fase 3 completada:** 2025-01-27  
**Optimización PersonaCliente/Index:** 2025-01-27 (SP optimizado, tarjetas, búsqueda en tiempo real)  
**Modernización ReservaHabitacion:** 2025-01-27 (Modal SaaS, formulario optimizado)

