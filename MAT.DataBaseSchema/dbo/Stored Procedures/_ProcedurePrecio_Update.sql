
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Precio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePrecio_Update
(

	@PrecioId uniqueidentifier   ,

	@OriginalPrecioId uniqueidentifier   ,

	@Monto float   ,

	@Vigencia datetime   ,

	@Descripcion varchar (100)  ,

	@Mes varchar (100)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Precio]
				SET
					[PrecioID] = @PrecioId
					,[Monto] = @Monto
					,[Vigencia] = @Vigencia
					,[Descripcion] = @Descripcion
					,[Mes] = @Mes
				WHERE
[PrecioID] = @OriginalPrecioId