
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Vendedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVendedor_Delete
(

	@VendedorId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Vendedor] WITH (ROWLOCK) 
				WHERE
					[VendedorID] = @VendedorId
					
			

