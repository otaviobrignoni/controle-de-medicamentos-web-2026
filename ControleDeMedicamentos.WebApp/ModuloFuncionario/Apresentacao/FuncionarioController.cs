using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Apresentacao
{
    public class FuncionarioController(ServicoFuncionario servico, IMapper mapeador) : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            var dtos = servico.Selecionar();

            var vms = mapeador.Map<List<FuncionarioViewModel>>(dtos);

            return View(vms);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var vm = new FuncionarioViewModel(string.Empty, string.Empty, string.Empty);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Cadastrar(FuncionarioViewModel vm)
        {
            if(!ModelState.IsValid)
                return View(vm);

            var dto = mapeador.Map<FuncionarioDto>(vm);

            var resultado = servico.Cadastrar(dto);

            if (resultado.IsFailed)
            {
                ModelState.AddModelError(resultado);
                return View(vm);
            }

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
    public ActionResult Editar(Guid id)
    {
        var resultado = servico.Selecionar(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        var dto = resultado.Value;

        var vm = mapeador.Map<FuncionarioViewModel>(dto);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Editar(FuncionarioViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var dto = mapeador.Map<FuncionarioDto>(vm);

        var resultado = servico.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        var resultado = servico.Selecionar(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        var dto = resultado.Value;

        var vm = mapeador.Map<FuncionarioViewModel>(dto);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(FuncionarioViewModel vm)
    {
        var resultado = servico.Excluir(vm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
    }
}
