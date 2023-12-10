
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Cuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuenta_Delete
(

	@CuentaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Cuenta] WITH (ROWLOCK) 
				WHERE
					[CuentaID] = @CuentaId