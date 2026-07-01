# Reserva/Index — Plan de mejoras UX/UI (header y contexto operativo)

**Fecha:** 2026-06-30  
**Estado:** Fases 1–2 implementadas (2026-06-30); Fases 3+ pendientes  
**Vista objetivo:** `MAT.MVC/Views/Reserva/Index.cshtml`  
**Relacionado:** `Reserva/DistribucionCoche.cshtml`, `SPEC.md` (épica DistribucionCoche), `VISTAS_PENDIENTES_ACTUALIZACION.md`

---

## 1. Contexto y problema

`Reserva/Index` es la pantalla principal de **operación diaria**: el vendedor/administrador elige butacas, consulta ocupación, accede a manifiestos, lista de espera, pre-reservas vencidas, etc.

Hoy la franja superior usa el patrón genérico de dashboard:

```html
<div class="modern-dashboard-header">
    <h2>Reserva <span>Gestión de reservas y distribución de butacas</span></h2>
</div>
```

**Problemas detectados:**

| Problema | Impacto |
|----------|---------|
| Título genérico (“Reserva”) en lugar del nombre del viaje | El usuario no sabe *de qué viaje* habla la pantalla hasta mirar la columna izquierda |
| Subtítulo estático que no aporta contexto | Ocupa ~80 % del ancho con texto redundante (breadcrumb + menú ya indican el módulo) |
| `margin-bottom: 3rem` del header (`modern-dashboard.css`) | Empuja el mapa de butacas hacia abajo en pantallas medianas |
| Contexto del viaje cargado por AJAX (`DetalleViaje`) | Parpadeo inicial; header vacío mientras carga el panel izquierdo |
| KPIs de ocupación solo visibles en `DistribucionCoche` (pestaña aparte) | En Index hay que “contar a ojo” en el mapa |
| Alertas (lista de espera, pre-reservas, presupuesto) dispersas | Banner de presupuesto arriba, alertas flotantes abajo — sin jerarquía unificada |
| Botonera con 9 botones del mismo peso visual | Acción principal (“Reservar”) compite con acciones secundarias |
| Leyenda de 9 estados siempre visible bajo el mapa | Ocupa espacio vertical en cada visita |

**Referencia positiva en el mismo proyecto:** `DistribucionCoche.cshtml` ya implementa cabecera contextual (nombre del viaje, fechas, toolbar con ocupación/disponibles/pagados/señados). Index debería **converger** hacia ese patrón, adaptado al layout de dos columnas.

---

## 2. Principios de diseño

1. **Contexto primero:** el H1/H2 de la página es el *viaje*, no el módulo.
2. **Escaneabilidad:** ocupación y alertas visibles en &lt; 2 segundos sin abrir otra pestaña.
3. **Coherencia visual:** mismos colores de estado que el mapa de butacas y la leyenda existente.
4. **Progresivo:** cada fase entrega valor sin bloquear la siguiente.
5. **Reutilizar antes de inventar:** clases `modern-card`, `stat-card-modern`, `dashboard-stat-card` (Home), header de `DistribucionCoche`.
6. **No duplicar ruido:** servicios incluidos, observaciones e itinerario permanecen en el panel lateral; el header solo resume lo operativo.

---

## 3. Estado actual — inventario de datos

### 3.1 Fuentes disponibles hoy

| Dato | Origen | Cuándo está disponible |
|------|--------|------------------------|
| Lista de butacas/pasajes | `Model` (`List<ReservaStandard>`) | GET Index — inmediato |
| `EstadoPasaje` por butaca | `Model` | GET Index — inmediato |
| Pre-reservas vencidas | `ViewBag.PreReservas` | GET Index — inmediato |
| Cantidad lista de espera | `ViewBag.ListaEspera` | GET Index — inmediato |
| Presupuesto vinculado | `ViewBag.Presupuesto` | GET Index — si viene de “Convertir en venta” |
| Detalle completo del viaje | AJAX → `DetalleViaje` partial | Tras carga JS (~200–500 ms) |
| KPIs precisos total/disponibles | `DistribucionCoche` action + SP | Solo en vista standalone |

