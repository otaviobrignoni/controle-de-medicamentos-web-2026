CREATE TABLE [dbo].[TBItemSaida] (
    [RequisicaoSaidaId] UNIQUEIDENTIFIER NOT NULL,
    [MedicamentoId]     UNIQUEIDENTIFIER NOT NULL,
    [Quantidade]        INT              NOT NULL
);
GO

ALTER TABLE [dbo].[TBItemSaida]
    ADD CONSTRAINT [FK_TBItemSaida_TBMedicamento] FOREIGN KEY ([MedicamentoId]) REFERENCES [dbo].[TBMedicamento] ([Id]);
GO

ALTER TABLE [dbo].[TBItemSaida]
    ADD CONSTRAINT [FK_TBItemSaida_TBRequisicaoSaida] FOREIGN KEY ([RequisicaoSaidaId]) REFERENCES [dbo].[TBRequisicaoSaida] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[TBItemSaida]
    ADD CONSTRAINT [PK_TBMedicamentoPrescrito] PRIMARY KEY CLUSTERED ([RequisicaoSaidaId] ASC, [MedicamentoId] ASC);
GO

