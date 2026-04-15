# SPEC.md — Proyecto MAT

## Features completadas
(El agente completa esta sección a medida que avanza)

- [x] Setup inicial del proyecto — estructura de solución, CLAUDE.md, SPEC.md, PROGRESS.md
- [x] Admin — eliminar usuario (`UsuarioEliminar`, confirmación BS5, restricciones auto-eliminación y último administrador)
- [x] Admin — exportar resumen de pagos por viaje a Excel (EPPlus, `ResumenPagosExcel`)

---

## Features pendientes

### P1 — Crítico / Deuda técnica

- [x] **BUG: Factura de compra no editable tras carga**
  Una vez que el usuario carga una factura de compra, ya no puede editarla. El sistema debe permitir editar facturas de compra que fueron creadas con errores o que necesitan actualización de datos.
  Archivos probables: `Controllers/PersonaCliente/PersonaClienteController.cs`, `Views/PersonaCliente/RegistrarPago.cshtml`, `Views/PersonaCliente/RegistrarPagoTotal.cshtml`
  Criterio de éxito: Las facturas de compra pueden editarse después de ser creadas.
  **Implementación 2026-04-10:** Agregada funcionalidad de edición en módulo Facturación Fiscal (FacturaFiscalController, Index.cshtml con botón Editar, Create.cshtml reutilizado para edición, FacturaFiscalMethod.Update ya existía).

- [x] **Facturación Fiscal: Validación de duplicado en edición**
  El `CheckDuplicate` actual no excluye la factura en edición, puede dar falsos positivos al editar. Modificar la llamada para pasar el ID de la factura actual y actualizar el SP para excluirla de la búsqueda.
  Archivos: `Views/FacturaFiscal/Create.cshtml`, `Controllers/FacturaFiscal/FacturaFiscalController.cs`, `Models/FacturaFiscalModel.cs`, `MAT.DB/Stored Procedures/usp_MAT_FacturaFiscal_CheckDuplicate.sql`
  Criterio de éxito: Al editar una factura, la validación de duplicado no muestra alerta para esa misma factura.

- [x] **Facturación Fiscal: Resaltar filtros activos**
  Los filtros aplicados (tipo, estado, fechas) deben mostrarse visualmente como activos para que el usuario sepa cuáles están aplicados.
  Archivos: `Views/FacturaFiscal/Index.cshtml`
  Criterio de éxito: Filtros activos tienen estilo diferenciado, limpiar filtros reinicia el estado visual.

- [x] **Facturación Fiscal: Lazy loading de proveedores**
  El dropdown carga todos los proveedores al inicio; con muchos registros puede lentificar. Implementar búsqueda asíncrona con debounce.
  Archivos: `Views/FacturaFiscal/Create.cshtml`, `Controllers/FacturaFiscal/FacturaFiscalController.cs`
  Criterio de éxito: Dropdown de proveedores con búsqueda en lugar de lista estática.

- [x] **Facturación Fiscal: Guardar filtros en localStorage**
  Recordar los últimos filtros usados para no tener que reingresarlos cada vez que el usuario vuelve al listado.
  Archivos: `Views/FacturaFiscal/Index.cshtml`
  Criterio de éxito: Al volver al listado, los últimos filtros aplicados se restauran automáticamente.

- [x] **Facturación Fiscal: Mejora de tooltips en acciones**
  Agregar descripción más clara en los botones de acción de la tabla (Ver detalle, Editar, Anular, Adjunto).
  Archivos: `Views/FacturaFiscal/Index.cshtml`
  Criterio de éxito: Todos los botones tienen tooltips descriptivos.

- [x] **Actualizar vistas pendientes de modernización**
  Revisar `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md` y migrar las vistas listadas a Bootstrap 5 / jQuery 3, eliminando dependencias obsoletas identificadas en `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md`.
  Criterio de éxito: Las vistas actualizadas renderizan correctamente en IIS Express sin errores de consola JS; MSBuild pasa sin errores.
  **Iteración 2026-04-08 (lote Fase 6 — Reserva):** `SearchModel` / `QuickSearch` con `GridView` estilizado BS5 y contenedor `table-responsive`; `PartialVinculacionMenor` usa modal Bootstrap 5 para confirmar desvinculación (sin `$.dialog` en ese flujo); demás ítems Reserva del inventario marcados ✅ en el doc (auditoría + criterio actual). **Iteración 2026-04-10:** Sincronización documento - todas las vistas del repositorio verificadas y modernas (sin BS2/glyphicons).

- [x] **Iteración CSS: normalizar estilos de `Shared/Error.cshtml`**
  La vista `MAT.MVC/Views/Shared/Error.cshtml` fue modernizada, pero sus estilos aún no están completamente normalizados con las convenciones de estilos compartidos del proyecto (nombres, tokens/variables y consistencia visual). Realizar una iteración de hardening CSS para alinear la vista con el estándar de UI actual sin cambiar la lógica de manejo de errores.
  Criterio de éxito: estilos de `Shared/Error.cshtml` normalizados y consistentes con el sistema visual del proyecto, sin regresiones de render y con compilación `MAT.MVC` en Debug sin errores.

- [x] **Iteración CSS: extraer estilos inline de `Shared/Error.cshtml` a archivo dedicado**
  La vista `MAT.MVC/Views/Shared/Error.cshtml` mantiene un bloque `<style>` inline. Crear un CSS dedicado (por ejemplo en `MAT.MVC/Content/`) y mover allí todos los estilos `modern-error-*`, dejando la vista sin estilos embebidos. Mantener la capacidad de render standalone de la pantalla de error sin depender del layout principal.
  Criterio de éxito: `Shared/Error.cshtml` sin bloque `<style>`; estilos cargados desde archivo CSS dedicado; apariencia y comportamiento sin regresiones; compilación `MAT.MVC` en Debug sin errores.

### P2 — Mejoras de producto

- [x] **Admin: Migrar ErrorLog.cshtml y Logs.cshtml a Bootstrap 5**
  Ambas vistas usan Bootstrap 2 (`glyphicon`, `btn-default`, `btn-xs`, `form-inline`, `table-condensed`). Son las únicas vistas del panel Admin que quedaron sin migrar. Reemplazar con Bootstrap Icons y clases BS5. No cambiar la lógica JS de carga/filtrado.
  Archivos: `Views/Admin/ErrorLog.cshtml`, `Views/Admin/Logs.cshtml`
  Criterio de éxito: Las vistas renderizan correctamente en IIS Express sin glyphicons ni clases BS2. MSBuild pasa sin errores.

- [x] **Admin: Gestión completa de roles en UsuarioEditar**
  El formulario `UsuarioEditar` solo permite asignar/quitar el rol "Administrador" mediante un checkbox. Otros roles existentes en el sistema (ej. "Vendedor") no pueden gestionarse desde la UI. Extender la vista y el controller para listar todos los roles del sistema como checkboxes y guardar los cambios. El controller debe leer los roles desde `Roles.GetAllRoles()` y aplicar add/remove por diferencia.
  Archivos: `Views/Admin/UsuarioEditar.cshtml`, `Controllers/Admin/AdminController.cs`, `Models/AdminUsuarioEditModel`
  Criterio de éxito: El formulario muestra todos los roles del sistema. Se pueden asignar y quitar roles arbitrarios. Las restricciones existentes (no quitarse admin a uno mismo, no eliminar último admin) se mantienen.

- [x] **Admin: Exponer ErrorLog y Logs en el panel Index**
  Las vistas `/Admin/ErrorLog` y `/Admin/Logs` existen y funcionan pero no están enlazadas desde `Admin/Index.cshtml`. Solo quien conoce las URLs puede acceder. Agregar una sección "Diagnóstico" en el Index (similar a la sección "Herramientas de Desarrollo" de ADMINDEV) que las exponga. Evaluar si debe estar restringida a ADMINDEV o visible para todos los admins.
  Archivos: `Views/Admin/Index.cshtml`
  Criterio de éxito: El panel Admin muestra las tarjetas de ErrorLog y Logs. El acceso respeta la restricción de rol acordada.

- [x] **Admin: Reemplazar confirm() de jQuery UI por modal Bootstrap 5 en Usuarios**
  El handler `js-admin-confirm-submit` en `Usuarios.cshtml` llama a una función `confirm()` que usa jQuery UI dialog. Esto crea dependencia mezclada (BS5 + jQuery UI) e inconsistencia visual. Reemplazar por un modal Bootstrap 5 reutilizable (puede ser el mismo patrón que ya usan otras vistas con `data-bs-toggle`).
  Archivos: `Views/Admin/Usuarios.cshtml`
  Criterio de éxito: La confirmación de deshabilitar usuario usa modal Bootstrap 5. No queda dependencia de jQuery UI dialog en esta vista.

- [x] **Admin: Agregar DataTables y filtro de búsqueda en Usuarios**
  La tabla de usuarios (`Usuarios.cshtml`) es un `foreach` estático sin paginación ni búsqueda. Agregar DataTables con búsqueda por nombre de usuario y filtro por estado (Activo / Deshabilitado / Bloqueado). Seguir el patrón de otras vistas del proyecto.
  Archivos: `Views/Admin/Usuarios.cshtml`
  Criterio de éxito: La tabla permite buscar por usuario y filtrar por estado. MSBuild pasa sin errores.

- [x] **Admin: Eliminar módulo de menú Planillas (PlanillaServicios / PlanillasGeneradas)**
  Las rutas **`/Admin/PlanillaServicios`** y **`/Admin/PlanillasGeneradas`** no se utilizan en operación; debe retirarse **todo** lo que las exponga o que dependa **exclusivamente** de ese flujo de menú, sin dejar enlaces rotos ni redirecciones inconsistentes.
  **Alcance orientativo (completar al implementar):** enlaces en `Views/Shared/_LayoutAdmin.cshtml` (nav desktop y offcanvas/móvil); acciones y vistas en `Controllers/Admin/AdminController.cs` ligadas a ese flujo (p. ej. `PlanillaServicios`, `PlanillasGeneradas`, `GridPlanillasGeneradas`); archivos bajo `Views/Admin/` correspondientes (`PlanillaServicios.cshtml`, `GridPlanillasGeneradas.cshtml`, vista `PlanillasGeneradas.cshtml` si aplica); ítems en `MAT.MVC.csproj`; handlers en `Scripts/mat.jquery.binding.js` (u otros scripts) **solo** usados por ese flujo; referencias en `Views/Admin/Index.cshtml`, `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md`, `HANDOFF.md` u otra documentación interna.
  **Qué no confundir con este task:** Partials y acciones que sigan siendo necesarias para **otros** flujos (p. ej. `ImprimirPlanilla`, `ImprimirPlanillaDetalle`, `EditarPlanilla`, `GridPlanillaServiciosItemEdit`, `GridPlanillaServiciosItemPrint`, grids de hotel en impresión/edición, persistencia en BD) no se eliminan salvo que queden **huérfanas** tras el retiro. Tras `DeletePlanilla`, redirección acordada: `Admin/Index`.
  Criterio de éxito: Ningún enlace en la app apunta a `/Admin/PlanillaServicios` ni `/Admin/PlanillasGeneradas`; MSBuild sin errores; smoke test del panel Admin sin excepciones; `PROGRESS.md` con lista de archivos/rutas eliminados o ajustados.

