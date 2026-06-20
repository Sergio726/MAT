CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetClientesDisponibles] (@ViajeID varchar(max) = '')
AS 
/*-- =============================================
 -- Author: Garcia Sergio
 -- Create date: 04-01-2017
 -- Description: Clientes disponibles para reservar en un viaje.
 -- 2026-06-19 Sebastian Garcia: excluir también pasajeros en otros viajes
 --   con la misma FechaSalida.
 ============================================= */
BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	
	IF (@ViajeID != '')
	BEGIN
		DECLARE @ViajeGuid UNIQUEIDENTIFIER = TRY_CAST(@ViajeID AS UNIQUEIDENTIFIER);

		IF @ViajeGuid IS NULL
			RETURN;

		SELECT c.ClienteID,
			   RTRIM(LTRIM(p.Apellido)) AS Apellido,
			   p.Nombre,
			   ISNULL(p.TipoDocumento, 1) AS TipoDocumento,
			   p.NroDocumento,
			   ISNULL(p.Telefono, '') AS Telefono,
			   ISNULL(p.Email, '') AS Email
		FROM dbo.Cliente c
		INNER JOIN dbo.Persona p ON p.PersonaID = c.ClienteID
		WHERE dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(c.ClienteID, @ViajeGuid, NULL) = 0;
	END
END
