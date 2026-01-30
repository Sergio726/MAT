
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the PaqueteServicio table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaqueteServicio_Find]
(

	@SearchUsingOR bit   = null ,

	@PaqueteServicioId uniqueidentifier   = null ,

	@ServicioId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaqueteServicioID]
	, [ServicioID]
	, [PaqueteID]
    FROM
	[dbo].[PaqueteServicio]
    WHERE 
	 ([PaqueteServicioID] = @PaqueteServicioId OR @PaqueteServicioId IS NULL)
	AND ([ServicioID] = @ServicioId OR @ServicioId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaqueteServicioID]
	, [ServicioID]
	, [PaqueteID]
    FROM
	[dbo].[PaqueteServicio]
    WHERE 
	 ([PaqueteServicioID] = @PaqueteServicioId AND @PaqueteServicioId is not null)
	OR ([ServicioID] = @ServicioId AND @ServicioId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	SELECT @@ROWCOUNT			
  END
				



