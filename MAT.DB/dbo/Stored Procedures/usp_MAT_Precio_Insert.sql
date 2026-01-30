CREATE PROCEDURE [dbo].[usp_MAT_Precio_Insert] (@Monto float(53),
												@Vigencia datetime = null,
												@Descripcion varchar(100),
												@Mes varchar(100) = null,
												@DescripcionVoucher varchar(500) = null
												)
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-12-11
  -- Description: insert new precio
  --History
  
  -- ============================================= */

  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	   DECLARE @errmsg   AS NVARCHAR (2048), 
               @errState INT 

	begin try
		begin tran
			insert into dbo.Precio (Monto,Vigencia,Descripcion,Mes, DescripcionVoucher)
			values (@Monto,@Vigencia,@Descripcion,@Mes, @DescripcionVoucher)

		commit;
		
	end try
	begin catch
		 IF @@TRANCOUNT > 0 
            ROLLBACK TRAN 

          SELECT @errmsg = Error_message() + Error_line(), 
                 @errState = Error_state() 

          RAISERROR (N'Error al crear nuevo precio: %d',16,@errState,1,@errmsg); 
	end catch
			
  END

