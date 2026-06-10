namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;

public record FuncionarioDto(
    string Nome,
    string Telefone,
    string Cpf,
    Guid Id = default
);
