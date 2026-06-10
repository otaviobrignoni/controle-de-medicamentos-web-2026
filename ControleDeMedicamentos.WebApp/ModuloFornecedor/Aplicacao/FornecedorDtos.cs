using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;

public record FornecedorDto
(
    string Nome,
    string Telefone,
    string CNPJ,
    int QtdMedicamentos,
    Guid Id = new Guid()
);
