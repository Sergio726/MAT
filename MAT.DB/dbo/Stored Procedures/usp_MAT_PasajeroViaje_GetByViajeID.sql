CREATE PROCEDURE [dbo].[usp_MAT_PasajeroViaje_GetByViajeID] (@ViajeID varchar(36))
AS
/*
  =============================================
  -- Author:    Garcia Sergio
  -- Create date: 22/07/2022
  -- Description:  Get all Passages by ViajeID
  --
  --22/07/2022	Garcia Sergio: Create
    03/08/2022  Garcia Sergio: Fix order by Apellido
  =============================================

    Fecha: 29/03/2026
    Autor: Seba Garcia
    Detalle: EsMenorVinculado, ApellidoResponsable, NombreResponsable, NroDocResponsable (join Persona pasajeroid). UNION ALL en lugar de UNION.

    Fecha: 30/03/2026
    Autor: Sebastian Garcia
    Detalle: Se agregan al resultado las columnas Sexo y Nacionalidad (rama PasajeroViaje pv y menores Persona per).
*/
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
		   pv.Telefono,
		   pv.Sexo,
		   Nacionalidad = pv.Nacionalidad,
		   EsMenorVinculado = CAST(0 AS bit),
		   ApellidoResponsable = CAST(NULL AS nvarchar(100)),
		   NombreResponsable = CAST(NULL AS nvarchar(100)),
		   NroDocResponsable = CAST(NULL AS nvarchar(50))
	FROM dbo.PasajeroViaje pv
		 LEFT JOIN dbo.Cliente c ON pv.PersonaID = c.ClienteID
	WHERE pv.ViajeID = @ViajeID
	UNION ALL
	SELECT ViajeID = @ViajeID,
		   Apellido = TRIM(per.Apellido),
		   Nombre = per.Nombre,
		   TipoDocumento = per.TipoDocumento,
		   NroDocumento = per.NroDocumento,
		   CUIT = ISNULL(c.Cuit, ''),
		   FechaNacimiento = CONVERT(VARCHAR(10), per.FechaNacimiento, 103),
		   Telefono = '',
		   per.Sexo,
		   Nacionalidad = per.Nacionalidad,
		   EsMenorVinculado = CAST(1 AS bit),
		   ApellidoResponsable = TRIM(pMayor.Apellido),
		   NombreResponsable = pMayor.Nombre,
		   NroDocResponsable = pMayor.NroDocumento
	FROM dbo.PasajeroMenor pm
		 INNER JOIN dbo.Pasaje p ON pm.pasajeid = p.PasajeID
		 INNER JOIN dbo.Persona per ON pm.menorid = per.PersonaID
		 INNER JOIN dbo.Persona pMayor ON pm.pasajeroid = pMayor.PersonaID
		 LEFT JOIN dbo.Cliente c ON per.PersonaID = c.ClienteID
	WHERE p.ViajeID = @ViajeID
	ORDER BY Apellido;

END
