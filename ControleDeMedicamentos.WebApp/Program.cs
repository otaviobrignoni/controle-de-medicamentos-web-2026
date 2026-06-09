using ControleDeMedicamentos.WebApp.Compartilhado;

namespace ControleDeMedicamentos.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddPresentationConfig();
        builder.Services.AddServicesConfig();
        builder.Services.AddRepositoriesConfig();

        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
