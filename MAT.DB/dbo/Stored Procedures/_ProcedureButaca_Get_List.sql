
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Butaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureButaca_Get_List]

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
					
				SELECT @@ROWCOUNT
			



