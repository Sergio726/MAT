# Dashboard UX — Command Center

**Fecha:** 2026-07-05  
**Estado Fase 1:** Implementado  
**Alcance:** Header contextual, sidebar agrupado, accesos frecuentes (Opción A)

---

## Fase 1 — Implementado

### Accesos frecuentes (home)

- Catálogo centralizado en [`MAT.MVC/Infrastructure/QuickAccessCatalog.cs`](../MAT.MVC/Infrastructure/QuickAccessCatalog.cs).
- Partial [`MAT.MVC/Views/Home/_QuickAccess.cshtml`](../MAT.MVC/Views/Home/_QuickAccess.cshtml):
  - **6 tiles de acción** en fila principal (verbo + objeto, un solo enlace por tile).
  - Panel colapsable **"Todos los módulos"** con rutas secundarias (cerrado por defecto).
  - Historial de pagos visible para rol **Administrador** o cuenta **admindev** (`AdminAuthorizationHelper.CanViewAllHistorialPagos`).
- Estilos en [`MAT.MVC/Content/modern-dashboard.css`](../MAT.MVC/Content/modern-dashboard.css) (`.quick-access-frequent-grid`, `.quick-access-tile*`).
- Eliminados: grid de 5 columnas, badge pulsante, doble acción (card + link), estilos inline en `Index.cshtml`.

### Header contextual

- Altura del navbar: **56px** (`--navbar-height` en `modern-layout.css`).
- En `/Home/Index`: título **"Panel Principal"** en lugar del breadcrumb redundante (`_Breadcrumb.cshtml`).
- En el resto de páginas: breadcrumb + botón Volver sin cambios.

### Sidebar agrupado

- Menú extraído a [`MAT.MVC/Views/Shared/_MainNav.cshtml`](../MAT.MVC/Views/Shared/_MainNav.cshtml).
- Secciones visuales: **Comercial**, **Operaciones**, **Finanzas**, **Catálogo**, **Herramientas** (Cotizador).
- Etiquetas `.menu-section-label` en `modern-menu.css`.
- URLs y submenús existentes preservados; Viajes/Paquetes siguen solo en accesos rápidos.

---

## Fases pendientes

### Fase 2 — Sidebar colapsable (desktop)

- Patrón de referencia: [`MAT.MVC/Scripts/mat.admin-nav.js`](../MAT.MVC/Scripts/mat.admin-nav.js) + `localStorage` (`mat.main.sidebar.collapsed`).
- Modo iconos (64px) con tooltips; botón en header o footer del sidebar.
- Ajustar `#content` margin-left y `--sidebar-width` dinámico.

### Fase 3 — Búsqueda global

- Atajo `Ctrl+K` / campo en header.
- Índice de rutas desde `QuickAccessCatalog` + entradas del sidebar.
- Modal o paleta de comandos (estilo command palette).

### Fase 4 — Accesos personalizables

- Preferencias por usuario/rol (BD o tabla de preferencias).
- Botón "Personalizar" en sección de accesos frecuentes.
- Orden drag-and-drop opcional.

### Fase 5 — Badges dinámicos

- Contadores reales en tiles o sidebar (ej. presupuestos en seguimiento pendientes).
- Requiere endpoints ligeros o reutilizar KPIs del hero.

### Fase 6 — Sección "Recientes"

- Historial de últimas 4–6 rutas visitadas (`localStorage` o server-side).
- Fila opcional bajo accesos frecuentes.

### Fase 7 — Rol real en menú de usuario

- Hoy `_LoginPartial.cshtml` muestra "Administrador" fijo.
- Resolver rol desde membership / perfil vendedor.

### Fase 8 — Viajes y Paquetes en sidebar

- Decisión de producto: agregar bajo **Operaciones** en `_MainNav.cshtml`.
- Evitar duplicar con accesos frecuentes sin criterio claro.

---

## Smoke test manual

1. Home: 6 tiles navegan correctamente.
2. Home: "Todos los módulos" expande/colapsa.
3. Home: header muestra "Panel Principal" (sin breadcrumb "Inicio").
4. `/Presupuesto/Index`: breadcrumb + Volver OK.
5. Sidebar: submenús y ítem activo al navegar.
6. Mobile: hamburguesa + overlay sin regresiones.
7. Ctrl+F5 por cache bust `modern-dashboard.css?v=20260705d`.

---

## Archivos principales

| Archivo | Rol |
|---------|-----|
| `Infrastructure/QuickAccessCatalog.cs` | Datos de accesos |
| `Views/Home/_QuickAccess.cshtml` | UI accesos frecuentes |
| `Views/Shared/_MainNav.cshtml` | Sidebar agrupado |
| `Views/Shared/_Breadcrumb.cshtml` | Título en home |
| `Content/modern-dashboard.css` | Tiles |
| `Content/modern-topbar.css` | Título navbar |
| `Content/modern-layout.css` | Altura header |
| `Content/modern-menu.css` | Labels sidebar |
