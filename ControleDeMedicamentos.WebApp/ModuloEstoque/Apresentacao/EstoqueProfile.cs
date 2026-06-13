using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;

public class EstoqueProfile : Profile
{
    public EstoqueProfile()
    {
        CreateMap<MostrarEntradaDto, MostrarEntradaViewModel>();
        CreateMap<MostrarSaidaDto, MostrarSaidaViewModel>();

        CreateMap<EntradaViewModel, EntradaDto>()
            .ForCtorParam(nameof(EntradaDto.MedicamentoId), opt => opt.MapFrom(src => src.MedicamentoId!.Value))
            .ForCtorParam(nameof(EntradaDto.FuncionarioId), opt => opt.MapFrom(src => src.FuncionarioId!.Value));
        CreateMap<SaidaViewModel, SaidaDto>()
            .ForCtorParam(nameof(SaidaDto.PacienteId), opt => opt.MapFrom(src => src.PacienteId!.Value));
        
        CreateMap<MostrarItemSaidaDto, MostrarItemSaidaViewModel>();
        CreateMap<ItemSaidaViewModel, ItemSaidaDto>()
            .ForCtorParam(nameof(ItemSaidaDto.MedicamentoId), opt => opt.MapFrom(src => src.MedicamentoId!.Value));

        CreateMap<MostrarMedicamentoDto, SelectListItem>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
        CreateMap<FuncionarioDto, SelectListItem>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
        CreateMap<PacienteDto, SelectListItem>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id)); 
    }
}
