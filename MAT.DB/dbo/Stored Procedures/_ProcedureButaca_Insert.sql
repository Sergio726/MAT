
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Butaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureButaca_Insert]
(

	@ButacaId uniqueidentifier    OUTPUT,

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


				
				INSERT INTO [dbo].[Butaca]
					(
					[ButacaID]
					,[NroButaca]
					,[Piso]
					,[Ubicacion]
					,[Tipo]
					,[TransporteID]
					,[Fila]
					,[Posicion]
					,[CodigoButaca]
					)
				VALUES
					(
					@ButacaId
					,@NroButaca
					,@Piso
					,@Ubicacion
					,@Tipo
					,@TransporteId
					,@Fila
					,@Posicion
					,@CodigoButaca
					)
				
									
							
			



