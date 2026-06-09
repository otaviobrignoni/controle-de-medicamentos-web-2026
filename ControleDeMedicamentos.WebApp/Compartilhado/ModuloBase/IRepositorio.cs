namespace ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
// Domínio
public interface IRepositorio<T> where T : EntidadeBase<T>
{
    List<T> Registros { get; }
    void Cadastrar(T registro);
    bool Editar(Guid id, T registroEditado);
    bool Editar(T? registro, T registroEditado);
    bool Excluir(Guid id);
    bool Excluir(T? registro);
    T? Selecionar(Guid id);
    List<T> Selecionar(Func<T, bool>? filtro = null);
}
