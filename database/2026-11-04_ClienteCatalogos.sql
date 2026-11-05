-- =============================================================================
-- 2026-11-04 — Módulo Clientes F4 (1 de 2): catálogos — esquema, semillas y SPs
-- Author: Sebastian Garcia
-- * Pais.Codigo (ISO alfa-3) + índice único; códigos de los 8 países y 9 países nuevos usados como nacionalidad.
-- * Tabla Ocupacion (nombres neutros) con las ocupaciones que hoy usan los clientes.
-- * SPs usp_MAT_Catalogo_Geografia y usp_MAT_PersonaCliente_Ficha (nombre del país de nacionalidad).
-- No toca datos de personas: la normalización y la deduplicación están en 2026-11-04_ClienteCatalogos_Datos.sql.
-- Paridad con MAT.DB (Tables/Pais.sql, Tables/Ocupacion.sql y los SPs). Idempotente.
-- Requiere 2026-11-04_ClienteFicha.sql.
-- =============================================================================
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF COL_LENGTH('dbo.Pais', 'Codigo') IS NULL
    ALTER TABLE dbo.Pais ADD Codigo CHAR(3) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Pais_Codigo' AND object_id = OBJECT_ID('dbo.Pais'))
    CREATE UNIQUE NONCLUSTERED INDEX UX_Pais_Codigo ON dbo.Pais (Codigo ASC) WHERE (Codigo IS NOT NULL);
GO

IF OBJECT_ID('dbo.Ocupacion', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Ocupacion (
        ID     INT          IDENTITY (1, 1) NOT NULL,
        Nombre VARCHAR (50) NOT NULL,
        Activo BIT          CONSTRAINT DF_Ocupacion_Activo DEFAULT ((1)) NOT NULL,
        Orden  INT          CONSTRAINT DF_Ocupacion_Orden DEFAULT ((100)) NOT NULL,
        CONSTRAINT PK_Ocupacion PRIMARY KEY CLUSTERED (ID ASC),
        CONSTRAINT UQ_Ocupacion_Nombre UNIQUE NONCLUSTERED (Nombre ASC)
    );
END
GO

-- ---------------------------------------------------------------- Países: códigos y países nuevos
BEGIN TRANSACTION;

DECLARE @Paises TABLE (Nombre VARCHAR(100) COLLATE Latin1_General_CI_AI NOT NULL, Codigo CHAR(3) NOT NULL);
INSERT INTO @Paises (Nombre, Codigo) VALUES
    ('ARGENTINA', 'ARG'), ('BOLIVIA', 'BOL'), ('BRASIL', 'BRA'), ('CHILE', 'CHL'), ('PARAGUAY', 'PRY'),
    ('PERU', 'PER'), ('REPUBLICA DOMINICANA', 'DOM'), ('URUGUAY', 'URY'),
    -- Nacionalidades de clientes que no estaban como país
    ('ESPAÑA', 'ESP'), ('ITALIA', 'ITA'), ('ESTADOS UNIDOS', 'USA'), ('MEXICO', 'MEX'),
    ('HUNGRIA', 'HUN'), ('DINAMARCA', 'DNK'), ('ALEMANIA', 'DEU'), ('COLOMBIA', 'COL'), ('PAISES BAJOS', 'NLD');

-- Existentes (comparación sin acentos: "PERÚ" = "PERU")
UPDATE pa SET Codigo = x.Codigo
FROM dbo.Pais pa
INNER JOIN @Paises x ON x.Nombre = pa.Descripcion COLLATE Latin1_General_CI_AI
WHERE pa.Codigo IS NULL
  AND NOT EXISTS (SELECT 1 FROM dbo.Pais otro WHERE otro.Codigo = x.Codigo);

-- Nuevos
INSERT INTO dbo.Pais (PaisID, Descripcion, Codigo)
SELECT NEWID(),
       CASE x.Codigo WHEN 'MEX' THEN 'MÉXICO' WHEN 'HUN' THEN 'HUNGRÍA' WHEN 'NLD' THEN 'PAÍSES BAJOS' ELSE x.Nombre END,
       x.Codigo
FROM @Paises x
WHERE NOT EXISTS (SELECT 1 FROM dbo.Pais pa WHERE pa.Codigo = x.Codigo)
  AND NOT EXISTS (SELECT 1 FROM dbo.Pais pa WHERE pa.Descripcion COLLATE Latin1_General_CI_AI = x.Nombre);

COMMIT TRANSACTION;
GO

-- ---------------------------------------------------------------- Ocupaciones (nombres neutros, decisión 8.3)
MERGE dbo.Ocupacion AS destino
USING (VALUES
    ('Jubilado/a', 10), ('Empleado/a', 20), ('Empleado/a público/a', 30), ('Empleado/a de comercio', 40),
    ('Docente', 50), ('Estudiante', 60), ('Profesional', 70), ('Trabajador/a independiente', 80),
    ('Comerciante', 90), ('Ama/o de casa', 100), ('Pensionado/a', 110), ('Sin ocupación', 120),
    ('Abogado/a', 200), ('Farmacéutico/a', 200), ('Odontólogo/a', 200), ('Periodista', 200)
) AS origen (Nombre, Orden)
ON destino.Nombre = origen.Nombre
WHEN NOT MATCHED BY TARGET THEN INSERT (Nombre, Orden) VALUES (origen.Nombre, origen.Orden);
GO


CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Catalogo_Geografia]
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Catálogos del formulario de cliente (módulo Clientes F4).
  --              RS1: países (PaisID, Codigo, Nombre, TieneProvincias), Argentina primero.
  --              RS2: provincias con su país, para filtrar País → Provincia en la pantalla.
  --              RS3: ocupaciones activas.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT pa.PaisID, pa.Codigo, pa.Descripcion AS Nombre,
           CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.Provincia p WHERE p.IdPais = pa.PaisID) THEN 1 ELSE 0 END AS BIT) AS TieneProvincias
    FROM dbo.Pais pa
    ORDER BY CASE WHEN pa.Codigo = 'ARG' THEN 0 ELSE 1 END, pa.Descripcion;

    SELECT p.ID, p.Nombre, p.IdPais
    FROM dbo.Provincia p
    ORDER BY p.Nombre;

    SELECT o.Nombre
    FROM dbo.Ocupacion o
    WHERE o.Activo = 1
    ORDER BY o.Orden, o.Nombre;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_PersonaCliente_Ficha]
