using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using FluentResults;
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
            MedicamentoViewModel vm = new(string.Empty, string.Empty, 0, new Guid(), ObterFornecedores());

            return View(vm);
        }

        [HttpPost]
        public ActionResult Cadastrar(MedicamentoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm with { Fornecedores = ObterFornecedores() });

            var dto = mapeador.Map<CadastrarMedicamentoDto>(vm);

            Result resultado = servicoMedicamento.Cadastrar(dto);

            if (resultado.IsFailed)
            {
                ModelState.AddModelError(resultado);

                return View(vm with { Fornecedores = ObterFornecedores() });
            }

            return RedirectToAction(nameof(Listar));
        }
        [HttpPost]
        [HttpPost]
        [HttpGet]
        [HttpGet]

        public List<OpcoesFornecedorViewModel> ObterFornecedores()
        {
            var dtos = servicoFornecedor.SelecionarTodos();

            return dtos.Select(dto => mapeador.Map<OpcoesFornecedorViewModel>(dto)).ToList();
        }
    }
}
