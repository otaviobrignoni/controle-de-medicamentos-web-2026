using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;

public class ServicoFornecedor
{
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoFornecedor(IRepositorioFornecedor repositorioFornecedor, IMapper mapeador)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public Result Cadastrar(FornecedorDto dto)
    {
        if (repositorioFornecedor.Registros.Any(f => string.Equals(f.CNPJ, dto.CNPJ)))
            return Falha("CNPJ", "Já existe um Fornecedor com esse CNPJ");

        Fornecedor novoForncedor = new(dto.Nome, dto.Telefone, dto.CNPJ);

        repositorioFornecedor.Cadastrar(novoForncedor);

        return Result.Ok();
    }

    public Result Editar(FornecedorDto dto)
    {
        if (repositorioFornecedor.Selecionar(f => f.Id != dto.Id).Any(f => string.Equals(f.CNPJ, dto.CNPJ)))
            return Falha("CNPJ", "Já existe um Fornecedor com esse CNPJ");

        Fornecedor fornecedorEditado = new(dto.Nome, dto.Telefone, dto.CNPJ);

        bool conseguiuEditar = repositorioFornecedor.Editar(dto.Id, fornecedorEditado);

        if (!conseguiuEditar)
            return Result.Fail("Fornecedor não encontrado");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioFornecedor.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Fornecedor não encontrado");

        return Result.Ok();
    }

    public List<FornecedorDto> SelecionarTodos()
    {
        return repositorioFornecedor.Selecionar().Select(f => new FornecedorDto(f.Nome, f.Telefone, f.CNPJ, f.Id)).ToList();
    }

    public Result<FornecedorDto> SelecionarPorId(Guid id)
    {
        Fornecedor? f = repositorioFornecedor.Selecionar(id);

        if (f is null)
            return Result.Fail("Fornecedor não encontrado");

        return Result.Ok(new FornecedorDto(f.Nome, f.Telefone, f.CNPJ, f.Id));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
