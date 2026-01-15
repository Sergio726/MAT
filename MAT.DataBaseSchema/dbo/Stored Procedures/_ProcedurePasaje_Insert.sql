
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the Pasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasaje_Insert
(

	@PasajeId uniqueidentifier    OUTPUT,

	@PasajeroId uniqueidentifier   ,

	@ButacaId uniqueidentifier   ,

	@FechaReserva date   ,

	@FechaCompra date   ,

	@ViajeId uniqueidentifier   ,

	@FacturaId uniqueidentifier   ,

	@EstadoPasaje int   ,

	@VoucherId uniqueidentifier   ,

	@PrecioId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[Pasaje]
					(
					[PasajeID]
					,[PasajeroID]
					,[ButacaID]
					,[FechaReserva]
					,[FechaCompra]
					,[ViajeID]
					,[FacturaID]
					,[EstadoPasaje]
					,[VoucherID]
					,[PrecioID]
					)
				VALUES
					(
					@PasajeId
					,@PasajeroId
					,@ButacaId
					,@FechaReserva
					,@FechaCompra
					,@ViajeId
					,@FacturaId
					,@EstadoPasaje
					,@VoucherId
					,@PrecioId
					)
				
									
							
			

