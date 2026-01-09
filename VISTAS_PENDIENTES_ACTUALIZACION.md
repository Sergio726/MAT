# LISTADO DE VISTAS PENDIENTES DE ACTUALIZAR

**Fecha de creacion:** 2025-01-27
**Estado:** En progreso - Fase 4 avanzada, Fase 5 avanzada

---

## COMPLETADAS (54 vistas modernizadas):

### Fase 1 - Completadas anteriormente:
1. Home/Index.cshtml
2. Home/ViajesPorFecha.cshtml
3. Reserva/Index.cshtml
4. Reserva/Observaciones.cshtml
5. Reserva/ObservacionesGenerales.cshtml
6. Reserva/ObservacionABM.cshtml
7. Account/Login.cshtml
8. Account/Register.cshtml
9. Account/RegistrarVendedor.cshtml
10. Account/Manage.cshtml
11. Account/_ChangePasswordPartial.cshtml
12. Account/_SetPasswordPartial.cshtml
13. Hotel/ABM.cshtml *(usado para Create y Edit)*
14. Hotel/Index.cshtml
15. Paquete/Index.cshtml
16. Transporte/Index.cshtml
17. Servicio/Index.cshtml
18. Excursion/Index.cshtml
19. Adicional/Index.cshtml
20. Precio/Index.cshtml
21. PersonaPasajero/Index.cshtml
22. PersonaProveedor/Index.cshtml
23. PasajeroViaje/Index.cshtml
24. Cliente/Index.cshtml
25. Butaca/Index.cshtml

### Fase 2 - Formularios Simples (Completados):
26. Servicio/Create.cshtml
27. Servicio/Edit.cshtml
28. Excursion/Create.cshtml
29. Excursion/Edit.cshtml
30. Adicional/Create.cshtml
31. Adicional/Edit.cshtml
32. Transporte/Create.cshtml
33. Transporte/Edit.cshtml
    *(Nota: Transporte/Details.cshtml fue eliminada)*

### Fase 3 - Formularios y Tablas Medios (Completados):
34. Persona/Index.cshtml
35. Persona/Create.cshtml
36. PersonaPasajero/Create.cshtml
37. PersonaPasajero/Edit.cshtml
38. PersonaPasajero/Details.cshtml
39. PersonaProveedor/Create.cshtml
40. PersonaProveedor/Edit.cshtml
41. PersonaProveedor/Details.cshtml
42. Butaca/Create.cshtml
43. Butaca/Edit.cshtml
44. Butaca/Details.cshtml

### Fase 4 - Formularios Complejos (Parcialmente completados - 2026-01-08):
45. **Precio/ABM.cshtml** - Modal modernizado con grid 2 columnas, iconos Bootstrap, inputs redondeados
46. **Viaje/Edit.cshtml** - Formulario reorganizado en 5 secciones (Info Basica, Fechas/Horarios, Ubicacion, Precios, Config Adicional), grids responsivos 2-4 columnas
    *(Nota: Viaje/Create usa la misma vista Edit con sAction=new)*

### Fase 4 - Completadas (2026-01-08 continuacion):
49. **Habitacion/ABM.cshtml** - Modal modernizado con grid 2 columnas, iconos Bootstrap, inputs redondeados
50. **Viaje/Details.cshtml** - Vista de detalles con 5 secciones (Info Basica, Fechas, Ubicacion, Precios, Capacidad), cards informativas
53. **Habitacion/Edit.cshtml** - Formulario moderno con 2 secciones (Info General, Capacidad/Estado), grid responsivo
54. **Habitacion/Details.cshtml** - Vista de detalles con secciones, barra de ocupacion visual, badges de estado
55. **HotelHabitacionViaje/ABM.cshtml** - Modal moderno con header info, formulario inline, tabla DataTables con iconos

