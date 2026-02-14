# Guía de Impresión PDF - Una Sola Página

## Descripción General

Sistema de impresión a PDF que genera el contenido en **una sola página**, ajustando automáticamente el tamaño del papel al contenido. Incluye un **selector de escala** para que el usuario controle el nivel de reducción antes de imprimir.

Implementación de referencia: **Hotel/Distribución** (`Distribucion.cshtml` + `mat.esquemadistribucion.print.css`).

---

## Arquitectura

```
┌─────────────────────────────────────────────┐
│  Vista (.cshtml)                            │
│  ├── Selector de escala (#print-scale)      │
│  ├── Botón Imprimir                         │
│  └── JS: mide contenido → inyecta @page    │
│         dinámico → window.print()           │
├─────────────────────────────────────────────┤
│  CSS de impresión (.css, media="print")     │
│  ├── Oculta layout (navbar, sidebar, etc.)  │
│  ├── Reset vertical (margin/padding 0)      │
│  ├── Elimina saltos de página               │
│  ├── transform: scale() por defecto         │
│  └── Estilos compactos para el contenido    │
└─────────────────────────────────────────────┘
```

---

## Paso a Paso para Implementar en una Nueva Página

### 1. Crear el archivo CSS de impresión

Crear `Content/mat.[nombre].print.css`. Estructura base:

```css
/* @page sin tamaño fijo: el JS lo inyecta dinámicamente */
@page {
    margin: 0;
}

@media print {

    /* ── RESET GLOBAL ── */
    * {
        box-sizing: border-box;
        margin: 0;
        padding: 0;
        page-break-before: auto !important;
        page-break-after: auto !important;
        page-break-inside: auto !important;
        break-before: auto !important;
        break-after: auto !important;
        break-inside: auto !important;
    }

    /* ── OCULTAR LAYOUT ── */
    /* Estas reglas son OBLIGATORIAS para quitar navbar, sidebar y alertas */
    .navbar.main,
    #menu,
    .sidebar-overlay,
    .hidden-print,
    .mobile-menu-toggle {
        display: none !important;
        visibility: hidden !important;
    }

    .container-fluid.fixed.menu-left {
        padding: 0 !important;
        margin: 0 !important;
    }

    #wrapper {
        padding-top: 0 !important;
        display: block !important;
        min-height: 0 !important;
    }

    #content {
        margin-left: 0 !important;
        padding: 0 !important;
        width: 100% !important;
        max-width: 100% !important;
    }

    .modern-alerts-host {
        display: none !important;
    }

    /* ── BASE TIPOGRÁFICA ── */
    html, body {
        background: #fff;
        margin: 0;
        padding: 0;
        font-size: 4.5pt;        /* Ajustar según densidad de contenido */
        line-height: 0.95;
        font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Arial, sans-serif;
        -webkit-print-color-adjust: exact;
        print-color-adjust: exact;
    }

    /* ── OCULTAR ELEMENTOS INTERACTIVOS ── */
    .btn-print,
    .print-controls,
    .print-scale-group,
    .modal,
    .loading-container,
    .loading-spinner {
        display: none !important;
    }

    /* ── CONTENEDOR PRINCIPAL ── */
    /* Escala por defecto 80%. El JS puede sobreescribirlo. */
    /* width = 100/scale (ej: 100/0.8 = 125%) para compensar. */
    .mi-contenedor-principal {
        width: 125%;
        max-width: none;
        margin: 0;
        padding: 0;
        box-shadow: none;
        border: none;
        border-radius: 0;
        transform: scale(0.8);
        transform-origin: top left;
        overflow: visible !important;
        -webkit-print-color-adjust: exact;
        print-color-adjust: exact;
    }

    /* ── ESTILOS ESPECÍFICOS DE LA PÁGINA ── */
    /* Aquí van los estilos propios del contenido a imprimir */
}
```

### 2. Referenciar el CSS en la vista

En la sección `@section Styles` del `.cshtml`:

```html
@section Styles {
    <link href="~/Content/mat.[nombre].print.css" rel="stylesheet" media="print" />
}
```

> **Importante**: usar `media="print"` para que solo aplique al imprimir.

### 3. Agregar el selector de escala en la toolbar

```html
<div class="print-controls">
    <div class="print-scale-group">
        <label for="print-scale" class="print-scale-label">Escala:</label>
        <select id="print-scale" class="print-scale-select">
            <option value="1.0">100%</option>
            <option value="0.95">95%</option>
            <option value="0.9">90%</option>
            <option value="0.85">85%</option>
            <option value="0.8" selected>80%</option>
            <option value="0.75">75%</option>
            <option value="0.7">70%</option>
            <option value="0.65">65%</option>
            <option value="0.6">60%</option>
        </select>
    </div>
    <button id="btn-print" type="button" class="btn-print">
        <i class="bi bi-printer-fill"></i> Imprimir
    </button>
</div>
```

### 4. JavaScript de impresión

Dentro de `@section Scripts`, agregar la lógica del botón imprimir.
**Reemplazar `.mi-contenedor-principal`** por la clase CSS del contenedor de la página.

