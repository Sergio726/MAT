/*
----------------------------------------------------------------------------------------------------
-- Created By: Sistema MAT
-- Create date: 2026-01-XX
-- Purpose: Obtiene presupuestos activos por DNI del cliente
-- Description: Retorna presupuestos pendientes no expirados para un DNI específico
----------------------------------------------------------------------------------------------------
*/

CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_GetByDni]
(
    @DniCliente VARCHAR(50)
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
    LEFT JOIN [dbo].[Viaje] v WITH (NOLOCK)
        ON p.[ViajeId] = v.[ViajeID]
    LEFT JOIN [dbo].[Paquete] paq WITH (NOLOCK)
        ON v.[PaqueteID] = paq.[PaqueteID]
    WHERE 
        p.[DniCliente] = @DniCliente
        AND p.[Estado] = 1 -- Solo pendientes
        AND p.[FechaExpiracion] >= GETDATE() -- No expirados
    ORDER BY 
        p.[FechaCreacion] DESC;
END

GO

