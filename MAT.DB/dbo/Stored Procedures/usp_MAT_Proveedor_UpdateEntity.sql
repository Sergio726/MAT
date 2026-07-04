CREATE PROCEDURE [dbo].[usp_MAT_Proveedor_UpdateEntity]
    @ProveedorID UNIQUEIDENTIFIER,
    @RazonSocial VARCHAR (50) = NULL,
    @Telefono VARCHAR (50) = NULL,
    @Fax VARCHAR (50) = NULL,
    @Web VARCHAR (50) = NULL,
    @Email VARCHAR (50) = NULL,
    @Idioma VARCHAR (50) = NULL,
    @CondicionIva INT = NULL,
    @Cuit VARCHAR (50) = NULL,
    @FormaPago INT = NULL,
    @LocalidadID INT = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Proveedor (NetTiers F7 - reemplaza ProveedorService.Update).
 --              Solo columnas conocidas por la entidad (no toca Domicilio ni Estado, paridad con NetTiers).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Proveedor
    SET RazonSocial = @RazonSocial,
        Telefono = @Telefono,
        Fax = @Fax,
        Web = @Web,
        Email = @Email,
        Idioma = @Idioma,
        CondicionIva = @CondicionIva,
        Cuit = @Cuit,
        FormaPago = @FormaPago,
        LocalidadID = @LocalidadID
    WHERE ProveedorID = @ProveedorID;
END
