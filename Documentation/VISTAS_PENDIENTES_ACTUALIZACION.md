# LISTADO DE VISTAS PENDIENTES DE ACTUALIZAR

**Fecha de creación:** 2025-01-27  
**Estado:** En progreso - Fase 1 completada

---

## ✅ COMPLETADAS (25 vistas modernizadas):

1. ✅ Home/Index.cshtml
2. ✅ Home/ViajesPorFecha.cshtml
3. ✅ Reserva/Index.cshtml
4. ✅ Reserva/Observaciones.cshtml
5. ✅ Reserva/ObservacionesGenerales.cshtml
6. ✅ Reserva/ObservacionABM.cshtml
7. ✅ Account/Login.cshtml
8. ✅ Account/Register.cshtml
9. ✅ Account/RegistrarVendedor.cshtml
10. ✅ Account/Manage.cshtml
11. ✅ Account/_ChangePasswordPartial.cshtml
12. ✅ Account/_SetPasswordPartial.cshtml
13. ✅ Hotel/ABM.cshtml
14. ✅ Hotel/Index.cshtml
15. ✅ Paquete/Index.cshtml
16. ✅ Transporte/Index.cshtml
17. ✅ Servicio/Index.cshtml
18. ✅ Excursion/Index.cshtml
19. ✅ Adicional/Index.cshtml
20. ✅ Precio/Index.cshtml
21. ✅ PersonaPasajero/Index.cshtml
22. ✅ PersonaProveedor/Index.cshtml
23. ✅ PasajeroViaje/Index.cshtml
24. ✅ Cliente/Index.cshtml
25. ✅ Butaca/Index.cshtml

---

## 📋 FASE 2: FORMULARIOS SIMPLES (9 vistas) - Costo: BAJO

**Estimación:** 2-4 horas cada una  
**Total estimado:** ~25 horas

### Formularios Create/Edit básicos:

26. ⏳ **Servicio/Create.cshtml** - Formulario simple (3 campos)
27. ⏳ **Servicio/Edit.cshtml** - Formulario simple
28. ⏳ **Excursion/Create.cshtml** - Formulario simple (3 campos)
29. ⏳ **Excursion/Edit.cshtml** - Formulario simple
30. ⏳ **Adicional/Create.cshtml** - Formulario simple (2 campos)
31. ⏳ **Adicional/Edit.cshtml** - Formulario simple
32. ⏳ **Transporte/Create.cshtml** - Formulario simple
33. ⏳ **Transporte/Edit.cshtml** - Formulario simple
34. ⏳ **Transporte/Details.cshtml** - Vista de detalles

---

## 📋 FASE 3: FORMULARIOS Y TABLAS MEDIANOS (15 vistas) - Costo: MEDIO

**Estimación:** 3-5 horas cada una  
**Total estimado:** ~60 horas

### Personas y Butacas:

35. ⏳ **Persona/Index.cshtml** - Tabla con DataTables (ya tiene estructura parcial)
36. ⏳ **Persona/Create.cshtml** - Formulario medio
37. ⏳ **PersonaPasajero/Create.cshtml** - Formulario medio
38. ⏳ **PersonaPasajero/Edit.cshtml** - Formulario medio
39. ⏳ **PersonaPasajero/Details.cshtml** - Vista de detalles
40. ⏳ **PersonaProveedor/Create.cshtml** - Formulario medio
41. ⏳ **PersonaProveedor/Edit.cshtml** - Formulario medio
42. ⏳ **PersonaProveedor/Details.cshtml** - Vista de detalles
43. ⏳ **Butaca/Create.cshtml** - Formulario simple
44. ⏳ **Butaca/Edit.cshtml** - Formulario simple
45. ⏳ **Butaca/Details.cshtml** - Vista de detalles

### Localidad y Hoteles:

46. ⏳ **Localidad/Create.cshtml** - Formulario simple
47. ⏳ **Hotel/Create.cshtml** - Formulario medio
48. ⏳ **Hotel/Edit.cshtml** - Formulario medio
49. ⏳ **Hotel/Details.cshtml** - Vista de detalles

---

## 📋 FASE 4: FORMULARIOS COMPLEJOS (9 vistas) - Costo: ALTO

**Estimación:** 4-8 horas cada una  
**Total estimado:** ~50 horas

### ABM y Viajes:

50. ⏳ **Precio/ABM.cshtml** - Formulario ABM (modal/popup)
51. ⏳ **Habitacion/ABM.cshtml** - Formulario ABM
52. ⏳ **Habitacion/Edit.cshtml** - Formulario medio
53. ⏳ **Habitacion/Details.cshtml** - Vista de detalles
54. ⏳ **HotelHabitacionViaje/ABM.cshtml** - Formulario ABM
55. ⏳ **Viaje/Index.cshtml** - Tabla con filtros y carga dinámica AJAX
56. ⏳ **Viaje/Create.cshtml** - Formulario complejo
57. ⏳ **Viaje/Edit.cshtml** - Formulario complejo
58. ⏳ **Viaje/Details.cshtml** - Vista de detalles

---

## 📋 FASE 5: VISTAS MUY COMPLEJAS (9 vistas) - Costo: MUY ALTO

**Estimación:** 8+ horas cada una  
**Total estimado:** ~75 horas

