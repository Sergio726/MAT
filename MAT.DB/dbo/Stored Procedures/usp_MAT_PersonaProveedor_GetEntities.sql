CREATE PROCEDURE [dbo].[usp_MAT_PersonaProveedor_GetEntities]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Filas de la vista PersonaProveedor (NetTiers F7 - reemplaza PersonaProveedorService.GetAll).
 --              F7.1: columnas explícitas en vez de SELECT *.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PersonaID,
        Apellido,
        Nombre,
        NroDocumento,
        LocalidadID,
        Telefono,
        Email,
        FechaNacimiento,
        Sexo,
        Domicilio,
        ProveedorID,
        RazonSocial,
        ProveedorLocalidadID,
        ProveedorTelefono,
        Fax,
        Web,
        ProveedorEmail,
        Idioma,
        CondicionIva,
        Cuit,
        FormaPago,
        TipoDocumento
    FROM dbo.PersonaProveedor;
END
