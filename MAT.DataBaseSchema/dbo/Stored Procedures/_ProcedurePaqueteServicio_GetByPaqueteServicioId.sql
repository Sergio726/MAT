
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the PaqueteServicio table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteServicio_GetByPaqueteServicioId
(

	@PaqueteServicioId uniqueidentifier   
)
AS


				SELECT
					[PaqueteServicioID],
					[ServicioID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteServicio]
				WHERE
					[PaqueteServicioID] = @PaqueteServicioId
				SELECT @@ROWCOUNT
					
			

