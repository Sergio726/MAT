# NetTiers — Plan de migración por fases

**Fecha:** 2026-06-30  
**SPEC:** épica *Arquitectura [P3]: Eliminar NetTiers* (`SPEC.md`)  
**Objetivo:** Salir de NetTiers sin big-bang, dominio por dominio, manteniendo `MAT.MVC` operativo en cada fase.

---

## 1. Situación actual

### Cadena de dependencias (MAT.MVC)

```
MAT.MVC
  ├── MAT.Services      (*Service → *ServiceBase.generated → DataRepository)
  ├── MAT.Data          (providers abstractos generated)
  ├── MAT.Data.SqlClient (SqlNetTiersProvider, Sql*Provider generated)
  ├── MAT.Entities      (*Base.generated.cs, EntityFactory, TList/VList)
  ├── MAT.Enums
  └── MAT.Utilities     (DBHelper — SqlClient directo, patrón moderno)
```

`Web.config` registra `SqlNetTiersProvider` como default de `MAT.Data`.

### Dos patrones de acceso coexistiendo

| Patrón | Dónde | Ejemplos |
|--------|-------|----------|
| **DBHelper + SP** | Models, controllers, Infrastructure | `FacturaFiscalModel`, `ReservaController`, `ReportesController`, `PagoModel.GetPagosByViaje` |
| ***Service NetTiers** | Controllers + Models legacy | `new ViajeService()`, `PaqueteController`, `PersonaClienteController`, `CuentaModel` |

`DataRepository` directo desde MVC es **marginal** (`PaqueteModel` transacciones).

### Proyectos legado

| Proyecto | En MAT.sln | Referenciado por MAT.MVC |
|----------|------------|---------------------------|
| `MAT.Data.WebServiceClient` | No | No |
| `MAT.Web` | Sí | No (solo `Web.config` tagPrefix / httpModule) |

---

## 2. Patrón objetivo (sin NuGet nuevo)

### Lecturas y reportes
- SP en `MAT.DB` + `DBHelper.ExecuteDataSet` / `ExecuteNonQuery`
- DTOs en `MAT.MVC/Models` o `Infrastructure`

### CRUD simple (sustituye *Service)
- Clase `XxxDataAccess` en `MAT.MVC/Infrastructure/Data/` o `MAT.Utilities`
- SqlCommand parametrizado o SP `usp_MAT_Xxx_*`
- Devuelve POCOs (`MAT.Entities` manual, fase final)

### Transacciones (reemplazo de `DataRepository.Provider.CreateTransaction`)
- `SqlConnection` + `SqlTransaction` en el DataAccess
- O un SP que encapsule la transacción en SQL Server

### Qué no hacer
- No editar `*.generated.cs`
- No regenerar NetTiers
- No introducir Dapper/EF sin aprobación

---

## 3. Plantilla por entidad (checklist)

Para cada `FooService` migrado:

1. [ ] `grep "FooService"` en `MAT.MVC` — listar callers
2. [ ] Listar métodos usados (no migrar API completa de NetTiers)
3. [ ] Decidir: ¿SP existente, SP nuevo, o SQL inline en DataAccess?
4. [ ] Implementar `FooDataAccess` con misma semántica
5. [ ] Reemplazar callers en MVC
6. [ ] Smoke test del módulo
7. [ ] Marcar `FooService` como sin uso (eliminar en Fase 9)

---

## 4. Fases (resumen)

| Fase | Alcance | Riesgo | Depende de |
|------|---------|--------|------------|
| **F0** | Inventario + doc | Bajo | — |
| **F1** | WebServiceClient, MAT.Web, congelar API nueva | Bajo | F0 |
| **F2** | Geo: Pais…Destino | Bajo | F1 |
| **F3** | Maestros: Hotel, Transporte, Servicio… | Medio | F2 |
| **F4** | Paquete, Precio, Voucher | Medio-alto | F3 |
| **F5** | Viaje, ViajeHotel, ReservaHabitacion | Alto | F3, F4 |
| **F6** | Factura, Pasaje, Pago, MovimientoCuenta | **Crítico** | F5 |
| **F7** | Persona, Cliente, CC, vistas Persona* | Alto | F6 parcial |
| **F8** | Planilla (retiro declarado), Historial (migrado) | Bajo | F5 — ✅ completada 2026-07-05 |
| **F9** | Retirar MAT.Services + legado Web/WCF | Medio | F2–F8 — ✅ 2026-07-05 |
| **F10** | Retirar MAT.Data + SqlClient + Web.config NetTiers | Medio | F9 — ✅ 2026-07-05 |
| **F11** | POCOs manuales en MAT.Entities | Medio | F10 — ✅ 2026-07-05 |
| **F12** | Docs + barrido final | Bajo | F11 — ✅ 2026-07-05 |

