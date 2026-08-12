# Fase 2 – Análisis y acciones (Unificar carga de CSS)

**Criterio:** Solo interfaz nueva en bundles; en esta fase se unifica la carga y se eliminan duplicados. Los archivos legacy (mat.styles.custom, mat.forms, etc.) se siguen cargando por ahora hasta Fase 5; lo que se hace aquí es ordenar la carga en bundles y quitar `<link>` duplicados en vistas.

---

## 1. Estado actual

### 1.1 _Layout.cshtml (principal)

- **Ya usa bundle:** `@Styles.Render("~/Content/css")` → solo incluye `Site.css` (BundleConfig).
- **Resto:** 18 `<link>` sueltos (jQuery UI, mat.busplugin, preset, tabla, mat.perfiles, bootstrap, mat.forms, voucher, timepicker, mat.styles.custom, saas-variables, modern-layout, modern-menu, modern-dashboard, modern-topbar, dataTable, modern-datatable).
- **No existe** `@RenderSection("Styles", required: false)` en `<head>`, por lo que las vistas que ponen `<link>` en el cuerpo quedan con CSS en el body.

### 1.2 _LayoutAdmin.cshtml

- Sin bundles: 7 `<link>` sueltos (bootstrap, bootstrap-icons, jQuery UI, timepicker, dataTable, mat.styles.custom, admin.modern).

### 1.3 _LayoutSplash.cshtml

- Sin bundles: 5 `<link>` (jQuery UI, mat.styles, mat.splash, mat.busplugin, preset). Usa jQuery 1.8 y jQuery UI 1.8 (versiones viejas).

### 1.4 Vistas con `<link>` propio

| Vista | Qué cargan | Usa layout |
|-------|------------|------------|
| Reserva/Index | mat.styles.custom | _Layout (duplicado) |
| PasajeroViaje/Manifiesto | mat.styles.custom, listadosimple, listadosimple.print, bootstrap-icons | _Layout (duplicado + específico) |
| NuevaReserva/Index | mat.styles.custom | _Layout (duplicado) |
| Hotel/EsquemaDistribucion | mat.styles.custom | _Layout (duplicado) |
| Hotel/Distribucion | hoteles, esquemadistribucion.print, mat.styles.custom, **Bootstrap Icons CDN** | _Layout (duplicado + específico + CDN) |
| Reserva/DistribucionCoche | mat.styles.custom | _Layout (duplicado) |
| PasajeroViaje/ListadoSimple | mat.styles.custom, listadosimple, listadosimple.print, **Bootstrap Icons CDN** | _Layout (duplicado + específico + CDN) |
| PasajeroViaje/ListadoSimpleToExport | listadosimple, listadosimple.print, mat.styles.custom | _Layout (duplicado + específico) |
| PasajeroViaje/CNRTHojaUno | mat.styles.custom | _Layout (duplicado) |
| Reserva/PrintVinculacionMenor | mat.styles.custom, tblVinculacionMenor, tblVinculacionMenorPrint | _Layout (duplicado + específico) |
| Reserva/PrintObservaciones | mat.styles.custom, listadosimple, listadosimple.print | _Layout (duplicado + específico) |
| PersonaCliente/Voucher.cshtml | voucher.css | **Sin layout** (página completa HTML propia) |
| PersonaCliente/VoucherGrupal.cshtml | voucher.css | **Sin layout** (página completa HTML propia) |
| Viaje/Edit, Admin/ResumenPagos, Factura/Index, Reserva/ObservacionABM | magicsearch + normalize | _Layout / _LayoutAdmin (específico) |
| Account/Login, Register, Manage, RegistrarVendedor | modern-account, saas-variables | _Layout o null (específico) |
| Admin/ImprimirPlanilla, ImprimirPlanillaDetalle | mat.planillaprint | _LayoutAdmin (específico) |
| PasajeroViaje/CNRT | mat.cnrt, mat.cnrt.print | _Layout (específico) |
| Viaje/_ItinerarioSection | Select2 CDN | parcial |

### 1.5 BundleConfig.cs

- `~/Content/css` → solo `Site.css`.
- `~/Content/themes/base/css` → varios jQuery UI (no usado en _Layout; _Layout usa jquery-ui-1.13.2.css directo).

---

## 2. Acciones propuestas

### A. BundleConfig.cs

1. **Crear dos nuevos StyleBundles** para el layout principal:
   - **`~/Content/css/core`**: Site.css, saas-variables.css, bootstrap-5.3.2.min.css, bootstrap-icons.css, themes/base/jquery-ui-1.13.2.css, mat.busplugin.css, preset.css, tabla.css, mat.perfiles.css, mat.forms.css, voucher.css, jquery.timepicker.css, mat.styles.custom.css (mientras sigan en uso).
   - **`~/Content/css/modern`**: modern-layout.css, modern-menu.css, modern-dashboard.css, modern-topbar.css, modern-datatable.css.
   - **`~/Content/css/datatable`** (opcional, o incluido en core): Scripts/dataTable/jquery.dataTables.min.css, modern-datatable.css.

   Incluir **dataTable** en `core` para no añadir un tercer bundle y mantener el orden actual (dataTable después de los demás).

