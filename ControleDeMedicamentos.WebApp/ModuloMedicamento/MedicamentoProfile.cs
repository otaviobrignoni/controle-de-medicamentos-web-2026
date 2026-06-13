using AutoMapper;
using ControleDeMedicamentos.WebApp.Compartilhado.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            .ForCtorParam(nameof(MedicamentoViewModel.Fornecedores), opt => opt.MapFromContext("fornecedores"));
        CreateMap<FornecedorDto, SelectListItem>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
        CreateMap<MostrarMedicamentoDto, MedicamentoMostrarViewModel>();
        // Domínio -> Aplicação
        CreateMap<Medicamento, MedicamentoDto>()
            .ForMember(dest => dest.FornecedorId, opt => opt.MapFrom(src => src.Fornecedor.Id));
        CreateMap<Medicamento, MostrarMedicamentoDto>()
            .ForMember(dest => dest.FornecedorNome, opt => opt.MapFrom(src => src.Fornecedor.Nome));
        // Aplicação -> Domínio
        CreateMap<MedicamentoDto, Medicamento>()
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != default))
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFromContext("fornecedor"));
    }
}
