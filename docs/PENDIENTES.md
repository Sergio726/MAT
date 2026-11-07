# Pendientes

Qué falta, ordenado por quién depende de cada cosa.

**Cómo usar este archivo:** fuente de verdad de tareas abiertas (sin Issues). Quien cierra un
ítem lo tilda `[x]` en el mismo commit o PR; el documento **se tilda, no se reescribe**.
Roles y forma de trabajo: [`HANDOFF.md`](HANDOFF.md).

Histórico de features ya hechas: [`../SPEC.md`](../SPEC.md). Bitácora: [`../PROGRESS.md`](../PROGRESS.md).

> **Estado en una línea (2026-09-30):** Build y tests OK; falta publicar SPs en BD; backlog = rentabilidad viaje + resumen de pagos por viaje para vendedores (Factura) + Integration API + Excursion UX + higiene de código (ex.Message, Web.config, warnings). Bug P1 reserva con menores corregido en código (publicar `2026-09-30_ReservaMenores_Fix.sql` + smoke).

---

## Pase a producción del módulo Clientes (revisión 05/11/2026)

Revisado contra el código de `MAT2026` (`fbd701c`) y las bases: DEV tiene todo aplicado; **Prod no tiene ninguno de los 8 scripts** (faltan 15 SPs, 3 tablas/columnas y la función de Seguimiento; `usp_MAT_PersonaCliente_GetTop` y `usp_MAT_Persona_GetByDNI` existen pero en versión vieja, los actualizan los scripts 8 y 1). Fuera de esos 8 no falta ningún SP que use el código.

Orden para el pase:

1. [ ] **Negocio:** revisar la tabla de equivalencias de localidades de Salta (script 6, bloque 4), sobre todo `CENTRO` → Salta (Capital) e `INGENIERO MAURY` → Rosario de Lerma.
2. [ ] **Respaldo** de `MAT.Intranet` antes de correr scripts.
3. [ ] **Scripts 1 a 8** en el orden de la lista de abajo (todos idempotentes; el 6 modifica datos y deja respaldo `dbo.Catalogos_Limpieza_20261104_*`).
4. [ ] **Configuración del servidor de Prod** (no la cambia la publicación): `SystemDEV` tiene que estar en `false` — con `true` los errores muestran el mensaje técnico al usuario y el login dice "Entorno de Prueba". `Web.Release.config` reemplaza las cadenas de conexión por `Data Source=MATSERVER` y **no** cambia `SystemDEV`: confirmar que el `Web.config` del servidor tenga los valores de Prod.
5. [ ] **Publicar `MAT.MVC`** (Release, quita `debug`).
6. [ ] **Smoke en Prod:** listado de clientes (buscar por DNI y por teléfono), ficha con pestañas, alta y edición de un cliente, Seguimiento (categoría y registrar contacto), reserva usando el autocompletado de clientes.
7. [ ] Con un usuario **Administrador**: dar de baja y reactivar un cliente de prueba, ver el historial.

Decisiones abiertas que no bloquean el pase: activar la 404 propia (`httpErrors` en `Web.config`), qué hacer con la rama vieja `refactor/normalizar-persona-cliente-controller` (marzo 2026, cambios a Reservas, Factura Fiscal y el Inicio nunca integrados).

## Bloquean — no dependen solo del equipo de código

