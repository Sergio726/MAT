CREATE PROCEDURE [dbo].[usp_Localidad_GetByIdProvincia]
(
	@IdProvincia int = 0
)
AS
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 02-10-2016
-- Description:	Busca localidades de una provincia
-- =============================================
BEGIN
	SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT   L.ID, 
		     L.Nombre
	FROM    dbo.Localidad L
		INNER JOIN	dbo.Departamento D ON D.ID = L.idDepartamento
		INNER JOIN	dbo.Provincia P ON P.ID = D.idProvincia
	WHERE (
			P.ID = @IdProvincia
			OR
			@IdProvincia = 0
		  )
	ORDER BY L.Nombre
END


