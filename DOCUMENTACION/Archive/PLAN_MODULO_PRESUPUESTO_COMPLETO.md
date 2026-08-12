# Plan de Implementación – Módulo Presupuesto Completo

**Fecha:** 2026-02-15  
**Versión:** 1.0  
**Objetivo:** Cerrar por completo el módulo de presupuestos, desde creación hasta análisis de conversión.

---

## 1. Estado Actual (Implementado)

### 1.1 Funcionalidades Core
| Funcionalidad | Estado | Ubicación |
|---------------|--------|-----------|
| Dashboard con estadísticas (Pendientes, Cerrados, Expirados, Total) | ✅ | Index, PresupuestoMethod.GetEstadisticas |
| Crear presupuesto (preventa telefónica) | ✅ | Create, CreatePresupuesto |
| Búsqueda de cliente por autocomplete | ✅ | Create + PersonaCliente/GetClientesTop |
| Selección de viaje + precio base + adicionales | ✅ | Create, GetViajeInfo |
| Código único (MAT-YYYYMMDD-XXXX) | ✅ | usp_MAT_Presupuesto_Insert |
| Mensaje WhatsApp con nombre del cliente | ✅ | ConstruirMensajeWhatsApp |
| Imprimir/PDF presupuesto | ✅ | ImprimirPresupuesto |
| Panel de seguimiento con filtros | ✅ | Seguimiento, GetAll |
| Convertir en venta (link a NuevaReserva) | ✅ | Seguimiento, irAVenta |
| Extender expiración 24h | ✅ | ExtenderExpiracion |
| Marcado automático de expirados | ✅ | Global.asax, MarcarExpirados |
| Vinculación presupuesto–factura al cerrar venta | ✅ | NuevaReservaController.PagarReserva |
| Crear cliente desde presupuesto | ✅ | PersonaCliente/CreateFromPresupuesto |

### 1.2 Base de Datos
| Objeto | Estado |
|--------|--------|
| Tabla Presupuesto | ✅ |
| usp_MAT_Presupuesto_Insert | ✅ |
| usp_MAT_Presupuesto_GetByDni | ✅ |
| usp_MAT_Presupuesto_GetByCodigo | ✅ |
| usp_MAT_Presupuesto_GetAll | ✅ |
| usp_MAT_Presupuesto_UpdateEstado | ✅ |
| usp_MAT_Presupuesto_Expirados | ✅ |
| usp_MAT_Presupuesto_ExtenderExpiracion | ✅ |

---

## 2. Pendientes de Implementación

### 2.1 Prioridad Alta

#### 2.1.1 Configuración de horas de expiración
- **Descripción:** Hoy la expiración está fija en 48h. Parametrizar para que sea configurable.
- **Archivos a modificar:**
  - `Web.config`: agregar `Presupuesto_HorasExpiracion` en appSettings.
  - `PresupuestoController.CreatePresupuesto`: leer horas desde config.
  - `ConstruirMensajeWhatsApp`: usar el valor configurado en el texto.
- **Esfuerzo:** Bajo (1–2 h).

#### 2.1.2 Enlace a factura en presupuesto cerrado
- **Descripción:** Cuando `Estado = Cerrado` y hay `FacturaId`, el modal ya muestra "Ver Factura" (línea 582 de Seguimiento.cshtml). Verificar que la ruta funcione.
- **Archivos:** `Seguimiento.cshtml` (revisar `PopupDetalleFactura`).
- **Esfuerzo:** Bajo (0.5 h) – posiblemente ya implementado.

#### 2.1.3 Estados adicionales: Rechazado/Cancelado
- **Descripción:** Permitir marcar un presupuesto como rechazado o cancelado sin cerrar venta.
- **Cambios necesarios:**
  - `eEstadoPresupuesto`: agregar `Rechazado = 4`, `Cancelado = 5` (o uno solo según negocio).
  - SP `usp_MAT_Presupuesto_UpdateEstado` o nuevo SP `usp_MAT_Presupuesto_Cancelar`.
  - Action `CancelarPresupuesto` en PresupuestoController.
  - Botón "Cancelar" en modal de Seguimiento (solo pendientes).
- **Esfuerzo:** Medio (2–3 h).

---

### 2.2 Prioridad Media

#### 2.2.1 Editar presupuesto (solo pendientes)
- **Descripción:** Editar monto, observaciones, fecha de expiración y datos de cliente en presupuestos pendientes.
- **Cambios necesarios:**
  - SP `usp_MAT_Presupuesto_Update`.
  - `PresupuestoMethod.Update`.
  - Action `Edit` (GET) y `UpdatePresupuesto` (POST).
  - Vista `Edit.cshtml` o modal de edición en Seguimiento.
  - Botón "Editar" en modal de detalle.
- **Esfuerzo:** Medio–Alto (4–6 h).

#### 2.2.2 Extender expiración con horas configurables
- **Descripción:** Permitir elegir 12h, 24h, 48h o valor custom.
- **Cambios necesarios:**
  - Modal o dropdown en lugar de botón fijo "Extender 24h".
  - Actualizar `ExtenderExpiracion` para recibir horas desde la UI.
- **Esfuerzo:** Bajo (1 h).

#### 2.2.3 Reporte / métricas de conversión
- **Descripción:** Dashboard con:
  - Presupuestos → ventas (% conversión).
  - Conversión por vendedor.
  - Tendencia temporal (gráfico o tabla).
- **Cambios necesarios:**
  - SP `usp_MAT_Presupuesto_GetMetricasConversion`.
  - `PresupuestoMethod.GetMetricasConversion`.
  - Action `Metricas` y vista `Metricas.cshtml`.
  - Gráficos (Chart.js o similar).
  - Entrada en menú "Presupuestos".
