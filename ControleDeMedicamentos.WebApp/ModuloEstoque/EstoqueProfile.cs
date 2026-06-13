using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque;

public class EstoqueProfile : Profile
{
    public EstoqueProfile()
    {
        // Apresentação -> Aplicação
        CreateMap<EntradaViewModel, EntradaDto>()
            .ForCtorParam(nameof(EntradaDto.MedicamentoId), opt => opt.MapFrom(src => src.MedicamentoId!.Value))
            .ForCtorParam(nameof(EntradaDto.FuncionarioId), opt => opt.MapFrom(src => src.FuncionarioId!.Value));
        CreateMap<SaidaViewModel, SaidaDto>()
            .ForCtorParam(nameof(SaidaDto.PacienteId), opt => opt.MapFrom(src => src.PacienteId!.Value));
        CreateMap<ItemSaidaViewModel, ItemSaidaDto>()
            .ForCtorParam(nameof(ItemSaidaDto.MedicamentoId), opt => opt.MapFrom(src => src.MedicamentoId!.Value));
        // Aplicação -> Apresentação
        CreateMap<MostrarEntradaDto, MostrarEntradaViewModel>();
        CreateMap<MostrarSaidaDto, MostrarSaidaViewModel>();
        CreateMap<MostrarItemSaidaDto, MostrarItemSaidaViewModel>();
        // Domínio -> Aplicação
        CreateMap<RequisicaoEntrada, MostrarEntradaDto>()
            .ForCtorParam(nameof(MostrarEntradaDto.Medicamento), opt => opt.MapFrom(src => src.Medicamento.Nome))
            .ForCtorParam(nameof(MostrarEntradaDto.Funcionario), opt => opt.MapFrom(src => src.Funcionario.Nome));
        CreateMap<RequisicaoSaida, MostrarSaidaDto>()
            .ForCtorParam(nameof(MostrarSaidaDto.Paciente), opt => opt.MapFrom(src => src.Paciente.Nome))
            .ForCtorParam(nameof(MostrarSaidaDto.Itens), opt => opt.MapFrom(src => src.Medicamentos));
        CreateMap<ItemSaida, MostrarItemSaidaDto>()
            .ForCtorParam(nameof(MostrarItemSaidaDto.Medicamento), opt => opt.MapFrom(src => src.Medicamento.Nome));
        // SelectListItem
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
