
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Butaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureButaca_Update]
(

	@ButacaId uniqueidentifier   ,

	@OriginalButacaId uniqueidentifier   ,

	@NroButaca int   ,

	@Piso int   ,

	@Ubicacion int   ,

	@Tipo int   ,

	@TransporteId uniqueidentifier   ,

	@Fila varchar (2)  ,

	@Posicion varchar (1)  ,

	@CodigoButaca varchar (4)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Butaca]
				SET
					[ButacaID] = @ButacaId
					,[NroButaca] = @NroButaca
					,[Piso] = @Piso
					,[Ubicacion] = @Ubicacion
					,[Tipo] = @Tipo
					,[TransporteID] = @TransporteId
					,[Fila] = @Fila
					,[Posicion] = @Posicion
					,[CodigoButaca] = @CodigoButaca
				WHERE
[ButacaID] = @OriginalButacaId 
				
			



