CREATE PROCEDURE [dbo].[usp_MAT_Precio_GetAllEntities]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Todos los precios, filas de entidad con tipos nativos (NetTiers F4 -
 --              reemplaza PrecioService.GetAll; el usp_MAT_Precio_GetAll existente
 --              convierte Vigencia a varchar y se conserva para PrecioMethod)
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
    FROM dbo.Precio;
END