- [x] **Admin: Panel Index completo — sección Sistema (sin Planillas en menú)**
  El panel `Index.cshtml` debe acercarse al menú admin útil: al menos **Sistema / Diagnóstico** (ErrorLog, Logs) y lo que el equipo defina como cards de acceso rápido. **No** incluir enlaces a PlanillaServicios / PlanillasGeneradas (quedan fuera por el task anterior).
  Archivos: `Views/Admin/Index.cshtml` (y, si aplica, enlaces ya cubiertos por el task "Exponer ErrorLog y Logs en el panel Index").
  Criterio de éxito: El Index refleja las secciones acordadas sin las planillas retiradas; sin duplicar lógica contradictoria con el sidebar tras el cleanup.

#### Reportes administrativos (misma BD que MAT Web; patrones MAT.MVC)

**Principios:** Implementar en el sistema actual usando `DBHelper`/SqlClient, `ErrorUtil`, `RequireAdministrador()`, vistas Admin Bootstrap 5 y **EPPlus** para Excel. Reutilizar **sin modificar** los SP existentes `usp_MAT_Reportes_Ventas`, `usp_MAT_Reportes_Pagos`, `usp_MAT_Reportes_RankingCompras`. Si el contrato columnas/parámetros no alcanza, crear **nuevos** SP en `MAT.DB` (nombre distinto, p. ej. sufijo `_Admin` o `_V2`) y dejar los originales intactos. Referencia funcional: `DOCUMENTACION\REPORTES_MAT_WEB.md` (paridad filtros/DTO/Excel con MAT Web; rutas pueden diferir).

- [x] **Reportes [P2 — Alta]: Inventario read-only de SP y definición de estrategia**
  Revisar en `MAT.DB` los tres SP anteriores: parámetros, columnas devueltas y tipos. Contrastar con el contrato deseado (filtros `from`/`to`/`viajeId`/opcionales, mapeo DTO). Decisión documentada: ¿llamar SP actuales desde C# solo con mapeo? ¿o alta de **nuevo** SP sin `ALTER` sobre los existentes?
  Criterio de éxito: Nota corta en `DOCUMENTACION` o comentario en PROGRESS; camino elegido acordado y sin cambios a los `.sql` de los SP actuales.

- [x] **Reportes [P2 — Alta]: Helper de fechas y parámetros (reutilizable)**
  Centralizar validación: aceptar `from`/`to` en `DD-MM-YYYY` y `YYYY-MM-DD`; normalizar a `DD-MM-YYYY` para pasar al SP (mismo criterio que usa `HomeController` con los SP); vacíos → `NULL` en SQL; rango máximo **365 días** cuando hay rango; condición de ejecución: solo si (`from` **y** `to`) **o** `viajeId`; filtros rango vs viaje **mutuamente excluyentes** (comportamiento explícito: rechazar o ignorar uno — documentar en código). GUIDs opcionales parseados o `NULL`.
  Criterio de éxito: Una clase/helper testeable; sin duplicar lógica en tres acciones.

- [x] **Reportes [P2 — Alta]: Mapeo de fila → DTO JSON (columnas heterogéneas)**
  Lectura **case-insensitive** de columnas (`FacturaId` vs `FacturaID`, etc.); renombres (`Descripcion` → `pagoDescripcion`, `FullName` → `clienteFullName`); `monedaTipo` siempre **string** (`"1"`, `"3"`); montos `decimal`; fechas desde string SP (`dd/MM/yyyy`, `dd-MM-yyyy`) o `DateTime` nativo; salida lista para serializar en **camelCase**.
  Criterio de éxito: Cubre las tres consultas sin ramas copiadas por reporte salvo composición mínima.

- [x] **Reportes [P2 — Alta]: Nuevo SP solo si hace falta (sin tocar los actuales)**
  Si el inventario detecta gap irreparable sin `ALTER`: agregar en `MAT.DB` nuevo procedimiento (nombre nuevo), migración en `database\` con paridad SSDT, **sin modificar** los tres SP originales. Las acciones del controller deben apuntar al SP acordado (existente o nuevo).
  Criterio de éxito: Los `.sql` originales de reportes no cambian; si hay SP nuevo, build de `MAT.DB` y documentación del nombre usado.

- [x] **Reportes [P2 — Alta]: `ReportesController` (o equivalente bajo `Controllers/Admin`) + seguridad**
  Todas las acciones con `[Authorize]` y `RequireAdministrador()` (mismo patrón que `AdminController`). Rutas GET claras para JSON y Excel. Errores con `ErrorUtil.LogAndGetPublicMessage`; respuestas JSON seguras (sin `e.Message`).
  Criterio de éxito: Usuario no administrador no puede ejecutar ni adivinar URL con éxito; MSBuild limpio.

- [x] **Reportes [P2 — Alta]: Endpoint datos JSON — Ventas**
  Llamada a `usp_MAT_Reportes_Ventas` vía `DBHelper`/patrón existente; contrato de respuesta alineado al proyecto (`ok`/`success` + `data` según convención que defina el task al implementar, pero **consistente y documentado**); lista vacía si no hay criterio válido.
  Criterio de éxito: Mismo comportamiento de filtros que el spec MAT Web; compilación y prueba manual con admin.

- [x] **Reportes [P2 — Alta]: Endpoint datos JSON — Pagos**
  Igual patrón con `usp_MAT_Reportes_Pagos`; parámetro opcional `tipoVentaId`.
  Criterio de éxito: Columnas y rename `pagoDescripcion`; `viaje` como texto.

- [x] **Reportes [P2 — Alta]: Endpoint datos JSON — Ranking de compras**
  Igual patrón con `usp_MAT_Reportes_RankingCompras`.
  Criterio de éxito: Rankings y conteos expuestos en JSON correcto.

- [x] **Reportes [P2 — Media]: Exportación Excel — Ventas (EPPlus)**
  Columnas en orden funcional acordado con el spec; cabecera con estilo (fondo azul, texto blanco, negrita); fechas formato es-AR; nombre archivo `ventas-YYYY-MM-DD.xlsx`; `Content-Type` correcto; mismos filtros que JSON.
  Criterio de éxito: Archivo válido; datos alineados al JSON del mismo filtro.

- [x] **Reportes [P2 — Media]: Exportación Excel — Pagos (EPPlus)**
  Mismos estándares que ventas.
  Criterio de éxito: Columnas en orden esperado; MSBuild limpio.

- [x] **Reportes [P2 — Media]: Exportación Excel — Ranking (EPPlus)**
  Mismos estándares que ventas.
  Criterio de éxito: Columnas en orden esperado.

- [x] **Reportes [P2 — Media]: Vistas Admin — índice y navegación**
  Vista listado de los tres reportes (cards o lista) con enlaces; entrada desde `Views/Admin/Index.cshtml` y/o menú lateral si aplica, sin romper diseño existente.
  Criterio de éxito: Solo administrador ve y accede a la sección.

- [x] **Reportes [P2 — Media]: Vista Admin — Reporte Ventas**
  Formulario filtros (rango fechas **o** viaje, excluyentes); opcionales vendedor/cliente reutilizando selects/autocomplete existentes en el proyecto si ya hay; tabla de resultados (patrón DataTables u otro ya usado en Admin); botón exportar Excel; consumo de endpoint JSON interno (jQuery o fetch según patrón del módulo).
  Criterio de éxito: Flujo completo sin errores de consola; BS5.

- [x] **Reportes [P2 — Media]: Vista Admin — Reporte Pagos**
  Igual enfoque que ventas; filtros y export.
  Criterio de éxito: Incluye tipo de venta si se expone en UI.

- [x] **Reportes [P2 — Media]: Vista Admin — Reporte Ranking compras**
  Igual enfoque; filtros y export.
  Criterio de éxito: Datos y export coherentes con JSON.

- [x] **Reportes [P2 — Media]: Regresión — usos existentes de reportes**
  Verificar `HomeController` y cualquier otro consumo de `usp_MAT_Reportes_Ventas` u otros SP de reportes; tras extraer helpers/servicios, no romper totales ni popups existentes.
  Criterio de éxito: Estadísticas/home intactos en prueba manual.

- [x] **Reportes [P3 — Baja]: UI comparación de períodos (tendencias)**
  Opcional: dos consultas seguidas al endpoint de ventas (periodo actual vs anterior) con fechas predefinidas (mes/mes, año/año, etc.) como en MAT Web, **sin** nuevo endpoint.
  Criterio de éxito: Documentado en pantalla o leyenda; sin lógica duplicada en backend.

- [x] **Reportes [P3 — Baja]: Alias de rutas REST (opcional)**
  Si hace falta compatibilidad con clientes externos: registrar rutas tipo `reportes/ventas` en `RouteConfig` apuntando a las mismas acciones (GET).
  Criterio de éxito: Documentar URLs finales en `DOCUMENTACION`.

- [x] **Reportes [P3 — Baja]: Documentación de operación**
  Archivo breve en `DOCUMENTACION`: URLs, parámetros, ejemplo JSON, nota sobre SP usados (existentes vs nuevos) y política de no modificar SP legacy.
  Criterio de éxito: Un desarrollador puede reproducir pruebas sin el archivo original de Downloads.

#### Reportes — hardening / calidad (post-auditoría)

- [x] **Reportes: Alineación HTTP JSON con Excel (401 / 403)**
  Endpoints JSON (`Ventas`, `Pagos`, `Ranking`) devuelven **401** si no hay sesión y **403** si el usuario no es Administrador, con cuerpo JSON `{ "ok", "message", "data" }`; las vistas interpretan el mensaje en `.fail()` de `getJSON`.
  Criterio de éxito: Paridad con códigos de las acciones Excel; mensaje visible en la UI.

- [x] **Reportes: Export Excel desde el navegador con manejo de error**
  Sustituir `window.location` por descarga vía `fetch` + blob; ante 400/401/403/500 mostrar el texto de respuesta en el alerta. Lógica en `Scripts/mat.reportes-excel-export.js`.
  Criterio de éxito: Mismo `.xlsx` en caso OK; error legible sin pantalla en blanco.

- [x] **Reportes: Excel EPPlus — cabeceras y GUIDs**
  Cabeceras centradas; columnas de identificadores GUID como texto (`@`) para evitar notación científica en Excel.
  Criterio de éxito: Alineado a `REPORTES_MAT_WEB.md`; revisión manual de una exportación.

- [x] **Reportes: Validación estricta de `tipoVentaId`**
  Si se envía `tipoVentaId`, solo **1** u **2**; otro valor → validación sin llamar al SP.
  Criterio de éxito: MSBuild limpio.

- [x] **Reportes: Corrección bug vista Pagos (`$tbl` en `renderTable`)**
  `ReportePagos.cshtml` define `$("#tblReporte")` antes de `destroy`/`empty` para no romper búsquedas repetidas.
  Criterio de éxito: Sin error JS en múltiples consultas.

- [x] **Reportes (mejora futura): Filtros vendedor/cliente sin GUID manual**
  Complementar inputs GUID con búsqueda/select reutilizando patrones del Admin donde existan. *(Complemento opcional al task **Reportes UX: filtrar vendedor y cliente solo en el front**: ese task prioriza refinado post-consulta sin BD; este sigue siendo útil si se mantiene envío opcional de GUID al SP en la consulta inicial.)*
  Criterio de éxito: Menos errores de pegado y mejor paridad con el SPEC de “reutilizar selects”.

- [x] **Reportes (mejora futura): Tests unitarios `ReportesQueryHelper`**
  Casos: feliz, rango mayor a 365 días, conflicto fechas+viaje, `from` sin `to`, `tipoVentaId` inválido.
  Criterio de éxito: Suite verde en el pipeline acordado.
  **Implementación (2026-04-09):** proyecto `MAT.MVC.Tests` (NUnit 3.14 + referencia a `MAT.MVC`); paquetes en `packages\` (`NUnit`, `NUnit3TestAdapter`). Ejecución local: `vstest.console.exe` con `/TestAdapterPath` apuntando a `packages\NUnit3TestAdapter.4.6.0\build\net462`.

- [x] **Reportes (mejora futura): DataTables i18n sin CDN**
  Evitar `cdn.datatables.net` para `Spanish.json` (archivo local o bundle).
  Criterio de éxito: Idioma cargado sin Internet.

#### Reportes — UX/UI / diseño datepicker

- [x] **Reportes UX: Corregir diseño de datepickers en vistas de reportes**
  Los botones de navegación `‹` / `›` del calendario jQuery UI se renderizaban incorrectamente en `ReporteVentas.cshtml`: las entidades HTML `&#x3C;` y `&#x3E;` dentro del bloque `<script>` no son decodificadas por JavaScript y aparecían como texto literal en el header del popup. Adicionalmente, el CSS en `admin.modern.css` exponía el texto interno del span (al resetear `text-indent` a 0 y usar `overflow: visible`), solapándolo con el `::after` que renderiza la flecha real. Se unificó también la configuración del datepicker de Ventas con los de Pagos y Ranking (`yearRange`, `maxDate`, `showButtonPanel`, `showOtherMonths`).
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, `Content/admin.modern.css`
  Criterio de éxito: Los tres datepickers muestran `‹` y `›` correctamente; configuración de rango de años y opciones de panel coherente entre los tres reportes.

