
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Excursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureExcursion_Get_List

AS


				
				SELECT
					[ExcursionID],
					[Descripcion],
					[Costo],
					[Observaciones],
					[ProveedorID]
				FROM
					[dbo].[Excursion]
					
				SELECT @@ROWCOUNT