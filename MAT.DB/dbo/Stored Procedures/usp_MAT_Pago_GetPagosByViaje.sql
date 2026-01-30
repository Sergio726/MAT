CREATE PROCEDURE [dbo].[usp_MAT_Pago_GetPagosByViaje]
    @ViajeID UNIQUEIDENTIFIER
AS
/*
-- =============================================
-- Author:        Garcia Sergio
-- Create date:   10-02-2017
-- Description:   Get ALL Pagos by ViajeID
--                2017-12-01 Garcia Sergio added DISTINCT
-- =============================================
*/

SET NOCOUNT, XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN

    SELECT DISTINCT
        p.PagoID,
        p.FechaPago,
        p.Monto,
        p.TransaccionID,
        p.NroRecibo,
        TipoPagoDescripcion = pt.Descripcion,
        Cliente = per.Apellido + ' ' + per.Nombre
    FROM dbo.Pasaje pj
    INNER JOIN dbo.Factura f 
        ON pj.FacturaID = f.FacturaID
    INNER JOIN dbo.MovimientoCuenta mc 
        ON mc.FacturaID = f.FacturaID
    INNER JOIN dbo.Pago p 
        ON mc.PagoID = p.PagoID
    LEFT JOIN dbo.PagoTipo pt 
        ON pt.Id = p.TipoPago
    INNER JOIN dbo.Persona per 
        ON f.ClienteID = per.PersonaID
    WHERE pj.ViajeID = @ViajeID
	ORDER BY p.FechaPago

END