/*
----------------------------------------------------------------------------------------------------
-- Created By: Sistema MAT
-- Create date: 2026-01-XX
-- Purpose: Obtiene todos los presupuestos con filtros opcionales
-- Description: Retorna lista completa de presupuestos permitiendo filtrar por estado, vendedor, DNI, código y fechas
----------------------------------------------------------------------------------------------------
*/

CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_GetAll]
(
    @Estado            INT = NULL,
    @VendedorIdOrigen  UNIQUEIDENTIFIER = NULL,
    @DniCliente        VARCHAR(50) = NULL,
    @CodigoSeguimiento VARCHAR(50) = NULL,
    @FechaDesde        DATETIME = NULL,
    @FechaHasta        DATETIME = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.[PresupuestoID],
        p.[DniCliente],
        p.[VendedorIdOrigen],
        vOrigen.[Descripcion] AS VendedorOrigenNombre,
        p.[CodigoSeguimiento],
        p.[MontoPactado],
        p.[ViajeId],
        v.[Descripcion] AS ViajeDescripcion,
        pa.[Descripcion] AS PaqueteDescripcion,
        p.[Estado],
        p.[FechaCreacion],
        p.[FechaExpiracion],
        p.[FacturaId],
        p.[VendedorIdCierre],
        vCierre.[Descripcion] AS VendedorCierreNombre,
        p.[Observaciones],
        CASE 
            WHEN p.[Estado] = 2 THEN 1 -- Expirado
            WHEN p.[FechaExpiracion] < GETDATE() AND p.[Estado] = 1 THEN 1 -- Pendiente pero expirado
            ELSE 0
        END AS IsExpirado,
        CASE 
            WHEN p.[FechaExpiracion] < GETDATE() AND p.[Estado] = 1 THEN 1 -- Pendiente pero expirado
            ELSE 0
        END AS IsExpiradoAutomatico
    FROM [dbo].[Presupuesto] p
    LEFT JOIN [dbo].[Vendedor] vOrigen ON p.[VendedorIdOrigen] = vOrigen.[VendedorID]
    LEFT JOIN [dbo].[Vendedor] vCierre ON p.[VendedorIdCierre] = vCierre.[VendedorID]
    LEFT JOIN [dbo].[Viaje] v ON p.[ViajeId] = v.[ViajeID]
    LEFT JOIN [dbo].[Paquete] pa ON v.[PaqueteID] = pa.[PaqueteID]
    WHERE 
        (@Estado IS NULL OR p.[Estado] = @Estado)
        AND (@VendedorIdOrigen IS NULL OR p.[VendedorIdOrigen] = @VendedorIdOrigen)
        AND (@DniCliente IS NULL OR p.[DniCliente] LIKE '%' + @DniCliente + '%')
        AND (@CodigoSeguimiento IS NULL OR p.[CodigoSeguimiento] LIKE '%' + @CodigoSeguimiento + '%')
        AND (@FechaDesde IS NULL OR p.[FechaCreacion] >= @FechaDesde)
        AND (@FechaHasta IS NULL OR p.[FechaCreacion] <= DATEADD(DAY, 1, @FechaHasta))
    ORDER BY p.[FechaCreacion] DESC;
END

GO

