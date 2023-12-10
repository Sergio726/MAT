
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PasajeroViaje view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasajeroViaje_Get_List

AS


                    
                    SELECT
                        [Nro],
                        [ViajeID],
                        [PersonaID],
                        [BusID],
                        [Apellido],
                        [Nombre],
                        [TipoDocumento],
                        [NroDocumento],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [Sexo],
                        [LocalidadID],
                        [Nacionalidad],
                        [PaisResidencia],
                        [Domicilio],
                        [Ocupacion],
                        [Pasaporte],
                        [VencimientoPasaporte],
                        [EmisionPasaporte],
                        [PaisOrigen]
                    FROM
                        [dbo].[PasajeroViaje]
                        
                    SELECT @@ROWCOUNT