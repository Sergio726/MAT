# Plan — Módulo Clientes: mejoras y rediseño

**Fecha:** 2026-11-04 · **Estado:** propuesto, pendiente de aprobación de las decisiones de la sección 8.
**Base:** auditoría de QA y UI del 04/11/2026 (`Modulo-Clientes-QA-y-propuesta-de-UI.md`) verificada contra el código,
la base de producción (solo lectura) y una ejecución local de la intranet.

Tareas vivas: [`docs/PENDIENTES.md`](../docs/PENDIENTES.md). Roadmap comercial: [`ROADMAP_COMERCIAL_2026.md`](ROADMAP_COMERCIAL_2026.md).

---

## 1. Objetivo

Que el módulo Clientes sea **rápido, consistente y confiable**: un solo formulario para alta y edición con
validación clara campo por campo, un listado que encuentre a cualquier cliente sin importar cómo se escriba el
DNI o el teléfono, una ficha que muestre al cliente completo (datos, viajes, pagos, cuenta, contactos) y catálogos
sin duplicados. Seguimiento de clientes es la vara de calidad.

**Fuera de alcance:** facturación, reservas y cuenta corriente (solo se integran como pestañas de la ficha),
reportes de Admin, y el roadmap comercial (recordatorios, CRM).

---

## 2. Diagnóstico verificado

| # | Problema | Evidencia | Causa |
|---|----------|-----------|-------|
| D1 | **El alta tarda decenas de segundos con el depurador** y bloquea el sitio | Captura `NullReferenceException` en `Create.cshtml` línea 102 | `GET Create` hace `return View()` **sin modelo**; los **45 helpers** `*For(model => …)` lanzan una excepción cada uno que MVC atrapa por dentro. Con Visual Studio conectado cada excepción frena el proceso; la sesión `InProc` serializa los demás pedidos del usuario. Los SPs del alta tardan 16 y 5 ms. |
| D2 | Alta y edición son **dos formularios distintos** (390 y 487 líneas) | Asteriscos y reglas que no coinciden; `Nombre` y `PaisResidencia` obligatorios solo en edición | Sin modelo de formulario ni vista compartida; parseo manual con `FormCollection` campo por campo |
| D3 | Etiquetas con nombres internos (`NroDocumento`, `TipoId`…) | La entidad `PersonaCliente` no tiene ningún `[Display]` | La vista usa la entidad de datos como modelo de pantalla |
| D4 | Errores en un aviso general, sin marcar el campo | Banner `validation-banner-create` | Validación manual en JS; no usa la validación estándar de MVC (`jqueryval` ya está en el proyecto) |
| D5 | Valores por defecto puestos con jQuery al cargar | `$("#Nacionalidad").val("ARG")`, `Provincia 16`, `TipoId 2`, `CondicionIva 5` | Sin modelo inicial |
| D6 | **El listado muestra 10 clientes** y no pagina | `PersonaClienteGetTop(searchTerm, 10)` | Reusa un SP de **autocompletado** (también lo usan Reservas, DetalleFactura y Presupuesto) |
| D7 | El buscador no encuentra `11470759` ni `3874140863` | El SP compara con `LIKE` sobre el texto guardado | Sin normalización de documento ni de teléfono |
| D8 | "Altas por período" carga todas las filas (3.808 en una consulta) | Modal con ~30.000 nodos y doble scroll | Sin paginación de servidor |
| D9 | La ficha (`Details`) parece un formulario, muestra el GUID y no tiene nada del cliente como cliente | `Details.cshtml` | Vista de datos sueltos; historial de pagos, facturas y cuenta en páginas aparte |
| D10 | Catálogos duplicados | Prod: 130 provincias / 125 nombres distintos; Salta: `GENERAL GUEMES` (182 clientes) + `GRL.GUEMES` (20), 4 variantes de San Martín, `LOA ANDES`; localidad `SAN LOREAZO` (35) | Carga sin control; `Provincia.IdPais` **ya existe** pero el formulario no filtra por país |
| D11 | Nacionalidad y país en texto libre | Prod: `ARG` 11.148 · `ARGENTINA` 640 · `ARGENTINO` 87 · vacía 116 | Input de texto |
| D12 | Ocupación duplicada por género | `usp_GetAllOcupacion` hace `SELECT DISTINCT` sobre `Persona` | No hay catálogo |
| D13 | Cuenta corriente con dos círculos sin texto | `Edit.cshtml` (`verificarCTA`) | 12.039 de 12.043 cuentas habilitadas: el control casi nunca se usa y no se entiende |
| D14 | Sin estados de carga | Botones sin spinner ni bloqueo | — |
| D15 | Accesibilidad | Rosa `#E63375` sobre blanco 4,12:1 (pide 4,5:1); `user-scalable=0`; buscador sin `<label>`; sin `<h1>` | Layout global |
| D16 | Seguimiento: acciones cortadas a 1017 px; listas traen todo al navegador (Dormido ~5 MB) | `mat.seguimiento.js` con `deferRender` | Paginación del lado del cliente |
| D17 | 404 con pantalla amarilla en local, error genérico en remoto; `debug="true"` en `Web.config` | `customErrors="RemoteOnly"` | Sin página de error propia |
| D18 | No se puede dar de baja un cliente ni saber quién lo modificó | — | Sin estado ni auditoría |
| D19 | Código muerto que no compila | `ClienteController` + `Views/Cliente/*` (sin referencias), `PersonaCliente/Edit - copia.cshtml` | Restos de versiones anteriores. `RenderGridClientes` **no** es código muerto: lo llama `mat.jquery.binding.js:284` |

