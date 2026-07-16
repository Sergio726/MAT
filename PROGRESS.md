# PROGRESS.md — Bitácora de desarrollo

---

### [2026-07-15] — UX FacturaListByViajeID: Pagado, Saldo y columnas
- **Archivos:** `MAT.DB/dbo/Stored Procedures/usp_MAT_Factura_Search.sql`, `MAT.MVC/Models/FacturaModel.cs`, `MAT.MVC/Controllers/Factura/FacturaController.cs`, `MAT.MVC/Views/Factura/FacturaResultSearch.cshtml`, `MAT.MVC/Views/Factura/FacturaListByViajeID.cshtml`, `SPEC.md`, `PROGRESS.md`
- **Qué se implementó:** SP agrega `Saldo` (`fn_MAT_SaldoFactura`) y `MontoPagado` (Monto − Saldo) vía CROSS APPLY. DTO `FacturaStandard.MontoPagado` + mapeo en `FacturaSearch`. Grilla: columnas Pagado/Saldo (saldo > 0 en rojo), Paquete oculto si `sViajeID`, Viaje con más peso, densidad reducida, `table-responsive`. Shell por viaje: cabecera compacta + card de resultados. `HidePaqueteColumn` desde controller.
- **Verificación:** MSBuild `MAT.MVC` Debug OK.
- **Acción humana:** publicar `usp_MAT_Factura_Search` en cada entorno (sin el SP la grilla fallará al mapear columnas nuevas).
- **Estado:** ✅ completo (código); pendiente publicar SP en BD

---

### [2026-07-15] — Fix BUG P1: cambio de butaca preserva badge H / estado hotel
- **Archivos:** `MAT.DB/dbo/Stored Procedures/usp_MAT_Reserva_CambioButacas.sql`, `database/2026-07-15_CambioButacas_PreserveHotel.sql`, `SPEC.md`, `PROGRESS.md`
- **Qué se implementó:** El `UPDATE ReservaHabitacion.PasajeID` corre **dentro** de la transacción y **antes** de `usp_MAT_Reserva_ActualizarEstados`, para que el JOIN hotel vea el pasaje nuevo y conserve estados 6/8/9 (badge H).
- **Acción humana:** publicar `database/2026-07-15_CambioButacas_PreserveHotel.sql` en cada entorno; smoke: pasajero pagado+hotel → cambio butaca → H en butaca nueva en Index/DistribucionCoche.
- **Estado:** ✅ completo (código); pendiente publicar SP en BD

---

### [2026-07-15] — Registro BUG P1: cambio de butaca pierde badge H / estado hotel
- **Archivos:** `SPEC.md`, `PROGRESS.md`
- **Qué se documentó:** Bug en `/Reserva/Index` (y DistribucionCoche): al cambiar butaca de un pasajero con reserva de habitación, desaparece la letra **H** y el `EstadoPasaje` compuesto (6/8/9). Comportamiento esperado: preservar todos los estados; solo cambia el número de butaca.
- **Causa raíz:** en `usp_MAT_Reserva_CambioButacas`, `usp_MAT_Reserva_ActualizarEstados` se ejecuta antes de `UPDATE ReservaHabitacion.PasajeID` (y ese UPDATE está post-COMMIT). Para factura pagada, el JOIN hotel falla → estado 4 sin H.
- **Fix previsto (no implementado aún):** mover el UPDATE de `ReservaHabitacion` dentro de la transacción, antes de `ActualizarEstados`; script en `database/` con paridad `MAT.DB`. Sin cambios UI/CSS.
- **Estado:** ✅ supersedido por fix del mismo día

---

### [2026-07-07] — ReporteRanking: fixes UX estados vacíos, validación viaje y alineación visual
- **Archivos:** `Views/Reportes/ReporteRanking.cshtml`
- **Qué se implementó:** Fix 1 — sin resultados: solo `#dashboardEmpty`, `render([])` resetea KPIs; Fix 2 — validación cliente si modo viaje sin `viajeId`; Fix 3 — `btn-modern` en Consultar, `aria-label` en modo filtro, icono Pasajeros con acento fucsia.
- **Verificación:** MSBuild `MAT.MVC` Debug OK.
- **Estado:** ✅ completo

---

### [2026-07-07] — Admin UI tercera pasada: subvistas y limpieza CSS
- **Archivos:** `Content/admin.modern.css`, `Views/Shared/_LayoutAdmin.cshtml`, `Views/Admin/ResumenPagos.cshtml`, `ResumenPagosPorFecha.cshtml`, `GridResumenPagosFecha.cshtml`, `GridResumenPagos.cshtml`, `MiCuenta.cshtml`, `RegistrarVendedor.cshtml`, `AuditoriaFacturas.cshtml`, `Views/Reportes/Index.cshtml`, `ReporteVentas.cshtml`, `ReporteRanking.cshtml`, `SPEC.md`.
- **Qué se implementó:** Migración de ~500 líneas CSS inline → secciones 4–7 en `admin.modern.css`; eliminado `mat.styles.custom.css` del layout admin (overrides mínimos de alertas en admin CSS); formularios `.admin-form-*`; ResumenPagos con `modern-page-header`; Auditoría con `MatAdmin.toast` y `.d-none`; modales reportes con `.admin-modal-table-wrap`; cache bust `?v=20260707b`.
- **Auditoría vistas (21):**

| Vista | `<style>` | CSS extra | Patrón modern-* | Acción |
|-------|-----------|-----------|-------------------|--------|
| Admin/Index | No | No | Hub `.admin-hub` | OK — intencional |
| Admin/Usuarios | No | No | Sí | OK |
| Admin/UsuarioEditar | No | No | Sí | OK |
| Admin/UsuarioResetPassword | No | No | Sí | OK |
| Admin/SistemaParametros | No | No | Sí + MatAdmin.toast | OK |
| Admin/ErrorLog | No | No | Sí | OK |
| Admin/Logs | No | No | Sí | OK |
| Admin/ResumenPagos | ~~Sí~~ | magicsearch | Sí (header) | ✅ migrado |
| Admin/ResumenPagosPorFecha | ~~Sí~~ | No | Sí | ✅ migrado |
| Admin/GridResumenPagos | No | No | Sí | ✅ btn-modern Excel |
| Admin/GridResumenPagosFecha | ~~Sí~~ | No | Sí | ✅ migrado |
| Admin/AuditoriaFacturas | No | No | Sí | ✅ toast + d-none |
| Admin/MiCuenta | No | ~~modern-account~~ | Sí | ✅ admin-form CSS |
| Admin/RegistrarVendedor | No | ~~modern-account~~ | Sí | ✅ admin-form-* |
| Reportes/Index | No | No | Sí + admin-card-link | ✅ iconos |
| Reportes/ReporteVentas | ~~Sí~~ | No | Sí | ✅ migrado |
| Reportes/ReportePagos | No | No | Sí | OK |
| Reportes/ReporteRanking | ~~Sí~~ | No | Sí | ✅ migrado |

- **Smoke visual (humano):** `/Admin/Index`, ResumenPagos (+ grid), ResumenPagosPorFecha, Reportes (Index + Ventas/Pagos/Ranking), Usuarios, SistemaParametros, ErrorLog/Logs, AuditoriaFacturas, MiCuenta/RegistrarVendedor, viewport 375px.
- **Verificación:** MSBuild `MAT.MVC` Debug OK.
- **Estado:** ✅ completo

---

### [2026-07-07] — DistribucionCoche Fase 3: acciones al clic en butaca
- **Archivos:** `usp_MAT_Reserva_DistribucionCoche_GetByViajeID.sql`, `database/2026-07-07_DistribucionCoche_FacturaID.sql`, `ReservaModel.cs`, `ReservaController.cs`, `_DistribucionCocheAsiento.cshtml`, `DistribucionCoche.cshtml`, `mat.distribucioncoche.css`, `PersonaClienteController.cs`, `ElegirNuevaButaca.cshtml`, `mat.jquery.binding.js`, `SPEC.md`.
- **Qué se implementó:** Menú contextual en butacas ocupadas (ver factura en modal BS5, cambiar butaca); `data-factura-id` + `FacturaID` en SP; `abrirCambioButaca` global reutilizable; fallback `ElegirNuevaButaca?shell=1` en ventana nueva si no hay `opener`.
- **Acción humana:** publicar `database/2026-07-07_DistribucionCoche_FacturaID.sql`.
- **Estado:** ✅ completo

---

### [2026-07-07] — Deuda funcional: CancelViaje SP + investigación tabla `Cuenta`

#### A) Eliminación de viaje (SP unificado)
- **2026-07-07 (refactor):** Un solo SP `usp_MAT_Viaje_DeleteViaje` con auditoría obligatoria (`@VendedorId`, `@DeleteDetalle`). Eliminado `usp_MAT_Viaje_CancelViaje`. UI: un botón eliminar con diálogo de motivo. Script: `database/2026-07-07_Viaje_DeleteViaje_Unified.sql` (DROP CancelViaje si existía).

#### B) Investigación `dbo.Cuenta`
- **Conclusión: MANTENER** — no es legado huérfano.
- **Rol:** cabecera de cuenta corriente por cliente (`CuentaID`, `ClienteID`, `Estado` bit activo/inactivo). Toda trazabilidad de pagos pasa por `MovimientoCuenta.CuentaID` → FK a `Cuenta`.
- **Código activo:** `CuentaDataAccess` + SPs `usp_MAT_Cuenta_*`; `PersonaClienteController` (activar/desactivar/verificar); `PersonaPasajeroController` insert al alta; `usp_MAT_PersonaCliente_Create`, `usp_MAT_RegistroPago_NuevoPago`, `usp_MAT_Reserva_RegistrarPago`, `usp_MAT_Nota_IsertNewNota` leen/insertan `Cuenta`.
- **No confundir con:**
  - `dbo.CuentaCorriente` — tabla legacy de montos/fecha; modelo MVC comentado; UI “Cuenta Corriente” del cliente usa `CreditoCliente`/notas y `PersonaCliente/CuentaCorriente`.
  - `Admin/MiCuenta` — contraseña del usuario.
- **Deuda menor detectada:** `PersonaPasajeroController` inserta `Cuenta` para todo pasajero nuevo aunque no sea cliente (posible fila con `ClienteID` sin fila en `Cliente` — validar con negocio; fuera de alcance schema).
- **Comentario engañoso:** `HistorialPagosLoader` dice “sin depender de Cuenta legacy” — significa que el reporte consulta `Pago` directo, no que la tabla esté obsoleta.
- **Consultas BD dev sugeridas (ejecutar humano):**
  ```sql
  SELECT COUNT(*) AS FilasCuenta FROM dbo.Cuenta;
  SELECT COUNT(*) AS Movimientos FROM dbo.MovimientoCuenta;
  SELECT MAX(mc.FechaRegistro) AS UltimoMovimiento FROM dbo.MovimientoCuenta mc;
  SELECT COUNT(*) AS CuentasSinCliente
  FROM dbo.Cuenta c LEFT JOIN dbo.Cliente cl ON cl.ClienteID = c.ClienteID WHERE cl.ClienteID IS NULL;
  ```
- **Estado:** ✅ completo (informe); métricas BD pendientes humano

