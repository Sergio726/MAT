CREATE PROCEDURE [dbo].[usp_MAT_Cuenta_GetByClienteId]
    @ClienteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Cuentas de un cliente, filas de entidad (NetTiers F7 - reemplaza CuentaService.GetByClienteId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        CuentaID,
        ClienteID,
        Estado
    FROM dbo.Cuenta
    WHERE ClienteID = @ClienteID;
END
