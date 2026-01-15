
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the PaqueteAdicional table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteAdicional_GetByAdicionalId
(

	@AdicionalId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteAdicionalID],
					[PaqueteID],
					[AdicionalID]
				FROM
					[dbo].[PaqueteAdicional]
				WHERE
					[AdicionalID] = @AdicionalId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON