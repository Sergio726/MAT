
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Butaca table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureButaca_Find
(

	@SearchUsingOR bit   = null ,

	@ButacaId uniqueidentifier   = null ,

	@NroButaca int   = null ,

	@Piso int   = null ,

	@Ubicacion int   = null ,

	@Tipo int   = null ,

	@TransporteId uniqueidentifier   = null ,

	@Fila varchar (2)  = null ,

	@Posicion varchar (1)  = null ,

	@CodigoButaca varchar (4)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ButacaID]
	, [NroButaca]
	, [Piso]
	, [Ubicacion]
	, [Tipo]
	, [TransporteID]
	, [Fila]
	, [Posicion]
	, [CodigoButaca]
    FROM
	[dbo].[Butaca]
    WHERE 
	 ([ButacaID] = @ButacaId OR @ButacaId IS NULL)
	AND ([NroButaca] = @NroButaca OR @NroButaca IS NULL)
	AND ([Piso] = @Piso OR @Piso IS NULL)
	AND ([Ubicacion] = @Ubicacion OR @Ubicacion IS NULL)
	AND ([Tipo] = @Tipo OR @Tipo IS NULL)
	AND ([TransporteID] = @TransporteId OR @TransporteId IS NULL)
	AND ([Fila] = @Fila OR @Fila IS NULL)
	AND ([Posicion] = @Posicion OR @Posicion IS NULL)
	AND ([CodigoButaca] = @CodigoButaca OR @CodigoButaca IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ButacaID]
	, [NroButaca]
	, [Piso]
	, [Ubicacion]
	, [Tipo]
	, [TransporteID]
	, [Fila]
	, [Posicion]
	, [CodigoButaca]
    FROM
	[dbo].[Butaca]
    WHERE 
	 ([ButacaID] = @ButacaId AND @ButacaId is not null)
	OR ([NroButaca] = @NroButaca AND @NroButaca is not null)
	OR ([Piso] = @Piso AND @Piso is not null)
	OR ([Ubicacion] = @Ubicacion AND @Ubicacion is not null)
	OR ([Tipo] = @Tipo AND @Tipo is not null)
	OR ([TransporteID] = @TransporteId AND @TransporteId is not null)
	OR ([Fila] = @Fila AND @Fila is not null)
	OR ([Posicion] = @Posicion AND @Posicion is not null)
	OR ([CodigoButaca] = @CodigoButaca AND @CodigoButaca is not null)
	SELECT @@ROWCOUNT			
  END
				

