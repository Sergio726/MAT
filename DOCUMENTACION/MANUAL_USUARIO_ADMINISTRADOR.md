# Manual de usuario — Rol Administrador (MAT)

Documento orientado a usuarios con rol **Administrador** en la intranet MAT (Marco Antonio Tours). Describe qué puede hacer un administrador, cómo acceder a cada función y en qué se diferencia de un vendedor.

**Última actualización:** 2026-06-19  
**Basado en:** código de `MAT.MVC` (controllers Admin/Reportes, layouts, filtros de seguridad) y documentación en `DOCUMENTACION/`.

---

## 1. Resumen del rol

El **Administrador** es un usuario autenticado con el rol de membership **`Administrador`** (ASP.NET SimpleMembership / `Roles.IsUserInRole`).

| Ámbito | Qué incluye |
|--------|-------------|
| **Exclusivo admin** | Panel `/Admin/*`, reportes operativos, gestión de usuarios, códigos de confirmación, logs y auditoría de facturas |
| **Operativo compartido** | Todo el resto de la intranet (presupuestos, viajes, reservas, clientes, facturación, catálogos, etc.) con la misma autenticación `[Authorize]` que un vendedor |
| **Visión global** | En el **Home**, las estadísticas del mes muestran datos de **todos los vendedores**, no solo los propios |

Un administrador **no** tiene permisos especiales codificados en la mayoría de módulos operativos: comparte pantallas con los vendedores. Su valor agregado está en el **panel Admin**, la **visión consolidada** del dashboard y la **gestión del sistema**.

---

## 2. Acceso al sistema

### 2.1 Inicio de sesión

1. Ir a la pantalla de login (`/Account/Login`).
2. Ingresar usuario y contraseña.
3. Si el usuario tiene rol **Administrador**, el sistema redirige automáticamente a **`/Admin/Index`** (panel de administración), no al Home.

Los usuarios sin rol administrador van al destino habitual (`returnUrl` o Home).

### 2.2 Navegación entre Admin e intranet

| Desde | Cómo volver / ir |
|-------|------------------|
| Panel Admin | Menú de usuario (avatar en barra superior) → **Ir a la intranet** → `/Home/Index` |
| Intranet (Home) | URL directa **`/Admin/Index`** o volver a iniciar sesión (redirección automática) |

**Barra superior:** botón **Buscar** (atajo **Ctrl+K** o `/`) y **menú de usuario** (guía, volver al panel, intranet, cerrar sesión). En pantallas medianas/grandes también muestra el **título de la página actual**.

**Menú lateral (desktop):** secciones colapsables (Reportes, Usuarios, Sistema, Dev), estado activo con indicador, opción **Colapsar menú** al pie. En mobile, el menú hamburguesa abre un panel con navegación y acciones globales al pie.

**Nota:** El menú lateral principal (`_Layout.cshtml`) **no** incluye un enlace visible al panel Admin. Los administradores deben conocer la URL o usar la redirección post-login.

### 2.3 Seguridad de acceso al panel

Todas las acciones de `AdminController` y `ReportesController` están protegidas por el filtro **`[RequireAdministrator]`**:

- **Sin sesión:** redirección a login (vistas) o respuesta **401** (JSON/Excel).
- **Con sesión pero sin rol Administrador:** redirección a Home o respuesta **403** (JSON/Excel).

### 2.4 Guía de inicio (onboarding) y búsqueda

Al entrar por **primera vez** a `/Admin/Index`, el sistema muestra un **tour guiado** que resalta:

1. Encabezado del panel  
2. Sección Reportes y consultas  
3. Sección Usuarios  
4. Sistema y diagnóstico  
5. Botón **Buscar** en la barra superior  
6. **Menú de usuario** en la barra superior (guía, intranet, cerrar sesión)  

El tour incluye barra de progreso, posición adaptativa de la tarjeta según el elemento resaltado, y puede omitirse con **Escape**. Tras completar u omitir, el buscador del hub se muestra de forma compacta; use el botón **Buscar** de la barra superior o **Ctrl+K**.

El progreso se guarda en base de datos (`AdminUsuarioPreferencia`) por cuenta de usuario. Tras completar u omitir el tour, **no** vuelve a mostrarse automáticamente en visitas posteriores (incluso desde otro navegador).

| Acción | Cómo |
|--------|------|
| Volver a ver la guía | Menú de usuario → *Ver guía de inicio* (redirige al panel si está en otra pantalla Admin) |
| Buscar herramientas | Botón **Buscar** en la barra superior, atajo **Ctrl+K** (también `/` fuera de un campo de texto). En mobile también desde el menú hamburguesa → *Buscar funciones* |
| Omitir tour | Botón *Omitir tour* o tecla **Escape** |
| Estado del onboarding | API interna `GET /Admin/OnboardingStatus` |

---

## 3. Panel de administración (`/Admin/Index`)

Hub central con tarjetas agrupadas en cuatro áreas. El menú lateral (`_LayoutAdmin.cshtml` → `_AdminNav.cshtml`) replica las mismas entradas desde el catálogo centralizado `AdminHelpCatalog`. Nombres alineados: *Auditoría de facturas*, *Reportes operativos*, *Códigos de confirmación*, etc.

### 3.1 Reportes y consultas

| Función | Ruta | Descripción |
|---------|------|-------------|
| **Pagos por viaje** | `/Admin/ResumenPagos` | Buscar un viaje y ver el resumen de pagos asociados. Incluye grilla con totales. |
| **Exportar pagos (Excel)** | `/Admin/ResumenPagosExcel?ViajeID={guid}` | Descarga `.xlsx` del resumen del viaje seleccionado (EPPlus). |
| **Pagos por fecha** | `/Admin/ResumenPagosPorFecha` | Consultar pagos registrados en una fecha concreta. |
| **Auditoría de facturas** | `/Admin/AuditoriaFacturas` | Historial de acciones sobre facturas en un rango de fechas (SP `usp_MAT_Admin_AuditoriaFacturas`). Columnas: acción, descripción, fecha, cliente, vendedor. |
| **Reportes operativos** | `/Admin/Reportes` | Índice de tres reportes analíticos (ver sección 4). |

### 3.2 Usuarios

| Función | Ruta | Descripción |
|---------|------|-------------|
| **Listado de usuarios** | `/Admin/Usuarios` | Tabla con DataTables: id, usuario, roles, estado (activo / deshabilitado / bloqueado). Filtro por estado. |
| **Editar usuario** | `/Admin/UsuarioEditar/{id}` | Asignar o quitar **todos los roles** definidos en el sistema (`Roles.GetAllRoles()`). |
| **Restablecer contraseña** | `/Admin/UsuarioResetPassword/{id}` | Define una nueva contraseña para el usuario. |
| **Deshabilitar / habilitar** | POST `UsuarioEstablecerEstado` | Controla si el usuario puede iniciar sesión (`webpages_Membership.IsConfirmed`). |
| **Desbloquear** | POST `UsuarioDesbloquear` | Quita bloqueo por intentos fallidos de contraseña. |
| **Eliminar usuario** | POST `UsuarioEliminar` | Elimina membership, roles y perfil (con confirmación en UI). |
| **Registrar vendedor** | `/Admin/RegistrarVendedor` | Alta de cuenta + datos de persona/vendedor **sin cerrar sesión** del admin. |
| **Mi cuenta** | `/Admin/MiCuenta` | Cambiar la contraseña del usuario con el que inició sesión. |

#### Reglas de seguridad en gestión de usuarios

- No puede **eliminarse** ni **deshabilitarse** a sí mismo.
- No puede **quitarse** el rol Administrador a sí mismo.
- No puede **eliminar**, **deshabilitar** ni **quitar el rol Administrador** al **último** administrador del sistema.
- La eliminación de usuarios requiere confirmación mediante modal Bootstrap 5.

### 3.3 Sistema y diagnóstico

| Función | Ruta | Descripción |
|---------|------|-------------|
| **Error Log** | `/Admin/ErrorLog` | Errores persistidos en BD (`ErrorLog`): fecha, correlation ID, tipo, mensaje, stack, URL, usuario. Vistas Tabla y Diagnóstico. Filtros por correlation ID y fecha. API: `/Admin/ErrorLogJson`. |
| **Log en memoria** | `/Admin/Logs` | Últimas ~1000 entradas del logger en memoria/archivo de proceso. Filtro por correlation ID. Exportar texto: `/Admin/LogsRaw`. |
| **Códigos de confirmación** | `/Admin/SistemaParametros` | CRUD de parámetros del sistema; claves `CodigoConfirmacion_*` activas autorizan acciones destructivas en ventas (ver 3.4). |

### 3.4 Códigos de confirmación (parámetros del sistema)

Los administradores gestionan códigos en **`/Admin/SistemaParametros`**:

- **Crear** código nuevo (clave tipo `CodigoConfirmacion_N`, valor secreto, descripción).
- **Editar** valor y descripción.
- **Activar / desactivar** sin borrar el registro.

Estos códigos los **utilizan todos los usuarios autenticados** (no solo admins) al:

- **Eliminar una venta / factura** (`PersonaCliente/EliminarVenta`).
- **Eliminar un pasajero de una factura** (`PersonaCliente/EliminarPasajeroDeFactura`).

