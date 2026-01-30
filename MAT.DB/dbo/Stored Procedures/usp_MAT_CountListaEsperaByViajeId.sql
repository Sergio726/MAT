
CREATE PROCEDURE [dbo].[usp_MAT_CountListaEsperaByViajeId] (@ViajeID UNIQUEIDENTIFIER)
AS

/*-- =============================================  
  -- Author:    Garcia Sergio  
  -- Create date: 18/12/2023
  -- Description:  COUNT RECORDS LISTA ESPERA BY VIAJE ID
  --History 
  18/12/2023  Garcia Sergio: create store procedure
  -- ============================================= */ 
 BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		SELECT CountListaEspera = count(*)
		FROM dbo.ListaEspera
		WHERE ViajeID = @ViajeID
END