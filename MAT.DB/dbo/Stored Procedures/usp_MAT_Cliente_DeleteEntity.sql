CREATE PROCEDURE [dbo].[usp_MAT_Cliente_DeleteEntity]
    @ClienteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Delete de la entidad Cliente por ID (NetTiers F7 - reemplaza ClienteService.Delete)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DELETE FROM dbo.Cliente
    WHERE ClienteID = @ClienteID;
END
