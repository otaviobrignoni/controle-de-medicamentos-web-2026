using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;

public class FornecedorController(ServicoFornecedor servicoFornecedor, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        var dtos = servicoFornecedor.SelecionarTodos();

        var ListarVms = mapeador.Map<List<FornecedorViewModel>>(dtos);

        return View(ListarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        FornecedorViewModel vm = new(string.Empty, string.Empty, string.Empty, 0);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Cadastrar(FornecedorViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var dto = mapeador.Map<FornecedorDto>(vm);

        Result resultado = servicoFornecedor.Cadastrar(dto);

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
        var resultado = servicoFornecedor.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        var dto = resultado.Value;

        var vm = mapeador.Map<FornecedorViewModel>(dto);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Editar(FornecedorViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var dto = mapeador.Map<FornecedorDto>(vm);

        var resultado = servicoFornecedor.Editar(dto);

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
        var resultado = servicoFornecedor.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        var dto = resultado.Value;

        var vm = mapeador.Map<FornecedorViewModel>(dto);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(FornecedorViewModel vm)
    {
        var resultado = servicoFornecedor.Excluir(vm.Id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        return RedirectToAction(nameof(Listar));
    }

}
