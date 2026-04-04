/*
  Migración: dbo.Split — función tabla-valued para división de strings por delimitador.
  Dependencia de: usp_MAT_PersonaCliente_GetTop (búsqueda por palabras).
  Paridad con MAT.DB\dbo\Functions\Split.sql
  Ejecutar contra la base configurada en MAT.Data.ConnectionString (p. ej. MAT.Intranet).

  Nota: las funciones TABLE-VALUED no admiten CREATE OR ALTER; se usa el patrón IF EXISTS DROP / CREATE.
*/
SET NOCOUNT ON;
GO

IF OBJECT_ID(N'dbo.Split', N'TF') IS NOT NULL
    DROP FUNCTION [dbo].[Split];
GO

CREATE FUNCTION [dbo].[Split] (
      @InputString  VARCHAR(MAX),
      @Delimiter    VARCHAR(50)
)
RETURNS @Items TABLE (
      Item VARCHAR(8000)
)
AS
BEGIN
      IF @Delimiter = ' '
      BEGIN
            SET @Delimiter = ','
            SET @InputString = REPLACE(@InputString, ' ', @Delimiter)
      END

      IF (@Delimiter IS NULL OR @Delimiter = '')
            SET @Delimiter = ','

      DECLARE @Item       VARCHAR(8000)
      DECLARE @ItemList   VARCHAR(8000)
      DECLARE @DelimIndex INT

      SET @ItemList   = @InputString
      SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)

      WHILE (@DelimIndex != 0)
      BEGIN
            SET @Item = SUBSTRING(@ItemList, 0, @DelimIndex)
            INSERT INTO @Items VALUES (@Item)

            SET @ItemList   = SUBSTRING(@ItemList, @DelimIndex + 1, LEN(@ItemList) - @DelimIndex)
            SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      END

      IF @Item IS NOT NULL
      BEGIN
            SET @Item = @ItemList
            INSERT INTO @Items VALUES (@Item)
      END
      ELSE
            INSERT INTO @Items VALUES (@InputString)

      RETURN
END
GO