La validación ocurre en servidor vía `SistemaParametroHelper.ValidarCodigo`. Solo los administradores pueden **administrar** la lista de códigos válidos.

---

## 4. Reportes operativos (`/Admin/Reportes`)

Índice: `/Admin/Reportes`. Documentación técnica ampliada: `DOCUMENTACION/REPORTES_MAT_MVC_OPERACION.md` y `DOCUMENTACION/REPORTES_MAT_WEB.md`.

### 4.1 Reporte de ventas

- **Pantalla:** `/Admin/Reportes/ReporteVentas`
- **Datos:** `/Admin/Reportes/Ventas`
- **Excel:** `/Admin/Reportes/VentasExcel`

**Filtros (mutuamente excluyentes):**

- Rango de fechas `from` / `to` (`DD-MM-YYYY` o `YYYY-MM-DD`), máximo **365 días**, **o**
- Un **viaje** (`viajeId` GUID), con autocompletado `/Admin/Reportes/BuscarViajes`.

Opcionales: `vendedorId`, `clienteId`.

**Contenido:** facturas, clientes, vendedores, viajes, montos facturados, pagado y saldo. Cards resumen, rankings y modal de detalle de facturas en la UI.

### 4.2 Reporte de pagos

- **Pantalla:** `/Admin/Reportes/ReportePagos`
- **Datos:** `/Admin/Reportes/Pagos`
- **Excel:** `/Admin/Reportes/PagosExcel`

Mismas reglas de fechas vs viaje. Además: filtro opcional **`tipoVentaId`** (valores 1 o 2).

### 4.3 Ranking de compras

- **Pantalla:** `/Admin/Reportes/ReporteRanking`
- **Datos:** `/Admin/Reportes/Ranking`

Ranking de clientes y viajes según volumen de compras (SP `usp_MAT_Reportes_RankingCompras_V2`). Cards resumen en cliente. **Exportación:** PDF generado en el navegador (no hay Excel en servidor para ranking).

### 4.4 Alias de compatibilidad (API)

Equivalentes REST bajo `/reportes/...` (misma seguridad y datos):

- `/reportes/ventas`, `/reportes/ventas/excel`
- `/reportes/pagos`, `/reportes/pagos/excel`
- `/reportes/ranking-compras`

---

## 5. Funcionalidades operativas (intranet compartida)

Además del panel Admin, un administrador autenticado accede a **todos los módulos operativos** de la intranet. No hay comprobación de rol Administrador en estos controllers; basta con `[Authorize]`.

### 5.1 Menú lateral principal

| Módulo | Accesos principales |
|--------|---------------------|
| **Inicio** | `/Home/Index` — dashboard y accesos rápidos |
| **Presupuestos** | Listado, Seguimiento, Métricas |
| **Facturación fiscal** | Listado, Nueva compra/venta, Proveedores |
| **Clientes** | Alta, Listado |
| **Hoteles** | ABM, Listado |
| **Transportes** | Listado |
| **Servicios** | Alta, Listado |
| **Excursiones** | Alta, Listado |
| **Butacas (precios)** | Ver o agregar |
| **Adicionales** | Alta, Listado |
| **Cotizador** | Modal desde menú |

### 5.2 Accesos rápidos en Home

Desde `/Home/Index`:

- Módulo de presupuestos  
- Gestión de viajes (`/Viaje/Index`)  
- Viajes por fecha  
- Todos los viajes  
- Gestión de paquetes  
- Clientes  
- Búsqueda de facturas  
- Notas de crédito  

### 5.3 Otros módulos operativos (sin ítem dedicado en menú lateral)

Accesibles por URL, flujos desde viajes/reservas o accesos rápidos:

- **Reservas y nueva reserva** (`ReservaController`, `NuevaReservaController`)
- **Pasajeros, pagos, cuenta corriente** (`PersonaClienteController`, `PagoController`, `CuentaCorrienteController`)
- **Facturas** (`FacturaController`, `FacturaFiscalController`)
- **Notas de crédito** (`NotaCreditoController`)
- **Viajes, paquetes, itinerarios, hoteles en viaje** (`ViajeController`, `PaqueteController`, etc.)
- **Búsqueda global** (`BusquedaController`)
- **Herramientas** (`HerramientasController`)

### 5.4 Dashboard — diferencia clave para administradores

En `/Home/Index`, si el usuario es administrador:

- Banner: *"Vista Administrador - Mostrando datos de todos los vendedores"*.
- **Presupuestos del mes:** todos los vendedores.
- **Clientes nuevos:** todos (`@VendedorId` nulo en consultas).
- **Ventas del mes:** consolidado global (`usp_MAT_Reportes_Ventas` sin filtro de vendedor).

