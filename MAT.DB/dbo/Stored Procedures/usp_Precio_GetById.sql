CREATE PROCEDURE [dbo].[usp_Precio_GetById] (@PrecioID UNIQUEIDENTIFIER)
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-12-10
  -- Description: get precio by Id
  
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
		WHERE PrecioID = @PrecioID	   
       
  END