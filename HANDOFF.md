# HANDOFF.md — Protocolo de continuidad entre agentes

Este archivo explica cómo cualquier agente (Claude Code, Cursor, otro)
debe retomar el trabajo en este proyecto.

---

## Archivos que definen el estado del proyecto

| Archivo | Propósito |
|---------|-----------|
| CLAUDE.md | Stack, arquitectura, convenciones y modo de trabajo autónomo |
| SPEC.md | Fuente de verdad de qué construir. [x] = hecho, [ ] = pendiente |
| PROGRESS.md | Bitácora cronológica de lo que se fue haciendo |
| HANDOFF.md | Este archivo — protocolo de continuidad |

---

## Prompt de handoff (pegarlo al arrancar una sesión nueva)

Antes de escribir cualquier código, leé en este orden:
1. CLAUDE.md → reglas del proyecto, stack, convenciones, modo de trabajo
2. PROGRESS.md → qué se hizo, qué problemas hubo, estado actual
3. SPEC.md → buscá el primer task con [ ] — ese es tu objetivo

Tu trabajo es continuar desde donde quedó el agente anterior.
No rehagas lo que ya está marcado [x] en SPEC.md.
No cambies arquitectura sin consultarme.

Antes de arrancar, confirmame:
- Cuál es el task que vas a encarar
- Qué archivos vas a tocar
- Si hay algo ambiguo en el SPEC que necesites aclarar

Después arrancá autónomamente siguiendo el flujo de CLAUDE.md.

---

## Prompt para agregar un bug urgente

Antes de continuar con el siguiente task del SPEC.md,
hay una tarea urgente:

BUG: [describí el bug en una línea]
Dónde: [archivo o pantalla donde aparece]
Cómo reproducirlo: [pasos o condición]
Prioridad: urgente — resolver antes de continuar con el SPEC

Al terminar:
- Documentá el fix en PROGRESS.md
- Volvé al orden normal del SPEC.md

---

## Prompt para agregar una tarea nueva al SPEC

Agregá este task al SPEC.md en la sección P1/P2/P3 según corresponda,
sin modificar los tasks existentes:

- [ ] [descripción del task nuevo]
  [criterio de éxito]

No lo encarés todavía, seguí con el orden actual del SPEC.

---

## Reglas de handoff

- Antes de cerrar una sesión, dejar PROGRESS.md actualizado
- Nunca cerrar con un task a medias — o se termina o se documenta dónde quedó
- Si un task quedó bloqueado: estado ⚠️ bloqueado + motivo en PROGRESS.md
- El siguiente agente arranca siempre desde el primer [ ] en SPEC.md

---

## Relación entre herramientas

| Herramienta | Cuándo usarla |
|-------------|--------------|
| Claude Code | Tasks complejos, multi-archivo, decisiones de arquitectura |
| Cursor Agent | Tasks simples/medios cuando Claude Code no está disponible |
| Ambos | Leen los mismos CLAUDE.md + SPEC.md + PROGRESS.md |
