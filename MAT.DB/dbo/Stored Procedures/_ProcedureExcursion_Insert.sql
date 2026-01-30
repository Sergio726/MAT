
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Excursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureExcursion_Insert]
(

	@ExcursionId uniqueidentifier    OUTPUT,

	@Descripcion varchar (200)  ,

	@Costo float   ,

	@Observaciones varchar (MAX)  ,

	@ProveedorId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[Excursion]
					(
					[ExcursionID]
					,[Descripcion]
					,[Costo]
					,[Observaciones]
					,[ProveedorID]
					)
				VALUES
					(
					@ExcursionId
					,@Descripcion
					,@Costo
					,@Observaciones
					,@ProveedorId
					)
				
									
							
			



