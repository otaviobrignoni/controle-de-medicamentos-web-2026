using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<ListarMedicamentoDto, MedicamentoMostrarViewModel>();

        CreateMap<MedicamentoViewModel, CadastrarMedicamentoDto>()
            .ForCtorParam(nameof(CadastrarMedicamentoDto.FornecedorId), opt => opt.MapFrom(src => src.FornecedorId!.Value));
        CreateMap<MedicamentoViewModel, EditarMedicamentoDto>()
            .ForCtorParam(nameof(EditarMedicamentoDto.FornecedorId), opt => opt.MapFrom(src => src.FornecedorId!.Value));

        CreateMap<DetalhesMedicamentoDto, MedicamentoViewModel>()
                .ForCtorParam("Fornecedores", opt => opt.MapFrom(_ => new List<OpcoesFornecedorViewModel>()));

        CreateMap<DetalhesMedicamentoDto, MedicamentoMostrarViewModel>();
    }
}
