using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

public class RequisicaoSaida : Requisicao
{
    public Paciente Paciente { get; set; } = null!;
    public ItemReqSaida[] Medicamentos { get; set; } = [];

    public RequisicaoSaida() { }

    public RequisicaoSaida(Paciente paciente, ItemReqSaida[] medicamentos)
    {
        Paciente = paciente;
        Medicamentos = medicamentos;
        foreach (ItemReqSaida irs in Medicamentos)
            irs.Medicamento.Quantidade -= irs.Quantidade;
    }
}
