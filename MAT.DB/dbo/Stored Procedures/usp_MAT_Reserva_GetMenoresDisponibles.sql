
 CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetMenoresDisponibles] (@ViajeID varchar(max) = ''
																 )
AS 
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 08-01-2017
-- Description:	muestra una lista de menores disponibles por viajes, los menores no pueden ser mayores de 4 años
-- =============================================
BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	
	if (@ViajeID != '')
	begin
		
		select pm.menorid
		into #tblMenoresEnViaje
		from dbo.PasajeroMenor pm
		inner join Pasaje pj on pm.pasajeid = pj.PasajeID
		where pj.ViajeID = @ViajeID

		select 
			   p.PersonaID,
			   p.Apellido,
			   p.Nombre,
			   p.NroDocumento
		from dbo.Persona p
		left join #tblMenoresEnViaje mv on p.PersonaID = mv.menorid
		where (cast((datediff(dd, p.FechaNacimiento , GETDATE()) + 1) / 365.25 as int))  < 5 --personas menores de 5 años
		and mv.menorid is null

	
	end
END
