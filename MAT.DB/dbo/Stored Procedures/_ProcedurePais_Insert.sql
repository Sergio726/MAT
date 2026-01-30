
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Pais table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePais_Insert]
(

	@PaisId uniqueidentifier    OUTPUT,

	@Descripcion varchar (100)  
)
AS


				
				INSERT INTO [dbo].[Pais]
					(
					[PaisID]
					,[Descripcion]
					)
				VALUES
					(
					@PaisId
					,@Descripcion
					)
				
									
							
			



