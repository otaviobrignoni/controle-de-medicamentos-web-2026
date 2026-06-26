using ControleDeMedicamentos.WebApp.Compartilhado.Infrastructure;
using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Infra;

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