Descartado tras verificar: documentos de 1 dígito (en producción no existen; solo 2 vacíos) y "Seguimiento pagina en el
servidor" (pagina en el navegador).

---

## 3. Principios de diseño

1. **Modelo de pantalla, no entidad.** Cada formulario tiene su `*FormModel` con `[Display]` y validaciones; la entidad queda para datos.
2. **Una vista por concepto.** Alta, edición y "crear desde presupuesto" usan el mismo parcial.
3. **La misma regla valida en el navegador y en el servidor** (DataAnnotations + validación discreta de MVC). Nunca una regla solo en JS.
4. **Paginar y filtrar en SQL.** Nada que pueda superar unos cientos de filas se manda entero al navegador.
5. **Normalizar en la base, no en cada búsqueda.** Columnas calculadas con dígitos de DNI y teléfonos.
6. **Catálogos como datos**, con relaciones y sin duplicados.
7. **Convenciones del repo:** DBHelper + SP en `MAT.DB` con script en `database/`, `ErrorUtil` en los `catch`, `PROGRESS.md` y `PENDIENTES.md` al cerrar cada fase.
8. **Accesibilidad WCAG 2.1 AA** como criterio de terminado, no como extra.

---

## 4. Arquitectura objetivo

### 4.1 Formulario de cliente

```
Models/Clientes/ClienteFormModel.cs        ← datos de pantalla + [Display] + validaciones
Models/Clientes/ClienteFormDefaults.cs     ← ARG, Salta, Consumidor final, Minorista…
Infrastructure/Clientes/ClienteFormMapper.cs   ← FormModel ↔ Persona/Cliente/Cuenta
Infrastructure/Validation/
    FechaNacimientoAttribute.cs            ← "falta" / "formato" / "futura" / "anterior a 1910" (mensajes separados)
    TelefonoOCelularAttribute.cs           ← al menos uno con 7+ dígitos (IClientValidatable)
    DocumentoAttribute.cs                  ← largo según tipo (DNI 7–8 dígitos; pasaporte alfanumérico)
Views/PersonaCliente/_ClienteForm.cshtml   ← único formulario (secciones)
Views/PersonaCliente/Create.cshtml         ← título + _ClienteForm
Views/PersonaCliente/Edit.cshtml           ← título + _ClienteForm + cuenta corriente
Scripts/mat.clientes.form.js               ← cascada geográfica, DNI remoto, guardado con spinner
```

- **Controlador:** `Create()` devuelve `View(ClienteFormDefaults.Nuevo())`; `CreateFromPresupuesto` reusa el mismo armado.
  `Create(ClienteFormModel)` y `Edit(Guid, ClienteFormModel)` con `[ValidateAntiForgeryToken]`, `ModelState` y patrón
  POST-redirect-GET. Si el pedido es AJAX devuelven JSON con los errores por campo (se mantiene `returnUrl`).
  Se eliminan `CreateAjax` y el parseo con `FormCollection`.
- **DNI repetido:** `[Remote("DocumentoDisponible")]` que excluye al propio cliente en la edición y devuelve el
  nombre y el enlace del cliente existente, mostrado bajo el campo (sin modal).
