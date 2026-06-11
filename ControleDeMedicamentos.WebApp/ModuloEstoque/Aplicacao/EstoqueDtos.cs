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
    List<ItemSaidaDto> Itens
);

public record CadastrarEntradaDto(
    Guid MedicamentoId,
    int Quantidade,
    Guid FuncionarioId
);

public record CadastrarSaidaDto(
    Guid PacienteId,
    List<CadastrarItemSaidaDto> Itens
);

public record ItemSaidaDto(
    string Medicamento,
    int Quantidade
);

public record CadastrarItemSaidaDto(
    Guid MedicamentoId,
    int Quantidade
);
