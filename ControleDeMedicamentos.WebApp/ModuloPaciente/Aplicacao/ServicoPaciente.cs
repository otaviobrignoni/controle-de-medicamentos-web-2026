using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;

public class ServicoPaciente
{
    readonly IRepositorioPaciente repositorioPaciente;

    public ServicoPaciente(IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioPaciente = repositorioPaciente;
    }
    public Result Cadastrar(PacienteDto dto)
    {
        if (repositorioPaciente.Registros.Any(f => string.Equals(f.CartaoSUS, dto.CartaoSUS)))
            return Falha("CartaoSUS", "Já existe um Paciente com esse cartaoSUS");

        Paciente novoPaciente = new(dto.Nome, dto.Telefone, dto.CartaoSUS, dto.CPF);

        repositorioPaciente.Cadastrar(novoPaciente);

        return Result.Ok();
    }

    public Result Editar(PacienteDto dto)
    {
        if (repositorioPaciente.Selecionar(f => f.Id != dto.Id).Any(f => string.Equals(f.CartaoSUS, dto.CartaoSUS)))
            return Falha("CartaoSUS", "Já existe um Paciente com esse cartaoSUS");

        Paciente pacienteEditado = new(dto.Nome, dto.Telefone, dto.CartaoSUS, dto.CPF);

        bool conseguiuEditar = repositorioPaciente.Editar(dto.Id, pacienteEditado);

        if (!conseguiuEditar)
            return Falha("Id", "Paciente não encontrado");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioPaciente.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Paciente não encontrado");

        return Result.Ok();
    }

    public List<PacienteDto> SelecionarTodos()
    {
        return repositorioPaciente.Selecionar().Select(f => new PacienteDto(f.Nome, f.Telefone, f.CartaoSUS, f.CPF, f.Id)).ToList();
    }

    public Result<PacienteDto> SelecionarPorId(Guid id)
    {
        Paciente? p = repositorioPaciente.Selecionar(id);

        if (p is null)
            return Result.Fail("Paciente não encontrado");

        return Result.Ok(new PacienteDto(p.Nome, p.Telefone, p.CartaoSUS, p.CPF, p.Id));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
