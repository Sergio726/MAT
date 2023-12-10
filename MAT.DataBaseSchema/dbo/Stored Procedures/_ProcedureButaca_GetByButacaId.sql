
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Butaca table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureButaca_GetByButacaId
(

	@ButacaId uniqueidentifier   
)
AS


				SELECT
					[ButacaID],
					[NroButaca],
					[Piso],
					[Ubicacion],
					[Tipo],
					[TransporteID],
					[Fila],
					[Posicion],
					[CodigoButaca]
				FROM
					[dbo].[Butaca]
				WHERE
					[ButacaID] = @ButacaId
				SELECT @@ROWCOUNT
					
			

