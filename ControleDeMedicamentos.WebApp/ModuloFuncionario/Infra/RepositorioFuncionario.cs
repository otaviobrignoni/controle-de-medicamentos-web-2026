using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Infra;

public class RepositorioFuncionario : RepositorioBase<Funcionario>, IRepositorioFuncionario
{
    public RepositorioFuncionario(ContextoJson contexto) : base(contexto) { }

    protected override List<Funcionario> CarregarRegistros()
    {
        return contexto.Funcionarios;
    }
}
