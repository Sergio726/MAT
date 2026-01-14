
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Pais table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePais_Update
(

	@PaisId uniqueidentifier   ,

	@OriginalPaisId uniqueidentifier   ,

	@Descripcion varchar (100)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Pais]
				SET
					[PaisID] = @PaisId
					,[Descripcion] = @Descripcion
				WHERE
[PaisID] = @OriginalPaisId 
				
			

