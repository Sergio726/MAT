
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Hotel table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHotel_GetByHotelId
(

	@HotelId uniqueidentifier   
)
AS


				SELECT
					[HotelID],
					[Nombre],
					[Direccion],
					[CP],
					[Telefono],
					[Email],
					[Contacto],
					[CantidadHabitaciones],
					[Categoria],
					[Child1],
					[Child2],
					[ChildHabitacion],
					[CheckIn],
					[CheckOut],
					[GoogleMapHtml],
					[LocalidadID]
				FROM
					[dbo].[Hotel]
				WHERE
					[HotelID] = @HotelId
				SELECT @@ROWCOUNT
					
			

