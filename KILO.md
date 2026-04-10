# KILO.md — Guía rápida para trabajar con Kilo (Claude Code)

## Inicio de sesión

Antes de empezar, leé en este orden:
1. **CLAUDE.md** — Stack, arquitectura, convenciones
2. **PROGRESS.md** — Qué se hizo, problemas, estado actual
3. **SPEC.md** — Tu objetivo: el primer task con `[ ]`

**Kilo arrancará automáticamente desde ahí.** No rehagas lo ya marcado `[x]`.

---

## Cómo dar instrucciones a Kilo

### Para un task nuevo
Pegá este formato al inicio de tu mensaje:

```
### Nueva tarea para Kilo

**Task:** [descripción breve]
**Archivos:** [qué vas a tocar]
**Prioridad:** [urgente / normal / baja]
** deadline:** [si hay]
**Notas:** [algo que deba saber?]
```

### Para agregar un bug urgente
Antes de continuar con el SPEC normal:

```
BUG URGENTE:
- Qué: [una línea]
- Dónde: [archivo/pantalla]
- Cómo reproducir: [pasos]
- Prioridad: urgente — resolver antes del SPEC
```

Kilo documentará el fix en PROGRESS.md al terminar.

---

## Cómo agregar tareas al SPEC.md

### Formato para tareas nuevas

Agregá en la sección correspondiente (P1 / P2 / P3 / Seguridad):

```markdown
- [ ] **Título breve**
   Descripción del task. Puede ocupar varias líneas si es necesario.
   **Archivos probables:** `ruta/archivo.cs`, `Views/...`
   **Criterio de éxito:** qué significa que está terminado
```

### Ejemplo

```markdown
- [ ] **Admin: nueva feature**
   Agregar botón de exportación en la página de usuarios.
   Archivos: Controllers/Admin/AdminController.cs, Views/Admin/Usuarios.cshtml
   Criterio de éxito: Exporta CSV con todos los usuarios
```

### No tocar
- Tasks ya marcados `[x]` (completados)
- La estructura de secciones del SPEC
- Los criterios de éxito existentes

---

## Reglas de handoff

1. **PROGRESS.md actualizado** antes de cerrar sesión
2. **Task terminado o documentado** — nada a medias
3. **Si se bloquea:** estado ⚠️ bloqueado + motivo en PROGRESS.md
4. **MSBuild pasa** antes de marcar task como completo

---

## Commands útiles

```bash
# Compilar solución
msbuild MAT.sln /t:Build /p:Configuration=Debug

# Compilar solo MVC
msbuild MAT.MVC\MAT.MVC.csproj /t:Build /p:Configuration=Debug
```

---

## Contacto

- Stack: ASP.NET MVC 4 / .NET 4.8 / Bootstrap 5 / jQuery 3
- Rama: `MAT2026`
- Rama integración: `MASTER`

Si necesitás más contexto, consultá `DOCUMENTACION/`.