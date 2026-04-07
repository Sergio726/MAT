# PROGRESS.md — Bitácora de desarrollo

---

### [2026-04-07] — Estado inicial del proyecto (baseline)

- Archivos auditados: `CLAUDE.md`, `SPEC.md`, `DOCUMENTACION\GUIA_SISTEMA_MAT.md`, `DOCUMENTACION\MAT_DB.md`, `DOCUMENTACION\RESUMEN_EJECUTIVO_MAT2026.md`, `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md`, `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md`
- Qué se implementó: Auditoría completa para establecer baseline; creación de CLAUDE.md actualizado, SPEC.md y PROGRESS.md
- Problemas encontrados:
  - Vistas pendientes de modernización identificadas (Bootstrap 5 / jQuery 3)
  - Deuda de librerías JS/CSS obsoletas documentada en `LIBRERIAS_OBSOLETAS_2026-01-04.md`
- Estado: ✅ baseline establecido — SPEC.md y PROGRESS.md creados

#### Resumen al 2026-04-07

**Completado y funcionando:**
- Gestión de reservas, viajes, pasajeros, paquetes, hoteles, transporte y excursiones
- Módulo de precios, clientes, vendedores y proveedores
- Facturación, cuenta corriente, pagos
- Módulo de presupuestos con seguimiento (ver `RESUMEN_EJECUTIVO_MAT2026.md`)
- Manejo de errores centralizado vía `ErrorUtil.LogAndGetPublicMessage`
- Logging de errores con `usp_MAT_ErrorLog_Insert`

**Pendiente (ver SPEC.md):**
- P1: Actualizar vistas pendientes de modernización a Bootstrap 5 / jQuery 3
- P2: (por definir con el equipo)
- P3: (por definir con el equipo)

---

### [2026-04-07] — P1 (avance): Viaje/Create modernizado

- Archivos modificados: `MAT.MVC/Views/Viaje/Create.cshtml`, `MAT.MVC/Controllers/Viaje/ViajeController.cs`, `DOCUMENTACION/VISTAS_PENDIENTES_ACTUALIZACION.md`, `PROGRESS.md`
- Qué se implementó: Reemplazo del layout legacy (formulario tipo BS2 `btn-inverse`, `.form` / `.form-label`) por contenedor `modern-page-container`, grid `modern-form-grid`, alertas `modern-alert-error`, botones `btn-modern`, iconos Bootstrap Icons; mismos names/POST y datepicker + timepicker que antes. `Create` GET y respuestas de error POST ahora devuelven `View(new MAT.Entities.Viaje())` para evitar modelo nulo y ambigüedad de tipo con el namespace del controller.
- Problemas encontrados: El listado de vistas pendientes tenía Fase 4 parcialmente desactualizada respecto al código (varias vistas ya estaban modernas); solo **Viaje/Create** requería cambio sustantivo en esta iteración.
- Estado: 🔄 P1 sigue abierto en SPEC — este commit avanza un ítem de Fase 4; MSBuild MAT.MVC OK