- **Guardado transaccional:** nuevo `usp_MAT_PersonaCliente_Guardar` (Persona + Cliente + Cuenta en una
  transacción). Hoy la edición hace dos `UPDATE` separados sin transacción.
- **Secciones del formulario:** Datos personales · Contacto · Domicilio · Datos comerciales · Otros (colapsada).
  Barra de acciones fija abajo: **Guardar**, **Guardar y cargar otro**, Cancelar.
- **Cuenta corriente:** interruptor con rótulo "Cuenta corriente habilitada" dentro del formulario de edición.
- **Sexo:** "Seleccioná…" vacío en lugar de Masculino por defecto. **Tipo de cliente** en lugar de `TipoId`.

### 4.2 Listado y búsqueda

- **Endpoint nuevo** `PersonaCliente/Buscar` (no se toca `GetClientesTop`, que usan otras pantallas) con
  `q`, filtros (`categoria`, `localidad`, `tipo`, `estado`), `pagina`, `tamanio`, `orden`.
- **SP nuevo** `usp_MAT_PersonaCliente_Buscar` con `OFFSET/FETCH` y `COUNT(*) OVER()` para el total.
  - Término numérico → compara contra **dígitos** de documento, teléfono y celular.
  - Texto → palabras en cualquier orden (como hoy) sobre apellido, nombre, email.
- **Columnas calculadas persistidas** en `Persona`: `TelefonoDigitos`, `CelularDigitos` (con índice), junto a la
  ya existente `NroDocumentoCalc`.
- **Pantalla:** tabla densa (~56 px por fila) con avatar de iniciales, nombre, documento y teléfono, badges de
  categoría con color, dos acciones visibles (Ver ficha, WhatsApp) y menú de tres puntos para el resto.
  Contador "Mostrando 1–25 de 342", paginación, orden por columna, encabezado fijo, botón ✕ para limpiar, el
  término y la página en la URL, fila clickeable, skeleton mientras carga. En celular, tarjetas compactas.
- **Altas por período:** mismo patrón (SP paginado), presets (este mes, mes pasado, este año) y exportar a Excel (EPPlus, como Reportes).

### 4.3 Ficha del cliente (`Details`) como perfil

- Encabezado: avatar, nombre, badges (categoría, menor de edad, no contactar), acciones (Editar, WhatsApp, Registrar contacto).
- Pestañas: **Datos** · **Viajes** · **Pagos** · **Cuenta corriente** · **Facturas** · **Contactos**.
  Reusan lo existente: `usp_MAT_Seguimiento_Ficha` (viajes, métricas, contactos), `partialHistorialdePagos`,
  `PartialFacturas`, `CuentaCorriente`. Carga perezosa por pestaña.
- Datos como texto (rótulo arriba, valor abajo) en grilla de 3 columnas, `—` tenue para vacíos, Departamento visible,
  ID solo en un "copiar ID" al pie.

### 4.4 Catálogos

- **Geografía:** cascada **País → Provincia → Departamento → Localidad** usando `Provincia.IdPais` existente.
  Combos con búsqueda (**Tom Select**, MIT, sin dependencias, archivo local en `Scripts/lib`).
  Botón "+ Nueva localidad" con rótulo.
- **Nacionalidad y país de residencia:** select sobre `Pais`. Se guarda el código actual (`ARG`) para no romper
  datos ni reportes; se normalizan las variantes (ver decisión 8.1).
- **Ocupación:** tabla `Ocupacion` (nombres neutros, activo/inactivo) y select; se normalizan los valores existentes.
- **Deduplicación** con un script de migración que: (1) arma una tabla de equivalencias duplicado → canónico,
  (2) respalda, (3) reapunta `Persona.LocalidadID`, `Persona.Provincia` y `Paquete.DestinoID` (FK a Localidad),
  (4) borra los duplicados sin referencias. Idempotente, probado en DEV antes de Prod.

### 4.5 Transversal

- `Views/Shared/_Loading` + `MatUI.cargando(boton)` para spinner y bloqueo en cualquier acción > 300 ms.
- Página de error propia (404 y 500) con `customErrors`/`httpErrors`; confirmar `debug="false"` en la publicación.
- Quitar `user-scalable=0` y `maximum-scale` del viewport; `<h1>` y `<label>` en todas las pantallas del módulo.
- Color de marca accesible para texto sobre rosa (ver decisión 8.4).
- Seguimiento: acciones como primera columna (o fijas), paginación en el servidor reutilizando el patrón de 4.2,
  campo **Canal** en el registro de contacto, mensajes de fecha separados.

