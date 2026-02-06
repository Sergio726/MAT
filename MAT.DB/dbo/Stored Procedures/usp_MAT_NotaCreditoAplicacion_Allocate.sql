CREATE PROCEDURE [dbo].[usp_MAT_NotaCreditoAplicacion_Allocate]
    @ClienteID     UNIQUEIDENTIFIER,
    @PagoID        UNIQUEIDENTIFIER,
    @FacturaID     UNIQUEIDENTIFIER,
    @Monto         MONEY
AS
/*
=============================================
Author:     Seba Garcia
Create date: 2026-02
Description: Reparte @Monto entre las notas de crédito del cliente (FIFO)
             e inserta en NotaCreditoAplicacion.
             @Monto debe ser positivo (monto usado en el pago).
=============================================
*/
SET NOCOUNT, XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN
    IF @Monto IS NULL OR @Monto <= 0
        RETURN;

    DECLARE @Resto          MONEY = @Monto,
            @NotaID         UNIQUEIDENTIFIER,
            @SaldoNota      MONEY,
            @Aplicar        MONEY;

    WHILE @Resto > 0
    BEGIN
        ;WITH NotasConSaldo AS (
            SELECT n.NotaID,
                   n.Fecha,
                   n.MontoNota - ISNULL(
                       (SELECT SUM(a.MontoAplicado)
                        FROM dbo.NotaCreditoAplicacion a
                        WHERE a.NotaCreditoID = n.NotaID), 0) AS SaldoDisponible
            FROM dbo.Nota n
            INNER JOIN dbo.CreditoCliente cc
                ON cc.NotaCreditoID = n.NotaID
               AND cc.ClienteID = @ClienteID
               AND cc.IsInput = 1
            WHERE n.ClienteID = @ClienteID
        )
        SELECT TOP 1
            @NotaID    = NotaID,
            @SaldoNota = SaldoDisponible
        FROM NotasConSaldo
        WHERE SaldoDisponible > 0
        ORDER BY Fecha ASC;

        IF @NotaID IS NULL
            BREAK;

        SET @Aplicar = CASE WHEN @SaldoNota < @Resto THEN @SaldoNota ELSE @Resto END;

        INSERT INTO dbo.NotaCreditoAplicacion (NotaCreditoID, PagoID, FacturaID, MontoAplicado)
        VALUES (@NotaID, @PagoID, @FacturaID, @Aplicar);

        SET @Resto = @Resto - @Aplicar;
    END
END
GO
