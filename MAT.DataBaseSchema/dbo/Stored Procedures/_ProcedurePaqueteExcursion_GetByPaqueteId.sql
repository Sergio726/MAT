
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the PaqueteExcursion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteExcursion_GetByPaqueteId
(

	@PaqueteId uniqueidentifier   
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
					[PaqueteID] = @PaqueteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON