
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Vendedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVendedor_Get_List

AS


				
				SELECT
					[VendedorID],
					[Descripcion]
				FROM
					[dbo].[Vendedor]
					
				SELECT @@ROWCOUNT
			

