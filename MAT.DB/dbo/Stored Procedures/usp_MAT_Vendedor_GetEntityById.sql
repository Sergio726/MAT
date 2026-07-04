CREATE PROCEDURE [dbo].[usp_MAT_Vendedor_GetEntityById]
    @VendedorID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Vendedor por ID, fila de entidad (NetTiers F7 - reemplaza VendedorService.GetByVendedorId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        VendedorID,
        Descripcion
    FROM dbo.Vendedor
    WHERE VendedorID = @VendedorID;
END
