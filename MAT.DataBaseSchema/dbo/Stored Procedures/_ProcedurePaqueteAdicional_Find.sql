
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the PaqueteAdicional table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteAdicional_Find
(

	@SearchUsingOR bit   = null ,

	@PaqueteAdicionalId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null ,

	@AdicionalId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaqueteAdicionalID]
	, [PaqueteID]
	, [AdicionalID]
    FROM
	[dbo].[PaqueteAdicional]
    WHERE 
	 ([PaqueteAdicionalID] = @PaqueteAdicionalId OR @PaqueteAdicionalId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
	AND ([AdicionalID] = @AdicionalId OR @AdicionalId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaqueteAdicionalID]
	, [PaqueteID]
	, [AdicionalID]
    FROM
	[dbo].[PaqueteAdicional]
    WHERE 
	 ([PaqueteAdicionalID] = @PaqueteAdicionalId AND @PaqueteAdicionalId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	OR ([AdicionalID] = @AdicionalId AND @AdicionalId is not null)
	SELECT @@ROWCOUNT			
  END