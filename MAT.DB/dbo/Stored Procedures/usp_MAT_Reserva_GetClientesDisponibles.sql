create PROCEDURE [dbo].[usp_MAT_Reserva_GetClientesDisponibles] (@ViajeID varchar(max) = ''
																 )
AS 
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 04-01-2017
-- Description:	mostrar clientes que no esten dentro de un viaje especifico
-- =============================================
BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	
	if (@ViajeID != '')
	begin

		select PasajeroID 
		into #temp
		from Pasaje where ViajeID = @ViajeID

		select c.ClienteID,
			   rtrim(ltrim(p.Apellido)) as Apellido,
			   p.Nombre,
			   isnull(p.TipoDocumento,1) as TipoDocumento,
			   p.NroDocumento,
			   isnull(p.Telefono,'') AS Telefono,
			   isnull(p.Email,'') as Email
		from dbo.Cliente  c
		inner join dbo.Persona p on p.PersonaID = c.ClienteID
		left join #temp on PasajeroID = c.ClienteID
		where pasajeroid is null

		drop table #temp

	end
END
