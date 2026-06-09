using System.Text.Json;
using System.Text.Json.Serialization;
using ControleDeMedicamentos.WebApp.Modulo_Fornecedor.Dominio;

namespace ControleDeMedicamentos.WebApp.Compartilhado;
// Infraestrutura
public class ContextoJson
{
    //List<T> (*) (*)s;
    public List<Fornecedor> Fornecedores { get; set; } = [];
    private readonly string caminhoArquivo;
    private readonly JsonSerializerOptions opcoesJson = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        ReferenceHandler = ReferenceHandler.Preserve
    };

    public ContextoJson()
    {
        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorio = Path.Combine(caminhoAppData, "ControleDeMedicamentosWeb");

        Directory.CreateDirectory(caminhoDiretorio);

        caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");
    }

    public void Salvar()
    {
        string jsonString = JsonSerializer.Serialize(this, opcoesJson);

        File.WriteAllText(caminhoArquivo, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return;

        string jsonString = File.ReadAllText(caminhoArquivo);

        ContextoJson? contextoSalvo = JsonSerializer
            .Deserialize<ContextoJson>(jsonString, opcoesJson);

        if (contextoSalvo is null)
            return;

        //*s = contextoSalvo.(*)s
        Fornecedores = contextoSalvo.Fornecedores;
    }
}
