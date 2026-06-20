/*
  MAT — Migración consolidada (2026-06-19)
  Impedir registrar el mismo pasajero en dos viajes distintos con la misma FechaSalida.

  Ejecutar en: BD de negocio (MAT.Data.ConnectionString), p. ej. MAT.Intranet
  Idempotente: CREATE OR ALTER en función y SPs.
  Prerequisito: dbo.Split (script 2026-04-04_dbo_Split.sql) si aún no existe.
*/

SET NOCOUNT ON;
GO

/* =============================================================================
   1) Función central de conflicto
   ============================================================================= */
CREATE OR ALTER FUNCTION [dbo].[fn_MAT_Pasaje_TieneConflictoFechaSalida]
(
    @PasajeroID UNIQUEIDENTIFIER,
    @ViajeID UNIQUEIDENTIFIER,
    @ExcluirPasajeID UNIQUEIDENTIFIER = NULL
)
RETURNS BIT
AS
BEGIN
    IF @PasajeroID IS NULL OR @ViajeID IS NULL
        RETURN 0;

    DECLARE @FechaSalida DATE;

    SELECT @FechaSalida = v.FechaSalida
    FROM dbo.Viaje v
    WHERE v.ViajeID = @ViajeID;

    IF @FechaSalida IS NULL
        RETURN 0;

    IF EXISTS (
        SELECT 1
        FROM dbo.Pasaje pa
        INNER JOIN dbo.Viaje v ON v.ViajeID = pa.ViajeID
        WHERE pa.PasajeroID = @PasajeroID
          AND ISNULL(pa.EstadoPasaje, 0) NOT IN (1, 7)  /* Disponible, Anulado */
          AND v.FechaSalida = @FechaSalida
          AND (@ExcluirPasajeID IS NULL OR pa.PasajeID <> @ExcluirPasajeID)
    )
        RETURN 1;

    RETURN 0;
END
GO

