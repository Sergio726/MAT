
CREATE PROCEDURE [dbo].[usp_MAT_Hotel_CheckNombre] (@Nombre varchar(50),
													@IfExists int OUTPUT )
AS
/*--=============================================   
-- Author:    Garcia Sergio   
-- Create date: 2019-05-14
-- Description: check if exists Hotel Nombre

  
-- =============================================*/ 
BEGIN
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

	begin try
			if exists (select * from dbo.Hotel where Nombre like @Nombre)
			begin
				select @IfExists = 1
			end
			else
				select @IfExists = 0
	end try
	begin catch
		
        DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + Error_line(), 
                @errState = Error_state() 

        RAISERROR (N'Error al agregar comprobar nombre de hotel. MSG: %d',16,@errState,1,@errmsg); 

	end catch


END