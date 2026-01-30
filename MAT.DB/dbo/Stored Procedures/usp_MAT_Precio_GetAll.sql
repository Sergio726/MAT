
CREATE PROCEDURE [dbo].[usp_MAT_Precio_GetAll]
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 06/17/2017
  -- Description: get habitacion by HotelId
  --History
  --2017-06-17  Garcia Sergio: created
    2017-06-23	Garcia Sergio: add HabitacionTipo
	2018-11-06	Garcia Sergio:  add Hotel column
  -- ============================================= */

  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		SELECT 
			PrecioID,
			Monto,
			Vigencia = convert(varchar(10),Vigencia,103),
			Descripcion,
			DescripcionVoucher,
			Mes
		FROM dbo.Precio 	   
       
  END

