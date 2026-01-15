
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the PaqueteServicio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteServicio_GetByServicioId
(

	@ServicioId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaqueteServicioID],
					[ServicioID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteServicio]
				WHERE
					[ServicioID] = @ServicioId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

