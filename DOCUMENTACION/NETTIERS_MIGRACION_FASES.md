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
| **F8** | Planilla, Historial | Bajo | F5 |
| **F9** | Retirar MAT.Services generated | Medio | F2–F8 |
| **F10** | Retirar MAT.Data + SqlClient | Medio | F9 |
| **F11** | POCOs manuales en MAT.Entities | Medio | F10 |
| **F12** | Docs + barrido final | Bajo | F11 |

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

**Pendiente NetTiers (siguientes fases):** Planilla/Historial F8, retiro de `MAT.Services`/`MAT.Data` (F9–F10), POCOs manuales (F11), barrido final (F12) — ver grep `new \w+Service(` en `MAT.MVC` para servicios residuales (p. ej. `HistorialService`).

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

**Estado 2026-07-03:** F0–F7 completadas en código (MSBuild `MAT.sln` Debug OK). F7 migró Persona, Cliente, Vendedor, Proveedor, Pasajero, Cuenta y las vistas Persona*/VPersona a 28 SPs + clases `*DataAccess` en `MAT.MVC/Infrastructure/Data`.

**Task SPEC:** `NetTiers F8` — Planilla e historial (`HistorialModel` aún usa `HistorialService`, `HomeController`, planillas en sesión `MATContext`).

**Deuda de despliegue (humano):** publicar los SPs de F4–F7 desde `MAT.DB` (y scripts `database/2026-07-03_*`) en cada entorno + smoke crítico (flujo de pago F6; ABM personas y cuenta corriente F7) antes de commitear por fase.
