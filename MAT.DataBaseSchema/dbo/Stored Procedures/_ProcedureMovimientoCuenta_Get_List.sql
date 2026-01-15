
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the MovimientoCuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureMovimientoCuenta_Get_List

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
					
				SELECT @@ROWCOUNT
			

