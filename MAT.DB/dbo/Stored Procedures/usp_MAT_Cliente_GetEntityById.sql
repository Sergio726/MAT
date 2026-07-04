CREATE PROCEDURE [dbo].[usp_MAT_Cliente_GetEntityById]
    @ClienteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Cliente por ID, fila de entidad (NetTiers F7 - reemplaza ClienteService.Get / GetByClienteId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
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
        TipoID
    FROM dbo.Cliente
    WHERE ClienteID = @ClienteID;
END
