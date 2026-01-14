
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Provincia table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureProvincia_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[Provincia] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

