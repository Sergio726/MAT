
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the PaquetePrecio table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaquetePrecio_GetByPaquetePrecioId]
(

	@PaquetePrecioId uniqueidentifier   
)
AS


				SELECT
					[PaquetePrecioID],
					[PaqueteID],
					[PrecioID]
				FROM
					[dbo].[PaquetePrecio]
				WHERE
					[PaquetePrecioID] = @PaquetePrecioId
				SELECT @@ROWCOUNT
					
			



