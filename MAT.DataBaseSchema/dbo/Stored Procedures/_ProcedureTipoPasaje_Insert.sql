
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the TipoPasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPasaje_Insert
(

	@TipoId int   ,

	@Descripcion varchar (50)  
)
AS


				
				INSERT INTO [dbo].[TipoPasaje]
					(
					[TipoID]
					,[Descripcion]
					)
				VALUES
					(
					@TipoId
					,@Descripcion
					)
				
									
							
			