### 3.2 Estados de pasaje (`eEstadoPasaje`)

| Valor | Nombre | Uso en KPI |
|-------|--------|------------|
| 1 | Disponible | Disponibles |
| 2 | Reservado | Señados / pendientes (agrupar con Señado según negocio) |
| 3 | Señado | Señados |
| 4 | Pagado | Pagados |
| 5 | Pre-reserva | Pre-reserva activa |
| 6 | Reserva-Hotel | Pagado + Hotel |
| 7 | Anulado | Excluir de ocupación o mostrar aparte |
| 8 | Pasaje y Hotel prereservados | Pre-reserva + Hotel |

### 3.3 Limitación conocida de métricas en Index

`ReservaMethod.GetListOfPasajesByViajeID` devuelve **una fila por pasaje/butaca ocupada en el mapa renderizado**, no necesariamente el total físico del coche si hay butacas sin fila de pasaje. Las métricas desde `Model` en Index son **operativas sobre lo visible**, no contabilidad exacta de capacidad.

Para KPIs **100 % exactos** (total de butacas del transporte vs ocupadas), depende del task en SPEC: *“Refactor layout + butacas vacías en SP”*. Documentado en **Fase 8** de este plan.

---

## 4. Roadmap por fases (de menor a mayor impacto)

Cada fase incluye: objetivo, cambios, archivos, esfuerzo estimado, criterios de aceptación y dependencias.

---

### Fase 1 — Quick wins visuales (sin backend) — **implementada 2026-06-30**

**Objetivo:** reducir sensación de vacío sin cambiar arquitectura de datos.

| # | Mejora | Detalle |
|---|--------|---------|
| 1.1 | Reducir márgenes del header en Index | Override `.modern-dashboard-header` → `margin-bottom: 1rem` (solo en Reserva/Index) |
| 1.2 | Eliminar subtítulo genérico | Quitar *“Gestión de reservas y distribución de butacas”* |
| 1.3 | Breadcrumb enriquecido | Mostrar nombre abreviado del viaje en breadcrumb si está disponible (opcional v1) |
| 1.4 | Contenedor header con altura mínima útil | Evitar bloque de solo 2 líneas de texto en franja completa |

**Archivos:** `Index.cshtml`, `mat.styles.custom.css` o bloque scoped en `modern-dashboard.css` (`.reserva-index-page`).

**Esfuerzo:** 1–2 h  
**Riesgo:** Bajo  
**Criterio de aceptación:** menos espacio muerto arriba; mapa visible más arriba en 1366×768.

---

### Fase 2 — Hero contextual del viaje (backend mínimo) — **implementada 2026-06-30**

**Objetivo:** reemplazar “Reserva” por identidad del viaje al primer paint.

| # | Mejora | Detalle |
|---|--------|---------|
| 2.1 | Cargar `DetalleViaje` en GET de Index | En `ReservaController.Index`: `ViewBag.DetalleViaje = ViajeMethod.GetDetalleViajeModel(viajeid)` (mismo patrón que `NuevaReserva/Index`) |
| 2.2 | Partial `_ReservaViajeHero.cshtml` | Fila 1: **Descripción** (H2), destino, salida/regreso, coche + patente |
| 2.3 | Meta en chips inline | Iconos Bootstrap: `bi-geo-alt`, `bi-calendar-event`, `bi-bus-front` |
| 2.4 | Evitar duplicar título en sidebar | En `DetalleViaje.cshtml`: ocultar o reducir `viaje-detalle-title` cuando se renderiza dentro de Index (flag `ViewBag.EmbedDetalle` o partial distinto) |

**Layout propuesto (Fase 2):**