- **Esfuerzo:** Medio–Alto (6–8 h).

#### 2.2.4 Notificaciones de presupuestos próximos a vencer
- **Descripción:** Avisar al vendedor sobre presupuestos que expiran en &lt; 24 h.
- **Cambios necesarios:**
  - SP `usp_MAT_Presupuesto_ProximosVencer`.
  - Endpoint para widget/badge en layout o Index.
  - Opcional: email o notificación interna.
- **Esfuerzo:** Medio (3–4 h).

---

### 2.3 Prioridad Baja

#### 2.3.1 Integración API WhatsApp
- **Descripción:** Envío real de mensajes vía API en lugar de solo link `wa.me`.
- **Cambios necesarios:**
  - Configuración `WhatsApp_API_URL`, credenciales.
  - Implementar `EnviarWhatsApp` con `HttpClient`.
  - Manejo de errores y reintentos.
- **Esfuerzo:** Alto (8+ h, depende del proveedor).

#### 2.3.2 Historial de cambios (auditoría)
- **Descripción:** Registrar cambios de estado, extensiones, cancelaciones.
- **Cambios necesarios:**
  - Tabla `PresupuestoAuditoria` o similar.
  - Triggers o lógica en SPs.
  - Vista de historial en modal de detalle.
- **Esfuerzo:** Medio–Alto (4–6 h).

#### 2.3.3 Exportar listado (Excel/CSV)
- **Descripción:** Exportar tabla de Seguimiento con filtros aplicados.
- **Cambios necesarios:**
  - Action `ExportarSeguimiento` que use EPPlus o NPOI.
  - Botón "Exportar" en Seguimiento.
- **Esfuerzo:** Bajo–Medio (2–3 h).

#### 2.3.4 Página de selección de viaje para “Convertir en venta”
- **Descripción:** Si el presupuesto no tiene viaje, mostrar listado de viajes activos y redirigir con `viajeId` + `codigoPresupuesto`.
- **Cambios necesarios:**
  - Vista `SeleccionarViaje.cshtml` o modal.
  - Action `SeleccionarViajeParaPresupuesto(codigoPresupuesto)`.
  - Actualizar `irAVenta` cuando no hay `viajeId`.
- **Esfuerzo:** Medio (2–3 h).

---

## 3. Orden de Implementación Sugerido

### Fase 1 – Cierre rápido (1–2 días)
1. Configuración de horas de expiración (Web.config).
2. Revisar enlace a factura en presupuesto cerrado.
3. Estados Rechazado/Cancelado + botón Cancelar en modal.
4. Extender expiración con selector de horas (12/24/48).

### Fase 2 – Funcionalidad extendida (3–5 días)
5. Editar presupuesto (Update).
6. Reporte de métricas de conversión (vista + SP).
7. Notificaciones de presupuestos próximos a vencer.

### Fase 3 – Mejoras opcionales (según prioridad)
8. Página de selección de viaje para “Convertir en venta”.
9. Exportar listado (Excel).
10. Historial/auditoría de presupuestos.

---

## 4. Dependencias Técnicas

| Tarea | Dependencias |
|-------|--------------|
| Configurar horas expiración | Ninguna |
| Estados Rechazado/Cancelado | eEstadoPresupuesto, SP |
| Editar presupuesto | SP Update, validaciones |
| Métricas conversión | SP, posible librería de gráficos |
| Notificaciones próximos a vencer | SP nuevo |
| API WhatsApp | Proveedor externo, configuración |

---

## 5. Matriz de Riesgo

| Tarea | Riesgo | Mitigación |
|-------|--------|------------|
| Nuevos estados presupuesto | Bajo | Mapear correctamente en SPs y vistas |
| Editar presupuesto | Medio | Validar que solo se editen pendientes |
| Métricas conversión | Bajo | Usar agregaciones existentes en BD |
| API WhatsApp | Alto | Mantener fallback con enlace wa.me |

---

## 6. Criterios de Aceptación (Módulo Completo)

- [ ] Horas de expiración configurables.
- [ ] Presupuestos cancelables/rechazables.
- [ ] Edición de presupuestos pendientes.
- [ ] Extender expiración con horas seleccionables.
- [ ] Dashboard de métricas de conversión.
- [ ] Avisos de presupuestos próximos a vencer.
- [ ] Enlace directo a factura en presupuestos cerrados.
- [ ] Documentación de usuario actualizada.

---

## 7. Archivos de Referencia

```
MAT.MVC/
├── Controllers/Presupuesto/PresupuestoController.cs
├── Models/PresupuestoModel.cs
├── Views/Presupuesto/
│   ├── Index.cshtml
│   ├── Create.cshtml
│   ├── Seguimiento.cshtml
│   └── ImprimirPresupuesto.cshtml
├── Content/modern-presupuesto.css
└── Global.asax.cs

MAT.DB/
├── dbo/Tables/Presupuesto.sql
└── dbo/Stored Procedures/
    ├── usp_MAT_Presupuesto_Insert.sql
    ├── usp_MAT_Presupuesto_GetAll.sql
    ├── usp_MAT_Presupuesto_GetByCodigo.sql
    ├── usp_MAT_Presupuesto_GetByDni.sql
    ├── usp_MAT_Presupuesto_UpdateEstado.sql
    ├── usp_MAT_Presupuesto_Expirados.sql
    └── usp_MAT_Presupuesto_ExtenderExpiracion.sql

MAT.Enums/
└── eEstadoPresupuesto.cs
```
