using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Infra;

public class RepositorioRequisicao : RepositorioBase<Requisicao>, IRepositorioRequisicao
{
    public RepositorioRequisicao(ContextoJson contexto) : base(contexto) { }
    public List<RequisicaoEntrada> Entrada => Selecionar<RequisicaoEntrada>();
    public List<RequisicaoSaida> Saida => Selecionar<RequisicaoSaida>();

    protected override List<Requisicao> CarregarRegistros()
    {
        return contexto.Requisicoes;
    }

    public List<T> Selecionar<T>(Func<T, bool>? filtro = null) where T : Requisicao
    {
        List<T> registros = Registros.OfType<T>().ToList();

        return filtro is null ? registros : registros.Where(filtro).ToList();
    }
}
