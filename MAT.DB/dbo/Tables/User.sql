CREATE TABLE [dbo].[User] (
    [id]         INT             IDENTITY (1, 1) NOT NULL,
    [email]      NVARCHAR (500)  NOT NULL,
    [password]   NVARCHAR (1000) NULL,
    [googleId]   VARCHAR (200)   NULL,
    [facebookId] VARCHAR (200)   NULL,
    [name]       NVARCHAR (1000) NULL,
    [image]      NVARCHAR (1000) NULL,
    [dni]        VARCHAR (12)    NULL,
    [telefono]   NVARCHAR (1000) NULL,
    [dob]        DATE            NULL,
    [role]       NVARCHAR (1000) CONSTRAINT [User_role_df] DEFAULT ('CUSTOMER') NOT NULL,
    [createdAt]  DATETIME2 (7)   CONSTRAINT [User_createdAt_df] DEFAULT (getdate()) NOT NULL,
    [updatedAt]  DATETIME2 (7)   NOT NULL,
    [lastname]   NVARCHAR (1000) NULL,
    CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_User_dni]
    ON [dbo].[User]([dni] ASC) WHERE ([dni] IS NOT NULL);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_User_email]
    ON [dbo].[User]([email] ASC) WHERE ([email] IS NOT NULL);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_User_facebookId]
    ON [dbo].[User]([facebookId] ASC) WHERE ([facebookId] IS NOT NULL);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_User_googleId]
    ON [dbo].[User]([googleId] ASC) WHERE ([googleId] IS NOT NULL);

