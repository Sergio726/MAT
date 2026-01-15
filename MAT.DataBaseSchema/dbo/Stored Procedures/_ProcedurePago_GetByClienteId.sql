
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Pago table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePago_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
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
					[ClienteId] = @ClienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON