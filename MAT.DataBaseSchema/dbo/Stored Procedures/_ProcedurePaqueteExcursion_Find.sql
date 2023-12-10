
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the PaqueteExcursion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteExcursion_Find
(

	@SearchUsingOR bit   = null ,

	@PaqueteExcursionId uniqueidentifier   = null ,

	@ExcursionId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaqueteExcursionID]
	, [ExcursionID]
	, [PaqueteID]
    FROM
	[dbo].[PaqueteExcursion]
    WHERE 
	 ([PaqueteExcursionID] = @PaqueteExcursionId OR @PaqueteExcursionId IS NULL)
	AND ([ExcursionID] = @ExcursionId OR @ExcursionId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaqueteExcursionID]
	, [ExcursionID]
	, [PaqueteID]
    FROM
	[dbo].[PaqueteExcursion]
    WHERE 
	 ([PaqueteExcursionID] = @PaqueteExcursionId AND @PaqueteExcursionId is not null)
	OR ([ExcursionID] = @ExcursionId AND @ExcursionId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	SELECT @@ROWCOUNT			
  END