/*
  Migración: usp_MAT_PersonaCliente_GetTop — obtiene los TOP N clientes con búsqueda por palabras.
  Paridad con MAT.DB\dbo\Stored Procedures\usp_MAT_PersonaCliente_GetTop.sql
  Prerequisito: dbo.Split (ejecutar 2026-04-04_dbo_Split.sql primero).
  Ejecutar contra la base configurada en MAT.Data.ConnectionString (p. ej. MAT.Intranet).
*/
SET NOCOUNT ON;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetTop]
(
    @SearchTerm NVARCHAR(200) = '',
    @TopCount INT = 12,
    @ViajeID UNIQUEIDENTIFIER = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-01-08
  -- Description: Obtiene los TOP N clientes. Sin parámetro de búsqueda: primeros N registros.
  --              Con búsqueda: filtra por palabras en Apellido, Nombre, NroDocumento,
  --              Telefono, Celular o Email usando dbo.Split.
  --              @ViajeID opcional: solo clientes no inscriptos en ese viaje.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET @SearchTerm = LTRIM(RTRIM(ISNULL(@SearchTerm, '')));
    SET @SearchTerm = REPLACE(REPLACE(REPLACE(@SearchTerm, ',', ' '), '-', ' '), N'–', ' ');
    WHILE CHARINDEX('  ', @SearchTerm) > 0
        SET @SearchTerm = REPLACE(@SearchTerm, '  ', ' ');
    SET @SearchTerm = LTRIM(RTRIM(@SearchTerm));

    IF @TopCount <= 0 OR @TopCount > 100
        SET @TopCount = 12;

    DECLARE @SoloDisponiblesParaViaje BIT = CASE WHEN @ViajeID IS NOT NULL THEN 1 ELSE 0 END;

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
        LEFT  JOIN dbo.Localidad l WITH (NOLOCK) ON p.LocalidadID = l.ID
        WHERE (@SoloDisponiblesParaViaje = 0 OR NOT EXISTS (
            SELECT 1 FROM dbo.Pasaje pa WITH (NOLOCK) WHERE pa.ViajeID = @ViajeID AND pa.PasajeroID = p.PersonaID
        ))
        ORDER BY p.Apellido ASC, p.Nombre ASC;
        RETURN;
    END

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
    LEFT  JOIN dbo.Localidad l WITH (NOLOCK) ON p.LocalidadID = l.ID
    WHERE (@SoloDisponiblesParaViaje = 0 OR NOT EXISTS (
        SELECT 1 FROM dbo.Pasaje pa WITH (NOLOCK) WHERE pa.ViajeID = @ViajeID AND pa.PasajeroID = p.PersonaID
    ))
    AND NOT EXISTS (
        SELECT 1
        FROM dbo.Split(CAST(@SearchTerm AS VARCHAR(200)), ' ') AS s
        WHERE LTRIM(RTRIM(s.Item)) <> ''
          AND ISNULL(p.Apellido,      '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
          AND ISNULL(p.Nombre,        '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
          AND ISNULL(p.NroDocumento,  '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
          AND ISNULL(p.Telefono,      '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
          AND ISNULL(p.Celular,       '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
          AND ISNULL(p.Email,         '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
    )
    ORDER BY p.Apellido ASC, p.Nombre ASC;
END
GO
