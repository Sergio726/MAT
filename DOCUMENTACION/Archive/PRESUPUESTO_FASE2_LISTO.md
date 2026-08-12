# Módulo Presupuesto – Fase 2: Listo para implementar

**Fecha:** 2026-02-15  
**Estado Fase 1:** ✅ Completada

---

## Fase 1 – Completada

| Tarea | Estado |
|-------|--------|
| Configuración horas expiración (Web.config `Presupuesto_HorasExpiracion`) | ✅ |
| Enlace a factura en presupuesto cerrado (`/PersonaCliente/PopupDetalleFactura`) | ✅ Verificado |
| Estados Rechazado (4) y Cancelado (5) + botón Cancelar | ✅ |
| Extender expiración con selector 12/24/48 h | ✅ |

---

## Fase 2 – Tareas pendientes

### 1. Editar presupuesto (solo pendientes)

**Archivos a crear/modificar:**
- `MAT.DB/dbo/Stored Procedures/usp_MAT_Presupuesto_Update.sql` (nuevo)
- `MAT.MVC/Models/PresupuestoModel.cs` → método `Update`
- `MAT.MVC/Controllers/Presupuesto/PresupuestoController.cs` → `Edit` (GET), `UpdatePresupuesto` (POST)
- `MAT.MVC/Views/Presupuesto/Edit.cshtml` (nuevo) o modal en Seguimiento
- `MAT.MVC/Views/Presupuesto/Seguimiento.cshtml` → botón "Editar" en modal

**Campos editables:** MontoPactado, Observaciones, FechaExpiracion, NombreCliente, TelefonoCliente, EmailCliente (DniCliente no cambiar si ya hay factura asociada – pero para pendientes no hay factura).

---

### 2. Reporte de métricas de conversión

**Archivos a crear/modificar:**
- `MAT.DB/dbo/Stored Procedures/usp_MAT_Presupuesto_GetMetricasConversion.sql` (nuevo)
- `MAT.MVC/Models/PresupuestoModel.cs` → `GetMetricasConversion`
- `MAT.MVC/Controllers/Presupuesto/PresupuestoController.cs` → `Metricas` (GET)
- `MAT.MVC/Views/Presupuesto/Metricas.cshtml` (nuevo)
- `MAT.MVC/Views/Shared/_Layout.cshtml` → entrada en menú Presupuestos
- Opcional: Chart.js o librería de gráficos para tendencias

**Métricas sugeridas:**
- Total presupuestos (por período)
- Conversión: Cerrados / (Cerrados + Expirados + Rechazados + Cancelados)
- Conversión por vendedor
- Tendencia mensual

---

### 3. Notificaciones de presupuestos próximos a vencer

**Archivos a crear/modificar:**
- `MAT.DB/dbo/Stored Procedures/usp_MAT_Presupuesto_ProximosVencer.sql` (nuevo)
- `MAT.MVC/Models/PresupuestoModel.cs` → `GetProximosVencer`
- `MAT.MVC/Controllers/Presupuesto/PresupuestoController.cs` → `GetProximosVencer` (JSON)
- `MAT.MVC/Views/Presupuesto/Index.cshtml` → widget/badge con cantidad
- Opcional: `_Layout.cshtml` → badge global en menú

**Lógica:** Presupuestos con Estado=1, FechaExpiracion entre NOW y NOW+24h.

---

## Orden sugerido para Fase 2

1. **Notificaciones próximos a vencer** (más rápido, alto impacto)
2. **Editar presupuesto**
3. **Métricas de conversión** (más trabajo, requiere SP y posiblemente gráficos)

---

## Compilación previa

Antes de continuar, compilar el proyecto en Visual Studio para validar que Fase 1 no introdujo errores.
