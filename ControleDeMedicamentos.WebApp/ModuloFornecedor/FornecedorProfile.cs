using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        // Apresentação -> Aplicação
        CreateMap<FornecedorViewModel, FornecedorDto>();
        // Aplicação -> Apresentação
        CreateMap<FornecedorDto, FornecedorViewModel>();
        // Domínio -> Aplicação
        CreateMap<Fornecedor, FornecedorDto>()
            .ForCtorParam(nameof(FornecedorDto.Medicamentos), opt => opt.MapFrom(src => src.Medicamentos.Count));
        // Aplicação -> Domínio
        CreateMap<FornecedorDto, Fornecedor>()
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != default))
            .ForMember(dest => dest.Medicamentos, opt => opt.Ignore());
    }
}
