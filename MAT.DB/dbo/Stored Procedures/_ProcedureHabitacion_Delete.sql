
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Habitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureHabitacion_Delete]
(

	@HabitacionId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Habitacion] WITH (ROWLOCK) 
				WHERE
					[HabitacionID] = @HabitacionId
					
			



