using AutoMapper;
using ControleDeMedicamentos.WebApp.Modulo_Fornecedor.Aplicacao;

namespace ControleDeMedicamentos.WebApp.Modulo_Fornecedor.Apresentacao.Views;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<FornecedorDto, FornecedorViewModel>();
        CreateMap<FornecedorViewModel, FornecedorDto>();
    }
}
