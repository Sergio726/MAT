-- =============================================================================
-- 2026-11-04 — Módulo Clientes F5: canal del contacto y Seguimiento paginado en la base
-- Author: Sebastian Garcia
-- * ClienteContacto.Canal (1 WhatsApp, 2 Llamada, 3 Email, 4 Presencial) + CHECK.
-- * usp_MAT_ClienteContacto_Insert (@Canal), usp_MAT_Seguimiento_Ficha (devuelve el canal),
--   usp_MAT_Seguimiento_ClientesPagina (nuevo: categoría o búsqueda paginada).
-- Paridad con MAT.DB. Idempotente. No modifica datos (los contactos anteriores quedan con canal vacío).
-- Requiere 2026-10-29_SeguimientoClientes.sql.
-- =============================================================================
SET NOCOUNT ON;
GO

IF COL_LENGTH('dbo.ClienteContacto', 'Canal') IS NULL
    ALTER TABLE dbo.ClienteContacto ADD Canal TINYINT NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_ClienteContacto_Canal')
    ALTER TABLE dbo.ClienteContacto ADD CONSTRAINT CK_ClienteContacto_Canal CHECK (Canal >= 1 AND Canal <= 4);
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_ClienteContacto_Insert]
(
    @ClienteID   UNIQUEIDENTIFIER,
    @VendedorID  UNIQUEIDENTIFIER = NULL,
    @Usuario     VARCHAR(100)     = NULL,
    @Resultado   INT,
    @Observacion VARCHAR(500)     = NULL,
    @Canal       TINYINT          = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Registra un contacto con el cliente (saludo, oferta, resultado).
  --              Resultado: 1 Saludo cumpleaños, 2 Ofrecido viaje, 3 Interesado, 4 Compró,
  --              5 No le interesa, 6 No contesta, 7 Número equivocado, 8 No volver a contactar.
  --              Devuelve el ID creado.
  -- 2026-11-04 (Clientes F5): @Canal — 1 WhatsApp, 2 Llamada, 3 Email, 4 Presencial.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    DECLARE @ID UNIQUEIDENTIFIER = NEWID();
    -- Hora argentina: el servidor está en UTC.
    DECLARE @Fecha DATETIME = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATETIME);

    INSERT INTO dbo.ClienteContacto (ClienteContactoID, ClienteID, VendedorID, Usuario, Fecha, Resultado, Observacion, Canal)
    VALUES (@ID, @ClienteID, @VendedorID, @Usuario, @Fecha, @Resultado, NULLIF(LTRIM(RTRIM(@Observacion)), ''), @Canal);

    SELECT @ID AS ClienteContactoID;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Seguimiento_Ficha]
(
    @ClienteID UNIQUEIDENTIFIER
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Ficha de seguimiento de un cliente.
  --              RS1: datos + métricas (fn_MAT_Seguimiento_Clientes).
  --              RS2: viajes como pasajero o pagados para otros (sin Disponible ni Anulado), más reciente primero.
  --              RS3: historial de contactos (con canal desde 2026-11-04, F5).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    -- Hora argentina: el servidor está en UTC y desde las 21:00 "hoy" sería mañana.
    DECLARE @Hoy DATE = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATE);

    SELECT s.*
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE s.ClienteID = @ClienteID;

    -- Viajes como pasajero o pagados por el cliente para otros (un renglón por viaje)
    SELECT v.FechaSalida,
           v.FechaRegreso,
           v.Descripcion AS Viaje,
           MAX(CASE WHEN p.PasajeroID = @ClienteID THEN e.Descripcion END) AS EstadoPasaje,
           CASE WHEN MAX(CASE WHEN p.PasajeroID = @ClienteID THEN 1 ELSE 0 END) = 1 THEN 'Viajó' ELSE 'Pagó para otros' END AS Rol,
           COUNT(*) AS Pasajes
    FROM dbo.Pasaje p
    INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
    INNER JOIN dbo.EstadoPasaje e ON e.ID = p.EstadoPasaje
    LEFT JOIN dbo.Factura f ON f.FacturaID = p.FacturaID
    WHERE (p.PasajeroID = @ClienteID OR f.ClienteID = @ClienteID)
      AND p.EstadoPasaje NOT IN (1, 7)
    GROUP BY v.ViajeID, v.FechaSalida, v.FechaRegreso, v.Descripcion
    ORDER BY v.FechaSalida DESC;

    SELECT cc.Fecha,
           cc.Resultado,
           cc.Canal,
           cc.Observacion,
           COALESCE(pv.FullName, cc.Usuario) AS Quien
    FROM dbo.ClienteContacto cc
    LEFT JOIN dbo.Persona pv ON pv.PersonaID = cc.VendedorID
    WHERE cc.ClienteID = @ClienteID
    ORDER BY cc.Fecha DESC;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Seguimiento_ClientesPagina]
