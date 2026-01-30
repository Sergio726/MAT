
CREATE PROCEDURE [dbo].[usp_MAT_Transporte_GetListTransporte]( @TransporteID VARCHAR(36) = '')
AS 
-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 05/21/2017
-- Description:  get list transporte
-- =============================================
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;

BEGIN 
	SELECT t.TransporteID, 
		   t.NroCoche, 
		   t.MaxPasajeros, 
		   t.KmRecorridos, 
		   t.UltimoService, 
		   t.Matricula 
	FROM   dbo.Transporte t 
	WHERE  (CONVERT(VARCHAR(36), t.TransporteID) = @TransporteID ) 
			OR ( @TransporteID = '' ) 
    
END