---

### [2026-07-07] — Admin UI segunda pasada: paleta slate + fucsia (Admin Command)
- **Archivos modificados:** `Content/admin.modern.css`, `Views/Shared/_LayoutAdmin.cshtml`, `_AdminTopbar.cshtml`, `Views/Admin/ResumenPagos.cshtml`, `ResumenPagosPorFecha.cshtml`, `GridResumenPagosFecha.cshtml`, `SPEC.md`.
- **Qué se implementó:** Reemplazo tema azul marino por tokens slate (`#1e293b`/`#0f172a`) + acento fucsia (`#e63375`); topbar con gradiente tipo login; sidebar con accent rail en ítem activo; datepicker, botones, cards hub, overrides Bootstrap; badge “Admin” en topbar; limpieza de `#0b2a4a` en resúmenes de pagos.
- **Diferenciación:** Admin = shell oscuro + acento contenido; vendedor = fondo claro + fucsia en CTAs; login = split hero fotográfico.
- **Verificación:** MSBuild `MAT.MVC` Debug OK. Smoke visual pendiente humano: Index, Usuarios, SistemaParametros, ReporteVentas, ErrorLog.
- **Estado:** ✅ completo

---

### [2026-07-05] — Testing post-NetTiers (opción B)
- **Archivos modificados/creados:** `MAT.MVC.Tests/Utilities/HelperTests.cs`, `MAT.MVC.Tests/Contract/MigrationContractTests.cs`, `MAT.Integration.Tests/*`, `tools/Run-Tests.ps1`, `tools/Verify-NetTiersMigration.ps1` (deuda `CancelViaje` → WARN), `DOCUMENTACION/TESTING.md`, `MAT.sln`, `CLAUDE.md`, `SPEC.md`.
- **Qué se implementó:** Tests unitarios ampliados (`Helper`, contrato migración); proyecto integración SQL con gate `MAT_TEST_CONNECTION_STRING`; script unificado build + verify + vstest; documentación de ejecución local/CI.
- **Problemas encontrados:** `usp_MAT_Viaje_CancelViaje` sigue ausente en MAT.DB (deuda previa, documentada como WARN).
- **Estado:** ✅ completo
- **Verificación:** `.\tools\Run-Tests.ps1` (unit + verify); integración con `-Integration` requiere BD + SPs F9/F12 desplegados.

---

### [2026-07-05] — NetTiers F11 + F12: POCOs + cierre épica
- **F11:** 59 entidades convertidas de `*Base.generated.cs` a POCOs manuales; eliminada infra NetTiers en `MAT.Entities` (`TList`, `EntityFactory`, `EntityBaseCore`, `Validation/`, interfaces `I*`, `EntityManager`, `EntityHelper`); scripts `tools/F11-EntityInventory.ps1`, `F11-ConvertToPoco.ps1`, `F11-CleanEntitiesProject.ps1`, `F11-RewriteEntitiesCsproj.ps1`; inventario `DOCUMENTACION/F11_ENTITY_INVENTORY.csv`.
- **F12:** Hotfix B1–B3 (`ErrorUtil` en 13 archivos, `AlertMessage` XSS, `Guid.TryParse` en `ToSelectItem`); perf B4–B5 (`usp_MAT_Viaje_GetSelectList`, `usp_MAT_ReservaHabitacion_CountByHabitacionAndViaje` + `LookupDataAccess`); docs `SPEC.md`, `NETTIERS_MIGRACION_FASES.md`; épica NetTiers marcada completa.
- **Verificación:** MSBuild `MAT.sln` Rebuild Debug OK; cero `*.generated.cs` en `MAT.Entities`.
- **Acción humana pendiente:** publicar `database/2026-07-05_NetTiers_F9_Lookup_SPs.sql` y `database/2026-07-05_NetTiers_F12_Lookup_Perf_SPs.sql`; smoke P0/P1.
- **Estado:** ✅ completo
- **Siguiente task SPEC:** *(épica NetTiers cerrada — ver backlog general en SPEC.md)*

---

### [2026-07-05] — NetTiers F9 + F10: eliminación legado Services/Data
- **Qué se implementó:**
  - **F9:** `MAT.Utilities/LookupDataAccess.cs`; `Helper.cs` migrado de `*Service` a SP + `DBHelper`; SPs `usp_MAT_Proveedor_GetSelectList`, `usp_MAT_PrecioServicio_GetActiveByServicioId`; eliminadas carpetas `MAT.Services/`, `MAT.Web/`, `MAT.WCF/`; borrados `InfopathModel.cs`, `VoucherModel-05122016.cs`; ~30 `using MAT.Services` removidos en MVC.
  - **F10:** `ProjectReference` a `MAT.Data`/`MAT.Data.SqlClient` quitadas de MVC/Utilities; `<MAT.Data>` NetTiers removido de `Web.config` (connection string `MAT.Data.ConnectionString` intacta); proyectos fuera de `MAT.sln`; carpetas `MAT.Data/`, `MAT.Data.SqlClient/`, `MAT.Data.WebServiceClient/` eliminadas.
- **Archivos clave:** `LookupDataAccess.cs`, `Helper.cs`, `MAT.sln`, `MAT.MVC.csproj`, `MAT.Utilities.csproj`, `Web.config`, `MAT.DB` (2 SPs), `database/2026-07-05_NetTiers_F9_Lookup_SPs.sql`, `SPEC.md`, `DOCUMENTACION/NETTIERS_MIGRACION_FASES.md`, `CLAUDE.md`.
- **Verificación:** MSBuild `MAT.sln` Rebuild Debug OK.
- **Acción humana pendiente:** publicar `database/2026-07-05_NetTiers_F9_Lookup_SPs.sql`; smoke P0: login/home, Butaca/Servicio/Excursion dropdowns, Viaje/Create, PersonaCliente/Details (`GetVendedorName`); smoke P1: reserva→pago; arranque IIS Express tras limpieza Web.config.
- **Estado:** ✅ completo
- **Siguiente task SPEC:** NetTiers F11 (POCOs manuales en `MAT.Entities`)

---

### [2026-07-05] — Correcciones post-auditoría Dashboard UX
- **Qué se implementó:**
  - **P1:** Tile Historial de pagos usa `AdminAuthorizationHelper.CanViewAllHistorialPagos` (rol Administrador o admindev).
  - **P2:** Header home con `<p role="doc-subtitle">` (un solo H1 en hero); sidebar sin `active` hardcodeado; script de menú unificado con `isMenuHrefActive`.
  - **P3:** Eliminado CSS huérfano `.quick-access-modern*`; mobile sidebar usa `var(--navbar-height)`; cache bust `?v=20260705e`.
- **Archivos:** `_QuickAccess.cshtml`, `QuickAccessCatalog.cs`, `_Breadcrumb.cshtml`, `_MainNav.cshtml`, `_Layout.cshtml`, `modern-dashboard.css`, `modern-menu.css`, `DASHBOARD_UX_COMMAND_CENTER.md`.
- **Verificación:** MSBuild `MAT.MVC` Debug OK.
- **Estado:** ✅ completo

---

### [2026-07-05] — Dashboard UX Command Center (Opción A)
- **Qué se implementó:**
  - **Accesos frecuentes:** `QuickAccessCatalog` + partial `_QuickAccess.cshtml` — 6 tiles de acción, panel colapsable "Todos los módulos", estilos en `modern-dashboard.css`.
  - **Header:** navbar 56px; título "Panel Principal" en home (`_Breadcrumb.cshtml`).
  - **Sidebar:** partial `_MainNav.cshtml` con secciones Comercial / Operaciones / Finanzas / Catálogo / Herramientas.
  - Documentación de fases pendientes en `DOCUMENTACION/DASHBOARD_UX_COMMAND_CENTER.md`.
  - Cache bust `modern-dashboard.css?v=20260705d`.
- **Archivos:** `Infrastructure/QuickAccessCatalog.cs`, `Views/Home/_QuickAccess.cshtml`, `Views/Home/Index.cshtml`, `Views/Shared/_MainNav.cshtml`, `Views/Shared/_Layout.cshtml`, `Views/Shared/_Breadcrumb.cshtml`, `Content/modern-dashboard.css`, `Content/modern-topbar.css`, `Content/modern-layout.css`, `Content/modern-menu.css`, `MAT.MVC.csproj`, `DOCUMENTACION/DASHBOARD_UX_COMMAND_CENTER.md`.
- **Verificación:** MSBuild `MAT.MVC` Debug.
- **Acción humana pendiente:** smoke manual home + navegación sidebar (ver doc).
- **Estado:** ✅ completo

---

### [2026-07-05] — Home hero band (continuidad con login)
- **Qué se implementó:**
  - Partial `_DashboardHero.cshtml`: franja con imagen Bariloche, overlay slate/fucsia, saludo personalizado y **3 KPIs glass integrados** (presupuestos, clientes, ventas) con click a `openDetalleModal`.
  - Eliminada la fila duplicada de stat cards en `Index.cshtml`; accesos rápidos quedan más arriba.
  - Estilos `.dashboard-hero__layout`, `.dashboard-hero-kpi*` en `modern-dashboard.css`; responsive tablet/mobile (scroll horizontal en KPIs).
  - Limpieza de estilos inline huérfanos (`.modern-stat-*`, `.dashboard-stat-card`) en Index.
  - Cache bust `modern-dashboard.css?v=20260705b`.
- **Archivos:** `Views/Home/_DashboardHero.cshtml`, `Views/Home/Index.cshtml`, `Content/modern-dashboard.css`, `Views/Shared/_Layout.cshtml`.
- **Verificación:** MSBuild `MAT.MVC` Debug OK.
- **Acción humana pendiente:** smoke clicks KPIs + modal; Ctrl+F5.
- **Estado:** ✅ completo

---

### [2026-07-05] — Login split screen (Opción A — Bariloche)
- **Qué se implementó:**
  - **Layout reutilizable:** `_LayoutLogin.cshtml` con split 50/50 (hero izquierda + formulario derecha), tagline de marca, pill DEV fija (`SystemDEV` / `ViewBag.IsDev`), sin cargar el `_Layout` principal (menú, DataTables, etc.).
  - **Estilos:** `login-split.css` — overlay slate + fucsia, float labels y toggle contraseña (Login), formulario scrollable (Registro), responsive (hero franja ~200px en mobile), botón con loading state al submit.
  - **Vistas:** `Login.cshtml` y `RegistrarVendedor.cshtml` migradas al nuevo layout; eliminados hacks inline `!important` y dependencia del gradiente violeta / `loginFondo.jpg`.
  - **Limpieza:** reglas obsoletas de login compact removidas de `modern-account.css` (Manage/Register embebidos siguen usando ese CSS).
  - **Controller:** `AccountController` — `PartialView()` → `View()` en GET Login y GET/POST error RegistrarVendedor para que aplique el layout split (PartialView no renderizaba el shell).
- **Archivos:** `Views/Shared/_LayoutLogin.cshtml`, `Content/login-split.css`, `Views/Account/Login.cshtml`, `Views/Account/RegistrarVendedor.cshtml`, `Content/modern-account.css`, `Controllers/Account/AccountController.cs`, `MAT.MVC.csproj`.
- **Problemas encontrados:** GET devolvía `PartialView()` → login sin panel hero; corregido con `View()`.
- **Verificación:** MSBuild `MAT.MVC` Debug OK.
- **Acción humana pendiente:** smoke manual Login GET/POST (éxito y error) + RegistrarVendedor + banner DEV. Asset hero: `MAT.MVC/Images/login-hero-bariloche.png`.
- **Estado:** ✅ completo (pendiente asset hero + smoke)

---

### [2026-07-04] — Reescritura módulo Paquete (UX/UI + rendimiento)
- **Qué se implementó:**
  - **SQL (MAT.DB):** `usp_MAT_Paquete_GetPaquetes` con filtros (año, `@Search`, `@Temporada`, `@Moneda`), destino (Localidad) y `MonedaCodigo` limpio; `@DateYear=''` sigue devolviendo todos los años (compat selector de Viaje). Nuevos SPs set-based que eliminan el N+1 de los modales: `usp_MAT_Servicio_GetAvailableForPaquete`, `usp_MAT_Precio_GetAvailableForPaquete`, `usp_MAT_Adicional_GetAvailableForPaquete`. `usp_MAT_Paquetes_VinculosByPaqueteID` extendido con `VinculoRowId` (PK de cada tabla de vínculo) para desvincular excursiones sin 2ª consulta.
  - **Data:** `PaqueteDataAccess.GetAvailableServicios/Precios/Adicionales`; `ListPaqueteByYear` con parámetros de filtro + `Destino`; `PaqueteViculosModel.VinculoRowId` mapeado; `PaqueteStandard.Destino`.
  - **Controller:** `Index` con filtros y `ErrorUtil`; `Delete` ahora `[HttpPost]` + `JsonResult`; `Vinculos` con una sola fuente de datos (se eliminó la consulta duplicada de excursiones) + `VinculosContent` para refresh parcial; `Vincular*/Desvincular*` devuelven `{ success, message }`; `RenderGrid*` usan los SPs GetAvailable y el partial unificado `_GridVincular`; se eliminó el POST legacy roto (`RedirectToAction("Details")`).
  - **Assets:** nuevos `Content/mat.paquete.css` y `Scripts/mat.paquete.js` (namespace `MatPaquete`, toasts/confirm/loading vía `MatAdmin`, debounce). Handlers de Paquete extraídos de `mat.jquery.binding.js`.
  - **Vistas:** `Index` (barra de filtros + DataTable + eliminar por AJAX), `Edit` (markup limpio, modal destino migrado a Bootstrap 5, guardado con toasts), `Vinculos` (header contextual + contadores + 4 secciones desde 1 SP) y modales de vinculación unificados en Bootstrap 5. Se eliminaron 8 parciales viejos (Servicios/Excursiones/Precios/Adicionales + RenderGrid*), reemplazados por `_PaqueteVinculosSections.cshtml` y `_GridVincular.cshtml`.
- **Archivos:** `MAT.DB/dbo/Stored Procedures/` (5 SPs), `MAT.DB.sqlproj`, `database/2026-07-04_Paquete_Index_Filters.sql`, `PaqueteDataAccess.cs`, `PaqueteModel.cs`, `PaqueteVinculos.cs`, `PaqueteController.cs`, `Content/mat.paquete.css`, `Scripts/mat.paquete.js`, `mat.jquery.binding.js`, `MAT.MVC.csproj`, vistas de `Views/Paquete/`.
- **Desvíos del plan:** `GetVinculos` e `Insert/Update` de paquete se mantuvieron en la capa `Models` (`ClassPaqueteVinculos` / `PaqueteVinculos`), ya F4 con `DBHelper`, para no romper el layering ni el contrato usado por Reserva/Viaje; solo se estandarizó el JSON en el controller. El accordion de Vínculos se resolvió como cards con contador (más robusto).
- **Verificación:** MSBuild `MAT.MVC` y `MAT.DB` en Debug OK, sin errores de linter.
- **Acción humana pendiente:** publicar `database/2026-07-04_Paquete_Index_Filters.sql` en cada entorno; smoke de: Index con filtros, alta/edición + subir imagen + modal destino, vincular/desvincular los 4 tipos (excursión opcional), eliminar paquete con/sin viajes, detalle solo lectura.
- **Estado:** ✅ completo (pendiente deploy SQL + smoke)

---

## Handoff — próxima sesión (2026-07-05)

**Rama:** `MAT2026` (cambios locales sin commit; revisar `git status` antes de commitear).

### NetTiers F9 + F10 — completadas

- Cadena MVC sin `MAT.Services`, `MAT.Data`, `MAT.Data.SqlClient` (proyectos eliminados del repo).
- `Helper.cs` → `LookupDataAccess` + SPs; legado `MAT.Web`/`MAT.WCF`/`MAT.Data.WebServiceClient` eliminados.
- **Deuda operativa (humano):** publicar `database/2026-07-05_NetTiers_F9_Lookup_SPs.sql` + smoke dropdowns/reserva/pago.

### Siguiente task SPEC

- **NetTiers F11** — Reemplazar `MAT.Entities/*Base.generated.cs` por POCOs manuales (lote pequeño).

---

### [2026-07-02] — Auditoría publish: vistas .cshtml vs MAT.MVC.csproj
- Método: comparar `Views/**/*.cshtml` en disco vs entradas `Content Include` en csproj + referencias `Html.Partial` / `PartialView`.
- **Causa clase de bug:** archivo en disco pero omitido en publish → OK en IIS Express local, 500 en producción.
- **Ya corregidos:** `Views/Transporte/_TransporteForm.cshtml`, `Views/Reserva/_PreReservasSection.cshtml`.
- **Riesgo activo restante (1):** `Reserva/_PreReservasSection` — AJAX `GET /Reserva/GetPreReservasSection` en `Reserva/Index` (fix en csproj, pendiente redeploy).
- **En disco pero NO en csproj (bajo riesgo / legacy):** `Cliente/Facturas.cshtml` (huérfano; ruta real es `PersonaCliente/Facturas`), `*- copia.cshtml`, `DistribucionCoche_backup`, `Hoteles07122016`.
- **Scripts `mat.*` y `Content/**`:** todos incluidos en csproj (0 faltantes).
- **Recomendación:** al crear partial nuevo `_*.cshtml`, agregar línea en `MAT.MVC.csproj` antes de publish; o migrar a `<Content Include="Views\**\*.cshtml" />` (refactor futuro).
- Estado: ✅ auditoría completa + fixes en csproj

### [2026-07-02] — Fix Transporte Create/Edit prod: partial no publicado
- Causa raíz: `Views/Transporte/_TransporteForm.cshtml` existía en disco pero **no** estaba en `MAT.MVC.csproj` → publish a producción omitía el partial → 500 en Create y Edit (Index OK).
- Archivos modificados: `MAT.MVC.csproj`, `PROGRESS.md`
- Acción humana: **republicar MAT.MVC** completo; verificar en servidor que exista `Views\Transporte\_TransporteForm.cshtml`.
- Estado: ✅ fix en repo — pendiente redeploy prod

### [2026-07-02] — Fix Transporte/Edit error 500 (mat.viewdns.net)
- Archivos modificados: `MaestrosDataAccess.cs`, `Transporte/Edit.cshtml`, `Transporte/Create.cshtml`, `database/2026-07-02_Transporte_Edit_Fix_SPs.sql`, `PROGRESS.md`
- Diagnóstico (sin acceso a BD prod): Index OK + Edit falla → probable `usp_MAT_Transporte_GetById` no publicado y/o fallo `@section Scripts` (`jqueryval`). CorrelationId reportado: `48880fe81c5441db80c5df56abf55cad` — verificar en `dbo.ErrorLog`.
- Qué se implementó: lectura segura columna `Tipo` (`GetOptionalString` + `HasColumn`); quitado `@section Scripts` en Create/Edit (validación servidor); script deploy GetById + Update.
- Acción humana: publicar `database/2026-07-02_Transporte_Edit_Fix_SPs.sql` en mat.viewdns.net y redeploy MVC; smoke Edit GUID `a69ba293-4730-4a4d-84ac-54bfff60a642`.
- Estado: ✅ código listo — pendiente deploy BD + smoke en prod

### [2026-07-01] — Localidad L12: cierre épica (docs + grep + eliminar QuickLocalidadSearch)
- Archivos modificados: `HomeController.cs`, `GeoDataAccess.cs`, `SPEC.md`, `PROGRESS.md`, `HANDOFF.md`, `DOCUMENTACION/NETTIERS_MIGRACION_FASES.md`, `DOCUMENTACION/LOCALIDAD_NORMALIZACION_PLAN.md`
- Qué se implementó: eliminado `QuickLocalidadSearch` y `SearchVLocalidad`; grep de cierre OK; MSBuild OK; documentación actualizada; smoke manual pendiente humano (checklist 8 casos en plan).
- Problemas encontrados: ninguno en código
- Estado: ✅ completo (smoke manual a cargo del humano)

### [2026-07-01] — Localidad L11: Helper — deprecar ToSelectEntities("Localidad")
- Archivos modificados: `MAT.Utilities/Helper.cs`, `MAT.Utilities/GeoDataAccess.cs`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: caso `"Localidad"` en `ToSelectEntities` devuelve lista vacía + XML deprecación; `GetAllLocalidades` marcado `[Obsolete]`; `GetLocalidadName` / `ToSelectItem` sin cambios (display por ID).
- Problemas encontrados: ninguno
- Estado: ✅ completo

### [2026-07-01] — Localidad L10: consolidar binding + deprecar QuickLocalidadSearch
- Archivos modificados: `MAT.MVC/Scripts/mat.jquery.binding.js`, `MAT.MVC/Controllers/Home/HomeController.cs`, `PersonaPasajero/Create|Edit.cshtml`, `PersonaProveedor/Create|Edit.cshtml`, `Hotel/Create|Edit.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: eliminados 3 handlers `keyup.autocomplete` legacy (`#txt-busqueda-localidad*`, `#txt-busqueda-destino`); `QuickLocalidadSearch` marcado `[Obsolete]` y delega en `GeoDataAccess.SearchLocalidades`; quitado `.off('keyup.autocomplete')` en vistas L7–L9.
- Problemas encontrados: ninguno
- Estado: ✅ completo

### [2026-07-01] — Pausa: deploy BD antes de L10–L12
- Acción humana: publicar scripts en `database/` (ver handoff arriba)
- Código: L1–L9 completo; L10–L12 pendiente
- Estado: ⏸ pausa — retomar con L10 tras smoke post-deploy

### [2026-07-01] — Localidad L9: Hotel Create/Edit matGeo autocomplete
- Archivos modificados: `MAT.MVC/Views/Hotel/Create.cshtml`, `MAT.MVC/Views/Hotel/Edit.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: `@section Scripts` con `matGeo.initAutocomplete` (`#txt-busqueda-localidad` → `#LocalidadId`); botón `+` opcional; evento `mat:localidad-created.hotelLocalidad`; `.off('keyup.autocomplete')`; Edit simplificado (un solo bloque localidad).
- Problemas encontrados: ninguno
- Estado: ✅ completo — smoke UC-05

