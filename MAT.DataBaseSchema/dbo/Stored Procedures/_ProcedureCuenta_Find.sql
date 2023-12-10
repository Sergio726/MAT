
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Cuenta table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuenta_Find
(

	@SearchUsingOR bit   = null ,

	@CuentaId uniqueidentifier   = null ,

	@ClienteId uniqueidentifier   = null ,

	@Estado bit   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [CuentaID]
	, [ClienteID]
	, [Estado]
    FROM
	[dbo].[Cuenta]
    WHERE 
	 ([CuentaID] = @CuentaId OR @CuentaId IS NULL)
	AND ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
	AND ([Estado] = @Estado OR @Estado IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [CuentaID]
	, [ClienteID]
	, [Estado]
    FROM
	[dbo].[Cuenta]
    WHERE 
	 ([CuentaID] = @CuentaId AND @CuentaId is not null)
	OR ([ClienteID] = @ClienteId AND @ClienteId is not null)
	OR ([Estado] = @Estado AND @Estado is not null)
	SELECT @@ROWCOUNT			
  END