- [ ] **Publicar SPs / scripts pendientes en cada entorno** — sin esto fallan features ya mergeadas en código. Incluye (verificar qué falta por entorno):
  - `usp_MAT_Factura_Search` (columnas Pagado/Saldo)
  - `database/2026-07-15_CambioButacas_PreserveHotel.sql`
  - `database/2026-07-07_DistribucionCoche_FacturaID.sql` y SP DistribucionCoche
  - `database/2026-09-30_ReservaMenores_Fix.sql` (4 SPs del circuito de reserva con menores; sin esto sigue fallando la reserva con 2+ adicionales) — **Prod: publicado 13/10/2026; DEV: aplicado 29/10/2026**
  - `database/2026-10-29_Persona_LimpiezaTelefonosFechas.sql` (limpieza de datos con respaldo en `dbo.Persona_Limpieza_20261029`) — **DEV y Prod: aplicado 29/10/2026** (Prod: 569 celulares, 1.772 teléfonos, 75 fechas)
  - `database/2026-10-29_SeguimientoClientes.sql` (tabla `ClienteContacto`, `fn_MAT_Seguimiento_Clientes`, SPs `usp_MAT_Seguimiento_*`) — **DEV: aplicado 29/10/2026; Prod: pendiente** (sin esto falla `/Seguimiento` y el recuadro del inicio)
  - **Módulo Clientes + Seguimiento — correr en Prod en este orden, antes de publicar `MAT.MVC`** (todos `CREATE OR ALTER`, idempotentes; DEV ya aplicado). Cada uno depende del anterior:
    1. [ ] `database/2026-10-29_SeguimientoClientes.sql` — tabla `ClienteContacto`, `fn_MAT_Seguimiento_Clientes`, SPs de Seguimiento (lo usan la ficha y el listado)
    2. [ ] `database/2026-11-04_ClienteFormulario.sql` — F1: `usp_MAT_PersonaCliente_Guardar`, `usp_MAT_Persona_BuscarPorDocumento`
    3. [ ] `database/2026-11-04_ClienteListado.sql` — F2: `usp_MAT_PersonaCliente_Buscar`, `usp_MAT_Reportes_ClientesNuevosPagina`
    4. [ ] `database/2026-11-04_ClienteFicha.sql` — F3: `usp_MAT_PersonaCliente_Ficha`
    5. [ ] `database/2026-11-04_ClienteCatalogos.sql` — F4: `Pais.Codigo`, tabla `Ocupacion` (+ semillas), `usp_MAT_Catalogo_Geografia`, ficha con nombre de país
    6. [ ] `database/2026-11-04_ClienteCatalogos_Datos.sql` — F4: **modifica datos** (nacionalidad → código, ocupación neutra, departamentos y localidades de Salta unificados). Con respaldo en `dbo.Catalogos_Limpieza_20261104_*` y bloque para revertir al final. **Antes:** el negocio revisa la tabla de equivalencias de localidades (bloque 4 del script). Va **antes** de publicar `MAT.MVC` de F4: el formulario espera códigos de país
    7. [ ] `database/2026-11-04_ClienteSeguimientoF5.sql` — F5: `ClienteContacto.Canal`, `usp_MAT_ClienteContacto_Insert` con canal, `usp_MAT_Seguimiento_ClientesPagina` (Seguimiento paginado). No modifica datos
    8. [ ] `database/2026-11-04_ClienteBajaAuditoria.sql` — F6: columnas de baja en `Cliente` (todos quedan activos), tabla `ClienteAudit`, filtros de activos en listado, autocompletado y Seguimiento, SPs de baja e historial. No modifica datos
    9. [ ] `database/2026-11-06_HomeProximasSalidas.sql` — Inicio: `usp_MAT_Dashboard_ProximasSalidas` (panel Próximas salidas). No modifica datos; independiente de los anteriores
    10. [ ] `database/2026-11-06_Notificaciones.sql` — Campanita: tablas `Notificacion` y `NotificacionLeida`, SPs `usp_MAT_Notificacion_*`. No modifica datos. **Después del 8** (los cumpleaños usan `Cliente.Activo`)
    - Verificación después de cada uno: `SELECT name, modify_date FROM sys.objects WHERE name LIKE 'usp_MAT_PersonaCliente_%' OR name LIKE '%Seguimiento%' ORDER BY modify_date DESC`
  - Otros scripts recientes en `database/` no aplicados
- [ ] **Viaje — definición de rentabilidad con negocio** — acordar base de ingresos (facturado vs cobrado), qué entra como costo, moneda, prorrateo, permisos. Registrar en [`../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md`](../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md) y en [`DECISIONES_ABIERTAS.md`](DECISIONES_ABIERTAS.md). **Bloquea** opciones A–D de schema.

