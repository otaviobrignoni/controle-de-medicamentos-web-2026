using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    List<ItemSaidaViewModel> Itens
);

public record CadastrarEntradaViewModel(
    [Required(ErrorMessage = "O campo \"Medicamento\" deve ser preenchido.")]
    Guid MedicamentoId,

    [Required(ErrorMessage = "O campo \"Medicamento\" deve ser preenchido.")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve conter um valor positivo.")]
    int Quantidade,

    [Required(ErrorMessage = "O campo \"Funcionário\" deve ser preenchido.")]
    Guid FuncionarioId,

    [ValidateNever]
    List<SelectListItem> Medicamentos,

    [ValidateNever]
    List<SelectListItem> Funcionarios
);

public record CadastrarSaidaViewModel(
    [Required(ErrorMessage = "O campo \"Paciente\" deve ser preenchido.")]
    Guid PacienteId,

    [MinLength(1, ErrorMessage = "Adicione pelo menos um medicamento.")]
    List<CadastrarItemSaidaViewModel> Itens,

    [ValidateNever]
    List<SelectListItem> Pacientes,

    [ValidateNever]
    List<SelectListItem> Medicamentos

);

public record CadastrarItemSaidaViewModel(
    Guid MedicamentoId,

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser positiva.")]
    int Quantidade
);

public record ItemSaidaViewModel(
    string Medicamento,
    int Quantidade
);
