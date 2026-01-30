
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the PasajeAdicional table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePasajeAdicional_Find]
(

	@SearchUsingOR bit   = null ,

	@PasajeAdicionalId uniqueidentifier   = null ,

	@PasajeId uniqueidentifier   = null ,

	@AdicionalId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PasajeAdicionalID]
	, [PasajeID]
	, [AdicionalID]
    FROM
	[dbo].[PasajeAdicional]
    WHERE 
	 ([PasajeAdicionalID] = @PasajeAdicionalId OR @PasajeAdicionalId IS NULL)
	AND ([PasajeID] = @PasajeId OR @PasajeId IS NULL)
	AND ([AdicionalID] = @AdicionalId OR @AdicionalId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PasajeAdicionalID]
	, [PasajeID]
	, [AdicionalID]
    FROM
	[dbo].[PasajeAdicional]
    WHERE 
	 ([PasajeAdicionalID] = @PasajeAdicionalId AND @PasajeAdicionalId is not null)
	OR ([PasajeID] = @PasajeId AND @PasajeId is not null)
	OR ([AdicionalID] = @AdicionalId AND @AdicionalId is not null)
	SELECT @@ROWCOUNT			
  END
				



