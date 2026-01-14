
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Destino table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureDestino_Update
(

	@DestinoId uniqueidentifier   ,

	@OriginalDestinoId uniqueidentifier   ,

	@LocalidadId int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Destino]
				SET
					[DestinoID] = @DestinoId
					,[LocalidadID] = @LocalidadId
				WHERE
[DestinoID] = @OriginalDestinoId 
				
			

