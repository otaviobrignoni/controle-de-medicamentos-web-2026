using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<ListarMedicamentoDto, MedicamentoMostrarViewModel>();
        CreateMap<MedicamentoViewModel, CadastrarMedicamentoDto>();
    }
}
