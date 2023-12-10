
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the CuentaCorriente table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuentaCorriente_GetByCuentaCorrienteId
(

	@CuentaCorrienteId uniqueidentifier   
)
AS


				SELECT
					[CuentaCorrienteID],
					[Fecha],
					[Monto],
					[ClienteID]
				FROM
					[dbo].[CuentaCorriente]
				WHERE
					[CuentaCorrienteID] = @CuentaCorrienteId
				SELECT @@ROWCOUNT
					
			

