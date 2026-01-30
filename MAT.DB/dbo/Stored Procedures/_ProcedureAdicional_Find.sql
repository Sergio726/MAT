
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Adicional table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureAdicional_Find]
(

	@SearchUsingOR bit   = null ,

	@AdicionalId uniqueidentifier   = null ,

	@Monto float   = null ,

	@Descripcion varchar (MAX)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [AdicionalID]
	, [Monto]
	, [Descripcion]
    FROM
	[dbo].[Adicional]
    WHERE 
	 ([AdicionalID] = @AdicionalId OR @AdicionalId IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [AdicionalID]
	, [Monto]
	, [Descripcion]
    FROM
	[dbo].[Adicional]
    WHERE 
	 ([AdicionalID] = @AdicionalId AND @AdicionalId is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				



