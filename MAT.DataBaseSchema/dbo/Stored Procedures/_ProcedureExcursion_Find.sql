
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the Excursion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureExcursion_Find
(

	@SearchUsingOR bit   = null ,

	@ExcursionId uniqueidentifier   = null ,

	@Descripcion varchar (200)  = null ,

	@Costo float   = null ,

	@Observaciones varchar (MAX)  = null ,

	@ProveedorId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ExcursionID]
	, [Descripcion]
	, [Costo]
	, [Observaciones]
	, [ProveedorID]
    FROM
	[dbo].[Excursion]
    WHERE 
	 ([ExcursionID] = @ExcursionId OR @ExcursionId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([Costo] = @Costo OR @Costo IS NULL)
	AND ([Observaciones] = @Observaciones OR @Observaciones IS NULL)
	AND ([ProveedorID] = @ProveedorId OR @ProveedorId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ExcursionID]
	, [Descripcion]
	, [Costo]
	, [Observaciones]
	, [ProveedorID]
    FROM
	[dbo].[Excursion]
    WHERE 
	 ([ExcursionID] = @ExcursionId AND @ExcursionId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([Costo] = @Costo AND @Costo is not null)
	OR ([Observaciones] = @Observaciones AND @Observaciones is not null)
	OR ([ProveedorID] = @ProveedorId AND @ProveedorId is not null)
	SELECT @@ROWCOUNT			
  END