using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Models;

public record FormSelectViewModel(
    string Name,
    string Label,
    object? Value,
    IEnumerable<SelectListItem> Options,
    string? Placeholder = null
);
