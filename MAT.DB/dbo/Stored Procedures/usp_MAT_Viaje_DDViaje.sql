CREATE PROCEDURE [dbo].[usp_MAT_Viaje_DDViaje]
AS
/*-- =============================================   
-- Author:    Garcia Sergio   
-- Create date: 10-01-2017   
-- Description:  get ALL VIAJE
        
-- =============================================*/ 
		SET nocount, xact_abort ON; 
		SET TRANSACTION isolation level READ uncommitted; 
BEGIN
		select v.ViajeID,
				Descripcion = upper(v.Descripcion),
				Paquete = upper(p.Descripcion),
				FechaSalida = REPLACE( CONVERT(varchar(10), v.FechaSalida,103),'-','/')
		from dbo.Viaje v
		inner join dbo.Paquete p
			on p.PaqueteID = v.PaqueteID
		where v.FechaSalida > '2015-01-01'
		ORDER BY v.FechaSalida desc
END

