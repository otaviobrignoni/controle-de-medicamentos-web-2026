using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;

public class ServicoPaciente(IRepositorioPaciente repositorioPaciente, IMapper mapeador)
{
    public Result Cadastrar(PacienteDto dto)
    {
        if (repositorioPaciente.Registros.Any(f => string.Equals(f.CartaoSus, dto.CartaoSUS)))
            return Falha("CartaoSUS", "Já existe um paciente com esse cartão SUS.");

        Paciente novoPaciente = mapeador.Map<Paciente>(dto);

        repositorioPaciente.Cadastrar(novoPaciente);

        return Result.Ok();
    }

    public Result Editar(PacienteDto dto)
    {
        if (repositorioPaciente.Selecionar(f => f.Id != dto.Id).Any(f => string.Equals(f.CartaoSus, dto.CartaoSUS)))
            return Falha("CartaoSUS", "Já existe um paciente com esse cartão SUS.");

        Paciente pacienteEditado = mapeador.Map<Paciente>(dto);

        bool conseguiuEditar = repositorioPaciente.Editar(dto.Id, pacienteEditado);

        if (!conseguiuEditar)
            return Falha("Id", "Paciente não encontrado");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        var paciente = repositorioPaciente.Selecionar(id);

        if (paciente is null)
            return Result.Fail("Paciente não encontrado");

        repositorioPaciente.Excluir(id);

        return Result.Ok();
    }

    public List<PacienteDto> Selecionar()
    {
        return mapeador.Map<List<PacienteDto>>(repositorioPaciente.Registros);
    }

    public Result<PacienteDto> Selecionar(Guid id)
    {
        var paciente = repositorioPaciente.Selecionar(id);

        if (paciente is null)
            return Result.Fail("Paciente não encontrado");

        return Result.Ok(mapeador.Map<PacienteDto>(paciente));
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
