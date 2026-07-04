CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetEntitiesByClienteId]
    @ClienteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Facturas de un cliente, filas de entidad (NetTiers F6 - reemplaza
 --              FacturaService.GetByClienteId; el usp_MAT_Factura_GetFacturaByClienteID
 --              existente devuelve un DTO joineado y se conserva)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        FacturaID,
        NroFactura,
        Monto,
        Fecha,
        Tipo,
        Estado,
        ClienteID,
        VendedorID,
        DescuentoAplicado,
        Observaciones,
        DiasPreReserva
    FROM dbo.Factura
    WHERE ClienteID = @ClienteID;
END
