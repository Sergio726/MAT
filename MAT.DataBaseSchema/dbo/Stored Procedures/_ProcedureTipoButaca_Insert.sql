
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the TipoButaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoButaca_Insert
(

	@TipoButacaId int   ,

	@Descripcion varchar (50)  
)
AS


				
				INSERT INTO [dbo].[TipoButaca]
					(
					[TipoButacaID]
					,[Descripcion]
					)
				VALUES
					(
					@TipoButacaId
					,@Descripcion
					)
				
									
							
			

