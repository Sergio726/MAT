
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the CuentaCorriente table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuentaCorriente_Find
(

	@SearchUsingOR bit   = null ,

	@CuentaCorrienteId uniqueidentifier   = null ,

	@Fecha datetime   = null ,

	@Monto float   = null ,

	@ClienteId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [CuentaCorrienteID]
	, [Fecha]
	, [Monto]
	, [ClienteID]
    FROM
	[dbo].[CuentaCorriente]
    WHERE 
	 ([CuentaCorrienteID] = @CuentaCorrienteId OR @CuentaCorrienteId IS NULL)
	AND ([Fecha] = @Fecha OR @Fecha IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [CuentaCorrienteID]
	, [Fecha]
	, [Monto]
	, [ClienteID]
    FROM
	[dbo].[CuentaCorriente]
    WHERE 
	 ([CuentaCorrienteID] = @CuentaCorrienteId AND @CuentaCorrienteId is not null)
	OR ([Fecha] = @Fecha AND @Fecha is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([ClienteID] = @ClienteId AND @ClienteId is not null)
	SELECT @@ROWCOUNT			
  END
				

