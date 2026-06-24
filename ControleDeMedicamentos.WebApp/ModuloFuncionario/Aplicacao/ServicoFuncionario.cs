using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;

public class ServicoFuncionario(IRepositorioFuncionario repositorioFuncionario, IMapper mapeador)
{
    public Result Cadastrar(FuncionarioDto dto)
    {
        if (CpfJaExiste(dto.Cpf))
            return Falha("Cpf", "Já existe um funcionário com este CPF.");

        var funcionario = mapeador.Map<Funcionario>(dto);

        repositorioFuncionario.Cadastrar(funcionario);

        return Result.Ok();
    }

    public Result Editar(FuncionarioDto dto)
    {
        if (CpfJaExiste(dto.Cpf, dto.Id))
            return Falha("Cpf", "Já existe um funcionário com este CPF.");

        var funcionarioEditado = mapeador.Map<Funcionario>(dto);

        if (!repositorioFuncionario.Editar(dto.Id, funcionarioEditado))
            return Result.Fail("Funcionario não encontrado");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        var funcionario = repositorioFuncionario.Selecionar(id);

        if (funcionario is null)
            return Result.Fail("Funcionario não encontrado.");

        repositorioFuncionario.Excluir(id);

        return Result.Ok();
    }

    public List<FuncionarioDto> Selecionar()
    {
        return mapeador.Map<List<FuncionarioDto>>(repositorioFuncionario.Registros);
    }

    public Result<FuncionarioDto> Selecionar(Guid id)
    {
        var funcionario = repositorioFuncionario.Selecionar(id);

        if (funcionario is null)
            return Result.Fail("Funcionario não encontrado.");

        return Result.Ok(mapeador.Map<FuncionarioDto>(funcionario));
    }

    private bool CpfJaExiste(string cpf, Guid id = default)
    {
        return repositorioFuncionario.Registros.Any(f => f.Cpf.Equals(cpf) && f.Id != id);
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
