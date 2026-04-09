# PROGRESS.md — Bitácora de desarrollo

---

### [2026-04-08] — Admin: exportar resumen de pagos por viaje a Excel (P3 SPEC)

- Archivos modificados: `MAT.MVC/Infrastructure/AdminResumenPagosExcelExport.cs` (nuevo), `MAT.MVC/Controllers/Admin/AdminController.cs` (ya tenía `GridResumenPagos` + `ResumenPagosExcel`), `MAT.MVC/Views/Admin/GridResumenPagos.cshtml`, `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: clase estática EPPlus con hoja "Pagos", título + Viaje ID, cabecera #4472C4, columnas alineadas a la grilla (Cliente, Fecha, Tipo de pago, Nro. transacción, Monto), fila Total; GET `ResumenPagosExcel` con `[Authorize]` y `RequireAdministrador()`; `ViewBag.ViajeIdResumen` en el partial; botón BS5 "Exportar a Excel" (`target="_blank"`).
- Problemas encontrados: ninguno.
- Estado: ✅ MSBuild MAT.MVC Debug OK; task SPEC P3 marcado `[x]`

### [2026-04-08] — P1: modernización vistas pendientes (lote Reserva Fase 6)

- Archivos modificados: `MAT.MVC/Models/SearchModel.cs`, `MAT.MVC/Controllers/Reserva/ReservaController.cs`, `MAT.MVC/Views/Reserva/QuickSearch.cshtml`, `MAT.MVC/Views/Reserva/PartialVinculacionMenor.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: grid de búsqueda rápida pasajeros con clases BS5 en `GridView`; HTML devuelto por `QuickSearch()` envuelto en `table-responsive quick-search-bs5`; vista Razor respaldo con `Html.Raw`; desvincular menor con modal BS5 + fallback `confirm` nativo; inventario Fase 6 Reserva pasado a ✅ con notas.
- Problemas encontrados: ninguno.
- Estado: ✅ MSBuild MAT.MVC; SPEC P1 sigue `[ ]` hasta cerrar resto ⏳ del doc

### [2026-04-08] — Admin: `UsuarioEliminar` (membership + UserProfile + roles)

- Archivos modificados: `MAT.MVC/Controllers/Admin/AdminController.cs`, `MAT.MVC/Models/AdminUserListItem.cs`, `MAT.MVC/Views/Admin/Usuarios.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: POST `UsuarioEliminar` con `[Authorize]`, `ValidateAntiForgeryToken`, `RequireAdministrator()`; no eliminar a uno mismo ni al último **Administrador**; `MostrarEliminar` en listado; modal de confirmación reutilizando `js-admin-confirm-submit`; quita roles, `Membership.DeleteUser(..., true)`, limpieza `webpages_UsersInRoles` / `webpages_Membership`, `UserProfile` por EF, e intento opcional `webpages_OAuthMembership` (ignora error 208 si no existe tabla).
- Problemas encontrados: ninguno.
- Estado: ✅ Task SPEC marcado `[x]`; MSBuild MAT.MVC Debug OK

### [2026-04-08] — Reportes: hardening post-auditoría (401/403 JSON, fetch Excel, EPPlus, SPEC)

- Archivos modificados: `MAT.MVC/Controllers/Admin/ReportesController.cs`, `MAT.MVC/Infrastructure/ReportesQueryHelper.cs`, `MAT.MVC/Infrastructure/ReportesExcelExport.cs`, `MAT.MVC/Scripts/mat.reportes-excel-export.js` (nuevo), `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `ReportePagos.cshtml`, `ReporteRanking.cshtml`, `MAT.MVC/MAT.MVC.csproj`, `DOCUMENTACION/REPORTES_MAT_MVC_OPERACION.md`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: JSON con **401/403** y cuerpo `{ ok, message, data }`; vistas usan `MatReportes.parseHttpErrorBody` en `getJSON.fail`; Excel con **fetch+blob** y mensaje de error en alerta; cabeceras Excel centradas; columnas GUID como texto; `tipoVentaId` solo 1|2; orden `@From`/`@To` en ranking; fix **`$tbl`** en `ReportePagos`. SPEC: sección **Reportes — hardening** con tareas [x] y backlog [ ] (GUID UI, tests helper, DataTables sin CDN).
- Problemas encontrados: ninguno.
- Estado: ✅ MSBuild MAT.MVC Debug OK

