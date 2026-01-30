CREATE PROCEDURE [dbo].[usp_MAT_Reserva_ExtenderPreReserva]
    @FacturaID VARCHAR(36),
    @VendedorID VARCHAR(36),
    @CantDias INT = 0
AS
-- =============================================
-- Author:    Garcia Sergio
-- Create date: 19/05/2017
-- Description: Extiende la pre-reserva de una factura (renovar vencida).
-- Optimizado: aislamiento correcto, SELECT determinista, código más claro.
-- =============================================
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    -- READ COMMITTED (default): evita leer datos no confirmados al armar @Descripcion para AuditFactura.
    -- READ UNCOMMITTED no es adecuado aquí porque luego insertamos esos datos en AuditFactura.

    BEGIN TRY
        BEGIN TRAN;

        DECLARE @PersonaID VARCHAR(36),
                @Descripcion VARCHAR(150);

        -- Una factura puede tener varios pasajes: tomar un único registro de forma determinista.
        SELECT TOP 1
            @PersonaID = f.ClienteID,
            @Descripcion = p.Descripcion + ' ' + CONVERT(VARCHAR(10), v.FechaSalida, 103)
        FROM dbo.Factura f
        INNER JOIN dbo.Pasaje pje ON pje.FacturaID = f.FacturaID
        INNER JOIN dbo.Viaje v ON pje.ViajeID = v.ViajeID
        INNER JOIN dbo.Paquete p ON v.PaqueteID = p.PaqueteID
        WHERE f.FacturaID = @FacturaID
        ORDER BY pje.PasajeID;

        IF @PersonaID IS NULL
        BEGIN
            ROLLBACK TRAN;
            SELECT 'Error: Factura no encontrada o sin pasajes.' AS Result;
            RETURN;
        END

        UPDATE f
        SET f.DiasPreReserva = DATEDIFF(DAY, f.Fecha, GETDATE()) + @CantDias
        FROM dbo.Factura f
        WHERE f.FacturaID = @FacturaID;

        INSERT INTO dbo.AuditFactura (FacturaID, PersonaID, VendedorID, Accion, Descripcion)
        VALUES (@FacturaID, @PersonaID, @VendedorID, 'Update', 'Extender PreReserva - ' + @Descripcion);

        COMMIT TRAN;
        SELECT 'Done.' AS Result;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        DECLARE @errmsg NVARCHAR(2048) = ERROR_MESSAGE();
        SELECT @errmsg AS Result;
    END CATCH
END