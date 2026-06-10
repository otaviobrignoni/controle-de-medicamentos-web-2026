using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

public interface IRepositorioRequisicao : IRepositorio<Requisicao>
{
    public List<RequisicaoEntrada> Entrada { get; }
    public List<RequisicaoSaida> Saida { get; }
    List<T> Selecionar<T>(Func<T, bool>? filtro = null) where T : Requisicao;
    public T? Selecionar<T>(Guid id) where T : Requisicao;
}
