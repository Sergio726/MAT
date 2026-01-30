
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the PaqueteExcursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaqueteExcursion_Insert]
(

	@PaqueteExcursionId uniqueidentifier    OUTPUT,

	@ExcursionId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PaqueteExcursion]
					(
					[PaqueteExcursionID]
					,[ExcursionID]
					,[PaqueteID]
					)
				VALUES
					(
					@PaqueteExcursionId
					,@ExcursionId
					,@PaqueteId
					)
				
									
							
			



