using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;

    public Fornecedor() { }

    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome;
        Telefone = telefone;
        CNPJ = cnpj;
    }

    public override void Atualizar(Fornecedor entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CNPJ = entidadeAtualizada.CNPJ;
    }
}
