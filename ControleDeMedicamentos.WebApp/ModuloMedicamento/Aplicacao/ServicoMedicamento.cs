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
            return Falha("Nome", "Já existe um medicamento com esse nome.");

        var fornecedor = repositorioFornecedor.Selecionar(dto.FornecedorId);

        if (fornecedor is null)
            return Falha("Fornecedor", "O \"Fornecedor\" e possivelmente nulo");

        var medicamento = mapeador.MapWith<Medicamento>(dto, ("fornecedor", fornecedor));
        fornecedor.AdicionarMedicamento(medicamento);

        repositorioMedicamento.Cadastrar(medicamento);

        return Result.Ok();
    }

    public Result Editar(MedicamentoDto dto)
    {
        if (repositorioMedicamento.Selecionar(m => m.Id != dto.Id).Any(f => string.Equals(f.Nome, dto.Nome)))
            return Falha("Nome", "Já existe um medicamento com esse nome.");

        var fornecedor = repositorioFornecedor.Selecionar(dto.FornecedorId);

        if (fornecedor is null)
            return Falha("Fornecedor", "Fornecedor não encontrado.");

        var medicamentoEditado = mapeador.MapWith<Medicamento>(dto, ("fornecedor", fornecedor));

        bool conseguiuEditar = repositorioMedicamento.Editar(dto.Id, medicamentoEditado);

        if (!conseguiuEditar)
            return Result.Fail("Medicamento não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.Selecionar(id);

        if (medicamento is null)
            return Result.Fail("Medicamento não encontrado.");

        medicamento.Fornecedor.RemoverMedicamento(medicamento);

        repositorioMedicamento.Excluir(medicamento);

        return Result.Ok();
    }

    public List<MostrarMedicamentoDto> Selecionar()
    {
        return mapeador.Map<List<MostrarMedicamentoDto>>(repositorioMedicamento.Registros);
    }
    public Result<TDto> Selecionar<TDto>(Guid id) where TDto : MedicamentoDtoBase<TDto>
    {
        Medicamento? medicamento = repositorioMedicamento.Selecionar(id);

        if (medicamento is null)
            return Result.Fail("Medicamento não encontrado.");

        return Result.Ok(mapeador.Map<TDto>(medicamento));
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
