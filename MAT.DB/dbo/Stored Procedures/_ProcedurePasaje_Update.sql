
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Pasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePasaje_Update]
(

	@PasajeId uniqueidentifier   ,

	@OriginalPasajeId uniqueidentifier   ,

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


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Pasaje]
				SET
					[PasajeID] = @PasajeId
					,[PasajeroID] = @PasajeroId
					,[ButacaID] = @ButacaId
					,[FechaReserva] = @FechaReserva
					,[FechaCompra] = @FechaCompra
					,[ViajeID] = @ViajeId
					,[FacturaID] = @FacturaId
					,[EstadoPasaje] = @EstadoPasaje
					,[VoucherID] = @VoucherId
					,[PrecioID] = @PrecioId
				WHERE
[PasajeID] = @OriginalPasajeId 
				
			