### PersonaCliente y Facturas:

59. ⏳ **PersonaCliente/Index.cshtml** - Carga grid dinámicamente con AJAX (`RenderGridClientes`)
60. ⏳ **PersonaCliente/Create.cshtml** - Formulario muy complejo (400+ líneas, AJAX, validaciones)
61. ⏳ **PersonaCliente/Edit.cshtml** - Formulario muy complejo
62. ⏳ **PersonaCliente/Details.cshtml** - Vista de detalles compleja
63. ⏳ **Factura/Index.cshtml** - Búsqueda compleja con MagicSearch, múltiples filtros
64. ⏳ **Factura/FacturaListByViajeID.cshtml** - Vista compleja
65. ⏳ **PersonaCliente/DetalleFactura.cshtml** - Vista compleja

### Reservas y Admin:

66. ⏳ **NuevaReserva/Index.cshtml** - Vista muy compleja (1100+ líneas, múltiples secciones)
67. ⏳ **ReservaHabitacion/Index.cshtml** - Vista con filtros y lógica compleja
68. ⏳ **Admin/Index.cshtml** - Panel de administración con widgets especiales

---

## 📋 FASE 6: VISTAS ESPECIALIZADAS/PRINT (Resto) - Costo: VARIABLE

**Estimación:** Variable según complejidad  
**Total estimado:** ~100 horas

### Vistas parciales y especializadas (menor prioridad):

#### Paquetes:
- ⏳ Paquete/Edit.cshtml
- ⏳ Paquete/Vinculos.cshtml
- ⏳ Paquete/Servicios.cshtml
- ⏳ Paquete/Precios.cshtml
- ⏳ Paquete/Excursiones.cshtml
- ⏳ Paquete/Adicionales.cshtml

#### Reservas:
- ⏳ Reserva/FormReserva.cshtml
- ⏳ Reserva/VinculacionMenor.cshtml
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
- ⏳ Viaje/ListViajes.cshtml
- ⏳ Viaje/PopPupViajes.cshtml
- ⏳ Viaje/Hoteles.cshtml
- ⏳ Viaje/HotelesDisponibles.cshtml
- ⏳ Viaje/Hoteles07122016.cshtml
- ⏳ Viaje/HotelSetIngresoEgreso.cshtml

#### Hoteles:
- ⏳ Hotel/Distribucion.cshtml
- ⏳ Hotel/EsquemaDistribucion.cshtml
- ⏳ Habitacion/PartialListHabitacion.cshtml

#### Admin:
- ⏳ Admin/ServiciosAdminList.cshtml
- ⏳ Admin/ServiciosAdminCreate.cshtml
- ⏳ Admin/ServiciosAdminPrecioHistorial.cshtml
- ⏳ Admin/ServiciosAdminPrecioHistorialGrid.cshtml
- ⏳ Admin/ServiciosAdminPrecioCreate.cshtml
- ⏳ Admin/ServiciosAdminPrecioEdit.cshtml
- ⏳ Admin/ResumenPagos.cshtml
- ⏳ Admin/ResumenPagosPorFecha.cshtml
- ⏳ Admin/GridResumenPagos.cshtml
- ⏳ Admin/GridResumenPagosFecha.cshtml
- ⏳ Admin/PlanillaServicios.cshtml
- ⏳ Admin/EditarPlanilla.cshtml
- ⏳ Admin/HistorialPrecios.cshtml
- ⏳ Admin/AuditoriaFacturas.cshtml
- ⏳ Admin/ImprimirPlanilla.cshtml
- ⏳ Admin/ImprimirPlanillaDetalle.cshtml
- ⏳ Admin/GridPlanillasGeneradas.cshtml
- ⏳ Admin/PartialGridServiciosAdmin.cshtml
- ⏳ Admin/GridPlanillaServicioItemContext.cshtml
- ⏳ Admin/GridPlanillaServiciosItemEdit.cshtml
- ⏳ Admin/GridPlanillaServiciosItemPrint.cshtml
- ⏳ Admin/GridPlanillaHotelPrint.cshtml
- ⏳ Admin/GridPlanillaHotelDetallePrint.cshtml
- ⏳ Admin/GridPlanillaHotelDetalleEdit.cshtml
- ⏳ Admin/PartialGridResumenPlanillaHotelPrint.cshtml
- ⏳ Admin/PartialGridPlanillaHotelPrint.cshtml
- ⏳ Admin/PartialDropDownHotel.cshtml

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
| ✅ Completadas | Vistas modernizadas | 25 | - | ✅ |
| ⏳ Fase 2 | Formularios Simples | 9 | ~25 horas | 🔴 Alta |
| ⏳ Fase 3 | Formularios y Tablas Medios | 15 | ~60 horas | 🟠 Media |
| ⏳ Fase 4 | Formularios Complejos | 9 | ~50 horas | 🟡 Media-Baja |
| ⏳ Fase 5 | Vistas Muy Complejas | 9 | ~75 horas | 🟢 Baja |
| ⏳ Fase 6 | Vistas Especializadas | ~60 | ~100 horas | ⚪ Muy Baja |

**TOTAL PENDIENTES:** ~102 vistas  
**TOTAL HORAS ESTIMADAS:** ~310 horas

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

**Última actualización:** 2025-01-27

