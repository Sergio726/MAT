
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the PasajeAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePasajeAdicional_Update]
(

	@PasajeAdicionalId uniqueidentifier   ,

	@OriginalPasajeAdicionalId uniqueidentifier   ,

	@PasajeId uniqueidentifier   ,

	@AdicionalId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PasajeAdicional]
				SET
					[PasajeAdicionalID] = @PasajeAdicionalId
					,[PasajeID] = @PasajeId
					,[AdicionalID] = @AdicionalId
				WHERE
[PasajeAdicionalID] = @OriginalPasajeAdicionalId 
				
			



