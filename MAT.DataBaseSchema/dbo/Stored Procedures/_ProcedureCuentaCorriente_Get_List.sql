
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the CuentaCorriente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuentaCorriente_Get_List

AS


				
				SELECT
					[CuentaCorrienteID],
					[Fecha],
					[Monto],
					[ClienteID]
				FROM
					[dbo].[CuentaCorriente]
					
				SELECT @@ROWCOUNT
			