### Fase 5 - Vistas Muy Complejas (Avanzada - 2026-01-08):
47. **PersonaCliente/Index.cshtml** - Ya estaba modernizada con tarjetas
48. **PersonaCliente/Create.cshtml** - Formulario reorganizado en 5 secciones (Datos Personales, Contacto, Ubicacion, Datos Fiscales, Info Adicional), selects en cascada Provincia/Departamento/Localidad
51. **PersonaCliente/Edit.cshtml** - Formulario identico a Create con 5 secciones, botones de Estado Cuenta Corriente modernizados
52. **Factura/Index.cshtml** - Formulario de busqueda moderno con grid, area de resultados con placeholder
56. **PersonaCliente/Details.cshtml** - Vista de detalles con 6 secciones (Personal, Contacto, Ubicacion, Fiscal, Laboral, Adicional), grids responsivos
57. **Factura/FacturaListByViajeID.cshtml** - Vista de facturas por viaje con loading spinner animado

---

## VERIFICACION EN CHROME (2026-01-08):

Se verificaron las siguientes vistas y se encontro que:

### Ya estaban actualizadas (no requieren cambios):
- Hotel/Create.cshtml - Usa Hotel/ABM.cshtml que ya esta moderno
- Hotel/Edit.cshtml - Usa Hotel/ABM.cshtml que ya esta moderno
- Hotel/Index.cshtml - Ya moderno
- Precio/Index.cshtml - Ya moderno
- PersonaCliente/Index.cshtml - Ya moderno con tarjetas
- Home/Index.cshtml (Panel Principal) - Ya moderno con tarjetas
- Viaje/Index.cshtml - Estilo propio funcional (tabla con colores)

### No existen o requieren contexto:
- Localidad/Create.cshtml - **NO EXISTE** (la ruta no funciona)
- Habitacion/Index.cshtml - No existe, se accede desde Hotel
- NuevaReserva/Index.cshtml - Requiere ID de viaje para acceder

---

## FASE 3: VISTAS RESTANTES (1 vista) - Costo: MEDIO

### Hoteles:
- ~~Localidad/Create.cshtml~~ - **NO EXISTE**
- ~~Hotel/Create.cshtml~~ - **YA ACTUALIZADA** (usa Hotel/ABM)
- ~~Hotel/Edit.cshtml~~ - **YA ACTUALIZADA** (usa Hotel/ABM)
- Hotel/Details.cshtml - Vista de detalles (pendiente verificar)

---

## FASE 4: FORMULARIOS COMPLEJOS (0 vistas restantes) - Costo: ALTO

### ABM y Viajes:
- ~~Precio/ABM.cshtml~~ - **COMPLETADA**
- ~~Habitacion/ABM.cshtml~~ - **COMPLETADA** (Modal con grid 2 columnas)
- ~~Habitacion/Edit.cshtml~~ - **COMPLETADA** (Formulario moderno 2 secciones)
- ~~Habitacion/Details.cshtml~~ - **COMPLETADA** (Vista con barra ocupacion)
- ~~HotelHabitacionViaje/ABM.cshtml~~ - **COMPLETADA** (Modal moderno con DataTables)
- Viaje/Index.cshtml - Tabla con filtros (estilo propio, funcional)
- ~~Viaje/Create.cshtml~~ - **COMPLETADA** (usa Viaje/Edit)
- ~~Viaje/Edit.cshtml~~ - **COMPLETADA**
- ~~Viaje/Details.cshtml~~ - **COMPLETADA** (Vista con 5 secciones informativas)

---

## FASE 5: VISTAS MUY COMPLEJAS (2 vistas restantes) - Costo: MUY ALTO

### PersonaCliente y Facturas:
- ~~PersonaCliente/Index.cshtml~~ - **YA ESTABA ACTUALIZADA**
- ~~PersonaCliente/Create.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/Edit.cshtml~~ - **COMPLETADA** (Formulario con 5 secciones, Estado Cuenta Corriente)
- ~~PersonaCliente/Details.cshtml~~ - **COMPLETADA** (Vista con 6 secciones informativas)
- ~~Factura/Index.cshtml~~ - **COMPLETADA** (Formulario busqueda moderno)
- ~~Factura/FacturaListByViajeID.cshtml~~ - **COMPLETADA** (Vista con loading spinner)
- PersonaCliente/DetalleFactura.cshtml - Vista compleja (pendiente)

