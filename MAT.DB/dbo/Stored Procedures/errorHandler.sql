CREATE PROCEDURE [dbo].[errorHandler]
	@userErrorDesc VARCHAR(500) = ''
	,@enableLogging BIT = 0
AS
SET NOCOUNT ON;
BEGIN
	DECLARE @errmsg AS NVARCHAR(2048)
		,@severity AS TINYINT
		,@state AS TINYINT
		,@errno AS INT
		,@proc AS SYSNAME
		,@lineno AS INT;

	SELECT @errmsg = error_message()
		,@severity = error_severity()
		,@state = error_state()
		,@errno = error_number()
		,@proc = error_procedure()
		,@lineno = ERROR_LINE();

	IF @errno IS NULL
		RETURN;

	IF @errmsg NOT LIKE '***%'
	BEGIN
		SET @errmsg = '*** ' + COALESCE(quotename(@proc), '<dynamic SQL>') + ', Line:' + ltrim(STR(@lineno)) + '. Errno ' + ltrim(str(@errno)) + ': ' + @errmsg + CASE 
			WHEN @userErrorDesc <> ''
				AND @userErrorDesc IS NOT NULL
				THEN '. User Desc.: ' + @userErrorDesc
			ELSE ''
			END;

		RAISERROR (
			@errmsg
			,@severity
			,@state
			);

	END
	ELSE
	BEGIN
		SET @errmsg += CASE 
				WHEN @userErrorDesc <> ''
					AND @userErrorDesc IS NOT NULL
					THEN '. User Desc.: ' + @userErrorDesc
				ELSE ''
				END;

		RAISERROR (
				@errmsg
				,@severity
				,@state
				);
	END
END