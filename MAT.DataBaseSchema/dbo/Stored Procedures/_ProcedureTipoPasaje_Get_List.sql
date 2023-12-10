
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the TipoPasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPasaje_Get_List

AS


				
				SELECT
					[TipoID],
					[Descripcion]
				FROM
					[dbo].[TipoPasaje]
					
				SELECT @@ROWCOUNT
			

