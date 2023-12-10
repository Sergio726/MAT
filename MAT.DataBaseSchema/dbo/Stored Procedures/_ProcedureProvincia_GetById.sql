
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Provincia table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureProvincia_GetById
(

	@Id int   
)
AS


				SELECT
					[ID],
					[Nombre]
				FROM
					[dbo].[Provincia]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

