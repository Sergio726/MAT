
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Nota table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureNota_Get_List

AS


				
				SELECT
					[NotaID],
					[PorcentajeRetencion],
					[MontoRetencion],
					[Fecha],
					[Dias],
					[ClienteID],
					[VendedorID],
					[NroNota]
				FROM
					[dbo].[Nota]
					
				SELECT @@ROWCOUNT