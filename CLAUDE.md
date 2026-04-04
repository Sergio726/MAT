# CLAUDE.md — Proyecto MAT

Guía de referencia para Claude Code al trabajar en este repositorio.

---

## 1. Qué es este proyecto

**MAT** es una aplicación web de intranet para la gestión operativa de una agencia de viajes/turismo. Cubre: reservas, viajes, pasajeros, paquetes, hoteles, transporte, excursiones, precios, clientes, vendedores, proveedores, facturación, cuenta corriente, pagos y presupuestos con seguimiento.

---

## 2. Arquitectura

| Proyecto | Rol |
|---|---|
| **MAT.MVC** | Host principal. ASP.NET MVC 4 / .NET 4.8. Controladores, vistas Razor, Web API, bundles, auth. |
| **MAT.Services** | Servicios de dominio. `*Service` extienden `*ServiceBase` → `ServiceBase<TEntity, TKey>`. |
| **MAT.Entities** | POCOs del modelo + claves (`*Key`). |
| **MAT.Enums** | Enumeraciones compartidas. |
| **MAT.Data** | Capa de datos estilo NetTiers: `DataRepository`, `NetTiersProvider`. |
| **MAT.Data.SqlClient** | Implementación SQL Server del provider (`SqlNetTiersProvider`). |
| **MAT.Utilities** | Utilidades transversales (logging, helpers). |
| **MAT.DB** | Proyecto SSDT — **fuente de verdad del esquema SQL Server**. |
| **MAT.Web** | Biblioteca legacy WebForms/NetTiers. No referenciada por MAT.MVC actualmente. |
| **MAT.WCF** | Servicio WCF legacy InfoPath. Proyecto separado. |

---

## 3. Stack tecnológico

- **.NET 4.8**, **ASP.NET MVC 4**, **Web API 4**, **Razor 2**
- **ORM activo**: NetTiers + ADO.NET/SqlClient. **Entity Framework está instalado pero deshabilitado** (`UseEntityFramework = NO` en `appSettings`). No asumir que EF está en uso.
- **AutoMapper 6.0.2**, **EPPlus 4.5.3.3**, **Enterprise Library 5**
- **Base de datos**: SQL Server (connection strings en `MAT.MVC\Web.config`)
- **Front**: jQuery 3 / Bootstrap 5 (en rama MAT2026; `packages.config` puede reflejar versiones antiguas)

---

## 4. Convenciones de código C#

- Controladores bajo `MAT.MVC\Controllers\<Dominio>\<Dominio>Controller.cs`
- Servicios: `NombreEntidadService` extiende `NombreEntidadServiceBase`
- Acceso a datos vía `DataRepository` + providers, no repositorios DDD manuales
- Archivos `*.generated.cs` no se editan manualmente (se regeneran con NetTiers)

### Manejo de errores en controladores (obligatorio)

Siempre usar `ErrorUtil.LogAndGetPublicMessage` en bloques `catch`. Nunca exponer `e.Message` al usuario.

```csharp
// Para JsonResult:
catch (Exception e)
{
    var msg = ErrorUtil.LogAndGetPublicMessage(e, "NombreController.NombreAccion");
    return Json(new { success = false, message = msg });
}

// Para View/Redirect:
catch (Exception e)
{
    var msg = ErrorUtil.LogAndGetPublicMessage(e, "ClienteController.Create");
    return RedirectToAction("Create", new { msj = msg });
}
```

**Namespace**: `using MAT.MVC.Infrastructure;`

---

## 5. Base de datos

- Connection strings en `MAT.MVC\Web.config`: `MAT.Data.ConnectionString` (negocio) y `MAT.Session.ConnectionString` (membership)
- **`MAT.DB` (SSDT) es la única fuente de verdad del esquema.** Todo cambio de schema debe reflejarse ahí.
- Scripts sueltos en `database\` son migraciones puntuales; deben tener paridad con `MAT.DB`
- SPs siguen convención de nombre `usp_MAT_*`

### Cabecera obligatoria en Stored Procedures

```sql
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: YYYY-MM-DD
  -- Description: [Descripción del SP o cambio]
  ============================================= */
BEGIN
```

---

## 6. Git y ramas

- Rama principal de integración: **`MASTER`**
- Rama de trabajo activa: **`MAT2026`**
- También existen: `develop`, `feature/*`, `refactor/*`
- Compilar sin errores antes de hacer push

---

## 7. Lo que NO se debe hacer

- **No commitear** `Web.config` con passwords reales de producción
- **No aplicar scripts SQL** sin actualizar también `MAT.DB`
- **No exponer** `e.Message` al usuario en controladores — usar `ErrorUtil.LogAndGetPublicMessage`
- **No editar masivamente** archivos `*.generated.cs` manualmente
- **No asumir** que Entity Framework está activo (está desactivado)
- **No tocar** `Application_BeginRequest` en `Global.asax.cs` sin entender el impacto (corre en cada request)
- **No publicar** `MAT.DB` contra la BD equivocada (es destructivo)

---

## 8. Áreas críticas — confirmar antes de modificar

- `MAT.MVC\Web.config` — connection strings, API keys, flags de entorno
- `MAT.DB\` — cualquier cambio afecta toda la aplicación
- `MAT.Data\` y `MAT.Data.SqlClient\` — archivos generados, romper el contrato impacta todos los servicios
- `MAT.Services\ServiceBase*` — capa central de persistencia
- `AccountController` + tablas de membership — autenticación y roles
- `FacturaController`, `FacturaFiscalController`, `NotaCreditoController` — facturación fiscal
- `Global.asax.cs` — lógica de arranque y jobs de presupuestos

---

## 9. Comandos

```bash
# Compilar
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" MAT.sln /t:Build /p:Configuration=Debug

# Levantar localmente
# Abrir MAT.sln en VS, proyecto de inicio MAT.MVC, IIS Express
```

---

## 10. Documentación interna

- `DOCUMENTACION\GUIA_SISTEMA_MAT.md` — contexto técnico completo
- `DOCUMENTACION\MAT_DB.md` — proyecto SSDT y flujo de esquema
- `DOCUMENTACION\RESUMEN_EJECUTIVO_MAT2026.md` — módulo presupuesto y evolución
- `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md` — vistas pendientes de modernizar
- `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md` — deuda JS/CSS
