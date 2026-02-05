# Plan: CSS prolijo y optimizado (sin estilos viejos)

**Proyecto:** MAT  
**Objetivo:** Dejar el uso de CSS prolijo y optimizado; en esta etapa ya no se utilizan los CSS viejos.

---

## Objetivo general

- Un solo criterio de estilos: **variables (`saas-variables.css`) + componentes `modern-*`**.
- Carga de CSS **centralizada y sin duplicados**.
- **Cero (o mínimo) inline** y **poco o nada de `!important`**.
- **Deprecar/eliminar** los CSS viejos que ya no se usen.

---

## Principio rector (aplicar en todas las fases)

**En cada fase se debe tener en cuenta:**

- Se usa **únicamente la interfaz nueva** (componentes y estilos `modern-*`, variables de `saas-variables.css`).
- **No se usa nada de los estilos viejos**: no agregar ni mantener referencias a `mat.styles.custom.css`, `custom_*`, `mat.forms`, `tabla.css`, `preset.css` ni a ningún CSS legacy como fuente de diseño.
- Cualquier cambio o nueva pantalla debe implementarse desde el inicio con la interfaz nueva; si se encuentra uso de estilos viejos, se migra a la nueva interfaz en el marco de esa fase.

---

## Fase 1: Inventario y estándar (1–2 días)

**Criterio:** Solo interfaz nueva; ningún estilo viejo en uso. El inventario sirve para detectar qué migrar o eliminar, no para seguir usando legacy.

### 1.1 Inventario de dependencias

- Listar en qué vistas/layouts se usa cada archivo “viejo”:
  - `mat.styles.custom.css` (clases `custom_*`, etc.)
  - `mat.forms.css`, `mat.perfiles.css`, `tabla.css`, `preset.css`
  - Cualquier otro que se considere legacy.
- Para cada vista que use clases de esos archivos, anotar: **nombre de vista** + **clases usadas** (búsqueda por `class="custom_` y por nombres de archivo en comentarios o en el layout que usa cada vista).

### 1.2 Definir el estándar

- **Paleta y tokens:** solo `saas-variables.css` (ampliarlo si hace falta).
- **Componentes/patrones:** solo archivos `modern-*.css` (layout, menu, dashboard, topbar, datatable, account, etc.).
- **Utilidades:** si hace falta, un solo archivo, p. ej. `Content/modern-utilities.css`, con clases tipo `u-hidden`, `u-display-block`, `u-padding-lg`, etc., **sin `!important`** siempre que se pueda.
- **Third-party:** jQuery UI, Bootstrap, Bootstrap Icons, DataTables, timepicker: mantener solo los que se usen; carga desde un único lugar (layout o bundle).

**Entregable:** Documento/listado “Qué vista usa qué CSS viejo” y “Estándar de CSS del proyecto” (1 página).

- **Completado:** Ver `DOCUMENTACION/INVENTARIO_CSS_FASE1.md` y `DOCUMENTACION/ESTANDAR_CSS_PROYECTO.md`.

---

## Fase 2: Unificar carga de CSS (1 día)

**Criterio:** Solo interfaz nueva; en los bundles solo entran CSS de la nueva interfaz (`modern-*`, `saas-variables`) y third-party necesarios. No incluir archivos de estilos viejos.

### 2.1 Un solo punto de carga por layout

- **`_Layout.cshtml` (principal):**
  - Quitar todos los `<link>` sueltos.
  - Crear en `BundleConfig.cs` bundles de CSS, por ejemplo:
    - `~/Content/css/core` → Site.css, saas-variables, bootstrap, bootstrap-icons, jQuery UI, timepicker, dataTable.
    - `~/Content/css/modern` → modern-layout, modern-menu, modern-dashboard, modern-topbar, modern-datatable (y si aplica modern-account para páginas de cuenta).
  - En `_Layout.cshtml` dejar solo:
    - `@Styles.Render("~/Content/css/core")`
    - `@Styles.Render("~/Content/css/modern")`
  - Incluir en el bundle “core” o “modern” los CSS que hoy se cargan a mano (mat.busplugin, voucher, mat.forms, etc.) **solo si siguen en uso**; si no, no incluirlos (se quitarán en Fase 5).

