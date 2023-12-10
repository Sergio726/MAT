
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Destino table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureDestino_Get_List

AS


				
				SELECT
					[DestinoID],
					[LocalidadID]
				FROM
					[dbo].[Destino]
					
				SELECT @@ROWCOUNT
			

