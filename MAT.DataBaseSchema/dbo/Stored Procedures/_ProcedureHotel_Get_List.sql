
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Hotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHotel_Get_List

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
					
				SELECT @@ROWCOUNT
			

