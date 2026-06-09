namespace ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
// Domínio
public abstract class EntidadeBase<T> where T : EntidadeBase<T>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public abstract void Atualizar(T entidadeAtualizada);
}
