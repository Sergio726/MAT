
 CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetPasajeroMenor] (@ViajeID varchar(max) = '' )
AS 
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 08-01-2017
-- Description:	ver los responsables de menores
-- =============================================
BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	
	if (@ViajeID != '')
	begin
		
		select pm.id,
			   pMayor.PersonaID as MayorID,
			   pMayor.Apellido as ApellidoMayor,
			   pMayor.Nombre as NombreMayor,
			   pMayor.NroDocumento as DocMayor,
			   pMenor.Apellido as ApellidoMenor,
			   pMenor.Nombre as NomreMenor,
			   pMenor.NroDocumento as DocMenor

			  
		from dbo.PasajeroMenor pm
		inner join dbo.Pasaje pj on pm.pasajeid = pj.PasajeID
		inner join dbo.Persona pMayor on pm.pasajeroid = pMayor.PersonaID 
		inner join dbo.Persona pMenor on pm.menorid = pMenor.PersonaID
		where pj.ViajeID = @ViajeID
					

	end
	else
	begin
		select '0' as id,
			   '0' as MayorID,
			   '0' as ApellidoMayor,
			   '0' as NombreMayor,
			   '0' as DocMayor,
			   '0' as ApellidoMenor,
			   '0' as NomreMenor,
			   '0' as DocMenor
	end

	select p.Descripcion as NombrePaquete,
			v.FechaSalida,
			v.FechaRegreso
	from Viaje v
	inner join Paquete p
		on v.PaqueteID = p.PaqueteID
	where v.ViajeID = @ViajeID	

END