- [x] **Admin / Reportes: Revisar bug del desplegable de mes en jQuery UI Datepicker (`changeMonth`)**
  Con `changeMonth: true` y `changeYear: true`, al abrir el combo del **mes** el listado se renderiza mal: contenedor alargado en vertical, casi vacío, solo se ve el ítem seleccionado arriba y el resto del calendario queda tapado (regresión visual; posible conflicto entre estilos de `admin.modern.css` sobre `select.ui-datepicker-month` / `.ui-datepicker-title` y el tema `jquery-ui-1.13.2.css`, o `overflow`/`height`/`line-height` en el popup).
  **Alcance:** reproducir en pantallas Admin que usen datepicker con esas opciones (p. ej. reportes Ventas/Pagos/Ranking, `AuditoriaFacturas`, otras con `mat-datepicker`). Documentar navegador si aplica.
  **Enfoque sugerido:** inspeccionar en DevTools el `<select class="ui-datepicker-month">`; ajustar CSS scoped a `body.admin-modern .ui-datepicker` sin romper el header ni el año; validar también el desplegable de año.
  Archivos probables: `Content/admin.modern.css`, eventualmente `Content/themes/base/jquery-ui-1.13.2.css` solo si hace falta override mínimo documentado.
  Criterio de éxito: Los dos combos muestran todas las opciones legibles, altura acotada al contenido o scroll coherente, sin cubrir el grilla de días ni salirse del popup; MSBuild limpio.

- [x] **Reportes UX: Cards de indicadores resumen en Reporte de Ventas**
  Tras ejecutar una consulta exitosa en `ReporteVentas`, mostrar entre el panel de filtros y la tabla de resultados un bloque de **cards con indicadores agregados** calculados a partir de los datos ya cargados en el cliente (sin nueva petición al servidor). Inspirado en el panel de Estadísticas de Ventas del Home.
  **Indicadores a mostrar (mínimo):**
  - **Total Facturado** — suma de `totalFactura`, desglosado por moneda (ARS / USD).
  - **Total Cobrado** — suma de `montoPagado`, desglosado por moneda.
  - **Saldo Pendiente** — suma de `saldo`, desglosado por moneda.
  - **Ventas con pago parcial/total** — cantidad de filas donde `montoPagado > 0`.
  - **Total de reservas** — cantidad total de filas del resultado.
  **Detalles de implementación:**
  - Los cálculos se realizan en JavaScript sobre el array `rows` recibido del endpoint, en la misma función `renderTable` o en una función auxiliar `renderIndicadores(rows)`.
  - Las cards se ocultan antes de la primera consulta y cuando el resultado está vacío.
  - El diseño sigue el sistema de cards moderno ya presente en el proyecto (Bootstrap 5, `card shadow-sm border-0`, iconos Bootstrap Icons); no usar el tema oscuro del Home — mantener el estilo claro del panel Admin.
  - Los montos se formatean con separador de miles en `es-AR`.
  Archivos: `Views/Reportes/ReporteVentas.cshtml` (HTML de las cards + JS de cálculo); opcionalmente extraer a `Scripts/mat.reportes-ventas-indicadores.js` si el bloque crece.
  Criterio de éxito: Al recibir resultados, las cards aparecen con los totales correctos; al cambiar el filtro y re-consultar, los valores se actualizan; la tabla y el Excel no se ven afectados.

- [x] **Reportes UX: Buscador de viajes en filtro "Por viaje" (autocompletar, sin GUID manual)**
  En los tres reportes (`ReporteVentas`, `ReportePagos`, `ReporteRanking`) el modo **Por viaje** exige que el usuario ingrese manualmente el GUID del viaje. Reemplazar ese campo de texto libre por un **autocompletar inteligente**: el usuario escribe parte del nombre del viaje, el campo sugiere resultados desde el servidor (endpoint JSON existente o nuevo, según conveniencia) y al seleccionar uno se almacena el GUID internamente sin exponerlo en pantalla. El campo debe mostrar solo el nombre descriptivo del viaje.
  **Detalles de implementación:**
  - Crear (o reutilizar) un endpoint ligero — por ejemplo `GET /Admin/Reportes/BuscarViajes?q=texto` — que devuelva `[{ id, descripcion }]` consultando la tabla/vista de viajes (sin tocar SP de reportes existentes).
  - Usar jQuery UI Autocomplete (ya disponible) o el plugin `magicsearch` que ya usa el proyecto si resulta más natural; respetar patrón existente.
  - El GUID se envía al SP solo cuando el usuario seleccionó un ítem de la lista (no texto libre sin match).
  - Si el usuario borra el texto, limpiar también el GUID guardado.
  - Aplicar el mismo cambio en los tres reportes para mantener paridad.
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, `Views/Reportes/ReportePagos.cshtml`, `Views/Reportes/ReporteRanking.cshtml`, `Controllers/Admin/ReportesController.cs` (nuevo endpoint `BuscarViajes`).
  Criterio de éxito: El usuario puede buscar un viaje por nombre sin conocer ni pegar GUIDs; la consulta y exportación Excel funcionan igual que antes; MSBuild limpio.

- [x] **Reportes UX: Buscador de viajes — botón “flecha abajo” y lista con nombre + fecha de salida**
  Complementar el autocompletar actual del filtro **Por viaje** con un control tipo **combo**: además de escribir para filtrar, el usuario debe poder hacer clic en un **botón** (icono flecha hacia abajo, accesible con `aria-label`) que **abra la lista de viajes** sin tener que tipear (equivalente a desplegar opciones).
  **Contenido de cada ítem:** siempre **nombre del viaje** (descripción) **+ fecha de salida** visible en la misma fila (formato coherente con el resto del Admin, p. ej. `dd/mm/yyyy`). El valor enviado al backend sigue siendo el **GUID** en campo oculto tras seleccionar.
  **Backend:** extender `BuscarViajes` / `usp_MAT_Reportes_BuscarViajes` (o endpoint adicional si se prefiere) para devolver `fechaSalida` (o string formateado) junto a `id` y `descripcion`; definir comportamiento cuando `q` está vacío o es comodín (p. ej. últimos N viajes por `FechaSalida` desc) para alimentar la lista al pulsar la flecha.
  **Frontend:** mismo patrón en `ReporteVentas`, `ReportePagos`, `ReporteRanking`; reutilizar o extender `mat.reportes-viaje-autocomplete.js` (menú custom, Autocomplete con `minLength: 0` + trigger en botón, u otro patrón acordado con BS5).
  Criterio de éxito: Clic en la flecha muestra lista usable con nombre y fecha; selección setea GUID oculto; teclado y lectores de pantalla razonables; MSBuild y SP/SSDT alineados.

#### Reportes — UX/UI (Ranking de compras, Admin)

- [x] **Reportes UX: Ranking — tarjetas de estadísticas (KPI)**
  Mejorar **Ranking de compras** (`ReporteRanking.cshtml`) con un bloque de **cards resumen** entre filtros y tabla, en la línea de **Reporte de ventas** (Bootstrap 5, tema claro Admin). Los indicadores se calculan en **cliente** sobre el JSON de la última consulta (sin nueva petición); no deben cambiar al usar filtros front de cliente/viaje salvo que se documente lo contrario.
  **Indicadores sugeridos (definir en implementación):** total de filas, clientes distintos, viajes distintos, suma o máximos relevantes de columnas ya expuestas (`cantClientesEligieronViaje`, rankings, etc.).
  Archivos: `Views/Reportes/ReporteRanking.cshtml`; opcional CSS compartido o reutilizar patrón de cards de ventas.
  Criterio de éxito: Tras consultar con datos, las cards muestran valores coherentes; consulta vacía u error no rompe la UI; MSBuild limpio.

- [x] **Reportes UX: Ranking — columna “F. salida” en formato `dd/mm/aaaa`**
  En la grilla, **`viajeFechaSalida`** hoy se muestra como ISO (`YYYY-MM-DDTHH:MM:SS`). Formatear solo **fecha** en locale **es-AR** (`dd/mm/aaaa`) vía `render` de DataTables (o helper JS). Revisar si la columna **“Fecha”** (`fecha` de factura) debe usar el mismo criterio para consistencia.
  Archivos: `Views/Reportes/ReporteRanking.cshtml`; export Excel (`ReportesExcelExport` ranking) si debe reflejar el mismo formato de fecha en celdas visibles.
  Criterio de éxito: Usuario ve fechas legibles sin hora innecesaria en pantalla; Excel acordado con negocio.

