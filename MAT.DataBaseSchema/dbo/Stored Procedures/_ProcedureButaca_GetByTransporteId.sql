
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Butaca table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureButaca_GetByTransporteId
(

	@TransporteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
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
					[TransporteID] = @TransporteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