### 4.6 Baja y auditoría

- `Cliente.Activo` + `FechaBaja` + `MotivoBaja`: los inactivos no aparecen en listados, búsquedas de reserva ni
  Seguimiento (filtro por defecto, visibles con "Mostrar inactivos").
- Tabla `ClienteAudit` (patrón de `ViajeAudit`/`AuditFactura`): alta, modificación con campos cambiados, baja; quién y cuándo.
  Pestaña "Historial de cambios" en la ficha para administradores.

---

## 5. Fases

Cada fase es un PR independiente contra `MAT2026`, con su script de base (si corresponde) probado en DEV.

| Fase | Contenido | Resuelve | Estimación |
|------|-----------|----------|------------|
| **F0. Preparación** | Usuario de prueba; métricas base (tiempos de alta, listado, ficha); checklist de pruebas manuales; borrar código muerto (`ClienteController` + `Views/Cliente/*`, `Edit - copia`); revisar si la pantalla que usa `RenderGridClientes` (`mat.jquery.binding.js:284`) sigue en uso; arreglar `Verify-NetTiersMigration.ps1` (no revisa subcarpetas) | D19 | 1 día |
| **F1. Formulario de cliente** | 4.1 completo: `ClienteFormModel`, validadores, parcial único, alta/edición/desde presupuesto, DNI remoto con enlace, guardado transaccional, cuenta corriente con interruptor, barra fija | D1–D5, D13 | 4 días |
| **F2. Listado y búsqueda** | 4.2: SP y endpoint `Buscar`, columnas calculadas, tabla densa con filtros, URL, contador, Altas por período paginado + Excel | D6–D8 | 3–4 días |
| **F3. Ficha como perfil** | 4.3: encabezado, pestañas con carga perezosa, integración con Seguimiento | D9 | 3 días |
| **F4. Catálogos y datos** | 4.4: País en la cascada, Tom Select, nacionalidad/ocupación como select, script de deduplicación (DEV → Prod con OK) | D10–D12 | 3–4 días + validación del negocio |
| **F5. Transversal y Seguimiento** | 4.5: carga, 404/500, viewport, contraste, `<h1>`/labels, Seguimiento paginado en servidor, acciones visibles, canal de contacto | D14–D17 | 2–3 días |
| **F6. Baja y auditoría** | 4.6 | D18 | 2 días |

**Total estimado:** 18–22 días de desarrollo. **Orden:** F0 → F1 → F2 → F3 → F4 → F5 → F6.
F1 va primero porque arregla el bloqueo y todo lo demás (ficha, catálogos) se apoya en el formulario nuevo.
F4 puede adelantarse si el negocio prioriza la calidad de datos para reportes.

---

## 6. Criterios de aceptación

**F1**
- `GET Create` no lanza ninguna excepción (verificado con "Break when thrown" activo) y responde en < 1 s.
- Alta, edición y "crear desde presupuesto" usan el mismo parcial; los asteriscos coinciden con las validaciones.
- Cada error aparece bajo su campo, en rojo, y el foco va al primero; las mismas reglas rechazan el envío si se saltea el navegador.
- Fecha `31/12/2099` → "La fecha de nacimiento no puede ser futura". Fecha `31/02/1980` → "Fecha inválida".
- DNI existente → aviso bajo el campo con nombre y enlace al cliente; en edición no se avisa por el propio DNI.
- Guardar con error de base no deja Persona sin Cliente (transacción).

**F2**
- `11470759`, `11.470.759`, `3874140863` y `0387 414-0863` encuentran al mismo cliente.
- `mamani` muestra el total real y permite paginar; búsqueda < 300 ms en producción.
- Recargar la página o usar "atrás" conserva búsqueda y página.
- Altas por período no supera 50 filas en pantalla y exporta todas a Excel.

**F3**
- La ficha muestra categoría, último contacto, viajes, pagos, cuenta, facturas y contactos sin salir de la página.
- No aparece el GUID en pantalla ni en el breadcrumb.

