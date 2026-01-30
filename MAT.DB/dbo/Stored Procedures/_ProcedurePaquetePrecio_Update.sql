
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the PaquetePrecio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaquetePrecio_Update]
(

	@PaquetePrecioId uniqueidentifier   ,

	@OriginalPaquetePrecioId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   ,

	@PrecioId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PaquetePrecio]
				SET
					[PaquetePrecioID] = @PaquetePrecioId
					,[PaqueteID] = @PaqueteId
					,[PrecioID] = @PrecioId
				WHERE
[PaquetePrecioID] = @OriginalPaquetePrecioId 
				
			



