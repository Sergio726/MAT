CREATE PROCEDURE [dbo].[usp_MAT_Reportes_BuscarViajes]
(
    @q NVARCHAR(200)
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-04-09
  -- Description: Búsqueda ligera de viajes por texto en Descripcion (autocompletar
  --              en reportes Admin). Máximo 30 filas, orden por FechaSalida desc.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @q IS NULL OR LEN(LTRIM(RTRIM(@q))) = 0
    BEGIN
        SELECT TOP (0)
               CAST(NULL AS UNIQUEIDENTIFIER) AS ViajeID,
               CAST(NULL AS NVARCHAR(200))    AS Descripcion;
        RETURN;
    END;

    DECLARE @trim NVARCHAR(200) = LTRIM(RTRIM(@q));
    DECLARE @like  NVARCHAR(210) = N'%' + REPLACE(REPLACE(@trim, N'%', N'[%]'), N'_', N'[_]') + N'%';

    SELECT TOP (30)
           v.ViajeID,
           v.Descripcion
    FROM   dbo.Viaje AS v
    WHERE  v.Descripcion IS NOT NULL
           AND v.Descripcion LIKE @like COLLATE Latin1_General_CI_AI
    ORDER BY v.FechaSalida DESC,
             v.Descripcion ASC;
END;
