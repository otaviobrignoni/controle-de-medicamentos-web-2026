using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao
{
    public class EstoqueController(ServicoEstoque servicoEstoque, IMapper mapeador) : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            var dtosEntrada = servicoEstoque.SelecionarEntrada();
            var dtosSaida = servicoEstoque.SelecionarSaida();

            var vms = (
                mapeador.Map<List<DetalhesEntradaViewModel>>(dtosEntrada), 
                mapeador.Map<List<DetalhesSaidaViewModel>>(dtosSaida)
            );

            return View(vms);
        }
    }
}
