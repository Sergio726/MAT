
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Provincia table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureProvincia_Insert
(

	@Id int    OUTPUT,

	@Nombre nvarchar (250)  
)
AS


				
				INSERT INTO [dbo].[Provincia]
					(
					[Nombre]
					)
				VALUES
					(
					@Nombre
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