(
    @Categoria          VARCHAR(20)  = NULL,
    @Texto              VARCHAR(100) = NULL,
    @IncluirNoContactar BIT          = 0,
    @Orden              VARCHAR(20)  = 'viajes',
    @Desc               BIT          = 1,
    @Inicio             INT          = 0,
    @Cantidad           INT          = 25
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Seguimiento por categoría o búsqueda, paginado en la base (módulo Clientes F5).
  --              Antes usp_MAT_Seguimiento_Clientes mandaba la categoría entera al navegador (Dormido: más de 6.000).
  --              @Orden: cliente | viajes | ultimoViaje | proximoViaje | ultimoContacto. @Cantidad: 10 a 100.
  --              RS1: Total. RS2: la página (mismas columnas que fn_MAT_Seguimiento_Clientes).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (@Inicio IS NULL OR @Inicio < 0) SET @Inicio = 0;
    IF (@Cantidad IS NULL OR @Cantidad < 10) SET @Cantidad = 10;
    IF (@Cantidad > 100) SET @Cantidad = 100;
    IF (@Orden IS NULL OR @Orden NOT IN ('cliente', 'viajes', 'ultimoViaje', 'proximoViaje', 'ultimoContacto')) SET @Orden = 'viajes';
    SET @Desc = ISNULL(@Desc, 1);

    -- Hora argentina: el servidor está en UTC y desde las 21:00 "hoy" sería mañana.
    DECLARE @Hoy DATE = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATE);
    DECLARE @Busqueda VARCHAR(102) = '%' + NULLIF(LTRIM(RTRIM(@Texto)), '') + '%';

    SELECT s.*
    INTO #Lista
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE (@Categoria IS NULL OR s.Categoria = @Categoria)
      AND (@Busqueda IS NULL
           OR s.Apellido LIKE @Busqueda
           OR s.Nombre LIKE @Busqueda
           OR (s.Apellido + ' ' + s.Nombre) LIKE @Busqueda
           OR REPLACE(s.NroDocumento, '.', '') LIKE REPLACE(@Busqueda, '.', ''))
      AND (@IncluirNoContactar = 1 OR s.NoContactar = 0);

    SELECT COUNT(*) AS Total FROM #Lista;

    SELECT *
    FROM #Lista l
    ORDER BY
        CASE WHEN @Orden = 'cliente' AND @Desc = 0 THEN LTRIM(l.Apellido) + ' ' + LTRIM(l.Nombre) END ASC,
        CASE WHEN @Orden = 'cliente' AND @Desc = 1 THEN LTRIM(l.Apellido) + ' ' + LTRIM(l.Nombre) END DESC,
        CASE WHEN @Orden = 'viajes' AND @Desc = 0 THEN l.ViajesTotales END ASC,
        CASE WHEN @Orden = 'viajes' AND @Desc = 1 THEN l.ViajesTotales END DESC,
        CASE WHEN @Orden = 'ultimoViaje' AND @Desc = 0 THEN l.UltimoViaje END ASC,
        CASE WHEN @Orden = 'ultimoViaje' AND @Desc = 1 THEN l.UltimoViaje END DESC,
        CASE WHEN @Orden = 'proximoViaje' AND @Desc = 0 THEN l.ProximoViaje END ASC,
        CASE WHEN @Orden = 'proximoViaje' AND @Desc = 1 THEN l.ProximoViaje END DESC,
        CASE WHEN @Orden = 'ultimoContacto' AND @Desc = 0 THEN l.UltimoContactoFecha END ASC,
        CASE WHEN @Orden = 'ultimoContacto' AND @Desc = 1 THEN l.UltimoContactoFecha END DESC,
        l.UltimoViaje DESC, l.Apellido, l.Nombre, l.ClienteID
    OFFSET @Inicio ROWS FETCH NEXT @Cantidad ROWS ONLY;
END
GO
