# SPEC.md — Proyecto MAT

## Features completadas
(El agente completa esta sección a medida que avanza)

- [x] Setup inicial del proyecto — estructura de solución, CLAUDE.md, SPEC.md, PROGRESS.md

---

## Features pendientes

### P1 — Crítico / Deuda técnica

- [ ] **Actualizar vistas pendientes de modernización**
  Revisar `DOCUMENTACION\VISTAS_PENDIENTES_ACTUALIZACION.md` y migrar las vistas listadas a Bootstrap 5 / jQuery 3, eliminando dependencias obsoletas identificadas en `DOCUMENTACION\LIBRERIAS_OBSOLETAS_2026-01-04.md`.
  Criterio de éxito: Las vistas actualizadas renderizan correctamente en IIS Express sin errores de consola JS; MSBuild pasa sin errores.

### P2 — Mejoras de producto

- [ ] **NOMBRE_DEL_TASK**
  DESCRIPCIÓN
  Criterio de éxito: CRITERIO

### P3 — Nuevas capacidades

- [ ] **NOMBRE_DEL_TASK**
  DESCRIPCIÓN
  Criterio de éxito: CRITERIO

---

## Criterios globales (aplican a todos los tasks)

- MSBuild sobre `MAT.MVC` debe pasar limpio al finalizar cada task
- Todo cambio de schema SQL debe reflejarse en `MAT.DB` (SSDT)
- No agregar paquetes NuGet sin consultar al humano
- Los contratos de entidades (`MAT.Entities`) e interfaces de servicio no se modifican sin consultar
- `PROGRESS.md` se actualiza al terminar cada task
