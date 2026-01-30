
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PersonaCliente view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePersonaCliente_Get_List]

AS


                    
                    SELECT
                        [PersonaID],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [Telefono],
                        [Email],
                        [FechaNacimiento],
                        [Domicilio],
                        [Sexo],
                        [LocalidadID],
                        [ClienteID],
                        [RazonSocial],
                        [Cuit],
                        [Moneda],
                        [Empresa],
                        [Ocupacion],
                        [FormaPago],
                        [CondicionIva],
                        [VendedorID],
                        [Fax],
                        [Web],
                        [Idioma],
                        [Promotor],
                        [Observacion],
                        [TipoID],
                        [TipoDocumento],
                        [Celular]
                    FROM
                        [dbo].[PersonaCliente]
                        
                    SELECT @@ROWCOUNT			
                



