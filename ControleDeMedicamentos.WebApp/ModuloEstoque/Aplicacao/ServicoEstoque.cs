using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using FluentResults;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;

public class ServicoEstoque(IRepositorioRequisicao repositorioRequisicao, IRepositorioMedicamento repositorioMedicamento, IRepositorioPaciente repositorioPaciente, IRepositorioFuncionario repositorioFuncionario, IMapper mapeador)
{
    public Result CadastrarEntrada(EntradaDto dto)
    {
        var medicamento = repositorioMedicamento.Selecionar(dto.MedicamentoId);

        if (medicamento is null)
            return Falha(nameof(dto.MedicamentoId), "Medicamento não encontrado.");

        var funcionario = repositorioFuncionario.Selecionar(dto.FuncionarioId);

        if (funcionario is null)
            return Falha(nameof(dto.FuncionarioId), "Funcionário não encontrado.");

        repositorioRequisicao.Cadastrar(new RequisicaoEntrada(medicamento, dto.Quantidade, funcionario));

        return Result.Ok();
    }

    public Result CadastrarSaida(SaidaDto dto)
    {
        var paciente = repositorioPaciente.Selecionar(dto.PacienteId);

        if (paciente is null)
            return Falha(nameof(dto.PacienteId), "Paciente não encontrado.");

        if (dto.Itens.Count == 0)
            return Falha(nameof(dto.Itens), "Adicione pelo menos um medicamento.");

        var itensSaida = new List<ItemSaida>();

        foreach (var isDto in dto.Itens)
        {
            var medicamento = repositorioMedicamento.Selecionar(isDto.MedicamentoId);

            if (medicamento is null)
                return Falha(nameof(dto.Itens), "Um ou mais medicamentos não foram encontrados.");

            if (isDto.Quantidade <= 0)
                return Falha(nameof(dto.Itens), "As quantidades devem ser positivas.");

            if (isDto.Quantidade > medicamento.Quantidade)
                return Falha(nameof(dto.Itens), $"Estoque insuficiente para {medicamento.Nome}.");

            itensSaida.Add(new ItemSaida(medicamento, isDto.Quantidade));
        }

        repositorioRequisicao.Cadastrar(new RequisicaoSaida(paciente, itensSaida.ToArray()));

        return Result.Ok();
    }

    public List<MostrarEntradaDto> SelecionarEntrada()
    {
        return mapeador.Map<List<MostrarEntradaDto>>(repositorioRequisicao.Entrada);
    }

    public List<MostrarSaidaDto> SelecionarSaida()
    {
        return mapeador.Map<List<MostrarSaidaDto>>(repositorioRequisicao.Saida);
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
