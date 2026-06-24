using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Infra;

public sealed class RepositorioPacienteSql(ISqlConnectionFactory connectionFactory) : IRepositorioPaciente
{
    public List<Paciente> Registros => Selecionar();

    public void Cadastrar(Paciente registro)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            INSERT INTO dbo.TBPaciente (Id, Nome, Telefone, Cpf, CartaoSus)
            VALUES (@Id, @Nome, @Telefone, @Cpf, @CartaoSus);
        """;

        conexao.Execute(sqlQuery, registro);
    }

    public bool Editar(Guid id, Paciente registroEditado)
    {
        var paciente = Selecionar(id);

        return Editar(paciente, registroEditado);
    }

    public bool Editar(Paciente? registro, Paciente registroEditado)
    {
        if (registro is null)
            return false;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        registroEditado.Id = registro.Id;

        string sqlQuery = """
            UPDATE dbo.TBPaciente
            SET
                Nome = @Nome,
                Telefone = @Telefone,
                Cpf = @Cpf,
                CartaoSus = @CartaoSus
            WHERE Id = @Id;
        """;

        return conexao.Execute(sqlQuery, registroEditado) == 1;
    }

    public bool Excluir(Guid id)
    {
        var paciente = Selecionar(id);

        return Excluir(paciente);
    }

    public bool Excluir(Paciente? registro)
    {
        if (registro is null)
            return false;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            DELETE FROM dbo.TBPaciente
            WHERE Id = @Id;
        """;

        return conexao.Execute(sqlQuery, new { registro.Id }) == 1;
    }

    public Paciente? Selecionar(Guid id)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            SELECT Id, Nome, Telefone, Cpf, CartaoSus
            FROM dbo.TBPaciente
            WHERE Id = @Id;
        """;

        return conexao.QuerySingleOrDefault<Paciente>(sqlQuery, new { Id = id });
    }

    public List<Paciente> Selecionar(Func<Paciente, bool>? filtro = null)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            SELECT Id, Nome, Telefone, Cpf, CartaoSus
            FROM dbo.TBPaciente
            ORDER BY Nome;
        """;

        return conexao.Query<Paciente>(sqlQuery).Where(filtro ?? (_ => true)).ToList();
    }
}
