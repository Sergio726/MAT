
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Paquete table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaquete_GetByPaqueteId]
(

	@PaqueteId uniqueidentifier   
)
AS


				SELECT
					[PaqueteID],
					[Descripcion],
					[PrecioCama],
					[Moneda],
					[Iva],
					[Alicuota],
					[Temporada],
					[Cotizacion],
					[Codigo],
					[DestinoID],
					[PrecioSemiCama],
					[Foto]
				FROM
					[dbo].[Paquete]
				WHERE
					[PaqueteID] = @PaqueteId
				SELECT @@ROWCOUNT
					
			



