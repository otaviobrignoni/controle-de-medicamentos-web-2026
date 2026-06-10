using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao
{
    public class MedicamentoController(ServicoMedicamento servicoMedicamento, ServicoFornecedor servicoFornecedor, IMapper mapeador) : Controller
    {
        public ActionResult Listar()
        {
            var dtos = servicoMedicamento.SelecionarTodosListagem();

            var listarVms = mapeador.Map<List<MedicamentoMostrarViewModel>>(dtos);

            return View(listarVms);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            MedicamentoViewModel vm = new(string.Empty, string.Empty, 0, new Guid(), ObterCategorias());

            return View(vm);
        }

        [HttpPost]
        [HttpPost]
        [HttpPost]
        [HttpGet]
        [HttpGet]

        public List<OpcoesFornecedorViewModel> ObterCategorias()
        {
            var dtos = servicoFornecedor.SelecionarTodos();

            return dtos.Select(dto => mapeador.Map<OpcoesFornecedorViewModel>(dto)).ToList();
        }
    }
}
