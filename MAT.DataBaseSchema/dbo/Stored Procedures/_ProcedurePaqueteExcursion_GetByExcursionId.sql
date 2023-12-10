
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the PaqueteExcursion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteExcursion_GetByExcursionId
(

	@ExcursionId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteExcursionID],
					[ExcursionID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteExcursion]
				WHERE
					[ExcursionID] = @ExcursionId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON