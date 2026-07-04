CREATE PROCEDURE [dbo].[usp_MAT_Cuenta_InsertEntity]
    @CuentaID UNIQUEIDENTIFIER,
    @ClienteID UNIQUEIDENTIFIER,
    @Estado BIT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Insert de la entidad Cuenta (NetTiers F7 - reemplaza CuentaService.Insert)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.Cuenta (CuentaID, ClienteID, Estado)
    VALUES (@CuentaID, @ClienteID, @Estado);
END
