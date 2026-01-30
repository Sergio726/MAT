CREATE PROCEDURE [dbo].[usp_MAT_Pago_GetPagosByFacturaID](@FacturaID uniqueidentifier)
AS
/*-----------------------------------------------------------
Author:    Garcia Sergio 
Create date: 2017-09-26
Description:  Get Pagos by FacturaID

2017-10-06	Garcia Sergio: add column Vendedor
2018-05-01	Garcia Sergio: add column MonedaTipo
-----------------------------------------------------------*/

	SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
BEGIN
	select p.PagoID,
			p.FechaPago,
			p.Monto,
			p.NroRecibo,
			p.TransaccionID,
			p.TipoPago,
			TipoPagoDescripcion = tp.Descripcion,
			Vendedor = per.Nombre + ' ' + per.Apellido,
			Moneda = isnull(mt.Codigo,'ARS'),
			MonedaPaquete = mp.Codigo
	from dbo.MovimientoCuenta mc
	inner join dbo.Pago p
		on mc.PagoID = p.PagoID
	inner join dbo.PagoTipo tp
		on p.TipoPago = tp.Id
	left join dbo.PagoDetalle pd
		on pd.PagoID = mc.PagoId
	left join dbo.MonedaTipo mt
		on mt.Id = pd.MontoRecibidoMonedaTipo
	left join dbo.Persona per
		on p.VendedorId = per.PersonaID
	cross apply(
		select top 1 mot.Codigo 
		from dbo.Pasaje pj
		inner join dbo.Viaje v
			on pj.ViajeID = v.ViajeID
		inner join dbo.Paquete p
			on v.PaqueteID = p.PaqueteID
		inner join dbo.MonedaTipo mot
			on mot.Id = p.Moneda
		where pj.FacturaID = @FacturaID
	) mp
	where mc.FacturaID = @FacturaID
		

END

