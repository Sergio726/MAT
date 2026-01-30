CREATE TABLE [dbo].[Provider] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [BusinessName]    VARCHAR (50)  NOT NULL,
    [Phone]           VARCHAR (50)  NULL,
    [Website]         VARCHAR (50)  NULL,
    [Email]           VARCHAR (100) NULL,
    [Language]        VARCHAR (50)  NULL,
    [TaxID]           VARCHAR (50)  NULL,
    [IdTaxCondition]  INT           NULL,
    [IdPaymentMethod] INT           NULL,
    [IdLocation]      INT           NULL,
    CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Provider_Location] FOREIGN KEY ([IdLocation]) REFERENCES [dbo].[Location] ([Id]),
    CONSTRAINT [FK_Provider_PaymentMethod] FOREIGN KEY ([IdPaymentMethod]) REFERENCES [dbo].[PaymentMethod] ([Id]),
    CONSTRAINT [FK_Provider_TaxCondition] FOREIGN KEY ([IdTaxCondition]) REFERENCES [dbo].[TaxCondition] ([Id])
);

