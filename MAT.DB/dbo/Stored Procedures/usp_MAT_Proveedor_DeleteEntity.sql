CREATE PROCEDURE [dbo].[usp_MAT_Proveedor_DeleteEntity]
    @ProveedorID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Delete de la entidad Proveedor por ID (NetTiers F7 - reemplaza ProveedorService.Delete)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DELETE FROM dbo.Proveedor
    WHERE ProveedorID = @ProveedorID;
END
