
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Viaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureViaje_Insert
(

	@ViajeId uniqueidentifier    OUTPUT,

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


				
				INSERT INTO [dbo].[Viaje]
					(
					[ViajeID]
					,[PaqueteID]
					,[Origen]
					,[FechaSalida]
					,[HoraSalida]
					,[PaisOrigen]
					,[PaisDestino]
					,[Paso]
					,[Medio]
					,[BusID]
					,[FechaRegreso]
					,[HoraRegreso]
					,[Descripcion]
					)
				VALUES
					(
					@ViajeId
					,@PaqueteId
					,@Origen
					,@FechaSalida
					,@HoraSalida
					,@PaisOrigen
					,@PaisDestino
					,@Paso
					,@Medio
					,@BusId
					,@FechaRegreso
					,@HoraRegreso
					,@Descripcion
					)
				
									
							
			

