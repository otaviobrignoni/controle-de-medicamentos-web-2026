using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

public class ItemSaida
{
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }

    public ItemSaida() { }

    public ItemSaida(Medicamento medicamento, int quantidade)
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
    }
}
