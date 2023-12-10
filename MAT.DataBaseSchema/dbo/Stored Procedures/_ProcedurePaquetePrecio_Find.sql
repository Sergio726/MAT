
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the PaquetePrecio table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaquetePrecio_Find
(

	@SearchUsingOR bit   = null ,

	@PaquetePrecioId uniqueidentifier   = null ,

	@PaqueteId uniqueidentifier   = null ,

	@PrecioId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaquetePrecioID]
	, [PaqueteID]
	, [PrecioID]
    FROM
	[dbo].[PaquetePrecio]
    WHERE 
	 ([PaquetePrecioID] = @PaquetePrecioId OR @PaquetePrecioId IS NULL)
	AND ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
	AND ([PrecioID] = @PrecioId OR @PrecioId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaquetePrecioID]
	, [PaqueteID]
	, [PrecioID]
    FROM
	[dbo].[PaquetePrecio]
    WHERE 
	 ([PaquetePrecioID] = @PaquetePrecioId AND @PaquetePrecioId is not null)
	OR ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	OR ([PrecioID] = @PrecioId AND @PrecioId is not null)
	SELECT @@ROWCOUNT			
  END