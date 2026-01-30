
CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetMayoresDisponibles] (@ViajeID varchar(max) = ''
																	 )
AS 
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 08-01-2017
-- Description:	mostrar clientes que no esten dentro de un viaje especifico 
--				
-- =============================================
BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	
	if (@ViajeID != '')
	begin

		select pj.PasajeID,
			   c.ClienteID,
			   rtrim(ltrim(p.Apellido)) as Apellido,
			   p.Nombre,
			   isnull(p.TipoDocumento,1) as TipoDocumento,
			   p.NroDocumento,
			   isnull(p.Telefono,'') AS Telefono,
			   isnull(p.Email,'') as Email
		from dbo.Cliente  c
		inner join dbo.Persona p on p.PersonaID = c.ClienteID
		inner join Pasaje pj on pj.PasajeroID = c.ClienteID
		where pj.ViajeID = @ViajeID
	
	end
END

