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
        CreateMap<DetalhesEntradaDto, DetalhesEntradaViewModel>();
        CreateMap<DetalhesSaidaDto, DetalhesSaidaViewModel>();

        CreateMap<CadastrarEntradaViewModel, CadastrarEntradaDto>()
            .ForCtorParam(nameof(CadastrarEntradaDto.MedicamentoId), opt => opt.MapFrom(src => src.MedicamentoId!.Value))
            .ForCtorParam(nameof(CadastrarEntradaDto.FuncionarioId), opt => opt.MapFrom(src => src.FuncionarioId!.Value));
        CreateMap<CadastrarSaidaViewModel, CadastrarSaidaDto>()
            .ForCtorParam(nameof(CadastrarSaidaDto.PacienteId), opt => opt.MapFrom(src => src.PacienteId!.Value));
        
        CreateMap<ItemSaidaDto, ItemSaidaViewModel>();
        CreateMap<CadastrarItemSaidaViewModel, CadastrarItemSaidaDto>()
            .ForCtorParam(nameof(CadastrarItemSaidaDto.MedicamentoId), opt => opt.MapFrom(src => src.MedicamentoId!.Value));

        CreateMap<ListarMedicamentoDto, SelectListItem>()
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
