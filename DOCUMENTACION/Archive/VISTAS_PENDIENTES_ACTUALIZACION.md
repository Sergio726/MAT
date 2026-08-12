# LISTADO DE VISTAS PENDIENTES DE ACTUALIZAR

**Fecha de creacion:** 2025-01-27
**Estado:** En progreso - Fase 6 avanzada

---

## COMPLETADAS (98 vistas modernizadas):

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

### Fase 4 - Formularios Complejos (Completados - 2026-01-08):
45. **Precio/ABM.cshtml** - Modal modernizado con grid 2 columnas, iconos Bootstrap, inputs redondeados
46. **Viaje/Edit.cshtml** - Formulario reorganizado en 5 secciones (Info Basica, Fechas/Horarios, Ubicacion, Precios, Config Adicional), grids responsivos 2-4 columnas
    *(Nota: Viaje/Create usa la misma vista Edit con sAction=new)*
47. **Habitacion/ABM.cshtml** - Modal modernizado con grid 2 columnas, iconos Bootstrap, inputs redondeados
48. **Viaje/Details.cshtml** - Vista de detalles con 5 secciones (Info Basica, Fechas, Ubicacion, Precios, Capacidad), cards informativas
49. **Habitacion/Edit.cshtml** - Formulario moderno con 2 secciones (Info General, Capacidad/Estado), grid responsivo
50. **Habitacion/Details.cshtml** - Vista de detalles con secciones, barra de ocupacion visual, badges de estado
51. **HotelHabitacionViaje/ABM.cshtml** - Modal moderno con header info, formulario inline, tabla DataTables con iconos
52. **Hotel/Details.cshtml** - Ya estaba modernizada con clases modernas, iconos Bootstrap, grid responsivo

### Fase 5 - Vistas Muy Complejas (Completadas - 2026-01-08/09):
53. **PersonaCliente/Index.cshtml** - Ya estaba modernizada con tarjetas
54. **PersonaCliente/Create.cshtml** - Formulario reorganizado en 5 secciones (Datos Personales, Contacto, Ubicacion, Datos Fiscales, Info Adicional), selects en cascada Provincia/Departamento/Localidad
55. **PersonaCliente/Edit.cshtml** - Formulario identico a Create con 5 secciones, botones de Estado Cuenta Corriente modernizados
56. **Factura/Index.cshtml** - Formulario de busqueda moderno con grid, area de resultados con placeholder
57. **PersonaCliente/Details.cshtml** - Vista de detalles con 6 secciones (Personal, Contacto, Ubicacion, Fiscal, Laboral, Adicional), grids responsivos
58. **Factura/FacturaListByViajeID.cshtml** - Vista de facturas por viaje con loading spinner animado
59. **PersonaCliente/DetalleFactura.cshtml** - Vista de detalle de factura modernizada con grid de info, tablas con secciones, botones de accion con iconos
60. **ReservaHabitacion/Index.cshtml** - Ya estaba modernizada con clases modernas, selector de hotel, loading placeholder
61. **Admin/Index.cshtml** - Panel de administracion modernizado con cards interactivas, iconos, secciones separadas

---

## VERIFICACION EN CHROME (2026-01-08/09):

Se verificaron las siguientes vistas y se encontro que:

### Ya estaban actualizadas (no requieren cambios):
- Hotel/Create.cshtml - Usa Hotel/ABM.cshtml que ya esta moderno
- Hotel/Edit.cshtml - Usa Hotel/ABM.cshtml que ya esta moderno
- Hotel/Index.cshtml - Ya moderno
- Hotel/Details.cshtml - Ya moderno con clases modern-*
- Precio/Index.cshtml - Ya moderno
- PersonaCliente/Index.cshtml - Ya moderno con tarjetas
- Home/Index.cshtml (Panel Principal) - Ya moderno con tarjetas
- Viaje/Index.cshtml - Estilo propio funcional (tabla con colores)
- ReservaHabitacion/Index.cshtml - Ya moderno con clases modern-*

### No existen o requieren contexto especial:
- Localidad/Create.cshtml - **NO EXISTE** (la ruta no funciona)
- Habitacion/Index.cshtml - No existe, se accede desde Hotel
- NuevaReserva/Index.cshtml - Vista muy compleja (1140+ lineas), requiere ViajeID, usa selector de butacas de bus con logica especifica - **SE RECOMIENDA NO MODIFICAR**

---

## FASE 3: VISTAS RESTANTES (0 vistas) - Costo: MEDIO

