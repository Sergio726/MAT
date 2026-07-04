CREATE PROCEDURE [dbo].[usp_MAT_Cliente_UpdateEntity]
    @ClienteID UNIQUEIDENTIFIER,
    @RazonSocial VARCHAR (50) = NULL,
    @Cuit VARCHAR (50) = NULL,
    @Moneda VARCHAR (50) = NULL,
    @Empresa VARCHAR (50) = NULL,
    @Ocupacion VARCHAR (50) = NULL,
    @FormaPago INT = NULL,
    @CondicionIva INT = NULL,
    @VendedorID UNIQUEIDENTIFIER = NULL,
    @Fax VARCHAR (50) = NULL,
    @Web VARCHAR (50) = NULL,
    @Idioma VARCHAR (50) = NULL,
    @Promotor VARCHAR (50) = NULL,
    @Observacion VARCHAR (250) = NULL,
    @TipoID INT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Cliente (NetTiers F7 - reemplaza ClienteService.Update).
 --              Solo columnas conocidas por la entidad (no toca FechaAlta, paridad con NetTiers).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Cliente
    SET RazonSocial = @RazonSocial,
        Cuit = @Cuit,
        Moneda = @Moneda,
        Empresa = @Empresa,
        Ocupacion = @Ocupacion,
        FormaPago = @FormaPago,
        CondicionIva = @CondicionIva,
        VendedorID = @VendedorID,
        Fax = @Fax,
        Web = @Web,
        Idioma = @Idioma,
        Promotor = @Promotor,
        Observacion = @Observacion,
        TipoID = @TipoID
    WHERE ClienteID = @ClienteID;
END
