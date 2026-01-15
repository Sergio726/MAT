
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the PaqueteAdicional table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteAdicional_GetByPaqueteId
(

	@PaqueteId uniqueidentifier   
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
					[PaqueteID] = @PaqueteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON