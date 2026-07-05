# Guía del sistema MAT

Documento de contexto técnico del repositorio: arquitectura, stack, convenciones, base de datos, flujo de trabajo y precauciones. Basado en la solución, `Web.config`, proyectos `.csproj` y la documentación existente en esta carpeta.

---

## 1. Nombre y propósito del sistema

**MAT** es una **aplicación web de intranet** para la gestión operativa de una **agencia de viajes / turismo**: reservas y pre-reservas, viajes, pasajeros, paquetes, hoteles y habitaciones, transporte, excursiones, precios, clientes, vendedores, proveedores, facturación y notas, cuenta corriente, pagos, etc. Los controladores en `MAT.MVC\Controllers\` (por ejemplo `ReservaController`, `ViajeController`, `PaqueteController`, `FacturaController`, `PresupuestoController`) y el esquema descrito en `DOCUMENTACION\MAT_DB.md` (tablas como `Reserva`, `Viaje`, `Pasaje`, `Factura`, `Cliente`, `Persona`, …) confirman ese dominio.

En la línea de producto reciente aparece un **módulo de presupuestos con seguimiento** (códigos tipo `MAT-YYYYMMDD-XXXX`, expiración, WhatsApp), documentado en `DOCUMENTACION\RESUMEN_EJECUTIVO_MAT2026.md` y con lógica en `Global.asax.cs` (`PresupuestoMethod.MarcarExpirados`, caché horaria).

---

## 2. Arquitectura por proyecto

> **Actualización 2026-07-05 (NetTiers F0–F12):** Épica NetTiers cerrada. Eliminados `MAT.Services`, `MAT.Data`, `MAT.Web`, `MAT.WCF`; `MAT.Entities` son POCOs manuales (sin `*.generated.cs`). Acceso a datos: **`DBHelper` + SP** + `*DataAccess`.

| Proyecto | En `MAT.sln` | Responsabilidad |
|----------|----------------|-----------------|
| **MAT.MVC** | Sí | **Host principal**: ASP.NET MVC 4 sobre .NET 4.8, vistas Razor, Web API, bundles, autenticación (`WebMatrix.WebData` / roles), integración con API externa (`BackendAPI_URL` en `Web.config`), AutoMapper en `Global.asax.cs`, infraestructura en `MAT.MVC\Infrastructure\`, `Integration\`. |
| ~~**MAT.Web**~~ | ~~Eliminado F9~~ | ~~Biblioteca NetTiers / Web Forms legacy~~ |
| ~~**MAT.Data**~~ | ~~Eliminado F10~~ | ~~Capa NetTiers: DataRepository, NetTiersProvider~~ |
| ~~**MAT.Data.SqlClient**~~ | ~~Eliminado F10~~ | ~~SqlNetTiersProvider~~ |
| ~~**MAT.Services**~~ | ~~Eliminado F9~~ | ~~Servicios *Service / *ServiceBase~~ |
| **MAT.Entities** | Sí | **POCOs manuales** (F11, 2026-07-05) — sin `*.generated.cs`. |
| **MAT.Enums** | Sí | **Enumeraciones** compartidas del dominio. |
| **MAT.Utilities** | Sí | **DBHelper**, `GeoDataAccess`, `LookupDataAccess`, `Helper.cs` (dropdowns), logging. |
| **MAT.DB** | Sí | Proyecto **SSDT** (`MAT.DB\MAT.DB.sqlproj`): **fuente de verdad del esquema** SQL Server (tablas, SPs, vistas, etc.). |
| ~~**MAT.WCF**~~ | ~~Eliminado F9~~ | ~~Servicio WCF InfoPath legacy~~ |
| ~~**MAT.Data.WebServiceClient**~~ | ~~Eliminado F10~~ | ~~Clientes Ws*Provider~~ |

---

## 3. Stack tecnológico

- **.NET**: **4.8** (`TargetFrameworkVersion` en `MAT.MVC\MAT.MVC.csproj`, etc.).
- **Web**: **ASP.NET MVC 4**, **Web API 4**, **Razor 2**, **Web Optimization** (`MAT.MVC\packages.config`).
- **ORM / datos**: **DBHelper + stored procedures** (`MAT.Utilities\DBHelper`, clases `*DataAccess` en MVC). EF 5 referenciado pero **`UseEntityFramework` = `NO`**. NetTiers retirado (F9–F10, 2026-07-05).
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
- **Patrón de datos**: **`DBHelper` + SP** (`usp_MAT_*` en `MAT.DB`), clases `*DataAccess` en `MAT.MVC\Infrastructure\Data\`.
- **Lookups / dropdowns**: `MAT.Utilities\Helper.cs` delega en `LookupDataAccess` (SPs de catálogo).
- **Entidades**: `MAT.Entities` con **`*Key`** para claves.
- **Errores en controladores**: la regla del repo (`.cursor\rules\error-handling.mdc`) exige **`ErrorUtil.LogAndGetPublicMessage`** (`MAT.MVC\Infrastructure`) con contexto `"Controller.Action"`, integración con `MATLogger` y `ErrorLog` en BD.
- **No hay proyecto de tests** `*Test*.csproj` en el repositorio buscado.

---

## 5. Base de datos

- **Motor**: **SQL Server** (connection strings con `System.Data.SqlClient`).
- **Conexión**: en **`MAT.MVC\Web.config`** → `<connectionStrings>`:
  - **`MAT.Data.ConnectionString`**: base principal de negocio (catálogo de intranet, p. ej. entornos `MAT_DEV` o `MAT` según configuración).
  - **`MAT.Session.ConnectionString`**: base de sesión / membership.
- **Provider de datos**: connection string **`MAT.Data.ConnectionString`** (nombre histórico; ya no hay sección `<MAT.Data>` / `SqlNetTiersProvider` en `Web.config`).
- **Cambios de esquema**: **`MAT.DB`** (SSDT) es la **única fuente de verdad**; flujo documentado en `DOCUMENTACION\MAT_DB.md` (compilar, publicar, Schema Compare). Scripts sueltos en **`database\`** pueden ser migraciones puntuales pero deben **replicarse en MAT.DB**.
- **SPs**: el proyecto `MAT.DB` incluye **`dbo\Stored Procedures`** (convención `usp_MAT_*` mencionada en la doc).

**Seguridad**: no versionar credenciales reales en `Web.config`; usar secretos locales o transformaciones de publicación fuera del control de código compartido.

---

## 6. Flujo de trabajo (Git y ambientes)

**Ramas típicas** (ajustar según `git branch -a` en el clon): `MAT2026`, `MASTER`, `develop`, y varias `feature/*`, `fixDependencies`, `refactor/*`; remotos pueden incluir `origin/MAT2026`, `origin/MASTER`, `origin/develop`, etc.

**Despliegue / ambientes**: el repo **no define** necesariamente pipelines visibles en todos los clones; lo inferible es:

- **`SystemDEV`** en `appSettings` (`Web.config`) como bandera de entorno de desarrollo.
- Nombres de base **`MAT_DEV.*`** vs comentarios con **`MAT.Intranet` / `MAT.Session`** sugieren **DEV vs otros** por catálogo/servidor.

---

## 7. Áreas críticas o sensibles

- **`MAT.MVC\Web.config`**: connection strings, `BackendAPI_URL`, timeouts, claves de negocio (`PersonaClienteCode`, presupuesto, WhatsApp).
- **`MAT.DB`**: cualquier cambio afecta **toda** la aplicación y despliegues; publicar contra la BD equivocada es destructivo.
- **`MAT.Entities`**: POCOs manuales (F11); sin NetTiers.
- **Autenticación y roles**: `AccountController`, `WebSecurity`, tablas de membership asociadas a `MAT.Session`.
- **Facturación / fiscal**: `FacturaController`, `FacturaFiscalController`, `NotaCreditoController`, auditoría (`AuditFactura*` en entidades relacionadas).
- **Presupuestos**: jobs en `Application_Start` / `Application_BeginRequest` en `Global.asax.cs` (efectos en cada arranque o tráfico).

---

## 8. Estado actual del proyecto y deuda técnica

- **Línea activa de evolución**: rama **`MAT2026`** — modernización UI, presupuesto/seguimiento, **migración NetTiers F0–F12 completada** (2026-07-05).
- **Deuda / riesgos explícitos en repo**:
  - Librerías JS/CSS obsoletas documentadas en `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md`.
  - **EF instalado pero deshabilitado** por configuración (`UseEntityFramework` = `NO`).
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
