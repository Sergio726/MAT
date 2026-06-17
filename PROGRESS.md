# PROGRESS.md — Bitácora de desarrollo

---

### [2026-06-17] — Admin UI Lote 3 (tokens CSS, hover cards, sticky headers, breadcrumbs)

- Archivos modificados:
  - `MAT.MVC/Content/admin.modern.css` — tokens `--admin-content-*`, alias ecosistema, `.admin-card-link`, sticky thead, breadcrumb, grid resumen pagos
  - `MAT.MVC/Views/Admin/Index.cshtml` — clase `admin-card-link` en cards
  - `MAT.MVC/Views/Reportes/Index.cshtml` — clase `admin-card-link` en cards
  - `MAT.MVC/Views/Admin/GridResumenPagos.cshtml` — eliminado `<style>` inline; clase `modern-table`
  - `MAT.MVC/Views/Shared/_LayoutAdmin.cshtml` — sección `BreadcrumbItems`
  - `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `ReportePagos.cshtml`, `ReporteRanking.cshtml` — breadcrumbs
  - `MAT.MVC/Views/Admin/SistemaParametros.cshtml`, `Usuarios.cshtml`, `ErrorLog.cshtml` — breadcrumbs
  - `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Lote 3 UI Admin — paleta unificada navy en contenido, hover coherente en índices, cabeceras fijas en tablas, breadcrumbs contextuales en 6 vistas prioritarias.
- Problemas encontrados: Mapeo `--text-primary` usa tokens de contenido claro (no `--admin-text` del sidebar) para mantener contraste en cards blancas.
- Estado: ✅ completo (smoke test manual pendiente en IIS)

---


- Archivos modificados:
  - `MAT.MVC/Scripts/mat.reportes-utils.js` — `parseMoneyCell`, `moneyFmt`, `renderMonedaBadge`, `renderMontoCurrency`, `estadoBadgeHtml`, `emptyTableHtml`, `dataTableEmptyLanguage`
  - `MAT.MVC/Scripts/mat.admin-utils.js` — `MatAdmin.showLoading`, `MatAdmin.hideLoading` (botón y overlay)
  - `MAT.MVC/Content/admin.modern.css` — `.admin-empty-state`, estilos `dataTables_empty`
  - `MAT.MVC/Views/Reportes/ReporteVentas.cshtml` — badges estado/moneda, montos formateados, spinner Consultar, emptyTable
  - `MAT.MVC/Views/Reportes/ReportePagos.cshtml` — helpers centralizados, USD con `U$D`, spinner, emptyTable
  - `MAT.MVC/Views/Reportes/ReporteRanking.cshtml` — spinner Consultar, empty dashboard coherente
  - `MAT.MVC/Views/Admin/SistemaParametros.cshtml` — spinner CRUD/toggle, emptyTable, script reportes-utils
  - `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Lote 2 UI Admin — chips semánticos de estado factura (Ventas), badges `$ARS`/`U$D` y montos con `Intl.NumberFormat`, feedback de carga en consultas y CRUD, estados vacíos contextuales en DataTables.
- Problemas encontrados: Pagos no tiene columna `facturaEstado` en el DTO (badges solo en Ventas); Ranking sin columnas monetarias.
- Estado: ✅ completo (smoke test manual pendiente en IIS)

---


- Archivos modificados:
  - `MAT.MVC/Scripts/mat.admin-utils.js` *(nuevo)* — `MatAdmin.toast`, `MatAdmin.confirm`
  - `MAT.MVC/Views/Shared/_AdminConfirmModal.cshtml` *(nuevo)*
  - `MAT.MVC/Views/Shared/_LayoutAdmin.cshtml` — modal/toast compartidos, `mat.admin-utils.js`, cache CSS
  - `MAT.MVC/Views/Admin/Usuarios.cshtml` — confirmación vía `MatAdmin.confirm`
  - `MAT.MVC/Views/Admin/SistemaParametros.cshtml` — antiforgery, modales/toasts, sin `alert`/`confirm`
  - `MAT.MVC/Views/Admin/Index.cshtml` — tildes, sin badges numéricos
  - `MAT.MVC/Views/Admin/ErrorLog.cshtml` — layout `modern-*`, badges importancia semánticos
  - `MAT.MVC/Views/Admin/Logs.cshtml` — cards, pre con scroll, volver al panel
  - `MAT.MVC/Content/admin.modern.css` — estilos ErrorLog/Logs, título Index 1.5rem
  - `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Lote 1 de modernización UI Admin según plan — utilidades JS compartidas, feedback BS5, pulido tipográfico del Index, ErrorLog y Logs alineados al panel.
- Problemas encontrados: Ninguno; MSBuild Debug OK.
- Estado: ✅ completo (smoke test manual pendiente en IIS)

---

### [2026-06-17] — Admin: auditoría P1–P3 (plan sin planillas)

- Archivos modificados:
  - `MAT.MVC/Controllers/Admin/AdminController.cs` — retiro planillas; `AuditFactura` con ErrorUtil; `MiCuenta` ErrorUtil; eliminado `SistemaParametroJson` y checks manuales auth
  - `MAT.MVC/Controllers/Admin/ReportesController.cs` — `[RequireAdministrator]`; eliminados helpers auth duplicados
  - `MAT.MVC/Infrastructure/AdminAuthorizationHelper.cs` *(nuevo)*
  - `MAT.MVC/Filters/RequireAdministratorAttribute.cs` *(nuevo)*
  - `MAT.MVC/App_Start/RouteConfig.cs` — sin `RankingExcel`; `BuscarViajes` en constraint
  - `MAT.MVC/Views/Admin/*` — Index, AuditoriaFacturas, SistemaParametros; eliminadas 10 vistas planilla
  - `MAT.MVC/Views/Shared/_LayoutAdmin.cshtml` — Scripts al pie; sidebar SistemaParametros; activo Reportes
  - `MAT.MVC/Content/admin.modern.css` — estilos `.admin-hub`
  - Eliminados: modelos planilla MVC, `mat.planillaprint.css`, CSS huérfanos, JS planilla en `mat.jquery.binding.js`
  - `DOCUMENTACION/REPORTES_*.md`, `VISTAS_PENDIENTES_ACTUALIZACION.md`, `SPEC.md`
- Qué se implementó: Plan auditoría Admin en 4 entregas — retiro total submódulo planillas; P1 RankingExcel + AuditFactura; P2 filtro `[RequireAdministrator]`, XSS SistemaParametros, auth unificada; P2 UX layout/sidebar/títulos; P3 CSS huérfanos e Index sin inline styles.
- Problemas encontrados: Ninguno bloqueante en compilación.
- Estado: ✅ completo (smoke test manual pendiente en IIS)