(
    @PersonaID UNIQUEIDENTIFIER
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Datos de la ficha del cliente (módulo Clientes F3) en una sola consulta, con los nombres
  --              ya resueltos (localidad, departamento, provincia, vendedor), el estado de la cuenta corriente
  --              y cuántas facturas tiene como titular. Reemplaza a Persona.GetById + Cliente.GetById + una
  --              consulta por cada nombre que hacía la vista.
  -- 2026-11-04 (F4): nombre del país de nacionalidad y de residencia (Pais.Codigo).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT p.PersonaID, p.Apellido, p.Nombre, p.TipoDocumento, p.NroDocumento, p.FechaNacimiento, p.Sexo,
           p.Nacionalidad, p.PaisResidencia, p.Ocupacion,
           pn.Descripcion AS NacionalidadNombre, pr2.Descripcion AS PaisResidenciaNombre,
           p.Domicilio, p.Telefono, p.Celular, p.Email,
           l.Nombre AS LocalidadNombre,
           d.Nombre AS DepartamentoNombre,
           COALESCE(pr.Nombre, prl.Nombre) AS ProvinciaNombre,
           c.RazonSocial, c.Cuit, c.CondicionIva, c.FormaPago, c.Empresa, c.Fax, c.Web, c.Idioma, c.Promotor,
           c.Observacion, c.TipoID, c.FechaAlta,
           LTRIM(RTRIM(ISNULL(v.Apellido, '') + ', ' + ISNULL(v.Nombre, ''))) AS VendedorNombre,
           cu.Estado AS CuentaHabilitada,
           (SELECT COUNT(*) FROM dbo.Factura f WHERE f.ClienteID = c.ClienteID) AS CantidadFacturas
    FROM dbo.Cliente c
    INNER JOIN dbo.Persona p ON p.PersonaID = c.ClienteID
    LEFT JOIN dbo.Localidad l ON l.ID = p.LocalidadID
    LEFT JOIN dbo.Departamento d ON d.ID = l.idDepartamento
    LEFT JOIN dbo.Provincia prl ON prl.ID = d.idProvincia
    LEFT JOIN dbo.Provincia pr ON pr.ID = p.Provincia
    LEFT JOIN dbo.Persona v ON v.PersonaID = c.VendedorID
    -- Nacionalidad y país de residencia se guardan como código ISO (F4)
    LEFT JOIN dbo.Pais pn ON pn.Codigo = p.Nacionalidad
    LEFT JOIN dbo.Pais pr2 ON pr2.Codigo = p.PaisResidencia
    OUTER APPLY (SELECT TOP 1 cu.Estado FROM dbo.Cuenta cu WHERE cu.ClienteID = c.ClienteID) cu
    WHERE c.ClienteID = @PersonaID;
END
GO
