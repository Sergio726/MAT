
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the PaqueteAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteAdicional_Update
(

	@PaqueteAdicionalId uniqueidentifier   ,

	@OriginalPaqueteAdicionalId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   ,

	@AdicionalId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PaqueteAdicional]
				SET
					[PaqueteAdicionalID] = @PaqueteAdicionalId
					,[PaqueteID] = @PaqueteId
					,[AdicionalID] = @AdicionalId
				WHERE
[PaqueteAdicionalID] = @OriginalPaqueteAdicionalId