---

### [2026-06-17] — Admin: auditoría documentada en SPEC

- Archivos modificados: `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Revisión del módulo `/Admin` (controladores, vistas, layout, JS). Hallazgos clasificados en **22 tasks pendientes** bajo `SPEC.md` → sección **"Admin — Auditoría funcional, seguridad y UX (2026-06-17)"** (P1 bugs, P2 seguridad/funcional/UX, P3 deuda). Tasks de UI transversal ya existentes (sidebar SistemaParametros, tokens CSS, etc.) referenciadas sin duplicar.
- Problemas encontrados: Prioridad inmediata — `DeletePlanilla` por GET, guardado planilla async con falso éxito, rutas `RankingExcel` huérfanas.
- Estado: ✅ documentación completa (implementación pendiente)

---

### [2026-06-16] — DistribucionCoche: auditoría post-Fase 2 y compatibilidad servidor

- Archivos modificados:
  - `MAT.MVC/Controllers/Reserva/ReservaController.cs`
  - `MAT.MVC/Views/Reserva/DistribucionCoche.cshtml`
  - `MAT.MVC/Views/Reserva/_DistribucionCocheAsiento.cshtml`
  - `database/2026-06-16_usp_MAT_Reserva_DistribucionCoche_GetByViajeID.sql` (script de despliegue)
  - `SPEC.md`
  - `PROGRESS.md`
- Qué se implementó:
  - **SPEC:** fases 3+ documentadas como mejoras futuras (modal Index, clic en butaca, lista mobile, refactor layout, menores en mapa).
  - **Servidor sin SP nuevo:** lectura defensiva `HasColumn` / `ReadIntOrDefault` para `EstadoPasaje` y `PasajeID`; banner `SpMigrationWarning` en vista; fallback de estado (libre=1, ocupado=Pagado) si el SP no está publicado.
  - **`[Authorize]`** en `DistribucionCoche`.
  - **Robustez vista:** eliminados `.First()` en butacas 3/4/7/8/59/60 → `butacaByNro` + placeholders vacíos; `HtmlAttributeEncode` en tooltips/`data-search`; guard null en partial.
- Problemas encontrados (documentados, no bloqueantes):
  - SP solo devuelve butacas con `Pasaje` (libres no aparecen en mapa); métricas total/disponibles son aproximadas → futuro en SPEC refactor layout.
  - `id` del asiento = `PasajeroID` (duplicado si mismo pasajero en dos butacas); diseño intencional para tutores.
  - Sin SP publicado, colores por estado pueden ser inexactos hasta migrar BD.
- Estado: ✅ MSBuild MAT.MVC Debug OK — **publicar SP en cada entorno antes de validar Fase 2 en servidor**

---

### [2026-06-16] — Fase 2 DistribucionCoche (Operativa)

- Archivos modificados:
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_Reserva_DistribucionCoche_GetByViajeID.sql`
  - `MAT.MVC/Models/ReservaModel.cs`
  - `MAT.MVC/Controllers/Reserva/ReservaController.cs`
  - `MAT.MVC/Views/Reserva/_DistribucionCocheAsiento.cshtml` (nuevo)
  - `MAT.MVC/Views/Reserva/DistribucionCoche.cshtml`
  - `MAT.MVC/Content/mat.distribucioncoche.css` (nuevo)
  - `MAT.MVC/MAT.MVC.csproj`
  - `PROGRESS.md`
- Qué se implementó:
  - **SP:** `PasajeID` y `EstadoPasaje` en resultado principal.
  - **Modelo:** campos extendidos + `GetCssClassEstadoButaca` / `GetDescripcionEstadoButaca` + `DistribucionCocheSeatViewModel`.
  - **Vista:** partial de asiento, colores por estado (alineados con Index), leyenda completa, métricas por estado en toolbar.
  - **UX:** búsqueda con resaltado/navegación (Enter/F3), tooltips Bootstrap 5, CSS dedicado.
- Problemas encontrados: requiere publicar SP en BD antes de probar en runtime.
- Estado: ✅ completo

---


- Archivos modificados:
  - `MAT.MVC/Controllers/Reserva/ReservaController.cs`
  - `MAT.MVC/Views/Reserva/DistribucionCoche.cshtml`
  - `MAT.MVC/Views/Hotel/EsquemaDistribucion.cshtml`
  - `PROGRESS.md`
- Qué se implementó:
  - **Controller:** `DistribucionCoche` con `ErrorUtil`, validación de `ViajeID`, métricas ocupadas/total, datos de viaje vía `ViajeMethod.ViajeByViajeID`.
  - **Tutores:** `GetListTutoresByViajeID` devuelve array JSON directo (sin doble serialización) + `try/catch` con `ErrorUtil`.
  - **Vista:** cabecera del viaje (descripción, fechas, logo), toolbar con métricas y botón Imprimir (`HideToPrint`), loading de tutores, banner de error AJAX.
  - **Bug fix:** `indexOf >= 0` para marcar tutores (antes omitía el primer tutor) en DistribucionCoche y EsquemaDistribucion.
- Problemas encontrados: ninguno en compilación.
- Estado: ✅ completo

---


- Archivos modificados:
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_Reserva_CambioButacas.sql`
  - `MAT.MVC/Controllers/PersonaCliente/PersonaClienteController.cs`
  - `MAT.MVC/Scripts/mat.jquery.binding.js`
  - `PROGRESS.md`
- Qué se implementó:
  - **Auditoría:** el error JS en FacturaListByViajeID era correlación temporal (factura sin DetalleFactura → Monto NULL), no fallo directo del AJAX post-cambio.
  - **SP:** sincroniza línea `Butaca XX` en DetalleFactura, recalcula `Factura.Monto`, inserta `AuditFactura`, llama `usp_MAT_Reserva_ActualizarEstados`.
  - **Controller:** `ConfirmarCambioButaca` → `JsonResult` + `ErrorUtil`; `ElegirNuevaButaca` catch con `ErrorUtil`.
  - **JS:** cierre de diálogos solo en success, IDs corregidos (`SeleccionImportes`), respuesta JSON `{ success, message }`, error handler AJAX.
- Problemas encontrados: facturas con Monto $0 e Items vacíos tras cambio de butaca por SP incompleto; excepciones no registradas en ErrorLog.
- Estado: ✅ completo (requiere publicar SP en BD)

---

### [2026-06-16] — Fix FacturaListByViajeID: error JS y registro en ErrorLog

- Archivos modificados:
  - `MAT.MVC/Models/FacturaModel.cs`
  - `MAT.MVC/Controllers/Factura/FacturaController.cs`
  - `MAT.MVC/Views/Factura/FacturaResultSearch.cshtml`
  - `PROGRESS.md`
- Qué se implementó:
  - `FacturaSearch`: manejo seguro de `DBNull` en `Monto` (SUM nulo), `Fecha` y demás campos.
  - `FacturaResultSearch` / `FacturaMoreDetails`: `ErrorUtil.LogAndGetPublicMessage` en catch (persiste en `ErrorLog` + log archivo).
  - `FacturaResultSearch.cshtml`: tabla y script solo en `@else`; mensaje con `modern-alert-error`; fallback `[]` en JSON.
- Problemas encontrados: `Convert.ToDouble` sobre `Monto` NULL del SP causaba excepción; el partial renderizaba `var dataSet = ;` y jQuery fallaba al inyectar HTML.
- Estado: ✅ completo

---

### [2026-04-15] — Reportes UX: Reporte Pagos — tarjetas de estadísticas (KPI) antes de la tabla

- Archivos modificados:
  - `MAT.MVC/Views/Reportes/ReportePagos.cshtml`
  - `SPEC.md`
  - `PROGRESS.md`
- Qué se implementó:
  - Bloque `#reportePagosIndicadores` con 4 cards Bootstrap 5 entre el panel de filtros front y la tabla DataTables.
  - **Total cobrado:** suma de `monto` desglosado por moneda ($ARS / U$S) con badges y `Intl.NumberFormat("es-AR")`.
  - **Resumen operativo:** total de pagos y facturas distintas (conteo de `facturaId` únicos).
  - **Por tipo de venta:** agrupación por `tipoVentaDescripcion`, conteo, porcentaje del total de filas, barras de progreso y montos por moneda.
  - **Por medio de pago:** agrupación por `pagoDescripcion`, ordenado por total de filas desc, Top 6 + agrupación "Otros" si hay más; barras de progreso y montos.
  - Los KPI se calculan en cliente sobre `rows` (sin nueva consulta); no se alteran al filtrar la tabla por vendedor/cliente/medio. Leyenda explícita en pantalla.
  - `renderIndicadoresPagos(rows)` invocada al inicio de `renderTable`; bloque oculto si no hay datos.
- Problemas encontrados: Ninguno — MSBuild limpio.
- Estado: ✅ completo

---

### [2026-04-15] — Reporte Ventas: SPEC — tareas UX modal facturas, rankings y podio

- Archivos modificados:
  - `SPEC.md` (tres ítems nuevos marcados `[x]` + alineación del bullet histórico de medallas Top 3)
  - `PROGRESS.md` (esta entrada)
- Qué se documentó en SPEC como completado:
  - Modal listado de facturas: datepickers en español (`mat-datepicker`, `mat.reportes-ventas-facturado-modal.js`).
  - Panel `reporte-rankings-strip` y estilos de cards para contraste Top Vendedores / Top Destinos.
  - Top 3 vendedores: medallas Unicode 🥇🥈🥉, efecto 1er puesto y ajustes cromáticos ya implementados en código en sesiones previas.
- Estado: ✅ completo (documentación)

---

### [2026-04-14] — Reportes UI: Unificar columna "Viaje" + "Fecha salida" en una sola celda

- Archivos modificados:
  - `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`
  - `MAT.MVC/Views/Reportes/ReporteRanking.cshtml`
- Qué se implementó:
  - En ambas vistas, las columnas "Viaje" y "Fecha salida" (o "F. salida") se fusionaron en una sola celda de dos líneas.
  - Primera línea: descripción del viaje con atributo `title` para tooltip al hover (texto completo sin truncar).
  - Segunda línea: fecha en `dd/mm/aaaa` con icono `bi-calendar3` en `text-muted small`.
  - `render(d, type, row)` retorna texto plano cuando `type !== "display"` para que DataTables pueda ordenar y filtrar correctamente.
  - En ReporteVentas: ajustados `COL_VENDEDOR` (4→3) y `COL_CLIENTE` (6→5) por la columna eliminada.
  - En ReporteRanking: la columna viaje permanece en índice 2; sin cambios en índices de filtros front.
  - HTML escapado manualmente (`&`, `"`, `<`, `>`) para prevenir XSS.
- Problemas encontrados: Ninguno.
- Estado: ✅ completo

---

### [2026-04-14] — Mejora: FacturaFiscal — separadores de miles y decimales en inputs de monto

- Archivos modificados:
  - `MAT.MVC/Views/FacturaFiscal/Create.cshtml`
- Qué se implementó:
  - `txtNeto`, `txtPercepciones`, `txtOtrosImpuestos` cambiados de `type="number"` a `type="text"` con `inputmode="decimal"` y clase `ff-monto-input`.
  - Helper `parseMonto(val)`: soporta formato es-AR (`1.234,56`) y English (`1234.56`).
  - Helper `formatMonto(n)`: formatea a es-AR con 2 decimales.
  - `blur` → formatea con miles y decimales (`1.234,56`). `focus` → muestra valor editable con coma decimal, selecciona todo el texto.
  - `calcularMontos()` y `guardarFactura()` usan `parseMonto()` en lugar de `parseFloat()`.
  - `cargarDatosEdicion()` aplica `formatMonto()` a neto/percepciones/otrosImpuestos al cargar.
- Problemas encontrados: Ninguno — MSBuild limpio.
- Estado: ✅ completo

---

### [2026-04-14] — BUG: FacturaFiscal/Edit — fechas y moneda/alicuota sin autocompletar

- Archivos modificados:
  - `MAT.MVC/Views/FacturaFiscal/Create.cshtml` (`cargarDatosEdicion`, nueva helper `fechaDdMmAaaa_a_ISO`)
- Qué se implementó:
  - `GetDetalle` devuelve fechas como `dd/MM/yyyy`; `<input type="date">` requiere `yyyy-MM-dd`. Agregada helper `fechaDdMmAaaa_a_ISO()` para la conversión.
  - `alicuotaIva || 4` reemplazado por `f.alicuotaIva != null ? String(...) : '4'` para no pisar el valor `0` (0% de IVA) con el default 21%.
  - `moneda` y `alicuota` ahora usan `String()` explícito para garantizar match con los `value` de las `<option>`.
