using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;

    public Paciente()
    {
    }
    public Paciente(string nome, string telefone, string cartaoSus, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
        Cpf = cpf;
    }
    public override void Atualizar(Paciente entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CartaoSus = entidadeAtualizada.CartaoSus;
        Cpf = entidadeAtualizada.Cpf;
    }
}
