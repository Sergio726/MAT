
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the TipoPasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPasaje_Update
(

	@TipoId int   ,

	@OriginalTipoId int   ,

	@Descripcion varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[TipoPasaje]
				SET
					[TipoID] = @TipoId
					,[Descripcion] = @Descripcion
				WHERE
[TipoID] = @OriginalTipoId 
				
			

