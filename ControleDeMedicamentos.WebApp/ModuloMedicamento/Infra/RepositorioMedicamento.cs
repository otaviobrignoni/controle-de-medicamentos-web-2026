using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Infra;

public class RepositorioMedicamento : RepositorioBase<Medicamento>, IRepositorioMedicamento
{
    public RepositorioMedicamento(ContextoJson contexto) : base(contexto)
    {
    }
    protected override List<Medicamento> CarregarRegistros()
    {
        return contexto.Medicamentos;
    }

}
