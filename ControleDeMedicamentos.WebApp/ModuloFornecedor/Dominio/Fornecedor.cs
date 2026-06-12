using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public List<Medicamento> Medicamentos { get; set; } = [];

    public Fornecedor() { }

    public Fornecedor(string nome, string telefone, string CNPJ)
    {
        Nome = nome;
        Telefone = telefone;
        this.CNPJ = CNPJ;
    }
    public override void Atualizar(Fornecedor entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CNPJ = entidadeAtualizada.CNPJ;
    }
    public void AdicionarMedicamentoHaFornecedor(Medicamento medicamento)
    {
        Medicamentos.Add(medicamento);
    }
    public void RemoverMedicamentoDoFornecedor(Medicamento medicamento)
    {
        Medicamentos.Remove(medicamento);
    }
    public void AtualizarMedicamentoDoFornecedor(Medicamento novoMedicamento)
    {
        for (int i = 0; i < Medicamentos.Count; i++)
        {
            if (novoMedicamento.Id == Medicamentos[i].Id)
                Medicamentos[i] = novoMedicamento;
        }
    }
}
