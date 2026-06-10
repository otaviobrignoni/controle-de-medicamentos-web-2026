using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

public class ItemReqSaida
{
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }

    public ItemReqSaida() { }

    public ItemReqSaida(Medicamento medicamento, int quantidade)
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
    }
}
