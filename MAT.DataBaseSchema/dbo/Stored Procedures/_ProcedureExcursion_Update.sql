
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Excursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureExcursion_Update
(

	@ExcursionId uniqueidentifier   ,

	@OriginalExcursionId uniqueidentifier   ,

	@Descripcion varchar (200)  ,

	@Costo float   ,

	@Observaciones varchar (MAX)  ,

	@ProveedorId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Excursion]
				SET
					[ExcursionID] = @ExcursionId
					,[Descripcion] = @Descripcion
					,[Costo] = @Costo
					,[Observaciones] = @Observaciones
					,[ProveedorID] = @ProveedorId
				WHERE
[ExcursionID] = @OriginalExcursionId