-- =============================================================================
-- 2026-10-29 — Seguimiento de clientes para vendedores
-- Author: Sebastian Garcia
-- Tabla ClienteContacto, función fn_MAT_Seguimiento_Clientes y SPs usp_MAT_Seguimiento_* /
-- usp_MAT_ClienteContacto_Insert. Paridad con MAT.DB (generado desde esos archivos).
-- Idempotente: la tabla se crea solo si no existe; función y SPs con CREATE OR ALTER.
-- Requiere SQL Server 2016 SP1+.
-- =============================================================================
SET NOCOUNT ON;
GO

IF OBJECT_ID('dbo.ClienteContacto', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[ClienteContacto] (
    [ClienteContactoID] UNIQUEIDENTIFIER CONSTRAINT [DF_ClienteContacto_ClienteContactoID] DEFAULT (newid()) NOT NULL,
    [ClienteID]         UNIQUEIDENTIFIER NOT NULL,
    [VendedorID]        UNIQUEIDENTIFIER NULL,
    [Usuario]           VARCHAR (100)    NULL,
    [Fecha]             DATETIME         CONSTRAINT [DF_ClienteContacto_Fecha] DEFAULT (getdate()) NOT NULL,
    [Resultado]         INT              NOT NULL,
    [Observacion]       VARCHAR (500)    NULL,
    CONSTRAINT [PK_ClienteContacto] PRIMARY KEY CLUSTERED ([ClienteContactoID] ASC),
    CONSTRAINT [FK_ClienteContacto_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_ClienteContacto_Vendedor] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Vendedor] ([VendedorID]),
    CONSTRAINT [CK_ClienteContacto_Resultado] CHECK ([Resultado] BETWEEN 1 AND 8)
);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_ClienteContacto_ClienteID_Fecha' AND object_id = OBJECT_ID('dbo.ClienteContacto'))
CREATE NONCLUSTERED INDEX [IX_ClienteContacto_ClienteID_Fecha]
    ON [dbo].[ClienteContacto]([ClienteID] ASC, [Fecha] DESC)
    INCLUDE([Resultado], [VendedorID], [Usuario]);
GO

CREATE OR ALTER FUNCTION [dbo].[fn_MAT_Seguimiento_Clientes] (@Hoy DATE)
RETURNS TABLE
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Una fila por cliente con métricas de viaje, gasto, último contacto y
  --              categoría de seguimiento (docs/DECISIONES_ABIERTAS.md, Seguimiento de clientes).
  --              Viaje realizado = pasaje Señado/Pagado (3,4,6,8) con salida hasta @Hoy.
  --              Categorías (gana la primera): VIP, EnRiesgo, Frecuente, Nuevo, Ocasional, Dormido, SinViajes.
  ============================================= */
