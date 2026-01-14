
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the PasajeAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasajeAdicional_Insert
(

	@PasajeAdicionalId uniqueidentifier    OUTPUT,

	@PasajeId uniqueidentifier   ,

	@AdicionalId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PasajeAdicional]
					(
					[PasajeAdicionalID]
					,[PasajeID]
					,[AdicionalID]
					)
				VALUES
					(
					@PasajeAdicionalId
					,@PasajeId
					,@AdicionalId
					)