```
┌──────────────────────────────────────────────────────────────────┐
│ TERMAS DE FEDERACIÓN EN ENTRE RÍOS          Coche 101 · AA999AA │
│ 📍 Federación  ·  Salida 21/06 14:00  ·  Regreso 27/06 10:00   │
└──────────────────────────────────────────────────────────────────┘
```

**Archivos:** `ReservaController.cs`, `Index.cshtml`, `_ReservaViajeHero.cshtml` (nuevo), `DetalleViaje.cshtml` (ajuste menor), CSS scoped.

**Esfuerzo:** 3–4 h  
**Riesgo:** Bajo (1 query extra ya usada en el partial AJAX)  
**Criterio de aceptación:** nombre del viaje visible sin esperar AJAX; sidebar sigue mostrando servicios/observaciones.

---

### Fase 3 — Strip de KPIs compactos

**Objetivo:** indicadores accionables en el header, calculados server-side desde `Model`.

| # | KPI | Cálculo (desde `Model`) | Estilo |
|---|-----|-------------------------|--------|
| 3.1 | Ocupación | `(total - Disponible) / total` + texto `32/40` | `stat-card-modern` brand + barra fina de progreso |
| 3.2 | Pagados | `EstadoPasaje == 4` | verde — clase `reservado_ref` |
| 3.3 | Señados | `EstadoPasaje == 3` (+ opcional `2` Reservado) | naranja — `señado` |
| 3.4 | Disponibles | `EstadoPasaje == 1` | gris/rosa — `disponible` |
| 3.5 | Pre-reserva | `EstadoPasaje == 5` | amarillo — `prereserva` |
| 3.6 | Alertas | `ViewBag.ListaEspera`, `ViewBag.PreReservas.Count` | chip warning solo si &gt; 0 |

**Layout propuesto (Fase 2 + 3):**

```
┌──────────────────────────────────────────────────────────────────┐
│ [HERO — ver Fase 2]                                              │
├──────────────────────────────────────────────────────────────────┤
│ ████████░░ 80%  │ 18 Pagados │ 6 Señados │ 4 Disp. │ ⚠ 2 espera │
└──────────────────────────────────────────────────────────────────┘
```

**Archivos:** `_ReservaViajeHero.cshtml` o `_ReservaViajeKpis.cshtml`, CSS (`.reserva-kpi-strip`), lógica Razor en bloque `@{}` de Index o helper en `ReservaModel`.

**Esfuerzo:** 4–6 h  
**Riesgo:** Medio-bajo (métricas aproximadas hasta Fase 8)  
**Criterio de aceptación:** números coinciden con conteo manual en mapa visible; colores alineados con leyenda.

**Referencia de código existente:** toolbar en `DistribucionCoche.cshtml` (líneas ~406–419) y conteos en `ReservaController.DistribucionCoche`.

---

### Fase 4 — Consolidación de alertas y presupuesto

**Objetivo:** unificar banners dispersos en la barra contextual.

| # | Mejora | Detalle |
|---|--------|---------|
| 4.1 | Integrar presupuesto vinculado en hero | Chip/banner compacto en header (código, cliente, monto) + link descargar — reemplaza bloque suelto actual |
| 4.2 | Lista de espera como KPI clickeable | Chip “N en espera” → `openWaitingList()` |
| 4.3 | Pre-reservas vencidas como KPI clickeable | Chip “N pre-reservas vencidas” → `showPreReservas()` |
| 4.4 | Eliminar o reducir `modern-alerts-container` flotante inferior | Solo si la acción queda en header; mantener accesibilidad (no perder `aria-live`) |

**Archivos:** `Index.cshtml`, partial header, posible extracción de `_PreReservasAlert.cshtml`.

**Esfuerzo:** 3–4 h  
**Riesgo:** Bajo  
**Criterio de aceptación:** una sola zona de alertas arriba; flujos JS existentes intactos.

---

### Fase 5 — Redistribución del panel lateral

**Objetivo:** sidebar pasa de “única fuente de contexto” a “detalle extendido”.

| # | Mejora | Detalle |
|---|--------|---------|
| 5.1 | Sidebar colapsable | Toggle “Detalle del viaje” — por defecto abierto en desktop, colapsado en mobile |
| 5.2 | Contenido priorizado en sidebar | Arriba: servicios incluidos + observaciones; abajo: itinerario (menos crítico para reserva) |
| 5.3 | Link “Ver detalle completo” en hero | Scroll suave a `#panel-detalle` |
| 5.4 | Mantener carga AJAX opcional | Hero server-side; sidebar puede seguir refrescándose por AJAX para no duplicar lógica pesada |

**Archivos:** `Index.cshtml`, `DetalleViaje.cshtml`, JS (collapse Bootstrap 5), CSS.

**Esfuerzo:** 4–5 h  
**Riesgo:** Bajo  
**Criterio de aceptación:** en mobile el mapa gana ancho; detalle accesible en 1 clic.

---

### Fase 6 — Botonera inteligente

**Objetivo:** jerarquía clara entre acción principal y secundarias.

| # | Mejora | Detalle |
|---|--------|---------|
| 6.1 | Agrupar acciones | **Primarias:** Reservar. **Operativas:** Manifiesto, Lista pasajeros, Distribución habitaciones/butacas. **Administrativas:** Facturas, Observaciones, Menores, Lista espera |
| 6.2 | Menú overflow “Más acciones” | En viewport &lt; 992px, botones secundarios en dropdown Bootstrap |
| 6.3 | Badges en botones con conteo | Ej.: Lista de Espera `(3)` si `ViewBag.ListaEspera > 0` |
| 6.4 | Sticky toolbar opcional | Barra de botones fija al scroll en pantallas altas (solo desktop) |

**Archivos:** `Index.cshtml`, CSS `.botonera-form`, JS responsive.

**Esfuerzo:** 5–6 h  
**Riesgo:** Medio (regresión en bindings jQuery de botones)  
**Criterio de aceptación:** “Reservar” siempre visible; resto accesible sin scroll horizontal en mobile.

---

### Fase 7 — Leyenda de estados optimizada

**Objetivo:** liberar espacio bajo el mapa sin perder referencia de colores.

| # | Mejora | Detalle |
|---|--------|---------|
| 7.1 | Leyenda colapsable | Por defecto cerrada; usuarios recurrentes no la necesitan siempre |
| 7.2 | Leyenda compacta en header (opcional) | Solo 4 estados principales + “+ más” tooltip |
| 7.3 | Sincronizar KPI chips con leyenda | Mismo color al hacer hover en chip → highlight en mapa (prep para Fase 9) |
| 7.4 | Tooltip en butacas ya existente | Mantener; leyenda es complemento para usuarios nuevos |

**Archivos:** `Index.cshtml` (`#tabla-referencia`), CSS `.referencias-compact`.

**Esfuerzo:** 2–3 h  
**Riesgo:** Bajo  
**Criterio de aceptación:** mapa gana ~80px verticales con leyenda colapsada.

---

### Fase 8 — Métricas exactas (datos / backend)

**Objetivo:** KPIs reflejan capacidad real del transporte, incluidas butacas vacías no renderizadas hoy.

| # | Mejora | Detalle |
|---|--------|---------|
| 8.1 | Alinear Index con SP de DistribucionCoche | Reutilizar lógica de conteo de `usp_MAT_Reserva_DistribucionCoche_GetByViajeID` o extender `GetListOfPasajesByViajeID` |
| 8.2 | ViewModel dedicado `ReservaIndexViewModel` | Propiedades: `DetalleViaje`, `Butacas`, `Kpis`, `PreReservas`, `ListaEspera`, `Presupuesto` — evita lógica pesada en la vista |
| 8.3 | Partial compartido `_ReservaKpiToolbar.cshtml` | Usado en Index y DistribucionCoche — **una sola fuente de verdad** para números |
| 8.4 | Warning si SP desactualizado | Mismo patrón que `ViewBag.SpMigrationWarning` en DistribucionCoche |

