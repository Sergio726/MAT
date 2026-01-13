/*
----------------------------------------------------------------------------------------------------
-- Created By: MAT System
-- Purpose: Elimina un pasajero de una factura y actualiza el monto total de la factura
-- Parámetros:
--   @PasajeID: ID del pasaje a eliminar
--   @FacturaID: ID de la factura a actualizar
-- Retorna: Result (Done. o mensaje de error)
----------------------------------------------------------------------------------------------------
*/

USE [MAT.Intranet]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS OFF
GO

-- Drop procedure if exists
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.usp_MAT_Factura_EliminarPasajero') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.usp_MAT_Factura_EliminarPasajero
GO

CREATE PROCEDURE dbo.usp_MAT_Factura_EliminarPasajero
(
    @PasajeID UNIQUEIDENTIFIER,
    @FacturaID UNIQUEIDENTIFIER,
    @Result VARCHAR(100) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ErrorMsg VARCHAR(500) = '';
    DECLARE @MontoPasajeEliminar FLOAT = 0;
    DECLARE @ButacaID UNIQUEIDENTIFIER = NULL;
    DECLARE @PasajeroID UNIQUEIDENTIFIER = NULL;
    DECLARE @MontoFacturaActual FLOAT = 0;
    DECLARE @MontoFacturaNuevo FLOAT = 0;
    DECLARE @TotalPagos FLOAT = 0;
    DECLARE @SaldoNuevo FLOAT = 0;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- 1. Obtener información del pasaje antes de eliminarlo
        SELECT 
            @ButacaID = ButacaID,
            @PasajeroID = PasajeroID
        FROM Pasaje
        WHERE PasajeID = @PasajeID AND FacturaID = @FacturaID;
        
        IF @ButacaID IS NULL
        BEGIN
            SET @Result = 'Error: Pasaje no encontrado o no pertenece a esta factura.';
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        -- 2. Calcular el monto del pasaje a eliminar (Precio + Adicionales)
        -- Obtener precio del pasaje
        SELECT @MontoPasajeEliminar = ISNULL(p.Monto, 0)
        FROM Pasaje pa
        INNER JOIN Precio p ON pa.PrecioID = p.PrecioID
        WHERE pa.PasajeID = @PasajeID;
        
        -- Sumar adicionales del pasaje
        SELECT @MontoPasajeEliminar = @MontoPasajeEliminar + ISNULL(SUM(a.Monto), 0)
        FROM PasajeAdicional pa
        INNER JOIN Adicional a ON pa.AdicionalID = a.AdicionalID
        WHERE pa.PasajeID = @PasajeID;
        
        -- 3. Eliminar adicionales del pasaje
        DELETE FROM PasajeAdicional
        WHERE PasajeID = @PasajeID;
        
        -- 4. Eliminar vínculo de pasajero menor si existe
        -- Nota: La eliminación de vínculos de menores se maneja a través del procedimiento
        -- usp_MAT_PersonaCliente_DesvincularMenor que se puede llamar desde C#
        -- Por ahora, eliminamos directamente si existe una tabla PasajeroMenor
        -- Si no existe, se manejará desde el código C#
        
        -- 5. Liberar habitación si tiene
        DECLARE @ReservaHabitacionID UNIQUEIDENTIFIER = NULL;
        SELECT @ReservaHabitacionID = ReservaHabitacionID
        FROM ReservaHabitacion
        WHERE PasajeID = @PasajeID;
        
        IF @ReservaHabitacionID IS NOT NULL
        BEGIN
            DELETE FROM ReservaHabitacion
            WHERE PasajeID = @PasajeID;
        END
        
        -- 6. Eliminar el pasaje
        -- Nota: Al eliminar el pasaje, la butaca queda automáticamente libre
        -- porque ya no hay un registro en Pasaje que la referencie (ButacaID)
        DELETE FROM Pasaje
        WHERE PasajeID = @PasajeID;
        
        -- 7. Recalcular el monto total de la factura
        -- Sumar precios de todos los pasajes restantes
        SELECT @MontoFacturaNuevo = ISNULL(SUM(ISNULL(p.Monto, 0)), 0)
        FROM Pasaje pa
        LEFT JOIN Precio p ON pa.PrecioID = p.PrecioID
        WHERE pa.FacturaID = @FacturaID;
        
        -- Sumar adicionales de todos los pasajes restantes
        SELECT @MontoFacturaNuevo = @MontoFacturaNuevo + ISNULL(SUM(ISNULL(a.Monto, 0)), 0)
        FROM Pasaje pa
        INNER JOIN PasajeAdicional paa ON pa.PasajeID = paa.PasajeID
        INNER JOIN Adicional a ON paa.AdicionalID = a.AdicionalID
        WHERE pa.FacturaID = @FacturaID;
        
        -- 8. Obtener monto actual de la factura
        SELECT @MontoFacturaActual = ISNULL(Monto, 0)
        FROM Factura
        WHERE FacturaID = @FacturaID;
        
        -- 9. Obtener total de pagos realizados (a través de MovimientoCuenta)
        SELECT @TotalPagos = ISNULL(SUM(ISNULL(p.Monto, 0)), 0)
        FROM MovimientoCuenta mc
        INNER JOIN Pago p ON mc.PagoID = p.PagoID
        WHERE mc.FacturaID = @FacturaID;
        
        -- 10. Calcular nuevo saldo
        SET @SaldoNuevo = @MontoFacturaNuevo - @TotalPagos;
        
        -- 11. Actualizar monto de la factura
        UPDATE Factura
        SET Monto = @MontoFacturaNuevo
        WHERE FacturaID = @FacturaID;
        
        COMMIT TRANSACTION;
        SET @Result = 'Done.';
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        SET @ErrorMsg = ERROR_MESSAGE();
        SET @Result = 'Error: ' + @ErrorMsg;
    END CATCH
END
GO

SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

