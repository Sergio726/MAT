
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Vendedor table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureVendedor_GetByVendedorId]
(

	@VendedorId uniqueidentifier   
)
AS


				SELECT
					[VendedorID],
					[Descripcion]
				FROM
					[dbo].[Vendedor]
				WHERE
					[VendedorID] = @VendedorId
				SELECT @@ROWCOUNT
					
			



