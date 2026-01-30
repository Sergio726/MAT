CREATE PROCEDURE [dbo].[usp_MAT_ListaEspera_SelectByViajeId] (@ViajeID UNIQUEIDENTIFIER)
AS

/*-- =============================================  
  -- Author:    Garcia Sergio  
  -- Create date: 09/02/2023
  -- Description:  SELECT LISTA ESPERA BY VIAJE ID
  --History 
  09-02-2023  Garcia Sergio: create store procedure
  -- ============================================= */ 
 BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		SELECT LE.Id, 
			   LE.ViajeID, 
			   LE.ClienteID, 
			   Cliente = UPPER(isnull(LE.PasajeroTemporal,'')) + UPPER(isnull(PE.Apellido,'')) + ' ' + UPPER(isnull(PE.Nombre,'')) + ' (' + isnull(PE.NroDocumento,'') + ')', 
			   Vendedor = UPPER(USR.UserName), 
			   Fecha = CONVERT(VARCHAR(10), LE.Fecha, 103), 
			   LE.Observacion
		FROM dbo.ListaEspera LE
			 LEFT JOIN dbo.Persona PE ON Le.ClienteID = PE.PersonaID
			 LEFT JOIN [MAT.Session].[dbo].[UserProfile] USR ON LE.UsuarioID = USR.UserId
		WHERE LE.ViajeID = @ViajeID
END