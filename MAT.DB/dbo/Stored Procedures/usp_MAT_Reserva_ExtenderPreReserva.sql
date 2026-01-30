  CREATE PROCEDURE [dbo].[usp_MAT_Reserva_ExtenderPreReserva](@FacturaID varchar(36),
															  @VendedorID varchar(36),
															  @CantDias int = 0)
  AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 19/05/2017
  -- Description:  Extended pre reserva
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


	BEGIN TRY 
	BEGIN TRAN

	DECLARE @PersonaID varchar(36),
	@Descripcion varchar(150)

	/****************GET DATA************/
	SELECT @PersonaID = f.ClienteID, 
		   @Descripcion = p.Descripcion + ' ' + CONVERT(VARCHAR(10), v.FechaSalida, 103) 
	FROM   Factura f 
	  INNER JOIN Pasaje pje 
	  ON pje.FacturaID = f.FacturaID 
	  INNER JOIN Viaje v 
	  ON pje.ViajeID = v.ViajeID 
	  INNER JOIN Paquete p 
	  ON v.PaqueteID = p.PaqueteID 
	WHERE  f.FacturaID = @FacturaID 
	/***********************************/

	UPDATE dbo.Factura 
	SET    DiasPreReserva = DiasPreReserva + @CantDias 
	WHERE  FacturaID = @FacturaID 

	INSERT INTO AuditFactura 
	(FacturaID, 
	PersonaID, 
	VendedorID, 
	Accion,
	Descripcion) 
	VALUES     ( @FacturaID, 
	@PersonaID, 
	@VendedorID, 
	'Update', 
	'Extender PreReserva - ' + @Descripcion) 

	COMMIT TRAN; 
	SELECT 'Done.' AS Result

	END TRY

	BEGIN CATCH
	IF @@TRANCOUNT > 0 
	ROLLBACK TRAN


	DECLARE @errmsg   AS NVARCHAR (2048)
	SELECT @errmsg = Error_message()

	SELECT @errmsg AS Result
	END CATCH

  END 
  
