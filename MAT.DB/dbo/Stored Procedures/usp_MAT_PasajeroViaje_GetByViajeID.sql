CREATE PROCEDURE [dbo].[usp_MAT_PasajeroViaje_GetByViajeID] (@ViajeID varchar(36))
AS 
  /* ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 22/07/2022
  -- Description:  Get all Passages by ViajeID

  --
  --22/07/2022	Garcia Sergio: Create
    03/08/2022  Garcia Sergio: Fix order by Apellido
  -- ============================================*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 


	SELECT pv.ViajeID, 
		   Apellido = TRIM(pv.Apellido), 
		   pv.Nombre, 
		   pv.TipoDocumento, 
		   pv.NroDocumento, 
		   CUIT = ISNULL(c.Cuit, ''), 
		   FechaNacimiento = CONVERT(VARCHAR(10), pv.FechaNacimiento, 103), 
		   pv.Telefono
	FROM dbo.PasajeroViaje pv
		 LEFT JOIN dbo.Cliente c ON pv.PersonaID = c.ClienteID
	WHERE pv.ViajeID = @ViajeID
	UNION
	SELECT ViajeID = @ViajeID, 
		   Apellido = TRIM(per.Apellido), 
		   Nombre = per.Nombre, 
		   TipoDocumento = per.TipoDocumento, 
		   NroDocumento = per.NroDocumento, 
		   CUIT = ISNULL(c.Cuit, ''), 
		   FechaNacimiento = CONVERT(VARCHAR(10), per.FechaNacimiento, 103), 
		   Telefono = ''
	FROM dbo.PasajeroMenor pm
		 INNER JOIN dbo.Pasaje p ON pm.pasajeid = p.PasajeID
		 INNER JOIN dbo.Persona per ON pm.menorid = per.PersonaID
		 LEFT JOIN dbo.Cliente c ON per.PersonaID = c.ClienteID
	WHERE p.ViajeID = @ViajeID
	ORDER BY Apellido;

END