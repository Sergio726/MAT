
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Provincia table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureProvincia_Update
(

	@Id int   ,

	@Nombre nvarchar (250)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Provincia]
				SET
					[Nombre] = @Nombre
				WHERE
[ID] = @Id 
				
			

