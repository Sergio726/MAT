
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the TipoCliente table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoCliente_Find
(

	@SearchUsingOR bit   = null ,

	@TipoId int   = null ,

	@Descripcion varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [TipoID]
	, [Descripcion]
    FROM
	[dbo].[TipoCliente]
    WHERE 
	 ([TipoID] = @TipoId OR @TipoId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [TipoID]
	, [Descripcion]
    FROM
	[dbo].[TipoCliente]
    WHERE 
	 ([TipoID] = @TipoId AND @TipoId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