- [x] **Reportes UX: Ranking — mejoras UX/UI en tabla tblReporte**
  Aplicar mejoras visuales y de usabilidad en la tabla de resultados de `ReporteRanking.cshtml`:
  - **Badges de ranking:** columnas "Rank. clientes" y "Rank. viajes" con badges de color (#1 oro, #2 plata, #3 bronce).
  - **Columnas numéricas centradas** y en `fw-semibold`.
  - **Íconos en encabezados:** Fecha con `bi-calendar3`, Cliente con `bi-person`, Viaje con `bi-geo-alt`.
  - **Tooltips en encabezados:** columnas numéricas y de ranking con tooltip explicativo (Bootstrap 5 Tooltip).
  - **Constantes de columna:** `COL_CLIENTE` y `COL_VIAJE` para filtros front.
  - **Leyenda colapsable:** "Cómo leer el ranking" como botón `collapse` BS5.
  - **Renombrado:** "Clientes viaje" a "Fact. viaje" (más preciso: cuenta filas de factura).
  Archivos: `Views/Reportes/ReporteRanking.cshtml`
  Criterio de éxito: Mejoras aplicadas; MSBuild limpio; leyenda colapsable; badges visibles.

- [x] **Reportes: Ranking — documentar métrica “Rank. viajes” y claridad en UI**
  **Definición en BD (verificado en `usp_MAT_Reportes_RankingCompras`):**
  - **`RankingViajes`** = `DENSE_RANK() OVER (ORDER BY CantClientesEligieronViaje DESC)`: posición del **viaje** según un valor numérico asociado (empates = mismo rank, sin huecos por `DENSE_RANK`).
  - **`CantClientesEligieronViaje`** = `COUNT(tCompras.ViajeID) OVER (PARTITION BY tCompras.ViajeID)` en el CTE: por cada viaje, cuenta **filas** del conjunto filtrado (facturas con pasajes a ese viaje que cumplen `@From`/`@To`/`@ViajeId`/etc.). **No** equivale a `COUNT(DISTINCT ClienteID)`; el nombre de columna en SQL puede ser **engañoso** si se interpreta como “clientes únicos”.
  **Acciones:** actualizar `DOCUMENTACION/REPORTES_MAT_MVC_OPERACION.md` (y si aplica `REPORTES_MAT_WEB.md`) con esta definición; en pantalla, **tooltip** y/o **texto de ayuda** bajo el título o junto a la tabla explicando qué mide “Rank. viajes” y la columna “Clientes viaje”. **Opcional (decisión de negocio):** encabezados más fieles a la métrica o evolución futura del SP para medir clientes distintos.
  Criterio de éxito: Documentación alineada al SP; usuario sin perfil técnico tiene pista de lectura del rank; sin cambiar contrato del SP salvo task aparte explícito.

- [x] **Reportes UX: Ranking — dashboard interactivo (reescritura completa)**
  Rediseño completo del módulo Ranking de compras como dashboard de tarjetas interactivas:
  - **Nuevo SP** `usp_MAT_Reportes_RankingCompras_V2`: granularidad factura x viaje, sin window functions (agregación en JS), con `CantPasajerosDistintos`.
  - **DTO simplificado**: `ReporteRankingRowDto` con 9 campos (sin rankings ni conteos por window function).
  - **Controller**: endpoint `Ranking` apunta al nuevo SP; `RankingExcel` eliminado.
  - **5 tarjetas KPI clicables**: Registros, Clientes, Pasajeros, Facturas, Top viajes. Cada una abre un modal con detalle.
  - **5 modales Bootstrap 5**: tablas con búsqueda debounce, ordenación por columna, contador.
  - **Carga automática**: al iniciar la vista se consulta el último mes; datepickers pre-cargados.
  - **Descarga PDF**: integra `html2pdf.js` (local) para captura del dashboard a PDF A4 horizontal.
  - **Sin DataTable visible**: eliminados tabla, filtros front, KPI cards anteriores, leyenda.
  Archivos nuevos: `MAT.DB/dbo/Stored Procedures/usp_MAT_Reportes_RankingCompras_V2.sql`, `database/2026-04-15_usp_MAT_Reportes_RankingCompras_V2.sql`, `Scripts/mat.reportes-ranking-dashboard.js`, `Scripts/mat.reportes-ranking-modals.js`, `Scripts/lib/html2pdf.bundle.min.js`
  Archivos modificados: `Views/Reportes/ReporteRanking.cshtml`, `Models/Reportes/ReporteRankingRowDto.cs`, `Controllers/Admin/ReportesController.cs`, `Infrastructure/ReportesDataReaderMapper.cs`, `Infrastructure/ReportesExcelExport.cs`, `MAT.MVC.csproj`, `MAT.DB.sqlproj`
  Criterio de éxito: Dashboard funcional con 5 tarjetas y modales; carga automática último mes; PDF descargable; MSBuild MAT.MVC + MAT.DB sin errores.

#### Reportes — UX/UI (Reporte de ventas, Admin)

**Contexto:** La pantalla **Reporte de ventas** (`/Admin/Reportes/...`) debe comunicar en lenguaje de negocio que el módulo sirve para ver **estadísticas de venta** eligiendo **un rango de fechas** o **un viaje** (criterios mutuamente excluyentes, como ya valida el backend). Las mejoras siguientes refieren principalmente a `Views/Reportes/ReporteVentas.cshtml` y scripts asociados; si el mismo patrón aplica a Pagos/Ranking, documentar paridad o tareas derivadas en `PROGRESS.md`.

- [x] **Reportes UX: Reporte Ventas — cartel informativo en lenguaje de usuario**
  Sustituir el aviso celeste actual (texto técnico: comparación de períodos vía GET, formatos `YYYY-MM-DD`, rutas, referencias a `DOCUMENTACION/REPORTES_MAT_WEB.md`, etc.) por una descripción **comprensible para el usuario final**: qué muestra el reporte, que puede acotar por fechas **o** por viaje (no ambos a la vez), y en una frase opcional qué hacen "Consultar" y "Exportar Excel". El detalle técnico para desarrolladores permanece solo en `DOCUMENTACION` (p. ej. `REPORTES_MAT_MVC_OPERACION.md` / `REPORTES_MAT_WEB.md`), no en el cuerpo de la vista.
  Archivos: `Views/Reportes/ReporteVentas.cshtml` (y ajuste breve en doc si hace falta trasladar texto técnico).
  Criterio de éxito: Sin jerga HTTP, sin rutas de archivo del repo en la UI; un perfil comercial/administrativo entiende el objetivo del módulo.

- [x] **Reportes UX: Reporte Ventas — fechas en español (dd/mm/aaaa)**
  Ajustar los selectores de **Desde** / **Hasta** para **locale y presentación en español**, placeholder y lectura coherentes con **día/mes/año** (`dd/mm/aaaa`), evitando la sensación de calendario “estilo EE.UU.” (`mm/dd/yyyy`). Mantener el contrato con el backend (normalización a lo que ya espera `ReportesQueryHelper` / endpoints, sin romper consultas ni Excel).
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, scripts/bundles que inicialicen datepicker o `input type="date"` según corresponda.
  Criterio de éxito: En navegador, fechas se entienden y eligen en formato local esperado; una consulta y una exportación manual confirman datos correctos.

- [x] **Reportes UX: Reporte Ventas — filtrar vendedor y cliente solo en el front (sin reconsultar BD)**
  Tras una **Consultar** exitosa, conservar en memoria (JavaScript) el arreglo de filas devuelto y permitir **refinar la grilla** por vendedor y/o por cliente (texto libre, coincidencia sobre nombres o campos ya presentes en el JSON; o controles alimentados únicamente con valores distintos del resultado cargado). **No** debe dispararse nueva petición al servidor ni al SP al cambiar estos filtros. Definir y documentar en la implementación si **Exportar Excel** debe reflejar solo las filas **visibles tras el filtro front** o el **total de la última consulta**, y reflejarlo en una nota breve en pantalla si hay riesgo de confusión.
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, y/o `Scripts/…` siguiendo el patrón del módulo (p. ej. extracción a `mat.reportes-ventas.js` si conviene).
  Criterio de éxito: Filtrado en cliente es inmediato; “Consultar” sigue definiendo el universo de datos según fechas o viaje; criterio Excel acordado y probado.

- [x] **Reportes UX: Reporte Ventas — tarjeta “Total facturado” + modal “Listado de facturas” + Excel en tarjeta**

  **Contexto (estado actual — análisis):**
  - La pantalla `Views/Reportes/ReporteVentas.cshtml` ya obtiene filas vía `GET` `Reportes/Ventas` y las guarda solo en memoria dentro del `DataTable` y de las funciones `renderTable` / `aggregateVentasRows`. El contrato de fila es `ReporteVentaRowDto` (camelCase en JSON): `viajeDescripcion`, `fechaSalida`, `cantidadButacas`, `vendedorFullName`, `clienteFullName`, `facturaId`, `facturaFecha`, `facturaEstado`, `monedaTipo` (`"1"` ARS, `"3"` U$S), `totalFactura`, `montoPagado`, `saldo`, etc.
  - Los KPI del bloque **Resumen de la consulta** se calculan en cliente con la misma lógica que debe reutilizarse (o extraerse a helper JS compartido) para los totales del modal **sobre el subconjunto filtrado**.
  - La exportación Excel del reporte la genera el servidor (`Reportes/VentasExcel` → `ReportesExcelExport.BuildVentas`): columnas fijas (Viaje ID, Viaje, Fecha salida, Butacas, Vendedor ID, Vendedor, Cliente ID, Cliente, Factura ID, Fecha factura, Estado, Moneda, Total / Pagado / Saldo). El helper `Scripts/mat.reportes-excel-export.js` hoy deshabilita solo `#btnExcel` durante la descarga; conviene parametrizar el botón objetivo o soportar múltiples disparadores sin duplicar lógica.
  - Los mockups adjuntos (tema oscuro) definen **layout y comportamiento**; la implementación debe mantener **coherencia visual con el Admin actual** (Bootstrap 5, cards claras en `_LayoutAdmin`) salvo decisión explícita de producto de adoptar tema oscuro en este módulo.

  **A) Tarjeta “Total facturado” (solo esta card):**
  - Es la única card interactiva: `cursor-pointer`, estados **hover** discretos (borde/sombra/fondo) acordes al sistema Admin.
  - **Tooltip** al posar el cursor sobre el área clicable de la tarjeta (no sobre el botón): texto *“Haz clic para ver el listado de facturas”* (Bootstrap 5 tooltip o `title` accesible, según patrón del proyecto).
  - **Clic** en la tarjeta (fuera del botón): abrir el modal de listado (equivalente a `isInvoiceModalOpen` / `InvoiceListModal` en el pseudocódigo); **no** nueva llamada al API: trabajar sobre la **última respuesta** de la consulta de ventas (mismo universo que la grilla principal).
  - Incorporar en el pie de la card un botón **“Exportar a Excel”** (icono documento + texto). En el handler del botón usar **`event.stopPropagation()`** para que **no** abra el modal; la acción de exportación debe ser la **misma** que el botón global **Exportar Excel** de la página (mismos `queryParams` → `VentasExcel`), de modo que el archivo coincida con el generado hoy por `ReportesExcelExport.BuildVentas` (mismo criterio: **toda la última consulta**, sin aplicar filtros del modal ni del filtro front vendedor/cliente, salvo que negocio documente lo contrario en `PROGRESS`).

  **B) Modal “Listado de Facturas”:**
  - **Fuente de datos:** copia de trabajo sobre el arreglo de filas de la última consulta exitosa (`ventasData` / nombre acordado). Al abrir, **sin** `getJSON` adicional.
  - **Orden por defecto:** fecha de factura **más reciente primero** (normalizar parsing si el JSON trae `Date` ISO o string).
  - **Búsqueda de texto** (un solo campo): filtra en cliente por coincidencia en número o identificadores visibles de factura, nombre cliente, vendedor y descripción de viaje (campos ya presentes en el DTO / columnas actuales).
  - **Filtros adicionales:** estado de factura (desplegable **dinámico** con valores distintos presentes en los datos + opción “Todos”), moneda (Todas / ARS / U$S alineado a `monedaTipo`), rango **fecha desde / hasta** aplicado sobre **fecha de factura**, botón **Limpiar filtros** que resetea búsqueda y filtros.
  - **Contador:** texto tipo *“Mostrando X de Y facturas”* (Y = total filas de la última consulta; X = tras búsqueda + filtros).
  - **Tabla:** columnas Fecha (factura), Cliente, Vendedor, Estado, Moneda, Total factura, Monto pagado, Saldo, **Acciones** (copiar **Factura ID** al portapapeles con feedback; expandir/contraer fila).
  - **Detalle expandido:** al menos Viaje, Fecha salida, Factura ID (GUID), Cantidad butacas — datos ya disponibles en el DTO.
  - **Resumen en modal:** totales **recalculados** sobre el subconjunto filtrado: facturado, cobrado y saldo **por moneda** (misma regla de agregación que KPI actuales para `"1"`, `"3"` y otras); opcionalmente subtotales de saldo destacados como en mockup (texto secundario).
  - **Accesibilidad:** modal Bootstrap 5, foco y cierre con teclado/Escape, `aria-*` razonables.

  **C) Implementación y archivos:**
  - Principal: `Views/Reportes/ReporteVentas.cshtml` (marcado modal + card); si el JS supera ~150–200 líneas relevantes, extraer a `Scripts/mat.reportes-ventas-facturado-modal.js` (o nombre alineado al proyecto) y referenciar en la vista + `MAT.MVC.csproj` si aplica.
  - Ajuste mínimo esperado: `Scripts/mat.reportes-excel-export.js` (botón a deshabilitar durante fetch parametrizable o soporte multi-botón).
  - **No** requiere cambio de SP ni `ReportesController` salvo se decida exportar solo filas filtradas del modal (nuevo contrato); fuera de alcance por defecto.

  **D) Mejoras opcionales recomendadas (funcionalidad + UI)** — implementar las que quepan en el mismo entregable o dejar documentadas para una segunda iteración:
  - **Funcionalidad**
    - **Exportar subconjunto del modal:** botón dentro del modal que genere Excel/CSV **solo con las filas que pasan** búsqueda + filtros (sin nuevo SP: export en cliente con las columnas visibles, o endpoint POST que reciba lista de `FacturaId` — acordar en implementación y límites de tamaño).
    - **Ordenación por columna** en la tabla del modal (clic en encabezado: fecha, total, saldo, cliente, etc.; indicador ▲/▼).
    - **Expandir / contraer todas** las filas de detalle con un control en la barra de herramientas del modal.
    - **Debounce** en el campo de búsqueda (~200–300 ms) para listas grandes y menos trabajo en cada tecla.
    - **Sincronía con el filtro front** de la página (vendedor/cliente): opción de que, al abrir el modal desde la tarjeta, el listado arranque **ya acotado** con los mismos criterios que la grilla principal (con leyenda “Aplicando filtros de la tabla” o botón “Ver todas las facturas de la consulta” para volver al universo completo).
    - **Deep link / estado en URL** (opcional, baja prioridad): query `?invoiceList=1` o hash para reabrir el modal tras F5 — solo si no complica el routing Admin.
    - **Atajo de teclado:** con foco en la tarjeta o en la página, `Enter`/`Alt+L` para abrir el modal (documentar en tooltip o ayuda breve).
  - **UI / UX**
    - **Subtítulo contextual** en el modal: mostrar en texto legible el criterio de la última consulta (rango de fechas `dd/mm/aaaa` o nombre del viaje seleccionado), para orientar al usuario.
    - **Estado vacío** cuando no hay resultados tras filtrar: mensaje claro + sugerencia “Limpiar filtros”; si no hubo consulta aún, deshabilitar la tarjeta o mostrar tooltip “Primero ejecutá Consultar”.
    - **Cabecera de tabla fija** (`sticky`) dentro del cuerpo del modal con scroll vertical en listas largas.
    - **Montos y saldos con jerarquía visual:** alinear con el reporte principal (saldo > 0 en énfasis rojo suave o `text-danger` acorde a BS5; moneda con badge como en KPI).
    - **Feedback de “Copiado”** accesible: toast o `aria-live` además del ícono, y soporte si `navigator.clipboard` falla (fallback seleccionar texto).
    - **Responsive:** en viewport angosto, modal **fullscreen** o casi fullscreen; filtros en acordeón o apilados; tabla con scroll horizontal explícito.
    - **Movimiento reducido:** respetar `prefers-reduced-motion` en transiciones del modal y hover de la tarjeta.
    - **Indicador sutil en la tarjeta** cuando hay datos cargados (p. ej. punto o borde accent), coherente con el sistema de iconos Admin — sin parecer “notificación” de error.
    - **Enlace opcional** a pantalla de detalle de factura / cliente si en el proyecto ya existe ruta estable (abrir en nueva pestaña); si no hay ruta, omitir.

  **Criterio de éxito:** Tras **Consultar**, la tarjeta Total facturado abre el modal con listado consistente con los datos mostrados en la tabla principal; filtros y búsqueda solo afectan el modal y sus totales; el botón Excel en la tarjeta descarga el mismo tipo de archivo que el export global; `stopPropagation` evita abrir modal al exportar; las demás cards del resumen siguen no clicables; MSBuild `MAT.MVC` Debug sin errores; breve nota en `PROGRESS.md` al cerrar el task **indicando qué ítems de D** se implementaron o se posponen.

- [x] **Reportes UX: Modal listado de facturas — datepickers en español (dd/mm/aaaa)**
  Los filtros **F. desde** / **F. hasta** del modal usaban `input type="date"` (calendario nativo según idioma del SO, a menudo inglés y formato MM/DD). Se alinearon al mismo patrón que **Desde/Hasta** del reporte: `type="text"` + clase `mat-datepicker` + `$.datepicker.regional['es']`. En `mat.reportes-ventas-facturado-modal.js`: limpieza con `datepicker('setDate', null)` y `_parseDate` con año de dos dígitos coherente con `dd/mm/yy` de jQuery UI.
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, `Scripts/mat.reportes-ventas-facturado-modal.js`
  Criterio de éxito: Calendario y formato en español; filtros por fecha del modal correctos; MSBuild `MAT.MVC` limpio.

- [x] **Reportes UX: Top 3 rankings — contraste panel y tarjetas**
  Las cards del Top Vendedores / Top Destinos (blancas, `border-0`, sombra muy suave) apenas se distinguían del fondo `#f3f6fb` del admin. Se añadió contenedor **`reporte-rankings-strip`** (gradiente más contrastado, borde e highlight interior) envolviendo ambas secciones y estilos **` .card.card-top-ranking`** con borde sutil, sombra en capas y hover reforzado.
  Archivos: `Views/Reportes/ReporteVentas.cshtml`
  Criterio de éxito: Rankings legibles a primera vista; MSBuild limpio.

- [x] **Reportes UX: Top 3 Vendedores — medallas 🥇🥈🥉 y efecto destacado 1er puesto**
  El 2º puesto refería **`bi-medal`**, que no existe en el `bootstrap-icons.css` versionado del proyecto (quedaba vacío). Se muestran **Unicode** 🥇 🥈 🥉. La tarjeta del **1er** puesto lleva **`card-top-ranking-first`**: animación de franja que cruza **de derecha a izquierda** (efecto “espejado”), borde/oro acorde al podio; iteración final de color a **amarillo oscuro / oro** (sin mezcla negra). Se respeta **`prefers-reduced-motion`** y hover que conserva el realce dorado.
  Archivos: `Scripts/mat.reportes-vendedores.js`, `Views/Reportes/ReporteVentas.cshtml` (estilos)
  Criterio de éxito: Tres puestos con medalla visible; 1er puesto claramente destacado; MSBuild limpio.

#### Reportes — Top Vendedores y Top Destinos (derivado de datos ya cargados)

- [x] **Reportes: Persistir `lastRows` como fuente compartida en ReporteVentas (prerequisito)**
  **Contexto:** Hoy el resultado de la consulta de ventas vive solo dentro del `DataTable` y de las funciones `renderTable` / `aggregateVentasRows`. Los tasks de modal de facturas, Top Vendedores y Top Destinos necesitan acceder al mismo arreglo en cualquier momento sin relanzar el `$.getJSON`.
  **Acción (mínima y no regresiva):** declarar `var lastRows = [];` en el closure de `$(function(){...})` de `ReporteVentas.cshtml` y asignarlo dentro de `renderTable(rows)` antes de cualquier otra operación (`lastRows = rows || [];`). Al limpiar / nueva consulta, `lastRows = []`. No cambia nada más del flujo existente.
  Archivos: `Views/Reportes/ReporteVentas.cshtml`.
  Criterio de éxito: `lastRows` disponible en el scope del módulo tras Consultar exitoso; DataTable, KPI y filtro front funcionan igual; MSBuild limpio. *Este task es bloqueante para los dos tasks siguientes y para el task del modal de facturas.*

- [x] **Reportes UX: Reporte Ventas — sección "Top 3 Vendedores" con cards y modal "Lista completa"**

  **Contexto técnico (estado del código):**
  - `ReporteVentaRowDto` ya expone `VendedorId`, `VendedorFullName`, `MonedaTipo`, `TotalFactura`, `MontoPagado`, `Saldo` — todos los campos necesarios.
  - `aggregateVentasRows` acumula totales globales pero no por vendedor; hay que agregar `aggregateByVendedor(rows)` como función hermana que hace `reduce` por `vendedorId`.
  - `renderMonedaRowsHtml` y `parseMoneyCell` son helpers reutilizables para mostrar los montos por moneda en las cards.
  - No se necesita cambio de SP ni de backend; todo el cálculo es en cliente sobre `lastRows`.

  **A) Agregación `aggregateByVendedor(rows)`:**
  Por cada fila de `rows`, hacer `reduce` usando `vendedorId` como clave:
  ```
  { vendedorId, nombre, ventas++, total += totalFactura,
    totalesPorMoneda["1"|"3"] += totalFactura,
    saldosPorMoneda["1"|"3"] += saldo }
  ```
  Ordenar el resultado por `total` descendente (suma numérica cruda de monedas mezcladas — mismo criterio del sistema de referencia). `Top 3` = primeros 3 del arreglo ordenado.

  **B) Bloque HTML "Top 3 Vendedores" (entre KPI y la grilla):**
  - Visible solo cuando `lastRows.length > 0`; oculto/desmontado al limpiar o al inicio.
  - Cabecera: título "Top 3 Vendedores" + botón "Ver todos" alineado a la derecha.
  - Tres cards Bootstrap 5 en grid `row-cols-1 row-cols-md-3 g-3` con diseño coherente con los KPI del bloque "Resumen":
    - **Medalla de posición:** `#1` / `#2` / `#3` con **Unicode** 🥇 🥈 🥉 *(reemplazo 2026-04: `bi-medal` no existía en el CSS de Bootstrap Icons del repo; ver task “Top 3 Vendedores — medallas” más abajo).*
    - **Nombre del vendedor** (truncado a una línea con `text-truncate`).
    - **Cantidad de ventas** en texto secundario.
    - **Totales por moneda** (solo mostrar monedas con valor > 0): reutilizar `renderMonedaRowsHtml` o patrón inline con badges `$ARS` / `U$D` + monto formateado `es-AR`.
    - **Saldo por cobrar por moneda** (solo si saldo > 0; si no, texto "Sin saldo pendiente" en `text-muted small`).
  - Hover en cada card: transición sutil de sombra acordada con el task de hover de cards del admin.

  **C) Modal "Lista completa de vendedores":**
  - Disparado por botón "Ver todos" — sin nuevo `$.getJSON`; trabajar sobre `aggregateByVendedor(lastRows)` completo.
  - Modal Bootstrap 5 tamaño `modal-xl`; título "Lista completa de vendedores" + icono `bi-people`.
  - **Búsqueda:** un campo `<input>` que filtra en tiempo real por nombre de vendedor (debounce ~250ms).
  - **Tabla ordenable:** columnas `#`, Vendedor, Total ventas, Total $ARS, Total U$D, Saldo $ARS, Saldo U$D, Promedio.
    - Promedio = `total / ventas` formateado con `es-AR`.
    - Saldo U$D: si 0 → "Sin saldo" (texto muted).
    - Orden default: `total` desc; clic en encabezado numérico invierte asc/desc con indicador ▲/▼.
  - **Contador:** "Mostrando X de Y vendedores".
  - **Exportar CSV:** botón "Exportar CSV" en la cabecera del modal; genera en cliente (sin servidor) un CSV con columnas: Vendedor, Total ventas, Total ARS, Total USD, Saldo ARS, Saldo USD, Promedio. Nombre de archivo `vendedores-YYYY-MM-DD.csv`. Usar `Blob` + `URL.createObjectURL` (mismo patrón que el Excel export del reporte).
  - **Estado vacío:** si no hay datos (lastRows vacío al abrir), mostrar mensaje + ícono `bi-people` sin tabla.

  **D) Implementación y archivos:**
  - `Views/Reportes/ReporteVentas.cshtml`: marcado HTML del bloque Top 3 y del modal.
  - Si el JS supera ~150 líneas relevantes, extraer a `Scripts/mat.reportes-vendedores.js`; registrar en vista + `MAT.MVC.csproj` si aplica.
  - **No** requiere cambios en `ReportesController`, SP ni modelos C#.

  Criterio de éxito: Tras Consultar, el bloque Top 3 Vendedores aparece con datos correctos; "Ver todos" abre el modal con lista completa; búsqueda, orden y CSV funcionan sin nueva llamada al servidor; las tres medallas muestran el vendedor correcto según suma de `totalFactura`; bloque oculto antes de la primera consulta; MSBuild limpio.