- **`_LayoutAdmin.cshtml`:**
  - Misma idea: uno o dos bundles solo con lo que Admin necesita (p. ej. admin.modern.css + core mínimo).

- **`_LayoutSplash.cshtml`:**
  - Un bundle específico para splash (solo los CSS que realmente use esa pantalla).

### 2.2 Quitar duplicados en vistas

- Quitar de **todas** las vistas los `<link>` que ya estén en el layout que usan (p. ej. `mat.styles.custom.css` en Reserva/Index, PasajeroViaje/Manifiesto, etc.).
- Si una vista necesita un CSS **solo para ella** (p. ej. print o un módulo muy específico), usar la sección `@section Styles { ... }` y que el layout la renderice en `<head>`, en lugar de poner `<link>` en medio del body.

### 2.3 Un solo origen para Bootstrap Icons

- Dejar **solo** el `bootstrap-icons.css` local (o solo CDN), no ambos. Quitar el `<link>` a CDN de `Hotel/Distribucion.cshtml` y `PasajeroViaje/ListadoSimple.cshtml` si el layout ya incluye el mismo CSS.

**Entregable:** Carga de CSS solo vía bundles y secciones; cero `<link>` duplicados entre layout y vistas.

---

## Fase 3: Sustituir estilos inline y uso de CSS desde JS (2–3 días)

**Criterio:** Solo interfaz nueva; al reemplazar inline o `.css()` en JS se usan únicamente clases de la nueva interfaz (`modern-*`, `u-*` en `modern-utilities.css`). No reintroducir estilos viejos ni clases `custom_*`.

### 3.1 Vistas (CSHTML)

- Para cada vista con `style="..."`:
  - **Display/visibilidad:** reemplazar por clases, p. ej. `u-hidden` / `u-visible` (definidas en `modern-utilities.css` o en el CSS del componente). Usar esas clases en el HTML y, si hace falta mostrar/ocultar por JS, hacer `element.classList.add/remove('u-hidden')` en lugar de `.css('display', ...)`.
  - **Dimensiones (width/height), márgenes, padding:** mover a clases de componente o de utilidad (p. ej. `modern-detail-value--pre-wrap`, `th--w250`).
  - **Colores (rojo para “*”, etc.):** usar variables y clases, p. ej. `class="text-danger"` o `class="u-required"` con color desde `saas-variables`.
- Priorizar vistas con más inline: DetalleFactura, Presupuesto/Seguimiento, NotaCreditoList, Paquete/Edit, Reserva/SeleccionarPasajero, PersonaCliente (Index, RegistrarNotaCredito, etc.).

### 3.2 JavaScript

- **`mat.jquery.binding.js`** (y cualquier otro que toque estilos):
  - Sustituir `.css("display", "none")` / `"inline-block"` por añadir/quitar clases (p. ej. `u-hidden` / `u-inline-block`).
  - El toast: en lugar de armar el HTML con `style="..."`, definir una clase (p. ej. `.modern-toast`) en un `modern-*.css` y construir el DOM solo con clases; opcionalmente un `.modern-toast--visible` para la animación de opacidad.
- **`global.js`:** reemplazar `.css({ ... })` por clases (p. ej. panel colapsado/expandido con una clase).
- **Paquete/Edit.cshtml (script inline):** mismo criterio: clases para inputs transparentes, bordes, etc., y desde JS solo togglear clases.

**Entregable:** Sin `style="..."` en vistas (salvo excepciones muy justificadas y documentadas); JS sin `.css()` para layout/visibilidad, solo cambio de clases.

---

## Fase 4: Reducir !important y consolidar “custom” (1–2 días)

**Criterio:** Solo interfaz nueva; las migraciones desde `custom_*` van a clases `modern-*` o `u-*`. No se mantienen ni extienden los estilos viejos.

### 4.1 modern-dashboard.css (y otros modern-*)

