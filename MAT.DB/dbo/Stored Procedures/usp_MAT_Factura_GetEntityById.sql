CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetEntityById]
    @FacturaID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Factura por ID, fila de entidad (NetTiers F6 - reemplaza FacturaService.GetByFacturaId)
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
    WHERE FacturaID = @FacturaID;
END
