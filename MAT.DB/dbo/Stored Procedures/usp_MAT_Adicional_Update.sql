CREATE PROCEDURE [dbo].[usp_MAT_Adicional_Update]
    @AdicionalID UNIQUEIDENTIFIER,
    @Monto FLOAT (53),
    @Descripcion VARCHAR (MAX)
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de adicional (NetTiers F4 - reemplaza AdicionalService.Update)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Adicional
    SET Monto = @Monto,
        Descripcion = @Descripcion
    WHERE AdicionalID = @AdicionalID;
END
