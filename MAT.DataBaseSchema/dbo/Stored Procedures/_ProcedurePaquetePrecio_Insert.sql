
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the PaquetePrecio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaquetePrecio_Insert
(

	@PaquetePrecioId uniqueidentifier    OUTPUT,

	@PaqueteId uniqueidentifier   ,

	@PrecioId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PaquetePrecio]
					(
					[PaquetePrecioID]
					,[PaqueteID]
					,[PrecioID]
					)
				VALUES
					(
					@PaquetePrecioId
					,@PaqueteId
					,@PrecioId
					)