CREATE PROCEDURE [dbo].[usp_MAT_Factura_Search](@ClienteId varchar(36) = null,
												@ViejeId varchar(36) = null) 
AS
 /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 07/11/2017
  -- Description:  Remove registration ReservaHabitacion
  01-04-2018	Sergio Garcia: add IsTituarFactura
  -- ============================================= */
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
			   Monto = (select sum(df.Precio * df.Cantidad) from DetalleFactura df where df.FacturaID = f.FacturaID),
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
		where 1 = 1'

	set @SQLString2 = N'
	union
	select distinct 
		f.FacturaID,
		f.NroFactura,
		Monto = (select sum(df.Precio * df.Cantidad) from DetalleFactura df where df.FacturaID = f.FacturaID),
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
