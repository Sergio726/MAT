
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the vPersona view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurevPersona_Get_List

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [TipoDocumento],
                        [NroDocumento],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [LocalidadID],
                        [UserId],
                        [Domicilio],
                        [Ocupacion],
                        [Nacionalidad],
                        [PaisResidencia],
                        [Sexo]
                    FROM
                        [dbo].[vPersona]
                        
                    SELECT @@ROWCOUNT