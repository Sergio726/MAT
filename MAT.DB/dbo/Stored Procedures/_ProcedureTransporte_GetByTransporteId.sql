
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Transporte table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureTransporte_GetByTransporteId]
(

	@TransporteId uniqueidentifier   
)
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
				WHERE
					[TransporteID] = @TransporteId
				SELECT @@ROWCOUNT
					
			



