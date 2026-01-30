CREATE PROCEDURE [dbo].[usp_MAT_Nota_GetNextNroNota]
AS
-- =============================================
-- Author:    MAT
-- Create date: 2026
-- Description: Devuelve el próximo número de nota de crédito (formato NC-YYYY-NNNN)
-- =============================================
SET NOCOUNT ON;

DECLARE @Anio INT = YEAR(GETDATE());
DECLARE @Siguiente INT = 1;
DECLARE @Patron VARCHAR(20) = 'NC-' + CAST(@Anio AS VARCHAR(4)) + '-%';

SELECT @Siguiente = ISNULL(MAX(
    TRY_CAST(SUBSTRING(NroNota, 9, 10) AS INT)
), 0) + 1
FROM dbo.Nota
WHERE NroNota LIKE @Patron
  AND LEN(NroNota) >= 12
  AND SUBSTRING(NroNota, 1, 7) = 'NC-' + CAST(@Anio AS VARCHAR(4));

IF @Siguiente IS NULL
    SET @Siguiente = 1;

SELECT 'NC-' + CAST(@Anio AS VARCHAR(4)) + '-' + RIGHT('0000' + CAST(@Siguiente AS VARCHAR(10)), 4) AS NroNota;
