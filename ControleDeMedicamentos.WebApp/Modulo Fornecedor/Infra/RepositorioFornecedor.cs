using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
using ControleDeMedicamentos.WebApp.Modulo_Fornecedor.Dominio;

namespace ControleDeMedicamentos.WebApp.Modulo_Fornecedor.Infra;

public class RepositorioFornecedor : RepositorioBase<Fornecedor>, IRepositorioFornecedor
{
    public RepositorioFornecedor(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Fornecedor> CarregarRegistros()
    {
        return contexto.Fornecedores;
    }
}
