using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Apresentacao;

public record PacienteViewModel
(
    [Required(ErrorMessage ="O campo \"Nome\" deve ser preenchido")]
    [StringLength(100, MinimumLength =3, ErrorMessage = "O campo \"Nome\" deve ter de 3 a 100 caracteres")]
    string Nome,

    [Required(ErrorMessage ="O campo \"Telefone\" deve ser preenchido")]
    [RegularExpression(@"^\(\d{2}\)\s(9?\d{4})-\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar no formato (XX) XXXX-XXXX ou (XX) 9XXXX-XXXX)")]
    string Telefone,

    [Required(ErrorMessage ="O campo \"cartão do SUS\" deve ser preenchido")]
    [RegularExpression(@"^\d{15}$",ErrorMessage = "O campo \"cartão do SUS\" deve conter 15 dígitos")]
    string CartaoSUS,

    [Required(ErrorMessage ="O campo \"CPF\" deve ser preenchido")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$",ErrorMessage = "O campo \"CPF\" deve estar no formato xxx.xxx.xxx-xx")]
    string CPF,

    Guid Id = new Guid()
);