## Necesarios pero no bloquean

- [ ] Smoke visual Admin ResumenPagos (viaje con/sin pagos, Limpiar, Excel) tras UX 2026-08-12
- [ ] Smoke Descuento/Recargo en factura ARS y USD (badge moneda, sin conversión)

---

## Trabajo del equipo

### Reserva con menores — BUG P1 (fix 2026-09-30; pendiente publicar SP + smoke)

**Síntoma:** en `Reserva/Index` la reserva con seguro de menor falla o termina sin vincular menores al viaje. Circuito completo, causas y evidencia: `PROGRESS.md` (entrada 2026-09-30 "Registro BUG P1"). **Fix 1–7 implementados el 2026-09-30** (entrada "Fix BUG P1" en `PROGRESS.md`); falta publicar `database/2026-09-30_ReservaMenores_Fix.sql` y el smoke.

- [x] **Fix 1 (bloqueo real, C#):** `ReservaController.ReservarPasajes` hace `Guid.Parse(p.adicionalesid)` sobre `"g1;g2"` (el JS une adicionales con `;`). Con 2+ seguros de menor o seguro + otro adicional → `FormatException` (5 casos en `ErrorLog` MAT_DEV, 25–26/08). Parsear separando por `;`/`,` con `TryParse` y mostrar `ViewBag.Error` en `Views/Reserva/FormReserva.cshtml` (hoy no se renderiza).
- [x] **Fix 2 (bloqueo real, separador SP):** `FormReserva` POST pasa `"g1;g2"` a `usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher`, que hace `Split(..., ',')` (publicado así en MAT_DEV), mientras `usp_MAT_Reserva_DetalleFactura` usa `';'`. Normalizar en C# (`Replace(";", ",")`) y/o alinear el SP a `';'` con script en `database/` + paridad `MAT.DB`.
- [x] **Fix 3 (vinculación silenciosa):** `FormReserva` descarta el resultado de `VincularMenorByViaje` y responde "Pagado" aunque no vincule. Validar en JS (`btn-reservar`) que `#hdnListMenor` tenga menores cuando `nroMenores > 0`; en el controller devolver/loguear el resultado de la vinculación.
- [x] **Fix 4 (SP):** `usp_MAT_Reserva_VincularMenorByViajeID`: inicializar `@PasajeID = ''` (hoy NULL → no devuelve resultset), `NOT EXISTS` por menor en vez de rechazar el lote entero, y `CAST(ERROR_LINE())` en los CATCH (`Error_message() + ERROR_LINE()` también en `UpdatePasajeAdicionalesVoucher` y `DetalleFactura`).
- [x] **Fix 5 (UX):** en `SeleccionarPasajero` el único acceso a la lista de menores es el ícono `(i)` (`validarMenores`); hacerlo un botón explícito "Seleccionar menores". Resetear `data-complete/tutor/monto` de `#hdnListMenor` tras reservar/cancelar (hoy queda en `true` y la segunda reserva muestra "EL SEGURO DE MENORES YA FUE SELECCIONADO").
- [x] **Fix 6 (opcional):** `Reserva/Index` → "Listado de Menores" abre `VinculacionMenor` sin `desdeFactura=1`, así que no muestra "Nueva vinculación"; hoy solo se puede vincular desde `DetalleFactura` (botón visible solo si la factura tiene ítem con "MENOR"). Decidir si habilitar desde el viaje.
- [x] **Fix 7 (defensivo):** `usp_MAT_Reserva_GetMenoresDisponibles` lista `Persona` sin join a `Cliente`, pero `PasajeroMenor.menorid` tiene FK a `Cliente`; un menor creado por `PersonaPasajero/Create` sin "es cliente" rompe la FK (0 casos hoy en MAT_DEV). Agregar el join o crear `Cliente` al vincular.
- [ ] **Smoke tras fix:** reserva con 1 menor, con 2 menores, con menor + otro adicional; segunda reserva sin recargar; verificar `PasajeroMenor` y badge en `DistribucionCoche`.

### Seguimiento de clientes para vendedores (P2)

Decisiones cerradas el 2026-10-29: [`DECISIONES_ABIERTAS.md`](DECISIONES_ABIERTAS.md) (sección Seguimiento de clientes). Consultas base ya probadas contra producción (ranking por pasajero, cumpleaños, última interacción).

- [x] Normalizador de teléfonos para WhatsApp (`WhatsAppHelper`) + fix enlace en Presupuesto
- [x] Alta de cliente: teléfono o celular completo y fecha de nacimiento obligatoria/validada (navegador y servidor); limpieza de datos existentes
- [x] Tabla `ClienteContacto` + SPs (registrar contacto, último contacto por cliente) en `MAT.DB` + script `database/2026-10-29_SeguimientoClientes.sql`
- [x] Función `fn_MAT_Seguimiento_Clientes` (categoría, viajes, último/próximo viaje, gasto ARS/USD, último contacto) y SPs de categorías, cumpleaños (hoy / próximos N días) y ficha
- [x] Pantalla `/Seguimiento`: para hoy (cumpleaños + WhatsApp con saludo), listas por categoría, búsqueda, ficha con viajes e historial de contactos, aviso de contacto reciente, "No volver a contactar"
- [x] Recuadro "Cumpleaños de hoy" en Home + menú Clientes → Seguimiento + acceso frecuente
- [x] Marcar fechas de nacimiento repetidas como "verificar con el cliente"
- [ ] **Publicar `database/2026-10-29_SeguimientoClientes.sql` en Prod** antes del deploy de `MAT.MVC` (DEV: aplicado 29/10/2026)
- [x] Segunda revisión (hora argentina, edición de cliente, menores, viajes pagados para otros, DNI sin puntos, firma de Presupuesto, listas grandes, accesibilidad)
- [ ] Smoke con vendedor real (saludo por WhatsApp, registrar contacto, ficha, categorías, editar un cliente sin teléfono fijo)
- [ ] Borrar `dbo.Persona_Limpieza_20261029` en Prod cuando ya no haga falta revertir la limpieza

### Módulo Clientes — mejoras y rediseño (P1)

Plan completo: [`../DOCUMENTACION/PLAN_MODULO_CLIENTES_REDISENO.md`](../DOCUMENTACION/PLAN_MODULO_CLIENTES_REDISENO.md) (auditoría QA/UI 2026-11-04 verificada). **Antes de empezar:** resolver las decisiones de [`DECISIONES_ABIERTAS.md`](DECISIONES_ABIERTAS.md) (Módulo Clientes) y conseguir usuario de prueba.

- [x] **F0 — Preparación:** métricas base y checklist manual ([`../DOCUMENTACION/CHECKLIST_MODULO_CLIENTES.md`](../DOCUMENTACION/CHECKLIST_MODULO_CLIENTES.md)), borrado `ClienteController` + `Views/Cliente/*` y `Edit - copia.cshtml`, `Verify-NetTiersMigration.ps1` recursivo (verifica 249 SPs)
- [ ] **F0 — Usuario de prueba en DEV** (decisión aceptada; falta crearlo) — necesario para el checklist
- [x] **F1 — Formulario de cliente:** `ClienteFormModel` + atributos (fecha, teléfonos, documento) con reglas iguales en navegador y servidor, parcial `_ClienteForm` para alta/edición/desde presupuesto, `GET Create` con modelo, errores por campo, aviso de documento repetido con enlace, guardado transaccional (`usp_MAT_PersonaCliente_Guardar`), cuenta corriente como interruptor
- [ ] **F1 — Publicar** `database/2026-11-04_ClienteFormulario.sql` en Prod (DEV: aplicado 04/11/2026) y pasar el checklist F1 con el usuario de prueba
- [x] **F2 — Listado y búsqueda:** *(04/11/2026, PR a MAT2026; falta publicar `database/2026-11-04_ClienteListado.sql` en prod)* `usp_MAT_PersonaCliente_Buscar` paginado (sin tocar `GetTop`), columnas de dígitos de teléfono, tabla densa con filtros y URL, Altas por período paginado + Excel
- [x] **F3 — Ficha como perfil:** *(04/11/2026, PR a MAT2026 después de F2; falta publicar `database/2026-11-04_ClienteFicha.sql` en prod)* encabezado + pestañas (Datos, Viajes, Pagos, Cuenta corriente, Facturas, Contactos) e integración con Seguimiento
- [x] **F4 — Catálogos y datos:** *(04/11/2026, PR a MAT2026 después de F3; DEV aplicado; Prod pendiente: revisión de equivalencias por el negocio + scripts 5 y 6)* País → Provincia → Departamento → Localidad, combos con búsqueda, nacionalidad/ocupación como select, deduplicación (DEV → Prod con OK)
- [ ] **F4 — Validar con el negocio** la tabla de equivalencias de localidades de Salta (`database/2026-11-04_ClienteCatalogos_Datos.sql`, bloque 4), en especial `CENTRO` → Salta (Capital) e `INGENIERO MAURY` → Rosario de Lerma
- [x] **F5 — Transversal y Seguimiento:** *(05/11/2026, PR a MAT2026; DEV aplicado)* estados de carga, viewport, contraste, Seguimiento paginado en servidor y acciones visibles, canal de contacto
- [ ] **F5 — Activar la página 404 propia** (`/Error/NoEncontrado` ya existe): agregar `httpErrors` en `MAT.MVC/Web.config` (área crítica, requiere OK). Propuesta: `<httpErrors errorMode="Custom" existingResponse="Auto"><remove statusCode="404" /><error statusCode="404" path="/Error/NoEncontrado" responseMode="ExecuteURL" /></httpErrors>` dentro de `system.webServer`, y `debug="false"` en la publicación (ya lo hace `Web.Release.config`)
- [x] **F6 — Baja y auditoría:** *(05/11/2026, PR a MAT2026 después de F5; DEV aplicado)* `Cliente.Activo`, `ClienteAudit`
- [ ] **F6 — Probar con un Administrador** los botones Dar de baja / Reactivar y la pestaña Historial (en DEV solo hay 1 usuario con ese rol; `admindev` no tiene ninguno)

### Panel principal — mejoras UX (P1)

Plan: [`../DOCUMENTACION/PLAN_PANEL_PRINCIPAL.md`](../DOCUMENTACION/PLAN_PANEL_PRINCIPAL.md) (auditoría UX/UI 06/11/2026, verificada contra el código).

- [x] **Bloque A — Correcciones de lectura** *(PR #45)*: barra de ocupación y texto con la misma métrica, badge solo en Últimos lugares/Completo, 3 salidas visibles, "Saludar" → "Ver seguimiento", "y N más…" con enlace, KPIs con `aria-label`, "Ver detalle" y texto para valor 0
- [x] **Bloque B — Reorganización del inicio** *(PR #46)*: accesos frecuentes compactos arriba, KPIs, dos columnas (salidas · cumpleaños colapsado), un solo estilo de tarjeta y encabezado
- [x] **Bloque C — Sidebar** *(PR #47)*: ítem activo con `aria-current`, densidad 38 px, chevrons con `aria-expanded`, ícono de Butacas
- [x] **Bloque D — Barra superior** *(PR #48; ajuste de breadcrumb a 1366px en #53)*: buscador global `Ctrl+K` (etapa 1: módulos), "+ Nuevo presupuesto", sidebar colapsable, breadcrumb en el inicio
- [x] **Bloque E — KPIs** *(PR #49; tendencia contra el mismo tramo del mes anterior, hero sin foto)*: tendencia vs mes anterior, hero sin foto o con contraste AA
- [x] **Bloque F — Retirar "Todos los módulos"** *(PR #50)* (después de D, verificando que ninguna ruta quede huérfana)
- [x] **Bloque G — Buscador con clientes, viajes y facturas** *(PR #51; facturas por titular: NroFactura casi nunca se carga)*
- [x] **Seguridad: sesión obligatoria en todo el sitio** *(PR #52)* — filtro global `AuthorizeAttribute`; ~150 acciones en 25 controladores respondían sin sesión (incluidas escrituras). Públicos: Account, Error, External.PackagesGetAll.
- [x] **Viajes por fecha y Reservas con el layout nuevo** *(PR #53)* — calendario dentro de su tarjeta, tarjeta de viaje sin título repetido, colectivo escalado al lugar real del panel (65–115 %).
- [ ] **Reservas [P3]: redibujar el plano del colectivo en SVG** — silueta del coche (frente, chofer, puerta, escalera, baño, bar) como fondo; asientos con forma de butaca y número adentro (hoy queda sobre el borde); estado con color **e** ícono/patrón (verde y amarillo se confunden), referencia fija con conteo por estado, frente y pasillo señalados; para doble piso, minibús y semicama/cama. Mantener la lógica actual (mismos asientos, estados y clics de `mat.busplugin`). Hacer primero un boceto con el coche real de un viaje para aprobar el estilo; si hay croquis o fotos del coche de la empresa de transporte, usarlos. **No** usar imágenes generadas por IA para el plano (no respetan las posiciones de los asientos y se pixelan al escalar). En tablet (800–1024px) hoy queda scroll horizontal.

### Prueba funcional del 2026-11-07 — pendientes

Recorrido de 37 rutas por enlaces + 90 acciones GET del código + 33 pantallas cargadas en el navegador + clics a mano (DEV, usuario `admindev`). Lo corregido está en el PR de la prueba; esto queda para decidir:

- [ ] **Seguridad [P1]: acciones que escriben con GET** — `PersonaCliente/EliminarReservaHotel` (borra), `Reserva/VincularMenor`, `Localidad/AddLocalidad`, `Viaje/HotelIngresoEgreso_Set`. Sin `[HttpPost]` ni token antifalsificación: un enlace o una imagen pueden dispararlas con la sesión del usuario (CSRF). Pasarlas a `[HttpPost]` + `[ValidateAntiForgeryToken]` y ajustar sus llamadas JS (hoy por GET).
- [ ] **Clientes nuevos [P2]: dos definiciones con el mismo nombre** — la tarjeta del inicio cuenta clientes cuya *primera factura* cae en el mes; el detalle que abre ("Clientes dados de alta por período") cuenta *altas* (`FechaAlta`). En DEV la tarjeta dice 0 y el detalle 1. Decidir cuál es "cliente nuevo" y alinear (o renombrar la tarjeta, p. ej. "Primeras compras").
- [ ] **Rutas muertas [P3]** — sin vista (dan 500 si se abren a mano, ninguna pantalla las enlaza): `Busqueda/Index`, `CuentaCorriente/Index`, `PersonaVendedor/Index`, `Precio/Create`. Borrarlas o redirigir.
- [ ] **Pantallas viejas muy pesadas [P3]** — `Persona/Index` (19 MB de HTML, 4,6 s), `PersonaPasajero/Index` (14 MB), `PersonaCliente/RenderGridClientes` (6,4 MB): no están enlazadas, pero cualquiera con sesión las puede abrir. Borrarlas o paginarlas.
- [ ] **Código muerto [P3]** — `#btnViajes` → `Viaje/PopPupViajes` (tarda más de 30 s) y `#txt-filter-cliente` → `RenderGridClientes` en `mat.jquery.functions.js` / `mat.jquery.binding.js`: ningún elemento con esos IDs existe en las vistas.
- [ ] **Validación de parámetros [P3]** — `Reserva/DetalleViaje` sin `viajeid` da 500 (`ParseExact` de nulo); devolver 400 o un mensaje.
- [ ] **Listados que mandan todas las filas [P3]** — Hoteles, Excursiones, Servicios y Adicionales generan 380–530 KB de HTML (todas las filas, DataTables pagina en el navegador); `Factura/FacturaResultSearch` sin filtro devuelve 8,5 MB. Paginar en el servidor si crecen.
- [ ] **Cosmético** — falta `Content/themes/base/images/ui-bg_highlight-soft_100_eeeeee_1x100.png` (404 en Reservas, lo pide jquery-ui-1.13.2.css).
### Factura / vendedores — resumen de pagos por viaje (P2)

**Pedido de operación:** en el módulo de factura que usan los **vendedores** (intranet, no solo Admin), necesitan un lugar para ver el **resumen de pagos hechos en el viaje** y el **método/medio de pago** de cada registro.

Hoy: Admin tiene `/Admin/ResumenPagos` (por viaje, con tipo de pago). En el flujo vendedor hay historial de pagos **por factura/cliente** (`DetalleFactura` → Pagos, `HistorialdePagos`), pero no un resumen consolidado **por viaje** con medios de pago visible en su día a día.

- [ ] **Factura [P2]: Revisar UX del módulo Factura (vista vendedor)** — mapear pantallas actuales (`Factura/Index`, listado por viaje, detalle factura / pagos) y gap vs necesidad de “pagos del viaje + medio de pago”.
- [ ] **Factura [P2]: Resumen de pagos por viaje para vendedores** — exponer (pantalla o sección en flujo existente) listado/resumen de pagos del viaje seleccionado, incluyendo **método de pago** (`TipoPagoDescripcion` / forma de pago), montos, cliente y fecha. Reutilizar lógica/SP de Admin (`GetPagosByViaje` / patrón ResumenPagos) con permisos de vendedor (solo sus viajes o todos según regla de negocio). Criterio: el vendedor encuentra el resumen sin entrar al panel Admin; MSBuild limpio; `ErrorUtil` en controller.

### Viaje — rentabilidad / gastos (P2)

Investigación: [`../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md`](../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md).  
Recomendación preliminar: híbrido A + B tras decisión de negocio.

- [ ] **Opción A:** `ViajeID` opcional en `FacturaFiscal` (Tipo=Compra) + UI + SP resumen
- [ ] **Opción B:** tabla `ViajeGasto` + CRUD + totales por SP
- [ ] **Opción C:** reemplazo acotado del flujo Planilla (solo si negocio lo pide)
- [ ] **Opción D:** costos sugeridos desde catálogo (borrador editable)
- [ ] **SP + pantalla rentabilidad por viaje** + Excel (patrón ResumenPagosExcel)

### Integration API (P3)

- [ ] **F0** — Inventario y contrato
- [ ] **F1** — Hiperdocumentación machine-readable
- [ ] **F2** — Autenticación API Key
- [ ] **F3** — REST read-only (MVP)
- [ ] **F4** — Webhooks salientes
- [ ] **F5** — Escrituras controladas (agent-safe)
- [ ] **F6** — Observabilidad y hardening

Detalle de criterios: sección correspondiente en `SPEC.md` (épica Integration API).

### Excursion UX (P3)

- [ ] **F0** — Investigación con usuarios y glosario
- [ ] **F1** — Propuesta de información (wireframe/copy)
- [ ] **F2** — Implementación listado + SP enriquecido
- [ ] **F3** — Alineación Create/Edit (opcional)

### Higiene de código (auditoría 2026-09-30)

- [ ] **`ex.Message` expuesto al usuario** (regla de `CLAUDE.md`): reemplazar por `ErrorUtil.LogAndGetPublicMessage` en `ExternalController.cs:28`, `FacturaFiscalController.cs:548/619/655`, `HabitacionController.cs:193`, `HomeController.cs:215/279`, `NuevaReservaController.cs:195`, `PersonaClienteController.cs:1197`, `PresupuestoController.cs:989`. Extender `tools/Verify-NetTiersMigration.ps1` para que detecte `Json(... ex.Message ...)` y `MsgError = e.Message`.
- [ ] **`Web.config` trackeado con credenciales reales** (`MAT_DEV.*` activas, producción comentadas, mismo servidor). Decidir: mover a transforms `Web.Debug/Release.config` o a archivo excluido del repo + rotar la contraseña. **Área crítica: confirmar antes de tocar.**
- [ ] **Vistas que no compilan** (detectado con `aspnet_compiler` 2026-10-29; restos de NetTiers, fallan al abrirlas): `Views/Cliente/Create.cshtml`, `Views/CuentaCorriente/DetalleComprobante.cshtml`, `Views/Persona/Details.cshtml`, `Views/Persona/Edit.cshtml`, `Views/Viaje/Details.cshtml`, `Views/PersonaCliente/Edit - copia.cshtml`. Arreglar o borrar si no se usan.
- [x] *(resuelto en F0 de Clientes, 04/11/2026: recorre subcarpetas, verifica 256 SPs)* **`Verify-NetTiersMigration.ps1` no revisa subcarpetas:** `Select-String -Path "MAT.MVC\**\*.cs"` en PowerShell 5.1 no es recursivo, así que los SPs de `Infrastructure\Data` no se verifican. Usar `Get-ChildItem -Recurse`.
- [ ] **Validación cargada dos veces:** `_Layout` ya carga jQuery Validate + validación discreta en el `<head>`, y 28 vistas vuelven a cargarlos con `@Scripts.Render("~/bundles/jqueryval")`; la segunda copia reemplaza las reglas registradas. Revisar vista por vista según su layout (las de `_LayoutLogin`/Admin pueden necesitarlo). Detectado en F1 del módulo Clientes (2026-11-04).
- [ ] **SP sin uso tras F1 de Clientes:** `usp_MAT_PersonaCliente_Create` (reemplazado por `usp_MAT_PersonaCliente_Guardar`). Borrar de `MAT.DB` cuando F1 esté en Prod. *(Corregido 05/11/2026: `usp_MAT_Persona_GetByDNI` **sí** se usa, en `PresupuestoController` vía `PersonaClienteGetByDNI`; no borrar.)*
- [ ] **Warnings de build:** `ReservaController.cs:317/433` y `ViajeModel.cs:293` (CS0168 `ex` sin usar), `ViajeController.cs:18-22` (CS0105 usings duplicados), `ReservaController.cs:36` (CS1998 `async` sin `await`).

### Backlog UX Admin (opcional)

- [ ] Tabs o pills **Por viaje / Por fecha** entre ResumenPagos hermanos
- [ ] Paridad columnas ResumenPagos vs Pagos por fecha (recibo, vendedor, cancelar) — requiere SP
- [ ] Reemplazar MagicSearch en ResumenPagos por control más mantenible

---

## Ya hecho y verificado — no rehacer

- [x] NetTiers F0–F12 (migración; proyectos NetTiers eliminados del repo)
- [x] Modernización masiva de vistas BS5 / inventario vistas (doc en Archive)
- [x] Admin onboarding, buscador Ctrl+K, tours contextuales, reset onboarding
- [x] Admin Usuarios (roles, DataTables, modal BS5, ErrorLog/Logs en hub)
- [x] DistribucionCoche Fases 1–3 (acciones butaca; publicar SP por entorno)
- [x] Fix cambio butaca conserva badge H (publicar SP por entorno)
- [x] UX FacturaListByViajeID Pagado/Saldo (publicar SP por entorno)
- [x] Enlace intranet ↔ Admin: “Ir a la intranet” / “Ir al administrador” (`_LoginPartial`)
- [x] Admin ResumenPagos: KPIs, contexto viaje, DataTable robusta, numeral en layout Admin
- [x] Descuento/Recargo: labels ARS/USD, validación cliente/servidor, sin conversión

---

## Al publicar / al entregar

- [ ] Checklist de scripts `database/` aplicados en Dev / Staging / Prod
- [ ] Compilar `MAT.MVC` Debug sin errores nuevos antes de push
- [ ] Actualizar `PROGRESS.md` con fecha y archivos tocados
