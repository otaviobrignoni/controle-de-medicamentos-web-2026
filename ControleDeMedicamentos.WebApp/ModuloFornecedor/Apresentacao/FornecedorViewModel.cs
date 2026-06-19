using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;

public record FornecedorViewModel
(
    [Required(ErrorMessage ="O campo \"Nome\" deve ser preenchido")]
    [StringLength(100, MinimumLength =3, ErrorMessage = "O campo \"Nome\" deve ter de 3 a 100 caracteres")]
    string Nome,

    [Required(ErrorMessage ="O campo \"Telefone\" deve ser preenchido")]
    [RegularExpression(@"^\(\d{2}\)\s(9?\d{4})-\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar no formato (XX) XXXX-XXXX ou (XX) 9XXXX-XXXX)")]
    string Telefone,

    [Required(ErrorMessage ="O campo \"CNPJ\" deve ser preenchido")]
    [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$",ErrorMessage = "O campo \"CNPJ\" deve estar no formato xx.xxx.xxx/xxxx-xx")]
    string CNPJ,

    int Medicamentos,

    Guid Id = default
);
