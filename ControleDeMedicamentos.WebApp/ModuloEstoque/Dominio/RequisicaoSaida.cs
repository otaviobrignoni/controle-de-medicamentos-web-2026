using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

public class RequisicaoSaida : Requisicao
{
    public Paciente Paciente { get; set; } = null!;
    public ItemSaida[] Medicamentos { get; set; } = [];

    public RequisicaoSaida() { }

    public RequisicaoSaida(Paciente paciente, ItemSaida[] medicamentos)
    {
        Paciente = paciente;
        Medicamentos = medicamentos;
        foreach (ItemSaida irs in Medicamentos)
            irs.Medicamento.Quantidade -= irs.Quantidade;
    }
}
