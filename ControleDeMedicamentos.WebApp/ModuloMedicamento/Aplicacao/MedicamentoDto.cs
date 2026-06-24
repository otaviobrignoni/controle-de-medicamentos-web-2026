namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

public abstract record MedicamentoDtoBase<T>
(
    string Nome,
    string Descricao,
    int Quantidade,
    Guid Id
);
public record MedicamentoDto : MedicamentoDtoBase<MedicamentoDto>
{
    public Guid FornecedorId { get; set; }
    public MedicamentoDto(string nome, string descricao, int quantidade, Guid fornecedorId, Guid id = default) : base(nome, descricao, quantidade, id)
    {
        FornecedorId = fornecedorId;
    }
}
public record MostrarMedicamentoDto : MedicamentoDtoBase<MostrarMedicamentoDto>
{
    public string FornecedorNome { get; set; }
    public MostrarMedicamentoDto(string nome, string descricao, int quantidade, string fornecedorNome, Guid id = default) : base(nome, descricao, quantidade, id)
    {
        FornecedorNome = fornecedorNome;
    }
}