- Revisar cada bloque que use `!important`. En muchos casos es para “matar” estilos de Bootstrap o de DataTables.
  - **Opción A:** subir especificidad con una clase contenedora (p. ej. `.modern-dashboard .modern-card ...`) en lugar de `!important`.
  - **Opción B:** si el estilo conflictivo viene de un tercero, cargar el override **después** del CSS de ese tercero y usar el mismo selector (o más específico) sin `!important`; si aun así no gana, entonces dejar 1–2 `!important` puntuales y comentar “Override DataTables/Bootstrap”.
- Objetivo: que la mayoría de reglas no lleve `!important`.

### 4.2 mat.styles.custom.css y clases custom_*

- Si en Fase 1 se confirmó que ya no se necesitan “CSS viejos”:
  - No agregar nada nuevo a `mat.styles.custom.css`.
  - Para cada clase `custom_*` que **sí** siga en uso en alguna vista/JS:
    - Migrar esa regla a `modern-utilities.css` (o al `modern-*.css` del componente) con un nombre nuevo (p. ej. `u-font-size-14`, `u-width-70`) y **sin** `!important` si es posible.
  - Ir reemplazando en vistas/JS las referencias `custom_*` por las nuevas clases.
- Cuando ninguna vista ni script use ya `mat.styles.custom.css`, pasará a Fase 5 (eliminación).

**Entregable:** `!important` reducido a los mínimos necesarios; clases “custom” migradas a utilidades/componentes modernos y referencias actualizadas.

---

## Fase 5: Deprecar y eliminar CSS viejo (1 día)

**Criterio:** Solo interfaz nueva; al finalizar esta fase la aplicación no debe cargar ni depender de ningún estilo viejo. Toda la UI se apoya en la nueva interfaz.

### 5.1 Dejar de cargar legacy

- En los bundles (o en el layout si aún hay algún link suelto), **quitar** la carga de:
  - `mat.styles.custom.css` (cuando ya no haya referencias).
  - Cualquier otro archivo marcado como “viejo y no usado”: `mat.forms.css`, `mat.perfiles.css`, `tabla.css`, `preset.css`, etc., según el inventario de Fase 1.
- Mantener solo lo que corresponda al estándar: `saas-variables`, `modern-*`, y los third-party necesarios.

### 5.2 Limpieza de archivos

- Mover los CSS que ya no se cargan a una carpeta tipo `Content/legacy/` o eliminarlos del proyecto si se está seguro de que no se usan en ningún entorno.
- Actualizar el `.csproj` si esos archivos estaban incluidos como contenido.

### 5.3 Pruebas

- Recorrer: layout principal, layout Admin, layout Splash, y las vistas más críticas (Reserva, Factura, PersonaCliente, Presupuesto, Paquete, etc.).
- Comprobar que no queden 404 de CSS ni estilos rotos.
- Verificar que los estilos que antes eran “custom” o inline se vean correctamente con las nuevas clases.

**Entregable:** Proyecto sin carga de CSS viejo; estructura de CSS clara y única.

---

## Orden sugerido y tiempos

| Fase | Descripción | Orden |
|------|-------------|-------|
| 1 | Inventario + estándar | Primero |
| 2 | Unificar carga (bundles, sin duplicados) | Segundo |
| 3 | Quitar inline en vistas y JS | Tercero |
| 4 | Reducir !important y migrar custom_* | Cuarto |
| 5 | Deprecar y eliminar CSS viejo | Quinto |

**Tiempo estimado total:** 6–9 días según tamaño del equipo y cantidad de vistas.

---

## Resumen de “uso adecuado” al final

- **Carga:** un lugar por layout (bundles); vistas solo con `@section Styles` cuando necesiten un CSS propio.
- **Estilos:** solo en archivos CSS (variables + modern-* + utilidades); sin inline ni `.css()` para layout/visibilidad.
- **Especificidad:** casi sin `!important`; preferir selectores más específicos o orden de carga.
- **Nomenclatura:** una convención clara (p. ej. `modern-*` componentes, `u-*` utilidades) y sin mezcla con `custom_*` legacy.
