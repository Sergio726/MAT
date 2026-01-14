
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the TipoButaca table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoButaca_Find
(

	@SearchUsingOR bit   = null ,

	@TipoButacaId int   = null ,

	@Descripcion varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [TipoButacaID]
	, [Descripcion]
    FROM
	[dbo].[TipoButaca]
    WHERE 
	 ([TipoButacaID] = @TipoButacaId OR @TipoButacaId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [TipoButacaID]
	, [Descripcion]
    FROM
	[dbo].[TipoButaca]
    WHERE 
	 ([TipoButacaID] = @TipoButacaId AND @TipoButacaId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