### Reservas y Admin:
- NuevaReserva/Index.cshtml - Vista muy compleja (1100+ lineas, requiere ViajeID)
- ReservaHabitacion/Index.cshtml - Vista con filtros y logica compleja
- Admin/Index.cshtml - Panel de administracion (verificar si existe)

---

## FASE 6: VISTAS ESPECIALIZADAS/PRINT (Resto) - Costo: VARIABLE

### Vistas parciales y especializadas (menor prioridad):

#### Paquetes:
- Paquete/Edit.cshtml
- Paquete/Vinculos.cshtml
- Paquete/Servicios.cshtml
- Paquete/Precios.cshtml
- Paquete/Excursiones.cshtml
- Paquete/Adicionales.cshtml

#### Reservas:
- Reserva/FormReserva.cshtml
- Reserva/VinculacionMenor.cshtml
- Reserva/QuickSearch.cshtml
- Reserva/DistribucionCoche.cshtml
- Reserva/FormListaMayor.cshtml
- Reserva/FormListaMenor.cshtml
- Reserva/PartialVinculacionMenor.cshtml
- Reserva/GetOffListPassengers.cshtml

#### Pasajeros:
- PasajeroViaje/Manifiesto.cshtml
- PasajeroViaje/ListadoSimple.cshtml
- PasajeroViaje/ListadoSimpleToExport.cshtml
- PasajeroViaje/PartialPasajerosHistorial.cshtml

#### PersonaCliente (Vistas adicionales):
- PersonaCliente/Voucher.cshtml
- PersonaCliente/VoucherGrupal.cshtml
- PersonaCliente/CuentaCorriente.cshtml
- PersonaCliente/HistorialdePagos.cshtml
- PersonaCliente/HistorialCuenta.cshtml
- PersonaCliente/RegistrarPago.cshtml
- PersonaCliente/RegistrarPagoTotal.cshtml
- PersonaCliente/RegistrarNotaCredito.cshtml
- PersonaCliente/NotaCreditoList.cshtml
- PersonaCliente/PrincipalHabitaciones.cshtml
- PersonaCliente/SeleccionarImportes.cshtml
- PersonaCliente/CambiarPrecioList.cshtml
- PersonaCliente/EliminarVenta.cshtml
- PersonaCliente/Facturas.cshtml
- PersonaCliente/GridHabitaciones.cshtml
- PersonaCliente/PartialFacturas.cshtml
- PersonaCliente/PartialCuentaCorriente.cshtml
- PersonaCliente/partialHistorialdePagos.cshtml
- PersonaCliente/partialHistorialdePagosByFactura.cshtml
- PersonaCliente/PopupDetalleFactura.cshtml

#### Viajes:
- Viaje/ListViajes.cshtml
- Viaje/PopPupViajes.cshtml
- Viaje/Hoteles.cshtml
- Viaje/HotelesDisponibles.cshtml
- Viaje/Hoteles07122016.cshtml
- Viaje/HotelSetIngresoEgreso.cshtml

#### Hoteles:
- Hotel/Distribucion.cshtml
- Hotel/EsquemaDistribucion.cshtml
- Habitacion/PartialListHabitacion.cshtml

#### Admin:
- Admin/ServiciosAdminList.cshtml
- Admin/ServiciosAdminCreate.cshtml
- Admin/ServiciosAdminPrecioHistorial.cshtml
- Admin/ServiciosAdminPrecioHistorialGrid.cshtml
- Admin/ServiciosAdminPrecioCreate.cshtml
- Admin/ServiciosAdminPrecioEdit.cshtml
- Admin/ResumenPagos.cshtml
- Admin/ResumenPagosPorFecha.cshtml
- Admin/GridResumenPagos.cshtml
- Admin/GridResumenPagosFecha.cshtml
- Admin/PlanillaServicios.cshtml
- Admin/EditarPlanilla.cshtml
- Admin/HistorialPrecios.cshtml
- Admin/AuditoriaFacturas.cshtml
- Admin/ImprimirPlanilla.cshtml
- Admin/ImprimirPlanillaDetalle.cshtml
- Admin/GridPlanillasGeneradas.cshtml
- Admin/PartialGridServiciosAdmin.cshtml
- Admin/GridPlanillaServicioItemContext.cshtml
- Admin/GridPlanillaServiciosItemEdit.cshtml
- Admin/GridPlanillaServiciosItemPrint.cshtml
- Admin/GridPlanillaHotelPrint.cshtml
- Admin/GridPlanillaHotelDetallePrint.cshtml
- Admin/GridPlanillaHotelDetalleEdit.cshtml
- Admin/PartialGridResumenPlanillaHotelPrint.cshtml
- Admin/PartialGridPlanillaHotelPrint.cshtml
- Admin/PartialDropDownHotel.cshtml

