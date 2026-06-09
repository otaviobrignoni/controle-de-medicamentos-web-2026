using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Apresentacao;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<PacienteViewModel, PacienteDto>();
        CreateMap<PacienteDto, PacienteViewModel>();
    }
}
