
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the TipoPago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPago_Update
(

	@TipoPagoId int   ,

	@OriginalTipoPagoId int   ,

	@Descripcion varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[TipoPago]
				SET
					[TipoPagoID] = @TipoPagoId
					,[Descripcion] = @Descripcion
				WHERE
[TipoPagoID] = @OriginalTipoPagoId 
				
			

