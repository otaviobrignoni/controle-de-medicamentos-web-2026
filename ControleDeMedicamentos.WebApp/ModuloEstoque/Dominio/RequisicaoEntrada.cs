using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

public class RequisicaoEntrada : Requisicao
{
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }
    public Funcionario Funcionario { get; set; } = null!;

    public RequisicaoEntrada() { }

    public RequisicaoEntrada(Medicamento medicamento, int quantidade, Funcionario funcionario)
    {
        Medicamento = medicamento;
        Funcionario = funcionario;
        Quantidade = quantidade;
        medicamento.Quantidade += Quantidade;
    }
}
