using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;

public class ServicoFornecedor(IRepositorioFornecedor repositorioFornecedor, IMapper mapeador)
{
    public Result Cadastrar(FornecedorDto dto)
    {
        if (repositorioFornecedor.Registros.Any(f => string.Equals(f.Cnpj, dto.CNPJ)))
            return Falha("CNPJ", "Já existe um fornecedor com esse CNPJ.");

        var fornecedor = mapeador.Map<Fornecedor>(dto);

        repositorioFornecedor.Cadastrar(fornecedor);

        return Result.Ok();
    }

    public Result Editar(FornecedorDto dto)
    {
        if (repositorioFornecedor.Selecionar(f => f.Id != dto.Id).Any(f => string.Equals(f.Cnpj, dto.CNPJ)))
            return Falha("CNPJ", "Já existe um fornecedor com esse CNPJ.");

        var fornecedorEditado = mapeador.Map<Fornecedor>(dto);

        bool conseguiuEditar = repositorioFornecedor.Editar(dto.Id, fornecedorEditado);

        if (!conseguiuEditar)
            return Result.Fail("Fornecedor não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        var fornecedor = repositorioFornecedor.Selecionar(id);

        if (fornecedor is null)
            return Result.Fail("Fornecedor não encontrado.");

        if (fornecedor.Medicamentos.Count != 0)
            return Result.Fail("Fornecedor possui medicamentos.");

        repositorioFornecedor.Excluir(id);

        return Result.Ok();
    }

    public List<FornecedorDto> Selecionar()
    {
        return mapeador.Map<List<FornecedorDto>>(repositorioFornecedor.Registros);
    }

    public Result<FornecedorDto> Selecionar(Guid id)
    {
        var fornecedor = repositorioFornecedor.Selecionar(id);

        if (fornecedor is null)
            return Result.Fail("Fornecedor não encontrado.");

        return Result.Ok(mapeador.Map<FornecedorDto>(fornecedor));
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