### [2026-07-01] — Localidad L8: PersonaProveedor dos autocompletes matGeo
- Archivos modificados: `MAT.MVC/Views/PersonaProveedor/Create.cshtml`, `MAT.MVC/Views/PersonaProveedor/Edit.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: `matGeo.initAutocomplete` en `#txt-busqueda-localidad` + hidden (`#LocalidadId` Create / `#Localidad` Edit) y `#txt-busqueda-localidad-empresa` + `#LocalidadEmpresa`; `minLength: 3`; `.off('keyup.autocomplete')` en ambos campos.
- Problemas encontrados: ninguno
- Estado: ✅ completo — smoke UC-04

### [2026-07-01] — Localidad L7: PersonaPasajero autocomplete matGeo
- Archivos modificados: `MAT.MVC/Views/PersonaPasajero/Create.cshtml`, `MAT.MVC/Views/PersonaPasajero/Edit.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: `matGeo.initAutocomplete` en `#txt-busqueda-localidad` + `#Localidad`; `minLength: 3`; handlers error vía matGeo; `off('keyup.autocomplete')` evita conflicto con binding global; botón `+` opcional con popup y evento `mat:localidad-created.pasajeroLocalidad`.
- Problemas encontrados: binding global en `mat.jquery.binding.js` compite por mismo id — mitigado con `.off` hasta L10
- Estado: ✅ completo — smoke UC-03

### [2026-07-01] — Localidad L6: Paquete Edit destino con matGeo
- Archivos modificados: `MAT.MVC/Views/Paquete/Edit.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Botón `+` en modal destino (`#lnk-Agregar-Localidad-Destino`); cascada `LoadProvincia`/`LoadDepartamento`/`LoadLocalidad` vía `matGeo`; carga inicial con `_paquetePaisId`/`_paqueteProvinciaId`/etc. (sin GUID hardcodeado); `openCreateDialog` refresca `#ddLocalidad`; `setDestino` actualiza `#DestinoID` y `#txtDestino` al aceptar.
- Problemas encontrados: ninguno
- Estado: ✅ completo — smoke UC-01

### [2026-07-01] — Localidad L5: PersonaCliente Create/Edit con matGeo
- Archivos modificados: `MAT.MVC/Views/PersonaCliente/Create.cshtml`, `MAT.MVC/Views/PersonaCliente/Edit.cshtml`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Cascada provincia/depto/localidad vía `matGeo.loadDepartamentos` / `loadLocalidades`; eliminado hardcode 445/5445 en Create; `LoadInfoLocalidad` en Edit con handlers `error`; botón `+` sigue vía `AgregarLocalidad` → `openCreateDialog` (L4).
- Problemas encontrados: ninguno
- Estado: ✅ completo — smoke UC-02

### [2026-07-01] — Localidad L4: popup Create + evento mat:localidad-created
- Archivos modificados: `MAT.MVC/Views/Localidad/Create.cshtml`, `MAT.MVC/Scripts/global.js`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: `fn_Unsubscribe` vía `matGeo.loadDepartamentos`; `AddLocalidad` dispara `matGeo.triggerLocalidadCreated({ localidadId, nombre, idDepartamento })` al OK; handlers `error` en AJAX; fallback sin matGeo. `AgregarLocalidad()` delega en `matGeo.openCreateDialog` con refresh de `#ddDepartamento` / `#ddLocalidad`.
- Problemas encontrados: ninguno
- Estado: ✅ completo — smoke: alta desde Cliente → localidad nueva en dropdown

### [2026-07-01] — Localidad L3: mat.geo.localidad.js
- Archivos modificados: `MAT.MVC/Scripts/mat.geo.localidad.js`, `Views/Shared/_Layout.cshtml`, `Views/Shared/_LayoutAdmin.cshtml`, `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Módulo global `window.matGeo` con `loadProvincias`, `loadDepartamentos`, `loadLocalidades`, `initAutocomplete`, `openCreateDialog`, `onLocalidadCreated`/`offLocalidadCreated`, `triggerLocalidadCreated`, `handleJsonError`, `handleAjaxError`. Parse JSON con `JSON.parse` (sin `eval`). Script incluido tras `mat.jquery.functions.js` en layouts principal y admin.
- Problemas encontrados: ninguno
- Estado: ✅ completo — smoke: consola sin errores en Home; `typeof matGeo === 'object'`

### [2026-07-01] — Localidad L1–L2: pasada de optimización (pre-L3)
- Archivos modificados: `MAT.Utilities/GeoDataAccess.cs`, `MAT.MVC/Controllers/Localidad/LocalidadController.cs`, `MAT.MVC/Models/LocalidadLookupDto.cs`, `PROGRESS.md`
- Qué se implementó: `GeoDataAccess` — `ReadList`/`MapVLocalidad` genéricos, `MinSearchTermLength`, `SearchVLocalidad` delega en `SearchLocalidades` (un solo SP). `LocalidadController` — helpers `TryParsePositiveInt`, `SerializeJson`, `ToJson`, respuestas JSON tipadas (`JsonCascade`, `JsonGeoInfo`, etc.); `GetDepartamento` sin re-ordenar (SP ya ordena). `LocalidadLookupDto.From(VLocalidad)`.
- Problemas encontrados: helpers estáticos no pueden llamar `Controller.Json` — resuelto con `new JsonResult`.
- Estado: ✅ completo — MSBuild OK; contratos JSON legacy intactos

### [2026-07-01] — Localidad L2: LocalidadController API completa
- Archivos modificados: `MAT.MVC/Controllers/Localidad/LocalidadController.cs`, `MAT.MVC/Models/LocalidadLookupDto.cs`, `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Refactor `GetProvincia`, `GetLocalidad`, `GetInfoByLocalidadId` → `GeoDataAccess` (cero `DBHelper` en controller). Nueva acción `Search(term, idProvincia?, idDepartamento?)` → `List<LocalidadLookupDto>`. `AddLocalidad` devuelve `Id` del insert. Shim `LProvincia` en `GetProvincia`. Validación numérica en cascada y mensajes user-safe.
- Problemas encontrados: ninguno
- Estado: ✅ completo — smoke con SP L1 publicado + sesión autenticada en Pasajero/Proveedor autocomplete

### [2026-07-01] — Localidad L1: GeoDataAccess + SP búsqueda
- Archivos modificados: `MAT.Utilities/GeoDataAccess.cs`, `MAT.DB/dbo/Stored Procedures/usp_MAT_Localidad_Search.sql`, `MAT.DB/MAT.DB.sqlproj`, `database/2026-07-01_Localidad_Normalizacion_SPs.sql`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Clase `LocalidadGeoInfo`; métodos `GetProvinciasByPaisId`, `GetLocalidadesByDepartamentoId`, `GetLocalidadGeoInfo`, `SearchLocalidades` (wrappers legacy + SP nuevo con filtros opcionales provincia/departamento, mín. 3 chars). SP `usp_MAT_Localidad_Search` registrado en sqlproj.
- Problemas encontrados: ninguno
- Estado: ✅ completo — **publicar** `database/2026-07-01_Localidad_Normalizacion_SPs.sql` en BD local antes de L2 smoke

### [2026-07-01] — SPEC: tasks Localidad L1–L12 (normalización post F2)
- Archivos modificados: `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Sección **Normalización Localidad (post NetTiers F2)** en SPEC § P2 con 12 tasks secuenciales (L1–L12), referencia a `DOCUMENTACION/LOCALIDAD_NORMALIZACION_PLAN.md`. Actualizado handoff en PROGRESS: siguiente task = L1.
- Problemas encontrados: ninguno
- Estado: ✅ completo

---

**Rama:** `MAT2026` (cambios locales sin commit; revisar `git status` antes de commitear).

### Hecho hoy (código compila OK)

| Bloque | Estado |
|--------|--------|
| NetTiers F0 + F1 + F3 | Implementado; SPEC F3 `[x]` |
| ErrorUtil en Transporte/Servicio/Butaca | Normalizado |
| Transporte ABM UX (Create/Edit/Index + Tipo) | Implementado; ver entrada abajo |

### Pendiente operativo (BD + smoke)

1. **Publicar SPs en SQL Server** del entorno local (Publish `MAT.DB` o scripts en `database/`):
   - `database/2026-06-30_NetTiers_F3_Maestros_SPs.sql` (índice → 12 SPs maestros)
   - `database/2026-06-30_Transporte_Tipo_UI.sql` → `usp_MAT_Transporte_Insert`, `usp_MAT_Transporte_GetListTransporte`
2. **Smoke manual:**
   - `/Transporte/Index`, Create, Edit (4 tipos: MINIBUS, CAMION 4X4, PISOELEVADO, DOBLEPISO)
   - `/Servicio/Index`, `/Butaca/Index` (ABM + alertas de error)
   - Reserva: mapa de butacas con un coche de cada tipo
3. **SQL útil:** `SELECT DISTINCT Tipo FROM dbo.Transporte ORDER BY 1` (legacy sin tipo → editar y asignar)

### Siguiente task SPEC (elegir uno)

- **NetTiers F2** — catálogo geográfico
- **NetTiers F4** — Paquete / `DataRepository` en `PaqueteModel.GenerarPasajes`
- **Viaje rentabilidad** — validación con negocio (SPEC § Viaje)

### Archivos clave Transporte UX

- `MAT.Enums/eTipoTransporte.cs`, `MAT.MVC/Models/TransporteFormViewModel.cs`
- `MAT.MVC/Views/Transporte/_TransporteForm.cshtml`, `Create.cshtml`, `Edit.cshtml`, `Index.cshtml`
- `TransporteController.cs`, `TransporteModel.cs`, `MaestrosDataAccess.cs`

**Nota:** Aéreo = `Viaje.Medio` (`AEREO`), no `Transporte.Tipo`.

---

### [2026-07-01] — Fixes post-auditoría NetTiers F2

- Archivos modificados:
  - `MAT.MVC/Views/Localidad/Create.cshtml` — alerts de error usan `response.Result`
  - `MAT.MVC/Controllers/Localidad/LocalidadController.cs` — `[Authorize]` a nivel clase
  - `MAT.MVC/Controllers/Home/HomeController.cs` — `[Authorize]` en `QuickLocalidadSearch`
  - `MAT.Utilities/GeoDataAccess.cs` — búsqueda retorna vacío si query tiene menos de 3 caracteres
  - `MAT.DB/.../usp_MAT_VLocalidad_Search.sql` — mismo mínimo en SP
  - `MAT.MVC/Models/VoucherModel.cs`, `VoucherModel-05122016.cs` — null-check `Pasajero`
- Qué se implementó: Correcciones puntos 1–4 de auditoría F2 (UI errores, auth geo, minLength búsqueda, NRE voucher)
- Problemas encontrados: ninguno en build
- Métricas: MSBuild MAT.MVC Debug OK
- Estado: ✅ completo — **republicar `usp_MAT_VLocalidad_Search` en BD**
- Siguiente task: NetTiers F4

---

- Archivos modificados/creados:
  - `MAT.Utilities/GeoDataAccess.cs` — Pais, Provincia, Departamento, Localidad, VLocalidad
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_Pais_GetAll.sql`, `_Provincia_GetById`, `_Departamento_GetByProvinciaId`, `_Localidad_GetById`, `_Localidad_GetAll`, `_Localidad_Insert`, `_VLocalidad_Search`
  - `MAT.DB/MAT.DB.sqlproj`, `database/2026-07-01_NetTiers_F2_Geo_SPs.sql`
  - `LocalidadController.cs`, `PaqueteController.cs`, `HomeController.cs`
  - `PaqueteModel.cs`, `VoucherModel.cs`, `InfopathModel.cs`, `VoucherModel-05122016.cs`
  - `MAT.Utilities/Helper.cs` (ramas geo)
