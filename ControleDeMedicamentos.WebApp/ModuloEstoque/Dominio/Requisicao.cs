using System.Text.Json.Serialization;
using ControleDeMedicamentos.WebApp.Compartilhado.ModuloBase;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$tipoRequisicao")]
[JsonDerivedType(typeof(RequisicaoEntrada), "entrada")]
[JsonDerivedType(typeof(RequisicaoSaida), "saida")]
public abstract class Requisicao : EntidadeBase<Requisicao>
{
    public DateTime Data { get; set; } = DateTime.Now;

    public Requisicao() { }

    public override void Atualizar(Requisicao entidadeAtualizada)
    {
        throw new NotImplementedException();
    }
}
