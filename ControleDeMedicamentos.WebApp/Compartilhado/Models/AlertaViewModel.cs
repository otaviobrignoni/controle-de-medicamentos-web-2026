namespace ControleDeMedicamentos.WebApp.Compartilhado.Models;

public record AlertaViewModel(
    string Mensagem,
    bool Erro = false
);