### [2026-04-08] — Reportes P3: alias `/reportes/...`, doc operación, leyenda tendencias

- Archivos modificados: `MAT.MVC/App_Start/RouteConfig.cs`, `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `DOCUMENTACION/REPORTES_MAT_MVC_OPERACION.md` (nuevo), `DOCUMENTACION/REPORTES_MAT_WEB.md`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: rutas alias GET hacia `ReportesController` (JSON + Excel) con `namespaces` explícito; página `ReporteVentas` con región informativa sobre comparación de períodos (dos llamadas, `YYYY-MM-DD`, sin backend nuevo); guía de URLs, auth, SP y pruebas en `REPORTES_MAT_MVC_OPERACION.md`.
- Problemas encontrados: ninguno.
- Estado: ✅ Tasks Reportes P3 en SPEC marcados `[x]`; MSBuild MAT.MVC Debug OK

### [2026-04-08] — Reportes P2: Excel EPPlus + vistas Admin (hub, 3 pantallas) + rutas

- Archivos modificados: `MAT.MVC/Infrastructure/ReportesExcelExport.cs` (nuevo), `MAT.MVC/Controllers/Admin/ReportesController.cs`, `MAT.MVC/App_Start/RouteConfig.cs`, `MAT.MVC/Views/Reportes/*.cshtml` (Index, ReporteVentas, ReportePagos, ReporteRanking), `MAT.MVC/Views/Admin/Index.cshtml`, `MAT.MVC/Views/Shared/_LayoutAdmin.cshtml`, `MAT.MVC/MAT.MVC.csproj`, `DOCUMENTACION/REPORTES_MAT_WEB.md`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: export **VentasExcel**, **PagosExcel**, **RankingExcel** (mismo filtro que JSON; cabecera #4472C4; fechas es-AR; montos `#,##0.00`; nombres `ventas-|pagos-|ranking-compras-{yyyy-MM-dd}.xlsx`); hub `/Admin/Reportes` + tres vistas con DataTables, filtros fechas/viaje excluyentes, botón export; enlaces en Admin index y sidebar; rutas `AdminReportesHub` + `AdminReportes` con constraint de acciones; `JsonMessage` sobrecarga para `data: null`.
- Problemas encontrados: `_LayoutAdmin` ejecuta `@RenderSection("Scripts")` antes del body — scripts de reportes deben usar `$(function(){...})` y referenciar `#tblReporte` tras crear la tabla.
- Estado: ✅ Tasks P2 Excel + vistas + índice + regresión marcados `[x]` en SPEC; `HomeController` sigue llamando `usp_MAT_Reportes_Ventas` sin cambios; MSBuild MAT.MVC Debug OK

### [2026-04-08] — Reportes P2: `ReportesController` + JSON Ventas/Pagos/Ranking + ruta Admin

- Archivos modificados: `MAT.MVC/Controllers/Admin/ReportesController.cs`, `MAT.MVC/App_Start/RouteConfig.cs`, `MAT.MVC/MAT.MVC.csproj`, `DOCUMENTACION/REPORTES_SP_INVENTARIO_ESTRATEGIA.md`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: controlador con `[Authorize]`, `[InitializeSimpleMembership]`, comprobación rol **Administrador** vía JSON (`ok: false`, sin redirección HTML); acciones **Ventas**, **Pagos**, **Ranking** usando `ReportesQueryHelper`, `DBHelper.ExecuteDataReader` y `ReportesDataReaderMapper`; respuesta Newtonsoft **camelCase** `{ ok, message, data }`; ruta explícita `Admin/Reportes/{action}`. Task **Nuevo SP solo si hace falta** cerrado sin SP nuevo (inventario previo).
- Problemas encontrados: ninguno.
- Estado: ✅ Tasks reportes (controller + 3 JSON + SP nuevo N/A) marcados `[x]` en SPEC; MSBuild MAT.MVC Debug OK

### [2026-04-08] — Reportes P2: `ReportesDataReaderMapper` + DTOs fila

- Archivos modificados: `MAT.MVC/Infrastructure/ReportesDataReaderMapper.cs`, `MAT.MVC/Models/Reportes/ReporteVentaRowDto.cs`, `ReportePagoRowDto.cs`, `ReporteRankingRowDto.cs`, `MAT.MVC/MAT.MVC.csproj`, `DOCUMENTACION/REPORTES_SP_INVENTARIO_ESTRATEGIA.md`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: mapper central con `ReadVentas`/`ReadPagos`/`ReadRanking` y clase interna de ordinales case-insensitive; lectura `decimal`/`int`/`DateTime`/strings de fecha (`SP`); `MonedaTipo` como string; renombres a propiedades DTO; métodos públicos `MapVenta`/`MapPago`/`MapRanking` por fila. Documentado uso de Newtonsoft camelCase para API.
- Problemas encontrados: ninguno.
- Estado: ✅ Task marcado `[x]` en SPEC; MSBuild MAT.MVC Debug OK

### [2026-04-08] — Reportes P2: Helper `ReportesQueryHelper` (fechas, viaje, opcionales)

- Archivos modificados: `MAT.MVC/Infrastructure/ReportesQueryHelper.cs`, `MAT.MVC/Infrastructure/ReportesQueryParseResult.cs`, `MAT.MVC/MAT.MVC.csproj`, `DOCUMENTACION/REPORTES_SP_INVENTARIO_ESTRATEGIA.md`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: clase estática `ReportesQueryHelper.Parse(...)` devolviendo `ReportesQueryParseResult` con `IsValid`, `ShouldExecute`, `FromDdMmYyyy`/`ToDdMmYyyy` (Ventas/Pagos), `FromDate`/`ToDate` (Ranking), `ViajeId`, GUIDs opcionales e `TipoVentaId`. Reglas: fechas `dd-MM-yyyy` o `yyyy-MM-dd`; solo ejecución si par de fechas o `viajeId`; **rechazo** si viaje + fechas juntos; hasta 365 días inclusive; `TryParseDateParameter` público para tests.
- Problemas encontrados: ninguno.
- Estado: ✅ Task marcado `[x]` en SPEC; MSBuild MAT.MVC Debug OK

### [2026-04-08] — Reportes P2: Inventario SP + estrategia (sin alterar SP)

- Archivos modificados: `DOCUMENTACION/REPORTES_SP_INVENTARIO_ESTRATEGIA.md` (nuevo), `DOCUMENTACION/REPORTES_MAT_WEB.md` (tabla Ranking + nota fechas), `SPEC.md`, `PROGRESS.md`
- Qué se implementó: inventario read-only de `usp_MAT_Reportes_Ventas`, `usp_MAT_Reportes_Pagos`, `usp_MAT_Reportes_RankingCompras` (parámetros, ramas viaje vs fechas, columnas). **Decisión:** usar los tres SP actuales desde C# sin `ALTER`; Ventas/Pagos con `@From`/`@To` como `NVARCHAR` `DD-MM-YYYY`; Ranking con `@From`/`@To` como **SQL `DATE`** desde `DateTime` parseado; documentado que `@VendedorId` en ranking no filtra hoy. Nuevo SP solo si aparece requisito no cubierto.
- Problemas encontrados: SPEC `REPORTES_MAT_WEB` asumía `@From`/`@To` string para Ranking — corregido en doc.
- Estado: ✅ Task marcado `[x]` en SPEC; no requiere MSBuild (solo documentación)

### [2026-04-08] — P2: Admin Usuarios — DataTables + filtro por estado

- Archivos modificados: `MAT.MVC/Views/Admin/Usuarios.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: tabla `#tblAdminUsuarios` con DataTables (traducción ES, 25 por página, orden por usuario). Búsqueda global limitada a la columna **Usuario** (`searchable: false` en Id, Roles, Estado, Acciones). Desplegable **Estado** (Todos / Activo / Deshabilitado / Bloqueado) con `data-mat-estado` en cada fila y `$.fn.dataTable.ext.search`. Scripts de confirmación Bootstrap movidos dentro de `$(function)` porque `_LayoutAdmin` inyecta `@RenderSection("Scripts")` antes de `@RenderBody()`.
- Problemas encontrados: ninguno.
- Estado: ✅ Task marcado `[x]` en SPEC; MSBuild MAT.MVC Debug OK

### [2026-04-08] — P1: Reserva VinculacionMenor — CSS extraído + cierre de modales

- Archivos modificados: `MAT.MVC/Scripts/mat.jquery.functions.js` (`window.matCloseShowFormDialog`), `MAT.MVC/Content/partial-vinculacion-menor.css`, `MAT.MVC/Views/Reserva/VinculacionMenor.cshtml`, `MAT.MVC/Views/Reserva/FormListaMenor.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: helper global para cerrar diálogos abiertos con `ShowFormDialog` sin llamar a `.dialog("close")` disperso; estilos de `VinculacionMenor.cshtml` movidos al CSS compartido ya enlazado en `_Layout`; `FormListaMenor` alineado al mismo cierre. Inventario: `Reserva/VinculacionMenor.cshtml` marcado ✅.
- Problemas encontrados: ninguno.
- Estado: ✅ MSBuild MAT.MVC Debug OK (sin errores)

### [2026-04-08] — P1 (auditoría inventario): Fase 5 + Home Todos los Viajes + `_LayoutAdmin`

- Archivos modificados: `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: auditoría código vs listado — **PersonaCliente/Create, Edit, Details**, **Factura/Index**, **Factura/FacturaListByViajeID**, **PersonaCliente/DetalleFactura** y **Home/TodosLosViajes + TodosLosViajesIndex** ya estaban en patrón moderno; inventario Fase 5 marcado ✅. **Shared/_LayoutAdmin** marcado ✅. **Shared/Voucher** anotado como archivo vacío. Resumen global inventario **~74% → ~77%**; pendientes **~35–45** (Fase 6 y restos).
- Problemas encontrados: ninguno.
- Estado: ✅ inventario alineado; P1 macro en SPEC sigue `[ ]` hasta cerrar todo el listado ⏳

### [2026-04-08] — P2: Admin Usuarios confirmación con modal Bootstrap 5

- Archivos modificados: `MAT.MVC/Views/Admin/Usuarios.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: reemplazo del flujo `confirm()`/jQuery UI por modal Bootstrap 5 reutilizable (`#adminConfirmModal`) para la acción de deshabilitar usuario. El botón `.js-admin-confirm-submit` ahora abre modal, setea título/mensaje por `data-*` y confirma submit del form al aceptar.
- Problemas encontrados: ninguno.
- Estado: ✅ Task marcado `[x]` en SPEC; MSBuild MAT.MVC Debug OK

### [2026-04-08] — P1 (avance): Shared/Error modernizado

- Archivos modificados: `MAT.MVC/Views/Shared/Error.cshtml`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: vista de error standalone alineada al estilo actual (`modern-error-*`), corrigiendo botón con contraste correcto, card responsive, bloque de detalles técnicos sólo para `SystemDEV=true`, y manteniendo `correlationId` para soporte. Se marcó `Shared/Error.cshtml` como ✅ en inventario de pendientes.
- Problemas encontrados: las pendientes de Fase 5 en `PersonaCliente` ya estaban modernizadas en código, por lo que se avanzó con la primera pendiente real no modernizada.
- Estado: ✅ MSBuild MAT.MVC Debug OK; P1 sigue abierto en SPEC

### [2026-04-08] — P1 (iteración): normalización CSS de Shared/Error

- Archivos modificados: `MAT.MVC/Views/Shared/Error.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: normalización de estilos para vista standalone sin depender de redefinir `:root` local; se reemplazaron variables locales por uso de tokens globales con fallback (`var(--token, fallback)`), preservando consistencia visual aun cuando no se cargue el layout principal.
- Problemas encontrados: ninguno.
- Estado: ✅ Task de iteración marcado `[x]` en SPEC; MSBuild MAT.MVC Debug OK

### [2026-04-08] — P1 (iteración): extracción de estilos inline en Shared/Error

- Archivos modificados: `MAT.MVC/Views/Shared/Error.cshtml`, `MAT.MVC/Content/modern-error.css`, `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: extracción completa del bloque `<style>` de `Shared/Error.cshtml` hacia `Content/modern-error.css`; la vista ahora carga `saas-variables.css` + `modern-error.css` para mantener render standalone sin depender de `_Layout`.
- Problemas encontrados: ninguno.
- Estado: ✅ Task marcado `[x]` en SPEC; MSBuild MAT.MVC Debug OK (sin errores)

### [2026-04-07] — P2: Admin UsuarioEditar con gestión completa de roles

- Archivos modificados: `MAT.MVC/Models/AdminUsuarioEditModel.cs`, `MAT.MVC/Controllers/Admin/AdminController.cs`, `MAT.MVC/Views/Admin/UsuarioEditar.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: `UsuarioEditar` ahora lista **todos** los roles del sistema (`Roles.GetAllRoles()`) como checkboxes y guarda por **diferencia** (altas/bajas) contra los roles actuales del usuario. Se mantiene la seguridad existente para el rol Administrador: no quitarse admin a sí mismo y no quitar el último admin.
- Problemas encontrados: Ninguno bloqueante.
- Estado: ✅ MSBuild MAT.MVC Debug OK; task de SPEC marcado `[x]`

### [2026-04-07] — P1 (~15% doc): Home/Shared partials + historial vendedor + doc ~74%

- Archivos modificados: `MAT.MVC/Models/VendedorHistorialPagoFila.cs`, `MAT.MVC/MAT.MVC.csproj`, `MAT.MVC/Controllers/PersonaVendedor/PersonaVendedorController.cs`, `MAT.MVC/Views/PersonaVendedor/PartialHistorialPagos.cshtml`, `MAT.MVC/Scripts/mat.jquery.binding.js`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md` (vistas Admin Index, RenderGridHistorialPagos, Shared `_DetalleViaje` / `_ViajesPartial` / `_Search`, PersonaPasajero partial ya auditadas en doc en esta tanda)
- Qué se implementó: **`PartialHistorialPagos`** ahora usa modelo **`VendedorHistorialPagoFila`** con **`FacturaId`** resuelto vía **`MovimientoCuentaService.GetByPagoId`** (primera fila); enlace **`.js-detalle-pago-vendedor`** llama **`imprimirReciboPago`** cuando hay factura; si no hay movimiento, muestra “—”. Documentación: tabla principal **#76–#82**, Fase 6 ⏳→✅ para esas rutas, **avance global ~59% → ~74%** (inventario P1).
- Problemas encontrados: Pagos sin fila en `MovimientoCuenta` no ofrecen recibo desde esta UI (esperado).
- Estado: ✅ MSBuild MAT.MVC Debug OK; P1 sigue abierto en SPEC

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
