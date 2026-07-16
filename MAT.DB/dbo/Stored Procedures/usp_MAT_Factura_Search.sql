CREATE PROCEDURE [dbo].[usp_MAT_Factura_Search](@ClienteId varchar(36) = null,
												@ViejeId varchar(36) = null) 
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-15
  -- Description: Busqueda de facturas por cliente y/o viaje.
  --              Incluye Monto, Saldo (fn_MAT_SaldoFactura) y MontoPagado.
  ============================================= */
SET nocount, xact_abort ON; 
SET TRANSACTION isolation level READ uncommitted;
BEGIN
	declare @SQLQuery nvarchar(max),
			@SQLString1 nvarchar(max) = '',
			@SQLString2 nvarchar(max) = '',
			@SQLWhere nvarchar(max) = '',
			@ParmDefinition nvarchar(500);  

	set @SQLString1 = N'
		select distinct 
			   f.FacturaID,
			   f.NroFactura,
			   Monto = calc.Monto,
			   Saldo = calc.Saldo,
			   MontoPagado = calc.Monto - calc.Saldo,
			   [FechaFactura] = f.Fecha,
			   EstadoFactura = ef.Descripcion,
			   Paquete = pq.Descripcion,
			   Viaje = v.Descripcion,
			   ClienteNomre = per.FullName
		from Pasaje p
		inner join dbo.Factura f
			on p.FacturaID = f.FacturaID
		inner join dbo.Viaje v
			on p.ViajeID = v.ViajeID
		inner join dbo.Paquete pq
			on v.PaqueteID = pq.PaqueteID
		inner join dbo.EstadoFactura ef
			on f.Estado = ef.Id
		inner join dbo.Persona per
			on f.ClienteID = per.PersonaID
		cross apply (
			select
				Monto = ISNULL((select sum(df.Precio * df.Cantidad) from DetalleFactura df where df.FacturaID = f.FacturaID), 0),
				Saldo = dbo.fn_MAT_SaldoFactura(f.FacturaID)
		) calc
		where 1 = 1'

	set @SQLString2 = N'
	union
	select distinct 
		f.FacturaID,
		f.NroFactura,
		Monto = calc.Monto,
		Saldo = calc.Saldo,
		MontoPagado = calc.Monto - calc.Saldo,
		[FechaFactura] = f.Fecha,
		EstadoFactura = ef.Descripcion,
		Paquete = pq.Descripcion,
		Viaje = v.Descripcion,
		ClienteNomre = per.FullName
	from Pasaje p
	inner join PasajeroMenor pm
		on p.PasajeID = pm.pasajeid
	inner join dbo.Factura f
		on p.FacturaID = f.FacturaID
	inner join dbo.Viaje v
		on p.ViajeID = v.ViajeID
	inner join dbo.Paquete pq
		on v.PaqueteID = pq.PaqueteID
	inner join dbo.EstadoFactura ef
		on f.Estado = ef.Id
	inner join dbo.Persona per
		on f.ClienteID = per.PersonaID
	cross apply (
		select
			Monto = ISNULL((select sum(df.Precio * df.Cantidad) from DetalleFactura df where df.FacturaID = f.FacturaID), 0),
			Saldo = dbo.fn_MAT_SaldoFactura(f.FacturaID)
	) calc
	where not exists (select * from dbo.Pasajero p1 where p1.PasajeroID = pm.menorid)'	

	if (@ClienteId is not null or @ClienteId <> '')
		set @SQLWhere = @SQLWhere  + N' and (f.ClienteID = @ClienteId or p.PasajeroID = @ClienteId)'

	if (@ViejeId is not null or @ViejeId <> '')
		set @SQLWhere = @SQLWhere  + N' and (v.ViajeID = @ViejeId)'

	

	 set @SQLQuery = @SQLString1 + @SQLWhere + @SQLString2 + @SQLWhere
	 --print @SQLQuery

	set @ParmDefinition = N'@ClienteId varchar(36), @ViejeId varchar(36)'
	EXECUTE sp_executesql @SQLQuery, @ParmDefinition,  
						  @ClienteId = @ClienteId,
						  @ViejeId = @ViejeId; 

END