### Hoteles:
- ~~Localidad/Create.cshtml~~ - **NO EXISTE**
- ~~Hotel/Create.cshtml~~ - **YA ACTUALIZADA** (usa Hotel/ABM)
- ~~Hotel/Edit.cshtml~~ - **YA ACTUALIZADA** (usa Hotel/ABM)
- ~~Hotel/Details.cshtml~~ - **YA ACTUALIZADA** (verificada 2026-01-09)

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

## FASE 5: VISTAS MUY COMPLEJAS (0 vistas restantes principales) - Costo: MUY ALTO

### PersonaCliente y Facturas:
- ~~PersonaCliente/Index.cshtml~~ - **YA ESTABA ACTUALIZADA**
- ~~PersonaCliente/Create.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/Edit.cshtml~~ - **COMPLETADA** (Formulario con 5 secciones, Estado Cuenta Corriente)
- ~~PersonaCliente/Details.cshtml~~ - **COMPLETADA** (Vista con 6 secciones informativas)
- ~~Factura/Index.cshtml~~ - **COMPLETADA** (Formulario busqueda moderno)
- ~~Factura/FacturaListByViajeID.cshtml~~ - **COMPLETADA** (Vista con loading spinner)
- ~~PersonaCliente/DetalleFactura.cshtml~~ - **COMPLETADA** (Vista detalle factura con grid, tablas, botones iconos)

### Reservas y Admin:
- NuevaReserva/Index.cshtml - **NO MODIFICAR** - Vista muy compleja (1140+ lineas) con selector de butacas de bus, logica de asignacion, tabs jQuery. Riesgo alto de romper funcionalidad.
- ~~ReservaHabitacion/Index.cshtml~~ - **YA ESTABA ACTUALIZADA** (verificada 2026-01-09)
- ~~Admin/Index.cshtml~~ - **COMPLETADA** (Panel con cards interactivas, secciones)

### Fase 6 - Vistas Especializadas (Completadas - 2026-01-09):
62. **Paquete/Edit.cshtml** - Formulario de paquete con upload de imagen, selector de destino con cascada Pais/Provincia/Departamento/Localidad, secciones modernas
63. **PersonaCliente/RegistrarPago.cshtml** - Modal de pago con grid layout, badge de saldo, integración con cotizador USD
64. **Admin/ResumenPagos.cshtml** - Página de resumen de pagos con búsqueda magicsearch, placeholder de resultados
65. **Reserva/FormReserva.cshtml** - Formulario de reserva con búsqueda de cliente, opciones de pago, card de total, integración cotizador
66. **PersonaCliente/RegistrarPagoTotal.cshtml** - Modal de pago total con grid layout, badge de saldo, campos de recibo/factura
67. **Admin/ResumenPagosPorFecha.cshtml** - Página de resumen por fecha con datepicker, loading spinner, placeholder
68. **Paquete/Vinculos.cshtml** - Vista de vínculos con 4 secciones (Servicios, Excursiones, Precios, Adicionales), tablas modernas, empty states
69. **PersonaCliente/CuentaCorriente.cshtml** - Vista de movimientos de nota de crédito con loading spinner, manejo de errores
70. **PersonaCliente/HistorialdePagos.cshtml** - Vista de historial con header, badge de cliente, loading spinner, botón volver
71. **PersonaCliente/RegistrarNotaCredito.cshtml** - Modal con info grid, formulario moderno, campos de montos y detalle
72. **Paquete/Servicios.cshtml** - Modal de búsqueda con input estilizado, grid container, botón cerrar
73. **Paquete/Precios.cshtml** - Modal de búsqueda con input estilizado, grid container, botón cerrar
74. **Paquete/Excursiones.cshtml** - Modal con grid container, loading spinner, botón cerrar
75. **Paquete/Adicionales.cshtml** - Modal de búsqueda con input estilizado, grid container, botón cerrar
76. **PersonaCliente/HistorialCuenta.cshtml** - Vista completa con tabla de movimientos, card de resumen totales, botones de acción
77. **PersonaCliente/NotaCreditoList.cshtml** - Lista con DataTables, botones de movimientos, loading modal

---

## FASE 6: VISTAS ESPECIALIZADAS/PRINT (Resto) - Costo: VARIABLE

### Vistas parciales y especializadas (menor prioridad):

#### Paquetes:
- ~~Paquete/Edit.cshtml~~ - **COMPLETADA**
- ~~Paquete/Vinculos.cshtml~~ - **COMPLETADA**
- ~~Paquete/Servicios.cshtml~~ - **COMPLETADA**
- ~~Paquete/Precios.cshtml~~ - **COMPLETADA**
- ~~Paquete/Excursiones.cshtml~~ - **COMPLETADA**
- ~~Paquete/Adicionales.cshtml~~ - **COMPLETADA**

