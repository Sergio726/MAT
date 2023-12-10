
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Factura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureFactura_Delete
(

	@FacturaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Factura] WITH (ROWLOCK) 
				WHERE
					[FacturaID] = @FacturaId
					
			

