using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        // Apresentação -> Aplicação
        CreateMap<PacienteViewModel, PacienteDto>();
        // Aplicação -> Apresentação
        CreateMap<PacienteDto, PacienteViewModel>();
        // Domínio -> Aplicação
        CreateMap<Paciente, PacienteDto>();
        // Aplicação -> Domínio
        CreateMap<PacienteDto, Paciente>()
            .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != default));
    }
}
