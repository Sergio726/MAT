# Handoff — MAT (Marco Antonio Tours)

Documento de relevo. Si acabás de entrar al repo, empezá acá.

> **Estado en una línea:** Intranet ASP.NET MVC operativa; NetTiers F0–F12 cerrado en código; trabajo abierto en rentabilidad por viaje, Integration API y Excursion UX; varios SPs pendientes de publicar en BD por entorno.

---

## Orden de lectura

1. **Este archivo** (`docs/HANDOFF.md`)
2. [`../CLAUDE.md`](../CLAUDE.md) — stack, arquitectura, convenciones y comandos
3. [`PENDIENTES.md`](PENDIENTES.md) — qué falta y de quién depende (**fuente de verdad de tareas**)
4. [`DECISIONES_ABIERTAS.md`](DECISIONES_ABIERTAS.md) — antes de schema/rentabilidad o copy frágil
5. [`../DOCUMENTACION/GUIA_SISTEMA_MAT.md`](../DOCUMENTACION/GUIA_SISTEMA_MAT.md) — guía técnica amplia
6. [`../PROGRESS.md`](../PROGRESS.md) — bitácora cronológica (no checklist)
7. [`../SPEC.md`](../SPEC.md) — **histórico** de features ya hechas (no abrir trabajo nuevo ahí)

Docs vivos en [`DOCUMENTACION/`](../DOCUMENTACION/README.md). Material cerrado: [`DOCUMENTACION/Archive/`](../DOCUMENTACION/Archive/README.md).

---

## Roles

| Quién | Qué hace |
|---|---|
| **Equipo de código** | Ítems de `PENDIENTES.md` (sección Trabajo del equipo) |
| **Operaciones / contabilidad** | Definición de rentabilidad y costos por viaje |
| **Humano (deploy)** | Publicar scripts `database/` / SPs de `MAT.DB` en cada entorno |

---

## Forma de trabajo

No hay Issues ni board para estas tareas. La colaboración es el checklist + el código.

- **Fuente de verdad de tareas abiertas:** [`PENDIENTES.md`](PENDIENTES.md). Checkboxes `[ ]` / `[x]`.
- **Al cerrar algo:** tildar `[x]` en el mismo commit o PR. Se tilda; **no se reescribe** el historial.
- **Bitácora:** al terminar un task, una entrada en `PROGRESS.md` (formato del repo).
- **SPEC.md:** solo referencia histórica / ya `[x]`. Tasks nuevos van a `PENDIENTES.md`.
- **Sin board:** no crear Issues para trackear lo que ya está en pendientes.

### Comandos útiles

```bash
# Compilar MAT.MVC
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" MAT.MVC\MAT.MVC.csproj /t:Build /p:Configuration=Debug

# Tests (sin BD)
.\tools\Run-Tests.ps1
```

Arranque local: Visual Studio 2022, proyecto de inicio `MAT.MVC`, IIS Express.

---

## Qué no hacer

1. Commitear `Web.config` con passwords de producción
2. Aplicar scripts SQL sin paridad en `MAT.DB`
3. Exponer `e.Message` al usuario — usar `ErrorUtil.LogAndGetPublicMessage`
4. Agregar usos nuevos de NetTiers `*Service` (proyecto eliminado; patrón = DBHelper + SP)
5. Publicar `MAT.DB` contra la BD equivocada
6. Inventar Issues para lo que ya está en `PENDIENTES.md`

---

## Trabajo abierto destacado

Ver [`PENDIENTES.md`](PENDIENTES.md):

1. **Bloquea deploy:** publicar SPs recientes en cada entorno
2. **Negocio:** definición de rentabilidad por viaje
3. **Producto:** resumen de pagos por viaje + medio de pago (módulo Factura / vendedores)
4. **Producto:** Integration API F0–F6 y Excursion UX F0–F3