RETURN
WITH viajes AS (
    SELECT p.PasajeroID AS ClienteID,
           COUNT(DISTINCT CASE WHEN p.EstadoPasaje IN (3, 4, 6, 8) AND v.FechaSalida <= @Hoy THEN p.ViajeID END) AS ViajesTotales,
           COUNT(DISTINCT CASE WHEN p.EstadoPasaje IN (3, 4, 6, 8) AND v.FechaSalida <= @Hoy
                                    AND v.FechaSalida > DATEADD(MONTH, -12, @Hoy) THEN p.ViajeID END) AS Viajes12Meses,
           MIN(CASE WHEN p.EstadoPasaje IN (3, 4, 6, 8) AND v.FechaSalida <= @Hoy THEN v.FechaSalida END) AS PrimerViaje,
           MAX(CASE WHEN p.EstadoPasaje IN (3, 4, 6, 8) AND v.FechaSalida <= @Hoy THEN v.FechaSalida END) AS UltimoViaje,
           MIN(CASE WHEN p.EstadoPasaje NOT IN (1, 7) AND v.FechaSalida > @Hoy THEN v.FechaSalida END) AS ProximoViaje
    FROM dbo.Pasaje p
    INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
    WHERE p.PasajeroID IS NOT NULL
    GROUP BY p.PasajeroID
),
gasto AS (
    SELECT f.ClienteID,
           SUM(CASE WHEN f.MonedaTipo = 1 THEN f.Monto END) AS GastoARS,
           SUM(CASE WHEN f.MonedaTipo = 3 THEN f.Monto END) AS GastoUSD
    FROM dbo.Factura f
    WHERE f.Estado IN (3, 4)
    GROUP BY f.ClienteID
),
fechasRepetidas AS (
    SELECT FechaNacimiento, COUNT(*) AS Cantidad
    FROM dbo.Persona
    WHERE FechaNacimiento IS NOT NULL
    GROUP BY FechaNacimiento
)
SELECT c.ClienteID,
       pe.Apellido,
       pe.Nombre,
       pe.NroDocumento,
       pe.Celular,
       pe.Telefono,
       pe.Email,
       pe.FechaNacimiento,
       CAST(CASE WHEN fr.Cantidad >= 10 THEN 1 ELSE 0 END AS BIT) AS FechaNacimientoDudosa,
       ISNULL(vj.ViajesTotales, 0) AS ViajesTotales,
       ISNULL(vj.Viajes12Meses, 0) AS Viajes12Meses,
       vj.PrimerViaje,
       vj.UltimoViaje,
       vj.ProximoViaje,
       g.GastoARS,
       g.GastoUSD,
       CASE
           WHEN ISNULL(vj.ViajesTotales, 0) = 0 THEN 'SinViajes'
           WHEN vj.ViajesTotales >= 5 AND vj.UltimoViaje > DATEADD(MONTH, -12, @Hoy) THEN 'VIP'
           WHEN vj.ViajesTotales >= 3 AND vj.UltimoViaje <= DATEADD(MONTH, -12, @Hoy)
                AND vj.UltimoViaje > DATEADD(MONTH, -24, @Hoy) THEN 'EnRiesgo'
           WHEN vj.ViajesTotales BETWEEN 2 AND 4 AND vj.UltimoViaje > DATEADD(MONTH, -12, @Hoy) THEN 'Frecuente'
           WHEN vj.ViajesTotales = 1 AND vj.UltimoViaje > DATEADD(MONTH, -6, @Hoy) THEN 'Nuevo'
           WHEN vj.UltimoViaje > DATEADD(MONTH, -24, @Hoy) THEN 'Ocasional'
           ELSE 'Dormido'
       END AS Categoria,
       uc.Fecha AS UltimoContactoFecha,
       uc.Resultado AS UltimoContactoResultado,
       uc.Quien AS UltimoContactoPor,
       CAST(CASE WHEN uc.Resultado = 8 THEN 1 ELSE 0 END AS BIT) AS NoContactar
FROM dbo.Cliente c
INNER JOIN dbo.Persona pe ON pe.PersonaID = c.ClienteID
LEFT JOIN viajes vj ON vj.ClienteID = c.ClienteID
LEFT JOIN gasto g ON g.ClienteID = c.ClienteID
LEFT JOIN fechasRepetidas fr ON fr.FechaNacimiento = pe.FechaNacimiento
OUTER APPLY (
    SELECT TOP (1) cc.Fecha, cc.Resultado, COALESCE(pv.FullName, cc.Usuario) AS Quien
    FROM dbo.ClienteContacto cc
    LEFT JOIN dbo.Persona pv ON pv.PersonaID = cc.VendedorID
    WHERE cc.ClienteID = c.ClienteID
    ORDER BY cc.Fecha DESC
) uc;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Seguimiento_Clientes]
(
    @Categoria          VARCHAR(20)  = NULL,
    @Texto              VARCHAR(100) = NULL,
    @IncluirNoContactar BIT          = 0
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Listado de seguimiento de clientes por categoría y/o texto (apellido, nombre o DNI).
  --              Por defecto excluye a los marcados "No volver a contactar".
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
    DECLARE @Busqueda VARCHAR(102) = '%' + NULLIF(LTRIM(RTRIM(@Texto)), '') + '%';

    SELECT s.*
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE (@Categoria IS NULL OR s.Categoria = @Categoria)
      AND (@Busqueda IS NULL
           OR s.Apellido LIKE @Busqueda
           OR s.Nombre LIKE @Busqueda
           OR (s.Apellido + ' ' + s.Nombre) LIKE @Busqueda
           OR REPLACE(s.NroDocumento, '.', '') LIKE REPLACE(@Busqueda, '.', ''))
      AND (@IncluirNoContactar = 1 OR s.NoContactar = 0)
    ORDER BY s.ViajesTotales DESC, s.UltimoViaje DESC, s.Apellido, s.Nombre;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Seguimiento_ResumenCategorias]
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Cantidad de clientes por categoría de seguimiento (sin los "No volver a contactar").
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);

    SELECT s.Categoria, COUNT(*) AS Cantidad
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE s.NoContactar = 0
    GROUP BY s.Categoria;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Seguimiento_Cumpleanios]
