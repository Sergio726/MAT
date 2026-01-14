
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the CuentaCorriente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuentaCorriente_Insert
(

	@CuentaCorrienteId uniqueidentifier   ,

	@Fecha datetime   ,

	@Monto float   ,

	@ClienteId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[CuentaCorriente]
					(
					[CuentaCorrienteID]
					,[Fecha]
					,[Monto]
					,[ClienteID]
					)
				VALUES
					(
					@CuentaCorrienteId
					,@Fecha
					,@Monto
					,@ClienteId
					)
				
									
							
			

