CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_DetalleFactura_DeleteById]
(
    @DetalleFacturaId INT,
    @FacturaID UNIQUEIDENTIFIER,
    @VendedorID UNIQUEIDENTIFIER
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-03-29
  -- Description: Elimina una línea de DetalleFactura con validación de pagos,
  --              auditoría en AuditFactura y actualización de estados de reserva.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        DECLARE @ClienteID UNIQUEIDENTIFIER;
        DECLARE @Detalle VARCHAR(300);
        DECLARE @Cantidad INT;
        DECLARE @Precio MONEY;
        DECLARE @AdicionalID UNIQUEIDENTIFIER;

        SELECT
            @ClienteID = f.ClienteID,
            @Detalle = df.Detalle,
            @Cantidad = df.Cantidad,
            @Precio = df.Precio,
            @AdicionalID = df.AdicionalID
        FROM dbo.DetalleFactura df
        INNER JOIN dbo.Factura f ON f.FacturaID = df.FacturaID
        WHERE df.Id = @DetalleFacturaId
          AND df.FacturaID = @FacturaID;

        IF @ClienteID IS NULL
        BEGIN
            ROLLBACK TRAN;
            THROW 50001, N'La línea de factura no existe o no pertenece a esta factura.', 1;
        END;

        DECLARE @TotalNuevo MONEY;
        SELECT @TotalNuevo = SUM(df.Precio * df.Cantidad)
        FROM dbo.DetalleFactura df
        WHERE df.FacturaID = @FacturaID
          AND df.Id <> @DetalleFacturaId;
        SET @TotalNuevo = ISNULL(@TotalNuevo, 0);

        DECLARE @Pagos MONEY;
        SELECT @Pagos = SUM(p.Monto)
        FROM dbo.MovimientoCuenta mc
        INNER JOIN dbo.Pago p ON mc.PagoID = p.PagoID
        WHERE mc.FacturaID = @FacturaID;
        SET @Pagos = ISNULL(@Pagos, 0);

        IF @TotalNuevo < @Pagos
        BEGIN
            ROLLBACK TRAN;
            THROW 50002, N'No se puede eliminar el ítem: el total de la factura quedaría menor que los pagos registrados.', 1;
        END;

        DECLARE @Aid VARCHAR(36) = ISNULL(CONVERT(VARCHAR(36), @AdicionalID), '-');
        DECLARE @Base VARCHAR(250) =
              'Id=' + CAST(@DetalleFacturaId AS VARCHAR(11))
            + ' Q=' + CAST(@Cantidad AS VARCHAR(10))
            + ' $' + CONVERT(VARCHAR(32), @Precio, 1)
            + ' A=' + @Aid
            + ' ';
        DECLARE @AuditDesc VARCHAR(200) = LEFT(@Base + ISNULL(@Detalle, ''), 200);

        INSERT INTO dbo.AuditFactura (FacturaID, PersonaID, VendedorID, Accion, Descripcion)
        VALUES (@FacturaID, @ClienteID, @VendedorID, 'DELETE DetalleFactura', @AuditDesc);

        DELETE FROM dbo.DetalleFactura
        WHERE Id = @DetalleFacturaId
          AND FacturaID = @FacturaID;

        EXEC dbo.usp_MAT_Reserva_ActualizarEstados @FacturaID = @FacturaID;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        DECLARE @ErrMsg NVARCHAR(2048) = ERROR_MESSAGE();
        DECLARE @ErrSev INT = ERROR_SEVERITY();
        DECLARE @ErrState INT = ERROR_STATE();
        RAISERROR (@ErrMsg, @ErrSev, @ErrState);
    END CATCH
END;
