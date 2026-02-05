# Estándar de CSS del proyecto MAT

**Vigente desde:** Fase 1 (Febrero 2026).  
**Principio:** Solo interfaz nueva; no se usan estilos viejos (`custom_*`, mat.styles.custom, mat.forms, tabla, preset).

---

## 1. Paleta y tokens

- **Única fuente de variables:** `Content/saas-variables.css`.
- Usar siempre variables para colores, bordes y radios:
  - `var(--brand-primary)`, `var(--brand-dark)`, `var(--bg-main)`, `var(--text-primary)`, `var(--text-secondary)`, `var(--text-muted)`, `var(--border-color)`, `var(--white)`, `var(--card-shadow)`, `var(--border-radius-lg)`.
- No definir colores ni sombras “a mano” en otros CSS; ampliar `saas-variables.css` si hace falta.

---

## 2. Componentes y patrones

- **Prefijo de componentes:** `modern-*`.
- **Archivos:** Solo los CSS de la nueva interfaz:
  - `modern-layout.css`, `modern-menu.css`, `modern-dashboard.css`, `modern-topbar.css`, `modern-datatable.css`, `modern-account.css`.
- Nuevos componentes: mismo prefijo `modern-` y uso de variables. Ej.: `.modern-card`, `.modern-form-group`, `.modern-form-label`, `.modern-table`.

---

## 3. Utilidades

- **Archivo:** `Content/modern-utilities.css` (crear si no existe).
- **Prefijo:** `u-` (ej.: `u-hidden`, `u-block`, `u-flex`, `u-mt-2`, `u-p-2`, `u-text-right`, `u-font-bold`).
- Sin `!important` salvo excepción documentada. Preferir especificidad o orden de carga.

---

## 4. Third-party

- **Incluir solo lo necesario:** jQuery UI (datepicker/dialog si se usa), Bootstrap 5, Bootstrap Icons, DataTables, timepicker.
- **Un solo origen por librería:** o todo local o todo CDN; no mezclar. Preferir local.
- Cargar desde layout/bundles, no desde vistas sueltas.

---

## 5. Carga de CSS

- **Por layout:** Bundles en `BundleConfig.cs` (p. ej. `~/Content/css/core`, `~/Content/css/modern`); en el layout solo `@Styles.Render(...)`.
- **Por vista:** Si una vista necesita un CSS propio (p. ej. impresión o módulo muy específico), usar `@section Styles { <link ...> }` y que el layout renderice la sección en `<head>`.
- No duplicar `<link>` entre layout y vista.

---

## 6. Nomenclatura y buenas prácticas

- **Clases:** minúsculas, guiones (`modern-card-title`, `u-mt-2`). No usar `custom_*` ni estilos viejos.
- **Evitar:** estilos inline (`style="..."`) y en JS (`.css(...)`) para layout/visibilidad; usar clases y togglear clases desde JS.
- **Evitar:** `!important`; usar selectores más específicos o orden de carga.

---

## 7. Resumen

| Qué | Dónde / Cómo |
|-----|----------------|
| Variables | Solo `saas-variables.css` |
| Componentes | Solo `modern-*.css` |
| Utilidades | `modern-utilities.css`, prefijo `u-` |
| Third-party | Bundles/layout, un origen por lib |
| Carga | Bundles por layout; vistas con `@section Styles` si hace falta |
| Estilos viejos | No usar; migrar a interfaz nueva |
