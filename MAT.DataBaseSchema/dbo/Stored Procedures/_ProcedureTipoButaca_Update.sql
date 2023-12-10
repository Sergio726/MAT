
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the TipoButaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoButaca_Update
(

	@TipoButacaId int   ,

	@OriginalTipoButacaId int   ,

	@Descripcion varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[TipoButaca]
				SET
					[TipoButacaID] = @TipoButacaId
					,[Descripcion] = @Descripcion
				WHERE
[TipoButacaID] = @OriginalTipoButacaId 
				
			

