
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the CuentaCorriente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuentaCorriente_Delete
(

	@CuentaCorrienteId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[CuentaCorriente] WITH (ROWLOCK) 
				WHERE
					[CuentaCorrienteID] = @CuentaCorrienteId
					
			

