
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the TipoButaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoButaca_Get_List

AS


				
				SELECT
					[TipoButacaID],
					[Descripcion]
				FROM
					[dbo].[TipoButaca]
					
				SELECT @@ROWCOUNT
			