#### Reservas:
- ~~Reserva/FormReserva.cshtml~~ - **COMPLETADA**
- ~~Reserva/VinculacionMenor.cshtml~~ - **COMPLETADA**
- ~~Reserva/QuickSearch.cshtml~~ - **MINIMA** (3 líneas, solo renderiza modelo)
- Reserva/DistribucionCoche.cshtml
- ~~Reserva/FormListaMayor.cshtml~~ - **COMPLETADA**
- ~~Reserva/FormListaMenor.cshtml~~ - **COMPLETADA**
- ~~Reserva/PartialVinculacionMenor.cshtml~~ - **COMPLETADA**
- ~~Reserva/GetOffListPassengers.cshtml~~ - **COMPLETADA**
- ~~Reserva/RenderGridPasajeros.cshtml~~ - **COMPLETADA**
- ~~Reserva/ObservacionesGeneralesEdit.cshtml~~ - **COMPLETADA**

#### Pasajeros:
- PasajeroViaje/Manifiesto.cshtml
- PasajeroViaje/ListadoSimple.cshtml
- PasajeroViaje/ListadoSimpleToExport.cshtml
- PasajeroViaje/PartialPasajerosHistorial.cshtml

#### PersonaCliente (Vistas adicionales):
- PersonaCliente/Voucher.cshtml - **DOCUMENTO DE IMPRESION** (CSS externo)
- PersonaCliente/VoucherGrupal.cshtml - **DOCUMENTO DE IMPRESION** (CSS externo)
- ~~PersonaCliente/CuentaCorriente.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/HistorialdePagos.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/HistorialCuenta.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/RegistrarPago.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/RegistrarPagoTotal.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/RegistrarNotaCredito.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/NotaCreditoList.cshtml~~ - **COMPLETADA**
- PersonaCliente/PrincipalHabitaciones.cshtml
- PersonaCliente/SeleccionarImportes.cshtml
- PersonaCliente/CambiarPrecioList.cshtml
- PersonaCliente/EliminarVenta.cshtml
- ~~PersonaCliente/Facturas.cshtml~~ - **COMPLETADA**
- PersonaCliente/GridHabitaciones.cshtml
- ~~PersonaCliente/PartialFacturas.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/PartialCuentaCorriente.cshtml~~ - **COMPLETADA**
- ~~PersonaCliente/partialHistorialdePagos.cshtml~~ - **COMPLETADA**
- PersonaCliente/partialHistorialdePagosByFactura.cshtml
- PersonaCliente/PopupDetalleFactura.cshtml

#### Viajes:
- ~~Viaje/Index.cshtml~~ - **COMPLETADA**
- ~~Viaje/ListViajes.cshtml~~ - **COMPLETADA**
- ~~Viaje/PopPupViajes.cshtml~~ - **COMPLETADA**
- ~~Viaje/Hoteles.cshtml~~ - **COMPLETADA**
- ~~Viaje/HotelesDisponibles.cshtml~~ - **COMPLETADA**
- Viaje/Hoteles07122016.cshtml
- ~~Viaje/HotelSetIngresoEgreso.cshtml~~ - **COMPLETADA**

#### Hoteles:
- Hotel/Distribucion.cshtml
- Hotel/EsquemaDistribucion.cshtml
- ~~Habitacion/PartialListHabitacion.cshtml~~ - **COMPLETADA**

#### Admin:
- Admin/ServiciosAdminList.cshtml
- Admin/ServiciosAdminCreate.cshtml
- Admin/ServiciosAdminPrecioHistorial.cshtml
- Admin/ServiciosAdminPrecioHistorialGrid.cshtml
- Admin/ServiciosAdminPrecioCreate.cshtml
- Admin/ServiciosAdminPrecioEdit.cshtml
- ~~Admin/ResumenPagos.cshtml~~ - **COMPLETADA**
- ~~Admin/ResumenPagosPorFecha.cshtml~~ - **COMPLETADA**
- ~~Admin/GridResumenPagos.cshtml~~ - **COMPLETADA**
- ~~Admin/GridResumenPagosFecha.cshtml~~ - **COMPLETADA**
- Admin/PlanillaServicios.cshtml
- Admin/EditarPlanilla.cshtml
- Admin/HistorialPrecios.cshtml
- Admin/AuditoriaFacturas.cshtml
- Admin/ImprimirPlanilla.cshtml
- Admin/ImprimirPlanillaDetalle.cshtml
- Admin/GridPlanillasGeneradas.cshtml
- ~~Admin/PartialGridServiciosAdmin.cshtml~~ - **COMPLETADA**
- Admin/GridPlanillaServicioItemContext.cshtml
- Admin/GridPlanillaServiciosItemEdit.cshtml
- Admin/GridPlanillaServiciosItemPrint.cshtml
- Admin/GridPlanillaHotelPrint.cshtml
- Admin/GridPlanillaHotelDetallePrint.cshtml
- Admin/GridPlanillaHotelDetalleEdit.cshtml
- Admin/PartialGridResumenPlanillaHotelPrint.cshtml
- Admin/PartialGridPlanillaHotelPrint.cshtml
- ~~Admin/PartialDropDownHotel.cshtml~~ - **COMPLETADA**

