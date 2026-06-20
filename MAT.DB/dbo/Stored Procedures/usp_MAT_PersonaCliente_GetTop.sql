CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetTop]
(
    @SearchTerm NVARCHAR(200) = '',
    @TopCount INT = 12,
    @ViajeID UNIQUEIDENTIFIER = NULL
)
AS 
/*
----------------------------------------------------------------------------------------------------
-- Author:    Seba Garcia
-- Create date: 2026-01-08
-- Description: Obtiene los TOP N clientes. Sin parámetro de búsqueda: primeros 12 registros.
-- Con búsqueda: filtra por palabras en Apellido, Nombre, NroDocumento, Telefono, Celular o Email.
-- Las palabras pueden estar separadas por espacio, coma (,) o guión (-).
-- @ViajeID opcional: si se informa, excluye clientes ya inscriptos en ese viaje o en otro viaje
--   con la misma FechaSalida (fecha de inicio/salida del viaje).
-- Optimizaciones:
--   - Solo clientes (INNER JOIN Cliente). Filtro por palabras con dbo.Split.
--   - IsTituarFactura con EXISTS. TipoDocumento y Email para autocomplete.
----------------------------------------------------------------------------------------------------
*/
BEGIN 
    SET NOCOUNT, XACT_ABORT ON; 
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; 

    -- Normalizar término de búsqueda: trim y tratar coma/guion como delimitadores (unificar con espacio)
    SET @SearchTerm = LTRIM(RTRIM(ISNULL(@SearchTerm, '')));
    SET @SearchTerm = REPLACE(REPLACE(REPLACE(@SearchTerm, ',', ' '), '-', ' '), N'–', ' ');  -- guion normal y en-dash
    -- Colapsar espacios múltiples (evita palabras vacías al hacer split)
    WHILE CHARINDEX('  ', @SearchTerm) > 0
        SET @SearchTerm = REPLACE(@SearchTerm, '  ', ' ');
    SET @SearchTerm = LTRIM(RTRIM(@SearchTerm));

    -- Validar TopCount
    IF @TopCount <= 0 OR @TopCount > 100
        SET @TopCount = 12;

    -- Filtro opcional: solo clientes no inscriptos en el viaje
    -- (cuando @ViajeID se usa desde autocomplete de pasajeros en reserva)
    DECLARE @SoloDisponiblesParaViaje BIT = CASE WHEN @ViajeID IS NOT NULL THEN 1 ELSE 0 END;

    -- Sin parámetro de búsqueda: primeros N registros (solo clientes, opcionalmente disponibles para @ViajeID)
    IF LEN(@SearchTerm) = 0
    BEGIN
        SELECT TOP (@TopCount)
            p.PersonaID,
            p.Apellido,
            p.Nombre,
            p.NroDocumento,
            p.Telefono,
            p.Celular,
            ISNULL(p.TipoDocumento, 1) AS TipoDocumento,
            ISNULL(p.Email, '') AS Email,
            LocalidadNombre = ISNULL(l.Nombre, ''),
            p.Nacionalidad,
            p.PaisResidencia,
            IsTituarFactura = CASE
                WHEN EXISTS (SELECT 1 FROM dbo.Factura f WITH (NOLOCK) WHERE f.ClienteID = p.PersonaID) THEN 1
                ELSE 0
            END
        FROM dbo.Persona p WITH (NOLOCK)
        INNER JOIN dbo.Cliente c WITH (NOLOCK) ON c.ClienteID = p.PersonaID
        LEFT JOIN dbo.Localidad l WITH (NOLOCK) ON p.LocalidadID = l.ID
        WHERE (@SoloDisponiblesParaViaje = 0 OR dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(p.PersonaID, @ViajeID, NULL) = 0)
        ORDER BY p.Apellido ASC, p.Nombre ASC;
        RETURN;
    END

    -- Con parámetro de búsqueda: filtrar por palabras usando dbo.Split
    SELECT TOP (@TopCount)
        p.PersonaID,
        p.Apellido,
        p.Nombre,
        p.NroDocumento,
        p.Telefono,
        p.Celular,
        ISNULL(p.TipoDocumento, 1) AS TipoDocumento,
        ISNULL(p.Email, '') AS Email,
        LocalidadNombre = ISNULL(l.Nombre, ''),
        p.Nacionalidad,
        p.PaisResidencia,
        IsTituarFactura = CASE
            WHEN EXISTS (SELECT 1 FROM dbo.Factura f WITH (NOLOCK) WHERE f.ClienteID = p.PersonaID) THEN 1
            ELSE 0
        END
    FROM dbo.Persona p WITH (NOLOCK)
    INNER JOIN dbo.Cliente c WITH (NOLOCK) ON c.ClienteID = p.PersonaID
    LEFT JOIN dbo.Localidad l WITH (NOLOCK) ON p.LocalidadID = l.ID
    WHERE (@SoloDisponiblesParaViaje = 0 OR dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(p.PersonaID, @ViajeID, NULL) = 0)
    AND NOT EXISTS (
        SELECT 1
        FROM dbo.Split(CAST(@SearchTerm AS VARCHAR(200)), ' ') AS s
        WHERE LTRIM(RTRIM(s.Item)) <> ''
        AND ISNULL(p.Apellido, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.Nombre, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.NroDocumento, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.Telefono, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.Celular, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.Email, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
    )
    ORDER BY p.Apellido ASC, p.Nombre ASC;

END