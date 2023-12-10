
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the PaqueteExcursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteExcursion_Update
(

	@PaqueteExcursionId uniqueidentifier   ,

	@OriginalPaqueteExcursionId uniqueidentifier   ,

	@ExcursionId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PaqueteExcursion]
				SET
					[PaqueteExcursionID] = @PaqueteExcursionId
					,[ExcursionID] = @ExcursionId
					,[PaqueteID] = @PaqueteId
				WHERE
[PaqueteExcursionID] = @OriginalPaqueteExcursionId