**Archivos:** `ReservaController.cs`, `ReservaModel.cs`, SP en `MAT.DB`, posible refactor SPEC *“Refactor layout + butacas vacías en SP”*.

**Esfuerzo:** 1–2 días  
**Riesgo:** Medio-alto (schema/SP, NetTiers)  
**Dependencia:** task SPEC DistribucionCoche — Refactor layout  
**Criterio de aceptación:** total butacas = capacidad del coche; disponibles incluye butacas sin pasajero en BD.

---

### Fase 9 — Interactividad avanzada en mapa + KPIs

**Objetivo:** header y mapa trabajan juntos como workspace.

| # | Mejora | Detalle |
|---|--------|---------|
| 9.1 | Clic en KPI filtra/resalta butacas | Ej.: clic “Señados” → `.señado` pulse, resto atenuado |
| 9.2 | Búsqueda de pasajero en Index | Portar barra de `DistribucionCoche` (`#buscar-pasajero`, F3, Enter) |
| 9.3 | Actualizar KPIs tras reserva/cambio | Endpoint JSON ligero `GetReservaKpis?viajeId=` + refresh parcial del strip |
| 9.4 | Selección múltiple visual | Contador “N butacas seleccionadas” en header durante flujo de reserva |

**Archivos:** `Index.cshtml` (JS), `ReservaController.cs` (endpoint KPI), CSS animaciones.

**Esfuerzo:** 2–3 días  
**Riesgo:** Medio  
**Criterio de aceptación:** filtro por estado sin recargar página; búsqueda encuentra butaca en &lt; 1 s.

---

### Fase 10 — Responsive y accesibilidad

**Objetivo:** experiencia sólida en tablet/móvil y cumplimiento básico WCAG.

| # | Mejora | Detalle |
|---|--------|---------|
| 10.1 | KPI strip horizontal scroll | Clase `scroll_x` (ya usada en tabs del bus) |
| 10.2 | Hero en 2 breakpoints | Desktop: 1 fila meta; mobile: stack vertical |
| 10.3 | Touch targets butacas | Mínimo 44×44 px en mobile (revisar CSS asientos) |
| 10.4 | `aria-live` en KPIs | Anunciar cambios de ocupación tras operaciones |
| 10.5 | Contraste leyenda vs fondo | Auditar clases `.disponible`, `.señado`, etc. |
| 10.6 | Vista lista alternativa mobile | Task SPEC *“Vista lista mobile”* para DistribucionCoche — considerar mismo patrón en Index |

**Archivos:** CSS responsive, `mat.distribucioncoche.css` (si se unifica), tests manuales en DevTools.

**Esfuerzo:** 2–3 días  
**Riesgo:** Medio  
**Criterio de aceptación:** usable en 390×844; Lighthouse Accessibility ≥ 85 en la vista.

---

### Fase 11 — Unificación Index ↔ DistribucionCoche

**Objetivo:** coherencia entre pantalla embebida y pestaña standalone (`window.open` — decisión de producto 2026-06).

| # | Mejora | Detalle |
|---|--------|---------|
| 11.1 | Mismo header/hero en ambas vistas | Partial compartido |
| 11.2 | Mismos KPIs y colores | Eliminar divergencia toolbar texto vs cards |
| 11.3 | Acciones al clic en butaca (SPEC Fase 3) | Menú popover: factura, cambio butaca — beneficia ambas vistas si se comparte partial `_DistribucionCocheAsiento` |
| 11.4 | **No reabrir modal embed** | Mantener `window.open` según SPEC; mejoras visuales son compatibles |

**Archivos:** partials compartidos, `DistribucionCoche.cshtml`, `Index.cshtml`.

