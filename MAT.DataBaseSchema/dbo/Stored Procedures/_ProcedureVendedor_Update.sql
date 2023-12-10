
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Vendedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVendedor_Update
(

	@VendedorId uniqueidentifier   ,

	@OriginalVendedorId uniqueidentifier   ,

	@Descripcion varchar (100)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Vendedor]
				SET
					[VendedorID] = @VendedorId
					,[Descripcion] = @Descripcion
				WHERE
[VendedorID] = @OriginalVendedorId 
				
			