#### Herramientas y Otros:
- Herramientas/Cotizador.cshtml
- Busqueda/Card.cshtml
- Busqueda/Perfil.cshtml
- NotaCredito/partialNotaCredito.cshtml
- NotaCredito/partialMovimientoNotaCredito.cshtml
- CuentaCorriente/DetalleComprobante.cshtml
- Home/HistorialPagos.cshtml
- Home/TodosLosViajes.cshtml
- Home/TodosLosViajesIndex.cshtml
- Home/RenderGridHistorialPagos.cshtml
- Shared/_ViajesPartial.cshtml
- Shared/_Search.cshtml
- Shared/_DetalleViaje.cshtml
- Shared/Voucher.cshtml
- Shared/Error.cshtml
- Shared/_LayoutAdmin.cshtml
- Shared/_LayoutSplash.cshtml
- PersonaVendedor/PartialHistorialPagos.cshtml

---

## RESUMEN POR PRIORIDAD:

| Fase | Descripcion | Vistas | Estado |
|------|-------------|--------|--------|
| Fase 1 | Vistas principales | 25 | COMPLETADA |
| Fase 2 | Formularios Simples | 8 | COMPLETADA |
| Fase 3 | Formularios y Tablas Medios | 11 | COMPLETADA (Hotel usa ABM, Localidad no existe) |
| Fase 4 | Formularios Complejos | 9 | **COMPLETADA** (9/9) |
| Fase 5 | Vistas Muy Complejas | 10 | 8 completadas, 2 pendientes |
| Fase 6 | Vistas Especializadas | ~60 | Pendiente |

**TOTAL COMPLETADAS:** 59 vistas (+5 en esta sesion)
**TOTAL PENDIENTES:** ~63 vistas

---

## PROXIMAS PRIORIDADES SUGERIDAS:

1. **PersonaCliente/DetalleFactura.cshtml** - Detalle de factura del cliente
2. **NuevaReserva/Index.cshtml** - Vista muy compleja (1100+ lineas)
3. **ReservaHabitacion/Index.cshtml** - Vista con filtros
4. **Hotel/Details.cshtml** - Vista de detalles de hotel
5. **Admin/Index.cshtml** - Panel de administracion

---

## NOTAS:

- Las clases modernas utilizadas son: `.modern-page-container`, `.modern-page-header`, `.modern-page-title`, `.modern-form-container`, `.modern-table-container`, `.modern-table`, `.btn-modern`, etc.
- Se debe mantener la funcionalidad existente al modernizar.
- Todas las vistas deben usar variables CSS definidas en `saas-variables.css`.
- Se deben usar Bootstrap Icons para los iconos.
- DataTables debe estar configurado con traduccion al espanol.
- Los formularios deben usar las clases modernas: `.modern-form-grid`, `.modern-form-group`, `.modern-form-label`, `.modern-form-input`, etc.
- **NUEVO:** Se agregaron clases `.modern-form-section` y `.modern-form-section-title` para organizar formularios largos en secciones.
- **NUEVO:** Se usan grids responsivos `.form-grid-2`, `.form-grid-3`, `.form-grid-4` con media queries para adaptarse a diferentes pantallas.

---

**Ultima actualizacion:** 2026-01-08 (Sesion 3: +5 vistas modernizadas - Total 59 vistas)
