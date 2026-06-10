using System.Text.Json;
using System.Text.Json.Serialization;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

namespace ControleDeMedicamentos.WebApp.Compartilhado;
// Infraestrutura
public class ContextoJson
{
    //List<T> (*)s = [];
    public List<Fornecedor> Fornecedores { get; set; } = [];
    public List<Paciente> Pacientes { get; set; } = [];
    public List<Medicamento> Medicamentos { get; set; } = [];
    public List<Funcionario> Funcionarios { get; set; } = [];
    public List<Requisicao> Requisicoes { get; set; } = [];

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
        Pacientes = contextoSalvo.Pacientes;
        Medicamentos = contextoSalvo.Medicamentos;
        Funcionarios = contextoSalvo.Funcionarios;
        Requisicoes = contextoSalvo.Requisicoes;
    }
}
