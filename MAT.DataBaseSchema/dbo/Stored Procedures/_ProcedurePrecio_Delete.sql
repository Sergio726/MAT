
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Precio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePrecio_Delete
(

	@PrecioId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Precio] WITH (ROWLOCK) 
				WHERE
					[PrecioID] = @PrecioId