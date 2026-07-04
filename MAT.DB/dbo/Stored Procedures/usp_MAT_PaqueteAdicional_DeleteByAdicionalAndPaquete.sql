CREATE PROCEDURE [dbo].[usp_MAT_PaqueteAdicional_DeleteByAdicionalAndPaquete]
    @AdicionalID UNIQUEIDENTIFIER,
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Desvincula un adicional de un paquete (NetTiers F4 - reemplaza
 --              GetAll().Where(...) + Delete(PK) de PaqueteController.DesvincularAdicional)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DELETE FROM dbo.PaqueteAdicional
    WHERE AdicionalID = @AdicionalID
      AND PaqueteID = @PaqueteID;

    SELECT @@ROWCOUNT AS Eliminados;
END
