using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;

public record MostrarEntradaViewModel(
    Guid Id,
    DateTime Data,
    string Medicamento,
    int Quantidade,
    string Funcionario
);

public record MostrarSaidaViewModel(
    Guid Id,
    DateTime Data,
    string Paciente,
    List<MostrarItemSaidaViewModel> Itens
);

public record EntradaViewModel(
    [Required(ErrorMessage = "O campo \"Medicamento\" deve ser preenchido.")]
    Guid? MedicamentoId,

    [Required(ErrorMessage = "O campo \"Quantidade\" deve ser preenchido.")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve conter um valor positivo.")]
    int Quantidade,

    [Required(ErrorMessage = "O campo \"Funcionário\" deve ser preenchido.")]
    Guid? FuncionarioId,

    [ValidateNever]
    List<SelectListItem> Medicamentos,

    [ValidateNever]
    List<SelectListItem> Funcionarios
);

public record SaidaViewModel(
    [Required(ErrorMessage = "O campo \"Paciente\" deve ser preenchido.")]
    Guid? PacienteId,

    [MinLength(1, ErrorMessage = "Adicione pelo menos um medicamento.")]
    List<ItemSaidaViewModel> Itens,

    [ValidateNever]
    List<SelectListItem> Pacientes,

    [ValidateNever]
    List<SelectListItem> Medicamentos

);

public record ItemSaidaViewModel(
    [Required(ErrorMessage = "O campo \"Medicamento\" deve ser preenchido.")]
    Guid? MedicamentoId,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve conter um valor positivo.")]
    int Quantidade
);

public record MostrarItemSaidaViewModel(
    string Medicamento,
    int Quantidade
);
