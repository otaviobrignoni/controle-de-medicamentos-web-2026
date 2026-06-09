namespace ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
// Infraestrutura
public abstract class RepositorioBase<T> where T : EntidadeBase<T>
{
    protected ContextoJson contexto;
    public List<T> Registros { get; protected set; }

    public RepositorioBase(ContextoJson contexto)
    {
        this.contexto = contexto;
        Registros = CarregarRegistros();
    }

    protected abstract List<T> CarregarRegistros();

    public void Cadastrar(T registro)
    {
        Registros.Add(registro);

        contexto.Salvar();
    }

    public bool Editar(Guid id, T registroEditado)
    {
        T? registro = Selecionar(id);

        return Editar(registro, registroEditado);
    }

    public bool Editar(T? registro, T registroEditado)
    {
        if (registro == null)
            return false;

        registro.Atualizar(registroEditado);

        contexto.Salvar();

        return true;
    }

    public bool Excluir(Guid id)
    {
        T? registro = Selecionar(id);

        return Excluir(registro);
    }

    public bool Excluir(T? registro)
    {
        if (registro == null)
            return false;

        if (Registros.Remove(registro))
        {
            contexto.Salvar();
            return true;
        }
        
        return false;
    }

    public T? Selecionar(Guid id)
    {
        return Registros.FirstOrDefault(e => e.Id == id);
    }

    public List<T> Selecionar(Func<T, bool>? filtro = null)
    {
        if (filtro is null)
            return Registros;
        return Registros.Where(filtro).ToList();
    }
}
