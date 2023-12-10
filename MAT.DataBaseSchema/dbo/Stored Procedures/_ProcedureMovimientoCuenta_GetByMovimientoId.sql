
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the MovimientoCuenta table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureMovimientoCuenta_GetByMovimientoId
(

	@MovimientoId uniqueidentifier   
)
AS


				SELECT
					[MovimientoID],
					[PagoID],
					[FacturaID],
					[FechaRegistro],
					[CuentaID],
					[NotaID],
					[CuentaCorrienteID]
				FROM
					[dbo].[MovimientoCuenta]
				WHERE
					[MovimientoID] = @MovimientoId
				SELECT @@ROWCOUNT
					
			

