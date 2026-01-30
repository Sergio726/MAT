CREATE PROCEDURE usp_MAT_Paquete_GetPaquetes(@DateYear VARCHAR(4) = '')

AS 

/*-- ============================================= 

-- Author:    Garcia Sergio 

-- Create date: 28/03/2017 

-- Description:  get list Paquetes

History

2017-11-06	Garcia Sergio	add ModePublicity
2018-03-10	Garcia Sergio   add MonedaDescripcion
2025-03-29	Garcia Sergio   remove field ModePublicity and PublicWeb

-- =============================================*/

SET nocount, xact_abort ON;

SET TRANSACTION isolation level READ uncommitted;



BEGIN 

  SELECT

			 P.PaqueteID
			,P.Descripcion
			,P.Iva
			,P.Alicuota
			,P.Temporada
			,P.Cotizacion
			,P.Codigo
			,P.DestinoID
			,P.Foto
			,CONVERT(VARCHAR(10), P.FechaCreacion,103) AS FechaCreacion
			,CONVERT(VARCHAR(10), P.LastUpdate,103) AS LastUpdate
			,MonedaCodigo = '( ' + mt.Codigo + ')'

	FROM dbo.Paquete p 
	INNER JOIN dbo.MonedaTipo mt
		on p.Moneda = mt.Id

	WHERE YEAR(p.FechaCreacion) = @DateYear

			OR

		  @DateYear = ''

    

END