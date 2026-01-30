
CREATE PROCEDURE [dbo].[usp_MAT_Habitacion_GetById](@HabitacionID uniqueidentifier)

AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 06/17/2017
  -- Description: get habitacion by Id
  --History
  --2017-06-17 Garcia Sergio: created
  --2018-11-06 Garcia Sergio: add Hotel column
  --2019-05-22 Garcia Sergio: delete link with Hotel
  --2024-09-18 Ruben Tejerina: get precio and descripcion
  -- ============================================= 

  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		SELECT h.habitacionid, 
			   nrohabitacion = isnull(h.nrohabitacion,''), 
			   h.tipo, 
			   h.Estado, 
			   h.Capacidad, 
			   h.Ocupacion, 
			   nombre = isnull(h.nombre,''),
			   HabitacionTipo = ht.Descripcion,
			   HabPrecio = h.Precio,
			   HabDescripcion = h.Descripcion
			   
		FROM   dbo.Habitacion h
		INNER JOIN dbo.HabitacionTipo ht
			on h.Tipo = ht.Id 
		
		WHERE  h.habitacionid = @HabitacionID 

       
  END