**F4**
- Provincia filtrada por país; no quedan nombres duplicados en Provincia ni Departamento (Salta); las personas
  conservan su ubicación tras la migración (conteos antes/después iguales).
- Nacionalidad sin variantes (`ARG` única para Argentina).

**F5**
- Contraste ≥ 4,5:1 en texto sobre color de marca; zoom habilitado en celular.
- URL inexistente → página 404 propia. Ninguna acción > 300 ms sin indicador de carga.
- Seguimiento a 1017 px muestra el botón de WhatsApp sin scroll horizontal; Dormido no descarga más de una página.

**F6**
- Cliente dado de baja no aparece en listados ni en autocompletados de reserva; su historial sigue accesible.
- Cada alta, modificación y baja queda registrada con usuario y fecha.

**En todas:** MSBuild y `tools/Run-Tests.ps1` OK, vistas compiladas con `aspnet_compiler`, scripts con paridad en
`MAT.DB`, `PROGRESS.md` actualizado, checklist manual pasado con el usuario de prueba.

---

## 7. Riesgos y cómo se mitigan

| Riesgo | Mitigación |
|--------|-----------|
| Romper el autocompletado de Reservas/Presupuesto al tocar la búsqueda | No se modifica `usp_MAT_PersonaCliente_GetTop` ni `GetClientesTop`; el listado usa un SP nuevo |
| Romper "crear cliente desde presupuesto" | Mismo parcial y mapper; caso incluido en el checklist de F1 |
| Manejadores globales de `mat.jquery.binding.js` sobre `#frmCreateCliente #NroDocumento` (línea 1320, chequeo de DNI) interfiriendo con el formulario nuevo | Inventariar y retirar los manejadores del módulo en F1; el formulario usa su propio JS (`mat.clientes.form.js`) |
| Deduplicar catálogos pierde la ubicación de clientes | Tabla de equivalencias revisada por el negocio, respaldo, conteos antes/después, DEV primero |
| Cambiar el color de marca afecta todo el sitio | Decisión explícita (8.4); token CSS único y revisión visual de las pantallas principales |
| Columnas calculadas persistidas sobre `Persona` (12.000 filas) bloquean la tabla al crearlas | Script en horario sin uso; es una operación de segundos con este volumen |
| Probar sin usuario real | F0 resuelve el usuario de prueba antes de empezar |

---

## 8. Decisiones necesarias antes de empezar

| # | Pregunta | Recomendación |
|---|----------|---------------|
| 8.1 | Nacionalidad y país de residencia: ¿se guarda el código (`ARG`) o el nombre? | Código, mostrando el nombre; normalizar `ARGENTINA`/`ARGENTINO` → `ARG` |
| 8.2 | Localidades `CAPITAL` (9.292 clientes) y `SALTA` (520) en Salta capital: ¿son la misma? | Unificar en una sola, "Salta (Capital)" |
| 8.3 | Ocupaciones: ¿catálogo con nombres neutros (`Empleado/a`, `Jubilado/a`)? | Sí |
| 8.4 | Color de marca: ¿oscurecer el rosa en todo el sitio (`#C9245F`) o solo para texto/botones? | Solo para botones y texto sobre rosa; el logo y los acentos quedan igual |
| 8.5 | ¿Quién puede dar de baja un cliente? | Solo Administrador |
| 8.6 | Seguimiento: ¿mostrar Dormido primero (58 % de la base) o mantener el orden por prioridad comercial? | Mantener VIP → En riesgo primero (acción), con el total de Dormido visible arriba |
| 8.7 | ¿Agregar "Canal" (WhatsApp / llamada / email / presencial) al registrar contacto? | Sí |
| 8.8 | Usuario de prueba para validar en el navegador (DEV) | Imprescindible para F0 |

---

## 9. Cómo se prueba

- **Unitarias:** validadores (fecha, teléfono, documento), mapper, normalización de búsqueda, armado de defaults.
- **Base (DEV):** cada SP con casos reales (DNI con y sin puntos, teléfonos en 5 formatos, paginación límites), tiempos.
- **Vistas:** `aspnet_compiler` sobre el sitio.
- **Manual con usuario de prueba:** checklist por fase (alta feliz, alta con cada error, edición de cliente viejo
  sin país/teléfono, desde presupuesto, búsqueda, ficha, baja). Navegador de escritorio y celular.
- **Accesibilidad:** contraste, navegación con teclado, lector de pantalla en formulario y listado.
