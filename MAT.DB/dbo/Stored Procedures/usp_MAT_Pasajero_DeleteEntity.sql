CREATE PROCEDURE [dbo].[usp_MAT_Pasajero_DeleteEntity]
    @PasajeroID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Delete de la entidad Pasajero por ID (NetTiers F7 - reemplaza PasajeroService.Delete)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DELETE FROM dbo.Pasajero
    WHERE PasajeroID = @PasajeroID;
END
