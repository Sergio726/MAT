# Checklist de pruebas — Módulo Clientes

Pruebas manuales por fase del plan ([`PLAN_MODULO_CLIENTES_REDISENO.md`](PLAN_MODULO_CLIENTES_REDISENO.md)).
Hacerlas en DEV con el usuario de prueba antes de publicar cada fase. Marcar con fecha y quién probó.

## Métricas base (antes de F1, 04/11/2026)

| Medición | Valor | Cómo |
|----------|-------|------|
| `GET /PersonaCliente/Create` con el depurador de VS conectado | ~79 s (auditoría) | 45 `NullReferenceException` atrapadas por MVC (vista sin modelo) |
| SPs que ejecuta esa acción | 16 ms (`usp_GetAllOcupacion`) + 5 ms (`usp_GetAllProvincia`) | Consulta directa en DEV |
| Arranque en frío de la aplicación (IIS Express) | 5–7 s | Primer pedido tras iniciar |
| Login con la aplicación iniciada | < 0,1 s | `GET /Account/Login` |

**Después de F1:** medir `GET /PersonaCliente/Create` logueado, con y sin depurador, y con "Break when thrown" activo
(no tiene que detenerse en ninguna excepción).

## F1 — Formulario de cliente

Alta (`Clientes → Nuevo`)
- [ ] La página abre en < 1 s y, con el depurador de VS conectado y "Break when thrown" activo, no se detiene en ninguna excepción.
- [ ] Vienen precargados: DNI, ARG / ARG, Salta, Consumidor Final, Contado, Minorista y cuenta corriente habilitada. Sexo dice "Seleccioná…".
- [ ] Las etiquetas son legibles ("N° de documento", "Fecha de nacimiento", "Tipo de cliente"…) y el `*` aparece solo en los obligatorios.
- [ ] Guardar vacío: cada obligatorio muestra su mensaje en rojo **bajo el campo** y el foco va al primero; no hay aviso amarillo general.
- [ ] Fecha `31/12/2099` → "no puede ser futura"; `31/02/1980` → "no es válida, usá dd/mm/aaaa"; `01/01/1900` → "anterior a 1910".
- [ ] Celular `387` → "El número está incompleto…"; sin ningún teléfono → "Cargá al menos un teléfono o celular"; solo fijo completo → guarda.
- [ ] DNI `12345` → "El DNI debe tener 7 u 8 números"; tipo Pasaporte acepta `AB123456`.
- [ ] Escribir el DNI de un cliente existente **sin puntos** y salir del campo → aviso bajo el campo con su nombre y "Ver su ficha" (abre la ficha correcta). Guardar igual → el servidor lo rechaza con el mismo aviso.
- [ ] Provincia → Departamento → Localidad cargan en cascada; "Nueva" abre el alta de localidad y la nueva queda elegida.
- [ ] Guardar: el botón muestra "Guardando…" y no permite doble clic; termina en la ficha con "Se guardó el cliente …".
- [ ] "Guardar y cargar otro": vuelve al alta vacía con el aviso del cliente recién guardado.
- [ ] En la base: el DNI quedó con el formato `NN.NNN.NNN`, el apellido en mayúsculas, la cuenta habilitada y el registro de pasajero creado.

Alta desde presupuesto (`Presupuesto → convertir en cliente`)
- [ ] Trae apellido, nombre, celular, email y DNI del presupuesto; al guardar vuelve al presupuesto.
- [ ] Presupuesto inexistente → mensaje arriba del formulario, sin error de página.

Edición (`Ficha → Editar`)
- [ ] Carga todos los datos, incluida la cascada Provincia / Departamento / Localidad del cliente y la ocupación (aunque no esté en la lista).
- [ ] Mismas reglas y mensajes que el alta. Editar un cliente sin teléfono fijo **no** exige el fijo.
- [ ] El propio DNI no aparece como repetido (borrar y reescribir un dígito).
- [ ] El interruptor "Cuenta corriente habilitada" refleja el estado real y se guarda con el resto (probar activar y desactivar).
- [ ] Guardar → vuelve a la ficha con "Se guardaron los cambios."
- [ ] Un cliente viejo con fecha de nacimiento vacía o datos incompletos muestra los errores puntuales al guardar.

Seguridad
- [ ] `POST /PersonaCliente/validarCTA` ya no existe (antes cambiaba la cuenta corriente sin estar logueado).
- [ ] Enviar el formulario sin token antiforgery → rechazado.

Navegador / accesibilidad
- [ ] Teclado: Tab recorre los campos en orden, el foco se ve, Enter guarda.
- [ ] Celular (ancho 375 px): una columna, barra de acciones abajo con los botones a lo ancho.
