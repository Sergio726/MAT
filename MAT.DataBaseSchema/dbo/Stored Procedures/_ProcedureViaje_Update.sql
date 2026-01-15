
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Viaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureViaje_Update
(

	@ViajeId uniqueidentifier   ,

	@OriginalViajeId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   ,

	@Origen varchar (50)  ,

	@FechaSalida date   ,

	@HoraSalida varchar (50)  ,

	@PaisOrigen varchar (50)  ,

	@PaisDestino varchar (50)  ,

	@Paso varchar (50)  ,

	@Medio varchar (50)  ,

	@BusId uniqueidentifier   ,

	@FechaRegreso date   ,

	@HoraRegreso varchar (50)  ,

	@Descripcion varchar (200)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Viaje]
				SET
					[ViajeID] = @ViajeId
					,[PaqueteID] = @PaqueteId
					,[Origen] = @Origen
					,[FechaSalida] = @FechaSalida
					,[HoraSalida] = @HoraSalida
					,[PaisOrigen] = @PaisOrigen
					,[PaisDestino] = @PaisDestino
					,[Paso] = @Paso
					,[Medio] = @Medio
					,[BusID] = @BusId
					,[FechaRegreso] = @FechaRegreso
					,[HoraRegreso] = @HoraRegreso
					,[Descripcion] = @Descripcion
				WHERE
[ViajeID] = @OriginalViajeId 
				
			