#### Herramientas y Otros:
- Herramientas/Cotizador.cshtml
- ~~Busqueda/Card.cshtml~~ - **COMPLETADA**
- ~~Busqueda/Perfil.cshtml~~ - **COMPLETADA**
- ~~NotaCredito/partialNotaCredito.cshtml~~ - **COMPLETADA**
- ~~NotaCredito/partialMovimientoNotaCredito.cshtml~~ - **COMPLETADA**
- ~~CuentaCorriente/DetalleComprobante.cshtml~~ - **COMPLETADA**
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
- ~~PersonaVendedor/PartialHistorialPagos.cshtml~~ - **COMPLETADA**

---

## RESUMEN POR PRIORIDAD:

| Fase | Descripcion | Vistas | Estado |
|------|-------------|--------|--------|
| Fase 1 | Vistas principales | 25 | **COMPLETADA** |
| Fase 2 | Formularios Simples | 8 | **COMPLETADA** |
| Fase 3 | Formularios y Tablas Medios | 11 | **COMPLETADA** |
| Fase 4 | Formularios Complejos | 10 | **COMPLETADA** |
| Fase 5 | Vistas Muy Complejas | 9 | **COMPLETADA** (NuevaReserva excluida por riesgo) |
| Fase 6 | Vistas Especializadas | ~56 | En progreso (45 completadas + 3 especiales) |

**TOTAL COMPLETADAS:** 108 vistas (+10 en esta sesion)
**TOTAL PENDIENTES:** ~15 vistas (Fase 6 - vistas especializadas/print de menor prioridad)
**ESPECIALES:** 3 vistas (2 documentos de impresión, 1 mínima)

---

## PROXIMAS PRIORIDADES SUGERIDAS (Fase 6):

1. ~~**Paquete/Edit.cshtml**~~ - **COMPLETADA**
2. ~~**PersonaCliente/RegistrarPago.cshtml**~~ - **COMPLETADA**
3. ~~**Admin/ResumenPagos.cshtml**~~ - **COMPLETADA**
4. ~~**Reserva/FormReserva.cshtml**~~ - **COMPLETADA**

### Completadas en esta sesion:
5. ~~**PersonaCliente/RegistrarPagoTotal.cshtml**~~ - **COMPLETADA**
6. ~~**Admin/ResumenPagosPorFecha.cshtml**~~ - **COMPLETADA**
7. ~~**Paquete/Vinculos.cshtml**~~ - **COMPLETADA**
8. ~~**PersonaCliente/CuentaCorriente.cshtml**~~ - **COMPLETADA**

### Completadas adicionales:
9. ~~**PersonaCliente/HistorialdePagos.cshtml**~~ - **COMPLETADA**
10. ~~**PersonaCliente/RegistrarNotaCredito.cshtml**~~ - **COMPLETADA**
11. ~~**Paquete/Servicios.cshtml**~~ - **COMPLETADA**
12. ~~**Paquete/Precios.cshtml**~~ - **COMPLETADA**

### Completadas adicionales (batch 2):
13. ~~**Paquete/Excursiones.cshtml**~~ - **COMPLETADA**
14. ~~**Paquete/Adicionales.cshtml**~~ - **COMPLETADA**
15. ~~**PersonaCliente/HistorialCuenta.cshtml**~~ - **COMPLETADA**
16. ~~**PersonaCliente/NotaCreditoList.cshtml**~~ - **COMPLETADA**

