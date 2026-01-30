CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetAdicionalesByViajeId]
(
	@ViajeID UNIQUEIDENTIFIER
)
AS
 /*-- ============================================= 
  -- Author: Ruben Tejerina 
  -- Create date: 18/10/2024
  -- Description: Get Adicionales by ViajeId
	 This SP is called by API BACKEND

  -- ============================================= */
BEGIN
	SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted;	

	select 
		a.AdicionalID, 
		a.Monto, 
		a.Descripcion,
		case when a.Descripcion like 'SEGURO MENOR%' 
			then 1 
			else 0 
		end as IsMenor

	from dbo.Viaje v
	inner join dbo.PaqueteAdicional pa on pa.PaqueteID = v.PaqueteID
	inner join dbo.Adicional a on a.AdicionalID = pa.AdicionalID
	where 
		v.ViajeID = @ViajeId
END