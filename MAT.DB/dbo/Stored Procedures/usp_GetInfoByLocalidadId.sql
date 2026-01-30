
CREATE PROCEDURE [dbo].[usp_GetInfoByLocalidadId]
(
	@idLocalidad int
)
AS
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 22-10-2016
-- Description:	Trae informacion Ids, segun la localidad
-- =============================================
BEGIN
	SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	
	SELECT l.ID IdLocalidad,
		   d.ID IdDepartamento,
		   p.ID IdProvincia,
		   ps.PaisID IdPais
	FROM Localidad l
	INNER JOIN Departamento d ON l.idDepartamento = d.ID
	INNER JOIN Provincia p ON d.idProvincia = p.ID
	INNER JOIN Pais ps ON p.IdPais = ps.PaisID
	WHERE l.ID = @idLocalidad

END
