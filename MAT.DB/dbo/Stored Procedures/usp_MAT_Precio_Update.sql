  CREATE PROCEDURE [dbo].[usp_MAT_Precio_Update]  (@PrecioID uniqueidentifier,
												 @Monto float(53),
												 @Vigencia datetime = null,
												 @Descripcion varchar(100),
												 @Mes varchar(100) = null,
												 @DescripcionVoucher varchar(500) = null
												 )
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-12-11
  -- Description: update precio
  --History
  
  -- ============================================= */

  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	   DECLARE @errmsg   AS NVARCHAR (2048), 
               @errState INT 

	begin try
		begin tran
			update dbo.Precio
			set Monto = @Monto,
				Vigencia = @Vigencia,
				Descripcion = @Descripcion,
				Mes = @Mes,
				DescripcionVoucher = @DescripcionVoucher
			where PrecioID = @PrecioID

		commit;
		
	end try
	begin catch
		 IF @@TRANCOUNT > 0 
            ROLLBACK TRAN 

          SELECT @errmsg = Error_message() + Error_line(), 
                 @errState = Error_state() 

          RAISERROR (N'Error al actualizar precio: %d',16,@errState,1,@errmsg); 
	end catch
			
  END

