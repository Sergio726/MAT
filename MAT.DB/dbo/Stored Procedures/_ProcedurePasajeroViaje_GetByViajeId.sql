
create procedure [dbo].[_ProcedurePasajeroViaje_GetByViajeId]

(

	@ViajeId uniqueidentifier   
)
as

select Pje.ViajeID, Per.PersonaID, Per.Apellido, Per .Nombre, Per.TipoDocumento, Per.NroDocumento, Per.Telefono, Per.Email,
Per.FechaNacimiento, Per.Sexo, Per.LocalidadID, Pas.Pasaporte, Pas.VencimientoPasaporte, Pas .EmisionPasaporte, Pas.PaisOrigen
from  Persona Per inner join Pasajero Pas 
on Per.PersonaID = Pas.PasajeroID inner join Pasaje Pje
on Pje.PasajeroID = Pas.PasajeroID where Pje.ViajeID = @ViajeId

















