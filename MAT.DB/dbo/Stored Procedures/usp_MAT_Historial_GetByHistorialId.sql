CREATE PROCEDURE [dbo].[usp_MAT_Historial_GetByHistorialId]
    @HistorialID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-05
 -- Description: Historial por ID (NetTiers F8 - reemplaza HistorialService.GetByHistorialId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        HistorialID,
        Tabla,
        Operacion,
        FechaHoraRegistro,
        Cliente,
        Vendedor,
        Observaciones,
        Monto
    FROM dbo.Historial
    WHERE HistorialID = @HistorialID;
END
