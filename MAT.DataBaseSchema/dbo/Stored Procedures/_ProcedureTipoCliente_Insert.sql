
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the TipoCliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoCliente_Insert
(

	@TipoId int   ,

	@Descripcion varchar (50)  
)
AS


				
				INSERT INTO [dbo].[TipoCliente]
					(
					[TipoID]
					,[Descripcion]
					)
				VALUES
					(
					@TipoId
					,@Descripcion
					)
				
									
							
			

