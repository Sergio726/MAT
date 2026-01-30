Create PROCEDURE [dbo].[usp_Localidad_GetByIdDepartamento]
(
	@IdDepartamento int = 0
)
AS
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 22-10-2016
-- Description:	Busca localidades de un departamento
-- =============================================
BEGIN
	SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT   L.ID, 
		     L.Nombre
	FROM    dbo.Localidad L
		INNER JOIN	dbo.Departamento D ON D.ID = L.idDepartamento
	WHERE (
			D.ID = @IdDepartamento
			OR
			@IdDepartamento = 0
		  )
	ORDER BY L.Nombre
END