(
    @Dias INT = 0
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Clientes que cumplen años entre hoy y hoy + @Dias (0 = solo hoy), sin los
  --              "No volver a contactar" ni fechas de nacimiento imposibles. Informa si ya se los
  --              saludó en los últimos 30 días y quién (aviso sin bloqueo entre vendedores).
  --              29/02: en años no bisiestos se saluda el 28/02 (DATEADD).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
    IF (@Dias IS NULL OR @Dias < 0) SET @Dias = 0;
    IF (@Dias > 60) SET @Dias = 60;

    SELECT s.*,
           pc.ProximoCumple,
           DATEDIFF(DAY, @Hoy, pc.ProximoCumple) AS DiasFaltan,
           DATEDIFF(YEAR, s.FechaNacimiento, pc.ProximoCumple) AS EdadQueCumple,
           sal.Fecha AS UltimoSaludoFecha,
           sal.Quien AS UltimoSaludoPor
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    CROSS APPLY (
        SELECT CASE
                   WHEN DATEADD(YEAR, YEAR(@Hoy) - YEAR(s.FechaNacimiento), s.FechaNacimiento) >= @Hoy
                       THEN DATEADD(YEAR, YEAR(@Hoy) - YEAR(s.FechaNacimiento), s.FechaNacimiento)
                   ELSE DATEADD(YEAR, YEAR(@Hoy) - YEAR(s.FechaNacimiento) + 1, s.FechaNacimiento)
               END AS ProximoCumple
    ) pc
    OUTER APPLY (
        SELECT TOP (1) cc.Fecha, COALESCE(pv.FullName, cc.Usuario) AS Quien
        FROM dbo.ClienteContacto cc
        LEFT JOIN dbo.Persona pv ON pv.PersonaID = cc.VendedorID
        WHERE cc.ClienteID = s.ClienteID
          AND cc.Resultado = 1
          AND cc.Fecha >= DATEADD(DAY, -30, @Hoy)
        ORDER BY cc.Fecha DESC
    ) sal
    WHERE s.FechaNacimiento IS NOT NULL
      AND YEAR(s.FechaNacimiento) >= 1910
      AND s.FechaNacimiento <= @Hoy
      AND s.NoContactar = 0
      AND DATEDIFF(DAY, @Hoy, pc.ProximoCumple) <= @Dias
    ORDER BY DiasFaltan, s.Apellido, s.Nombre;
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
  --              RS2: viajes (sin Disponible ni Anulado), más reciente primero.
  --              RS3: historial de contactos.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);

    SELECT s.*
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE s.ClienteID = @ClienteID;

    SELECT v.FechaSalida,
           v.FechaRegreso,
           v.Descripcion AS Viaje,
           e.Descripcion AS EstadoPasaje
    FROM dbo.Pasaje p
    INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
    INNER JOIN dbo.EstadoPasaje e ON e.ID = p.EstadoPasaje
    WHERE p.PasajeroID = @ClienteID
      AND p.EstadoPasaje NOT IN (1, 7)
    ORDER BY v.FechaSalida DESC;

    SELECT cc.Fecha,
           cc.Resultado,
           cc.Observacion,
           COALESCE(pv.FullName, cc.Usuario) AS Quien
    FROM dbo.ClienteContacto cc
    LEFT JOIN dbo.Persona pv ON pv.PersonaID = cc.VendedorID
    WHERE cc.ClienteID = @ClienteID
    ORDER BY cc.Fecha DESC;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_ClienteContacto_Insert]
(
    @ClienteID   UNIQUEIDENTIFIER,
    @VendedorID  UNIQUEIDENTIFIER = NULL,
    @Usuario     VARCHAR(100)     = NULL,
    @Resultado   INT,
    @Observacion VARCHAR(500)     = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Registra un contacto con el cliente (saludo, oferta, resultado).
  --              Resultado: 1 Saludo cumpleaños, 2 Ofrecido viaje, 3 Interesado, 4 Compró,
  --              5 No le interesa, 6 No contesta, 7 Número equivocado, 8 No volver a contactar.
  --              Devuelve el ID creado.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    DECLARE @ID UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.ClienteContacto (ClienteContactoID, ClienteID, VendedorID, Usuario, Resultado, Observacion)
    VALUES (@ID, @ClienteID, @VendedorID, @Usuario, @Resultado, NULLIF(LTRIM(RTRIM(@Observacion)), ''));

    SELECT @ID AS ClienteContactoID;
END
GO
