namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

public record MedicamentoDto
(
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId,
    Guid Id = default
);

public record MostrarMedicamentoDto
(
    string Nome,
    string Descricao,
    int Quantidade,
    string FornecedorNome,
    Guid Id
);
