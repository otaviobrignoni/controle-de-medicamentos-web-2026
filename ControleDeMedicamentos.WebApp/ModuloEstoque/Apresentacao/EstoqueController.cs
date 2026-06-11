using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Extensions;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao
{
    public class EstoqueController(ServicoEstoque servicoEstoque, ServicoMedicamento servicoMedicamento, ServicoPaciente servicoPaciente, ServicoFuncionario servicoFuncionario, IMapper mapeador) : Controller
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

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CadastrarEntrada()
        {
            var vm = new CadastrarEntradaViewModel(null, 0, null, SelecionarMedicamentos(), SelecionarFuncionarios());
            return View(vm);
        }

        [HttpPost]
        public ActionResult CadastrarEntrada(CadastrarEntradaViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm with { Medicamentos = SelecionarMedicamentos(), Funcionarios = SelecionarFuncionarios() });

            var dto = mapeador.Map<CadastrarEntradaDto>(vm);

            var resultado = servicoEstoque.CadastrarEntrada(dto);

            if (resultado.IsFailed)
            {
                ModelState.AddModelError(resultado);
                return View(vm with { Medicamentos = SelecionarMedicamentos(), Funcionarios = SelecionarFuncionarios() });
            }

            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public ActionResult CadastrarSaida()
        {
            var vm = new CadastrarSaidaViewModel(null, [new(null, 1)], SelecionarPacientes(), SelecionarMedicamentos());
            return View(vm);
        }

        [HttpPost]
        public ActionResult CadastrarSaida(CadastrarSaidaViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm with { Pacientes = SelecionarPacientes(), Medicamentos = SelecionarMedicamentos() });

            var dto = mapeador.Map<CadastrarSaidaDto>(vm);

            var resultado = servicoEstoque.CadastrarSaida(dto);

            if (resultado.IsFailed)
            {
                ModelState.AddModelError(resultado);
                return View(vm with { Pacientes = SelecionarPacientes(), Medicamentos = SelecionarMedicamentos() });
            }

            return RedirectToAction(nameof(Listar));
        }

        private List<SelectListItem> SelecionarMedicamentos()
        {
            var dtos = servicoMedicamento.SelecionarTodosListagem();

            return mapeador.Map<List<SelectListItem>>(dtos);
        }

        private List<SelectListItem> SelecionarPacientes()
        {
            var dtos = servicoPaciente.SelecionarTodos();

            return mapeador.Map<List<SelectListItem>>(dtos);
        }

        private List<SelectListItem> SelecionarFuncionarios()
        {
            var dtos = servicoFuncionario.Selecionar();

            return mapeador.Map<List<SelectListItem>>(dtos);
        }
    }
}
