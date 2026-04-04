# Guía del sistema MAT

Documento de contexto técnico del repositorio: arquitectura, stack, convenciones, base de datos, flujo de trabajo y precauciones. Basado en la solución, `Web.config`, proyectos `.csproj` y la documentación existente en esta carpeta.

---

## 1. Nombre y propósito del sistema

**MAT** es una **aplicación web de intranet** para la gestión operativa de una **agencia de viajes / turismo**: reservas y pre-reservas, viajes, pasajeros, paquetes, hoteles y habitaciones, transporte, excursiones, precios, clientes, vendedores, proveedores, facturación y notas, cuenta corriente, pagos, etc. Los controladores en `MAT.MVC\Controllers\` (por ejemplo `ReservaController`, `ViajeController`, `PaqueteController`, `FacturaController`, `PresupuestoController`) y el esquema descrito en `DOCUMENTACION\MAT_DB.md` (tablas como `Reserva`, `Viaje`, `Pasaje`, `Factura`, `Cliente`, `Persona`, …) confirman ese dominio.

En la línea de producto reciente aparece un **módulo de presupuestos con seguimiento** (códigos tipo `MAT-YYYYMMDD-XXXX`, expiración, WhatsApp), documentado en `DOCUMENTACION\RESUMEN_EJECUTIVO_MAT2026.md` y con lógica en `Global.asax.cs` (`PresupuestoMethod.MarcarExpirados`, caché horaria).

---

## 2. Arquitectura por proyecto

| Proyecto | En `MAT.sln` | Responsabilidad |
|----------|----------------|-----------------|
| **MAT.MVC** | Sí | **Host principal**: ASP.NET MVC 4 sobre .NET 4.8, vistas Razor, Web API, bundles, autenticación (`WebMatrix.WebData` / roles), integración con API externa (`BackendAPI_URL` en `Web.config`), AutoMapper en `Global.asax.cs`, infraestructura en carpetas como `MAT.MVC\Infrastructure\`, `Integration\`. |
| **MAT.Web** | Sí | Biblioteca **NetTiers / Web Forms**: controles `EntityGridView`, `EntityDropDownList`, `DataSourceControls\*DataSource`, repeaters. **No hay `ProjectReference` desde `MAT.MVC` ni `MAT.Services`** en los `.csproj` actuales: queda como **legado / reutilizable** si algún front antiguo la usara, pero el MVC no la enlaza. |
| **MAT.Data** | Sí | Capa de **acceso a datos estilo NetTiers**: `DataRepository`, `NetTiersProvider`, configuración `<MAT.Data>` en `Web.config`, contratos de providers. |
| **MAT.Data.SqlClient** | Sí | **Implementación SQL Server** del provider (`SqlNetTiersProvider` referenciado en `MAT.MVC\Web.config`). |
| **MAT.Services** | Sí | **Servicios de dominio** generados + parciales: `*Service` / `*ServiceBase` heredan de `ServiceBase<TEntity, TKey>` (`MAT.Services\ServiceBase.cs`), encapsulan operaciones sobre entidades vía `MAT.Data`. |
| **MAT.Entities** | Sí | **Entidades** del modelo (POCO + keys, factory `EntityFactory` citada en config). |
| **MAT.Enums** | Sí | **Enumeraciones** compartidas del dominio. |
| **MAT.Utilities** | Sí | Utilidades transversales (p. ej. logging usado desde MVC según reglas en `.cursor\rules`). |
| **MAT.DB** | Sí | Proyecto **SSDT** (`MAT.DB\MAT.DB.sqlproj`): **fuente de verdad del esquema** SQL Server (tablas, SPs, vistas, etc.). |
| **MAT.WCF** | **No** está en `MAT.sln` | Servicio WCF **InfoPath**: `IInfopathService`, `InfopathService.svc` — expone `GetViaje` / `GetAllViajes` usando `MAT.Entities` y `MAT.MVC.Models` (según `MAT.WCF\IInfopathService.cs`). Proyecto aparte para integración legacy. |
| **MAT.Data.WebServiceClient** | **No** está en `MAT.sln` | Clientes **Ws*Provider** generados (`WsNetTiersProvider`, `WsFacturaProvider`, …) para consumir datos vía **servicios web** en lugar de SQL directo; patrón alternativo al `SqlNetTiersProvider`. |

---

## 3. Stack tecnológico

- **.NET**: **4.8** (`TargetFrameworkVersion` en `MAT.MVC\MAT.MVC.csproj`, `MAT.Data`, `MAT.Services`, etc.).
- **Web**: **ASP.NET MVC 4**, **Web API 4**, **Razor 2**, **Web Optimization** (`MAT.MVC\packages.config`).
- **ORM**: **Entity Framework 5** está referenciado y hay sección `entityFramework` en `Web.config`, pero **`appSettings` fuerza `UseEntityFramework` = `NO`** — el camino activo es **NetTiers + ADO.NET/SqlClient** vía `MAT.Data` / `MAT.Data.SqlClient`.
- **Mapeo**: **AutoMapper 6.0.2**.
- **Excel**: **EPPlus 4.5.3.3**.
- **HTTP / DI moderno (parcial)**: **Microsoft.Extensions.Http / DependencyInjection / Logging 6.0.0** (conviven con el stack clásico).
- **Enterprise Library 5** (caching, data, logging, etc.): DLLs en `References\` y `Web.config` (`enterpriseLibrary.ConfigurationSource`).
- **Auth social legacy**: **DotNetOpenAuth** (paquetes en `packages.config`).
- **Front (estado en repo)**: `packages.config` aún lista **jQuery 1.8.2**, **jQuery UI 1.8.24**; `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md` y el resumen ejecutivo hablan de **actualización a jQuery 3 / Bootstrap 5** en la rama de trabajo — puede haber divergencia entre documentación y `packages.config` según commit.
- **Base de datos**: **SQL Server** (`System.Data.SqlClient`, proyecto SSDT **Sql160** según `MAT_DB.md`).

---

## 4. Convenciones de código

- **Namespaces y carpetas MVC**: controladores agrupados por dominio bajo `MAT.MVC\Controllers\<Dominio>\<Dominio>Controller.cs` (ej. `Controllers\Reserva\ReservaController.cs`).
- **Patrón de datos**: **NetTiers** — `DataRepository` + **providers** (`MAT.Data\DataRepository.cs`), no un “Repository” manual único al estilo DDD moderno.
- **Capa de negocio**: servicios **`NombreEntidadService`** que extienden bases generadas **`NombreEntidadServiceBase`** (`MAT.Services\FacturaService.cs`, etc.).
- **Entidades**: `MAT.Entities` con **`*Key`** para claves.
- **Errores en controladores**: la regla del repo (`.cursor\rules\error-handling.mdc`) exige **`ErrorUtil.LogAndGetPublicMessage`** (`MAT.MVC\Infrastructure`) con contexto `"Controller.Action"`, integración con `MATLogger` y `ErrorLog` en BD.
- **No hay proyecto de tests** `*Test*.csproj` en el repositorio buscado.

---

## 5. Base de datos

- **Motor**: **SQL Server** (connection strings con `System.Data.SqlClient`).
- **Conexión**: en **`MAT.MVC\Web.config`** → `<connectionStrings>`:
  - **`MAT.Data.ConnectionString`**: base principal de negocio (catálogo de intranet, p. ej. entornos `MAT_DEV` o `MAT` según configuración).
  - **`MAT.Session.ConnectionString`**: base de sesión / membership.
- **Provider de datos**: sección **`<MAT.Data defaultProvider="SqlNetTiersProvider">`** con `connectionStringName="MAT.Data.ConnectionString"`, `useStoredProcedure="false"` (SQL dinámico/parametrizado según generación, no obligatorio SP para todo).
- **Cambios de esquema**: **`MAT.DB`** (SSDT) es la **única fuente de verdad**; flujo documentado en `DOCUMENTACION\MAT_DB.md` (compilar, publicar, Schema Compare). Scripts sueltos en **`database\`** pueden ser migraciones puntuales pero deben **replicarse en MAT.DB**.
- **SPs**: el proyecto `MAT.DB` incluye **`dbo\Stored Procedures`** (convención `usp_MAT_*` mencionada en la doc).

**Seguridad**: no versionar credenciales reales en `Web.config`; usar secretos locales o transformaciones de publicación fuera del control de código compartido.

---

## 6. Flujo de trabajo (Git y ambientes)

**Ramas típicas** (ajustar según `git branch -a` en el clon): `MAT2026`, `MASTER`, `develop`, y varias `feature/*`, `fixDependencies`, `refactor/*`; remotos pueden incluir `origin/MAT2026`, `origin/MASTER`, `origin/develop`, etc.

**Despliegue / ambientes**: el repo **no define** necesariamente pipelines visibles en todos los clones; lo inferible es:

- **`SystemDEV`** en `appSettings` (`Web.config`) como bandera de entorno de desarrollo.
- Nombres de base **`MAT_DEV.*`** vs comentarios con **`MAT.Intranet` / `MAT.Session`** sugieren **DEV vs otros** por catálogo/servidor.
- **MAT.WCF** tiene `Properties\PublishProfiles\MAT.WCF.pubxml` (publicación manual típica de Visual Studio).

---

## 7. Áreas críticas o sensibles

- **`MAT.MVC\Web.config`**: connection strings, `BackendAPI_URL`, timeouts, claves de negocio (`PersonaClienteCode`, presupuesto, WhatsApp).
- **`MAT.DB`**: cualquier cambio afecta **toda** la aplicación y despliegues; publicar contra la BD equivocada es destructivo.
- **`MAT.Data` / `MAT.Data.SqlClient` / archivos `*.generated.cs`**: regenerarlos o romper el contrato del provider **impacta todos los servicios**.
- **`MAT.Services\ServiceBase*` y servicios generados**: capa central de persistencia/consistencia.
- **Autenticación y roles**: `AccountController`, `WebSecurity`, tablas de membership asociadas a `MAT.Session`.
- **Facturación / fiscal**: `FacturaController`, `FacturaFiscalController`, `NotaCreditoController`, auditoría (`AuditFactura*` en `MAT.Web` y entidades relacionadas).
- **Presupuestos**: jobs en `Application_Start` / `Application_BeginRequest` en `Global.asax.cs` (efectos en cada arranque o tráfico).

---

## 8. Estado actual del proyecto y deuda técnica

- **Línea activa de evolución**: documentación ene-2026 describe rama **`MAT2026`** con **modernización UI**, **módulo presupuesto/seguimiento**, limpieza/refactor y mención de **eliminación de capa NetTiers** en algún commit (verificar en el historial: en el código pueden seguir existiendo `DataRepository` / `SqlNetTiersProvider`).
- **Deuda / riesgos explícitos en repo**:
  - Librerías JS/CSS obsoletas documentadas en `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md`.
  - **EF instalado pero deshabilitado** por configuración (`UseEntityFramework` = `NO`).
  - **MAT.Web** y **MAT.Data.WebServiceClient** / **MAT.WCF** como **código legacy** o paralelo, no integrados en la solución principal en el caso de WCF/WebServiceClient.
  - **Sin tests automatizados** de proyecto visibles en la búsqueda habitual.
- **Vistas pendientes**: `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md`.

---

## 9. Comandos importantes

- **Compilar la solución** (Visual Studio o MSBuild), ejemplo:

  ```text
  "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" MAT.sln /t:Build /p:Configuration=Release
  ```

  Ajustar la ruta si la instalación de Visual Studio es otra (por ejemplo versión “18” / Community).

- **Levantar localmente**: abrir **`MAT.sln`**, proyecto de inicio **`MAT.MVC`**, **IIS Express** (según `.csproj`: `UseIISExpress` true). Ajustar **`Web.config`** a la instancia SQL local o de desarrollo.

- **Base de datos**: publicar **`MAT.DB`** contra el servidor correcto (ver `DOCUMENTACION\MAT_DB.md`).

- **Tests**: no hay comando estándar de tests en la solución actual.

**Regla de equipo**: antes de un push a git, compilar el proyecto y verificar que no haya errores de compilación.

---

## 10. Lo que NO se debe hacer

- **No commitear** `Web.config` con **passwords reales** de producción; rotar credenciales si ya se expusieron.
- **No aplicar solo scripts** a SQL sin actualizar **`MAT.DB`** (regla explícita en `MAT_DB.md`).
- **No exponer** `Exception.Message` al usuario en controladores; usar **`ErrorUtil.LogAndGetPublicMessage`** (regla del repo en `.cursor\rules\error-handling.mdc`).
- **No editar a mano** masivamente archivos **`.generated.cs`** salvo que sepas regenerar desde la herramienta NetTiers/original — se pierde en el siguiente codegen.
- **No asumir** que **Entity Framework** está en uso: está **desactivado** por `appSettings`.
- **No mezclar** ramas sin alinear política del equipo: coexisten **`MASTER`**, **`develop`**, **`MAT2026`**; conviene acordar cuál es la rama de integración.
- **Cuidado** al tocar **`Application_BeginRequest`** en `Global.asax.cs`: lógica ahí corre en **cada request** (presupuestos expirados + correlation id).

---

## Referencias en este repositorio

- `DOCUMENTACION\README.md` — índice de documentación.
- `DOCUMENTACION\MAT_DB.md` — proyecto SSDT y flujo de esquema.
- `DOCUMENTACION\RESUMEN_EJECUTIVO_MAT2026.md` — resumen de evolución y módulo presupuesto.
- `MAT.sln` — proyectos incluidos en la solución principal.

---

*Última actualización del contenido: alineado con el análisis del repositorio MAT (año 2026). Ajustar ramas, rutas de MSBuild y detalles de despliegue según el entorno del equipo.*
