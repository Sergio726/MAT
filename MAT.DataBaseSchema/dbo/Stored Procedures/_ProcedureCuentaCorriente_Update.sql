
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the CuentaCorriente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCuentaCorriente_Update
(

	@CuentaCorrienteId uniqueidentifier   ,

	@OriginalCuentaCorrienteId uniqueidentifier   ,

	@Fecha datetime   ,

	@Monto float   ,

	@ClienteId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[CuentaCorriente]
				SET
					[CuentaCorrienteID] = @CuentaCorrienteId
					,[Fecha] = @Fecha
					,[Monto] = @Monto
					,[ClienteID] = @ClienteId
				WHERE
[CuentaCorrienteID] = @OriginalCuentaCorrienteId 
				
			