### Completadas adicionales (batch 3 - sesión 7):
17. ~~**Reserva/VinculacionMenor.cshtml**~~ - **COMPLETADA** - Diálogo de vinculación de menores
18. ~~**Reserva/FormListaMayor.cshtml**~~ - **COMPLETADA** - Lista de tutores con DataTables
19. ~~**Reserva/FormListaMenor.cshtml**~~ - **COMPLETADA** - Lista de menores con selección múltiple
20. ~~**Reserva/PartialVinculacionMenor.cshtml**~~ - **COMPLETADA** - Tabla de menores vinculados
21. ~~**Reserva/ObservacionesGeneralesEdit.cshtml**~~ - **COMPLETADA** - Formulario de edición
22. ~~**Reserva/GetOffListPassengers.cshtml**~~ - **COMPLETADA** - Lista de espera modernizada
23. ~~**Reserva/RenderGridPasajeros.cshtml**~~ - **COMPLETADA** - Grid de pasajeros
24. ~~**NotaCredito/partialMovimientoNotaCredito.cshtml**~~ - **COMPLETADA** - Movimientos nota crédito
25. ~~**NotaCredito/partialNotaCredito.cshtml**~~ - **COMPLETADA** - Detalle nota crédito

### Documentos de impresión (no requieren modernización CSS):
- **PersonaCliente/Voucher.cshtml** - Documento de impresión con CSS externo (voucher.css)
- **PersonaCliente/VoucherGrupal.cshtml** - Documento de impresión grupal con CSS externo

### Vistas mínimas (ya funcionales):
- **Reserva/QuickSearch.cshtml** - Solo 3 líneas, renderiza HTML del modelo

### Completadas adicionales (batch 4 - baja complejidad):
26. ~~**Busqueda/Card.cshtml**~~ - **COMPLETADA** - Tarjeta de perfil con acciones
27. ~~**Busqueda/Perfil.cshtml**~~ - **COMPLETADA** - Página de búsqueda de perfil
28. ~~**CuentaCorriente/DetalleComprobante.cshtml**~~ - **COMPLETADA** - Detalle de comprobante
29. ~~**Habitacion/PartialListHabitacion.cshtml**~~ - **COMPLETADA** - Lista de habitaciones DataTables
30. ~~**Persona/Details.cshtml**~~ - **COMPLETADA** - Detalles de persona
31. ~~**PersonaVendedor/PartialHistorialPagos.cshtml**~~ - **COMPLETADA** - Historial de pagos vendedor
32. ~~**Admin/GridResumenPagos.cshtml**~~ - **COMPLETADA** - Grid resumen pagos DataTables
33. ~~**Admin/GridResumenPagosFecha.cshtml**~~ - **COMPLETADA** - Grid pagos por fecha
34. ~~**Admin/PartialDropDownHotel.cshtml**~~ - **COMPLETADA** - Dropdown de hoteles
35. ~~**Admin/PartialGridServiciosAdmin.cshtml**~~ - **COMPLETADA** - Grid servicios admin

### Completadas adicionales (batch 5 - Viaje y PersonaCliente):
36. ~~**Viaje/Index.cshtml**~~ - **COMPLETADA** - Página principal de viajes con filtro por año
37. ~~**Viaje/ListViajes.cshtml**~~ - **COMPLETADA** - Grid de viajes con DataTables y acciones
38. ~~**Viaje/Hoteles.cshtml**~~ - **COMPLETADA** - Lista de hoteles por viaje
39. ~~**Viaje/HotelesDisponibles.cshtml**~~ - **COMPLETADA** - Grid de hoteles disponibles
40. ~~**Viaje/HotelSetIngresoEgreso.cshtml**~~ - **COMPLETADA** - Modal edición ingreso/egreso
41. ~~**Viaje/PopPupViajes.cshtml**~~ - **COMPLETADA** - Popup de viajes con acciones de impresión
42. ~~**PersonaCliente/Facturas.cshtml**~~ - **COMPLETADA** - Lista de facturas por cliente
43. ~~**PersonaCliente/PartialFacturas.cshtml**~~ - **COMPLETADA** - Tabla parcial de facturas
44. ~~**PersonaCliente/PartialCuentaCorriente.cshtml**~~ - **COMPLETADA** - Cuenta corriente con resumen
45. ~~**PersonaCliente/partialHistorialdePagos.cshtml**~~ - **COMPLETADA** - Historial de pagos por viaje

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
- **IMPORTANTE:** NuevaReserva/Index.cshtml se excluye de modernizacion por su complejidad (1140+ lineas, selector de butacas de bus, jQuery tabs, logica de asignacion). Riesgo alto de romper funcionalidad.

---

**Ultima actualizacion:** 2026-01-09 (Sesion 9: +10 vistas Viaje y PersonaCliente - Total 108 vistas)