- Problemas encontrados: Ninguno — MSBuild limpio.
- Estado: ✅ completo

---

### [2026-04-14] — P1 Facturación Fiscal: 4 tasks (resaltar filtros, lazy loading proveedores, localStorage, tooltips)

- Archivos modificados:
  - `MAT.MVC/Views/FacturaFiscal/Index.cshtml` (filtros activos, localStorage, tooltips Bootstrap 5)
  - `MAT.MVC/Views/FacturaFiscal/Create.cshtml` (autocomplete de proveedores, seleccionarProveedor, crearProveedorRapido)
  - `MAT.MVC/Controllers/FacturaFiscal/FacturaFiscalController.cs` (nuevo action BuscarProveedores)
- Qué se implementó:
  - **Resaltar filtros activos:** CSS `.ff-filtro-activo` + badge contador en el header del panel de filtros; se actualiza en `aplicarFiltros`/`limpiarFiltros`.
  - **Lazy loading de proveedores:** `<select>` reemplazado por `<input>` + `<ul>` de sugerencias; nuevo endpoint `GET BuscarProveedores?q=` con debounce 280ms; límite 20 resultados; click fuera cierra sugerencias; edición carga nombre del proveedor en el input.
  - **Guardar filtros en localStorage:** clave `ff_filtros_v1`; se guarda en `aplicarFiltros`, se restaura en `document.ready`, se limpia en `limpiarFiltros`.
  - **Mejora de tooltips:** `data-bs-toggle="tooltip"` en los 4 botones de acción de la tabla; re-inicialización en `drawCallback` de DataTables.
- Problemas encontrados: Ninguno — MSBuild limpio (solo warnings preexistentes).
- Estado: ✅ completo

---

### [2026-04-14] — Facturación Fiscal: Validación de duplicado en edición

- Archivos modificados:
  - `MAT.MVC/Views/FacturaFiscal/Create.cshtml` (función JS `validarDuplicado`)
- Qué se implementó:
  - El SP `usp_MAT_FacturaFiscal_CheckDuplicate`, el modelo y el controller ya soportaban `@ExcludeID`/`excludeId`. Solo faltaba que la llamada AJAX del cliente lo enviara.
  - Agregado `excludeId: esEdicion ? ($('#hdnFacturaId').val() || '') : ''` en el `data` de la llamada AJAX a `CheckDuplicate`.
  - En modo edición el backend excluye la factura actual de la búsqueda de duplicados, eliminando el falso positivo.
- Problemas encontrados: Ninguno — MSBuild limpio.
- Estado: ✅ completo

---

### [2026-04-10] — Editar facturas de compra (Facturación Fiscal)

- Archivos modificados:
  - `MAT.MVC/Controllers/FacturaFiscal/FacturaFiscalController.cs` (acción Edit GET + POST UpdateFactura)
  - `MAT.MVC/Views/FacturaFiscal/Create.cshtml` (reutilizado para edición con ViewBag.EsEdicion)
  - `MAT.MVC/Views/FacturaFiscal/Index.cshtml` (agregado botón Editar en columna acciones)
- Qué se implementó:
  - Acción Edit en controller que carga factura existente
  - POST UpdateFactura valida que no esté anulada antes de guardar
  - Vista Create.cshtml ahora soporta modo edición (carga datos vía GetDetalle)
  - Tabla Index muestra botón editar (lápiz) para facturas activas
- Problemas encontrados: Ninguno — MSBuild limpio.
- Estado: ✅ completo

---

### [2026-04-10] — Códigos de confirmación en BD (PersonaCliente)

- Archivos modificados:
  - `MAT.DB/dbo/Tables/SistemaParametro.sql` (nueva tabla)
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_SistemaParametro_GetByClave.sql`, `GetAll.sql`, `Insert.sql`, `Update.sql`, `Toggle.sql`
  - `MAT.DB/MAT.DB.sqlproj` (agregados tabla y SPs)
  - `database/2026-04-10_SistemaParametro_Migration.sql` (script de migración con datos iniciales)
  - `MAT.MVC/Models/SistemaParametroItem.cs` (nuevo modelo)
  - `MAT.MVC/Infrastructure/SistemaParametroHelper.cs` (helper con caché de 5 min)
  - `MAT.MVC/Controllers/PersonaCliente/PersonaClienteController.cs` (validación vía helper en `EliminarVenta` y `EliminarPasajeroDeFactura`)
  - `MAT.MVC/Controllers/Admin/AdminController.cs` (acciones CRUD JSON + vista)
  - `MAT.MVC/Views/Admin/SistemaParametros.cshtml` (nueva vista con DataTable + modal Bootstrap 5)
  - `MAT.MVC/Views/Admin/Index.cshtml` (enlace en sección Sistema)
  - `MAT.MVC/MAT.MVC.csproj` (agregados archivos nuevos)
- Qué se implementó:
  - Tabla `SistemaParametro` con clave única, valor, descripción, estado y fechas.
  - 5 SPs para CRUD + uno de búsqueda.
  - Datos iniciales: `Tinto.29` y `Martes.2025` (claves `CodigoConfirmacion_1` y `_2`).
  - Helper con caché en memoria (5 min) para validar códigos sin golpear BD en cada request.
  - Controller `PersonaCliente` ahora usa `SistemaParametroHelper.ValidarCodigo()` en lugar de `ConfigurationManager.AppSettings`.
  - UI Admin para gestionar códigos (listado, crear, editar, activar/desactivar).
  - Panel Admin linkeable desde `/Admin/SistemaParametros`.
- Problemas encontrados: Ninguno — MSBuild limpio (solo warnings preexistentes).
- Estado: ✅ completo

---

### [2026-04-10] — Sincronización documento VISTAS_PENDIENTES_ACTUALIZACION.md

- Archivos modificados: `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `SPEC.md`
- Qué se implementó: Auditoría completa de vistas ⏳ del documento vs código real:
  - PasajeroViaje (Manifiesto, ListadoSimple, ListadoSimpleToExport): modernas, sin glyphicons
  - PersonaCliente (todas las vistas): modernas, sin BS2/glyphicons
  - Admin/ServiciosAdmin*: no existen en repositorio (marcadas 🗑️)
  - Admin/HistorialPrecios: no existe (marcado 🗑️)
  - Actualizado resumen: 100% completado (~144 vistas)
- Estado: ✅ completo

---

### [2026-04-09] — Tests unitarios `ReportesQueryHelper` (NUnit)