- [x] **Reportes UX: Reporte Ventas — sección "Top 3 Destinos" con cards y modal "Lista completa"**

  **Contexto técnico (estado del código):**
  - `ReporteVentaRowDto` expone `ViajeId`, `ViajeDescripcion`, `MonedaTipo`, `TotalFactura`.
  - Mismos helpers reutilizables que el task anterior.
  - **Criterio de ranking diferente al de vendedores:** destinos se ordenan por **cantidad de ventas** (filas por `viajeId`), no por monto.

  **A) Agregación `aggregateByViaje(rows)`:**
  Por cada fila de `rows`, `reduce` usando `viajeId` como clave:
  ```
  { viajeId, descripcion, ventas++, total += totalFactura,
    totalesPorMoneda["1"|"3"] += totalFactura }
  ```
  Ordenar por `ventas` descendente. `Top 3` = primeros 3.

  **B) Bloque HTML "Top 3 Destinos" (después del Top 3 Vendedores, antes de la grilla):**
  - Mismo comportamiento de visibilidad (visible solo con datos).
  - Cabecera: "Top 3 Destinos" + botón "Ver todos".
  - Tres cards con:
    - **Posición** `#1` / `#2` / `#3` con iconos `bi-geo-alt-fill` o variante.
    - **Nombre del viaje** (truncado).
    - **Cantidad de ventas.**
    - **Monto total** formateado (suma numérica cruda de ambas monedas, con prefijo `$` y `es-AR`; nota: sin distinguir moneda, equivalente al sistema de referencia — documentar la limitación en comentario HTML o leyenda).

  **C) Modal "Lista completa de destinos":**
  - Mismo patrón que el modal de vendedores.
  - Tabla: `#`, Destino, Total ventas, Monto total, Promedio (total / ventas).
  - Orden default: ventas desc; ordenable también por descripcion y total.
  - Búsqueda por descripción del viaje.
  - Exportar CSV: `destinos-YYYY-MM-DD.csv`; columnas: Destino, Total ventas, Monto total, Promedio.
  - Estado vacío coherente.

  **D) Archivos:**
  - `Views/Reportes/ReporteVentas.cshtml`; si conviene, `Scripts/mat.reportes-destinos.js`.
  - **No** requiere backend.

  Criterio de éxito: Top 3 Destinos muestra los viajes con más filas en el resultado; modal con búsqueda, orden y CSV; ranking correcto por cantidad de ventas (no por monto); bloque oculto hasta Consultar; MSBuild limpio.

#### Reportes — UX/UI (Reporte de pagos, Admin)

- [x] **Reportes UX: Reporte Pagos — tarjetas de estadísticas (KPI) antes de la tabla**
  Añadir entre el panel de filtros front y la tabla de resultados un bloque de **cards de indicadores** calculados en cliente sobre el JSON de la última consulta (sin nueva petición al servidor), alineado al patrón de `ReporteVentas`.
  **Indicadores:**
  - **Total cobrado** — suma de `monto` desglosado por moneda ($ARS / U$S), badges + `Intl.NumberFormat("es-AR")`.
  - **Resumen operativo** — total de registros (pagos) y cantidad de facturas distintas (`facturaId` único).
  - **Por tipo de venta** — agrupar por `tipoVentaDescripcion`: conteo, porcentaje del total de filas, montos ARS/U$S.
  - **Por medio de pago** — agrupar por `pagoDescripcion`: ordenar por total de filas; cada ítem con nombre, % filas y montos ARS/U$S.
  Las cards se ocultan antes de la primera consulta y si el resultado está vacío. Los filtros front (vendedor, cliente, medio) **no** alteran los KPI; leyenda explícita en pantalla.
  Archivos: `Views/Reportes/ReportePagos.cshtml`.
  Criterio de éxito: Tras Consultar con datos, las tarjetas muestran totales coherentes; consulta vacía/error mantiene bloque oculto; tabla y Excel sin regresión; MSBuild limpio.

#### Reportes — Calidad de datos en tablas (formato visual)

- [x] **Reportes UI: Fechas en formato dd/mm/aaaa en todas las tablas de reportes**
  Los tres reportes (Ventas, Pagos, Ranking) muestran columnas de fecha con el valor crudo del JSON (timestamp ISO `2026-04-02T00:00:00` o string del SP). La única excepción correcta es Ranking que ya usa `Intl.DateTimeFormat("es-AR")`. Unificar con un helper JS reutilizable `formatFechaES(val)` (solo fecha, sin hora) e inyectarlo en `render` de DataTables en las tres vistas.
  **Columnas afectadas:** `fechaSalida` y `facturaFecha` en Ventas; `fechaPago` en Pagos; `fecha` y `viajeFechaSalida` en Ranking (ya parcialmente resuelto — verificar paridad).
  **Regla de formateo:** entrada puede ser ISO string, `Date` serializado de .NET (`/Date(ms)/`) o `dd/MM/yyyy` — normalizar a `dd/mm/aaaa` en pantalla. El Excel no cambia (ya tiene `ToString("d", es-AR)` en `ReportesExcelExport`).
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, `ReportePagos.cshtml`, `ReporteRanking.cshtml`; si conviene, helper en `Scripts/mat.reportes-utils.js` y registro en bundle.
  Criterio de éxito: Todas las fechas en las tres tablas Admin se leen en `dd/mm/aaaa`; sin hora ni milisegundos; MSBuild limpio.

- [x] **Reportes UI: Unificar columna "Viaje" + "Fecha salida" en una sola celda (Ventas y Ranking)**
  En el Reporte de Ventas y Ranking las columnas "Viaje" y "Fecha salida" son adyacentes y separar la fecha en columna propia consume ancho sin aportar legibilidad. Combinarlas en una celda de **dos líneas**: primera línea descripción del viaje (texto truncado con `title` al hover), segunda línea la fecha en `dd/mm/aaaa` en gris/pequeño, con icono `bi-calendar3` como indicador visual.
  La columna combinada se llama "Viaje" en el encabezado; la fecha se renderiza debajo en tono `text-muted small`.
  Actualizar `columnDefs` de DataTables y el `render` correspondiente; el Export Excel **no cambia** (sigue con columnas separadas en el archivo).
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, `Views/Reportes/ReporteRanking.cshtml`.
  Criterio de éxito: Una sola columna "Viaje" muestra descripción + fecha; tabla más compacta; export Excel intacto; MSBuild limpio.

- [ ] **Reportes UI: Chips/badges para estado de factura en tablas de reportes**
  La columna `facturaEstado` (Ventas) y equivalente en Pagos muestra texto plano (`Pagado`, `Pre-Reserva`, `Anulado`, etc.) sin diferenciación visual. Usar **badges Bootstrap 5** con color semántico según valor: `Pagado` → `bg-success-subtle text-success`; `Pre-Reserva` → `bg-warning-subtle text-warning`; `Anulado` → `bg-danger-subtle text-danger`; otros → `bg-secondary-subtle text-secondary`.
  El mapeo se define en un helper JS `estadoBadgeHtml(val)` reutilizable para ambos reportes.
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, `Views/Reportes/ReportePagos.cshtml`; helper en `Scripts/mat.reportes-utils.js` si ya existe por el task de fechas.
  Criterio de éxito: Estados se leen con color semántico en las tres tablas; sin regresión en filtros DataTables; MSBuild limpio.

- [ ] **Reportes UI: Moneda y montos — "$ARS" / "U$D" y formateo numérico en tablas**
  **Problema actual:** la columna "Mon." muestra el código numérico (`1`, `3`); los montos (Total, Pagado, Saldo) son números crudos sin separador de miles ni símbolo de moneda. Los KPI del resumen ya resuelven esto correctamente con badges y `Intl.NumberFormat("es-AR")`.
  **Cambios requeridos:**
  - Reemplazar la columna "Mon." por un **badge de moneda** reutilizando el mismo patrón del KPI: `"1"` → badge `$ARS` (success-subtle), `"3"` → badge `U$D` (info-subtle).
  - En las columnas de montos (Total factura / Pagado / Saldo), anteponer el **símbolo según la moneda de la fila** y aplicar `Intl.NumberFormat("es-AR")` con separador de miles.
  - En Reporte Pagos: igual criterio para la columna `monto`.
  - El helper `renderMontoCurrency(monto, monedaTipo)` encapsula el formateo y se reutiliza en las tres vistas.
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, `ReportePagos.cshtml`, `ReporteRanking.cshtml`; helper en `Scripts/mat.reportes-utils.js`.
  Criterio de éxito: Ningún código numérico visible para moneda; montos con separador de miles y símbolo; Export Excel sin cambio; MSBuild limpio.

#### Admin — Modernización visual del panel (UI/UX transversal)

- [ ] **Admin UI: Unificar sistema de tokens CSS (`--admin-*`) en todo el panel**
  **Problema:** el panel Admin usa tres "mundos" de variables CSS que no convergen: `--admin-*` (en `admin.modern.css`), `--text-primary` / `--primary-color` (fallback rosa del ecosistema principal, en `Admin/Index` y `GridResumenPagos`), y valores hardcodeados inline en vistas legacy. Esto provoca que colores, tipografía y tamaños difieran entre pantallas del mismo panel.
  **Acción:** en `admin.modern.css` mapear alias `--text-primary: var(--admin-text)`, `--text-secondary: var(--admin-text-muted)`, `--border-color: var(--admin-border)`, `--primary-color: var(--admin-primary)`, `--primary-rgb: var(--admin-primary-rgb)`, `--bg-secondary: var(--admin-surface-2)` en el selector `:root body.admin-modern` (o clase equivalente del body en `_LayoutAdmin`). Eliminar definiciones inline duplicadas en `Admin/Index.cshtml` y `GridResumenPagos.cshtml`.
  Archivos: `Content/admin.modern.css`, `Views/Admin/Index.cshtml`, `Views/Admin/GridResumenPagos.cshtml`.
  Criterio de éxito: Sin múltiples sistemas de variables conviviendo; paleta visual uniforme entre pantallas Admin; MSBuild limpio; smoke test visual de las 5 vistas Admin principales.

