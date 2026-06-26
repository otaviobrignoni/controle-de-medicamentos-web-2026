using ControleDeMedicamentos.WebApp.Compartilhado.Infrastructure;
using ControleDeMedicamentos.WebApp.Compartilhado.Logging;
using ControleDeMedicamentos.WebApp.Compartilhado.Mapping;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Infra;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Infra;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Infra;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Infra;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Infra;

namespace ControleDeMedicamentos.WebApp.Compartilhado;

public static class InjecaoDependencia
{
    // Camada de Apresentação
    public static void AddPresentationConfig(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            // Resetar a configuração padrão do MVC
            options.ViewLocationFormats.Clear();

            // Views dos módulos: /ModuloCaixa/Apresentacao/Views/Listar.cshtml
            options.ViewLocationFormats.Add("/Modulo{1}/Views/{0}.cshtml");
            options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            // Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
            options.ViewLocationFormats.Add("/Compartilhado/Views/{0}.cshtml");
        });

        services.AddAutoMapper(mapperCfg =>
        {
            var opt = config.GetSection(AutoMapperOptions.SectionName).Get<AutoMapperOptions>();

            if (!string.IsNullOrWhiteSpace(opt?.LicenseKey))
                mapperCfg.LicenseKey = opt.LicenseKey;

            mapperCfg.AddMaps(typeof(Program));
        });
    }

    // Camada de Infraestrutura
    public static void AddRepositoriesConfig(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new();

            contextoJson.Carregar();

            return contextoJson;
        });

        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        //services.AddScoped<IRepositorio(*), Repositorio(*)>();
        services.AddScoped<IRepositorioFornecedor, RepositorioFornecedorSql>();
        services.AddScoped<IRepositorioPaciente, RepositorioPacienteSql>();
        services.AddScoped<IRepositorioMedicamento, RepositorioMedicamentoSql>();
        services.AddScoped<IRepositorioFuncionario, RepositorioFuncionarioSql>();
        services.AddScoped<IRepositorioRequisicao, RepositorioRequisicaoSql>();
    }

    // Camada de Aplicação
    public static void AddServicesConfig(this IServiceCollection services, IConfiguration config, ILoggingBuilder logging)
    {
        services.AddSerilogLogger(config, logging);

        //services.AddScoped<Servico(*)>();
        services.AddScoped<ServicoFornecedor>();
        services.AddScoped<ServicoPaciente>();
        services.AddScoped<ServicoMedicamento>();
        services.AddScoped<ServicoFuncionario>();
        services.AddScoped<ServicoEstoque>();
    }
}
