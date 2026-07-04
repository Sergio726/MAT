CREATE PROCEDURE [dbo].[usp_MAT_Factura_UpdateEntity]
    @FacturaID UNIQUEIDENTIFIER,
    @NroFactura VARCHAR (50) = NULL,
    @Monto FLOAT (53) = NULL,
    @Fecha DATETIME = NULL,
    @Tipo INT = NULL,
    @Estado INT = NULL,
    @ClienteID UNIQUEIDENTIFIER,
    @VendedorID UNIQUEIDENTIFIER,
    @DescuentoAplicado FLOAT (53),
    @Observaciones VARCHAR (MAX) = NULL,
    @DiasPreReserva INT = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Factura (NetTiers F6 - reemplaza
 --              FacturaService.Update/Save sobre facturas existentes).
 --              No toca MonedaTipo (columna desconocida por la entidad).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Factura
    SET NroFactura = @NroFactura,
        Monto = @Monto,
        Fecha = @Fecha,
        Tipo = @Tipo,
        Estado = @Estado,
        ClienteID = @ClienteID,
        VendedorID = @VendedorID,
        DescuentoAplicado = @DescuentoAplicado,
        Observaciones = @Observaciones,
        DiasPreReserva = @DiasPreReserva
    WHERE FacturaID = @FacturaID;
END