---

## 5. Inventario inicial (F0 — completado 2026-06-30)

**Cadena MVC:** `MAT.MVC` → `MAT.Services` → `MAT.Data` → `MAT.Data.SqlClient`; `Web.config` registra `SqlNetTiersProvider`. `DataRepository` directo: solo `PaqueteModel.GenerarPasajes`.

**F1 ejecutado:** `MAT.Web` fuera de `MAT.sln` y `Web.config`; README en `MAT.Data.WebServiceClient`.

**F2 migrado (sin `PaisService`…`VLocalidadService` en `MAT.MVC` ni geo en `Helper.cs`):**

| Entidad | Estado | Archivos |
|---------|--------|----------|
| Pais, Provincia, Departamento, Localidad | Completo F2 | `GeoDataAccess.cs`, SPs `usp_MAT_*` + legacy `usp_GetAllProvincia`, etc. |
| VLocalidad (búsqueda) | Completo F2 + L12 | `usp_MAT_Localidad_Search`, `LocalidadController.Search`, `mat.geo.localidad.js` |
| Callers | Migrados | `LocalidadController`, `PaqueteController`, models, `Helper.cs` (display por ID) |

**F3 migrado (sin `TransporteService` / `ServicioService` / `ButacaService` en `.cs` activos):**

| Entidad | Estado | Archivos |
|---------|--------|----------|
| Hotel, Habitacion, HabitacionTipo | Ya DBHelper | Sin cambios F3 |
| Transporte | Completo F3 | `TransporteController`, `TransporteMethod` + SPs Get/Update/Delete |
| Servicio | Completo F3 | `ServicioController`, `ServicioMethod` + SPs Get/Update/Delete cascade |
| Butaca | Completo F3 | `ButacaController`, `ButacaMethod` + SPs CRUD |
| Lookups | MaestrosDataAccess | `PasajeModel`, `VoucherModel`, `PaqueteModel`, `InfopathModel`, `ViajeController`, `PaqueteController.RenderGridServicios` |

**F4 (Paquete/Precio/Voucher), F5 (Viaje/ReservaHabitacion), F6 (Factura/Pago) y F7 (Personas/Cliente/Vendedor/Proveedor/Pasajero/Cuenta + vistas Persona*/VPersona) migradas** a SPs + `*DataAccess` (2026-07-03).

**F8 completada (2026-07-05) — Planilla e Historial:**

- **Historial → migrado.** Único uso real de NetTiers en el dominio: `HistorialService.GetAll()` (en `HomeController.RenderGridHistorialPagos`) y `HistorialService.GetByHistorialId` (en `HistorialModel`), ambos de solo lectura. Reemplazados por `HistorialDataAccess` (`Infrastructure/Data/HistorialDataAccess.cs`) + SPs `usp_MAT_Historial_GetAll` / `usp_MAT_Historial_GetByHistorialId`. Sin cambios de comportamiento (mismo filtro en cliente, misma paginación).
- **Planilla → retiro total declarado, no migrado.** El submódulo de impresión/edición de planillas en `AdminController` (`EditarPlanilla`, `ImprimirPlanilla*`, `GridPlanillaServiciosItem*`, `GridPlanillaHotel*`) ya había sido **eliminado por completo** en el commit `f3c75f9` (2026-06-17), antes de llegar a F8 — no solo el menú (retirado en 2026-04-07), sino también las acciones y vistas. A la fecha de F8, las entidades/servicios NetTiers `PlanillaService`, `PlanillaServicioItemService`, `PlanillaHabitacionItemService` y `PlanillaServicioService` ya tenían **cero callers** en `MAT.MVC`. El único resto vivo eran 4 campos/propiedades **estáticas** (no de `HttpContext.Session`, pese a como las describía el enunciado original) en `MATContext.cs` (`Planilla`, `ColeccionPlanillas`, `ServiciosSeleccionados`, `HabitacionesPlanilla`) — huérfanas, sin lectores ni escritores en todo el repo. Se eliminaron directamente. **No se migró nada a DBHelper/SP porque no había nada activo que migrar.**
  - Las tablas `Planilla`, `PlanillaServicioItem`, `PlanillaHabitacionItem` **permanecen intactas en la base de datos**, sin ningún acceso desde `MAT.MVC`. Quedan disponibles para una eventual reactivación futura si negocio confirma la **Opción C** de `SPEC.md` § Viaje-Rentabilidad (revivir Planilla como modelo de costeo por viaje) — ver `DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md`.
  - Nota sobre `PlanillaServicio`: es un artefacto NetTiers huérfano (entidad/provider generado con `DestinationTableName = "PlanillaServicio"`) que en realidad apunta a la misma tabla física `Planilla` — el `PRIMARY KEY` de `Planilla.sql` se llama `PK_PlanillaServicio`, rastro de un rename histórico. No existe una tabla `PlanillaServicio` separada.
