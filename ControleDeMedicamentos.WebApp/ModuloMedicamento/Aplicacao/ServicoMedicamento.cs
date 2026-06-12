using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

public class ServicoMedicamento
{
    readonly IRepositorioFornecedor repositorioFornecedor;
    readonly IRepositorioMedicamento repositorioMedicamento;
    public ServicoMedicamento(IRepositorioFornecedor repositorioFornecedor, IRepositorioMedicamento repositorioMedicamento)
    {
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
    }

    public Result Cadastrar(CadastrarMedicamentoDto dto)
    {
        if (repositorioMedicamento.Registros.Any(f => string.Equals(f.Nome, dto.Nome)))
            return Falha("Nome", "Esse medicamento já existe no programa");

        Fornecedor? fornecedor = repositorioFornecedor.Selecionar(dto.FornecedorId);

        if (fornecedor is null)
            return Falha("Fornecedor", "O \"Fornecedor\" e possivelmente nulo");

        Medicamento novoMedicamento = new(dto.Nome, dto.Descricao, dto.Quantidade, fornecedor);

        repositorioMedicamento.Cadastrar(novoMedicamento);

        return Result.Ok();
    }

    public Result Editar(EditarMedicamentoDto dto)
    {
        if (repositorioMedicamento.Selecionar(m => m.Id != dto.Id).Any(f => string.Equals(f.Nome, dto.Nome)))
            return Falha("Nome", "Já existe um Paciente com esse cartaoSUS");

        Fornecedor? fornecedor = repositorioFornecedor.Selecionar(dto.FornecedorId);

        if (fornecedor is null)
            return Falha("Fornecedor", "O \"Fornecedor\" e possivelmente nulo");

        Medicamento pacienteEditado = new(dto.Nome, dto.Descricao, dto.Quantidade, fornecedor, false);

        bool conseguiuEditar = repositorioMedicamento.Editar(dto.Id, pacienteEditado);

        if (!conseguiuEditar)
            return Result.Fail("Ocorreu um erro ao Medicamento ao ser editado");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.Selecionar(id);

        if (medicamento is null)
            return Result.Fail("O medicamento não foi encontrado");

        medicamento.Fornecedor.RemoverMedicamentoDoFornecedor(medicamento);

        repositorioMedicamento.Excluir(medicamento);

        return Result.Ok();
    }

    public List<ListarMedicamentoDto> SelecionarTodosListagem()
    {
        return repositorioMedicamento.Selecionar().Select(m => new ListarMedicamentoDto(m.Nome, m.Descricao, m.Quantidade,
                                                                                        m.Fornecedor.Nome, m.Id)).ToList();
    }
    public Result<DetalhesMedicamentoDto> SelecionarPorId(Guid id)
    {
        Medicamento? m = repositorioMedicamento.Selecionar(id);

        if (m is null)
            return Result.Fail("O medicamento possivelmente e nulo");

        return Result.Ok(new DetalhesMedicamentoDto(m.Nome, m.Descricao, m.Quantidade, m.Fornecedor.Id,
                                                 m.Fornecedor.Nome, m.Fornecedor.Telefone, m.Fornecedor.CNPJ, m.Id));
    }
    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
