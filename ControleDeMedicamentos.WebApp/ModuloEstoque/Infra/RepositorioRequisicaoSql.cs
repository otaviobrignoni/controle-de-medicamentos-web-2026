using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using Dapper;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Infra;

public sealed class RepositorioRequisicaoSql(ISqlConnectionFactory connectionFactory, IMapper mapeador) : IRepositorioRequisicao
{
    public List<Requisicao> Registros => throw new NotImplementedException();

    public List<RequisicaoEntrada> Entrada => Selecionar<RequisicaoEntrada>();

    public List<RequisicaoSaida> Saida => Selecionar<RequisicaoSaida>();

    public void Cadastrar(Requisicao registro)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        using SqlTransaction transacao = conexao.BeginTransaction();

        try
        {
            string sqlQueryBase = """
                INSERT INTO dbo.TBRequisicao (Id, Data)
                VALUES (@Id, @Data);
            """;

            conexao.Execute(sqlQueryBase, registro, transacao);

            if (registro is RequisicaoEntrada requisicaoEntrada)
            {
                string sqlQueryEntrada = """
                    INSERT INTO dbo.TBRequisicaoEntrada (Id, FuncionarioId, MedicamentoId, Quantidade)
                    VALUES (@Id, @FuncionarioId, @MedicamentoId, @Quantidade);
                """;

                var objEntrada = new { requisicaoEntrada.Id, FuncionarioId = requisicaoEntrada.Funcionario.Id, MedicamentoId = requisicaoEntrada.Medicamento.Id, requisicaoEntrada.Quantidade };

                conexao.Execute(sqlQueryEntrada, objEntrada, transacao);
            }
            else if (registro is RequisicaoSaida requisicaoSaida)
            {
                string sqlQuerySaida = """
                    INSERT INTO dbo.TBRequisicaoSaida (Id, PacienteId)
                    VALUES (@Id, @PacienteId);
                """;

                var objSaida = new { requisicaoSaida.Id, PacienteId = requisicaoSaida.Paciente.Id };

                conexao.Execute(sqlQuerySaida, objSaida, transacao);

                foreach (var item in requisicaoSaida.Medicamentos)
                {
                    string sqlQueryItem = """
                        INSERT INTO dbo.TBItemSaida (RequisicaoSaidaId, MedicamentoId, Quantidade)
                        VALUES (@RequisicaoSaidaId, @MedicamentoId, @Quantidade);
                    """;

                    var objItem = new { RequisicaoSaidaId = requisicaoSaida.Id, MedicamentoId = item.Medicamento.Id, item.Quantidade };

                    conexao.Execute(sqlQueryItem, objItem, transacao);
                }
            }
            transacao.Commit();
        }
        catch
        {
            transacao.Rollback();
            throw;
        }
    }

    public List<T> Selecionar<T>(Func<T, bool>? filtro = null) where T : Requisicao
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        List<T> resultado;

        if (typeof(T) == typeof(RequisicaoEntrada))
        {
            string sqlQueryEntrada = """
                SELECT
                    re.Id AS RequisicaoId,
                    r.Data,
                    func.Id AS FuncionarioId,
                    func.Nome AS FuncionarioNome,
                    func.Telefone AS FuncionarioTelefone,
                    func.Cpf AS FuncionarioCpf,
                    m.Id AS MedicamentoId,
                    m.Nome AS MedicamentoNome,
                    m.Descricao AS MedicamentoDescricao,
                    f.Id AS FornecedorId,
                    f.Nome AS FornecedorNome,
                    f.Telefone AS FornecedorTelefone,
                    f.Cnpj AS FornecedorCnpj,
                    re.Quantidade
                FROM dbo.TBRequisicaoEntrada AS re
                JOIN dbo.TBRequisicao AS r
                    ON r.Id = re.Id
                JOIN dbo.TBFuncionario AS func
                    ON func.Id = re.FuncionarioId
                JOIN dbo.TBMedicamento AS m
                    ON m.Id = re.MedicamentoId
                JOIN dbo.TBFornecedor AS f
                    ON f.Id = m.FornecedorId
                ORDER BY r.Data, re.Id;
            """;
            resultado = conexao.Query<RequisicaoEntradaRow>(sqlQueryEntrada).Select(mapeador.Map<RequisicaoEntrada>).Cast<T>().ToList();
        }
        else if (typeof(T) == typeof(RequisicaoSaida))
        {
            string sqlQuerySaida = """
                SELECT
                    rs.Id AS RequisicaoSaidaId,
                    r.Data,
                    p.Id AS PacienteId,
                    p.Nome AS PacienteNome,
                    p.Telefone AS PacienteTelefone,
                    p.Cpf AS PacienteCpf,
                    p.CartaoSus AS PacienteCartaoSus
                FROM dbo.TBRequisicaoSaida AS rs
                JOIN dbo.TBRequisicao AS r
                    ON r.Id = rs.Id
                JOIN dbo.TBPaciente AS p
                    ON p.Id = rs.PacienteId
                ORDER BY r.Data, rs.Id;
            """;

            string sqlQueryItensSaida = """
                SELECT
                    i.RequisicaoSaidaId,
                    m.Id AS MedicamentoId,
                    m.Nome AS MedicamentoNome,
                    m.Descricao AS MedicamentoDescricao,
                    f.Id AS FornecedorId,
                    f.Nome AS FornecedorNome,
                    f.Telefone AS FornecedorTelefone,
                    f.Cnpj AS FornecedorCnpj,
                    i.Quantidade
                FROM dbo.TBItemSaida AS i
                JOIN dbo.TBMedicamento AS m
                    ON m.Id = i.MedicamentoId
                JOIN dbo.TBFornecedor AS f
                    ON f.Id = m.FornecedorId;
            """;

            var rowItem = conexao.Query<ItemSaidaRow>(sqlQueryItensSaida);

            resultado = conexao
                .Query<RequisicaoSaidaRow>(sqlQuerySaida)
                .Select(rs =>
                {
                    var saida = mapeador.Map<RequisicaoSaida>(rs);
                    saida.Medicamentos = rowItem
                        .Where(ri => ri.RequisicaoSaidaId == rs.RequisicaoSaidaId)
                        .Select(ri => ri.ExtrairItemSaida())
                        .ToArray();
                    return saida;
                })
                .Cast<T>()
                .ToList();
        }
        else
        {
            throw new NotSupportedException($"Tipo {typeof(T).Name} não suportado.");
        }

        return resultado.Where(filtro ?? (_ => true)).ToList();
    }
}

