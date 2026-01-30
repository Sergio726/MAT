create PROCEDURE usp_Reserva_GetSenasByViajeID (@ViajeID uniqueidentifier)
as
 /*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 10-18-2017 
  -- Description:  Get pasajes with pay seña incompleta
        
  -- =============================================*/ 
begin
 SET nocount, xact_abort ON; 
 SET TRANSACTION isolation level READ uncommitted; 


		select p.PasajeID,
			   f.FacturaID,
			   f.ClienteID,
			   tf.TotalFactura,
			   tp.TotalPagos 
		from dbo.pasaje p
		inner join dbo.Factura f
		on p.FacturaID = f.FacturaID
		cross apply (
			select TotalFactura = sum(df.Precio)
			from dbo.DetalleFactura df
			where df.FacturaID = p.FacturaID
		) tf
		cross apply(
			select TotalPagos = sum(pa.Monto) from dbo.Pago pa
			inner join dbo.MovimientoCuenta mc
			on pa.PagoID = mc.PagoID
			and mc.FacturaID = p.FacturaID
		) tp
		where p.ViajeID = @ViajeID
		and p.EstadoPasaje in (3,8) --pasaje señado

end
