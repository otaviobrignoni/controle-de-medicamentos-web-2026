using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Infra;

public sealed class RepositorioFornecedorEmSql(ISqlConnectionFactory connectionFactory) : IRepositorioFornecedor
{
    public List<Fornecedor> Registros => Selecionar();

    public void Cadastrar(Fornecedor registro)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string InserirSql = """
            INSERT INTO dbo.TBFornecedor (Id, Nome, Telefone, Cnpj)
            VALUES (@Id, @Nome, @Telefone, @Cnpj);
        """;

        conexao.Execute(InserirSql, registro);
    }

    public bool Editar(Guid id, Fornecedor registroEditado)
    {
        var fornecedor = Selecionar(id);

        return Editar(fornecedor, registroEditado);
    }

    public bool Editar(Fornecedor? registro, Fornecedor registroEditado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        if (registro is null)
            return false;

        registroEditado.Id = registro.Id;

        string AtualizarSql = """
            UPDATE dbo.TBFornecedor
            SET
                Nome = @Nome,
                Telefone = @Telefone,
                Cnpj = @Cnpj
            WHERE Id = @Id;
        """;

        return conexao.Execute(AtualizarSql, registroEditado) == 1;
    }

    public bool Excluir(Guid id)
    {
        var fornecedor = Selecionar(id);

        return Excluir(fornecedor);
    }

    public bool Excluir(Fornecedor? registro)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        if (registro is null)
            return false;

        conexao.Open();

        string ExcluirSql = """
            DELETE FROM dbo.TBFornecedor
            WHERE Id = @Id;
        """;

        return conexao.Execute(ExcluirSql, new { registro.Id }) == 1;

    }

    public Fornecedor? Selecionar(Guid id)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string SelecionarPorIdSql = """
            SELECT Id, Nome, Telefone, Cnpj
            FROM dbo.TBFornecedor
            WHERE Id = @Id;
        """;

        return conexao.QuerySingleOrDefault<Fornecedor>(SelecionarPorIdSql, new { Id = id });
    }

    public List<Fornecedor> Selecionar(Func<Fornecedor, bool>? filtro = null)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        string SelecionarTodosSql = """
            SELECT Id, Nome, Telefone, Cnpj
            FROM dbo.TBFornecedor
            ORDER BY Nome;
        """;

        return conexao.Query<Fornecedor>(SelecionarTodosSql).Where(filtro ?? (_ => true)).ToList();
    }
}
