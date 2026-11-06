# Plan — Mejoras del Panel Principal (`/Home/Index`)

Origen: auditoría UX/UI del 06/11/2026 (`mejoras-panel-principal-mat.md`). Este documento la ordena en **bloques entregables
por separado**, cada uno en su PR y verificable en `/` antes del siguiente. Estado de avance: `docs/PENDIENTES.md`.

## Estado real verificado en el código (07/11/2026)

La auditoría se hizo mirando la pantalla; algunos puntos ya estaban resueltos o eran distintos:

| Punto de la auditoría | Estado real en `MAT2026` |
|---|---|
| Campana "sin badge, ícono muerto" | **Ya funciona:** número de no leídos y avisos reales (PR #43). Se veía sin número porque el usuario ya había leído todo. No hay que ocultarla. |
| Menú de usuario "falta" | **Ya existe:** desplegable con nombre, rol y "Cerrar sesión" (`_LoginPartial`). No hay dato de "sucursal" en el sistema. |
| Sidebar sin estado activo | Hay un resaltado del ítem actual; falta `aria-current`, que el padre del submenú quede marcado y unificar el estilo. |
| "Saludar" solo navega | Confirmado (`_CumpleaniosHoy`): `<a href="/Seguimiento">`. |
| "y 31 más…" sin enlace | Confirmado (`mat.seguimiento.js`, recuadro del inicio): `<li>` sin link. |
| Barra de ocupación vs. número | Confirmado: el número grande dice **libres** y la barra se llena con **ocupados** (vendidos + pre-reservas). |
| Ícono `$` en Butacas | Confirmado (`bi-currency-dollar` en `_MainNav`). |
| Módulos para el buscador | Ya están en `QuickAccessCatalog` (13 destinos): el buscador puede arrancar sin tocar la base. |
| Accesos frecuentes al pie | Confirmado: `Index` = hero → Próximas salidas → Cumpleaños → Accesos. |

## Bloques

Orden pensado para entregar valor rápido con poco riesgo: primero lo que corrige errores de lectura y no toca el layout
global; después la reorganización del inicio; al final lo que cambia todas las pantallas (sidebar, barra superior).

### Bloque A — Correcciones de lectura en el inicio · **próximo release**
Riesgo bajo · solo vistas, JS y CSS del inicio · sin base de datos · ~0,5 día

- **Próximas salidas:** una sola métrica — barra = % ocupado (vendidos + pre-reservas sobre capacidad), texto
  "5 de 60 vendidos · 55 libres"; `role="progressbar"` con `aria-valuenow/min/max`.
- **Badge solo en estados excepcionales:** Últimos lugares (≤ 15 % libre, mínimo 1), Completo. Estado normal sin badge.
- **3 salidas visibles** + "Ver N más" y "Ver todas".
- **Cumpleaños:** "Saludar" → "Ver seguimiento"; "y N más…" pasa a ser enlace a Seguimiento.
- **KPIs del hero:** `aria-label` (el `title` queda como tooltip), pie "Ver detalle ›" visible sin hover, y con valor 0
  un texto de contexto ("Sin presupuestos este mes") en vez de "Total: $0,00".

Aceptación: barra y texto cuentan lo mismo; "Disponible" no aparece en viajes normales; el botón no dice "Saludar".

### Bloque B — Reorganización del inicio · **próximo release**
Riesgo bajo-medio · `Index`, parciales del inicio y `modern-dashboard.css` · sin base de datos · ~1 día

- **Orden:** Accesos frecuentes → KPIs → dos columnas (Próximas salidas `2fr` · Cumpleaños `1fr`, desde 1200 px; apiladas
  debajo, salidas primero).
- **Accesos frecuentes compactos:** una fila de 6 chips (ícono 20 px + texto, sin subtítulo), 3 columnas < 1200 px,
  2 < 768 px. Acciones (Nuevo cliente) con fondo de acento suave; navegación con estilo neutro. Mismos `href` de hoy.
- **Cumpleaños colapsado** por defecto ("37 cumpleaños · 37 sin saludar"), estado recordado en `localStorage`.
- **Un solo lenguaje de tarjeta y de encabezado** en las tres secciones (tokens de `saas-variables.css`, sin borde gris
  de 1 px, ícono bootstrap monocromo + título + contador).

Aceptación: en 1366×768 se ven sin scroll los 6 accesos y los KPIs; todas las rutas siguen igual.

### Bloque C — Sidebar · release siguiente
Riesgo medio (**afecta todas las pantallas**) · `_MainNav`, `modern-menu.css`, `common.js` · ~1 día

- Ítem activo claro (fondo de acento 8 %, barra izquierda 3 px, texto 600) + `aria-current="page"`; el padre del
  submenú activo queda marcado más tenue.
- Densidad: ítems de 38 px, encabezados de grupo más chicos; el menú completo entra en 768 px de alto.
- Chevron que rota al abrir el submenú, con `aria-expanded` sincronizado.
- Butacas con `bi-ticket-perforated`; íconos del mismo grosor; borde sutil entre menú y contenido.

### Bloque D — Barra superior con buscador global · release siguiente
Riesgo medio (**todas las pantallas**) · `_Layout`, `modern-topbar.css`, JS nuevo · ~2 días

- Buscador central "Buscar cliente, viaje, factura o módulo…" con `Ctrl+K`, ↑ ↓ Enter Esc.
  **Etapa 1:** módulos (`QuickAccessCatalog` + menú) — "fact" lleva a `/Factura/Index`. Resultados agrupados por tipo,
  listos para la etapa 2.
- Botón primario "+ Nuevo presupuesto" (`/Presupuesto/Create`).
- Toggle de sidebar colapsado a rail de 64 px (íconos + tooltip), preferencia en `localStorage`.
- En el inicio, breadcrumb en vez del título fijo "Panel Principal".

### Bloque E — KPIs con tendencia y hero sin foto · release siguiente
Riesgo bajo-medio · `HomeController.GetEstadisticasMesActual`, `_DashboardHero` · ~1 día

- Comparación con el mes anterior ("▲ +12 % vs octubre"), con los mismos SP de reportes y el rango del mes anterior.
- Tira de KPIs sobre superficie neutra (sin foto): gana legibilidad y ~80 px. Si se conserva la foto, overlay oscuro
  y label al 88 % hasta 4,5:1.

### Bloque F — Retirar "Todos los módulos" · después de D
Riesgo bajo · ~0,5 día

- Con el buscador funcionando, quitar el acordeón del pie. Antes, verificar que cada destino siga alcanzable desde el
  menú lateral o el buscador: `/Presupuesto/Index`, `/Viaje/Index`, `/Paquete/Index`, `/PersonaCliente/Index`,
  `/PersonaCliente/NotaCreditoList`, métricas de presupuestos, `/Home/HistorialPagos`.

### Bloque G — Buscador: clientes, viajes y facturas · a definir
Riesgo medio · reusa `usp_MAT_PersonaCliente_Buscar` (F2) y búsquedas existentes de viajes y facturas · ~2 días

## Reglas para todos los bloques

1. Colores, radios, sombras y espacios desde `saas-variables.css`; nada fijo en el código.
2. Un solo lenguaje de tarjeta y un solo patrón de encabezado de sección.
3. Foco visible, contraste AA, `aria-label` donde el texto no alcance, teclado completo.
4. **No cambiar rutas:** todos los `href` actuales se conservan.
5. Versionar (`?v=`) cada CSS y JS tocado.
6. Cada bloque: `tools/Run-Tests.ps1`, `aspnet_compiler` y prueba en el navegador contra DEV antes del PR.

## Próximo release

Entran **A y B**: corrigen lo que hoy se lee mal y reordenan el inicio, sin base de datos y sin tocar las demás
pantallas, así que no suman riesgo al release que ya lleva el módulo Clientes y sus 10 scripts.
C, D y E cambian el layout de todo el sitio y conviene probarlos aparte, en el release siguiente.
