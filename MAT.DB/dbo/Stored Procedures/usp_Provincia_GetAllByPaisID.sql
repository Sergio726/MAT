CREATE PROCEDURE [dbo].[usp_Provincia_GetAllByPaisID]
(
	@PaisID varchar(36) = ''
)
AS
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 06-05-2017
-- Description:	Get Provincia by PaisID
-- =============================================
BEGIN
	SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT p.ID, 
		   p.IdPais, 
		   p.Nombre 
	FROM   Provincia p 
	WHERE  IdPais = @PaisID 
	ORDER  BY p.Nombre 
END

