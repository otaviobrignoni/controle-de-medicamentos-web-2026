using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

public class Medicamento : EntidadeBase<Medicamento>
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Quantidade { get; set; } = 0;
    public Fornecedor Fornecedor { get; set; } = null!;

    public Medicamento()
    {
    }

    public Medicamento(string nome, string descricao, int quantidade, Fornecedor fornecedor, bool cadastraEmFornecedor = true)
    {
        Nome = nome;
        Descricao = descricao;
        Quantidade = quantidade;
        Fornecedor = fornecedor;
        if (cadastraEmFornecedor)
            fornecedor.AdicionarMedicamentoHaFornecedor(this);
    }
    public override void Atualizar(Medicamento entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Descricao = entidadeAtualizada.Descricao;
        Quantidade = entidadeAtualizada.Quantidade;
        if (Fornecedor != entidadeAtualizada.Fornecedor)
        {
            Fornecedor.RemoverMedicamentoDoFornecedor(this);

            Fornecedor = entidadeAtualizada.Fornecedor;

            entidadeAtualizada.Fornecedor.AdicionarMedicamentoHaFornecedor(this);
        }
        else
        {
            Fornecedor = entidadeAtualizada.Fornecedor;

            Fornecedor.AtualizarMedicamentoDoFornecedor(this);
        }
    }
}
