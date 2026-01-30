
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Reserva view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureReserva_Get_List]

AS


                    
                    SELECT
                        [PasajeID],
                        [FechaReserva],
                        [Apellido],
                        [Nombre],
                        [NroDocumento],
                        [ViajeID],
                        [Paquete],
                        [FacturaID],
                        [ClienteID],
                        [TipoCliente]
                    FROM
                        [dbo].[Reserva]
                        
                    SELECT @@ROWCOUNT			
                



