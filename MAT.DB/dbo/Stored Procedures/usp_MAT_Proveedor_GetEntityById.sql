CREATE PROCEDURE [dbo].[usp_MAT_Proveedor_GetEntityById]
    @ProveedorID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Proveedor por ID, fila de entidad (NetTiers F7 - reemplaza ProveedorService.Get / GetByProveedorId).
 --              Solo columnas conocidas por la entidad (no expone Domicilio ni Estado, paridad con NetTiers).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        ProveedorID,
        RazonSocial,
        Telefono,
        Fax,
        Web,
        Email,
        Idioma,
        CondicionIva,
        Cuit,
        FormaPago,
        LocalidadID
    FROM dbo.Proveedor
    WHERE ProveedorID = @ProveedorID;
END
