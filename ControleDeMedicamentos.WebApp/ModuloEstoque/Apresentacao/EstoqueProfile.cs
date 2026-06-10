using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;

public class EstoqueProfile : Profile
{
    public EstoqueProfile()
    {
        CreateMap<DetalhesEntradaDto, DetalhesEntradaViewModel>();
        CreateMap<DetalhesSaidaDto, DetalhesSaidaViewModel>();
    }
}
