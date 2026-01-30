
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Cuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureCuenta_Insert]
(

	@CuentaId uniqueidentifier    OUTPUT,

	@ClienteId uniqueidentifier   ,

	@Estado bit   
)
AS


				
				INSERT INTO [dbo].[Cuenta]
					(
					[CuentaID]
					,[ClienteID]
					,[Estado]
					)
				VALUES
					(
					@CuentaId
					,@ClienteId
					,@Estado
					)
				
									
							
			