- [ ] **Admin UI: Transiciones y hover en cards del panel (Index, Reportes/Index)**
  **Problema:** las tarjetas de `Admin/Index.cshtml` ya tienen `cursor-pointer` y algo de `transform`, pero no son totalmente coherentes con `Reportes/Index.cshtml` (más plano) ni con el modal de confirmación. Unificar la experiencia de **hover** en **todas** las cards navegables del admin:
  - **Elevación:** `box-shadow` más pronunciado al hover (transición `0.18s ease`).
  - **Desplazamiento sutil:** `translateY(-3px)` al hover.
  - **Borde accent:** borde izquierdo `3px solid --admin-primary` aparece suavemente al hover.
  - **Transición de icono:** el icono circular de fondo hace `scale(1.08)` al hover.
  - **Clases reutilizables:** extractar todo en `.admin-card-link` en `admin.modern.css`; aplicar en `Admin/Index` y `Reportes/Index`; documentar uso.
  - Respetar `prefers-reduced-motion` (no translate ni transition si el usuario lo pidió).
  Archivos: `Content/admin.modern.css`, `Views/Admin/Index.cshtml`, `Views/Reportes/Index.cshtml`.
  Criterio de éxito: Hover coherente y animado (pero sutil) en todas las cards del admin; sin regresión en tarjetas del dashboard principal; MSBuild limpio.

- [ ] **Admin UI: Sticky header en tablas DataTables del panel**
  Las tablas largas (Usuarios, Reportes, SistemaParametros) pierden los encabezados al hacer scroll vertical. Implementar cabecera fija (`position: sticky; top: 0; z-index: 2`) para `thead` en las tablas `.modern-table` dentro del panel Admin.
  **Alcance:** agregar regla en `admin.modern.css` dentro de `.admin-modern .modern-table-container thead th` con `position: sticky` y fondo `--admin-surface` para opacar el contenido al pasar por debajo. Verificar que no rompa las columnas congeladas de DataTables en las vistas afectadas.
  **Vistas a verificar:** `Usuarios.cshtml`, `ErrorLog.cshtml`, `SistemaParametros.cshtml`, `ReporteVentas.cshtml`, `ReportePagos.cshtml`, `ReporteRanking.cshtml`.
  Archivos: `Content/admin.modern.css`.
  Criterio de éxito: Encabezado visible en scroll vertical en todas las tablas Admin; sin artefactos visuales con DataTables; MSBuild limpio.

- [ ] **Admin UI: Indicadores de carga (spinner) en todas las operaciones async del panel**
  Varias acciones admin disparan `$.getJSON` o `fetch` sin ningún indicador visible de carga (p. ej. Consultar en Reportes Ventas/Pagos/Ranking). El CSS ya tiene `.modern-loading-spinner`; el uso es esporádico.
  **Patrón a implementar:** `MatAdmin.showLoading(container)` / `MatAdmin.hideLoading(container)` en `Scripts/mat.admin-utils.js` (crear si no existe, registrar en bundle). `container` puede ser el botón disparador (poner `disabled` + spinner inline en el texto) o un overlay sobre la tabla.
  **Aplicar en:** botón "Consultar" de los tres reportes (antes del `$.getJSON`, restaurar en `.done`/`.fail`); acciones CRUD de SistemaParametros.
  Archivos: `Scripts/mat.admin-utils.js` (nuevo o extender), `Views/Reportes/ReporteVentas.cshtml`, `ReportePagos.cshtml`, `ReporteRanking.cshtml`, `Views/Admin/SistemaParametros.cshtml`.
  Criterio de éxito: Toda operación async del panel muestra feedback visual durante la espera; sin doble click posible en acciones de escritura; MSBuild limpio.

- [ ] **Admin UI: Estados vacíos amigables en tablas del panel**
  Cuando una consulta devuelve cero filas, DataTables muestra el mensaje por defecto de internacionalización del plugin (texto plano, sin contexto). Reemplazar por un componente de **estado vacío** con icono `bi-inbox` (o similar), título contextual ("No hay resultados para este filtro") y sugerencia de acción ("Probá con otro rango de fechas o limpiar los filtros"), siguiendo el patrón de los `<div class="alert alert-info">` ya usados en `ErrorLog` y `Logs`.
  **Implementación:** `columnDefs` de DataTables con `language.emptyTable` personalizado, o función `renderEmpty(msg, sugerencia)` que retorna HTML usado en `language.emptyTable` y/o en el `initComplete` callback. Aplicar en los tres reportes y en SistemaParametros.
  Archivos: `Content/admin.modern.css` (estilos del estado vacío), `Views/Reportes/*.cshtml`, `Views/Admin/SistemaParametros.cshtml`.
  Criterio de éxito: DataTables vacío muestra icono + texto contextual; ningún reporte muestra solo "No hay datos disponibles" del plugin sin contexto; MSBuild limpio.

- [ ] **Admin UI: Breadcrumbs contextuales en el panel**
  Ninguna pantalla Admin muestra la ubicación actual dentro del panel (solo el `<title>` cambia). Un breadcrumb `Administración › Reportes › Ventas` (o equivalente) en la cabecera de cada vista orienta al usuario, especialmente en módulos anidados.
  **Implementación:** sección `@section BreadcrumbItems` en `_LayoutAdmin.cshtml` que renderiza un `<nav aria-label="breadcrumb">` debajo del topbar si se provee; las vistas que lo soporten declaran la sección con ítems `<li class="breadcrumb-item">`. Vistas prioritarias: `ReporteVentas`, `ReportePagos`, `ReporteRanking`, `SistemaParametros`, `Usuarios`, `ErrorLog`.
  Archivos: `Views/Shared/_LayoutAdmin.cshtml`, `Content/admin.modern.css` (estilos breadcrumb), vistas prioritarias.
  Criterio de éxito: Las vistas listadas muestran breadcrumb; las que no declaran `BreadcrumbItems` no muestran nada extra; Bootstrap 5 `breadcrumb` estilizado con `--admin-*`; MSBuild limpio.

- [ ] **Admin UI: Sidebar — completar navegación (SistemaParametros + enlace a módulos activos)**
  El menú lateral de `_LayoutAdmin.cshtml` expone Reportes y Usuarios pero **no** enlaza a `SistemaParametros`. Si el usuario accede desde una URL directa o bookmark, no tiene forma de volver al módulo ni de descubrir otros módulos del admin desde el sidebar.
  **Acción:** agregar ítem "Parámetros del sistema" bajo la sección "Sistema" en el menú lateral (desktop y offcanvas mobile), con icono `bi-sliders`. Evaluar si agregar también `AuditoriaFacturas` si está activa. Revisar que todos los ítems del sidebar tengan `aria-current="page"` cuando corresponde.
  Archivos: `Views/Shared/_LayoutAdmin.cshtml`.
  Criterio de éxito: Todos los módulos admin activos aparecen en el sidebar; `SistemaParametros` accesible desde el menú; sin ítems huérfanos; MSBuild limpio.

- [ ] **Admin UI: Reemplazar `confirm()` y `alert()` nativos en SistemaParametros por modales Bootstrap 5**
  `Views/Admin/SistemaParametros.cshtml` usa `confirm()` del navegador para toggle de estado y `alert()` para errores de validación frontend. Esto rompe la consistencia visual con Usuarios (que ya usa modal Bootstrap) y bloquea el hilo JS.
  **Reemplazar por:** modal de confirmación BS5 reutilizando el patrón de `Usuarios.cshtml` (`data-bs-toggle`, handler JS) para toggle; `mostrarMensaje(msg, tipo)` (o equivalente inline con `alert-dismissible` Bootstrap) para errores de validación.
  Archivos: `Views/Admin/SistemaParametros.cshtml`.
  Criterio de éxito: Sin `confirm()` ni `alert()` en la vista; confirmaciones con modal Bootstrap; MSBuild limpio.

- [ ] **Admin UI: Modernizar ErrorLog.cshtml al estilo `modern-*` del panel**
  `ErrorLog.cshtml` usa colores hardcodeados (`#2c3e50`), badges numéricos custom (`.badge-1`, `.badge-2`, `.badge-3`) y clases propias que no corresponden al sistema visual del panel Admin moderno.
  **Cambios:**
  - Reemplazar paleta hardcodeada por variables `--admin-*`.
  - Convertir badges de importancia numérica (1/2/3) a badges semánticos Bootstrap 5: `1` → `bg-secondary-subtle` "Baja"; `2` → `bg-warning-subtle` "Media"; `3` → `bg-danger-subtle` "Alta".
  - Cabecera y layout con `.modern-page-title`, `.modern-page-container`.
  - Formulario de filtros con `.form-control`, `.form-select` BS5, input de fecha `type="date"` alineado visualmente con el resto.
  - El filtro de fecha `type="date"` muestra `yyyy-mm-dd` según el navegador — agregar un placeholder o label explícito.
  Archivos: `Views/Admin/ErrorLog.cshtml`.
  Criterio de éxito: Sin colores hardcodeados; badges semánticos con etiqueta; layout coherente con el resto del Admin; funcionalidad intacta; MSBuild limpio.

- [ ] **Admin UI: Modernizar Logs.cshtml — look de herramienta, no de página en blanco**
  `Logs.cshtml` es un formulario GET + `<pre>` sin estructura visual moderna. Es una herramienta técnica pero igual forma parte del panel Admin.
  **Cambios mínimos:** envolver en `.modern-page-container`; formulario de filtros (fecha, cantidad de líneas) con cards BS5; el `<pre>` dentro de un bloque `card` con fondo `--admin-surface-2`, borde, `font-family: monospace`, scroll vertical máx 600px; agregar enlace de retorno al panel en la cabecera con `.modern-page-header`.
  Archivos: `Views/Admin/Logs.cshtml`.
  Criterio de éxito: Vista encuadrada en el sistema visual del admin; `<pre>` con scroll; navegación de retorno; MSBuild limpio.

- [ ] **Admin UI: Consistencia tipográfica y ortográfica en todo el panel**
  Problemas encontrados en el análisis:
  1. `Admin/Index` usa `h1` a `1.75rem`; el resto del admin usa `.modern-page-title` a `1.5rem` — **inconsistencia de escala**.
  2. Textos sin tildes: "Administracion", "Seccion", "Auditoria", "Configuracion" — **calidad percibida baja**.
  3. Los números 1–4 en las tarjetas del Index parecen cantidad de ítems; en realidad son orden — usar icono o eliminar si confunden.
  **Acción:** corregir tildes en `Admin/Index.cshtml` y `_LayoutAdmin.cshtml`; unificar tamaño de título con `.modern-page-title`; revisar si los badges numéricos del Index aportan o confunden.
  Archivos: `Views/Admin/Index.cshtml`, `Views/Shared/_LayoutAdmin.cshtml`.
  Criterio de éxito: Sin palabras con tildes faltantes en el panel; escala de título uniforme; MSBuild limpio.

- [ ] **Admin UI: Feedback con toasts (Bootstrap 5) para acciones async en el panel**
  Hoy las acciones CRUD de SistemaParametros y otras vistas admin muestran feedback con `mostrarMensaje` (alert fijo en la página) o alerts que desaparecen tras 3s de forma abrupta. Los Toasts de Bootstrap 5 (ya incluido en el bundle del admin) son el patrón idiomático para confirmar acciones async sin interrumpir el flujo.
  **Acción:** crear `MatAdmin.toast(msg, tipo)` en `Scripts/mat.admin-utils.js` que instancia y muestra un `<div class="toast">` BS5 posicionado en `bottom-end` o `top-end` del viewport; reemplazar `mostrarMensaje` en SistemaParametros por este helper. Aplicar también en cualquier CRUD async del panel que hoy use alerts flotantes.
  Archivos: `Scripts/mat.admin-utils.js`, `Views/Admin/SistemaParametros.cshtml`, y registro en bundle si aplica.
  Criterio de éxito: Confirmaciones de éxito/error como toasts en el extremo del viewport; sin bloqueo de UI; `mostrarMensaje` legacy puede coexistir o quedar en desuso; MSBuild limpio.

