
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Adicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureAdicional_Update
(

	@AdicionalId uniqueidentifier   ,

	@OriginalAdicionalId uniqueidentifier   ,

	@Monto float   ,

	@Descripcion varchar (MAX)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Adicional]
				SET
					[AdicionalID] = @AdicionalId
					,[Monto] = @Monto
					,[Descripcion] = @Descripcion
				WHERE
[AdicionalID] = @OriginalAdicionalId