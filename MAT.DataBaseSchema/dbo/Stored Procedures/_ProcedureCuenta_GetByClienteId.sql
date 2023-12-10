
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Cuenta table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuenta_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[CuentaID],
					[ClienteID],
					[Estado]
				FROM
					[dbo].[Cuenta]
				WHERE
					[ClienteID] = @ClienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON