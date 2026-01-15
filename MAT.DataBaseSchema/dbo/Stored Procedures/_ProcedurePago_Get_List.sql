
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Pago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePago_Get_List

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
					
				SELECT @@ROWCOUNT
			

