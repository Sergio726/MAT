CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetPasajeroMenorByFactura] (@FacturaID varchar(max) = '')
AS 
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 28-01-2017
-- Description:	trae la lista de menores y tutotes segun facturaID
--				
-- =============================================
BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	
	select pm.id,
		   p.PasajeID AS PasajeID,
		   tutor.Apellido AS ApellidoMayor,
		   tutor.Nombre AS NombreMayor,
		   tutor.NroDocumento AS DocMayor,
		   menor.Apellido AS ApellidoMenor,
		   menor.Nombre AS NomreMenor,
		   menor.NroDocumento AS DocMenor
	from Pasaje p
	inner join PasajeroMenor pm 
		on pm.pasajeid = p.PasajeID
	inner join Persona tutor
		on pm.pasajeroid = tutor.PersonaID
	inner join Persona menor
		on pm.menorid = menor.PersonaID
	where p.FacturaID = @FacturaID

END

