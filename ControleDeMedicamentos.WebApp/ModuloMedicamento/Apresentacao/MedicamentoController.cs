using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;

public class MedicamentoController(ServicoMedicamento servicoMedicamento, ServicoFornecedor servicoFornecedor, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        var dtos = servicoMedicamento.Selecionar();

        var listarVms = mapeador.Map<List<MedicamentoMostrarViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        MedicamentoViewModel vm = new(string.Empty, string.Empty, 0, null, ObterFornecedores());

        return View(vm);
    }

    [HttpPost]
    public ActionResult Cadastrar(MedicamentoViewModel vm)
    {
        if (!ModelState.IsValid)
            return ViewComFornecedores(vm);

        var dto = mapeador.Map<MedicamentoDto>(vm);

        Result resultado = servicoMedicamento.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return ViewComFornecedores(vm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        var resultado = servicoMedicamento.Selecionar<MedicamentoDto>(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        var vm = mapeador.MapWith<MedicamentoViewModel>(resultado.Value, ("fornecedores", ObterFornecedores()));

        return View(vm);
    }

    [HttpPost]
    public ActionResult Editar(MedicamentoViewModel vm)
    {
        if (!ModelState.IsValid)
            return ViewComFornecedores(vm);

        var dto = mapeador.Map<MedicamentoDto>(vm);

        Result resultado = servicoMedicamento.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return ViewComFornecedores(vm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        var resultado = servicoMedicamento.Selecionar<MostrarMedicamentoDto>(id);

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
        var resultado = servicoMedicamento.Selecionar<MostrarMedicamentoDto>(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        var vm = mapeador.Map<MedicamentoMostrarViewModel>(resultado.Value);

        return View(vm);
    }
    public List<SelectListItem> ObterFornecedores()
    {
        var dtos = servicoFornecedor.Selecionar();

        return mapeador.Map<List<SelectListItem>>(dtos);
    }

    private ViewResult ViewComFornecedores(MedicamentoViewModel vm)
    {
        return View(vm with { Fornecedores = ObterFornecedores() });
    }
}
