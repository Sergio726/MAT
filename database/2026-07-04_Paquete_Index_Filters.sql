-- Reescritura modulo Paquete (UX/UI + rendimiento): publicar SP nuevos/modificados en cada entorno.
-- Fuente de verdad: MAT.DB/dbo/Stored Procedures/
-- Contenido:
--   1) usp_MAT_Paquete_GetPaquetes (MODIFICADO): filtros (anio actual por defecto,
--      Search, Temporada, Moneda) + destino (Localidad) + MonedaCodigo limpio.
--   2) usp_MAT_Servicio_GetAvailableForPaquete   (NUEVO): servicios no vinculados (elimina N+1).
--   3) usp_MAT_Precio_GetAvailableForPaquete      (NUEVO): precios no vinculados (elimina N+1).
--   4) usp_MAT_Adicional_GetAvailableForPaquete   (NUEVO): adicionales no vinculados (elimina N+1).
--   5) usp_MAT_Paquetes_VinculosByPaqueteID (MODIFICADO): agrega VinculoRowId (PK de vinculo)
--      para desvincular excursiones sin una segunda consulta.

-- =========================================================================
-- 1) MODIFICADO: usp_MAT_Paquete_GetPaquetes
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_Paquete_GetPaquetes', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Paquete_GetPaquetes;
GO

CREATE PROCEDURE usp_MAT_Paquete_GetPaquetes
(
    @DateYear  VARCHAR(4)    = '',
    @Search    NVARCHAR(100) = NULL,
    @Temporada INT           = NULL,
    @Moneda    INT           = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Lista de paquetes con filtros para el listado /Paquete.
  --              @DateYear vacio devuelve todos los anios (compat con el selector
  --              de paquetes de Viaje); el listado /Paquete fija el anio actual por
  --              defecto desde el controlador. Filtros opcionales por texto
  --              (Descripcion/Codigo), Temporada y Moneda. Devuelve MonedaCodigo
  --              y el nombre del Destino (Localidad) para el listado.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Year INT =
        CASE
            WHEN @DateYear IS NULL OR LTRIM(RTRIM(@DateYear)) = '' THEN NULL
            ELSE TRY_CONVERT(INT, @DateYear)
        END;

    IF @Search IS NOT NULL AND LTRIM(RTRIM(@Search)) = ''
        SET @Search = NULL;

    SELECT
         P.PaqueteID
        ,P.Descripcion
        ,P.Iva
        ,P.Alicuota
        ,P.Temporada
        ,P.Cotizacion
        ,P.Codigo
        ,P.DestinoID
        ,Destino = L.Nombre
        ,P.Foto
        ,FechaCreacion = CONVERT(VARCHAR(10), P.FechaCreacion, 103)
        ,LastUpdate    = CONVERT(VARCHAR(10), P.LastUpdate, 103)
        ,MonedaCodigo  = mt.Codigo
    FROM dbo.Paquete P
    INNER JOIN dbo.MonedaTipo mt
        ON P.Moneda = mt.Id
    LEFT JOIN dbo.Localidad L
        ON P.DestinoID = L.ID
    WHERE (@Year IS NULL OR YEAR(P.FechaCreacion) = @Year)
      AND (@Search IS NULL
           OR P.Descripcion LIKE '%' + @Search + '%'
           OR P.Codigo LIKE '%' + @Search + '%'
           OR L.Nombre LIKE '%' + @Search + '%')
      AND (@Temporada IS NULL OR P.Temporada = @Temporada)
      AND (@Moneda IS NULL OR P.Moneda = @Moneda)
    ORDER BY P.FechaCreacion DESC;
END
GO

-- =========================================================================
-- 2) NUEVO: usp_MAT_Servicio_GetAvailableForPaquete
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_Servicio_GetAvailableForPaquete', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Servicio_GetAvailableForPaquete;
GO

CREATE PROCEDURE [dbo].[usp_MAT_Servicio_GetAvailableForPaquete]
(
    @PaqueteID UNIQUEIDENTIFIER,
    @Filter    NVARCHAR(100) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Servicios disponibles para vincular a un paquete (los que aun
  --              no estan vinculados), con filtro opcional por descripcion.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @Filter IS NOT NULL AND LTRIM(RTRIM(@Filter)) = ''
        SET @Filter = NULL;

    SELECT TOP (100)
         s.ServicioID
        ,s.Descripcion
    FROM dbo.Servicio s
    WHERE NOT EXISTS (
              SELECT 1
              FROM dbo.PaqueteServicio ps
              WHERE ps.ServicioID = s.ServicioID
                AND ps.PaqueteID = @PaqueteID)
      AND (@Filter IS NULL OR s.Descripcion LIKE '%' + @Filter + '%')
    ORDER BY s.Descripcion;
END
GO

-- =========================================================================
-- 3) NUEVO: usp_MAT_Precio_GetAvailableForPaquete
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_Precio_GetAvailableForPaquete', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Precio_GetAvailableForPaquete;
GO

