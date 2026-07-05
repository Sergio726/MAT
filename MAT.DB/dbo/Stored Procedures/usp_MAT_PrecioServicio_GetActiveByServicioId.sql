CREATE PROCEDURE [dbo].[usp_MAT_PrecioServicio_GetActiveByServicioId]
    @ServicioID UNIQUEIDENTIFIER
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-05
  -- Description: Precio activo de un servicio (NetTiers F9 - reemplaza PrecioServicioService en Helper).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT TOP (1)
        Precio
    FROM dbo.PrecioServicio
    WHERE ServicioID = @ServicioID
      AND Activo = 1
    ORDER BY FechaRegistro DESC;
END
