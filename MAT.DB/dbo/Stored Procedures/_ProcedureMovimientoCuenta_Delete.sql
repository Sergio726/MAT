
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the MovimientoCuenta table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureMovimientoCuenta_Delete]
(

	@MovimientoId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[MovimientoCuenta] WITH (ROWLOCK) 
				WHERE
					[MovimientoID] = @MovimientoId
					
			



