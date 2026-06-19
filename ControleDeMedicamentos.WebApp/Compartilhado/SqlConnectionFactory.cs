using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentos.WebApp.Compartilhado;

public class SqlConnectionFactory(IConfiguration config) : ISqlConnectionFactory
{
    private const string NomeConnectionString = "ControleDeMedicamentoWeb";

    public SqlConnection CreateConnection()
    {
        string? connectionString = config.GetConnectionString(NomeConnectionString);
        
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException($"Connection string {NomeConnectionString} não encontrada");
        
        return new SqlConnection(connectionString);
    }
}

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}
