# Roadmap comercial 2026 — MAT

Hoja de ruta de las herramientas comerciales de la intranet: seguimiento de clientes, recordatorios,
notificaciones, CRM y liquidación de comisiones. Nació del brainstorm del 2026-07-07 (`SPEC.md`,
"Plataforma comercial") y se concretó con el seguimiento de clientes del 2026-10-29.

- **Tareas y estado al día:** [`docs/PENDIENTES.md`](../docs/PENDIENTES.md) (fuente de verdad; este documento no se tilda).
- **Decisiones:** [`docs/DECISIONES_ABIERTAS.md`](../docs/DECISIONES_ABIERTAS.md).
- **Guía para vendedores:** [`GUIA_VENDEDORES_SEGUIMIENTO_CLIENTES.md`](GUIA_VENDEDORES_SEGUIMIENTO_CLIENTES.md).

---

## Objetivo

Que los vendedores sepan **a quién contactar cada día, por qué y con qué propuesta**, y que el equipo vea lo que
ya se habló con cada cliente. Más segundos viajes, recuperar a los que dejaron de viajar y no perder el contacto
con los clientes fieles.

## Punto de partida (producción, 29/10/2026)

| Dato | Valor | Qué implica |
|------|-------|-------------|
| Pasajeros que viajaron alguna vez | ~10.300 | — |
| Viajaron una sola vez | 68 % | La oportunidad principal es el **segundo viaje** |
| Clientes con WhatsApp válido | 83 % (10.017 de 12.024) | El canal es WhatsApp |
| Clientes con email válido | 3,6 % | El email no sirve como canal masivo |
| Cumpleaños por día | ~33 | Una lista diaria es manejable para el equipo (8 vendedores activos) |
| Contactos registrados antes del seguimiento | Ninguno | No había forma de saber quién habló con quién |

Clientes por categoría al arrancar (aprox., antes de sumar los viajes pagados para otros):
VIP 384 · En riesgo 322 · Frecuente 527 · Nuevo 1.026 · Ocasional 1.403 · Dormido 6.641 · Sin viajes 1.721.

---

## Fases

### Fase 0 — Datos confiables ✅ hecho (2026-10-29, PR #27)

- Enlaces de WhatsApp bien armados para números argentinos (`WhatsAppHelper`).
- Limpieza en producción: 569 celulares, 1.772 teléfonos y 75 fechas de nacimiento basura, con respaldo.
- Alta y edición de cliente: teléfono **o** celular completo y fecha de nacimiento obligatoria, validados en navegador y servidor.

### Fase 1 — Seguimiento de clientes 🟡 listo en código (PR #28), falta publicar

- Pantalla `/Seguimiento`: cumpleaños del día con saludo por WhatsApp, 7 categorías con acción sugerida, búsqueda y ficha del cliente.
- Registro de contactos (`ClienteContacto`) con aviso cuando otro vendedor ya contactó al cliente; "No volver a contactar".
- Recuadro "Cumpleaños de hoy" en el inicio.
- **Para cerrar:** publicar `database/2026-10-29_SeguimientoClientes.sql` en producción, desplegar y probar con un vendedor.

### Fase 2 — Recordatorios ⏳ próxima

Que el sistema diga **cuándo volver a llamar**, en vez de depender de la memoria del vendedor.

- Al registrar "Interesado" o "No contesta", proponer una fecha para volver a contactar (por ejemplo, 7 días).
- Lista "Para retomar hoy" en la pestaña Para hoy.
- Presupuestos por vencer (el módulo Presupuesto ya tiene vencimiento) en la misma lista.
- Depende de: Fase 1 en producción. Usa la tabla `ClienteContacto` (sumar fecha de próximo contacto).

### Fase 3 — Notificaciones en la intranet

- Aviso al entrar: cumpleaños sin saludar, contactos para retomar, presupuestos por vencer.
- Decisión abierta: ¿solo dentro de la intranet o también por email/WhatsApp al vendedor?

### Fase 4 — CRM

- Embudo por cliente: interesado → presupuesto → reserva → viaje, con el historial de contactos que ya se registra.
- Campañas por categoría (por ejemplo, salida nueva para los "En riesgo" de una zona) con registro masivo del envío.
- Decisiones abiertas: ¿lead y presupuesto son lo mismo? ¿lo usan también counter/agencias?

### Fase 5 — Liquidador de comisiones (solo Administrador)

- Comisión por vendedor a partir de facturas y pagos.
- Decisión abierta: ¿hay reglas de comisión hoy en Excel para replicar?

### Relacionado (fuera de este roadmap)

- **Resumen de pagos por viaje para vendedores** (P2 en `PENDIENTES.md`): complementa la ficha del cliente.
- **Integration API** (P3): los recordatorios y notificaciones podrían emitir webhooks; el CRM podría recibir leads.

---

## Decisiones

| Pregunta (brainstorm 2026-07-07) | Estado |
|----------------------------------|--------|
| ¿Canales externos: alcanza WhatsApp manual o hace falta API? | **Resuelta (2026-10-29):** WhatsApp manual con enlace y saludo armado. Revisar si el volumen lo justifica. |
| ¿Lead y Presupuesto son entidades distintas? | Abierta — necesaria para la Fase 4 |
| ¿Reglas de comisión en Excel para replicar? | Abierta — necesaria para la Fase 5 |
| ¿Quién usa el CRM: vendedores internos o también counter/agencias? | Abierta |
| ¿Notificaciones solo en la intranet o también email/celular? | Abierta — necesaria para la Fase 3 |

Decisiones del seguimiento de clientes (categorías, saludo, contactos, menores, viajes pagados para otros, hora
argentina): ver `docs/DECISIONES_ABIERTAS.md`.

---

## Cómo medir si funciona

Medir cada mes desde que la Fase 1 esté en producción (los datos salen de `ClienteContacto` y de las facturas):

| Indicador | Cómo se mide | Meta inicial sugerida |
|-----------|--------------|------------------------|
| Cumpleaños saludados | Saludos registrados / cumpleaños con WhatsApp válido | 80 % |
| Contactos registrados | Contactos por vendedor por semana | Tendencia en alza |
| Segundo viaje | Clientes "Nuevo" que compran otro viaje en 6 meses | Medir base el primer mes |
| Recuperados | Clientes "En riesgo" contactados que vuelven a comprar en 3 meses | Medir base el primer mes |
| Calidad de datos | % de clientes con WhatsApp válido y fecha de nacimiento | ≥ 85 % |
