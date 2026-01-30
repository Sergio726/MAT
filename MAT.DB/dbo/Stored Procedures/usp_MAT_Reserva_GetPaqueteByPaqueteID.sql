CREATE	 PROCEDURE [dbo].[usp_MAT_Reserva_GetPaqueteByPaqueteID](@PaqueteID VARCHAR(36))
AS 
/*-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 07/05/2017 
-- Description:  get Paqute by PaqueteID
History
06-11-2017	Garcia Sergio add ModePublicity
2025-03-29	Garcia Sergio   remove field ModePublicity and PublicWeb
-- =============================================*/
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;

BEGIN 
  SELECT
			 P.PaqueteID
			,P.Descripcion
			,P.Moneda
			,P.Iva
			,P.Alicuota
			,P.Temporada
			,P.Cotizacion
			,P.Codigo
			,P.DestinoID
			,P.Foto
			,P.FechaCreacion
	FROM Paquete p 
	WHERE p.PaqueteID = @PaqueteID
    
END