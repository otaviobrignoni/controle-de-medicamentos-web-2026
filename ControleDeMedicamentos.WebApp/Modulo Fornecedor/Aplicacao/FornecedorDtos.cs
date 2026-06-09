namespace ControleDeMedicamentos.WebApp.Modulo_Fornecedor.Aplicacao;

public record FornecedorDto
(
    string Nome,
    string Telefone,
    string CNPJ,
    Guid Id = new Guid()
);
