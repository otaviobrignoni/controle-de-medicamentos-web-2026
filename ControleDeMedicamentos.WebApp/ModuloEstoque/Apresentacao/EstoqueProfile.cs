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
        CreateMap<ItemSaidaDto, ItemSaidaViewModel>();

        CreateMap<CadastrarEntradaViewModel, CadastrarEntradaDto>();
        CreateMap<CadastrarSaidaViewModel, CadastrarSaidaDto>();
        CreateMap<CadastrarItemSaidaViewModel, CadastrarItemSaidaDto>();

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
