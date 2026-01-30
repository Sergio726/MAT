
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PaquetePrecio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaquetePrecio_Get_List]

AS


				
				SELECT
					[PaquetePrecioID],
					[PaqueteID],
					[PrecioID]
				FROM
					[dbo].[PaquetePrecio]
					
				SELECT @@ROWCOUNT
			



