using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Infra;

public class RepositorioPaciente : RepositorioBase<Paciente>, IRepositorioPaciente
{
    public RepositorioPaciente(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Paciente> CarregarRegistros()
    {
        return contexto.Pacientes;
    }
}
