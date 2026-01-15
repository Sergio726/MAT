
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the TipoPago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPago_Insert
(

	@TipoPagoId int   ,

	@Descripcion varchar (50)  
)
AS


				
				INSERT INTO [dbo].[TipoPago]
					(
					[TipoPagoID]
					,[Descripcion]
					)
				VALUES
					(
					@TipoPagoId
					,@Descripcion
					)
				
									
							
			

