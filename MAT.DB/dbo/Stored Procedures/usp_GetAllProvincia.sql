CREATE PROCEDURE [dbo].[usp_GetAllProvincia]
AS
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 28-02-2026
-- Description:	Trae todas las provincias para dropdowns
-- =============================================
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT ID, Nombre
	FROM   Provincia
	ORDER  BY Nombre

END
