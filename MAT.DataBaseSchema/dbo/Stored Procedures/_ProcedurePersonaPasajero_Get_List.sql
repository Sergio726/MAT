
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PersonaPasajero view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePersonaPasajero_Get_List

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [Telefono],
                        [Domicilio],
                        [Email],
                        [FechaNacimiento],
                        [Sexo],
                        [PasajeroID],
                        [Pasaporte],
                        [VencimientoPasaporte],
                        [EmisionPasaporte],
                        [PaisOrigen],
                        [LocalidadID],
                        [TipoDocumento]
                    FROM
                        [dbo].[PersonaPasajero]
                        
                    SELECT @@ROWCOUNT			
                