- Qué se implementó: Cero `PaisService`…`VLocalidadService` en `MAT.MVC` y `MAT.Utilities`; `GeoDataAccess` + SP; `ErrorUtil` en `PaqueteController.Edit` y `QuickLocalidadSearch`; validación en `AddLocalidad`
- Problemas encontrados: `HomeController` perdió `using System.Configuration` al agregar `ErrorUtil` — restaurado
- Métricas: MSBuild MAT.MVC Debug OK (warnings preexistentes)
- Estado: ✅ completo — **publicar SPs F2 en BD**
- Siguiente task: NetTiers F4

---

- Archivos modificados/creados:
  - `MAT.Enums/eTipoTransporte.cs` — MINIBUS, CAMION 4X4, PISOELEVADO, DOBLEPISO (códigos BD)
  - `MAT.Utilities/EnumExtensions.cs` — ToSelectListByDbValue, GetDisplayName, validación Tipo
  - `MAT.MVC/Models/TransporteFormViewModel.cs`, `TransporteModel.cs`, `TransporteController.cs`
  - `MAT.MVC/Infrastructure/Data/MaestrosDataAccess.cs` — GetTransporteFormById
  - `MAT.MVC/Views/Transporte/_TransporteForm.cshtml`, `Create.cshtml`, `Edit.cshtml`, `Index.cshtml`
  - `MAT.DB/.../usp_MAT_Transporte_Insert.sql`, `usp_MAT_Transporte_GetListTransporte.sql`
  - `database/2026-06-30_Transporte_Tipo_UI.sql` (índice despliegue)
- Qué se implementó: Formulario unificado con validación; campo Tipo obligatorio; quitados Km/Último Service de UI; Index con badge de tipo; Edit POST con model binding + AntiForgery; Km/UltimoService preservados en BD al editar
- Problemas encontrados: MAT.Utilities requería referencia a System.ComponentModel.DataAnnotations
- Métricas: MSBuild MAT.MVC Debug OK
- Estado: ✅ completo — **publicar SPs Insert/GetList en BD**
- Siguiente task: NetTiers F2 o F4

---


- Archivos modificados/creados:
  - **F0:** `DOCUMENTACION/NETTIERS_MIGRACION_FASES.md` §5 inventario; `SPEC.md` F0 marcado
  - **F1:** `MAT.sln` (sin MAT.Web), `MAT.MVC/Web.config` (sin tagPrefix/httpModule MAT.Web), `MAT.Data.WebServiceClient/README.md`, `CLAUDE.md` política congelamiento
  - **F3 schema:** 12 SPs en `MAT.DB` + `usp_MAT_Habitacion_GetById` (+HotelID); `MAT.DB.sqlproj`; `database/2026-06-30_NetTiers_F3_Maestros_SPs.sql` (índice)
  - **F3 código:** `MaestrosDataAccess.cs`, `ButacaModel.cs`, `TransporteMethod`/`ServicioMethod` extendidos, `ButacaController`, `TransporteController`, `ServicioController`, lookups en `PasajeModel`, `VoucherModel`, `PaqueteModel`, `InfopathModel`, `ViajeController`, `PaqueteController`
- Qué se implementó: Migración Transporte/Servicio/Butaca de NetTiers a DBHelper+SP; cero `TransporteService`/`ServicioService`/`ButacaService` en `.cs` compilados de MVC
- Problemas encontrados: conflicto namespace `Butaca` en controller (resuelto con `MAT.Entities.Butaca`)
- Métricas: MSBuild MAT.MVC Debug OK (warnings preexistentes)
- Estado: ✅ completo — **requiere publicar SPs en BD** del entorno
- Siguiente task: NetTiers F2 (geo) o F4 (Paquete)

---

### [2026-06-30] — NetTiers: épica dividida en fases F0–F12

- Archivos modificados/creados:
  - `SPEC.md` — épica NetTiers desglosada en 13 sub-tasks (F0 inventario … F12 limpieza)
  - `DOCUMENTACION/NETTIERS_MIGRACION_FASES.md` — plan, patrones DBHelper vs Service, orden y riesgos
- Qué se implementó: Solo documentación/planificación. Cadena actual MAT.MVC → Services → Data → SqlClient; dualidad DBHelper+SP ya usada en módulos nuevos.
- Problemas encontrados: Ninguno.
- Estado: 🔄 F0 pendiente de ejecución (inventario con grep por servicio)

---

### [2026-06-30] — Viaje: investigación rentabilidad y gastos por viaje

- Archivos modificados/creados:
  - `SPEC.md` — nueva sección *Viaje — Rentabilidad y gastos* (hallazgos, opciones A–D, tasks P2)
  - `DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md` — investigación detallada
- Qué se implementó: Investigación documentada — ingresos ya trazables vía `Pasaje.ViajeID` y reportes Admin; gap principal en costos (`FacturaFiscal` compra sin viaje; planilla legacy con UI retirada; no hay `ViajeGasto`). Recomendación preliminar híbrido A+B.
- Problemas encontrados: Ninguno (solo documentación).
- Estado: ✅ investigación completa; implementación pendiente de validación con negocio

---

### [2026-06-30] — ReservaController: normalización ErrorUtil

- Archivos modificados: `MAT.MVC/Controllers/Reserva/ReservaController.cs`
- Qué se implementó: 13 bloques `catch` que exponían `e.Message` reemplazados por `ErrorUtil.LogAndGetPublicMessage` (partials, JsonResult, `DetalleViaje`, `FormReserva` mantiene `return "Error"` al cliente)
- Problemas encontrados: Ninguno en MSBuild MAT.MVC.
- Estado: ✅ completo

---

### [2026-06-30] — Reserva/Index UX Fases 1–2 (header operativo)

- Archivos modificados/creados:
  - `MAT.MVC/Controllers/Reserva/ReservaController.cs` — `GetDetalleViajeModel`, `ViewBag.Title`, `BreadcrumbActiveTitle`, `OcultarResumenViajeEnPanel`
  - `MAT.MVC/Views/Reserva/Index.cshtml` — hero, sidebar server-side, sin AJAX `getDetalleViaje`
  - `MAT.MVC/Views/Reserva/_ReservaViajeHero.cshtml` — **nuevo** partial hero contextual
  - `MAT.MVC/Views/Reserva/DetalleViaje.cshtml` — ocultar resumen duplicado en panel lateral
  - `MAT.MVC/Views/Shared/_Breadcrumb.cshtml` — `ViewBag.BreadcrumbActiveTitle`
  - `MAT.MVC/Content/mat.styles.custom.css` — estilos `.reserva-index-page`, `.reserva-viaje-hero*`
  - `MAT.MVC/MAT.MVC.csproj`
  - `DOCUMENTACION/RESERVA_INDEX_UX_MEJORAS.md`
- Qué se implementó:
  - Header genérico reemplazado por hero con nombre del viaje, destino, fechas y transporte al primer paint
  - Breadcrumb activo con nombre abreviado del viaje (no "Listado")
  - Panel lateral renderizado server-side; una sola carga de `DetalleViaje` (sin AJAX redundante)
- Problemas encontrados: Ninguno en MSBuild MAT.MVC.
- **Correcciones post-auditoría:** `ErrorUtil` en `Index`; try separado para detalle; hero sin exigir `Descripcion`; breadcrumb con fallback destino/ID; `ViewBag.Title` ya no se pisa en la vista.
- Estado: ✅ completo
- Siguiente task sugerido: Fase 3 — strip de KPIs (`DOCUMENTACION/RESERVA_INDEX_UX_MEJORAS.md`)

---

### [2026-06-30] — DistribucionCoche Fase 3: modal en Reserva/Index — **revertido (decisión UX)**

- Archivos tocados en el experimento (revert manual pendiente por el usuario):
  - `MAT.MVC/Controllers/Reserva/ReservaController.cs`
  - `MAT.MVC/Views/Reserva/DistribucionCoche.cshtml`
  - `MAT.MVC/Content/mat.distribucioncoche.css`
  - `MAT.MVC/Scripts/mat.jquery.binding.js`
  - `MAT.MVC/Scripts/mat.jquery.functions.js` (`ShowFormDialogIframe`, `matInjectDialogHtml`, etc.)
- Qué se probó: modal en `Reserva/Index` (partial + iframe) en lugar de `window.open`.
- **Decisión:** no usar modal; volver a **pestaña standalone**. Revert de código a cargo del usuario.
- Problemas del enfoque modal: error 500 con partial embed; iframe operativo pero UX no preferida.
- Estado: ✅ task cerrado en SPEC (decisión documentada); código en revert manual

---

