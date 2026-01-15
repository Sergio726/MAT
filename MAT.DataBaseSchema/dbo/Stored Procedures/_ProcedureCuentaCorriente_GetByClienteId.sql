
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the CuentaCorriente table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuentaCorriente_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[CuentaCorrienteID],
					[Fecha],
					[Monto],
					[ClienteID]
				FROM
					[dbo].[CuentaCorriente]
				WHERE
					[ClienteID] = @ClienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

