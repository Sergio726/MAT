# Fase 1 – Inventario de dependencias CSS (estilos viejos)

**Fecha:** Febrero 2026  
**Objetivo:** Detectar qué vistas/layouts usan CSS legacy para **migrar o eliminar**, no para seguir usando.

---

## 1. Archivos CSS considerados “viejos”

| Archivo | Dónde se carga | Acción |
|---------|----------------|--------|
| `mat.styles.custom.css` | _Layout.cshtml, _LayoutAdmin.cshtml, y 11 vistas con `<link>` propio | Migrar clases `custom_*` a `modern-*` / `u-*` y dejar de cargar |
| `mat.forms.css` | Solo _Layout.cshtml (todas las vistas del layout principal) | Reemplazar por estilos modern-* en formularios; dejar de cargar |
| `mat.perfiles.css` | Solo _Layout.cshtml | Reemplazar por modern-* donde se use; dejar de cargar |
| `tabla.css` | Solo _Layout.cshtml | Reemplazar clase `.tabla` / `.mat-customtable` por modern-datatable o modern-table; dejar de cargar |
| `preset.css` | _Layout.cshtml y _LayoutSplash.cshtml | Evaluar si preset.js/preset.css son necesarios; si no, dejar de cargar |

---

## 2. Dónde se carga mat.styles.custom.css

| Origen | Alcance |
|--------|---------|
| **_Layout.cshtml** | Todas las vistas que usan layout principal (default) |
| **_LayoutAdmin.cshtml** | Vistas Admin (Index, ResumenPagos, AuditoriaFacturas, etc.) |
| **Vistas con `<link>` duplicado** (quitar en Fase 2) | Reserva/Index, PasajeroViaje/Manifiesto, NuevaReserva/Index, Hotel/EsquemaDistribucion, Hotel/Distribucion, Reserva/DistribucionCoche, PasajeroViaje/ListadoSimple, ListadoSimpleToExport, CNRTHojaUno, PrintVinculacionMenor, PrintObservaciones |

---

## 3. Vistas que usan clases custom_* (mat.styles.custom.css)

Cada vista debe migrarse a clases de la interfaz nueva (`modern-*` o `u-*` en `modern-utilities.css`).

### 3.1 PersonaCliente

| Vista | Clases custom_* usadas |
|-------|------------------------|
| **VoucherGrupal.cshtml** | custom_padding_left_5, custom_text_aling_right, custom_text_aling_left, custom_font_weigh_bold, custom_width_400px, custom_width_90percent, custom_diplay_block, custom_width_200px, custom_background_yellow, custom_height_30, custom_margin_top_10 |
| **Voucher.cshtml** | custom_padding_left_5, custom_text_aling_right, custom_text_aling_left, custom_text_aling_center, custom_font_weigh_bold, custom_width_400px, custom_width_90percent, custom_diplay_block, custom_width_200px, custom_background_yellow, custom_border_2px, custom_padding_2, custom_margin_top_10 |
| **ElegirNuevaButaca.cshtml** | custom_display_box |
| **CambiarPrecioList.cshtml** | custom_padding_top_20, custom_padding_10, custom_float_right |
| **Edit - copia.cshtml** | custom_diplay_flex, custom_width_90percent, custom_width_10percent, custom_text_aling_right, custom_padding_top_8, custom_cursor_pointer, custom_font_weigh_bold, custom_font_size_18 |

### 3.2 Reserva

| Vista | Clases custom_* usadas |
|-------|------------------------|
| **SeleccionarPasajero.cshtml** | custom_padding_4 |
| **DistribucionCoche.cshtml** | custom_margin_top_47, custom_margin_top_20 (y define .custom_margin_top_47, .custom_margin_top_20 inline en la misma vista) |
| **PrintObservaciones.cshtml** | custom_padding_4, custom_margin_top_15, custom_label_radius, custom_border_printObservaciones, custom_font_weigh_bold, custom_font_size_11, custom_font_style_italic |

### 3.3 NuevaReserva

| Vista | Clases custom_* usadas |
|-------|------------------------|
| **FormReserva.cshtml** | custom_display_box, custom_padding_top_10, custom_background_dialog, custom_width_60percent, custom_width_40percent, custom_font_size_18, custom_font_size_16, custom_margin_20, custom_margin_left_10 |
| **DetalleViaje.cshtml** | custom_font_weigh_bold |

