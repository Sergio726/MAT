
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Pago table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePago_GetByPagoId
(

	@PagoId uniqueidentifier   
)
AS


				SELECT
					[PagoID],
					[FechaPago],
					[Monto],
					[TipoPago],
					[TransaccionID],
					[ClienteId],
					[VendedorId],
					[NroRecibo],
					[EstadoRendicion],
					[CuentaCorrienteID]
				FROM
					[dbo].[Pago]
				WHERE
					[PagoID] = @PagoId
				SELECT @@ROWCOUNT
					
			

