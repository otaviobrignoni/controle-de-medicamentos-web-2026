namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

public interface IRepositorioRequisicao
{
    public List<Requisicao> Registros { get; }
    public List<RequisicaoEntrada> Entrada { get; }
    public List<RequisicaoSaida> Saida { get; }
    void Cadastrar(Requisicao registro);
    List<T> Selecionar<T>(Func<T, bool>? filtro = null) where T : Requisicao;
}
