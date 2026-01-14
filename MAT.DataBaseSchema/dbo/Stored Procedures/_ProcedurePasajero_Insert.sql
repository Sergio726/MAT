
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the Pasajero table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasajero_Insert
(

	@PasajeroId uniqueidentifier   ,

	@Pasaporte varchar (100)  ,

	@VencimientoPasaporte date   ,

	@EmisionPasaporte date   ,

	@PaisOrigen varchar (50)  
)
AS


				
				INSERT INTO [dbo].[Pasajero]
					(
					[PasajeroID]
					,[Pasaporte]
					,[VencimientoPasaporte]
					,[EmisionPasaporte]
					,[PaisOrigen]
					)
				VALUES
					(
					@PasajeroId
					,@Pasaporte
					,@VencimientoPasaporte
					,@EmisionPasaporte
					,@PaisOrigen
					)
				
									
							
			

