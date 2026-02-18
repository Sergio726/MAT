CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_ProximosVencer]
(
    @HorasVentana INT = 24
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Created By: Seba Garcia
-- Create date: 2026-02-15
-- Purpose: Presupuestos pendientes que expiran en las proximas N horas
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.[PresupuestoID],
        p.[CodigoSeguimiento],
        p.[DniCliente],
        p.[NombreCliente],
        p.[MontoPactado],
        p.[FechaExpiracion],
        v.[Descripcion] AS VendedorOrigenNombre,
        DATEDIFF(MINUTE, GETDATE(), p.[FechaExpiracion]) AS MinutosRestantes
    FROM [dbo].[Presupuesto] p
    INNER JOIN [dbo].[Vendedor] v ON p.[VendedorIdOrigen] = v.[VendedorID]
    WHERE 
        p.[Estado] = 1
        AND p.[FacturaId] IS NULL
        AND p.[FechaExpiracion] > GETDATE()
        AND p.[FechaExpiracion] <= DATEADD(HOUR, @HorasVentana, GETDATE())
    ORDER BY p.[FechaExpiracion] ASC;
END
