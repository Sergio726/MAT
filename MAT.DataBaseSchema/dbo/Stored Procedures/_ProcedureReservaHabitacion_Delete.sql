
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the ReservaHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureReservaHabitacion_Delete
(

	@ReservaHabitacionId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[ReservaHabitacion] WITH (ROWLOCK) 
				WHERE
					[ReservaHabitacionID] = @ReservaHabitacionId