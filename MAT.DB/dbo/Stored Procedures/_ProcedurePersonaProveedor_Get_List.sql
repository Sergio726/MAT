
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PersonaProveedor view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePersonaProveedor_Get_List]

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [LocalidadID],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [Sexo],
                        [Domicilio],
                        [ProveedorID],
                        [RazonSocial],
                        [ProveedorLocalidadID],
                        [ProveedorTelefono],
                        [Fax],
                        [Web],
                        [ProveedorEmail],
                        [Idioma],
                        [CondicionIva],
                        [Cuit],
                        [FormaPago],
                        [TipoDocumento]
                    FROM
                        [dbo].[PersonaProveedor]
                        
                    SELECT @@ROWCOUNT			
                



