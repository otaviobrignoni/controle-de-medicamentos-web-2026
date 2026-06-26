using ControleDeMedicamentos.WebApp.Compartilhado.Infrastructure;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Infra;

public sealed class RepositorioFornecedorSql(ISqlConnectionFactory connectionFactory) : IRepositorioFornecedor
{
    public List<Fornecedor> Registros => Selecionar();

    public void Cadastrar(Fornecedor registro)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            INSERT INTO dbo.TBFornecedor (Id, Nome, Telefone, Cnpj)
            VALUES (@Id, @Nome, @Telefone, @Cnpj);
        """;

        conexao.Execute(sqlQuery, registro);
    }

    public bool Editar(Guid id, Fornecedor registroEditado)
    {
        var fornecedor = Selecionar(id);

        return Editar(fornecedor, registroEditado);
    }

    public bool Editar(Fornecedor? registro, Fornecedor registroEditado)
    {
        if (registro is null)
            return false;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        registroEditado.Id = registro.Id;

        string sqlQuery = """
            UPDATE dbo.TBFornecedor
            SET
                Nome = @Nome,
                Telefone = @Telefone,
                Cnpj = @Cnpj
            WHERE Id = @Id;
        """;

        return conexao.Execute(sqlQuery, registroEditado) == 1;
    }

    public bool Excluir(Guid id)
    {
        var fornecedor = Selecionar(id);

        return Excluir(fornecedor);
    }

    public bool Excluir(Fornecedor? registro)
    {
        if (registro is null)
            return false;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            DELETE FROM dbo.TBFornecedor
            WHERE Id = @Id;
        """;

        return conexao.Execute(sqlQuery, new { registro.Id }) == 1;
    }

    public Fornecedor? Selecionar(Guid id)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            SELECT Id, Nome, Telefone, Cnpj
            FROM dbo.TBFornecedor
            WHERE Id = @Id;
        """;

        return conexao.QuerySingleOrDefault<Fornecedor>(sqlQuery, new { Id = id });
    }

    public List<Fornecedor> Selecionar(Func<Fornecedor, bool>? filtro = null)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string sqlQuery = """
            SELECT Id, Nome, Telefone, Cnpj
            FROM dbo.TBFornecedor
            ORDER BY Nome;
        """;

        return conexao.Query<Fornecedor>(sqlQuery).Where(filtro ?? (_ => true)).ToList();
    }
}
