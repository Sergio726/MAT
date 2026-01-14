
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the PaqueteAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteAdicional_Insert
(

	@PaqueteAdicionalId uniqueidentifier    OUTPUT,

	@PaqueteId uniqueidentifier   ,

	@AdicionalId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PaqueteAdicional]
					(
					[PaqueteAdicionalID]
					,[PaqueteID]
					,[AdicionalID]
					)
				VALUES
					(
					@PaqueteAdicionalId
					,@PaqueteId
					,@AdicionalId
					)