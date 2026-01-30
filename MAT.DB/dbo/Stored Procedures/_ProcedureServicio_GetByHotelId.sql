
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Servicio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureServicio_GetByHotelId]
(

	@HotelId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ServicioID],
					[Descripcion],
					[Precio],
					[Moneda],
					[Iva],
					[Alicuota],
					[Validez],
					[VisibilidadTarifa],
					[ProveedorID],
					[TransporteID],
					[HotelID]
				FROM
					[dbo].[Servicio]
				WHERE
					[HotelID] = @HotelId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			



