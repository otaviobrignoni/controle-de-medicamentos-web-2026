using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Infra;

public sealed class RepositorioFuncionarioSql(ISqlConnectionFactory connectionFactory) : IRepositorioFuncionario
{
    public List<Funcionario> Registros => Selecionar();

    public void Cadastrar(Funcionario registro)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            INSERT INTO dbo.TBFuncionario (Id, Nome, Telefone, Cpf)
            VALUES (@Id, @Nome, @Telefone, @Cpf);
        """;

        conexao.Execute(sqlQuery, registro);
    }

    public bool Editar(Guid id, Funcionario registroEditado)
    {
        var funcionario = Selecionar(id);

        return Editar(funcionario, registroEditado);
    }

    public bool Editar(Funcionario? registro, Funcionario registroEditado)
    {
        if (registro is null)
            return false;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        registroEditado.Id = registro.Id;

        string sqlQuery = """
            UPDATE dbo.TBFuncionario
            SET
                Nome = @Nome,
                Telefone = @Telefone,
                Cpf = @Cpf
            WHERE Id = @Id;
        """;

        return conexao.Execute(sqlQuery, registroEditado) == 1;
    }

    public bool Excluir(Guid id)
    {
        var funcionario = Selecionar(id);

        return Excluir(funcionario);
    }

    public bool Excluir(Funcionario? registro)
    {
        if (registro is null)
            return false;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            DELETE FROM dbo.TBFuncionario
            WHERE Id = @Id;
        """;

        return conexao.Execute(sqlQuery, new { registro.Id }) == 1;
    }

    public Funcionario? Selecionar(Guid id)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            SELECT Id, Nome, Telefone, Cpf
            FROM dbo.TBFuncionario
            WHERE Id = @Id;
        """;

        return conexao.QuerySingleOrDefault<Funcionario>(sqlQuery, new { Id = id });
    }

    public List<Funcionario> Selecionar(Func<Funcionario, bool>? filtro = null)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            SELECT Id, Nome, Telefone, Cpf
            FROM dbo.TBFuncionario
            ORDER BY Nome;
        """;

        return conexao.Query<Funcionario>(sqlQuery).Where(filtro ?? (_ => true)).ToList();
    }
}