Los vendedores ven solo **sus** métricas (filtro por `MATContext.CurrentVendedor`).

---

## 6. Limitaciones importantes

### 6.1 Cuenta administrador sin registro de vendedor

`MATContext.CurrentVendedor` obtiene el vendedor vinculado a la **Persona** del usuario logueado. Si el administrador **no** tiene persona/vendedor asociado, `CurrentVendedor` es **null** y fallan operaciones que lo exigen, por ejemplo:

- Crear presupuestos  
- Registrar pagos y reservas que graban `VendedorId`  
- Varias acciones en `PersonaCliente`, `Viaje`, `Hotel`, etc.

**Recomendación:** las cuentas administrativas que deban operar ventas/reservas deben registrarse también como vendedor (p. ej. con **Registrar vendedor** o vínculo persona–usuario en BD).

### 6.2 Rol vs. usuario ADMINDEV

La cuenta cuyo **nombre de usuario** es exactamente **`ADMINDEV`** (comparación insensible a mayúsculas) ve herramientas extra:

| Ubicación | Función |
|-----------|---------|
| Panel Admin → sección "Herramientas de Desarrollo" | **Historial de pagos** → `/Home/HistorialPagos` |
| Home → acceso rápido | Misma pantalla de historial |

Esto **no** depende del rol Administrador sino del **nombre de usuario**. Cualquier otro admin no ve esas entradas.

### 6.3 Módulos retirados del panel Admin

Desde **2026-04 / 2026-06** se eliminó del menú Admin el submódulo de **Planillas** (`PlanillaServicios`, `PlanillasGeneradas`, impresión/edición asociada). Las rutas `/Admin/PlanillaServicios` y `/Admin/PlanillasGeneradas` **ya no existen** en el producto actual.

---

## 7. Matriz de permisos (referencia rápida)

| Acción | Administrador | Vendedor |
|--------|:-------------:|:--------:|
| Panel `/Admin/*` | ✅ | ❌ |
| Reportes operativos (ventas/pagos/ranking) | ✅ | ❌ |
| Resumen pagos / auditoría facturas (Admin) | ✅ | ❌ |
| Gestión de usuarios y roles | ✅ | ❌ |
| Códigos de confirmación (ABM) | ✅ | ❌ |
| Error Log / Logs en memoria | ✅ | ❌ |
| Dashboard Home (todos los vendedores) | ✅ | ❌ (solo propios) |
| Módulos operativos intranet | ✅* | ✅* |
| Usar código de confirmación para borrar venta/pasajero | ✅ | ✅ |
| Historial pagos (`ADMINDEV`) | Solo usuario ADMINDEV | ❌ |

\*Requiere sesión autenticada; operaciones con `CurrentVendedor` requieren vínculo persona–vendedor.

---

## 8. Roles en el sistema

Los roles se gestionan con **ASP.NET Roles** (`webpages_Roles`, `webpages_UsersInRoles`). El rol principal documentado es:

- **`Administrador`** — acceso al panel Admin y reportes.

En **Editar usuario**, el admin puede asignar **cualquier rol** devuelto por `Roles.GetAllRoles()` (p. ej. **Vendedor**, si existe en la base de membership). Los roles concretos dependen de la configuración desplegada en cada entorno.

---

## 9. Referencias técnicas

| Tema | Archivo |
|------|---------|
| Arquitectura general | `CLAUDE.md`, `DOCUMENTACION/GUIA_SISTEMA_MAT.md` |
| Reportes — operación | `DOCUMENTACION/REPORTES_MAT_MVC_OPERACION.md` |
| Reportes — contrato / filtros | `DOCUMENTACION/REPORTES_MAT_WEB.md` |
| Seguridad Admin | `MAT.MVC/Filters/RequireAdministratorAttribute.cs`, `Infrastructure/AdminAuthorizationHelper.cs` |
| Controllers | `MAT.MVC/Controllers/Admin/AdminController.cs`, `ReportesController.cs` |
| Vistas panel | `MAT.MVC/Views/Admin/`, `MAT.MVC/Views/Reportes/` |

---

## 10. Glosario breve

| Término | Significado |
|---------|-------------|
| **Correlation ID** | Identificador de trazabilidad por request; enlaza logs en memoria, archivo y tabla `ErrorLog`. |
| **Código de confirmación** | Secreto configurado por el admin para autorizar eliminaciones sensibles. |
| **SimpleMembership** | Sistema de usuarios/contraseñas y roles usado por MAT.MVC. |
| **EPPlus** | Librería usada para exportaciones Excel desde Admin. |

---

*Este manual refleja el estado del código en la rama de trabajo activa (MAT2026). Ante discrepancias, prevalece el comportamiento desplegado en el entorno y el código fuente citado.*