- Archivos modificados: `MAT.sln`, `MAT.MVC.Tests\` (nuevo: `.csproj`, `packages.config`, `Properties\AssemblyInfo.cs`, `Infrastructure\ReportesQueryHelperTests.cs`), `packages\NUnit.3.14.0\`, `packages\NUnit3TestAdapter.4.6.0\`
- Qué se implementó: 11 casos (rango válido `yyyy-MM-dd` / `dd-MM-yyyy`, >365 días, viaje+fechas, `from` sin `to`, `tipoVentaId` 3 / no numérico / 1, solo viaje, sin criterio, `TryParseDateParameter`). Compilación `MAT.MVC.Tests` y solución OK; `vstest.console` + adaptador NUnit3: 11/11 correctas.
- Si falta carpeta `packages\`: `nuget install MAT.MVC.Tests\packages.config -OutputDirectory packages` (o instalar los dos paquetes con las versiones del `packages.config`).
- Estado: ✅ completo

---

### [2026-04-09] — Lote SPEC: datepicker mes/año, viaje combo+recent, ranking KPI/fechas/docs

- Archivos modificados:
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_Reportes_BuscarViajes.sql`, `database/2026-04-09_usp_MAT_Reportes_BuscarViajes.sql`
  - `MAT.MVC/Content/admin.modern.css`, `Views/Shared/_LayoutAdmin.cshtml`
  - `MAT.MVC/Controllers/Admin/ReportesController.cs`, `Models/Reportes/ReporteViajeLookupDto.cs`
  - `MAT.MVC/Scripts/mat.reportes-viaje-autocomplete.js`
  - `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `ReportePagos.cshtml`, `ReporteRanking.cshtml`
  - `MAT.MVC/Infrastructure/ReportesExcelExport.cs`
  - `DOCUMENTACION/REPORTES_MAT_MVC_OPERACION.md`, `SPEC.md`
- Qué se implementó:
  - **Datepicker Admin:** selects mes/año con `appearance` nativo, texto oscuro sobre fondo blanco, `overflow: visible` en header/popup para desplegables legibles.
  - **BuscarViajes:** parámetro `recent=true` + SP `@RecentOnly`; respuesta con `fechaSalida`; botón chevron en input-group abre últimos 30 viajes; etiquetas “descripción — dd/mm/aaaa”.
  - **Ranking:** cards resumen (filas, clientes/viajes/facturas distintos, mejor rank viajes); columnas Fecha y F. salida en `dd/mm/aaaa`; caja de ayuda; Excel ranking fechas `dd/MM/yyyy`.
- Problemas encontrados: redeploy del SP obligatorio (script `database/` o publicar SSDT).
- Estado: ✅ completo

---

### [2026-04-09] — Reportes: autocompletar viaje por nombre (sin GUID manual)

- Archivos modificados:
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_Reportes_BuscarViajes.sql` *(nuevo)*
  - `MAT.DB/MAT.DB.sqlproj`
  - `database/2026-04-09_usp_MAT_Reportes_BuscarViajes.sql`
  - `MAT.MVC/Controllers/Admin/ReportesController.cs`
  - `MAT.MVC/Models/Reportes/ReporteViajeLookupDto.cs` *(nuevo)*
  - `MAT.MVC/Scripts/mat.reportes-viaje-autocomplete.js` *(nuevo)*
  - `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `ReportePagos.cshtml`, `ReporteRanking.cshtml`
  - `MAT.MVC/Content/admin.modern.css`
  - `MAT.MVC/MAT.MVC.csproj`
  - `DOCUMENTACION/REPORTES_MAT_MVC_OPERACION.md`, `SPEC.md`
- Qué se implementó: Endpoint `GET /Admin/Reportes/BuscarViajes?q=` (admin JSON); SP nuevo en SSDT + script en `database/`; UI con jQuery UI Autocomplete + hidden `viajeId` en los tres reportes; limpieza al volver a “Por fechas”.
- Problemas encontrados: hay que **publicar el SP** en la BD (o ejecutar el script de `database/`) para que funcione en runtime.
- Estado: ✅ completo

---

### [2026-04-09] — Reporte Ventas: cards de indicadores (KPI) post-consulta

- Archivos modificados: `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Bloque **Resumen de la consulta** entre filtros y tabla: Total facturado / cobrado / saldo por moneda (`monedaTipo` `"1"` ARS, `"3"` U$S, otras agrupadas), cantidad de filas con `montoPagado > 0`, total de filas. Cálculo en JS sobre `res.data`; se oculta si no hay filas; leyenda explícita de que no cambia con el filtro front de vendedor/cliente; al pulsar Consultar se oculta hasta la nueva respuesta.
- Problemas encontrados: ninguno.
- Estado: ✅ completo

---

### [2026-04-09] — Extraer estilos Cotizador.cshtml; modernizar _LayoutSplash.cshtml

- Archivos modificados:
  - `Content/mat.cotizador.css` *(nuevo)*
  - `Views/Herramientas/Cotizador.cshtml`
  - `Views/Shared/_LayoutSplash.cshtml`
  - `Views/Shared/_Layout.cshtml`
  - `MAT.MVC.csproj`
  - `VISTAS_PENDIENTES_ACTUALIZACION.md`
- Qué se implementó:
  - **Cotizador.cshtml**: eliminado bloque `<style>` de 266 líneas → `Content/mat.cotizador.css`; partial sin estilos embebidos; CSS registrado en `_Layout.cshtml` y `MAT.MVC.csproj`.
  - **_LayoutSplash.cshtml**: reemplazados jQuery 1.8.2 / jQuery UI 1.8.24 (2012) por jQuery 3.7.1 + migrate 3.4.1 + UI 1.13.2; agregados Bootstrap 5.3.2 + Bootstrap Icons; CSS modernos (`saas-variables.css`, `mat.forms.css`, CSS UI 1.13.2); `@section Styles` disponible; scripts movidos al pie del `<body>` (patrón correcto); ninguna vista actualmente referencia este layout (verificado: no hay `Layout = "_LayoutSplash"` en el codebase) — actualizado de forma preventiva para futura reutilización.
  - 2 vistas marcadas ✅ en `VISTAS_PENDIENTES_ACTUALIZACION.md`.
- Problemas encontrados: ninguno — build 0 errores.
- Estado: ✅ completo

---

### [2026-04-09] — Extraer estilos inline Hotel/Distribucion, EsquemaDistribucion, Habitacion/PartialListHabitacion

