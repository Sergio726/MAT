
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the PaqueteAdicional table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaqueteAdicional_GetByPaqueteAdicionalId]
(

	@PaqueteAdicionalId uniqueidentifier   
)
AS


				SELECT
					[PaqueteAdicionalID],
					[PaqueteID],
					[AdicionalID]
				FROM
					[dbo].[PaqueteAdicional]
				WHERE
					[PaqueteAdicionalID] = @PaqueteAdicionalId
				SELECT @@ROWCOUNT
					
			