/* =============================================================================
   2) Autocomplete / búsqueda de clientes para reserva
   ============================================================================= */
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetTop]
(
    @SearchTerm NVARCHAR(200) = '',
    @TopCount INT = 12,
    @ViajeID UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET @SearchTerm = LTRIM(RTRIM(ISNULL(@SearchTerm, '')));
    SET @SearchTerm = REPLACE(REPLACE(REPLACE(@SearchTerm, ',', ' '), '-', ' '), N'–', ' ');
    WHILE CHARINDEX('  ', @SearchTerm) > 0
        SET @SearchTerm = REPLACE(@SearchTerm, '  ', ' ');
    SET @SearchTerm = LTRIM(RTRIM(@SearchTerm));

    IF @TopCount <= 0 OR @TopCount > 100
        SET @TopCount = 12;

    DECLARE @SoloDisponiblesParaViaje BIT = CASE WHEN @ViajeID IS NOT NULL THEN 1 ELSE 0 END;

    IF LEN(@SearchTerm) = 0
    BEGIN
        SELECT TOP (@TopCount)
            p.PersonaID,
            p.Apellido,
            p.Nombre,
            p.NroDocumento,
            p.Telefono,
            p.Celular,
            ISNULL(p.TipoDocumento, 1) AS TipoDocumento,
            ISNULL(p.Email, '') AS Email,
            LocalidadNombre = ISNULL(l.Nombre, ''),
            p.Nacionalidad,
            p.PaisResidencia,
            IsTituarFactura = CASE
                WHEN EXISTS (SELECT 1 FROM dbo.Factura f WITH (NOLOCK) WHERE f.ClienteID = p.PersonaID) THEN 1
                ELSE 0
            END
        FROM dbo.Persona p WITH (NOLOCK)
        INNER JOIN dbo.Cliente c WITH (NOLOCK) ON c.ClienteID = p.PersonaID
        LEFT JOIN dbo.Localidad l WITH (NOLOCK) ON p.LocalidadID = l.ID
        WHERE (@SoloDisponiblesParaViaje = 0 OR dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(p.PersonaID, @ViajeID, NULL) = 0)
        ORDER BY p.Apellido ASC, p.Nombre ASC;
        RETURN;
    END

    SELECT TOP (@TopCount)
        p.PersonaID,
        p.Apellido,
        p.Nombre,
        p.NroDocumento,
        p.Telefono,
        p.Celular,
        ISNULL(p.TipoDocumento, 1) AS TipoDocumento,
        ISNULL(p.Email, '') AS Email,
        LocalidadNombre = ISNULL(l.Nombre, ''),
        p.Nacionalidad,
        p.PaisResidencia,
        IsTituarFactura = CASE
            WHEN EXISTS (SELECT 1 FROM dbo.Factura f WITH (NOLOCK) WHERE f.ClienteID = p.PersonaID) THEN 1
            ELSE 0
        END
    FROM dbo.Persona p WITH (NOLOCK)
    INNER JOIN dbo.Cliente c WITH (NOLOCK) ON c.ClienteID = p.PersonaID
    LEFT JOIN dbo.Localidad l WITH (NOLOCK) ON p.LocalidadID = l.ID
    WHERE (@SoloDisponiblesParaViaje = 0 OR dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(p.PersonaID, @ViajeID, NULL) = 0)
      AND NOT EXISTS (
            SELECT 1
            FROM dbo.Split(CAST(@SearchTerm AS VARCHAR(200)), ' ') AS s
            WHERE LTRIM(RTRIM(s.Item)) <> ''
              AND ISNULL(p.Apellido, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
              AND ISNULL(p.Nombre, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
              AND ISNULL(p.NroDocumento, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
              AND ISNULL(p.Telefono, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
              AND ISNULL(p.Celular, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
              AND ISNULL(p.Email, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        )
    ORDER BY p.Apellido ASC, p.Nombre ASC;
END
GO

/* =============================================================================
   3) Grilla de clientes disponibles (modal SeleccionarPasajero)
   ============================================================================= */
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Reserva_GetClientesDisponibles]
(
    @ViajeID VARCHAR(MAX) = ''
)
AS
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (@ViajeID != '')
    BEGIN
        DECLARE @ViajeGuid UNIQUEIDENTIFIER = TRY_CAST(@ViajeID AS UNIQUEIDENTIFIER);

        IF @ViajeGuid IS NULL
            RETURN;

        SELECT c.ClienteID,
               RTRIM(LTRIM(p.Apellido)) AS Apellido,
               p.Nombre,
               ISNULL(p.TipoDocumento, 1) AS TipoDocumento,
               p.NroDocumento,
               ISNULL(p.Telefono, '') AS Telefono,
               ISNULL(p.Email, '') AS Email
        FROM dbo.Cliente c
        INNER JOIN dbo.Persona p ON p.PersonaID = c.ClienteID
        WHERE dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(c.ClienteID, @ViajeGuid, NULL) = 0;
    END
END
GO

/* =============================================================================
   4) Lista de espera — búsqueda de cliente
   ============================================================================= */
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_ListaEspera_SearchCliente]
(
    @param   VARCHAR(50) = NULL,
    @ViajeID UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @countParam INT = 1;
    DECLARE @tmp TABLE (Value VARCHAR(100));

    SET @param = REPLACE(@param, '.', '');

    INSERT INTO @tmp
    SELECT s.Item
    FROM dbo.Split(@param, ' ') s;

    SELECT @countParam = COUNT(*) FROM @tmp;

    SELECT DISTINCT
           p.PersonaID,
           p.Apellido,
           p.Nombre,
           p.NroDocumento
    FROM dbo.Persona p
    INNER JOIN @tmp t ON p.FullName LIKE '%' + t.Value + '%'
                      OR p.NroDocumentoCalc LIKE '' + t.Value + '%'
    WHERE dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(p.PersonaID, @ViajeID, NULL) = 0
    GROUP BY p.PersonaID, p.Apellido, p.Nombre, p.NroDocumento
    HAVING (COUNT(p.PersonaID) > 1 AND @countParam > 1)
        OR (COUNT(p.PersonaID) > 0 AND @countParam = 1);
END
GO

/* =============================================================================
   5) Confirmar reserva (flujo clásico)
   ============================================================================= */
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher]
(
    @PasajeID UNIQUEIDENTIFIER,
    @FacturaID UNIQUEIDENTIFIER,
    @PasajeroID UNIQUEIDENTIFIER,
    @AdicionalesIDs VARCHAR(MAX),
    @EstadoFactura INT,
    @VendedorID UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    BEGIN TRY
        BEGIN TRAN;

        DECLARE @ViajeID UNIQUEIDENTIFIER;

        SELECT @ViajeID = p.ViajeID
        FROM dbo.Pasaje p
        WHERE p.PasajeID = @PasajeID;

        IF dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(@PasajeroID, @ViajeID, @PasajeID) = 1
        BEGIN
            RAISERROR('El pasajero ya está registrado en otro viaje con la misma fecha de salida.', 16, 1);
            RETURN;
        END

        UPDATE dbo.Pasaje
        SET FacturaID = @FacturaID,
            PasajeroID = @PasajeroID,
            EstadoPasaje = @EstadoFactura
        WHERE PasajeID = @PasajeID;

        IF (@AdicionalesIDs != '')
        BEGIN
            INSERT INTO dbo.PasajeAdicional (pasajeadicionalid, pasajeid, adicionalid)
            SELECT NEWID(), @PasajeID, item
            FROM dbo.Split(@AdicionalesIDs, ',');
        END

        IF (@EstadoFactura = 4)
        BEGIN
            DECLARE @VoucherID UNIQUEIDENTIFIER = NEWID();

            INSERT INTO dbo.Voucher (VoucherID, FechaEmision, VendedorID)
            VALUES (@VoucherID, GETDATE(), @VendedorID);

            UPDATE dbo.Pasaje
            SET voucherid = @VoucherID
            WHERE pasajeid = @PasajeID;
        END

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        DECLARE @errmsg NVARCHAR(2048), @errState INT;
        SELECT @errmsg = ERROR_MESSAGE() + CAST(ERROR_LINE() AS VARCHAR(10)), @errState = ERROR_STATE();
        RAISERROR(@errmsg, 16, @errState);
    END CATCH
END
GO

/* =============================================================================
   6) Cambiar pasajero de butaca
   ============================================================================= */
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Pasaje_CambiarPasajero]
(
    @PasajeID UNIQUEIDENTIFIER,
    @NuevoPasajeroID UNIQUEIDENTIFIER,
    @Result VARCHAR(100) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Pasaje WHERE PasajeID = @PasajeID)
        BEGIN
            SET @Result = 'Error: El pasaje especificado no existe.';
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM dbo.Persona WHERE PersonaID = @NuevoPasajeroID)
        BEGIN
            SET @Result = 'Error: El pasajero especificado no existe.';
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM dbo.Pasajero WHERE PasajeroID = @NuevoPasajeroID)
        BEGIN
            SET @Result = 'Error: El pasajero especificado no tiene registro en la tabla Pasajero.';
            RETURN;
        END

        DECLARE @ViajeID UNIQUEIDENTIFIER;

        SELECT @ViajeID = p.ViajeID
        FROM dbo.Pasaje p
        WHERE p.PasajeID = @PasajeID;

        IF dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(@NuevoPasajeroID, @ViajeID, @PasajeID) = 1
        BEGIN
            SET @Result = 'Error: El pasajero ya está registrado en otro viaje con la misma fecha de salida.';
            RETURN;
        END

        UPDATE dbo.Pasaje
        SET PasajeroID = @NuevoPasajeroID
        WHERE PasajeID = @PasajeID;

        IF @@ROWCOUNT = 0
        BEGIN
            SET @Result = 'Error: No se pudo actualizar el pasaje.';
            RETURN;
        END

        UPDATE dbo.ReservaHabitacion
        SET PasajeroID = @NuevoPasajeroID
        WHERE PasajeID = @PasajeID;

        SET @Result = 'Done.';
    END TRY
    BEGIN CATCH
        SET @Result = 'Error: ' + ERROR_MESSAGE();
    END CATCH
END
GO

/* =============================================================================
   7) Nueva reserva — validación antes de persistir
      (solo el bloque nuevo; el resto del SP permanece igual en BD si ya existe)
      Si usás publicación SSDT, este CREATE OR ALTER reemplaza el SP completo.
   ============================================================================= */
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Reserva_Reservar]
(
    @ReservaId UNIQUEIDENTIFIER,
    @ViajeId UNIQUEIDENTIFIER,
    @VendedorID UNIQUEIDENTIFIER,
    @ClienteID UNIQUEIDENTIFIER,
    @Observaciones VARCHAR(8000) = NULL,
    @Condicion VARCHAR(50) = NULL,
    @MonedaTipo INT,
    @DescuentoDetalle VARCHAR(200) = NULL,
    @DescuentoMonto MONEY,
    @DescuentoIsDescuento BIT = 1,
    @PagoMonto MONEY = 0,
    @PagoMontoRecibidoMonedaTipo INT = 1,
    @PagoMontoEquivalente MONEY = NULL,
    @PagoMontoEquivalenteMonedaTipo INT = NULL,
    @PagoMontoEquivalenteCotizacion MONEY = NULL,
    @PagoNroRecibo VARCHAR(50) = '',
    @PagoTransaccionId VARCHAR(50) = NULL,
    @PagoTipoPago INT,
    @PagoNroFactura VARCHAR(50) = NULL,
    @ExpirationMinutes INT = 15
)
AS
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    BEGIN TRY
        BEGIN TRAN;

        DECLARE @FacturaID UNIQUEIDENTIFIER,
                @FechaHoy DATETIME,
                @PrecioFinal FLOAT(8),
                @DiasPreReserva INT = 0,
                @EstadoFactura INT,
                @EstadoPasaje INT,
                @ExpirationOn DATETIME;

        DECLARE @tempPasajes TABLE (
            PasajeId UNIQUEIDENTIFIER,
            PasajeroId UNIQUEIDENTIFIER,
            ButacaId UNIQUEIDENTIFIER,
            ButacaCodigo VARCHAR(4),
            ButacaPrecio FLOAT(53),
            AdicionalesIds VARCHAR(MAX),
            HabitacionId UNIQUEIDENTIFIER,
            PasajeroAdultoId UNIQUEIDENTIFIER,
            VoucherId UNIQUEIDENTIFIER,
            Edad TINYINT,
            ReservaHabId UNIQUEIDENTIFIER
        );

        IF (@Condicion = 'Cuenta Corriente')
            SET @DiasPreReserva = 7;

        SELECT @FacturaID = NEWID(),
               @FechaHoy = GETDATE(),
               @EstadoFactura = 2,
               @EstadoPasaje = 5,
               @ExpirationOn = DATEADD(MINUTE, @ExpirationMinutes, @FechaHoy),
               @PagoNroRecibo = CASE WHEN @PagoNroRecibo IS NULL THEN '' ELSE @PagoNroRecibo END;

        INSERT INTO PedidoReserva (
            [Id], [ViajeId], [VendedorId], [ClienteId],
            [Observaciones], [Condicion], [MonedaTipo],
            [DescuentoDetalle], [DescuentoMonto], [DescuentoIsDescuento],
            [PagoMonto], [PagoMontoRecibidoMonedaTipo], [PagoMontoEquivalente],
            [PagoMontoEquivalenteMonedaTipo], [PagoMontoEquivalenteCotizacion], [PagoNroRecibo],
            [PagoTransaccionId], [PagoTipoPago], [PagoNroFactura],
            [FacturaId], [CreatedOn], [ExpirationOn]
        )
        VALUES (
            @ReservaId, @ViajeId, @VendedorID, @ClienteID,
            @Observaciones, @Condicion, @MonedaTipo,
            @DescuentoDetalle, @DescuentoMonto, @DescuentoIsDescuento,
            @PagoMonto, @PagoMontoRecibidoMonedaTipo, @PagoMontoEquivalente,
            @PagoMontoEquivalenteMonedaTipo, @PagoMontoEquivalenteCotizacion, @PagoNroRecibo,
            @PagoTransaccionId, @PagoTipoPago, @PagoNroFactura,
            @FacturaID, @FechaHoy, @ExpirationOn
        );

        INSERT INTO @tempPasajes (
            PasajeId, PasajeroId, ButacaId, ButacaCodigo, ButacaPrecio,
            AdicionalesIds, HabitacionId, PasajeroAdultoId, VoucherId, Edad, ReservaHabId
        )
        SELECT p.PasajeId, p.PasajeroId, p.ButacaId, p.ButacaCodigo, p.ButacaPrecio,
               p.AdicionalesIds, p.HabitacionId, p.PasajeroAdultoId, NEWID(),
               DATEDIFF(YEAR, per.FechaNacimiento, @FechaHoy),
               CASE WHEN p.HabitacionId IS NOT NULL THEN NEWID() ELSE NULL END
        FROM dbo.PasajeSeleccionado p
        INNER JOIN dbo.Persona per ON per.PersonaID = p.PasajeroId
        WHERE p.ReservaId = @ReservaId;

        IF EXISTS (
            SELECT 1 FROM @tempPasajes p
            WHERE p.Edad < 3 AND p.ButacaId IS NULL AND p.PasajeroAdultoId IS NULL
        )
            RAISERROR('Hay menores sin asignar adultos.', 16, 1);

        IF EXISTS (
            SELECT 1
            FROM @tempPasajes tp
            WHERE tp.PasajeroId IS NOT NULL
              AND tp.PasajeId IS NOT NULL
              AND dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(tp.PasajeroId, @ViajeId, tp.PasajeId) = 1
        )
            RAISERROR('Uno o más pasajeros ya están registrados en otro viaje con la misma fecha de salida.', 16, 1);

        INSERT INTO dbo.Factura (FacturaID, Monto, Fecha, Estado, ClienteID, VendedorID, Observaciones, DiasPreReserva, MonedaTipo)
        VALUES (@FacturaID, @PrecioFinal, @FechaHoy, @EstadoFactura, @ClienteID, @VendedorID, @Observaciones, @DiasPreReserva, @MonedaTipo);

        CREATE TABLE #TEMP (Result VARCHAR(2048));
        INSERT INTO #TEMP
        EXEC usp_Factura_Audit @FacturaID, @ClienteID, @VendedorID, 'Insert', 'Nueva Reserva';
        DROP TABLE #TEMP;

        UPDATE p
        SET p.FacturaID = @FacturaID,
            p.PasajeroID = pas.PasajeroId,
            p.EstadoPasaje = @EstadoPasaje,
            p.VoucherID = CASE WHEN @EstadoPasaje = 4 THEN pas.VoucherId ELSE NULL END
        FROM @tempPasajes pas
        INNER JOIN dbo.Pasaje p ON p.PasajeID = pas.PasajeId
        WHERE pas.PasajeId IS NOT NULL;

        INSERT INTO dbo.PasajeAdicional (PasajeAdicionalID, PasajeID, AdicionalID, PasajeroId)
        SELECT NEWID(), pas.PasajeId, tAdicionales.AdicionalId, tAdicionales.PasajeroId
        FROM @tempPasajes pas
        CROSS APPLY (
            SELECT a.Item AS AdicionalId, pas.PasajeroId
            FROM dbo.Split(pas.AdicionalesIds, ',') a
            WHERE LEN(pas.AdicionalesIds) > 0
            UNION
            SELECT tAdicional_1.AdicionalId, auxPas.PasajeroId
            FROM @tempPasajes auxPas
            CROSS APPLY (
                SELECT a_1.Item AS AdicionalId
                FROM dbo.Split(auxPas.AdicionalesIds, ',') a_1
                WHERE LEN(auxPas.AdicionalesIds) > 0
            ) tAdicional_1
            WHERE auxPas.PasajeroAdultoId = pas.PasajeroId
        ) tAdicionales
        WHERE pas.PasajeId IS NOT NULL;

        IF (@EstadoPasaje = 4)
        BEGIN
            INSERT INTO dbo.Voucher (VoucherID, FechaEmision, VendedorID)
            SELECT p.VoucherId, @FechaHoy, @VendedorID
            FROM @tempPasajes p
            WHERE p.PasajeId IS NOT NULL;
        END

        INSERT INTO dbo.DetalleFactura (FacturaID, Detalle, Precio, Cantidad)
        SELECT @FacturaID, 'Butaca ' + ISNULL(b.CodigoButaca, ''), pas.ButacaPrecio, 1
        FROM @tempPasajes pas
        INNER JOIN dbo.Butaca b ON b.ButacaID = pas.ButacaId;

        ;WITH tHabitaciones AS (
            SELECT t.HabitacionId
            FROM @tempPasajes t
            WHERE t.PasajeId IS NOT NULL
            GROUP BY t.HabitacionId
        )
        INSERT INTO dbo.DetalleFactura (FacturaID, Detalle, Precio, Cantidad)
        SELECT @FacturaID, 'Habitacion ' + ISNULL(h.Descripcion, ''), h.Precio, 1
        FROM tHabitaciones hab
        INNER JOIN dbo.Habitacion h ON h.HabitacionID = hab.HabitacionId;

        ;WITH tAdicionales AS (
            SELECT ax.Item AS AdicionalId, COUNT(*) AS Cantidad
            FROM @tempPasajes pas
            CROSS APPLY (
                SELECT a.Item FROM dbo.Split(pas.AdicionalesIds, ',') a WHERE LEN(pas.AdicionalesIds) > 0
                UNION
                SELECT tAdicional1.AdicionalId
                FROM @tempPasajes auxPas
                CROSS APPLY (
                    SELECT a1.Item AS AdicionalId FROM dbo.Split(auxPas.AdicionalesIds, ',') a1 WHERE LEN(auxPas.AdicionalesIds) > 0
                ) tAdicional1
                WHERE auxPas.PasajeroAdultoId = pas.PasajeroId
            ) ax
            WHERE pas.PasajeId IS NOT NULL
            GROUP BY ax.Item
        )
        INSERT INTO dbo.DetalleFactura (FacturaID, Detalle, Precio, Cantidad, AdicionalID)
        SELECT @FacturaID, a.Descripcion, a.Monto, adi.Cantidad, a.AdicionalID
        FROM dbo.Adicional a
        INNER JOIN tAdicionales adi ON adi.AdicionalId = a.AdicionalID;

        IF (@DescuentoIsDescuento = 1)
        BEGIN
            INSERT INTO dbo.DetalleFactura (FacturaID, Detalle, Precio, Cantidad)
            VALUES (@FacturaID, @DescuentoDetalle, -@DescuentoMonto, 1);
        END

        DECLARE @tempOutput TABLE (ActionType VARCHAR(50), Id INT, HabitacionId UNIQUEIDENTIFIER);

        DECLARE @tHabitaciones TABLE (
            Id INT IDENTITY(1,1),
            Desde DATE, Hasta DATE,
            HoraIngreso VARCHAR(10), HoraSalida VARCHAR(10),
            HotelId UNIQUEIDENTIFIER, HabitacionId UNIQUEIDENTIFIER,
            PasajeroId UNIQUEIDENTIFIER, PasajeId UNIQUEIDENTIFIER,
            HabIdReservada UNIQUEIDENTIFIER
        );

        SET DATEFORMAT DMY;

        INSERT INTO @tHabitaciones (Desde, Hasta, HoraIngreso, HoraSalida, HotelId, HabitacionId, PasajeroId, PasajeId, HabIdReservada)
        SELECT CONVERT(DATE, vh.Desde), CONVERT(DATE, vh.Hasta), vh.HoraIngreso, vh.HoraSalida,
               vh.HotelID, p.HabitacionId, p.PasajeroId, p.PasajeId, p.ReservaHabId
        FROM dbo.ViajeHotel vh
        INNER JOIN TransHotelHabitacionViaje t ON t.ViajeID = vh.ViajeID AND t.HotelID = vh.HotelID
        INNER JOIN @tempPasajes p ON p.HabitacionId = t.HabitacionID
        WHERE vh.ViajeID = @ViajeId AND p.PasajeId IS NOT NULL;

        MERGE dbo.ReservaHabitacion AS t
        USING @tHabitaciones AS s
            ON s.PasajeroId = t.PasajeroID
           AND s.PasajeId = t.PasajeID
           AND s.Desde = t.Desde
           AND t.ViajeID = @ViajeId
        WHEN NOT MATCHED THEN
            INSERT (reservahabitacionid, habitacionid, pasajeid, fechareserva, desde, hasta, expiro, horaingreso, horasalida, pasajeroid, viajeid)
            VALUES (s.HabIdReservada, s.HabitacionId, s.PasajeId, @FechaHoy, s.Desde, s.Hasta, 1, s.HoraIngreso, s.HoraSalida, s.PasajeroId, @ViajeId)
        OUTPUT $action, s.Id, s.HabitacionId INTO @tempOutput(ActionType, Id, HabitacionId);

        UPDATE h SET estado = 1
        FROM dbo.Habitacion h
        INNER JOIN @tempOutput t ON t.HabitacionId = h.HabitacionID
        WHERE t.ActionType = 'INSERT';

        INSERT INTO AuditReservaHabitacion (ReservaHabitacionId, HabitacionId, PasajeId, ViajeId, UserId, [Action])
        SELECT h.HabIdReservada, h.HabitacionId, h.PasajeId, @ViajeId, @VendedorID, 'INSERT'
        FROM @tHabitaciones h
        INNER JOIN @tempOutput t ON t.Id = h.Id
        WHERE t.ActionType = 'INSERT';

        UPDATE p
        SET EstadoPasaje = CASE EstadoPasaje
            WHEN 4 THEN 6 WHEN 3 THEN 8 WHEN 5 THEN 9
            WHEN 6 THEN 6 WHEN 8 THEN 8 WHEN 9 THEN 9 END
        FROM dbo.Pasaje p
        INNER JOIN @tempPasajes t ON t.PasajeId = p.PasajeID;

        DECLARE @tMenores TABLE (MenorId UNIQUEIDENTIFIER, AdultoId UNIQUEIDENTIFIER, AdultoPasajeId UNIQUEIDENTIFIER);

        INSERT INTO @tMenores (MenorId, AdultoId, AdultoPasajeId)
        SELECT p.PasajeroId, pAdulto.PasajeroId, pAdulto.PasajeId
        FROM @tempPasajes p
        INNER JOIN @tempPasajes pAdulto ON pAdulto.PasajeroId = p.PasajeroAdultoId
        WHERE p.Edad < 3 AND p.ButacaId IS NULL AND p.PasajeroAdultoId IS NOT NULL;

        INSERT INTO dbo.pasajeromenor (pasajeid, pasajeroid, menorid)
        SELECT t.AdultoPasajeId, t.AdultoId, t.MenorId
        FROM @tMenores t
        LEFT JOIN dbo.PasajeroMenor pm
            ON pm.pasajeid = t.AdultoPasajeId AND pm.pasajeroid = t.AdultoId AND pm.menorid = t.MenorId
        WHERE pm.id IS NULL;

        CREATE TABLE #tPago (FacturaId UNIQUEIDENTIFIER, PagoId UNIQUEIDENTIFIER, Saldo MONEY, TotalFactura MONEY);
        INSERT INTO #tPago
        EXEC usp_MAT_Reserva_RegistrarPago @ReservaId, @PagoMonto;

        SELECT @ReservaId AS ReservaId, @ExpirationOn AS ExpirationOn;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        DECLARE @errmsg2 NVARCHAR(2048);
        SELECT @errmsg2 = 'Error in usp_MAT_Reserva_Reservar. Message:' + ERROR_MESSAGE()
            + ' Error Line:' + STR(ERROR_LINE())
            + ' ReservaId: ' + CONVERT(VARCHAR(50), @ReservaId);
        RAISERROR(@errmsg2, 16, 1);
    END CATCH
END
GO

PRINT 'Migración 2026-06-19_PasajeroConflictoFechaSalida completada.';
GO
