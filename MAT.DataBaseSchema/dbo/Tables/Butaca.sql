CREATE TABLE [dbo].[Butaca] (
    [ButacaID]     UNIQUEIDENTIFIER CONSTRAINT [DF_Table_1_VoucherID] DEFAULT (newid()) NOT NULL,
    [NroButaca]    INT              NULL,
    [Piso]         INT              NULL,
    [Ubicacion]    INT              NULL,
    [Tipo]         INT              NULL,
    [TransporteID] UNIQUEIDENTIFIER NULL,
    [Fila]         VARCHAR (2)      NULL,
    [Posicion]     VARCHAR (1)      NULL,
    [CodigoButaca] VARCHAR (4)      NULL,
    CONSTRAINT [PK_Butaca] PRIMARY KEY CLUSTERED ([ButacaID] ASC),
    CONSTRAINT [FK_Butaca_Transporte] FOREIGN KEY ([TransporteID]) REFERENCES [dbo].[Transporte] ([TransporteID])
);