- Archivos modificados:
  - `Content/mat.hotel.distribucion.css` *(nuevo — estilos de Distribucion + EsquemaDistribucion)*
  - `Content/mat.habitacion.list.css` *(nuevo — estilos de PartialListHabitacion)*
  - `Views/Hotel/Distribucion.cshtml`
  - `Views/Hotel/EsquemaDistribucion.cshtml`
  - `Views/Habitacion/PartialListHabitacion.cshtml`
  - `Views/Shared/_Layout.cshtml`
  - `MAT.MVC.csproj`
  - `VISTAS_PENDIENTES_ACTUALIZACION.md`
- Qué se implementó:
  - **Hotel/Distribucion.cshtml**: eliminados dos bloques `<style>` inline (principal 234 líneas + modal 15 líneas) → `mat.hotel.distribucion.css`; error message usa clase `.hotel-distribucion-error` con BI icon; `@section Styles` queda solo con links externos.
  - **Hotel/EsquemaDistribucion.cshtml**: eliminado bloque `<style>` inline de 263 líneas → `mat.hotel.distribucion.css`; partial cargado vía AJAX sin estilos embebidos.
  - **Habitacion/PartialListHabitacion.cshtml**: eliminado bloque `<style>` inline de 81 líneas → `mat.habitacion.list.css`; DataTable ya usaba i18n local (`/Scripts/dataTable/i18n/Spanish.json`).
  - Ambos CSS registrados en `MAT.MVC.csproj` e incluidos en `_Layout.cshtml`.
  - 3 vistas marcadas ✅ en `VISTAS_PENDIENTES_ACTUALIZACION.md`.
- Problemas encontrados: ninguno — build 0 errores, 0 advertencias.
- Estado: ✅ completo

---

### [2026-04-09] — Lote 10 tasks: DataTables i18n, UX Reportes Pagos/Ranking, extraer estilos inline, locale datepicker

- Archivos modificados:
  - `Scripts/dataTable/i18n/Spanish.json` *(nuevo)*
  - `Content/mat.busqueda.css` *(nuevo)*
  - `Content/mat.notacredito.css` *(nuevo)*
  - `Views/Reportes/ReportePagos.cshtml`
  - `Views/Reportes/ReporteRanking.cshtml`
  - `Views/Admin/AuditoriaFacturas.cshtml`
  - `Views/Busqueda/Perfil.cshtml`
  - `Views/Busqueda/Card.cshtml`
  - `Views/NotaCredito/partialMovimientoNotaCredito.cshtml`
  - `Views/CuentaCorriente/DetalleComprobante.cshtml`
  - `Views/PersonaCliente/partialHistorialdePagosByFactura.cshtml`
  - `Views/Shared/_Layout.cshtml`
  - `MAT.MVC.csproj`
  - `SPEC.md`, `VISTAS_PENDIENTES_ACTUALIZACION.md`
- Qué se implementó:
  - **DataTables i18n sin CDN**: `Spanish.json` local en `Scripts/dataTable/i18n/`; reemplazadas 17 referencias de 3 variantes CDN (`1.10.25/Spanish.json`, `1.13.7/es-AR.json`, `1.13.7/es-ES.json`) en todas las vistas del proyecto.
  - **ReportePagos UX**: descripción en lenguaje de negocio; datepicker jQuery UI en español (`dd/mm/aaaa`); eliminados inputs GUID `vendedorId`/`clienteId` del formulario de consulta; sección `filtroFront` con filtros por vendedor, cliente y medio de pago en el front (sin reconsultar BD); columnas DataTable limpias (sin GUIDs visibles). `tipoVentaId` permanece en la consulta.
  - **ReporteRanking UX**: misma mejora — descripción, datepicker ES, filtros front por cliente y viaje.
  - **AuditoriaFacturas**: locale `$.datepicker.regional['es']` cargado antes de init; nombres de meses y días en español; DataTable con `language` local.
  - **Extraer estilos inline**:
    - `Busqueda/Perfil.cshtml` + `Busqueda/Card.cshtml` → `Content/mat.busqueda.css`
    - `NotaCredito/partialMovimientoNotaCredito.cshtml` + `CuentaCorriente/DetalleComprobante.cshtml` → `Content/mat.notacredito.css`
    - Ambos CSS registrados en `MAT.MVC.csproj` e incluidos en `_Layout.cshtml`.
  - **VISTAS_PENDIENTES_ACTUALIZACION.md**: 8 vistas marcadas ✅ (`partialHistorialdePagosByFactura`, `PopupDetalleFactura`, `AuditoriaFacturas`, `Busqueda/Card`, `Busqueda/Perfil`, `partialNotaCredito`, `partialMovimientoNotaCredito`, `DetalleComprobante`).
- Problemas encontrados: ninguno — build limpio.
- Estado: ✅ completo

---

### [2026-04-09] — Seguridad: hardening AdminController (3 tasks SPEC)

- Archivos modificados: `MAT.MVC/Controllers/Admin/AdminController.cs`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó:
  - **`[Authorize]` a nivel de clase**: agregado al `AdminController` para que todo el controller exija autenticación sin excepciones; ya no depende de que cada acción lo declare individualmente.
  - **`RequireAdministrador()` en acciones sin protección de rol**: `Index`, `ResumenPagos`, `ResumenPagosPorFecha`, `GridResumenPagos`, `GridResumenPagosFecha`, `GridPlanillaHotelPrint`, `GridPlanillaHotelDetallePrint`, `PartialGridResumenPlanillaHotelPrint`, `ImprimirPlanilla`, `ImprimirPlanillaDetalle`, `GridPlanillaServiciosItemPrint`, `DeletePlanilla`, `EditarPlanilla`, `GridPlanillaHotelDetalleEdit`, `GridPlanillaServiciosItemEdit`, `AuditoriaFacturas`. Para los métodos `bool` (`GuardarDatosPlanilla`, `GuardarDatosItem`, `GuardarDatosServiciosItem`) se usó `if (!IsAdminUser()) return false`.
  - **Fix `e.Message` en `GridResumenPagosFecha`**: reemplazado `ViewBag.Error = "Error: " + e.Message` por `ErrorUtil.LogAndGetPublicMessage(e, ...)`.
  - **Fix `IsAdminUser()` fallback inseguro**: el `catch` ya no retorna `User.Identity.IsAuthenticated`; retorna `false` y loguea la excepción con `ErrorUtil`.
- Problemas encontrados: ninguno.
- Estado: ✅ 3 tasks SPEC marcados `[x]`; MSBuild MAT.MVC Debug OK

