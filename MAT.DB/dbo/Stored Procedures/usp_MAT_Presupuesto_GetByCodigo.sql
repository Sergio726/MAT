/*
----------------------------------------------------------------------------------------------------
-- Created By: Seba Garcia
-- Create date: 2026-01-11
-- Purpose: Obtiene un presupuesto por código de seguimiento
-- Description: Retorna un presupuesto específico usando su código único
----------------------------------------------------------------------------------------------------
*/

CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_GetByCodigo]
(
    @CodigoSeguimiento VARCHAR(50)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT 
        p.[PresupuestoID],
        p.[DniCliente],
        p.[VendedorIdOrigen],
        p.[CodigoSeguimiento],
        p.[MontoPactado],
        p.[ViajeId],
        p.[Estado],
        p.[FechaCreacion],
        p.[FechaExpiracion],
        p.[FacturaId],
        p.[VendedorIdCierre],
        p.[Observaciones],
        -- Información del vendedor origen
        vOrigen.Descripcion AS VendedorOrigenNombre,
        -- Información del vendedor cierre (si existe)
        vCierre.Descripcion AS VendedorCierreNombre,
        -- Información del viaje
        v.Descripcion AS ViajeDescripcion,
        paq.Descripcion AS PaqueteDescripcion,
        -- Verificar si está expirado
        CASE 
            WHEN p.[FechaExpiracion] < GETDATE() THEN 1
            ELSE 0
        END AS IsExpirado
    FROM [dbo].[Presupuesto] p WITH (NOLOCK)
    INNER JOIN [dbo].[Vendedor] vOrigen WITH (NOLOCK)
        ON p.[VendedorIdOrigen] = vOrigen.[VendedorID]
    LEFT JOIN [dbo].[Vendedor] vCierre WITH (NOLOCK)
        ON p.[VendedorIdCierre] = vCierre.[VendedorID]
    LEFT JOIN [dbo].[Viaje] v WITH (NOLOCK)
        ON p.[ViajeId] = v.[ViajeID]
    LEFT JOIN [dbo].[Paquete] paq WITH (NOLOCK)
        ON v.[PaqueteID] = paq.[PaqueteID]
    WHERE 
        p.[CodigoSeguimiento] = @CodigoSeguimiento;
END