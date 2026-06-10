namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

public record CadastrarMedicamentoDto
(
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId
);
public record EditarMedicamentoDto
(
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId,
    Guid Id
);
public record DetalhesMedicamentoDto
(
    string Nome,
    string Descricao,
    int Quantidade,
    Guid FornecedorId,
    string FornecedorNome,
    string FornecedorTelefone,
    string FornecedorCnpj,
    Guid Id
);
public record ListarMedicamentoDto
(
    string Nome,
    string Descricao,
    int Quantidade,
    string FornecedorNome,
    Guid Id
);