### [2026-04-09] — Reportes UX/UI: Reporte Ventas (3 tasks SPEC)

- Archivos modificados: `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó:
  - **Texto descriptivo**: eliminado el aviso técnico `alert-info` (comparación de períodos, URLs, referencias a DOCUMENTACION); reemplazado por párrafo orientado al usuario (qué es el reporte, filtro fechas/viaje, refinar resultados, Excel).
  - **Fechas en español**: `input[type=date]` → `input[type=text]` + jQuery UI datepicker en español (locale definido inline; ya incluido en `_LayoutAdmin`); `dd/mm/yyyy` → `dd-mm-yyyy` en `normalizeDate()` antes de enviar al backend (compatible con `TryParseDateParameter`).
  - **Filtro front vendedor/cliente**: campos GUID de vendedorId/clienteId retirados del formulario de consulta (ya no se envían al SP); panel "Acotar resultados" (hidden → visible tras primer Consultar exitoso) con inputs de texto que filtran `vendedorFullName`/`clienteFullName` vía `dt.column().search().draw()` sin nueva petición; botón Limpiar; Excel exporta total de la consulta (tooltip indica esto); columnas GUID ocultadas en la grilla (IDs de viaje, vendedor, cliente, factura).
- Problemas encontrados: ninguno.
- Estado: ✅ 3 tasks SPEC marcados `[x]`; MSBuild MAT.MVC Debug OK

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

### [2026-04-14] — Reportes: Persistir `lastRows` en ReporteVentas (prerequisito)

- Archivos modificados: `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Declarado `var lastRows = []` junto a `var dt = null` en el closure del módulo. Asignado `lastRows = rows || []` como primera operación en `renderTable(rows)`. Reseteo explícito `lastRows = []` al inicio del handler `#btnBuscar`. Sin cambio de contrato: DataTable, KPI, filtro front y Excel funcionan igual.
- Problemas encontrados: Ninguno.
- Estado: ✅ MSBuild MAT.MVC Debug OK; `lastRows` disponible en scope del módulo para tasks siguientes (modal facturas, Top Vendedores, Top Destinos)

### [2026-04-14] — Reportes UX: Tarjeta "Total facturado" + modal "Listado de facturas" + Excel en tarjeta

- Archivos modificados: `MAT.MVC/Scripts/mat.reportes-ventas-facturado-modal.js` (nuevo), `MAT.MVC/Scripts/mat.reportes-excel-export.js`, `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó:
  - **Card "Total facturado"** interactiva: `cursor-pointer`, hover con sombra y borde verde sutil, tooltip BS5, `role=button tabindex=0`, estado deshabilitado (`card-facturado-sin-datos`) hasta que haya datos; botón "Exportar Excel" en el footer con `stopPropagation`.
  - **Modal "Listado de Facturas"** (`#modalListadoFacturas`): externo en `mat.reportes-ventas-facturado-modal.js`. Filtros: texto libre (debounce 250ms), estado dinámico, moneda, rango de fecha de factura (input[type=date]), botón Limpiar. Tabla con orden por columna (▲/▼), filas expandibles con detalle (Viaje, fecha salida, butacas, GUID), copiar FacturaId al portapapeles con feedback visual, estado vacío. Totales recalculados sobre subconjunto filtrado por moneda; saldo en `text-danger` si > 0. Botón "Expandir todo". Subtítulo contextual con criterio de la última consulta.
  - **`mat.reportes-excel-export.js`**: parametrizado con `extraBtnSel` (4º param opcional) para deshabilitar múltiples botones durante la descarga.
  - CSS inline en la vista: `.card-facturado-interactiva` / `.card-facturado-sin-datos`, con `prefers-reduced-motion`.
- Ítems D implementados: debounce búsqueda, ordenación por columna, expandir/contraer todo, sticky-header (max-height + overflow-y), estado vacío, jerarquía visual saldo (text-danger), feedback "Copiado", responsive (modal-fullscreen-md-down), subtítulo contextual, prefers-reduced-motion.
- Ítems D pospuestos: exportar subconjunto del modal (requeriría nuevo endpoint o Excel en cliente), deep link/URL state, sincronía con filtro front de página, atajo de teclado Alt+L.
- Problemas encontrados: Ninguno.
- Estado: ✅ MSBuild MAT.MVC Debug OK

### [2026-04-14] — Reportes UX: Top 3 Vendedores + Top 3 Destinos con cards y modales

- Archivos modificados: `MAT.MVC/Scripts/mat.reportes-vendedores.js` (nuevo), `MAT.MVC/Scripts/mat.reportes-destinos.js` (nuevo), `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó:
  - **`mat.reportes-vendedores.js`**: `aggregateByVendedor` reduce por `vendedorId` acumulando ventas, total, arsTotal, usdTotal, arsSaldo, usdSaldo; ordena por `total` desc. Cards Top 3 con medallas (`bi-trophy-fill`/`bi-medal`/`bi-award`), badges de moneda, saldo en rojo o "Sin saldo pendiente". Modal con búsqueda debounced, tabla ordenable (Vendedor, Total ventas, $ARS, U$S, Saldo $ARS, Saldo U$S, Promedio), Exportar CSV en cliente.
  - **`mat.reportes-destinos.js`**: `aggregateByViaje` reduce por `viajeId` acumulando ventas y total (suma cruda de ambas monedas — limitación documentada en comentario); ordena por `ventas` desc. Cards Top 3 con `bi-geo-alt-fill`, nombre truncado, ventas, monto y promedio. Modal con búsqueda, tabla ordenable, Exportar CSV.
  - **Vista**: bloques `#topVendedores` y `#topDestinos` ocultos hasta Consultar; dos modales Bootstrap 5 modal-xl; CSS `.card-top-ranking` con hover; `renderTable` renderiza ambos; `btnBuscar` oculta ambos.
- Problemas encontrados: Ninguno.
- Estado: ✅ MSBuild MAT.MVC Debug OK
- Estado: ✅ MSBuild MAT.MVC OK

### [2026-04-14] — Reportes UI: Fechas dd/mm/aaaa en las tres tablas de reportes

