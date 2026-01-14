
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the Precio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePrecio_Insert
(

	@PrecioId uniqueidentifier    OUTPUT,

	@Monto float   ,

	@Vigencia datetime   ,

	@Descripcion varchar (100)  ,

	@Mes varchar (100)  
)
AS


				
				INSERT INTO [dbo].[Precio]
					(
					[PrecioID]
					,[Monto]
					,[Vigencia]
					,[Descripcion]
					,[Mes]
					)
				VALUES
					(
					@PrecioId
					,@Monto
					,@Vigencia
					,@Descripcion
					,@Mes
					)