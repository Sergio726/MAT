CREATE PROCEDURE [dbo].[usp_MAT_Localidad_Insert]
    @IdDepartamento INT,
    @Nombre         NVARCHAR(250),
    @Id             INT OUTPUT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-01
 -- Description: Alta de localidad (NetTiers F2)
 ============================================= */
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Localidad (idDepartamento, Nombre)
    VALUES (@IdDepartamento, @Nombre);

    SET @Id = SCOPE_IDENTITY();
END
