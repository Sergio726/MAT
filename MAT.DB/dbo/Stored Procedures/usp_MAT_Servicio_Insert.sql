CREATE PROCEDURE [dbo].[usp_MAT_Servicio_Insert](
												@Descripcion varchar(250),
												@Precio	float = null,
												@Moneda	varchar(50)= null,
												@Iva	varchar(50)= null,
												@Alicuota	float = null,
												@Validez	date = null,
												@VisibilidadTarifa	int = null,
												@ProveedorID	uniqueidentifier = null,
												@TransporteID	uniqueidentifier = null,
												@HotelID	uniqueidentifier = null,
												@TipoServicio	int = null)

AS 

  -- ============================================= 

  -- Author:    Garcia Sergio 
  -- Create date: 09-09-2017
  -- Description:  insert new servicio
  --History
  --09-09-2017 Garcia Sergio: create servicio
  --04-07-2023 Garcia Sergio: @Descripcion varchar(250)
  -- ============================================= 

  BEGIN 

      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

        BEGIN TRY 
            BEGIN TRAN
         
			insert into dbo.Servicio(Descripcion,
									 Precio,
									 Moneda,
									 Iva,
									 Alicuota,
									 Validez,
									 VisibilidadTarifa,
									 ProveedorID,
									 TransporteID,
									 HotelID,
									 TipoServicio)
			values(
					@Descripcion,
					@Precio,
					@Moneda,
					@Iva,
					@Alicuota,
					@Validez,
					@VisibilidadTarifa,
					@ProveedorID,
					@TransporteID,
					@HotelID,
					@TipoServicio
				)
			
			commit
        END TRY
        
		BEGIN CATCH
		IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048),
					@errState int
			select  @errmsg = Error_message() + ERROR_LINE(), @errState = ERROR_STATE()
					RAISERROR (@errmsg,16,@errState);  

            

        END CATCH


  END