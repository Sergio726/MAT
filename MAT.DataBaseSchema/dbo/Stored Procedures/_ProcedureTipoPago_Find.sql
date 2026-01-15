
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the TipoPago table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPago_Find
(

	@SearchUsingOR bit   = null ,

	@TipoPagoId int   = null ,

	@Descripcion varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [TipoPagoID]
	, [Descripcion]
    FROM
	[dbo].[TipoPago]
    WHERE 
	 ([TipoPagoID] = @TipoPagoId OR @TipoPagoId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [TipoPagoID]
	, [Descripcion]
    FROM
	[dbo].[TipoPago]
    WHERE 
	 ([TipoPagoID] = @TipoPagoId AND @TipoPagoId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

