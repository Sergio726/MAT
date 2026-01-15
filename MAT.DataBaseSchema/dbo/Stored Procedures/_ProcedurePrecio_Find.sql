
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the Precio table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePrecio_Find
(

	@SearchUsingOR bit   = null ,

	@PrecioId uniqueidentifier   = null ,

	@Monto float   = null ,

	@Vigencia datetime   = null ,

	@Descripcion varchar (100)  = null ,

	@Mes varchar (100)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PrecioID]
	, [Monto]
	, [Vigencia]
	, [Descripcion]
	, [Mes]
    FROM
	[dbo].[Precio]
    WHERE 
	 ([PrecioID] = @PrecioId OR @PrecioId IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([Vigencia] = @Vigencia OR @Vigencia IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([Mes] = @Mes OR @Mes IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PrecioID]
	, [Monto]
	, [Vigencia]
	, [Descripcion]
	, [Mes]
    FROM
	[dbo].[Precio]
    WHERE 
	 ([PrecioID] = @PrecioId AND @PrecioId is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([Vigencia] = @Vigencia AND @Vigencia is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([Mes] = @Mes AND @Mes is not null)
	SELECT @@ROWCOUNT			
  END