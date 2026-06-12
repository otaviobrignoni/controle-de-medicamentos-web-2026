using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

public class ServicoMedicamento(IRepositorioFornecedor repositorioFornecedor, IRepositorioMedicamento repositorioMedicamento, IMapper mapeador)
{
    public Result Cadastrar(MedicamentoDto dto)
    {
        if (repositorioMedicamento.Registros.Any(f => string.Equals(f.Nome, dto.Nome)))
            return Falha("Nome", "Esse medicamento já existe no programa");

        Fornecedor? fornecedor = repositorioFornecedor.Selecionar(dto.FornecedorId);

        if (fornecedor is null)
            return Falha("Fornecedor", "O \"Fornecedor\" e possivelmente nulo");

        Medicamento novoMedicamento = mapeador.MapWith<Medicamento>(dto, ("fornecedor", fornecedor));
        fornecedor.AdicionarMedicamento(novoMedicamento);

        repositorioMedicamento.Cadastrar(novoMedicamento);

        return Result.Ok();
    }

    public Result Editar(MedicamentoDto dto)
    {
        if (repositorioMedicamento.Selecionar(m => m.Id != dto.Id).Any(f => string.Equals(f.Nome, dto.Nome)))
            return Falha("Nome", "Já existe um medicamento com esse nome");

        Fornecedor? fornecedor = repositorioFornecedor.Selecionar(dto.FornecedorId);

        if (fornecedor is null)
            return Falha("Fornecedor", "O \"Fornecedor\" e possivelmente nulo");

        Medicamento medicamentoEditado = mapeador.MapWith<Medicamento>(dto, ("fornecedor", fornecedor));

        bool conseguiuEditar = repositorioMedicamento.Editar(dto.Id, medicamentoEditado);

        if (!conseguiuEditar)
            return Result.Fail("Ocorreu um erro ao Medicamento ao ser editado");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.Selecionar(id);

        if (medicamento is null)
            return Result.Fail("O medicamento não foi encontrado");

        medicamento.Fornecedor.RemoverMedicamento(medicamento);

        repositorioMedicamento.Excluir(medicamento);

        return Result.Ok();
    }

    public List<MostrarMedicamentoDto> SelecionarTodosListagem()
    {
        return repositorioMedicamento.Registros.Select(mapeador.Map<MostrarMedicamentoDto>).ToList();
    }
    public Result<MedicamentoDto> SelecionarPorId(Guid id)
    {
        Medicamento? m = repositorioMedicamento.Selecionar(id);

        if (m is null)
            return Result.Fail("O medicamento possivelmente e nulo");

        return Result.Ok(mapeador.Map<MedicamentoDto>(m));
    }

    public Result<MostrarMedicamentoDto> SelecionarMostrar(Guid id)
    {
        Medicamento? m = repositorioMedicamento.Selecionar(id);

        if (m is null)
            return Result.Fail("O medicamento possivelmente e nulo");

        return Result.Ok(mapeador.Map<MostrarMedicamentoDto>(m));
    }
    
    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
