
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the PaqueteExcursion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteExcursion_GetByPaqueteExcursionId
(

	@PaqueteExcursionId uniqueidentifier   
)
AS


				SELECT
					[PaqueteExcursionID],
					[ExcursionID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteExcursion]
				WHERE
					[PaqueteExcursionID] = @PaqueteExcursionId
				SELECT @@ROWCOUNT