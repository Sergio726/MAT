# Normalización MVC y plan de mejoras de arquitectura

Este documento define qué se entiende por **normalización MVC** en el proyecto MAT y describe el plan de mejoras, con el **módulo Hotel** como próxima implementación piloto.

---

## 1. Criterios de normalización MVC

Un controlador/vista/modelo se considera **normalizado** cuando se cumple lo siguiente:

| Criterio | Descripción |
|----------|-------------|
| **Controlador delgado** | El controlador no arma `SqlParameter[]` ni llama a `DBHelper` directamente. Solo valida parámetros de entrada, llama a servicios y decide qué vista o JSON devolver. |
| **Lógica de negocio en servicios** | La orquestación (varias llamadas a datos, reglas, combinación de resultados) vive en una capa de **servicio** inyectable (`IHotelService`, etc.), no en el controlador ni en clases estáticas. |
| **Acceso a datos en repositorio/DAL** | Las llamadas a stored procedures y el mapeo `DataSet`/`DataReader` → modelos están en una capa de acceso a datos (repositorio o equivalente), no repartidas entre controlador y “Method” estático. |
| **Vistas fuertemente tipadas** | Las vistas reciben un **ViewModel** (`@model XViewModel`) en lugar de depender de `ViewBag` y `ViewData` para los datos principales. |
| **Testeable** | Los servicios dependen de interfaces (ej. `IHotelRepository`), de modo que se puedan mockear en tests unitarios. |
| **Sin “Method” estático como DAL** | Se evita el patrón de clase estática que mezcla “helper de BD” (ej. `HotelMethod.GetHotelByViaje`) con lógica que debería estar en servicio. |

**Estado actual (ejemplo Hotel):** el controlador llama a `HotelMethod.GetHotelByViaje` y `HotelMethod.GetEsquemaDistribucion`, pero en `EsquemaDistribucion` además arma parámetros y llama a `DBHelper.ExecuteScalar` para `CantPax`. Hay uso intensivo de `ViewBag`/`ViewData`. Esto no cumple la normalización anterior.

---

## 2. Plan de mejoras de arquitectura (resumen)

1. **Capa de servicio**  
   Introducir `IHotelService` / `HotelService` que concentre la lógica de “hotel/distribución” y que el controlador solo use el servicio.

2. **Refinar modelo / DAL**  
   Convertir el uso de `HotelMethod` estático en un repositorio (o equivalente) que solo ejecute SPs y mapee a DTOs/entidades; el servicio consumirá ese repositorio.

3. **ViewModels**  
   Crear ViewModels específicos (ej. `HotelDistribucionViewModel`) y pasar `View(model)` en lugar de `ViewBag`/`ViewData` para los datos principales.

4. **Validaciones y errores**  
   Centralizar validaciones de negocio en el servicio; en el controlador solo validaciones “HTTP” (400, 404, etc.) y formato de respuesta.

5. **Tests**  
   Añadir tests unitarios sobre el servicio con repositorio mockeado.

6. **Replicar el patrón**  
   Una vez aplicado en Hotel, usar la misma estructura en otros módulos (Reserva, Pasajero, etc.).

---

## 3. Próxima mejora: módulo Hotel

El **módulo Hotel** (vista Distribución de habitaciones, EsquemaDistribucion, y acciones relacionadas) se toma como **piloto** para aplicar la normalización MVC.

### 3.1 Objetivos

- Crear **`IHotelService`** y **`HotelService`** que concentren la lógica hoy repartida entre `HotelController` y `HotelMethod`.
- Mover al servicio la llamada a `usp_MAT_Hotel_EsquemaDistribucion_CantPax` (hoy en el controlador).
- Introducir **`HotelDistribucionViewModel`** para la vista Distribución y usar `@model` en lugar de `ViewBag.ListHotel` y `ViewData["viajeid"]`.
- Opcional: extraer el acceso a datos a **`IHotelRepository`** / **`HotelRepository`** para dejar `HotelMethod` como capa interna o reemplazarla progresivamente.

### 3.2 Estructura objetivo (referencia)

```
Controllers/Hotel/HotelController.cs     → usa IHotelService
Services/Hotel/IHotelService.cs
Services/Hotel/HotelService.cs           → usa IHotelRepository o HotelMethod internamente al inicio
(Repositories/Hotel/IHotelRepository.cs)  → opcional en una primera fase
(Repositories/Hotel/HotelRepository.cs)
Models/Hotel/                            → modelos de dominio (Ej. EsquemaDistribucion, HotelDropDown)
ViewModels/Hotel/                        → HotelDistribucionViewModel, etc.
```

### 3.3 Referencias en código actual

- **Controlador:** `MAT.MVC\Controllers\Hotel\HotelController.cs`
- **Modelo estático:** `MAT.MVC\Models\HotelModel.cs` (`HotelMethod`, `GetHotelByViaje`, `GetEsquemaDistribucion`)
- **Vistas:** `Views\Hotel\Distribucion.cshtml`, `Views\Hotel\EsquemaDistribucion.cshtml`

---

## 4. Documentos relacionados

- **Estilo de modales de confirmación:** `DOCUMENTACION/ESTILO_MODAL_CONFIRMACION.md`
- **Estándar CSS:** `DOCUMENTACION/ESTANDAR_CSS_PROYECTO.md`
- **Fase 2 (CSS/layout):** `DOCUMENTACION/FASE2_ANALISIS_Y_ACCIONES.md`

---

*Última actualización: 2026-02-11. Próxima implementación piloto: módulo Hotel (capa de servicio + ViewModels).*
