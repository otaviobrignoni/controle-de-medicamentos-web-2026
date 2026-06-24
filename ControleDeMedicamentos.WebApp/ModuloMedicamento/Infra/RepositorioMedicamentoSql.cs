using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Infra;

public class RepositorioMedicamentoSql(ISqlConnectionFactory connectionFactory, IMapper mapeador) : IRepositorioMedicamento
{
    public List<Medicamento> Registros => Selecionar();

    public void Cadastrar(Medicamento registro)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            INSERT INTO dbo.TBMedicamento (Id, Nome, Descricao, FornecedorId)
            VALUES (@Id, @Nome, @Descricao, @FornecedorId);
        """;

        var obj = new { registro.Id, registro.Nome, registro.Descricao, FornecedorId = registro.Fornecedor.Id };

        conexao.Execute(sqlQuery, obj);
    }

    public bool Editar(Guid id, Medicamento registroEditado)
    {
        var medicamento = Selecionar(id);

        return Editar(medicamento, registroEditado);
    }

    public bool Editar(Medicamento? registro, Medicamento registroEditado)
    {
        if (registro is null)
            return false;

        using SqlConnection conexao = connectionFactory.CreateConnection();
        conexao.Open();

        registroEditado.Id = registro.Id;

        string sqlQuery = """
            UPDATE dbo.TBMedicamento
            SET Nome = @Nome,
                Descricao = @Descricao,
                FornecedorId = @FornecedorId
            WHERE Id = @Id;
        """;

        var obj = new { registroEditado.Id, registroEditado.Nome, registroEditado.Descricao, FornecedorId = registroEditado.Fornecedor.Id };

        return conexao.Execute(sqlQuery, obj) == 1;
    }

    public bool Excluir(Guid id)
    {
        var medicamento = Selecionar(id);

        return Excluir(medicamento);
    }

    public bool Excluir(Medicamento? registro)
    {
        if (registro is null)
            return false;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            DELETE FROM dbo.TBMedicamento
            WHERE Id = @Id;
        """;

        return conexao.Execute(sqlQuery, new { registro.Id }) == 1;
    }

    public Medicamento? Selecionar(Guid id)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            SELECT
                m.Id AS MedicamentoId,
                m.Nome AS MedicamentoNome,
                m.Descricao AS MedicamentoDescricao,
                f.Id AS FornecedorId,
                f.Nome AS FornecedorNome,
                f.Telefone AS FornecedorTelefone,
                f.Cnpj AS FornecedorCnpj
            FROM dbo.TBMedicamento AS m
            JOIN dbo.TBFornecedor AS f
                ON f.Id = m.FornecedorId
            WHERE m.Id = @id;
        """;

        var row = conexao.QuerySingleOrDefault<MedicamentoRow>(sqlQuery, new { id });

        if (row == null)
            return null;

        return mapeador.Map<Medicamento>(row);
    }

    public List<Medicamento> Selecionar(Func<Medicamento, bool>? filtro = null)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            SELECT
                m.Id AS MedicamentoId,
                m.Nome AS MedicamentoNome,
                m.Descricao AS MedicamentoDescricao,
                f.Id AS FornecedorId,
                f.Nome AS FornecedorNome,
                f.Telefone AS FornecedorTelefone,
                f.Cnpj AS FornecedorCnpj
            FROM dbo.TBMedicamento AS m
            JOIN dbo.TBFornecedor AS f
                ON f.Id = m.FornecedorId
            ORDER BY m.Nome;
        """;

        return conexao.Query<MedicamentoRow>(sqlQuery).Select(mapeador.Map<Medicamento>).Where(filtro ?? (_ => true)).ToList();
    }
}
public sealed class MedicamentoRow
{
    public Guid MedicamentoId { get; set; }
    public string MedicamentoNome { get; set; } = string.Empty;
    public string MedicamentoDescricao { get; set; } = string.Empty;
    public Guid FornecedorId { get; set; }
    public string FornecedorNome { get; set; } = string.Empty;
    public string FornecedorTelefone { get; set; } = string.Empty;
    public string FornecedorCnpj { get; set; } = string.Empty;

    public Fornecedor ExtrairFornecedor()
    {
        return new Fornecedor { Id = FornecedorId, Nome = FornecedorNome, Telefone = FornecedorTelefone, Cnpj = FornecedorCnpj };
    }
}

public class MedicamentoSqlProfile : Profile
{
    public MedicamentoSqlProfile()
    {
        CreateMap<MedicamentoRow, Medicamento>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MedicamentoId))
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.MedicamentoNome))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.MedicamentoDescricao))
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.ExtrairFornecedor()));
    }
}