2. **Crear bundles para _LayoutAdmin:**
   - **`~/Content/css/admin`**: bootstrap-5.3.2.min.css, bootstrap-icons.css, themes/base/jquery-ui-1.13.2.css, timepicker, dataTable, mat.styles.custom.css, admin.modern.css.

3. **No tocar** el bundle `~/Content/themes/base/css` ni el de modernizr.

### B. _Layout.cshtml

4. **Añadir** en `<head>` (antes de `</head>`), después de los scripts que consideres adecuado, una línea para estilos por vista:
   - `@RenderSection("Styles", required: false)`

5. **Reemplazar** todos los `<link>` de CSS (líneas 19–60) por:
   - `@Styles.Render("~/Content/css/core")`
   - `@Styles.Render("~/Content/css/modern")`
   - Mantener `@Styles.Render("~/Content/css")` solo si se quiere seguir incluyendo Site.css desde el bundle existente; como Site.css estará en `core`, se puede **quitar** `@Styles.Render("~/Content/css")` y dejar solo los dos nuevos.

   Orden propuesto en head: primero `~/Content/css`, luego `~/Content/css/core`, luego `~/Content/css/modern`, luego scripts, luego dataTable (o todo en core). Para minimizar cambios, **incluir en `core` todo** (Site + variables + bootstrap + jQuery UI + busplugin + preset + tabla + perfiles + forms + voucher + timepicker + mat.styles.custom + dataTable + modern-datatable) y **en `modern`** solo layout, menu, dashboard, topbar. Así en _Layout quedarían solo:
   - `@Styles.Render("~/Content/css/core")`
   - `@Styles.Render("~/Content/css/modern")`
   - `@RenderSection("Styles", required: false)`

### C. _LayoutAdmin.cshtml

6. **Reemplazar** los 7 `<link>` de CSS por:
   - `@Styles.Render("~/Content/css/admin")`
   - Y añadir `@RenderSection("Styles", required: false)` en `<head>` si no existe.

### D. _LayoutSplash.cshtml

7. **No crear bundle** por ahora (pantalla de splash con stack distinto y jQuery viejo). Dejar los `<link>` como están. Opcional: documentar que en una fase posterior se puede unificar si se actualiza el splash.

### E. Quitar `<link>` duplicados en vistas (mat.styles.custom ya viene del layout)

8. **Eliminar** solo la línea que carga `mat.styles.custom.css` en estas vistas (usan _Layout):
   - Reserva/Index.cshtml
   - PasajeroViaje/Manifiesto.cshtml
   - NuevaReserva/Index.cshtml
   - Hotel/EsquemaDistribucion.cshtml
   - Hotel/Distribucion.cshtml
   - Reserva/DistribucionCoche.cshtml
   - PasajeroViaje/ListadoSimple.cshtml
   - PasajeroViaje/ListadoSimpleToExport.cshtml
   - PasajeroViaje/CNRTHojaUno.cshtml
   - Reserva/PrintVinculacionMenor.cshtml
   - Reserva/PrintObservaciones.cshtml

   En Manifiesto, ListadoSimple, ListadoSimpleToExport, Distribucion, PrintVinculacionMenor, PrintObservaciones **mantener** los demás `<link>` (listadosimple, print, hoteles, etc.) pero **moverlos a `@section Styles`** (ver punto F) para que se rendericen en `<head>`.

### F. Un solo origen para Bootstrap Icons y CSS específico de vista

9. **Quitar** el `<link>` a Bootstrap Icons desde CDN en:
   - Hotel/Distribucion.cshtml (línea 11)
   - PasajeroViaje/ListadoSimple.cshtml (línea 258)

   El layout principal ya carga bootstrap-icons.css local; no hace falta el CDN.

