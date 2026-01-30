
CREATE PROCEDURE [dbo].[usp_MAT_ObservacionViaje_Insert](@ViajeID UNIQUEIDENTIFIER,
														 @VendedorID INT,
														 @Fecha DATETIME,
														 @PasajerosID VARCHAR(MAX) = NULL,
														 @Detalle VARCHAR(MAX),
														 @CategoriaID INT,
														 @ObservacionViajeID INT OUTPUT
														 )

AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-08-06
-- Description: insert ObservacionViaje

2019-12-05  GARCIA SERGIO:  add parameter @Fecha
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try
		DECLARE @tblObservacionViaje  TABLE (ObservacionViajeId int NOT NULL)

		begin tran
			insert into dbo.ObservacionViaje(ViajeID, VendedorID,Fecha, PasajerosID, Detalle, CategoriaId)
			output inserted.Id into @tblObservacionViaje
			values(@ViajeID,@VendedorID,@Fecha, @PasajerosID,@Detalle,@CategoriaID)

			select @ObservacionViajeID = ov.ObservacionViajeId from @tblObservacionViaje ov
			
		commit;
	end try
	begin catch
		IF @@TRANCOUNT > 0 
        ROLLBACK TRAN 

        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + CONVERT(NVARCHAR(10),Error_line()), 
                @errState = Error_state() 

        RAISERROR (N'Error al agregar una observacion del viaje. MSG: %d',16,@errState,1,@errmsg); 

	end catch


END