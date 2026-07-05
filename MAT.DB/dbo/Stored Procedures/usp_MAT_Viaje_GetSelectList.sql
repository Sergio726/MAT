CREATE PROCEDURE [dbo].[usp_MAT_Viaje_GetSelectList]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-05
 -- Description: ViajeID + descripcion paquete + fecha salida para dropdowns (NetTiers F12 perf).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        v.ViajeID,
        v.FechaSalida,
        PaqueteDescripcion = ISNULL(p.Descripcion, N'Sin paquete')
    FROM dbo.Viaje v
    LEFT JOIN dbo.Paquete p ON p.PaqueteID = v.PaqueteID
    ORDER BY v.FechaSalida DESC;
END
