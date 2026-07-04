CREATE PROCEDURE [dbo].[usp_MAT_Vendedor_InsertEntity]
    @VendedorID UNIQUEIDENTIFIER,
    @Descripcion VARCHAR (100) = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Insert de la entidad Vendedor (NetTiers F7 - reemplaza VendedorService.Insert)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.Vendedor (VendedorID, Descripcion)
    VALUES (@VendedorID, @Descripcion);
END
