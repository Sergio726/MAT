CREATE TABLE [dbo].[Voucher] (
    [VoucherID]    UNIQUEIDENTIFIER CONSTRAINT [DF_Voucher_VoucherID] DEFAULT (newid()) NOT NULL,
    [NroVoucher]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [FechaEmision] DATETIME         NULL,
    [VendedorID]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED ([VoucherID] ASC),
    CONSTRAINT [FK_Voucher_Vendedor] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Vendedor] ([VendedorID]),
    CONSTRAINT [FK_Voucher_Voucher] FOREIGN KEY ([VoucherID]) REFERENCES [dbo].[Voucher] ([VoucherID])
);



