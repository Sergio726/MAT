CREATE PROCEDURE [dbo].[usp_MAT_Adicional_Insert]
    @AdicionalID UNIQUEIDENTIFIER,
    @Monto FLOAT (53),
    @Descripcion VARCHAR (MAX)
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Alta de adicional (NetTiers F4 - reemplaza AdicionalService.Insert)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.Adicional (AdicionalID, Monto, Descripcion)
    VALUES (@AdicionalID, @Monto, @Descripcion);
END