### P3 — Nuevas capacidades

- [x] **Admin: Agregar acción de eliminar usuario**
  El sistema permite deshabilitar/habilitar/desbloquear usuarios pero no eliminarlos. Si se necesita borrar un usuario del sistema (membership + UserProfile), no hay UI ni acción en el controller. Implementar `UsuarioEliminar` con confirmación explícita. Restricciones: no puede eliminarse a sí mismo, no puede eliminar al último administrador.
  Archivos: `Controllers/Admin/AdminController.cs`, `Views/Admin/Usuarios.cshtml`
  Criterio de éxito: Existe botón "Eliminar" en el listado (solo visible para admins sobre otros usuarios). La acción requiere confirmación. Elimina la fila de `UserProfiles` y la entrada de `webpages_Membership`.

- [x] **Admin: Exportar Resumen de Pagos a Excel**
  `ResumenPagos` ya carga los datos de pagos por viaje en una grilla HTML. Agregar un botón "Exportar a Excel" que llame a una acción del controller que use EPPlus (ya disponible en el proyecto) para generar el archivo. No requiere nuevas dependencias.
  Archivos: `Controllers/Admin/AdminController.cs`, `Views/Admin/GridResumenPagos.cshtml`, `Infrastructure/AdminResumenPagosExcelExport.cs`
  Criterio de éxito: El botón descarga un .xlsx con los datos del viaje seleccionado. La acción tiene `[Authorize]` y `RequireAdministrador()`.

- [ ] **Arquitectura [P3 — Baja]: Eliminar NetTiers y capa generada asociada**
  **Contexto:** Hoy gran parte del acceso a datos y entidades proviene de plantillas NetTiers (`*generated*.cs`, `NetTiersProvider`, `DataRepository`, proyectos `MAT.Data`, `MAT.Data.SqlClient`, `MAT.Data.WebServiceClient`, bases `*ServiceBase.generated.cs` en `MAT.Services`, `*Base.generated.cs` en `MAT.Entities`, y posibles restos en `MAT.Web`). Objetivo final: **ninguna dependencia ni referencia a NetTiers**, y código muerto eliminado.
  **Restricción:** Épica de alto riesgo; **consultar al humano** antes de arrancar la primera fase (estrategia de reemplazo: SqlClient/SPs ya usados, repositorios manuales, u otra capa acordada). **No agregar paquetes NuGet** (p. ej. Dapper) sin aprobación explícita.
  **Enfoque por fases (marcar avance en PROGRESS):**
  1. **Inventario:** Listar referencias a `NetTiers`, `DataRepository`, providers Sql/Ws y usos de `*.generated.cs` desde `MAT.MVC`, `MAT.Services` (clases manuales), `MAT.Web`, `MAT.WCF`. Identificar proyectos/piezas realmente usados en el build de la solución principal vs. legado desreferenciado.
  2. **Sustitución incremental:** Por dominio o por proyecto, reemplazar llamadas a la capa NetTiers por el método acordado (p. ej. `DBHelper`/SqlCommand, servicios ya existentes sin generated, nuevos repositorios). Mantener contratos públicos de la app estables donde sea posible.
  3. **Poda:** Eliminar archivos `*.generated.cs` y bases generadas cuando ningún compilado los referencie; retirar proyectos o carpetas enteras si quedan huérfanos (`WebServiceClient`, etc., solo si el análisis confirma que no se usan).
  4. **Limpieza:** Quitar referencias de ensamblados, `using`, comentarios de plantilla `www.nettiers.com`, y actualizar `CLAUDE.md` / documentación de arquitectura para reflejar la nueva capa de datos.
  **Criterio de éxito global:** `MAT.sln` compila en Debug sin proyectos NetTiers en la cadena de uso de `MAT.MVC`; búsqueda en repo sin `NetTiers` ni `nettiers.com`; comportamiento funcional validado en pruebas manuales/regresión por módulos migrados. Si una fase no puede cerrarse sin decisión de diseño, documentar bloqueo en PROGRESS y pausar.

- [ ] **Arquitectura [P3 — Baja]: Retirar proyecto `MAT.Web` (legado Web Forms / NetTiers)**
  **Contexto:** `MAT.Web` sigue en `MAT.sln` y se compila, pero `CLAUDE.md` y `DOCUMENTACION\GUIA_SISTEMA_MAT.md` indican que **no hay `ProjectReference` desde `MAT.MVC` ni `MAT.Services`**. Aun así el host principal declara ensamblado y módulo en `MAT.MVC\Web.config` (`tagPrefix` → `MAT.Web.Data` / `MAT.Web.UI`, `EntityTransactionModule` → `MAT.Web.Data.EntityTransactionModule`). Antes de borrar nada hay que **confirmar uso cero** (vistas `.aspx`/ascx, controles `<data:...>`, scripts de despliegue, otros repos o IIS que referencien `MAT.Web.dll`).
  **Fases sugeridas:**
  1. **Inventario:** Búsqueda global `MAT.Web` / `assembly="MAT.Web"` en solution, configs y pipelines; verificar que ningún `.csproj` (salvo el propio) referencie el proyecto; listar dependencias internas (p. ej. solo NetTiers + Entities).
  2. **Desacople del host:** Si no hay uso real, eliminar de `Web.config` las entradas de `pages/controls` y `httpModules` (o equivalente en la sección migrada a `<system.webServer>`) que apunten a `MAT.Web`; validar arranque y una pasada por pantallas críticas.
  3. **Solución y carpeta:** Quitar `MAT.Web` de `MAT.sln` y build configurations; eliminar carpeta `MAT.Web\` del repo **o** archivar en rama/documentación según política del equipo; ajustar `CLAUDE.md` / guías que mencionen el proyecto.
  **Criterio de éxito:** MSBuild de `MAT.sln` (o al menos `MAT.MVC`) en Debug sin errores; aplicación corre en IIS Express; no quedan referencias rotas a `MAT.Web`; documentación alineada.

### Seguridad — correcciones obligatorias

- [x] **Admin: Agregar [Authorize] + RequireAdministrador() en acciones sin protección**
  Las siguientes acciones no tienen `[Authorize]` ni llaman a `RequireAdministrador()`, por lo que cualquier usuario autenticado puede acceder directamente a sus URLs:
  `ResumenPagos`, `ResumenPagosPorFecha`, `AuditoriaFacturas`, `GridResumenPagos`, `GridResumenPagosFecha`, `GridPlanillaHotelPrint`, `GridPlanillaHotelDetallePrint`, `ImprimirPlanilla`, `ImprimirPlanillaDetalle`, `EditarPlanilla`, y otras partials del controller. *(Acciones retiradas 2026-04-07: PlanillaServicios, PlanillasGeneradas, GridPlanillasGeneradas, PartialDropDownHotel, GridPlanillaHotel, wizard planilla en sesión.)*
  Agregar `[Authorize]` en el controller a nivel de clase o en cada acción faltante, y `var redir = RequireAdministrador(); if (redir != null) return redir;` en las que no lo tienen.
  Archivos: `Controllers/Admin/AdminController.cs`
  Criterio de éxito: Ninguna acción del AdminController es accesible sin rol Administrador. MSBuild pasa sin errores.

- [x] **Admin: Corregir exposición de e.Message en GridResumenPagosFecha**
  Línea 606 del controller: `ViewBag.Error = "Error: " + e.Message;` — viola la política del proyecto. Reemplazar con `ErrorUtil.LogAndGetPublicMessage`.
  Archivos: `Controllers/Admin/AdminController.cs` (línea ~606)
  Criterio de éxito: El catch usa `ErrorUtil.LogAndGetPublicMessage(e, "AdminController.GridResumenPagosFecha")`. No se expone el mensaje de excepción al usuario.

- [x] **Admin: Corregir fallback inseguro en IsAdminUser()**
  `IsAdminUser()` (línea ~1131) hace `return User.Identity.IsAuthenticated` si el sistema de roles lanza excepción. Esto significa que ante un error de configuración del RoleManager, cualquier usuario logueado pasa como administrador. Cambiar el fallback para retornar `false` en el catch.
  Archivos: `Controllers/Admin/AdminController.cs`
  Criterio de éxito: El catch de `IsAdminUser()` retorna `false`. Se agrega log del error antes de retornar.

### Seguridad — mejoras / deuda (pendientes)

- [x] **PersonaCliente: códigos de confirmación en BD (sustituir `PersonaClienteCode` y `PersonaClienteCode2` en `Web.config`)**
   **Contexto:** En `MAT.MVC\Web.config` existían `appSettings` `PersonaClienteCode` y `PersonaClienteCode2`. Se usan como **segunda capa** de autorización: el usuario ya autenticado debe ingresar uno de esos valores para ejecutar acciones sensibles. Hoy la comparación es contra `ConfigurationManager.AppSettings` en `PersonaClienteController`: acción **`EliminarVenta`** (POST `bool EliminarVenta(...)`) y **`EliminarPasajeroDeFactura`** (validación antes de llamar a `usp_MAT_Factura_EliminarPasajero`).

   **Problema:** Funciona operativamente, pero mantener códigos en configuración desplegable no es el enfoque más adecuado: cambios exigen redeploy o tocar config en cada entorno, no hay auditoría centralizada ni rotación clara desde negocio.

   **Objetivo:** Persistir la definición vigente (dos códigos activos: `Tinto.29` y `Martes.2025`, con posibilidad de agregar más) en **SQL Server**, con paridad en **`MAT.DB`** (SSDT) y script bajo `database\`. Leer valores vía helper `SistemaParametroHelper`; **no** exponer en logs ni en JSON el valor esperado ni el ingresado (mensajes genéricos al usuario). UI Admin para gestionar códigos.

   **Archivos modificados:**
   - `MAT.DB/dbo/Tables/SistemaParametro.sql` (nueva tabla)
   - `MAT.DB/dbo/Stored Procedures/usp_MAT_SistemaParametro_*.sql` (5 SPs)
   - `MAT.DB/MAT.DB.sqlproj` (agregados tabla y SPs)
   - `database/2026-04-10_SistemaParametro_Migration.sql` (script de migración)
   - `MAT.MVC/Models/SistemaParametroItem.cs` (nuevo modelo)
   - `MAT.MVC/Infrastructure/SistemaParametroHelper.cs` (helper con caché)
   - `MAT.MVC/Controllers/PersonaCliente/PersonaClienteController.cs` (validación vía helper)
   - `MAT.MVC/Controllers/Admin/AdminController.cs` (CRUD JSON + acción view)
   - `MAT.MVC/Views/Admin/SistemaParametros.cshtml` (nueva vista)
   - `MAT.MVC/Views/Admin/Index.cshtml` (enlace en panel Admin)
   - `MAT.MVC/MAT.MVC.csproj` (agregados archivos nuevos)
   - `MAT.MVC/Web.config` (eliminados appSettings `PersonaClienteCode` y `PersonaClienteCode2` — **no hacer commit con passwords reales**)

   **Criterio de éxito:** Las dos acciones anteriores validan contra origen BD; `MAT.DB` y documentación breve alineados; MSBuild limpio; prueba manual de rechazo/aceptación de código; `PROGRESS.md` actualizado.

---

## Criterios globales (aplican a todos los tasks)

- MSBuild sobre `MAT.MVC` debe pasar limpio al finalizar cada task
- Todo cambio de schema SQL debe reflejarse en `MAT.DB` (SSDT)
- No agregar paquetes NuGet sin consultar al humano
- Los contratos de entidades (`MAT.Entities`) e interfaces de servicio no se modifican sin consultar
- `PROGRESS.md` se actualiza al terminar cada task
