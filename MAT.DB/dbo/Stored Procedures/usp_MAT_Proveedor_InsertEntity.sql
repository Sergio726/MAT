CREATE PROCEDURE [dbo].[usp_MAT_Proveedor_InsertEntity]
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
 -- Description: Insert de la entidad Proveedor (NetTiers F7 - reemplaza ProveedorService.Save).
 --              Estado usa el default de la tabla (1); no toca Domicilio (paridad con NetTiers).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.Proveedor
    (
        ProveedorID, RazonSocial, Telefono, Fax, Web, Email, Idioma, CondicionIva, Cuit, FormaPago, LocalidadID
    )
    VALUES
    (
        @ProveedorID, @RazonSocial, @Telefono, @Fax, @Web, @Email, @Idioma, @CondicionIva, @Cuit, @FormaPago, @LocalidadID
    );
END
