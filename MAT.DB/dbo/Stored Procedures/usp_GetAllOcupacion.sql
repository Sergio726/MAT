CREATE PROCEDURE [dbo].[usp_GetAllOcupacion] 
AS 
-- =============================================
-- Author:		Garcia Sergio
-- Create date: 23-10-2016
-- Description:	Trae las ocupaciones de personas
-- =============================================
BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT distinct(p.Ocupacion)
	FROM Persona p
	WHERE p.Ocupacion IS NOT NULL 
	ORDER BY p.Ocupacion

END
