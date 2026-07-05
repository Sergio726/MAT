CREATE PROCEDURE [dbo].[usp_MAT_Historial_GetAll]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-05
 -- Description: Todo el historial de pagos (NetTiers F8 - reemplaza HistorialService.GetAll)
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
    FROM dbo.Historial;
END