### 3.4 PasajeroViaje

| Vista | Clases custom_* usadas |
|-------|------------------------|
| **ListadoSimpleToExport.cshtml** | custom_padding_4 |
| **CNRTHojaUno.cshtml** | custom_width_200px, custom_width_215px, custom_width_95px, custom_width_196px, custom_width_185px, custom_font_weigh_bold |

### 3.5 Paquete

| Vista | Clases custom_* usadas |
|-------|------------------------|
| **RenderGridExcursiones.cshtml** | custom_margin_top_10 |

---

## 4. Uso de mat.forms.css y mat.perfiles.css

- **mat.forms.css:** Cargado en _Layout.cshtml. Clases usadas en muchas vistas: `.form`, `.form-modal`, `.form-label`, `.form-field`, `.fromReservaPasaje`.  
  Varias vistas ya usan **modern-form-*** (PersonaCliente/Edit, Create; Paquete/Edit en parte). Las que aún usan `.form-label`, `.form-field`, etc. deben migrarse a `modern-form-*`.
- **mat.perfiles.css:** Cargado en _Layout.cshtml. Clases como `.search-perfil`, `.content-perfil`, `.card`, etc. Usado en búsqueda de perfiles y cards; migrar a componentes modern-*.

---

## 5. Uso de tabla.css

- **Clase `.tabla`:** Usada en PasajeroViaje/Manifiesto.cshtml, ListadoSimple.cshtml, ListadoSimpleToExport.cshtml; CNRT.cshtml usa `class="tabla"` en un div.
- **table.tblSingle:** Definido en mat.styles.custom.css y en mat.esquemadistribucion.print.css; **no hay uso de clase `tblSingle` en ninguna vista** (solo en CSS). Se puede eliminar o dejar para impresión si se usa en otro contexto.

---

## 6. Resumen de clases custom_* a migrar

Agrupadas por tipo (para definir equivalentes en `modern-utilities.css` o componentes):

| Tipo | Clases | Equivalente sugerido (nueva interfaz) |
|------|--------|----------------------------------------|
| Tipografía | custom_font_weigh_bold, custom_font_size_11, custom_font_size_16, custom_font_size_18, custom_font_style_italic | u-font-bold, u-text-sm, u-text-base, u-text-lg, u-italic |
| Anchuras % | custom_width_10percent … custom_width_90percent, custom_width_100percent | u-w-10p … u-w-100p (o clases de grid modern-*) |
| Anchuras px | custom_width_200px, custom_width_400px, custom_width_95px, etc. | u-w-200, u-w-400, etc. o variables/rem |
| Display | custom_display_box, custom_diplay_block, custom_diplay_flex | u-display-flex, u-block, u-flex (modern-*) |
| Padding | custom_padding_4, custom_padding_left_5, custom_padding_top_10, etc. | u-p-1, u-pl-1, u-pt-2, etc. |
| Margin | custom_margin_top_10, custom_margin_top_20, custom_margin_left_10, etc. | u-mt-2, u-mt-4, u-ml-2, etc. |
| Text align | custom_text_aling_left, custom_text_aling_right, custom_text_aling_center | u-text-left, u-text-right, u-text-center |
| Otros | custom_cursor_pointer, custom_background_yellow, custom_border_2px, custom_float_right, custom_label_radius, custom_border_printObservaciones, custom_background_dialog | u-cursor-pointer, u-bg-warning, u-border, u-float-right, y clases de componente para print/dialog |

---

## 7. Próximos pasos (Fase 2 en adelante)

1. **Fase 2:** Dejar de cargar en vistas el `<link>` duplicado a mat.styles.custom.css; unificar carga por layout/bundles.
2. **Fase 3–4:** En cada vista listada arriba, reemplazar clases `custom_*` por las nuevas (`modern-*` / `u-*`); crear `modern-utilities.css` con las utilidades necesarias.
3. **Fase 5:** Dejar de cargar mat.styles.custom.css, mat.forms.css, mat.perfiles.css, tabla.css y preset.css (según criterio); eliminar o mover a Content/legacy.
