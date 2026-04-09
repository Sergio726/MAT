# SPEC.md — Proyecto MAT

## Features completadas
(El agente completa esta sección a medida que avanza)

- [x] Setup inicial del proyecto — estructura de solución, CLAUDE.md, SPEC.md, PROGRESS.md
- [x] Admin — eliminar usuario (`UsuarioEliminar`, confirmación BS5, restricciones auto-eliminación y último administrador)
- [x] Admin — exportar resumen de pagos por viaje a Excel (EPPlus, `ResumenPagosExcel`)

---

## Features pendientes

### P1 — Crítico / Deuda técnica

- [ ] **Actualizar vistas pendientes de modernización**
  Revisar `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md` y migrar las vistas listadas a Bootstrap 5 / jQuery 3, eliminando dependencias obsoletas identificadas en `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md`.
  Criterio de éxito: Las vistas actualizadas renderizan correctamente en IIS Express sin errores de consola JS; MSBuild pasa sin errores.
  **Iteración 2026-04-08 (lote Fase 6 — Reserva):** `SearchModel` / `QuickSearch` con `GridView` estilizado BS5 y contenedor `table-responsive`; `PartialVinculacionMenor` usa modal Bootstrap 5 para confirmar desvinculación (sin `$.dialog` en ese flujo); demás ítems Reserva del inventario marcados ✅ en el doc (auditoría + criterio actual). Siguen ⏳ otras sublistas (PersonaCliente, PasajeroViaje, Admin servicios, Splash, etc.).

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

- [ ] **Reportes (mejora futura): Tests unitarios `ReportesQueryHelper`**
  Casos: feliz, rango mayor a 365 días, conflicto fechas+viaje, `from` sin `to`, `tipoVentaId` inválido.
  Criterio de éxito: Suite verde en el pipeline acordado.

- [x] **Reportes (mejora futura): DataTables i18n sin CDN**
  Evitar `cdn.datatables.net` para `Spanish.json` (archivo local o bundle).
  Criterio de éxito: Idioma cargado sin Internet.

#### Reportes — UX/UI / diseño datepicker

- [x] **Reportes UX: Corregir diseño de datepickers en vistas de reportes**
  Los botones de navegación `‹` / `›` del calendario jQuery UI se renderizaban incorrectamente en `ReporteVentas.cshtml`: las entidades HTML `&#x3C;` y `&#x3E;` dentro del bloque `<script>` no son decodificadas por JavaScript y aparecían como texto literal en el header del popup. Adicionalmente, el CSS en `admin.modern.css` exponía el texto interno del span (al resetear `text-indent` a 0 y usar `overflow: visible`), solapándolo con el `::after` que renderiza la flecha real. Se unificó también la configuración del datepicker de Ventas con los de Pagos y Ranking (`yearRange`, `maxDate`, `showButtonPanel`, `showOtherMonths`).
  Archivos: `Views/Reportes/ReporteVentas.cshtml`, `Content/admin.modern.css`
  Criterio de éxito: Los tres datepickers muestran `‹` y `›` correctamente; configuración de rango de años y opciones de panel coherente entre los tres reportes.

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

---

## Criterios globales (aplican a todos los tasks)

- MSBuild sobre `MAT.MVC` debe pasar limpio al finalizar cada task
- Todo cambio de schema SQL debe reflejarse en `MAT.DB` (SSDT)
- No agregar paquetes NuGet sin consultar al humano
- Los contratos de entidades (`MAT.Entities`) e interfaces de servicio no se modifican sin consultar
- `PROGRESS.md` se actualiza al terminar cada task
