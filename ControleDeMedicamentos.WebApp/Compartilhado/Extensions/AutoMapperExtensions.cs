using AutoMapper;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Extensions;

public static class AutoMapperExtensions
{
    public static TDestination MapWith<TDestination>(this IMapper mapper, object source, params (string Key, object Value)[] items)
    {
        return mapper.Map<TDestination>(source, opts =>
        {
            foreach (var (key, value) in items)
                opts.Items[key] = value;
        });
    }

    public static void MapFromContext<TSource, TDestination, TMember>(this IMemberConfigurationExpression<TSource, TDestination, TMember> opt, string key)
    {
        opt.MapFrom((src, dest, destMember, context) => (TMember)context.Items[key]);
    }
}
