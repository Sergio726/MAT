# SPEC.md — Proyecto MAT

## Features completadas
(El agente completa esta sección a medida que avanza)

- [x] Setup inicial del proyecto — estructura de solución, CLAUDE.md, SPEC.md, PROGRESS.md

---

## Features pendientes

### P1 — Crítico / Deuda técnica

- [ ] **Actualizar vistas pendientes de modernización**
  Revisar `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md` y migrar las vistas listadas a Bootstrap 5 / jQuery 3, eliminando dependencias obsoletas identificadas en `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md`.
  Criterio de éxito: Las vistas actualizadas renderizan correctamente en IIS Express sin errores de consola JS; MSBuild pasa sin errores.

### P2 — Mejoras de producto

- [ ] **Admin: Migrar ErrorLog.cshtml y Logs.cshtml a Bootstrap 5**
  Ambas vistas usan Bootstrap 2 (`glyphicon`, `btn-default`, `btn-xs`, `form-inline`, `table-condensed`). Son las únicas vistas del panel Admin que quedaron sin migrar. Reemplazar con Bootstrap Icons y clases BS5. No cambiar la lógica JS de carga/filtrado.
  Archivos: `Views/Admin/ErrorLog.cshtml`, `Views/Admin/Logs.cshtml`
  Criterio de éxito: Las vistas renderizan correctamente en IIS Express sin glyphicons ni clases BS2. MSBuild pasa sin errores.

- [ ] **Admin: Gestión completa de roles en UsuarioEditar**
  El formulario `UsuarioEditar` solo permite asignar/quitar el rol "Administrador" mediante un checkbox. Otros roles existentes en el sistema (ej. "Vendedor") no pueden gestionarse desde la UI. Extender la vista y el controller para listar todos los roles del sistema como checkboxes y guardar los cambios. El controller debe leer los roles desde `Roles.GetAllRoles()` y aplicar add/remove por diferencia.
  Archivos: `Views/Admin/UsuarioEditar.cshtml`, `Controllers/Admin/AdminController.cs`, `Models/AdminUsuarioEditModel`
  Criterio de éxito: El formulario muestra todos los roles del sistema. Se pueden asignar y quitar roles arbitrarios. Las restricciones existentes (no quitarse admin a uno mismo, no eliminar último admin) se mantienen.

- [ ] **Admin: Exponer ErrorLog y Logs en el panel Index**
  Las vistas `/Admin/ErrorLog` y `/Admin/Logs` existen y funcionan pero no están enlazadas desde `Admin/Index.cshtml`. Solo quien conoce las URLs puede acceder. Agregar una sección "Diagnóstico" en el Index (similar a la sección "Herramientas de Desarrollo" de ADMINDEV) que las exponga. Evaluar si debe estar restringida a ADMINDEV o visible para todos los admins.
  Archivos: `Views/Admin/Index.cshtml`
  Criterio de éxito: El panel Admin muestra las tarjetas de ErrorLog y Logs. El acceso respeta la restricción de rol acordada.

- [ ] **Admin: Reemplazar confirm() de jQuery UI por modal Bootstrap 5 en Usuarios**
  El handler `js-admin-confirm-submit` en `Usuarios.cshtml` llama a una función `confirm()` que usa jQuery UI dialog. Esto crea dependencia mezclada (BS5 + jQuery UI) e inconsistencia visual. Reemplazar por un modal Bootstrap 5 reutilizable (puede ser el mismo patrón que ya usan otras vistas con `data-bs-toggle`).
  Archivos: `Views/Admin/Usuarios.cshtml`
  Criterio de éxito: La confirmación de deshabilitar usuario usa modal Bootstrap 5. No queda dependencia de jQuery UI dialog en esta vista.

- [ ] **Admin: Agregar DataTables y filtro de búsqueda en Usuarios**
  La tabla de usuarios (`Usuarios.cshtml`) es un `foreach` estático sin paginación ni búsqueda. Agregar DataTables con búsqueda por nombre de usuario y filtro por estado (Activo / Deshabilitado / Bloqueado). Seguir el patrón de otras vistas del proyecto.
  Archivos: `Views/Admin/Usuarios.cshtml`
  Criterio de éxito: La tabla permite buscar por usuario y filtrar por estado. MSBuild pasa sin errores.

