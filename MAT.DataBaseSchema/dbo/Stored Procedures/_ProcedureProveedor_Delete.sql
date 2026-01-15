
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Proveedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureProveedor_Delete
(

	@ProveedorId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Proveedor] WITH (ROWLOCK) 
				WHERE
					[ProveedorID] = @ProveedorId
					
			

