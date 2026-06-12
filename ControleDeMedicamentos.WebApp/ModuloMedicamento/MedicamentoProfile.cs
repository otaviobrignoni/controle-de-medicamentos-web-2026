using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        // Apresentação -> Aplicação
        CreateMap<MedicamentoViewModel, MedicamentoDto>()
            .ForCtorParam(nameof(MedicamentoDto.FornecedorId), opt => opt.MapFrom(src => src.FornecedorId!.Value));
        // Aplicação -> Apresentação
        CreateMap<MedicamentoDto, MedicamentoViewModel>()
            .ForMember(dest => dest.Fornecedores, opt => opt.MapFromContext("fornecedores"));
        CreateMap<MostrarMedicamentoDto, MedicamentoMostrarViewModel>();
        CreateMap<FornecedorDto, OpcoesFornecedorViewModel>();
        // Domínio -> Aplicação
        CreateMap<Medicamento, MedicamentoDto>()
            .ForMember(dest => dest.FornecedorId, opt => opt.MapFrom(src => src.Fornecedor.Id));
        CreateMap<Medicamento, MostrarMedicamentoDto>()
            .ForMember(dest => dest.FornecedorNome, opt => opt.MapFrom(src => src.Fornecedor.Nome));
        // Aplicação -> Domínio
        CreateMap<MedicamentoDto, Medicamento>()
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFromContext("fornecedor"));
    }
}
