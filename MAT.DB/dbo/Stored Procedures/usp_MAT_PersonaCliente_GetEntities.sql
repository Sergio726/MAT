CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetEntities]
    @PersonaID UNIQUEIDENTIFIER = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Filas de la vista PersonaCliente (NetTiers F7 - reemplaza PersonaClienteService.GetAll).
 --              @PersonaID NULL devuelve todas; en caso contrario filtra por ClienteID (= PersonaID, 1:1).
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
        Telefono,
        Email,
        FechaNacimiento,
        Domicilio,
        Sexo,
        LocalidadID,
        ClienteID,
        RazonSocial,
        Cuit,
        Moneda,
        Empresa,
        Ocupacion,
        FormaPago,
        CondicionIva,
        VendedorID,
        Fax,
        Web,
        Idioma,
        Promotor,
        Observacion,
        TipoID,
        TipoDocumento,
        Celular,
        Nacionalidad,
        PaisResidencia,
        Provincia
    FROM dbo.PersonaCliente
    WHERE (@PersonaID IS NULL OR ClienteID = @PersonaID);
END
