
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Paquete table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaquete_GetByDestinoId
(

	@DestinoId int   
)
AS


				SET ANSI_NULLS OFF
				
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
					[PrecioSemiCama]
				FROM
					[dbo].[Paquete]
				WHERE
					[DestinoID] = @DestinoId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