- Archivos modificados/creados:
  - `MAT.MVC/Infrastructure/AdminContextualTourCatalog.cs` — definición de tours por pantalla
  - `MAT.MVC/Infrastructure/AdminHelpCatalog.cs` — ~12 ítems categoría Intranet
  - `MAT.MVC/Controllers/Admin/AdminController.cs` — `POST OnboardingReset`
  - `MAT.MVC/Views/Shared/_AdminTopbar.cshtml` — menú: guía contextual + reiniciar onboarding
  - `MAT.MVC/Views/Shared/_AdminHelpModal.cshtml` — JSON tours contextuales, copy buscador
  - `MAT.MVC/Views/Shared/_LayoutAdmin.cshtml` — `data-admin-user-id` para localStorage
  - `MAT.MVC/Scripts/mat.admin-help.js` — intranet UI, tours contextuales, reset onboarding
  - `MAT.MVC/Content/admin.modern.css` — badge Intranet en resultados
  - `MAT.MVC/Views/Reportes/Index.cshtml`, `Admin/Usuarios.cshtml`, `SistemaParametros.cshtml`, `ErrorLog.cshtml` — anclas tour
  - `MAT.MVC/MAT.MVC.csproj`
  - `SPEC.md`, `DOCUMENTACION/MANUAL_USUARIO_ADMINISTRADOR.md`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`
- Qué se implementó:
  - **Fase 0:** SPEC actualizado (topbar [x], copy onboarding); manual §2.4; fix duplicado HistorialPrecios
  - **Fase 1:** reset onboarding desde menú usuario con confirmación y toast
  - **Fase 2:** búsqueda Ctrl+K incluye módulos intranet con badge visual
  - **Fase 3:** tours contextuales (Reportes, Usuarios, Parámetros, Error Log) con persistencia localStorage
- Problemas encontrados: Ninguno en MSBuild MAT.MVC.
- Estado: ✅ completo

---

### [2026-06-19] — Admin UI: barra superior y menú lateral (plan completo UX/UI)

- Archivos modificados/creados:
  - `MAT.MVC/Infrastructure/AdminNavModels.cs` — view models nav/topbar/page context
  - `MAT.MVC/Infrastructure/AdminHelpCatalog.cs` — sidebar sections, ResolvePage, títulos canónicos
  - `MAT.MVC/Views/Shared/_AdminTopbar.cshtml`, `_AdminNav.cshtml` — partials unificados
  - `MAT.MVC/Views/Shared/_LayoutAdmin.cshtml` — layout simplificado, breadcrumb automático
  - `MAT.MVC/Scripts/mat.admin-nav.js` — active state, secciones colapsables, sidebar colapsado, offcanvas
  - `MAT.MVC/Scripts/mat.admin-help.js` — tour menú usuario, búsqueda solo topbar
  - `MAT.MVC/Content/admin.modern.css` — tokens topbar, nav activa, sidebar colapsado, offcanvas footer
  - `MAT.MVC/Views/Admin/Index.cshtml`, `UsuarioEditar.cshtml`, `UsuarioResetPassword.cshtml`
  - `MAT.MVC/MAT.MVC.csproj`
  - `DOCUMENTACION/MANUAL_USUARIO_ADMINISTRADOR.md`, `PROGRESS.md`
- Qué se implementó:
  - **Fase 1:** topbar con botón Buscar + menú usuario (guía, panel, intranet, logout); partial `_AdminTopbar`
  - **Fase 2:** navegación lateral desde `AdminHelpCatalog.GetSidebarSections`; partial único `_AdminNav`; `mat.admin-nav.js`
  - **Fase 3:** título de página en topbar; breadcrumb automático desde catálogo; breadcrumbs custom en UsuarioEditar/ResetPassword
  - **Fase 4:** secciones colapsables, sidebar colapsable desktop, userbox con iniciales, acciones en offcanvas mobile, topbar compact al scroll
- Problemas encontrados: Ninguno en MSBuild MAT.MVC.
- Estado: ✅ completo (smoke test manual desktop + mobile recomendado)

---

### [2026-06-19] — Admin: mejoras UX onboarding y buscador (UX-01..10)

- Archivos modificados:
  - `MAT.MVC/Scripts/mat.admin-help.js` — empty state, posición adaptativa tour card, dropdown Ayuda paso 6, Escape/focus/reduced-motion, barra progreso, collapse hub search, replaceState `?tour=1`, toast OnboardingStatus, flechas modal
  - `MAT.MVC/Views/Admin/Index.cshtml` — buscador al final del hub; hint compacto post-onboarding
  - `MAT.MVC/Views/Shared/_AdminHelpModal.cshtml` — barra de progreso tour
  - `MAT.MVC/Content/admin.modern.css`, `_LayoutAdmin.cshtml` (cache bust)
  - `DOCUMENTACION/MANUAL_USUARIO_ADMINISTRADOR.md`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: P1/P2 del plan UX — menos ruido visual en Index, flujo de tour alineado al DOM, accesibilidad básica y pulido de búsqueda modal.
- Problemas encontrados: Ninguno en compilación.
- Estado: ✅ completo (smoke test manual recomendado)

---

### [2026-06-19] — Admin: onboarding guiado y buscador de funciones

- Archivos modificados:
  - `MAT.DB/dbo/Tables/AdminUsuarioPreferencia.sql`, `usp_MAT_AdminUsuarioPreferencia_Get.sql`, `usp_MAT_AdminUsuarioPreferencia_SetOnboarding.sql`, `MAT.DB.sqlproj`
  - `database/2026-06-19_AdminUsuarioPreferencia.sql`
  - `MAT.MVC/Infrastructure/AdminHelpCatalog.cs`
  - `MAT.MVC/Controllers/Admin/AdminController.cs` — `OnboardingStatus`, `OnboardingComplete`
  - `MAT.MVC/Scripts/mat.admin-help.js`, `Content/admin.modern.css`
  - `MAT.MVC/Views/Shared/_AdminHelpModal.cshtml`, `_LayoutAdmin.cshtml`
  - `MAT.MVC/Views/Admin/Index.cshtml`
  - `MAT.MVC/MAT.MVC.csproj`
  - `DOCUMENTACION/MANUAL_USUARIO_ADMINISTRADOR.md`, `DOCUMENTACION/README.md`, `SPEC.md`, `PROGRESS.md`
- Qué se implementó: Tour guiado con spotlight en hub Admin (primera visita persistida en BD por UserId). Buscador en Index + modal global (Ctrl+K). Botón Ayuda en topbar (reabrir guía / buscar). Catálogo centralizado ~18 ítems (+ ADMINDEV).
- Problemas encontrados: Requiere aplicar script SQL en entorno local antes de probar persistencia.
- Estado: ✅ completo (aplicar migración SQL + smoke test manual)

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
### [2026-07-03] — NetTiers F4: Paquete, precios y voucher migrados

- Archivos nuevos:
  - `MAT.DB/dbo/Stored Procedures/`: `usp_MAT_Paquete_GetById.sql`, `usp_MAT_Paquete_UpdateEntity.sql`, `usp_MAT_Paquete_DeleteCascade.sql` (⚡ transaccional), `usp_MAT_Viaje_CountByPaqueteId.sql`, `usp_MAT_PaqueteServicio_GetByPaqueteId/_Insert/_DeleteByServicioAndPaquete.sql`, `usp_MAT_PaqueteExcursion_GetByPaqueteId.sql`, `usp_MAT_PaquetePrecio_GetByPaqueteId/_Insert/_DeleteByPrecioAndPaquete.sql`, `usp_MAT_PaqueteAdicional_GetByPaqueteId/_Insert/_DeleteByAdicionalAndPaquete.sql`, `usp_MAT_Precio_GetById/_GetAllEntities/_DeleteCascade.sql` (⚡), `usp_MAT_Adicional_GetAll/_GetById/_Insert/_Update/_Delete.sql` (⚡ cascada), `usp_MAT_Excursion_GetAll/_Insert/_Update/_Delete.sql` (⚡ cascada), `usp_MAT_Voucher_GetById/_Insert.sql`, `usp_MAT_PrecioHabitacion_GetAll.sql`, `usp_MAT_Pasaje_GenerarByViaje.sql` (⚡ set-based)
  - `MAT.MVC/Infrastructure/Data/PaqueteDataAccess.cs`, `PasajeDataAccess.cs`, `VoucherDataAccess.cs`
  - `database/2026-07-03_NetTiers_F4_Paquete_SPs.sql` (índice de publicación)
- Archivos modificados: `MaestrosDataAccess.cs` (CRUD Adicional/Excursion), `PaqueteController.cs`, `PrecioController.cs`, `AdicionalController.cs`, `ExcursionController.cs`, `PaqueteModel.cs`, `SeleccionarPasajeroModel.cs`, `InfopathModel.cs`, `VoucherModel.cs`, `RegistroPagoModel.cs`, `PlanillaHotelModel.cs`, `ReservaModel.cs`, `FacturaModel.cs`, `PagosClientesModel.cs`, `MAT.MVC.csproj`, `MAT.DB.sqlproj`, `SPEC.md`
- Qué se implementó:
  - **GenerarPasajes sin DataRepository**: `usp_MAT_Pasaje_GenerarByViaje` inserta set-based (una fila por butaca del transporte) con transacción en el SP y `@Generados OUTPUT`; `PaqueteModel.GenerarPasajes` devuelve `generados > 0` (preserva `false` si no hay butacas o falla). **Era el único uso de `DataRepository`/`TransactionManager` en MAT.MVC — precondición de F10 cumplida.**
  - **Cascadas movidas a SPs transaccionales** (precedente F3 `usp_MAT_Servicio_Delete`): delete de Paquete (4 tablas de vínculo + guard de viajes con RAISERROR), Precio (vínculos + `Pasaje.PrecioID = NULL`), Adicional y Excursion (vínculos + maestro).
  - **Desvincular X de paquete**: los `GetAll().Where(...).FirstOrDefault()` + `Delete(PK)` se reemplazaron por SPs `_DeleteBy<X>AndPaquete` que borran por par (servicio/precio/adicional, paquete) y devuelven filas afectadas. Nota: si existieran vínculos duplicados, ahora se eliminan todos (antes quedaba uno fantasma imposible de quitar por UI).
  - **Decisiones**: check de viajes antes del delete se mantiene en C# (`CountViajesByPaqueteId`) para conservar el mensaje amigable + guard redundante dentro del SP. `usp_MAT_Precio_Insert` existente se reutiliza para el alta de entidad Precio (PrecioID lo genera el default de la tabla; equivalente porque el flujo no usa el Guid del cliente). `usp_MAT_Paquete_UpdateEntity` actualiza solo columnas conocidas por la entidad (no toca ImageId/GalleryId, igual que NetTiers). GetAll+LINQ se mantuvo en `PlanillaHotelModel` (PrecioHabitacion) y grillas de vinculación para no cambiar comportamiento.
- Problemas encontrados: la entidad `PaqueteExcursion` no tiene `IsOpcional` (columna posterior a la generación NetTiers) — el mapper no la mapea, paridad con el service. Corregido y recompilado.
- Pendiente de despliegue: publicar SPs de `database/2026-07-03_NetTiers_F4_Paquete_SPs.sql` en la BD de cada entorno antes del smoke.
- Estado: ✅ MSBuild MAT.sln Debug OK (MAT.MVC + MAT.DB dacpac)

### [2026-07-03] — NetTiers F5: Viaje y ReservaHabitacion migrados

- Archivos nuevos:
  - `MAT.DB/dbo/Stored Procedures/`: `usp_MAT_Viaje_GetAllEntities/_GetEntityById/_InsertEntity/_UpdateEntity.sql`, `usp_MAT_ReservaHabitacion_GetById/_GetByPasajeId/_GetByHabitacionId/_UpdateEntity.sql`, `usp_MAT_VConsultaReservaHabitacion_GetByHabitacionId.sql`, `usp_MAT_Habitacion_GetEntitiesByHotelId.sql`, `usp_MAT_Habitacion_UpdateEntity.sql`
  - `MAT.MVC/Infrastructure/Data/ViajeDataAccess.cs`, `ReservaHabitacionDataAccess.cs`
  - `database/2026-07-03_NetTiers_F5_Viaje_SPs.sql`
- Archivos modificados: `ViajeController.cs`, `ReservaHabitacionController.cs`, `PersonaClienteController.cs` (ConfirmarCambioHabitacion), `HomeController.cs` (2 dashboards), `PasajeroViajeController.cs` (ViewBag.Viaje), `Views/PasajeroViaje/Manifiesto.cshtml`, `ListadoSimple.cshtml`, `ListadoSimpleToExport.cshtml`, `Views/Hotel/Create.cshtml` (@using muerto), `PasajeModel.cs`, `VoucherModel.cs`, `InfopathModel.cs`, `FacturaModel.cs`, `PagosClientesModel.cs`, `PaqueteModel.cs`, `PlanillaHotelModel.cs`, `SearchModel.cs`, `MAT.MVC.csproj`, `MAT.DB.sqlproj`, `SPEC.md`
- Qué se implementó:
  - **Entidad Viaje**: SPs `*Entity*` cubren solo las 19 columnas conocidas por la entidad NetTiers (no tocan `TiempoConsentracion`, `Observaciones`, `MonedaTipo`, `IsPublicWeb` — igual que el service). `ViajeDataAccess.Insert` genera `Guid.NewGuid()` si la entidad llega con `Guid.Empty` (el flujo legacy `Create(FormCollection)` no tiene POST activo desde ninguna vista — creación real vía `InsertViaje`/`ViajeMethod`; se documenta la mejora defensiva).
  - **Vista NetTiers `VConsultaReservaHabitacion`** → SP dedicado sobre `dbo.vConsultaReservaHabitacion` con `@HabitacionID` obligatorio y `@Expiro` opcional. El call-site (`ReservaHabitacionController.Actualizar`) hacía `GetAll()` de la vista completa + LINQ dentro de un loop por habitación (N+1 sobre tabla entera) — filtro trivialmente traducible, se empujó a SQL (decisión documentada).
  - **Habitacion**: `GetEntitiesByHotelId` nuevo (el `usp_MAT_Habitacion_GetByHotelId` legacy devuelve DTO display con tipo/estado como texto — incompatible con la entidad; se conserva). `UpdateEntity` solo columnas de la entidad (no toca `Precio`/`Descripcion` agregadas en 2024).
  - **3 vistas Razor de PasajeroViaje**: eliminado `@using MAT.Services` + instanciación inline de `ViajeService`; el controller carga `ViewBag.Viaje` con `ViajeDataAccess.GetById(Id)`. En `ListadoSimple`, si el modelo llega vacío la vista ya no lanza `InvalidOperationException` por `Model.First()` (mejora colateral mínima).
  - También migró `PaqueteServicioService` residual en `ViajeController.Create` (instancia calificada `new Services.X()` que el grep de cierre de F4 no capturaba — patrón de grep corregido para las fases siguientes).
- Problemas encontrados: ninguno de compilación. ⚠ Las vistas Razor no compilan con MSBuild — Manifiesto/ListadoSimple/Export requieren smoke manual en runtime.
- Pendiente de despliegue: publicar SPs de `database/2026-07-03_NetTiers_F5_Viaje_SPs.sql`.
- Estado: ✅ MSBuild MAT.sln Debug OK

### [2026-07-03] — NetTiers F6: Facturación y pagos operativos migrados (crítico)

- Archivos nuevos:
  - `MAT.DB/dbo/Stored Procedures/`: `usp_MAT_Factura_GetEntityById/_GetEntitiesByClienteId/_UpdateEntity/_GetSaldoInfo.sql`, `usp_MAT_Pasaje_GetEntityById/_GetByFacturaId/_GetEntitiesByViajeId/_GetByPasajeroId/_UpdateEntity.sql`, `usp_MAT_Pago_GetEntityById/_GetByVendedorId/_GetByFacturaId/_UpdateEntity.sql`, `usp_MAT_MovimientoCuenta_GetByPagoId.sql`
  - `MAT.MVC/Infrastructure/Data/FacturaDataAccess.cs` (con DTO `FacturaSaldoInfo`), `PagoDataAccess.cs`
  - `database/2026-07-03_NetTiers_F6_Factura_SPs.sql` (⚠ incluye ALTERs de paridad de esquema)
- Archivos modificados: `MAT.DB/dbo/Tables/Factura.sql`, `Pago.sql`, `MovimientoCuenta.sql` (columnas de paridad), `PasajeDataAccess.cs` (extendido), `MATContext.cs` (Saldo), `FacturaModel.cs`, `PagosClientesModel.cs`, `RegistroPagoModel.cs`, `PagoController.cs`, `PersonaVendedorController.cs`, `PersonaClienteController.cs` (ElegirNuevaButaca), `PasajeModel.cs`, `PasajeroHistorialModel.cs`, `ListaFacturasModel.cs`, `PagosPorFechaModel.cs`, `InfopathModel.cs`, `VoucherModel.cs`, `ReservaModel.cs` (campos muertos), `MAT.MVC.csproj`, `MAT.DB.sqlproj`, `SPEC.md`
- Qué se implementó:
  - **HALLAZGO — MAT.DB desincronizado con la BD real:** las entidades NetTiers (generadas desde la BD) referencian columnas que no estaban en SSDT: `Factura.DescuentoAplicado`, `Pago.ClienteID`, `Pago.EstadoRendicion`, `Pago.CuentaCorrienteID`, `MovimientoCuenta.DebitoID`. Con `useStoredProcedure="false"` el provider arma SQL dinámico con esas columnas y los flujos corren en producción (p. ej. `Pago.ClienteId.Value` en `PagosPorFechaModel`, `.Where(p => p.DebitoId.HasValue)` en `CalcularSaldo`) ⇒ existen en la BD operativa. Se agregaron a las tablas SSDT y la migración usa guardas `IF COL_LENGTH(...) IS NULL` (no-op donde ya existan). **Omitirlas habría roto el cálculo de saldo con débitos.**
  - **Saldos**: `usp_MAT_Factura_GetSaldoInfo` devuelve `Monto`, `TotalPagos`, `TotalDebitos` set-based; reemplaza dos N+1. La resta queda en C# por call-site porque las semánticas difieren: `MATContext.Saldo` = Monto − Pagos (SIN débitos, como siempre); `FacturaModel/FacturaPagosModel.CalcularSaldo` = Monto − Pagos − Débitos. `FacturaPagosModel` además necesita la lista de pagos ⇒ `usp_MAT_Pago_GetByFacturaId` (join MovimientoCuenta→Pago, un roundtrip) y el total de pagos se sigue sumando en C# (misma aritmética double).
  - **RegistroPagoModel.ActualizarEstados**: `facturaService.Save(Factura)` sobre factura existente ⇒ `FacturaDataAccess.Update` (documentado). La orquestación pago total → vouchers → estados de pasajes se migró 1:1 **sin transacción** (comportamiento idéntico al actual); SP transaccional queda como deuda futura.
  - Updates de entidad limitados a columnas conocidas por la entidad: `Factura_UpdateEntity` no toca `MonedaTipo`.
  - Campos de servicio muertos eliminados en ReservaModel/VoucherModel/PasajeroHistorialModel (solo se instanciaban).
- Problemas encontrados: ninguno de compilación.
- Pendiente de despliegue: correr `database/2026-07-03_NetTiers_F6_Factura_SPs.sql` (los ALTERs son idempotentes) + SPs. **Smoke prioritario**: seña → pago total → vouchers/estados; saldo idéntico pre/post con la misma factura.
- Estado: ✅ MSBuild MAT.sln Debug OK

### [2026-07-03] — NetTiers F7: Personas, clientes y cuenta corriente migrados

- Archivos nuevos:
  - `MAT.DB/dbo/Stored Procedures/` (28 SPs): `usp_MAT_Persona_GetEntityById/_GetAllEntities/_GetEntityByUserId/_InsertEntity/_UpdateEntity/_DeleteEntity.sql`, `usp_MAT_Cliente_GetEntityById/_InsertEntity/_UpdateEntity/_DeleteEntity.sql`, `usp_MAT_Vendedor_GetEntityById/_InsertEntity.sql`, `usp_MAT_Pasajero_GetEntityById/_InsertEntity/_UpdateEntity/_DeleteEntity.sql`, `usp_MAT_Proveedor_GetEntityById/_InsertEntity/_UpdateEntity/_DeleteEntity.sql`, `usp_MAT_Cuenta_GetByClienteId/_InsertEntity/_UpdateEntity.sql`, `usp_MAT_PersonaCliente_GetEntities/_PersonaPasajero_GetEntities/_PersonaProveedor_GetEntities/_PersonaVendedor_GetEntities/_VPersona_GetEntities.sql`
  - `MAT.MVC/Infrastructure/Data/`: `PersonaDataAccess.cs`, `ClienteDataAccess.cs`, `VendedorDataAccess.cs`, `PasajeroDataAccess.cs`, `ProveedorDataAccess.cs`, `CuentaDataAccess.cs`, `PersonaVistasDataAccess.cs` (5 vistas), `SqlReaderHelper.cs`
- Archivos modificados: controllers `PersonaController.cs`, `PersonaClienteController.cs`, `PersonaPasajeroController.cs`, `PersonaProveedorController.cs`, `ClienteController.cs`, `BusquedaController.cs`, `ReservaController.cs`; models `PerfilModel.cs`, `AccountModels.cs`, `PagosClientesModel.cs`, `PasajeModel.cs`, `PasajeroHistorialModel.cs`, `ReservaModel.cs`, `InfopathModel.cs`, `VoucherModel.cs`, `SearchModel.cs`, `HistorialModel.cs`, `PagosPorFechaModel.cs`, `ListaFacturasModel.cs`, `PlanillaHotelModel.cs`; `Common/MATContext.cs` (`CurrentVendedor`); `MAT.MVC.csproj`, `MAT.DB.sqlproj`, `SPEC.md`
- Qué se implementó:
  - Reemplazo completo de `PersonaService`, `ClienteService`, `VendedorService`, `ProveedorService`, `PasajeroService`, `CuentaService` y las vistas NetTiers (`PersonaClienteService`, `PersonaPasajeroService`, `PersonaProveedorService`, `PersonaVendedorService`, `VPersonaService`) por SPs + `*DataAccess` siguiendo el patrón F6.
  - `SqlReaderHelper`: extensiones tipadas/nullable sobre `SqlDataReader` (`GetString`, `GetGuid`, `GetNullableInt`, etc.) compartidas por los DataAccess nuevos.
  - Búsquedas de autocompletar (`BusquedaController.PersonaAutocomplete` → `VPersonaDataAccess.Search`, `ReservaController.QuickPasajeroSearch` → `PersonaPasajeroDataAccess.Search`) resueltas con SP `@Term` en vez de `GetAll().Where(...)` en memoria.
  - `MATContext.CurrentVendedor`: `PersonaDataAccess.GetByUserId` + `VendedorDataAccess.GetById` reemplazan el `GetAll().Where(UserId)` NetTiers.
  - `HistorialModel` conserva `HistorialService` (alcance F8) — solo se migraron sus lookups de `PersonaCliente`/`PersonaVendedor`.
- Problemas encontrados: ninguno de compilación.
- Pendiente de despliegue: publicar los 28 SPs de `MAT.DB` en el entorno + smoke de ABM cliente/pasajero/proveedor/vendedor, perfil, búsqueda rápida y cuenta corriente. Deploy BD + smoke + commit delegados a humano.
- Estado: ✅ MSBuild MAT.sln Debug OK

### [2026-07-03] — NetTiers F7.1: limpieza y optimización (conservador)

- Archivos nuevos:
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_Perfil_GetByPersonaId.sql` (perfil en 1 roundtrip: vPersona + flags EXISTS EsCliente/EsPasajero/EsVendedor/EsProveedor)
  - `MAT.MVC/Infrastructure/Data/PerfilDataAccess.cs` (con DTO `PerfilData`)
  - `database/2026-07-03_NetTiers_F7.1_Cleanup_SPs.sql` (SP nuevo + versiones modificadas de los SP de vistas)
- Archivos modificados:
  - `MAT.MVC/Models/PerfilModel.cs`: 5 roundtrips → 1 (usa `PerfilDataAccess`); props públicas sin cambios.
  - `MAT.MVC/Controllers/PersonaCliente/PersonaClienteController.cs`: `validarCTA` con guarda de null (crea Cuenta si no existe en Activar/Desactivar, helper `SetEstadoCuenta`) + `try/catch` con `ErrorUtil`.
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_PersonaCliente_GetEntities.sql`, `_PersonaProveedor_GetEntities.sql`, `_VPersona_GetEntities.sql`: `SELECT *` → columnas explícitas.
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_PersonaPasajero_GetEntities.sql`: columnas explícitas + parámetro `@Top` (NULL = todas) + `ORDER BY`.
  - `MAT.MVC/Infrastructure/Data/PersonaVistasDataAccess.cs`: nuevo `PersonaPasajeroDataAccess.GetTop(int)` (Query acepta `@Top`); `GetAll()` sin cambios de contrato.
  - `MAT.MVC/Models/SearchModel.cs`: la búsqueda rápida (Reserva/QuickSearch por GridView) usa `GetTop(200)` en vez de `GetAll()` (evita materializar toda la vista).
  - `MAT.MVC/MAT.MVC.csproj`, `MAT.DB/MAT.DB.sqlproj`: alta de los archivos nuevos.
