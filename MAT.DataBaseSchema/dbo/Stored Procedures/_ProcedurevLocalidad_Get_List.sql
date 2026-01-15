
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the vLocalidad view
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurevLocalidad_Get_List

AS


                    
                    SELECT
                        [ID],
                        [Nombre]
                    FROM
                        [dbo].[vLocalidad]
                        
                    SELECT @@ROWCOUNT