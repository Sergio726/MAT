
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the vConsultaReservaHabitacion view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurevConsultaReservaHabitacion_Get_List

AS


                    
                    SELECT
                        [ReservaHabitacionID],
                        [HotelID],
                        [Expiro],
                        [HabitacionID],
                        [Capacidad],
                        [Ocupacion],
                        [Estado],
                        [Desde],
                        [Hasta]
                    FROM
                        [dbo].[vConsultaReservaHabitacion]
                        
                    SELECT @@ROWCOUNT