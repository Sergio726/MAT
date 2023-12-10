
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the TipoCliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoCliente_Get_List

AS


				
				SELECT
					[TipoID],
					[Descripcion]
				FROM
					[dbo].[TipoCliente]
					
				SELECT @@ROWCOUNT
			

