create PROCEDURE [dbo].[usp_MAT_Servicios_GetAll]
as
-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 2018-03-11
-- Description:  get all servicios

-- =============================================
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;
BEGIN 

	select s.ServicioID,
		   s.Descripcion,
		   Precio = isnull(s.Precio, 0),
		   Moneda = mt.Codigo
	from dbo.Servicio s
	inner join dbo.MonedaTipo mt
		on s.Moneda = mt.Id
	
END
