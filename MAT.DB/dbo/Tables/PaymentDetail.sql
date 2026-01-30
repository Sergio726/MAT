CREATE TABLE [dbo].[PaymentDetail] (
    [Id]                UNIQUEIDENTIFIER CONSTRAINT [DF_PaymentDetail_Id] DEFAULT (newid()) NOT NULL,
    [PaymentId]         UNIQUEIDENTIFIER NULL,
    [ExternalPaymentId] VARCHAR (100)    NOT NULL,
    [Status]            VARCHAR (100)    NULL,
    [StatusDetail]      VARCHAR (100)    NULL,
    [DateApproved]      VARCHAR (50)     NULL,
    [DateLastUpdated]   VARCHAR (50)     NULL,
    [GeneratedOn]       DATETIME         CONSTRAINT [DF_PaymentDetail_GeneratedOn] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PaymentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentDetail_Payment] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[Payment] ([Id])
);

