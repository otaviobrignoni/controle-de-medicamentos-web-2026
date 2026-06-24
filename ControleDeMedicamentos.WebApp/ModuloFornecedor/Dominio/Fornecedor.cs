using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public List<Medicamento> Medicamentos { get; set; } = [];

    public Fornecedor() { }

    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome;
        Telefone = telefone;
        Cnpj = cnpj;
    }
    public override void Atualizar(Fornecedor entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        Cnpj = entidadeAtualizada.Cnpj;
    }
    public void AdicionarMedicamento(Medicamento medicamento)
    {
        Medicamentos.Add(medicamento);
    }
    public void RemoverMedicamento(Medicamento medicamento)
    {
        Medicamentos.Remove(medicamento);
    }
}
