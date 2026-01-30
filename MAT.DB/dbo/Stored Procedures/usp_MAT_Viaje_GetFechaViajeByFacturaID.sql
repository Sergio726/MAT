CREATE PROCEDURE [dbo].[usp_MAT_Viaje_GetFechaViajeByFacturaID](@FacturaID UniqueIdentifier)
AS
/*-- =============================================   
-- Author:    Garcia Sergio   
-- Create date: 10-04-2017   
-- Description:  get fechasalida by FacturaID
        
-- =============================================*/ 
		SET nocount, xact_abort ON; 
		SET TRANSACTION isolation level READ uncommitted; 
begin
	select distinct v.FechaSalida
	from dbo.Factura f
	inner join dbo.Pasaje ps
		on f.FacturaID = ps.FacturaID
	inner join dbo.Viaje v
		on v.ViajeID = ps.ViajeID
	where f.FacturaID = @FacturaID
end

