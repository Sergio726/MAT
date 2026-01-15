
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the Pasaje table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasaje_Find
(

	@SearchUsingOR bit   = null ,

	@PasajeId uniqueidentifier   = null ,

	@PasajeroId uniqueidentifier   = null ,

	@ButacaId uniqueidentifier   = null ,

	@FechaReserva date   = null ,

	@FechaCompra date   = null ,

	@ViajeId uniqueidentifier   = null ,

	@FacturaId uniqueidentifier   = null ,

	@EstadoPasaje int   = null ,

	@VoucherId uniqueidentifier   = null ,

	@PrecioId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PasajeID]
	, [PasajeroID]
	, [ButacaID]
	, [FechaReserva]
	, [FechaCompra]
	, [ViajeID]
	, [FacturaID]
	, [EstadoPasaje]
	, [VoucherID]
	, [PrecioID]
    FROM
	[dbo].[Pasaje]
    WHERE 
	 ([PasajeID] = @PasajeId OR @PasajeId IS NULL)
	AND ([PasajeroID] = @PasajeroId OR @PasajeroId IS NULL)
	AND ([ButacaID] = @ButacaId OR @ButacaId IS NULL)
	AND ([FechaReserva] = @FechaReserva OR @FechaReserva IS NULL)
	AND ([FechaCompra] = @FechaCompra OR @FechaCompra IS NULL)
	AND ([ViajeID] = @ViajeId OR @ViajeId IS NULL)
	AND ([FacturaID] = @FacturaId OR @FacturaId IS NULL)
	AND ([EstadoPasaje] = @EstadoPasaje OR @EstadoPasaje IS NULL)
	AND ([VoucherID] = @VoucherId OR @VoucherId IS NULL)
	AND ([PrecioID] = @PrecioId OR @PrecioId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PasajeID]
	, [PasajeroID]
	, [ButacaID]
	, [FechaReserva]
	, [FechaCompra]
	, [ViajeID]
	, [FacturaID]
	, [EstadoPasaje]
	, [VoucherID]
	, [PrecioID]
    FROM
	[dbo].[Pasaje]
    WHERE 
	 ([PasajeID] = @PasajeId AND @PasajeId is not null)
	OR ([PasajeroID] = @PasajeroId AND @PasajeroId is not null)
	OR ([ButacaID] = @ButacaId AND @ButacaId is not null)
	OR ([FechaReserva] = @FechaReserva AND @FechaReserva is not null)
	OR ([FechaCompra] = @FechaCompra AND @FechaCompra is not null)
	OR ([ViajeID] = @ViajeId AND @ViajeId is not null)
	OR ([FacturaID] = @FacturaId AND @FacturaId is not null)
	OR ([EstadoPasaje] = @EstadoPasaje AND @EstadoPasaje is not null)
	OR ([VoucherID] = @VoucherId AND @VoucherId is not null)
	OR ([PrecioID] = @PrecioId AND @PrecioId is not null)
	SELECT @@ROWCOUNT			
  END
				