**Esfuerzo:** 3–5 días (parcialmente solapado con tasks SPEC)  
**Dependencia:** SPEC DistribucionCoche Fase 3 acciones, vista lista mobile.

---

### Fase 12 — Top tier (excelencia operativa)

**Objetivo:** experiencia de producto premium para operadores de alto volumen.

| # | Mejora | Detalle |
|---|--------|---------|
| 12.1 | Header sticky al scroll | Hero + KPIs fijos; mapa scroll independiente |
| 12.2 | Atajos de teclado | `R` Reservar, `/` buscar pasajero, `Esc` limpiar selección |
| 12.3 | Indicadores de negocio | Monto señado vs pagado pendiente (requiere datos financieros del viaje) |
| 12.4 | Días hasta salida / urgencia | Chip “Sale en 3 días” con color semántico |
| 12.5 | Comparativa vs viaje anterior | “Ocupación 80% vs 65% misma fecha año pasado” — requiere reporting |
| 12.6 | Refresh silencioso | Polling cada N min o SignalR para multi-usuario en mismo viaje |
| 12.7 | Modo impresión del workspace | CSS print: hero + KPI + mapa simplificado |
| 12.8 | Onboarding contextual | Tooltip tour primera visita (opcional, baja prioridad) |
| 12.9 | Telemetría UX | Tiempo en pantalla, clics en KPI (sin PII) para iterar |

**Archivos:** transversales — controller, JS module, posible nuevo servicio de métricas.

**Esfuerzo:** 1–2 semanas (por ítem incremental)  
**Riesgo:** Alto en 12.3–12.6 (datos y concurrencia)  
**Criterio de aceptación:** definir por ítem al priorizar con negocio.

---

## 5. Matriz de priorización recomendada

| Prioridad | Fases | Valor / esfuerzo | Cuándo hacer |
|-----------|-------|------------------|--------------|
| **P0 — Ahora** | 1, 2, 3 | Alto / bajo-medio | Primer sprint UX Reserva |
| **P1 — Siguiente** | 4, 5, 6, 7 | Alto / medio | Mismo sprint o inmediato después |
| **P2 — Con backend** | 8, 11 | Muy alto / alto | Alinear con SPEC DistribucionCoche refactor |
| **P3 — Diferenciador** | 9, 10 | Alto / medio-alto | Tras P0–P1 estables |
| **P4 — Premium** | 12 | Variable / alto | Backlog producto según feedback operadores |

---

## 6. Wireframe objetivo final (P0 + P1 + P2 parcial)

```
┌─ TOP BAR (layout existente) ─────────────────────────────────────────┐
│ marcoantonio │ Volver │ Inicio > Reservas > Termas Federación │ user  │
├─ SIDEBAR ─┬─ MAIN ───────────────────────────────────────────────────┤
│           │ ┌─ RESERVA HERO (Fase 2) ──────────────────────────────┐ │
│           │ │ TERMAS DE FEDERACIÓN EN ENTRE RÍOS    Coche 101 · AA999│ │
│           │ │ 📍 Federación · 21/06 14:00 · 27/06 10:00            │ │
│           │ │ [Presupuesto MAT-2026-xxx] (Fase 4, si aplica)       │ │
│           │ └──────────────────────────────────────────────────────┘ │
│           │ ┌─ KPI STRIP (Fase 3–4) ───────────────────────────────┐ │
│           │ │ ████░░ 80% │ 18 Pag │ 6 Señ │ 4 Disp │ ⚠ 2 espera   │ │
│           │ └──────────────────────────────────────────────────────┘ │
│  Nav      │ ┌─ col-lg-3 ──────┐ ┌─ col-lg-9 MAPA ─────────────────┐ │
│  icons    │ │ Detalle viaje ▼   │ │ [Piso Sup] [Piso Inf]          │ │
│           │ │ (servicios, obs)  │ │  butacas...                    │ │
│           │ └───────────────────┘ │ [Reservar] [Más ▼] (Fase 6)    │ │
│           │                       │ Leyenda ▼ (Fase 7)             │ │
│           │                       └────────────────────────────────┘ │
└───────────┴──────────────────────────────────────────────────────────┘
```

