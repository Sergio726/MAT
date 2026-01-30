
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Cuenta table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureCuenta_GetByCuentaId]
(

	@CuentaId uniqueidentifier   
)
AS


				SELECT
					[CuentaID],
					[ClienteID],
					[Estado]
				FROM
					[dbo].[Cuenta]
				WHERE
					[CuentaID] = @CuentaId
				SELECT @@ROWCOUNT
					
			



