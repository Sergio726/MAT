
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the TipoCliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoCliente_Update
(

	@TipoId int   ,

	@OriginalTipoId int   ,

	@Descripcion varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[TipoCliente]
				SET
					[TipoID] = @TipoId
					,[Descripcion] = @Descripcion
				WHERE
[TipoID] = @OriginalTipoId 
				
			

