-- Normalización Localidad L1 — SP búsqueda unificada
-- Publicar en SQL Server antes de smoke de /Localidad/Search (L2).
-- Fuente de verdad: MAT.DB/dbo/Stored Procedures/usp_MAT_Localidad_Search.sql

IF OBJECT_ID(N'dbo.usp_MAT_Localidad_Search', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Localidad_Search;
GO

CREATE PROCEDURE [dbo].[usp_MAT_Localidad_Search]
    @Term            NVARCHAR(250) = '',
    @IdProvincia     INT = NULL,
    @IdDepartamento  INT = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-01
 -- Description: Búsqueda de localidades por nombre con filtros opcionales (normalización L1). Mínimo 3 caracteres.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF LEN(LTRIM(RTRIM(@Term))) < 3
        RETURN;

    SELECT l.ID,
           l.Nombre
    FROM dbo.Localidad l
    INNER JOIN dbo.Departamento d ON d.ID = l.idDepartamento
    INNER JOIN dbo.Provincia p ON p.ID = d.idProvincia
    WHERE l.Nombre LIKE '%' + @Term + '%'
      AND (@IdDepartamento IS NULL OR d.ID = @IdDepartamento)
      AND (@IdProvincia IS NULL OR p.ID = @IdProvincia)
    ORDER BY l.Nombre;
END
GO
