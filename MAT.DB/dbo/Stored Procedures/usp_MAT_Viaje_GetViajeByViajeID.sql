CREATE PROCEDURE usp_MAT_Viaje_GetViajeByViajeID(@ViajeID VARCHAR(36)) 
AS 
 /* ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 02/05/2017
  -- Description:  get Viaje  by ViajeID

  2019/04/17  Garcia Sergio: add TiempoConsentracion
  2019/05/04  Garcia Sergio: add Observaciones
  2025/03/27  Garcia Sergio: add Moneda Tipo
  2025/03/31  Garcia Sergio: add IsPublicWeb
  -- ============================================= */
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
				 ISNULL(v.TiempoConsentracion,30) AS TiempoConsentracion,
				 v.Observaciones,
				 v.IsPublicWeb
		  FROM   dbo.Viaje v 
		  LEFT JOIN dbo.Paquete p
				ON v.PaqueteID = p.PaqueteID
		  WHERE  v.ViajeID = @ViajeID
		  
END