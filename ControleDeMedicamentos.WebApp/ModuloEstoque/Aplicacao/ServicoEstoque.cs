using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;

public class ServicoEstoque(IRepositorioRequisicao repositorioRequisicao)
{
    public List<DetalhesEntradaDto> SelecionarEntrada()
    {
        return repositorioRequisicao.Entrada
            .Select(re => 
                new DetalhesEntradaDto(
                    re.Id,
                    re.Data,
                    re.Medicamento.Nome,
                    re.Quantidade,
                    re.Funcionario.Nome)
            )
            .ToList();
    }

    public List<DetalhesSaidaDto> SelecionarSaida()
    {
        return repositorioRequisicao.Saida
            .Select(rs => 
                new DetalhesSaidaDto(
                    rs.Id,
                    rs.Data,
                    rs.Paciente.Nome,
                    rs.Medicamentos.Select(irs => (irs.Medicamento.Nome, irs.Quantidade)).ToList()))
            .ToList();
    }
}
