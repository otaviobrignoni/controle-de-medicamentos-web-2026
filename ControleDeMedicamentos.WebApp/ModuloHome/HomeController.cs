using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.WebApp.Compartilhado.ModuloBase;

public class HomeController : Controller
{
    // GET: HomeController
    public ActionResult Index()
    {
        return View();
    }

}