- Fuera de alcance de F8 (detectado durante la investigación, no tocado): `Models/PlanillaHotelModel.cs` y `Models/ResumenPlanillaModel.cs` quedaron sin callers activos, pero **no pertenecen al dominio NetTiers Planilla** (usan `MaestrosDataAccess`/`PaqueteDataAccess`/etc., ya migrados) — candidatos a limpieza en una tarea aparte, no en esta fase.

**Pendiente NetTiers (siguientes fases):** retiro de `MAT.Services`/`MAT.Data` (F9–F10), POCOs manuales (F11), barrido final (F12).

### Inventario borrador (F0 — sustituido por tabla arriba)

### Servicios NetTiers (~50 entidades + vistas)

**Geo (F2):** Pais, Provincia, Ciudad, Localidad, Departamento, Destino  

**Maestros (F3):** Hotel, Habitacion, HabitacionTipo, Transporte, Butaca, Proveedor, Servicio, Excursion, Adicional, EstadoPasaje  

**Paquete (F4):** Paquete, PaqueteServicio, PaqueteExcursion, PaqueteAdicional, PaquetePrecio, Precio, PrecioServicio, PrecioHabitacion, Voucher  

**Viaje (F5):** Viaje, ViajeHotel, ReservaHabitacion, PasajeroMenor  

**Facturación (F6):** Factura, Pasaje, PasajeAdicional, Pago, Debito, MovimientoCuenta, Nota, AuditFactura  

**Personas (F7):** Persona, Cliente, Vendedor, TipoCliente, Cuenta, CuentaCorriente + vistas PersonaCliente, PersonaPasajero, PersonaProveedor, PersonaVendedor, VConsultaReservaHabitacion  

**Legacy (F8):** Planilla, PlanillaServicioItem, PlanillaHabitacionItem, PlanillaServicio, Historial  

### DBHelper ya dominante en (no migrar, solo mantener)

`FacturaFiscalModel`, `PresupuestoModel`, `ReservaController`, `AdminController` (parcial), `ReportesController`, `ViajeModel` (parcial), `PersonaClienteModel` (parcial), etc.

---

## 6. Cómo avanzar en la práctica

1. **Empezar por F0** — completar inventario con `grep` y tabla de callers por servicio.
2. **Cerrar F1** — quick wins sin tocar reserva/factura.
3. **Un PR / task = una fase** (o subconjunto pequeño dentro de F2–F3).
4. Tras cada fase: MSBuild `MAT.MVC` Debug + smoke del módulo + entrada en `PROGRESS.md`.
5. **F6** solo cuando F2–F5 estén estables; considerar feature flag o rama larga.

---

## 7. Riesgos

| Riesgo | Mitigación |
|--------|------------|
| Regresión en facturación | F6 al final; pruebas manuales reserva→pago |
| Transacciones en PaqueteModel | SP transaccional o SqlTransaction explícita antes de F4 |
| Vistas NetTiers (PersonaCliente) | SP + DTO, no replicar `Get(whereClause)` dinámico |
| EntityFactory / tracking | Al migrar, no depender de change tracking NetTiers |
| Tipos `TList`/`VList` | Reemplazar por `List<T>` en capa nueva |

---

## 8. Próximo paso concreto

**Estado 2026-07-05:** **Épica NetTiers F0–F12 completada.** F11 convirtió 59 entidades/vistas a POCOs manuales; F12 cerró documentación, hotfix seguridad (ErrorUtil), perf LookupDataAccess y barrido final. Cadena activa: `MAT.MVC` → `MAT.Utilities` (DBHelper) + `MAT.Entities` (POCOs).

**Deuda de despliegue (humano):** publicar `database/2026-07-05_NetTiers_F9_Lookup_SPs.sql` + `database/2026-07-05_NetTiers_F12_Lookup_Perf_SPs.sql`; smoke dropdowns + reserva→pago.
