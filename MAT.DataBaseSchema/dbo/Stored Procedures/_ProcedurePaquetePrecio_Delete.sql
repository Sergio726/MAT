
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the PaquetePrecio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaquetePrecio_Delete
(

	@PaquetePrecioId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PaquetePrecio] WITH (ROWLOCK) 
				WHERE
					[PaquetePrecioID] = @PaquetePrecioId