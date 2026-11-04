# Decisiones abiertas

Antes de tocar schema o copy frágil, resolver acá (y tildar en [`PENDIENTES.md`](PENDIENTES.md)).

---

## Rentabilidad por viaje

**Contexto:** [`../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md`](../DOCUMENTACION/VIAJE_RENTABILIDAD_GASTOS_INVESTIGACION.md)

| Pregunta | Estado |
|----------|--------|
| ¿Ingresos = facturado, cobrado, o ambos? | Pendiente negocio |
| ¿Qué costos imputar (fiscales, hotel, transporte, imprevistos)? | Pendiente negocio |
| ¿Moneda y prorrateo entre viajes? | Pendiente negocio |
| ¿Solo Admin o también vendedores? | Pendiente negocio |
| ¿Preferencia A+B vs Planilla (C)? | Preliminar: A+B; confirmar |

Hasta no registrar respuestas, **no** implementar columnas/tablas nuevas de rentabilidad.

---

## Seguimiento de clientes para vendedores — resueltas 2026-10-29

**Contexto:** pantalla para que los vendedores saluden en cumpleaños y hagan seguimiento por frecuencia de viaje. Datos de producción (29/10/2026): 68 % de los pasajeros viajó una sola vez, 1.077 frecuentes (3+ viajes) llevan más de 12 meses sin viajar, 83 % de los clientes tiene WhatsApp válido y solo 3,6 % email.

| Pregunta | Decisión |
|----------|----------|
| Categorías | VIP (5+ viajes y viajó en 12 meses) · En riesgo (3+ viajes, 12–24 meses sin viajar) · Frecuente (2–4 viajes, viajó en 12 meses) · Nuevo (1 viaje en 6 meses) · Ocasional (1–2 viajes, 6–24 meses sin viajar) · Dormido (más de 24 meses). Si entra en dos, gana la de arriba. |
| ¿Qué clientes ve cada vendedor? | **Todos** los clientes. |
| Regalos | **Sin presupuesto por ahora:** solo saludos. |
| Texto del saludo | "¡Feliz cumpleaños, {Nombre}! 🎉 De parte de todo el equipo de Marco Antonio Tours te deseamos un gran día. Gracias por viajar con nosotros. — {Vendedor}" |
| Registro de contactos | **Sí.** Resultados: Saludo de cumpleaños · Ofrecido viaje · Interesado · Compró · No le interesa · No contesta · Número equivocado · No volver a contactar (sale de las listas). |
| Contacto repetido entre vendedores | **Avisar sin bloquear** ("Ya lo saludó X hoy" / "Contactado hace N días por X"). |
| Ubicación | Recuadro "Cumpleaños de hoy" en Home + menú "Seguimiento de clientes" (para hoy, categorías, ficha, historial de contactos). |
| Fecha de nacimiento en el alta | **Obligatoria** y validada (1910–hoy) en navegador y servidor. Las fechas repetidas existentes se marcan "verificar con el cliente". |
| Canal | WhatsApp vía `WhatsAppHelper` (email descartado: 3,6 % de cobertura). |
| Menores de 18 | Se muestran marcados **"Menor de edad"** (en Prod hay 1.719 clientes menores; ~10 % de los cumpleaños de la semana). |
| Viajes que cuentan | Los del cliente como pasajero **y** los que pagó para otros (factura a su nombre); la ficha distingue "Viajó" / "Pagó para otros". |
| "Hoy" | Hora argentina (`AT TIME ZONE 'Argentina Standard Time'`): el servidor SQL está en UTC. |

---

## Módulo Clientes — rediseño (resueltas 2026-11-04)

**Contexto:** [`../DOCUMENTACION/PLAN_MODULO_CLIENTES_REDISENO.md`](../DOCUMENTACION/PLAN_MODULO_CLIENTES_REDISENO.md), sección 8.

| Pregunta | Recomendación | Estado |
|----------|---------------|--------|
| Nacionalidad / país de residencia: ¿código (`ARG`) o nombre? | Código, mostrando el nombre; normalizar `ARGENTINA`/`ARGENTINO` → `ARG` | **Aceptada** |
| Localidades `CAPITAL` (9.292) y `SALTA` (520) en Salta capital: ¿unificar? | Sí, "Salta (Capital)" | **Aceptada** |
| Ocupaciones con nombres neutros (`Empleado/a`) | Sí | **Aceptada** |
| Color de marca: ¿`#C9245F` en todo el sitio o solo botones/texto sobre rosa? | Solo botones y texto sobre rosa | **Aceptada** |
| ¿Quién da de baja clientes? | Solo Administrador | **Aceptada** |
| Seguimiento: ¿Dormido primero o orden por prioridad comercial? | Prioridad comercial, con total de Dormido visible | **Aceptada** |
| ¿Canal en el registro de contacto? | Sí | **Aceptada** |
| Usuario de prueba en DEV | Imprescindible para F0 | **Aceptada — falta crear el usuario** |

El usuario aceptó todas las recomendaciones el 2026-11-04. Solo falta el usuario de prueba para validar en el navegador.

---

## Forma de documentar

- Tareas: solo [`PENDIENTES.md`](PENDIENTES.md)
- Histórico largo: `SPEC.md` / `PROGRESS.md`
- Docs cerrados: `DOCUMENTACION/Archive/`
