namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;

public record FornecedorDto
(
    string Nome,
    string Telefone,
    string CNPJ,
    int Medicamentos,
    Guid Id = new Guid()
);
