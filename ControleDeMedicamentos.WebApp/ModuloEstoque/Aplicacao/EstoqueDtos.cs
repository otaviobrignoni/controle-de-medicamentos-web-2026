namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;

public record EntradaDto(
    Guid MedicamentoId,
    int Quantidade,
    Guid FuncionarioId
);

public record SaidaDto(
    Guid PacienteId,
    List<ItemSaidaDto> Itens
);

public record ItemSaidaDto(
    Guid MedicamentoId,
    int Quantidade
);

public record MostrarEntradaDto(
    Guid Id,
    DateTime Data,
    string Medicamento,
    int Quantidade,
    string Funcionario
);

public record MostrarSaidaDto(
    Guid Id,
    DateTime Data,
    string Paciente,
    List<MostrarItemSaidaDto> Itens
);

public record MostrarItemSaidaDto(
    string Medicamento,
    int Quantidade
);


