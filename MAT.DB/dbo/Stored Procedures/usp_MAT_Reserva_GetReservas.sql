CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetReservas]
(
	@ReservaId  UNIQUEIDENTIFIER = NULL,
	@ViajeId    UNIQUEIDENTIFIER = NULL,	
	@ClienteId	UNIQUEIDENTIFIER = NULL
)
AS 
/*
============================================= 
Author:    Ruben Tejerina 
Create date: 4/12/2024
Description:  Devolver la reserva basado en uno de los parametros

2024/12/28 Ruben Tejerina Added more details
============================================= 
*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

	select 		
		pr.Id as ReservaId	
		,pr.CreatedOn as ReservaCreatedOn
		,pr.ExpirationOn as ReservaExpirationOn
		,DATEDIFF(minute, getdate(),pr.ExpirationOn) as ReservaExpirationOnMinutes

		,pay.DateCreated as OrdenPagoDateCreated
		,pay.DateExpiration as OrdenPagoDateExpiration
		,pay.ItemTitle as OrdenPagoDescription
		,pay.ItemUnitPrice as OrdenPagoUnitPrice
		,pay.ItemQuanity as OrdenPagoQuantity
		,pay.PreferenceId as OrdenPagoPreferenceId
		,pay.InitPoint as OrdenPagoInitPoint
	
		,pr.FacturaId
		,ef.ID as FacturaEstado
		,ef.Descripcion as FacturaEstadoDescripcion
		,tTotal.Total as FacturaTotal
		,tPagos.MontoPagado as FacturaMontoPagado,
		(tTotal.Total - tPagos.MontoPagado) as FacturaSaldo		
		,tPagos.UltimaFechaPago as FacturaUltimaFechaPago
	
		,cli.FullName as ClienteFullName
		,cli.Email as ClienteEmail
	
		,v.ViajeID as ViajeId
		,v.Origen as ViajeOrigen
		,v.Descripcion as ViajeDescripcion
		,v.FechaSalida as ViajeFechaSalida
		,v.HoraSalida as ViajeHoraSalida
		,v.FechaRegreso as ViajeFechaRegreso
		,v.HoraRegreso as ViajeHoraRegreso
		,v.nDias as ViajeDias
		,v.nNoches as ViajeNoches

	from dbo.PedidoReserva pr
	inner join dbo.Viaje v on v.ViajeID = pr.ViajeId
	left join dbo.Payment pay on pay.ReservaId = pr.Id
	inner join dbo.Factura f on f.FacturaID = pr.FacturaId
	inner join dbo.EstadoFactura ef on ef.ID = f.Estado
	inner join dbo.Persona cli on cli.PersonaID = f.ClienteID
	cross apply (
		select sum(df.precio * df.Cantidad) as Total
		from DetalleFactura df
		where df.FacturaID = pr.FacturaId
	) as tTotal
	cross apply (		
		select 
			ISNULL(SUM(ISNULL(a_p.Monto,0)),0) as MontoPagado
			,MAX(a_p.FechaPago) as UltimaFechaPago		
		from MovimientoCuenta a_mc
		left join Pago a_p on a_p.PagoID = a_mc.PagoID
		where  
			a_mc.FacturaId = f.FacturaId
	) as tPagos
	where
	
		(@ReservaId is null or pr.Id = @ReservaId) 
		and (@ClienteId is null or pr.ClienteId = @ClienteId)
		and (@ViajeId is null or pr.ViajeId = @ViajeId)

END