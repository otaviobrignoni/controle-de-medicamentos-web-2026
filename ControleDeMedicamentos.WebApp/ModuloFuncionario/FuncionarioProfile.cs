using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        // Apresentação -> Aplicação
        CreateMap<FuncionarioViewModel, FuncionarioDto>();
        // Aplicação -> Apresentação
        CreateMap<FuncionarioDto, FuncionarioViewModel>();
        // Domínio -> Aplicação
        CreateMap<Funcionario, FuncionarioDto>();
        // Aplicação -> Domínio
        CreateMap<FuncionarioDto, Funcionario>()
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != default));
    }
}
