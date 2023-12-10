
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Cuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuenta_Update
(

	@CuentaId uniqueidentifier   ,

	@OriginalCuentaId uniqueidentifier   ,

	@ClienteId uniqueidentifier   ,

	@Estado bit   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Cuenta]
				SET
					[CuentaID] = @CuentaId
					,[ClienteID] = @ClienteId
					,[Estado] = @Estado
				WHERE
[CuentaID] = @OriginalCuentaId