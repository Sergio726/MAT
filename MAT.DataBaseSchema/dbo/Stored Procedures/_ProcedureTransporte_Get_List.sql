
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Transporte table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTransporte_Get_List

AS


				
				SELECT
					[TransporteID],
					[NroCoche],
					[MaxPasajeros],
					[KmRecorridos],
					[UltimoService],
					[Matricula]
				FROM
					[dbo].[Transporte]
					
				SELECT @@ROWCOUNT
			

