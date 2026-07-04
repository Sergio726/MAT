CREATE PROCEDURE [dbo].[usp_MAT_Precio_GetById]
    @PrecioID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Precio por ID, fila de entidad con tipos nativos (NetTiers F4 -
 --              reemplaza PrecioService.GetByPrecioId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PrecioID,
        Monto,
        Vigencia,
        Descripcion,
        Mes,
        DescripcionVoucher
    FROM dbo.Precio
    WHERE PrecioID = @PrecioID;
END
