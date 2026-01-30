CREATE PROCEDURE [dbo].[usp_MAT_Viaje_GetViajesByDateYear](@DateYear VARCHAR(4) = null) 
AS 
 -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 04/29/2017
  -- Description:  get all viaje by date year
  -- 2025/03/27	Garcia Sergio: add MonedaTipo, IsPublicWeb
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		
		SELECT   v.Viajeid, 
				 v.Paqueteid, 
				 p.Descripcion AS PaqueteNombre,
				 v.Origen, 
				 CONVERT(VARCHAR(10), v.Fechasalida, 103) AS FechaSalida,
				 CONVERT(VARCHAR(10), v.Horasalida,  103) AS HoraSalida,
				 v.Paisorigen, 
				 v.Paisdestino, 
				 v.Paso, 
				 v.Medio, 
				 v.Busid, 
				 CONVERT(VARCHAR(10),v.Fecharegreso, 103) AS FechaRegreso,
				 v.Horaregreso, 
				 v.Descripcion, 
				 v.MonedaTipo,
				 v.Preciosemicama, 
				 v.Preciocama, 
				 v.Preciopromocional, 
				 CONVERT(VARCHAR(10),v.Fechapromocion, 103) AS FechaPromocion,
				 v.Ndias, 
				 v.Nnoches,
				 v.IsPublicWeb
		  FROM   dbo.VIAJE v 
		  LEFT JOIN dbo.Paquete p
				ON v.PaqueteID = p.PaqueteID
		  WHERE  Year(v.Fechasalida) = @DateYear
				 OR @DateYear IS NULL
		  
END