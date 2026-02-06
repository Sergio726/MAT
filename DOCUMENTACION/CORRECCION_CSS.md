# Correcciones y Sugerencias al Estandar CSS del Proyecto MAT

**Fecha:** Febrero 2026
**Referencia:** `ESTANDAR_CSS_PROYECTO.md`

---

## 1. Responsive Breakpoints

El estandar no define tokens para breakpoints de media queries. Se recomienda documentar los anchos estandar en `saas-variables.css` o en el propio estandar.

**Propuesta:**

```css
/* Content/saas-variables.css */
:root {
  --breakpoint-sm: 576px;
  --breakpoint-md: 768px;
  --breakpoint-lg: 992px;
  --breakpoint-xl: 1200px;
}
```

> Nota: Las custom properties no se pueden usar directamente en `@media`, pero documentarlas como referencia asegura que todo el equipo use los mismos valores.

---

## 2. Convencion para Estados Interactivos

Falta definir como nombrar estados de componentes. Sin una convencion explicita, se pueden mezclar estilos como `.modern-btn.active`, `.modern-btn-active` y `.modern-btn--active`.

**Propuesta:** Usar doble guion para modificadores de estado:

| Estado | Clase |
|--------|-------|
| Activo | `.modern-btn--active` |
| Deshabilitado | `.modern-card--disabled` |
| Cargando | `.modern-table--loading` |
| Abierto | `.modern-menu--open` |

Esto es compatible con BEM parcial y se distingue claramente de los componentes base.

---

## 3. Preparacion para Dark Mode

Aunque no se implemente ahora, las variables ya facilitan un futuro dark mode. Se sugiere documentar esta posibilidad y agrupar las variables de color de forma que el cambio sea sencillo.

**Propuesta futura:**

```css
/* Content/saas-variables.css */
:root {
  --bg-main: #f5f7fa;
  --text-primary: #1a1a2e;
  /* ... */
}

[data-theme="dark"] {
  --bg-main: #1a1a2e;
  --text-primary: #e0e0e0;
  /* ... */
}
```

> No requiere accion inmediata, solo tener en cuenta la estructura de variables para no bloquear esta opcion.

---

## 4. Orden de Propiedades CSS

Definir un orden estandar de propiedades dentro de cada regla mejora la legibilidad y reduce conflictos en diffs.

**Propuesta de orden:**

1. Posicionamiento (`position`, `top`, `right`, `z-index`)
2. Display y layout (`display`, `flex`, `grid`, `align-items`)
3. Box model (`width`, `height`, `margin`, `padding`, `border`)
4. Tipografia (`font-size`, `font-weight`, `line-height`, `color`)
5. Visual (`background`, `box-shadow`, `opacity`, `border-radius`)
6. Transiciones y animaciones (`transition`, `animation`)

---

## 5. Crear `modern-utilities.css` de Inmediato

El estandar menciona "crear si no existe", lo cual deja ambiguedad. Se recomienda crear el archivo con las utilidades base desde ahora para que sea referencia concreta.

**Utilidades base sugeridas:**

```css
/* Content/modern-utilities.css */

/* Visibilidad */
.u-hidden { display: none; }
.u-block  { display: block; }
.u-flex   { display: flex; }
.u-inline { display: inline; }

/* Espaciado (margin-top) */
.u-mt-1 { margin-top: 0.25rem; }
.u-mt-2 { margin-top: 0.5rem; }
.u-mt-3 { margin-top: 1rem; }
.u-mt-4 { margin-top: 1.5rem; }

/* Espaciado (padding) */
.u-p-1 { padding: 0.25rem; }
.u-p-2 { padding: 0.5rem; }
.u-p-3 { padding: 1rem; }
.u-p-4 { padding: 1.5rem; }

/* Texto */
.u-text-left   { text-align: left; }
.u-text-center { text-align: center; }
.u-text-right  { text-align: right; }
.u-font-bold   { font-weight: 700; }
.u-font-normal { font-weight: 400; }

/* Overflow */
.u-overflow-hidden { overflow: hidden; }
.u-overflow-auto   { overflow: auto; }
```

---

## Resumen de Acciones

| # | Accion | Prioridad | Archivo afectado |
|---|--------|-----------|------------------|
| 1 | Documentar breakpoints | Media | `saas-variables.css` / estandar |
| 2 | Definir convencion de estados (`--modifier`) | Alta | Estandar CSS |
| 3 | Preparar estructura para dark mode | Baja | `saas-variables.css` |
| 4 | Definir orden de propiedades | Baja | Estandar CSS |
| 5 | Crear `modern-utilities.css` con utilidades base | Alta | `Content/modern-utilities.css` |
