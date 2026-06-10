using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Apresentacao;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<FuncionarioDto, FuncionarioViewModel>();
        
        CreateMap<FuncionarioViewModel, FuncionarioDto>();
    }
}
