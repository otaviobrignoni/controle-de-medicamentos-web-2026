using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CartaoSUS { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;

    public Paciente()
    {
    }
    public Paciente
    (
        string nome,
        string telefone,
        string cartaoSUS,
        string CPF
    )
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaoSUS;
        this.CPF = CPF;
    }
    public override void Atualizar(Paciente entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CartaoSUS = entidadeAtualizada.CartaoSUS;
        CPF = entidadeAtualizada.CPF;
    }
}
