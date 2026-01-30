CREATE PROCEDURE [dbo].[usp_Paquete_GetDestinoByPaqueteID]
(
	@PaqueteID varchar(36) = ''
)
AS
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 06-10-2017
-- Description:	Get Destino by PaqueteID
-- =============================================
BEGIN
	SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT p.PaqueteID, 
		   l.ID      AS LocalidadID, 
		   d.ID      AS DepartamentoID, 
		   pr.ID     AS ProvinciaID, 
		   ps.PaisID AS PaisID,
		   (ps.Descripcion + ', ' + pr.Nombre + ', ' +d.Nombre + ', '+ l.Nombre) AS Destino
	FROM   Paquete p 
		   INNER JOIN Localidad l 
				   ON p.DestinoID = l.ID 
		   INNER JOIN Departamento d 
				   ON l.idDepartamento = d.ID 
		   INNER JOIN Provincia pr 
				   ON d.idProvincia = pr.ID 
		   INNER JOIN Pais ps 
				   ON pr.IdPais = ps.PaisID 
	WHERE  p.PaqueteID = @PaqueteID 

END