CREATE PROCEDURE [dbo].[usp_MAT_Precio_GetAvailableForPaquete]
(
    @PaqueteID UNIQUEIDENTIFIER,
    @Filter    NVARCHAR(100) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Precios disponibles para vincular a un paquete (los que aun
  --              no estan vinculados), con filtro opcional por descripcion.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @Filter IS NOT NULL AND LTRIM(RTRIM(@Filter)) = ''
        SET @Filter = NULL;

    SELECT TOP (100)
         p.PrecioID
        ,p.Monto
        ,p.Vigencia
        ,p.Descripcion
        ,p.Mes
    FROM dbo.Precio p
    WHERE NOT EXISTS (
              SELECT 1
              FROM dbo.PaquetePrecio pp
              WHERE pp.PrecioID = p.PrecioID
                AND pp.PaqueteID = @PaqueteID)
      AND (@Filter IS NULL OR p.Descripcion LIKE '%' + @Filter + '%')
    ORDER BY p.Descripcion;
END
GO

-- =========================================================================
-- 4) NUEVO: usp_MAT_Adicional_GetAvailableForPaquete
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_Adicional_GetAvailableForPaquete', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Adicional_GetAvailableForPaquete;
GO

CREATE PROCEDURE [dbo].[usp_MAT_Adicional_GetAvailableForPaquete]
(
    @PaqueteID UNIQUEIDENTIFIER,
    @Filter    NVARCHAR(100) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Adicionales disponibles para vincular a un paquete (los que aun
  --              no estan vinculados), con filtro opcional por descripcion.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @Filter IS NOT NULL AND LTRIM(RTRIM(@Filter)) = ''
        SET @Filter = NULL;

    SELECT TOP (100)
         a.AdicionalID
        ,a.Monto
        ,a.Descripcion
    FROM dbo.Adicional a
    WHERE NOT EXISTS (
              SELECT 1
              FROM dbo.PaqueteAdicional pa
              WHERE pa.AdicionalID = a.AdicionalID
                AND pa.PaqueteID = @PaqueteID)
      AND (@Filter IS NULL OR a.Descripcion LIKE '%' + @Filter + '%')
    ORDER BY a.Descripcion;
END
GO

-- =========================================================================
-- 5) MODIFICADO: usp_MAT_Paquetes_VinculosByPaqueteID (agrega VinculoRowId)
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_Paquetes_VinculosByPaqueteID', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Paquetes_VinculosByPaqueteID;
GO

CREATE PROCEDURE [dbo].[usp_MAT_Paquetes_VinculosByPaqueteID]
 @PaqueteID uniqueidentifier
AS
 /*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Vinculos de un paquete (Servicios, Excursiones, Precios,
  --              Adicionales) en un solo resultset. Agrega VinculoRowId (PK de
  --              la tabla de vinculo) para desvincular excursiones sin una
  --              segunda consulta.
  ============================================= */
BEGIN
    SET NOCOUNT ON;

    SELECT
        ps.PaqueteID,
        ps.ServicioID as ID,
        VinculoRowId = ps.PaqueteServicioID,
        NULL as IsOpcional,
        s.Precio as Precio,
        s.Descripcion as Descripcion,
        'Servicio' as Tipo
    FROM dbo.PaqueteServicio ps
    inner join Servicio s
        on ps.ServicioID = s.ServicioID
    WHERE ps.PaqueteID = @PaqueteID

    UNION ALL

    SELECT
        pe.PaqueteID,
        pe.ExcursionID as ID,
        VinculoRowId = pe.PaqueteExcursionID,
        pe.IsOpcional,
        e.Costo as Precio,
        e.Descripcion as Descripcion,
        'Excursion' as Tipo
    FROM dbo.PaqueteExcursion pe
    inner join Excursion e
        on pe.ExcursionID = e.ExcursionID
    WHERE pe.PaqueteID = @PaqueteID

    UNION ALL

    SELECT
        pp.PaqueteID,
        pp.PrecioID as ID,
        VinculoRowId = pp.PaquetePrecioID,
        NULL as IsOpcional,
        p.Monto as Precio,
        p.Descripcion as Descripcion,
        'Precio' as Tipo
    FROM dbo.PaquetePrecio pp
    inner join dbo.Precio p
        on pp.PrecioID = p.PrecioID
    WHERE pp.PaqueteID = @PaqueteID

    UNION ALL

    SELECT
        pa.PaqueteID,
        pa.AdicionalID as ID,
        VinculoRowId = pa.PaqueteAdicionalID,
        NULL as IsOpcional,
        a.Monto as Precio,
        a.Descripcion as Descripcion,
        'Adicional' as Tipo
    FROM dbo.PaqueteAdicional pa
    inner join dbo.Adicional a
        on pa.AdicionalID = a.AdicionalID
    WHERE pa.PaqueteID = @PaqueteID

    ORDER BY Tipo, Descripcion
END
GO
