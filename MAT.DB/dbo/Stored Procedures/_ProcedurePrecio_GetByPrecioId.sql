
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Precio table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePrecio_GetByPrecioId]
(

	@PrecioId uniqueidentifier   
)
AS


				SELECT
					[PrecioID],
					[Monto],
					[Vigencia],
					[Descripcion],
					[Mes]
				FROM
					[dbo].[Precio]
				WHERE
					[PrecioID] = @PrecioId
				SELECT @@ROWCOUNT
					
			



