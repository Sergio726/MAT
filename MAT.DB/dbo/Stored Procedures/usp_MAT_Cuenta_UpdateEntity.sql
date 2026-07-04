CREATE PROCEDURE [dbo].[usp_MAT_Cuenta_UpdateEntity]
    @CuentaID UNIQUEIDENTIFIER,
    @ClienteID UNIQUEIDENTIFIER,
    @Estado BIT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Cuenta (NetTiers F7 - reemplaza CuentaService.Update)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Cuenta
    SET ClienteID = @ClienteID,
        Estado = @Estado
    WHERE CuentaID = @CuentaID;
END
