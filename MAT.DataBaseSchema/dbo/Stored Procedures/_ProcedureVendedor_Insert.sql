
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Vendedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVendedor_Insert
(

	@VendedorId uniqueidentifier   ,

	@Descripcion varchar (100)  
)
AS


				
				INSERT INTO [dbo].[Vendedor]
					(
					[VendedorID]
					,[Descripcion]
					)
				VALUES
					(
					@VendedorId
					,@Descripcion
					)
				
									
							
			

