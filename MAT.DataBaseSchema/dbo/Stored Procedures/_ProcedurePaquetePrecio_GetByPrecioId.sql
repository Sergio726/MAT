
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the PaquetePrecio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaquetePrecio_GetByPrecioId
(

	@PrecioId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PaquetePrecioID],
					[PaqueteID],
					[PrecioID]
				FROM
					[dbo].[PaquetePrecio]
				WHERE
					[PrecioID] = @PrecioId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON