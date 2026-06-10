using System.Globalization;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Extensions;

public static class StringExtensions
{
    public static string Capitalizar(this string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return texto;

        return char.ToUpper(texto[0], CultureInfo.CurrentCulture) + texto[1..];
    }
}
