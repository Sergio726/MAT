CREATE PROCEDURE usp_MAT_Nota_IsertNewNota(@PorcentajeRetencion money = NULL, 
                                           @MontoRetencion      money = NULL, 
                                           @Dias                INT = NULL, 
                                           @ClienteID           UNIQUEIDENTIFIER, 
                                           @VendedorID UNIQUEIDENTIFIER, 
                                           @NroNota             VARCHAR(50), 
                                           @MontoNota           money,
										   @FacturaID UNIQUEIDENTIFIER,
										   @Detalle varchar(1000),
										   @MontoDevolucion     money = NULL)
AS 
  /*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 09-30-2017   
  -- Description:  insert nota credito 
     2018-03-07		Garcia Sergio add @Detalle
	 2018-06-02		Garcia Sergio: add Moneda Tipo, default Argentinos
	 2026			add @MontoDevolucion: movimiento negativo en Pago (devolución por nota de crédito)
  -- =============================================*/ 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

      BEGIN try 
	
		 declare @NotaID UNIQUEIDENTIFIER
		 SET @MontoDevolucion = ISNULL(@MontoDevolucion, 0)

		 begin tran 
		 set @NotaID = NEWID()
          INSERT INTO [Nota] 
                      (NotaID,
					   PorcentajeRetencion, 
                       MontoRetencion, 
                       Dias, 
                       ClienteID, 
                       VendedorID, 
                       NroNota, 
                       MontoNota,
					   MontoDevolucion,
					   Detalle) 
          VALUES      (@NotaID,
					   @PorcentajeRetencion, 
                       @MontoRetencion, 
                       @Dias, 
                       @ClienteID, 
                       @VendedorID, 
                       @NroNota, 
                       @MontoNota,
					   @MontoDevolucion,
					   @Detalle)

		 
		

		/*insert in tbl CreditoCliente*/
		DECLARE @NroFactura varchar(50)
		select @NroFactura = isnull(f.NroFactura,'') from dbo.Factura f where f.FacturaID = @FacturaID

		insert into dbo.CreditoCliente (NotaCreditoID,VendedorID,ClienteID,Monto,Descripcion,IsInput)
								values (@NotaID,@VendedorID,@ClienteID,@MontoNota,'NOTA DE CREDITO POR FACTURA ' + @NroFactura,1)

		/* Movimiento negativo en Pago: devolución por nota de crédito */
		IF @MontoDevolucion > 0
		BEGIN
			DECLARE @PagoIDDevolucion UNIQUEIDENTIFIER = NEWID()
			DECLARE @MovimientoIDDevolucion UNIQUEIDENTIFIER = NEWID()
			DECLARE @CuentaID UNIQUEIDENTIFIER
			DECLARE @TipoPagoDevolucion INT

			SELECT @TipoPagoDevolucion = Id FROM dbo.PagoTipo WHERE Descripcion = 'Devolución'
			IF @TipoPagoDevolucion IS NOT NULL
			BEGIN
				SELECT @CuentaID = c.CuentaID FROM dbo.Cuenta c WHERE c.ClienteID = @ClienteID

				INSERT INTO dbo.Pago (PagoID, FechaPago, Monto, NroRecibo, TipoPago, VendedorId)
				VALUES (@PagoIDDevolucion, GETDATE(), -@MontoDevolucion,
					'Devolución por nota de crédito N° ' + @NroNota,
					@TipoPagoDevolucion, @VendedorID)

				INSERT INTO dbo.MovimientoCuenta (MovimientoID, CuentaID, PagoID, FacturaID, FechaRegistro)
				VALUES (@MovimientoIDDevolucion, @CuentaID, @PagoIDDevolucion, @FacturaID, GETDATE())
			END
		END

		/*------------insert nota in movimientos------------*/
			if exists(SELECT * FROM   dbo.MovimientoCuenta mc WHERE  mc.FacturaID = @FacturaID )
			begin
				UPDATE mc 
				SET    mc.NotaID = @NotaID 
				FROM   dbo.MovimientoCuenta mc 
				WHERE  mc.FacturaID = @FacturaID 
			end		

		commit

		begin tran
		--CLEAR PASAJES
			IF EXISTS(SELECT * FROM Pasaje WHERE FacturaID = @FacturaID)
			BEGIN 
				UPDATE p
				SET p.PasajeroID = null,
					p.FechaReserva = null,
					p.FechaCompra = null,
					p.FacturaID = null,
					p.EstadoPasaje = 1, --disponible
					p.VoucherID = null
				FROM dbo.Pasaje p
				INNER JOIN dbo.Factura f
					ON p.FacturaID = f.FacturaID
					AND p.FacturaID = @FacturaID
			END

			--CHECK AND DELETE VOUCHER
			IF EXISTS(
				SELECT * 
				FROM Voucher v
				left join Pasaje p
					on v.VoucherID = p.VoucherID
				where p.VoucherID is null
			)
			BEGIN 
				DELETE v
				FROM   Voucher v 
					   LEFT JOIN Pasaje p 
							  ON v.VoucherID = p.VoucherID 
				WHERE  p.VoucherID IS NULL 
			END

			------Update EstadoFactuta-------------
			update f
			set f.Estado = 6	--Nota de Credito
			from dbo.Factura f
			where f.FacturaID = @FacturaID

		commit

      END try 

      BEGIN catch 
          IF @@TRANCOUNT > 0 
            ROLLBACK TRAN 

          DECLARE @errmsg   AS NVARCHAR (2048), 
                  @errState INT 

          SELECT @errmsg = Error_message() + Error_line(), 
                 @errState = Error_state() 

          RAISERROR (N'Error al agregar nueva nota de credito: %d',16,@errState,1,@errmsg); 
      END catch 
  END 
