namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;

public record PacienteDto
(
    string Nome,
    string Telefone,
    string CartaoSUS,
    string CPF,
    Guid Id = default
);
