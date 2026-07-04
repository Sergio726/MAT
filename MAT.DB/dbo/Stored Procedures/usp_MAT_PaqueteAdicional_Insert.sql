CREATE PROCEDURE [dbo].[usp_MAT_PaqueteAdicional_Insert]
    @PaqueteAdicionalID UNIQUEIDENTIFIER,
    @PaqueteID UNIQUEIDENTIFIER,
    @AdicionalID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Vincula un adicional a un paquete (NetTiers F4)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.PaqueteAdicional (PaqueteAdicionalID, PaqueteID, AdicionalID)
    VALUES (@PaqueteAdicionalID, @PaqueteID, @AdicionalID);
END
