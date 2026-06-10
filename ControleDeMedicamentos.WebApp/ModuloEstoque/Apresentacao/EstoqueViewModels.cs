namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;

public record DetalhesEntradaViewModel(
    Guid Id,
    DateTime Data,
    string Medicamento,
    int Quantidade,
    string Funcionario
);
public record DetalhesSaidaViewModel(
    Guid Id,
    DateTime Data,
    string Paciente,
    List<(string Medicamento, int Quantidade)> Itens
);
