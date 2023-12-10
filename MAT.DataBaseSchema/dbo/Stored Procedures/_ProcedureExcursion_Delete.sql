
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Excursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureExcursion_Delete
(

	@ExcursionId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Excursion] WITH (ROWLOCK) 
				WHERE
					[ExcursionID] = @ExcursionId