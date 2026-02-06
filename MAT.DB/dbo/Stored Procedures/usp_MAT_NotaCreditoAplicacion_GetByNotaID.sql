CREATE PROCEDURE [dbo].[usp_MAT_NotaCreditoAplicacion_GetByNotaID]
    @NotaID UNIQUEIDENTIFIER
AS
/*
=============================================
Author:     Seba Garcia
Create date: 2026-02
Description: Lista aplicaciones de una nota (pagos/facturas donde se usó el crédito).
=============================================
*/
SET NOCOUNT, XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN
    SELECT a.Id,
           a.NotaCreditoID,
           a.PagoID,
           a.FacturaID,
           a.MontoAplicado,
           a.Fecha,
           p.NroRecibo,
           f.NroFactura
    FROM dbo.NotaCreditoAplicacion a
    INNER JOIN dbo.Pago p ON p.PagoID = a.PagoID
    INNER JOIN dbo.Factura f ON f.FacturaID = a.FacturaID
    WHERE a.NotaCreditoID = @NotaID
    ORDER BY a.Fecha ASC;
END
GO
