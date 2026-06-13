using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;

public record MedicamentoViewModel
(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido")]
    [StringLength(100, MinimumLength = 3, ErrorMessage =" O campo \"Nome\" deve conter entre 3 a 100 digitos")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido")]
    [StringLength(255, MinimumLength = 5, ErrorMessage =" O campo \"Descrição\" deve conter entre 5 a 255 digitos")]
    string Descricao,

    int Quantidade,

    [Required(ErrorMessage = "O campo \"Fornecedor\" deve ser preenchido")]
    Guid? FornecedorId,

    [ValidateNever]
    List<SelectListItem> Fornecedores,

    Guid Id = default
);

public record MedicamentoMostrarViewModel
(
    string Nome,

    string Descricao,

    int Quantidade,

    string FornecedorNome,

    Guid Id = default
);


