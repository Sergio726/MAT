
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Precio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePrecio_Get_List]

AS


				
				SELECT
					[PrecioID],
					[Monto],
					[Vigencia],
					[Descripcion],
					[Mes]
				FROM
					[dbo].[Precio]
					
				SELECT @@ROWCOUNT
			



