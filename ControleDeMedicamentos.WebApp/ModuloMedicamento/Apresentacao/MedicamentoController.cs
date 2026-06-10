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
        [HttpGet]
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

        [HttpGet]
        public ActionResult Editar(Guid id)
        {
            var resultado = servicoMedicamento.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                TempData.AddErrorMessage(resultado);

                return RedirectToAction(nameof(Listar));
            }

            var vm = mapeador.Map<MedicamentoViewModel>(resultado.Value);

            return View(vm with { Fornecedores = ObterFornecedores() });
        }
        [HttpPost]
        public ActionResult Editar(MedicamentoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm with { Fornecedores = ObterFornecedores() });

            var dto = mapeador.Map<EditarMedicamentoDto>(vm);

            Result resultado = servicoMedicamento.Editar(dto);

            if (resultado.IsFailed)
            {
                ModelState.AddModelError(resultado);

                return View(vm with { Fornecedores = ObterFornecedores() });
            }

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult Excluir(Guid id)
        {
            var resultado = servicoMedicamento.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                TempData.AddErrorMessage(resultado);

                return RedirectToAction(nameof(Listar));
            }

            var dto = resultado.Value;

            var vm = mapeador.Map<MedicamentoMostrarViewModel>(dto);

            return View(vm);
        }
        [HttpPost]
        public ActionResult Excluir(MedicamentoMostrarViewModel vm)
        {
            if (ModelState.IsValid)
                return View(vm);

            var resultado = servicoMedicamento.Excluir(vm.Id);

            if (resultado.IsFailed)
            {
                ModelState.AddModelError(resultado);

                return View(vm);
            }

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult Detalhes(Guid id)
        {
            var resultado = servicoMedicamento.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                TempData.AddErrorMessage(resultado);

                return RedirectToAction(nameof(Listar));
            }

            var vm = mapeador.Map<MedicamentoMostrarViewModel>(resultado.Value);

            return View(vm);
        }
        public List<OpcoesFornecedorViewModel> ObterFornecedores()
        {
            var dtos = servicoFornecedor.SelecionarTodos();

            return dtos.Select(dto => mapeador.Map<OpcoesFornecedorViewModel>(dto)).ToList();
        }
    }
}
