CREATE PROCEDURE [dbo].[usp_MAT_Viaje_DeleteViaje](@ViajeId uniqueidentifier)
	
as
  /*============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-05-19
  -- Description:  delete viaje
  --History:
	2020-10-30	Garcia Sergio: 
   
  -- =============================================*/
begin
	
	begin tran;
	begin try

		DELETE rh
		FROM dbo.ReservaHabitacion rh
			 INNER JOIN dbo.Pasaje p ON rh.PasajeID = p.PasajeID
		WHERE p.ViajeID = @ViajeID;

		--select * 
		DELETE p
		FROM dbo.Pasaje p
			 INNER JOIN dbo.Viaje v ON p.ViajeID = v.ViajeID
		WHERE p.ViajeID = @ViajeID;

		--select *
		DELETE thv
		FROM dbo.TransHotelHabitacionViaje thv
		WHERE thv.ViajeID = @ViajeID;

		--select *
		DELETE vh
		FROM dbo.ViajeHotel vh
		WHERE vh.ViajeID = @ViajeID;
		DELETE Viaje
		WHERE ViajeID = @ViajeID;
		

		commit
	end try
	BEGIN CATCH
				IF @@TRANCOUNT > 0 
				ROLLBACK TRAN


				DECLARE @errmsg   AS NVARCHAR (2048)
				SELECT @errmsg = Error_message()

				select @ViajeId as ViajeId, @errmsg AS Result
	END CATCH

end