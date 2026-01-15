
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Excursion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureExcursion_GetByProveedorId
(

	@ProveedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ExcursionID],
					[Descripcion],
					[Costo],
					[Observaciones],
					[ProveedorID]
				FROM
					[dbo].[Excursion]
				WHERE
					[ProveedorID] = @ProveedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON