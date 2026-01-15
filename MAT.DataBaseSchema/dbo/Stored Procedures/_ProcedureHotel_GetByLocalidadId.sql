
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Hotel table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHotel_GetByLocalidadId
(

	@LocalidadId int   
)
AS


				SET ANSI_NULLS OFF
				
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
					[LocalidadID] = @LocalidadId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON