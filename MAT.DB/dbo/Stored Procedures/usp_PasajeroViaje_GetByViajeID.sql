CREATE PROCEDURE [dbo].[usp_PasajeroViaje_GetByViajeID] (@ViajeID varchar(max))
AS 
/* =============================================
 Author:		Garcia Sergio
 Create date: 08-11-2016
 Description:	Trae las personas de un viaje
 Historial:

   01/23/2017	Garcia Sergio: se agrega los menores con seguro
   12/29/2019	Garcia Sergio: fix IsMenor
   07/08/2024	Garcia Sergio: Fix IsMenor, change logic 
   01/04/2025	Garcia Sergio: Add email
 =============================================*/


BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	with tmp as (
		select 
		   pv.ViajeID,
		   pv.PersonaID,
		   0 IsMenor
		from dbo.PasajeroViaje pv 
		where pv.ViajeID = @ViajeID
		and not exists (select * from dbo.PasajeroMenor pm where pm.menorid = pv.PersonaID)
		group by
		pv.ViajeID,
		pv.PersonaID

		union

		select 
		   pv.ViajeID,
		   pm.menorid,
		   1
		from dbo.PasajeroViaje pv 
		inner join dbo.Pasaje p
			on pv.ViajeID = p.ViajeID
		inner join dbo.PasajeroMenor pm
			on pm.menorid = pv.PersonaID
		where pv.ViajeID = @ViajeID
		group by
		pv.ViajeID,
		pm.menorid
	)
		

	select 
	   tmp.ViajeID,
	   p.PersonaID,
	   rtrim(ltrim(p.Apellido)) as Apellido,
	   p.Nombre,
	   p.TipoDocumento,
	   p.NroDocumento,
	   isnull(p.FechaNacimiento,'') AS FechaNacimiento,
	   isnull(p.Telefono,'') AS Telefono,
	   p.Sexo,
	   p.Nacionalidad,
	   p.PaisResidencia,
	   p.Ocupacion,
	   p.Email,
	   tmp.IsMenor
	from dbo.Persona p
	inner join tmp 
		on p.PersonaID = tmp.PersonaID
	order by p.Apellido,
	         p.Nombre

END