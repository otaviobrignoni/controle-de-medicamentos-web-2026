using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao
{
    public class MedicamentoController(ServicoMedicamento servicoMedicamento, IMapper mapeador) : Controller
    {
        public ActionResult Listar()
        {
            var dtos = servicoMedicamento.SelecionarTodosListagem();

            var listarVms = mapeador.Map<List<MedicamentoMostrarViewModel>>(dtos);

            return View(listarVms);
        }

    }
}