- [ ] **Admin: Panel Index completo — agregar secciones Planillas y Sistema**
  El panel `Index.cshtml` tiene cards para Reportes y Usuarios, pero omite las secciones de Planillas (PlanillaServicios, PlanillasGeneradas) y Sistema (ErrorLog, Logs) que sí aparecen en el sidebar. El Index debería ser el punto de entrada completo, no solo una versión parcial del menú.
  Archivos: `Views/Admin/Index.cshtml`
  Criterio de éxito: El Index muestra cards para todas las secciones del sidebar. Sin cambios en el sidebar.

#### Reportes administrativos (misma BD que MAT Web; patrones MAT.MVC)

**Principios:** Implementar en el sistema actual usando `DBHelper`/SqlClient, `ErrorUtil`, `RequireAdministrador()`, vistas Admin Bootstrap 5 y **EPPlus** para Excel. Reutilizar **sin modificar** los SP existentes `usp_MAT_Reportes_Ventas`, `usp_MAT_Reportes_Pagos`, `usp_MAT_Reportes_RankingCompras`. Si el contrato columnas/parámetros no alcanza, crear **nuevos** SP en `MAT.DB` (nombre distinto, p. ej. sufijo `_Admin` o `_V2`) y dejar los originales intactos. Referencia funcional: documento de requisitos reportes MAT Web (paridad endpoints/filtros/DTO).

- [ ] **Reportes [P2 — Alta]: Inventario read-only de SP y definición de estrategia**
  Revisar en `MAT.DB` los tres SP anteriores: parámetros, columnas devueltas y tipos. Contrastar con el contrato deseado (filtros `from`/`to`/`viajeId`/opcionales, mapeo DTO). Decisión documentada: ¿llamar SP actuales desde C# solo con mapeo? ¿o alta de **nuevo** SP sin `ALTER` sobre los existentes?
  Criterio de éxito: Nota corta en `DOCUMENTACION` o comentario en PROGRESS; camino elegido acordado y sin cambios a los `.sql` de los SP actuales.

- [ ] **Reportes [P2 — Alta]: Helper de fechas y parámetros (reutilizable)**
  Centralizar validación: aceptar `from`/`to` en `DD-MM-YYYY` y `YYYY-MM-DD`; normalizar a `DD-MM-YYYY` para pasar al SP (mismo criterio que usa `HomeController` con los SP); vacíos → `NULL` en SQL; rango máximo **365 días** cuando hay rango; condición de ejecución: solo si (`from` **y** `to`) **o** `viajeId`; filtros rango vs viaje **mutuamente excluyentes** (comportamiento explícito: rechazar o ignorar uno — documentar en código). GUIDs opcionales parseados o `NULL`.
  Criterio de éxito: Una clase/helper testeable; sin duplicar lógica en tres acciones.

- [ ] **Reportes [P2 — Alta]: Mapeo de fila → DTO JSON (columnas heterogéneas)**
  Lectura **case-insensitive** de columnas (`FacturaId` vs `FacturaID`, etc.); renombres (`Descripcion` → `pagoDescripcion`, `FullName` → `clienteFullName`); `monedaTipo` siempre **string** (`"1"`, `"3"`); montos `decimal`; fechas desde string SP (`dd/MM/yyyy`, `dd-MM-yyyy`) o `DateTime` nativo; salida lista para serializar en **camelCase**.
  Criterio de éxito: Cubre las tres consultas sin ramas copiadas por reporte salvo composición mínima.

