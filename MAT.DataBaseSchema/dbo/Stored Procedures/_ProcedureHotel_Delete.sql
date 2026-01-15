
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Hotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHotel_Delete
(

	@HotelId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Hotel] WITH (ROWLOCK) 
				WHERE
					[HotelID] = @HotelId
					
			

