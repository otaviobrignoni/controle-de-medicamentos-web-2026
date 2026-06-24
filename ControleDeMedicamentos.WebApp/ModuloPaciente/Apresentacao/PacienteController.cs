using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Extensions;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Apresentacao;

public class PacienteController(ServicoPaciente servicoPaciente, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        var dtos = servicoPaciente.Selecionar();

        var ListarVms = mapeador.Map<List<PacienteViewModel>>(dtos);

        return View(ListarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        PacienteViewModel vm = new(string.Empty, string.Empty, string.Empty, string.Empty);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Cadastrar(PacienteViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var dto = mapeador.Map<PacienteDto>(vm);

        Result resultado = servicoPaciente.Cadastrar(dto);

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
        var resultado = servicoPaciente.Selecionar(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        var dto = resultado.Value;

        var vm = mapeador.Map<PacienteViewModel>(dto);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Editar(PacienteViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var dto = mapeador.Map<PacienteDto>(vm);

        var resultado = servicoPaciente.Editar(dto);

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
        var resultado = servicoPaciente.Selecionar(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        var dto = resultado.Value;

        var vm = mapeador.Map<PacienteViewModel>(dto);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(PacienteViewModel vm)
    {
        if (ModelState.IsValid)
            return View(vm);

        var resultado = servicoPaciente.Excluir(vm.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
}
