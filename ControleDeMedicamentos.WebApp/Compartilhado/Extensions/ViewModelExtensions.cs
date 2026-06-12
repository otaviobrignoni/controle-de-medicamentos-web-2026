using System.Linq.Expressions;
using ControleDeMedicamentos.WebApp.Compartilhado.Models;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Extensions;

public static class ViewModelExtensions
{
    public static FormFieldViewModel ConvertToFormField<TModel>(this TModel model, Expression<Func<TModel, string>> expression, string type, string? label = null)
    {
        var name = GetMemberName(expression.Body) ?? throw new ArgumentException($"Could not determine the field name from expression '{expression}'.", nameof(expression)); ;
        var value = expression.Compile()(model);

        return new FormFieldViewModel(name, label ?? name, value, type);
    }

    public static DisplayFieldViewModel DisplayField<TModel, TValue>(this TModel model, Expression<Func<TModel, TValue>> expression, string? label = null)
    {
        var name = GetMemberName(expression.Body);
        if (name is null && label is null)
            throw new ArgumentException($"Could not determine the field name from expression '{expression}'. Provide the label explicitly.", nameof(expression));
        var value = expression.Compile()(model);

        return new((label ?? name)!, value);
    }

    public static DisplayFieldViewModel DisplayField<TModel, TValue>(this TModel model, Expression<Func<TModel, TValue>> expression, object? value, string? label = null)
    {
        var name = GetMemberName(expression.Body);
        if (name is null && label is null)
            throw new ArgumentException($"Could not determine the field name from expression '{expression}'. Provide the label explicitly.", nameof(expression));

        return new((label ?? name)!, value);
    }

    private static string? GetMemberName(Expression expression)
    {
        return expression switch
        {
            MemberExpression member => member.Member.Name,
            MethodCallExpression { Object: not null } method => GetMemberName(method.Object),
            MethodCallExpression { Arguments.Count: > 0 } method => GetMemberName(method.Arguments[0]),
            UnaryExpression unary => GetMemberName(unary.Operand),
            _ => null
        };
    }
}