10. **Vistas con CSS específico (solo para esa pantalla):** mover sus `<link>` a `@section Styles { ... }` y asegurar que _Layout tenga `@RenderSection("Styles", required: false)` en `<head>`:
    - PasajeroViaje/Manifiesto.cshtml: listadosimple, listadosimple.print, y quitar bootstrap-icons y mat.styles.custom (ya en layout).
    - Hotel/Distribucion.cshtml: hoteles, mat.esquemadistribucion.print (y quitar mat.styles.custom y CDN bootstrap-icons).
    - PasajeroViaje/ListadoSimple.cshtml: listadosimple, listadosimple.print (y quitar mat.styles.custom y CDN bootstrap-icons).
    - PasajeroViaje/ListadoSimpleToExport.cshtml: listadosimple, listadosimple.print (y quitar mat.styles.custom).
    - Reserva/PrintVinculacionMenor.cshtml: tblVinculacionMenor, tblVinculacionMenorPrint (y quitar mat.styles.custom).
    - Reserva/PrintObservaciones.cshtml: listadosimple, listadosimple.print (y quitar mat.styles.custom).
    - Viaje/Edit, Admin/ResumenPagos, Factura/Index, Reserva/ObservacionABM: magicsearch + normalize en `@section Styles`.
    - Account/*: modern-account + saas-variables en `@section Styles` (ya lo tienen; solo asegurar que el layout que usen renderice la sección).
    - Admin/ImprimirPlanilla, ImprimirPlanillaDetalle: mat.planillaprint en `@section Styles` (y que _LayoutAdmin renderice Styles).
    - PasajeroViaje/CNRT: mat.cnrt, mat.cnrt.print en `@section Styles`.

11. **Voucher.cshtml y VoucherGrupal.cshtml:** no se modifican (páginas completas con HTML propio, sin _Layout).

---

## 3. Resumen de archivos a tocar

| Archivo | Acción |
|---------|--------|
| App_Start/BundleConfig.cs | Añadir StyleBundles ~/Content/css/core, ~/Content/css/modern, ~/Content/css/admin |
| Views/Shared/_Layout.cshtml | Añadir RenderSection("Styles"); reemplazar ~18 <link> por 2 Render de bundles |
| Views/Shared/_LayoutAdmin.cshtml | Añadir RenderSection("Styles"); reemplazar 7 <link> por 1 Render de bundle admin |
| Reserva/Index.cshtml | Quitar 1 línea (mat.styles.custom) |
| PasajeroViaje/Manifiesto.cshtml | Quitar mat.styles.custom y bootstrap-icons; mover listadosimple + print a @section Styles |
| NuevaReserva/Index.cshtml | Quitar 1 línea (mat.styles.custom) |
| Hotel/EsquemaDistribucion.cshtml | Quitar 1 línea (mat.styles.custom) |
| Hotel/Distribucion.cshtml | Quitar mat.styles.custom y CDN bootstrap-icons; mover hoteles + print a @section Styles |
| Reserva/DistribucionCoche.cshtml | Quitar 1 línea (mat.styles.custom) |
| PasajeroViaje/ListadoSimple.cshtml | Quitar mat.styles.custom y CDN bootstrap-icons; mover listadosimple + print a @section Styles |
| PasajeroViaje/ListadoSimpleToExport.cshtml | Quitar mat.styles.custom; mover listadosimple + print a @section Styles |
| PasajeroViaje/CNRTHojaUno.cshtml | Quitar 1 línea (mat.styles.custom) |
| Reserva/PrintVinculacionMenor.cshtml | Quitar mat.styles.custom; mover tblVinculacionMenor + print a @section Styles |
| Reserva/PrintObservaciones.cshtml | Quitar mat.styles.custom; mover listadosimple + print a @section Styles |
| Viaje/Edit.cshtml, Factura/Index.cshtml, Admin/ResumenPagos.cshtml, Reserva/ObservacionABM.cshtml | Mover magicsearch + normalize a @section Styles (si no están ya en head) |
| Account/* (Login, Register, Manage, RegistrarVendedor) | Verificar que usen @section Styles y que el layout renderice Styles |
| Admin/ImprimirPlanilla.cshtml, ImprimirPlanillaDetalle.cshtml | Mover planillaprint a @section Styles |
| PasajeroViaje/CNRT.cshtml | Mover cnrt + cnrt.print a @section Styles |

---

## 4. Orden recomendado de implementación

1. BundleConfig: crear bundles core, modern, admin.
2. _Layout: añadir RenderSection("Styles"); sustituir <link> por Render de core y modern.
3. _LayoutAdmin: añadir RenderSection("Styles"); sustituir <link> por Render de admin.
4. Vistas: quitar duplicados mat.styles.custom y CDN Bootstrap Icons; mover CSS específico a @section Styles en cada vista listada.

---

## 5. Riesgos y consideraciones

- **Cache:** Al pasar a bundles, en producción la URL será tipo `/Content/css/core?v=...`. Si hay cache agresivo, conviene probar en entorno de desarrollo primero.
- **Orden de carga:** El orden en Include() del bundle debe respetar dependencias (p. ej. saas-variables antes de modern-*).
- **Vistas sin Layout:** Voucher y VoucherGrupal no usan _Layout; no se modifican.
- **_LayoutSplash:** No se modifica en esta fase; sigue con <link> sueltos.

Si confirmas, se implementan estas acciones en el código.