- Alcance conservador (por decisión del usuario): NO se borró `PersonaController` ni se fusionaron las dos capas de PersonaCliente; la lógica de Cuenta en `PersonaPasajeroController.Create` no se tocó.
- Pendiente de verificación humana:
  - **FK Cuenta**: `PersonaPasajeroController.Create` inserta `Cuenta` fuera del `if (escliente)` (posible violación de `FK_Cuenta_Cliente` para pasajero no-cliente) — confirmar contra BD real antes de fix.
  - **Dead code**: `PersonaController` genérico (+ `usp_MAT_Persona_GetAllEntities`) no está enlazado en el menú — confirmar antes de borrar.
  - **PersonaAutocomplete/VPersona.Search**: `#txt-filter-persona` usa `/Factura/PersonaAutocomplete`; `/Busqueda/PersonaAutocomplete` quedó comentado — verificar si `BusquedaController.PersonaAutocomplete` y el path `@Term` de vPersona siguen vivos.
  - **Redundancia**: coexisten `PersonaClienteDataAccess` (F7) y `PersonaClienteMethod` (`PersonaClienteModel.cs`, SPs preexistentes) — deuda de consolidación pospuesta.
- Pendiente de despliegue: correr `database/2026-07-03_NetTiers_F7.1_Cleanup_SPs.sql` en el entorno + smoke (perfil, búsqueda rápida de pasajeros, activar/desactivar CC). Deploy BD + smoke + commit delegados a humano.
- Estado: ✅ MSBuild MAT.sln Debug OK

### [2026-07-05] — NetTiers F8: Planilla e Historial (migrado / retiro declarado)

- Archivos nuevos:
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_Historial_GetAll.sql`, `usp_MAT_Historial_GetByHistorialId.sql`
  - `MAT.MVC/Infrastructure/Data/HistorialDataAccess.cs`
  - `database/2026-07-05_NetTiers_F8_Historial_SPs.sql`
- Archivos modificados:
  - `MAT.MVC/Models/HistorialModel.cs`: `HistorialService.GetByHistorialId` → `HistorialDataAccess.GetByHistorialId`
  - `MAT.MVC/Controllers/Home/HomeController.cs` (`RenderGridHistorialPagos`): `new Services.HistorialService().GetAll()` → `HistorialDataAccess.GetAll()`
  - `MAT.MVC/Common/MATContext.cs`: eliminados 4 campos/propiedades estáticas huérfanas (`Planilla`, `ColeccionPlanillas`, `ServiciosSeleccionados`, `HabitacionesPlanilla`) — cero lectores/escritores en todo el repo
  - `MAT.MVC/MAT.MVC.csproj`, `MAT.DB/MAT.DB.sqlproj`: alta de los archivos nuevos
  - `SPEC.md` (F8 → `[x]`), `DOCUMENTACION/NETTIERS_MIGRACION_FASES.md`, `DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md`
- Qué se implementó:
  - **Historial migrado a DBHelper/SP** (único uso real de NetTiers en el dominio F8; solo lectura, sin cambio de comportamiento).
  - **Planilla: retiro total declarado, sin migración.** Investigación previa confirmó que el submódulo de impresión/edición (`EditarPlanilla`, `ImprimirPlanilla*`, `GridPlanillaServiciosItem*`, `GridPlanillaHotel*`) ya había sido eliminado por completo en un commit anterior (`f3c75f9`, 2026-06-17) — no solo el menú (retirado 2026-04-07). Los servicios NetTiers `PlanillaService`/`PlanillaServicioItemService`/`PlanillaHabitacionItemService`/`PlanillaServicioService` tenían cero callers en `MAT.MVC`; el único resto vivo eran los 4 campos de `MATContext` (estáticos de clase, **no** `HttpContext.Session` pese a como se los describía originalmente), eliminados en esta fase.
  - Tablas `Planilla`, `PlanillaServicioItem`, `PlanillaHabitacionItem` quedan intactas en BD, sin acceso desde la app — disponibles si negocio confirma la Opción C de Viaje-Rentabilidad más adelante.
  - Aclarado en docs: la entidad NetTiers `PlanillaServicio` es un artefacto huérfano que apunta a la misma tabla física `Planilla` (no hay tabla `PlanillaServicio` separada).
- Fuera de alcance (detectado, no tocado): `Models/PlanillaHotelModel.cs` y `Models/ResumenPlanillaModel.cs` quedaron sin callers activos pero no pertenecen al dominio NetTiers Planilla (usan DataAccess ya migrados) — candidatos a limpieza en tarea aparte. Código muerto comentado de `NotaService` en `CuentaModel.cs` tampoco se tocó (Nota ya migrada en fase previa; el comentario es cosmético y ese archivo tiene mucho más código comentado no relacionado a F8).
- Problemas encontrados: ninguno de compilación.
- Pendiente de despliegue: publicar los 2 SPs de `MAT.DB` en el entorno + smoke de `/Home/HistorialPagos`. Deploy BD + smoke + commit delegados a humano.
- Estado: ✅ MSBuild MAT.MVC Debug OK

### [2026-07-07] — DistribucionCoche Fases 4–6 (lista mobile, refactor, menores)

- Archivos nuevos:
  - `database/2026-07-07_DistribucionCoche_AllSeats.sql`
  - `MAT.MVC/Scripts/mat.distribucioncoche.js`
  - `MAT.MVC/Views/Reserva/_DistribucionCocheLista.cshtml`
  - `MAT.MVC/Views/Reserva/_DistribucionCocheLayout_Minibus.cshtml`
  - `MAT.MVC/Views/Reserva/_DistribucionCocheLayout_Camion4x4.cshtml`
  - `MAT.MVC/Views/Reserva/_DistribucionCocheLayout_PisoElevado.cshtml`
  - `MAT.MVC/Views/Reserva/_DistribucionCocheLayout_Semicama.cshtml`
- Archivos modificados:
  - `MAT.DB/dbo/Stored Procedures/usp_MAT_Reserva_DistribucionCoche_GetByViajeID.sql` — LEFT JOIN Butaca (todas las butacas); EsMenor/EsTutor/TutorNombre/TutorButacaNro; CapacidadTotal
  - `MAT.MVC/Models/ReservaModel.cs` — campos menores/tutores; `DistribucionCochePageViewModel`; `EstaOcupada`
  - `MAT.MVC/Controllers/Reserva/ReservaController.cs` — lectura SP v3; métricas corregidas
  - `MAT.MVC/Views/Reserva/DistribucionCoche.cshtml` — 219 líneas; toggle Mapa/Lista; partials layout
  - `MAT.MVC/Views/Reserva/_DistribucionCocheAsiento.cshtml` — `.Menor`/`.Tutor` server-side; data attrs vínculos
  - `MAT.MVC/Content/mat.distribucioncoche.css` — layout, lista, menores, print, SVG
  - `MAT.MVC/MAT.MVC.csproj`, `SPEC.md`
- Qué se implementó:
  - **Fase 5a:** SP devuelve todas las butacas del transporte; métricas Ocupadas/Disponibles/Total correctas.
  - **Fase 4:** Vista lista alternativa con toggle; búsqueda unificada; impresión solo mapa.
  - **Fase 5b:** Partials por tipo de transporte; CSS inline movido a archivo externo.
  - **Fase 6:** Menores con clase `.Menor`, tooltip tutor, leyenda; toggle SVG vínculos tutor↔menor.
- Pendiente de despliegue humano: `database/2026-07-07_DistribucionCoche_AllSeats.sql` (+ `FacturaID` si aún no aplicado) antes de smoke en BD real.
- Smoke manual pendiente: MINIBUS, CAMION 4X4, PISOELEVADO, semicama 81/101; toggle lista; vínculos menores.
- Estado: ✅ MSBuild MAT.MVC Debug OK

### [2026-07-07] — Fix circuito Login / LogOff (P0–P4)

- Archivos modificados:
  - `MAT.MVC/Views/Account/Login.cshtml` — eliminado `@if` inválido dentro de `@using` (HttpParseException)
  - `MAT.MVC/Infrastructure/AuthCookieHelper.cs` — limpieza acotada: cookies del request + legacy fijo + chunks auth 1–8 (sin bucle 1–64)
  - `MAT.MVC/Common/MATContext.cs` — `ResetCurrentUser()`
  - `MAT.MVC/Controllers/Account/AccountController.cs` — `ResetCurrentUser()` en LogOff
  - `MAT.MVC/Views/Shared/_LayoutLogin.cshtml` — hint DEV cookies
  - `MAT.MVC/Content/login-split.css` — `.login-split__dev-hint`
- Qué se implementó:
  - **P0:** Login compila y renderiza con `ReturnUrl`
  - **P1:** Menos `Set-Cookie` en respuesta (evita headers gigantes)
  - **P3:** Estado estático de usuario reseteado al cerrar sesión
  - **P4:** Aviso de recuperación en layout login (solo DEV)
- **P2 (humano, una vez):** F12 → Application → Cookies → borrar todas de `localhost:64315` → F5 → probar login + logout
- Smoke manual pendiente: login → navegar → cerrar sesión → volver a login
- Estado: ✅ MSBuild MAT.MVC Debug OK

### [2026-07-07] — Fix machineKey Web.config (ConfigurationErrorsException Login)

- Archivos modificados: `MAT.MVC/Web.config`
- Qué se implementó: `validationKey` tenía 129 caracteres hex (debe ser 128 para SHA1); `decryptionKey` tenía 48 chars (AES requiere 64). Reemplazadas por claves generadas con RNG válidas.
- Error resuelto: `ConfigurationErrorsException` al renderizar `@Html.AntiForgeryToken()` en Login.
- **P2 (humano, una vez):** borrar cookies de localhost (cambió machineKey → cookies auth previas inválidas) → F5 → smoke login/logoff.
- Estado: ✅ MSBuild MAT.MVC Debug OK