- Archivos modificados: `MAT.MVC/Scripts/mat.reportes-utils.js` (nuevo), `MAT.MVC/Views/Reportes/ReporteVentas.cshtml`, `ReportePagos.cshtml`, `ReporteRanking.cshtml`, `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Helper `window.MatReportes.formatFechaES(val)` en `mat.reportes-utils.js`. Maneja: ISO con hora (`2024-03-15T00:00:00` → local), ISO solo fecha (`2024-03-15` → local para evitar offset UTC), `/Date(ms)/` (legacy .NET), número (ms) y Date. Devuelve `dd/mm/aaaa` o `""`. Ventas: `fechaSalida` reemplazó render anterior (`d ? string : ""`); `facturaFecha` recibió render por primera vez. Pagos: `fechaPago` recibió render. Ranking: eliminados `fmtSoloFecha`, `parseToDate`, `cellSoloFecha` inline; `fecha` y `viajeFechaSalida` apuntan al helper compartido.
- Problemas encontrados: Ninguno.
- Estado: ✅ MSBuild MAT.MVC Debug OK

### [2026-04-15] — Reportes UX: Ranking — mejoras UX/UI en tabla tblReporte

- Archivos modificados: `MAT.MVC/Views/Reportes/ReporteRanking.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó:
  - **Badges de ranking:** columnas "Rank. clientes" y "Rank. viajes" muestran badges de color semántico (#1 oro/warning, #2 plata/secondary, #3 bronce/danger) en vez de número plano; posiciones >3 quedan en texto muted.
  - **Columnas numéricas centradas:** "Pasajes fact.", "Viajes cl.", "Pasajes cl.", "Fact. viaje" con `className: "text-center"` y render `fw-semibold`; valores nulos como `-` en muted.
  - **Íconos en encabezados DataTables:** Fecha → `bi-calendar3`, Cliente → `bi-person`, Viaje → `bi-geo-alt` (HTML en `title` de `columns`).
  - **Tooltips explicativos en th:** `COL_TOOLTIPS` aplica Bootstrap 5 `Tooltip` vía `initComplete` a las columnas numéricas y de ranking, explicando cada métrica al usuario.
  - **Constantes de columna:** `COL_CLIENTE = 1`, `COL_VIAJE = 2` reemplazan índices mágicos en filtros front y `limpiarFiltros`.
  - **Leyenda colapsable:** la caja "Cómo leer el ranking" pasó de `alert alert-light` fijo a botón `btn-outline-secondary` + `collapse` de BS5; texto actualizado ("Fact. viaje" en vez de "Clientes viaje").
  - **Renombrado columna:** "Clientes viaje" → "Fact. viaje" (refleja con más precisión que cuenta filas de factura, no clientes únicos).
  - **Render helpers:** `escHtml`, `renderRankBadge`, `renderNumCentered` como funciones reutilizables en el scope de la vista.
  - **Viaje truncado:** columna Viaje con `text-truncate` y `max-width: 220px` + `title` para tooltip nativo al hover.
- Problemas encontrados: Ninguno.
- Estado: ✅ MSBuild MAT.MVC Debug OK

### [2026-04-15] — Reportes UX: Ranking — dashboard interactivo (reescritura completa)

- Archivos nuevos:
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_Reportes_RankingCompras_V2.sql`
  - `database/2026-04-15_usp_MAT_Reportes_RankingCompras_V2.sql`
  - `MAT.MVC/Scripts/mat.reportes-ranking-dashboard.js`
  - `MAT.MVC/Scripts/mat.reportes-ranking-modals.js`
  - `MAT.MVC/Scripts/lib/html2pdf.bundle.min.js`
- Archivos modificados: `MAT.MVC/Views/Reportes/ReporteRanking.cshtml` (reescritura completa), `MAT.MVC/Models/Reportes/ReporteRankingRowDto.cs`, `MAT.MVC/Controllers/Admin/ReportesController.cs`, `MAT.MVC/Infrastructure/ReportesDataReaderMapper.cs`, `MAT.MVC/Infrastructure/ReportesExcelExport.cs`, `MAT.MVC/MAT.MVC.csproj`, `MAT.DB/MAT.DB.sqlproj`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó:
  - **Nuevo SP `usp_MAT_Reportes_RankingCompras_V2`**: devuelve una fila por factura x viaje con `CantidadPasajesXFactura` y `CantPasajerosDistintos` (COUNT DISTINCT PasajeroID). Eliminadas window functions de rankings y conteos por cliente/viaje (se hacen en JS). Parámetros: `@From`, `@To`, `@ViajeId`, `@ClienteId`.
  - **DTO simplificado**: 9 propiedades (sin `CantViajesCompradosXCliente`, `CantPasajesCompradosXCliente`, `CantClientesEligieronViaje`, `RankingClientesCompradoresViajes`, `RankingViajes`). Mapper actualizado con alias `ClienteFullName`/`FullName`.
  - **Controller**: `Ranking` apunta al nuevo SP V2. `RankingExcel` eliminado (reemplazado por PDF en cliente). `BuildRankingParams` sin `@VendedorId`.
  - **Vista reescrita**: eliminados DataTable `#tblReporte`, filtros front `#filtroFront`, KPI cards `#reporteRankingIndicadores`, leyenda `#rankingLeyenda`. Reemplazados por 5 tarjetas KPI clicables (Registros, Clientes, Pasajeros, Facturas, Top viajes) con hover, role=button, data-modal; 5 modales Bootstrap 5 (`modal-xl modal-fullscreen-md-down`); filtros con datepickers pre-cargados (último mes) y auto-consulta al cargar; botón "Descargar PDF" en vez de "Exportar Excel"; estado vacío con ícono y mensaje.
  - **`mat.reportes-ranking-dashboard.js`**: agregaciones JS (`countDistinct`, `sumField`, `aggregateViajes`), render de las 5 tarjetas, PDF via `html2pdf.js` con fallback a `window.print()`.
  - **`mat.reportes-ranking-modals.js`**: 5 funciones de render de tabla (Registros, Clientes agrupados por clienteId, Pasajeros agrupados por viajeDescripcion, Facturas agrupados por facturaId, Top viajes). Búsqueda con debounce 250ms, ordenación por columna con flechas, contador "X de Y".
  - **`html2pdf.js`** v0.10.2 descargado como archivo local en `Scripts/lib/`. Captura dashboard a PDF A4 horizontal.
  - **`ReportesExcelExport.BuildRanking`**: actualizado para compilar con el nuevo DTO (dead code, no hay callers tras eliminar `RankingExcel`).
- Problemas encontrados: `ReportesExcelExport.cs` rompía compilación al referenciar propiedades del DTO eliminadas; se actualizó `BuildRanking` para compilar.
- Estado: ✅ MSBuild MAT.MVC + MAT.DB Debug OK