```javascript
$(document).on("click", "#btn-print", function () {
    var scale = parseFloat($("#print-scale").val()) || 0.8;
    var widthPct = (100 / scale).toFixed(2);
    var $page = $(".mi-contenedor-principal");

    // Medir contenido real (sin transform) para calcular tamaño de página
    var origTransform = $page.css("transform");
    var origWidth = $page.css("width");
    $page.css({ "transform": "none", "width": "100%" });

    var contentW = $page[0].scrollWidth;
    var contentH = $page[0].scrollHeight;

    // Restaurar estilos
    $page.css({ "transform": origTransform, "width": origWidth });

    // Calcular dimensiones en mm (1px = 1/3.7795 mm a 96dpi)
    var pageWmm = Math.ceil((contentW * scale) / 3.7795) + 6;
    var pageHmm = Math.ceil((contentH * scale) / 3.7795) + 6;

    // Inyectar @page dinámico (escapar @ para Razor)
    var styleId = "print-scale-dynamic";
    $("#" + styleId).remove();
    var atPage = String.fromCharCode(64) + 'page';
    $("head").append(
        '<style id="' + styleId + '" media="print">' +
        atPage + ' { size: ' + pageWmm + 'mm ' + pageHmm + 'mm; margin: 3mm; } ' +
        '.mi-contenedor-principal { ' +
            'transform: scale(' + scale + ') !important; ' +
            'transform-origin: top left !important; ' +
            'width: ' + widthPct + '% !important; ' +
        '}' +
        '</style>'
    );

    window.print();
});
```

---

## Reglas Clave

### Ocultar el layout

Estas reglas son **obligatorias** en todo CSS de impresión del proyecto para eliminar la navbar y el sidebar:

| Selector | Razón |
|---|---|
| `.navbar.main` | Barra superior con logo y breadcrumb |
| `#menu` | Menú lateral (sidebar) |
| `.sidebar-overlay` | Overlay móvil del sidebar |
| `.hidden-print` | Elementos ya marcados como ocultos en impresión |
| `#wrapper { padding-top: 0 }` | Quita espacio reservado para la navbar fija |
| `#content { margin-left: 0 }` | Quita espacio reservado para el sidebar (260px) |
| `.modern-alerts-host` | Notificaciones/alertas del sistema |

### Evitar saltos de página

```css
* {
    page-break-before: auto !important;
    page-break-after: auto !important;
    page-break-inside: auto !important;
    break-before: auto !important;
    break-after: auto !important;
    break-inside: auto !important;
}
```

> **NUNCA usar** `break-inside: avoid` ni `page-break-inside: avoid` en los contenedores.
> Esto causa que el navegador empuje bloques enteros a la siguiente página.

### Preservar colores en impresión

Agregar en todo elemento con fondo de color:

```css
-webkit-print-color-adjust: exact;
print-color-adjust: exact;
```

### Escala dinámica con transform

La fórmula es:
- `transform: scale(S)` donde S es la escala (ej: 0.8 = 80%)
- `width: (100/S)%` para compensar (ej: 125% para S=0.8)
- `transform-origin: top left` para que escale desde la esquina superior izquierda

### Escapar `@page` en Razor (.cshtml)

Razor interpreta `@page` como código C#. Soluciones según contexto:

| Contexto | Solución |
|---|---|
| En bloque `<style>` inline | Usar `@@page { ... }` |
| En `@@keyframes` | Usar `@@keyframes nombre { ... }` |
| En JavaScript (string) | Usar `String.fromCharCode(64) + 'page'` |
| En archivo `.css` externo | No hace falta escapar (Razor no lo procesa) |

---

## Estilos del Selector de Escala (para pantalla)

Agregar en el `<style>` de la vista:

```css
.print-controls {
    display: flex;
    align-items: center;
    gap: 12px;
}

.print-scale-group {
    display: flex;
    align-items: center;
    gap: 6px;
}

.print-scale-label {
    font-weight: 600;
    color: #1e3a5f;
    font-size: 13px;
    white-space: nowrap;
}

.print-scale-select {
    padding: 8px 30px 8px 10px;
    font-size: 13px;
    border: 2px solid #e0e0e0;
    border-radius: 6px;
    background: white;
    color: #333;
    cursor: pointer;
    appearance: none;
    background-image: url("data:image/svg+xml,..."); /* flecha dropdown */
    background-repeat: no-repeat;
    background-position: right 8px center;
}
```

---

## Referencia: Valores Usados en Distribución de Hoteles

| Propiedad | Valor |
|---|---|
| Escala por defecto | 0.8 (80%) |
| Fuente base body | 4.5pt |
| Fuente tablas | 4.2pt |
| Fuente headers tabla | 4pt |
| line-height global | 0.95 |
| Margen @page | 0 (base) / 3mm (dinámico) |
| Color header | #1a3a5e (azul oscuro) |
| Color secciones | #e91e63 (magenta) |
| Color tutor | #7c3aed (violeta) |

---

## Checklist para Nueva Página de Impresión

- [ ] Crear archivo `Content/mat.[nombre].print.css`
- [ ] Copiar bloque de reset global y ocultamiento de layout
- [ ] Agregar `<link>` con `media="print"` en `@section Styles`
- [ ] Agregar selector de escala + botón imprimir en la toolbar
- [ ] Agregar estilos CSS del selector (pantalla)
- [ ] Agregar JS del botón imprimir con medición dinámica de contenido
- [ ] Usar `String.fromCharCode(64) + 'page'` en el JS para escapar Razor
- [ ] Agregar `-webkit-print-color-adjust: exact` en elementos con fondo de color
- [ ] NO usar `break-inside: avoid` en contenedores
- [ ] Probar con distintas escalas (80%, 75%, 70%)