public sealed class RequisicaoEntradaRow
{
    public Guid RequisicaoId { get; set; }
    public DateTime Data { get; set; }
    public Guid FuncionarioId { get; set; }
    public string FuncionarioNome { get; set; } = string.Empty;
    public string FuncionarioTelefone { get; set; } = string.Empty;
    public string FuncionarioCpf { get; set; } = string.Empty;
    public Guid MedicamentoId { get; set; }
    public string MedicamentoNome { get; set; } = string.Empty;
    public string MedicamentoDescricao { get; set; } = string.Empty;
    public Guid FornecedorId { get; set; }
    public string FornecedorNome { get; set; } = string.Empty;
    public string FornecedorTelefone { get; set; } = string.Empty;
    public string FornecedorCnpj { get; set; } = string.Empty;
    public int Quantidade { get; set; }

    public Fornecedor ExtrairFornecedor() =>
        new() { Id = FornecedorId, Nome = FornecedorNome, Telefone = FornecedorTelefone, Cnpj = FornecedorCnpj };

    public Medicamento ExtrairMedicamento() =>
        new() { Id = MedicamentoId, Nome = MedicamentoNome, Descricao = MedicamentoDescricao, Fornecedor = ExtrairFornecedor() };

    public Funcionario ExtrairFuncionario() =>
        new() { Id = FuncionarioId, Nome = FuncionarioNome, Telefone = FuncionarioTelefone, Cpf = FuncionarioCpf };
}

public sealed class RequisicaoSaidaRow
{
    public Guid RequisicaoSaidaId { get; set; }
    public DateTime Data { get; set; }
    public Guid PacienteId { get; set; }
    public string PacienteNome { get; set; } = string.Empty;
    public string PacienteTelefone { get; set; } = string.Empty;
    public string PacienteCpf { get; set; } = string.Empty;
    public string PacienteCartaoSus { get; set; } = string.Empty;

    public Paciente ExtrairPaciente() =>
        new() { Id = PacienteId, Nome = PacienteNome, Telefone = PacienteTelefone, Cpf = PacienteCpf, CartaoSus = PacienteCartaoSus };
}

public sealed class ItemSaidaRow
{
    public Guid RequisicaoSaidaId { get; set; }
    public Guid MedicamentoId { get; set; }
    public string MedicamentoNome { get; set; } = string.Empty;
    public string MedicamentoDescricao { get; set; } = string.Empty;
    public Guid FornecedorId { get; set; }
    public string FornecedorNome { get; set; } = string.Empty;
    public string FornecedorTelefone { get; set; } = string.Empty;
    public string FornecedorCnpj { get; set; } = string.Empty;
    public int Quantidade { get; set; }

    public Fornecedor ExtrairFornecedor() =>
        new() { Id = FornecedorId, Nome = FornecedorNome, Telefone = FornecedorTelefone, Cnpj = FornecedorCnpj };

    public Medicamento ExtrairMedicamento() =>
        new() { Id = MedicamentoId, Nome = MedicamentoNome, Descricao = MedicamentoDescricao, Fornecedor = ExtrairFornecedor() };

    public ItemSaida ExtrairItemSaida() =>
        new() { Medicamento = ExtrairMedicamento(), Quantidade = Quantidade };
}

public class RequisicaoSqlProfile : Profile
{
    public RequisicaoSqlProfile()
    {
        CreateMap<RequisicaoEntradaRow, RequisicaoEntrada>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RequisicaoId))
            .ForMember(dest => dest.Funcionario, opt => opt.MapFrom(src => src.ExtrairFuncionario()))
            .ForMember(dest => dest.Medicamento, opt => opt.MapFrom(src => src.ExtrairMedicamento()));

        CreateMap<RequisicaoSaidaRow, RequisicaoSaida>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RequisicaoSaidaId))
            .ForMember(dest => dest.Paciente, opt => opt.MapFrom(src => src.ExtrairPaciente()))
            .ForMember(dest => dest.Medicamentos, opt => opt.Ignore());
    }
}
