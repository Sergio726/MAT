# CLAUDE.md — Proyecto MAT

**MAT** es una aplicación web de intranet para la gestión operativa de una agencia de viajes/turismo. Cubre: reservas, viajes, pasajeros, paquetes, hoteles, transporte, excursiones, precios, clientes, vendedores, proveedores, facturación, cuenta corriente, pagos y presupuestos con seguimiento.

---

## Stack

| Capa | Tecnología |
|------|-----------|
| Framework | ASP.NET MVC 4 / .NET 4.8 |
| UI | Razor 2, Bootstrap 5, jQuery 3 |
| Lenguaje | C# / T-SQL |
| ORM | **DBHelper + SP**; entidades **POCOs manuales** en `MAT.Entities` (F11, 2026-07-05); EF deshabilitado |
| Base de datos | SQL Server |
| Librerías | AutoMapper 6.0.2, EPPlus 4.5.3.3, Enterprise Library 5 |

---

## Arquitectura de proyectos

```
MAT.sln
├── MAT.MVC/              # Host principal — controladores, vistas Razor, Web API, bundles, auth
│   ├── Controllers/<Dominio>/<Dominio>Controller.cs
│   ├── Views/<Dominio>/
│   ├── Infrastructure/Data/  # *DataAccess (DBHelper + SP)
│   └── Web.config        # Connection strings, appSettings (¡no commitear passwords!)
├── MAT.Entities/         # POCOs del modelo (F11 — sin *.generated.cs)
├── MAT.Enums/            # Enumeraciones compartidas
├── MAT.Utilities/        # DBHelper, GeoDataAccess, LookupDataAccess, Helper
└── MAT.DB/               # Proyecto SSDT — FUENTE DE VERDAD del esquema SQL Server
```

**Eliminados del repo (2026-07-05, F9–F10):** `MAT.Services`, `MAT.Data`, `MAT.Data.SqlClient`, `MAT.Web`, `MAT.WCF`, `MAT.Data.WebServiceClient`. Recuperables desde git si hiciera falta legado.

---

## Comandos esenciales

```bash
# Compilar solución completa
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" MAT.sln /t:Build /p:Configuration=Debug

# Compilar solo MAT.MVC
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" MAT.MVC\MAT.MVC.csproj /t:Build /p:Configuration=Debug

# Levantar localmente
# Abrir MAT.sln en Visual Studio 2022, proyecto de inicio MAT.MVC, correr con IIS Express
```

---

## Convenciones de código C#

- Controladores bajo `MAT.MVC\Controllers\<Dominio>\<Dominio>Controller.cs`
- Servicios: `NombreEntidadService` extiende `NombreEntidadServiceBase` → `ServiceBase<TEntity, TKey>`
- Acceso a datos vía `DataRepository` + providers, nunca repositorios DDD manuales
- Archivos `*.generated.cs` **no se editan manualmente** (se regeneran con NetTiers)

### Migración NetTiers (en curso)

- **Código nuevo en `MAT.MVC`:** solo `DBHelper` + SP en `MAT.DB` (patrones: `HotelModel`, `ServicioMethod`, `MaestrosDataAccess`).
- **No agregar** nuevos usos de `*Service` NetTiers desde MVC; extender `*Method` / `Infrastructure/Data/`.
- Épica y fases: `SPEC.md` (NetTiers F0–F12), detalle en `DOCUMENTACION/NETTIERS_MIGRACION_FASES.md`.

### Manejo de errores en controladores (obligatorio)

Siempre usar `ErrorUtil.LogAndGetPublicMessage` en bloques `catch`. **Nunca** exponer `e.Message` al usuario.

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

## Base de datos

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

## Git y ramas

- Rama principal de integración: **`MASTER`**
- Rama de trabajo activa: **`MAT2026`**
- También existen: `develop`, `feature/*`, `refactor/*`
- Compilar sin errores antes de hacer push

---

## Lo que NO se debe hacer

- **No commitear** `Web.config` con passwords reales de producción
- **No aplicar scripts SQL** sin actualizar también `MAT.DB`
- **No exponer** `e.Message` al usuario en controladores — usar `ErrorUtil.LogAndGetPublicMessage`
- **No editar masivamente** archivos `*.generated.cs` manualmente
- **No asumir** que Entity Framework está activo (está desactivado: `UseEntityFramework = NO`)
- **No tocar** `Application_BeginRequest` en `Global.asax.cs` sin entender el impacto (corre en cada request)
- **No publicar** `MAT.DB` contra la BD equivocada (es destructivo)

---

## Áreas críticas — confirmar antes de modificar

- `MAT.MVC\Web.config` — connection strings, API keys, flags de entorno
- `MAT.DB\` — cualquier cambio afecta toda la aplicación
- `MAT.Data\` y `MAT.Data.SqlClient\` — archivos generados, romper el contrato impacta todos los servicios
- `MAT.Services\ServiceBase*` — capa central de persistencia
- `AccountController` + tablas de membership — autenticación y roles
- `FacturaController`, `FacturaFiscalController`, `NotaCreditoController` — facturación fiscal
- `Global.asax.cs` — lógica de arranque y jobs de presupuestos

---

## Modo de trabajo autónomo

### Flujo por feature
1. Leer el task activo en `SPEC.md` antes de escribir código
2. Implementar solo lo que el task describe, sin scope creep
3. Compilar con MSBuild al terminar cada task; corregir errores antes de continuar
4. Si un error no se resuelve en 2 intentos, pausar y reportar
5. Marcar el task como `[x]` en `SPEC.md` al completarlo
6. Registrar en `PROGRESS.md`: fecha, archivos tocados, qué se hizo, problemas
7. Pasar al siguiente task

### Cuándo pausar y preguntar
- Decisión de arquitectura que afecta más de 2 proyectos o archivos core
- Cambio en entidades, claves (`*Key`) o contratos de servicio globales
- Ambigüedad en lógica de negocio que no se puede inferir del código
- Error de compilación que no se resuelve tras 2 intentos de autocorrección
- Cualquier modificación en las áreas críticas listadas arriba

### Cuándo NO pausar
- Errores de compilación resolubles con el contexto disponible
- Ajustes de estilos o layout en vistas Razor
- Refactors internos de un solo controlador o servicio
- Agregar funcionalidad siguiendo un patrón ya existente en el proyecto

### Definición de "terminado"
Un task está terminado cuando:
- MSBuild pasa sin errores nuevos en `MAT.MVC`
- La feature funciona según la descripción del task
- Cambios de schema están reflejados en `MAT.DB`
- `PROGRESS.md` está actualizado

---

## Gestión de archivos de trabajo

- **`SPEC.md`** → fuente de verdad de qué construir. Solo modificar para marcar `[x]` en tasks completados o agregar tasks nuevos acordados con el humano.
- **`PROGRESS.md`** → bitácora de lo que se fue haciendo. Actualizar al terminar cada task con este formato:

```
### [YYYY-MM-DD] — nombre del task
- Archivos modificados: ...
- Qué se implementó: ...
- Problemas encontrados: ...
- Estado: ✅ completo / ⚠️ bloqueado / 🔄 en progreso
```

---

## Documentación interna

- `DOCUMENTACION\GUIA_SISTEMA_MAT.md` — contexto técnico completo
- `DOCUMENTACION\MAT_DB.md` — proyecto SSDT y flujo de esquema
- `DOCUMENTACION\RESUMEN_EJECUTIVO_MAT2026.md` — módulo presupuesto y evolución
- `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md` — vistas pendientes de modernizar
- `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md` — deuda JS/CSS
