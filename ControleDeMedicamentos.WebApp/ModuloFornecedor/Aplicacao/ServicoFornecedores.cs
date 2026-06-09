using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;

public class ServicoFornecedores
{
    private readonly IRepositorioFornecedor repositorioFornecedor;
    private readonly IMapper mapeador;

    public ServicoFornecedores(IRepositorioFornecedor repositorioFornecedor, IMapper mapeador)
    {
        this.repositorioFornecedor = repositorioFornecedor;
        this.mapeador = mapeador;
    }

    public Result Cadastrar(FornecedorDto dto)
    {
        if (repositorioFornecedor.Registros.Any(f => string.Equals(f.CNPJ, dto.CNPJ)))
            return Falha("Nome", "Já existe um Fornecedor com esse CNPJ");

        Fornecedor novoForncedor = new(dto.Nome, dto.Telefone, dto.CNPJ);

        repositorioFornecedor.Cadastrar(novoForncedor);

        return Result.Ok();
    }

    public Result Editar(FornecedorDto dto)
    {
        if (repositorioFornecedor.Selecionar(f => f.Id != dto.Id).Any(f => string.Equals(f.CNPJ, dto.CNPJ)))
            return Falha("Nome", "Já existe um Fornecedor com esse CNPJ");

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
        List<Fornecedor> fornecedores = repositorioFornecedor.Selecionar();

        return fornecedores.Select(f => new FornecedorDto(f.Nome, f.Telefone, f.CNPJ, f.Id)).ToList();
    }

    public Result<FornecedorDto> SelecionarPorId(Guid id)
    {
        Fornecedor? f = repositorioFornecedor.Selecionar(id);

        if (f is null)
            return Result.Fail("Fornecedor não encontrado");

        return Result.Ok(new FornecedorDto(f.Nome, f.Telefone, f.CNPJ));
    }

    private Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
