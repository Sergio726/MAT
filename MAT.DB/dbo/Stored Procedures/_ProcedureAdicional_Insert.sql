
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Adicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureAdicional_Insert]
(

	@AdicionalId uniqueidentifier    OUTPUT,

	@Monto float   ,

	@Descripcion varchar (MAX)  
)
AS


				
				INSERT INTO [dbo].[Adicional]
					(
					[AdicionalID]
					,[Monto]
					,[Descripcion]
					)
				VALUES
					(
					@AdicionalId
					,@Monto
					,@Descripcion
					)
				
									
							
			