- [ ] **Reportes [P2 — Alta]: Nuevo SP solo si hace falta (sin tocar los actuales)**
  Si el inventario detecta gap irreparable sin `ALTER`: agregar en `MAT.DB` nuevo procedimiento (nombre nuevo), migración en `database\` con paridad SSDT, **sin modificar** los tres SP originales. Las acciones del controller deben apuntar al SP acordado (existente o nuevo).
  Criterio de éxito: Los `.sql` originales de reportes no cambian; si hay SP nuevo, build de `MAT.DB` y documentación del nombre usado.

- [ ] **Reportes [P2 — Alta]: `ReportesController` (o equivalente bajo `Controllers/Admin`) + seguridad**
  Todas las acciones con `[Authorize]` y `RequireAdministrador()` (mismo patrón que `AdminController`). Rutas GET claras para JSON y Excel. Errores con `ErrorUtil.LogAndGetPublicMessage`; respuestas JSON seguras (sin `e.Message`).
  Criterio de éxito: Usuario no administrador no puede ejecutar ni adivinar URL con éxito; MSBuild limpio.

- [ ] **Reportes [P2 — Alta]: Endpoint datos JSON — Ventas**
  Llamada a `usp_MAT_Reportes_Ventas` vía `DBHelper`/patrón existente; contrato de respuesta alineado al proyecto (`ok`/`success` + `data` según convención que defina el task al implementar, pero **consistente y documentado**); lista vacía si no hay criterio válido.
  Criterio de éxito: Mismo comportamiento de filtros que el spec MAT Web; compilación y prueba manual con admin.

- [ ] **Reportes [P2 — Alta]: Endpoint datos JSON — Pagos**
  Igual patrón con `usp_MAT_Reportes_Pagos`; parámetro opcional `tipoVentaId`.
  Criterio de éxito: Columnas y rename `pagoDescripcion`; `viaje` como texto.

- [ ] **Reportes [P2 — Alta]: Endpoint datos JSON — Ranking de compras**
  Igual patrón con `usp_MAT_Reportes_RankingCompras`.
  Criterio de éxito: Rankings y conteos expuestos en JSON correcto.

- [ ] **Reportes [P2 — Media]: Exportación Excel — Ventas (EPPlus)**
  Columnas en orden funcional acordado con el spec; cabecera con estilo (fondo azul, texto blanco, negrita); fechas formato es-AR; nombre archivo `ventas-YYYY-MM-DD.xlsx`; `Content-Type` correcto; mismos filtros que JSON.
  Criterio de éxito: Archivo válido; datos alineados al JSON del mismo filtro.

- [ ] **Reportes [P2 — Media]: Exportación Excel — Pagos (EPPlus)**
  Mismos estándares que ventas.
  Criterio de éxito: Columnas en orden esperado; MSBuild limpio.

- [ ] **Reportes [P2 — Media]: Exportación Excel — Ranking (EPPlus)**
  Mismos estándares que ventas.
  Criterio de éxito: Columnas en orden esperado.

- [ ] **Reportes [P2 — Media]: Vistas Admin — índice y navegación**
  Vista listado de los tres reportes (cards o lista) con enlaces; entrada desde `Views/Admin/Index.cshtml` y/o menú lateral si aplica, sin romper diseño existente.
  Criterio de éxito: Solo administrador ve y accede a la sección.

- [ ] **Reportes [P2 — Media]: Vista Admin — Reporte Ventas**
  Formulario filtros (rango fechas **o** viaje, excluyentes); opcionales vendedor/cliente reutilizando selects/autocomplete existentes en el proyecto si ya hay; tabla de resultados (patrón DataTables u otro ya usado en Admin); botón exportar Excel; consumo de endpoint JSON interno (jQuery o fetch según patrón del módulo).
  Criterio de éxito: Flujo completo sin errores de consola; BS5.

- [ ] **Reportes [P2 — Media]: Vista Admin — Reporte Pagos**
  Igual enfoque que ventas; filtros y export.
  Criterio de éxito: Incluye tipo de venta si se expone en UI.

- [ ] **Reportes [P2 — Media]: Vista Admin — Reporte Ranking compras**
  Igual enfoque; filtros y export.
  Criterio de éxito: Datos y export coherentes con JSON.

- [ ] **Reportes [P2 — Media]: Regresión — usos existentes de reportes**
  Verificar `HomeController` y cualquier otro consumo de `usp_MAT_Reportes_Ventas` u otros SP de reportes; tras extraer helpers/servicios, no romper totales ni popups existentes.
  Criterio de éxito: Estadísticas/home intactos en prueba manual.

- [ ] **Reportes [P3 — Baja]: UI comparación de períodos (tendencias)**
  Opcional: dos consultas seguidas al endpoint de ventas (periodo actual vs anterior) con fechas predefinidas (mes/mes, año/año, etc.) como en MAT Web, **sin** nuevo endpoint.
  Criterio de éxito: Documentado en pantalla o leyenda; sin lógica duplicada en backend.

- [ ] **Reportes [P3 — Baja]: Alias de rutas REST (opcional)**
  Si hace falta compatibilidad con clientes externos: registrar rutas tipo `reportes/ventas` en `RouteConfig` apuntando a las mismas acciones (GET).
  Criterio de éxito: Documentar URLs finales en `DOCUMENTACION`.

- [ ] **Reportes [P3 — Baja]: Documentación de operación**
  Archivo breve en `DOCUMENTACION`: URLs, parámetros, ejemplo JSON, nota sobre SP usados (existentes vs nuevos) y política de no modificar SP legacy.
  Criterio de éxito: Un desarrollador puede reproducir pruebas sin el archivo original de Downloads.

### P3 — Nuevas capacidades

- [ ] **Admin: Agregar acción de eliminar usuario**
  El sistema permite deshabilitar/habilitar/desbloquear usuarios pero no eliminarlos. Si se necesita borrar un usuario del sistema (membership + UserProfile), no hay UI ni acción en el controller. Implementar `UsuarioEliminar` con confirmación explícita. Restricciones: no puede eliminarse a sí mismo, no puede eliminar al último administrador.
  Archivos: `Controllers/Admin/AdminController.cs`, `Views/Admin/Usuarios.cshtml`
  Criterio de éxito: Existe botón "Eliminar" en el listado (solo visible para admins sobre otros usuarios). La acción requiere confirmación. Elimina la fila de `UserProfiles` y la entrada de `webpages_Membership`.

- [ ] **Admin: Exportar Resumen de Pagos a Excel**
  `ResumenPagos` ya carga los datos de pagos por viaje en una grilla HTML. Agregar un botón "Exportar a Excel" que llame a una acción del controller que use EPPlus (ya disponible en el proyecto) para generar el archivo. No requiere nuevas dependencias.
  Archivos: `Controllers/Admin/AdminController.cs`, `Views/Admin/GridResumenPagos.cshtml`
  Criterio de éxito: El botón descarga un .xlsx con los datos del viaje seleccionado. La acción tiene `[Authorize]` y `RequireAdministrador()`.

### Seguridad — correcciones obligatorias

- [ ] **Admin: Agregar [Authorize] + RequireAdministrador() en acciones sin protección**
  Las siguientes acciones no tienen `[Authorize]` ni llaman a `RequireAdministrador()`, por lo que cualquier usuario autenticado puede acceder directamente a sus URLs:
  `ResumenPagos`, `ResumenPagosPorFecha`, `AuditoriaFacturas`, `GridResumenPagos`, `GridResumenPagosFecha`, `PartialDropDownHotel`, `GridPlanillaHotel`, `GridPlanillaHotelPrint`, `GridPlanillaHotelDetallePrint`, `PlanillaServicios`, `PartialGridServiciosAdmin`, `PlanillasGeneradas`, `GridPlanillasGeneradas`, `ImprimirPlanilla`, `ImprimirPlanillaDetalle`, `EditarPlanilla`, y otras partials del controller.
  Agregar `[Authorize]` en el controller a nivel de clase o en cada acción faltante, y `var redir = RequireAdministrador(); if (redir != null) return redir;` en las que no lo tienen.
  Archivos: `Controllers/Admin/AdminController.cs`
  Criterio de éxito: Ninguna acción del AdminController es accesible sin rol Administrador. MSBuild pasa sin errores.

- [ ] **Admin: Corregir exposición de e.Message en GridResumenPagosFecha**
  Línea 606 del controller: `ViewBag.Error = "Error: " + e.Message;` — viola la política del proyecto. Reemplazar con `ErrorUtil.LogAndGetPublicMessage`.
  Archivos: `Controllers/Admin/AdminController.cs` (línea ~606)
  Criterio de éxito: El catch usa `ErrorUtil.LogAndGetPublicMessage(e, "AdminController.GridResumenPagosFecha")`. No se expone el mensaje de excepción al usuario.

- [ ] **Admin: Corregir fallback inseguro en IsAdminUser()**
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
