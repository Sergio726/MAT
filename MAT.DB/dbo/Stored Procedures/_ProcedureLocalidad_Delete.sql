
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Localidad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureLocalidad_Delete]
(

	@Id int   
)
AS


				DELETE FROM [dbo].[Localidad] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			



