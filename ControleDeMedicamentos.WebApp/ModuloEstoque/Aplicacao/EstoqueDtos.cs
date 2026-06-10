namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;

public record DetalhesEntradaDto(
    Guid Id,
    DateTime Data,
    string Medicamento,
    int Quantidade,
    string Funcionario
);
public record DetalhesSaidaDto(
    Guid Id,
    DateTime Data,
    string Paciente,
    List<(string Medicamento, int Quantidade)> Itens
);
