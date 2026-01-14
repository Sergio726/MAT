
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the PaqueteExcursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteExcursion_Delete
(

	@PaqueteExcursionId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PaqueteExcursion] WITH (ROWLOCK) 
				WHERE
					[PaqueteExcursionID] = @PaqueteExcursionId