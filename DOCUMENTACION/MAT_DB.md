# Proyecto MAT.DB – Esquema de base de datos

Proyecto de base de datos (SSDT) de la solución MAT. Contiene la estructura completa y actualizada del esquema de la base de datos, sincronizada con la base actual.

---

## Descripción

- **Tipo:** SQL Server Database Project (SSDT)
- **Proveedor de esquema:** Sql160 (SQL Server 16.x)
- **Ubicación en la solución:** `MAT.DB\MAT.DB.sqlproj`
- **Collation por defecto:** SQL_Latin1_General_CP1_CI_AS

El proyecto se usa como **referencia oficial** del esquema: tablas, vistas, stored procedures, funciones y tipos definidos por el usuario. Cualquier cambio en la base (nuevas tablas, SPs, índices, etc.) debe reflejarse aquí y publicarse desde este proyecto.

---

## Regla de trabajo: actualizar siempre MAT.DB

**Si se necesita crear, modificar o eliminar estructuras de la base de datos, se debe actualizar el proyecto MAT.DB.**

| Acción | Qué hacer |
|--------|-----------|
| **Crear** tablas nuevas, stored procedures, vistas, funciones, índices, tipos de usuario, etc. | Añadir o crear el objeto correspondiente dentro del proyecto MAT.DB (en la carpeta adecuada: `dbo\Tables`, `dbo\Stored Procedures`, `dbo\Views`, etc.) y luego **publicar** el proyecto contra la base. |
| **Modificar** tablas (columnas, tipos, restricciones), SPs, vistas, etc. | Editar el archivo `.sql` del objeto en MAT.DB y luego **publicar** el proyecto. |
| **Eliminar** tablas, SPs, vistas u otras estructuras | Quitar el archivo del objeto del proyecto MAT.DB (o eliminar su contenido/definición según el caso) y luego **publicar** el proyecto. |

- No se deben aplicar cambios de esquema solo ejecutando scripts sueltos contra la base sin reflejarlos en MAT.DB.
- Los scripts en la carpeta `database\` (raíz del repo) pueden usarse como migraciones puntuales o referencia; los cambios definitivos deben quedar incorporados en MAT.DB para que el proyecto siga siendo la **única fuente de verdad** del esquema.

---

## Estructura del proyecto

| Carpeta / archivo | Contenido |
|-------------------|-----------|
| **dbo\Tables** | Definiciones de tablas (Factura, Pasaje, Cliente, Persona, Viaje, ListaEspera, etc.) |
| **dbo\Stored Procedures** | Stored procedures (usp_MAT_*, procedimientos de negocio) |
| **dbo\Views** | Vistas (PersonaCliente, PersonaPasajero, Reserva, vPersona, vLocalidad, etc.) |
| **dbo\Functions** | Funciones escalares o con valores de tabla |
| **dbo\User Defined Types** | Tipos de tabla (tvp_*, etc.) |
| **Security** | Usuarios y roles (matuser, RoleMemberships) |
| **SqlSchemaCompare.scmp** | Archivo de comparación de esquema (Schema Compare) |

---

## Cómo trabajar con el proyecto

### Compilar

- En Visual Studio: clic derecho en **MAT.DB** → **Compilar**.
- Genera un script de despliegue en `bin\Debug\MAT.DB.sql` (o Release).

### Publicar (actualizar la base de datos)

1. Clic derecho en **MAT.DB** → **Publicar**.
2. Elegir un **perfil de publicación** (o crear uno que apunte a la base deseada).
3. Revisar el resumen de cambios y hacer clic en **Publicar**.

**Recomendación:** Usar el mismo connection string que la aplicación (por ejemplo la llave `MAT.Data.ConnectionString` en `MAT.MVC\Web.config`) para el perfil de publicación, de modo que el esquema se actualice en la misma base que usa la app.

### Comparar esquema (Schema Compare)

- **Comparar con la base de datos:** Herramientas → SQL Server → Nueva comparación de esquema. Origen: proyecto MAT.DB; destino: conexión a la base.
- El archivo **SqlSchemaCompare.scmp** guarda una sesión de comparación para reutilizarla.

### Sincronizar desde la base existente

Si la base ya tiene cambios que no están en el proyecto:

1. Usar **Schema Compare**: origen = base de datos, destino = proyecto MAT.DB.
2. Revisar las diferencias y elegir **Actualizar** hacia el proyecto para incorporar los cambios.

---

## Relación con el resto del proyecto

- **MAT.MVC / MAT.Services:** Ejecutan stored procedures y consultas contra la base. Los nombres de tablas, columnas y SPs deben coincidir con lo definido en MAT.DB.
- **Carpeta `database\` (raíz del repo):** Scripts sueltos (por ejemplo `usp_MAT_Reserva_ExtenderPreReserva.sql`). Pueden ser migraciones puntuales; si el cambio es definitivo, conviene incorporarlo también al proyecto MAT.DB para mantener un único origen de verdad.
- **MAT.Entities:** Entidades C# que suelen mapear tablas; al cambiar tablas o columnas en MAT.DB, revisar si hay que actualizar las entidades.

---

## Requisitos

- Visual Studio con **SQL Server Data Tools (SSDT)** o carga de trabajo de base de datos.
- Acceso a una instancia de SQL Server para publicar y comparar esquema.

---

## Resumen

MAT.DB es el proyecto de esquema de la base de datos de MAT. **Cualquier creación, modificación o eliminación de estructuras de la base de datos (tablas, SPs, vistas, funciones, índices, etc.) debe hacerse actualizando MAT.DB y publicando desde este proyecto.** Se usa para compilar, publicar y comparar el esquema; toda referencia a tablas, SPs, vistas e índices debe consultarse y mantenerse en este proyecto.
