
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Pasajero table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasajero_Update
(

	@PasajeroId uniqueidentifier   ,

	@OriginalPasajeroId uniqueidentifier   ,

	@Pasaporte varchar (100)  ,

	@VencimientoPasaporte date   ,

	@EmisionPasaporte date   ,

	@PaisOrigen varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Pasajero]
				SET
					[PasajeroID] = @PasajeroId
					,[Pasaporte] = @Pasaporte
					,[VencimientoPasaporte] = @VencimientoPasaporte
					,[EmisionPasaporte] = @EmisionPasaporte
					,[PaisOrigen] = @PaisOrigen
				WHERE
[PasajeroID] = @OriginalPasajeroId 
				
			

