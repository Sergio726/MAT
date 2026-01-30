CREATE PROCEDURE [dbo].[usp_MAT_Pago_GetPagosByViaje](@ViajeID uniqueidentifier)
AS
/*-- =============================================   
---- Author:    Garcia Sergio   
---- Create date: 10-02-2017   
---- Description:  get ALL Pagos by ViajeID
     
	 2017-12-01 Garcia Sergio add distinct   
---- =============================================*/ 
		SET nocount, xact_abort ON; 
		SET TRANSACTION isolation level READ uncommitted; 
BEGIN
	select distinct p.PagoID,
		   p.FechaPago,
		   p.Monto,
		   p.TransaccionID,
		   p.NroRecibo,
		   TipoPagoDescripcion = pt.Descripcion,
		   Cliente = per.Apellido + ' ' + per.Nombre
	from dbo.Pasaje pj
	inner join dbo.Factura f
		on pj.FacturaID = f.FacturaID
	inner join dbo.MovimientoCuenta mc
		on mc.FacturaID = f.FacturaID
	inner join dbo.Pago p
		on mc.PagoID = p.PagoID
	left join dbo.PagoTipo pt
		on pt.Id = p.TipoPago
	inner join dbo.Persona per
		on f.ClienteID = per.PersonaID
	where pj.ViajeID = @ViajeID	
END
