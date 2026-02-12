# Estilo estándar: modal de confirmación

Este documento define el patrón visual y de comportamiento que debe usarse para los **modales de confirmación** (acciones irreversibles o que requieren validación explícita del usuario).  
Referencia de implementación: **Hotel/Distribución** — “Quitar pasajero de la habitación”.

---

## 1. Estructura HTML (Bootstrap 5)

- Modal con `data-bs-backdrop="static"` para que no se cierre al hacer clic fuera.
- Contenido centrado con `modal-dialog-centered`.
- Clase de alcance en `.modal-content` (ej. `modal-confirm-*`) para no afectar otros modales.

```html
<div class="modal fade" id="modalConfirmacionEjemplo" tabindex="-1" 
     aria-labelledby="modalConfirmacionEjemploLabel" aria-hidden="true" 
     data-bs-backdrop="static" data-bs-keyboard="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content modal-confirm-estandar">
            <div class="modal-header modal-confirm-estandar-header">
                <h5 class="modal-title" id="modalConfirmacionEjemploLabel">
                    <i class="bi bi-exclamation-triangle-fill"></i> Confirmar acción
                </h5>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
            </div>
            <div class="modal-body">
                <p id="modal-confirmacion-mensaje" class="mb-0"></p>
                <p class="text-danger small mb-0 mt-2">
                    <strong>Texto de advertencia opcional.</strong> Ej.: "Este cambio es irreversible."
                </p>
            </div>
            <div class="modal-footer modal-confirm-estandar-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                <button type="button" class="btn btn-danger" id="btn-modal-confirmar">
                    <i class="bi bi-[icono-accion]"></i> Texto del botón de acción
                </button>
            </div>
        </div>
    </div>
</div>
```

---

## 2. Estilos CSS a usar

- **Header**: fondo azul corporativo en gradiente; texto blanco; sin borde inferior.
- **Título**: negrita (font-weight 600).
- **Footer**: borde superior discreto; botón principal en rojo para acciones destructivas o de riesgo.

Colores de referencia del proyecto:

| Uso              | Valor |
|------------------|--------|
| Header (gradiente) | `linear-gradient(135deg, #1e3a5f 0%, #2d4a6f 100%)` |
| Texto en header   | `#fff` |
| Botón “Confirmar” (destructivo) | Gradiente: `#c2185b` → `#ad1457`; hover: `#ad1457` → `#880e4f` |
| Footer (borde)    | `1px solid #e0e0e0` |

Ejemplo de bloque de estilos (clase de alcance `.modal-confirm-estandar`):

```css
.modal-confirm-estandar .modal-header {
    background: linear-gradient(135deg, #1e3a5f 0%, #2d4a6f 100%);
    color: #fff;
    border-bottom: none;
}
.modal-confirm-estandar .modal-title {
    font-weight: 600;
}
.modal-confirm-estandar .modal-footer {
    border-top: 1px solid #e0e0e0;
    padding: 1rem 1.25rem;
}
/* Botón de acción principal (ej. eliminar / quitar) */
.modal-confirm-estandar .modal-footer .btn-danger {
    background: linear-gradient(135deg, #c2185b 0%, #ad1457 100%);
    border: none;
}
.modal-confirm-estandar .modal-footer .btn-danger:hover {
    background: linear-gradient(135deg, #ad1457 0%, #880e4f 100%);
}
```

- Cerrar del header: `btn-close-white` para que sea visible sobre el fondo azul.
- Mensaje de advertencia: `text-danger` + `small` en un párrafo secundario.

---

## 3. Comportamiento recomendado (JavaScript)

1. **Abrir**: al hacer clic en el disparador (ej. “Quitar”), actualizar el mensaje en el modal, guardar en el modal los datos necesarios para la acción (por ejemplo con `$modal.data("payload", { ... })`) y luego mostrar el modal con `bootstrap.Modal.getOrCreateInstance(modalEl).show()`.
2. **Confirmar**: al hacer clic en el botón de aceptar, leer el payload del modal, cerrar el modal (`bootstrap.Modal.getInstance(modalEl).hide()`), borrar el payload y ejecutar la acción (AJAX, etc.).
3. **Cancelar**: solo cerrar el modal (`data-bs-dismiss="modal"` o `hide()`); no ejecutar la acción.
4. No usar `confirm()` ni `alert()` del navegador para estas confirmaciones; usar siempre un modal con el estilo anterior.

---

## 4. Resumen rápido

| Elemento        | Regla |
|-----------------|--------|
| Header          | Fondo `linear-gradient(135deg, #1e3a5f 0%, #2d4a6f 100%)`, texto blanco, icono de advertencia |
| Mensaje         | Párrafo principal + opcional párrafo `.text-danger.small` para advertencia |
| Botón cancelar  | `btn btn-secondary`, cierra modal |
| Botón confirmar  | `btn btn-danger`, gradiente rojo (#c2185b / #ad1457), con icono Bootstrap Icons |
| Backdrop        | `data-bs-backdrop="static"` |
| Referencia      | Vista Hotel/Distribución, modal “Quitar pasajero de la habitación” |