---

## 7. Archivos previstos (consolidado)

| Archivo | Fases |
|---------|-------|
| `Views/Reserva/Index.cshtml` | 1–7, 9–10 |
| `Views/Reserva/_ReservaViajeHero.cshtml` *(nuevo)* | 2–4 |
| `Views/Reserva/_ReservaKpiStrip.cshtml` *(nuevo)* | 3–4, 8–9 |
| `Views/Reserva/DetalleViaje.cshtml | 2, 5 |
| `Controllers/Reserva/ReservaController.cs` | 2, 8, 9 |
| `Models/ReservaModel.cs` / ViewModel nuevo | 8 |
| `Content/mat.styles.custom.css` o `modern-dashboard.css` | 1–7, 10 |
| `Views/Reserva/DistribucionCoche.cshtml` | 8, 11 |
| `MAT.DB` — SP butacas | 8 |

---

## 8. Qué NO hacer

| Evitar | Motivo |
|--------|--------|
| 8–10 tarjetas KPI grandes estilo Home | Index es workspace; el mapa es protagonista |
| Reintroducir modal fullscreen para DistribucionCoche | Decisión de producto cerrada en SPEC (2026-06) |
| Duplicar servicios incluidos en header | Ruido; van en sidebar |
| Hardcodear totales de butaca por tipo de coche | Debe venir de BD (Fase 8) |
| Exponer `Exception.Message` al calcular KPIs | Usar `ErrorUtil tampoco` en controller |
| Cambiar flujos JS de reserva/pre-reserva en fases 1–4 | Solo layout y presentación |

---

## 9. Criterios de “terminado” por release

### Release A (Fases 1–4) — Header útil
- [ ] Nombre del viaje visible al cargar
- [ ] Strip KPI con ocupación, pagados, señados, disponibles
- [ ] Alertas integradas en header
- [ ] MSBuild sin errores
- [ ] Sin regresión en clic de butacas y botón Reservar

### Release B (Fases 5–7) — Layout optimizado
- [ ] Sidebar colapsable en mobile
- [ ] Botonera con jerarquía y overflow
- [ ] Leyenda colapsable

### Release C (Fases 8–11) — Datos exactos y paridad DistribucionCoche
- [ ] KPIs = capacidad real del transporte
- [ ] Partial compartido Index/DistribucionCoche
- [ ] SP publicado en MAT.DB

### Release D (Fases 9–10–12) — Premium
- [ ] Filtro por KPI, búsqueda, sticky, accesibilidad auditada
- [ ] Ítems Fase 12 priorizados con negocio

---

## 10. Relación con SPEC.md

| Task SPEC | Fase de este doc |
|-----------|------------------|
| DistribucionCoche — Refactor layout + butacas vacías SP | **Fase 8** |
| DistribucionCoche Fase 3 — Acciones al clic en butaca | **Fase 11** |
| DistribucionCoche — Vista lista mobile | **Fase 10.6** |
| DistribucionCoche Fase 3 — Modal Index | **Cerrado — no aplicar** |

**Sugerencia:** al implementar Release A, agregar en `SPEC.md` un bloque *“Reserva/Index — Header operativo”* con subtasks 1–4 enlazando a este documento.

---

## 11. Próximo paso sugerido

1. Validar con operadores los KPIs de **Fase 3** (¿incluir Reservado en Señados? ¿mostrar Anulados?).
2. Implementar **Release A** (Fases 1–4) en un PR acotado.
3. Registrar avance en `PROGRESS.md` al completar cada release.

---

*Documento generado a partir de análisis UX de captura Reserva/Index (2026-06-30) y código existente en `DistribucionCoche`, `Home/Index` y `modern-dashboard.css`.*
