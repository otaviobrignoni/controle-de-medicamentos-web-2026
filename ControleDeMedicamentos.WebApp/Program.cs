using ControleDeMedicamentos.WebApp.Compartilhado;

namespace ControleDeMedicamentos.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsProduction())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }

        builder.Services.AddPresentationConfig();
        builder.Services.AddServicesConfig();
        builder.Services.AddRepositoriesConfig();

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}
