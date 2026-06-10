using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao.Views;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<FornecedorDto, FornecedorViewModel>();
        CreateMap<FornecedorViewModel, FornecedorDto>();
        CreateMap<FornecedorDto, OpcoesFornecedorViewModel>();
    }
}
