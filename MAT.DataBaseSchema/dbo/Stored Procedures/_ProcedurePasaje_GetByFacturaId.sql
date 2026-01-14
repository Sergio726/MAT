
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Pasaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasaje_GetByFacturaId
(

	@FacturaId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeID],
					[PasajeroID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[VoucherID],
					[PrecioID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[FacturaID] = @FacturaId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

