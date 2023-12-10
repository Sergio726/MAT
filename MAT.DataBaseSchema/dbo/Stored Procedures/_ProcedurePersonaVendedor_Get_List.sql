
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PersonaVendedor view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePersonaVendedor_Get_List

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [Domicilio],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [Sexo],
                        [LocalidadID],
                        [Descripcion],
                        [VendedorID],
                        [TipoDocumento]
                    FROM
                        [dbo].[PersonaVendedor]
                        
                    SELECT @@ROWCOUNT			
                

