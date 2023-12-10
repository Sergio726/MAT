
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the MovimientoCuenta table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureMovimientoCuenta_GetByNotaId
(

	@NotaId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
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
					[NotaID] = @NotaId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON