/* Paridad con MAT.DB — desplegar en SQL Server (reemplaza versión anterior sin @RecentOnly / FechaSalida). */
IF OBJECT_ID(N'dbo.usp_MAT_Reportes_BuscarViajes', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Reportes_BuscarViajes;
GO

CREATE PROCEDURE [dbo].[usp_MAT_Reportes_BuscarViajes]
(
    @q           NVARCHAR(200) = NULL,
    @RecentOnly  BIT           = 0
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-04-09
  -- Description: Búsqueda de viajes para autocompletar reportes Admin. Si @RecentOnly=1,
  --              devuelve los últimos 30 viajes con descripción por FechaSalida desc (sin filtro texto).
  --              Si @RecentOnly=0, filtra Descripcion por @q (LIKE). Siempre devuelve FechaSalida.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @RecentOnly = 1
    BEGIN
        SELECT TOP (30)
               v.ViajeID,
               v.Descripcion,
               v.FechaSalida
        FROM   dbo.Viaje AS v
        WHERE  v.Descripcion IS NOT NULL
        ORDER BY v.FechaSalida DESC,
                 v.Descripcion ASC;
        RETURN;
    END;

    IF @q IS NULL OR LEN(LTRIM(RTRIM(@q))) = 0
    BEGIN
        SELECT TOP (0)
               CAST(NULL AS UNIQUEIDENTIFIER) AS ViajeID,
               CAST(NULL AS NVARCHAR(200))    AS Descripcion,
               CAST(NULL AS DATE)             AS FechaSalida;
        RETURN;
    END;

    DECLARE @trim NVARCHAR(200) = LTRIM(RTRIM(@q));
    DECLARE @like  NVARCHAR(210) = N'%' + REPLACE(REPLACE(@trim, N'%', N'[%]'), N'_', N'[_]') + N'%';

    SELECT TOP (30)
           v.ViajeID,
           v.Descripcion,
           v.FechaSalida
    FROM   dbo.Viaje AS v
    WHERE  v.Descripcion IS NOT NULL
           AND v.Descripcion LIKE @like COLLATE Latin1_General_CI_AI
    ORDER BY v.FechaSalida DESC,
             v.Descripcion ASC;
END;
GO
