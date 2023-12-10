
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Cuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuenta_Get_List

AS


				
				SELECT
					[CuentaID],
					[ClienteID],
					[Estado]
				FROM
					[dbo].[Cuenta]
					
				SELECT @@ROWCOUNT