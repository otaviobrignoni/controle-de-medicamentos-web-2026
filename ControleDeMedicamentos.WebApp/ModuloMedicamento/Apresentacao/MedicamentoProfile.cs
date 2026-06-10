using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<ListarMedicamentoDto, MedicamentoMostrarViewModel>();

        CreateMap<MedicamentoViewModel, CadastrarMedicamentoDto>();
        CreateMap<MedicamentoViewModel, EditarMedicamentoDto>();

        CreateMap<DetalhesMedicamentoDto, MedicamentoViewModel>()
                .ForCtorParam("Fornecedores", opt => opt.MapFrom(_ => new List<OpcoesFornecedorViewModel>()));

        CreateMap<DetalhesMedicamentoDto, MedicamentoMostrarViewModel>();
    }
}
